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
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Enums;

namespace CMCS.CarTransport.DAO
{
	/// <summary>
	/// 汽车入厂排队业务
	/// </summary>
	public class QueuerDAO
	{
		private static QueuerDAO instance;

		public static QueuerDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new QueuerDAO();
			}

			return instance;
		}

		private QueuerDAO()
		{ }

		public OracleDapperDber SelfDber
		{
			get { return Dbers.GetInstance().SelfDber; }
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();
		CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();

		#region 入厂煤业务

		/// <summary>
		/// 生成入厂煤运输排队记录，同时生成批次信息以及采制化三级编码
		/// </summary>
		/// <param name="autotruck">车</param>
		/// <param name="supplier">供煤单位</param>
		/// <param name="mine">矿点</param>
		/// <param name="transportCompany">运输单位</param>
		/// <param name="fuelKind">煤种</param>
		/// <param name="ticketWeight">矿发量</param>
		/// <param name="inFactoryTime">入厂时间</param>
		/// <param name="remark">备注</param>
		/// <param name="place">地点</param>
		/// <param name="samplingType">采样方式</param> 
		/// <returns></returns>
		public bool JoinQueueBuyFuelTransport(CmcsAutotruck autotruck, CmcsSupplier supplier, CmcsMine mine, CmcsTransportCompany transportCompany, CmcsFuelKind fuelKind, decimal ticketWeight, DateTime inFactoryTime, string remark, string place)
		{
			CmcsBuyFuelTransport transport = new CmcsBuyFuelTransport
			{
				//SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(eCarType.入厂煤, inFactoryTime),
				AutotruckId = autotruck.Id,
				CarNumber = autotruck.CarNumber,
				SupplierId = supplier.Id,
				SupplierName = supplier.Name,
				MineId = mine != null ? mine.Id : "",
				MineName = mine != null ? mine.Name : "",
				TransportCompanyId = transportCompany != null ? transportCompany.Id : "",
				TransportCompanyName = transportCompany != null ? transportCompany.Name : "",
				FuelKindId = fuelKind.Id,
				FuelKindName = fuelKind.Name,
				TicketWeight = ticketWeight,
				InFactoryTime = inFactoryTime,
				IsFinish = 0,
				IsUse = 1,
				StepName = eTruckInFactoryStep.入厂.ToString(),
				Remark = remark,
				SamplingType = eSamplingType.机械采样.ToString()
			};
			// 生成批次以及采制化三级编码数据 
			CmcsInFactoryBatch inFactoryBatch = carTransportDAO.GCQCInFactoryBatchByBuyFuelTransport(transport);

			if (SelfDber.Insert(transport) > 0)
			{
				// 插入未完成运输记录
				return SelfDber.Insert(new CmcsUnFinishTransport
				{
					TransportId = transport.Id,
					CarType = eCarType.入厂煤.ToString(),
					AutotruckId = autotruck.Id,
					PrevPlace = place,
				}) > 0;
			}

			return false;
		}

		/// <summary>
		/// 获取指定日期已完成的入厂煤运输记录
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public List<View_BuyFuelTransport> GetFinishedBuyFuelTransport(DateTime dtStart, DateTime dtEnd)
		{
			return SelfDber.Entities<View_BuyFuelTransport>("where IsFinish=1 and ((InFactoryTime>=:dtStart and InFactoryTime<:dtEnd) or (TareTime>=:dtStart and TareTime<:dtEnd)) order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
		}

		/// <summary>
		/// 获取未完成的入厂煤运输记录
		/// </summary>
		/// <returns></returns>
		public List<View_BuyFuelTransport> GetUnFinishBuyFuelTransport()
		{
			return SelfDber.Entities<View_BuyFuelTransport>("where IsFinish=0 and IsUse=1 order by InFactoryTime desc");
		}

		/// <summary>
		/// 更改入厂煤运输记录的无效状态
		/// </summary>
		/// <param name="buyFuelTransportId"></param>
		/// <param name="isValid">是否有效</param>
		/// <returns></returns>
		public bool ChangeBuyFuelTransportToInvalid(string buyFuelTransportId, bool isValid)
		{
			if (isValid)
			{
				// 设置为有效
				CmcsBuyFuelTransport buyFuelTransport = SelfDber.Get<CmcsBuyFuelTransport>(buyFuelTransportId);
				if (buyFuelTransport != null)
				{
					if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsBuyFuelTransport>() + " set IsUse=1 where Id=:Id", new { Id = buyFuelTransportId }) > 0)
					{
						if (buyFuelTransport.IsFinish == 0)
						{
							SelfDber.Insert(new CmcsUnFinishTransport
							{
								AutotruckId = buyFuelTransport.AutotruckId,
								CarType = eCarType.入厂煤.ToString(),
								TransportId = buyFuelTransport.Id,
								PrevPlace = "未知"
							});
						}

						return true;
					}
				}
			}
			else
			{
				// 设置为无效

				if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsBuyFuelTransport>() + " set IsUse=0 where Id=:Id", new { Id = buyFuelTransportId }) > 0)
				{
					SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = buyFuelTransportId });

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 根据车牌号获取指定到达日期的入厂煤来煤预报
		/// </summary>
		/// <param name="carNumber">车牌号</param>
		/// <param name="inFactoryTime">预计到达日期</param>
		/// <returns></returns>
		public List<CmcsLMYB> GetBuyFuelForecastByCarNumber(string carNumber, DateTime inFactoryTime)
		{
			return SelfDber.Query<CmcsLMYB>("select l.* from " + EntityReflectionUtil.GetTableName<CmcsLMYBDetail>() + " ld left join " + EntityReflectionUtil.GetTableName<CmcsLMYB>() + " l on l.Id=ld.lmybid where ld.CarNumber=:CarNumber and to_char(InFactoryTime,'yyyymmdd')=to_char(:InFactoryTime,'yyyymmdd') order by l.InFactoryTime desc",
				new { CarNumber = carNumber.Trim(), InFactoryTime = inFactoryTime }).ToList();
		}

		#endregion

		#region 销售煤业务
		public bool JoinQueueSaleFuelTransport(CmcsAutotruck autotruck, CmcsTransportSales transportsales, DateTime inFactoryTime, string remark, string place, string loadarea)
		{
			CmcsSaleFuelTransport transport = new CmcsSaleFuelTransport
			{
				SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(eCarType.销售煤, inFactoryTime),
				AutotruckId = autotruck.Id,
				CarNumber = autotruck.CarNumber,
				TransportSalesNum = transportsales.YbNum,
				TransportSalesId = transportsales.Id,
				TransportCompanyName = transportsales.TransportCompayName,
				TransportCompanyId = transportsales.TransportCompayId,
				TransportNo = transportsales.TransportNo,
				SupplierName = transportsales.Consignee,
				SupplierId = transportsales.SaleSorderId,
				InFactoryTime = inFactoryTime,
				IsFinish = 0,
				IsUse = 1,
				StepName = eTruckInFactoryStep.入厂.ToString(),
				LoadArea = loadarea,
				Remark = remark
			};
			if (SelfDber.Insert(transport) > 0)
			{
				// 插入未完成运输记录
				return SelfDber.Insert(new CmcsUnFinishTransport
				{
					TransportId = transport.Id,
					CarType = eCarType.销售煤.ToString(),
					AutotruckId = autotruck.Id,
					PrevPlace = place,
				}) > 0;
			}
			return false;
		}


		/// <summary>
		/// 获取指定日期已完成的入厂煤运输记录
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public List<View_SaleFuelTransport> GetFinishedSaleFuelTransport(DateTime dtStart, DateTime dtEnd)
		{
			return SelfDber.Entities<View_SaleFuelTransport>("where IsFinish=1 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
		}

		/// <summary>
		/// 获取未完成的入厂煤运输记录
		/// </summary>
		/// <returns></returns>
		public List<View_SaleFuelTransport> GetUnFinishSaleFuelTransport()
		{
			return SelfDber.Entities<View_SaleFuelTransport>("where IsFinish=0 and IsUse=1 and UnFinishTransportId is not null order by InFactoryTime desc");
		}

		#endregion

		#region 其他物资业务

		/// <summary>
		/// 生成其他物资运输排队记录
		/// </summary>
		/// <param name="autotruck">车辆</param>
		/// <param name="supply">供货单位</param>
		/// <param name="receive">收货单位</param>
		/// <param name="goodsType">物资类型</param>
		/// <param name="inFactoryTime">入厂时间</param>
		/// <param name="remark">备注</param>
		/// <param name="place">地点</param>
		/// <returns></returns>
		public bool JoinQueueGoodsTransport(CmcsAutotruck autotruck, CmcsSupplyReceive supply, CmcsSupplyReceive receive, CmcsGoodsType goodsType, DateTime inFactoryTime, string remark, string place)
		{
			CmcsGoodsTransport transport = new CmcsGoodsTransport
			{
				SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(eCarType.其他物资, inFactoryTime),
				AutotruckId = autotruck.Id,
				CarNumber = autotruck.CarNumber,
				SupplyUnitId = supply.Id,
				SupplyUnitName = supply.UnitName,
				ReceiveUnitId = receive.Id,
				ReceiveUnitName = receive.UnitName,
				GoodsTypeId = goodsType.Id,
				GoodsTypeName = goodsType.GoodsName,
				InFactoryTime = inFactoryTime,
				IsFinish = 0,
				IsUse = 1,
				StepName = eTruckInFactoryStep.入厂.ToString(),
				Remark = remark
			};

			if (SelfDber.Insert(transport) > 0)
			{
				// 插入未完成运输记录
				return SelfDber.Insert(new CmcsUnFinishTransport
				{
					TransportId = transport.Id,
					CarType = eCarType.其他物资.ToString(),
					AutotruckId = autotruck.Id,
					PrevPlace = place,
				}) > 0;
			}

			return false;
		}

		/// <summary>
		/// 获取指定日期已完成的其他物资运输记录
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public List<CmcsGoodsTransport> GetFinishedGoodsTransport(DateTime dtStart, DateTime dtEnd)
		{
			return SelfDber.Entities<CmcsGoodsTransport>("where IsFinish=1 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
		}

		/// <summary>
		/// 获取未完成的其他物资运输记录
		/// </summary>
		/// <returns></returns>
		public List<CmcsGoodsTransport> GetUnFinishGoodsTransport()
		{
			return SelfDber.Entities<CmcsGoodsTransport>("where IsFinish=0 and IsUse=1 and Id in (select TransportId from " + EntityReflectionUtil.GetTableName<CmcsUnFinishTransport>() + " where CarType=:CarType) order by InFactoryTime desc", new { CarType = eCarType.其他物资.ToString() });
		}

		/// <summary>
		/// 更改其他物资运输记录的无效状态
		/// </summary>
		/// <param name="transportId"></param>
		/// <param name="isValid">是否有效</param>
		/// <returns></returns>
		public bool ChangeGoodsTransportToInvalid(string transportId, bool isValid)
		{
			if (isValid)
			{
				// 设置为有效
				CmcsGoodsTransport buyFuelTransport = SelfDber.Get<CmcsGoodsTransport>(transportId);
				if (buyFuelTransport != null)
				{
					if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsGoodsTransport>() + " set IsUse=1 where Id=:Id", new { Id = transportId }) > 0)
					{
						if (buyFuelTransport.IsFinish == 0)
						{
							SelfDber.Insert(new CmcsUnFinishTransport
							{
								AutotruckId = buyFuelTransport.AutotruckId,
								CarType = eCarType.其他物资.ToString(),
								TransportId = buyFuelTransport.Id,
								PrevPlace = "未知"
							});
						}

						return true;
					}
				}
			}
			else
			{
				// 设置为无效

				if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsGoodsTransport>() + " set IsUse=0 where Id=:Id", new { Id = transportId }) > 0)
				{
					SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = transportId });

					return true;
				}
			}

			return false;
		}

		#endregion

	}
}
