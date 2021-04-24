namespace WinForm
{
    partial class FrmPoTradingComanyPO
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
            this.btnSelected = new System.Windows.Forms.Button();
            this.txtSelectedFilePath = new System.Windows.Forms.TextBox();
            this.cbSelectSheet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSheetHead = new System.Windows.Forms.TextBox();
            this.btnLoadExcel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbLoad = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.bgExcel = new System.Windows.Forms.GroupBox();
            this.dgvExcels = new System.Windows.Forms.DataGridView();
            this.butDelDoubleRows = new System.Windows.Forms.Button();
            this.gbLoad.SuspendLayout();
            this.bgExcel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcels)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelected
            // 
            this.btnSelected.Location = new System.Drawing.Point(6, 16);
            this.btnSelected.Name = "btnSelected";
            this.btnSelected.Size = new System.Drawing.Size(75, 23);
            this.btnSelected.TabIndex = 0;
            this.btnSelected.Text = "浏览...";
            this.btnSelected.UseVisualStyleBackColor = true;
            this.btnSelected.Click += new System.EventHandler(this.butSelected_Click);
            // 
            // txtSelectedFilePath
            // 
            this.txtSelectedFilePath.Location = new System.Drawing.Point(87, 17);
            this.txtSelectedFilePath.Name = "txtSelectedFilePath";
            this.txtSelectedFilePath.ReadOnly = true;
            this.txtSelectedFilePath.Size = new System.Drawing.Size(313, 21);
            this.txtSelectedFilePath.TabIndex = 1;
            // 
            // cbSelectSheet
            // 
            this.cbSelectSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectSheet.FormattingEnabled = true;
            this.cbSelectSheet.Location = new System.Drawing.Point(481, 17);
            this.cbSelectSheet.Name = "cbSelectSheet";
            this.cbSelectSheet.Size = new System.Drawing.Size(223, 20);
            this.cbSelectSheet.TabIndex = 2;
            this.cbSelectSheet.SelectedIndexChanged += new System.EventHandler(this.cbSelectSheet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(404, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "选择工作表:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(710, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "表头行数:";
            // 
            // txtSheetHead
            // 
            this.txtSheetHead.Location = new System.Drawing.Point(769, 17);
            this.txtSheetHead.Name = "txtSheetHead";
            this.txtSheetHead.Size = new System.Drawing.Size(100, 21);
            this.txtSheetHead.TabIndex = 5;
            this.txtSheetHead.Text = "3";
            this.txtSheetHead.TextChanged += new System.EventHandler(this.txtSheetHead_TextChanged);
            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.Location = new System.Drawing.Point(899, 14);
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.Size = new System.Drawing.Size(81, 26);
            this.btnLoadExcel.TabIndex = 6;
            this.btnLoadExcel.Text = "加载EXCEL";
            this.btnLoadExcel.UseVisualStyleBackColor = true;
            this.btnLoadExcel.Click += new System.EventHandler(this.butLoadExcel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(989, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 26);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // gbLoad
            // 
            this.gbLoad.Controls.Add(this.butDelDoubleRows);
            this.gbLoad.Controls.Add(this.pgBar);
            this.gbLoad.Controls.Add(this.btnSave);
            this.gbLoad.Controls.Add(this.btnLoadExcel);
            this.gbLoad.Controls.Add(this.txtSheetHead);
            this.gbLoad.Controls.Add(this.label2);
            this.gbLoad.Controls.Add(this.label1);
            this.gbLoad.Controls.Add(this.cbSelectSheet);
            this.gbLoad.Controls.Add(this.txtSelectedFilePath);
            this.gbLoad.Controls.Add(this.btnSelected);
            this.gbLoad.Location = new System.Drawing.Point(5, 2);
            this.gbLoad.Name = "gbLoad";
            this.gbLoad.Size = new System.Drawing.Size(1162, 55);
            this.gbLoad.TabIndex = 8;
            this.gbLoad.TabStop = false;
            this.gbLoad.Text = "导入条件";
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(5, 44);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(1157, 5);
            this.pgBar.TabIndex = 8;
            this.pgBar.Visible = false;
            // 
            // bgExcel
            // 
            this.bgExcel.Controls.Add(this.dgvExcels);
            this.bgExcel.Location = new System.Drawing.Point(0, 62);
            this.bgExcel.Name = "bgExcel";
            this.bgExcel.Size = new System.Drawing.Size(1167, 733);
            this.bgExcel.TabIndex = 9;
            this.bgExcel.TabStop = false;
            this.bgExcel.Text = "Excel内容";
            // 
            // dgvExcels
            // 
            this.dgvExcels.AllowUserToAddRows = false;
            this.dgvExcels.AllowUserToDeleteRows = false;
            this.dgvExcels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvExcels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExcels.Location = new System.Drawing.Point(3, 17);
            this.dgvExcels.Name = "dgvExcels";
            this.dgvExcels.ReadOnly = true;
            this.dgvExcels.RowTemplate.Height = 23;
            this.dgvExcels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExcels.Size = new System.Drawing.Size(1161, 713);
            this.dgvExcels.TabIndex = 0;
            this.dgvExcels.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvExcels_RowPostPaint);
            // 
            // butDelDoubleRows
            // 
            this.butDelDoubleRows.Location = new System.Drawing.Point(1076, 16);
            this.butDelDoubleRows.Name = "butDelDoubleRows";
            this.butDelDoubleRows.Size = new System.Drawing.Size(75, 23);
            this.butDelDoubleRows.TabIndex = 9;
            this.butDelDoubleRows.Text = "删除重复行";
            this.butDelDoubleRows.UseVisualStyleBackColor = true;
            this.butDelDoubleRows.Click += new System.EventHandler(this.butDelDoubleRows_Click);
            // 
            // FrmPoTradingComanyPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 795);
            this.Controls.Add(this.bgExcel);
            this.Controls.Add(this.gbLoad);
            this.Name = "FrmPoTradingComanyPO";
            this.Text = "PO# & Trading Company PO 导入";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.FrmPoTradingComanyPO_Resize);
            this.gbLoad.ResumeLayout(false);
            this.gbLoad.PerformLayout();
            this.bgExcel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelected;
        private System.Windows.Forms.TextBox txtSelectedFilePath;
        private System.Windows.Forms.ComboBox cbSelectSheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSheetHead;
        private System.Windows.Forms.Button btnLoadExcel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbLoad;
        private System.Windows.Forms.GroupBox bgExcel;
        private System.Windows.Forms.DataGridView dgvExcels;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Button butDelDoubleRows;
    }
}