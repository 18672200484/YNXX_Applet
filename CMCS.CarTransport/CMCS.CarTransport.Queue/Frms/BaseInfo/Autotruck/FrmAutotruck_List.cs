using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.CarTransport.Queue.Enums;
using CMCS.Common.Utilities;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using System.Linq;
using System.IO;

namespace CMCS.CarTransport.Queue.Frms.BaseInfo.Autotruck
{
	public partial class FrmAutotruck_List : MetroAppForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmAutotruck_List";

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

		public FrmAutotruck_List()
		{
			InitializeComponent();
		}

		private void FrmAutotruck_List_Load(object sender, EventArgs e)
		{
			superGridControl1.PrimaryGrid.AutoGenerateColumns = false;

			btnSearch_Click(null, null);
		}

		public void BindData()
		{
			string tempSqlWhere = this.SqlWhere;
			List<CmcsAutotruck> list = Dbers.GetInstance().SelfDber.ExecutePager<CmcsAutotruck>(PageSize, CurrentIndex, tempSqlWhere + " order by CreateDate desc");
			superGridControl1.PrimaryGrid.DataSource = list;

			GetTotalCount(tempSqlWhere);
			PagerControlStatue();

			lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			FrmAutotruck_Oper frmEdit = new FrmAutotruck_Oper(Guid.NewGuid().ToString(), eEditMode.新增);
			if (frmEdit.ShowDialog() == DialogResult.OK)
			{
				BindData();
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.SqlWhere = " where 1=1";

			if (!string.IsNullOrEmpty(txtCarNumber_Ser.Text)) this.SqlWhere += " and CarNumber like '%" + txtCarNumber_Ser.Text + "%'";

			CurrentIndex = 0;
			BindData();
		}

		private void btnAll_Click(object sender, EventArgs e)
		{
			this.SqlWhere = string.Empty;
			txtCarNumber_Ser.Text = string.Empty;

			CurrentIndex = 0;
			BindData();
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

		private void GetTotalCount(string sqlWhere)
		{
			TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsAutotruck>(sqlWhere);
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
			CmcsAutotruck entity = Dbers.GetInstance().SelfDber.Get<CmcsAutotruck>(superGridControl1.PrimaryGrid.GetCell(e.GridCell.GridRow.Index, superGridControl1.PrimaryGrid.Columns["clmId"].ColumnIndex).Value.ToString());
			switch (superGridControl1.PrimaryGrid.Columns[e.GridCell.ColumnIndex].Name)
			{

				case "clmShow":
					FrmAutotruck_Oper frmShow = new FrmAutotruck_Oper(entity.Id, eEditMode.查看);
					if (frmShow.ShowDialog() == DialogResult.OK)
					{
						BindData();
					}
					break;
				case "clmEdit":
					FrmAutotruck_Oper frmEdit = new FrmAutotruck_Oper(entity.Id, eEditMode.修改);
					if (frmEdit.ShowDialog() == DialogResult.OK)
					{
						BindData();
					}
					break;
				case "clmDelete":
					// 查询正在使用该车牌号的车数 
					if (MessageBoxEx.Show("确定要删除该车牌号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						try
						{
							Dbers.GetInstance().SelfDber.Delete<CmcsAutotruck>(entity.Id);
						}
						catch (Exception)
						{
							MessageBoxEx.Show("该车牌号正在使用中，禁止删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}

						BindData();
					}
					break;
			}
		}

		private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				CmcsAutotruck entity = gridRow.DataItem as CmcsAutotruck;
				if (entity == null) return;

				// 填充有效状态
				gridRow.Cells["clmIsUse"].Value = (entity.IsUse == 1 ? "是" : "否");
			}
		}

		/// <summary>
		/// 设置行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
		{
			e.Text = (e.GridRow.RowIndex + 1).ToString();
		}

		#endregion

		/// <summary>
		/// 导入
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnImport_Click(object sender, EventArgs e)
		{
			int res = 0;
			try
			{
				openFileDialog1.Filter = "(*.xlsx)|*.xlsx|(*.xls)|*.xls";
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					//获取用户选择文件的后缀名
					string extension = Path.GetExtension(openFileDialog1.FileName);
					//声明允许的后缀名
					string[] str = new string[] { ".xls", ".xlsx" };
					if (!str.Contains(extension))
					{
						MessageBoxEx.Show("仅能导入xls,xlsx格式的文件！");
						return;
					}

					DataTable data = CommonDAO.GetInstance().GetExcelDatatable(openFileDialog1.FileName, "ImportTable");

					if (data != null && data.Rows.Count > 0)
					{
						foreach (DataRow item in data.Rows)
						{
							string carNumber = item[0] != DBNull.Value ? item[0].ToString() : "";
							if (string.IsNullOrEmpty(carNumber) || carNumber.Length != 7)
							{
								Log4Neter.Info(string.Format("车牌号：{0}导入失败，车牌号格式不正确。", carNumber));
								continue;
							}
							//车号
							CmcsAutotruck autoTruck = CommonDAO.GetInstance().SelfDber.Entity<CmcsAutotruck>("where CarNumber=:CarNumber", new { CarNumber = carNumber });
							if (autoTruck == null)
							{
								autoTruck = new CmcsAutotruck();
								autoTruck.CarNumber = carNumber;
								autoTruck.CarType = eCarType.入厂煤.ToString();
								autoTruck.IsUse = 1;

								autoTruck.Driver = item[1] != DBNull.Value ? item[1].ToString() : "";
								autoTruck.CellPhoneNumber = item[2] != DBNull.Value ? item[2].ToString() : "";
								autoTruck.CarriageTotalLength = item[3] != DBNull.Value ? Convert.ToInt32(item[3]) : 0;
								autoTruck.CarriageLength = item[4] != DBNull.Value ? Convert.ToInt32(item[4]) : 0;
								autoTruck.CarriageWidth = item[5] != DBNull.Value ? Convert.ToInt32(item[5]) : 0;
								autoTruck.CarriageHeight = item[6] != DBNull.Value ? Convert.ToInt32(item[6]) : 0;
								autoTruck.CarriageBottomToFloor = item[7] != DBNull.Value ? Convert.ToInt32(item[7]) : 0;
								autoTruck.LeftObstacle1 = item[8] != DBNull.Value ? Convert.ToInt32(item[8]) : 0;
								autoTruck.LeftObstacle2 = item[9] != DBNull.Value ? Convert.ToInt32(item[9]) : 0;
								autoTruck.LeftObstacle3 = item[10] != DBNull.Value ? Convert.ToInt32(item[10]) : 0;
								autoTruck.LeftObstacle4 = item[11] != DBNull.Value ? Convert.ToInt32(item[11]) : 0;
								autoTruck.LeftObstacle5 = item[12] != DBNull.Value ? Convert.ToInt32(item[12]) : 0;
								autoTruck.LeftObstacle6 = item[13] != DBNull.Value ? Convert.ToInt32(item[13]) : 0;
								autoTruck.ReMark = item[14] != DBNull.Value ? item[14].ToString() : "";

								res += Dbers.GetInstance().SelfDber.Insert(autoTruck);
							}
							else
							{
								autoTruck.Driver = item[1] != DBNull.Value ? item[1].ToString() : "";
								autoTruck.CellPhoneNumber = item[2] != DBNull.Value ? item[2].ToString() : "";
								autoTruck.CarriageTotalLength = item[3] != DBNull.Value ? Convert.ToInt32(item[3]) : 0;
								autoTruck.CarriageLength = item[4] != DBNull.Value ? Convert.ToInt32(item[4]) : 0;
								autoTruck.CarriageWidth = item[5] != DBNull.Value ? Convert.ToInt32(item[5]) : 0;
								autoTruck.CarriageHeight = item[6] != DBNull.Value ? Convert.ToInt32(item[6]) : 0;
								autoTruck.CarriageBottomToFloor = item[7] != DBNull.Value ? Convert.ToInt32(item[7]) : 0;
								autoTruck.LeftObstacle1 = item[8] != DBNull.Value ? Convert.ToInt32(item[8]) : 0;
								autoTruck.LeftObstacle2 = item[9] != DBNull.Value ? Convert.ToInt32(item[9]) : 0;
								autoTruck.LeftObstacle3 = item[10] != DBNull.Value ? Convert.ToInt32(item[10]) : 0;
								autoTruck.LeftObstacle4 = item[11] != DBNull.Value ? Convert.ToInt32(item[11]) : 0;
								autoTruck.LeftObstacle5 = item[12] != DBNull.Value ? Convert.ToInt32(item[12]) : 0;
								autoTruck.LeftObstacle6 = item[13] != DBNull.Value ? Convert.ToInt32(item[13]) : 0;
								autoTruck.ReMark = item[14] != DBNull.Value ? item[14].ToString() : "";

								res += Dbers.GetInstance().SelfDber.Update(autoTruck);
							}

						}
					}
					MessageBoxEx.Show("成功导入" + res + "条信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					btnSearch_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("导入失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
