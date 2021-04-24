namespace WinForm
{
    partial class FrmTNFScan
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
            this.bgExcel = new System.Windows.Forms.GroupBox();
            this.dgvExcels = new System.Windows.Forms.DataGridView();
            this.gbLoad = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.txtSheetHead = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSelectSheet = new System.Windows.Forms.ComboBox();
            this.txtSelectedFilePath = new System.Windows.Forms.TextBox();
            this.btnSelected = new System.Windows.Forms.Button();
            this.rbOrgTOP = new System.Windows.Forms.RadioButton();
            this.cbSubinv = new System.Windows.Forms.ComboBox();
            this.rbOrgSAA = new System.Windows.Forms.RadioButton();
            this.butDelDoubleRows = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnLoadExcel = new System.Windows.Forms.Button();
            this.bgExcel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcels)).BeginInit();
            this.gbLoad.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgExcel
            // 
            this.bgExcel.Controls.Add(this.dgvExcels);
            this.bgExcel.Location = new System.Drawing.Point(1, 57);
            this.bgExcel.Name = "bgExcel";
            this.bgExcel.Size = new System.Drawing.Size(1162, 643);
            this.bgExcel.TabIndex = 11;
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
            this.dgvExcels.Size = new System.Drawing.Size(1156, 623);
            this.dgvExcels.TabIndex = 0;
            this.dgvExcels.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvExcels_RowPostPaint);
            // 
            // gbLoad
            // 
            this.gbLoad.Controls.Add(this.pgBar);
            this.gbLoad.Controls.Add(this.txtSheetHead);
            this.gbLoad.Controls.Add(this.label2);
            this.gbLoad.Controls.Add(this.label1);
            this.gbLoad.Controls.Add(this.cbSelectSheet);
            this.gbLoad.Controls.Add(this.txtSelectedFilePath);
            this.gbLoad.Controls.Add(this.btnSelected);
            this.gbLoad.Location = new System.Drawing.Point(321, 2);
            this.gbLoad.Name = "gbLoad";
            this.gbLoad.Size = new System.Drawing.Size(629, 55);
            this.gbLoad.TabIndex = 10;
            this.gbLoad.TabStop = false;
            this.gbLoad.Text = "导入条件";
            this.gbLoad.Visible = false;
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(7, 39);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(615, 10);
            this.pgBar.TabIndex = 8;
            this.pgBar.Visible = false;
            // 
            // txtSheetHead
            // 
            this.txtSheetHead.Location = new System.Drawing.Point(578, 13);
            this.txtSheetHead.Name = "txtSheetHead";
            this.txtSheetHead.Size = new System.Drawing.Size(42, 21);
            this.txtSheetHead.TabIndex = 5;
            this.txtSheetHead.Text = "0";
            this.txtSheetHead.TextChanged += new System.EventHandler(this.txtSheetHead_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(518, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "表头行数:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "选择工作表:";
            // 
            // cbSelectSheet
            // 
            this.cbSelectSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectSheet.FormattingEnabled = true;
            this.cbSelectSheet.Location = new System.Drawing.Point(373, 13);
            this.cbSelectSheet.Name = "cbSelectSheet";
            this.cbSelectSheet.Size = new System.Drawing.Size(137, 20);
            this.cbSelectSheet.TabIndex = 2;
            this.cbSelectSheet.SelectedIndexChanged += new System.EventHandler(this.cbSelectSheet_SelectedIndexChanged);
            // 
            // txtSelectedFilePath
            // 
            this.txtSelectedFilePath.Location = new System.Drawing.Point(88, 13);
            this.txtSelectedFilePath.Name = "txtSelectedFilePath";
            this.txtSelectedFilePath.ReadOnly = true;
            this.txtSelectedFilePath.Size = new System.Drawing.Size(206, 21);
            this.txtSelectedFilePath.TabIndex = 1;
            // 
            // btnSelected
            // 
            this.btnSelected.Location = new System.Drawing.Point(7, 12);
            this.btnSelected.Name = "btnSelected";
            this.btnSelected.Size = new System.Drawing.Size(75, 23);
            this.btnSelected.TabIndex = 0;
            this.btnSelected.Text = "浏览...";
            this.btnSelected.UseVisualStyleBackColor = true;
            this.btnSelected.Click += new System.EventHandler(this.btnSelected_Click);
            // 
            // rbOrgTOP
            // 
            this.rbOrgTOP.AutoSize = true;
            this.rbOrgTOP.Location = new System.Drawing.Point(87, 9);
            this.rbOrgTOP.Name = "rbOrgTOP";
            this.rbOrgTOP.Size = new System.Drawing.Size(41, 16);
            this.rbOrgTOP.TabIndex = 13;
            this.rbOrgTOP.TabStop = true;
            this.rbOrgTOP.Text = "TOP";
            this.rbOrgTOP.UseVisualStyleBackColor = true;
            this.rbOrgTOP.CheckedChanged += new System.EventHandler(this.rbOrgTOP_CheckedChanged);
            // 
            // cbSubinv
            // 
            this.cbSubinv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubinv.FormattingEnabled = true;
            this.cbSubinv.Location = new System.Drawing.Point(12, 30);
            this.cbSubinv.Name = "cbSubinv";
            this.cbSubinv.Size = new System.Drawing.Size(121, 20);
            this.cbSubinv.TabIndex = 12;
            this.cbSubinv.SelectedIndexChanged += new System.EventHandler(this.cbSubinv_SelectedIndexChanged);
            // 
            // rbOrgSAA
            // 
            this.rbOrgSAA.AutoSize = true;
            this.rbOrgSAA.Location = new System.Drawing.Point(19, 9);
            this.rbOrgSAA.Name = "rbOrgSAA";
            this.rbOrgSAA.Size = new System.Drawing.Size(41, 16);
            this.rbOrgSAA.TabIndex = 10;
            this.rbOrgSAA.TabStop = true;
            this.rbOrgSAA.Text = "SAA";
            this.rbOrgSAA.UseVisualStyleBackColor = true;
            this.rbOrgSAA.CheckedChanged += new System.EventHandler(this.rbOrgSAA_CheckedChanged);
            // 
            // butDelDoubleRows
            // 
            this.butDelDoubleRows.Location = new System.Drawing.Point(956, 7);
            this.butDelDoubleRows.Name = "butDelDoubleRows";
            this.butDelDoubleRows.Size = new System.Drawing.Size(195, 49);
            this.butDelDoubleRows.TabIndex = 9;
            this.butDelDoubleRows.Text = "删除重复行";
            this.butDelDoubleRows.UseVisualStyleBackColor = true;
            this.butDelDoubleRows.Visible = false;
            this.butDelDoubleRows.Click += new System.EventHandler(this.butDelDoubleRows_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(234, 10);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(81, 41);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.Location = new System.Drawing.Point(147, 10);
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.Size = new System.Drawing.Size(81, 41);
            this.btnLoadExcel.TabIndex = 6;
            this.btnLoadExcel.Text = "加载EXCEL";
            this.btnLoadExcel.UseVisualStyleBackColor = true;
            this.btnLoadExcel.Click += new System.EventHandler(this.btnLoadExcel_Click);
            // 
            // FrmTNFScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 705);
            this.Controls.Add(this.rbOrgTOP);
            this.Controls.Add(this.bgExcel);
            this.Controls.Add(this.gbLoad);
            this.Controls.Add(this.cbSubinv);
            this.Controls.Add(this.rbOrgSAA);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.butDelDoubleRows);
            this.Controls.Add(this.btnLoadExcel);
            this.Name = "FrmTNFScan";
            this.Text = "外箱条码上传";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmTNFScan_Load);
            this.Resize += new System.EventHandler(this.FrmTNFScan_Resize);
            this.bgExcel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcels)).EndInit();
            this.gbLoad.ResumeLayout(false);
            this.gbLoad.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox bgExcel;
        private System.Windows.Forms.DataGridView dgvExcels;
        private System.Windows.Forms.GroupBox gbLoad;
        private System.Windows.Forms.Button butDelDoubleRows;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnLoadExcel;
        private System.Windows.Forms.TextBox txtSheetHead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSelectSheet;
        private System.Windows.Forms.TextBox txtSelectedFilePath;
        private System.Windows.Forms.Button btnSelected;
        private System.Windows.Forms.RadioButton rbOrgTOP;
        private System.Windows.Forms.ComboBox cbSubinv;
        private System.Windows.Forms.RadioButton rbOrgSAA;
    }
}