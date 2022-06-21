namespace WinForm
{
    partial class FrmSizeRun
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
            this.txtMyNumber = new System.Windows.Forms.TextBox();
            this.labMyNumber = new System.Windows.Forms.Label();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYYMM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.btSearch = new System.Windows.Forms.Button();
            this.dgvSizeRunAll = new System.Windows.Forms.DataGridView();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.txtcust_abbr = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSizeRun = new System.Windows.Forms.DataGridView();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.ckLogs = new System.Windows.Forms.CheckBox();
            this.gbSize = new System.Windows.Forms.GroupBox();
            this.gbLogs = new System.Windows.Forms.GroupBox();
            this.gbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeRunAll)).BeginInit();
            this.MenuRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeRun)).BeginInit();
            this.gbSize.SuspendLayout();
            this.gbLogs.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMyNumber
            // 
            this.txtMyNumber.Location = new System.Drawing.Point(8, 28);
            this.txtMyNumber.Name = "txtMyNumber";
            this.txtMyNumber.Size = new System.Drawing.Size(195, 22);
            this.txtMyNumber.TabIndex = 0;
            this.txtMyNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMyNumber_KeyDown);
            // 
            // labMyNumber
            // 
            this.labMyNumber.AutoSize = true;
            this.labMyNumber.Location = new System.Drawing.Point(8, 14);
            this.labMyNumber.Name = "labMyNumber";
            this.labMyNumber.Size = new System.Drawing.Size(53, 12);
            this.labMyNumber.TabIndex = 1;
            this.labMyNumber.Text = "自编单号";
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.label3);
            this.gbSearch.Controls.Add(this.label2);
            this.gbSearch.Controls.Add(this.txtcust_abbr);
            this.gbSearch.Controls.Add(this.txtYYMM);
            this.gbSearch.Controls.Add(this.label1);
            this.gbSearch.Controls.Add(this.txtStyle);
            this.gbSearch.Controls.Add(this.btSearch);
            this.gbSearch.Controls.Add(this.labMyNumber);
            this.gbSearch.Controls.Add(this.txtMyNumber);
            this.gbSearch.Location = new System.Drawing.Point(1, 6);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(859, 55);
            this.gbSearch.TabIndex = 2;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "查询条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(557, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Cust_Abbr";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(385, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "YYYYMM-X";
            // 
            // txtYYMM
            // 
            this.txtYYMM.Location = new System.Drawing.Point(383, 28);
            this.txtYYMM.Name = "txtYYMM";
            this.txtYYMM.Size = new System.Drawing.Size(166, 22);
            this.txtYYMM.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "款式号";
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(211, 28);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.Size = new System.Drawing.Size(166, 22);
            this.txtStyle.TabIndex = 5;
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(730, 13);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(91, 36);
            this.btSearch.TabIndex = 4;
            this.btSearch.Text = "查询";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // dgvSizeRunAll
            // 
            this.dgvSizeRunAll.AllowUserToAddRows = false;
            this.dgvSizeRunAll.AllowUserToDeleteRows = false;
            this.dgvSizeRunAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSizeRunAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSizeRunAll.Location = new System.Drawing.Point(0, 0);
            this.dgvSizeRunAll.Name = "dgvSizeRunAll";
            this.dgvSizeRunAll.ReadOnly = true;
            this.dgvSizeRunAll.RowTemplate.Height = 23;
            this.dgvSizeRunAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSizeRunAll.Size = new System.Drawing.Size(850, 235);
            this.dgvSizeRunAll.TabIndex = 0;
            this.dgvSizeRunAll.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSizeRunAll_CellMouseDown);
            this.dgvSizeRunAll.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSizeRunAll_RowPostPaint);
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
            // txtcust_abbr
            // 
            this.txtcust_abbr.Location = new System.Drawing.Point(555, 28);
            this.txtcust_abbr.Name = "txtcust_abbr";
            this.txtcust_abbr.Size = new System.Drawing.Size(166, 22);
            this.txtcust_abbr.TabIndex = 9;
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
            this.splitContainer1.Panel1.Controls.Add(this.dgvSizeRun);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvSizeRunAll);
            this.splitContainer1.Size = new System.Drawing.Size(850, 475);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 5;
            // 
            // dgvSizeRun
            // 
            this.dgvSizeRun.AllowUserToAddRows = false;
            this.dgvSizeRun.AllowUserToDeleteRows = false;
            this.dgvSizeRun.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSizeRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSizeRun.Location = new System.Drawing.Point(0, 0);
            this.dgvSizeRun.Name = "dgvSizeRun";
            this.dgvSizeRun.ReadOnly = true;
            this.dgvSizeRun.RowTemplate.Height = 23;
            this.dgvSizeRun.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSizeRun.Size = new System.Drawing.Size(850, 236);
            this.dgvSizeRun.TabIndex = 0;
            this.dgvSizeRun.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSizeRun_CellMouseDown);
            this.dgvSizeRun.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSizeRun_RowPostPaint);
            // 
            // txtLogs
            // 
            this.txtLogs.Location = new System.Drawing.Point(6, 21);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.Size = new System.Drawing.Size(198, 521);
            this.txtLogs.TabIndex = 1;
            // 
            // ckLogs
            // 
            this.ckLogs.AutoSize = true;
            this.ckLogs.Location = new System.Drawing.Point(803, 0);
            this.ckLogs.Name = "ckLogs";
            this.ckLogs.Size = new System.Drawing.Size(47, 16);
            this.ckLogs.TabIndex = 6;
            this.ckLogs.Text = "Logs";
            this.ckLogs.UseVisualStyleBackColor = true;
            this.ckLogs.CheckedChanged += new System.EventHandler(this.ckLogs_CheckedChanged);
            // 
            // gbSize
            // 
            this.gbSize.Controls.Add(this.splitContainer1);
            this.gbSize.Controls.Add(this.ckLogs);
            this.gbSize.Location = new System.Drawing.Point(4, 67);
            this.gbSize.Name = "gbSize";
            this.gbSize.Size = new System.Drawing.Size(856, 496);
            this.gbSize.TabIndex = 3;
            this.gbSize.TabStop = false;
            this.gbSize.Text = "SizeRun";
            this.gbSize.Resize += new System.EventHandler(this.gbSize_Resize);
            // 
            // gbLogs
            // 
            this.gbLogs.Controls.Add(this.txtLogs);
            this.gbLogs.Location = new System.Drawing.Point(866, 12);
            this.gbLogs.Name = "gbLogs";
            this.gbLogs.Size = new System.Drawing.Size(210, 548);
            this.gbLogs.TabIndex = 7;
            this.gbLogs.TabStop = false;
            this.gbLogs.Text = "LOG";
            this.gbLogs.Resize += new System.EventHandler(this.gbLogs_Resize);
            // 
            // FrmSizeRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 569);
            this.Controls.Add(this.gbLogs);
            this.Controls.Add(this.gbSize);
            this.Controls.Add(this.gbSearch);
            this.Name = "FrmSizeRun";
            this.Text = "尺码表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmSizeRun_Load);
            this.Resize += new System.EventHandler(this.FrmSizeRun_Resize);
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeRunAll)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeRun)).EndInit();
            this.gbSize.ResumeLayout(false);
            this.gbSize.PerformLayout();
            this.gbLogs.ResumeLayout(false);
            this.gbLogs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtMyNumber;
        private System.Windows.Forms.Label labMyNumber;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.DataGridView dgvSizeRunAll;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYYMM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtcust_abbr;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSizeRun;
        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.CheckBox ckLogs;
        private System.Windows.Forms.GroupBox gbSize;
        private System.Windows.Forms.GroupBox gbLogs;
    }
}