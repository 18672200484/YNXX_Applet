using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.DAO;
using CMCS.Common.Entities.Sys;
using CMCS.DapperDber.Attrs;

namespace CMCS.Common.Entities.AutoCupboard
{
	/// <summary>
	/// 存样柜-样品明表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("cmcstbcupcoardSaveDetail")]
	public class CmcsCupCoardSaveDetail : EntityBase1
	{
		/// <summary>
		/// 主表ID
		/// </summary>
		public string CupCoardSaveId { get; set; }

		/// <summary>
		/// 样品编码
		/// </summary>
		public string SampleCode { get; set; }

		/// <summary>
		/// 存样人
		/// </summary>
		public string SaveUser { get; set; }

		/// <summary>
		/// 是否已取样
		/// </summary>
		public int IsTake { get; set; }

		/// <summary>
		/// 取样人
		/// </summary>
		public string TakeUser { get; set; }

		/// <summary>
		/// 取样时间
		/// </summary>
		public DateTime TakeTime { get; set; }

		/// <summary>
		/// 存样时间
		/// </summary>
		public DateTime SaveTime { get; set; }

		/// <summary>
		/// 主表信息
		/// </summary>
		[DapperIgnore]
		public CmcsCupCoardSave TheSave
		{
			get { return CommonDAO.GetInstance().SelfDber.Get<CmcsCupCoardSave>(this.CupCoardSaveId); }
		}
	}
}
