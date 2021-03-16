using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
	/// <summary>
	/// 入厂煤制样明细表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("CmcsTbMakeDetail")]
	public class CmcsRCMakeDetail : EntityBase1
	{
		private string _MakeId;

		/// <summary>
		/// 入厂煤制样Id
		/// </summary>
		public string MakeId
		{
			get { return _MakeId; }
			set { _MakeId = value; }
		}
		private string _SampleCode;

		/// <summary>
		/// 制样子码
		/// </summary>
		public string SampleCode
		{
			get { return _SampleCode; }
			set { _SampleCode = value; }
		}
		private string _BackupCode;

		/// <summary>
		/// 备用码
		/// </summary>
		public string BackupCode
		{
			get { return _BackupCode; }
			set { _BackupCode = value; }
		}
		private string _SampleType;

		/// <summary>
		/// 样品类型
		/// </summary>
		public string SampleType
		{
			get { return _SampleType; }
			set { _SampleType = value; }
		}
		private double _SampleWeight;

		/// <summary>
		/// 样品样重
		/// </summary>
		public double SampleWeight
		{
			get { return _SampleWeight; }
			set { _SampleWeight = value; }
		}

		private double _CheckWeight;

		/// <summary>
		/// 校验样重
		/// </summary>
		public double CheckWeight
		{
			get { return _CheckWeight; }
			set { _CheckWeight = value; }
		}

		/// <summary>
		/// 是否删除 0已删除 1 未删除
		/// </summary>
		public Int32 IsDeleted { get; set; }

		/// <summary>
		/// 打印时间
		/// </summary>
		public DateTime PrintTime { get; set; }

		/// <summary>
		/// 打印次数
		/// </summary>
		public int PrintCount { get; set; }

		/// <summary>
		/// 校验类型
		/// </summary>
		public string CheckType { get; set; }

		/// <summary>
		/// 校验人
		/// </summary>
		public string CheckUser { get; set; }

		/// <summary>
		/// 是否校验
		/// </summary>
		public int IsCheck { get; set; }

		//[DapperDber.Attrs.DapperIgnoreAttribute]
		//public CmcsRCMake TheRCMake
		//{
		//	get
		//	{
		//		return Dbers.GetInstance().SelfDber.Get<CmcsRCMake>(this.MakeId);
		//	}
		//}
	}
}
