using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.InOutStorage.Entities
{
    /// <summary>
    /// 煤场调整表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("stgtbfueladjustment")]
    public class STGAdjustment : EntityBase1
    {
        /// <summary>
        /// 是否上传
        /// </summary>
        public virtual String IsUpload { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public virtual String DataFrom { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public virtual String BillNumber { get; set; }
        /// <summary>
        /// 调整时间
        /// </summary>
        public virtual DateTime RecordDate { get; set; }
        /// <summary>
        /// 调整人员
        /// </summary>
        public virtual String RecordName { get; set; }
        /// <summary>
        /// 调整状态
        /// </summary>
        public virtual String Status { get; set; }
        /// <summary>
        /// 调整说明
        /// </summary>
        public virtual String Remark { get; set; }
        /// <summary>
        /// 调整前总库存
        /// </summary>
        public virtual Decimal Adjust_BeforeStocks { get; set; }
        /// <summary>
        /// 调整后煤场库存
        /// </summary>
        public virtual Decimal Adjust_AfterStocks { get; set; }
        /// <summary>
        /// 调整库存数量
        /// </summary>
        public virtual Decimal AdjustStocks { get; set; }
    }
}
