using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Win.Core;

using CMCS.DumblyConcealer.Tasks.BeltSampler;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.WeightBridger;
using CMCS.DumblyConcealer.Tasks.TruckWeight;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmTruckWeight : TaskForm
    {
        RTxtOutputer rTxtOutputer;
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        /// <summary>
        /// 最后一次心跳值
        /// </summary>
        string lastHeartbeat;

        public FrmTruckWeight()
        {
            InitializeComponent();
        }

        private void FrmWeightBridger_Load(object sender, EventArgs e)
        {
            this.Text = "汽车衡数据同步业务";

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            TruckWeightDAO weightBridger_DAO = TruckWeightDAO.GetInstance();
            taskSimpleScheduler.StartNewTask("同步汽车衡过衡数据", () =>
            {
                weightBridger_DAO.SyncTransport(this.rTxtOutputer.Output);

            }, 30000, OutputError);
        }

        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        void OutputError(string text, Exception ex)
        {
            this.rTxtOutputer.Output(text + Environment.NewLine + ex.Message, eOutputType.Error);
        }

        /// <summary>
        /// 窗体关闭后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBeltSampler_NCGM_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注意：必须取消任务
            this.taskSimpleScheduler.Cancal();
        }
    }
}
