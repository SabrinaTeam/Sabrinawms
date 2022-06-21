using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmMain : Form
    {

        public FrmMain()
        {
            InitializeComponent();
            menuStrip.MdiWindowListItem = this.MenuWindow;
        }

        private void MenuAccessoryOut_Click(object sender, EventArgs e)
        {
            FrmaccessoryOut frm = FrmaccessoryOut.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void TSMenuExit_Click(object sender, EventArgs e)
        {
            const string message ="Are you sure that you would like to close the system?";
            const string caption = "system Closing";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.Yes)
            {
                // cancel the closure of the form.
                Application.Exit();
            }

        }

        private void MenuCloseAll_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length > 0)   //当子窗体个数大于0的时候遍历所有子窗体
            {
                foreach (Form myForm in this.MdiChildren)// 遍历所有子窗体
                    myForm.Close(); //关闭子窗体
            }
        }

        private void MenuSizeRun_Click(object sender, EventArgs e)
        {
            FrmSizeRun frm = FrmSizeRun.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuOutgoing_Click(object sender, EventArgs e)
        {
            FrmOutgoing frm = FrmOutgoing.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuPropertyNumber_Click(object sender, EventArgs e)
        {
            FrmFactoryplanning frm = FrmFactoryplanning.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuPOTrading_Click(object sender, EventArgs e)
        {
            FrmPoTradingComanyPO frm = FrmPoTradingComanyPO.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void pONikeConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNikeConnect frm = FrmNikeConnect.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuDeliveryCompare_Click(object sender, EventArgs e)
        {
            FrmDeliveryCompare frm = FrmDeliveryCompare.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuReceipt_Click(object sender, EventArgs e)
        {
            FrmNoBraCodeReceipt frm = FrmNoBraCodeReceipt.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();

        }

        private void pDA管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPDAManager frm = FrmPDAManager.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuNumber_Click(object sender, EventArgs e)
        {
            FrmPO_MyNo frm = FrmPO_MyNo.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();

        }

        private void MenuTNFImport_Click(object sender, EventArgs e)
        {
            FrmImportVF frm = FrmImportVF.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuTNFScan_Click(object sender, EventArgs e)
        {
            FrmTNFScan frm = FrmTNFScan.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuMesEmployee_Click(object sender, EventArgs e)
        {
            FrmMesEmployee frm = FrmMesEmployee.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();

        }

        private void MenuCompletedToMes_Click(object sender, EventArgs e)
        {
            CompletedToMesLogin frm = CompletedToMesLogin.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();

        }

        private void MenuInvoicePrint_Click(object sender, EventArgs e)
        {
            FrmInvoicePrint frm = FrmInvoicePrint.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.MdiChildren.Length > 0)   //当子窗体个数大于0的时候遍历所有子窗体
            {
                foreach (Form myForm in this.MdiChildren)// 遍历所有子窗体
                    myForm.Close(); //关闭子窗体
            }
            System.Environment.Exit(0);
        }

        private void MenuCompletedSearch_Click(object sender, EventArgs e)
        {
            FrmCompletedSearch frm = FrmCompletedSearch.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuDelScan_Click(object sender, EventArgs e)
        {
            FrmDelScanHURLEY frm = FrmDelScanHURLEY.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuCompletedSyncMes_Click(object sender, EventArgs e)
        {
            FrmCompletedSyncMesData frm = FrmCompletedSyncMesData.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();

        }

        private void MenuFullPO_Click(object sender, EventArgs e)
        {
            FrmProductsFullSearch frm = FrmProductsFullSearch.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuProductSearch_Click(object sender, EventArgs e)
        {
            FrmProductSearch frm = FrmProductSearch.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void menuCFoutput_Click(object sender, EventArgs e)
        {
            FrmCFoutput frm = FrmCFoutput.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void menuHDIn_Click(object sender, EventArgs e)
        {
            CompletedToMesLogin frm = CompletedToMesLogin.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void menu10In_Click(object sender, EventArgs e)
        {
            CompletedToMesLogin frm = CompletedToMesLogin.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuAsicsImport_Click(object sender, EventArgs e)
        {
            FrmAsicsImport frm = FrmAsicsImport.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }



        private void MenuEmployeeManager_Click(object sender, EventArgs e)
        {
            FrmEmployee frm = FrmEmployee.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuScanSearch_Click(object sender, EventArgs e)
        {
            FrmScanSearch frm = FrmScanSearch.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();

        }

        private void MenuScanMakeComplete_Click(object sender, EventArgs e)
        {
            FrmComplete frm = FrmComplete.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void productionStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductionStatus frm = ProductionStatus.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void ShippingPackages_Click(object sender, EventArgs e)
        {
            FrmShippingPackages frm = FrmShippingPackages.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void productionStatus2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductionStatus2 frm = ProductionStatus2.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuIE_Click(object sender, EventArgs e)
        {
            lEBom frm = lEBom.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void MenuLuluRfidImport_Click(object sender, EventArgs e)
        {
            FrmRFIDNikeImport frm = FrmRFIDNikeImport.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuNikeRfidImport_Click(object sender, EventArgs e)
        {
            FrmRFIDLuluImport frm = FrmRFIDLuluImport.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuSingleScan_Click(object sender, EventArgs e)
        {
            FrmLuluSingleScan frm = FrmLuluSingleScan.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuBoxScan_Click(object sender, EventArgs e)
        {
            FrmRFIDBoxScan frm = FrmRFIDBoxScan.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuScanLog_Click(object sender, EventArgs e)
        {
            FrmRFIDScanSearch frm = FrmRFIDScanSearch.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuIESearch_Click(object sender, EventArgs e)
        {
            FrmlEBomSearch frm = FrmlEBomSearch.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuNewStyleSearch_Click(object sender, EventArgs e)
        {
            FrmNewStyleSearch frm = FrmNewStyleSearch.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuHardwareTest_Click(object sender, EventArgs e)
        {
            FrmHardwareTest frm = FrmHardwareTest.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MenuRFIDReader2_Click(object sender, EventArgs e)
        {
            FrmAsanRfidRead frm = FrmAsanRfidRead.GetSingleton();
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }
    }
}
