using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.SaveCupCoard.Core;
using CMCS.SaveCupCoard.Utilities;
using CMCS.WeighCheck.DAO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CMCS.SaveCupCoard.Frms
{
	public partial class FrmTakeCupCoard : MetroForm
	{
		public FrmTakeCupCoard()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmTakeCupCoard";

		string SqlWhere = "where 1=1";
		#region Vars

		CommonDAO commonDAO = CommonDAO.GetInstance();
		CZYHandlerDAO czyHandlerDAO = CZYHandlerDAO.GetInstance();

		CmcsCupCoardSaveDetail currentCupCoard = null;
		/// <summary>
		/// 当前取样记录
		/// </summary>
		public CmcsCupCoardSaveDetail CurrentCupCoard
		{
			get { return currentCupCoard; }
			set { currentCupCoard = value; }
		}

		string resMessage = string.Empty;

		#endregion

		public void InitFrom()
		{
			// 生成取样按钮
			GridButtonXEditControl btnNewCode = superGridControl1.PrimaryGrid.Columns["gclmTakeSample"].EditControl as GridButtonXEditControl;
			btnNewCode.ColorTable = eButtonColor.BlueWithBackground;
			btnNewCode.Click += new EventHandler(btnTakeCupBoard_Click);
		}

		private void FrmSampleCheck_Load(object sender, EventArgs e)
		{
			//初始化
			InitFrom();
			btnSearch_Click(null, null);
		}

		private void BindData()
		{
			object param = new { SampleCode = txt_SampleCode.Text.Trim(), StartTime = dtpStartTime.Value.Date, EndTime = dtpEndTime.Value.AddDays(1).Date };
			string tempSqlWhere = this.SqlWhere;
			List<CmcsCupCoardSaveDetail> list = Dbers.GetInstance().SelfDber.ExecutePager<CmcsCupCoardSaveDetail>(PageSize, CurrentIndex, tempSqlWhere + " and IsTake=0 order by SaveTime desc", param);
			superGridControl1.PrimaryGrid.DataSource = list;
			GetTotalCount(tempSqlWhere, param);
			PagerControlStatue();
			lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);
		}

		#region 业务

		/// <summary>
		/// 重置
		/// </summary>
		private void Restet()
		{
			this.CurrentCupCoard = null;

			txtInputSampleCode.ResetText();
			rtxtOutputInfo.ResetText();

			// 方便客户快速使用，获取焦点
			txtInputSampleCode.Focus();
		}

		#endregion

		#region 操作

		void btnTakeCupBoard_Click(object sender, EventArgs e)
		{
			GridButtonXEditControl btn = sender as GridButtonXEditControl;
			if (btn == null) return;

			CmcsCupCoardSaveDetail rCMakeDetail = btn.EditorCell.GridRow.DataItem as CmcsCupCoardSaveDetail;
			if (rCMakeDetail == null || rCMakeDetail.TheSave == null) return;
			this.CurrentCupCoard = rCMakeDetail;
			Hardwarer.Iocer.Output(rCMakeDetail.TheSave.CupCoardNumber);
			Thread.Sleep(1000);
			if (Hardwarer.Iocer.IsOpenSuccess)
				ShowMessage("取样成功,请到" + this.CurrentCupCoard.TheSave.CupCoardNumber + "号柜取样", eOutputType.Normal);
		}

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

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			string barrelCode = txtInputSampleCode.Text.Trim();
			if (String.IsNullOrWhiteSpace(barrelCode)) return;

			// 采样桶编码属于同一采样单下则验证通过，直到全部验证完毕
			if (this.CurrentCupCoard != null)
			{
				if (this.CurrentCupCoard.SampleCode == barrelCode)
				{
					ShowMessage(barrelCode + "为当前取样记录，取样完成", eOutputType.Normal);
					if (czyHandlerDAO.TakeCupBoard(this.CurrentCupCoard.TheSave.CupCoardNumber, SelfVars.LoginUser.UserName, barrelCode))
						btnSearch_Click(null, null);
				}
				else
				{
					ShowMessage(barrelCode + "不是当前取样记录，请继续扫描下一个样码", eOutputType.Warn);
					txtInputSampleCode.ResetText();
				}
			}
			else
			{
				txtInputSampleCode.ResetText();
				ShowMessage("当前没有取样记录", eOutputType.Error);
			}
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

		/// <summary>
		/// 搜索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.SqlWhere = "where 1=1";
			if (!string.IsNullOrEmpty(txt_SampleCode.Text))
				this.SqlWhere += " and SampleCode=:SampleCode";
			if (!string.IsNullOrEmpty(dtpStartTime.Text))
				this.SqlWhere += " and SaveTime>=:StartTime";
			if (!string.IsNullOrEmpty(dtpEndTime.Text))
				this.SqlWhere += " and SaveTime<:EndTime";
			BindData();
		}
		#endregion

		#region 其他

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
		private void OutputRunInfo(RichTextBox richTextBox, string text, eOutputType outputType = eOutputType.Normal)
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

		#region Pager

		/// <summary>
		/// 每页显示行数
		/// </summary>
		int PageSize = 18;

		/// <summary>
		/// 总页数
		/// </summary>
		int PageCount = 0;

		/// <summary>
		/// 总记录数
		/// </summary>
		int TotalCount = 0;

		/// <summary>
		/// 当前页索引
		/// </summary>
		int CurrentIndex = 0;

		private void btnPagerCommand_Click(object sender, EventArgs e)
		{
			ButtonX btn = sender as ButtonX;
			switch (btn.CommandParameter.ToString())
			{
				case "First":
					CurrentIndex = 0;
					break;
				case "Previous":
					CurrentIndex = CurrentIndex - 1;
					break;
				case "Next":
					CurrentIndex = CurrentIndex + 1;
					break;
				case "Last":
					CurrentIndex = PageCount - 1;
					break;
			}

			BindData();
		}

		public void PagerControlStatue()
		{
			if (PageCount <= 1)
			{
				btnFirst.Enabled = false;
				btnPrevious.Enabled = false;
				btnLast.Enabled = false;
				btnNext.Enabled = false;

				return;
			}

			if (CurrentIndex == 0)
			{
				// 首页
				btnFirst.Enabled = false;
				btnPrevious.Enabled = false;
				btnLast.Enabled = true;
				btnNext.Enabled = true;
			}

			if (CurrentIndex > 0 && CurrentIndex < PageCount - 1)
			{
				// 上一页/下一页
				btnFirst.Enabled = true;
				btnPrevious.Enabled = true;
				btnLast.Enabled = true;
				btnNext.Enabled = true;
			}

			if (CurrentIndex == PageCount - 1)
			{
				// 末页
				btnFirst.Enabled = true;
				btnPrevious.Enabled = true;
				btnLast.Enabled = false;
				btnNext.Enabled = false;
			}
		}

		private void GetTotalCount(string sqlWhere, object param)
		{
			TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsCupCoardSaveDetail>(sqlWhere, param);
			if (TotalCount % PageSize != 0)
				PageCount = TotalCount / PageSize + 1;
			else
				PageCount = TotalCount / PageSize;
		}

		#endregion

		#region DataGridView

		private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
		{

			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				try
				{
					CmcsCupCoardSaveDetail entity = gridRow.DataItem as CmcsCupCoardSaveDetail;
					if (entity == null) return;

					// 填充有效状态
					gridRow.Cells["CupBoardNumber"].Value = entity.TheSave.CupCoardNumber.ToString();
				}
				catch (Exception)
				{
				}
			}
			#endregion
		}

	}
}
