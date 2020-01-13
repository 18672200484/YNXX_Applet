// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.CarTransport
{
	/// <summary>
	/// 汽车智能化-车辆管理
	/// </summary>
	[Serializable]
	[CMCS.DapperDber.Attrs.DapperBind("CmcsTbAppletLog")]
	public class CmcsAppletLog : EntityBase1
	{
		public string Title { get; set; }

		public string Content { get; set; }

		public string AppIdentifier { get; set; }

		public string LogLevel { get; set; }
	}
}
