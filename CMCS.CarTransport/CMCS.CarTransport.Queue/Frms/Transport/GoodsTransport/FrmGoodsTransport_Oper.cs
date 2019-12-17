using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.CarTransport.Queue.Core;
using CMCS.CarTransport.Queue.Frms.BaseInfo.SupplyReceive;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using DevComponents.DotNetBar;
using CMCS.CarTransport.Queue.Enums;

namespace CMCS.CarTransport.Queue.Frms.Transport.GoodsTransport
{
    public partial class FrmGoodsTransport_Oper : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Var

        //业务id
        string PId = String.Empty;

        //编辑模式
        eEditMode EditMode = eEditMode.默认;

        CmcsGoodsTransport CmcsGoodsTransport;

        CmcsSupplyReceive supplyUnit;
        /// <summary>
        /// 供货单位
        /// </summary>
        public CmcsSupplyReceive SupplyUnit
        {
            get { return supplyUnit; }
            set { supplyUnit = value; }
        }

        CmcsSupplyReceive receiveUnit;
        /// <summary>
        /// 收货单位
        /// </summary>
        public CmcsSupplyReceive ReceiveUnit
        {
            get { return receiveUnit; }
            set { receiveUnit = value; }
        }

        CmcsGoodsType cmcsGoodsType;
        /// <summary>
        /// 物资类型
        /// </summary>
        public CmcsGoodsType CmcsGoodsType
        {
            get { return cmcsGoodsType; }
            set { cmcsGoodsType = value; }
        }

        #endregion

        public FrmGoodsTransport_Oper(string pId, eEditMode editMode)
        {
            InitializeComponent();

            this.PId = pId;
            this.EditMode = editMode;
        }

        private void FrmGoodsTransport_Oper_Load(object sender, EventArgs e)
        {
            cmb_GoodsTypeName.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsGoodsType>(" where ParentId is not null order by OrderNumber");
            cmb_GoodsTypeName.DisplayMember = "GoodsName";
            cmb_GoodsTypeName.ValueMember = "Id";
            cmb_GoodsTypeName.SelectedIndex = 0;

            this.CmcsGoodsTransport = Dbers.GetInstance().SelfDber.Get<CmcsGoodsTransport>(this.PId);
            if (this.CmcsGoodsTransport != null)
            {
                txt_SerialNumber.Text = CmcsGoodsTransport.SerialNumber;
                txt_CarNumber.Text = CmcsGoodsTransport.CarNumber;
                txt_SupplyUnitName.Text = CmcsGoodsTransport.SupplyUnitName;
                txt_ReceiveUnitName.Text = CmcsGoodsTransport.ReceiveUnitName;
                dbi_FirstWeight.Value = (double)CmcsGoodsTransport.FirstWeight;
                dbi_SecondWeight.Value = (double)CmcsGoodsTransport.SecondWeight;
                cmb_GoodsTypeName.Text = CmcsGoodsTransport.GoodsTypeName;
                dbi_SuttleWeight.Value = (double)CmcsGoodsTransport.SuttleWeight;
                txt_InFactoryTime.Text = CmcsGoodsTransport.InFactoryTime.Year == 1 ? "" : CmcsGoodsTransport.InFactoryTime.ToString();
                txt_OutFactoryTime.Text = CmcsGoodsTransport.OutFactoryTime.Year == 1 ? "" : CmcsGoodsTransport.OutFactoryTime.ToString();
                txt_FirstTime.Text = CmcsGoodsTransport.FirstTime.Year == 1 ? "" : CmcsGoodsTransport.FirstTime.ToString();
                txt_SecondTime.Text = CmcsGoodsTransport.SecondTime.Year == 1 ? "" : CmcsGoodsTransport.SecondTime.ToString();
                txt_Remark.Text = CmcsGoodsTransport.Remark;
                chb_IsFinish.Checked = (CmcsGoodsTransport.IsFinish == 1);
                chb_IsUse.Checked = (CmcsGoodsTransport.IsUse == 1);
            }

            if (this.EditMode == eEditMode.查看)
            {
                btnSubmit.Enabled = false;
                HelperUtil.ControlReadOnly(panelEx2, true);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.EditMode == eEditMode.修改)
            {
                CmcsGoodsTransport.FirstWeight = (decimal)dbi_FirstWeight.Value;
                CmcsGoodsTransport.SecondWeight = (decimal)dbi_SecondWeight.Value;
                CmcsGoodsTransport.SuttleWeight = (decimal)dbi_SuttleWeight.Value;
                if (supplyUnit != null)
                {
                    CmcsGoodsTransport.ReceiveUnitId = receiveUnit.Id;
                    CmcsGoodsTransport.ReceiveUnitName = receiveUnit.UnitName;
                }
                if (receiveUnit != null)
                {
                    CmcsGoodsTransport.SupplyUnitId = supplyUnit.Id;
                    CmcsGoodsTransport.SupplyUnitName = supplyUnit.UnitName;
                }
                if (cmcsGoodsType != null)
                {
                    CmcsGoodsTransport.GoodsTypeId = cmcsGoodsType.Id;
                    CmcsGoodsTransport.GoodsTypeName = cmcsGoodsType.GoodsName;
                }
                txt_Remark.Text = CmcsGoodsTransport.Remark;
                CmcsGoodsTransport.IsFinish = (chb_IsFinish.Checked ? 1 : 0);
                CmcsGoodsTransport.IsUse = (chb_IsUse.Checked ? 1 : 0);
                Dbers.GetInstance().SelfDber.Update(CmcsGoodsTransport);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        private void btnReceiveUnit_Click(object sender, EventArgs e)
        {
            FrmSupplyReceive_Select Frm = new FrmSupplyReceive_Select(string.Empty);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                receiveUnit = Frm.Output;
            }
        }

        private void btnSupplyUnit_Click(object sender, EventArgs e)
        {
            FrmSupplyReceive_Select Frm = new FrmSupplyReceive_Select(string.Empty);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                supplyUnit = Frm.Output;
            }
        }

        private void cmb_GoodsTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmcsGoodsType = cmb_GoodsTypeName.SelectedItem as CmcsGoodsType;
        }

    }
}