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

namespace CMCS.CarTransport.Queue.Frms.BaseInfo.TransportCompany
{
    public partial class FrmTransportCompany_Oper : DevComponents.DotNetBar.Metro.MetroForm
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

        CmcsTransportCompany CmcsTransportCompany;

        #endregion

        public FrmTransportCompany_Oper(string pId, eEditMode editMode)
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
        private void FrmTransportCompany_Oper_Load(object sender, EventArgs e)
        {
            this.CmcsTransportCompany = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(this.PId);
            if (this.CmcsTransportCompany != null)
            {
                txt_Code.Text = CmcsTransportCompany.Code;
                txt_Name.Text = CmcsTransportCompany.Name;
                txt_ShortName.Text = CmcsTransportCompany.ShortName;
                txt_OrganizationCode.Text = CmcsTransportCompany.OrganizationCode;
                chb_IsUse.Checked = (CmcsTransportCompany.IsStop == 0);
                txt_Remark.Text = CmcsTransportCompany.Remark;
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
                MessageBoxEx.Show("运输单位名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_Code.Text.Length == 0)
            {
                MessageBoxEx.Show("运输单位编号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_OrganizationCode.Text.Length == 0)
            {
                MessageBoxEx.Show("组织机构代码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_ShortName.Text.Length == 0)
            {
                MessageBoxEx.Show("运输单位简称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((CmcsTransportCompany == null || CmcsTransportCompany.Name != txt_Name.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsTransportCompany>(" where Name=:Name", new { Name = txt_Name.Text }).Count > 0)
            {
                MessageBoxEx.Show("该单位名称不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((CmcsTransportCompany == null || CmcsTransportCompany.Code != txt_Code.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsTransportCompany>(" where Code=:Code", new { Code = txt_Code.Text }).Count > 0)
            {
                MessageBoxEx.Show("该单位编号不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((CmcsTransportCompany == null || CmcsTransportCompany.OrganizationCode != txt_OrganizationCode.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsTransportCompany>(" where OrganizationCode=:OrganizationCode", new { OrganizationCode = txt_OrganizationCode.Text }).Count > 0)
            {
                MessageBoxEx.Show("组织机构代码不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.EditMode == eEditMode.修改)
            {
                CmcsTransportCompany.Name = txt_Name.Text;
                CmcsTransportCompany.Code = txt_Code.Text;
                CmcsTransportCompany.IsStop = (chb_IsUse.Checked ? 0 : 1);
                CmcsTransportCompany.ShortName = txt_ShortName.Text;
                CmcsTransportCompany.OrganizationCode = txt_OrganizationCode.Text;
                CmcsTransportCompany.Remark = txt_Remark.Text;
                Dbers.GetInstance().SelfDber.Update(CmcsTransportCompany);
            }
            else if (this.EditMode == eEditMode.新增)
            {
                CmcsTransportCompany = new CmcsTransportCompany();
                CmcsTransportCompany.Name = txt_Name.Text;
                CmcsTransportCompany.Code = txt_Code.Text;
                CmcsTransportCompany.IsStop = (chb_IsUse.Checked ? 0 : 1);
                CmcsTransportCompany.ShortName = txt_ShortName.Text;
                CmcsTransportCompany.OrganizationCode = txt_OrganizationCode.Text;
                CmcsTransportCompany.Remark = txt_Remark.Text;
                Dbers.GetInstance().SelfDber.Insert(CmcsTransportCompany);
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