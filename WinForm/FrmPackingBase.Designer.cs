namespace WinForm
{
    partial class FrmPackingBase
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
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.ButSingleWeight = new System.Windows.Forms.Button();
            this.ButBoxWeight = new System.Windows.Forms.Button();
            this.butRepeal = new System.Windows.Forms.Button();
            this.ButSearch = new System.Windows.Forms.Button();
            this.butSubmit = new System.Windows.Forms.Button();
            this.butCreate = new System.Windows.Forms.Button();
            this.butModify = new System.Windows.Forms.Button();
            this.butPackBase = new System.Windows.Forms.Button();
            this.gbPackBase = new System.Windows.Forms.GroupBox();
            this.dgvPackBase = new System.Windows.Forms.DataGridView();
            this.txtModify_CustID = new System.Windows.Forms.TextBox();
            this.txtModify_Qtys = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtModify_Size = new System.Windows.Forms.TextBox();
            this.txtModify_StyleID = new System.Windows.Forms.TextBox();
            this.txtModify_BoxName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbModify = new System.Windows.Forms.GroupBox();
            this.txtAntiTheftDeductionWeight = new System.Windows.Forms.TextBox();
            this.txtAccessoriesWeight = new System.Windows.Forms.TextBox();
            this.txtClapBoardWeight = new System.Windows.Forms.TextBox();
            this.txtBagWeight = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.txtCust_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbMenu = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStyle_id = new System.Windows.Forms.TextBox();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbFunction.SuspendLayout();
            this.gbPackBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackBase)).BeginInit();
            this.gbModify.SuspendLayout();
            this.gbMenu.SuspendLayout();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
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
            this.gbFunction.Location = new System.Drawing.Point(632, 8);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(106, 503);
            this.gbFunction.TabIndex = 11;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "功能区";
            // 
            // ButSingleWeight
            // 
            this.ButSingleWeight.Location = new System.Drawing.Point(9, 326);
            this.ButSingleWeight.Name = "ButSingleWeight";
            this.ButSingleWeight.Size = new System.Drawing.Size(85, 50);
            this.ButSingleWeight.TabIndex = 34;
            this.ButSingleWeight.Text = "单件重量表";
            this.ButSingleWeight.UseVisualStyleBackColor = true;
            this.ButSingleWeight.Click += new System.EventHandler(this.ButSingleWeight_Click);
            // 
            // ButBoxWeight
            // 
            this.ButBoxWeight.Location = new System.Drawing.Point(9, 382);
            this.ButBoxWeight.Name = "ButBoxWeight";
            this.ButBoxWeight.Size = new System.Drawing.Size(85, 50);
            this.ButBoxWeight.TabIndex = 35;
            this.ButBoxWeight.Text = "外箱基础数据";
            this.ButBoxWeight.UseVisualStyleBackColor = true;
            this.ButBoxWeight.Click += new System.EventHandler(this.ButBoxWeight_Click);
            // 
            // butRepeal
            // 
            this.butRepeal.Location = new System.Drawing.Point(9, 198);
            this.butRepeal.Name = "butRepeal";
            this.butRepeal.Size = new System.Drawing.Size(85, 34);
            this.butRepeal.TabIndex = 18;
            this.butRepeal.Text = "删除";
            this.butRepeal.UseVisualStyleBackColor = true;
            this.butRepeal.Click += new System.EventHandler(this.butRepeal_Click);
            // 
            // ButSearch
            // 
            this.ButSearch.Location = new System.Drawing.Point(9, 21);
            this.ButSearch.Name = "ButSearch";
            this.ButSearch.Size = new System.Drawing.Size(85, 34);
            this.ButSearch.TabIndex = 4;
            this.ButSearch.Text = "查询";
            this.ButSearch.UseVisualStyleBackColor = true;
            this.ButSearch.Click += new System.EventHandler(this.ButSearch_Click);
            // 
            // butSubmit
            // 
            this.butSubmit.Location = new System.Drawing.Point(9, 133);
            this.butSubmit.Name = "butSubmit";
            this.butSubmit.Size = new System.Drawing.Size(85, 34);
            this.butSubmit.TabIndex = 17;
            this.butSubmit.Text = "保存";
            this.butSubmit.UseVisualStyleBackColor = true;
            this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
            // 
            // butCreate
            // 
            this.butCreate.Location = new System.Drawing.Point(9, 63);
            this.butCreate.Name = "butCreate";
            this.butCreate.Size = new System.Drawing.Size(85, 34);
            this.butCreate.TabIndex = 16;
            this.butCreate.Text = "新增";
            this.butCreate.UseVisualStyleBackColor = true;
            this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
            // 
            // butModify
            // 
            this.butModify.Location = new System.Drawing.Point(9, 96);
            this.butModify.Name = "butModify";
            this.butModify.Size = new System.Drawing.Size(85, 34);
            this.butModify.TabIndex = 31;
            this.butModify.Text = "修改";
            this.butModify.UseVisualStyleBackColor = true;
            // 
            // butPackBase
            // 
            this.butPackBase.Location = new System.Drawing.Point(9, 438);
            this.butPackBase.Name = "butPackBase";
            this.butPackBase.Size = new System.Drawing.Size(85, 50);
            this.butPackBase.TabIndex = 36;
            this.butPackBase.Text = "装箱方式基础数据";
            this.butPackBase.UseVisualStyleBackColor = true;
            this.butPackBase.Click += new System.EventHandler(this.butPackBase_Click);
            // 
            // gbPackBase
            // 
            this.gbPackBase.Controls.Add(this.dgvPackBase);
            this.gbPackBase.Location = new System.Drawing.Point(7, 171);
            this.gbPackBase.Name = "gbPackBase";
            this.gbPackBase.Size = new System.Drawing.Size(619, 340);
            this.gbPackBase.TabIndex = 14;
            this.gbPackBase.TabStop = false;
            this.gbPackBase.Text = "详细数据";
            // 
            // dgvPackBase
            // 
            this.dgvPackBase.AllowUserToAddRows = false;
            this.dgvPackBase.AllowUserToDeleteRows = false;
            this.dgvPackBase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPackBase.Location = new System.Drawing.Point(3, 18);
            this.dgvPackBase.Name = "dgvPackBase";
            this.dgvPackBase.RowTemplate.Height = 24;
            this.dgvPackBase.Size = new System.Drawing.Size(613, 319);
            this.dgvPackBase.TabIndex = 0;
            this.dgvPackBase.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPackBase_CellDoubleClick);
            this.dgvPackBase.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPackBase_CellMouseDown);
            this.dgvPackBase.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvPackBase_RowPostPaint);
            // 
            // txtModify_CustID
            // 
            this.txtModify_CustID.Location = new System.Drawing.Point(57, 13);
            this.txtModify_CustID.Name = "txtModify_CustID";
            this.txtModify_CustID.Size = new System.Drawing.Size(92, 22);
            this.txtModify_CustID.TabIndex = 6;
            this.txtModify_CustID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtModify_CustID.Leave += new System.EventHandler(this.txtModify_CustID_Leave);
            // 
            // txtModify_Qtys
            // 
            this.txtModify_Qtys.Location = new System.Drawing.Point(551, 13);
            this.txtModify_Qtys.Name = "txtModify_Qtys";
            this.txtModify_Qtys.Size = new System.Drawing.Size(48, 22);
            this.txtModify_Qtys.TabIndex = 10;
            this.txtModify_Qtys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(62, 72);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(537, 24);
            this.txtRemark.TabIndex = 15;
            // 
            // txtModify_Size
            // 
            this.txtModify_Size.Location = new System.Drawing.Point(467, 13);
            this.txtModify_Size.Name = "txtModify_Size";
            this.txtModify_Size.Size = new System.Drawing.Size(48, 22);
            this.txtModify_Size.TabIndex = 9;
            this.txtModify_Size.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtModify_StyleID
            // 
            this.txtModify_StyleID.Location = new System.Drawing.Point(193, 13);
            this.txtModify_StyleID.Name = "txtModify_StyleID";
            this.txtModify_StyleID.Size = new System.Drawing.Size(87, 22);
            this.txtModify_StyleID.TabIndex = 7;
            this.txtModify_StyleID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtModify_StyleID.Leave += new System.EventHandler(this.txtModify_StyleID_Leave);
            // 
            // txtModify_BoxName
            // 
            this.txtModify_BoxName.Location = new System.Drawing.Point(345, 13);
            this.txtModify_BoxName.Name = "txtModify_BoxName";
            this.txtModify_BoxName.Size = new System.Drawing.Size(88, 22);
            this.txtModify_BoxName.TabIndex = 8;
            this.txtModify_BoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtModify_BoxName.Leave += new System.EventHandler(this.txtModify_BoxName_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "Remark";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(525, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "qtys";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(441, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "Size";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(151, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "Style_id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Cust_ID";
            // 
            // gbModify
            // 
            this.gbModify.Controls.Add(this.txtAntiTheftDeductionWeight);
            this.gbModify.Controls.Add(this.txtAccessoriesWeight);
            this.gbModify.Controls.Add(this.txtClapBoardWeight);
            this.gbModify.Controls.Add(this.txtBagWeight);
            this.gbModify.Controls.Add(this.label13);
            this.gbModify.Controls.Add(this.label12);
            this.gbModify.Controls.Add(this.label10);
            this.gbModify.Controls.Add(this.label6);
            this.gbModify.Controls.Add(this.txtModify_CustID);
            this.gbModify.Controls.Add(this.txtModify_Qtys);
            this.gbModify.Controls.Add(this.txtRemark);
            this.gbModify.Controls.Add(this.txtModify_Size);
            this.gbModify.Controls.Add(this.txtModify_StyleID);
            this.gbModify.Controls.Add(this.txtModify_BoxName);
            this.gbModify.Controls.Add(this.label11);
            this.gbModify.Controls.Add(this.label9);
            this.gbModify.Controls.Add(this.label8);
            this.gbModify.Controls.Add(this.label7);
            this.gbModify.Controls.Add(this.label5);
            this.gbModify.Controls.Add(this.label4);
            this.gbModify.Location = new System.Drawing.Point(7, 66);
            this.gbModify.Name = "gbModify";
            this.gbModify.Size = new System.Drawing.Size(616, 102);
            this.gbModify.TabIndex = 13;
            this.gbModify.TabStop = false;
            this.gbModify.Text = "外箱基础数据维护";
            // 
            // txtAntiTheftDeductionWeight
            // 
            this.txtAntiTheftDeductionWeight.Location = new System.Drawing.Point(107, 43);
            this.txtAntiTheftDeductionWeight.Name = "txtAntiTheftDeductionWeight";
            this.txtAntiTheftDeductionWeight.Size = new System.Drawing.Size(42, 22);
            this.txtAntiTheftDeductionWeight.TabIndex = 11;
            this.txtAntiTheftDeductionWeight.Text = "0";
            this.txtAntiTheftDeductionWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAccessoriesWeight
            // 
            this.txtAccessoriesWeight.Location = new System.Drawing.Point(546, 43);
            this.txtAccessoriesWeight.Name = "txtAccessoriesWeight";
            this.txtAccessoriesWeight.Size = new System.Drawing.Size(53, 22);
            this.txtAccessoriesWeight.TabIndex = 14;
            this.txtAccessoriesWeight.Text = "0";
            this.txtAccessoriesWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtClapBoardWeight
            // 
            this.txtClapBoardWeight.Location = new System.Drawing.Point(391, 43);
            this.txtClapBoardWeight.Name = "txtClapBoardWeight";
            this.txtClapBoardWeight.Size = new System.Drawing.Size(42, 22);
            this.txtClapBoardWeight.TabIndex = 13;
            this.txtClapBoardWeight.Text = "0";
            this.txtClapBoardWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBagWeight
            // 
            this.txtBagWeight.Location = new System.Drawing.Point(238, 43);
            this.txtBagWeight.Name = "txtBagWeight";
            this.txtBagWeight.Size = new System.Drawing.Size(42, 22);
            this.txtBagWeight.TabIndex = 12;
            this.txtBagWeight.Text = "0";
            this.txtBagWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(440, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 12);
            this.label13.TabIndex = 27;
            this.label13.Text = "其它配件总重量(g)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(159, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "单个袋子重(g)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(300, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "箱隔板总重量(g)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(14, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "单个防盗扣重(g)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(287, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "box_Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "box_Name";
            // 
            // txtBoxName
            // 
            this.txtBoxName.Location = new System.Drawing.Point(437, 21);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(106, 22);
            this.txtBoxName.TabIndex = 3;
            this.txtBoxName.Leave += new System.EventHandler(this.txtBoxName_Leave);
            // 
            // txtCust_ID
            // 
            this.txtCust_ID.Location = new System.Drawing.Point(49, 21);
            this.txtCust_ID.Name = "txtCust_ID";
            this.txtCust_ID.Size = new System.Drawing.Size(129, 22);
            this.txtCust_ID.TabIndex = 1;
            this.txtCust_ID.Leave += new System.EventHandler(this.txtCust_ID_Leave);
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
            // gbMenu
            // 
            this.gbMenu.Controls.Add(this.label3);
            this.gbMenu.Controls.Add(this.txtStyle_id);
            this.gbMenu.Controls.Add(this.label2);
            this.gbMenu.Controls.Add(this.txtBoxName);
            this.gbMenu.Controls.Add(this.txtCust_ID);
            this.gbMenu.Controls.Add(this.label1);
            this.gbMenu.Location = new System.Drawing.Point(7, 8);
            this.gbMenu.Name = "gbMenu";
            this.gbMenu.Size = new System.Drawing.Size(619, 55);
            this.gbMenu.TabIndex = 12;
            this.gbMenu.TabStop = false;
            this.gbMenu.Text = "菜单区";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Style_id";
            // 
            // txtStyle_id
            // 
            this.txtStyle_id.Location = new System.Drawing.Point(258, 21);
            this.txtStyle_id.Name = "txtStyle_id";
            this.txtStyle_id.Size = new System.Drawing.Size(85, 22);
            this.txtStyle_id.TabIndex = 2;
            this.txtStyle_id.Leave += new System.EventHandler(this.txtStyle_id_Leave);
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
            this.MenuRight.Size = new System.Drawing.Size(141, 98);
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
            // FrmPackingBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 517);
            this.Controls.Add(this.gbFunction);
            this.Controls.Add(this.gbPackBase);
            this.Controls.Add(this.gbModify);
            this.Controls.Add(this.gbMenu);
            this.Name = "FrmPackingBase";
            this.Text = "装箱方式基础数据";
            this.Load += new System.EventHandler(this.FrmPackingBase_Load);
            this.Resize += new System.EventHandler(this.FrmPackingBase_Resize);
            this.gbFunction.ResumeLayout(false);
            this.gbPackBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackBase)).EndInit();
            this.gbModify.ResumeLayout(false);
            this.gbModify.PerformLayout();
            this.gbMenu.ResumeLayout(false);
            this.gbMenu.PerformLayout();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.Button ButSingleWeight;
        private System.Windows.Forms.Button ButBoxWeight;
        private System.Windows.Forms.Button butRepeal;
        private System.Windows.Forms.Button ButSearch;
        private System.Windows.Forms.Button butSubmit;
        private System.Windows.Forms.Button butCreate;
        private System.Windows.Forms.Button butModify;
        private System.Windows.Forms.Button butPackBase;
        private System.Windows.Forms.GroupBox gbPackBase;
        private System.Windows.Forms.DataGridView dgvPackBase;
        private System.Windows.Forms.TextBox txtModify_CustID;
        private System.Windows.Forms.TextBox txtModify_Qtys;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtModify_Size;
        private System.Windows.Forms.TextBox txtModify_StyleID;
        private System.Windows.Forms.TextBox txtModify_BoxName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbModify;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.TextBox txtCust_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbMenu;
        private System.Windows.Forms.TextBox txtAntiTheftDeductionWeight;
        private System.Windows.Forms.TextBox txtAccessoriesWeight;
        private System.Windows.Forms.TextBox txtClapBoardWeight;
        private System.Windows.Forms.TextBox txtBagWeight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStyle_id;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
    }
}