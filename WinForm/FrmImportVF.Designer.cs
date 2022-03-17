namespace WinForm
{
    partial class FrmImportVF
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
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStopDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.butSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.butImport = new System.Windows.Forms.Button();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.cbOnlyAdd = new System.Windows.Forms.CheckBox();
            this.txtPo = new System.Windows.Forms.TextBox();
            this.cbJustPO = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbJustDate = new System.Windows.Forms.CheckBox();
            this.rbCustTNF = new System.Windows.Forms.RadioButton();
            this.rbCustNike = new System.Windows.Forms.RadioButton();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbOrgTOP = new System.Windows.Forms.RadioButton();
            this.rbOrgSAA = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butSearch2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(646, 15);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(102, 22);
            this.dtpStartDate.TabIndex = 0;
            this.dtpStartDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtpStartDate_MouseDown);
            // 
            // dtpStopDate
            // 
            this.dtpStopDate.Location = new System.Drawing.Point(771, 15);
            this.dtpStopDate.Name = "dtpStopDate";
            this.dtpStopDate.Size = new System.Drawing.Size(109, 22);
            this.dtpStopDate.TabIndex = 1;
            this.dtpStopDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtpStopDate_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(754, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(9, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "-";
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(895, 9);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(75, 34);
            this.butSearch.TabIndex = 4;
            this.butSearch.Text = "查询";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(3, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1374, 695);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "详细信息";
            this.groupBox1.Resize += new System.EventHandler(this.groupBox1_Resize);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 18);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1368, 674);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // butImport
            // 
            this.butImport.Location = new System.Drawing.Point(1080, 9);
            this.butImport.Name = "butImport";
            this.butImport.Size = new System.Drawing.Size(146, 34);
            this.butImport.TabIndex = 6;
            this.butImport.Text = "转汇入成品扫描";
            this.butImport.UseVisualStyleBackColor = true;
            this.butImport.Click += new System.EventHandler(this.butImport_Click);
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
            // cbOnlyAdd
            // 
            this.cbOnlyAdd.AutoSize = true;
            this.cbOnlyAdd.Location = new System.Drawing.Point(148, 18);
            this.cbOnlyAdd.Name = "cbOnlyAdd";
            this.cbOnlyAdd.Size = new System.Drawing.Size(84, 16);
            this.cbOnlyAdd.TabIndex = 7;
            this.cbOnlyAdd.Text = "仅新增部分";
            this.cbOnlyAdd.UseVisualStyleBackColor = true;
            this.cbOnlyAdd.Click += new System.EventHandler(this.cbOnlyAdd_Click);
            // 
            // txtPo
            // 
            this.txtPo.Location = new System.Drawing.Point(281, 15);
            this.txtPo.Name = "txtPo";
            this.txtPo.Size = new System.Drawing.Size(100, 22);
            this.txtPo.TabIndex = 8;
            this.txtPo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtPo_MouseDown);
            // 
            // cbJustPO
            // 
            this.cbJustPO.AutoSize = true;
            this.cbJustPO.Location = new System.Drawing.Point(236, 18);
            this.cbJustPO.Name = "cbJustPO";
            this.cbJustPO.Size = new System.Drawing.Size(44, 16);
            this.cbJustPO.TabIndex = 9;
            this.cbJustPO.Text = "PO#";
            this.cbJustPO.UseVisualStyleBackColor = true;
            this.cbJustPO.Click += new System.EventHandler(this.cbJustPO_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1057, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = ">>";
            // 
            // cbJustDate
            // 
            this.cbJustDate.AutoSize = true;
            this.cbJustDate.Location = new System.Drawing.Point(582, 18);
            this.cbJustDate.Name = "cbJustDate";
            this.cbJustDate.Size = new System.Drawing.Size(60, 16);
            this.cbJustDate.TabIndex = 11;
            this.cbJustDate.Text = "时间段";
            this.cbJustDate.UseVisualStyleBackColor = true;
            this.cbJustDate.Click += new System.EventHandler(this.cbJustDate_Click);
            // 
            // rbCustTNF
            // 
            this.rbCustTNF.AutoSize = true;
            this.rbCustTNF.Location = new System.Drawing.Point(3, 3);
            this.rbCustTNF.Name = "rbCustTNF";
            this.rbCustTNF.Size = new System.Drawing.Size(44, 16);
            this.rbCustTNF.TabIndex = 12;
            this.rbCustTNF.TabStop = true;
            this.rbCustTNF.Text = "TNF";
            this.rbCustTNF.UseVisualStyleBackColor = true;
            this.rbCustTNF.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbCustNike
            // 
            this.rbCustNike.AutoSize = true;
            this.rbCustNike.Location = new System.Drawing.Point(2, 21);
            this.rbCustNike.Name = "rbCustNike";
            this.rbCustNike.Size = new System.Drawing.Size(50, 16);
            this.rbCustNike.TabIndex = 13;
            this.rbCustNike.TabStop = true;
            this.rbCustNike.Text = "NIKE";
            this.rbCustNike.UseVisualStyleBackColor = true;
            // 
            // txtLine
            // 
            this.txtLine.Location = new System.Drawing.Point(449, 13);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(100, 22);
            this.txtLine.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(392, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "Main-Line";
            // 
            // rbOrgTOP
            // 
            this.rbOrgTOP.AutoSize = true;
            this.rbOrgTOP.Location = new System.Drawing.Point(6, 21);
            this.rbOrgTOP.Name = "rbOrgTOP";
            this.rbOrgTOP.Size = new System.Drawing.Size(44, 16);
            this.rbOrgTOP.TabIndex = 17;
            this.rbOrgTOP.TabStop = true;
            this.rbOrgTOP.Text = "TOP";
            this.rbOrgTOP.UseVisualStyleBackColor = true;
            // 
            // rbOrgSAA
            // 
            this.rbOrgSAA.AutoSize = true;
            this.rbOrgSAA.Location = new System.Drawing.Point(7, 3);
            this.rbOrgSAA.Name = "rbOrgSAA";
            this.rbOrgSAA.Size = new System.Drawing.Size(45, 16);
            this.rbOrgSAA.TabIndex = 16;
            this.rbOrgSAA.TabStop = true;
            this.rbOrgSAA.Text = "SAA";
            this.rbOrgSAA.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbOrgTOP);
            this.panel1.Controls.Add(this.rbOrgSAA);
            this.panel1.Location = new System.Drawing.Point(68, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(58, 42);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbCustNike);
            this.panel2.Controls.Add(this.rbCustTNF);
            this.panel2.Location = new System.Drawing.Point(4, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(58, 42);
            this.panel2.TabIndex = 2;
            // 
            // butSearch2
            // 
            this.butSearch2.Location = new System.Drawing.Point(976, 9);
            this.butSearch2.Name = "butSearch2";
            this.butSearch2.Size = new System.Drawing.Size(75, 34);
            this.butSearch2.TabIndex = 16;
            this.butSearch2.Text = "查询未导入PO";
            this.butSearch2.UseVisualStyleBackColor = true;
            this.butSearch2.Click += new System.EventHandler(this.butImport2_Click);
            // 
            // FrmImportVF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 741);
            this.Controls.Add(this.butSearch2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.cbJustDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbJustPO);
            this.Controls.Add(this.txtPo);
            this.Controls.Add(this.cbOnlyAdd);
            this.Controls.Add(this.butImport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpStopDate);
            this.Controls.Add(this.dtpStartDate);
            this.Name = "FrmImportVF";
            this.Text = "TNF 资料导入";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.FrmImportVF_Resize);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpStopDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button butImport;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.CheckBox cbOnlyAdd;
        private System.Windows.Forms.TextBox txtPo;
        private System.Windows.Forms.CheckBox cbJustPO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbJustDate;
        private System.Windows.Forms.RadioButton rbCustTNF;
        private System.Windows.Forms.RadioButton rbCustNike;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbOrgTOP;
        private System.Windows.Forms.RadioButton rbOrgSAA;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butSearch2;
    }
}