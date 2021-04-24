namespace WinForm
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuSys = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPP = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSizeRun = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNumber = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMesEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFP = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPropertyNumber = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPDAManager = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAgoProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCompletedToMes = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCompletedSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuInvoiceSendTest = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMain = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAccessory = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAccessoryOut = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOutgoing = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDeliveryCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPOTrading = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPONikeConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTNFImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuTNFScan = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDelScan = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.MenuCompletedSyncMes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSys,
            this.MenuPP,
            this.MenuFP,
            this.MenuAgoProcess,
            this.MenuMain,
            this.MenuAccessory,
            this.MenuProduct,
            this.MenuWindow});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.MenuWindow;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1007, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // MenuSys
            // 
            this.MenuSys.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMenuExit});
            this.MenuSys.Name = "MenuSys";
            this.MenuSys.Size = new System.Drawing.Size(58, 20);
            this.MenuSys.Text = "程式(&S)";
            // 
            // TSMenuExit
            // 
            this.TSMenuExit.Name = "TSMenuExit";
            this.TSMenuExit.Size = new System.Drawing.Size(113, 22);
            this.TSMenuExit.Text = "退出(&E)";
            this.TSMenuExit.Click += new System.EventHandler(this.TSMenuExit_Click);
            // 
            // MenuPP
            // 
            this.MenuPP.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSizeRun,
            this.MenuNumber,
            this.MenuMesEmployee});
            this.MenuPP.Name = "MenuPP";
            this.MenuPP.Size = new System.Drawing.Size(82, 20);
            this.MenuPP.Text = "生产计划(&P)";
            // 
            // MenuSizeRun
            // 
            this.MenuSizeRun.Name = "MenuSizeRun";
            this.MenuSizeRun.Size = new System.Drawing.Size(180, 22);
            this.MenuSizeRun.Text = "SizeRun(&R)";
            this.MenuSizeRun.Click += new System.EventHandler(this.MenuSizeRun_Click);
            // 
            // MenuNumber
            // 
            this.MenuNumber.Name = "MenuNumber";
            this.MenuNumber.Size = new System.Drawing.Size(180, 22);
            this.MenuNumber.Text = "PO&MyNumber(&P)";
            this.MenuNumber.Click += new System.EventHandler(this.MenuNumber_Click);
            // 
            // MenuMesEmployee
            // 
            this.MenuMesEmployee.Name = "MenuMesEmployee";
            this.MenuMesEmployee.Size = new System.Drawing.Size(180, 22);
            this.MenuMesEmployee.Text = "Mes用户管理(&U)";
            this.MenuMesEmployee.Click += new System.EventHandler(this.MenuMesEmployee_Click);
            // 
            // MenuFP
            // 
            this.MenuFP.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuPropertyNumber,
            this.MenuPDAManager});
            this.MenuFP.Name = "MenuFP";
            this.MenuFP.Size = new System.Drawing.Size(81, 20);
            this.MenuFP.Text = "厂务规划(&F)";
            // 
            // MenuPropertyNumber
            // 
            this.MenuPropertyNumber.Name = "MenuPropertyNumber";
            this.MenuPropertyNumber.Size = new System.Drawing.Size(138, 22);
            this.MenuPropertyNumber.Text = "财编打印(&P)";
            this.MenuPropertyNumber.Click += new System.EventHandler(this.MenuPropertyNumber_Click);
            // 
            // MenuPDAManager
            // 
            this.MenuPDAManager.Name = "MenuPDAManager";
            this.MenuPDAManager.Size = new System.Drawing.Size(138, 22);
            this.MenuPDAManager.Text = "PDA管理(&A)";
            this.MenuPDAManager.Click += new System.EventHandler(this.pDA管理ToolStripMenuItem_Click);
            // 
            // MenuAgoProcess
            // 
            this.MenuAgoProcess.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuCompletedToMes,
            this.MenuCompletedSearch,
            this.MenuCompletedSyncMes,
            this.toolStripMenuItem3,
            this.MenuInvoiceSendTest});
            this.MenuAgoProcess.Name = "MenuAgoProcess";
            this.MenuAgoProcess.Size = new System.Drawing.Size(72, 20);
            this.MenuAgoProcess.Text = "前制程(&G)";
            // 
            // MenuCompletedToMes
            // 
            this.MenuCompletedToMes.Name = "MenuCompletedToMes";
            this.MenuCompletedToMes.Size = new System.Drawing.Size(190, 22);
            this.MenuCompletedToMes.Text = "报工送货单打印(&P)";
            this.MenuCompletedToMes.Click += new System.EventHandler(this.MenuCompletedToMes_Click);
            // 
            // MenuCompletedSearch
            // 
            this.MenuCompletedSearch.Name = "MenuCompletedSearch";
            this.MenuCompletedSearch.Size = new System.Drawing.Size(190, 22);
            this.MenuCompletedSearch.Text = "报工查询(&S)";
            this.MenuCompletedSearch.Click += new System.EventHandler(this.MenuCompletedSearch_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(187, 6);
            // 
            // MenuInvoiceSendTest
            // 
            this.MenuInvoiceSendTest.Enabled = false;
            this.MenuInvoiceSendTest.Name = "MenuInvoiceSendTest";
            this.MenuInvoiceSendTest.Size = new System.Drawing.Size(190, 22);
            this.MenuInvoiceSendTest.Text = "信息队列发送测试(&M)";
            this.MenuInvoiceSendTest.Click += new System.EventHandler(this.MenuInvoicePrint_Click);
            // 
            // MenuMain
            // 
            this.MenuMain.Name = "MenuMain";
            this.MenuMain.Size = new System.Drawing.Size(63, 20);
            this.MenuMain.Text = "主料(&M)";
            // 
            // MenuAccessory
            // 
            this.MenuAccessory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAccessoryOut});
            this.MenuAccessory.Name = "MenuAccessory";
            this.MenuAccessory.Size = new System.Drawing.Size(59, 20);
            this.MenuAccessory.Text = "辅料(&A)";
            // 
            // MenuAccessoryOut
            // 
            this.MenuAccessoryOut.Name = "MenuAccessoryOut";
            this.MenuAccessoryOut.Size = new System.Drawing.Size(140, 22);
            this.MenuAccessoryOut.Text = "辅料发料(&O)";
            this.MenuAccessoryOut.Click += new System.EventHandler(this.MenuAccessoryOut_Click);
            // 
            // MenuProduct
            // 
            this.MenuProduct.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuReceipt,
            this.MenuOutgoing,
            this.MenuDeliveryCompare,
            this.MenuPOTrading,
            this.MenuPONikeConnect,
            this.MenuTNFImport,
            this.toolStripMenuItem2,
            this.MenuTNFScan,
            this.MenuDelScan});
            this.MenuProduct.Name = "MenuProduct";
            this.MenuProduct.Size = new System.Drawing.Size(59, 20);
            this.MenuProduct.Text = "成品(&C)";
            // 
            // MenuReceipt
            // 
            this.MenuReceipt.Name = "MenuReceipt";
            this.MenuReceipt.Size = new System.Drawing.Size(239, 22);
            this.MenuReceipt.Text = "无条码收货入库(&R)";
            this.MenuReceipt.Click += new System.EventHandler(this.MenuReceipt_Click);
            // 
            // MenuOutgoing
            // 
            this.MenuOutgoing.Name = "MenuOutgoing";
            this.MenuOutgoing.Size = new System.Drawing.Size(239, 22);
            this.MenuOutgoing.Text = "NIKE 入库查询(&S)";
            this.MenuOutgoing.Click += new System.EventHandler(this.MenuOutgoing_Click);
            // 
            // MenuDeliveryCompare
            // 
            this.MenuDeliveryCompare.Name = "MenuDeliveryCompare";
            this.MenuDeliveryCompare.Size = new System.Drawing.Size(239, 22);
            this.MenuDeliveryCompare.Text = "收货入库比对(&C)";
            this.MenuDeliveryCompare.Click += new System.EventHandler(this.MenuDeliveryCompare_Click);
            // 
            // MenuPOTrading
            // 
            this.MenuPOTrading.Enabled = false;
            this.MenuPOTrading.Name = "MenuPOTrading";
            this.MenuPOTrading.Size = new System.Drawing.Size(239, 22);
            this.MenuPOTrading.Text = "PO# Trading Company PO(&T)";
            this.MenuPOTrading.Click += new System.EventHandler(this.MenuPOTrading_Click);
            // 
            // MenuPONikeConnect
            // 
            this.MenuPONikeConnect.Name = "MenuPONikeConnect";
            this.MenuPONikeConnect.Size = new System.Drawing.Size(239, 22);
            this.MenuPONikeConnect.Text = "PO# NikeConnect(&N)";
            this.MenuPONikeConnect.Click += new System.EventHandler(this.pONikeConnectToolStripMenuItem_Click);
            // 
            // MenuTNFImport
            // 
            this.MenuTNFImport.Name = "MenuTNFImport";
            this.MenuTNFImport.Size = new System.Drawing.Size(239, 22);
            this.MenuTNFImport.Text = "TNF 订单条码资料导入(&T)";
            this.MenuTNFImport.Click += new System.EventHandler(this.MenuTNFImport_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(236, 6);
            // 
            // MenuTNFScan
            // 
            this.MenuTNFScan.Name = "MenuTNFScan";
            this.MenuTNFScan.Size = new System.Drawing.Size(239, 22);
            this.MenuTNFScan.Text = "外箱条码上传(&U)";
            this.MenuTNFScan.Click += new System.EventHandler(this.MenuTNFScan_Click);
            // 
            // MenuDelScan
            // 
            this.MenuDelScan.Name = "MenuDelScan";
            this.MenuDelScan.Size = new System.Drawing.Size(239, 22);
            this.MenuDelScan.Text = "删除条码";
            this.MenuDelScan.Click += new System.EventHandler(this.MenuDelScan_Click);
            // 
            // MenuWindow
            // 
            this.MenuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.MenuCloseAll});
            this.MenuWindow.Name = "MenuWindow";
            this.MenuWindow.Size = new System.Drawing.Size(63, 20);
            this.MenuWindow.Text = "窗口(&W)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // MenuCloseAll
            // 
            this.MenuCloseAll.Name = "MenuCloseAll";
            this.MenuCloseAll.Size = new System.Drawing.Size(162, 22);
            this.MenuCloseAll.Text = "关闭所有窗口(&C)";
            this.MenuCloseAll.Click += new System.EventHandler(this.MenuCloseAll_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Sabrina 仓库管理助手";
            this.notifyIcon1.Visible = true;
            // 
            // MenuCompletedSyncMes
            // 
            this.MenuCompletedSyncMes.Name = "MenuCompletedSyncMes";
            this.MenuCompletedSyncMes.Size = new System.Drawing.Size(190, 22);
            this.MenuCompletedSyncMes.Text = "同步失败查询(&N)";
            this.MenuCompletedSyncMes.Click += new System.EventHandler(this.MenuCompletedSyncMes_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1007, 758);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FrmMain";
            this.Text = "仓库管理助手";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem MenuSys;
        private System.Windows.Forms.ToolStripMenuItem TSMenuExit;
        private System.Windows.Forms.ToolStripMenuItem MenuMain;
        private System.Windows.Forms.ToolStripMenuItem MenuAccessory;
        private System.Windows.Forms.ToolStripMenuItem MenuAccessoryOut;
        private System.Windows.Forms.ToolStripMenuItem MenuWindow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenuCloseAll;
        private System.Windows.Forms.ToolStripMenuItem MenuPP;
        private System.Windows.Forms.ToolStripMenuItem MenuSizeRun;
        private System.Windows.Forms.ToolStripMenuItem MenuProduct;
        private System.Windows.Forms.ToolStripMenuItem MenuOutgoing;
        private System.Windows.Forms.ToolStripMenuItem MenuFP;
        private System.Windows.Forms.ToolStripMenuItem MenuPropertyNumber;
        private System.Windows.Forms.ToolStripMenuItem MenuPOTrading;
        private System.Windows.Forms.ToolStripMenuItem MenuPONikeConnect;
        private System.Windows.Forms.ToolStripMenuItem MenuDeliveryCompare;
        private System.Windows.Forms.ToolStripMenuItem MenuReceipt;
        private System.Windows.Forms.ToolStripMenuItem MenuPDAManager;
        private System.Windows.Forms.ToolStripMenuItem MenuNumber;
        private System.Windows.Forms.ToolStripMenuItem MenuTNFImport;
        private System.Windows.Forms.ToolStripMenuItem MenuTNFScan;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem MenuMesEmployee;
        private System.Windows.Forms.ToolStripMenuItem MenuAgoProcess;
        private System.Windows.Forms.ToolStripMenuItem MenuCompletedToMes;
        private System.Windows.Forms.ToolStripMenuItem MenuInvoiceSendTest;
        private System.Windows.Forms.ToolStripMenuItem MenuCompletedSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem MenuDelScan;
        private System.Windows.Forms.ToolStripMenuItem MenuCompletedSyncMes;
    }
}

