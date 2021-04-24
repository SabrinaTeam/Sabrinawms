namespace WinForm
{
    partial class FrmOutgoing
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
            this.cbSubinv = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.dtpkStarTime = new System.Windows.Forms.DateTimePicker();
            this.dtpkStopTime = new System.Windows.Forms.DateTimePicker();
            this.bgSearch = new System.Windows.Forms.GroupBox();
            this.butExcelReport = new System.Windows.Forms.Button();
            this.labWorking2 = new System.Windows.Forms.Label();
            this.labWorking = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.rabutTOP = new System.Windows.Forms.RadioButton();
            this.rabutSAA = new System.Windows.Forms.RadioButton();
            this.butSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.tbOut = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvOutgoingTable = new System.Windows.Forms.DataGridView();
            this.dgvOutMWH = new System.Windows.Forms.DataGridView();
            this.dgvOutCount = new System.Windows.Forms.DataGridView();
            this.bgSearch.SuspendLayout();
            this.MenuRight.SuspendLayout();
            this.tbOut.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutgoingTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutMWH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutCount)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSubinv
            // 
            this.cbSubinv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubinv.FormattingEnabled = true;
            this.cbSubinv.Location = new System.Drawing.Point(237, 25);
            this.cbSubinv.Name = "cbSubinv";
            this.cbSubinv.Size = new System.Drawing.Size(121, 20);
            this.cbSubinv.TabIndex = 0;
            this.cbSubinv.SelectedIndexChanged += new System.EventHandler(this.cbSubinv_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "仓库";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(368, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "储位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(539, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "时间段";
            // 
            // cbLocation
            // 
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Location = new System.Drawing.Point(403, 25);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(121, 20);
            this.cbLocation.TabIndex = 4;
            this.cbLocation.SelectedIndexChanged += new System.EventHandler(this.cbLocation_SelectedIndexChanged);
            // 
            // dtpkStarTime
            // 
            this.dtpkStarTime.Location = new System.Drawing.Point(585, 25);
            this.dtpkStarTime.Name = "dtpkStarTime";
            this.dtpkStarTime.Size = new System.Drawing.Size(110, 21);
            this.dtpkStarTime.TabIndex = 5;
            // 
            // dtpkStopTime
            // 
            this.dtpkStopTime.Location = new System.Drawing.Point(720, 25);
            this.dtpkStopTime.Name = "dtpkStopTime";
            this.dtpkStopTime.Size = new System.Drawing.Size(110, 21);
            this.dtpkStopTime.TabIndex = 6;
            // 
            // bgSearch
            // 
            this.bgSearch.Controls.Add(this.butExcelReport);
            this.bgSearch.Controls.Add(this.labWorking2);
            this.bgSearch.Controls.Add(this.labWorking);
            this.bgSearch.Controls.Add(this.progressBar2);
            this.bgSearch.Controls.Add(this.progressBar1);
            this.bgSearch.Controls.Add(this.rabutTOP);
            this.bgSearch.Controls.Add(this.rabutSAA);
            this.bgSearch.Controls.Add(this.butSearch);
            this.bgSearch.Controls.Add(this.label4);
            this.bgSearch.Controls.Add(this.cbLocation);
            this.bgSearch.Controls.Add(this.cbSubinv);
            this.bgSearch.Controls.Add(this.dtpkStopTime);
            this.bgSearch.Controls.Add(this.label1);
            this.bgSearch.Controls.Add(this.dtpkStarTime);
            this.bgSearch.Controls.Add(this.label3);
            this.bgSearch.Controls.Add(this.label2);
            this.bgSearch.Location = new System.Drawing.Point(5, 5);
            this.bgSearch.Name = "bgSearch";
            this.bgSearch.Size = new System.Drawing.Size(931, 91);
            this.bgSearch.TabIndex = 8;
            this.bgSearch.TabStop = false;
            this.bgSearch.Text = "查询条件";
            // 
            // butExcelReport
            // 
            this.butExcelReport.Location = new System.Drawing.Point(848, 57);
            this.butExcelReport.Name = "butExcelReport";
            this.butExcelReport.Size = new System.Drawing.Size(75, 31);
            this.butExcelReport.TabIndex = 15;
            this.butExcelReport.Text = "导出汇总表";
            this.butExcelReport.UseVisualStyleBackColor = true;
            this.butExcelReport.Click += new System.EventHandler(this.butExcelReport_Click);
            // 
            // labWorking2
            // 
            this.labWorking2.AutoSize = true;
            this.labWorking2.Location = new System.Drawing.Point(7, 73);
            this.labWorking2.Name = "labWorking2";
            this.labWorking2.Size = new System.Drawing.Size(77, 12);
            this.labWorking2.TabIndex = 14;
            this.labWorking2.Text = "总任务进度：";
            // 
            // labWorking
            // 
            this.labWorking.AutoSize = true;
            this.labWorking.Location = new System.Drawing.Point(7, 57);
            this.labWorking.Name = "labWorking";
            this.labWorking.Size = new System.Drawing.Size(89, 12);
            this.labWorking.TabIndex = 13;
            this.labWorking.Text = "本次任务进度：";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(237, 73);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(600, 15);
            this.progressBar2.TabIndex = 12;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(238, 57);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(599, 10);
            this.progressBar1.TabIndex = 11;
            // 
            // rabutTOP
            // 
            this.rabutTOP.AutoSize = true;
            this.rabutTOP.Location = new System.Drawing.Point(126, 27);
            this.rabutTOP.Name = "rabutTOP";
            this.rabutTOP.Size = new System.Drawing.Size(41, 16);
            this.rabutTOP.TabIndex = 10;
            this.rabutTOP.TabStop = true;
            this.rabutTOP.Text = "TOP";
            this.rabutTOP.UseVisualStyleBackColor = true;
            this.rabutTOP.CheckedChanged += new System.EventHandler(this.rabutTOP_CheckedChanged);
            // 
            // rabutSAA
            // 
            this.rabutSAA.AutoSize = true;
            this.rabutSAA.Location = new System.Drawing.Point(32, 27);
            this.rabutSAA.Name = "rabutSAA";
            this.rabutSAA.Size = new System.Drawing.Size(41, 16);
            this.rabutSAA.TabIndex = 9;
            this.rabutSAA.TabStop = true;
            this.rabutSAA.Text = "SAA";
            this.rabutSAA.UseVisualStyleBackColor = true;
            this.rabutSAA.CheckedChanged += new System.EventHandler(this.rabutSAA_CheckedChanged);
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(848, 10);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(74, 45);
            this.butSearch.TabIndex = 8;
            this.butSearch.Text = "查询";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(701, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "至";
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
            // tbOut
            // 
            this.tbOut.Controls.Add(this.tabPage1);
            this.tbOut.Controls.Add(this.tabPage2);
            this.tbOut.Location = new System.Drawing.Point(5, 102);
            this.tbOut.Name = "tbOut";
            this.tbOut.SelectedIndex = 0;
            this.tbOut.Size = new System.Drawing.Size(931, 596);
            this.tbOut.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(923, 570);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "入库详情";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvOutCount);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(923, 570);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "汇总";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvOutgoingTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvOutMWH);
            this.splitContainer1.Size = new System.Drawing.Size(917, 564);
            this.splitContainer1.SplitterDistance = 643;
            this.splitContainer1.TabIndex = 9;
            // 
            // dgvOutgoingTable
            // 
            this.dgvOutgoingTable.AllowUserToAddRows = false;
            this.dgvOutgoingTable.AllowUserToDeleteRows = false;
            this.dgvOutgoingTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOutgoingTable.Location = new System.Drawing.Point(0, 0);
            this.dgvOutgoingTable.Name = "dgvOutgoingTable";
            this.dgvOutgoingTable.ReadOnly = true;
            this.dgvOutgoingTable.RowTemplate.Height = 23;
            this.dgvOutgoingTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutgoingTable.Size = new System.Drawing.Size(639, 560);
            this.dgvOutgoingTable.TabIndex = 8;
            this.dgvOutgoingTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOutgoingTable_CellDoubleClick);
            this.dgvOutgoingTable.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOutgoingTable_CellMouseDown);
            this.dgvOutgoingTable.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvOutgoingTable_RowPostPaint);
            // 
            // dgvOutMWH
            // 
            this.dgvOutMWH.AllowUserToAddRows = false;
            this.dgvOutMWH.AllowUserToDeleteRows = false;
            this.dgvOutMWH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutMWH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOutMWH.Location = new System.Drawing.Point(0, 0);
            this.dgvOutMWH.Name = "dgvOutMWH";
            this.dgvOutMWH.ReadOnly = true;
            this.dgvOutMWH.RowTemplate.Height = 23;
            this.dgvOutMWH.Size = new System.Drawing.Size(266, 560);
            this.dgvOutMWH.TabIndex = 0;
            // 
            // dgvOutCount
            // 
            this.dgvOutCount.AllowUserToAddRows = false;
            this.dgvOutCount.AllowUserToDeleteRows = false;
            this.dgvOutCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOutCount.Location = new System.Drawing.Point(3, 3);
            this.dgvOutCount.Name = "dgvOutCount";
            this.dgvOutCount.ReadOnly = true;
            this.dgvOutCount.RowTemplate.Height = 23;
            this.dgvOutCount.Size = new System.Drawing.Size(917, 564);
            this.dgvOutCount.TabIndex = 0;
            this.dgvOutCount.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOutCount_CellMouseDown);
            this.dgvOutCount.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvOutCount_RowPostPaint);
            // 
            // FrmOutgoing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 697);
            this.Controls.Add(this.tbOut);
            this.Controls.Add(this.bgSearch);
            this.Name = "FrmOutgoing";
            this.Text = "Nike 入库查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOutgoing_Load);
            this.Resize += new System.EventHandler(this.FrmOutgoing_Resize);
            this.bgSearch.ResumeLayout(false);
            this.bgSearch.PerformLayout();
            this.MenuRight.ResumeLayout(false);
            this.tbOut.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutgoingTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutMWH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSubinv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLocation;
        private System.Windows.Forms.DateTimePicker dtpkStarTime;
        private System.Windows.Forms.DateTimePicker dtpkStopTime;
        private System.Windows.Forms.GroupBox bgSearch;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rabutTOP;
        private System.Windows.Forms.RadioButton rabutSAA;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label labWorking;
        private System.Windows.Forms.Label labWorking2;
        private System.Windows.Forms.Button butExcelReport;
        private System.Windows.Forms.TabControl tbOut;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvOutgoingTable;
        private System.Windows.Forms.DataGridView dgvOutMWH;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvOutCount;
    }
}