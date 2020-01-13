using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.DAO;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.InOutStorage.Entities;
using CMCS.Common.Entities.Fuel;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;


namespace CMCS.DumblyConcealer.Tasks.InOutStorage
{
    public class InOutStorageDAO
    {
        private static InOutStorageDAO instance;

        public static InOutStorageDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new InOutStorageDAO();
            }
            return instance;
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();

        private InOutStorageDAO()
        { }

        public String GetTimeParamter()
        {
            string result = commonDAO.GetParameterByName("煤场快照数据生成时间点");
            return result;
        }

        /// <summary>
        /// 生成煤场快照信息
        /// </summary>
        /// <returns></returns>
        public void CreateInOutStorage(Action<string, eOutputType> output)
        {
            int res = 0;
            //第二天生成前一天的数据
            DateTime dtStart = DateTime.Now.AddDays(-1).Date;
            DateTime dtEnd = DateTime.Now.Date;
            bool isHave = true;
            STGDayInOutStorage entity = commonDAO.SelfDber.Entity<STGDayInOutStorage>(" where RecordDate >= :dtStart and RecordDate < :dtEnd", new { dtStart = dtStart, dtEnd = dtEnd });
            if (entity == null)
            {
                entity = new STGDayInOutStorage() { RecordDate = dtStart };
                isHave = false;
            }
            //第一步、获取当日进煤量
            IList<CmcsInFactoryBatch> batchs = commonDAO.SelfDber.Entities<CmcsInFactoryBatch>("where BackBatchDate >=  :dtStart and BackBatchDate < :dtEnd and IsQty =1", new { dtStart = dtStart, dtEnd = dtEnd });
            if (batchs.Count > 0)
                entity.InQty = batchs.Sum(a => a.CheckQty);
            //第二步、获取当日出库量
            IList<STGDelivery> infurnaces = commonDAO.SelfDber.Entities<STGDelivery>("where RecordDate >= :dtStart and RecordDate < :dtEnd and Status = '1'", new { dtStart = dtStart, dtEnd = dtEnd }).ToList();
            if (infurnaces.Count > 0)
                entity.UseQty = infurnaces.Sum(a => a.TotalQty);
            //第三步、获取当日调整量
            IList<STGAdjustment> sTGAdjustments = commonDAO.SelfDber.Entities<STGAdjustment>("where RecordDate >= :dtStart and RecordDate < :dtEnd and Status ='1'", new { dtStart = dtStart, dtEnd = dtEnd }).ToList();
            if (sTGAdjustments.Count > 0)
                entity.AdjustQty = sTGAdjustments.Sum(a => a.AdjustStocks);
            /*
             * 第四步、获取当日库存量
             * 库存量计算方式：
             *    1、前一天有进耗存统计：当日库存量=昨日库存量 + 当日入厂煤量 - 当日耗用煤量 + 调整量
             *    2、前一天没有进耗存统计：当日库存量=当前实时库存量-当日已堆放量+当日已入炉量
             */
            STGDayInOutStorage lastentity = commonDAO.SelfDber.Entity<STGDayInOutStorage>(" where RecordDate >= :dtStart and RecordDate <:dtEnd ", new { dtStart = dtStart.AddDays(-1), dtEnd = dtEnd.AddDays(-1) });
            if (lastentity != null)
            {
                entity.StorageQty = lastentity.StorageQty + entity.InQty - entity.UseQty + entity.AdjustQty;
            }
            else
            {
                DataTable sTGSelectStorageData = GetAllStorageInfo();
                decimal nowStorageQty = (sTGSelectStorageData == null || sTGSelectStorageData.Rows.Count <= 0) ? 0 : ParseDecimal(sTGSelectStorageData.Rows[0]["VALUE"]);


                //当天的堆放量，按照堆放的创建时间查询
                #region 根据创建时间查询当日已堆放量
                string depoSql = String.Format(@"select sum(t.totalqty)
                  from stgtbdeposit t
                  left join fultbinfactorybatch a on t.infactorybatchid = a.id
                  where a.id is not null and t.isexecute=1 and t.createdate>=:dtStart and t.createdate<=:dtEnd");
                DbParameter[] depoParameters ={
                                              new OracleParameter(":dtStart",dtEnd),
                                              new OracleParameter(":dtEnd",DateTime.Now),
                                         };
                DataTable nowDeposit = commonDAO.SelfDber.ExecuteDataTable(depoSql, depoParameters);
                decimal nowDepositQty = (nowDeposit == null || nowDeposit.Rows.Count <= 0) ? 0 : ParseDecimal(nowDeposit.Rows[0][0]);
                #endregion

                //当天已经入炉的量
                #region 根据创建时间查询当日已耗用量
                string outSql = String.Format(@"select sum(t.totalqty) stgtbdelivery t where  t.recorddate>=:dtStart and t.recorddate<=:dtEnd and t.status = '1'");
                DbParameter[] outParameters ={
                                              new OracleParameter(":dtStart",dtEnd),
                                              new OracleParameter(":dtEnd",DateTime.Now),
                                         };
                DataTable nowOut = commonDAO.SelfDber.ExecuteDataTable(outSql, outParameters);
                decimal nowOutQty = (nowOut == null || nowOut.Rows.Count <= 0) ? 0 : ParseDecimal(nowOut.Rows[0][0]);
                #endregion

                entity.StorageQty = nowStorageQty - nowDepositQty + nowOutQty;
            }
            entity.StorageQty = entity.StorageQty <= 0 ? 0 : entity.StorageQty;

            if (isHave)
                res += commonDAO.SelfDber.Update(entity);
            else
                res += commonDAO.SelfDber.Insert(entity);

            if (res > 0)
                output(string.Format("{0}煤场快照数据，进：{1}，耗{2}，存：{3}，调整：{4}", isHave ? "更新" : "新增", entity.InQty, entity.UseQty, entity.StorageQty, entity.AdjustQty), eOutputType.Normal);
        }


        private DataTable GetAllStorageInfo()
        {
            #region 查询语句
            string sql = @"SELECT SUM(A.QTYHAVE) VALUE,
ROUND(CASE WHEN SUM(CASE WHEN QNETARKCAL>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(QNETARKCAL*A.QTYHAVE)/SUM(CASE WHEN QNETARKCAL>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) QCAL,
ROUND(CASE WHEN SUM(CASE WHEN qnetarmj>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(qnetarmj*A.QTYHAVE)/SUM(CASE WHEN qnetarmj>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) QJ,
ROUND(CASE WHEN SUM(CASE WHEN Mt>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(Mt*A.QTYHAVE)/SUM(CASE WHEN Mt>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) MAR,
ROUND(CASE WHEN SUM(CASE WHEN MAD>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(MAD*A.QTYHAVE)/SUM(CASE WHEN MAD>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) MAD,
ROUND(CASE WHEN SUM(CASE WHEN VAR>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(VAR*A.QTYHAVE)/SUM(CASE WHEN VAR>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) VAR,
ROUND(CASE WHEN SUM(CASE WHEN VAD >0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(VAD *A.QTYHAVE)/SUM(CASE WHEN VAD >0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) VAD ,
ROUND(CASE WHEN SUM(CASE WHEN VD  >0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(VD  *A.QTYHAVE)/SUM(CASE WHEN VD  >0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) VD  ,
ROUND(CASE WHEN SUM(CASE WHEN VDAF>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(VDAF*A.QTYHAVE)/SUM(CASE WHEN VDAF>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) VDAF,
ROUND(CASE WHEN SUM(CASE WHEN STAR>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(STAR*A.QTYHAVE)/SUM(CASE WHEN STAR>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) STAR,
ROUND(CASE WHEN SUM(CASE WHEN STAD>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(STAD*A.QTYHAVE)/SUM(CASE WHEN STAD>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) STAD,
ROUND(CASE WHEN SUM(CASE WHEN STD >0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(STD *A.QTYHAVE)/SUM(CASE WHEN STD >0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) STD ,
ROUND(CASE WHEN SUM(CASE WHEN AAR>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(AAR*A.QTYHAVE)/SUM(CASE WHEN AAR>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) AAR,
ROUND(CASE WHEN SUM(CASE WHEN AAD>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(AAD*A.QTYHAVE)/SUM(CASE WHEN AAD>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) AAD,
ROUND(CASE WHEN SUM(CASE WHEN AD >0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(AD *A.QTYHAVE)/SUM(CASE WHEN AD >0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) AD ,
ROUND(CASE WHEN SUM(CASE WHEN ST>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(ST*A.QTYHAVE)/SUM(CASE WHEN ST>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) ST,
ROUND(CASE WHEN SUM(CASE WHEN FUELPRICE>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(FUELPRICE*A.QTYHAVE)/SUM(CASE WHEN FUELPRICE>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) FUELPRICE 
FROM (SELECT E.INMIDDLEID,SUM(E.QTYHAVE) QTYHAVE
FROM STGTBINMINQTY E WHERE E.QTYHAVE>0 GROUP BY E.INMIDDLEID) A 
LEFT JOIN STGTBALLINMIDDLE B ON A.INMIDDLEID=B.ID LEFT JOIN FULTBFUELQUALITY C ON B.FUELQUALITYID=C.ID";
            #endregion
            return commonDAO.SelfDber.ExecuteDataTable(sql);
        }

        private Decimal ParseDecimal(object obj)
        {
            Decimal result = 0m;
            if (obj != null)
                Decimal.TryParse(obj.ToString(),out result);
            return result;
        }
    }
}