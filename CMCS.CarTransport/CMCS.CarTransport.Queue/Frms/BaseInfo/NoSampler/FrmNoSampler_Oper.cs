using System;
using System.Windows.Forms;
using CMCS.CarTransport.Queue.Core;
using CMCS.CarTransport.Queue.Enums;
//
using CMCS.CarTransport.Queue.Frms.BaseInfo.Mine;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using DevComponents.DotNetBar;

namespace CMCS.CarTransport.Queue.Frms.BaseInfo.NoSampler
{
	public partial class FrmNoSampler_Oper : DevComponents.DotNetBar.Metro.MetroForm
	{
		String id = String.Empty;
		eEditMode EditMode = eEditMode.默认;
		CmcsNoSampler noSampler;

		CmcsMine cmcsMine;
		/// <summary>
		/// 矿点
		/// </summary>
		public CmcsMine CmcsMine
		{
			get { return cmcsMine; }
			set
			{
				cmcsMine = value;

				if (value != null)
				{
					txt_MineName.Text = value.Name;
				}
				else
				{
					txt_MineName.ResetText();
				}
			}
		}

		public FrmNoSampler_Oper()
		{
			InitializeComponent();
		}

		public FrmNoSampler_Oper(CmcsNoSampler pId, eEditMode editMode)
		{
			InitializeComponent();
			noSampler = pId;
			this.EditMode = editMode;
		}


		private void FrmAppletLog_Oper_Load(object sender, EventArgs e)
		{
			if (this.noSampler != null)
			{
				this.CmcsMine = CommonDAO.GetInstance().SelfDber.Get<CmcsMine>(noSampler.MineId);
				dtpStartTime.Value = noSampler.StartTime;
				dtpEndTime.Value = noSampler.EndTime;
			}

			if (this.EditMode == eEditMode.查看)
			{
				btnSubmit.Enabled = false;
				HelperUtil.ControlReadOnly(panelEx2, true);
			}
		}

		private void BtnMine_Click(object sender, EventArgs e)
		{
			FrmMine_Select Frm = new FrmMine_Select(string.Empty);
			Frm.ShowDialog();
			if (Frm.DialogResult == DialogResult.OK)
			{
				this.CmcsMine = Frm.Output;
			}
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txt_MineName.Text))
			{
				MessageBoxEx.Show("请选择矿点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (string.IsNullOrEmpty(this.dtpStartTime.Text))
			{
				MessageBoxEx.Show("请选择开始时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (string.IsNullOrEmpty(this.dtpEndTime.Text))
			{
				MessageBoxEx.Show("请选择结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (this.EditMode == eEditMode.修改)
			{
				noSampler.MineId = this.CmcsMine.Id;
				noSampler.MineName = this.CmcsMine.Name;
				noSampler.StartTime = this.dtpStartTime.Value;
				noSampler.EndTime = this.dtpEndTime.Value;

				//Dbers.GetInstance().SelfDber.Execute(string.Format("update cmcstbnosampler set mineid='{0}',minename='{1}',starttime='{2}',endtime='{3}' where id='{4}'", noSampler.MineId, noSampler.MineName, noSampler.StartTime, noSampler.EndTime, noSampler.Id));
				Dbers.GetInstance().SelfDber.Update(noSampler);
			}
			else if (this.EditMode == eEditMode.新增)
			{
				noSampler = new CmcsNoSampler();
				noSampler.MineId = this.CmcsMine.Id;
				noSampler.MineName = this.CmcsMine.Name;
				noSampler.StartTime = this.dtpStartTime.Value;
				noSampler.EndTime = this.dtpEndTime.Value;

				CommonDAO.GetInstance().SelfDber.Insert(noSampler);
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.No;
			this.Close();
		}
	}
}