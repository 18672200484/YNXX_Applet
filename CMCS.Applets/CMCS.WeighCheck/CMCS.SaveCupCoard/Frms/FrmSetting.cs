using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.WeighCheck.DAO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace CMCS.SaveCupCoard.Frms
{
	public partial class FrmSetting : DevComponents.DotNetBar.Metro.MetroForm
	{
		CommonDAO commonDAO = CommonDAO.GetInstance();

		string Old_Param = string.Empty;
		public FrmSetting()
		{
			InitializeComponent();
		}

		private void FrmSetting_Load(object sender, EventArgs e)
		{
			try
			{
				cmbLibra_COM.SelectedIndex = 0;
				cmbLibra_Bandrate.SelectedIndex = 0;
				cmbDataBits.SelectedIndex = 0;
				cmbParity.SelectedIndex = 0;

				labelX4.ForeColor = Color.Red;

				txtAppIdentifier.Text = CommonAppConfig.GetInstance().AppIdentifier;
				txtSelfConnStr.Text = CommonAppConfig.GetInstance().SelfConnStr;
				//存样柜
				SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("存样柜串口"), cmbLibra_COM);
				SelectedComboItem(commonDAO.GetAppletConfigString("存样柜波特率"), cmbLibra_Bandrate);
				SelectedComboItem(commonDAO.GetAppletConfigString("存样柜数据位"), cmbDataBits);
				SelectedComboItem(commonDAO.GetAppletConfigString("存样柜停止位"), cmbParity);
				dbi_OverDay.Value = commonDAO.GetAppletConfigInt32("存样柜超期天数");
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("参数初始化失败" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
			}
		}

		/// <summary>
		/// 选中ComboItem
		/// </summary>
		/// <param name="text"></param>
		/// <param name="cmb"></param>
		private void SelectedComboItem(string text, ComboBoxEx cmb)
		{
			foreach (ComboItem item in cmb.Items)
			{
				if (item.Text == text)
				{
					cmb.SelectedItem = item;
					break;
				}
			}
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			CommonAppConfig.GetInstance().AppIdentifier = txtAppIdentifier.Text.Trim();
			CommonAppConfig.GetInstance().SelfConnStr = txtSelfConnStr.Text.Trim();
			//存样柜
			commonDAO.SetAppletConfig("存样柜串口", (cmbLibra_COM.SelectedIndex + 1).ToString());
			commonDAO.SetAppletConfig("存样柜波特率", (cmbLibra_Bandrate.SelectedItem as ComboItem).Text);
			commonDAO.SetAppletConfig("存样柜数据位", (cmbDataBits.SelectedItem as ComboItem).Text);
			commonDAO.SetAppletConfig("存样柜停止位", (cmbParity.SelectedItem as ComboItem).Text);
			commonDAO.SetAppletConfig("存样柜超期天数", dbi_OverDay.Value.ToString());
			CommonAppConfig.GetInstance().Save();
			if (MessageBoxEx.Show("更改的配置需要重启程序才能生效，是否立刻重启？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				Application.Restart();
			else
				this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnInitCupBoard_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("是否初始化存样柜数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				int res = CZYHandlerDAO.GetInstance().InitCupBoard();
				MessageBoxEx.Show("初始化完成" + res + "条存样柜数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}