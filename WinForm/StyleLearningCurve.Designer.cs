namespace WinForm
{
    partial class StyleLearningCurve
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
            this.gbStandardNewModulus = new System.Windows.Forms.GroupBox();
            this.dgvStandardNewModulus = new System.Windows.Forms.DataGridView();
            this.gbStandardOldModulus = new System.Windows.Forms.GroupBox();
            this.dgvStandardOldModulus = new System.Windows.Forms.DataGridView();
            this.gbMemu = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.butLoadLearningCurve = new System.Windows.Forms.Button();
            this.cbCurveNames = new System.Windows.Forms.ComboBox();
            this.butEmpty = new System.Windows.Forms.Button();
            this.butAddLearningCurve = new System.Windows.Forms.Button();
            this.butSaveLearningCurve = new System.Windows.Forms.Button();
            this.butModilyLearningCurve = new System.Windows.Forms.Button();
            this.butProductionCapacity = new System.Windows.Forms.Button();
            this.txtGroupCounts = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbProductionCapacity = new System.Windows.Forms.GroupBox();
            this.cbOldStyle = new System.Windows.Forms.CheckBox();
            this.cbNewStyle = new System.Windows.Forms.CheckBox();
            this.cb10Hour = new System.Windows.Forms.CheckBox();
            this.cb8Hour = new System.Windows.Forms.CheckBox();
            this.butPrints = new System.Windows.Forms.Button();
            this.gbCurveTableName = new System.Windows.Forms.GroupBox();
            this.butCurveCancel = new System.Windows.Forms.Button();
            this.butCurveConfirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurveTableName = new System.Windows.Forms.TextBox();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gbCapacityReport = new System.Windows.Forms.GroupBox();
            this.dgvCapacityReport = new System.Windows.Forms.DataGridView();
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbStandardNewModulus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStandardNewModulus)).BeginInit();
            this.gbStandardOldModulus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStandardOldModulus)).BeginInit();
            this.gbMemu.SuspendLayout();
            this.gbProductionCapacity.SuspendLayout();
            this.gbCurveTableName.SuspendLayout();
            this.MenuRight.SuspendLayout();
            this.gbCapacityReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapacityReport)).BeginInit();
            this.SuspendLayout();
            // 
            // gbStandardNewModulus
            // 
            this.gbStandardNewModulus.Controls.Add(this.dgvStandardNewModulus);
            this.gbStandardNewModulus.Location = new System.Drawing.Point(3, 78);
            this.gbStandardNewModulus.Name = "gbStandardNewModulus";
            this.gbStandardNewModulus.Size = new System.Drawing.Size(1850, 403);
            this.gbStandardNewModulus.TabIndex = 2;
            this.gbStandardNewModulus.TabStop = false;
            this.gbStandardNewModulus.Text = "名称-标准系数-新款";
            // 
            // dgvStandardNewModulus
            // 
            this.dgvStandardNewModulus.AllowUserToAddRows = false;
            this.dgvStandardNewModulus.AllowUserToResizeColumns = false;
            this.dgvStandardNewModulus.AllowUserToResizeRows = false;
            this.dgvStandardNewModulus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStandardNewModulus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStandardNewModulus.Location = new System.Drawing.Point(3, 18);
            this.dgvStandardNewModulus.Name = "dgvStandardNewModulus";
            this.dgvStandardNewModulus.RowTemplate.Height = 24;
            this.dgvStandardNewModulus.Size = new System.Drawing.Size(1844, 382);
            this.dgvStandardNewModulus.TabIndex = 0;
            this.dgvStandardNewModulus.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStandardNewModulus_CellDoubleClick);
            this.dgvStandardNewModulus.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStandardNewModulus_CellEndEdit);
            this.dgvStandardNewModulus.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvStandardNewModulus_CellMouseDown);
            this.dgvStandardNewModulus.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvStandardNewModulus_RowPostPaint);
            this.dgvStandardNewModulus.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStandardNewModulus_RowValidated);
            this.dgvStandardNewModulus.Enter += new System.EventHandler(this.dgvStandardNewModulus_Enter);
            this.dgvStandardNewModulus.Validated += new System.EventHandler(this.dgvStandardNewModulus_Validated);
            // 
            // gbStandardOldModulus
            // 
            this.gbStandardOldModulus.Controls.Add(this.dgvStandardOldModulus);
            this.gbStandardOldModulus.Location = new System.Drawing.Point(3, 487);
            this.gbStandardOldModulus.Name = "gbStandardOldModulus";
            this.gbStandardOldModulus.Size = new System.Drawing.Size(1850, 414);
            this.gbStandardOldModulus.TabIndex = 3;
            this.gbStandardOldModulus.TabStop = false;
            this.gbStandardOldModulus.Text = "名称-标准系数-老款";
            // 
            // dgvStandardOldModulus
            // 
            this.dgvStandardOldModulus.AllowUserToAddRows = false;
            this.dgvStandardOldModulus.AllowUserToResizeColumns = false;
            this.dgvStandardOldModulus.AllowUserToResizeRows = false;
            this.dgvStandardOldModulus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStandardOldModulus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStandardOldModulus.Location = new System.Drawing.Point(3, 18);
            this.dgvStandardOldModulus.Name = "dgvStandardOldModulus";
            this.dgvStandardOldModulus.RowTemplate.Height = 24;
            this.dgvStandardOldModulus.Size = new System.Drawing.Size(1844, 393);
            this.dgvStandardOldModulus.TabIndex = 1;
            this.dgvStandardOldModulus.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStandardOldModulus_CellDoubleClick);
            this.dgvStandardOldModulus.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStandardOldModulus_CellEndEdit);
            this.dgvStandardOldModulus.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvStandardOldModulus_CellMouseDown);
            this.dgvStandardOldModulus.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvStandardOldModulus_RowPostPaint);
            this.dgvStandardOldModulus.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStandardOldModulus_RowValidated);
            this.dgvStandardOldModulus.Enter += new System.EventHandler(this.dgvStandardOldModulus_Enter);
            this.dgvStandardOldModulus.Validated += new System.EventHandler(this.dgvStandardOldModulus_Validated);
            // 
            // gbMemu
            // 
            this.gbMemu.Controls.Add(this.label2);
            this.gbMemu.Controls.Add(this.butLoadLearningCurve);
            this.gbMemu.Controls.Add(this.cbCurveNames);
            this.gbMemu.Controls.Add(this.butEmpty);
            this.gbMemu.Controls.Add(this.butAddLearningCurve);
            this.gbMemu.Controls.Add(this.butSaveLearningCurve);
            this.gbMemu.Controls.Add(this.butModilyLearningCurve);
            this.gbMemu.Location = new System.Drawing.Point(6, 3);
            this.gbMemu.Name = "gbMemu";
            this.gbMemu.Size = new System.Drawing.Size(606, 69);
            this.gbMemu.TabIndex = 5;
            this.gbMemu.TabStop = false;
            this.gbMemu.Text = "系统菜单";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "标准系数名称";
            // 
            // butLoadLearningCurve
            // 
            this.butLoadLearningCurve.Location = new System.Drawing.Point(143, 21);
            this.butLoadLearningCurve.Name = "butLoadLearningCurve";
            this.butLoadLearningCurve.Size = new System.Drawing.Size(76, 39);
            this.butLoadLearningCurve.TabIndex = 14;
            this.butLoadLearningCurve.Text = "加载";
            this.butLoadLearningCurve.UseVisualStyleBackColor = true;
            this.butLoadLearningCurve.Click += new System.EventHandler(this.butLoadLearningCurve_Click);
            // 
            // cbCurveNames
            // 
            this.cbCurveNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurveNames.FormattingEnabled = true;
            this.cbCurveNames.Location = new System.Drawing.Point(14, 37);
            this.cbCurveNames.Name = "cbCurveNames";
            this.cbCurveNames.Size = new System.Drawing.Size(121, 20);
            this.cbCurveNames.TabIndex = 13;
            this.cbCurveNames.Click += new System.EventHandler(this.cbCurveNames_Click);
            // 
            // butEmpty
            // 
            this.butEmpty.Location = new System.Drawing.Point(398, 21);
            this.butEmpty.Name = "butEmpty";
            this.butEmpty.Size = new System.Drawing.Size(76, 39);
            this.butEmpty.TabIndex = 12;
            this.butEmpty.Text = "清空";
            this.butEmpty.UseVisualStyleBackColor = true;
            // 
            // butAddLearningCurve
            // 
            this.butAddLearningCurve.Location = new System.Drawing.Point(240, 21);
            this.butAddLearningCurve.Name = "butAddLearningCurve";
            this.butAddLearningCurve.Size = new System.Drawing.Size(76, 39);
            this.butAddLearningCurve.TabIndex = 11;
            this.butAddLearningCurve.Text = "新增标准系数";
            this.butAddLearningCurve.UseVisualStyleBackColor = true;
            this.butAddLearningCurve.Click += new System.EventHandler(this.butAddLearningCurve_Click);
            // 
            // butSaveLearningCurve
            // 
            this.butSaveLearningCurve.Location = new System.Drawing.Point(524, 21);
            this.butSaveLearningCurve.Name = "butSaveLearningCurve";
            this.butSaveLearningCurve.Size = new System.Drawing.Size(76, 39);
            this.butSaveLearningCurve.TabIndex = 10;
            this.butSaveLearningCurve.Text = "保存";
            this.butSaveLearningCurve.UseVisualStyleBackColor = true;
            this.butSaveLearningCurve.Click += new System.EventHandler(this.butSaveLearningCurve_Click);
            // 
            // butModilyLearningCurve
            // 
            this.butModilyLearningCurve.Location = new System.Drawing.Point(319, 21);
            this.butModilyLearningCurve.Name = "butModilyLearningCurve";
            this.butModilyLearningCurve.Size = new System.Drawing.Size(76, 39);
            this.butModilyLearningCurve.TabIndex = 9;
            this.butModilyLearningCurve.Text = "标准系数修改";
            this.butModilyLearningCurve.UseVisualStyleBackColor = true;
            // 
            // butProductionCapacity
            // 
            this.butProductionCapacity.Location = new System.Drawing.Point(242, 19);
            this.butProductionCapacity.Name = "butProductionCapacity";
            this.butProductionCapacity.Size = new System.Drawing.Size(106, 39);
            this.butProductionCapacity.TabIndex = 4;
            this.butProductionCapacity.Text = "》计算产能";
            this.butProductionCapacity.UseVisualStyleBackColor = true;
            this.butProductionCapacity.Click += new System.EventHandler(this.butProductionCapacity_Click);
            // 
            // txtGroupCounts
            // 
            this.txtGroupCounts.Location = new System.Drawing.Point(166, 36);
            this.txtGroupCounts.Name = "txtGroupCounts";
            this.txtGroupCounts.Size = new System.Drawing.Size(61, 22);
            this.txtGroupCounts.TabIndex = 5;
            this.txtGroupCounts.Text = "43";
            this.txtGroupCounts.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "组人数";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // gbProductionCapacity
            // 
            this.gbProductionCapacity.Controls.Add(this.cbOldStyle);
            this.gbProductionCapacity.Controls.Add(this.cbNewStyle);
            this.gbProductionCapacity.Controls.Add(this.cb10Hour);
            this.gbProductionCapacity.Controls.Add(this.cb8Hour);
            this.gbProductionCapacity.Controls.Add(this.butPrints);
            this.gbProductionCapacity.Controls.Add(this.butProductionCapacity);
            this.gbProductionCapacity.Controls.Add(this.txtGroupCounts);
            this.gbProductionCapacity.Controls.Add(this.label1);
            this.gbProductionCapacity.Location = new System.Drawing.Point(618, 3);
            this.gbProductionCapacity.Name = "gbProductionCapacity";
            this.gbProductionCapacity.Size = new System.Drawing.Size(474, 69);
            this.gbProductionCapacity.TabIndex = 6;
            this.gbProductionCapacity.TabStop = false;
            this.gbProductionCapacity.Text = "产能计算";
            // 
            // cbOldStyle
            // 
            this.cbOldStyle.AutoSize = true;
            this.cbOldStyle.Checked = true;
            this.cbOldStyle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOldStyle.Location = new System.Drawing.Point(16, 42);
            this.cbOldStyle.Name = "cbOldStyle";
            this.cbOldStyle.Size = new System.Drawing.Size(48, 16);
            this.cbOldStyle.TabIndex = 13;
            this.cbOldStyle.Text = "老款";
            this.cbOldStyle.UseVisualStyleBackColor = true;
            // 
            // cbNewStyle
            // 
            this.cbNewStyle.AutoSize = true;
            this.cbNewStyle.Checked = true;
            this.cbNewStyle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNewStyle.Location = new System.Drawing.Point(16, 19);
            this.cbNewStyle.Name = "cbNewStyle";
            this.cbNewStyle.Size = new System.Drawing.Size(48, 16);
            this.cbNewStyle.TabIndex = 12;
            this.cbNewStyle.Text = "新款";
            this.cbNewStyle.UseVisualStyleBackColor = true;
            // 
            // cb10Hour
            // 
            this.cb10Hour.AutoSize = true;
            this.cb10Hour.Checked = true;
            this.cb10Hour.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb10Hour.Location = new System.Drawing.Point(70, 42);
            this.cb10Hour.Name = "cb10Hour";
            this.cb10Hour.Size = new System.Drawing.Size(84, 16);
            this.cb10Hour.TabIndex = 11;
            this.cb10Hour.Text = "10小时产能";
            this.cb10Hour.UseVisualStyleBackColor = true;
            // 
            // cb8Hour
            // 
            this.cb8Hour.AutoSize = true;
            this.cb8Hour.Checked = true;
            this.cb8Hour.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb8Hour.Location = new System.Drawing.Point(70, 19);
            this.cb8Hour.Name = "cb8Hour";
            this.cb8Hour.Size = new System.Drawing.Size(78, 16);
            this.cb8Hour.TabIndex = 10;
            this.cb8Hour.Text = "8小时产能";
            this.cb8Hour.UseVisualStyleBackColor = true;
            // 
            // butPrints
            // 
            this.butPrints.Location = new System.Drawing.Point(375, 18);
            this.butPrints.Name = "butPrints";
            this.butPrints.Size = new System.Drawing.Size(75, 39);
            this.butPrints.TabIndex = 9;
            this.butPrints.Text = "打印";
            this.butPrints.UseVisualStyleBackColor = true;
            // 
            // gbCurveTableName
            // 
            this.gbCurveTableName.Controls.Add(this.butCurveCancel);
            this.gbCurveTableName.Controls.Add(this.butCurveConfirm);
            this.gbCurveTableName.Controls.Add(this.label3);
            this.gbCurveTableName.Controls.Add(this.txtCurveTableName);
            this.gbCurveTableName.Location = new System.Drawing.Point(530, 200);
            this.gbCurveTableName.Name = "gbCurveTableName";
            this.gbCurveTableName.Size = new System.Drawing.Size(346, 138);
            this.gbCurveTableName.TabIndex = 1;
            this.gbCurveTableName.TabStop = false;
            this.gbCurveTableName.Text = "请输入标准系数表名称";
            // 
            // butCurveCancel
            // 
            this.butCurveCancel.Location = new System.Drawing.Point(49, 99);
            this.butCurveCancel.Name = "butCurveCancel";
            this.butCurveCancel.Size = new System.Drawing.Size(75, 23);
            this.butCurveCancel.TabIndex = 3;
            this.butCurveCancel.Text = "取消";
            this.butCurveCancel.UseVisualStyleBackColor = true;
            this.butCurveCancel.Click += new System.EventHandler(this.butCurveCancel_Click);
            // 
            // butCurveConfirm
            // 
            this.butCurveConfirm.Location = new System.Drawing.Point(225, 99);
            this.butCurveConfirm.Name = "butCurveConfirm";
            this.butCurveConfirm.Size = new System.Drawing.Size(75, 23);
            this.butCurveConfirm.TabIndex = 2;
            this.butCurveConfirm.Text = "确认";
            this.butCurveConfirm.UseVisualStyleBackColor = true;
            this.butCurveConfirm.Click += new System.EventHandler(this.butCurveConfirm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "标准系数名称";
            // 
            // txtCurveTableName
            // 
            this.txtCurveTableName.Location = new System.Drawing.Point(49, 61);
            this.txtCurveTableName.Name = "txtCurveTableName";
            this.txtCurveTableName.Size = new System.Drawing.Size(251, 22);
            this.txtCurveTableName.TabIndex = 0;
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
            // gbCapacityReport
            // 
            this.gbCapacityReport.Controls.Add(this.dgvCapacityReport);
            this.gbCapacityReport.Location = new System.Drawing.Point(1098, 13);
            this.gbCapacityReport.Name = "gbCapacityReport";
            this.gbCapacityReport.Size = new System.Drawing.Size(95, 50);
            this.gbCapacityReport.TabIndex = 7;
            this.gbCapacityReport.TabStop = false;
            this.gbCapacityReport.Text = "标准产能";
            // 
            // dgvCapacityReport
            // 
            this.dgvCapacityReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCapacityReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCapacityReport.Location = new System.Drawing.Point(3, 18);
            this.dgvCapacityReport.Name = "dgvCapacityReport";
            this.dgvCapacityReport.RowTemplate.Height = 24;
            this.dgvCapacityReport.Size = new System.Drawing.Size(89, 29);
            this.dgvCapacityReport.TabIndex = 0;
            this.dgvCapacityReport.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCapacityReport_CellMouseDown);
            this.dgvCapacityReport.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvCapacityReport_RowPostPaint);
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
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "区间 (分钟)";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "难易等级(A-P)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "单件时长 (分钟)";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "学系率";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "上线第01天 (%)";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "上线第02天 (%)";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "上线第03天 (%)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "上线第04天 (%)";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "上线第05天 (%)";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "上线第06天 (%)";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "上线第07天 (%)";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "上线第08天 (%)";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "上线第09天 (%)";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "上线第10天 (%)";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "上线第11天 (%)";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "上线第12天 (%)";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "上线第13天 (%)";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.HeaderText = "上线第14天 (%)";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            // 
            // StyleLearningCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1858, 905);
            this.Controls.Add(this.gbCapacityReport);
            this.Controls.Add(this.gbCurveTableName);
            this.Controls.Add(this.gbProductionCapacity);
            this.Controls.Add(this.gbMemu);
            this.Controls.Add(this.gbStandardOldModulus);
            this.Controls.Add(this.gbStandardNewModulus);
            this.Name = "StyleLearningCurve";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "學習曲線表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.StyleLearningCurve_Load);
            this.SizeChanged += new System.EventHandler(this.StyleLearningCurve_SizeChanged);
            this.gbStandardNewModulus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStandardNewModulus)).EndInit();
            this.gbStandardOldModulus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStandardOldModulus)).EndInit();
            this.gbMemu.ResumeLayout(false);
            this.gbMemu.PerformLayout();
            this.gbProductionCapacity.ResumeLayout(false);
            this.gbProductionCapacity.PerformLayout();
            this.gbCurveTableName.ResumeLayout(false);
            this.gbCurveTableName.PerformLayout();
            this.MenuRight.ResumeLayout(false);
            this.gbCapacityReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapacityReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbStandardNewModulus;
        private System.Windows.Forms.DataGridView dgvStandardNewModulus;
        private System.Windows.Forms.GroupBox gbStandardOldModulus;
        private System.Windows.Forms.GroupBox gbMemu;
        private System.Windows.Forms.Button butAddLearningCurve;
        private System.Windows.Forms.Button butSaveLearningCurve;
        private System.Windows.Forms.Button butModilyLearningCurve;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGroupCounts;
        private System.Windows.Forms.Button butProductionCapacity;
        private System.Windows.Forms.GroupBox gbProductionCapacity;
        private System.Windows.Forms.CheckBox cb10Hour;
        private System.Windows.Forms.CheckBox cb8Hour;
        private System.Windows.Forms.Button butPrints;
        private System.Windows.Forms.CheckBox cbOldStyle;
        private System.Windows.Forms.CheckBox cbNewStyle;
        private System.Windows.Forms.Button butEmpty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butLoadLearningCurve;
        private System.Windows.Forms.ComboBox cbCurveNames;
        private System.Windows.Forms.GroupBox gbCurveTableName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurveTableName;
        private System.Windows.Forms.Button butCurveCancel;
        private System.Windows.Forms.Button butCurveConfirm;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridView dgvStandardOldModulus;
        private System.Windows.Forms.GroupBox gbCapacityReport;
        private System.Windows.Forms.DataGridView dgvCapacityReport;
    }
}