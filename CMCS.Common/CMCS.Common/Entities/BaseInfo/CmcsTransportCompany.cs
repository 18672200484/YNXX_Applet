using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys; 

namespace CMCS.Common.Entities.BaseInfo
{
    /// <summary>
    /// 基础信息-运输单位
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("FulTbTransportCompany")]
    public class CmcsTransportCompany : EntityBase1
    {
        /// <summary>
        /// 运输单位编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 运输单位名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 运输单位简称
        /// </summary>
        public virtual string ShortName { get; set; }

        /// <summary>
        /// 组织机构代码
        /// </summary>
        public virtual string OrganizationCode { get; set; }

        /// <summary>
        /// 是否启用   0 启用 1 停用
        /// </summary>
        public virtual Int32 IsStop { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public virtual string DataFrom { get; set; }

		private int _IsSynch;
		/// <summary>
		/// 同步标识
		/// </summary>
		public virtual int IsSynch { get { return _IsSynch; } set { _IsSynch = value; } }
	}
}
