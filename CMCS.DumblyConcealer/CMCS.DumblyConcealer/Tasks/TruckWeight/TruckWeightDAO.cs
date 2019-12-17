using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.TruckWeight.Entities;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;

namespace CMCS.DumblyConcealer.Tasks.TruckWeight
{
    public class TruckWeightDAO
    {
        private static TruckWeightDAO instance;

        public static TruckWeightDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new TruckWeightDAO();
            }
            return instance;
        }

        private TruckWeightDAO()
        {

        }

        public int SyncTransport(Action<string, eOutputType> output)
        {
            int res = 0;
            IList<TransportOld> list = DcDbers.GetInstance().TruckWeight_Dber.Entities<TransportOld>("where IsToServer=0 and SuttleWeight>0");
            foreach (TransportOld item in list)
            {
                CmcsBuyFuelTransport entity = CommonDAO.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(item.Id);
                if (entity == null)
                {
                    entity = new CmcsBuyFuelTransport();
                    entity.Id = item.Id;
                    entity.CreateDate = item.CreateDate;
                    entity.OperDate = item.OperDate;
                    entity.OperUser = item.OperUser;
                    entity.CreateUser = item.CreateUser;
                    entity.InFactoryTime = item.InFactoryTime;
                    entity.GrossPlace = item.GrossPlace;
                    entity.GrossTime = item.GrossTime;
                    entity.GrossWeight = item.GrossWeight;
                    entity.TarePlace = item.TarePlace;
                    entity.TareTime = item.TareTime;
                    entity.TareWeight = item.TareWeight;
                    entity.SuttleWeight = item.SuttleWeight;
                    entity.KsWeight = item.KsWeight;
                    entity.KgWeight = item.KgWeight;
                    entity.AutoKsWeight = item.AutoKsWeight;
                    entity.CheckWeight = item.CheckWeight;
                    entity.ProfitAndLossWeight = item.ProfitAndLossWeight;
                    entity.OutFactoryTime = item.OutFactoryTime;
                    entity.MineName = item.MineName;
                    entity.AutotruckId = item.AutotruckId;
                    entity.CarNumber = item.CarNumber;
                    entity.TicketWeight = item.TicketWeight;
                    entity.FuelKindId = item.FuelKindId;
                    entity.FuelKindName = item.FuelKindName;
                    entity.IsUse = item.IsUse;
                    entity.IsFinish = item.IsFinish;
                    entity.StepName = item.StepName;
                    entity.InFactoryBatchId = item.InFactoryBatchId;
                    entity.SerialNumber = item.SerialNumber;
                    res += CommonDAO.GetInstance().SelfDber.Insert(entity);
                    item.IsToServer = 1;
                    DcDbers.GetInstance().TruckWeight_Dber.Update(item);
                }
            }
            output(string.Format("同步运输记录 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
            return res;
        }
    }
}
