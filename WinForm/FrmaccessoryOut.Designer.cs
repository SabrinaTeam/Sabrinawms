namespace WinForm
{
    partial class FrmaccessoryOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmaccessoryOut));
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.gbViews = new System.Windows.Forms.GroupBox();
            this.cbboxseleceall = new System.Windows.Forms.CheckBox();
            this.dgvAccessory = new System.Windows.Forms.DataGridView();
            this.gbSearchLists = new System.Windows.Forms.GroupBox();
            this.txtReceiveBatch = new System.Windows.Forms.TextBox();
            this.labQty = new System.Windows.Forms.Label();
            this.labUnfinishedQty = new System.Windows.Forms.Label();
            this.labFinishedQty = new System.Windows.Forms.Label();
            this.labQtyName = new System.Windows.Forms.Label();
            this.labPuQty = new System.Windows.Forms.Label();
            this.labUnfinishedQtyName = new System.Windows.Forms.Label();
            this.labFinishedQtyName = new System.Windows.Forms.Label();
            this.labPuQtyName = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btConfirmOut = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtManufactures = new System.Windows.Forms.TextBox();
            this.cklistBoxPO = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGroupColor = new System.Windows.Forms.ComboBox();
            this.txtReceiveNumber = new System.Windows.Forms.TextBox();
            this.cbReceiveNumber = new System.Windows.Forms.CheckBox();
            this.btSearch = new System.Windows.Forms.Button();
            this.txtPurNo = new System.Windows.Forms.TextBox();
            this.txtMyNo = new System.Windows.Forms.TextBox();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.cbPurNo = new System.Windows.Forms.CheckBox();
            this.cbStyle = new System.Windows.Forms.CheckBox();
            this.cbMyNo = new System.Windows.Forms.CheckBox();
            this.cklistSize = new System.Windows.Forms.CheckedListBox();
            this.btPrint = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusLabelNote1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusLabelStatusName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStatusLabelNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusLabelNote2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusLabelAll = new System.Windows.Forms.ToolStripStatusLabel();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.MenuRight.SuspendLayout();
            this.gbViews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccessory)).BeginInit();
            this.gbSearchLists.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
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
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.Size = new System.Drawing.Size(1367, 710);
            // 
            // gbViews
            // 
            this.gbViews.Controls.Add(this.cbboxseleceall);
            this.gbViews.Controls.Add(this.dgvAccessory);
            this.gbViews.Location = new System.Drawing.Point(5, 109);
            this.gbViews.Name = "gbViews";
            this.gbViews.Size = new System.Drawing.Size(1124, 338);
            this.gbViews.TabIndex = 6;
            this.gbViews.TabStop = false;
            this.gbViews.Text = "物料详情";
            // 
            // cbboxseleceall
            // 
            this.cbboxseleceall.AutoSize = true;
            this.cbboxseleceall.Checked = true;
            this.cbboxseleceall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbboxseleceall.Location = new System.Drawing.Point(69, 0);
            this.cbboxseleceall.Name = "cbboxseleceall";
            this.cbboxseleceall.Size = new System.Drawing.Size(48, 16);
            this.cbboxseleceall.TabIndex = 1;
            this.cbboxseleceall.Text = "全选";
            this.cbboxseleceall.UseVisualStyleBackColor = true;
            this.cbboxseleceall.CheckedChanged += new System.EventHandler(this.cbboxseleceall_CheckedChanged);
            // 
            // dgvAccessory
            // 
            this.dgvAccessory.AllowDrop = true;
            this.dgvAccessory.AllowUserToAddRows = false;
            this.dgvAccessory.AllowUserToDeleteRows = false;
            this.dgvAccessory.AllowUserToResizeRows = false;
            this.dgvAccessory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccessory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccessory.Location = new System.Drawing.Point(3, 17);
            this.dgvAccessory.Name = "dgvAccessory";
            this.dgvAccessory.RowTemplate.Height = 23;
            this.dgvAccessory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccessory.Size = new System.Drawing.Size(1118, 318);
            this.dgvAccessory.TabIndex = 0;
            this.dgvAccessory.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccessory_CellEndEdit);
            this.dgvAccessory.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAccessory_CellMouseDown);
            this.dgvAccessory.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvAccessory_CellValidating);
            this.dgvAccessory.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvAccessory_RowPostPaint);
            // 
            // gbSearchLists
            // 
            this.gbSearchLists.Controls.Add(this.txtReceiveBatch);
            this.gbSearchLists.Controls.Add(this.labQty);
            this.gbSearchLists.Controls.Add(this.labUnfinishedQty);
            this.gbSearchLists.Controls.Add(this.labFinishedQty);
            this.gbSearchLists.Controls.Add(this.labQtyName);
            this.gbSearchLists.Controls.Add(this.labPuQty);
            this.gbSearchLists.Controls.Add(this.labUnfinishedQtyName);
            this.gbSearchLists.Controls.Add(this.labFinishedQtyName);
            this.gbSearchLists.Controls.Add(this.labPuQtyName);
            this.gbSearchLists.Controls.Add(this.button1);
            this.gbSearchLists.Controls.Add(this.btConfirmOut);
            this.gbSearchLists.Controls.Add(this.label2);
            this.gbSearchLists.Controls.Add(this.txtManufactures);
            this.gbSearchLists.Controls.Add(this.cklistBoxPO);
            this.gbSearchLists.Controls.Add(this.label5);
            this.gbSearchLists.Controls.Add(this.label4);
            this.gbSearchLists.Controls.Add(this.label1);
            this.gbSearchLists.Controls.Add(this.cbGroupColor);
            this.gbSearchLists.Controls.Add(this.txtReceiveNumber);
            this.gbSearchLists.Controls.Add(this.cbReceiveNumber);
            this.gbSearchLists.Controls.Add(this.btSearch);
            this.gbSearchLists.Controls.Add(this.txtPurNo);
            this.gbSearchLists.Controls.Add(this.txtMyNo);
            this.gbSearchLists.Controls.Add(this.txtStyle);
            this.gbSearchLists.Controls.Add(this.cbPurNo);
            this.gbSearchLists.Controls.Add(this.cbStyle);
            this.gbSearchLists.Controls.Add(this.cbMyNo);
            this.gbSearchLists.Controls.Add(this.cklistSize);
            this.gbSearchLists.Location = new System.Drawing.Point(5, 2);
            this.gbSearchLists.Name = "gbSearchLists";
            this.gbSearchLists.Size = new System.Drawing.Size(704, 105);
            this.gbSearchLists.TabIndex = 5;
            this.gbSearchLists.TabStop = false;
            this.gbSearchLists.Text = "查询条件";
            // 
            // txtReceiveBatch
            // 
            this.txtReceiveBatch.Location = new System.Drawing.Point(415, 57);
            this.txtReceiveBatch.Name = "txtReceiveBatch";
            this.txtReceiveBatch.Size = new System.Drawing.Size(70, 21);
            this.txtReceiveBatch.TabIndex = 42;
            // 
            // labQty
            // 
            this.labQty.AutoSize = true;
            this.labQty.Location = new System.Drawing.Point(464, 88);
            this.labQty.Name = "labQty";
            this.labQty.Size = new System.Drawing.Size(11, 12);
            this.labQty.TabIndex = 41;
            this.labQty.Text = "0";
            this.labQty.Visible = false;
            // 
            // labUnfinishedQty
            // 
            this.labUnfinishedQty.AutoSize = true;
            this.labUnfinishedQty.Location = new System.Drawing.Point(319, 87);
            this.labUnfinishedQty.Name = "labUnfinishedQty";
            this.labUnfinishedQty.Size = new System.Drawing.Size(11, 12);
            this.labUnfinishedQty.TabIndex = 40;
            this.labUnfinishedQty.Text = "0";
            this.labUnfinishedQty.Visible = false;
            // 
            // labFinishedQty
            // 
            this.labFinishedQty.AutoSize = true;
            this.labFinishedQty.Location = new System.Drawing.Point(202, 86);
            this.labFinishedQty.Name = "labFinishedQty";
            this.labFinishedQty.Size = new System.Drawing.Size(11, 12);
            this.labFinishedQty.TabIndex = 39;
            this.labFinishedQty.Text = "0";
            this.labFinishedQty.Visible = false;
            // 
            // labQtyName
            // 
            this.labQtyName.AutoSize = true;
            this.labQtyName.Location = new System.Drawing.Point(385, 87);
            this.labQtyName.Name = "labQtyName";
            this.labQtyName.Size = new System.Drawing.Size(89, 12);
            this.labQtyName.TabIndex = 38;
            this.labQtyName.Text = "本次生产数量：";
            this.labQtyName.Visible = false;
            // 
            // labPuQty
            // 
            this.labPuQty.AutoSize = true;
            this.labPuQty.Location = new System.Drawing.Point(67, 85);
            this.labPuQty.Name = "labPuQty";
            this.labPuQty.Size = new System.Drawing.Size(11, 12);
            this.labPuQty.TabIndex = 37;
            this.labPuQty.Text = "0";
            // 
            // labUnfinishedQtyName
            // 
            this.labUnfinishedQtyName.AutoSize = true;
            this.labUnfinishedQtyName.Location = new System.Drawing.Point(253, 86);
            this.labUnfinishedQtyName.Name = "labUnfinishedQtyName";
            this.labUnfinishedQtyName.Size = new System.Drawing.Size(77, 12);
            this.labUnfinishedQtyName.TabIndex = 36;
            this.labUnfinishedQtyName.Text = "剩余订单数：";
            this.labUnfinishedQtyName.Visible = false;
            // 
            // labFinishedQtyName
            // 
            this.labFinishedQtyName.AutoSize = true;
            this.labFinishedQtyName.Location = new System.Drawing.Point(131, 85);
            this.labFinishedQtyName.Name = "labFinishedQtyName";
            this.labFinishedQtyName.Size = new System.Drawing.Size(77, 12);
            this.labFinishedQtyName.TabIndex = 35;
            this.labFinishedQtyName.Text = "已生产件数：";
            this.labFinishedQtyName.Visible = false;
            // 
            // labPuQtyName
            // 
            this.labPuQtyName.AutoSize = true;
            this.labPuQtyName.Location = new System.Drawing.Point(7, 85);
            this.labPuQtyName.Name = "labPuQtyName";
            this.labPuQtyName.Size = new System.Drawing.Size(65, 12);
            this.labPuQtyName.TabIndex = 34;
            this.labPuQtyName.Text = "订单数量：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(931, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 38);
            this.button1.TabIndex = 33;
            this.button1.Text = "计算发料";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // btConfirmOut
            // 
            this.btConfirmOut.Location = new System.Drawing.Point(590, 14);
            this.btConfirmOut.Name = "btConfirmOut";
            this.btConfirmOut.Size = new System.Drawing.Size(105, 84);
            this.btConfirmOut.TabIndex = 21;
            this.btConfirmOut.Text = "保存提交";
            this.btConfirmOut.UseVisualStyleBackColor = true;
            this.btConfirmOut.Click += new System.EventHandler(this.btConfirmOut_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(927, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "本次生产件数";
            this.label2.Visible = false;
            // 
            // txtManufactures
            // 
            this.txtManufactures.Location = new System.Drawing.Point(929, 36);
            this.txtManufactures.Name = "txtManufactures";
            this.txtManufactures.Size = new System.Drawing.Size(95, 21);
            this.txtManufactures.TabIndex = 31;
            this.txtManufactures.Visible = false;
            this.txtManufactures.TextChanged += new System.EventHandler(this.txtManufactures_TextChanged);
            this.txtManufactures.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtManufactures_KeyDown);
            // 
            // cklistBoxPO
            // 
            this.cklistBoxPO.CheckOnClick = true;
            this.cklistBoxPO.Enabled = false;
            this.cklistBoxPO.FormattingEnabled = true;
            this.cklistBoxPO.Location = new System.Drawing.Point(1074, 51);
            this.cklistBoxPO.Name = "cklistBoxPO";
            this.cklistBoxPO.Size = new System.Drawing.Size(110, 52);
            this.cklistBoxPO.TabIndex = 30;
            this.cklistBoxPO.SelectedIndexChanged += new System.EventHandler(this.cklistBoxPO_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1045, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "po#";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(790, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 27;
            this.label4.Text = "Size:";
            this.label4.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1010, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "生产色组:";
            // 
            // cbGroupColor
            // 
            this.cbGroupColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroupColor.Enabled = false;
            this.cbGroupColor.FormattingEnabled = true;
            this.cbGroupColor.Location = new System.Drawing.Point(1074, 25);
            this.cbGroupColor.Name = "cbGroupColor";
            this.cbGroupColor.Size = new System.Drawing.Size(110, 20);
            this.cbGroupColor.TabIndex = 25;
            this.cbGroupColor.SelectedIndexChanged += new System.EventHandler(this.cbGroupColor_SelectedIndexChanged);
            // 
            // txtReceiveNumber
            // 
            this.txtReceiveNumber.Location = new System.Drawing.Point(299, 56);
            this.txtReceiveNumber.Name = "txtReceiveNumber";
            this.txtReceiveNumber.Size = new System.Drawing.Size(110, 21);
            this.txtReceiveNumber.TabIndex = 6;
            this.txtReceiveNumber.TextChanged += new System.EventHandler(this.txtReceiveNumber_TextChanged);
            // 
            // cbReceiveNumber
            // 
            this.cbReceiveNumber.AutoSize = true;
            this.cbReceiveNumber.Location = new System.Drawing.Point(230, 59);
            this.cbReceiveNumber.Name = "cbReceiveNumber";
            this.cbReceiveNumber.Size = new System.Drawing.Size(72, 16);
            this.cbReceiveNumber.TabIndex = 4;
            this.cbReceiveNumber.Text = "领料单号";
            this.cbReceiveNumber.UseVisualStyleBackColor = true;
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(491, 14);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(93, 84);
            this.btSearch.TabIndex = 12;
            this.btSearch.Text = "查询";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // txtPurNo
            // 
            this.txtPurNo.Location = new System.Drawing.Point(78, 57);
            this.txtPurNo.Name = "txtPurNo";
            this.txtPurNo.Size = new System.Drawing.Size(146, 21);
            this.txtPurNo.TabIndex = 10;
            this.txtPurNo.TextChanged += new System.EventHandler(this.txtPurNo_TextChanged);
            // 
            // txtMyNo
            // 
            this.txtMyNo.Location = new System.Drawing.Point(78, 26);
            this.txtMyNo.Name = "txtMyNo";
            this.txtMyNo.Size = new System.Drawing.Size(146, 21);
            this.txtMyNo.TabIndex = 8;
            this.txtMyNo.TextChanged += new System.EventHandler(this.txtMyNo_TextChanged);
            this.txtMyNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMyNo_KeyDown);
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(299, 26);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.Size = new System.Drawing.Size(186, 21);
            this.txtStyle.TabIndex = 7;
            this.txtStyle.TextChanged += new System.EventHandler(this.txtStyle_TextChanged);
            // 
            // cbPurNo
            // 
            this.cbPurNo.AutoSize = true;
            this.cbPurNo.Location = new System.Drawing.Point(8, 59);
            this.cbPurNo.Name = "cbPurNo";
            this.cbPurNo.Size = new System.Drawing.Size(72, 16);
            this.cbPurNo.TabIndex = 5;
            this.cbPurNo.Text = "采购单号";
            this.cbPurNo.UseVisualStyleBackColor = true;
            // 
            // cbStyle
            // 
            this.cbStyle.AutoSize = true;
            this.cbStyle.Location = new System.Drawing.Point(230, 28);
            this.cbStyle.Name = "cbStyle";
            this.cbStyle.Size = new System.Drawing.Size(72, 16);
            this.cbStyle.TabIndex = 1;
            this.cbStyle.Text = "成品款号";
            this.cbStyle.UseVisualStyleBackColor = true;
            // 
            // cbMyNo
            // 
            this.cbMyNo.AutoSize = true;
            this.cbMyNo.Location = new System.Drawing.Point(8, 28);
            this.cbMyNo.Name = "cbMyNo";
            this.cbMyNo.Size = new System.Drawing.Size(72, 16);
            this.cbMyNo.TabIndex = 0;
            this.cbMyNo.Text = "自编单号";
            this.cbMyNo.UseVisualStyleBackColor = true;
            // 
            // cklistSize
            // 
            this.cklistSize.CheckOnClick = true;
            this.cklistSize.FormattingEnabled = true;
            this.cklistSize.Location = new System.Drawing.Point(792, 34);
            this.cklistSize.Name = "cklistSize";
            this.cklistSize.Size = new System.Drawing.Size(110, 68);
            this.cklistSize.TabIndex = 29;
            this.cklistSize.Visible = false;
            this.cklistSize.SelectedIndexChanged += new System.EventHandler(this.cklistSize_SelectedIndexChanged);
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(715, 9);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(90, 99);
            this.btPrint.TabIndex = 20;
            this.btPrint.Text = "打印";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatusLabelStatus,
            this.toolStatusLabelNote1,
            this.toolStatusLabelStatusName,
            this.toolStatusProgressBar,
            this.toolStatusLabelNow,
            this.toolStatusLabelNote2,
            this.toolStatusLabelAll});
            this.statusStrip.Location = new System.Drawing.Point(0, 450);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1134, 22);
            this.statusStrip.TabIndex = 23;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStatusLabelStatus
            // 
            this.toolStatusLabelStatus.Name = "toolStatusLabelStatus";
            this.toolStatusLabelStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStatusLabelNote1
            // 
            this.toolStatusLabelNote1.Name = "toolStatusLabelNote1";
            this.toolStatusLabelNote1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStatusLabelStatusName
            // 
            this.toolStatusLabelStatusName.Name = "toolStatusLabelStatusName";
            this.toolStatusLabelStatusName.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStatusProgressBar
            // 
            this.toolStatusProgressBar.AutoSize = false;
            this.toolStatusProgressBar.Name = "toolStatusProgressBar";
            this.toolStatusProgressBar.Size = new System.Drawing.Size(300, 16);
            this.toolStatusProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStatusLabelNow
            // 
            this.toolStatusLabelNow.Name = "toolStatusLabelNow";
            this.toolStatusLabelNow.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStatusLabelNote2
            // 
            this.toolStatusLabelNote2.Name = "toolStatusLabelNote2";
            this.toolStatusLabelNote2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStatusLabelAll
            // 
            this.toolStatusLabelAll.Name = "toolStatusLabelAll";
            this.toolStatusLabelAll.Size = new System.Drawing.Size(0, 17);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            this.printPreviewDialog1.Load += new System.EventHandler(this.printPreviewDialog1_Load);
            // 
            // FrmaccessoryOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 472);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.gbViews);
            this.Controls.Add(this.gbSearchLists);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmaccessoryOut";
            this.Text = "辅料发料";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmaccessoryOut_Load);
            this.Resize += new System.EventHandler(this.FrmaccessoryOut_Resize);
            this.MenuRight.ResumeLayout(false);
            this.gbViews.ResumeLayout(false);
            this.gbViews.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccessory)).EndInit();
            this.gbSearchLists.ResumeLayout(false);
            this.gbSearchLists.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
        private System.Windows.Forms.GroupBox gbViews;
        private System.Windows.Forms.DataGridView dgvAccessory;
        private System.Windows.Forms.GroupBox gbSearchLists;
        private System.Windows.Forms.TextBox txtReceiveNumber;
        private System.Windows.Forms.CheckBox cbReceiveNumber;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.TextBox txtPurNo;
        private System.Windows.Forms.TextBox txtMyNo;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.CheckBox cbPurNo;
        private System.Windows.Forms.CheckBox cbStyle;
        private System.Windows.Forms.CheckBox cbMyNo;
        private System.Windows.Forms.Button btConfirmOut;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabelNote1;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabelStatusName;
        private System.Windows.Forms.ToolStripProgressBar toolStatusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabelNow;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabelNote2;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabelAll;
        private System.Windows.Forms.Label labPuQtyName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtManufactures;
        private System.Windows.Forms.CheckedListBox cklistBoxPO;
        private System.Windows.Forms.CheckedListBox cklistSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGroupColor;
        private System.Windows.Forms.Label labUnfinishedQtyName;
        private System.Windows.Forms.Label labFinishedQtyName;
        private System.Windows.Forms.Label labPuQty;
        private System.Windows.Forms.CheckBox cbboxseleceall;
        private System.Windows.Forms.Label labQty;
        private System.Windows.Forms.Label labUnfinishedQty;
        private System.Windows.Forms.Label labFinishedQty;
        private System.Windows.Forms.Label labQtyName;
        private System.Windows.Forms.TextBox txtReceiveBatch;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
    }
}