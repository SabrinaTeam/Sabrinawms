namespace WinForm
{
    partial class ProductionStatus
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
            this.gbSeach = new System.Windows.Forms.GroupBox();
            this.page = new System.Windows.Forms.Label();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.butNext = new System.Windows.Forms.Button();
            this.butPrevious = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbOperationsCenter = new System.Windows.Forms.ComboBox();
            this.rbMakeStatus = new System.Windows.Forms.RadioButton();
            this.rbWIP = new System.Windows.Forms.RadioButton();
            this.butCurrentWeek = new System.Windows.Forms.Button();
            this.butNextWeek = new System.Windows.Forms.Button();
            this.butLastWeek = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSeason = new System.Windows.Forms.TextBox();
            this.txtBuyID = new System.Windows.Forms.TextBox();
            this.dtpStopDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cbDateType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMyNumber = new System.Windows.Forms.TextBox();
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.gbProductionStatus = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvProductionStatus = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gbSeach.SuspendLayout();
            this.gbProductionStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSeach
            // 
            this.gbSeach.Controls.Add(this.page);
            this.gbSeach.Controls.Add(this.txtPage);
            this.gbSeach.Controls.Add(this.butNext);
            this.gbSeach.Controls.Add(this.butPrevious);
            this.gbSeach.Controls.Add(this.label5);
            this.gbSeach.Controls.Add(this.cbOperationsCenter);
            this.gbSeach.Controls.Add(this.rbMakeStatus);
            this.gbSeach.Controls.Add(this.rbWIP);
            this.gbSeach.Controls.Add(this.butCurrentWeek);
            this.gbSeach.Controls.Add(this.butNextWeek);
            this.gbSeach.Controls.Add(this.butLastWeek);
            this.gbSeach.Controls.Add(this.butSearch);
            this.gbSeach.Controls.Add(this.label4);
            this.gbSeach.Controls.Add(this.txtSeason);
            this.gbSeach.Controls.Add(this.txtBuyID);
            this.gbSeach.Controls.Add(this.dtpStopDate);
            this.gbSeach.Controls.Add(this.dtpStartDate);
            this.gbSeach.Controls.Add(this.cbDateType);
            this.gbSeach.Controls.Add(this.label3);
            this.gbSeach.Controls.Add(this.label2);
            this.gbSeach.Controls.Add(this.label1);
            this.gbSeach.Controls.Add(this.txtMyNumber);
            this.gbSeach.Controls.Add(this.cbDate);
            this.gbSeach.Location = new System.Drawing.Point(6, 5);
            this.gbSeach.Name = "gbSeach";
            this.gbSeach.Size = new System.Drawing.Size(1207, 58);
            this.gbSeach.TabIndex = 0;
            this.gbSeach.TabStop = false;
            this.gbSeach.Text = "查询条件";
            // 
            // page
            // 
            this.page.AutoSize = true;
            this.page.Location = new System.Drawing.Point(1020, 13);
            this.page.Name = "page";
            this.page.Size = new System.Drawing.Size(0, 12);
            this.page.TabIndex = 27;
            // 
            // txtPage
            // 
            this.txtPage.Location = new System.Drawing.Point(1016, 27);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(62, 22);
            this.txtPage.TabIndex = 26;
            // 
            // butNext
            // 
            this.butNext.Location = new System.Drawing.Point(1082, 13);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(37, 36);
            this.butNext.TabIndex = 25;
            this.butNext.Text = "下一页";
            this.butNext.UseVisualStyleBackColor = true;
            this.butNext.Click += new System.EventHandler(this.butNext_Click);
            // 
            // butPrevious
            // 
            this.butPrevious.Location = new System.Drawing.Point(974, 15);
            this.butPrevious.Name = "butPrevious";
            this.butPrevious.Size = new System.Drawing.Size(39, 36);
            this.butPrevious.TabIndex = 24;
            this.butPrevious.Text = "上一页";
            this.butPrevious.UseVisualStyleBackColor = true;
            this.butPrevious.Click += new System.EventHandler(this.butPrevious_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "营运中心:";
            // 
            // cbOperationsCenter
            // 
            this.cbOperationsCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperationsCenter.FormattingEnabled = true;
            this.cbOperationsCenter.Items.AddRange(new object[] {
            "SAA",
            "TOP"});
            this.cbOperationsCenter.Location = new System.Drawing.Point(7, 31);
            this.cbOperationsCenter.Name = "cbOperationsCenter";
            this.cbOperationsCenter.Size = new System.Drawing.Size(63, 20);
            this.cbOperationsCenter.TabIndex = 22;
            // 
            // rbMakeStatus
            // 
            this.rbMakeStatus.AutoSize = true;
            this.rbMakeStatus.Location = new System.Drawing.Point(87, 33);
            this.rbMakeStatus.Name = "rbMakeStatus";
            this.rbMakeStatus.Size = new System.Drawing.Size(108, 16);
            this.rbMakeStatus.TabIndex = 21;
            this.rbMakeStatus.TabStop = true;
            this.rbMakeStatus.Text = "PI 生产进度报表";
            this.rbMakeStatus.UseVisualStyleBackColor = true;
            // 
            // rbWIP
            // 
            this.rbWIP.AutoSize = true;
            this.rbWIP.Location = new System.Drawing.Point(87, 13);
            this.rbWIP.Name = "rbWIP";
            this.rbWIP.Size = new System.Drawing.Size(71, 16);
            this.rbWIP.TabIndex = 20;
            this.rbWIP.TabStop = true;
            this.rbWIP.Text = "WIP 报表";
            this.rbWIP.UseVisualStyleBackColor = true;
            // 
            // butCurrentWeek
            // 
            this.butCurrentWeek.Location = new System.Drawing.Point(651, 12);
            this.butCurrentWeek.Name = "butCurrentWeek";
            this.butCurrentWeek.Size = new System.Drawing.Size(76, 18);
            this.butCurrentWeek.TabIndex = 16;
            this.butCurrentWeek.Text = "本星期";
            this.butCurrentWeek.UseVisualStyleBackColor = true;
            this.butCurrentWeek.Click += new System.EventHandler(this.butCurrentWeek_Click);
            // 
            // butNextWeek
            // 
            this.butNextWeek.Location = new System.Drawing.Point(858, 12);
            this.butNextWeek.Name = "butNextWeek";
            this.butNextWeek.Size = new System.Drawing.Size(108, 18);
            this.butNextWeek.TabIndex = 15;
            this.butNextWeek.Text = "后一星期";
            this.butNextWeek.UseVisualStyleBackColor = true;
            this.butNextWeek.Click += new System.EventHandler(this.butNextWeek_Click);
            // 
            // butLastWeek
            // 
            this.butLastWeek.Location = new System.Drawing.Point(730, 12);
            this.butLastWeek.Name = "butLastWeek";
            this.butLastWeek.Size = new System.Drawing.Size(108, 18);
            this.butLastWeek.TabIndex = 14;
            this.butLastWeek.Text = "前一星期";
            this.butLastWeek.UseVisualStyleBackColor = true;
            this.butLastWeek.Click += new System.EventHandler(this.butLastWeek_Click);
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(1121, 11);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(79, 40);
            this.butSearch.TabIndex = 13;
            this.butSearch.Text = "查询";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(846, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(9, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "-";
            // 
            // txtSeason
            // 
            this.txtSeason.Location = new System.Drawing.Point(407, 30);
            this.txtSeason.Name = "txtSeason";
            this.txtSeason.Size = new System.Drawing.Size(92, 22);
            this.txtSeason.TabIndex = 11;
            this.txtSeason.TextChanged += new System.EventHandler(this.txtSeason_TextChanged);
            // 
            // txtBuyID
            // 
            this.txtBuyID.Location = new System.Drawing.Point(524, 30);
            this.txtBuyID.Name = "txtBuyID";
            this.txtBuyID.Size = new System.Drawing.Size(92, 22);
            this.txtBuyID.TabIndex = 10;
            this.txtBuyID.TextChanged += new System.EventHandler(this.txtBuyID_TextChanged);
            // 
            // dtpStopDate
            // 
            this.dtpStopDate.Location = new System.Drawing.Point(858, 30);
            this.dtpStopDate.Name = "dtpStopDate";
            this.dtpStopDate.Size = new System.Drawing.Size(108, 22);
            this.dtpStopDate.TabIndex = 9;
            this.dtpStopDate.ValueChanged += new System.EventHandler(this.dtpStopDate_ValueChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(730, 30);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(108, 22);
            this.dtpStartDate.TabIndex = 8;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // cbDateType
            // 
            this.cbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDateType.FormattingEnabled = true;
            this.cbDateType.Items.AddRange(new object[] {
            "订单日期",
            "   交  期",
            "报工时间"});
            this.cbDateType.Location = new System.Drawing.Point(651, 31);
            this.cbDateType.Name = "cbDateType";
            this.cbDateType.Size = new System.Drawing.Size(76, 20);
            this.cbDateType.TabIndex = 6;
            this.cbDateType.SelectedIndexChanged += new System.EventHandler(this.cbDateType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(407, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "季节:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(524, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "月BUY:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "自编单号:";
            // 
            // txtMyNumber
            // 
            this.txtMyNumber.Location = new System.Drawing.Point(205, 30);
            this.txtMyNumber.Name = "txtMyNumber";
            this.txtMyNumber.Size = new System.Drawing.Size(187, 22);
            this.txtMyNumber.TabIndex = 0;
            this.txtMyNumber.TextChanged += new System.EventHandler(this.txtMyNumber_TextChanged);
            // 
            // cbDate
            // 
            this.cbDate.AutoSize = true;
            this.cbDate.Location = new System.Drawing.Point(635, 33);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(77, 16);
            this.cbDate.TabIndex = 17;
            this.cbDate.Text = "checkBox1";
            this.cbDate.UseVisualStyleBackColor = true;
            this.cbDate.CheckedChanged += new System.EventHandler(this.cbDate_CheckedChanged);
            // 
            // gbProductionStatus
            // 
            this.gbProductionStatus.Controls.Add(this.label6);
            this.gbProductionStatus.Controls.Add(this.splitContainer1);
            this.gbProductionStatus.Location = new System.Drawing.Point(6, 69);
            this.gbProductionStatus.Name = "gbProductionStatus";
            this.gbProductionStatus.Size = new System.Drawing.Size(1207, 612);
            this.gbProductionStatus.TabIndex = 1;
            this.gbProductionStatus.TabStop = false;
            this.gbProductionStatus.Text = "Production  Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "MES 进度追踪";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 18);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvProductionStatus);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1201, 591);
            this.splitContainer1.SplitterDistance = 295;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // dgvProductionStatus
            // 
            this.dgvProductionStatus.AllowUserToAddRows = false;
            this.dgvProductionStatus.AllowUserToDeleteRows = false;
            this.dgvProductionStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductionStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductionStatus.Location = new System.Drawing.Point(0, 0);
            this.dgvProductionStatus.Name = "dgvProductionStatus";
            this.dgvProductionStatus.ReadOnly = true;
            this.dgvProductionStatus.RowTemplate.Height = 24;
            this.dgvProductionStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductionStatus.Size = new System.Drawing.Size(1201, 295);
            this.dgvProductionStatus.TabIndex = 0;
            this.dgvProductionStatus.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvProductionStatus_CellMouseDown);
            this.dgvProductionStatus.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProductionStatus_RowPostPaint);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label8);
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer2.Size = new System.Drawing.Size(1201, 292);
            this.splitContainer2.SplitterDistance = 398;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(314, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "Best 剩余";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(398, 292);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(653, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "ERP 报工剩余";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(799, 292);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseDown);
            this.dataGridView2.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView2_RowPostPaint);
            // 
            // MenuRight
            // 
            this.MenuRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RmeCopyCells,
            this.RmeCopyRows,
            this.RmeExportExcel});
            this.MenuRight.Name = "contextMenuStrip1";
            this.MenuRight.Size = new System.Drawing.Size(144, 70);
            // 
            // RmeCopyCells
            // 
            this.RmeCopyCells.Name = "RmeCopyCells";
            this.RmeCopyCells.Size = new System.Drawing.Size(143, 22);
            this.RmeCopyCells.Text = "CopyCells";
            this.RmeCopyCells.Click += new System.EventHandler(this.RmeCopyCells_Click);
            // 
            // RmeCopyRows
            // 
            this.RmeCopyRows.Name = "RmeCopyRows";
            this.RmeCopyRows.Size = new System.Drawing.Size(143, 22);
            this.RmeCopyRows.Text = "CopyRows";
            this.RmeCopyRows.Click += new System.EventHandler(this.RmeCopyRows_Click);
            // 
            // RmeExportExcel
            // 
            this.RmeExportExcel.Name = "RmeExportExcel";
            this.RmeExportExcel.Size = new System.Drawing.Size(143, 22);
            this.RmeExportExcel.Text = "ExportExcel";
            this.RmeExportExcel.Click += new System.EventHandler(this.RmeExportExcel_Click);
            // 
            // ProductionStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 684);
            this.Controls.Add(this.gbProductionStatus);
            this.Controls.Add(this.gbSeach);
            this.Name = "ProductionStatus";
            this.Text = "Production Status";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ProductionStatus_Load);
            this.Resize += new System.EventHandler(this.ProductionStatus_Resize);
            this.gbSeach.ResumeLayout(false);
            this.gbSeach.PerformLayout();
            this.gbProductionStatus.ResumeLayout(false);
            this.gbProductionStatus.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionStatus)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSeach;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSeason;
        private System.Windows.Forms.TextBox txtBuyID;
        private System.Windows.Forms.DateTimePicker dtpStopDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ComboBox cbDateType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMyNumber;
        private System.Windows.Forms.GroupBox gbProductionStatus;
        private System.Windows.Forms.DataGridView dgvProductionStatus;
        private System.Windows.Forms.Button butNextWeek;
        private System.Windows.Forms.Button butLastWeek;
        private System.Windows.Forms.Button butCurrentWeek;
        private System.Windows.Forms.CheckBox cbDate;
        private System.Windows.Forms.RadioButton rbMakeStatus;
        private System.Windows.Forms.RadioButton rbWIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbOperationsCenter;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label page;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Button butNext;
        private System.Windows.Forms.Button butPrevious;
    }
}