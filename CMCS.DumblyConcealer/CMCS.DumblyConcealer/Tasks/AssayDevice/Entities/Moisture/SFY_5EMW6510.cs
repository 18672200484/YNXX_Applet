using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice.Entities
{
	/// <summary>
	/// .水分仪 型号：5E-MW6510
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("HYTBSFY")]
	public class SFY_5EMW6510
	{
		[DapperPrimaryKey]
		public string PKID { get; set; }
		/// <summary>
		/// 设备编码
		/// </summary>
		public string MachineCode { get; set; }
		/// <summary>
		/// 自动编号
		/// </summary>
		public string Id { get; set; }
		/// <summary>
		/// 流水号
		/// </summary>
		public string SampleNumber { get; set; }
		/// <summary>
		/// 坩埚号
		/// </summary>
		public int CurcileNo { get; set; }
		/// <summary>
		/// 样品编号
		/// </summary>
		public string SampleName { get; set; }
		/// <summary>
		/// 化验方法
		/// </summary>
		public string TestItem { get; set; }

		/// <summary>
		/// 样重
		/// </summary>
		public decimal SampleWeight { get; set; }

		public decimal Tare { get; set; }
		/// <summary>
		/// 水分值
		/// </summary>
		public decimal Moisture { get; set; }
		/// <summary>
		/// 操作员
		/// </summary>
		public string Operator { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime BeginDateTime { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndDateTime { get; set; }

		/// <summary>
		/// 修正温度
		/// </summary>
		public decimal Temperature { get; set; }
		/// <summary>
		/// 持续时间
		/// </summary>
		public decimal KeepTime { get; set; }

	}
}
