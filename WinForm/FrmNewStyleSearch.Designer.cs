namespace WinForm
{
    partial class FrmNewStyleSearch
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
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.dgvResultNewStyle = new System.Windows.Forms.DataGridView();
            this.butExport = new System.Windows.Forms.Button();
            this.butClear = new System.Windows.Forms.Button();
            this.butPaste = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.gbSearchPO = new System.Windows.Forms.GroupBox();
            this.dgvSearch = new System.Windows.Forms.DataGridView();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.bgFunctionMenu = new System.Windows.Forms.GroupBox();
            this.txtyymm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultNewStyle)).BeginInit();
            this.gbSearchPO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.bgFunctionMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.dgvResultNewStyle);
            this.gbResult.Location = new System.Drawing.Point(159, 68);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(857, 452);
            this.gbResult.TabIndex = 15;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "查询结果";
            // 
            // dgvResultNewStyle
            // 
            this.dgvResultNewStyle.AllowUserToAddRows = false;
            this.dgvResultNewStyle.AllowUserToDeleteRows = false;
            this.dgvResultNewStyle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultNewStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultNewStyle.Location = new System.Drawing.Point(3, 18);
            this.dgvResultNewStyle.Name = "dgvResultNewStyle";
            this.dgvResultNewStyle.ReadOnly = true;
            this.dgvResultNewStyle.RowTemplate.Height = 23;
            this.dgvResultNewStyle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResultNewStyle.Size = new System.Drawing.Size(851, 431);
            this.dgvResultNewStyle.TabIndex = 0;
            this.dgvResultNewStyle.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvResult_CellMouseDown);
            this.dgvResultNewStyle.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvResult_RowPostPaint);
            this.dgvResultNewStyle.MouseEnter += new System.EventHandler(this.dgvResult_MouseEnter);
            this.dgvResultNewStyle.MouseLeave += new System.EventHandler(this.dgvResult_MouseLeave);
            // 
            // butExport
            // 
            this.butExport.Location = new System.Drawing.Point(491, 18);
            this.butExport.Name = "butExport";
            this.butExport.Size = new System.Drawing.Size(75, 41);
            this.butExport.TabIndex = 14;
            this.butExport.Text = "结果导出Excel";
            this.butExport.UseVisualStyleBackColor = true;
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(203, 18);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(75, 41);
            this.butClear.TabIndex = 13;
            this.butClear.Text = "清空";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.butClear_Click);
            // 
            // butPaste
            // 
            this.butPaste.Location = new System.Drawing.Point(295, 18);
            this.butPaste.Name = "butPaste";
            this.butPaste.Size = new System.Drawing.Size(83, 41);
            this.butPaste.TabIndex = 11;
            this.butPaste.Text = "粘贴";
            this.butPaste.UseVisualStyleBackColor = true;
            this.butPaste.Click += new System.EventHandler(this.butPaste_Click);
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(394, 18);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(75, 41);
            this.butSearch.TabIndex = 10;
            this.butSearch.Text = "查询";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // gbSearchPO
            // 
            this.gbSearchPO.Controls.Add(this.dgvSearch);
            this.gbSearchPO.Location = new System.Drawing.Point(4, 68);
            this.gbSearchPO.Name = "gbSearchPO";
            this.gbSearchPO.Size = new System.Drawing.Size(152, 452);
            this.gbSearchPO.TabIndex = 12;
            this.gbSearchPO.TabStop = false;
            this.gbSearchPO.Text = "查询款号";
            // 
            // dgvSearch
            // 
            this.dgvSearch.AllowUserToAddRows = false;
            this.dgvSearch.AllowUserToDeleteRows = false;
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearch.Location = new System.Drawing.Point(3, 18);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowTemplate.Height = 23;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearch.Size = new System.Drawing.Size(146, 431);
            this.dgvSearch.TabIndex = 0;
            this.dgvSearch.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSearch_CellMouseDown);
            this.dgvSearch.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSearch_RowPostPaint);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            this.dgvSearch.MouseEnter += new System.EventHandler(this.dgvSearch_MouseEnter);
            this.dgvSearch.MouseLeave += new System.EventHandler(this.dgvSearch_MouseLeave);
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
            // bgFunctionMenu
            // 
            this.bgFunctionMenu.Controls.Add(this.txtyymm);
            this.bgFunctionMenu.Controls.Add(this.label1);
            this.bgFunctionMenu.Controls.Add(this.butExport);
            this.bgFunctionMenu.Controls.Add(this.butClear);
            this.bgFunctionMenu.Controls.Add(this.butPaste);
            this.bgFunctionMenu.Controls.Add(this.butSearch);
            this.bgFunctionMenu.Location = new System.Drawing.Point(3, 2);
            this.bgFunctionMenu.Name = "bgFunctionMenu";
            this.bgFunctionMenu.Size = new System.Drawing.Size(1013, 65);
            this.bgFunctionMenu.TabIndex = 16;
            this.bgFunctionMenu.TabStop = false;
            this.bgFunctionMenu.Text = "功能菜单";
            // 
            // txtyymm
            // 
            this.txtyymm.Location = new System.Drawing.Point(22, 33);
            this.txtyymm.Name = "txtyymm";
            this.txtyymm.Size = new System.Drawing.Size(100, 22);
            this.txtyymm.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "yyyymm-x";
            // 
            // FrmNewStyleSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 525);
            this.Controls.Add(this.bgFunctionMenu);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.gbSearchPO);
            this.Name = "FrmNewStyleSearch";
            this.Text = "新款式查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseEnter += new System.EventHandler(this.FrmNewStyleSearch_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.FrmNewStyleSearch_MouseLeave);
            this.Resize += new System.EventHandler(this.FrmNewStyleSearch_Resize);
            this.gbResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultNewStyle)).EndInit();
            this.gbSearchPO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.bgFunctionMenu.ResumeLayout(false);
            this.bgFunctionMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.DataGridView dgvResultNewStyle;
        private System.Windows.Forms.Button butExport;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.Button butPaste;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.GroupBox gbSearchPO;
        private System.Windows.Forms.DataGridView dgvSearch;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.GroupBox bgFunctionMenu;
        private System.Windows.Forms.TextBox txtyymm;
        private System.Windows.Forms.Label label1;
    }
}