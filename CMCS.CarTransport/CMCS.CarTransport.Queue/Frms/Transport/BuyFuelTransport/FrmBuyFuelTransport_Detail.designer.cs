namespace CMCS.CarTransport.Queue.Frms.Transport.BuyFuelTransport
{
    partial class FrmBuyFuelTransport_Detail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn53 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn54 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn55 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn56 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn57 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn58 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn59 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn60 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn61 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn62 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn63 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn64 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn65 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbMineName_BuyFuel = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtpStartTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cmbFuelKindName_BuyFuel = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtpEndTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.txtMineName_Ser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPrint});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmiPrint
            // 
            this.tsmiPrint.Name = "tsmiPrint";
            this.tsmiPrint.Size = new System.Drawing.Size(124, 22);
            this.tsmiPrint.Text = "打印磅单";
            this.tsmiPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(914, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "搜 索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Location = new System.Drawing.Point(0, 1);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
            this.splitContainer1.Panel2.Controls.Add(this.superGridControl1);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1138, 453);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 147;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.cmbMineName_BuyFuel);
            this.panel1.Controls.Add(this.dtpStartTime);
            this.panel1.Controls.Add(this.cmbFuelKindName_BuyFuel);
            this.panel1.Controls.Add(this.dtpEndTime);
            this.panel1.Controls.Add(this.txtMineName_Ser);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1138, 40);
            this.panel1.TabIndex = 12;
            // 
            // cmbMineName_BuyFuel
            // 
            this.cmbMineName_BuyFuel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMineName_BuyFuel.DisplayMember = "Text";
            this.cmbMineName_BuyFuel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMineName_BuyFuel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMineName_BuyFuel.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cmbMineName_BuyFuel.ForeColor = System.Drawing.Color.White;
            this.cmbMineName_BuyFuel.FormattingEnabled = true;
            this.cmbMineName_BuyFuel.ItemHeight = 21;
            this.cmbMineName_BuyFuel.Location = new System.Drawing.Point(370, 6);
            this.cmbMineName_BuyFuel.Name = "cmbMineName_BuyFuel";
            this.cmbMineName_BuyFuel.Size = new System.Drawing.Size(240, 27);
            this.cmbMineName_BuyFuel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbMineName_BuyFuel.TabIndex = 280;
            this.cmbMineName_BuyFuel.WatermarkText = "请选择矿点";
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
            this.dtpStartTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStartTime.ForeColor = System.Drawing.Color.White;
            this.dtpStartTime.IsPopupCalendarOpen = false;
            this.dtpStartTime.Location = new System.Drawing.Point(75, 7);
            this.dtpStartTime.LockUpdateChecked = false;
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
            this.dtpStartTime.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
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
            this.dtpStartTime.Size = new System.Drawing.Size(136, 26);
            this.dtpStartTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpStartTime.TabIndex = 279;
            this.dtpStartTime.WatermarkText = "开始时间";
            // 
            // cmbFuelKindName_BuyFuel
            // 
            this.cmbFuelKindName_BuyFuel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFuelKindName_BuyFuel.DisplayMember = "Text";
            this.cmbFuelKindName_BuyFuel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFuelKindName_BuyFuel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuelKindName_BuyFuel.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cmbFuelKindName_BuyFuel.ForeColor = System.Drawing.Color.White;
            this.cmbFuelKindName_BuyFuel.FormattingEnabled = true;
            this.cmbFuelKindName_BuyFuel.ItemHeight = 21;
            this.cmbFuelKindName_BuyFuel.Location = new System.Drawing.Point(622, 7);
            this.cmbFuelKindName_BuyFuel.Name = "cmbFuelKindName_BuyFuel";
            this.cmbFuelKindName_BuyFuel.Size = new System.Drawing.Size(130, 27);
            this.cmbFuelKindName_BuyFuel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbFuelKindName_BuyFuel.TabIndex = 278;
            this.cmbFuelKindName_BuyFuel.WatermarkText = "请选择煤种";
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
            this.dtpEndTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEndTime.ForeColor = System.Drawing.Color.White;
            this.dtpEndTime.IsPopupCalendarOpen = false;
            this.dtpEndTime.Location = new System.Drawing.Point(217, 7);
            this.dtpEndTime.LockUpdateChecked = false;
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
            this.dtpEndTime.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
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
            this.dtpEndTime.Size = new System.Drawing.Size(136, 26);
            this.dtpEndTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpEndTime.TabIndex = 270;
            this.dtpEndTime.WatermarkText = "结束时间";
            // 
            // txtMineName_Ser
            // 
            this.txtMineName_Ser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMineName_Ser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtMineName_Ser.Border.Class = "TextBoxBorder";
            this.txtMineName_Ser.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMineName_Ser.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMineName_Ser.ForeColor = System.Drawing.Color.White;
            this.txtMineName_Ser.Location = new System.Drawing.Point(758, 8);
            this.txtMineName_Ser.Name = "txtMineName_Ser";
            this.txtMineName_Ser.Size = new System.Drawing.Size(150, 27);
            this.txtMineName_Ser.TabIndex = 14;
            this.txtMineName_Ser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMineName_Ser.WatermarkImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtMineName_Ser.WatermarkText = "请输入车号...";
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(1071, 10);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(64, 23);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 13;
            this.btnExport.Text = "导  出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(993, 10);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 23);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "打 印";
            this.btnPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
            // 
            // superGridControl1
            // 
            this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.ForeColor = System.Drawing.Color.White;
            this.superGridControl1.Location = new System.Drawing.Point(0, 0);
            this.superGridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            this.superGridControl1.PrimaryGrid.Caption.Text = "";
            gridColumn53.DataPropertyName = "CarNumber";
            gridColumn53.HeaderText = "车号";
            gridColumn53.Name = "";
            gridColumn53.Width = 120;
            gridColumn54.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn54.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn54.DataPropertyName = "MineName";
            gridColumn54.HeaderText = "矿点";
            gridColumn54.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn54.Name = "clmMine";
            gridColumn54.Width = 250;
            gridColumn55.DataPropertyName = "FuelKindName";
            gridColumn55.HeaderText = "煤种";
            gridColumn55.Name = "clmFuelKind";
            gridColumn55.Width = 120;
            gridColumn56.DataPropertyName = "GrossTime";
            gridColumn56.HeaderText = "毛重时间";
            gridColumn56.Name = "clmGrossTime";
            gridColumn56.Width = 170;
            gridColumn57.DataPropertyName = "TareTime";
            gridColumn57.HeaderText = "皮重时间";
            gridColumn57.Name = "clmTareTime";
            gridColumn57.Width = 170;
            gridColumn58.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn58.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn58.DataPropertyName = "TicketWeight";
            gridColumn58.HeaderText = "矿发量";
            gridColumn58.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn58.Name = "";
            gridColumn58.Width = 120;
            gridColumn59.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn59.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn59.DataPropertyName = "GrossWeight";
            gridColumn59.HeaderText = "毛重";
            gridColumn59.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn59.Name = "";
            gridColumn59.Width = 120;
            gridColumn60.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn60.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn60.DataPropertyName = "TareWeight";
            gridColumn60.HeaderText = "皮重";
            gridColumn60.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn60.Name = "";
            gridColumn60.Width = 120;
            gridColumn61.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn61.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn61.DataPropertyName = "SuttleWeight";
            gridColumn61.HeaderText = "净重";
            gridColumn61.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn61.Name = "";
            gridColumn61.Width = 120;
            gridColumn62.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn62.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn62.DataPropertyName = "DeductWeight";
            gridColumn62.HeaderText = "扣重";
            gridColumn62.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn62.Name = "";
            gridColumn62.Width = 120;
            gridColumn63.DataPropertyName = "KgWeight";
            gridColumn63.HeaderText = "扣矸";
            gridColumn63.Name = "";
            gridColumn63.Width = 80;
            gridColumn64.HeaderText = "扣水";
            gridColumn64.Name = "clmKsWeight";
            gridColumn64.Width = 80;
            gridColumn65.DataPropertyName = "CheckWeight";
            gridColumn65.HeaderText = "验收量";
            gridColumn65.Name = "clmCheckWeight";
            gridColumn65.Width = 120;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn53);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn54);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn55);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn56);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn57);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn58);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn59);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn60);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn61);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn62);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn63);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn64);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn65);
            this.superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(1138, 412);
            this.superGridControl1.TabIndex = 5;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
            // 
            // FrmBuyFuelTransport_Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 455);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmBuyFuelTransport_Detail";
            this.Text = "入厂煤明细";
            this.Load += new System.EventHandler(this.FrmBuyFuelTransport_List_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMineName_Ser;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpEndTime;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbFuelKindName_BuyFuel;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpStartTime;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbMineName_BuyFuel;
    }
}