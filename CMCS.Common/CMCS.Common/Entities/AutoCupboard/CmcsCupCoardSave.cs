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
	/// 存样柜-样品主表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("cmcstbcupcoardSave")]
	public class CmcsCupCoardSave : EntityBase1
	{
		/// <summary>
		/// 柜门编号
		/// </summary>
		public int CupCoardNumber { get; set; }

		/// <summary>
		/// 已存样品个数
		/// </summary>
		public int SaveCount { get; set; }

		/// <summary>
		/// 是否启用
		/// </summary>
		public int IsUse { get; set; }

		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpdateTime { get; set; }

		/// <summary>
		/// 存样明细
		/// </summary>
		[DapperIgnore]
		public IList<CmcsCupCoardSaveDetail> SaveDetails
		{
			get { return CommonDAO.GetInstance().SelfDber.Entities<CmcsCupCoardSaveDetail>("where CupCoardSaveId=:CupCoardSaveId", new { CupCoardSaveId = this.Id }); }
		}
	}
}
