namespace WinForm
{
    partial class FrmSingleWeightMaintain
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
            this.gbMenu = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.txtCust_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbModify = new System.Windows.Forms.GroupBox();
            this.txtModify_Season = new System.Windows.Forms.TextBox();
            this.labSeason = new System.Windows.Forms.Label();
            this.txtModify_CustID = new System.Windows.Forms.TextBox();
            this.txtModify_BoxHigh = new System.Windows.Forms.TextBox();
            this.txtModify_BoxWidth = new System.Windows.Forms.TextBox();
            this.txtBoxRemark = new System.Windows.Forms.TextBox();
            this.txtModify_BoxLong = new System.Windows.Forms.TextBox();
            this.txtModify_BoxWeight = new System.Windows.Forms.TextBox();
            this.txtModify_BoxName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbMainTain = new System.Windows.Forms.GroupBox();
            this.dgvBoxsBase = new System.Windows.Forms.DataGridView();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.ButSingleWeight = new System.Windows.Forms.Button();
            this.ButBoxWeight = new System.Windows.Forms.Button();
            this.butRepeal = new System.Windows.Forms.Button();
            this.ButSearch = new System.Windows.Forms.Button();
            this.butSubmit = new System.Windows.Forms.Button();
            this.butCreate = new System.Windows.Forms.Button();
            this.butModify = new System.Windows.Forms.Button();
            this.butPackBase = new System.Windows.Forms.Button();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbMenu.SuspendLayout();
            this.gbModify.SuspendLayout();
            this.gbMainTain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxsBase)).BeginInit();
            this.gbFunction.SuspendLayout();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMenu
            // 
            this.gbMenu.Controls.Add(this.label2);
            this.gbMenu.Controls.Add(this.txtBoxName);
            this.gbMenu.Controls.Add(this.txtCust_ID);
            this.gbMenu.Controls.Add(this.label1);
            this.gbMenu.Location = new System.Drawing.Point(6, 2);
            this.gbMenu.Name = "gbMenu";
            this.gbMenu.Size = new System.Drawing.Size(551, 55);
            this.gbMenu.TabIndex = 6;
            this.gbMenu.TabStop = false;
            this.gbMenu.Text = "菜单区";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "box_Name";
            // 
            // txtBoxName
            // 
            this.txtBoxName.Location = new System.Drawing.Point(243, 23);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(129, 22);
            this.txtBoxName.TabIndex = 2;
            // 
            // txtCust_ID
            // 
            this.txtCust_ID.Location = new System.Drawing.Point(49, 21);
            this.txtCust_ID.Name = "txtCust_ID";
            this.txtCust_ID.Size = new System.Drawing.Size(129, 22);
            this.txtCust_ID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cust_ID";
            // 
            // gbModify
            // 
            this.gbModify.Controls.Add(this.txtModify_Season);
            this.gbModify.Controls.Add(this.labSeason);
            this.gbModify.Controls.Add(this.txtModify_CustID);
            this.gbModify.Controls.Add(this.txtModify_BoxHigh);
            this.gbModify.Controls.Add(this.txtModify_BoxWidth);
            this.gbModify.Controls.Add(this.txtBoxRemark);
            this.gbModify.Controls.Add(this.txtModify_BoxLong);
            this.gbModify.Controls.Add(this.txtModify_BoxWeight);
            this.gbModify.Controls.Add(this.txtModify_BoxName);
            this.gbModify.Controls.Add(this.label11);
            this.gbModify.Controls.Add(this.label10);
            this.gbModify.Controls.Add(this.label9);
            this.gbModify.Controls.Add(this.label8);
            this.gbModify.Controls.Add(this.label7);
            this.gbModify.Controls.Add(this.label5);
            this.gbModify.Controls.Add(this.label4);
            this.gbModify.Location = new System.Drawing.Point(6, 60);
            this.gbModify.Name = "gbModify";
            this.gbModify.Size = new System.Drawing.Size(551, 101);
            this.gbModify.TabIndex = 9;
            this.gbModify.TabStop = false;
            this.gbModify.Text = "外箱基础数据维护";
            // 
            // txtModify_Season
            // 
            this.txtModify_Season.Location = new System.Drawing.Point(84, 70);
            this.txtModify_Season.Name = "txtModify_Season";
            this.txtModify_Season.Size = new System.Drawing.Size(77, 22);
            this.txtModify_Season.TabIndex = 11;
            // 
            // labSeason
            // 
            this.labSeason.AutoSize = true;
            this.labSeason.Location = new System.Drawing.Point(14, 75);
            this.labSeason.Name = "labSeason";
            this.labSeason.Size = new System.Drawing.Size(35, 12);
            this.labSeason.TabIndex = 19;
            this.labSeason.Text = "season";
            // 
            // txtModify_CustID
            // 
            this.txtModify_CustID.Location = new System.Drawing.Point(61, 19);
            this.txtModify_CustID.Name = "txtModify_CustID";
            this.txtModify_CustID.Size = new System.Drawing.Size(100, 22);
            this.txtModify_CustID.TabIndex = 5;
            // 
            // txtModify_BoxHigh
            // 
            this.txtModify_BoxHigh.Location = new System.Drawing.Point(440, 45);
            this.txtModify_BoxHigh.Name = "txtModify_BoxHigh";
            this.txtModify_BoxHigh.Size = new System.Drawing.Size(100, 22);
            this.txtModify_BoxHigh.TabIndex = 10;
            // 
            // txtModify_BoxWidth
            // 
            this.txtModify_BoxWidth.Location = new System.Drawing.Point(255, 45);
            this.txtModify_BoxWidth.Name = "txtModify_BoxWidth";
            this.txtModify_BoxWidth.Size = new System.Drawing.Size(100, 22);
            this.txtModify_BoxWidth.TabIndex = 9;
            // 
            // txtBoxRemark
            // 
            this.txtBoxRemark.Location = new System.Drawing.Point(255, 69);
            this.txtBoxRemark.Multiline = true;
            this.txtBoxRemark.Name = "txtBoxRemark";
            this.txtBoxRemark.Size = new System.Drawing.Size(285, 24);
            this.txtBoxRemark.TabIndex = 12;
            // 
            // txtModify_BoxLong
            // 
            this.txtModify_BoxLong.Location = new System.Drawing.Point(84, 45);
            this.txtModify_BoxLong.Name = "txtModify_BoxLong";
            this.txtModify_BoxLong.Size = new System.Drawing.Size(77, 22);
            this.txtModify_BoxLong.TabIndex = 8;
            // 
            // txtModify_BoxWeight
            // 
            this.txtModify_BoxWeight.Location = new System.Drawing.Point(440, 19);
            this.txtModify_BoxWeight.Name = "txtModify_BoxWeight";
            this.txtModify_BoxWeight.Size = new System.Drawing.Size(100, 22);
            this.txtModify_BoxWeight.TabIndex = 7;
            // 
            // txtModify_BoxName
            // 
            this.txtModify_BoxName.Location = new System.Drawing.Point(255, 19);
            this.txtModify_BoxName.Name = "txtModify_BoxName";
            this.txtModify_BoxName.Size = new System.Drawing.Size(100, 22);
            this.txtModify_BoxName.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(207, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "Remark";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(375, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "box_H(mm)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(191, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "Box_W(mm)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "Box_L(mm)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(375, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "BoxWeight";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "box_Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Cust_ID";
            // 
            // gbMainTain
            // 
            this.gbMainTain.Controls.Add(this.dgvBoxsBase);
            this.gbMainTain.Location = new System.Drawing.Point(6, 165);
            this.gbMainTain.Name = "gbMainTain";
            this.gbMainTain.Size = new System.Drawing.Size(551, 337);
            this.gbMainTain.TabIndex = 10;
            this.gbMainTain.TabStop = false;
            this.gbMainTain.Text = "详细数据";
            // 
            // dgvBoxsBase
            // 
            this.dgvBoxsBase.AllowUserToAddRows = false;
            this.dgvBoxsBase.AllowUserToDeleteRows = false;
            this.dgvBoxsBase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoxsBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBoxsBase.Location = new System.Drawing.Point(3, 18);
            this.dgvBoxsBase.Name = "dgvBoxsBase";
            this.dgvBoxsBase.RowTemplate.Height = 24;
            this.dgvBoxsBase.Size = new System.Drawing.Size(545, 316);
            this.dgvBoxsBase.TabIndex = 0;
            this.dgvBoxsBase.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoxsBase_CellDoubleClick);
            this.dgvBoxsBase.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBoxsBase_CellMouseDown);
            this.dgvBoxsBase.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvBoxsBase_RowPostPaint);
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.ButSingleWeight);
            this.gbFunction.Controls.Add(this.ButBoxWeight);
            this.gbFunction.Controls.Add(this.butRepeal);
            this.gbFunction.Controls.Add(this.ButSearch);
            this.gbFunction.Controls.Add(this.butSubmit);
            this.gbFunction.Controls.Add(this.butCreate);
            this.gbFunction.Controls.Add(this.butModify);
            this.gbFunction.Controls.Add(this.butPackBase);
            this.gbFunction.Location = new System.Drawing.Point(562, 2);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(106, 503);
            this.gbFunction.TabIndex = 3;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "功能区";
            // 
            // ButSingleWeight
            // 
            this.ButSingleWeight.Location = new System.Drawing.Point(9, 326);
            this.ButSingleWeight.Name = "ButSingleWeight";
            this.ButSingleWeight.Size = new System.Drawing.Size(85, 50);
            this.ButSingleWeight.TabIndex = 18;
            this.ButSingleWeight.Text = "单件重量表";
            this.ButSingleWeight.UseVisualStyleBackColor = true;
            this.ButSingleWeight.Click += new System.EventHandler(this.ButSingleWeight_Click);
            // 
            // ButBoxWeight
            // 
            this.ButBoxWeight.Location = new System.Drawing.Point(9, 382);
            this.ButBoxWeight.Name = "ButBoxWeight";
            this.ButBoxWeight.Size = new System.Drawing.Size(85, 50);
            this.ButBoxWeight.TabIndex = 17;
            this.ButBoxWeight.Text = "外箱基础数据";
            this.ButBoxWeight.UseVisualStyleBackColor = true;
            this.ButBoxWeight.Click += new System.EventHandler(this.ButBoxWeight_Click);
            // 
            // butRepeal
            // 
            this.butRepeal.Location = new System.Drawing.Point(9, 198);
            this.butRepeal.Name = "butRepeal";
            this.butRepeal.Size = new System.Drawing.Size(85, 34);
            this.butRepeal.TabIndex = 15;
            this.butRepeal.Text = "删除";
            this.butRepeal.UseVisualStyleBackColor = true;
            this.butRepeal.Click += new System.EventHandler(this.butRepeal_Click);
            // 
            // ButSearch
            // 
            this.ButSearch.Location = new System.Drawing.Point(9, 21);
            this.ButSearch.Name = "ButSearch";
            this.ButSearch.Size = new System.Drawing.Size(85, 34);
            this.ButSearch.TabIndex = 3;
            this.ButSearch.Text = "查询";
            this.ButSearch.UseVisualStyleBackColor = true;
            this.ButSearch.Click += new System.EventHandler(this.ButSearch_Click);
            // 
            // butSubmit
            // 
            this.butSubmit.Location = new System.Drawing.Point(9, 133);
            this.butSubmit.Name = "butSubmit";
            this.butSubmit.Size = new System.Drawing.Size(85, 34);
            this.butSubmit.TabIndex = 13;
            this.butSubmit.Text = "保存";
            this.butSubmit.UseVisualStyleBackColor = true;
            this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
            // 
            // butCreate
            // 
            this.butCreate.Location = new System.Drawing.Point(9, 63);
            this.butCreate.Name = "butCreate";
            this.butCreate.Size = new System.Drawing.Size(85, 34);
            this.butCreate.TabIndex = 4;
            this.butCreate.Text = "新增";
            this.butCreate.UseVisualStyleBackColor = true;
            this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
            // 
            // butModify
            // 
            this.butModify.Location = new System.Drawing.Point(9, 96);
            this.butModify.Name = "butModify";
            this.butModify.Size = new System.Drawing.Size(85, 34);
            this.butModify.TabIndex = 13;
            this.butModify.Text = "修改";
            this.butModify.UseVisualStyleBackColor = true;
            // 
            // butPackBase
            // 
            this.butPackBase.Location = new System.Drawing.Point(9, 438);
            this.butPackBase.Name = "butPackBase";
            this.butPackBase.Size = new System.Drawing.Size(85, 50);
            this.butPackBase.TabIndex = 20;
            this.butPackBase.Text = "装箱方式基础数据";
            this.butPackBase.UseVisualStyleBackColor = true;
            this.butPackBase.Click += new System.EventHandler(this.butPackBase_Click);
            // 
            // MenuRight
            // 
            this.MenuRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RmeCopyCells,
            this.RmeCopyRows,
            this.RmeExportExcel,
            this.toolStripMenuItem1,
            this.deleteRowToolStripMenuItem});
            this.MenuRight.Name = "contextMenuStrip1";
            this.MenuRight.Size = new System.Drawing.Size(181, 120);
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteRowToolStripMenuItem.Text = "DeleteRow";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // FrmSingleWeightMaintain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 517);
            this.Controls.Add(this.gbFunction);
            this.Controls.Add(this.gbMainTain);
            this.Controls.Add(this.gbModify);
            this.Controls.Add(this.gbMenu);
            this.Name = "FrmSingleWeightMaintain";
            this.Text = "外箱基础数据";
            this.Load += new System.EventHandler(this.FrmSingleWeightMaintain_Load);
            this.Resize += new System.EventHandler(this.FrmSingleWeightMaintain_Resize);
            this.gbMenu.ResumeLayout(false);
            this.gbMenu.PerformLayout();
            this.gbModify.ResumeLayout(false);
            this.gbModify.PerformLayout();
            this.gbMainTain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxsBase)).EndInit();
            this.gbFunction.ResumeLayout(false);
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbMenu;
        private System.Windows.Forms.GroupBox gbModify;
        private System.Windows.Forms.TextBox txtModify_BoxHigh;
        private System.Windows.Forms.TextBox txtModify_BoxWidth;
        private System.Windows.Forms.TextBox txtBoxRemark;
        private System.Windows.Forms.TextBox txtModify_BoxLong;
        private System.Windows.Forms.TextBox txtModify_BoxWeight;
        private System.Windows.Forms.TextBox txtModify_BoxName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbMainTain;
        private System.Windows.Forms.DataGridView dgvBoxsBase;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.Button ButSingleWeight;
        private System.Windows.Forms.Button ButBoxWeight;
        private System.Windows.Forms.Button butRepeal;
        private System.Windows.Forms.Button ButSearch;
        private System.Windows.Forms.Button butSubmit;
        private System.Windows.Forms.Button butCreate;
        private System.Windows.Forms.Button butModify;
        private System.Windows.Forms.Button butPackBase;
        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.TextBox txtCust_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtModify_CustID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModify_Season;
        private System.Windows.Forms.Label labSeason;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
    }
}