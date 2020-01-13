using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.InOutStorage.Entities
{
    /// <summary>
    /// 煤场快照表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("stgtbshcrb")]
    public class STGDayInOutStorage :EntityBase1
    {
        /// <summary>
        /// 记录日期
        /// </summary>
        public virtual DateTime RecordDate { get; set; }
        /// <summary>
        /// 当日进煤量
        /// </summary>
        public virtual Decimal InQty { get; set; }
        /// <summary>
        /// 当日耗煤量
        /// </summary>
        public virtual Decimal UseQty { get; set; }
        /// <summary>
        /// 当日库存量
        /// </summary>
        public virtual Decimal StorageQty { get; set; }
        /// <summary>
        /// 调整量
        /// </summary>
        public virtual Decimal AdjustQty { get; set; }
        /// <summary>
        /// 数据标识(备用)
        /// </summary>
        public virtual Int32 DataFlag { get; set; }
    }
}
