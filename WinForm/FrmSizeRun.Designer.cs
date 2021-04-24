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
            this.btSearch = new System.Windows.Forms.Button();
            this.gbSize = new System.Windows.Forms.GroupBox();
            this.dgvSizeRun = new System.Windows.Forms.DataGridView();
            this.gbPO = new System.Windows.Forms.GroupBox();
            this.dgvSizeRunAll = new System.Windows.Forms.DataGridView();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.gbSearch.SuspendLayout();
            this.gbSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeRun)).BeginInit();
            this.gbPO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeRunAll)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMyNumber
            // 
            this.txtMyNumber.Location = new System.Drawing.Point(69, 17);
            this.txtMyNumber.Name = "txtMyNumber";
            this.txtMyNumber.Size = new System.Drawing.Size(195, 21);
            this.txtMyNumber.TabIndex = 0;
            this.txtMyNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMyNumber_KeyDown);
            // 
            // labMyNumber
            // 
            this.labMyNumber.AutoSize = true;
            this.labMyNumber.Location = new System.Drawing.Point(8, 21);
            this.labMyNumber.Name = "labMyNumber";
            this.labMyNumber.Size = new System.Drawing.Size(53, 12);
            this.labMyNumber.TabIndex = 1;
            this.labMyNumber.Text = "自编单号";
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.label1);
            this.gbSearch.Controls.Add(this.txtStyle);
            this.gbSearch.Controls.Add(this.btSearch);
            this.gbSearch.Controls.Add(this.labMyNumber);
            this.gbSearch.Controls.Add(this.txtMyNumber);
            this.gbSearch.Location = new System.Drawing.Point(1, 6);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(600, 55);
            this.gbSearch.TabIndex = 2;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "查询条件";
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(489, 15);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(91, 23);
            this.btSearch.TabIndex = 4;
            this.btSearch.Text = "查询";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // gbSize
            // 
            this.gbSize.Controls.Add(this.dgvSizeRun);
            this.gbSize.Location = new System.Drawing.Point(4, 67);
            this.gbSize.Name = "gbSize";
            this.gbSize.Size = new System.Drawing.Size(600, 219);
            this.gbSize.TabIndex = 3;
            this.gbSize.TabStop = false;
            this.gbSize.Text = "SizeRun";
            // 
            // dgvSizeRun
            // 
            this.dgvSizeRun.AllowUserToAddRows = false;
            this.dgvSizeRun.AllowUserToDeleteRows = false;
            this.dgvSizeRun.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSizeRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSizeRun.Location = new System.Drawing.Point(3, 17);
            this.dgvSizeRun.Name = "dgvSizeRun";
            this.dgvSizeRun.ReadOnly = true;
            this.dgvSizeRun.RowTemplate.Height = 23;
            this.dgvSizeRun.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSizeRun.Size = new System.Drawing.Size(594, 199);
            this.dgvSizeRun.TabIndex = 0;
            this.dgvSizeRun.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSizeRun_CellMouseDown);
            this.dgvSizeRun.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSizeRun_RowPostPaint);
            // 
            // gbPO
            // 
            this.gbPO.Controls.Add(this.dgvSizeRunAll);
            this.gbPO.Location = new System.Drawing.Point(1, 292);
            this.gbPO.Name = "gbPO";
            this.gbPO.Size = new System.Drawing.Size(600, 237);
            this.gbPO.TabIndex = 4;
            this.gbPO.TabStop = false;
            this.gbPO.Text = "Size PO#";
            // 
            // dgvSizeRunAll
            // 
            this.dgvSizeRunAll.AllowUserToAddRows = false;
            this.dgvSizeRunAll.AllowUserToDeleteRows = false;
            this.dgvSizeRunAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSizeRunAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSizeRunAll.Location = new System.Drawing.Point(3, 17);
            this.dgvSizeRunAll.Name = "dgvSizeRunAll";
            this.dgvSizeRunAll.RowTemplate.Height = 23;
            this.dgvSizeRunAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSizeRunAll.Size = new System.Drawing.Size(594, 217);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "款式号";
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(317, 17);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.Size = new System.Drawing.Size(166, 21);
            this.txtStyle.TabIndex = 5;
            // 
            // FrmSizeRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 532);
            this.Controls.Add(this.gbPO);
            this.Controls.Add(this.gbSize);
            this.Controls.Add(this.gbSearch);
            this.Name = "FrmSizeRun";
            this.Text = "尺码表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmSizeRun_Load);
            this.Resize += new System.EventHandler(this.FrmSizeRun_Resize);
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.gbSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeRun)).EndInit();
            this.gbPO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeRunAll)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtMyNumber;
        private System.Windows.Forms.Label labMyNumber;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.GroupBox gbSize;
        private System.Windows.Forms.DataGridView dgvSizeRun;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.GroupBox gbPO;
        private System.Windows.Forms.DataGridView dgvSizeRunAll;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStyle;
    }
}