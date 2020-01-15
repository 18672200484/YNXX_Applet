using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AssayDevice.Entities;
using CMCS.Common;
using CMCS.Common.DAO;
using System.Xml.Linq;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice
{
	public class EquAssayDeviceDAO
	{
		private static EquAssayDeviceDAO instance;

		public static EquAssayDeviceDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new EquAssayDeviceDAO();
			}
			return instance;
		}

		private EquAssayDeviceDAO()
		{

		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		#region 生成多个设备的标准数据

		/// <summary>
		/// 生成标准量热仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToHeatStdAssay(Action<string, eOutputType> output, int day)
		{
			int res = 0;

			// .量热仪 型号：5E-C5500A双控
			foreach (LRY_5EC5500 entity in Dbers.GetInstance().SelfDber.Entities<LRY_5EC5500>("where TestTime>= :TestTime and Mancoding is not null", new { TestTime = DateTime.Now.AddDays(-day).Date }))
			{
				res += SaveToHeatStdAssay(entity);
			}

			output(string.Format("生成标准量热仪数据 {0} 条", res), eOutputType.Normal);

			return res;
		}

		/// <summary>
		/// 生成标准测硫仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToSulfurStdAssay(Action<string, eOutputType> output, int day)
		{
			int res = 0;

			// .测硫仪 型号：5E-8SAII
			foreach (CLY_5E8SAII entity in Dbers.GetInstance().SelfDber.Entities<CLY_5E8SAII>("where Date_Ex>= :TestTime and Name is not null", new { TestTime = DateTime.Now.AddDays(-day).Date }))
			{
				res += SaveToSulfurStdAssay(entity);
			}

			output(string.Format("生成标准测硫仪数据 {0} 条", res), eOutputType.Normal);
			return res;
		}

		/// <summary>
		///  生成标准水分仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToMoistureStdAssay(Action<string, eOutputType> output, int day)
		{
			int res = 0;
			// .水分仪 型号：5E-MW6510
			foreach (SFY_5EMW6510 entity in Dbers.GetInstance().SelfDber.Entities<SFY_5EMW6510>("where BeginDateTime>= :TestTime and SampleName is not null", new { TestTime = DateTime.Now.AddDays(-day).Date }))
			{
				res += SaveToMoistureStdAssay(entity);
			}

			output(string.Format("生成标准水分仪数据 {0} 条", res), eOutputType.Normal);
			return res;
		}
		/// <summary>
		///  生成标准工分仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToProximateStdAssay(Action<string, eOutputType> output, int day)
		{
			int res = 0;
			// .工分仪 型号：5E-MAG6700
			foreach (HYTBPAG_5EMAG6700 entity in Dbers.GetInstance().SelfDber.Entities<HYTBPAG_5EMAG6700>("where Date_Ex >=:Date_Ex  and SampleName is not null", new { Date_Ex = DateTime.Now.AddDays(-day).Date }))
			{
				res += SaveToProximateStdAssay(entity);
			}

			output(string.Format("生成标准工分仪数据 {0} 条", res), eOutputType.Normal);
			return res;
		}

		/// <summary>
		///  生成标准灰熔融数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToAshStdAssay(Action<string, eOutputType> output, int day)
		{
			int res = 0;
			// .灰熔融 型号：5E-AF
			foreach (ASH_5EAF entity in Dbers.GetInstance().SelfDber.Entities<ASH_5EAF>("where TestDate >=:TestDate  and SampleName is not null", new { TestDate = DateTime.Now.AddDays(-day).Date }))
			{
				res += SaveToAshStdAssay(entity);
			}

			output(string.Format("生成标准灰熔融数据 {0} 条", res), eOutputType.Normal);
			return res;
		}

		/// <summary>
		///  生成标准碳氢仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToHadStdAssay(Action<string, eOutputType> output, int day)
		{
			int res = 0;
			// .碳氢仪 型号：5E-CH2200
			foreach (HAD_CH2200 entity in Dbers.GetInstance().SelfDber.Entities<HAD_CH2200>("where Date_Ex >=:Date_Ex  and Name is not null", new { Date_Ex = DateTime.Now.AddDays(-day).Date }))
			{
				res += SaveToHadStdAssay(entity);
			}

			output(string.Format("生成标准碳氢仪数据 {0} 条", res), eOutputType.Normal);
			return res;
		}

		#endregion

		#region 保存标准数据
		/// <summary>
		/// 生成标准测硫仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SaveToSulfurStdAssay(CLY_5E8SAII entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsSulfurStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsSulfurStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (item == null)
			{
				item = new CmcsSulfurStdAssay();
				item.SampleNumber = entity.Name;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = entity.Weight;
				item.Stad = entity.Stad;
				item.Std = entity.Std;
				item.AssayTime = Convert.ToDateTime(entity.Date1.ToString("yyyy-MM-dd ") + entity.Time.ToString("HH:mm:ss"));
				item.AssayUser = entity.Assayer;
				item.OrderNumber = 0;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				res += Dbers.GetInstance().SelfDber.Insert<CmcsSulfurStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;
				item.SampleNumber = entity.Name;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = entity.Weight;
				item.Stad = entity.Stad;
				item.Std = entity.Std;
				item.AssayTime = Convert.ToDateTime(entity.Date1.ToString("yyyy-MM-dd ") + entity.Time.ToString("HH:mm:ss"));
				item.AssayUser = entity.Assayer;
				item.OrderNumber = 0;
				res += Dbers.GetInstance().SelfDber.Update<CmcsSulfurStdAssay>(item);
			}
			return res;
		}

		/// <summary>
		/// 生成标准量热仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SaveToHeatStdAssay(LRY_5EC5500 entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsHeatStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHeatStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (entity.Mingchen.Contains('-'))
				entity.Mingchen = entity.Mingchen.Substring(0, entity.Mingchen.LastIndexOf('-') - 1);
			if (item == null)
			{
				item = new CmcsHeatStdAssay();
				item.SampleNumber = entity.Mingchen;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = Convert.ToDecimal(entity.Weight);
				item.Qbad = Convert.ToDecimal(entity.Qb);
				item.AssayTime = entity.TestTime;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.AssayUser = entity.Testman;
				res += Dbers.GetInstance().SelfDber.Insert<CmcsHeatStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;
				item.SampleNumber = entity.Mingchen;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = Convert.ToDecimal(entity.Weight);
				item.Qbad = Convert.ToDecimal(entity.Qb);
				item.AssayTime = entity.TestTime;
				item.AssayUser = entity.Testman;
				res += Dbers.GetInstance().SelfDber.Update<CmcsHeatStdAssay>(item);
			}

			return res;
		}

		/// <summary>
		/// 保存标准水分仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SaveToMoistureStdAssay(SFY_5EMW6510 entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsMoistureStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsMoistureStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (item == null)
			{
				item = new CmcsMoistureStdAssay();
				item.SampleNumber = entity.SampleName;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = entity.SampleWeight;
				item.WaterType = entity.TestItem == "0" ? "全水分" : "分析水";
				item.WaterPer = entity.Moisture;
				//if (item.WaterType == "全水分") item.WaterPer += commonDAO.GetCommonAppletConfigDecimal("全水补正值");
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.AssayTime = entity.BeginDateTime;
				item.AssayUser = entity.Operator;
				res += Dbers.GetInstance().SelfDber.Insert<CmcsMoistureStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;
				item.SampleNumber = entity.SampleName;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = entity.SampleWeight;
				item.WaterPer = entity.Moisture;
				item.AssayTime = entity.BeginDateTime;
				item.WaterType = entity.TestItem == "0" ? "全水分" : "分析水";
				item.AssayUser = entity.Operator;
				res += Dbers.GetInstance().SelfDber.Update<CmcsMoistureStdAssay>(item);
			}
			return res;
		}

		/// <summary>
		/// 保存工业分析仪数据
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public int SaveToProximateStdAssay(HYTBPAG_5EMAG6700 entity)
		{
			int res = 0;
			if (entity == null) return res;

			CmcsProximateStdAssay present = Dbers.GetInstance().SelfDber.Entity<CmcsProximateStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (present == null)
			{
				present = new CmcsProximateStdAssay();

				present.PKID = entity.PKID;
				present.SampleNumber = entity.SampleName;
				present.ContainerWeight = entity.EmptyGGWeight;
				present.SampleWeight = entity.ColeWeight;
				present.Vad = entity.Vad;
				present.Mad = entity.Mad;
				present.Aad = entity.Aad;
				present.AssayTime = entity.Date_Ex;
				present.FacilityNumber = entity.MachineCode;
				present.OrderNumber = entity.ObjCode;
				present.AssayUser = entity.Operator;
				return Dbers.GetInstance().SelfDber.Insert(present);
			}
			if (present.IsEffective == 1) return res;
			present.SampleNumber = entity.SampleName;
			present.ContainerWeight = entity.EmptyGGWeight;
			present.SampleWeight = entity.ColeWeight;
			present.Vad = entity.Vad;
			present.Mad = entity.Mad;
			present.Aad = entity.Aad;
			present.AssayTime = entity.Date_Ex;
			present.FacilityNumber = entity.MachineCode;
			present.OrderNumber = entity.ObjCode;
			present.AssayUser = entity.Operator;
			return Dbers.GetInstance().SelfDber.Update(present);
		}

		/// <summary>
		/// 保存灰熔融数据
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public int SaveToAshStdAssay(ASH_5EAF entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsAshStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsAshStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (item == null)
			{
				item = new CmcsAshStdAssay();
				item.SampleNumber = entity.SampleName;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = 0;
				item.DT = entity.DT;
				item.ST = entity.ST;
				item.FT = entity.FT;
				item.HT = entity.HT;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.AssayTime = entity.TestDate;
				item.AssayUser = entity.Operator;
				res += Dbers.GetInstance().SelfDber.Insert<CmcsAshStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;
				item.SampleNumber = entity.SampleName;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = 0;
				item.DT = entity.DT;
				item.ST = entity.ST;
				item.FT = entity.FT;
				item.HT = entity.HT;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.AssayTime = entity.TestDate;
				item.AssayUser = entity.Operator;
				res += Dbers.GetInstance().SelfDber.Update<CmcsAshStdAssay>(item);
			}
			return res;
		}


		/// <summary>
		/// 保存碳氢仪数据
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public int SaveToHadStdAssay(HAD_CH2200 entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsHadStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHadStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (item == null)
			{
				item = new CmcsHadStdAssay();
				item.SampleNumber = entity.Name;
				item.FacilityNumber = entity.MachineCode;
				item.SampleWeight = entity.Weight;
				item.Had = entity.Had;
				item.Hd = entity.Hd;
				item.Cad = entity.Cad;
				item.Cd = entity.Cd;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.AssayTime = entity.Date_Ex;
				item.AssayUser = entity.Operator;
				res += Dbers.GetInstance().SelfDber.Insert<CmcsHadStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;
				item.SampleNumber = entity.Name;
				item.FacilityNumber = entity.MachineCode;
				item.SampleWeight = entity.Weight;
				item.Had = entity.Had;
				item.Hd = entity.Hd;
				item.Cad = entity.Cad;
				item.Cd = entity.Cd;
				item.IsEffective = 0;
				item.AssayTime = entity.Date_Ex;
				item.AssayUser = entity.Operator;
				res += Dbers.GetInstance().SelfDber.Update<CmcsHadStdAssay>(item);
			}
			return res;
		}
		#endregion

	}
}
