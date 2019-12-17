using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
//
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
    public partial class FrmSaleFuelTransport_Collect : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmSaleFuelTransport_Collect";

        WagonPrinterCollect wagonPrinter = null;
        List<CmcsSaleFuelTransport> listCount = new List<CmcsSaleFuelTransport>();

        string SqlWhere = string.Empty;

        public FrmSaleFuelTransport_Collect()
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
            this.wagonPrinter = new WagonPrinterCollect(printDocument1);

            btnSearch_Click(null, null);
        }
        /// <summary>
        /// 加载矿点
        /// </summary>
        void LoadMine(ComboBoxEx comboBoxEx)
        {
            IList<CmcsMine> list = Dbers.GetInstance().SelfDber.Entities<CmcsMine>("where Valid='有效' and ParentId is not null order by Sequence");
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
            List<CmcsSaleFuelTransport> list = Dbers.GetInstance().SelfDber.Entities<CmcsSaleFuelTransport>(tempSqlWhere + " order by SerialNumber desc");

            var minename = from p in list group p by new { p.SupplierId } into g select new { SupplierId = g.Key.SupplierId };

            foreach (var item in minename)
            {
                List<CmcsSaleFuelTransport> listone = list.Where(a => a.SupplierId == item.SupplierId).ToList();
                if (listone != null && listone.Count > 0)
                {
                    CmcsSaleFuelTransport entity = new CmcsSaleFuelTransport();
                    entity.SupplierId = listone[0].SupplierId;
                    entity.FuelKindId = listone[0].FuelKindId;
                    entity.GrossWeight = listone.Sum(a => a.GrossWeight);
                    entity.TareWeight = listone.Sum(a => a.TareWeight);
                    entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
                    entity.IsFinish = listone.Count;//把车数放到完成状态，用来过渡数据
                    listCount.Add(entity);
                }
            }
            listCount.OrderBy(a => a.SupplierId);
            if (chkFuelKindTotal.Checked && !chkMineTotal.Checked)
            {
                var fuelname = from p in list group p by new { p.FuelKindId } into g select new { FuelKindName = g.Key.FuelKindId };
                listCount.Clear();
                foreach (var item in fuelname)
                {
                    List<CmcsSaleFuelTransport> listone = list.Where(a => a.FuelKindId == item.FuelKindId).ToList();
                    if (listone != null && listone.Count > 0)
                    {
                        CmcsSaleFuelTransport entity = new CmcsSaleFuelTransport();
                        entity.MineName = listone[0].MineName;
                        entity.FuelKindName = listone[0].FuelKindName;
                        entity.GrossWeight = listone.Sum(a => a.GrossWeight);
                        entity.TareWeight = listone.Sum(a => a.TareWeight);
                        entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
                        entity.IsFinish = listone.Count;//把车数放到完成状态，用来过渡数据
                        listCount.Add(entity);
                    }
                }
                listCount.OrderBy(a => a.FuelKindId);
            }
            else if (chkMineTotal.Checked && chkFuelKindTotal.Checked)
            {
                var minefuelname = from p in list group p by new { p.MineName, p.FuelKindName } into g select new { MineName = g.Key.MineName, FuelKindName = g.Key.FuelKindName };
                listCount.Clear();
                foreach (var item in minefuelname)
                {
                    List<CmcsSaleFuelTransport> listone = list.Where(a => a.MineName == item.MineName && a.FuelKindName == item.FuelKindName).ToList();
                    if (listone != null && listone.Count > 0)
                    {
                        CmcsSaleFuelTransport entity = new CmcsSaleFuelTransport();
                        entity.MineName = listone[0].MineName;
                        entity.FuelKindName = listone[0].FuelKindName;
                        entity.TicketWeight = listone.Sum(a => a.TicketWeight);
                        entity.GrossWeight = listone.Sum(a => a.GrossWeight);
                        entity.TareWeight = listone.Sum(a => a.TareWeight);
                        entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
                        entity.DeductWeight = listone.Sum(a => a.DeductWeight);
                        entity.KsWeight = listone.Sum(a => a.AutoKsWeight + a.KsWeight);
                        entity.KgWeight = listone.Sum(a => a.KgWeight);
                        entity.CheckWeight = listone.Sum(a => a.CheckWeight);
                        entity.IsFinish = listone.Count;//把车数放到完成状态，用来过渡数据
                        listCount.Add(entity);
                    }
                }
                listCount.OrderBy(a => a.MineName).OrderBy(a => a.FuelKindName);
            }


            CmcsSaleFuelTransport listTotal1 = new CmcsSaleFuelTransport();
            listTotal1.MineName = "合计";
            listTotal1.FuelKindName = "";
            listTotal1.TicketWeight = list.Sum(a => a.TicketWeight);
            listTotal1.GrossWeight = list.Sum(a => a.GrossWeight);
            listTotal1.TareWeight = list.Sum(a => a.TareWeight);
            listTotal1.SuttleWeight = list.Sum(a => a.SuttleWeight);
            listTotal1.DeductWeight = list.Sum(a => a.DeductWeight);
            listTotal1.KsWeight = list.Sum(a => a.AutoKsWeight + a.KsWeight);
            listTotal1.KgWeight = list.Sum(a => a.KgWeight);
            listTotal1.CheckWeight = list.Sum(a => a.CheckWeight);
            listTotal1.IsFinish = list.Count;//车数
            listCount.Add(listTotal1);

            superGridControl1.PrimaryGrid.DataSource = listCount;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = " where TareWeight>0";
            if (dtpStartTime.Value != DateTime.MinValue) this.SqlWhere += " and to_char(TareTime,'yyyy-MM-dd') >= '" + dtpStartTime.Value.ToString("yyyy-MM-dd") + "'";
            if (dtpEndTime.Value != DateTime.MinValue) this.SqlWhere += " and to_char(TareTime,'yyyy-MM-dd') < '" + dtpEndTime.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
            if (!string.IsNullOrEmpty(cmbMineName_BuyFuel.Text)) this.SqlWhere += " and MineName = '" + cmbMineName_BuyFuel.Text + "'";
            if (!string.IsNullOrEmpty(cmbFuelKindName_BuyFuel.Text)) this.SqlWhere += " and FuelKindName = '" + cmbFuelKindName_BuyFuel.Text + "'";

            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            cmbMineName_BuyFuel.Text = string.Empty;
            cmbFuelKindName_BuyFuel.Text = string.Empty;
            BindData();
        }

        private void btnInStore_Click(object sender, EventArgs e)
        {
            FrmBuyFuelTransport_Oper frm = new FrmBuyFuelTransport_Oper();
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
            this.wagonPrinter.Print(this.listCount, null, dtpStartTime.Value, dtpEndTime.Value);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ImportExcel import = new ImportExcel();
            import.DataGridViewToExcels(this.superGridControl1);
        }
    }
}
