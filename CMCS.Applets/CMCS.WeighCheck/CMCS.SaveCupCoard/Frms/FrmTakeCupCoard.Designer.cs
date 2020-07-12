namespace CMCS.SaveCupCoard.Frms
{
    partial class FrmTakeCupCoard
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
			DevComponents.DotNetBar.SuperGrid.Style.Background background1 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTakeCupCoard));
			this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
			this.txtInputSampleCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.btnReset = new DevComponents.DotNetBar.ButtonX();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
			this.dtpEndTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
			this.dtpStartTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
			this.btnSearch = new DevComponents.DotNetBar.ButtonX();
			this.txt_SampleCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
			this.lblPagerInfo = new DevComponents.DotNetBar.LabelX();
			this.btnPrevious = new DevComponents.DotNetBar.ButtonX();
			this.btnFirst = new DevComponents.DotNetBar.ButtonX();
			this.btnLast = new DevComponents.DotNetBar.ButtonX();
			this.btnNext = new DevComponents.DotNetBar.ButtonX();
			this.rtxtOutputInfo = new System.Windows.Forms.RichTextBox();
			this.pnlExMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.panelEx2.SuspendLayout();
			this.panelEx3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtpEndTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtpStartTime)).BeginInit();
			this.panelEx4.SuspendLayout();
			this.SuspendLayout();
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
			this.pnlExMain.Size = new System.Drawing.Size(839, 627);
			this.pnlExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pnlExMain.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(63)))));
			this.pnlExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pnlExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pnlExMain.Style.GradientAngle = 90;
			this.pnlExMain.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.superGridControl1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panelEx3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.panelEx4, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.rtxtOutputInfo, 0, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(839, 627);
			this.tableLayoutPanel1.TabIndex = 219;
			// 
			// superGridControl1
			// 
			this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			background1.Color1 = System.Drawing.Color.DarkTurquoise;
			this.superGridControl1.DefaultVisualStyles.RowStyles.Selected.Background = background1;
			this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
			this.superGridControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.superGridControl1.ForeColor = System.Drawing.Color.White;
			this.superGridControl1.Location = new System.Drawing.Point(0, 140);
			this.superGridControl1.Margin = new System.Windows.Forms.Padding(0);
			this.superGridControl1.Name = "superGridControl1";
			this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			gridColumn1.DataPropertyName = "CupCoardNumber";
			gridColumn1.HeaderText = "柜门编号";
			gridColumn1.Name = "CupBoardNumber";
			gridColumn1.Width = 80;
			gridColumn2.DataPropertyName = "SampleCode";
			gridColumn2.HeaderText = "样品编码";
			gridColumn2.Name = "SampleCode";
			gridColumn2.Width = 150;
			gridColumn3.DataPropertyName = "SaveTime";
			gridColumn3.HeaderText = "存样时间";
			gridColumn3.Name = "glcmSaveTime";
			gridColumn3.Width = 180;
			gridColumn4.DataPropertyName = "SaveUser";
			gridColumn4.HeaderText = "存样人";
			gridColumn4.Name = "";
			gridColumn5.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn5.HeaderText = "";
			gridColumn5.Name = "gclmTakeSample";
			gridColumn5.NullString = "取样";
			gridColumn5.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn5.Width = 80;
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
			this.superGridControl1.PrimaryGrid.EnterKeySelectsNextRow = false;
			this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
			this.superGridControl1.PrimaryGrid.MultiSelect = false;
			this.superGridControl1.PrimaryGrid.NoRowsText = "";
			this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
			this.superGridControl1.Size = new System.Drawing.Size(839, 307);
			this.superGridControl1.TabIndex = 224;
			this.superGridControl1.Text = "superGridControl1";
			this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.btnSubmit);
			this.panelEx1.Controls.Add(this.txtInputSampleCode);
			this.panelEx1.Controls.Add(this.btnReset);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(3, 63);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(833, 39);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 203;
			// 
			// btnSubmit
			// 
			this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnSubmit.Location = new System.Drawing.Point(617, 0);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(103, 39);
			this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSubmit.TabIndex = 219;
			this.btnSubmit.Text = "确  定";
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// txtInputSampleCode
			// 
			this.txtInputSampleCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
			this.txtInputSampleCode.Location = new System.Drawing.Point(255, 0);
			this.txtInputSampleCode.Name = "txtInputSampleCode";
			this.txtInputSampleCode.Size = new System.Drawing.Size(356, 39);
			this.txtInputSampleCode.TabIndex = 203;
			this.txtInputSampleCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtInputSampleCode.WatermarkText = "扫描制样子码. . .";
			this.txtInputSampleCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInputSampleCode_KeyUp);
			// 
			// btnReset
			// 
			this.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReset.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnReset.Location = new System.Drawing.Point(726, 0);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(103, 39);
			this.btnReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnReset.TabIndex = 218;
			this.btnReset.Text = "重  置";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx2.Controls.Add(this.textBoxX1);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx2.Location = new System.Drawing.Point(3, 3);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(833, 54);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 205;
			// 
			// textBoxX1
			// 
			this.textBoxX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.textBoxX1.Border.Class = "TextBoxBorder";
			this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxX1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxX1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.textBoxX1.ForeColor = System.Drawing.Color.White;
			this.textBoxX1.Location = new System.Drawing.Point(0, 0);
			this.textBoxX1.Multiline = true;
			this.textBoxX1.Name = "textBoxX1";
			this.textBoxX1.ReadOnly = true;
			this.textBoxX1.Size = new System.Drawing.Size(833, 54);
			this.textBoxX1.TabIndex = 206;
			this.textBoxX1.Text = "取样流程：选择要取样的记录点击取样；柜门打开后拿出柜门内所有样品依次扫描，扫描到要取的样品编码会提示取样操作为该样品；将剩余的样品放回柜门并关闭。";
			// 
			// panelEx3
			// 
			this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx3.Controls.Add(this.dtpEndTime);
			this.panelEx3.Controls.Add(this.dtpStartTime);
			this.panelEx3.Controls.Add(this.btnSearch);
			this.panelEx3.Controls.Add(this.txt_SampleCode);
			this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx3.Location = new System.Drawing.Point(3, 108);
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Size = new System.Drawing.Size(833, 29);
			this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			this.panelEx3.TabIndex = 226;
			// 
			// dtpEndTime
			// 
			this.dtpEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.dtpEndTime.BackgroundStyle.Class = "DateTimeInputBackground";
			this.dtpEndTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtpEndTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
			this.dtpEndTime.ButtonDropDown.Visible = true;
			this.dtpEndTime.CustomFormat = "yyyy-MM-dd";
			this.dtpEndTime.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.dtpEndTime.ForeColor = System.Drawing.Color.White;
			this.dtpEndTime.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
			this.dtpEndTime.IsPopupCalendarOpen = false;
			this.dtpEndTime.Location = new System.Drawing.Point(585, 0);
			// 
			// 
			// 
			this.dtpEndTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dtpEndTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtpEndTime.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
			this.dtpEndTime.MonthCalendar.ClearButtonVisible = true;
			// 
			// 
			// 
			this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
			this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
			this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
			this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtpEndTime.MonthCalendar.DisplayMonth = new System.DateTime(2020, 7, 1, 0, 0, 0, 0);
			this.dtpEndTime.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
			this.dtpEndTime.MonthCalendar.MarkedDates = new System.DateTime[0];
			this.dtpEndTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dtpEndTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.dtpEndTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
			this.dtpEndTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.dtpEndTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtpEndTime.MonthCalendar.TodayButtonVisible = true;
			this.dtpEndTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
			this.dtpEndTime.Name = "dtpEndTime";
			this.dtpEndTime.Size = new System.Drawing.Size(164, 27);
			this.dtpEndTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.dtpEndTime.TabIndex = 3;
			this.dtpEndTime.WatermarkText = "存样结束日期";
			// 
			// dtpStartTime
			// 
			this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.dtpStartTime.BackgroundStyle.Class = "DateTimeInputBackground";
			this.dtpStartTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtpStartTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
			this.dtpStartTime.ButtonDropDown.Visible = true;
			this.dtpStartTime.CustomFormat = "yyyy-MM-dd";
			this.dtpStartTime.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.dtpStartTime.ForeColor = System.Drawing.Color.White;
			this.dtpStartTime.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
			this.dtpStartTime.IsPopupCalendarOpen = false;
			this.dtpStartTime.Location = new System.Drawing.Point(415, 0);
			// 
			// 
			// 
			this.dtpStartTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dtpStartTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtpStartTime.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
			this.dtpStartTime.MonthCalendar.ClearButtonVisible = true;
			// 
			// 
			// 
			this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
			this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
			this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
			this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtpStartTime.MonthCalendar.DisplayMonth = new System.DateTime(2020, 7, 1, 0, 0, 0, 0);
			this.dtpStartTime.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
			this.dtpStartTime.MonthCalendar.MarkedDates = new System.DateTime[0];
			this.dtpStartTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dtpStartTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.dtpStartTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
			this.dtpStartTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.dtpStartTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtpStartTime.MonthCalendar.TodayButtonVisible = true;
			this.dtpStartTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
			this.dtpStartTime.Name = "dtpStartTime";
			this.dtpStartTime.Size = new System.Drawing.Size(164, 27);
			this.dtpStartTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.dtpStartTime.TabIndex = 2;
			this.dtpStartTime.WatermarkText = "存样开始日期";
			// 
			// btnSearch
			// 
			this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnSearch.Location = new System.Drawing.Point(755, 2);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSearch.TabIndex = 1;
			this.btnSearch.Text = "搜  索";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// txt_SampleCode
			// 
			this.txt_SampleCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txt_SampleCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txt_SampleCode.Border.Class = "TextBoxBorder";
			this.txt_SampleCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txt_SampleCode.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.txt_SampleCode.ForeColor = System.Drawing.Color.White;
			this.txt_SampleCode.Location = new System.Drawing.Point(255, 0);
			this.txt_SampleCode.Name = "txt_SampleCode";
			this.txt_SampleCode.Size = new System.Drawing.Size(154, 27);
			this.txt_SampleCode.TabIndex = 0;
			this.txt_SampleCode.WatermarkText = "请输入制样子码...";
			// 
			// panelEx4
			// 
			this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx4.Controls.Add(this.lblPagerInfo);
			this.panelEx4.Controls.Add(this.btnPrevious);
			this.panelEx4.Controls.Add(this.btnFirst);
			this.panelEx4.Controls.Add(this.btnLast);
			this.panelEx4.Controls.Add(this.btnNext);
			this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx4.Location = new System.Drawing.Point(3, 450);
			this.panelEx4.Name = "panelEx4";
			this.panelEx4.Size = new System.Drawing.Size(833, 24);
			this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx4.Style.GradientAngle = 90;
			this.panelEx4.TabIndex = 227;
			// 
			// lblPagerInfo
			// 
			this.lblPagerInfo.AutoSize = true;
			this.lblPagerInfo.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.lblPagerInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblPagerInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.lblPagerInfo.ForeColor = System.Drawing.Color.White;
			this.lblPagerInfo.Location = new System.Drawing.Point(0, 0);
			this.lblPagerInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lblPagerInfo.Name = "lblPagerInfo";
			this.lblPagerInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.lblPagerInfo.Size = new System.Drawing.Size(338, 24);
			this.lblPagerInfo.TabIndex = 109;
			this.lblPagerInfo.Text = "共 0 条记录，每页20 条，共 0 页，当前第 0 页";
			// 
			// btnPrevious
			// 
			this.btnPrevious.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevious.CommandParameter = "Previous";
			this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnPrevious.Location = new System.Drawing.Point(628, 1);
			this.btnPrevious.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(64, 23);
			this.btnPrevious.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnPrevious.TabIndex = 108;
			this.btnPrevious.Text = "<";
			this.btnPrevious.Click += new System.EventHandler(this.btnPagerCommand_Click);
			// 
			// btnFirst
			// 
			this.btnFirst.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFirst.CommandParameter = "First";
			this.btnFirst.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnFirst.Location = new System.Drawing.Point(558, 1);
			this.btnFirst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnFirst.Name = "btnFirst";
			this.btnFirst.Size = new System.Drawing.Size(64, 23);
			this.btnFirst.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnFirst.TabIndex = 107;
			this.btnFirst.Text = "|<";
			this.btnFirst.Click += new System.EventHandler(this.btnPagerCommand_Click);
			// 
			// btnLast
			// 
			this.btnLast.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLast.CommandParameter = "Last";
			this.btnLast.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnLast.Location = new System.Drawing.Point(767, 1);
			this.btnLast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnLast.Name = "btnLast";
			this.btnLast.Size = new System.Drawing.Size(64, 23);
			this.btnLast.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnLast.TabIndex = 106;
			this.btnLast.Text = ">|";
			this.btnLast.Click += new System.EventHandler(this.btnPagerCommand_Click);
			// 
			// btnNext
			// 
			this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNext.CommandParameter = "Next";
			this.btnNext.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnNext.Location = new System.Drawing.Point(697, 1);
			this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(64, 23);
			this.btnNext.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnNext.TabIndex = 105;
			this.btnNext.Text = ">";
			this.btnNext.Click += new System.EventHandler(this.btnPagerCommand_Click);
			// 
			// rtxtOutputInfo
			// 
			this.rtxtOutputInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtOutputInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.rtxtOutputInfo.Location = new System.Drawing.Point(3, 480);
			this.rtxtOutputInfo.Name = "rtxtOutputInfo";
			this.rtxtOutputInfo.Size = new System.Drawing.Size(833, 144);
			this.rtxtOutputInfo.TabIndex = 228;
			this.rtxtOutputInfo.Text = "";
			// 
			// FrmTakeCupCoard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.ClientSize = new System.Drawing.Size(839, 627);
			this.Controls.Add(this.pnlExMain);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "FrmTakeCupCoard";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "取  样";
			this.Load += new System.EventHandler(this.FrmSampleCheck_Load);
			this.pnlExMain.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.panelEx2.ResumeLayout(false);
			this.panelEx3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtpEndTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtpStartTime)).EndInit();
			this.panelEx4.ResumeLayout(false);
			this.panelEx4.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx pnlExMain;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.Controls.TextBoxX txtInputSampleCode;
		private DevComponents.DotNetBar.ButtonX btnReset;
		private DevComponents.DotNetBar.PanelEx panelEx2;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
		private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
		private DevComponents.DotNetBar.PanelEx panelEx3;
		private DevComponents.DotNetBar.ButtonX btnSearch;
		private DevComponents.DotNetBar.Controls.TextBoxX txt_SampleCode;
		private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpStartTime;
		private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpEndTime;
		private DevComponents.DotNetBar.PanelEx panelEx4;
		private DevComponents.DotNetBar.ButtonX btnPrevious;
		private DevComponents.DotNetBar.ButtonX btnFirst;
		private DevComponents.DotNetBar.ButtonX btnLast;
		private DevComponents.DotNetBar.ButtonX btnNext;
		private DevComponents.DotNetBar.LabelX lblPagerInfo;
		private System.Windows.Forms.RichTextBox rtxtOutputInfo;
		private DevComponents.DotNetBar.ButtonX btnSubmit;
	}
}

