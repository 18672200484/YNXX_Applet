using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.CarJXSampler;
using CMCS.DumblyConcealer.Win.Core;
using CMCS.DumblyConcealer.Tasks.InOutStorage;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmInOutStorage : TaskForm
    {
        RTxtOutputer rTxtOutputer;
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
        Boolean isExecuted = true;

        public FrmInOutStorage()
        {
            InitializeComponent();
        }

        private void FrmCarSampler_CSKY_Load(object sender, EventArgs e)
        {
            this.Text = "煤场快照数据定时生成";

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

            ExecuteAllTask();

        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            InOutStorageDAO inOutStorageDAO = InOutStorageDAO.GetInstance();

            taskSimpleScheduler.StartNewTask("创建煤场快照数据", () =>
            {
                if (isExecuted)
                {
                    isExecuted = false;
                    string time = inOutStorageDAO.GetTimeParamter();
                    if (String.IsNullOrWhiteSpace(time))
                    {
                        this.rTxtOutputer.Output("未在系统中查询到【煤场快照数据生成时间点】的参数管理！", eOutputType.Error);
                        isExecuted = true;
                        return;
                    }
                    string setDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + time;
                    DateTime dt = DateTime.Now;
                    if (!DateTime.TryParse(setDateTime, out dt))
                    {
                        this.rTxtOutputer.Output(string.Format("时间节点：【{0}】格式不正确，转换为时间失败", time), eOutputType.Error);
                        isExecuted = true;
                        return;
                    }
                    if (DateTime.Now.ToString("yyyy-MM-dd HH:mm") == dt.ToString("yyyy-MM-dd HH:mm"))
                    {
                        inOutStorageDAO.CreateInOutStorage(this.rTxtOutputer.Output);
                    }
                    isExecuted = true;
                }
            }, 1000 * 60, OutputError);//1分钟执行一次  //时间只精确到时分，不精确到秒，因为连接数据库查询参数存在耗时间导致错过秒数的匹配
        }

        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        void OutputError(string text, Exception ex)
        {
            isExecuted = true;
            this.rTxtOutputer.Output(text + Environment.NewLine + ex.Message, eOutputType.Error);

            Log4Neter.Error(text, ex);
        }

        /// <summary>
        /// 窗体关闭后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCarSampler_CSKY_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注意：必须取消任务
            this.taskSimpleScheduler.Cancal();
        }
    }
}
