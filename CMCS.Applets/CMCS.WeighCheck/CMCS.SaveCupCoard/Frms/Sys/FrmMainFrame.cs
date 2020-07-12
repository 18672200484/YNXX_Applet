using System;
using System.Windows.Forms;
//
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using DevComponents.DotNetBar.Metro;
using CMCS.Common;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.SaveCupCoard.Core;
using System.Drawing;
using CMCS.SaveCupCoard.Utilities;

namespace CMCS.SaveCupCoard.Frms.Sys
{
	public partial class FrmMainFrame : MetroForm
	{
		CommonDAO commonDAO = CommonDAO.GetInstance();

		public static SuperTabControlManager superTabControlManager;

		public FrmMainFrame()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			lblVersion.Text = new AU.Updater().Version;

			this.superTabControl1.Tabs.Clear();
			FrmMainFrame.superTabControlManager = new SuperTabControlManager(this.superTabControl1);

			OpenSaveCupBoard();
			OpenTakeCupBoard();
			OpenSaveCupBoard();
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			if (SelfVars.LoginUser != null) lblLoginUserName.Text = SelfVars.LoginUser.UserName;
			InitHardware();
			commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.系统.ToString(), "1");
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (MessageBoxEx.Show("确认退出系统？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.系统.ToString(), "0");
					UnloadHardware();
					Application.Exit();
				}
				else
				{
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// 退出系统
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnApplicationExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void timer_CurrentTime_Tick(object sender, EventArgs e)
		{
			lblCurrentTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
		}

		#region 打开/切换可视主界面

		#region 弹出窗体

		/// <summary>
		/// 打开存样界面
		/// </summary>
		public void OpenSaveCupBoard()
		{
			string uniqueKey = FrmSaveCupCoard.UniqueKey;

			if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
			{
				FrmSaveCupCoard frm = new FrmSaveCupCoard();
				FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, false);
			}
			else
				FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
		}

		/// <summary>
		/// 打开取样界面
		/// </summary>
		public void OpenTakeCupBoard()
		{
			string uniqueKey = FrmTakeCupCoard.UniqueKey;

			if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
			{
				FrmTakeCupCoard frm = new FrmTakeCupCoard();
				FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, false);
			}
			else
				FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
		}

		/// <summary>
		/// 打开参数设置界面
		/// </summary>
		public void OpenSetting()
		{
			FrmSetting frm = new FrmSetting();
			frm.ShowDialog();
		}

		#endregion

		/// <summary>
		/// 打开参数设置界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenSetting_Click(object sender, EventArgs e)
		{
			OpenSetting();
		}

		#endregion

		#region 设备初始化与卸载

		/// <summary>
		/// 初始化外接设备
		/// </summary>
		private void InitHardware()
		{
			try
			{
				bool success = false;
				Hardwarer.Iocer.OnStatusChange += new CupCoard.DJ.CupCoard_DJ.StatusChangeHandler(OnReceiveStatus);

				success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("存样柜串口"), commonDAO.GetAppletConfigInt32("存样柜波特率"));

			}
			catch (Exception ex)
			{
				Log4Neter.Error("设备初始化", ex);
			}
		}

		void OnReceiveStatus(bool status)
		{
			InvokeEx(() =>
			{
				this.slightWber.LightColor = status ? Color.Green : Color.Red;
			});

		}

		/// <summary>
		/// 卸载设备
		/// </summary>
		private void UnloadHardware()
		{
			// 注意此段代码
			Application.DoEvents();

			try
			{
				Hardwarer.Iocer.CloseCom();
			}
			catch { }
		}
		#endregion

		/// <summary>
		/// Invoke封装
		/// </summary>
		/// <param name="action"></param>
		public void InvokeEx(Action action)
		{
			if (this.IsDisposed || !this.IsHandleCreated) return;

			this.Invoke(action);
		}

	}
}
