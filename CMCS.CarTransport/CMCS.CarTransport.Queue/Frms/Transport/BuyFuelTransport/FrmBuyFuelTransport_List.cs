using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CMCS.CarTransport.Queue.Frms.Transport.TransportPicture;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.CarTransport.Queue.Enums;
using CMCS.CarTransport.Queue.Frms.Transport.Print;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.DAO;
using CMCS.CarTransport.Queue.Core;
using CMCS.Common.Enums;

namespace CMCS.CarTransport.Queue.Frms.Transport.BuyFuelTransport
{
	public partial class FrmBuyFuelTransport_List : MetroAppForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmBuyFuelTransport_List";

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

		string SqlWhere = string.Empty;

		bool hasManagePower = false;
		/// <summary>
		/// 对否有维护权限
		/// </summary>
		public bool HasManagePower
		{
			get
			{
				return hasManagePower;
			}

			set
			{
				hasManagePower = value;

				superGridControl1.PrimaryGrid.Columns["clmDelete"].Visible = value;
			}
		}

		public FrmBuyFuelTransport_List()
		{
			InitializeComponent();
		}

		private void FrmBuyFuelTransport_List_Load(object sender, EventArgs e)
		{
			superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			HasManagePower = CommonDAO.GetInstance().HasResourcePowerByResCode(SelfVars.LoginUser.UserAccount, eUserRoleCodes.汽车智能化信息维护.ToString());
			LoadMine(cmbMineName_BuyFuel);
			LoadFuelkind(cmbFuelKindName_BuyFuel);
			dtpStartTime.Value = DateTime.Now;
			dtpEndTime.Value = DateTime.Now;

			btnSearch_Click(null, null);
		}

		/// <summary>
		/// 加载矿点
		/// </summary>
		void LoadMine(ComboBoxEx comboBoxEx)
		{
			IList<CmcsMine> list = Dbers.GetInstance().SelfDber.Entities<CmcsMine>("where IsStop=0 and ParentId is not null order by Sequence");
			foreach (CmcsMine item in list)
			{
				comboBoxEx.Items.Add(new ComboBoxItem(item.Id, item.Name));
			}
			comboBoxEx.Items.Insert(0, new ComboBoxItem("", ""));
		}
		/// <summary>
		/// 加载煤种
		/// </summary>
		void LoadFuelkind(ComboBoxEx comboBoxEx)
		{
			IList<CmcsFuelKind> list = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where IsStop=0 and ParentId is not null order by Sequence");
			foreach (CmcsFuelKind item in list)
			{
				comboBoxEx.Items.Add(new ComboBoxItem(item.Id, item.Name));
			}
			comboBoxEx.Items.Insert(0, new ComboBoxItem("", ""));
		}

		public void BindData()
		{
			object param = new { StartTime = dtpStartTime.Value.Date, EndTime = dtpEndTime.Value.AddDays(1).Date };
			string tempSqlWhere = this.SqlWhere;
			List<CmcsBuyFuelTransport> list = Dbers.GetInstance().SelfDber.ExecutePager<CmcsBuyFuelTransport>(PageSize, CurrentIndex, tempSqlWhere + " order by SerialNumber desc", param);
			superGridControl1.PrimaryGrid.DataSource = list;
			List<CmcsBuyFuelTransport> listTotal = Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransport>(tempSqlWhere + " order by SerialNumber desc", param);
			GetTotalCount(tempSqlWhere, param);
			PagerControlStatue();
			lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);
			labNumber_BuyFuel.Text = string.Format("已登记：{0}  已称重：{1}  已回皮：{2}  未回皮：{3}", listTotal.Count, listTotal.Where(a => a.GrossWeight > 0).Count(), listTotal.Where(a => a.TareWeight > 0).Count(), listTotal.Where(a => a.SuttleWeight == 0).Count());
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{

			this.SqlWhere = " where 1=1";
			if (!string.IsNullOrEmpty(dtpStartTime.Text)) this.SqlWhere += " and InFactoryTime>=:StartTime";
			if (!string.IsNullOrEmpty(dtpEndTime.Text)) this.SqlWhere += " and InFactoryTime<:EndTime";
			if (!string.IsNullOrEmpty(txtCarNumber_Ser.Text)) this.SqlWhere += " and CarNumber like '%" + txtCarNumber_Ser.Text + "%'";
			if (!string.IsNullOrEmpty(cmbMineName_BuyFuel.Text)) this.SqlWhere += " and MineName like '%" + cmbMineName_BuyFuel.Text + "%'";
			if (!string.IsNullOrEmpty(cmbFuelKindName_BuyFuel.Text)) this.SqlWhere += " and FuelKindName = '" + cmbFuelKindName_BuyFuel.Text + "'";
			if (!string.IsNullOrEmpty(txtCarNumber_Ser.Text)) this.SqlWhere += " and CarNumber like '%" + txtCarNumber_Ser.Text + "%'";

			CurrentIndex = 0;
			BindData();
		}

		private void btnAll_Click(object sender, EventArgs e)
		{
			this.SqlWhere = "where 1=1";
			txtCarNumber_Ser.Text = string.Empty;

			CurrentIndex = 0;
			BindData();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			FrmBuyFuelTransport_Oper frmEdit = new FrmBuyFuelTransport_Oper(Guid.NewGuid().ToString(), eEditMode.新增);
			if (frmEdit.ShowDialog() == DialogResult.OK)
			{
				BindData();
			}
		}

		#region Pager

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
			TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>(sqlWhere, param);
			if (TotalCount % PageSize != 0)
				PageCount = TotalCount / PageSize + 1;
			else
				PageCount = TotalCount / PageSize;
		}

		#endregion

		#region DataGridView

		private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		private void superGridControl1_CellMouseDown(object sender, DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs e)
		{
			CmcsBuyFuelTransport entity = Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(superGridControl1.PrimaryGrid.GetCell(e.GridCell.GridRow.Index, superGridControl1.PrimaryGrid.Columns["clmId"].ColumnIndex).Value.ToString());
			switch (superGridControl1.PrimaryGrid.Columns[e.GridCell.ColumnIndex].Name)
			{

				case "clmShow":
					FrmBuyFuelTransport_Oper frmShow = new FrmBuyFuelTransport_Oper(entity.Id, eEditMode.查看);
					if (frmShow.ShowDialog() == DialogResult.OK)
					{
						BindData();
					}
					break;
				case "clmEdit":
					FrmBuyFuelTransport_Oper frmEdit = new FrmBuyFuelTransport_Oper(entity.Id, eEditMode.修改);
					if (frmEdit.ShowDialog() == DialogResult.OK)
					{
						BindData();
					}
					break;
				case "clmDelete":
					// 查询正在使用该记录的车数 
					if (MessageBoxEx.Show("确定要删除该记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						try
						{
							string logValue = "删除运输记录：" + Environment.NewLine;
							logValue += "车号：" + entity.CarNumber + Environment.NewLine;
							logValue += "矿点:" + entity.MineName + "   煤种：" + entity.FuelKindName + Environment.NewLine;
							logValue += "入厂时间：" + entity.InFactoryTime + "   矿发量：" + entity.TicketWeight + Environment.NewLine;
							logValue += "毛重时间：" + entity.GrossTime + "   毛重：" + entity.GrossWeight + Environment.NewLine;
							logValue += "皮重时间：" + entity.TareTime + "   皮重：" + entity.TareWeight + Environment.NewLine;
							logValue += "扣矸：" + entity.KgWeight + "   扣水：" + entity.KsWeight + "   自动扣水：" + entity.AutoKsWeight + Environment.NewLine;
							logValue += "出厂时间：" + entity.OutFactoryTime + "   验收量：" + entity.CheckWeight + Environment.NewLine;
							logValue += "修改人：" + SelfVars.LoginUser.UserName;
							CommonDAO.GetInstance().SaveAppletLog(eAppletLogLevel.Info, "删除运输记录", logValue, SelfVars.LoginUser.UserAccount);
							CommonDAO.GetInstance().SaveHandleEvent("删除入厂煤运输记录", entity.Id);
							Dbers.GetInstance().SelfDber.Delete<CmcsBuyFuelTransport>(entity.Id);
							Dbers.GetInstance().SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = entity.Id });
						}
						catch (Exception)
						{
							MessageBoxEx.Show("该记录正在使用中，禁止删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}

						BindData();
					}
					break;
				case "clmPic":

					if (Dbers.GetInstance().SelfDber.Entities<CmcsTransportPicture>(String.Format(" where TransportId='{0}'", entity.Id)).Count > 0)
					{
						FrmTransportPicture frmPic = new FrmTransportPicture(entity.Id, entity.CarNumber);
						if (frmPic.ShowDialog() == DialogResult.OK)
						{
							BindData();
						}
					}
					else
					{
						MessageBoxEx.Show("暂无抓拍图片！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					break;
			}
		}

		private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
		{

			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				CmcsBuyFuelTransport entity = gridRow.DataItem as CmcsBuyFuelTransport;
				if (entity == null) return;

				// 填充有效状态
				gridRow.Cells["clmIsUse"].Value = (entity.IsUse == 1 ? "是" : "否");
				CmcsInFactoryBatch cmcsinfactorybatch = Dbers.GetInstance().SelfDber.Get<CmcsInFactoryBatch>(entity.InFactoryBatchId);
				if (cmcsinfactorybatch != null)
				{
					gridRow.Cells["clmInFactoryBatchNumber"].Value = cmcsinfactorybatch.Batch;
				}

				//List<CmcsTransportPicture> cmcstrainwatchs = Dbers.GetInstance().SelfDber.Entities<CmcsTransportPicture>(String.Format(" where TransportId='{0}'", gridRow.Cells["clmId"].Value));
				//if (cmcstrainwatchs.Count == 0)
				//{
				//    //gridRow.Cells["clmPic"].Value = "";
				//}

			}
		}

		#endregion

		private void tsmiPrint_Click(object sender, EventArgs e)
		{
			GridRow gridRow = superGridControl1.PrimaryGrid.ActiveRow as GridRow;
			if (gridRow == null) return;
			CmcsBuyFuelTransport entity = gridRow.DataItem as CmcsBuyFuelTransport;
			FrmPrintWeb frm = new FrmPrintWeb(entity);
			frm.ShowDialog();
		}

		private void btnAll_Click_1(object sender, EventArgs e)
		{
			this.SqlWhere = string.Empty;
			txtCarNumber_Ser.Text = string.Empty;
			cmbFuelKindName_BuyFuel.Text = string.Empty;
			cmbMineName_BuyFuel.Text = string.Empty;
			CurrentIndex = 0;
			BindData();
		}
	}
}
