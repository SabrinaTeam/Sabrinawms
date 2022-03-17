namespace WinForm
{
    partial class FrmCFoutput
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
            this.dtpStarDate = new System.Windows.Forms.DateTimePicker();
            this.cbSubinv = new System.Windows.Forms.ComboBox();
            this.labSubinv = new System.Windows.Forms.Label();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.butSearch = new System.Windows.Forms.Button();
            this.cbOrg = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labD = new System.Windows.Forms.Label();
            this.dtpStopDate = new System.Windows.Forms.DateTimePicker();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.labStyle = new System.Windows.Forms.Label();
            this.gbCFoutPut = new System.Windows.Forms.GroupBox();
            this.dgvCFoutPut = new System.Windows.Forms.DataGridView();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gbSearch.SuspendLayout();
            this.gbCFoutPut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCFoutPut)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.dtpStarDate);
            this.gbSearch.Controls.Add(this.cbSubinv);
            this.gbSearch.Controls.Add(this.labSubinv);
            this.gbSearch.Controls.Add(this.cbSearchType);
            this.gbSearch.Controls.Add(this.label2);
            this.gbSearch.Controls.Add(this.cbDate);
            this.gbSearch.Controls.Add(this.butSearch);
            this.gbSearch.Controls.Add(this.cbOrg);
            this.gbSearch.Controls.Add(this.label4);
            this.gbSearch.Controls.Add(this.labD);
            this.gbSearch.Controls.Add(this.dtpStopDate);
            this.gbSearch.Controls.Add(this.txtStyle);
            this.gbSearch.Controls.Add(this.labStyle);
            this.gbSearch.Location = new System.Drawing.Point(2, 3);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(981, 55);
            this.gbSearch.TabIndex = 0;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "查询条件";
            // 
            // dtpStarDate
            // 
            this.dtpStarDate.Location = new System.Drawing.Point(655, 20);
            this.dtpStarDate.Name = "dtpStarDate";
            this.dtpStarDate.Size = new System.Drawing.Size(113, 21);
            this.dtpStarDate.TabIndex = 3;
            this.dtpStarDate.VisibleChanged += new System.EventHandler(this.dtpStarDate_VisibleChanged);
            // 
            // cbSubinv
            // 
            this.cbSubinv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubinv.FormattingEnabled = true;
            this.cbSubinv.Location = new System.Drawing.Point(328, 21);
            this.cbSubinv.Name = "cbSubinv";
            this.cbSubinv.Size = new System.Drawing.Size(81, 20);
            this.cbSubinv.TabIndex = 13;
            this.cbSubinv.Click += new System.EventHandler(this.cbSubinv_Click);
            // 
            // labSubinv
            // 
            this.labSubinv.AutoSize = true;
            this.labSubinv.Location = new System.Drawing.Point(296, 25);
            this.labSubinv.Name = "labSubinv";
            this.labSubinv.Size = new System.Drawing.Size(29, 12);
            this.labSubinv.TabIndex = 12;
            this.labSubinv.Text = "仓别";
            // 
            // cbSearchType
            // 
            this.cbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Items.AddRange(new object[] {
            "车缝产量",
            "后道入库",
            "成仓入库"});
            this.cbSearchType.Location = new System.Drawing.Point(65, 20);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(81, 20);
            this.cbSearchType.TabIndex = 11;
            this.cbSearchType.SelectedIndexChanged += new System.EventHandler(this.cbSearchType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "查询类别";
            // 
            // cbDate
            // 
            this.cbDate.AutoSize = true;
            this.cbDate.Location = new System.Drawing.Point(574, 23);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(84, 16);
            this.cbDate.TabIndex = 9;
            this.cbDate.Text = "报工日期：";
            this.cbDate.UseVisualStyleBackColor = true;
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(900, 13);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(74, 34);
            this.butSearch.TabIndex = 8;
            this.butSearch.Text = "查询";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // cbOrg
            // 
            this.cbOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrg.FormattingEnabled = true;
            this.cbOrg.Items.AddRange(new object[] {
            "SAA",
            "TOP"});
            this.cbOrg.Location = new System.Drawing.Point(201, 20);
            this.cbOrg.Name = "cbOrg";
            this.cbOrg.Size = new System.Drawing.Size(81, 20);
            this.cbOrg.TabIndex = 7;
            this.cbOrg.SelectedIndexChanged += new System.EventHandler(this.cbOrg_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "厂别";
            // 
            // labD
            // 
            this.labD.AutoSize = true;
            this.labD.Location = new System.Drawing.Point(773, 24);
            this.labD.Name = "labD";
            this.labD.Size = new System.Drawing.Size(11, 12);
            this.labD.TabIndex = 5;
            this.labD.Text = "-";
            // 
            // dtpStopDate
            // 
            this.dtpStopDate.Location = new System.Drawing.Point(788, 20);
            this.dtpStopDate.Name = "dtpStopDate";
            this.dtpStopDate.Size = new System.Drawing.Size(91, 21);
            this.dtpStopDate.TabIndex = 4;
            this.dtpStopDate.ValueChanged += new System.EventHandler(this.dtpStopDate_ValueChanged);
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(458, 20);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.Size = new System.Drawing.Size(99, 21);
            this.txtStyle.TabIndex = 1;
            // 
            // labStyle
            // 
            this.labStyle.AutoSize = true;
            this.labStyle.Location = new System.Drawing.Point(420, 24);
            this.labStyle.Name = "labStyle";
            this.labStyle.Size = new System.Drawing.Size(41, 12);
            this.labStyle.TabIndex = 0;
            this.labStyle.Text = "款式：";
            // 
            // gbCFoutPut
            // 
            this.gbCFoutPut.Controls.Add(this.dgvCFoutPut);
            this.gbCFoutPut.Location = new System.Drawing.Point(2, 63);
            this.gbCFoutPut.Name = "gbCFoutPut";
            this.gbCFoutPut.Size = new System.Drawing.Size(981, 518);
            this.gbCFoutPut.TabIndex = 1;
            this.gbCFoutPut.TabStop = false;
            this.gbCFoutPut.Text = "详细数据";
            // 
            // dgvCFoutPut
            // 
            this.dgvCFoutPut.AllowUserToAddRows = false;
            this.dgvCFoutPut.AllowUserToDeleteRows = false;
            this.dgvCFoutPut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCFoutPut.Location = new System.Drawing.Point(3, 17);
            this.dgvCFoutPut.Name = "dgvCFoutPut";
            this.dgvCFoutPut.ReadOnly = true;
            this.dgvCFoutPut.RowTemplate.Height = 23;
            this.dgvCFoutPut.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCFoutPut.Size = new System.Drawing.Size(975, 498);
            this.dgvCFoutPut.TabIndex = 0;
            this.dgvCFoutPut.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCFoutPut_CellMouseDown);
            this.dgvCFoutPut.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvCFoutPut_RowPostPaint);
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
            // FrmCFoutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 585);
            this.Controls.Add(this.gbCFoutPut);
            this.Controls.Add(this.gbSearch);
            this.Name = "FrmCFoutput";
            this.Text = "产量报表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCFoutput_Load);
            this.Resize += new System.EventHandler(this.FrmCFoutput_Resize);
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.gbCFoutPut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCFoutPut)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.ComboBox cbOrg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labD;
        private System.Windows.Forms.DateTimePicker dtpStopDate;
        private System.Windows.Forms.DateTimePicker dtpStarDate;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.Label labStyle;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.GroupBox gbCFoutPut;
        private System.Windows.Forms.DataGridView dgvCFoutPut;
        private System.Windows.Forms.CheckBox cbDate;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSubinv;
        private System.Windows.Forms.Label labSubinv;
    }
}