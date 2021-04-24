namespace WinForm
{
    partial class FrmCompletedToMes
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
            this.butStartReceiv = new System.Windows.Forms.Button();
            this.gbSetting = new System.Windows.Forms.GroupBox();
            this.butStopReceiv = new System.Windows.Forms.Button();
            this.cbAutoPrint = new System.Windows.Forms.CheckBox();
            this.labOrgID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbOrg = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labProcessID = new System.Windows.Forms.Label();
            this.cbProcessName = new System.Windows.Forms.ComboBox();
            this.txtReceiptNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.cbCheckScanDate = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.butPrint = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.dtpStopScanDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStarScanDate = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvInvoice = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.msg = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dgvWorkTagScans = new System.Windows.Forms.DataGridView();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gbDetail = new System.Windows.Forms.GroupBox();
            this.gbSetting.SuspendLayout();
            this.gbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkTagScans)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.gbDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // butStartReceiv
            // 
            this.butStartReceiv.Location = new System.Drawing.Point(7, 27);
            this.butStartReceiv.Name = "butStartReceiv";
            this.butStartReceiv.Size = new System.Drawing.Size(75, 27);
            this.butStartReceiv.TabIndex = 1;
            this.butStartReceiv.Text = "开始接收";
            this.butStartReceiv.UseVisualStyleBackColor = true;
            this.butStartReceiv.Click += new System.EventHandler(this.butStartReceiv_Click);
            // 
            // gbSetting
            // 
            this.gbSetting.Controls.Add(this.butStopReceiv);
            this.gbSetting.Controls.Add(this.butStartReceiv);
            this.gbSetting.Controls.Add(this.cbAutoPrint);
            this.gbSetting.Location = new System.Drawing.Point(642, 1);
            this.gbSetting.Name = "gbSetting";
            this.gbSetting.Size = new System.Drawing.Size(179, 58);
            this.gbSetting.TabIndex = 8;
            this.gbSetting.TabStop = false;
            this.gbSetting.Text = "设置";
            // 
            // butStopReceiv
            // 
            this.butStopReceiv.Location = new System.Drawing.Point(90, 27);
            this.butStopReceiv.Name = "butStopReceiv";
            this.butStopReceiv.Size = new System.Drawing.Size(75, 26);
            this.butStopReceiv.TabIndex = 15;
            this.butStopReceiv.Text = "停止接收";
            this.butStopReceiv.UseVisualStyleBackColor = true;
            this.butStopReceiv.Click += new System.EventHandler(this.butStopReceiv_Click);
            // 
            // cbAutoPrint
            // 
            this.cbAutoPrint.AutoCheck = false;
            this.cbAutoPrint.AutoSize = true;
            this.cbAutoPrint.Location = new System.Drawing.Point(8, 13);
            this.cbAutoPrint.Name = "cbAutoPrint";
            this.cbAutoPrint.Size = new System.Drawing.Size(72, 16);
            this.cbAutoPrint.TabIndex = 16;
            this.cbAutoPrint.Text = "自动打印";
            this.cbAutoPrint.UseVisualStyleBackColor = true;
            this.cbAutoPrint.Click += new System.EventHandler(this.cbAutoPrint_Click);
            // 
            // labOrgID
            // 
            this.labOrgID.AutoSize = true;
            this.labOrgID.Location = new System.Drawing.Point(969, 15);
            this.labOrgID.Name = "labOrgID";
            this.labOrgID.Size = new System.Drawing.Size(0, 12);
            this.labOrgID.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(831, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "厂区";
            // 
            // cbOrg
            // 
            this.cbOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrg.FormattingEnabled = true;
            this.cbOrg.Location = new System.Drawing.Point(866, 10);
            this.cbOrg.Name = "cbOrg";
            this.cbOrg.Size = new System.Drawing.Size(95, 20);
            this.cbOrg.TabIndex = 12;
            this.cbOrg.SelectedIndexChanged += new System.EventHandler(this.cbOrg_SelectedIndexChanged);
            this.cbOrg.Click += new System.EventHandler(this.cbOrg_ClickAsync);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(828, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "制程";
            // 
            // labProcessID
            // 
            this.labProcessID.AutoSize = true;
            this.labProcessID.Location = new System.Drawing.Point(969, 40);
            this.labProcessID.Name = "labProcessID";
            this.labProcessID.Size = new System.Drawing.Size(0, 12);
            this.labProcessID.TabIndex = 9;
            // 
            // cbProcessName
            // 
            this.cbProcessName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcessName.FormattingEnabled = true;
            this.cbProcessName.Location = new System.Drawing.Point(866, 36);
            this.cbProcessName.Name = "cbProcessName";
            this.cbProcessName.Size = new System.Drawing.Size(95, 20);
            this.cbProcessName.TabIndex = 4;
            this.cbProcessName.SelectedIndexChanged += new System.EventHandler(this.cbProcessName_SelectedIndexChanged);
            this.cbProcessName.Click += new System.EventHandler(this.cbProcessName_ClickAsync);
            // 
            // txtReceiptNumber
            // 
            this.txtReceiptNumber.Location = new System.Drawing.Point(6, 29);
            this.txtReceiptNumber.Name = "txtReceiptNumber";
            this.txtReceiptNumber.Size = new System.Drawing.Size(196, 21);
            this.txtReceiptNumber.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "送货单号";
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.cbCheckScanDate);
            this.gbSearch.Controls.Add(this.label6);
            this.gbSearch.Controls.Add(this.butPrint);
            this.gbSearch.Controls.Add(this.butSearch);
            this.gbSearch.Controls.Add(this.dtpStopScanDate);
            this.gbSearch.Controls.Add(this.dtpStarScanDate);
            this.gbSearch.Controls.Add(this.label4);
            this.gbSearch.Controls.Add(this.txtReceiptNumber);
            this.gbSearch.Location = new System.Drawing.Point(4, 1);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(632, 58);
            this.gbSearch.TabIndex = 11;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "查询条件";
            // 
            // cbCheckScanDate
            // 
            this.cbCheckScanDate.AutoSize = true;
            this.cbCheckScanDate.Checked = true;
            this.cbCheckScanDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCheckScanDate.Location = new System.Drawing.Point(213, 10);
            this.cbCheckScanDate.Name = "cbCheckScanDate";
            this.cbCheckScanDate.Size = new System.Drawing.Size(72, 16);
            this.cbCheckScanDate.TabIndex = 13;
            this.cbCheckScanDate.Text = "扫描日期";
            this.cbCheckScanDate.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(337, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "-";
            // 
            // butPrint
            // 
            this.butPrint.Location = new System.Drawing.Point(550, 15);
            this.butPrint.Name = "butPrint";
            this.butPrint.Size = new System.Drawing.Size(75, 37);
            this.butPrint.TabIndex = 13;
            this.butPrint.Text = "打印";
            this.butPrint.UseVisualStyleBackColor = true;
            this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(469, 15);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(75, 37);
            this.butSearch.TabIndex = 3;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // dtpStopScanDate
            // 
            this.dtpStopScanDate.Location = new System.Drawing.Point(353, 27);
            this.dtpStopScanDate.Name = "dtpStopScanDate";
            this.dtpStopScanDate.Size = new System.Drawing.Size(103, 21);
            this.dtpStopScanDate.TabIndex = 2;
            // 
            // dtpStarScanDate
            // 
            this.dtpStarScanDate.Location = new System.Drawing.Point(213, 28);
            this.dtpStarScanDate.Name = "dtpStarScanDate";
            this.dtpStarScanDate.Size = new System.Drawing.Size(120, 21);
            this.dtpStarScanDate.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(8, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvInvoice);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.msg);
            this.splitContainer1.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel2.Controls.Add(this.dgvWorkTagScans);
            this.splitContainer1.Size = new System.Drawing.Size(1186, 539);
            this.splitContainer1.SplitterDistance = 393;
            this.splitContainer1.TabIndex = 12;
            // 
            // dgvInvoice
            // 
            this.dgvInvoice.AllowUserToAddRows = false;
            this.dgvInvoice.AllowUserToDeleteRows = false;
            this.dgvInvoice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoice.Location = new System.Drawing.Point(0, 0);
            this.dgvInvoice.MultiSelect = false;
            this.dgvInvoice.Name = "dgvInvoice";
            this.dgvInvoice.ReadOnly = true;
            this.dgvInvoice.RowTemplate.Height = 23;
            this.dgvInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoice.Size = new System.Drawing.Size(393, 539);
            this.dgvInvoice.TabIndex = 6;
            this.dgvInvoice.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoice_CellDoubleClick);
            this.dgvInvoice.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvInvoice_CellMouseDown);
            this.dgvInvoice.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvInvoice_RowPostPaint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(395, 222);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // msg
            // 
            this.msg.AutoSize = true;
            this.msg.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.msg.ForeColor = System.Drawing.Color.Blue;
            this.msg.Location = new System.Drawing.Point(306, 259);
            this.msg.Margin = new System.Windows.Forms.Padding(0);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(46, 24);
            this.msg.TabIndex = 43;
            this.msg.Text = "msg";
            this.msg.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(208, 248);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(372, 43);
            this.progressBar1.TabIndex = 42;
            this.progressBar1.Visible = false;
            // 
            // dgvWorkTagScans
            // 
            this.dgvWorkTagScans.AllowUserToAddRows = false;
            this.dgvWorkTagScans.AllowUserToDeleteRows = false;
            this.dgvWorkTagScans.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvWorkTagScans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkTagScans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWorkTagScans.Location = new System.Drawing.Point(0, 0);
            this.dgvWorkTagScans.Name = "dgvWorkTagScans";
            this.dgvWorkTagScans.ReadOnly = true;
            this.dgvWorkTagScans.RowTemplate.Height = 23;
            this.dgvWorkTagScans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWorkTagScans.Size = new System.Drawing.Size(789, 539);
            this.dgvWorkTagScans.TabIndex = 0;
            this.dgvWorkTagScans.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvWorkTagScans_CellMouseDown);
            this.dgvWorkTagScans.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvWorkTagScans_RowPostPaint);
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
            // gbDetail
            // 
            this.gbDetail.Controls.Add(this.splitContainer1);
            this.gbDetail.Location = new System.Drawing.Point(0, 58);
            this.gbDetail.Name = "gbDetail";
            this.gbDetail.Size = new System.Drawing.Size(1200, 556);
            this.gbDetail.TabIndex = 17;
            this.gbDetail.TabStop = false;
            this.gbDetail.Text = "详细数据";
            // 
            // FrmCompletedToMes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 615);
            this.Controls.Add(this.labOrgID);
            this.Controls.Add(this.gbDetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbOrg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbSearch);
            this.Controls.Add(this.labProcessID);
            this.Controls.Add(this.gbSetting);
            this.Controls.Add(this.cbProcessName);
            this.Name = "FrmCompletedToMes";
            this.Text = "报工送货单打印";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCompletedToMes_FormClosing);
            this.Load += new System.EventHandler(this.FrmCompletedToMes_LoadAsync);
            this.Resize += new System.EventHandler(this.FrmCompletedToMes_Resize);
            this.gbSetting.ResumeLayout(false);
            this.gbSetting.PerformLayout();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkTagScans)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.gbDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button butStartReceiv;
        private System.Windows.Forms.GroupBox gbSetting;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labProcessID;
        private System.Windows.Forms.ComboBox cbProcessName;
        private System.Windows.Forms.TextBox txtReceiptNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.DateTimePicker dtpStopScanDate;
        private System.Windows.Forms.DateTimePicker dtpStarScanDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbOrg;
        private System.Windows.Forms.Label labOrgID;
        private System.Windows.Forms.Button butStopReceiv;
        private System.Windows.Forms.CheckBox cbAutoPrint;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvInvoice;
        private System.Windows.Forms.DataGridView dgvWorkTagScans;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.Button butPrint;
        private System.Windows.Forms.GroupBox gbDetail;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox cbCheckScanDate;
    }
}