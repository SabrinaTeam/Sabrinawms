namespace WinForm
{
    partial class FrmRFIDScanSearch
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
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.labStyle = new System.Windows.Forms.Label();
            this.labPO = new System.Windows.Forms.Label();
            this.txtPO = new System.Windows.Forms.TextBox();
            this.labColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.gbSearchFilter = new System.Windows.Forms.GroupBox();
            this.ckScanDate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStopDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.gbScanDetails = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvBoxsHeads = new System.Windows.Forms.DataGridView();
            this.dgvScanDetails = new System.Windows.Forms.DataGridView();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.cbScanType = new System.Windows.Forms.ComboBox();
            this.labCartonNumber = new System.Windows.Forms.Label();
            this.txtCartonNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbSearchFilter.SuspendLayout();
            this.gbScanDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxsHeads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScanDetails)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(967, 16);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(88, 34);
            this.butSearch.TabIndex = 0;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(135, 30);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.Size = new System.Drawing.Size(103, 22);
            this.txtStyle.TabIndex = 1;
            // 
            // labStyle
            // 
            this.labStyle.AutoSize = true;
            this.labStyle.Location = new System.Drawing.Point(137, 15);
            this.labStyle.Name = "labStyle";
            this.labStyle.Size = new System.Drawing.Size(61, 12);
            this.labStyle.TabIndex = 2;
            this.labStyle.Text = "Buyer_Item";
            // 
            // labPO
            // 
            this.labPO.AutoSize = true;
            this.labPO.Location = new System.Drawing.Point(358, 15);
            this.labPO.Name = "labPO";
            this.labPO.Size = new System.Drawing.Size(19, 12);
            this.labPO.TabIndex = 4;
            this.labPO.Text = "PO";
            // 
            // txtPO
            // 
            this.txtPO.Location = new System.Drawing.Point(353, 30);
            this.txtPO.Name = "txtPO";
            this.txtPO.Size = new System.Drawing.Size(103, 22);
            this.txtPO.TabIndex = 3;
            // 
            // labColor
            // 
            this.labColor.AutoSize = true;
            this.labColor.Location = new System.Drawing.Point(249, 15);
            this.labColor.Name = "labColor";
            this.labColor.Size = new System.Drawing.Size(60, 12);
            this.labColor.TabIndex = 6;
            this.labColor.Text = "Color_code";
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(244, 30);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(103, 22);
            this.txtColor.TabIndex = 5;
            // 
            // gbSearchFilter
            // 
            this.gbSearchFilter.Controls.Add(this.label1);
            this.gbSearchFilter.Controls.Add(this.labCartonNumber);
            this.gbSearchFilter.Controls.Add(this.txtCartonNumber);
            this.gbSearchFilter.Controls.Add(this.cbScanType);
            this.gbSearchFilter.Controls.Add(this.ckScanDate);
            this.gbSearchFilter.Controls.Add(this.label2);
            this.gbSearchFilter.Controls.Add(this.dtpStopDate);
            this.gbSearchFilter.Controls.Add(this.dtpStartDate);
            this.gbSearchFilter.Controls.Add(this.labColor);
            this.gbSearchFilter.Controls.Add(this.txtColor);
            this.gbSearchFilter.Controls.Add(this.labPO);
            this.gbSearchFilter.Controls.Add(this.txtPO);
            this.gbSearchFilter.Controls.Add(this.labStyle);
            this.gbSearchFilter.Controls.Add(this.txtStyle);
            this.gbSearchFilter.Controls.Add(this.butSearch);
            this.gbSearchFilter.Location = new System.Drawing.Point(2, 2);
            this.gbSearchFilter.Name = "gbSearchFilter";
            this.gbSearchFilter.Size = new System.Drawing.Size(1061, 56);
            this.gbSearchFilter.TabIndex = 9;
            this.gbSearchFilter.TabStop = false;
            this.gbSearchFilter.Text = "查询条件";
            // 
            // ckScanDate
            // 
            this.ckScanDate.AutoSize = true;
            this.ckScanDate.Location = new System.Drawing.Point(586, 13);
            this.ckScanDate.Name = "ckScanDate";
            this.ckScanDate.Size = new System.Drawing.Size(72, 16);
            this.ckScanDate.TabIndex = 13;
            this.ckScanDate.Text = "扫描日期";
            this.ckScanDate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(697, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(9, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "-";
            // 
            // dtpStopDate
            // 
            this.dtpStopDate.Location = new System.Drawing.Point(712, 30);
            this.dtpStopDate.Name = "dtpStopDate";
            this.dtpStopDate.Size = new System.Drawing.Size(110, 22);
            this.dtpStopDate.TabIndex = 11;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(584, 30);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(110, 22);
            this.dtpStartDate.TabIndex = 10;
            // 
            // gbScanDetails
            // 
            this.gbScanDetails.Controls.Add(this.splitContainer1);
            this.gbScanDetails.Location = new System.Drawing.Point(3, 60);
            this.gbScanDetails.Name = "gbScanDetails";
            this.gbScanDetails.Size = new System.Drawing.Size(1060, 494);
            this.gbScanDetails.TabIndex = 10;
            this.gbScanDetails.TabStop = false;
            this.gbScanDetails.Text = "扫描明细";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 18);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvBoxsHeads);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvScanDetails);
            this.splitContainer1.Size = new System.Drawing.Size(1054, 473);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvBoxsHeads
            // 
            this.dgvBoxsHeads.AllowUserToAddRows = false;
            this.dgvBoxsHeads.AllowUserToDeleteRows = false;
            this.dgvBoxsHeads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoxsHeads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBoxsHeads.Location = new System.Drawing.Point(0, 0);
            this.dgvBoxsHeads.MultiSelect = false;
            this.dgvBoxsHeads.Name = "dgvBoxsHeads";
            this.dgvBoxsHeads.ReadOnly = true;
            this.dgvBoxsHeads.RowTemplate.Height = 24;
            this.dgvBoxsHeads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBoxsHeads.Size = new System.Drawing.Size(350, 473);
            this.dgvBoxsHeads.TabIndex = 0;
            this.dgvBoxsHeads.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoxsHeads_CellDoubleClick);
            this.dgvBoxsHeads.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBoxsHeads_CellMouseDown);
            this.dgvBoxsHeads.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvBoxsHeads_RowPostPaint);
            // 
            // dgvScanDetails
            // 
            this.dgvScanDetails.AllowUserToAddRows = false;
            this.dgvScanDetails.AllowUserToDeleteRows = false;
            this.dgvScanDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScanDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvScanDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvScanDetails.Name = "dgvScanDetails";
            this.dgvScanDetails.ReadOnly = true;
            this.dgvScanDetails.RowTemplate.Height = 24;
            this.dgvScanDetails.Size = new System.Drawing.Size(700, 473);
            this.dgvScanDetails.TabIndex = 0;
            this.dgvScanDetails.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvScanDetails_CellMouseDown);
            this.dgvScanDetails.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvScanDetails_RowPostPaint);
            // 
            // MenuRight
            // 
            this.MenuRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RmeCopyCells,
            this.RmeCopyRows,
            this.RmeExportExcel});
            this.MenuRight.Name = "contextMenuStrip1";
            this.MenuRight.Size = new System.Drawing.Size(141, 70);
            // 
            // RmeCopyCells
            // 
            this.RmeCopyCells.Image = global::WinForm.Properties.Resources.icons8_复制_64;
            this.RmeCopyCells.Name = "RmeCopyCells";
            this.RmeCopyCells.Size = new System.Drawing.Size(140, 22);
            this.RmeCopyCells.Text = "CopyCells";
            this.RmeCopyCells.Click += new System.EventHandler(this.RmeCopyCells_Click);
            // 
            // RmeCopyRows
            // 
            this.RmeCopyRows.Image = global::WinForm.Properties.Resources.icons8_复制_48;
            this.RmeCopyRows.Name = "RmeCopyRows";
            this.RmeCopyRows.Size = new System.Drawing.Size(140, 22);
            this.RmeCopyRows.Text = "CopyRows";
            this.RmeCopyRows.Click += new System.EventHandler(this.RmeCopyRows_Click);
            // 
            // RmeExportExcel
            // 
            this.RmeExportExcel.Image = global::WinForm.Properties.Resources.Excel_32px_1185986_easyicon_net;
            this.RmeExportExcel.Name = "RmeExportExcel";
            this.RmeExportExcel.Size = new System.Drawing.Size(140, 22);
            this.RmeExportExcel.Text = "ExportExcel";
            this.RmeExportExcel.Click += new System.EventHandler(this.RmeExportExcel_Click);
            // 
            // cbScanType
            // 
            this.cbScanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScanType.FormattingEnabled = true;
            this.cbScanType.Items.AddRange(new object[] {
            "单件扫描",
            "整箱扫描"});
            this.cbScanType.Location = new System.Drawing.Point(8, 31);
            this.cbScanType.Name = "cbScanType";
            this.cbScanType.Size = new System.Drawing.Size(121, 20);
            this.cbScanType.TabIndex = 14;
            // 
            // labCartonNumber
            // 
            this.labCartonNumber.AutoSize = true;
            this.labCartonNumber.Location = new System.Drawing.Point(469, 15);
            this.labCartonNumber.Name = "labCartonNumber";
            this.labCartonNumber.Size = new System.Drawing.Size(75, 12);
            this.labCartonNumber.TabIndex = 16;
            this.labCartonNumber.Text = "CartonNumber";
            // 
            // txtCartonNumber
            // 
            this.txtCartonNumber.Location = new System.Drawing.Point(468, 30);
            this.txtCartonNumber.Name = "txtCartonNumber";
            this.txtCartonNumber.Size = new System.Drawing.Size(103, 22);
            this.txtCartonNumber.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "查询类型";
            // 
            // FrmRFIDScanSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 557);
            this.Controls.Add(this.gbScanDetails);
            this.Controls.Add(this.gbSearchFilter);
            this.Name = "FrmRFIDScanSearch";
            this.Text = "RFID 扫描记录查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRFIDScanSearch_Load);
            this.Resize += new System.EventHandler(this.FrmRFIDScanSearch_Resize);
            this.gbSearchFilter.ResumeLayout(false);
            this.gbSearchFilter.PerformLayout();
            this.gbScanDetails.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxsHeads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScanDetails)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.Label labStyle;
        private System.Windows.Forms.Label labPO;
        private System.Windows.Forms.TextBox txtPO;
        private System.Windows.Forms.Label labColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.GroupBox gbSearchFilter;
        private System.Windows.Forms.GroupBox gbScanDetails;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvBoxsHeads;
        private System.Windows.Forms.DataGridView dgvScanDetails;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStopDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.CheckBox ckScanDate;
        private System.Windows.Forms.ComboBox cbScanType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labCartonNumber;
        private System.Windows.Forms.TextBox txtCartonNumber;
    }
}