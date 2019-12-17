using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.CarTransport.Queue.Core;
using CMCS.CarTransport.Queue.Enums;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using DevComponents.DotNetBar;
using DevComponents.Editors;

namespace CMCS.CarTransport.Queue.Frms.BaseInfo.Supplier
{
    public partial class FrmSupplier_Oper : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Var

        //业务id
        string PId = String.Empty;

        eEditMode editMode = eEditMode.默认;
        //编辑模式
        eEditMode EditMode
        {
            get { return editMode; }
            set
            {
                editMode = value;
                if (value == eEditMode.修改 || value == eEditMode.新增)
                    this.btnSubmit.Visible = true;
                else
                    this.btnSubmit.Visible = false;
            }
        }

        CmcsSupplier CmcsSupplier;

        #endregion

        public FrmSupplier_Oper(string pId, eEditMode editMode)
        {
            InitializeComponent();

            this.PId = pId;
            this.EditMode = editMode;
        }

        /// <summary>
        /// 窗体加载绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSupplier_Oper_Load(object sender, EventArgs e)
        {
            cmb_CreditRank.SelectedIndex = 0;
            cmb_Type.SelectedIndex = 0;

            this.CmcsSupplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(this.PId);
            if (this.CmcsSupplier != null)
            {
                txt_Code.Text = CmcsSupplier.Code;
                txt_Name.Text = CmcsSupplier.Name;
                HelperUtil.SelectedComboBoxItem(cmb_CreditRank, CmcsSupplier.CreditRank);
                txt_ShortName.Text = CmcsSupplier.ShortName;
                HelperUtil.SelectedComboBoxItem(cmb_Type, CmcsSupplier.Type);
                txt_RepIdentity.Text = CmcsSupplier.RepIdentity;
                txt_OrganizationCode.Text = CmcsSupplier.OrganizationCode;
                db_RegisterFund.Value = CmcsSupplier.RegisterFund;
                txt_LicenceNum.Text = CmcsSupplier.LicenceNum;
                txt_Operallver.Text = CmcsSupplier.Operallver;
                txt_TaxregCode.Text = CmcsSupplier.TaxregCode;
                txt_LinkMan.Text = CmcsSupplier.LinkMan;
                txt_LinkTel.Text = CmcsSupplier.LinkTel;
                txt_Facsimile.Text = CmcsSupplier.Facsimile;
                txt_Email.Text = CmcsSupplier.Email;
                txt_PostalCode.Text = CmcsSupplier.PostalCode;
                txt_Address.Text = CmcsSupplier.Address;
                txt_Remark.Text = CmcsSupplier.Remark;
                chb_IsUse.Checked = (CmcsSupplier.IsStop == 0);
            }

            if (this.EditMode == eEditMode.查看)
            {
                btnSubmit.Enabled = false;
                HelperUtil.ControlReadOnly(panelEx2, true);
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text.Length == 0)
            {
                MessageBoxEx.Show("该供应商名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((CmcsSupplier == null || CmcsSupplier.Name != txt_Name.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsSupplier>(" where Name=:Name", new { Name = txt_Name.Text }).Count > 0)
            {
                MessageBoxEx.Show("该供应商名称不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_Code.Text.Length == 0)
            {
                MessageBoxEx.Show("该供应商编号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((CmcsSupplier == null || CmcsSupplier.Code != txt_Code.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsSupplier>(" where Code=:Code", new { Code = txt_Code.Text }).Count > 0)
            {
                MessageBoxEx.Show("该供应商编号不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_OrganizationCode.Text.Length == 0)
            {
                MessageBoxEx.Show("组织机构代码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((CmcsSupplier == null || CmcsSupplier.OrganizationCode != txt_OrganizationCode.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsSupplier>(" where OrganizationCode=:OrganizationCode", new { OrganizationCode = txt_OrganizationCode.Text }).Count > 0)
            {
                MessageBoxEx.Show("组织机构代码不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_ShortName.Text.Length == 0)
            {
                MessageBoxEx.Show("供应商简称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.EditMode == eEditMode.修改)
            {
                CmcsSupplier.OperUser = CmcsSupplier.CreateUser;
                CmcsSupplier.Name = txt_Name.Text;
                CmcsSupplier.Code = txt_Code.Text;
                CmcsSupplier.ShortName = txt_ShortName.Text;
                CmcsSupplier.CreditRank = (cmb_CreditRank.SelectedItem as ComboItem).Text;
                CmcsSupplier.Type = (cmb_Type.SelectedItem as ComboItem).Text;
                CmcsSupplier.RepIdentity = txt_RepIdentity.Text;
                //CmcsSupplier.OrganizationCode = txt_OrganizationCode.Text;
                CmcsSupplier.RegisterFund = db_RegisterFund.Value;
                CmcsSupplier.LicenceNum = txt_LicenceNum.Text;
                CmcsSupplier.Operallver = txt_Operallver.Text;
                CmcsSupplier.TaxregCode = txt_TaxregCode.Text;
                CmcsSupplier.LinkMan = txt_LinkMan.Text;
                CmcsSupplier.LinkTel = txt_LinkTel.Text;
                CmcsSupplier.Facsimile = txt_Facsimile.Text;
                CmcsSupplier.Email = txt_Email.Text;
                CmcsSupplier.PostalCode = txt_PostalCode.Text;
                CmcsSupplier.Address = txt_Address.Text;
                CmcsSupplier.Remark = txt_Remark.Text;
                CmcsSupplier.IsStop = (chb_IsUse.Checked ? 0 : 1);
                CmcsSupplier.IsCheck = 1;

                Dbers.GetInstance().SelfDber.Update(CmcsSupplier);
            }
            else if (this.EditMode == eEditMode.新增)
            {
                CmcsSupplier = new CmcsSupplier();
                CmcsSupplier.CreateUser = SelfVars.LoginUser.UserAccount;
                CmcsSupplier.OperUser = CmcsSupplier.CreateUser;
                CmcsSupplier.Name = txt_Name.Text;
                CmcsSupplier.Code = txt_Code.Text;
                CmcsSupplier.ShortName = txt_ShortName.Text;
                CmcsSupplier.CreditRank = (cmb_CreditRank.SelectedItem as ComboItem).Text;
                CmcsSupplier.Type = (cmb_Type.SelectedItem as ComboItem).Text;
                CmcsSupplier.RepIdentity = txt_RepIdentity.Text;
                CmcsSupplier.OrganizationCode = txt_OrganizationCode.Text;
                CmcsSupplier.RegisterFund = db_RegisterFund.Value;
                CmcsSupplier.LicenceNum = txt_LicenceNum.Text;
                CmcsSupplier.Operallver = txt_Operallver.Text;
                CmcsSupplier.TaxregCode = txt_TaxregCode.Text;
                CmcsSupplier.LinkMan = txt_LinkMan.Text;
                CmcsSupplier.LinkTel = txt_LinkTel.Text;
                CmcsSupplier.Facsimile = txt_Facsimile.Text;
                CmcsSupplier.Email = txt_Email.Text;
                CmcsSupplier.PostalCode = txt_PostalCode.Text;
                CmcsSupplier.Address = txt_Address.Text;
                CmcsSupplier.Remark = txt_Remark.Text;
                CmcsSupplier.IsStop = (chb_IsUse.Checked ? 0 : 1);
                CmcsSupplier.IsCheck = 1;
                CmcsSupplier.DataFrom = "智能化排队程序录入";
                Dbers.GetInstance().SelfDber.Insert(CmcsSupplier);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}