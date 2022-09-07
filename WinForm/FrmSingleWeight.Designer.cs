namespace WinForm
{
    partial class FrmSingleWeight
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
            this.butPackBase = new System.Windows.Forms.Button();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.ButSingleWeight = new System.Windows.Forms.Button();
            this.ButBoxWeight = new System.Windows.Forms.Button();
            this.butRepeal = new System.Windows.Forms.Button();
            this.ButSearch = new System.Windows.Forms.Button();
            this.butSubmit = new System.Windows.Forms.Button();
            this.butCreate = new System.Windows.Forms.Button();
            this.butModify = new System.Windows.Forms.Button();
            this.gbMenu = new System.Windows.Forms.GroupBox();
            this.txtStyle_ID = new System.Windows.Forms.TextBox();
            this.txtCust_ID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbWeight = new System.Windows.Forms.GroupBox();
            this.dgvSingleWeight = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtModify_Weight = new System.Windows.Forms.TextBox();
            this.txtModify_CustID = new System.Windows.Forms.TextBox();
            this.txtModify_StyleID = new System.Windows.Forms.TextBox();
            this.txtModify_SizesID = new System.Windows.Forms.TextBox();
            this.gbModify = new System.Windows.Forms.GroupBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbFunction.SuspendLayout();
            this.gbMenu.SuspendLayout();
            this.gbWeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSingleWeight)).BeginInit();
            this.gbModify.SuspendLayout();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // butPackBase
            // 
            this.butPackBase.Location = new System.Drawing.Point(9, 438);
            this.butPackBase.Name = "butPackBase";
            this.butPackBase.Size = new System.Drawing.Size(85, 50);
            this.butPackBase.TabIndex = 15;
            this.butPackBase.Text = "装箱方式基础数据";
            this.butPackBase.UseVisualStyleBackColor = true;
            this.butPackBase.Click += new System.EventHandler(this.butPackBase_Click);
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
            this.gbFunction.Location = new System.Drawing.Point(562, 3);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(106, 503);
            this.gbFunction.TabIndex = 2;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "功能区";
            // 
            // ButSingleWeight
            // 
            this.ButSingleWeight.Location = new System.Drawing.Point(9, 326);
            this.ButSingleWeight.Name = "ButSingleWeight";
            this.ButSingleWeight.Size = new System.Drawing.Size(85, 50);
            this.ButSingleWeight.TabIndex = 13;
            this.ButSingleWeight.Text = "单件重量表";
            this.ButSingleWeight.UseVisualStyleBackColor = true;
            this.ButSingleWeight.Click += new System.EventHandler(this.ButSingleWeight_Click);
            // 
            // ButBoxWeight
            // 
            this.ButBoxWeight.Location = new System.Drawing.Point(9, 382);
            this.ButBoxWeight.Name = "ButBoxWeight";
            this.ButBoxWeight.Size = new System.Drawing.Size(85, 50);
            this.ButBoxWeight.TabIndex = 14;
            this.ButBoxWeight.Text = "外箱基础数据";
            this.ButBoxWeight.UseVisualStyleBackColor = true;
            this.ButBoxWeight.Click += new System.EventHandler(this.ButBoxWeight_Click);
            // 
            // butRepeal
            // 
            this.butRepeal.Location = new System.Drawing.Point(9, 198);
            this.butRepeal.Name = "butRepeal";
            this.butRepeal.Size = new System.Drawing.Size(85, 34);
            this.butRepeal.TabIndex = 12;
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
            this.butSubmit.Location = new System.Drawing.Point(9, 134);
            this.butSubmit.Name = "butSubmit";
            this.butSubmit.Size = new System.Drawing.Size(85, 34);
            this.butSubmit.TabIndex = 10;
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
            this.butModify.Location = new System.Drawing.Point(9, 98);
            this.butModify.Name = "butModify";
            this.butModify.Size = new System.Drawing.Size(85, 34);
            this.butModify.TabIndex = 11;
            this.butModify.Text = "修改";
            this.butModify.UseVisualStyleBackColor = true;
            this.butModify.Click += new System.EventHandler(this.butModify_Click);
            // 
            // gbMenu
            // 
            this.gbMenu.Controls.Add(this.txtStyle_ID);
            this.gbMenu.Controls.Add(this.txtCust_ID);
            this.gbMenu.Controls.Add(this.label2);
            this.gbMenu.Controls.Add(this.label1);
            this.gbMenu.Location = new System.Drawing.Point(4, 3);
            this.gbMenu.Name = "gbMenu";
            this.gbMenu.Size = new System.Drawing.Size(553, 55);
            this.gbMenu.TabIndex = 3;
            this.gbMenu.TabStop = false;
            this.gbMenu.Text = "菜单区";
            // 
            // txtStyle_ID
            // 
            this.txtStyle_ID.Location = new System.Drawing.Point(237, 20);
            this.txtStyle_ID.Name = "txtStyle_ID";
            this.txtStyle_ID.Size = new System.Drawing.Size(129, 22);
            this.txtStyle_ID.TabIndex = 2;
            // 
            // txtCust_ID
            // 
            this.txtCust_ID.Location = new System.Drawing.Point(50, 20);
            this.txtCust_ID.Name = "txtCust_ID";
            this.txtCust_ID.Size = new System.Drawing.Size(129, 22);
            this.txtCust_ID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Style_ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cust_ID";
            // 
            // gbWeight
            // 
            this.gbWeight.Controls.Add(this.dgvSingleWeight);
            this.gbWeight.Location = new System.Drawing.Point(4, 147);
            this.gbWeight.Name = "gbWeight";
            this.gbWeight.Size = new System.Drawing.Size(553, 362);
            this.gbWeight.TabIndex = 4;
            this.gbWeight.TabStop = false;
            this.gbWeight.Text = "详细数据";
            // 
            // dgvSingleWeight
            // 
            this.dgvSingleWeight.AllowUserToAddRows = false;
            this.dgvSingleWeight.AllowUserToDeleteRows = false;
            this.dgvSingleWeight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSingleWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSingleWeight.Location = new System.Drawing.Point(3, 18);
            this.dgvSingleWeight.MultiSelect = false;
            this.dgvSingleWeight.Name = "dgvSingleWeight";
            this.dgvSingleWeight.RowTemplate.Height = 24;
            this.dgvSingleWeight.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSingleWeight.Size = new System.Drawing.Size(547, 341);
            this.dgvSingleWeight.TabIndex = 10;
            this.dgvSingleWeight.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSingleWeight_CellDoubleClick);
            this.dgvSingleWeight.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSingleWeight_CellMouseDown);
            this.dgvSingleWeight.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSingleWeight_RowPostPaint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "Cust_ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "Style_ID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(282, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "Size";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(403, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "SingleWeight(g)";
            // 
            // txtModify_Weight
            // 
            this.txtModify_Weight.Location = new System.Drawing.Point(490, 17);
            this.txtModify_Weight.Name = "txtModify_Weight";
            this.txtModify_Weight.Size = new System.Drawing.Size(56, 22);
            this.txtModify_Weight.TabIndex = 8;
            // 
            // txtModify_CustID
            // 
            this.txtModify_CustID.Location = new System.Drawing.Point(69, 17);
            this.txtModify_CustID.Name = "txtModify_CustID";
            this.txtModify_CustID.Size = new System.Drawing.Size(77, 22);
            this.txtModify_CustID.TabIndex = 5;
            // 
            // txtModify_StyleID
            // 
            this.txtModify_StyleID.Location = new System.Drawing.Point(199, 17);
            this.txtModify_StyleID.Name = "txtModify_StyleID";
            this.txtModify_StyleID.Size = new System.Drawing.Size(77, 22);
            this.txtModify_StyleID.TabIndex = 6;
            // 
            // txtModify_SizesID
            // 
            this.txtModify_SizesID.Location = new System.Drawing.Point(312, 17);
            this.txtModify_SizesID.Name = "txtModify_SizesID";
            this.txtModify_SizesID.Size = new System.Drawing.Size(77, 22);
            this.txtModify_SizesID.TabIndex = 7;
            // 
            // gbModify
            // 
            this.gbModify.Controls.Add(this.txtNote);
            this.gbModify.Controls.Add(this.label3);
            this.gbModify.Controls.Add(this.txtModify_SizesID);
            this.gbModify.Controls.Add(this.txtModify_StyleID);
            this.gbModify.Controls.Add(this.txtModify_CustID);
            this.gbModify.Controls.Add(this.txtModify_Weight);
            this.gbModify.Controls.Add(this.label11);
            this.gbModify.Controls.Add(this.label10);
            this.gbModify.Controls.Add(this.label5);
            this.gbModify.Controls.Add(this.label4);
            this.gbModify.Location = new System.Drawing.Point(4, 64);
            this.gbModify.Name = "gbModify";
            this.gbModify.Size = new System.Drawing.Size(553, 71);
            this.gbModify.TabIndex = 15;
            this.gbModify.TabStop = false;
            this.gbModify.Text = "单件重量数据维护";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(69, 42);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(478, 22);
            this.txtNote.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "Note";
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(137, 6);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.deleteRowToolStripMenuItem.Text = "DeleteRow";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // FrmSingleWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 512);
            this.Controls.Add(this.gbModify);
            this.Controls.Add(this.gbWeight);
            this.Controls.Add(this.gbMenu);
            this.Controls.Add(this.gbFunction);
            this.Name = "FrmSingleWeight";
            this.Text = "单件重量表";
            this.Load += new System.EventHandler(this.FrmSingleWeight_Load);
            this.Resize += new System.EventHandler(this.FrmSingleWeight_Resize);
            this.gbFunction.ResumeLayout(false);
            this.gbMenu.ResumeLayout(false);
            this.gbMenu.PerformLayout();
            this.gbWeight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSingleWeight)).EndInit();
            this.gbModify.ResumeLayout(false);
            this.gbModify.PerformLayout();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button butPackBase;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.GroupBox gbMenu;
        private System.Windows.Forms.Button ButSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbWeight;
        private System.Windows.Forms.DataGridView dgvSingleWeight;
        private System.Windows.Forms.Button butSubmit;
        private System.Windows.Forms.Button butCreate;
        private System.Windows.Forms.Button butRepeal;
        private System.Windows.Forms.Button butModify;
        private System.Windows.Forms.Button ButSingleWeight;
        private System.Windows.Forms.Button ButBoxWeight;
        private System.Windows.Forms.TextBox txtStyle_ID;
        private System.Windows.Forms.TextBox txtCust_ID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtModify_Weight;
        private System.Windows.Forms.TextBox txtModify_CustID;
        private System.Windows.Forms.TextBox txtModify_StyleID;
        private System.Windows.Forms.TextBox txtModify_SizesID;
        private System.Windows.Forms.GroupBox gbModify;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
    }
}