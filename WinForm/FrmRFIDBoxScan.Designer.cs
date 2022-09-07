namespace WinForm
{
    partial class FrmRFIDBoxScan
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
            this.butSaveLogs = new System.Windows.Forms.Button();
            this.bgCartonInfo = new System.Windows.Forms.GroupBox();
            this.txtCartonNoscanQty = new System.Windows.Forms.TextBox();
            this.labCartonScanQty = new System.Windows.Forms.Label();
            this.labCartonQty = new System.Windows.Forms.Label();
            this.txtCartonScanQty = new System.Windows.Forms.TextBox();
            this.txtCartonQty = new System.Windows.Forms.TextBox();
            this.txtCustID = new System.Windows.Forms.TextBox();
            this.labCartonNoscanQty = new System.Windows.Forms.Label();
            this.labCustID = new System.Windows.Forms.Label();
            this.labScanSizeQtys = new System.Windows.Forms.Label();
            this.txtScanSizeQtys = new System.Windows.Forms.TextBox();
            this.txtSizeQtys = new System.Windows.Forms.TextBox();
            this.labSizeQtys = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.txtPO = new System.Windows.Forms.TextBox();
            this.labStyle = new System.Windows.Forms.Label();
            this.labPO = new System.Windows.Forms.Label();
            this.labSizes = new System.Windows.Forms.Label();
            this.labColor = new System.Windows.Forms.Label();
            this.labMsg = new System.Windows.Forms.Label();
            this.labCarton = new System.Windows.Forms.Label();
            this.txtCartonNumber = new System.Windows.Forms.TextBox();
            this.gbScanPort = new System.Windows.Forms.GroupBox();
            this.labStatusRuning = new System.Windows.Forms.Label();
            this.labAlarmStatus = new System.Windows.Forms.Label();
            this.labCartonReaderStatus = new System.Windows.Forms.Label();
            this.labRFID3 = new System.Windows.Forms.Label();
            this.txtRFIDNumber = new System.Windows.Forms.TextBox();
            this.labRFID = new System.Windows.Forms.Label();
            this.serialPortRFIDNumber1 = new System.IO.Ports.SerialPort(this.components);
            this.gbBoxs = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvBoxHeads = new System.Windows.Forms.ListView();
            this.cNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cEPC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dgvBoxDetails = new System.Windows.Forms.DataGridView();
            this.labCartonNumber = new System.Windows.Forms.Label();
            this.labScanQtys = new System.Windows.Forms.Label();
            this.gbBoxInfo = new System.Windows.Forms.GroupBox();
            this.pnlMsg = new System.Windows.Forms.Panel();
            this.butReScan = new System.Windows.Forms.Button();
            this.ledAllTimes = new System.Windows.Forms.Label();
            this.ledAllTags = new System.Windows.Forms.Label();
            this.ledInventoryTimes = new System.Windows.Forms.Label();
            this.ledCurrentAnt = new System.Windows.Forms.Label();
            this.ledInventoryTags = new System.Windows.Forms.Label();
            this.lblCurrentAnt = new System.Windows.Forms.Label();
            this.nudInventoryInterval = new System.Windows.Forms.NumericUpDown();
            this.nudInventoryCount = new System.Windows.Forms.NumericUpDown();
            this.lblInventoryCount = new System.Windows.Forms.Label();
            this.nudRunTimes = new System.Windows.Forms.NumericUpDown();
            this.lblRunTimes = new System.Windows.Forms.Label();
            this.nudMaxTag = new System.Windows.Forms.NumericUpDown();
            this.lblAllTimes = new System.Windows.Forms.Label();
            this.lblInventoryInterval = new System.Windows.Forms.Label();
            this.lblMaxTag = new System.Windows.Forms.Label();
            this.lblInventoryTimes = new System.Windows.Forms.Label();
            this.lblInventoryTags = new System.Windows.Forms.Label();
            this.lblAllTags = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.serialPortRFIDNumber2 = new System.IO.Ports.SerialPort(this.components);
            this.serialPortRFIDNumber3 = new System.IO.Ports.SerialPort(this.components);
            this.labRFID2 = new System.Windows.Forms.Label();
            this.labRFID1 = new System.Windows.Forms.Label();
            this.gbStatus = new System.Windows.Forms.GroupBox();
            this.labRFID5 = new System.Windows.Forms.Label();
            this.labRFID4 = new System.Windows.Forms.Label();
            this.labRFID6 = new System.Windows.Forms.Label();
            this.labPowerControlStatus = new System.Windows.Forms.Label();
            this.serialPortRFIDNumber4 = new System.IO.Ports.SerialPort(this.components);
            this.serialPortRFIDNumber5 = new System.IO.Ports.SerialPort(this.components);
            this.serialPortRFIDNumber6 = new System.IO.Ports.SerialPort(this.components);
            this.butgoing = new System.Windows.Forms.Button();
            this.butstop = new System.Windows.Forms.Button();
            this.butback = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bgCartonInfo.SuspendLayout();
            this.gbScanPort.SuspendLayout();
            this.gbBoxs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxDetails)).BeginInit();
            this.gbBoxInfo.SuspendLayout();
            this.pnlMsg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInventoryInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInventoryCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRunTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTag)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // butSaveLogs
            // 
            this.butSaveLogs.Location = new System.Drawing.Point(714, -2);
            this.butSaveLogs.Name = "butSaveLogs";
            this.butSaveLogs.Size = new System.Drawing.Size(75, 23);
            this.butSaveLogs.TabIndex = 1;
            this.butSaveLogs.Text = "保存日志文件";
            this.butSaveLogs.UseVisualStyleBackColor = true;
            this.butSaveLogs.Visible = false;
            this.butSaveLogs.Click += new System.EventHandler(this.butSaveLogs_Click);
            // 
            // bgCartonInfo
            // 
            this.bgCartonInfo.BackColor = System.Drawing.SystemColors.Control;
            this.bgCartonInfo.Controls.Add(this.txtCartonNoscanQty);
            this.bgCartonInfo.Controls.Add(this.labCartonScanQty);
            this.bgCartonInfo.Controls.Add(this.labCartonQty);
            this.bgCartonInfo.Controls.Add(this.txtCartonScanQty);
            this.bgCartonInfo.Controls.Add(this.txtCartonQty);
            this.bgCartonInfo.Controls.Add(this.txtCustID);
            this.bgCartonInfo.Controls.Add(this.labCartonNoscanQty);
            this.bgCartonInfo.Controls.Add(this.labCustID);
            this.bgCartonInfo.Controls.Add(this.labScanSizeQtys);
            this.bgCartonInfo.Controls.Add(this.txtScanSizeQtys);
            this.bgCartonInfo.Controls.Add(this.txtSizeQtys);
            this.bgCartonInfo.Controls.Add(this.labSizeQtys);
            this.bgCartonInfo.Controls.Add(this.txtSize);
            this.bgCartonInfo.Controls.Add(this.txtColor);
            this.bgCartonInfo.Controls.Add(this.txtStyle);
            this.bgCartonInfo.Controls.Add(this.txtPO);
            this.bgCartonInfo.Controls.Add(this.labStyle);
            this.bgCartonInfo.Controls.Add(this.labPO);
            this.bgCartonInfo.Controls.Add(this.labSizes);
            this.bgCartonInfo.Controls.Add(this.labColor);
            this.bgCartonInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bgCartonInfo.Location = new System.Drawing.Point(7, 6);
            this.bgCartonInfo.Name = "bgCartonInfo";
            this.bgCartonInfo.Size = new System.Drawing.Size(299, 358);
            this.bgCartonInfo.TabIndex = 14;
            this.bgCartonInfo.TabStop = false;
            this.bgCartonInfo.Text = "外箱信息";
            // 
            // txtCartonNoscanQty
            // 
            this.txtCartonNoscanQty.BackColor = System.Drawing.Color.White;
            this.txtCartonNoscanQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCartonNoscanQty.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCartonNoscanQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCartonNoscanQty.Location = new System.Drawing.Point(210, 47);
            this.txtCartonNoscanQty.Multiline = true;
            this.txtCartonNoscanQty.Name = "txtCartonNoscanQty";
            this.txtCartonNoscanQty.ReadOnly = true;
            this.txtCartonNoscanQty.Size = new System.Drawing.Size(72, 31);
            this.txtCartonNoscanQty.TabIndex = 17;
            this.txtCartonNoscanQty.TabStop = false;
            this.txtCartonNoscanQty.Text = "0";
            this.txtCartonNoscanQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labCartonScanQty
            // 
            this.labCartonScanQty.AutoSize = true;
            this.labCartonScanQty.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCartonScanQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonScanQty.Location = new System.Drawing.Point(109, 23);
            this.labCartonScanQty.Name = "labCartonScanQty";
            this.labCartonScanQty.Size = new System.Drawing.Size(75, 16);
            this.labCartonScanQty.TabIndex = 8;
            this.labCartonScanQty.Text = "已扫件数";
            // 
            // labCartonQty
            // 
            this.labCartonQty.AutoSize = true;
            this.labCartonQty.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCartonQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonQty.Location = new System.Drawing.Point(22, 23);
            this.labCartonQty.Name = "labCartonQty";
            this.labCartonQty.Size = new System.Drawing.Size(75, 16);
            this.labCartonQty.TabIndex = 3;
            this.labCartonQty.Text = "本箱件数";
            // 
            // txtCartonScanQty
            // 
            this.txtCartonScanQty.BackColor = System.Drawing.Color.White;
            this.txtCartonScanQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCartonScanQty.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCartonScanQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCartonScanQty.Location = new System.Drawing.Point(113, 47);
            this.txtCartonScanQty.Multiline = true;
            this.txtCartonScanQty.Name = "txtCartonScanQty";
            this.txtCartonScanQty.ReadOnly = true;
            this.txtCartonScanQty.Size = new System.Drawing.Size(72, 31);
            this.txtCartonScanQty.TabIndex = 14;
            this.txtCartonScanQty.TabStop = false;
            this.txtCartonScanQty.Text = "0";
            this.txtCartonScanQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCartonQty
            // 
            this.txtCartonQty.BackColor = System.Drawing.Color.White;
            this.txtCartonQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCartonQty.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCartonQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCartonQty.Location = new System.Drawing.Point(23, 47);
            this.txtCartonQty.Multiline = true;
            this.txtCartonQty.Name = "txtCartonQty";
            this.txtCartonQty.ReadOnly = true;
            this.txtCartonQty.Size = new System.Drawing.Size(72, 31);
            this.txtCartonQty.TabIndex = 15;
            this.txtCartonQty.TabStop = false;
            this.txtCartonQty.Text = "0";
            this.txtCartonQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCustID
            // 
            this.txtCustID.BackColor = System.Drawing.Color.White;
            this.txtCustID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCustID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCustID.Location = new System.Drawing.Point(94, 91);
            this.txtCustID.Multiline = true;
            this.txtCustID.Name = "txtCustID";
            this.txtCustID.ReadOnly = true;
            this.txtCustID.Size = new System.Drawing.Size(199, 22);
            this.txtCustID.TabIndex = 23;
            this.txtCustID.TabStop = false;
            this.txtCustID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labCartonNoscanQty
            // 
            this.labCartonNoscanQty.AutoSize = true;
            this.labCartonNoscanQty.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCartonNoscanQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonNoscanQty.Location = new System.Drawing.Point(207, 23);
            this.labCartonNoscanQty.Name = "labCartonNoscanQty";
            this.labCartonNoscanQty.Size = new System.Drawing.Size(75, 16);
            this.labCartonNoscanQty.TabIndex = 16;
            this.labCartonNoscanQty.Text = "待扫件数";
            // 
            // labCustID
            // 
            this.labCustID.AutoSize = true;
            this.labCustID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCustID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCustID.Location = new System.Drawing.Point(32, 94);
            this.labCustID.Name = "labCustID";
            this.labCustID.Size = new System.Drawing.Size(59, 16);
            this.labCustID.TabIndex = 22;
            this.labCustID.Text = "Cust_ID";
            // 
            // labScanSizeQtys
            // 
            this.labScanSizeQtys.AutoSize = true;
            this.labScanSizeQtys.Font = new System.Drawing.Font("新細明體", 11F);
            this.labScanSizeQtys.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labScanSizeQtys.Location = new System.Drawing.Point(9, 322);
            this.labScanSizeQtys.Name = "labScanSizeQtys";
            this.labScanSizeQtys.Size = new System.Drawing.Size(82, 15);
            this.labScanSizeQtys.TabIndex = 21;
            this.labScanSizeQtys.Text = "已扫尺码数";
            // 
            // txtScanSizeQtys
            // 
            this.txtScanSizeQtys.BackColor = System.Drawing.Color.White;
            this.txtScanSizeQtys.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScanSizeQtys.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtScanSizeQtys.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtScanSizeQtys.Location = new System.Drawing.Point(94, 313);
            this.txtScanSizeQtys.Multiline = true;
            this.txtScanSizeQtys.Name = "txtScanSizeQtys";
            this.txtScanSizeQtys.ReadOnly = true;
            this.txtScanSizeQtys.Size = new System.Drawing.Size(199, 22);
            this.txtScanSizeQtys.TabIndex = 20;
            this.txtScanSizeQtys.TabStop = false;
            this.txtScanSizeQtys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSizeQtys
            // 
            this.txtSizeQtys.BackColor = System.Drawing.Color.White;
            this.txtSizeQtys.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSizeQtys.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSizeQtys.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSizeQtys.Location = new System.Drawing.Point(94, 276);
            this.txtSizeQtys.Multiline = true;
            this.txtSizeQtys.Name = "txtSizeQtys";
            this.txtSizeQtys.ReadOnly = true;
            this.txtSizeQtys.Size = new System.Drawing.Size(199, 22);
            this.txtSizeQtys.TabIndex = 19;
            this.txtSizeQtys.TabStop = false;
            this.txtSizeQtys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labSizeQtys
            // 
            this.labSizeQtys.AutoSize = true;
            this.labSizeQtys.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labSizeQtys.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labSizeQtys.Location = new System.Drawing.Point(20, 284);
            this.labSizeQtys.Name = "labSizeQtys";
            this.labSizeQtys.Size = new System.Drawing.Size(71, 16);
            this.labSizeQtys.TabIndex = 18;
            this.labSizeQtys.Text = "尺码数量";
            // 
            // txtSize
            // 
            this.txtSize.BackColor = System.Drawing.Color.White;
            this.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSize.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSize.Location = new System.Drawing.Point(94, 239);
            this.txtSize.Multiline = true;
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(199, 22);
            this.txtSize.TabIndex = 12;
            this.txtSize.TabStop = false;
            this.txtSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtColor
            // 
            this.txtColor.BackColor = System.Drawing.Color.White;
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtColor.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtColor.Location = new System.Drawing.Point(94, 202);
            this.txtColor.Multiline = true;
            this.txtColor.Name = "txtColor";
            this.txtColor.ReadOnly = true;
            this.txtColor.Size = new System.Drawing.Size(199, 22);
            this.txtColor.TabIndex = 11;
            this.txtColor.TabStop = false;
            this.txtColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtStyle
            // 
            this.txtStyle.BackColor = System.Drawing.Color.White;
            this.txtStyle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStyle.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtStyle.Location = new System.Drawing.Point(94, 165);
            this.txtStyle.Multiline = true;
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(199, 22);
            this.txtStyle.TabIndex = 10;
            this.txtStyle.TabStop = false;
            this.txtStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPO
            // 
            this.txtPO.BackColor = System.Drawing.Color.White;
            this.txtPO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPO.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPO.Location = new System.Drawing.Point(94, 128);
            this.txtPO.Multiline = true;
            this.txtPO.Name = "txtPO";
            this.txtPO.ReadOnly = true;
            this.txtPO.Size = new System.Drawing.Size(199, 22);
            this.txtPO.TabIndex = 9;
            this.txtPO.TabStop = false;
            this.txtPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labStyle
            // 
            this.labStyle.AutoSize = true;
            this.labStyle.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labStyle.Location = new System.Drawing.Point(52, 170);
            this.labStyle.Name = "labStyle";
            this.labStyle.Size = new System.Drawing.Size(39, 16);
            this.labStyle.TabIndex = 5;
            this.labStyle.Text = "款式";
            // 
            // labPO
            // 
            this.labPO.AutoSize = true;
            this.labPO.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labPO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labPO.Location = new System.Drawing.Point(65, 132);
            this.labPO.Name = "labPO";
            this.labPO.Size = new System.Drawing.Size(26, 16);
            this.labPO.TabIndex = 4;
            this.labPO.Text = "PO";
            // 
            // labSizes
            // 
            this.labSizes.AutoSize = true;
            this.labSizes.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labSizes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labSizes.Location = new System.Drawing.Point(20, 246);
            this.labSizes.Name = "labSizes";
            this.labSizes.Size = new System.Drawing.Size(71, 16);
            this.labSizes.TabIndex = 2;
            this.labSizes.Text = "尺码名称";
            // 
            // labColor
            // 
            this.labColor.AutoSize = true;
            this.labColor.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labColor.Location = new System.Drawing.Point(52, 208);
            this.labColor.Name = "labColor";
            this.labColor.Size = new System.Drawing.Size(39, 16);
            this.labColor.TabIndex = 1;
            this.labColor.Text = "颜色";
            // 
            // labMsg
            // 
            this.labMsg.AutoSize = true;
            this.labMsg.Font = new System.Drawing.Font("新細明體", 12F);
            this.labMsg.ForeColor = System.Drawing.Color.Red;
            this.labMsg.Location = new System.Drawing.Point(9, 31);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(39, 16);
            this.labMsg.TabIndex = 9;
            this.labMsg.Text = "提示";
            // 
            // labCarton
            // 
            this.labCarton.AutoSize = true;
            this.labCarton.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCarton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCarton.Location = new System.Drawing.Point(6, 57);
            this.labCarton.Name = "labCarton";
            this.labCarton.Size = new System.Drawing.Size(75, 16);
            this.labCarton.TabIndex = 1;
            this.labCarton.Text = "外箱条码:";
            // 
            // txtCartonNumber
            // 
            this.txtCartonNumber.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCartonNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCartonNumber.Location = new System.Drawing.Point(94, 54);
            this.txtCartonNumber.Name = "txtCartonNumber";
            this.txtCartonNumber.Size = new System.Drawing.Size(192, 23);
            this.txtCartonNumber.TabIndex = 0;
            this.txtCartonNumber.TextChanged += new System.EventHandler(this.txtCartonNumber_TextChanged);
            this.txtCartonNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCartonNumber_KeyDown);
            this.txtCartonNumber.Leave += new System.EventHandler(this.txtCartonNumber_Leave);
            // 
            // gbScanPort
            // 
            this.gbScanPort.Controls.Add(this.label1);
            this.gbScanPort.Controls.Add(this.butback);
            this.gbScanPort.Controls.Add(this.butstop);
            this.gbScanPort.Controls.Add(this.butgoing);
            this.gbScanPort.Controls.Add(this.txtRFIDNumber);
            this.gbScanPort.Controls.Add(this.labRFID);
            this.gbScanPort.Controls.Add(this.txtCartonNumber);
            this.gbScanPort.Controls.Add(this.labCarton);
            this.gbScanPort.Controls.Add(this.labMsg);
            this.gbScanPort.Location = new System.Drawing.Point(7, 368);
            this.gbScanPort.Name = "gbScanPort";
            this.gbScanPort.Size = new System.Drawing.Size(297, 178);
            this.gbScanPort.TabIndex = 16;
            this.gbScanPort.TabStop = false;
            this.gbScanPort.Text = "外箱扫描";
            // 
            // labStatusRuning
            // 
            this.labStatusRuning.Location = new System.Drawing.Point(13, 19);
            this.labStatusRuning.Name = "labStatusRuning";
            this.labStatusRuning.Size = new System.Drawing.Size(100, 46);
            this.labStatusRuning.TabIndex = 19;
            this.labStatusRuning.Text = "SystemStatus";
            this.labStatusRuning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labAlarmStatus
            // 
            this.labAlarmStatus.Location = new System.Drawing.Point(644, 19);
            this.labAlarmStatus.Name = "labAlarmStatus";
            this.labAlarmStatus.Size = new System.Drawing.Size(85, 46);
            this.labAlarmStatus.TabIndex = 18;
            this.labAlarmStatus.Text = "Alarm";
            this.labAlarmStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCartonReaderStatus
            // 
            this.labCartonReaderStatus.Location = new System.Drawing.Point(758, 19);
            this.labCartonReaderStatus.Name = "labCartonReaderStatus";
            this.labCartonReaderStatus.Size = new System.Drawing.Size(85, 46);
            this.labCartonReaderStatus.TabIndex = 17;
            this.labCartonReaderStatus.Text = "CartonReader";
            this.labCartonReaderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labRFID3
            // 
            this.labRFID3.Location = new System.Drawing.Point(259, 19);
            this.labRFID3.Name = "labRFID3";
            this.labRFID3.Size = new System.Drawing.Size(45, 46);
            this.labRFID3.TabIndex = 15;
            this.labRFID3.Text = "RFID3";
            this.labRFID3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRFIDNumber
            // 
            this.txtRFIDNumber.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtRFIDNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtRFIDNumber.Location = new System.Drawing.Point(94, 83);
            this.txtRFIDNumber.Name = "txtRFIDNumber";
            this.txtRFIDNumber.Size = new System.Drawing.Size(192, 23);
            this.txtRFIDNumber.TabIndex = 11;
            this.txtRFIDNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRFIDNumber_KeyDown);
            // 
            // labRFID
            // 
            this.labRFID.AutoSize = true;
            this.labRFID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labRFID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labRFID.Location = new System.Drawing.Point(6, 86);
            this.labRFID.Name = "labRFID";
            this.labRFID.Size = new System.Drawing.Size(77, 16);
            this.labRFID.TabIndex = 10;
            this.labRFID.Text = "吊卡RFID:";
            // 
            // serialPortRFIDNumber1
            // 
            this.serialPortRFIDNumber1.BaudRate = 57600;
            this.serialPortRFIDNumber1.PortName = "COM5";
            this.serialPortRFIDNumber1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortRFIDNumber_DataReceived);
            // 
            // gbBoxs
            // 
            this.gbBoxs.Controls.Add(this.splitContainer1);
            this.gbBoxs.Controls.Add(this.butSaveLogs);
            this.gbBoxs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbBoxs.Location = new System.Drawing.Point(312, 231);
            this.gbBoxs.Name = "gbBoxs";
            this.gbBoxs.Size = new System.Drawing.Size(798, 315);
            this.gbBoxs.TabIndex = 16;
            this.gbBoxs.TabStop = false;
            this.gbBoxs.Text = "扫描明细";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 18);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvBoxHeads);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.dgvBoxDetails);
            this.splitContainer1.Size = new System.Drawing.Size(792, 294);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 18;
            // 
            // dgvBoxHeads
            // 
            this.dgvBoxHeads.AutoArrange = false;
            this.dgvBoxHeads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cNumber,
            this.cEPC});
            this.dgvBoxHeads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBoxHeads.FullRowSelect = true;
            this.dgvBoxHeads.HideSelection = false;
            this.dgvBoxHeads.Location = new System.Drawing.Point(0, 0);
            this.dgvBoxHeads.MultiSelect = false;
            this.dgvBoxHeads.Name = "dgvBoxHeads";
            this.dgvBoxHeads.Size = new System.Drawing.Size(250, 294);
            this.dgvBoxHeads.TabIndex = 87;
            this.dgvBoxHeads.UseCompatibleStateImageBehavior = false;
            this.dgvBoxHeads.View = System.Windows.Forms.View.Details;
            // 
            // cNumber
            // 
            this.cNumber.Text = "";
            this.cNumber.Width = 30;
            // 
            // cEPC
            // 
            this.cEPC.Text = "EPC";
            this.cEPC.Width = 440;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 361);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(329, 19);
            this.textBox1.TabIndex = 2;
            // 
            // dgvBoxDetails
            // 
            this.dgvBoxDetails.AllowUserToAddRows = false;
            this.dgvBoxDetails.AllowUserToDeleteRows = false;
            this.dgvBoxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoxDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBoxDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvBoxDetails.Name = "dgvBoxDetails";
            this.dgvBoxDetails.ReadOnly = true;
            this.dgvBoxDetails.RowTemplate.Height = 24;
            this.dgvBoxDetails.Size = new System.Drawing.Size(538, 294);
            this.dgvBoxDetails.TabIndex = 1;
            this.dgvBoxDetails.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvBoxDetails_RowPostPaint);
            // 
            // labCartonNumber
            // 
            this.labCartonNumber.BackColor = System.Drawing.SystemColors.Control;
            this.labCartonNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labCartonNumber.Font = new System.Drawing.Font("新細明體", 55F, System.Drawing.FontStyle.Bold);
            this.labCartonNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonNumber.Location = new System.Drawing.Point(6, 18);
            this.labCartonNumber.Name = "labCartonNumber";
            this.labCartonNumber.Size = new System.Drawing.Size(468, 92);
            this.labCartonNumber.TabIndex = 21;
            this.labCartonNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labScanQtys
            // 
            this.labScanQtys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labScanQtys.Font = new System.Drawing.Font("新細明體", 75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labScanQtys.Location = new System.Drawing.Point(480, 18);
            this.labScanQtys.Name = "labScanQtys";
            this.labScanQtys.Size = new System.Drawing.Size(309, 92);
            this.labScanQtys.TabIndex = 23;
            this.labScanQtys.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbBoxInfo
            // 
            this.gbBoxInfo.Controls.Add(this.labScanQtys);
            this.gbBoxInfo.Controls.Add(this.labCartonNumber);
            this.gbBoxInfo.Location = new System.Drawing.Point(312, 104);
            this.gbBoxInfo.Name = "gbBoxInfo";
            this.gbBoxInfo.Size = new System.Drawing.Size(795, 121);
            this.gbBoxInfo.TabIndex = 17;
            this.gbBoxInfo.TabStop = false;
            this.gbBoxInfo.Text = "外箱信息";
            // 
            // pnlMsg
            // 
            this.pnlMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMsg.Controls.Add(this.butReScan);
            this.pnlMsg.Controls.Add(this.ledAllTimes);
            this.pnlMsg.Controls.Add(this.ledAllTags);
            this.pnlMsg.Controls.Add(this.ledInventoryTimes);
            this.pnlMsg.Controls.Add(this.ledCurrentAnt);
            this.pnlMsg.Controls.Add(this.ledInventoryTags);
            this.pnlMsg.Controls.Add(this.lblCurrentAnt);
            this.pnlMsg.Controls.Add(this.nudInventoryInterval);
            this.pnlMsg.Controls.Add(this.nudInventoryCount);
            this.pnlMsg.Controls.Add(this.lblInventoryCount);
            this.pnlMsg.Controls.Add(this.nudRunTimes);
            this.pnlMsg.Controls.Add(this.lblRunTimes);
            this.pnlMsg.Controls.Add(this.nudMaxTag);
            this.pnlMsg.Controls.Add(this.lblAllTimes);
            this.pnlMsg.Controls.Add(this.lblInventoryInterval);
            this.pnlMsg.Controls.Add(this.lblMaxTag);
            this.pnlMsg.Controls.Add(this.lblInventoryTimes);
            this.pnlMsg.Controls.Add(this.lblInventoryTags);
            this.pnlMsg.Controls.Add(this.lblAllTags);
            this.pnlMsg.Location = new System.Drawing.Point(5, 13);
            this.pnlMsg.Name = "pnlMsg";
            this.pnlMsg.Size = new System.Drawing.Size(783, 78);
            this.pnlMsg.TabIndex = 86;
            // 
            // butReScan
            // 
            this.butReScan.Enabled = false;
            this.butReScan.Location = new System.Drawing.Point(662, 7);
            this.butReScan.Name = "butReScan";
            this.butReScan.Size = new System.Drawing.Size(101, 65);
            this.butReScan.TabIndex = 2306;
            this.butReScan.Text = "重新扫描";
            this.butReScan.UseVisualStyleBackColor = true;
            this.butReScan.Click += new System.EventHandler(this.butReScan_Click);
            // 
            // ledAllTimes
            // 
            this.ledAllTimes.AutoSize = true;
            this.ledAllTimes.Location = new System.Drawing.Point(394, 32);
            this.ledAllTimes.Name = "ledAllTimes";
            this.ledAllTimes.Size = new System.Drawing.Size(33, 12);
            this.ledAllTimes.TabIndex = 2305;
            this.ledAllTimes.Text = "label1";
            // 
            // ledAllTags
            // 
            this.ledAllTags.AutoSize = true;
            this.ledAllTags.Location = new System.Drawing.Point(394, 7);
            this.ledAllTags.Name = "ledAllTags";
            this.ledAllTags.Size = new System.Drawing.Size(33, 12);
            this.ledAllTags.TabIndex = 2304;
            this.ledAllTags.Text = "label1";
            // 
            // ledInventoryTimes
            // 
            this.ledInventoryTimes.AutoSize = true;
            this.ledInventoryTimes.Location = new System.Drawing.Point(252, 32);
            this.ledInventoryTimes.Name = "ledInventoryTimes";
            this.ledInventoryTimes.Size = new System.Drawing.Size(33, 12);
            this.ledInventoryTimes.TabIndex = 2303;
            this.ledInventoryTimes.Text = "label1";
            // 
            // ledCurrentAnt
            // 
            this.ledCurrentAnt.AutoSize = true;
            this.ledCurrentAnt.Location = new System.Drawing.Point(252, 7);
            this.ledCurrentAnt.Name = "ledCurrentAnt";
            this.ledCurrentAnt.Size = new System.Drawing.Size(33, 12);
            this.ledCurrentAnt.TabIndex = 2302;
            this.ledCurrentAnt.Text = "label1";
            // 
            // ledInventoryTags
            // 
            this.ledInventoryTags.AutoSize = true;
            this.ledInventoryTags.Font = new System.Drawing.Font("新細明體", 22F);
            this.ledInventoryTags.Location = new System.Drawing.Point(31, 27);
            this.ledInventoryTags.Name = "ledInventoryTags";
            this.ledInventoryTags.Size = new System.Drawing.Size(83, 30);
            this.ledInventoryTags.TabIndex = 2301;
            this.ledInventoryTags.Text = "label1";
            // 
            // lblCurrentAnt
            // 
            this.lblCurrentAnt.AutoSize = true;
            this.lblCurrentAnt.Location = new System.Drawing.Point(182, 7);
            this.lblCurrentAnt.Name = "lblCurrentAnt";
            this.lblCurrentAnt.Size = new System.Drawing.Size(64, 12);
            this.lblCurrentAnt.TabIndex = 2299;
            this.lblCurrentAnt.Text = "Current Ant:";
            this.lblCurrentAnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudInventoryInterval
            // 
            this.nudInventoryInterval.Location = new System.Drawing.Point(600, 51);
            this.nudInventoryInterval.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.nudInventoryInterval.Name = "nudInventoryInterval";
            this.nudInventoryInterval.Size = new System.Drawing.Size(44, 22);
            this.nudInventoryInterval.TabIndex = 2294;
            this.nudInventoryInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudInventoryCount
            // 
            this.nudInventoryCount.Location = new System.Drawing.Point(379, 51);
            this.nudInventoryCount.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.nudInventoryCount.Name = "nudInventoryCount";
            this.nudInventoryCount.Size = new System.Drawing.Size(48, 22);
            this.nudInventoryCount.TabIndex = 2290;
            this.nudInventoryCount.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // lblInventoryCount
            // 
            this.lblInventoryCount.AutoSize = true;
            this.lblInventoryCount.Location = new System.Drawing.Point(229, 56);
            this.lblInventoryCount.Name = "lblInventoryCount";
            this.lblInventoryCount.Size = new System.Drawing.Size(124, 12);
            this.lblInventoryCount.TabIndex = 2289;
            this.lblInventoryCount.Text = "Stop for Inventory(num):";
            this.lblInventoryCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudRunTimes
            // 
            this.nudRunTimes.Location = new System.Drawing.Point(596, 27);
            this.nudRunTimes.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.nudRunTimes.Name = "nudRunTimes";
            this.nudRunTimes.Size = new System.Drawing.Size(48, 22);
            this.nudRunTimes.TabIndex = 2288;
            this.nudRunTimes.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblRunTimes
            // 
            this.lblRunTimes.AutoSize = true;
            this.lblRunTimes.Location = new System.Drawing.Point(446, 32);
            this.lblRunTimes.Name = "lblRunTimes";
            this.lblRunTimes.Size = new System.Drawing.Size(122, 12);
            this.lblRunTimes.TabIndex = 2287;
            this.lblRunTimes.Text = "Stop for Run Times(sec):";
            this.lblRunTimes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudMaxTag
            // 
            this.nudMaxTag.Location = new System.Drawing.Point(596, 2);
            this.nudMaxTag.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.nudMaxTag.Name = "nudMaxTag";
            this.nudMaxTag.Size = new System.Drawing.Size(48, 22);
            this.nudMaxTag.TabIndex = 2286;
            this.nudMaxTag.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // lblAllTimes
            // 
            this.lblAllTimes.AutoSize = true;
            this.lblAllTimes.Location = new System.Drawing.Point(317, 32);
            this.lblAllTimes.Name = "lblAllTimes";
            this.lblAllTimes.Size = new System.Drawing.Size(71, 12);
            this.lblAllTimes.TabIndex = 2284;
            this.lblAllTimes.Text = "Run Times(s):";
            this.lblAllTimes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInventoryInterval
            // 
            this.lblInventoryInterval.AutoSize = true;
            this.lblInventoryInterval.Location = new System.Drawing.Point(454, 56);
            this.lblInventoryInterval.Name = "lblInventoryInterval";
            this.lblInventoryInterval.Size = new System.Drawing.Size(114, 12);
            this.lblInventoryInterval.TabIndex = 2283;
            this.lblInventoryInterval.Text = "Inventory Interval(ms):";
            this.lblInventoryInterval.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMaxTag
            // 
            this.lblMaxTag.AutoSize = true;
            this.lblMaxTag.Location = new System.Drawing.Point(446, 7);
            this.lblMaxTag.Name = "lblMaxTag";
            this.lblMaxTag.Size = new System.Drawing.Size(97, 12);
            this.lblMaxTag.TabIndex = 2272;
            this.lblMaxTag.Text = "Stop for Tags(tags):";
            this.lblMaxTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInventoryTimes
            // 
            this.lblInventoryTimes.AutoSize = true;
            this.lblInventoryTimes.Location = new System.Drawing.Point(149, 32);
            this.lblInventoryTimes.Name = "lblInventoryTimes";
            this.lblInventoryTimes.Size = new System.Drawing.Size(97, 12);
            this.lblInventoryTimes.TabIndex = 2270;
            this.lblInventoryTimes.Text = "Inventory Times(s):";
            this.lblInventoryTimes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInventoryTags
            // 
            this.lblInventoryTags.Location = new System.Drawing.Point(14, -2);
            this.lblInventoryTags.Name = "lblInventoryTags";
            this.lblInventoryTags.Size = new System.Drawing.Size(100, 30);
            this.lblInventoryTags.TabIndex = 79;
            this.lblInventoryTags.Text = "Inventory tags :";
            this.lblInventoryTags.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAllTags
            // 
            this.lblAllTags.AutoSize = true;
            this.lblAllTags.Location = new System.Drawing.Point(315, 7);
            this.lblAllTags.Name = "lblAllTags";
            this.lblAllTags.Size = new System.Drawing.Size(73, 12);
            this.lblAllTags.TabIndex = 2280;
            this.lblAllTags.Text = "All Tags(tags):";
            this.lblAllTags.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 10;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlMsg);
            this.groupBox1.Location = new System.Drawing.Point(312, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(795, 95);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RFID运行信息";
            // 
            // serialPortRFIDNumber2
            // 
            this.serialPortRFIDNumber2.BaudRate = 57600;
            this.serialPortRFIDNumber2.PortName = "COM6";
            this.serialPortRFIDNumber2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortRFIDNumber2_DataReceived);
            // 
            // serialPortRFIDNumber3
            // 
            this.serialPortRFIDNumber3.BaudRate = 57600;
            this.serialPortRFIDNumber3.PortName = "COM7";
            this.serialPortRFIDNumber3.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortRFIDNumber3_DataReceived);
            // 
            // labRFID2
            // 
            this.labRFID2.Location = new System.Drawing.Point(201, 19);
            this.labRFID2.Name = "labRFID2";
            this.labRFID2.Size = new System.Drawing.Size(45, 46);
            this.labRFID2.TabIndex = 20;
            this.labRFID2.Text = "RFID2";
            this.labRFID2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labRFID1
            // 
            this.labRFID1.Location = new System.Drawing.Point(143, 19);
            this.labRFID1.Name = "labRFID1";
            this.labRFID1.Size = new System.Drawing.Size(45, 46);
            this.labRFID1.TabIndex = 21;
            this.labRFID1.Text = "RFID1";
            this.labRFID1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbStatus
            // 
            this.gbStatus.Controls.Add(this.labPowerControlStatus);
            this.gbStatus.Controls.Add(this.labAlarmStatus);
            this.gbStatus.Controls.Add(this.labRFID5);
            this.gbStatus.Controls.Add(this.labCartonReaderStatus);
            this.gbStatus.Controls.Add(this.labRFID4);
            this.gbStatus.Controls.Add(this.labRFID6);
            this.gbStatus.Controls.Add(this.labRFID2);
            this.gbStatus.Controls.Add(this.labRFID1);
            this.gbStatus.Controls.Add(this.labRFID3);
            this.gbStatus.Controls.Add(this.labStatusRuning);
            this.gbStatus.Location = new System.Drawing.Point(5, 554);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Size = new System.Drawing.Size(1101, 75);
            this.gbStatus.TabIndex = 88;
            this.gbStatus.TabStop = false;
            this.gbStatus.Text = "硬件状态";
            // 
            // labRFID5
            // 
            this.labRFID5.Location = new System.Drawing.Point(375, 19);
            this.labRFID5.Name = "labRFID5";
            this.labRFID5.Size = new System.Drawing.Size(45, 46);
            this.labRFID5.TabIndex = 23;
            this.labRFID5.Text = "RFID5";
            this.labRFID5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labRFID4
            // 
            this.labRFID4.Location = new System.Drawing.Point(317, 19);
            this.labRFID4.Name = "labRFID4";
            this.labRFID4.Size = new System.Drawing.Size(45, 46);
            this.labRFID4.TabIndex = 24;
            this.labRFID4.Text = "RFID4";
            this.labRFID4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labRFID6
            // 
            this.labRFID6.Location = new System.Drawing.Point(433, 19);
            this.labRFID6.Name = "labRFID6";
            this.labRFID6.Size = new System.Drawing.Size(45, 46);
            this.labRFID6.TabIndex = 22;
            this.labRFID6.Text = "RFID6";
            this.labRFID6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labPowerControlStatus
            // 
            this.labPowerControlStatus.Location = new System.Drawing.Point(530, 19);
            this.labPowerControlStatus.Name = "labPowerControlStatus";
            this.labPowerControlStatus.Size = new System.Drawing.Size(85, 46);
            this.labPowerControlStatus.TabIndex = 25;
            this.labPowerControlStatus.Text = "PowerControl";
            this.labPowerControlStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // serialPortRFIDNumber4
            // 
            this.serialPortRFIDNumber4.BaudRate = 57600;
            this.serialPortRFIDNumber4.PortName = "COM5";
            this.serialPortRFIDNumber4.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortRFIDNumber4_DataReceived);
            // 
            // serialPortRFIDNumber5
            // 
            this.serialPortRFIDNumber5.BaudRate = 57600;
            this.serialPortRFIDNumber5.PortName = "COM6";
            this.serialPortRFIDNumber5.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortRFIDNumber5_DataReceived);
            // 
            // serialPortRFIDNumber6
            // 
            this.serialPortRFIDNumber6.BaudRate = 57600;
            this.serialPortRFIDNumber6.PortName = "COM7";
            this.serialPortRFIDNumber6.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortRFIDNumber6_DataReceived);
            // 
            // butgoing
            // 
            this.butgoing.Location = new System.Drawing.Point(232, 136);
            this.butgoing.Name = "butgoing";
            this.butgoing.Size = new System.Drawing.Size(52, 35);
            this.butgoing.TabIndex = 12;
            this.butgoing.Text = "前进 >>";
            this.butgoing.UseVisualStyleBackColor = true;
            this.butgoing.Click += new System.EventHandler(this.butgoing_Click);
            // 
            // butstop
            // 
            this.butstop.Location = new System.Drawing.Point(132, 136);
            this.butstop.Name = "butstop";
            this.butstop.Size = new System.Drawing.Size(52, 35);
            this.butstop.TabIndex = 13;
            this.butstop.Text = "停止 ||";
            this.butstop.UseVisualStyleBackColor = true;
            this.butstop.Click += new System.EventHandler(this.butstop_Click);
            // 
            // butback
            // 
            this.butback.Location = new System.Drawing.Point(16, 136);
            this.butback.Name = "butback";
            this.butback.Size = new System.Drawing.Size(68, 35);
            this.butback.TabIndex = 14;
            this.butback.Text = "<< 后退";
            this.butback.UseVisualStyleBackColor = true;
            this.butback.Click += new System.EventHandler(this.butback_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 9F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(7, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "输送带测试";
            // 
            // FrmRFIDBoxScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 634);
            this.Controls.Add(this.gbStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbBoxInfo);
            this.Controls.Add(this.gbBoxs);
            this.Controls.Add(this.gbScanPort);
            this.Controls.Add(this.bgCartonInfo);
            this.Name = "FrmRFIDBoxScan";
            this.Text = "RFID 整箱扫描";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmRFIDBoxScan_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRFIDBoxScan_FormClosing);
            this.Load += new System.EventHandler(this.FrmRFIDBoxScan_Load);
            this.Resize += new System.EventHandler(this.FrmRFIDBoxScan_Resize);
            this.bgCartonInfo.ResumeLayout(false);
            this.bgCartonInfo.PerformLayout();
            this.gbScanPort.ResumeLayout(false);
            this.gbScanPort.PerformLayout();
            this.gbBoxs.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxDetails)).EndInit();
            this.gbBoxInfo.ResumeLayout(false);
            this.pnlMsg.ResumeLayout(false);
            this.pnlMsg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInventoryInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInventoryCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRunTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTag)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.gbStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button butSaveLogs;
        private System.Windows.Forms.GroupBox bgCartonInfo;
        private System.Windows.Forms.TextBox txtCartonNoscanQty;
        private System.Windows.Forms.Label labCartonScanQty;
        private System.Windows.Forms.Label labCartonQty;
        private System.Windows.Forms.TextBox txtCartonScanQty;
        private System.Windows.Forms.TextBox txtCartonQty;
        private System.Windows.Forms.TextBox txtCustID;
        private System.Windows.Forms.Label labCartonNoscanQty;
        private System.Windows.Forms.Label labCustID;
        private System.Windows.Forms.Label labScanSizeQtys;
        private System.Windows.Forms.TextBox txtScanSizeQtys;
        private System.Windows.Forms.TextBox txtSizeQtys;
        private System.Windows.Forms.Label labSizeQtys;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.TextBox txtPO;
        private System.Windows.Forms.Label labStyle;
        private System.Windows.Forms.Label labPO;
        private System.Windows.Forms.Label labSizes;
        private System.Windows.Forms.Label labColor;
        private System.Windows.Forms.Label labMsg;
        private System.Windows.Forms.Label labCarton;
        private System.Windows.Forms.TextBox txtCartonNumber;
        private System.Windows.Forms.GroupBox gbScanPort;
        private System.IO.Ports.SerialPort serialPortRFIDNumber1;
        private System.Windows.Forms.GroupBox gbBoxs;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvBoxDetails;
        private System.Windows.Forms.Label labCartonNumber;
        private System.Windows.Forms.Label labScanQtys;
        private System.Windows.Forms.GroupBox gbBoxInfo;
        private System.Windows.Forms.TextBox txtRFIDNumber;
        private System.Windows.Forms.Label labRFID;
        private System.Windows.Forms.Label labStatusRuning;
        private System.Windows.Forms.Label labAlarmStatus;
        private System.Windows.Forms.Label labCartonReaderStatus;
        private System.Windows.Forms.Label labRFID3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel pnlMsg;
        private System.Windows.Forms.Label ledAllTimes;
        private System.Windows.Forms.Label ledAllTags;
        private System.Windows.Forms.Label ledInventoryTimes;
        private System.Windows.Forms.Label ledCurrentAnt;
        private System.Windows.Forms.Label ledInventoryTags;
        private System.Windows.Forms.Label lblCurrentAnt;
        private System.Windows.Forms.NumericUpDown nudInventoryInterval;
        private System.Windows.Forms.NumericUpDown nudInventoryCount;
        private System.Windows.Forms.Label lblInventoryCount;
        private System.Windows.Forms.NumericUpDown nudRunTimes;
        private System.Windows.Forms.Label lblRunTimes;
        private System.Windows.Forms.NumericUpDown nudMaxTag;
        private System.Windows.Forms.Label lblAllTimes;
        private System.Windows.Forms.Label lblInventoryInterval;
        private System.Windows.Forms.Label lblMaxTag;
        private System.Windows.Forms.Label lblInventoryTimes;
        private System.Windows.Forms.Label lblInventoryTags;
        private System.Windows.Forms.Label lblAllTags;
        private System.Windows.Forms.ListView dgvBoxHeads;
        private System.Windows.Forms.ColumnHeader cNumber;
        private System.Windows.Forms.ColumnHeader cEPC;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butReScan;
        private System.IO.Ports.SerialPort serialPortRFIDNumber2;
        private System.IO.Ports.SerialPort serialPortRFIDNumber3;
        private System.Windows.Forms.Label labRFID1;
        private System.Windows.Forms.Label labRFID2;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.Label labPowerControlStatus;
        private System.Windows.Forms.Label labRFID5;
        private System.Windows.Forms.Label labRFID4;
        private System.Windows.Forms.Label labRFID6;
        private System.IO.Ports.SerialPort serialPortRFIDNumber4;
        private System.IO.Ports.SerialPort serialPortRFIDNumber5;
        private System.IO.Ports.SerialPort serialPortRFIDNumber6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butback;
        private System.Windows.Forms.Button butstop;
        private System.Windows.Forms.Button butgoing;
    }
}