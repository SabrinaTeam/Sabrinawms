namespace WinForm
{
    partial class FrmComplete
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
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMyNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.dtpStopDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStarDate = new System.Windows.Forms.DateTimePicker();
            this.cbScanDate = new System.Windows.Forms.CheckBox();
            this.gbDataDetails = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvMesDataDetails = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvERPDataDetails = new System.Windows.Forms.DataGridView();
            this.dgvAllDataDetails = new System.Windows.Forms.DataGridView();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gbSearch.SuspendLayout();
            this.gbDataDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesDataDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvERPDataDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllDataDetails)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.label3);
            this.gbSearch.Controls.Add(this.butSearch);
            this.gbSearch.Controls.Add(this.label2);
            this.gbSearch.Controls.Add(this.txtMyNumber);
            this.gbSearch.Controls.Add(this.label1);
            this.gbSearch.Controls.Add(this.txtStyle);
            this.gbSearch.Controls.Add(this.dtpStopDate);
            this.gbSearch.Controls.Add(this.dtpStarDate);
            this.gbSearch.Controls.Add(this.cbScanDate);
            this.gbSearch.Location = new System.Drawing.Point(4, 3);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(835, 51);
            this.gbSearch.TabIndex = 0;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "查询条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(585, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(9, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "-";
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(749, 11);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(75, 34);
            this.butSearch.TabIndex = 6;
            this.butSearch.Text = "查询";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "自编单号";
            // 
            // txtMyNumber
            // 
            this.txtMyNumber.Location = new System.Drawing.Point(213, 18);
            this.txtMyNumber.Name = "txtMyNumber";
            this.txtMyNumber.Size = new System.Drawing.Size(142, 22);
            this.txtMyNumber.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "款式";
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(42, 18);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.Size = new System.Drawing.Size(106, 22);
            this.txtStyle.TabIndex = 1;
            // 
            // dtpStopDate
            // 
            this.dtpStopDate.Location = new System.Drawing.Point(598, 18);
            this.dtpStopDate.Name = "dtpStopDate";
            this.dtpStopDate.Size = new System.Drawing.Size(105, 22);
            this.dtpStopDate.TabIndex = 5;
            // 
            // dtpStarDate
            // 
            this.dtpStarDate.Location = new System.Drawing.Point(467, 18);
            this.dtpStarDate.Name = "dtpStarDate";
            this.dtpStarDate.Size = new System.Drawing.Size(114, 22);
            this.dtpStarDate.TabIndex = 4;
            // 
            // cbScanDate
            // 
            this.cbScanDate.AutoSize = true;
            this.cbScanDate.Location = new System.Drawing.Point(393, 21);
            this.cbScanDate.Name = "cbScanDate";
            this.cbScanDate.Size = new System.Drawing.Size(72, 16);
            this.cbScanDate.TabIndex = 3;
            this.cbScanDate.Text = "扫描日期";
            this.cbScanDate.UseVisualStyleBackColor = true;
            // 
            // gbDataDetails
            // 
            this.gbDataDetails.Controls.Add(this.splitContainer1);
            this.gbDataDetails.Location = new System.Drawing.Point(3, 60);
            this.gbDataDetails.Name = "gbDataDetails";
            this.gbDataDetails.Size = new System.Drawing.Size(834, 576);
            this.gbDataDetails.TabIndex = 1;
            this.gbDataDetails.TabStop = false;
            this.gbDataDetails.Text = "扫描报工详细数据";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 18);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvMesDataDetails);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(828, 555);
            this.splitContainer1.SplitterDistance = 276;
            this.splitContainer1.TabIndex = 5;
            // 
            // dgvMesDataDetails
            // 
            this.dgvMesDataDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMesDataDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMesDataDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvMesDataDetails.Name = "dgvMesDataDetails";
            this.dgvMesDataDetails.RowTemplate.Height = 24;
            this.dgvMesDataDetails.Size = new System.Drawing.Size(276, 555);
            this.dgvMesDataDetails.TabIndex = 2;
            this.dgvMesDataDetails.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMesDataDetails_CellMouseDown);
            this.dgvMesDataDetails.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvMesDataDetails_RowPostPaint);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvERPDataDetails);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvAllDataDetails);
            this.splitContainer2.Size = new System.Drawing.Size(548, 555);
            this.splitContainer2.SplitterDistance = 182;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvERPDataDetails
            // 
            this.dgvERPDataDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvERPDataDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvERPDataDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvERPDataDetails.Name = "dgvERPDataDetails";
            this.dgvERPDataDetails.RowTemplate.Height = 24;
            this.dgvERPDataDetails.Size = new System.Drawing.Size(548, 182);
            this.dgvERPDataDetails.TabIndex = 3;
            this.dgvERPDataDetails.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvERPDataDetails_CellMouseDown);
            this.dgvERPDataDetails.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvERPDataDetails_RowPostPaint);
            // 
            // dgvAllDataDetails
            // 
            this.dgvAllDataDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllDataDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllDataDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvAllDataDetails.Name = "dgvAllDataDetails";
            this.dgvAllDataDetails.RowTemplate.Height = 24;
            this.dgvAllDataDetails.Size = new System.Drawing.Size(548, 369);
            this.dgvAllDataDetails.TabIndex = 3;
            this.dgvAllDataDetails.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAllDataDetails_CellMouseDown);
            this.dgvAllDataDetails.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvAllDataDetails_RowPostPaint_1);
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
            this.RmeCopyCells.Size = new System.Drawing.Size(180, 22);
            this.RmeCopyCells.Text = "CopyCells";
            this.RmeCopyCells.Click += new System.EventHandler(this.RmeCopyCells_Click);
            // 
            // RmeCopyRows
            // 
            this.RmeCopyRows.Image = global::WinForm.Properties.Resources.icons8_复制_48;
            this.RmeCopyRows.Name = "RmeCopyRows";
            this.RmeCopyRows.Size = new System.Drawing.Size(180, 22);
            this.RmeCopyRows.Text = "CopyRows";
            this.RmeCopyRows.Click += new System.EventHandler(this.RmeCopyRows_Click);
            // 
            // RmeExportExcel
            // 
            this.RmeExportExcel.Image = global::WinForm.Properties.Resources.Excel_32px_1185986_easyicon_net;
            this.RmeExportExcel.Name = "RmeExportExcel";
            this.RmeExportExcel.Size = new System.Drawing.Size(180, 22);
            this.RmeExportExcel.Text = "ExportExcel";
            this.RmeExportExcel.Click += new System.EventHandler(this.RmeExportExcel_Click);
            // 
            // FrmComplete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 639);
            this.Controls.Add(this.gbDataDetails);
            this.Controls.Add(this.gbSearch);
            this.Name = "FrmComplete";
            this.Text = "报工ERP对接数据";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmComplete_Load);
            this.Resize += new System.EventHandler(this.FrmComplete_Resize);
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.gbDataDetails.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesDataDetails)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvERPDataDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllDataDetails)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMyNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.DateTimePicker dtpStopDate;
        private System.Windows.Forms.DateTimePicker dtpStarDate;
        private System.Windows.Forms.CheckBox cbScanDate;
        private System.Windows.Forms.GroupBox gbDataDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvMesDataDetails;
        private System.Windows.Forms.DataGridView dgvERPDataDetails;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvAllDataDetails;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
    }
}