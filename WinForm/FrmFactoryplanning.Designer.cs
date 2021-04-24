namespace WinForm
{
    partial class FrmFactoryplanning
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
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.cbIsDel = new System.Windows.Forms.CheckBox();
            this.rdbutTOP = new System.Windows.Forms.RadioButton();
            this.rdbutSAA = new System.Windows.Forms.RadioButton();
            this.btSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPropertyNumber = new System.Windows.Forms.TextBox();
            this.bgProperty = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbORG = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpPropertyBuyDate = new System.Windows.Forms.DateTimePicker();
            this.butGeneratePropertyNumber = new System.Windows.Forms.Button();
            this.txtPropertySavePerson = new System.Windows.Forms.TextBox();
            this.txtPropertyDept = new System.Windows.Forms.TextBox();
            this.txtPropertyBuyID = new System.Windows.Forms.TextBox();
            this.txtPropertyLocal = new System.Windows.Forms.TextBox();
            this.txtPropertyFirstNumber = new System.Windows.Forms.TextBox();
            this.txtPropertyCounts = new System.Windows.Forms.TextBox();
            this.txtPropertyType = new System.Windows.Forms.TextBox();
            this.txtUnid = new System.Windows.Forms.TextBox();
            this.txtPropertyMode = new System.Windows.Forms.TextBox();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butDeleteSelected = new System.Windows.Forms.Button();
            this.gblist = new System.Windows.Forms.GroupBox();
            this.btnBarcodeReader = new System.Windows.Forms.Button();
            this.btnCreateQuickMark = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCreateBarCode = new System.Windows.Forms.Button();
            this.gbDelNote = new System.Windows.Forms.GroupBox();
            this.butDelCancel = new System.Windows.Forms.Button();
            this.butDelNote = new System.Windows.Forms.Button();
            this.txtDelNote = new System.Windows.Forms.TextBox();
            this.cbSelected = new System.Windows.Forms.CheckBox();
            this.dgvPropertys = new System.Windows.Forms.DataGridView();
            this.butSave = new System.Windows.Forms.Button();
            this.butPrints = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.msg = new System.Windows.Forms.Label();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gbSearch.SuspendLayout();
            this.bgProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gblist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbDelNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertys)).BeginInit();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.cbIsDel);
            this.gbSearch.Controls.Add(this.rdbutTOP);
            this.gbSearch.Controls.Add(this.rdbutSAA);
            this.gbSearch.Controls.Add(this.btSearch);
            this.gbSearch.Controls.Add(this.label1);
            this.gbSearch.Controls.Add(this.txtPropertyNumber);
            this.gbSearch.Location = new System.Drawing.Point(3, 1);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(599, 53);
            this.gbSearch.TabIndex = 1;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "查询条件";
            // 
            // cbIsDel
            // 
            this.cbIsDel.AutoSize = true;
            this.cbIsDel.ForeColor = System.Drawing.Color.Red;
            this.cbIsDel.Location = new System.Drawing.Point(162, 12);
            this.cbIsDel.Name = "cbIsDel";
            this.cbIsDel.Size = new System.Drawing.Size(60, 16);
            this.cbIsDel.TabIndex = 2;
            this.cbIsDel.Text = "已报废";
            this.cbIsDel.UseVisualStyleBackColor = true;
            // 
            // rdbutTOP
            // 
            this.rdbutTOP.AutoSize = true;
            this.rdbutTOP.Location = new System.Drawing.Point(92, 24);
            this.rdbutTOP.Name = "rdbutTOP";
            this.rdbutTOP.Size = new System.Drawing.Size(41, 16);
            this.rdbutTOP.TabIndex = 1;
            this.rdbutTOP.Text = "TOP";
            this.rdbutTOP.UseVisualStyleBackColor = true;
            this.rdbutTOP.CheckedChanged += new System.EventHandler(this.rdbutTOP_CheckedChanged);
            // 
            // rdbutSAA
            // 
            this.rdbutSAA.AutoSize = true;
            this.rdbutSAA.Checked = true;
            this.rdbutSAA.Location = new System.Drawing.Point(10, 24);
            this.rdbutSAA.Name = "rdbutSAA";
            this.rdbutSAA.Size = new System.Drawing.Size(41, 16);
            this.rdbutSAA.TabIndex = 0;
            this.rdbutSAA.TabStop = true;
            this.rdbutSAA.Text = "SAA";
            this.rdbutSAA.UseVisualStyleBackColor = true;
            this.rdbutSAA.CheckedChanged += new System.EventHandler(this.rdbutSAA_CheckedChanged);
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(516, 15);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 34);
            this.btSearch.TabIndex = 4;
            this.btSearch.Text = "查询";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "财产编号：";
            // 
            // txtPropertyNumber
            // 
            this.txtPropertyNumber.Location = new System.Drawing.Point(229, 27);
            this.txtPropertyNumber.Name = "txtPropertyNumber";
            this.txtPropertyNumber.Size = new System.Drawing.Size(281, 21);
            this.txtPropertyNumber.TabIndex = 3;
            this.txtPropertyNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPropertyNumber_KeyDown);
            // 
            // bgProperty
            // 
            this.bgProperty.Controls.Add(this.splitContainer1);
            this.bgProperty.Location = new System.Drawing.Point(2, 56);
            this.bgProperty.Name = "bgProperty";
            this.bgProperty.Size = new System.Drawing.Size(1045, 647);
            this.bgProperty.TabIndex = 5;
            this.bgProperty.TabStop = false;
            this.bgProperty.Text = "固资清单";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbORG);
            this.splitContainer1.Panel1.Controls.Add(this.label13);
            this.splitContainer1.Panel1.Controls.Add(this.dtpPropertyBuyDate);
            this.splitContainer1.Panel1.Controls.Add(this.butGeneratePropertyNumber);
            this.splitContainer1.Panel1.Controls.Add(this.txtPropertySavePerson);
            this.splitContainer1.Panel1.Controls.Add(this.txtPropertyDept);
            this.splitContainer1.Panel1.Controls.Add(this.txtPropertyBuyID);
            this.splitContainer1.Panel1.Controls.Add(this.txtPropertyLocal);
            this.splitContainer1.Panel1.Controls.Add(this.txtPropertyFirstNumber);
            this.splitContainer1.Panel1.Controls.Add(this.txtPropertyCounts);
            this.splitContainer1.Panel1.Controls.Add(this.txtPropertyType);
            this.splitContainer1.Panel1.Controls.Add(this.txtUnid);
            this.splitContainer1.Panel1.Controls.Add(this.txtPropertyMode);
            this.splitContainer1.Panel1.Controls.Add(this.txtPropertyName);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.butDeleteSelected);
            this.splitContainer1.Panel2.Controls.Add(this.gblist);
            this.splitContainer1.Size = new System.Drawing.Size(1039, 627);
            this.splitContainer1.SplitterDistance = 73;
            this.splitContainer1.TabIndex = 5;
            // 
            // cbORG
            // 
            this.cbORG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbORG.FormattingEnabled = true;
            this.cbORG.Items.AddRange(new object[] {
            "SAA",
            "TOP"});
            this.cbORG.Location = new System.Drawing.Point(61, 9);
            this.cbORG.Name = "cbORG";
            this.cbORG.Size = new System.Drawing.Size(90, 20);
            this.cbORG.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(6, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 18;
            this.label13.Text = "财产归属";
            // 
            // dtpPropertyBuyDate
            // 
            this.dtpPropertyBuyDate.Location = new System.Drawing.Point(843, 42);
            this.dtpPropertyBuyDate.Name = "dtpPropertyBuyDate";
            this.dtpPropertyBuyDate.Size = new System.Drawing.Size(90, 21);
            this.dtpPropertyBuyDate.TabIndex = 11;
            // 
            // butGeneratePropertyNumber
            // 
            this.butGeneratePropertyNumber.Location = new System.Drawing.Point(951, 8);
            this.butGeneratePropertyNumber.Name = "butGeneratePropertyNumber";
            this.butGeneratePropertyNumber.Size = new System.Drawing.Size(79, 60);
            this.butGeneratePropertyNumber.TabIndex = 17;
            this.butGeneratePropertyNumber.Text = "生成资产清单";
            this.butGeneratePropertyNumber.UseVisualStyleBackColor = true;
            this.butGeneratePropertyNumber.Click += new System.EventHandler(this.butGeneratePropertyNumber_Click);
            // 
            // txtPropertySavePerson
            // 
            this.txtPropertySavePerson.Location = new System.Drawing.Point(548, 42);
            this.txtPropertySavePerson.Name = "txtPropertySavePerson";
            this.txtPropertySavePerson.Size = new System.Drawing.Size(85, 21);
            this.txtPropertySavePerson.TabIndex = 15;
            // 
            // txtPropertyDept
            // 
            this.txtPropertyDept.Location = new System.Drawing.Point(61, 42);
            this.txtPropertyDept.Name = "txtPropertyDept";
            this.txtPropertyDept.Size = new System.Drawing.Size(100, 21);
            this.txtPropertyDept.TabIndex = 12;
            // 
            // txtPropertyBuyID
            // 
            this.txtPropertyBuyID.Location = new System.Drawing.Point(388, 42);
            this.txtPropertyBuyID.Name = "txtPropertyBuyID";
            this.txtPropertyBuyID.Size = new System.Drawing.Size(100, 21);
            this.txtPropertyBuyID.TabIndex = 14;
            // 
            // txtPropertyLocal
            // 
            this.txtPropertyLocal.Location = new System.Drawing.Point(226, 42);
            this.txtPropertyLocal.Name = "txtPropertyLocal";
            this.txtPropertyLocal.Size = new System.Drawing.Size(100, 21);
            this.txtPropertyLocal.TabIndex = 13;
            // 
            // txtPropertyFirstNumber
            // 
            this.txtPropertyFirstNumber.Location = new System.Drawing.Point(699, 8);
            this.txtPropertyFirstNumber.Name = "txtPropertyFirstNumber";
            this.txtPropertyFirstNumber.Size = new System.Drawing.Size(59, 21);
            this.txtPropertyFirstNumber.TabIndex = 9;
            // 
            // txtPropertyCounts
            // 
            this.txtPropertyCounts.Location = new System.Drawing.Point(823, 8);
            this.txtPropertyCounts.Name = "txtPropertyCounts";
            this.txtPropertyCounts.Size = new System.Drawing.Size(108, 21);
            this.txtPropertyCounts.TabIndex = 10;
            // 
            // txtPropertyType
            // 
            this.txtPropertyType.Location = new System.Drawing.Point(539, 8);
            this.txtPropertyType.Name = "txtPropertyType";
            this.txtPropertyType.Size = new System.Drawing.Size(102, 21);
            this.txtPropertyType.TabIndex = 8;
            // 
            // txtUnid
            // 
            this.txtUnid.Location = new System.Drawing.Point(672, 42);
            this.txtUnid.Name = "txtUnid";
            this.txtUnid.Size = new System.Drawing.Size(106, 21);
            this.txtUnid.TabIndex = 16;
            // 
            // txtPropertyMode
            // 
            this.txtPropertyMode.Location = new System.Drawing.Point(377, 8);
            this.txtPropertyMode.Name = "txtPropertyMode";
            this.txtPropertyMode.Size = new System.Drawing.Size(102, 21);
            this.txtPropertyMode.TabIndex = 7;
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(212, 8);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(102, 21);
            this.txtPropertyName.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(332, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "请购单号";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(494, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "保管人";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(172, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "存放地点";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "设备部门";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(784, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "购入日期";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(323, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "财产型号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(157, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "财产名称";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(639, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "单位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(766, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "财产数量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(645, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "起始流水";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(483, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "固资分类";
            // 
            // butDeleteSelected
            // 
            this.butDeleteSelected.Location = new System.Drawing.Point(939, 3);
            this.butDeleteSelected.Name = "butDeleteSelected";
            this.butDeleteSelected.Size = new System.Drawing.Size(95, 23);
            this.butDeleteSelected.TabIndex = 8;
            this.butDeleteSelected.Text = "报废选中项";
            this.butDeleteSelected.UseVisualStyleBackColor = true;
            this.butDeleteSelected.Click += new System.EventHandler(this.butDeleteSelected_Click);
            // 
            // gblist
            // 
            this.gblist.Controls.Add(this.btnBarcodeReader);
            this.gblist.Controls.Add(this.btnCreateQuickMark);
            this.gblist.Controls.Add(this.pictureBox1);
            this.gblist.Controls.Add(this.btnCreateBarCode);
            this.gblist.Controls.Add(this.gbDelNote);
            this.gblist.Controls.Add(this.cbSelected);
            this.gblist.Controls.Add(this.dgvPropertys);
            this.gblist.Location = new System.Drawing.Point(-2, 9);
            this.gblist.Name = "gblist";
            this.gblist.Size = new System.Drawing.Size(1040, 543);
            this.gblist.TabIndex = 7;
            this.gblist.TabStop = false;
            this.gblist.Text = "资产清单";
            // 
            // btnBarcodeReader
            // 
            this.btnBarcodeReader.Location = new System.Drawing.Point(935, 74);
            this.btnBarcodeReader.Name = "btnBarcodeReader";
            this.btnBarcodeReader.Size = new System.Drawing.Size(75, 45);
            this.btnBarcodeReader.TabIndex = 43;
            this.btnBarcodeReader.Text = "读码";
            this.btnBarcodeReader.UseVisualStyleBackColor = true;
            this.btnBarcodeReader.Visible = false;
            // 
            // btnCreateQuickMark
            // 
            this.btnCreateQuickMark.Location = new System.Drawing.Point(854, 74);
            this.btnCreateQuickMark.Name = "btnCreateQuickMark";
            this.btnCreateQuickMark.Size = new System.Drawing.Size(75, 45);
            this.btnCreateQuickMark.TabIndex = 42;
            this.btnCreateQuickMark.Text = "生成二维码";
            this.btnCreateQuickMark.UseVisualStyleBackColor = true;
            this.btnCreateQuickMark.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(770, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(230, 45);
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // btnCreateBarCode
            // 
            this.btnCreateBarCode.Location = new System.Drawing.Point(770, 74);
            this.btnCreateBarCode.Name = "btnCreateBarCode";
            this.btnCreateBarCode.Size = new System.Drawing.Size(75, 47);
            this.btnCreateBarCode.TabIndex = 41;
            this.btnCreateBarCode.Text = "生成条码";
            this.btnCreateBarCode.UseVisualStyleBackColor = true;
            this.btnCreateBarCode.Visible = false;
            // 
            // gbDelNote
            // 
            this.gbDelNote.Controls.Add(this.butDelCancel);
            this.gbDelNote.Controls.Add(this.butDelNote);
            this.gbDelNote.Controls.Add(this.txtDelNote);
            this.gbDelNote.Location = new System.Drawing.Point(239, 113);
            this.gbDelNote.Name = "gbDelNote";
            this.gbDelNote.Size = new System.Drawing.Size(557, 287);
            this.gbDelNote.TabIndex = 8;
            this.gbDelNote.TabStop = false;
            this.gbDelNote.Text = "报废原因：";
            this.gbDelNote.Visible = false;
            // 
            // butDelCancel
            // 
            this.butDelCancel.Location = new System.Drawing.Point(113, 241);
            this.butDelCancel.Name = "butDelCancel";
            this.butDelCancel.Size = new System.Drawing.Size(125, 40);
            this.butDelCancel.TabIndex = 2;
            this.butDelCancel.Text = "取消";
            this.butDelCancel.UseVisualStyleBackColor = true;
            this.butDelCancel.Click += new System.EventHandler(this.butDelCancel_Click);
            // 
            // butDelNote
            // 
            this.butDelNote.Location = new System.Drawing.Point(306, 241);
            this.butDelNote.Name = "butDelNote";
            this.butDelNote.Size = new System.Drawing.Size(125, 40);
            this.butDelNote.TabIndex = 1;
            this.butDelNote.Text = "确认";
            this.butDelNote.UseVisualStyleBackColor = true;
            this.butDelNote.Click += new System.EventHandler(this.butDelNote_Click);
            // 
            // txtDelNote
            // 
            this.txtDelNote.Location = new System.Drawing.Point(6, 20);
            this.txtDelNote.Multiline = true;
            this.txtDelNote.Name = "txtDelNote";
            this.txtDelNote.Size = new System.Drawing.Size(545, 215);
            this.txtDelNote.TabIndex = 0;
            // 
            // cbSelected
            // 
            this.cbSelected.AutoSize = true;
            this.cbSelected.Checked = true;
            this.cbSelected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSelected.Location = new System.Drawing.Point(69, -1);
            this.cbSelected.Name = "cbSelected";
            this.cbSelected.Size = new System.Drawing.Size(48, 16);
            this.cbSelected.TabIndex = 7;
            this.cbSelected.Text = "全选";
            this.cbSelected.UseVisualStyleBackColor = true;
            this.cbSelected.CheckedChanged += new System.EventHandler(this.cbSelected_CheckedChanged);
            // 
            // dgvPropertys
            // 
            this.dgvPropertys.AllowUserToAddRows = false;
            this.dgvPropertys.AllowUserToDeleteRows = false;
            this.dgvPropertys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPropertys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPropertys.Location = new System.Drawing.Point(3, 17);
            this.dgvPropertys.Name = "dgvPropertys";
            this.dgvPropertys.RowTemplate.Height = 23;
            this.dgvPropertys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPropertys.Size = new System.Drawing.Size(1034, 523);
            this.dgvPropertys.TabIndex = 6;
            this.dgvPropertys.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPropertys_CellMouseDown);
            this.dgvPropertys.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvPropertys_RowPostPaint);
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(844, 7);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(95, 49);
            this.butSave.TabIndex = 18;
            this.butSave.Text = "确认保存";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // butPrints
            // 
            this.butPrints.Location = new System.Drawing.Point(945, 7);
            this.butPrints.Name = "butPrints";
            this.butPrints.Size = new System.Drawing.Size(95, 49);
            this.butPrints.TabIndex = 19;
            this.butPrints.Text = "打印财编标";
            this.butPrints.UseVisualStyleBackColor = true;
            this.butPrints.Click += new System.EventHandler(this.butPrints_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(608, 36);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(230, 13);
            this.progressBar1.TabIndex = 20;
            this.progressBar1.Visible = false;
            // 
            // msg
            // 
            this.msg.AutoSize = true;
            this.msg.Location = new System.Drawing.Point(608, 17);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(23, 12);
            this.msg.TabIndex = 21;
            this.msg.Text = "msg";
            this.msg.Visible = false;
            // 
            // MenuRight
            // 
            this.MenuRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RmeCopyCells,
            this.RmeCopyRows,
            this.RmeExportExcel});
            this.MenuRight.Name = "contextMenuStrip1";
            this.MenuRight.Size = new System.Drawing.Size(181, 92);
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
            // FrmFactoryplanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 701);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.butPrints);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.bgProperty);
            this.Controls.Add(this.gbSearch);
            this.Name = "FrmFactoryplanning";
            this.Text = "财编打印";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmFactoryplanning_Load);
            this.Resize += new System.EventHandler(this.FrmFactoryplanning_Resize);
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.bgProperty.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gblist.ResumeLayout(false);
            this.gblist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbDelNote.ResumeLayout(false);
            this.gbDelNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertys)).EndInit();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPropertyNumber;
        private System.Windows.Forms.GroupBox bgProperty;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtPropertyType;
        private System.Windows.Forms.TextBox txtUnid;
        private System.Windows.Forms.TextBox txtPropertyMode;
        private System.Windows.Forms.TextBox txtPropertyName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPropertyFirstNumber;
        private System.Windows.Forms.TextBox txtPropertyCounts;
        private System.Windows.Forms.TextBox txtPropertySavePerson;
        private System.Windows.Forms.TextBox txtPropertyDept;
        private System.Windows.Forms.TextBox txtPropertyBuyID;
        private System.Windows.Forms.TextBox txtPropertyLocal;
        private System.Windows.Forms.Button butGeneratePropertyNumber;
        private System.Windows.Forms.RadioButton rdbutTOP;
        private System.Windows.Forms.RadioButton rdbutSAA;
        private System.Windows.Forms.Button butDeleteSelected;
        private System.Windows.Forms.GroupBox gblist;
        private System.Windows.Forms.CheckBox cbSelected;
        private System.Windows.Forms.DataGridView dgvPropertys;
        private System.Windows.Forms.Button butPrints;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.DateTimePicker dtpPropertyBuyDate;
        private System.Windows.Forms.CheckBox cbIsDel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.GroupBox gbDelNote;
        private System.Windows.Forms.TextBox txtDelNote;
        private System.Windows.Forms.Button butDelNote;
        private System.Windows.Forms.Button butDelCancel;
        private System.Windows.Forms.ComboBox cbORG;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnBarcodeReader;
        private System.Windows.Forms.Button btnCreateQuickMark;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCreateBarCode;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
    }
}