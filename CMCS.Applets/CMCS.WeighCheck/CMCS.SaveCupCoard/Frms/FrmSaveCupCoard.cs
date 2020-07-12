using CMCS.Common.DAO;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.Common.Entities.Fuel;
using CMCS.SaveCupCoard.Core;
using CMCS.SaveCupCoard.Utilities;
using CMCS.WeighCheck.DAO;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CMCS.SaveCupCoard.Frms
{
	public partial class FrmSaveCupCoard : MetroForm
	{
		public FrmSaveCupCoard()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmSaveCupCoard";

		#region Vars

		CommonDAO commonDAO = CommonDAO.GetInstance();
		CZYHandlerDAO czyHandlerDAO = CZYHandlerDAO.GetInstance();

		CmcsRCMakeDetail rCMakeDetail = null;
		/// <summary>
		/// 当前扫描的制样子码信息
		/// </summary>
		public CmcsRCMakeDetail RCMakeDetail
		{
			get { return rCMakeDetail; }
			set
			{
				rCMakeDetail = value;
			}
		}
		/// <summary>
		/// 当前柜门
		/// </summary>
		int CurrentCupBoardNumber;
		string resMessage = string.Empty;
		#endregion

		public void InitFrom()
		{
		}

		private void FrmSampleCheck_Load(object sender, EventArgs e)
		{
			//初始化
			InitFrom();
		}

		#region 业务

		/// <summary>
		/// 重置
		/// </summary>
		private void Restet()
		{
			this.CurrentCupBoardNumber = 0;
			this.RCMakeDetail = null;
			txtInputSampleCode.ResetText();
			rtxtOutputInfo.ResetText();

			// 方便客户快速使用，获取焦点
			txtInputSampleCode.Focus();
		}

		#endregion

		#region 操作

		/// <summary>
		/// 键入Enter检测有效性
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtInputSampleCode_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnSubmit_Click(null, null);
			}
		}

		/// <summary>
		/// 确定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSubmit_Click(object sender, EventArgs e)
		{
			string barrelCode = txtInputSampleCode.Text.Trim();
			if (String.IsNullOrWhiteSpace(barrelCode)) return;
			this.RCMakeDetail = commonDAO.SelfDber.Entity<CmcsRCMakeDetail>("where SampleCode=:SampleCode order by CreateDate desc", new { SampleCode = barrelCode });
			//  根据采样桶编码查找该采样单下所有采样桶记录
			if (this.RCMakeDetail != null)
			{
				CmcsCupCoardSaveDetail saveDetail = commonDAO.SelfDber.Entity<CmcsCupCoardSaveDetail>("where SampleCode=:SampleCode ", new { SampleCode = barrelCode });

				if (saveDetail != null)
				{
					ShowMessage("该样品已存样，存样位置：" + saveDetail.TheSave.CupCoardNumber + "柜", eOutputType.Warn);
					return;
				}
				else
				{
					CmcsCupCoardSaveDetail saveDetailReady = commonDAO.SelfDber.Entity<CmcsCupCoardSaveDetail>("where SampleCode like :SampleCode||'%' ", new { SampleCode = barrelCode.Substring(0, barrelCode.Length - 3) });
					if (saveDetailReady != null && saveDetailReady.TheSave != null)
					{
						CurrentCupBoardNumber = saveDetailReady.TheSave.CupCoardNumber;
						Hardwarer.Iocer.Output(CurrentCupBoardNumber);
						Thread.Sleep(1000);
						if (Hardwarer.Iocer.IsOpenSuccess)
						{
							ShowMessage(CurrentCupBoardNumber + "号柜门已打开，请放入样品关好柜门", eOutputType.Normal);
							czyHandlerDAO.SaveCupBoard(CurrentCupBoardNumber, SelfVars.LoginUser.UserName, barrelCode);
						}
					}
					else
					{
						CmcsCupCoardSave saveCupBoard = commonDAO.SelfDber.Entity<CmcsCupCoardSave>(" where IsUse=1 order by dbms_random.value()");
						if (saveCupBoard != null)
						{
							CurrentCupBoardNumber = saveCupBoard.CupCoardNumber;
							Hardwarer.Iocer.Output(CurrentCupBoardNumber);
							Thread.Sleep(1000);
							if (Hardwarer.Iocer.IsOpenSuccess)
							{
								ShowMessage(CurrentCupBoardNumber + "号柜门已打开，请放入样品关好柜门", eOutputType.Normal);
								czyHandlerDAO.SaveCupBoard(CurrentCupBoardNumber, SelfVars.LoginUser.UserName, barrelCode);
							}
						}
					}
				}
			}
			else
			{
				txtInputSampleCode.ResetText();
				ShowMessage("制样子码：" + barrelCode + " 不存在", eOutputType.Error);
			}
		}

		/// <summary>
		/// 重新开柜
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenDoor_Click(object sender, EventArgs e)
		{
			if (CurrentCupBoardNumber == 0)
			{
				ShowMessage("柜门编号已清空，无法开柜", eOutputType.Error);
				return;
			}
			Hardwarer.Iocer.Output(CurrentCupBoardNumber);
			ShowMessage(CurrentCupBoardNumber + "号柜门已打开", eOutputType.Normal);
		}
		/// <summary>
		/// 重置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReset_Click(object sender, EventArgs e)
		{
			Restet();
		}

		#endregion

		#region 其他

		private void ClearBarrelCode()
		{
			txtInputSampleCode.ResetText();
			this.RCMakeDetail = null;
		}

		private void ShowMessage(string info, eOutputType outputType)
		{
			OutputRunInfo(rtxtOutputInfo, info, outputType);
		}

		/// <summary>
		/// 输出运行信息
		/// </summary>
		/// <param name="richTextBox"></param>
		/// <param name="text"></param>
		/// <param name="outputType"></param>
		private void OutputRunInfo(RichTextBoxEx richTextBox, string text, eOutputType outputType = eOutputType.Normal)
		{
			this.Invoke((EventHandler)(delegate
			{
				if (richTextBox.TextLength > 100000) richTextBox.Clear();

				text = string.Format("{0}  {1}", DateTime.Now.ToString("HH:mm:ss"), text);

				richTextBox.SelectionStart = richTextBox.TextLength;

				switch (outputType)
				{
					case eOutputType.Normal:
						richTextBox.SelectionColor = ColorTranslator.FromHtml("#BD86FA");
						break;
					case eOutputType.Important:
						richTextBox.SelectionColor = ColorTranslator.FromHtml("#A50081");
						break;
					case eOutputType.Warn:
						richTextBox.SelectionColor = ColorTranslator.FromHtml("#F9C916");
						break;
					case eOutputType.Error:
						richTextBox.SelectionColor = ColorTranslator.FromHtml("#DB2606");
						break;
					default:
						richTextBox.SelectionColor = Color.White;
						break;
				}

				richTextBox.AppendText(string.Format("{0}\r", text));

				richTextBox.ScrollToCaret();

			}));
		}

		/// <summary>
		/// 输出信息类型
		/// </summary>
		public enum eOutputType
		{
			/// <summary>
			/// 普通
			/// </summary>
			[Description("#BD86FA")]
			Normal,
			/// <summary>
			/// 重要
			/// </summary>
			[Description("#A50081")]
			Important,
			/// <summary>
			/// 警告
			/// </summary>
			[Description("#F9C916")]
			Warn,
			/// <summary>
			/// 错误
			/// </summary>
			[Description("#DB2606")]
			Error
		}

		/// <summary>
		/// Invoke封装
		/// </summary>
		/// <param name="action"></param>
		public void InvokeEx(Action action)
		{
			if (this.IsDisposed || !this.IsHandleCreated) return;

			this.Invoke(action);
		}

		#endregion

	}
}
