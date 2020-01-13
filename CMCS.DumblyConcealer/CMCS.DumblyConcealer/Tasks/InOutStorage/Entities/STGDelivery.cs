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
    [CMCS.DapperDber.Attrs.DapperBind("stgtbdelivery")]
    public class STGDelivery : EntityBase1
    {
        private String _IsUpload;
        /// <summary>
        /// 是否上传
        /// </summary>
        public virtual String IsUpload { get { return _IsUpload; } set { _IsUpload = value; } }

        private String _DataFrom;
        /// <summary>
        /// 数据来源
        /// </summary>
        public virtual String DataFrom { get { return _DataFrom; } set { _DataFrom = value; } }

        private String _BillNumber;
        /// <summary>
        /// 单据编号
        /// </summary>
        public virtual String BillNumber { get { return _BillNumber; } set { _BillNumber = value; } }

        private DateTime _RecordDate;
        /// <summary>
        /// 出库时间
        /// </summary>
        public virtual DateTime RecordDate { get { return _RecordDate; } set { _RecordDate = value; } }

        private String _RecordName;
        /// <summary>
        /// 出库人员
        /// </summary>
        public virtual String RecordName { get { return _RecordName; } set { _RecordName = value; } }

        private String _Status;
        /// <summary>
        /// 出库状态
        /// </summary>
        public virtual String Status { get { return _Status; } set { _Status = value; } }

        private String _Remark;
        /// <summary>
        /// 出库说明
        /// </summary>
        public virtual String Remark { get { return _Remark; } set { _Remark = value; } }


        private String _ClassCyc;
        /// <summary>
        /// 班次
        /// </summary>
        public virtual String ClassCyc { get { return _ClassCyc; } set { _ClassCyc = value; } }


        private String _DutyCyc;
        /// <summary>
        /// 值次
        /// </summary>
        public virtual String DutyCyc { get { return _DutyCyc; } set { _DutyCyc = value; } }

        private Decimal _TotalQty;
        /// <summary>
        /// 出库总量
        /// </summary>
        public virtual Decimal TotalQty { get { return _TotalQty; } set { _TotalQty = value; } }
    }
}
