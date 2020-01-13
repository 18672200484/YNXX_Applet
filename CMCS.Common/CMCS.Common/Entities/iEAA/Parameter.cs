using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.iEAA
{
    /// <summary>
    /// 参数管理
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("SYSSMTBPARAMETER")]
    public class Parameter : EntityBase1
    {
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
        public virtual int IsModify { get; set; }
        public virtual int IsDelete { get; set; }
        public virtual string SubSystem { get; set; }
        public virtual string ModuleName { get; set; }
        public virtual string Remark { get; set; }
    }
}
