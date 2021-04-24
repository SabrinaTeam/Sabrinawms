namespace WinForm
{
    partial class FrmDeliveryCompare
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
            this.bgSearch = new System.Windows.Forms.GroupBox();
            this.cbSelectSheet = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSubinv = new System.Windows.Forms.ComboBox();
            this.labWorking = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.clboxLocation = new System.Windows.Forms.CheckedListBox();
            this.rabutSAA = new System.Windows.Forms.RadioButton();
            this.rabutTOP = new System.Windows.Forms.RadioButton();
            this.btnCompare = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStopTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStarTime = new System.Windows.Forms.DateTimePicker();
            this.txtSelectedFilePath = new System.Windows.Forms.TextBox();
            this.btnSelected = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvExcels = new System.Windows.Forms.DataGridView();
            this.dgvLocalHostDB = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvCompareResult = new System.Windows.Forms.DataGridView();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.bgSearch.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalHostDB)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompareResult)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgSearch
            // 
            this.bgSearch.Controls.Add(this.cbSelectSheet);
            this.bgSearch.Controls.Add(this.label4);
            this.bgSearch.Controls.Add(this.label3);
            this.bgSearch.Controls.Add(this.cbSubinv);
            this.bgSearch.Controls.Add(this.labWorking);
            this.bgSearch.Controls.Add(this.progressBar1);
            this.bgSearch.Controls.Add(this.clboxLocation);
            this.bgSearch.Controls.Add(this.rabutSAA);
            this.bgSearch.Controls.Add(this.rabutTOP);
            this.bgSearch.Controls.Add(this.btnCompare);
            this.bgSearch.Controls.Add(this.label2);
            this.bgSearch.Controls.Add(this.label1);
            this.bgSearch.Controls.Add(this.dtpStopTime);
            this.bgSearch.Controls.Add(this.dtpStarTime);
            this.bgSearch.Controls.Add(this.txtSelectedFilePath);
            this.bgSearch.Controls.Add(this.btnSelected);
            this.bgSearch.Location = new System.Drawing.Point(1, 2);
            this.bgSearch.Name = "bgSearch";
            this.bgSearch.Size = new System.Drawing.Size(1071, 89);
            this.bgSearch.TabIndex = 0;
            this.bgSearch.TabStop = false;
            this.bgSearch.Text = "比较条件";
            // 
            // cbSelectSheet
            // 
            this.cbSelectSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectSheet.FormattingEnabled = true;
            this.cbSelectSheet.Location = new System.Drawing.Point(695, 18);
            this.cbSelectSheet.Name = "cbSelectSheet";
            this.cbSelectSheet.Size = new System.Drawing.Size(227, 20);
            this.cbSelectSheet.TabIndex = 15;
            this.cbSelectSheet.SelectedIndexChanged += new System.EventHandler(this.cbSelectSheet_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "仓库:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "厂区:";
            // 
            // cbSubinv
            // 
            this.cbSubinv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubinv.FormattingEnabled = true;
            this.cbSubinv.Location = new System.Drawing.Point(61, 53);
            this.cbSubinv.Name = "cbSubinv";
            this.cbSubinv.Size = new System.Drawing.Size(135, 20);
            this.cbSubinv.TabIndex = 12;
            this.cbSubinv.SelectedIndexChanged += new System.EventHandler(this.cbSubinv_SelectedIndexChanged);
            // 
            // labWorking
            // 
            this.labWorking.AutoSize = true;
            this.labWorking.Location = new System.Drawing.Point(363, 71);
            this.labWorking.Name = "labWorking";
            this.labWorking.Size = new System.Drawing.Size(65, 12);
            this.labWorking.TabIndex = 11;
            this.labWorking.Text = "任务进度：";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(582, 73);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(485, 10);
            this.progressBar1.TabIndex = 10;
            // 
            // clboxLocation
            // 
            this.clboxLocation.CheckOnClick = true;
            this.clboxLocation.FormattingEnabled = true;
            this.clboxLocation.Location = new System.Drawing.Point(202, 15);
            this.clboxLocation.Name = "clboxLocation";
            this.clboxLocation.Size = new System.Drawing.Size(155, 68);
            this.clboxLocation.TabIndex = 9;
            this.clboxLocation.SelectedIndexChanged += new System.EventHandler(this.clboxLocation_SelectedIndexChanged);
            // 
            // rabutSAA
            // 
            this.rabutSAA.AutoSize = true;
            this.rabutSAA.Location = new System.Drawing.Point(69, 24);
            this.rabutSAA.Name = "rabutSAA";
            this.rabutSAA.Size = new System.Drawing.Size(41, 16);
            this.rabutSAA.TabIndex = 8;
            this.rabutSAA.TabStop = true;
            this.rabutSAA.Text = "SAA";
            this.rabutSAA.UseVisualStyleBackColor = true;
            this.rabutSAA.CheckedChanged += new System.EventHandler(this.rabutSAA_CheckedChanged);
            // 
            // rabutTOP
            // 
            this.rabutTOP.AutoSize = true;
            this.rabutTOP.Location = new System.Drawing.Point(130, 24);
            this.rabutTOP.Name = "rabutTOP";
            this.rabutTOP.Size = new System.Drawing.Size(41, 16);
            this.rabutTOP.TabIndex = 7;
            this.rabutTOP.TabStop = true;
            this.rabutTOP.Text = "TOP";
            this.rabutTOP.UseVisualStyleBackColor = true;
            this.rabutTOP.CheckedChanged += new System.EventHandler(this.rabutTOP_CheckedChanged);
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(944, 15);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(119, 52);
            this.btnCompare.TabIndex = 6;
            this.btnCompare.Text = "比较";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(550, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "至";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(361, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "比较区间";
            // 
            // dtpStopTime
            // 
            this.dtpStopTime.Location = new System.Drawing.Point(582, 45);
            this.dtpStopTime.Name = "dtpStopTime";
            this.dtpStopTime.Size = new System.Drawing.Size(107, 21);
            this.dtpStopTime.TabIndex = 3;
            this.dtpStopTime.ValueChanged += new System.EventHandler(this.dtpStopTime_ValueChanged);
            // 
            // dtpStarTime
            // 
            this.dtpStarTime.Location = new System.Drawing.Point(428, 45);
            this.dtpStarTime.Name = "dtpStarTime";
            this.dtpStarTime.Size = new System.Drawing.Size(111, 21);
            this.dtpStarTime.TabIndex = 2;
            this.dtpStarTime.ValueChanged += new System.EventHandler(this.dtpStarTime_ValueChanged);
            // 
            // txtSelectedFilePath
            // 
            this.txtSelectedFilePath.Location = new System.Drawing.Point(452, 18);
            this.txtSelectedFilePath.Name = "txtSelectedFilePath";
            this.txtSelectedFilePath.ReadOnly = true;
            this.txtSelectedFilePath.Size = new System.Drawing.Size(237, 21);
            this.txtSelectedFilePath.TabIndex = 1;
            // 
            // btnSelected
            // 
            this.btnSelected.Location = new System.Drawing.Point(361, 17);
            this.btnSelected.Name = "btnSelected";
            this.btnSelected.Size = new System.Drawing.Size(77, 23);
            this.btnSelected.TabIndex = 0;
            this.btnSelected.Text = "浏览...";
            this.btnSelected.UseVisualStyleBackColor = true;
            this.btnSelected.Click += new System.EventHandler(this.btnSelected_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 91);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1075, 632);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1067, 606);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "送收货明细";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.dgvExcels);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvLocalHostDB);
            this.splitContainer1.Size = new System.Drawing.Size(1061, 600);
            this.splitContainer1.SplitterDistance = 472;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvExcels
            // 
            this.dgvExcels.AllowUserToAddRows = false;
            this.dgvExcels.AllowUserToDeleteRows = false;
            this.dgvExcels.AllowUserToResizeColumns = false;
            this.dgvExcels.AllowUserToResizeRows = false;
            this.dgvExcels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvExcels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExcels.Location = new System.Drawing.Point(0, 0);
            this.dgvExcels.Name = "dgvExcels";
            this.dgvExcels.ReadOnly = true;
            this.dgvExcels.RowTemplate.Height = 23;
            this.dgvExcels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExcels.Size = new System.Drawing.Size(468, 596);
            this.dgvExcels.TabIndex = 0;
            this.dgvExcels.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvExcels_RowPostPaint);
            // 
            // dgvLocalHostDB
            // 
            this.dgvLocalHostDB.AllowUserToAddRows = false;
            this.dgvLocalHostDB.AllowUserToDeleteRows = false;
            this.dgvLocalHostDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalHostDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLocalHostDB.Location = new System.Drawing.Point(0, 0);
            this.dgvLocalHostDB.Name = "dgvLocalHostDB";
            this.dgvLocalHostDB.ReadOnly = true;
            this.dgvLocalHostDB.RowTemplate.Height = 23;
            this.dgvLocalHostDB.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalHostDB.Size = new System.Drawing.Size(581, 596);
            this.dgvLocalHostDB.TabIndex = 0;
            this.dgvLocalHostDB.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvLocalHostDB_RowPostPaint);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvCompareResult);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1067, 606);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "比对结果";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvCompareResult
            // 
            this.dgvCompareResult.AllowUserToAddRows = false;
            this.dgvCompareResult.AllowUserToDeleteRows = false;
            this.dgvCompareResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCompareResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompareResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompareResult.Location = new System.Drawing.Point(3, 3);
            this.dgvCompareResult.Name = "dgvCompareResult";
            this.dgvCompareResult.ReadOnly = true;
            this.dgvCompareResult.RowTemplate.Height = 23;
            this.dgvCompareResult.Size = new System.Drawing.Size(1061, 600);
            this.dgvCompareResult.TabIndex = 0;
            this.dgvCompareResult.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCompareResult_CellMouseDown);
            this.dgvCompareResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvCompareResult_RowPostPaint);
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
           // this.RmeCopyCells.Image = global::WinForm.Properties.Resources.icons8_复制_64;
            this.RmeCopyCells.Name = "RmeCopyCells";
            this.RmeCopyCells.Size = new System.Drawing.Size(143, 22);
            this.RmeCopyCells.Text = "CopyCells";
            this.RmeCopyCells.Click += new System.EventHandler(this.RmeCopyCells_Click);
            // 
            // RmeCopyRows
            // 
           // this.RmeCopyRows.Image = global::WinForm.Properties.Resources.icons8_复制_48;
            this.RmeCopyRows.Name = "RmeCopyRows";
            this.RmeCopyRows.Size = new System.Drawing.Size(143, 22);
            this.RmeCopyRows.Text = "CopyRows";
            this.RmeCopyRows.Click += new System.EventHandler(this.RmeCopyRows_Click);
            // 
            // RmeExportExcel
            // 
            //this.RmeExportExcel.Image = global::WinForm.Properties.Resources.Excel_32px_1185986_easyicon_net;
            this.RmeExportExcel.Name = "RmeExportExcel";
            this.RmeExportExcel.Size = new System.Drawing.Size(143, 22);
            this.RmeExportExcel.Text = "ExportExcel";
            this.RmeExportExcel.Click += new System.EventHandler(this.RmeExportExcel_Click);
            // 
            // FrmDeliveryCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 722);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.bgSearch);
            this.Name = "FrmDeliveryCompare";
            this.Text = "收货比对";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.FrmDeliveryCompare_Resize);
            this.bgSearch.ResumeLayout(false);
            this.bgSearch.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalHostDB)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompareResult)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox bgSearch;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStopTime;
        private System.Windows.Forms.DateTimePicker dtpStarTime;
        private System.Windows.Forms.TextBox txtSelectedFilePath;
        private System.Windows.Forms.Button btnSelected;
        private System.Windows.Forms.CheckedListBox clboxLocation;
        private System.Windows.Forms.RadioButton rabutSAA;
        private System.Windows.Forms.RadioButton rabutTOP;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labWorking;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSubinv;
        private System.Windows.Forms.ComboBox cbSelectSheet;
        private System.Windows.Forms.DataGridView dgvExcels;
        private System.Windows.Forms.DataGridView dgvLocalHostDB;
        private System.Windows.Forms.DataGridView dgvCompareResult;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
    }
}