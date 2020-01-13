using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;
using System.ComponentModel;

namespace CMCS.DumblyConcealer.Tasks.InOutStorage.Entities
{
    /// <summary>
    /// 入炉煤量信息主表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("fultbinfurance")]
    public class FulInfurnace : EntityBase1
    {
        /// <summary>
        /// 业务日期
        /// </summary>
        [Description("业务日期")]
        public virtual DateTime BusinessDate { get; set; }
        /// <summary>
        /// 机组 1#机组；2#机组
        /// </summary>
        [Description("机组 1#机组；2#机组")]
        public virtual string Crew { get; set; }
        /// <summary>
        /// 班次 白班、前夜班、后夜班
        /// </summary>
        [Description("班次 白班、前夜班、后夜班")]
        public virtual string Shifts { get; set; }
        /// <summary>
        /// 入炉开始时间
        /// </summary>
        [Description("入炉开始时间")]
        public virtual DateTime StartTime { get; set; }
        /// <summary>
        /// 入炉结束时间
        /// </summary>
        [Description("入炉结束时间")]
        public virtual DateTime EndTime { get; set; }
        /// <summary>
        /// 记录人员
        /// </summary>
        [Description("记录人员")]
        public virtual String RecordPle { get; set; }
        /// <summary>
        /// 入炉总上煤量
        /// </summary>
        [Description("入炉总上煤量")]
        public virtual Decimal RltotalQty { get; set; }
        /// <summary>
        /// 状态 0：初始 1：结束 入炉结束状态默认为0
        /// </summary>
        [Description("状态 0：初始 1：结束 入炉结束状态默认为0")]
        public virtual Int32 IsState { get; set; }
        /// <summary>
        /// 备注
        [Description("备注")]
        /// </summary>
        public virtual String Remark { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public virtual String DataFrom { get; set; }
    }
}
