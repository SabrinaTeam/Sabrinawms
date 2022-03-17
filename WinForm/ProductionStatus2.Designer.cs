namespace WinForm
{
    partial class ProductionStatus2
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
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.cbOperationsCenter = new System.Windows.Forms.ComboBox();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.butSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSeason = new System.Windows.Forms.TextBox();
            this.txtBuyID = new System.Windows.Forms.TextBox();
            this.dtpStopDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.gbProductionStatus = new System.Windows.Forms.GroupBox();
            this.dgvProductionStatus = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMyNumber = new System.Windows.Forms.TextBox();
            this.gbSeach = new System.Windows.Forms.GroupBox();
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.MenuRight.SuspendLayout();
            this.gbProductionStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionStatus)).BeginInit();
            this.gbSeach.SuspendLayout();
            this.SuspendLayout();
            // 
            // RmeCopyCells
            // 
            this.RmeCopyCells.Name = "RmeCopyCells";
            this.RmeCopyCells.Size = new System.Drawing.Size(140, 22);
            this.RmeCopyCells.Text = "CopyCells";
            this.RmeCopyCells.Click += new System.EventHandler(this.RmeCopyCells_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "营运中心:";
            // 
            // cbOperationsCenter
            // 
            this.cbOperationsCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperationsCenter.FormattingEnabled = true;
            this.cbOperationsCenter.Items.AddRange(new object[] {
            "SAA",
            "TOP"});
            this.cbOperationsCenter.Location = new System.Drawing.Point(7, 31);
            this.cbOperationsCenter.Name = "cbOperationsCenter";
            this.cbOperationsCenter.Size = new System.Drawing.Size(63, 20);
            this.cbOperationsCenter.TabIndex = 22;
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
            // RmeCopyRows
            // 
            this.RmeCopyRows.Name = "RmeCopyRows";
            this.RmeCopyRows.Size = new System.Drawing.Size(140, 22);
            this.RmeCopyRows.Text = "CopyRows";
            this.RmeCopyRows.Click += new System.EventHandler(this.RmeCopyRows_Click);
            // 
            // RmeExportExcel
            // 
            this.RmeExportExcel.Name = "RmeExportExcel";
            this.RmeExportExcel.Size = new System.Drawing.Size(140, 22);
            this.RmeExportExcel.Text = "ExportExcel";
            this.RmeExportExcel.Click += new System.EventHandler(this.RmeExportExcel_Click);
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(811, 15);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(79, 40);
            this.butSearch.TabIndex = 13;
            this.butSearch.Text = "查询";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(682, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(9, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "-";
            // 
            // txtSeason
            // 
            this.txtSeason.Location = new System.Drawing.Point(299, 30);
            this.txtSeason.Name = "txtSeason";
            this.txtSeason.Size = new System.Drawing.Size(92, 22);
            this.txtSeason.TabIndex = 11;
            // 
            // txtBuyID
            // 
            this.txtBuyID.Location = new System.Drawing.Point(397, 31);
            this.txtBuyID.Name = "txtBuyID";
            this.txtBuyID.Size = new System.Drawing.Size(92, 22);
            this.txtBuyID.TabIndex = 10;
            // 
            // dtpStopDate
            // 
            this.dtpStopDate.Location = new System.Drawing.Point(697, 25);
            this.dtpStopDate.Name = "dtpStopDate";
            this.dtpStopDate.Size = new System.Drawing.Size(108, 22);
            this.dtpStopDate.TabIndex = 9;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(568, 26);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(108, 22);
            this.dtpStartDate.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "MES 进度追踪";
            // 
            // gbProductionStatus
            // 
            this.gbProductionStatus.Controls.Add(this.dgvProductionStatus);
            this.gbProductionStatus.Controls.Add(this.label6);
            this.gbProductionStatus.Location = new System.Drawing.Point(4, 65);
            this.gbProductionStatus.Name = "gbProductionStatus";
            this.gbProductionStatus.Size = new System.Drawing.Size(902, 578);
            this.gbProductionStatus.TabIndex = 3;
            this.gbProductionStatus.TabStop = false;
            this.gbProductionStatus.Text = "Production  Status";
            // 
            // dgvProductionStatus
            // 
            this.dgvProductionStatus.AllowUserToAddRows = false;
            this.dgvProductionStatus.AllowUserToDeleteRows = false;
            this.dgvProductionStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductionStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductionStatus.Location = new System.Drawing.Point(3, 18);
            this.dgvProductionStatus.Name = "dgvProductionStatus";
            this.dgvProductionStatus.ReadOnly = true;
            this.dgvProductionStatus.RowTemplate.Height = 24;
            this.dgvProductionStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductionStatus.Size = new System.Drawing.Size(896, 557);
            this.dgvProductionStatus.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "季节:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "月BUY:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "自编单号:";
            // 
            // txtMyNumber
            // 
            this.txtMyNumber.Location = new System.Drawing.Point(88, 29);
            this.txtMyNumber.Name = "txtMyNumber";
            this.txtMyNumber.Size = new System.Drawing.Size(187, 22);
            this.txtMyNumber.TabIndex = 0;
            // 
            // gbSeach
            // 
            this.gbSeach.Controls.Add(this.label5);
            this.gbSeach.Controls.Add(this.cbOperationsCenter);
            this.gbSeach.Controls.Add(this.butSearch);
            this.gbSeach.Controls.Add(this.label4);
            this.gbSeach.Controls.Add(this.txtSeason);
            this.gbSeach.Controls.Add(this.txtBuyID);
            this.gbSeach.Controls.Add(this.dtpStopDate);
            this.gbSeach.Controls.Add(this.dtpStartDate);
            this.gbSeach.Controls.Add(this.label3);
            this.gbSeach.Controls.Add(this.label2);
            this.gbSeach.Controls.Add(this.label1);
            this.gbSeach.Controls.Add(this.txtMyNumber);
            this.gbSeach.Controls.Add(this.cbDate);
            this.gbSeach.Location = new System.Drawing.Point(5, 2);
            this.gbSeach.Name = "gbSeach";
            this.gbSeach.Size = new System.Drawing.Size(902, 58);
            this.gbSeach.TabIndex = 2;
            this.gbSeach.TabStop = false;
            this.gbSeach.Text = "查询条件";
            // 
            // cbDate
            // 
            this.cbDate.AutoSize = true;
            this.cbDate.Location = new System.Drawing.Point(495, 33);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(72, 16);
            this.cbDate.TabIndex = 17;
            this.cbDate.Text = "出货日期";
            this.cbDate.UseVisualStyleBackColor = true;
            // 
            // ProductionStatus2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 646);
            this.Controls.Add(this.gbProductionStatus);
            this.Controls.Add(this.gbSeach);
            this.Name = "ProductionStatus2";
            this.Text = "ProductionStatus2";
            this.MenuRight.ResumeLayout(false);
            this.gbProductionStatus.ResumeLayout(false);
            this.gbProductionStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionStatus)).EndInit();
            this.gbSeach.ResumeLayout(false);
            this.gbSeach.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbOperationsCenter;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSeason;
        private System.Windows.Forms.TextBox txtBuyID;
        private System.Windows.Forms.DateTimePicker dtpStopDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbProductionStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMyNumber;
        private System.Windows.Forms.GroupBox gbSeach;
        private System.Windows.Forms.DataGridView dgvProductionStatus;
        private System.Windows.Forms.CheckBox cbDate;
    }
}