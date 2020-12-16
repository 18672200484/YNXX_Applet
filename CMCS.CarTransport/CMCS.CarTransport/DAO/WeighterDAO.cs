using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.Views;
using CMCS.DapperDber.Util;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Common.DapperDber_etc;

namespace CMCS.CarTransport.DAO
{
	/// <summary>
	/// 汽车过衡业务
	/// </summary>
	public class WeighterDAO
	{
		private static WeighterDAO instance;

		public static WeighterDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new WeighterDAO();
			}

			return instance;
		}

		private WeighterDAO()
		{ }

		public OracleDapperDber_iEAA SelfDber
		{
			get { return Dbers.GetInstance().SelfDber; }
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();
		CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();

		#region 入厂煤业务

		/// <summary>
		/// 获取指定日期已完成的入厂煤运输记录
		/// </summary>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public List<View_BuyFuelTransport> GetFinishedBuyFuelTransport(DateTime dtStart, DateTime dtEnd)
		{
			return SelfDber.Entities<View_BuyFuelTransport>("where SuttleWeight!=0 and IsUse=1 and ((InFactoryTime>=:dtStart and InFactoryTime<:dtEnd) or (TareTime>=:dtStart and TareTime<:dtEnd)) order by taretime desc,grosstime desc,infactorytime desc", new { dtStart = dtStart, dtEnd = dtEnd });
		}

		/// <summary>
		/// 获取未完成的入厂煤运输记录
		/// </summary>
		/// <returns></returns>
		public List<View_BuyFuelTransport> GetUnFinishBuyFuelTransport()
		{
			return SelfDber.Entities<View_BuyFuelTransport>("where SuttleWeight=0 and IsUse=1 order by InFactoryTime desc");
		}

		/// <summary>
		/// 保存入厂煤运输记录
		/// </summary>
		/// <param name="transportId"></param>
		/// <param name="weight">重量</param>
		/// <param name="place"></param>
		/// <returns></returns>
		public bool SaveBuyFuelTransport(string transportId, decimal weight, DateTime dt, string place)
		{
			CmcsBuyFuelTransport transport = SelfDber.Get<CmcsBuyFuelTransport>(transportId);
			if (transport == null) return false;
			if (weight <= 0)
			{
				Log4Neter.Error("称重失败", new Exception("称重时重量为0"));
				return false;
			}
			//根据当前流程节点名称判断
			if (transport.GrossWeight == 0)
			{
				transport.StepName = eTruckInFactoryStep.重车.ToString();
				transport.GrossWeight = weight;
				transport.GrossPlace = place;
				transport.GrossTime = dt;
				transport.SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(eCarType.入厂煤, dt);
			}
			else if (transport.TareWeight == 0)
			{
				transport.StepName = eTruckInFactoryStep.轻车.ToString();
				transport.TareWeight = weight;
				transport.TarePlace = place;
				transport.TareTime = dt;
				transport.SuttleWeight = transport.GrossWeight - transport.TareWeight;
				////验收量大于票重时多余的量算到扣吨
				if (transport.TicketWeight != 0)
				{
					decimal deduct = transport.SuttleWeight > transport.TicketWeight ? (transport.SuttleWeight - transport.TicketWeight) : 0;
					decimal letterdeduct = 0;//抹去的小数位
											 //transport.SuttleWeight -= deduct;
					transport.CheckWeight = OneDigit(transport.SuttleWeight - deduct - transport.KsWeight - transport.KgWeight, ref letterdeduct);
					deduct += letterdeduct;
					transport.AutoKsWeight = deduct;
					transport.DeductWeight = transport.AutoKsWeight + transport.KsWeight + transport.KgWeight;
				}
				else
				{
					transport.DeductWeight = transport.KsWeight + transport.KgWeight;
					transport.CheckWeight = transport.SuttleWeight - transport.DeductWeight;
				}
				transport.ProfitAndLossWeight = transport.CheckWeight - transport.TicketWeight;

				// 回皮即完结
				transport.IsFinish = 1;
				transport.IsSyncBatch = 0;

				//流程结束时删除临时运输记录
				CmcsUnFinishTransport unFinishTransport = SelfDber.Entity<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = transportId });
				if (unFinishTransport != null)
					SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
			}
			else
				return false;

			transport.IsSynch = 0;

			return SelfDber.Update(transport) > 0;
		}

		/// <summary>
		/// 手动保存重量
		/// </summary>
		/// <param name="transport"></param>
		/// <returns></returns>
		public bool SaveBuyFuelTransportHand(CmcsBuyFuelTransport transport)
		{
			if (transport == null) return false;
			if (transport.GrossWeight > 0)
			{
				if (transport.GrossTime.Year < 2000) { transport.GrossTime = DateTime.Now; transport.InFactoryTime = DateTime.Now; }
				if (transport.TareWeight > 0)
				{
					if (transport.TareTime.Year < 2000) transport.TareTime = DateTime.Now;

					transport.SuttleWeight = transport.GrossWeight - transport.TareWeight;
					////验收量大于票重时多余的量算到扣吨
					if (transport.TicketWeight != 0)
					{
						decimal deduct = transport.SuttleWeight > transport.TicketWeight ? (transport.SuttleWeight - transport.TicketWeight) : 0;
						decimal letterdeduct = 0;//抹去的小数位
												 //transport.SuttleWeight -= deduct;
						transport.CheckWeight = OneDigit(transport.SuttleWeight - deduct - transport.KsWeight - transport.KgWeight, ref letterdeduct);
						deduct += letterdeduct;
						transport.AutoKsWeight = deduct;
						transport.DeductWeight = transport.AutoKsWeight + transport.KsWeight + transport.KgWeight;
					}
					else
					{
						transport.AutoKsWeight = 0;
						transport.DeductWeight = transport.KsWeight + transport.KgWeight;
						transport.CheckWeight = transport.SuttleWeight - transport.DeductWeight;
					}
					transport.ProfitAndLossWeight = transport.CheckWeight - transport.TicketWeight;
					transport.IsFinish = 1;
					transport.IsSyncBatch = 0;
					//流程结束时删除临时运输记录
					SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = transport.Id });
				}
			}
			if (transport.IsFinish == 0)
			{
				CmcsUnFinishTransport unFinishTransport = SelfDber.Entity<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = transport.Id });
				if (unFinishTransport == null)
				{
					unFinishTransport = new CmcsUnFinishTransport();
					unFinishTransport.TransportId = transport.Id;
					unFinishTransport.CarType = eCarType.入厂煤.ToString();
					unFinishTransport.AutotruckId = transport.AutotruckId;
					unFinishTransport.PrevPlace = "未知";
					SelfDber.Insert(unFinishTransport);
				}
			}

			transport.IsSynch = 0;

			return SelfDber.Update(transport) > 0;
		}

		/// <summary>
		/// 舍去第二位小数，无论大小
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		decimal OneDigit(decimal value, ref decimal deductvalue)
		{
			decimal result = Math.Floor(value * 10) / 10m;
			deductvalue = value - result;
			return result;
		}

		/// <summary>
		/// 获取扣吨量
		/// </summary>
		/// <param name="transportId"></param>
		/// <returns></returns>
		public decimal GetDeductWeight(string transportId)
		{
			decimal DeductWeight = 0;
			List<CmcsBuyFuelTransportDeduct> listDeducts = SelfDber.Entities<CmcsBuyFuelTransportDeduct>("where TransportId=:TransportId", new { TransportId = transportId });
			if (listDeducts.Count > 0)
				DeductWeight = listDeducts.Sum(a => a.DeductWeight);

			return DeductWeight;
		}

		#endregion

		#region 销售煤业务

		/// <summary>
		/// 获取指定日期已完成的销售煤运输记录
		/// </summary>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public List<View_SaleFuelTransport> GetFinishedSaleFuelTransport(DateTime dtStart, DateTime dtEnd)
		{
			return SelfDber.Entities<View_SaleFuelTransport>("where SuttleWeight!=0 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
		}

		/// <summary>
		/// 获取未完成的销售煤运输记录
		/// </summary>
		/// <returns></returns>
		public List<View_SaleFuelTransport> GetUnFinishSaleFuelTransport()
		{
			return SelfDber.Entities<View_SaleFuelTransport>("where SuttleWeight=0 and IsUse=1 and UnFinishTransportId is not null order by InFactoryTime desc");
		}

		/// <summary>
		/// 保存销售煤运输记录
		/// </summary>
		/// <param name="transportId"></param>
		/// <param name="weight">重量</param>
		/// <param name="place"></param>
		/// <returns></returns>
		public bool SaveSaleFuelTransport(string transportId, decimal weight, DateTime dt, string place)
		{
			CmcsSaleFuelTransport transport = SelfDber.Get<CmcsSaleFuelTransport>(transportId);
			if (transport == null) return false;

			if (transport.TareWeight == 0)
			{
				transport.StepName = eTruckInFactoryStep.轻车.ToString();
				transport.TareWeight = weight;
				transport.TarePlace = place;
				transport.TareTime = dt;
			}
			else if (transport.GrossWeight == 0)
			{
				transport.StepName = eTruckInFactoryStep.重车.ToString();
				transport.GrossWeight = weight;
				transport.GrossPlace = place;
				transport.GrossTime = dt;
				transport.SuttleWeight = transport.GrossWeight - transport.TareWeight;

				// 回皮即完结
				transport.IsFinish = 1;

				//commonDAO.InsertWaitForHandleEvent("汽车智能化_同步销售煤运输记录到批次", transport.Id);
			}
			else
				return false;

			return SelfDber.Update(transport) > 0;
		}
		#endregion

		#region 其他物资业务

		/// <summary>
		/// 获取指定日期已完成的其他物资运输记录
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public List<CmcsGoodsTransport> GetFinishedGoodsTransport(DateTime dtStart, DateTime dtEnd)
		{
			return SelfDber.Entities<CmcsGoodsTransport>("where SuttleWeight>0 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
		}

		/// <summary>
		/// 获取未完成的其他物资运输记录
		/// </summary>
		/// <returns></returns>
		public List<CmcsGoodsTransport> GetUnFinishGoodsTransport()
		{
			return SelfDber.Entities<CmcsGoodsTransport>("where SuttleWeight=0 and IsUse=1 and Id in (select TransportId from " + EntityReflectionUtil.GetTableName<CmcsUnFinishTransport>() + " where CarType=:CarType) order by InFactoryTime desc", new { CarType = eCarType.其他物资.ToString() });
		}

		/// <summary>
		/// 保存其他物资运输记录
		/// </summary>
		/// <param name="transportId"></param>
		/// <param name="weight">重量</param>
		/// <param name="place"></param>
		/// <returns></returns>
		public bool SaveGoodsTransport(string transportId, decimal weight, DateTime dt, string place)
		{
			CmcsGoodsTransport transport = SelfDber.Get<CmcsGoodsTransport>(transportId);
			if (transport == null) return false;

			if (transport.FirstWeight == 0)
			{
				transport.StepName = eTruckInFactoryStep.重车.ToString();
				transport.FirstWeight = weight;
				transport.FirstPlace = place;
				transport.FirstTime = dt;
			}
			else if (transport.SecondWeight == 0)
			{
				transport.StepName = eTruckInFactoryStep.轻车.ToString();
				transport.SecondWeight = weight;
				transport.SecondPlace = place;
				transport.SecondTime = dt;
				transport.SuttleWeight = Math.Abs(transport.FirstWeight - transport.SecondWeight);

				// 回皮即完结
				transport.IsFinish = 1;
			}
			else
				return false;

			return SelfDber.Update(transport) > 0;
		}


		#endregion
	}
}
