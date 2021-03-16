using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.DapperDber.Util;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.DAO;
using CMCS.Common.Entities.AutoCupboard;

namespace CMCS.WeighCheck.DAO
{
	public class CZYHandlerDAO
	{
		private static CZYHandlerDAO instance;

		public static CZYHandlerDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CZYHandlerDAO();
			}

			return instance;
		}

		private CZYHandlerDAO()
		{ }

		#region 获取配置信息

		#endregion
		#region 采样前样桶登记
		/// <summary>
		/// 新增采样明细
		/// </summary>
		/// <param name="sampleId"></param>
		/// <returns></returns>
		public bool SaveSampleDetail(string sampleId, double weight)
		{
			CmcsRCSampling sample = Dbers.GetInstance().SelfDber.Get<CmcsRCSampling>(sampleId);
			if (sample != null)
			{
				CmcsRCSampleBarrel sampleBarrel = new CmcsRCSampleBarrel();
				sampleBarrel.SamplingId = sampleId;
				sampleBarrel.InFactoryBatchId = sample.InFactoryBatchId;
				sampleBarrel.BarrellingTime = DateTime.Now;
				sampleBarrel.SampSecondCode = CommonDAO.GetInstance().CreateSampleDetailCode(sample.SampleCode);
				sampleBarrel.BarrelCode = sampleBarrel.SampSecondCode;
				sampleBarrel.SampleWeight = weight;
				sampleBarrel.SampleType = "人工采样";
				sampleBarrel.SamplerName = "人工";
				return Dbers.GetInstance().SelfDber.Insert(sampleBarrel) > 0;
			}
			return false;
		}

		/// <summary>
		/// 获取指定日期的批次
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public IList<CmcsInFactoryBatch> GetInFactoryBatchByDate(DateTime startDate, DateTime endDate)
		{
			return Dbers.GetInstance().SelfDber.Entities<CmcsInFactoryBatch>("where BACKBATCHDATE>=:StartDate and BACKBATCHDATE<:EndDate", new { StartDate = startDate, EndDate = endDate });
		}

		/// <summary>
		/// 根据批次Id获取采样单
		/// </summary>
		/// <param name="batchId"></param>
		/// <returns></returns>
		public IList<CmcsRCSampling> GetSamplingByBatchId(string batchId)
		{
			return Dbers.GetInstance().SelfDber.Entities<CmcsRCSampling>("where InFactoryBatchId=:InFactoryBatchId order by CreateDate desc", new { InFactoryBatchId = batchId });
		}

		/// <summary>
		/// 根据批次Id获取人工采样单
		/// </summary>
		/// <param name="batchId"></param>
		/// <returns></returns>
		public IList<CmcsRCSampling> GetRGSamplingByBatchId(string batchId)
		{
			return Dbers.GetInstance().SelfDber.Entities<CmcsRCSampling>("where InFactoryBatchId=:InFactoryBatchId and SamplingType='人工采样' order by CreateDate desc", new { InFactoryBatchId = batchId });
		}
		#endregion

		#region 采样后样桶称重登记
		/// <summary>
		/// 查找样桶登记记录
		/// </summary>
		/// <param name="BarrelCode">样桶编码</param>
		public CmcsRCSampleBarrel GetRCSampleBarrel(string BarrelCode, out string message)
		{
			message = string.Empty;
			CmcsRCSampleBarrel entity = Dbers.GetInstance().SelfDber.Entity<CmcsRCSampleBarrel>(" where SampSecondCode='" + BarrelCode + "' and IsDeleted=0 order by BarrellingTime desc");
			if (entity == null)
				message = "未找到编码【" + BarrelCode + "】的样桶登记记录";
			return entity;
		}

		/// <summary>
		/// 保存样桶登记记录(人工样 采样第一次称重)
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool SaveRCSampleBarrel(CmcsRCSampleBarrel entity)
		{
			return Dbers.GetInstance().SelfDber.Insert<CmcsRCSampleBarrel>(entity) > 0 ? true : false;
		}

		/// <summary>
		/// 记录样桶校验记录(机器样 采样第一次称重)
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool UpdateRCSampleBarrelSampleWeight(string rCSampleBarrelId, double weight)
		{
			return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCSampleBarrel>() + " set SampleWeight=:SampleWeight where Id=:Id", new { SampleWeight = weight, Id = rCSampleBarrelId }) > 0;
		}

		/// <summary>
		/// 获取采样单信息
		/// </summary>
		/// <param name="dtStart">开始时间</param>
		/// <param name="dtEnd">结束时间</param>
		/// <returns></returns>
		public DataTable GetSampleInfo(DateTime dtStart, DateTime dtEnd)
		{
			string sql = @" select a.batch,a.id as batchid,
                                             b.name as suppliername,
                                             c.name as minename,
                                             d.name as kindname,
                                             e.name as stationname,
                                             a.factarrivedate,
                                             t.id,
                                             t.samplecode,
                                             t.samplingdate,
                                             t.samplingtype
                                        from cmcstbrcsampling t
                                        left join fultbinfactorybatch a on t.infactorybatchid = a.id
                                        left join fultbsupplier b on a.supplierid = b.id
                                        left join fultbmine c on a.mineid = c.id
                                        left join fultbfuelkind d on a.fuelkindid = d.id
                                        left join fultbstation e on a.stationid = e.id
                                   where t.samplingdate >= to_date('" + dtStart + "','yyyy-MM-dd HH24:MI:SS') and t.samplingdate<to_date('" + dtEnd + "','yyyy-MM-dd HH24:MI:SS')";

			return Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
		}

		/// <summary>
		/// 保存交接样记录的交样
		/// </summary>
		/// <returns></returns>
		public bool SaveHandSamplingReceive(string sampleId, string makeReceivePle, DateTime makeReceiveDate)
		{
			CmcsRCSampling sampling = Dbers.GetInstance().SelfDber.Get<CmcsRCSampling>(sampleId);
			CmcsRCHandSampling handSampling = Dbers.GetInstance().SelfDber.Entity<CmcsRCHandSampling>("where SamplingId=:SamplingId order by CreateDate desc", new { SamplingId = sampleId });
			if (handSampling == null)
			{
				handSampling = new CmcsRCHandSampling();
				handSampling.SamplingSendPle = sampling != null ? sampling.SamplingPle : "";
				handSampling.SamplingSendDate = sampleId != null ? sampling.SamplingDate : DateTime.MinValue;
				handSampling.SamplingId = sampleId;
				handSampling.MakeReceivePle = makeReceivePle;
				handSampling.MakeReceiveDate = makeReceiveDate;
				return Dbers.GetInstance().SelfDber.Insert(handSampling) > 0;
			}
			CmcsRCMake make = Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where SamplingId=:SamplingId order by Createdate desc", new { SamplingId = sampleId });
			if (make != null)
			{
				make.GetPle = makeReceivePle;
				make.GetDate = makeReceiveDate;
				make.IsHandOver = 1;
				Dbers.GetInstance().SelfDber.Update(make);
			}
			handSampling.MakeReceivePle = makeReceivePle;
			handSampling.MakeReceiveDate = makeReceiveDate;
			return Dbers.GetInstance().SelfDber.Update(handSampling) > 0;
		}

		#endregion

		#region 制样前样桶称重校验
		/// <summary>
		/// 根据入厂煤采样单Id获取制样记录
		/// </summary>
		/// <param name="sampleId">采样单Id</param>
		/// <returns></returns>
		public CmcsRCMake GetRCMakeBySampleId(string sampleId)
		{
			return Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where SamplingId=:SamplingId and IsDeleted=0", new { SamplingId = sampleId });
		}

		/// <summary>
		/// 查找同一采样单的样桶登记记录
		/// </summary>
		/// <param name="BarrelCode"></param>
		/// <returns></returns>
		public List<CmcsRCSampleBarrel> GetRCSampleBarrels(string BarrelCode, out string message)
		{
			message = string.Empty;
			List<CmcsRCSampleBarrel> list = new List<CmcsRCSampleBarrel>();
			CmcsRCSampleBarrel entity = GetRCSampleBarrel(BarrelCode, out message);
			if (entity != null)
			{
				list = Dbers.GetInstance().SelfDber.Entities<CmcsRCSampleBarrel>(" where SamplingId='" + entity.SamplingId + "' and IsDeleted=0");
				message = "扫码成功，该批次采样桶共" + list.Count + "桶";
			}
			return list;
		}

		/// <summary>
		/// 记录样桶校验记录(采样第二次称重)
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool UpdateRCSampleBarrelCheckSampleWeight(string rCSampleBarrelId, double weight)
		{
			return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCSampleBarrel>() + " set CheckSampleWeight=:CheckSampleWeight where Id=:Id", new { CheckSampleWeight = weight, Id = rCSampleBarrelId }) > 0;
		}
		#endregion

		#region 制样后样品称重登记
		/// <summary>
		/// 根据制样码获取制样主表信息
		/// </summary>
		/// <param name="makeCode">制样码</param>
		/// <returns></returns>
		public CmcsRCMake GetRCMake(string makeCode)
		{
			return Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>(" where MakeCode=:MakeCode and IsDeleted=0 order by CreateDate desc", new { MakeCode = makeCode });
		}

		/// <summary>
		/// 根据制样码获取制样从表明细集合
		/// </summary>
		/// <param name="makeCode">制样码</param>
		/// <param name="message"></param>
		/// <returns></returns>
		public List<CmcsRCMakeDetail> GetRCMakeDetails(string makeCode, ref string makeId, ref bool res, out string message)
		{
			message = "扫码成功";
			res = false;

			List<CmcsRCMakeDetail> list = new List<CmcsRCMakeDetail>();
			CmcsRCMake rcmake = GetRCMake(makeCode);
			if (rcmake != null)
			{
				makeId = rcmake.Id;

				list = Dbers.GetInstance().SelfDber.Entities<CmcsRCMakeDetail>(" where MakeId=:MakeId and IsDeleted=0 order by CreateDate asc", new { MakeId = rcmake.Id });

				res = true;
			}
			else
				message = "未找到制样记录，制样码：" + makeCode;

			return list;
		}

		/// <summary> 
		/// 更新制样明细记录的样重和样罐编码
		/// </summary>
		/// <param name="rCMakeDetailId">制样明细记录Id</param>
		/// <param name="weight">重量</param>
		/// <param name="barrelCode">样罐编码</param>
		/// <returns></returns>
		public bool UpdateMakeDetailWeightAndBarrelCode(string rCMakeDetailId, double weight, string barrelCode)
		{
			return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCMakeDetail>() + " set SampleWeight=:SampleWeight,SampleCode=:SampleCode where Id=:Id", new { Id = rCMakeDetailId, SampleWeight = weight, SampleCode = barrelCode }) > 0;
		}
		#endregion

		#region 化验前样品称重校验
		/// <summary>
		/// 获取制样从表明细记录
		/// </summary>
		/// <param name="MakeCode">制样码</param>
		/// <returns></returns>
		public CmcsRCMakeDetail GetRCMakeDetail(string barrelCode, out string message)
		{
			CmcsRCMakeDetail rcMakeDetail = null;
			try
			{
				rcMakeDetail = Dbers.GetInstance().SelfDber.Entity<CmcsRCMakeDetail>(" where SampleCode=:SampleCode and IsDeleted=0 order by CreateDate desc", new { SampleCode = barrelCode });
				if (rcMakeDetail == null)
				{
					message = "未找到【" + barrelCode + "】制样登记记录";
					return null;
				}
			}
			catch (Exception ex)
			{
				message = "程序异常！" + ex.Message;
				return null;
			}
			message = "扫码成功";
			return rcMakeDetail;
		}

		/// <summary>
		/// 根据制样明细Id查找化验记录
		/// </summary>
		/// <param name="rCMakeDetailId">制样明细Id</param>
		/// <returns></returns>
		public CmcsRCAssay GetRCAssayByMakeCode(string rCMakeDetailId)
		{
			CmcsRCMakeDetail rCMakeDetail = Dbers.GetInstance().SelfDber.Get<CmcsRCMakeDetail>(rCMakeDetailId);
			if (rCMakeDetail != null)
			{
				string AssayTypes = CommonDAO.GetInstance().GetAppletConfigString("化验样品类型");
				// 三级编码化验查询
				//if (rCMakeDetail.SampleType == eMakeSampleType.Type1 || rCMakeDetail.SampleType == eMakeSampleType.Type3)
				if (AssayTypes.Contains(rCMakeDetail.SampleType))
					return Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId and IsDeleted=0 order by CreateDate desc", new { MakeId = rCMakeDetail.MakeId });
				// 不同类型的化验查询
				//else if(rCMakeDetail.SampleType==eMakeSampleType.Type2)
			}

			return null;
		}

		/// <summary> 
		/// 更新制样明细记录的校验样重
		/// </summary>
		/// <param name="rCMakeDetailId">制样明细记录Id</param>
		/// <param name="weight">重量</param>
		/// <returns></returns>
		public bool UpdateMakeDetailCheckWeight(string rCMakeDetailId, double weight)
		{
			return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCMakeDetail>() + " set CheckWeight=:CheckWeight where Id=:Id", new { Id = rCMakeDetailId, CheckWeight = weight }) > 0;
		}

		/// <summary>
		/// 解绑化验单及化验操作
		/// </summary>
		/// <param name="makeCode"></param>
		/// <returns></returns>
		public bool RelieveAssay(CmcsRCMakeDetail makeDetail, string makeCreateUser, string assayCheckUser)
		{
			CmcsRCAssay assay = Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId order by CreateDate desc", new { MakeId = makeDetail.MakeId });
			if (assay != null)
			{
				assay.IsRelieve = 1;

				assay.GetPle = assayCheckUser;
				assay.SendPle = makeCreateUser;
				if (assay.AssayType == "三级编码化验" && makeDetail.SampleType.ToLower().Contains("6mm"))
					assay.SendDate = DateTime.Now;
				else if ((assay.AssayType == "复查样化验" || assay.AssayType == "抽查样化验") && makeDetail.SampleType.ToLower().Contains("0.2mm"))
					assay.SendDate = DateTime.Now;
				if (makeDetail.SampleType.ToLower().Contains("0.2mm") && assay.GetDate.Year < 2000)
				{
					assay.GetDate = DateTime.Now;
				}
				if (makeDetail.PrintCount == 0)
				{
					if (string.IsNullOrEmpty(assay.GetPle))
						assay.GetPle = assayCheckUser;
					else
						assay.GetPle += "," + assayCheckUser;
				}
				return Dbers.GetInstance().SelfDber.Update<CmcsRCAssay>(assay) > 0;
			}
			return false;
		}

		#endregion

		#region 存样柜
		/// <summary>
		/// 存样
		/// </summary>
		/// <param name="cupBoardNumber"></param>
		/// <param name="saveUser"></param>
		/// <param name="sampleCode"></param>
		/// <returns></returns>
		public bool SaveCupBoard(int cupBoardNumber, string saveUser, string sampleCode)
		{
			CmcsCupCoardSave saveBoard = CommonDAO.GetInstance().SelfDber.Entity<CmcsCupCoardSave>("where CupCoardNumber=:CupCoardNumber and IsUse=1", new { CupCoardNumber = cupBoardNumber });
			if (saveBoard == null)
			{
				saveBoard = new CmcsCupCoardSave();
				saveBoard.CupCoardNumber = cupBoardNumber;
				saveBoard.SaveCount++;
				saveBoard.IsUse = 1;
				saveBoard.UpdateTime = DateTime.Now;
				CommonDAO.GetInstance().SelfDber.Insert(saveBoard);
			}
			else
			{
				saveBoard.SaveCount++;
				saveBoard.UpdateTime = DateTime.Now;
				CommonDAO.GetInstance().SelfDber.Update(saveBoard);
			}
			CmcsCupCoardSaveDetail saveDetail = new CmcsCupCoardSaveDetail();
			saveDetail.CupCoardSaveId = saveBoard.Id;
			saveDetail.SampleCode = sampleCode;
			saveDetail.SaveUser = saveUser;
			saveDetail.IsTake = 0;
			saveDetail.SaveTime = DateTime.Now;
			return CommonDAO.GetInstance().SelfDber.Insert(saveDetail) > 0;
		}

		/// <summary>
		/// 取样
		/// </summary>
		/// <param name="cupBoardNumber"></param>
		/// <param name="takeUser"></param>
		/// <param name="sampleCode"></param>
		/// <returns></returns>
		public bool TakeCupBoard(int cupBoardNumber, string takeUser, string sampleCode)
		{
			CmcsCupCoardSave saveBoard = CommonDAO.GetInstance().SelfDber.Entity<CmcsCupCoardSave>("where CupCoardNumber=:CupCoardNumber and IsUse=1", new { CupCoardNumber = cupBoardNumber });
			if (saveBoard != null)
			{
				saveBoard.SaveCount--;
				saveBoard.UpdateTime = DateTime.Now;
				CommonDAO.GetInstance().SelfDber.Update(saveBoard);
			}
			CmcsCupCoardSaveDetail saveDetail = CommonDAO.GetInstance().SelfDber.Entity<CmcsCupCoardSaveDetail>("where SampleCode=:SampleCode and IsTake=0", new { SampleCode = sampleCode });
			if (saveDetail != null)
			{
				saveDetail.TakeUser = takeUser;
				saveDetail.IsTake = 1;
				saveDetail.TakeTime = DateTime.Now;
				return CommonDAO.GetInstance().SelfDber.Update(saveDetail) > 0;
			}

			return false;
		}

		/// <summary>
		/// 初始化样柜
		/// </summary>
		public int InitCupBoard()
		{
			int res = 0;
			for (int i = 1; i <= 420; i++)
			{
				CmcsCupCoardSave saveBoard = CommonDAO.GetInstance().SelfDber.Entity<CmcsCupCoardSave>("where CupCoardNumber=:CupCoardNumber", new { CupCoardNumber = i });
				if (saveBoard == null)
				{
					saveBoard = new CmcsCupCoardSave();
					saveBoard.CupCoardNumber = i;
					saveBoard.IsUse = 1;
					saveBoard.UpdateTime = DateTime.Now;
					res += CommonDAO.GetInstance().SelfDber.Insert(saveBoard);
				}
			}
			return res;
		}
		#endregion
	}
}
