namespace CMCS.SaveCupCoard.Frms
{
    partial class FrmSaveCupCoard
	{
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSaveCupCoard));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
			this.btnOpenDoor = new DevComponents.DotNetBar.ButtonX();
			this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
			this.btnReset = new DevComponents.DotNetBar.ButtonX();
			this.txtInputSampleCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.rtxtOutputInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.pnlExMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Interval = 2000;
			// 
			// pnlExMain
			// 
			this.pnlExMain.CanvasColor = System.Drawing.SystemColors.Control;
			this.pnlExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pnlExMain.Controls.Add(this.tableLayoutPanel1);
			this.pnlExMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlExMain.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.pnlExMain.Location = new System.Drawing.Point(0, 0);
			this.pnlExMain.Name = "pnlExMain";
			this.pnlExMain.Size = new System.Drawing.Size(739, 390);
			this.pnlExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pnlExMain.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(63)))));
			this.pnlExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pnlExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pnlExMain.Style.GradientAngle = 90;
			this.pnlExMain.TabIndex = 0;
			// 
			// btnOpenDoor
			// 
			this.btnOpenDoor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnOpenDoor.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnOpenDoor.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnOpenDoor.Location = new System.Drawing.Point(642, 1);
			this.btnOpenDoor.Name = "btnOpenDoor";
			this.btnOpenDoor.Size = new System.Drawing.Size(87, 39);
			this.btnOpenDoor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnOpenDoor.TabIndex = 219;
			this.btnOpenDoor.Text = "重新开柜";
			this.btnOpenDoor.Click += new System.EventHandler(this.btnOpenDoor_Click);
			// 
			// btnSubmit
			// 
			this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSubmit.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnSubmit.Location = new System.Drawing.Point(451, 1);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(92, 39);
			this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSubmit.TabIndex = 217;
			this.btnSubmit.Text = "确  定";
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// btnReset
			// 
			this.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnReset.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnReset.Location = new System.Drawing.Point(549, 1);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(87, 39);
			this.btnReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnReset.TabIndex = 218;
			this.btnReset.Text = "重  置";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// txtInputSampleCode
			// 
			this.txtInputSampleCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.txtInputSampleCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtInputSampleCode.Border.Class = "TextBoxBorder";
			this.txtInputSampleCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtInputSampleCode.ButtonCustom.Text = "重置";
			this.txtInputSampleCode.ButtonCustom2.Enabled = false;
			this.txtInputSampleCode.ButtonCustom2.Text = "打印";
			this.txtInputSampleCode.Font = new System.Drawing.Font("Segoe UI", 18F);
			this.txtInputSampleCode.ForeColor = System.Drawing.Color.White;
			this.txtInputSampleCode.Location = new System.Drawing.Point(2, 0);
			this.txtInputSampleCode.Name = "txtInputSampleCode";
			this.txtInputSampleCode.Size = new System.Drawing.Size(443, 39);
			this.txtInputSampleCode.TabIndex = 203;
			this.txtInputSampleCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtInputSampleCode.WatermarkText = "扫描制样子码. . .";
			this.txtInputSampleCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInputSampleCode_KeyUp);
			// 
			// rtxtOutputInfo
			// 
			// 
			// 
			// 
			this.rtxtOutputInfo.BackgroundStyle.Class = "RichTextBoxBorder";
			this.rtxtOutputInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.rtxtOutputInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtOutputInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.rtxtOutputInfo.ForeColor = System.Drawing.Color.White;
			this.rtxtOutputInfo.Location = new System.Drawing.Point(3, 48);
			this.rtxtOutputInfo.Name = "rtxtOutputInfo";
			this.rtxtOutputInfo.ReadOnly = true;
			this.rtxtOutputInfo.Size = new System.Drawing.Size(733, 339);
			this.rtxtOutputInfo.TabIndex = 202;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.rtxtOutputInfo, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(739, 390);
			this.tableLayoutPanel1.TabIndex = 220;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.btnSubmit);
			this.panelEx1.Controls.Add(this.btnOpenDoor);
			this.panelEx1.Controls.Add(this.txtInputSampleCode);
			this.panelEx1.Controls.Add(this.btnReset);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(3, 3);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(733, 39);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 0;
			// 
			// FrmSaveCupCoard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.ClientSize = new System.Drawing.Size(739, 390);
			this.Controls.Add(this.pnlExMain);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "FrmSaveCupCoard";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "存  样";
			this.Load += new System.EventHandler(this.FrmSampleCheck_Load);
			this.pnlExMain.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInputSampleCode;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtxtOutputInfo;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private DevComponents.DotNetBar.ButtonX btnReset;
		private DevComponents.DotNetBar.ButtonX btnOpenDoor;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private DevComponents.DotNetBar.PanelEx panelEx1;
	}
}

