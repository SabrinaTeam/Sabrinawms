namespace WinForm
{
    partial class FrmLuluSingleScan
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
            this.txtCartonNumber = new System.Windows.Forms.TextBox();
            this.labCarton = new System.Windows.Forms.Label();
            this.serialPortPolybagNumber = new System.IO.Ports.SerialPort(this.components);
            this.serialPortRFIDNumber = new System.IO.Ports.SerialPort(this.components);
            this.labPolybag = new System.Windows.Forms.Label();
            this.labWWMT = new System.Windows.Forms.Label();
            this.labRFID = new System.Windows.Forms.Label();
            this.txtPolybagNumber = new System.Windows.Forms.TextBox();
            this.txtWwmtNumber = new System.Windows.Forms.TextBox();
            this.txtRFIDNumber = new System.Windows.Forms.TextBox();
            this.bgScanNumbers = new System.Windows.Forms.GroupBox();
            this.labStatusRuning = new System.Windows.Forms.Label();
            this.labAlarmStatus = new System.Windows.Forms.Label();
            this.labCartonReaderStatus = new System.Windows.Forms.Label();
            this.labPolybagStatus = new System.Windows.Forms.Label();
            this.labRFIDReaderStatus = new System.Windows.Forms.Label();
            this.labMsg = new System.Windows.Forms.Label();
            this.bgCartonInfo = new System.Windows.Forms.GroupBox();
            this.txtCustID = new System.Windows.Forms.TextBox();
            this.labCustID = new System.Windows.Forms.Label();
            this.labScanSizeQtys = new System.Windows.Forms.Label();
            this.txtScanSizeQtys = new System.Windows.Forms.TextBox();
            this.txtSizeQtys = new System.Windows.Forms.TextBox();
            this.labSizeQtys = new System.Windows.Forms.Label();
            this.txtCartonNoscanQty = new System.Windows.Forms.TextBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.labCartonNoscanQty = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.txtCartonQty = new System.Windows.Forms.TextBox();
            this.txtPO = new System.Windows.Forms.TextBox();
            this.txtCartonScanQty = new System.Windows.Forms.TextBox();
            this.labStyle = new System.Windows.Forms.Label();
            this.labCartonQty = new System.Windows.Forms.Label();
            this.labPO = new System.Windows.Forms.Label();
            this.labCartonScanQty = new System.Windows.Forms.Label();
            this.labSizes = new System.Windows.Forms.Label();
            this.labColor = new System.Windows.Forms.Label();
            this.bgCartonLogs = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.butSaveLogs = new System.Windows.Forms.Button();
            this.dgvScanLogs = new System.Windows.Forms.DataGridView();
            this.labQtys = new System.Windows.Forms.Label();
            this.labCartonNumber = new System.Windows.Forms.Label();
            this.labCartonN = new System.Windows.Forms.Label();
            this.labCantons = new System.Windows.Forms.Label();
            this.labCartonNs = new System.Windows.Forms.Label();
            this.bgScaninfo = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labPolySize = new System.Windows.Forms.Label();
            this.labSizeQty = new System.Windows.Forms.Label();
            this.labRFIDNumber = new System.Windows.Forms.Label();
            this.labPolybagNumber = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgScanNumbers.SuspendLayout();
            this.bgCartonInfo.SuspendLayout();
            this.bgCartonLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScanLogs)).BeginInit();
            this.bgScaninfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCartonNumber
            // 
            this.txtCartonNumber.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCartonNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCartonNumber.Location = new System.Drawing.Point(104, 37);
            this.txtCartonNumber.Name = "txtCartonNumber";
            this.txtCartonNumber.Size = new System.Drawing.Size(189, 23);
            this.txtCartonNumber.TabIndex = 0;
            this.txtCartonNumber.TextChanged += new System.EventHandler(this.txtCartonNumber_TextChanged);
            this.txtCartonNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCartonNumber_KeyDown);
            // 
            // labCarton
            // 
            this.labCarton.AutoSize = true;
            this.labCarton.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCarton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCarton.Location = new System.Drawing.Point(23, 40);
            this.labCarton.Name = "labCarton";
            this.labCarton.Size = new System.Drawing.Size(75, 16);
            this.labCarton.TabIndex = 1;
            this.labCarton.Text = "外箱条码:";
            // 
            // serialPortPolybagNumber
            // 
            this.serialPortPolybagNumber.PortName = "COM3";
            this.serialPortPolybagNumber.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortPolybagNumber_DataReceived);
            // 
            // serialPortRFIDNumber
            // 
            this.serialPortRFIDNumber.PortName = "COM5";
            this.serialPortRFIDNumber.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortRFIDNumber_DataReceived);
            // 
            // labPolybag
            // 
            this.labPolybag.AutoSize = true;
            this.labPolybag.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labPolybag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labPolybag.Location = new System.Drawing.Point(7, 65);
            this.labPolybag.Name = "labPolybag";
            this.labPolybag.Size = new System.Drawing.Size(91, 16);
            this.labPolybag.TabIndex = 3;
            this.labPolybag.Text = "内包装贴纸:";
            // 
            // labWWMT
            // 
            this.labWWMT.AutoSize = true;
            this.labWWMT.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labWWMT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labWWMT.Location = new System.Drawing.Point(23, 92);
            this.labWWMT.Name = "labWWMT";
            this.labWWMT.Size = new System.Drawing.Size(75, 16);
            this.labWWMT.TabIndex = 4;
            this.labWWMT.Text = "吊卡贴纸:";
            // 
            // labRFID
            // 
            this.labRFID.AutoSize = true;
            this.labRFID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labRFID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labRFID.Location = new System.Drawing.Point(21, 117);
            this.labRFID.Name = "labRFID";
            this.labRFID.Size = new System.Drawing.Size(77, 16);
            this.labRFID.TabIndex = 5;
            this.labRFID.Text = "吊卡RFID:";
            // 
            // txtPolybagNumber
            // 
            this.txtPolybagNumber.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPolybagNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPolybagNumber.Location = new System.Drawing.Point(104, 62);
            this.txtPolybagNumber.Name = "txtPolybagNumber";
            this.txtPolybagNumber.Size = new System.Drawing.Size(189, 23);
            this.txtPolybagNumber.TabIndex = 6;
            this.txtPolybagNumber.TextChanged += new System.EventHandler(this.txtPolybagNumber_TextChanged);
            this.txtPolybagNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPolybagNumber_KeyDown);
            // 
            // txtWwmtNumber
            // 
            this.txtWwmtNumber.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtWwmtNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtWwmtNumber.Location = new System.Drawing.Point(104, 89);
            this.txtWwmtNumber.Name = "txtWwmtNumber";
            this.txtWwmtNumber.ReadOnly = true;
            this.txtWwmtNumber.Size = new System.Drawing.Size(189, 23);
            this.txtWwmtNumber.TabIndex = 7;
            // 
            // txtRFIDNumber
            // 
            this.txtRFIDNumber.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtRFIDNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtRFIDNumber.Location = new System.Drawing.Point(104, 114);
            this.txtRFIDNumber.Name = "txtRFIDNumber";
            this.txtRFIDNumber.Size = new System.Drawing.Size(189, 23);
            this.txtRFIDNumber.TabIndex = 8;
            this.txtRFIDNumber.TextChanged += new System.EventHandler(this.txtRFIDNumber_TextChanged);
            this.txtRFIDNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRFIDNumber_KeyDown);
            // 
            // bgScanNumbers
            // 
            this.bgScanNumbers.BackColor = System.Drawing.SystemColors.Control;
            this.bgScanNumbers.Controls.Add(this.labStatusRuning);
            this.bgScanNumbers.Controls.Add(this.labAlarmStatus);
            this.bgScanNumbers.Controls.Add(this.labCartonReaderStatus);
            this.bgScanNumbers.Controls.Add(this.labPolybagStatus);
            this.bgScanNumbers.Controls.Add(this.labRFIDReaderStatus);
            this.bgScanNumbers.Controls.Add(this.labMsg);
            this.bgScanNumbers.Controls.Add(this.txtRFIDNumber);
            this.bgScanNumbers.Controls.Add(this.txtWwmtNumber);
            this.bgScanNumbers.Controls.Add(this.txtPolybagNumber);
            this.bgScanNumbers.Controls.Add(this.labRFID);
            this.bgScanNumbers.Controls.Add(this.labWWMT);
            this.bgScanNumbers.Controls.Add(this.labPolybag);
            this.bgScanNumbers.Controls.Add(this.labCarton);
            this.bgScanNumbers.Controls.Add(this.txtCartonNumber);
            this.bgScanNumbers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bgScanNumbers.Location = new System.Drawing.Point(9, 334);
            this.bgScanNumbers.Name = "bgScanNumbers";
            this.bgScanNumbers.Size = new System.Drawing.Size(299, 201);
            this.bgScanNumbers.TabIndex = 9;
            this.bgScanNumbers.TabStop = false;
            this.bgScanNumbers.Text = "扫描组件";
            // 
            // labStatusRuning
            // 
            this.labStatusRuning.Location = new System.Drawing.Point(9, 145);
            this.labStatusRuning.Name = "labStatusRuning";
            this.labStatusRuning.Size = new System.Drawing.Size(59, 46);
            this.labStatusRuning.TabIndex = 14;
            this.labStatusRuning.Text = "Status";
            this.labStatusRuning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labStatusRuning.Click += new System.EventHandler(this.labStatusRuning_Click);
            // 
            // labAlarmStatus
            // 
            this.labAlarmStatus.Location = new System.Drawing.Point(185, 171);
            this.labAlarmStatus.Name = "labAlarmStatus";
            this.labAlarmStatus.Size = new System.Drawing.Size(100, 23);
            this.labAlarmStatus.TabIndex = 13;
            this.labAlarmStatus.Text = "Alarm";
            this.labAlarmStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCartonReaderStatus
            // 
            this.labCartonReaderStatus.Location = new System.Drawing.Point(79, 145);
            this.labCartonReaderStatus.Name = "labCartonReaderStatus";
            this.labCartonReaderStatus.Size = new System.Drawing.Size(100, 23);
            this.labCartonReaderStatus.TabIndex = 12;
            this.labCartonReaderStatus.Text = "CartonReader";
            this.labCartonReaderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labPolybagStatus
            // 
            this.labPolybagStatus.Location = new System.Drawing.Point(185, 145);
            this.labPolybagStatus.Name = "labPolybagStatus";
            this.labPolybagStatus.Size = new System.Drawing.Size(100, 23);
            this.labPolybagStatus.TabIndex = 11;
            this.labPolybagStatus.Text = "PolybagReader";
            this.labPolybagStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labRFIDReaderStatus
            // 
            this.labRFIDReaderStatus.Location = new System.Drawing.Point(79, 171);
            this.labRFIDReaderStatus.Name = "labRFIDReaderStatus";
            this.labRFIDReaderStatus.Size = new System.Drawing.Size(100, 23);
            this.labRFIDReaderStatus.TabIndex = 10;
            this.labRFIDReaderStatus.Text = "RFIDReader";
            this.labRFIDReaderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMsg
            // 
            this.labMsg.AutoSize = true;
            this.labMsg.Font = new System.Drawing.Font("新細明體", 12F);
            this.labMsg.ForeColor = System.Drawing.Color.Red;
            this.labMsg.Location = new System.Drawing.Point(13, 16);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(39, 16);
            this.labMsg.TabIndex = 9;
            this.labMsg.Text = "提示";
            // 
            // bgCartonInfo
            // 
            this.bgCartonInfo.BackColor = System.Drawing.SystemColors.Control;
            this.bgCartonInfo.Controls.Add(this.txtCustID);
            this.bgCartonInfo.Controls.Add(this.labCustID);
            this.bgCartonInfo.Controls.Add(this.labScanSizeQtys);
            this.bgCartonInfo.Controls.Add(this.txtScanSizeQtys);
            this.bgCartonInfo.Controls.Add(this.txtSizeQtys);
            this.bgCartonInfo.Controls.Add(this.labSizeQtys);
            this.bgCartonInfo.Controls.Add(this.txtCartonNoscanQty);
            this.bgCartonInfo.Controls.Add(this.txtSize);
            this.bgCartonInfo.Controls.Add(this.labCartonNoscanQty);
            this.bgCartonInfo.Controls.Add(this.txtColor);
            this.bgCartonInfo.Controls.Add(this.txtStyle);
            this.bgCartonInfo.Controls.Add(this.txtCartonQty);
            this.bgCartonInfo.Controls.Add(this.txtPO);
            this.bgCartonInfo.Controls.Add(this.txtCartonScanQty);
            this.bgCartonInfo.Controls.Add(this.labStyle);
            this.bgCartonInfo.Controls.Add(this.labCartonQty);
            this.bgCartonInfo.Controls.Add(this.labPO);
            this.bgCartonInfo.Controls.Add(this.labCartonScanQty);
            this.bgCartonInfo.Controls.Add(this.labSizes);
            this.bgCartonInfo.Controls.Add(this.labColor);
            this.bgCartonInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bgCartonInfo.Location = new System.Drawing.Point(9, 7);
            this.bgCartonInfo.Name = "bgCartonInfo";
            this.bgCartonInfo.Size = new System.Drawing.Size(299, 321);
            this.bgCartonInfo.TabIndex = 10;
            this.bgCartonInfo.TabStop = false;
            this.bgCartonInfo.Text = "外箱信息";
            // 
            // txtCustID
            // 
            this.txtCustID.BackColor = System.Drawing.Color.White;
            this.txtCustID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCustID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCustID.Location = new System.Drawing.Point(90, 28);
            this.txtCustID.Multiline = true;
            this.txtCustID.Name = "txtCustID";
            this.txtCustID.ReadOnly = true;
            this.txtCustID.Size = new System.Drawing.Size(199, 22);
            this.txtCustID.TabIndex = 23;
            this.txtCustID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labCustID
            // 
            this.labCustID.AutoSize = true;
            this.labCustID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCustID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCustID.Location = new System.Drawing.Point(28, 31);
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
            this.labScanSizeQtys.Location = new System.Drawing.Point(5, 224);
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
            this.txtScanSizeQtys.Location = new System.Drawing.Point(90, 216);
            this.txtScanSizeQtys.Multiline = true;
            this.txtScanSizeQtys.Name = "txtScanSizeQtys";
            this.txtScanSizeQtys.ReadOnly = true;
            this.txtScanSizeQtys.Size = new System.Drawing.Size(199, 22);
            this.txtScanSizeQtys.TabIndex = 20;
            this.txtScanSizeQtys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSizeQtys
            // 
            this.txtSizeQtys.BackColor = System.Drawing.Color.White;
            this.txtSizeQtys.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSizeQtys.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSizeQtys.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSizeQtys.Location = new System.Drawing.Point(90, 186);
            this.txtSizeQtys.Multiline = true;
            this.txtSizeQtys.Name = "txtSizeQtys";
            this.txtSizeQtys.ReadOnly = true;
            this.txtSizeQtys.Size = new System.Drawing.Size(199, 22);
            this.txtSizeQtys.TabIndex = 19;
            this.txtSizeQtys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labSizeQtys
            // 
            this.labSizeQtys.AutoSize = true;
            this.labSizeQtys.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labSizeQtys.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labSizeQtys.Location = new System.Drawing.Point(16, 193);
            this.labSizeQtys.Name = "labSizeQtys";
            this.labSizeQtys.Size = new System.Drawing.Size(71, 16);
            this.labSizeQtys.TabIndex = 18;
            this.labSizeQtys.Text = "尺码数量";
            // 
            // txtCartonNoscanQty
            // 
            this.txtCartonNoscanQty.BackColor = System.Drawing.Color.White;
            this.txtCartonNoscanQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCartonNoscanQty.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCartonNoscanQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCartonNoscanQty.Location = new System.Drawing.Point(199, 283);
            this.txtCartonNoscanQty.Multiline = true;
            this.txtCartonNoscanQty.Name = "txtCartonNoscanQty";
            this.txtCartonNoscanQty.ReadOnly = true;
            this.txtCartonNoscanQty.Size = new System.Drawing.Size(72, 31);
            this.txtCartonNoscanQty.TabIndex = 17;
            this.txtCartonNoscanQty.Text = "0";
            this.txtCartonNoscanQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSize
            // 
            this.txtSize.BackColor = System.Drawing.Color.White;
            this.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSize.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSize.Location = new System.Drawing.Point(90, 156);
            this.txtSize.Multiline = true;
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(199, 22);
            this.txtSize.TabIndex = 12;
            this.txtSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labCartonNoscanQty
            // 
            this.labCartonNoscanQty.AutoSize = true;
            this.labCartonNoscanQty.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCartonNoscanQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonNoscanQty.Location = new System.Drawing.Point(196, 259);
            this.labCartonNoscanQty.Name = "labCartonNoscanQty";
            this.labCartonNoscanQty.Size = new System.Drawing.Size(75, 16);
            this.labCartonNoscanQty.TabIndex = 16;
            this.labCartonNoscanQty.Text = "待扫件数";
            // 
            // txtColor
            // 
            this.txtColor.BackColor = System.Drawing.Color.White;
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtColor.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtColor.Location = new System.Drawing.Point(90, 126);
            this.txtColor.Multiline = true;
            this.txtColor.Name = "txtColor";
            this.txtColor.ReadOnly = true;
            this.txtColor.Size = new System.Drawing.Size(199, 22);
            this.txtColor.TabIndex = 11;
            this.txtColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtStyle
            // 
            this.txtStyle.BackColor = System.Drawing.Color.White;
            this.txtStyle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStyle.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtStyle.Location = new System.Drawing.Point(90, 96);
            this.txtStyle.Multiline = true;
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(199, 22);
            this.txtStyle.TabIndex = 10;
            this.txtStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCartonQty
            // 
            this.txtCartonQty.BackColor = System.Drawing.Color.White;
            this.txtCartonQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCartonQty.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCartonQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCartonQty.Location = new System.Drawing.Point(12, 283);
            this.txtCartonQty.Multiline = true;
            this.txtCartonQty.Name = "txtCartonQty";
            this.txtCartonQty.ReadOnly = true;
            this.txtCartonQty.Size = new System.Drawing.Size(72, 31);
            this.txtCartonQty.TabIndex = 15;
            this.txtCartonQty.Text = "0";
            this.txtCartonQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPO
            // 
            this.txtPO.BackColor = System.Drawing.Color.White;
            this.txtPO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPO.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPO.Location = new System.Drawing.Point(90, 66);
            this.txtPO.Multiline = true;
            this.txtPO.Name = "txtPO";
            this.txtPO.ReadOnly = true;
            this.txtPO.Size = new System.Drawing.Size(199, 22);
            this.txtPO.TabIndex = 9;
            this.txtPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCartonScanQty
            // 
            this.txtCartonScanQty.BackColor = System.Drawing.Color.White;
            this.txtCartonScanQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCartonScanQty.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCartonScanQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCartonScanQty.Location = new System.Drawing.Point(102, 283);
            this.txtCartonScanQty.Multiline = true;
            this.txtCartonScanQty.Name = "txtCartonScanQty";
            this.txtCartonScanQty.ReadOnly = true;
            this.txtCartonScanQty.Size = new System.Drawing.Size(72, 31);
            this.txtCartonScanQty.TabIndex = 14;
            this.txtCartonScanQty.Text = "0";
            this.txtCartonScanQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labStyle
            // 
            this.labStyle.AutoSize = true;
            this.labStyle.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labStyle.Location = new System.Drawing.Point(48, 100);
            this.labStyle.Name = "labStyle";
            this.labStyle.Size = new System.Drawing.Size(39, 16);
            this.labStyle.TabIndex = 5;
            this.labStyle.Text = "款式";
            // 
            // labCartonQty
            // 
            this.labCartonQty.AutoSize = true;
            this.labCartonQty.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCartonQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonQty.Location = new System.Drawing.Point(11, 259);
            this.labCartonQty.Name = "labCartonQty";
            this.labCartonQty.Size = new System.Drawing.Size(75, 16);
            this.labCartonQty.TabIndex = 3;
            this.labCartonQty.Text = "本箱件数";
            // 
            // labPO
            // 
            this.labPO.AutoSize = true;
            this.labPO.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labPO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labPO.Location = new System.Drawing.Point(61, 69);
            this.labPO.Name = "labPO";
            this.labPO.Size = new System.Drawing.Size(26, 16);
            this.labPO.TabIndex = 4;
            this.labPO.Text = "PO";
            // 
            // labCartonScanQty
            // 
            this.labCartonScanQty.AutoSize = true;
            this.labCartonScanQty.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCartonScanQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonScanQty.Location = new System.Drawing.Point(98, 259);
            this.labCartonScanQty.Name = "labCartonScanQty";
            this.labCartonScanQty.Size = new System.Drawing.Size(75, 16);
            this.labCartonScanQty.TabIndex = 8;
            this.labCartonScanQty.Text = "已扫件数";
            // 
            // labSizes
            // 
            this.labSizes.AutoSize = true;
            this.labSizes.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labSizes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labSizes.Location = new System.Drawing.Point(16, 162);
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
            this.labColor.Location = new System.Drawing.Point(48, 131);
            this.labColor.Name = "labColor";
            this.labColor.Size = new System.Drawing.Size(39, 16);
            this.labColor.TabIndex = 1;
            this.labColor.Text = "颜色";
            // 
            // bgCartonLogs
            // 
            this.bgCartonLogs.Controls.Add(this.button3);
            this.bgCartonLogs.Controls.Add(this.button2);
            this.bgCartonLogs.Controls.Add(this.button1);
            this.bgCartonLogs.Controls.Add(this.butSaveLogs);
            this.bgCartonLogs.Controls.Add(this.dgvScanLogs);
            this.bgCartonLogs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bgCartonLogs.Location = new System.Drawing.Point(770, 7);
            this.bgCartonLogs.Name = "bgCartonLogs";
            this.bgCartonLogs.Size = new System.Drawing.Size(233, 528);
            this.bgCartonLogs.TabIndex = 12;
            this.bgCartonLogs.TabStop = false;
            this.bgCartonLogs.Text = "扫描LOG";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(106, 303);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "setDefault";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(106, 274);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "setWrong";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "setAllright";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // butSaveLogs
            // 
            this.butSaveLogs.Location = new System.Drawing.Point(171, 9);
            this.butSaveLogs.Name = "butSaveLogs";
            this.butSaveLogs.Size = new System.Drawing.Size(75, 23);
            this.butSaveLogs.TabIndex = 1;
            this.butSaveLogs.Text = "保存日志文件";
            this.butSaveLogs.UseVisualStyleBackColor = true;
            this.butSaveLogs.Visible = false;
            this.butSaveLogs.Click += new System.EventHandler(this.butSaveLogs_Click);
            // 
            // dgvScanLogs
            // 
            this.dgvScanLogs.AllowUserToAddRows = false;
            this.dgvScanLogs.AllowUserToDeleteRows = false;
            this.dgvScanLogs.AllowUserToResizeColumns = false;
            this.dgvScanLogs.AllowUserToResizeRows = false;
            this.dgvScanLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScanLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvScanLogs.Location = new System.Drawing.Point(3, 18);
            this.dgvScanLogs.Name = "dgvScanLogs";
            this.dgvScanLogs.ReadOnly = true;
            this.dgvScanLogs.RowTemplate.Height = 24;
            this.dgvScanLogs.Size = new System.Drawing.Size(227, 507);
            this.dgvScanLogs.TabIndex = 0;
            this.dgvScanLogs.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvScanLogs_RowPostPaint);
            // 
            // labQtys
            // 
            this.labQtys.BackColor = System.Drawing.SystemColors.Control;
            this.labQtys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labQtys.Font = new System.Drawing.Font("新細明體", 128.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labQtys.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labQtys.Location = new System.Drawing.Point(8, 194);
            this.labQtys.Name = "labQtys";
            this.labQtys.Size = new System.Drawing.Size(439, 331);
            this.labQtys.TabIndex = 18;
            this.labQtys.Text = "0";
            this.labQtys.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCartonNumber
            // 
            this.labCartonNumber.BackColor = System.Drawing.SystemColors.Control;
            this.labCartonNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labCartonNumber.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.labCartonNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonNumber.Location = new System.Drawing.Point(9, 28);
            this.labCartonNumber.Name = "labCartonNumber";
            this.labCartonNumber.Size = new System.Drawing.Size(298, 44);
            this.labCartonNumber.TabIndex = 19;
            this.labCartonNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCartonN
            // 
            this.labCartonN.BackColor = System.Drawing.SystemColors.Control;
            this.labCartonN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labCartonN.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.labCartonN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonN.Location = new System.Drawing.Point(313, 28);
            this.labCartonN.Name = "labCartonN";
            this.labCartonN.Size = new System.Drawing.Size(134, 44);
            this.labCartonN.TabIndex = 20;
            this.labCartonN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCantons
            // 
            this.labCantons.AutoSize = true;
            this.labCantons.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCantons.Location = new System.Drawing.Point(12, 16);
            this.labCantons.Name = "labCantons";
            this.labCantons.Size = new System.Drawing.Size(53, 12);
            this.labCantons.TabIndex = 21;
            this.labCantons.Text = "外箱条码";
            // 
            // labCartonNs
            // 
            this.labCartonNs.AutoSize = true;
            this.labCartonNs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labCartonNs.Location = new System.Drawing.Point(339, 15);
            this.labCartonNs.Name = "labCartonNs";
            this.labCartonNs.Size = new System.Drawing.Size(29, 12);
            this.labCartonNs.TabIndex = 22;
            this.labCartonNs.Text = "箱次";
            // 
            // bgScaninfo
            // 
            this.bgScaninfo.BackColor = System.Drawing.SystemColors.Control;
            this.bgScaninfo.Controls.Add(this.label6);
            this.bgScaninfo.Controls.Add(this.label5);
            this.bgScaninfo.Controls.Add(this.label4);
            this.bgScaninfo.Controls.Add(this.label3);
            this.bgScaninfo.Controls.Add(this.labPolySize);
            this.bgScaninfo.Controls.Add(this.labSizeQty);
            this.bgScaninfo.Controls.Add(this.labRFIDNumber);
            this.bgScaninfo.Controls.Add(this.labPolybagNumber);
            this.bgScaninfo.Controls.Add(this.labCartonNs);
            this.bgScaninfo.Controls.Add(this.labCantons);
            this.bgScaninfo.Controls.Add(this.labCartonN);
            this.bgScaninfo.Controls.Add(this.labCartonNumber);
            this.bgScaninfo.Controls.Add(this.labQtys);
            this.bgScaninfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bgScaninfo.Location = new System.Drawing.Point(311, 6);
            this.bgScaninfo.Name = "bgScaninfo";
            this.bgScaninfo.Size = new System.Drawing.Size(453, 529);
            this.bgScaninfo.TabIndex = 23;
            this.bgScaninfo.TabStop = false;
            this.bgScaninfo.Text = "扫描信息";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(11, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "RFID号码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(13, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "吊卡号码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(337, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 31;
            this.label4.Text = "SIZE数量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(243, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "SIZE";
            // 
            // labPolySize
            // 
            this.labPolySize.BackColor = System.Drawing.SystemColors.Control;
            this.labPolySize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labPolySize.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.labPolySize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labPolySize.Location = new System.Drawing.Point(237, 86);
            this.labPolySize.Name = "labPolySize";
            this.labPolySize.Size = new System.Drawing.Size(93, 44);
            this.labPolySize.TabIndex = 29;
            this.labPolySize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSizeQty
            // 
            this.labSizeQty.BackColor = System.Drawing.SystemColors.Control;
            this.labSizeQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labSizeQty.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.labSizeQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labSizeQty.Location = new System.Drawing.Point(336, 85);
            this.labSizeQty.Name = "labSizeQty";
            this.labSizeQty.Size = new System.Drawing.Size(111, 44);
            this.labSizeQty.TabIndex = 28;
            this.labSizeQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labRFIDNumber
            // 
            this.labRFIDNumber.BackColor = System.Drawing.SystemColors.Control;
            this.labRFIDNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labRFIDNumber.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.labRFIDNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labRFIDNumber.Location = new System.Drawing.Point(8, 146);
            this.labRFIDNumber.Name = "labRFIDNumber";
            this.labRFIDNumber.Size = new System.Drawing.Size(439, 44);
            this.labRFIDNumber.TabIndex = 27;
            this.labRFIDNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labPolybagNumber
            // 
            this.labPolybagNumber.BackColor = System.Drawing.SystemColors.Control;
            this.labPolybagNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labPolybagNumber.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.labPolybagNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labPolybagNumber.Location = new System.Drawing.Point(9, 86);
            this.labPolybagNumber.Name = "labPolybagNumber";
            this.labPolybagNumber.Size = new System.Drawing.Size(222, 44);
            this.labPolybagNumber.TabIndex = 26;
            this.labPolybagNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // FrmLuluSingleScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1009, 540);
            this.Controls.Add(this.bgScaninfo);
            this.Controls.Add(this.bgCartonLogs);
            this.Controls.Add(this.bgCartonInfo);
            this.Controls.Add(this.bgScanNumbers);
            this.Name = "FrmLuluSingleScan";
            this.Text = "RFID 单件扫描";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLuluSingleScan_FormClosing);
            this.Load += new System.EventHandler(this.FrmLuluSingleScan_Load);
            this.Resize += new System.EventHandler(this.FrmLuluSingleScan_Resize);
            this.bgScanNumbers.ResumeLayout(false);
            this.bgScanNumbers.PerformLayout();
            this.bgCartonInfo.ResumeLayout(false);
            this.bgCartonInfo.PerformLayout();
            this.bgCartonLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScanLogs)).EndInit();
            this.bgScaninfo.ResumeLayout(false);
            this.bgScaninfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCartonNumber;
        private System.Windows.Forms.Label labCarton;
        private System.IO.Ports.SerialPort serialPortPolybagNumber;
        private System.IO.Ports.SerialPort serialPortRFIDNumber;
        private System.Windows.Forms.Label labPolybag;
        private System.Windows.Forms.Label labWWMT;
        private System.Windows.Forms.Label labRFID;
        private System.Windows.Forms.TextBox txtPolybagNumber;
        private System.Windows.Forms.TextBox txtWwmtNumber;
        private System.Windows.Forms.TextBox txtRFIDNumber;
        private System.Windows.Forms.GroupBox bgScanNumbers;
        private System.Windows.Forms.GroupBox bgCartonInfo;
        private System.Windows.Forms.Label labCartonScanQty;
        private System.Windows.Forms.Label labStyle;
        private System.Windows.Forms.Label labPO;
        private System.Windows.Forms.Label labCartonQty;
        private System.Windows.Forms.Label labSizes;
        private System.Windows.Forms.Label labColor;
        private System.Windows.Forms.TextBox txtCartonNoscanQty;
        private System.Windows.Forms.Label labCartonNoscanQty;
        private System.Windows.Forms.TextBox txtCartonQty;
        private System.Windows.Forms.TextBox txtCartonScanQty;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.TextBox txtPO;
        private System.Windows.Forms.GroupBox bgCartonLogs;
        private System.Windows.Forms.Label labQtys;
        private System.Windows.Forms.Label labCartonNumber;
        private System.Windows.Forms.Label labCartonN;
        private System.Windows.Forms.Label labCantons;
        private System.Windows.Forms.Label labCartonNs;
        private System.Windows.Forms.GroupBox bgScaninfo;
        private System.Windows.Forms.DataGridView dgvScanLogs;
        private System.Windows.Forms.Label labMsg;
        private System.Windows.Forms.Label labRFIDNumber;
        private System.Windows.Forms.Label labPolybagNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Label labPolySize;
        private System.Windows.Forms.Label labSizeQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labScanSizeQtys;
        private System.Windows.Forms.TextBox txtScanSizeQtys;
        private System.Windows.Forms.TextBox txtSizeQtys;
        private System.Windows.Forms.Label labSizeQtys;
        private System.Windows.Forms.TextBox txtCustID;
        private System.Windows.Forms.Label labCustID;
        private System.Windows.Forms.Button butSaveLogs;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labAlarmStatus;
        private System.Windows.Forms.Label labCartonReaderStatus;
        private System.Windows.Forms.Label labPolybagStatus;
        private System.Windows.Forms.Label labRFIDReaderStatus;
        private System.Windows.Forms.Label labStatusRuning;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}