namespace WinForm
{
    partial class FrmNoBraCodeReceipt
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
            this.butSearch = new System.Windows.Forms.Button();
            this.bgSearch = new System.Windows.Forms.GroupBox();
            this.cbOrg = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.chbReceiDate = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPO = new System.Windows.Forms.TextBox();
            this.cbSubinv = new System.Windows.Forms.ComboBox();
            this.cklbLocation = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStopDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReceiNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbNewOrg = new System.Windows.Forms.ComboBox();
            this.gbReceiData = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSearchDate = new System.Windows.Forms.DataGridView();
            this.dgvReceiData = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbNews = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtMark = new System.Windows.Forms.TextBox();
            this.butNew = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.txtNewReceiEmp = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtNewBoxQty = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNewSizeQty = new System.Windows.Forms.TextBox();
            this.cbNewSize = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbNewColor = new System.Windows.Forms.ComboBox();
            this.cbNewSubinv = new System.Windows.Forms.ComboBox();
            this.cbNewStyle = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbNewLine = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNewPo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dtpNewReceiDate = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNewReceiNumber = new System.Windows.Forms.TextBox();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RmeDelRow = new System.Windows.Forms.ToolStripMenuItem();
            this.bgSearch.SuspendLayout();
            this.gbReceiData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceiData)).BeginInit();
            this.gbNews.SuspendLayout();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(481, 13);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(81, 84);
            this.butSearch.TabIndex = 25;
            this.butSearch.Text = "查询";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // bgSearch
            // 
            this.bgSearch.Controls.Add(this.cbOrg);
            this.bgSearch.Controls.Add(this.label21);
            this.bgSearch.Controls.Add(this.chbReceiDate);
            this.bgSearch.Controls.Add(this.label10);
            this.bgSearch.Controls.Add(this.label8);
            this.bgSearch.Controls.Add(this.txtPO);
            this.bgSearch.Controls.Add(this.cbSubinv);
            this.bgSearch.Controls.Add(this.cklbLocation);
            this.bgSearch.Controls.Add(this.label9);
            this.bgSearch.Controls.Add(this.txtColor);
            this.bgSearch.Controls.Add(this.label5);
            this.bgSearch.Controls.Add(this.txtStyle);
            this.bgSearch.Controls.Add(this.label4);
            this.bgSearch.Controls.Add(this.label3);
            this.bgSearch.Controls.Add(this.dtpStopDate);
            this.bgSearch.Controls.Add(this.dtpStartDate);
            this.bgSearch.Controls.Add(this.label1);
            this.bgSearch.Controls.Add(this.txtReceiNumber);
            this.bgSearch.Controls.Add(this.butSearch);
            this.bgSearch.Location = new System.Drawing.Point(678, 6);
            this.bgSearch.Name = "bgSearch";
            this.bgSearch.Size = new System.Drawing.Size(568, 108);
            this.bgSearch.TabIndex = 41;
            this.bgSearch.TabStop = false;
            this.bgSearch.Text = "查询条件";
            // 
            // cbOrg
            // 
            this.cbOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrg.FormattingEnabled = true;
            this.cbOrg.ItemHeight = 12;
            this.cbOrg.Location = new System.Drawing.Point(43, 17);
            this.cbOrg.Name = "cbOrg";
            this.cbOrg.Size = new System.Drawing.Size(69, 20);
            this.cbOrg.TabIndex = 15;
            this.cbOrg.SelectedIndexChanged += new System.EventHandler(this.cbOrg_SelectedIndexChanged_1);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 21);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 57;
            this.label21.Text = "厂区";
            // 
            // chbReceiDate
            // 
            this.chbReceiDate.AutoSize = true;
            this.chbReceiDate.Enabled = false;
            this.chbReceiDate.Location = new System.Drawing.Point(15, 80);
            this.chbReceiDate.Name = "chbReceiDate";
            this.chbReceiDate.Size = new System.Drawing.Size(72, 16);
            this.chbReceiDate.TabIndex = 22;
            this.chbReceiDate.Text = "录入日期";
            this.chbReceiDate.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(240, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 12);
            this.label10.TabIndex = 67;
            this.label10.Text = "PO#";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(123, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 59;
            this.label8.Text = "仓库";
            // 
            // txtPO
            // 
            this.txtPO.Location = new System.Drawing.Point(269, 17);
            this.txtPO.Name = "txtPO";
            this.txtPO.Size = new System.Drawing.Size(94, 21);
            this.txtPO.TabIndex = 18;
            // 
            // cbSubinv
            // 
            this.cbSubinv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubinv.FormattingEnabled = true;
            this.cbSubinv.ItemHeight = 12;
            this.cbSubinv.Location = new System.Drawing.Point(156, 17);
            this.cbSubinv.Name = "cbSubinv";
            this.cbSubinv.Size = new System.Drawing.Size(69, 20);
            this.cbSubinv.TabIndex = 16;
            this.cbSubinv.SelectedIndexChanged += new System.EventHandler(this.cbSubinv_SelectedIndexChanged);
            // 
            // cklbLocation
            // 
            this.cklbLocation.CheckOnClick = true;
            this.cklbLocation.FormattingEnabled = true;
            this.cklbLocation.Location = new System.Drawing.Point(369, 12);
            this.cklbLocation.Name = "cklbLocation";
            this.cklbLocation.Size = new System.Drawing.Size(106, 84);
            this.cklbLocation.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(199, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 12);
            this.label9.TabIndex = 16;
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(157, 48);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(68, 21);
            this.txtColor.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 60;
            this.label5.Text = "色号";
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(43, 50);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.Size = new System.Drawing.Size(69, 21);
            this.txtStyle.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 58;
            this.label4.Text = "款式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(200, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 63;
            this.label3.Text = "至";
            // 
            // dtpStopDate
            // 
            this.dtpStopDate.Enabled = false;
            this.dtpStopDate.Location = new System.Drawing.Point(220, 78);
            this.dtpStopDate.Name = "dtpStopDate";
            this.dtpStopDate.Size = new System.Drawing.Size(100, 21);
            this.dtpStopDate.TabIndex = 24;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Enabled = false;
            this.dtpStartDate.Location = new System.Drawing.Point(89, 78);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(102, 21);
            this.dtpStartDate.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(231, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 62;
            this.label1.Text = "送货单号";
            // 
            // txtReceiNumber
            // 
            this.txtReceiNumber.Location = new System.Drawing.Point(290, 47);
            this.txtReceiNumber.Name = "txtReceiNumber";
            this.txtReceiNumber.Size = new System.Drawing.Size(73, 21);
            this.txtReceiNumber.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(10, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 44;
            this.label6.Text = "厂    区";
            // 
            // cbNewOrg
            // 
            this.cbNewOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewOrg.FormattingEnabled = true;
            this.cbNewOrg.Location = new System.Drawing.Point(67, 12);
            this.cbNewOrg.Name = "cbNewOrg";
            this.cbNewOrg.Size = new System.Drawing.Size(83, 20);
            this.cbNewOrg.TabIndex = 1;
            this.cbNewOrg.SelectedIndexChanged += new System.EventHandler(this.cbOrg_SelectedIndexChanged);
            this.cbNewOrg.Click += new System.EventHandler(this.cbOrg_Click);
            // 
            // gbReceiData
            // 
            this.gbReceiData.Controls.Add(this.splitContainer1);
            this.gbReceiData.Location = new System.Drawing.Point(5, 132);
            this.gbReceiData.Name = "gbReceiData";
            this.gbReceiData.Size = new System.Drawing.Size(1368, 597);
            this.gbReceiData.TabIndex = 43;
            this.gbReceiData.TabStop = false;
            this.gbReceiData.Text = "收货明细";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvSearchDate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvReceiData);
            this.splitContainer1.Size = new System.Drawing.Size(1362, 577);
            this.splitContainer1.SplitterDistance = 288;
            this.splitContainer1.TabIndex = 15;
            // 
            // dgvSearchDate
            // 
            this.dgvSearchDate.AllowUserToAddRows = false;
            this.dgvSearchDate.AllowUserToDeleteRows = false;
            this.dgvSearchDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchDate.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchDate.Name = "dgvSearchDate";
            this.dgvSearchDate.ReadOnly = true;
            this.dgvSearchDate.RowTemplate.Height = 23;
            this.dgvSearchDate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchDate.Size = new System.Drawing.Size(1362, 288);
            this.dgvSearchDate.TabIndex = 0;
            this.dgvSearchDate.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchDate_CellDoubleClick);
            this.dgvSearchDate.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSearchDate_RowPostPaint);
            // 
            // dgvReceiData
            // 
            this.dgvReceiData.AllowUserToAddRows = false;
            this.dgvReceiData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReceiData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceiData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReceiData.EnableHeadersVisualStyles = false;
            this.dgvReceiData.Location = new System.Drawing.Point(0, 0);
            this.dgvReceiData.Name = "dgvReceiData";
            this.dgvReceiData.ReadOnly = true;
            this.dgvReceiData.RowTemplate.Height = 23;
            this.dgvReceiData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReceiData.Size = new System.Drawing.Size(1362, 285);
            this.dgvReceiData.TabIndex = 14;
            this.dgvReceiData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvReceiData_CellMouseDown);
            this.dgvReceiData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvReceiData_RowPostPaint);
            this.dgvReceiData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvReceiData_KeyDown);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(5, 116);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1365, 10);
            this.progressBar1.TabIndex = 42;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1252, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(118, 100);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbNews
            // 
            this.gbNews.Controls.Add(this.label22);
            this.gbNews.Controls.Add(this.txtMark);
            this.gbNews.Controls.Add(this.butNew);
            this.gbNews.Controls.Add(this.label20);
            this.gbNews.Controls.Add(this.txtNewReceiEmp);
            this.gbNews.Controls.Add(this.label17);
            this.gbNews.Controls.Add(this.cbNewOrg);
            this.gbNews.Controls.Add(this.txtNewBoxQty);
            this.gbNews.Controls.Add(this.label6);
            this.gbNews.Controls.Add(this.label19);
            this.gbNews.Controls.Add(this.label14);
            this.gbNews.Controls.Add(this.txtNewSizeQty);
            this.gbNews.Controls.Add(this.cbNewSize);
            this.gbNews.Controls.Add(this.label13);
            this.gbNews.Controls.Add(this.label7);
            this.gbNews.Controls.Add(this.cbNewColor);
            this.gbNews.Controls.Add(this.cbNewSubinv);
            this.gbNews.Controls.Add(this.cbNewStyle);
            this.gbNews.Controls.Add(this.label2);
            this.gbNews.Controls.Add(this.cbNewLine);
            this.gbNews.Controls.Add(this.label11);
            this.gbNews.Controls.Add(this.txtNewPo);
            this.gbNews.Controls.Add(this.label12);
            this.gbNews.Controls.Add(this.label15);
            this.gbNews.Controls.Add(this.label16);
            this.gbNews.Controls.Add(this.dtpNewReceiDate);
            this.gbNews.Controls.Add(this.label18);
            this.gbNews.Controls.Add(this.txtNewReceiNumber);
            this.gbNews.Location = new System.Drawing.Point(3, 6);
            this.gbNews.Name = "gbNews";
            this.gbNews.Size = new System.Drawing.Size(669, 108);
            this.gbNews.TabIndex = 40;
            this.gbNews.TabStop = false;
            this.gbNews.Text = "新增入库";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(10, 88);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 12);
            this.label22.TabIndex = 47;
            this.label22.Text = "备    注";
            // 
            // txtMark
            // 
            this.txtMark.Location = new System.Drawing.Point(67, 84);
            this.txtMark.Name = "txtMark";
            this.txtMark.Size = new System.Drawing.Size(514, 21);
            this.txtMark.TabIndex = 13;
            // 
            // butNew
            // 
            this.butNew.Location = new System.Drawing.Point(587, 12);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(78, 93);
            this.butNew.TabIndex = 14;
            this.butNew.Text = "新增";
            this.butNew.UseVisualStyleBackColor = true;
            this.butNew.Click += new System.EventHandler(this.butNew_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(439, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 56;
            this.label20.Text = "收货人";
            // 
            // txtNewReceiEmp
            // 
            this.txtNewReceiEmp.Location = new System.Drawing.Point(482, 12);
            this.txtNewReceiEmp.Name = "txtNewReceiEmp";
            this.txtNewReceiEmp.Size = new System.Drawing.Size(102, 21);
            this.txtNewReceiEmp.TabIndex = 12;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(439, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 52;
            this.label17.Text = "箱  数";
            // 
            // txtNewBoxQty
            // 
            this.txtNewBoxQty.Location = new System.Drawing.Point(482, 36);
            this.txtNewBoxQty.Name = "txtNewBoxQty";
            this.txtNewBoxQty.Size = new System.Drawing.Size(100, 21);
            this.txtNewBoxQty.TabIndex = 8;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(278, 64);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 54;
            this.label19.Text = "收货日期";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(278, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 51;
            this.label14.Text = "件    数";
            // 
            // txtNewSizeQty
            // 
            this.txtNewSizeQty.Location = new System.Drawing.Point(333, 12);
            this.txtNewSizeQty.Name = "txtNewSizeQty";
            this.txtNewSizeQty.Size = new System.Drawing.Size(100, 21);
            this.txtNewSizeQty.TabIndex = 7;
            // 
            // cbNewSize
            // 
            this.cbNewSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewSize.FormattingEnabled = true;
            this.cbNewSize.Location = new System.Drawing.Point(188, 60);
            this.cbNewSize.Name = "cbNewSize";
            this.cbNewSize.Size = new System.Drawing.Size(83, 20);
            this.cbNewSize.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(156, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 50;
            this.label13.Text = "尺码";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(10, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 45;
            this.label7.Text = "仓    库";
            // 
            // cbNewColor
            // 
            this.cbNewColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewColor.FormattingEnabled = true;
            this.cbNewColor.Location = new System.Drawing.Point(188, 36);
            this.cbNewColor.Name = "cbNewColor";
            this.cbNewColor.Size = new System.Drawing.Size(83, 20);
            this.cbNewColor.TabIndex = 5;
            // 
            // cbNewSubinv
            // 
            this.cbNewSubinv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewSubinv.FormattingEnabled = true;
            this.cbNewSubinv.Location = new System.Drawing.Point(67, 36);
            this.cbNewSubinv.Name = "cbNewSubinv";
            this.cbNewSubinv.Size = new System.Drawing.Size(83, 20);
            this.cbNewSubinv.TabIndex = 2;
            this.cbNewSubinv.SelectedIndexChanged += new System.EventHandler(this.cbNewSubinv_SelectedIndexChanged);
            // 
            // cbNewStyle
            // 
            this.cbNewStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewStyle.FormattingEnabled = true;
            this.cbNewStyle.Location = new System.Drawing.Point(188, 12);
            this.cbNewStyle.Name = "cbNewStyle";
            this.cbNewStyle.Size = new System.Drawing.Size(83, 20);
            this.cbNewStyle.TabIndex = 4;
            this.cbNewStyle.SelectedIndexChanged += new System.EventHandler(this.cbNewStyle_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(10, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 46;
            this.label2.Text = "转线线别";
            // 
            // cbNewLine
            // 
            this.cbNewLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewLine.FormattingEnabled = true;
            this.cbNewLine.Location = new System.Drawing.Point(67, 60);
            this.cbNewLine.Name = "cbNewLine";
            this.cbNewLine.Size = new System.Drawing.Size(83, 20);
            this.cbNewLine.TabIndex = 3;
            this.cbNewLine.SelectedIndexChanged += new System.EventHandler(this.cbNewLine_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(451, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 53;
            this.label11.Text = "PO #";
            this.label11.Visible = false;
            // 
            // txtNewPo
            // 
            this.txtNewPo.Location = new System.Drawing.Point(482, 60);
            this.txtNewPo.Name = "txtNewPo";
            this.txtNewPo.Size = new System.Drawing.Size(100, 21);
            this.txtNewPo.TabIndex = 9;
            this.txtNewPo.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(190, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 12);
            this.label12.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(156, 40);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 49;
            this.label15.Text = "色号";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(156, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 48;
            this.label16.Text = "款式";
            // 
            // dtpNewReceiDate
            // 
            this.dtpNewReceiDate.Location = new System.Drawing.Point(333, 60);
            this.dtpNewReceiDate.Name = "dtpNewReceiDate";
            this.dtpNewReceiDate.Size = new System.Drawing.Size(102, 21);
            this.dtpNewReceiDate.TabIndex = 10;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(278, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 55;
            this.label18.Text = "送货单号";
            // 
            // txtNewReceiNumber
            // 
            this.txtNewReceiNumber.Location = new System.Drawing.Point(333, 36);
            this.txtNewReceiNumber.Name = "txtNewReceiNumber";
            this.txtNewReceiNumber.Size = new System.Drawing.Size(102, 21);
            this.txtNewReceiNumber.TabIndex = 11;
            // 
            // MenuRight
            // 
            this.MenuRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RmeCopyCells,
            this.RmeCopyRows,
            this.RmeExportExcel,
            this.toolStripSeparator1,
            this.RmeDelRow});
            this.MenuRight.Name = "contextMenuStrip1";
            this.MenuRight.Size = new System.Drawing.Size(144, 98);
            // 
            // RmeCopyCells
            // 
            this.RmeCopyCells.Image = global::WinForm.Properties.Resources.icons8_复制_64;
            this.RmeCopyCells.Name = "RmeCopyCells";
            this.RmeCopyCells.Size = new System.Drawing.Size(143, 22);
            this.RmeCopyCells.Text = "CopyCells";
            this.RmeCopyCells.Click += new System.EventHandler(this.RmeCopyCells_Click);
            // 
            // RmeCopyRows
            // 
            this.RmeCopyRows.Image = global::WinForm.Properties.Resources.icons8_复制_48;
            this.RmeCopyRows.Name = "RmeCopyRows";
            this.RmeCopyRows.Size = new System.Drawing.Size(143, 22);
            this.RmeCopyRows.Text = "CopyRows";
            this.RmeCopyRows.Click += new System.EventHandler(this.RmeCopyRows_Click);
            // 
            // RmeExportExcel
            // 
            this.RmeExportExcel.Image = global::WinForm.Properties.Resources.Excel_32px_1185986_easyicon_net;
            this.RmeExportExcel.Name = "RmeExportExcel";
            this.RmeExportExcel.Size = new System.Drawing.Size(143, 22);
            this.RmeExportExcel.Text = "ExportExcel";
            this.RmeExportExcel.Click += new System.EventHandler(this.RmeExportExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // RmeDelRow
            // 
            this.RmeDelRow.Name = "RmeDelRow";
            this.RmeDelRow.Size = new System.Drawing.Size(143, 22);
            this.RmeDelRow.Text = "DelThisRow";
            this.RmeDelRow.Click += new System.EventHandler(this.RmeDelRow_Click);
            // 
            // FrmNoBraCodeReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 730);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbNews);
            this.Controls.Add(this.gbReceiData);
            this.Controls.Add(this.bgSearch);
            this.Name = "FrmNoBraCodeReceipt";
            this.Text = "无条码收货入库";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmNoBraCodeReceipt_Load);
            this.Resize += new System.EventHandler(this.FrmNoBraCodeReceipt_Resize);
            this.bgSearch.ResumeLayout(false);
            this.bgSearch.PerformLayout();
            this.gbReceiData.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceiData)).EndInit();
            this.gbNews.ResumeLayout(false);
            this.gbNews.PerformLayout();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.GroupBox bgSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReceiNumber;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStopDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.GroupBox gbReceiData;
        private System.Windows.Forms.DataGridView dgvReceiData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPO;
        private System.Windows.Forms.CheckedListBox cklbLocation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSubinv;
        private System.Windows.Forms.ComboBox cbNewOrg;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chbReceiDate;
        private System.Windows.Forms.GroupBox gbNews;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtNewReceiEmp;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNewSizeQty;
        private System.Windows.Forms.ComboBox cbNewSize;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbNewColor;
        private System.Windows.Forms.ComboBox cbNewSubinv;
        private System.Windows.Forms.ComboBox cbNewStyle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbNewLine;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNewPo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpNewReceiDate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtNewReceiNumber;
        private System.Windows.Forms.ComboBox cbOrg;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtNewBoxQty;
        private System.Windows.Forms.Button butNew;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtMark;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.ToolStripMenuItem RmeDelRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSearchDate;
    }
}