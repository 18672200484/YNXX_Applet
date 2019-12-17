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
using CMCS.Common.Entities;
using CMCS.Common.Entities.Fuel;
using CMCS.CarTransport.Queue.Frms.Transport.Print;
using CMCS.Common.Enums;
using System.Linq;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.Entities.BaseInfo;
using CMCS.CarTransport.Queue.Utilities;

namespace CMCS.CarTransport.Queue.Frms.Transport.SaleFuelTransport
{
    public partial class FrmSaleFuelTransport_Detail : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmSaleFuelTransport_Detail";

        WagonPrinterDetail wagonPrinter = null;
        List<CmcsSaleFuelTransport> listCount = new List<CmcsSaleFuelTransport>();

        string SqlWhere = string.Empty;

        public FrmSaleFuelTransport_Detail()
        {
            InitializeComponent();
        }

        private void FrmBuyFuelTransport_List_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;

            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
            LoadMine(cmbMineName_BuyFuel);
            LoadFuelkind(cmbFuelKindName_BuyFuel);
            this.wagonPrinter = new WagonPrinterDetail(printDocument1);

            btnSearch_Click(null, null);
        }
        /// <summary>
        /// 加载收货单位
        /// </summary>
        void LoadMine(ComboBoxEx comboBoxEx)
        {
            IList<CmcsSupplyReceive> list = Dbers.GetInstance().SelfDber.Entities<CmcsSupplyReceive>("where IsValid=1 order by UnitName");
            foreach (CmcsSupplyReceive item in list)
            {
                comboBoxEx.Items.Add(new ComboBoxItem(item.Id, item.UnitName));
            }
            comboBoxEx.Items.Insert(0, new ComboBoxItem("", ""));
        }
        /// <summary>
        /// 加载煤种
        /// </summary>
        void LoadFuelkind(ComboBoxEx comboBoxEx)
        {
            IList<CmcsFuelKind> list = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='有效' and ParentId is not null order by Sequence");
            foreach (CmcsFuelKind item in list)
            {
                comboBoxEx.Items.Add(new ComboBoxItem(item.Id, item.FuelName));
            }
            comboBoxEx.Items.Insert(0, new ComboBoxItem("", ""));
        }

        public void BindData()
        {
            listCount.Clear();
            string tempSqlWhere = this.SqlWhere;
            listCount = Dbers.GetInstance().SelfDber.Entities<CmcsSaleFuelTransport>(tempSqlWhere + " order by SerialNumber desc");

            listCount.OrderBy(a => a.SupplierId);
            CmcsSaleFuelTransport listTotal1 = new CmcsSaleFuelTransport();
            listTotal1.SupplierId = "合计";
            listTotal1.GrossWeight = listCount.Sum(a => a.GrossWeight);
            listTotal1.TareWeight = listCount.Sum(a => a.TareWeight);
            listTotal1.SuttleWeight = listCount.Sum(a => a.SuttleWeight);
            listTotal1.IsFinish = listCount.Count;//车数
            listCount.Add(listTotal1);

            superGridControl1.PrimaryGrid.DataSource = listCount;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = " where 1=1";
            if (dtpStartTime.Value != DateTime.MinValue) this.SqlWhere += " and to_char(GrossTime,'yyyy-MM-dd') >= '" + dtpStartTime.Value.ToString("yyyy-MM-dd") + "'";
            if (dtpEndTime.Value != DateTime.MinValue) this.SqlWhere += " and to_char(GrossTime,'yyyy-MM-dd') <'" + dtpEndTime.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
            if (!string.IsNullOrEmpty(txtMineName_Ser.Text)) this.SqlWhere += " and CarNumber like '%'||" + txtMineName_Ser.Text + "||'%'";
            if (!string.IsNullOrEmpty(cmbFuelKindName_BuyFuel.Text)) this.SqlWhere += " and FuelKindId = '" + ((ComboBoxItem)(cmbFuelKindName_BuyFuel.SelectedItem)).Name + "'";
            if (!string.IsNullOrEmpty(cmbMineName_BuyFuel.Text)) this.SqlWhere += " and SupplierId = '" + ((ComboBoxItem)(cmbMineName_BuyFuel.SelectedItem)).Name + "'";
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            txtMineName_Ser.Text = string.Empty;
            cmbMineName_BuyFuel.Text = string.Empty;
            cmbFuelKindName_BuyFuel.Text = string.Empty;
            BindData();
        }

        private void btnInStore_Click(object sender, EventArgs e)
        {
            FrmSaleFuelTransport_Oper frm = new FrmSaleFuelTransport_Oper();
            frm.ShowDialog();

            BindData();
        }

        #region GridView

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsSaleFuelTransport entity = gridRow.DataItem as CmcsSaleFuelTransport;
                if (entity == null) return;
                CmcsSupplier supplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(entity.SupplierId);
                if (supplier != null)
                    gridRow.Cells["clmSupplier"].Value = supplier.Name;
                CmcsFuelKind fuelkind = Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(entity.FuelKindId);
                if (fuelkind != null)
                    gridRow.Cells["clmFuelKind"].Value = fuelkind.FuelName;
                CmcsTransportCompany company = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(entity.TransportCompanyId);
                if (company != null)
                    gridRow.Cells["clmTransportCompany"].Value = company.Name;

                if (entity.SupplierId == "合计")
                {
                    gridRow.Cells["clmSupplier"].Value = "合计";
                    gridRow.Cells["clmFuelKind"].Visible = false;
                    gridRow.Cells["clmTransportCompany"].Visible = false;
                    gridRow.Cells["clmGrossTime"].Visible = false;
                    gridRow.Cells["clmTareTime"].Visible = false;
                }
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
        /// 打印磅单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPrint_Click(object sender, EventArgs e)
        {
            //this.wagonPrinter.Print(this.listCount, null, dtpStartTime.Value, dtpEndTime.Value);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ImportExcel import = new ImportExcel();
            import.DataGridViewToExcels(this.superGridControl1);
        }
    }
}
