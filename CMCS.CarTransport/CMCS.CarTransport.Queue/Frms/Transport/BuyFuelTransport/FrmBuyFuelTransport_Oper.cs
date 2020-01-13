using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.CarTransport.Queue.Core;
using CMCS.CarTransport.Queue.Frms.BaseInfo.Mine;
using CMCS.CarTransport.Queue.Frms.BaseInfo.Supplier;
using CMCS.CarTransport.Queue.Frms.BaseInfo.TransportCompany;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using DevComponents.DotNetBar;
using CMCS.CarTransport.DAO;
using CMCS.CarTransport.Queue.Enums;
using CMCS.Common.Enums;
using CMCS.Common.DAO;

namespace CMCS.CarTransport.Queue.Frms.Transport.BuyFuelTransport
{
	public partial class FrmBuyFuelTransport_Oper : DevComponents.DotNetBar.Metro.MetroForm
	{
		#region Var

		CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();

		//业务id
		string PId = String.Empty;

		//编辑模式
		eEditMode EditMode = eEditMode.默认;

		//当前运输记录
		CmcsBuyFuelTransport CmcsBuyFuelTransport;

		CmcsTransportCompany cmcsTransportCompany;
		/// <summary>
		/// 运输单位
		/// </summary>
		public CmcsTransportCompany CmcsTransportCompany
		{
			get { return cmcsTransportCompany; }
			set
			{
				cmcsTransportCompany = value;

				if (value != null)
				{
					txt_TransportCompanyName.Text = value.Name;
				}
				else
				{
					txt_TransportCompanyName.ResetText();
				}
			}
		}

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

		CmcsSupplier cmcsSupplier;
		/// <summary>
		/// 供应商
		/// </summary>
		public CmcsSupplier CmcsSupplier
		{
			get { return cmcsSupplier; }
			set
			{
				cmcsSupplier = value;

				if (value != null)
				{
					txt_SupplierName.Text = value.Name;
				}
				else
				{
					txt_SupplierName.ResetText();
				}
			}
		}

		CmcsFuelKind cmcsFuelKind;
		/// <summary>
		/// 煤种
		/// </summary>
		public CmcsFuelKind CmcsFuelKind
		{
			get { return cmcsFuelKind; }
			set { cmcsFuelKind = value; }
		}

		List<CmcsBuyFuelTransportDeduct> cmcsbuyfueltransportdeducts;

		#endregion

		public FrmBuyFuelTransport_Oper(string pId, eEditMode editMode)
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
		private void FrmBuyFuelTransport_Oper_Load(object sender, EventArgs e)
		{
			cmb_SampingType.Items.Add("机械采样");
			cmb_SampingType.Items.Add("皮带采样");
			cmb_SampingType.Items.Add("人工采样");
			cmb_SampingType.SelectedIndex = 0;

			//绑定煤种信息
			cmbFuelName_BuyFuel.DisplayMember = "Name";
			cmbFuelName_BuyFuel.ValueMember = "Id";
			cmbFuelName_BuyFuel.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where IsStop=0 and ParentId is not null");
			cmbFuelName_BuyFuel.SelectedIndex = 0;

			this.CmcsBuyFuelTransport = Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(this.PId);
			if (this.CmcsBuyFuelTransport != null)
			{
				txt_SerialNumber.Text = CmcsBuyFuelTransport.SerialNumber;
				txt_CarNumber.Text = CmcsBuyFuelTransport.CarNumber;
				CmcsInFactoryBatch cmcsinfactorybatch = Dbers.GetInstance().SelfDber.Get<CmcsInFactoryBatch>(CmcsBuyFuelTransport.InFactoryBatchId);
				if (cmcsinfactorybatch != null)
				{
					txt_InFactoryBatchNumber.Text = cmcsinfactorybatch.Batch;
				}
				txt_SupplierName.Text = CmcsBuyFuelTransport.SupplierName;
				txt_TransportCompanyName.Text = CmcsBuyFuelTransport.TransportCompanyName;
				txt_MineName.Text = CmcsBuyFuelTransport.MineName;
				cmbFuelName_BuyFuel.Text = CmcsBuyFuelTransport.FuelKindName;
				cmb_SampingType.SelectedItem = CmcsBuyFuelTransport.SamplingType;
				dbi_TicketWeight.Value = (double)CmcsBuyFuelTransport.TicketWeight;
				dbi_GrossWeight.Value = (double)CmcsBuyFuelTransport.GrossWeight;
				dbi_TareWeight.Value = (double)CmcsBuyFuelTransport.TareWeight;
				dbi_DeductWeight.Value = (double)CmcsBuyFuelTransport.DeductWeight;
				dbi_SuttleWeight.Value = (double)CmcsBuyFuelTransport.SuttleWeight;
				dbi_KsWeight.Value = (double)CmcsBuyFuelTransport.KsWeight;
				dbi_KgWeight.Value = (double)CmcsBuyFuelTransport.KgWeight;
				dbi_AutoKsWeight.Value = (double)CmcsBuyFuelTransport.AutoKsWeight;
				dbi_CheckWeight.Value = (double)CmcsBuyFuelTransport.CheckWeight;
				txt_UnloadArea.Text = CmcsBuyFuelTransport.UnLoadArea;
				txt_InFactoryTime.Text = CmcsBuyFuelTransport.InFactoryTime.Year == 1 ? "" : CmcsBuyFuelTransport.InFactoryTime.ToString();
				txt_SamplingTime.Text = CmcsBuyFuelTransport.SamplingTime.Year == 1 ? "" : CmcsBuyFuelTransport.SamplingTime.ToString();
				txt_GrossTime.Text = CmcsBuyFuelTransport.GrossTime.Year == 1 ? "" : CmcsBuyFuelTransport.GrossTime.ToString();
				txt_UploadTime.Text = CmcsBuyFuelTransport.UploadTime.Year == 1 ? "" : CmcsBuyFuelTransport.UploadTime.ToString();
				txt_TareTime.Text = CmcsBuyFuelTransport.TareTime.Year == 1 ? "" : CmcsBuyFuelTransport.TareTime.ToString();
				txt_OutFactoryTime.Text = CmcsBuyFuelTransport.OutFactoryTime.Year == 1 ? "" : CmcsBuyFuelTransport.OutFactoryTime.ToString();
				txt_Remark.Text = CmcsBuyFuelTransport.Remark;
				chb_IsFinish.Checked = (CmcsBuyFuelTransport.IsFinish == 1);
				chb_IsUse.Checked = (CmcsBuyFuelTransport.IsUse == 1);
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
			if (this.EditMode == eEditMode.修改)
			{
				CmcsBuyFuelTransport.SerialNumber = txt_SerialNumber.Text;
				CmcsBuyFuelTransport.CarNumber = txt_CarNumber.Text;
				if (cmcsSupplier != null)
				{
					CmcsBuyFuelTransport.SupplierId = cmcsSupplier.Id;
					CmcsBuyFuelTransport.SupplierName = cmcsSupplier.Name;
				}
				if (cmcsTransportCompany != null)
				{
					CmcsBuyFuelTransport.TransportCompanyId = cmcsTransportCompany.Id;
					CmcsBuyFuelTransport.TransportCompanyName = cmcsTransportCompany.Name;
				}
				if (cmcsMine != null)
				{
					CmcsBuyFuelTransport.MineId = cmcsMine.Id;
					CmcsBuyFuelTransport.MineName = cmcsMine.Name;
				}
				if (cmcsFuelKind != null)
				{
					CmcsBuyFuelTransport.FuelKindId = cmcsFuelKind.Id;
					CmcsBuyFuelTransport.FuelKindName = cmcsFuelKind.Name;
				}

				string logValue = "修改前：" + Environment.NewLine;
				logValue += "车号：" + CmcsBuyFuelTransport.CarNumber + Environment.NewLine;
				logValue += "矿点：" + CmcsBuyFuelTransport.MineName + "   煤种：" + CmcsBuyFuelTransport.FuelKindName + Environment.NewLine;
				logValue += "入厂时间：" + CmcsBuyFuelTransport.InFactoryTime + "   矿发量：" + CmcsBuyFuelTransport.TicketWeight + Environment.NewLine;
				logValue += "毛重时间：" + CmcsBuyFuelTransport.GrossTime + "   毛重：" + CmcsBuyFuelTransport.GrossWeight + Environment.NewLine;
				logValue += "皮重时间：" + CmcsBuyFuelTransport.TareTime + "   皮重：" + CmcsBuyFuelTransport.TareWeight + Environment.NewLine;
				logValue += "扣矸：" + CmcsBuyFuelTransport.KgWeight + "   扣水：" + CmcsBuyFuelTransport.KsWeight + "   自动扣水：" + CmcsBuyFuelTransport.AutoKsWeight + Environment.NewLine;
				logValue += "出厂时间：" + CmcsBuyFuelTransport.OutFactoryTime + "   验收量：" + CmcsBuyFuelTransport.CheckWeight + Environment.NewLine;

				CmcsBuyFuelTransport.SamplingType = (string)cmb_SampingType.SelectedItem;
				CmcsBuyFuelTransport.TicketWeight = (decimal)dbi_TicketWeight.Value;
				CmcsBuyFuelTransport.GrossWeight = (decimal)dbi_GrossWeight.Value;
				CmcsBuyFuelTransport.TareWeight = (decimal)dbi_TareWeight.Value;
				CmcsBuyFuelTransport.KgWeight = (decimal)dbi_KgWeight.Value;
				CmcsBuyFuelTransport.KsWeight = (decimal)dbi_KsWeight.Value;
				CmcsBuyFuelTransport.Remark = txt_Remark.Text;
				CmcsBuyFuelTransport.IsFinish = (chb_IsFinish.Checked ? 1 : 0);
				CmcsBuyFuelTransport.IsUse = (chb_IsUse.Checked ? 1 : 0);

				// 生成批次以及采制化三级编码数据 
				CmcsInFactoryBatch inFactoryBatch = carTransportDAO.GCQCInFactoryBatchByBuyFuelTransport(CmcsBuyFuelTransport);
				WeighterDAO.GetInstance().SaveBuyFuelTransportHand(CmcsBuyFuelTransport);

				logValue += "修改后：" + Environment.NewLine;
				logValue += "车号：" + CmcsBuyFuelTransport.CarNumber + Environment.NewLine;
				logValue += "矿点:" + CmcsBuyFuelTransport.MineName + "   煤种：" + CmcsBuyFuelTransport.FuelKindName + Environment.NewLine;
				logValue += "入厂时间：" + CmcsBuyFuelTransport.InFactoryTime + "   矿发量：" + CmcsBuyFuelTransport.TicketWeight + Environment.NewLine;
				logValue += "毛重时间：" + CmcsBuyFuelTransport.GrossTime + "   毛重：" + CmcsBuyFuelTransport.GrossWeight + Environment.NewLine;
				logValue += "皮重时间：" + CmcsBuyFuelTransport.TareTime + "   皮重：" + CmcsBuyFuelTransport.TareWeight + Environment.NewLine;
				logValue += "扣矸：" + CmcsBuyFuelTransport.KgWeight + "   扣水：" + CmcsBuyFuelTransport.KsWeight + "   自动扣水：" + CmcsBuyFuelTransport.AutoKsWeight + Environment.NewLine;
				logValue += "出厂时间：" + CmcsBuyFuelTransport.OutFactoryTime + "   验收量：" + CmcsBuyFuelTransport.CheckWeight + Environment.NewLine;
				logValue += "修改人：" + SelfVars.LoginUser.UserName;
				
				CommonDAO.GetInstance().SaveAppletLog(eAppletLogLevel.Info, "修改运输记录", logValue, SelfVars.LoginUser.UserAccount);
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

		private void btnSupplier_Click(object sender, EventArgs e)
		{
			FrmSupplier_Select Frm = new FrmSupplier_Select(string.Empty);
			Frm.ShowDialog();
			if (Frm.DialogResult == DialogResult.OK)
			{
				this.CmcsSupplier = Frm.Output;
			}
		}

		private void btnTransportCompany_Click(object sender, EventArgs e)
		{
			FrmTransportCompany_Select Frm = new FrmTransportCompany_Select(string.Empty);
			Frm.ShowDialog();
			if (Frm.DialogResult == DialogResult.OK)
			{
				this.CmcsTransportCompany = Frm.Output;
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

		private void cmbFuelName_BuyFuel_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.cmcsFuelKind = cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind;
		}
	}
}