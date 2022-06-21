using ADRcpLib;
using ADSioLib;
using ADUtilsLib.Initializer;
using ADUtilsLib.Utils;
using BLL;
using COMMON;
using ReaderB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmHardwareTest : Form
    {
        private static FrmHardwareTest frm;
        public AlarmManager am = new AlarmManager();
        WyuanRFIDReaderHelper WyuanHelper = new WyuanRFIDReaderHelper();
        AsadDuooRFIDReaderHelper AsadHelper = new AsadDuooRFIDReaderHelper();

        // [STAThread]
        private bool fAppClosed; //在测试模式下响应关闭应用程序
        private byte fComAdr = 0xff; //当前操作的ComAdr
        private int ferrorcode;
        private byte fBaud;
        private double fdminfre;
        private double fdmaxfre;
        private byte Maskadr;
        private byte MaskLen;
        private byte MaskFlag;
        private int fCmdRet = 30; //所有执行指令的返回值
        private int fOpenComIndex; //打开的串口索引号
        private bool fIsInventoryScan;
        private bool fisinventoryscan_6B;
        private byte[] fOperEPC = new byte[36];
        private byte[] fPassWord = new byte[4];
        private byte[] fOperID_6B = new byte[8];
        private int CardNum1 = 0;
        ArrayList list = new ArrayList();
        private bool fTimer_6B_ReadWrite;
        private string fInventory_EPC_List; //存贮询查列表（如果读取的数据没有变化，则不进行刷新）
        private int frmcomportindex;
        private bool ComOpen = false;
        private bool breakflag = false;
        private double x_z;
        private double y_f;
        //以下TCPIP配置所需变量
        public string fRecvUDPstring = "";
        public string RemostIP = "";


        private const int READ_STOP = 0;
        private const int READ_START = 1;
        private const int READ_SETANT = 2;
        private const int READ_SETANT_WAIT = 3;
        private const int READ_SETANTOK = 4;
        private const int READ_IDENTIFY = 5;
        private const int READ_IDENTIFY_WAIT = 6;
        private const int READ_IDENTIFYOK = 7;
        private const int READ_EXIT = 8;
        bool mIsStop = true;
        bool mIsStart = false;
        int mIsStatus = 0;
        int mLoopDelay = 0;
        int mLoopInterval = 0;
        DateTime nReadShowTime = DateTime.Now;
        int nReadShowRate = 50;
        int nAllTags = 0;
        int nInventoryTags = 0;
        int nInventoryCount = 0;
        int isStopAutoRead = 1; //是否主动工作方式
        int mCommandDelay = 2; //是否主动工作方式
        int mCurrentAnt = 0xff; //当前天线
        int nMaxAnt = 0;

        DateTime dtStart = DateTime.Now;
        public FrmHardwareTest()
        {
            InitializeComponent();
            AsadDuooSystemPub.RcpBase.RxRspParsed += RcpBase_RxRspParsed;
            AsadDuooSystemPub.RcpBase.TxRspParsed += RcpBase_TxRspParsed;
        }
        public static FrmHardwareTest GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmHardwareTest();
            }
            return frm;
        }
        private void FrmHardwareTest_Load(object sender, EventArgs e)
        {

            progressBar1.Visible = false;
            fOpenComIndex = -1;
            fComAdr = 0;
            ferrorcode = -1;
            fBaud = 5;
            InitComList();
            InitReaderList();

            fAppClosed = false;
            fIsInventoryScan = false;
            fisinventoryscan_6B = false;
            fTimer_6B_ReadWrite = false;

            Timer_Test_.Enabled = false;
            Timer_G2_Read.Enabled = false;
            Timer_G2_Alarm.Enabled = false;
            timer1.Enabled = false;

            butGetReaderInfo.Enabled = false;
            butSetParameter.Enabled = false;
            butDefaultParameter.Enabled = false;

            button20.Enabled = false;




            rbWiegand26.Checked = true;
            rbWiegandOutputMSBFirst.Checked = true;
            rbEPCC1G2.Checked = true;
            radioButton7.Checked = true;
            rbEPC.Checked = true;
            rbActivateBuzzer.Checked = true;
            butSetWGParameter.Enabled = false;
            butSetWorkMode.Enabled = false;
            butGetWorkModeParameter.Enabled = false;
            butGetData.Enabled = false;
            butClearData.Enabled = false;
            cbReadWordNumber.Enabled = false;
            rbEPCC1G2.Enabled = false;
            RBiso180006B.Enabled = false;
            radioButton7.Enabled = false;
            radioButton8.Enabled = false;
            rbPassword.Enabled = false;
            rbEPC.Enabled = false;
            rbTID.Enabled = false;
            rbUser.Enabled = false;
            rbMultiTag.Enabled = false;
            rbActivateBuzzer.Enabled = false;
            rbDisEnableBuzzer.Enabled = false;
            txtFirstWordAddr.Enabled = false;
            radioButton_band_User.Checked = true;
            rbWordAddr.Enabled = false;
            rbButeAddr.Enabled = false;
            rbSingleTag.Enabled = false;
            rbEAS.Enabled = false;
            rbWordAddr.Checked = true;
            ComboBox_baud2.SelectedIndex = 3;
            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;
            rbCom.Checked = true;

            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
        }

        private void FrmHardwareTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer_Test_.Enabled = false;
            Timer_G2_Read.Enabled = false;
            Timer_G2_Alarm.Enabled = false;
            breakflag = true;
            fAppClosed = true;
            if (rbCom.Checked && frmcomportindex > 0)
            {
                StaticClassReaderB.CloseComPort();
            }
            if (rbTCPIP.Checked && frmcomportindex > 0)
            {
                StaticClassReaderB.CloseNetPort(frmcomportindex);
            }
        }

        private void radioButton22_CheckedChanged(object sender, EventArgs e)
        {
            OpenPort.Enabled = true;
            ClosePort.Enabled = true;
            OpenNetPort.Enabled = false;
            CloseNetPort.Enabled = false;
            CloseNetPort_Click(sender, e);
        }

        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
            if (ComboBox_AlreadyOpenCOM.Items.Count > 0)
                ClosePort_Click(sender, e);
            OpenPort.Enabled = false;
            ClosePort.Enabled = false;
            OpenNetPort.Enabled = true;
            CloseNetPort.Enabled = true;

        }

        private void ComboBox_COM_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox_baud2.Items.Clear();
            if (ComboBox_COM.SelectedIndex == 0)
            {
                ComboBox_baud2.Items.Add("9600bps");
                ComboBox_baud2.Items.Add("19200bps");
                ComboBox_baud2.Items.Add("38400bps");
                ComboBox_baud2.Items.Add("57600bps");
                ComboBox_baud2.Items.Add("115200bps");
                ComboBox_baud2.SelectedIndex = 3;
            }
            else
            {
                ComboBox_baud2.Items.Add("Auto");
                ComboBox_baud2.SelectedIndex = 0;
            }

        }

        private void OpenPort_Click(object sender, EventArgs e)
        {

            byte[] TrType = new byte[2];
            byte[] VersionInfo = new byte[2];
            byte ReaderType = 0;
            byte ScanTime = 0;
            byte dmaxfre = 0;
            byte dminfre = 0;
            byte powerdBm = 0;
            byte FreBand = 0;
            Edit_Version.Text = "";
            Edit_ComAdr.Text = "";
            Edit_scantime.Text = "";
            Edit_Type.Text = "";
            ISO180006B.Checked = false;
            EPCC1G2.Checked = false;
            Edit_powerdBm.Text = "";
            Edit_dminfre.Text = "";
            Edit_dmaxfre.Text = "";
            ComboBox_PowerDbm.Items.Clear();


            string PortName = this.ComboBox_COM.Text.Trim().ToUpper();
            string BaudRate = this.ComboBox_baud2.Text.Trim().ToUpper();
            string ReaderAddr = this.Edit_CmdComAddr.Text.Trim().ToUpper();
            fBaud = 5;
            int dd = WyuanHelper.connRfidCOM(PortName, fBaud, ReaderAddr);
            if (dd == 0)
            {
                ComOpen = true;
                frmcomportindex = Convert.ToInt32(PortName.Substring(3, PortName.Length - 3));
                fOpenComIndex = frmcomportindex;
                WyuanInfos info = WyuanHelper.GetReaderInfo();
                if (fBaud > 3)
                {
                    ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                }
                else
                {
                    ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                }
                if ((info._CmdRet == 0x35) || (info._CmdRet == 0x30))
                {
                    ComOpen = false;
                    MessageBox.Show("Serial Communication Error or Occupied", "Information");
                    StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                    return;
                }
                MessageBox.Show("连接成功");
                if (info._CmdRet == 0)
                {
                    Edit_Version.Text = info._Version;
                    if (info._Ver >= 30)
                    {
                        for (int i = 0; i < 31; i++)
                            ComboBox_PowerDbm.Items.Add(Convert.ToString(i));
                        if (Convert.ToInt32(info._PowerDbm) > 30)
                            ComboBox_PowerDbm.SelectedIndex = 30;
                        else
                            ComboBox_PowerDbm.SelectedIndex = Convert.ToInt32(info._PowerDbm);
                    }
                    else
                    {
                        for (int i = 0; i < 19; i++)
                            ComboBox_PowerDbm.Items.Add(Convert.ToString(i));
                        if (Convert.ToInt32(info._PowerDbm) > 18)
                            ComboBox_PowerDbm.SelectedIndex = 18;
                        else
                            ComboBox_PowerDbm.SelectedIndex = Convert.ToInt32(info._PowerDbm);
                    }
                    Edit_ComAdr.Text = info._ComAdr;
                    Edit_NewComAdr.Text = info._ComAdr;
                    Edit_scantime.Text = info._scantime;
                    ComboBox_scantime.SelectedIndex = info._scantimeIndex;
                    Edit_powerdBm.Text = info._PowerDbm;

                    FreBand = info._FreBand;
                    switch (FreBand)
                    {
                        case 0:
                            {
                                radioButton_band_User.Checked = true;
                                fdminfre = 902.6 + (dminfre & 0x3F) * 0.4;
                                fdmaxfre = 902.6 + (dmaxfre & 0x3F) * 0.4;
                            }
                            break;
                        case 1:
                            {
                                radioButton_band_Chinese.Checked = true;
                                fdminfre = 920.125 + (dminfre & 0x3F) * 0.25;
                                fdmaxfre = 920.125 + (dmaxfre & 0x3F) * 0.25;
                            }
                            break;
                        case 2:
                            {
                                radioButton_band_US.Checked = true;
                                fdminfre = 902.75 + (dminfre & 0x3F) * 0.5;
                                fdmaxfre = 902.75 + (dmaxfre & 0x3F) * 0.5;
                            }
                            break;
                        case 3:
                            {
                                radioButton_band_Korean.Checked = true;
                                fdminfre = 917.1 + (dminfre & 0x3F) * 0.2;
                                fdmaxfre = 917.1 + (dmaxfre & 0x3F) * 0.2;
                            }
                            break;
                        case 4:
                            {
                                radioButton_band_EU.Checked = true;
                                fdminfre = 865.1 + (dminfre & 0x3F) * 0.2;
                                fdmaxfre = 865.1 + (dmaxfre & 0x3F) * 0.2;
                            }
                            break;
                    }
                    Edit_dminfre.Text = Convert.ToString(fdminfre) + "MHz";
                    Edit_dmaxfre.Text = Convert.ToString(fdmaxfre) + "MHz";
                    if (fdmaxfre != fdminfre)
                        CheckBox_SameFre.Checked = false;
                    ComboBox_dminfre.SelectedIndex = dminfre & 0x3F;
                    ComboBox_dmaxfre.SelectedIndex = dmaxfre & 0x3F;
                    if (ReaderType == 0x03)
                        Edit_Type.Text = "";
                    if (ReaderType == 0x06)
                        Edit_Type.Text = "";
                    if (ReaderType == 0x09)
                        Edit_Type.Text = "UHFReader18";
                    if ((TrType[0] & 0x02) == 0x02) //第二个字节低第四位代表支持的协议“ISO/IEC 15693”
                    {
                        ISO180006B.Checked = true;
                        EPCC1G2.Checked = true;
                    }
                    else
                    {
                        ISO180006B.Checked = false;
                        EPCC1G2.Checked = false;
                    }
                }
                AddCmdLog("GetReaderInformation", "GetReaderInformation", fCmdRet);
            }
            else if (dd == 1)
            {
                //  COM 口已被占用
                MessageBox.Show("COM Opened", "Information");
                return;

            }
            else if (dd == -1)
            {
                //  COM 连接失败
                MessageBox.Show("Serial Communication Error or Occupied", "Information");
                return;
            }
            else
            {
                //  COM 未知错误
                MessageBox.Show("Serial Communication Error or Occupied", "Information");
                return;
            }
            //  RefreshStatus();

            /*


         int port = 0;
         int openresult, i;
         openresult = 30;
         string temp;
         Cursor = Cursors.WaitCursor;
         if (Edit_CmdComAddr.Text == "")
             Edit_CmdComAddr.Text = "FF";
         fComAdr = Convert.ToByte(Edit_CmdComAddr.Text, 16); // $FF;
         try
         {
             if (ComboBox_COM.SelectedIndex == 0)//Auto
             {
                 fBaud = Convert.ToByte(ComboBox_baud2.SelectedIndex);
                 if (fBaud > 2)
                 {
                     fBaud = Convert.ToByte(fBaud + 2);
                 }
                 openresult = StaticClassReaderB.AutoOpenComPort(ref port, ref fComAdr, fBaud, ref frmcomportindex);
                 fOpenComIndex = frmcomportindex;
                 if (openresult == 0)
                 {
                     ComOpen = true;
                     // Button3_Click(sender, e); //自动执行读取写卡器信息
                     if (fBaud > 3)
                     {
                         ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                     }
                     else
                     {
                         ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                     }
                     Button3_Click(sender, e); //自动执行读取写卡器信息
                     if ((fCmdRet == 0x35) | (fCmdRet == 0x30))
                     {
                         MessageBox.Show("Serial Communication Error or Occupied", "Information");
                         StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                         ComOpen = false;
                     }
                 }
             }
             else
             {
                 temp = ComboBox_COM.SelectedItem.ToString();
                 temp = temp.Trim();
                 port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                 for (i = 6; i >= 0; i--)
                 {
                     fBaud = Convert.ToByte(i);
                     if (fBaud == 3)
                         continue;
                     openresult = StaticClassReaderB.OpenComPort(port, ref fComAdr, fBaud, ref frmcomportindex);
                     fOpenComIndex = frmcomportindex;
                     if (openresult == 0x35)
                     {
                         MessageBox.Show("COM Opened", "Information");
                         return;
                     }
                     if (openresult == 0)
                     {
                         ComOpen = true;
                         Button3_Click(sender, e); //自动执行读取写卡器信息
                         if (fBaud > 3)
                         {
                             ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                         }
                         else
                         {
                             ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                         }
                         if ((fCmdRet == 0x35) || (fCmdRet == 0x30))
                         {
                             ComOpen = false;
                             MessageBox.Show("Serial Communication Error or Occupied", "Information");
                             StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                             return;
                         }
                         RefreshStatus();
                         break;
                     }

                 }
             }
         }
         finally
         {
             Cursor = Cursors.Default;
         }

         if ((fOpenComIndex != -1) & (openresult != 0X35) & (openresult != 0X30))
         {
             ComboBox_AlreadyOpenCOM.Items.Add("COM" + Convert.ToString(fOpenComIndex));
             ComboBox_AlreadyOpenCOM.SelectedIndex = ComboBox_AlreadyOpenCOM.SelectedIndex + 1;
             butGetReaderInfo.Enabled = true;
             button20.Enabled = true;
             butSetParameter.Enabled = true;
             butDefaultParameter.Enabled = true;

             butSetWGParameter.Enabled = true;
             butSetWorkMode.Enabled = true;
             butGetWorkModeParameter.Enabled = true;
             button12.Enabled = true;
             button_OffsetTime.Enabled = true;
             button_settigtime.Enabled = true;
             button_gettigtime.Enabled = true;
             ComOpen = true;
         }
         if ((fOpenComIndex == -1) && (openresult == 0x30))
             MessageBox.Show("Serial Communication Error", "Information");

         if ((ComboBox_AlreadyOpenCOM.Items.Count != 0) & (fOpenComIndex != -1) & (openresult != 0X35) & (openresult != 0X30) & (fCmdRet == 0))
         {
             fComAdr = Convert.ToByte(Edit_ComAdr.Text, 16);
             temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
             frmcomportindex = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
         }
         */


            ComboBox_AlreadyOpenCOM.Items.Add(PortName);
            ComboBox_AlreadyOpenCOM.SelectedIndex = ComboBox_AlreadyOpenCOM.SelectedIndex + 1;
            butGetReaderInfo.Enabled = true;
            button20.Enabled = true;
            butSetParameter.Enabled = true;
            butDefaultParameter.Enabled = true;

            butSetWGParameter.Enabled = true;
            butSetWorkMode.Enabled = true;
            butGetWorkModeParameter.Enabled = true;
            button12.Enabled = true;
            button_OffsetTime.Enabled = true;
            button_settigtime.Enabled = true;
            button_gettigtime.Enabled = true;
            ComOpen = true;

            RefreshStatus();

            WyuanWorkMode WorkMode = WyuanHelper.getRFIDWorkMode();




            //  fCmdRet = StaticClassReaderB.GetWorkModeParameter(ref fComAdr, Parameter, frmcomportindex);
            if (WorkMode._CmdRet == 0)
            {

                switch (WorkMode._Wiegand)
                {
                    case 0:
                        rbWiegand26.Checked = true;
                        rbWiegandOutputMSBFirst.Checked = true;
                        break;
                    case 1:
                        rbWiegand34.Checked = true;
                        rbWiegandOutputMSBFirst.Checked = true;
                        break;
                    case 2:
                        rbWiegand26.Checked = true;
                        rbWiegandOutputLSBFirst.Checked = true;
                        break;
                    case 3:
                        rbWiegand34.Checked = true;
                        rbWiegandOutputLSBFirst.Checked = true;
                        break;
                    default:
                        break;
                }
                cbDataOutputInvterval.SelectedIndex = WorkMode._DataOutputInvterval;
                cbPulseInterval.SelectedIndex = WorkMode._PulseInterval - 1;
                cbPulseWidth.SelectedIndex = WorkMode._PulseWidth - 1;
                cbWorkMode.SelectedIndex = WorkMode._WorkMode;
                if ((WorkMode._WorkMode == 1) || (WorkMode._WorkMode == 2) || (WorkMode._WorkMode == 3))
                {
                    butGetData.Enabled = true;
                    butClearData.Enabled = true;
                    rbEPCC1G2.Enabled = true;
                    RBiso180006B.Enabled = true;
                    radioButton7.Enabled = true;
                    radioButton8.Enabled = true;

                    if (rbEPCC1G2.Checked)
                    {
                        if (radioButton7.Checked)
                        {
                            rbWordAddr.Enabled = true;
                            rbButeAddr.Enabled = true;
                        }
                        else
                        {
                            rbWordAddr.Enabled = false;
                            rbButeAddr.Enabled = false;
                        }
                        rbPassword.Enabled = true;
                        rbEPC.Enabled = true;
                        rbTID.Enabled = true;
                        rbUser.Enabled = true;
                        rbSingleTag.Enabled = true;
                        radioButton20.Enabled = true;
                        if (Convert.ToInt32((WorkMode._MultiTagEAS & 0x10)) == 0x10)
                        {
                            rbMultiTag.Enabled = false;
                            rbEAS.Enabled = false;
                        }
                        else
                        {
                            rbMultiTag.Enabled = true;
                            rbEAS.Enabled = true;
                        }
                        if ((rbMultiTag.Checked) || (rbEAS.Checked))
                            cbSingleTagFilteringTime.Enabled = false;
                        else
                            cbSingleTagFilteringTime.Enabled = true;
                    }
                    else
                        cbSingleTagFilteringTime.Enabled = true;
                    rbActivateBuzzer.Enabled = true;
                    rbDisEnableBuzzer.Enabled = true;
                    txtFirstWordAddr.Enabled = true;
                    if ((radioButton8.Checked) || (radioButton20.Checked))
                        cbReadWordNumber.Enabled = true;
                }
                if (WorkMode._WorkMode == 0)
                {
                    butGetData.Enabled = false;
                    butClearData.Enabled = false;
                    rbEPCC1G2.Enabled = false;
                    RBiso180006B.Enabled = false;
                    radioButton7.Enabled = false;
                    radioButton8.Enabled = false;
                    rbPassword.Enabled = false;
                    rbEPC.Enabled = false;
                    rbTID.Enabled = false;
                    rbUser.Enabled = false;
                    rbMultiTag.Enabled = false;
                    rbActivateBuzzer.Enabled = false;
                    rbDisEnableBuzzer.Enabled = false;
                    rbWordAddr.Enabled = false;
                    rbButeAddr.Enabled = false;
                    rbSingleTag.Enabled = false;
                    rbEAS.Enabled = false;
                    radioButton20.Enabled = false;
                    txtFirstWordAddr.Enabled = false;
                    cbReadWordNumber.Enabled = false;
                    cbSingleTagFilteringTime.Enabled = false;
                }
                if (Convert.ToInt32((WorkMode._MultiTagEAS) & 0x01) == 0)
                    rbEPCC1G2.Checked = true;
                else
                    RBiso180006B.Checked = true;
                if (Convert.ToInt32((WorkMode._MultiTagEAS) & 0x02) == 0)
                    radioButton7.Checked = true;
                else
                {
                    if (Convert.ToInt32((WorkMode._MultiTagEAS & 0x10)) == 0)
                        radioButton8.Checked = true;
                    else
                        radioButton20.Checked = true;
                }
                if (Convert.ToInt32((WorkMode._MultiTagEAS) & 0x04) == 0)
                    rbActivateBuzzer.Checked = true;
                else
                    rbDisEnableBuzzer.Checked = true;
                if (Convert.ToInt32((WorkMode._MultiTagEAS) & 0x08) == 0)
                    rbWordAddr.Checked = true;
                else
                    rbButeAddr.Checked = true;
                switch (WorkMode._Inquiry)
                {
                    case 0:
                        rbPassword.Checked = true;
                        break;
                    case 1:
                        rbEPC.Checked = true;
                        break;
                    case 2:
                        rbTID.Checked = true;
                        break;
                    case 3:
                        rbUser.Checked = true;
                        break;
                    case 4:
                        rbMultiTag.Checked = true;
                        break;
                    case 5:
                        rbSingleTag.Checked = true;
                        break;
                    case 6:
                        rbEAS.Checked = true;
                        break;
                    default:
                        break;
                }
                txtFirstWordAddr.Text = WorkMode._FirstWordAddr;
                cbReadWordNumber.SelectedIndex = Convert.ToInt32(WorkMode._ReadWordNumber - 1);
                cbSingleTagFilteringTime.SelectedIndex = Convert.ToInt32(WorkMode._SingleTagFilteringTime);
                cbEASAccuracy.SelectedIndex = Convert.ToInt32(WorkMode._EASAccuracy);
                comboBox_OffsetTime.SelectedIndex = Convert.ToInt32(WorkMode._OffsetTime);
            }


        }

        private void ClosePort_Click(object sender, EventArgs e)
        {
            int port;
            //string SelectCom ;
            string temp;
            ClearLastInfo();
            try
            {
                if (ComboBox_AlreadyOpenCOM.SelectedIndex < 0)
                {
                    MessageBox.Show("Please Choose COM Port to close", "Information");
                }
                else
                {
                    temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                    port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                    fCmdRet = StaticClassReaderB.CloseSpecComPort(port);
                    if (fCmdRet == 0)
                    {
                        ComboBox_AlreadyOpenCOM.Items.RemoveAt(0);
                        if (ComboBox_AlreadyOpenCOM.Items.Count != 0)
                        {
                            temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                            port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                            StaticClassReaderB.CloseSpecComPort(port);
                            fComAdr = 0xFF;
                            StaticClassReaderB.OpenComPort(port, ref fComAdr, fBaud, ref frmcomportindex);
                            fOpenComIndex = frmcomportindex;
                            RefreshStatus();
                            Button3_Click(sender, e); //自动执行读取写卡器信息
                        }
                    }
                    else
                        MessageBox.Show("Serial Communication Error", "Information");
                }
            }
            finally
            {

            }
            if (ComboBox_AlreadyOpenCOM.Items.Count != 0)
                ComboBox_AlreadyOpenCOM.SelectedIndex = 0;
            else
            {
                fOpenComIndex = -1;
                ComboBox_AlreadyOpenCOM.Items.Clear();
                ComboBox_AlreadyOpenCOM.Refresh();
                RefreshStatus();
                butGetReaderInfo.Enabled = false;
                button20.Enabled = false;
                butSetParameter.Enabled = false;
                butDefaultParameter.Enabled = false;

                butSetWGParameter.Enabled = false;
                butSetWorkMode.Enabled = false;
                butGetWorkModeParameter.Enabled = false;


                ComOpen = false;
                button12.Enabled = false;
                butGetData.Text = "Get";
                butGetData.Enabled = false;
                butClearData.Enabled = false;
                timer1.Enabled = false;
                cbWorkMode.SelectedIndex = 0;
                button_OffsetTime.Enabled = false;
                button_settigtime.Enabled = false;
                button_gettigtime.Enabled = false;
            }
        }

        private void OpenNetPort_Click(object sender, EventArgs e)
        {
            int port, openresult = 0;
            string IPAddr;
            if (txtNetAddr.Text == "")
                Edit_CmdComAddr.Text = "FF";
            fComAdr = Convert.ToByte(txtNetAddr.Text, 16); // $FF;
            if ((txtNetPort.Text == "") || (txtNetIP.Text == ""))
                MessageBox.Show("Config error!", "information");
            port = Convert.ToInt32(txtNetPort.Text);
            IPAddr = txtNetIP.Text;
            openresult = StaticClassReaderB.OpenNetPort(port, IPAddr, ref fComAdr, ref frmcomportindex);
            fOpenComIndex = frmcomportindex;
            if (openresult == 0)
            {
                ComOpen = true;
                Button3_Click(sender, e); //自动执行读取写卡器信息
            }
            if ((openresult == 0x35) || (openresult == 0x30))
            {
                MessageBox.Show("TCPIP error", "Information");
                StaticClassReaderB.CloseNetPort(frmcomportindex);
                ComOpen = false;
                return;
            }
            if ((fOpenComIndex != -1) && (openresult != 0X35) && (openresult != 0X30))
            {
                butGetReaderInfo.Enabled = true;
                button20.Enabled = true;
                butSetParameter.Enabled = true;
                butDefaultParameter.Enabled = true;

                butSetWGParameter.Enabled = true;
                butSetWorkMode.Enabled = true;
                butGetWorkModeParameter.Enabled = true;
                button12.Enabled = true;
                button_OffsetTime.Enabled = true;
                button_settigtime.Enabled = true;
                button_gettigtime.Enabled = true;
                ComOpen = true;
            }
            if ((fOpenComIndex == -1) && (openresult == 0x30))
                MessageBox.Show("TCPIP Communication Error", "Information");
            RefreshStatus();
        }

        private void CloseNetPort_Click(object sender, EventArgs e)
        {

            ClearLastInfo();
            fCmdRet = StaticClassReaderB.CloseNetPort(frmcomportindex);
            if (fCmdRet == 0)
            {
                fOpenComIndex = -1;
                RefreshStatus();
                butGetReaderInfo.Enabled = false;
                button20.Enabled = false;
                butSetParameter.Enabled = false;
                butDefaultParameter.Enabled = false;

                butSetWGParameter.Enabled = false;
                butSetWorkMode.Enabled = false;
                butGetWorkModeParameter.Enabled = false;


                ComOpen = false;
                button12.Enabled = false;
                butGetData.Text = "Get";
                butGetData.Enabled = false;
                butClearData.Enabled = false;
                timer1.Enabled = false;
                cbWorkMode.SelectedIndex = 0;
                button_OffsetTime.Enabled = false;
                button_settigtime.Enabled = false;
                button_gettigtime.Enabled = false;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {

            byte RelayStatus = 0;
            if (comboBox9.SelectedIndex == 0)
                RelayStatus = Convert.ToByte(RelayStatus | 0);
            else
                RelayStatus = Convert.ToByte(RelayStatus | 1);
            if (comboBox10.SelectedIndex == 0)
                RelayStatus = Convert.ToByte(RelayStatus | 0);
            else
                RelayStatus = Convert.ToByte(RelayStatus | 2);
            fCmdRet = StaticClassReaderB.SetRelay(ref fComAdr, RelayStatus, frmcomportindex);
            AddCmdLog("SetRelay", "Set", fCmdRet);
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            byte[] TrType = new byte[2];
            byte[] VersionInfo = new byte[2];
            byte ReaderType = 0;
            byte ScanTime = 0;
            byte dmaxfre = 0;
            byte dminfre = 0;
            byte powerdBm = 0;
            byte FreBand = 0;
            Edit_Version.Text = "";
            Edit_ComAdr.Text = "";
            Edit_scantime.Text = "";
            Edit_Type.Text = "";
            ISO180006B.Checked = false;
            EPCC1G2.Checked = false;
            Edit_powerdBm.Text = "";
            Edit_dminfre.Text = "";
            Edit_dmaxfre.Text = "";
            ComboBox_PowerDbm.Items.Clear();

            fCmdRet = StaticClassReaderB.GetReaderInformation(ref fComAdr, VersionInfo, ref ReaderType, TrType, ref dmaxfre, ref dminfre, ref powerdBm, ref ScanTime, frmcomportindex);
            if (fCmdRet == 0)
            {
                Edit_Version.Text = Convert.ToString(VersionInfo[0], 10).PadLeft(2, '0') + "." + Convert.ToString(VersionInfo[1], 10).PadLeft(2, '0');
                if (VersionInfo[1] >= 30)
                {
                    for (int i = 0; i < 31; i++)
                        ComboBox_PowerDbm.Items.Add(Convert.ToString(i));
                    if (powerdBm > 30)
                        ComboBox_PowerDbm.SelectedIndex = 30;
                    else
                        ComboBox_PowerDbm.SelectedIndex = powerdBm;
                }
                else
                {
                    for (int i = 0; i < 19; i++)
                        ComboBox_PowerDbm.Items.Add(Convert.ToString(i));
                    if (powerdBm > 18)
                        ComboBox_PowerDbm.SelectedIndex = 18;
                    else
                        ComboBox_PowerDbm.SelectedIndex = powerdBm;
                }
                Edit_ComAdr.Text = Convert.ToString(fComAdr, 16).PadLeft(2, '0');
                Edit_NewComAdr.Text = Convert.ToString(fComAdr, 16).PadLeft(2, '0');
                Edit_scantime.Text = Convert.ToString(ScanTime, 10).PadLeft(2, '0') + "*100ms";
                ComboBox_scantime.SelectedIndex = ScanTime - 3;
                Edit_powerdBm.Text = Convert.ToString(powerdBm, 10).PadLeft(2, '0');

                FreBand = Convert.ToByte(((dmaxfre & 0xc0) >> 4) | (dminfre >> 6));
                switch (FreBand)
                {
                    case 0:
                        {
                            radioButton_band_User.Checked = true;
                            fdminfre = 902.6 + (dminfre & 0x3F) * 0.4;
                            fdmaxfre = 902.6 + (dmaxfre & 0x3F) * 0.4;
                        }
                        break;
                    case 1:
                        {
                            radioButton_band_Chinese.Checked = true;
                            fdminfre = 920.125 + (dminfre & 0x3F) * 0.25;
                            fdmaxfre = 920.125 + (dmaxfre & 0x3F) * 0.25;
                        }
                        break;
                    case 2:
                        {
                            radioButton_band_US.Checked = true;
                            fdminfre = 902.75 + (dminfre & 0x3F) * 0.5;
                            fdmaxfre = 902.75 + (dmaxfre & 0x3F) * 0.5;
                        }
                        break;
                    case 3:
                        {
                            radioButton_band_Korean.Checked = true;
                            fdminfre = 917.1 + (dminfre & 0x3F) * 0.2;
                            fdmaxfre = 917.1 + (dmaxfre & 0x3F) * 0.2;
                        }
                        break;
                    case 4:
                        {
                            radioButton_band_EU.Checked = true;
                            fdminfre = 865.1 + (dminfre & 0x3F) * 0.2;
                            fdmaxfre = 865.1 + (dmaxfre & 0x3F) * 0.2;
                        }
                        break;
                }
                Edit_dminfre.Text = Convert.ToString(fdminfre) + "MHz";
                Edit_dmaxfre.Text = Convert.ToString(fdmaxfre) + "MHz";
                if (fdmaxfre != fdminfre)
                    CheckBox_SameFre.Checked = false;
                ComboBox_dminfre.SelectedIndex = dminfre & 0x3F;
                ComboBox_dmaxfre.SelectedIndex = dmaxfre & 0x3F;
                if (ReaderType == 0x03)
                    Edit_Type.Text = "";
                if (ReaderType == 0x06)
                    Edit_Type.Text = "";
                if (ReaderType == 0x09)
                    Edit_Type.Text = "UHFReader18";
                if ((TrType[0] & 0x02) == 0x02) //第二个字节低第四位代表支持的协议“ISO/IEC 15693”
                {
                    ISO180006B.Checked = true;
                    EPCC1G2.Checked = true;
                }
                else
                {
                    ISO180006B.Checked = false;
                    EPCC1G2.Checked = false;
                }
            }
            AddCmdLog("GetReaderInformation", "GetReaderInformation", fCmdRet);
        }

        private void Edit_NewComAdr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789ABCDEF".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
        }

        private void ComboBox_dminfre_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (CheckBox_SameFre.Checked)
            {
                ComboBox_dminfre.SelectedIndex = ComboBox_dmaxfre.SelectedIndex;
            }
            else if (ComboBox_dminfre.SelectedIndex > ComboBox_dmaxfre.SelectedIndex)
            {
                ComboBox_dminfre.SelectedIndex = ComboBox_dmaxfre.SelectedIndex;
                MessageBox.Show("Min.Frequency is equal or lesser than Max.Frequency", "Error Information");
            }
        }

        private void CheckBox_SameFre_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_SameFre.Checked)
                ComboBox_dmaxfre.SelectedIndex = ComboBox_dminfre.SelectedIndex;
        }

        private void ComboBox_dmaxfre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckBox_SameFre.Checked)
            {
                ComboBox_dminfre.SelectedIndex = ComboBox_dmaxfre.SelectedIndex;
            }
            else if (ComboBox_dminfre.SelectedIndex > ComboBox_dmaxfre.SelectedIndex)
            {
                ComboBox_dminfre.SelectedIndex = ComboBox_dmaxfre.SelectedIndex;
                MessageBox.Show("Min.Frequency is equal or lesser than Max.Frequency", "Error Information");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {

            byte aNewComAdr, powerDbm, dminfre, dmaxfre, scantime, band = 0;
            string returninfo = "";
            string returninfoDlg = "";
            string setinfo;
            if (radioButton_band_User.Checked)
                band = 0;
            if (radioButton_band_Chinese.Checked)
                band = 1;
            if (radioButton_band_US.Checked)
                band = 2;
            if (radioButton_band_Korean.Checked)
                band = 3;
            if (radioButton_band_EU.Checked)
                band = 4;
            if (Edit_NewComAdr.Text == "")
                return;
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            dminfre = Convert.ToByte(((band & 3) << 6) | (ComboBox_dminfre.SelectedIndex & 0x3F));
            dmaxfre = Convert.ToByte(((band & 0x0c) << 4) | (ComboBox_dmaxfre.SelectedIndex & 0x3F));
            aNewComAdr = Convert.ToByte(Edit_NewComAdr.Text);
            powerDbm = Convert.ToByte(ComboBox_PowerDbm.SelectedIndex);
            fBaud = Convert.ToByte(ComboBox_baud.SelectedIndex);
            if (fBaud > 2)
                fBaud = Convert.ToByte(fBaud + 2);
            scantime = Convert.ToByte(ComboBox_scantime.SelectedIndex + 3);
            setinfo = "Write";
            progressBar1.Value = 10;
            fCmdRet = StaticClassReaderB.WriteComAdr(ref fComAdr, ref aNewComAdr, frmcomportindex);
            if (fCmdRet == 0x13)
                fComAdr = aNewComAdr;
            if (fCmdRet == 0)
            {
                fComAdr = aNewComAdr;
                returninfo = returninfo + setinfo + "Address Successfully";
            }
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + setinfo + "Address Response Command Error";
            else
            {
                returninfo = returninfo + setinfo + "Address Fail";
                returninfoDlg = returninfoDlg + setinfo + "Address Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 25;
            fCmdRet = StaticClassReaderB.SetPowerDbm(ref fComAdr, powerDbm, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Power Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Power Response Command Error";
            else
            {
                returninfo = returninfo + ",Power Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Power Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 40;
            fCmdRet = StaticClassReaderB.Writedfre(ref fComAdr, ref dmaxfre, ref dminfre, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Frequency Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Frequency Response Command Error";
            else
            {
                returninfo = returninfo + ",Frequency Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Frequency Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 55;
            fCmdRet = StaticClassReaderB.Writebaud(ref fComAdr, ref fBaud, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Baud Rate Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Baud Rate Response Command Error";
            else
            {
                returninfo = returninfo + ",Baud Rate Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Baud Rate Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 70;
            fCmdRet = StaticClassReaderB.WriteScanTime(ref fComAdr, ref scantime, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",InventoryScanTime Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",InventoryScanTime Response Command Error";
            else
            {
                returninfo = returninfo + ",InventoryScanTime Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "InventoryScanTime Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 100;
            Button3_Click(sender, e);
            progressBar1.Visible = false;
            StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
            if (returninfoDlg != "")
                MessageBox.Show(returninfoDlg, "Information");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            byte aNewComAdr, powerDbm, dminfre, dmaxfre, scantime;
            string returninfo = "";
            string returninfoDlg = "";
            string setinfo;
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            dminfre = 0;
            dmaxfre = 62;
            aNewComAdr = 0X00;
            if (Convert.ToInt32(Edit_Version.Text.Substring(3, 2)) >= 30)
                powerDbm = 30;
            else
                powerDbm = 18;
            fBaud = 5;
            scantime = 10;
            setinfo = " Recovery ";
            ComboBox_baud.SelectedIndex = 3;
            progressBar1.Value = 10;
            fCmdRet = StaticClassReaderB.WriteComAdr(ref fComAdr, ref aNewComAdr, frmcomportindex);
            if (fCmdRet == 0x13)
                fComAdr = aNewComAdr;
            if (fCmdRet == 0)
            {
                fComAdr = aNewComAdr;
                returninfo = returninfo + setinfo + "Address Successfully";
            }
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + setinfo + "Address Response Command Error";
            else
            {
                returninfo = returninfo + setinfo + "Address Fail";
                returninfoDlg = returninfoDlg + setinfo + "Address Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 25;
            fCmdRet = StaticClassReaderB.SetPowerDbm(ref fComAdr, powerDbm, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Power Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Power Response Command Error";
            else
            {
                returninfo = returninfo + ",Power Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Power Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 40;
            fCmdRet = StaticClassReaderB.Writedfre(ref fComAdr, ref dmaxfre, ref dminfre, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Frequency Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Frequency Response Command Error";
            else
            {
                returninfo = returninfo + ",Frequency Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Frequency Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 55;
            fCmdRet = StaticClassReaderB.Writebaud(ref fComAdr, ref fBaud, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Baud Rate Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Baud Rate Response Command Error";
            else
            {
                returninfo = returninfo + ",Baud Rate Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Baud Rate Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 70;
            fCmdRet = StaticClassReaderB.WriteScanTime(ref fComAdr, ref scantime, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",InventoryScanTime Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",InventoryScanTime Response Command Error";
            else
            {
                returninfo = returninfo + ",InventoryScanTime Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "InventoryScanTime Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 100;
            Button3_Click(sender, e);
            progressBar1.Visible = false;
            StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
            if (returninfoDlg != "")
                MessageBox.Show(returninfoDlg, "Information");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte Wg_mode = 0;
            byte Wg_Data_Inteval;
            byte Wg_Pulse_Width;
            byte Wg_Pulse_Inteval;
            if (rbWiegand26.Checked)
            {
                if (rbWiegandOutputLSBFirst.Checked)
                    Wg_mode = 2;
                else
                    Wg_mode = 0;
            }
            if (rbWiegand34.Checked)
            {
                if (rbWiegandOutputLSBFirst.Checked)
                    Wg_mode = 3;
                else
                    Wg_mode = 1;
            }
            Wg_Data_Inteval = Convert.ToByte(cbDataOutputInvterval.SelectedIndex);
            Wg_Pulse_Width = Convert.ToByte(cbPulseWidth.SelectedIndex + 1);
            Wg_Pulse_Inteval = Convert.ToByte(cbPulseInterval.SelectedIndex + 1);
            fCmdRet = StaticClassReaderB.SetWGParameter(ref fComAdr, Wg_mode, Wg_Data_Inteval, Wg_Pulse_Width, Wg_Pulse_Inteval, frmcomportindex);
            AddCmdLog("SetWGParameter", "SetWGParameter", fCmdRet);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

            if (rbEPCC1G2.Checked)
            {
                if ((cbWorkMode.SelectedIndex == 1) | (cbWorkMode.SelectedIndex == 2) | (cbWorkMode.SelectedIndex == 3))
                {
                    rbPassword.Enabled = true;
                    rbEPC.Enabled = true;
                    rbTID.Enabled = true;
                    rbUser.Enabled = true;
                    rbMultiTag.Enabled = true;
                    rbSingleTag.Enabled = true;
                    if (rbWordAddr.Checked)
                        label41.Text = "First Word Addr(Hex):";
                    else
                        label41.Text = "First Byte Addr(Hex):";
                    if (radioButton20.Checked)
                    {
                        rbMultiTag.Enabled = false;
                        rbEAS.Enabled = false;
                        label41.Text = "First Byte Addr(Hex):";
                    }
                    else
                    {
                        rbMultiTag.Enabled = true;
                        rbEAS.Enabled = true;
                    }
                    if (radioButton7.Checked)
                    {
                        rbWordAddr.Enabled = true;
                        rbButeAddr.Enabled = true;
                        if ((rbMultiTag.Checked) | (rbEAS.Checked))
                        {
                            cbSingleTagFilteringTime.Enabled = false;
                        }
                        else
                        {
                            cbSingleTagFilteringTime.Enabled = true;
                        }

                    }
                    else
                    {
                        rbWordAddr.Enabled = false;
                        rbButeAddr.Enabled = false;
                        if ((rbMultiTag.Checked) || (rbEAS.Checked))
                            cbSingleTagFilteringTime.Enabled = false;
                        else
                            cbSingleTagFilteringTime.Enabled = true;
                        if (radioButton20.Checked)
                            label41.Text = "First Byte Addr(Hex):";
                        else
                            label41.Text = "First Word Addr(Hex):";
                    }
                }
            }
            else
            {
                rbPassword.Enabled = false;
                rbEPC.Enabled = false;
                rbTID.Enabled = false;
                rbUser.Enabled = false;
                rbMultiTag.Enabled = false;
                rbSingleTag.Enabled = false;
                rbWordAddr.Enabled = false;
                rbButeAddr.Enabled = false;
                rbEAS.Enabled = false;
                cbSingleTagFilteringTime.Enabled = true;
                label41.Text = "First Byte Addr(Hex)";
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

            if (rbEPCC1G2.Checked)
            {
                if ((cbWorkMode.SelectedIndex == 1) | (cbWorkMode.SelectedIndex == 2) | (cbWorkMode.SelectedIndex == 3))
                {
                    rbPassword.Enabled = true;
                    rbEPC.Enabled = true;
                    rbTID.Enabled = true;
                    rbUser.Enabled = true;
                    rbMultiTag.Enabled = true;
                    rbSingleTag.Enabled = true;
                    if (rbWordAddr.Checked)
                        label41.Text = "First Word Addr(Hex):";
                    else
                        label41.Text = "First Byte Addr(Hex):";
                    if (radioButton20.Checked)
                    {
                        rbMultiTag.Enabled = false;
                        rbEAS.Enabled = false;
                        label41.Text = "First Byte Addr(Hex):";
                    }
                    else
                    {
                        rbMultiTag.Enabled = true;
                        rbEAS.Enabled = true;
                    }
                    if (radioButton7.Checked)
                    {
                        rbWordAddr.Enabled = true;
                        rbButeAddr.Enabled = true;
                        if ((rbMultiTag.Checked) | (rbEAS.Checked))
                        {
                            cbSingleTagFilteringTime.Enabled = false;
                        }
                        else
                        {
                            cbSingleTagFilteringTime.Enabled = true;
                        }

                    }
                    else
                    {
                        rbWordAddr.Enabled = false;
                        rbButeAddr.Enabled = false;
                        if ((rbMultiTag.Checked) || (rbEAS.Checked))
                            cbSingleTagFilteringTime.Enabled = false;
                        else
                            cbSingleTagFilteringTime.Enabled = true;
                        if (radioButton20.Checked)
                            label41.Text = "First Byte Addr(Hex):";
                        else
                            label41.Text = "First Word Addr(Hex):";
                    }
                }
            }
            else
            {
                rbPassword.Enabled = false;
                rbEPC.Enabled = false;
                rbTID.Enabled = false;
                rbUser.Enabled = false;
                rbMultiTag.Enabled = false;
                rbSingleTag.Enabled = false;
                rbWordAddr.Enabled = false;
                rbButeAddr.Enabled = false;
                rbEAS.Enabled = false;
                cbSingleTagFilteringTime.Enabled = true;
                label41.Text = "First Byte Addr(Hex)";
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            cbSingleTagFilteringTime.Enabled = true;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            cbSingleTagFilteringTime.Enabled = true;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            cbSingleTagFilteringTime.Enabled = true;
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            cbSingleTagFilteringTime.Enabled = true;
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            cbSingleTagFilteringTime.Enabled = true;
        }

        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {
            cbSingleTagFilteringTime.Enabled = true;
        }

        private void radioButton19_CheckedChanged(object sender, EventArgs e)
        {
            cbSingleTagFilteringTime.Enabled = false;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if ((rbEPCC1G2.Checked) && (cbWorkMode.SelectedIndex > 0))
            {
                rbWordAddr.Enabled = true;
                rbButeAddr.Enabled = true;
                rbMultiTag.Enabled = true;
                rbEAS.Enabled = true;
                if (rbWordAddr.Checked)
                    label41.Text = "First Word Addr(Hex):";
                else
                    label41.Text = "First Byte Addr(Hex):";
                labReadWordNumber.Text = "Read Word Number:";
            }
            cbReadWordNumber.Enabled = false;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

            if ((cbWorkMode.SelectedIndex == 1) || (cbWorkMode.SelectedIndex == 2) || (cbWorkMode.SelectedIndex == 3))
            {
                if (radioButton8.Checked)
                    cbReadWordNumber.Enabled = true;
                cbReadWordNumber.Items.Clear();
                if (radioButton20.Checked)
                {
                    for (int i = 1; i < 5; i++)
                        cbReadWordNumber.Items.Add(Convert.ToString(i));
                    cbReadWordNumber.SelectedIndex = 3;
                    labReadWordNumber.Text = "Read Byte Number:";
                    cbReadWordNumber.Enabled = true;
                    label41.Text = "First Byte Addr(Hex):";
                }
                else
                {
                    for (int i = 1; i < 33; i++)
                        cbReadWordNumber.Items.Add(Convert.ToString(i));
                    cbReadWordNumber.SelectedIndex = 0;
                    labReadWordNumber.Text = "Read Word Number:";
                    label41.Text = "First Word Addr((Hex):";
                }
                if (rbEPCC1G2.Checked)
                {
                    rbWordAddr.Enabled = false;
                    rbButeAddr.Enabled = false;
                    if (radioButton20.Checked)
                    {
                        rbMultiTag.Enabled = false;
                        rbEAS.Enabled = false;
                    }
                    else
                    {
                        rbMultiTag.Enabled = true;
                        rbEAS.Enabled = true;
                    }
                }
                else
                {
                    label41.Text = "First Byte Addr((Hex):";
                    rbMultiTag.Enabled = false;
                    rbEAS.Enabled = false;
                }
            }
        }

        private void radioButton20_CheckedChanged(object sender, EventArgs e)
        {

            if ((cbWorkMode.SelectedIndex == 1) || (cbWorkMode.SelectedIndex == 2) || (cbWorkMode.SelectedIndex == 3))
            {
                if (radioButton8.Checked)
                    cbReadWordNumber.Enabled = true;
                cbReadWordNumber.Items.Clear();
                if (radioButton20.Checked)
                {
                    for (int i = 1; i < 5; i++)
                        cbReadWordNumber.Items.Add(Convert.ToString(i));
                    cbReadWordNumber.SelectedIndex = 3;
                    labReadWordNumber.Text = "Read Byte Number:";
                    cbReadWordNumber.Enabled = true;
                    label41.Text = "First Byte Addr(Hex):";
                }
                else
                {
                    for (int i = 1; i < 33; i++)
                        cbReadWordNumber.Items.Add(Convert.ToString(i));
                    cbReadWordNumber.SelectedIndex = 0;
                    labReadWordNumber.Text = "Read Word Number:";
                    label41.Text = "First Word Addr((Hex):";
                }
                if (rbEPCC1G2.Checked)
                {
                    rbWordAddr.Enabled = false;
                    rbButeAddr.Enabled = false;
                    if (radioButton20.Checked)
                    {
                        rbMultiTag.Enabled = false;
                        rbEAS.Enabled = false;
                    }
                    else
                    {
                        rbMultiTag.Enabled = true;
                        rbEAS.Enabled = true;
                    }
                }
                else
                {
                    label41.Text = "First Byte Addr((Hex):";
                    rbMultiTag.Enabled = false;
                    rbEAS.Enabled = false;
                }
            }
        }

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            label41.Text = "First Word Addr";
        }

        private void radioButton17_CheckedChanged(object sender, EventArgs e)
        {
            label41.Text = "First Byte Addr";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbWorkMode.SelectedIndex == 0)
            {
                rbEPCC1G2.Enabled = false;
                RBiso180006B.Enabled = false;
                radioButton7.Enabled = false;
                radioButton8.Enabled = false;
                rbPassword.Enabled = false;
                rbEPC.Enabled = false;
                rbTID.Enabled = false;
                rbUser.Enabled = false;
                rbMultiTag.Enabled = false;
                rbActivateBuzzer.Enabled = false;
                rbDisEnableBuzzer.Enabled = false;
                rbWordAddr.Enabled = false;
                rbButeAddr.Enabled = false;
                rbSingleTag.Enabled = false;
                rbEAS.Enabled = false;
                radioButton20.Enabled = false;
                txtFirstWordAddr.Enabled = false;
                cbReadWordNumber.Enabled = false;
                cbSingleTagFilteringTime.Enabled = false;
            }
            if ((cbWorkMode.SelectedIndex == 1) | (cbWorkMode.SelectedIndex == 2) | (cbWorkMode.SelectedIndex == 3))
            {
                rbEPCC1G2.Enabled = true;
                RBiso180006B.Enabled = true;
                radioButton7.Enabled = true;
                radioButton8.Enabled = true;
                radioButton20.Enabled = true;
                cbReadWordNumber.Items.Clear();
                if (radioButton20.Checked)
                {
                    for (int i = 1; i < 5; i++)
                        cbReadWordNumber.Items.Add(Convert.ToString(i));
                    cbReadWordNumber.SelectedIndex = 3;
                    labReadWordNumber.Text = "Read Byte Number:";
                }
                else
                {
                    for (int i = 1; i < 33; i++)
                        cbReadWordNumber.Items.Add(Convert.ToString(i));
                    cbReadWordNumber.SelectedIndex = 0;
                    labReadWordNumber.Text = "Read Word Number:";
                }

                if (radioButton7.Checked)
                {
                    rbWordAddr.Enabled = true;
                    rbButeAddr.Enabled = true;
                }
                else
                {
                    rbWordAddr.Enabled = false;
                    rbButeAddr.Enabled = false;
                }
                if (rbEPCC1G2.Checked)
                {
                    rbPassword.Enabled = true;
                    rbEPC.Enabled = true;
                    rbTID.Enabled = true;
                    rbUser.Enabled = true;
                    rbSingleTag.Enabled = true;
                    if (radioButton20.Checked)    //Syris485
                    {
                        rbMultiTag.Enabled = false;
                        rbEAS.Enabled = false;
                    }
                    else
                    {
                        rbMultiTag.Enabled = true;
                        rbEAS.Enabled = true;
                    }
                    if ((rbMultiTag.Checked) || (rbEAS.Checked))
                        cbSingleTagFilteringTime.Enabled = false;
                    else
                        cbSingleTagFilteringTime.Enabled = true;
                }
                else
                    cbSingleTagFilteringTime.Enabled = true;
                rbActivateBuzzer.Enabled = true;
                rbDisEnableBuzzer.Enabled = true;
                txtFirstWordAddr.Enabled = true;
                if (radioButton7.Checked)
                    cbReadWordNumber.Enabled = false;
                else
                    cbReadWordNumber.Enabled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789ABCDEF".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            byte Accuracy;
            Accuracy = Convert.ToByte(cbEASAccuracy.SelectedIndex);
            fCmdRet = StaticClassReaderB.SetAccuracy(ref fComAdr, Accuracy, frmcomportindex);
            AddCmdLog("SetAccuracy", "SetAccuracy", fCmdRet);
        }

        private void button_settigtime_Click(object sender, EventArgs e)
        {
            byte TriggerTime;
            TriggerTime = Convert.ToByte(comboBox_tigtime.SelectedIndex);
            fCmdRet = StaticClassReaderB.SetTriggerTime(ref fComAdr, ref TriggerTime, frmcomportindex);
            AddCmdLog("SetTriggerTime", "Set TriggerTime", fCmdRet);
        }

        private void button_gettigtime_Click(object sender, EventArgs e)
        {
            byte TriggerTime;
            TriggerTime = 255;
            fCmdRet = StaticClassReaderB.SetTriggerTime(ref fComAdr, ref TriggerTime, frmcomportindex);
            if (fCmdRet == 0)
            {
                comboBox_tigtime.SelectedIndex = TriggerTime;
            }
            AddCmdLog("SetTriggerTime", "Get TriggerTime", fCmdRet);
        }

        private void button_OffsetTime_Click(object sender, EventArgs e)
        {
            byte OffsetTime;
            OffsetTime = Convert.ToByte(comboBox_OffsetTime.SelectedIndex);
            fCmdRet = StaticClassReaderB.SetOffsetTime(ref fComAdr, OffsetTime, frmcomportindex);
            AddCmdLog("SetOffsetTime", "SetOffsetTime", fCmdRet);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            byte[] Parameter = new byte[12];

            fCmdRet = StaticClassReaderB.GetWorkModeParameter(ref fComAdr, Parameter, frmcomportindex);
            if (fCmdRet == 0)
            {
                if (Parameter[0] == 0)
                {
                    rbWiegand26.Checked = true;
                    rbWiegandOutputMSBFirst.Checked = true;
                }
                if (Parameter[0] == 1)
                {
                    rbWiegand34.Checked = true;
                    rbWiegandOutputMSBFirst.Checked = true;
                }
                if (Parameter[0] == 2)
                {
                    rbWiegand26.Checked = true;
                    rbWiegandOutputLSBFirst.Checked = true;
                }
                if (Parameter[0] == 3)
                {
                    rbWiegand34.Checked = true;
                    rbWiegandOutputLSBFirst.Checked = true;
                }
                cbDataOutputInvterval.SelectedIndex = Convert.ToInt32(Parameter[1]);
                cbPulseInterval.SelectedIndex = Convert.ToInt32(Parameter[3] - 1);
                cbPulseWidth.SelectedIndex = Convert.ToInt32(Parameter[2] - 1);
                cbWorkMode.SelectedIndex = Convert.ToInt32(Parameter[4]);
                if ((Parameter[4] == 1) || (Parameter[4] == 2) || (Parameter[4] == 3))
                {
                    butGetData.Enabled = true;
                    butClearData.Enabled = true;
                    rbEPCC1G2.Enabled = true;
                    RBiso180006B.Enabled = true;
                    radioButton7.Enabled = true;
                    radioButton8.Enabled = true;

                    if (rbEPCC1G2.Checked)
                    {
                        if (radioButton7.Checked)
                        {
                            rbWordAddr.Enabled = true;
                            rbButeAddr.Enabled = true;
                        }
                        else
                        {
                            rbWordAddr.Enabled = false;
                            rbButeAddr.Enabled = false;
                        }
                        rbPassword.Enabled = true;
                        rbEPC.Enabled = true;
                        rbTID.Enabled = true;
                        rbUser.Enabled = true;
                        rbSingleTag.Enabled = true;
                        radioButton20.Enabled = true;
                        if (Convert.ToInt32((Parameter[5] & 0x10)) == 0x10)
                        {
                            rbMultiTag.Enabled = false;
                            rbEAS.Enabled = false;
                        }
                        else
                        {
                            rbMultiTag.Enabled = true;
                            rbEAS.Enabled = true;
                        }
                        if ((rbMultiTag.Checked) || (rbEAS.Checked))
                            cbSingleTagFilteringTime.Enabled = false;
                        else
                            cbSingleTagFilteringTime.Enabled = true;
                    }
                    else
                        cbSingleTagFilteringTime.Enabled = true;
                    rbActivateBuzzer.Enabled = true;
                    rbDisEnableBuzzer.Enabled = true;
                    txtFirstWordAddr.Enabled = true;
                    if ((radioButton8.Checked) || (radioButton20.Checked))
                        cbReadWordNumber.Enabled = true;
                }
                if (Parameter[4] == 0)
                {
                    butGetData.Enabled = false;
                    butClearData.Enabled = false;
                    rbEPCC1G2.Enabled = false;
                    RBiso180006B.Enabled = false;
                    radioButton7.Enabled = false;
                    radioButton8.Enabled = false;
                    rbPassword.Enabled = false;
                    rbEPC.Enabled = false;
                    rbTID.Enabled = false;
                    rbUser.Enabled = false;
                    rbMultiTag.Enabled = false;
                    rbActivateBuzzer.Enabled = false;
                    rbDisEnableBuzzer.Enabled = false;
                    rbWordAddr.Enabled = false;
                    rbButeAddr.Enabled = false;
                    rbSingleTag.Enabled = false;
                    rbEAS.Enabled = false;
                    radioButton20.Enabled = false;
                    txtFirstWordAddr.Enabled = false;
                    cbReadWordNumber.Enabled = false;
                    cbSingleTagFilteringTime.Enabled = false;
                }
                if (Convert.ToInt32((Parameter[5]) & 0x01) == 0)
                    rbEPCC1G2.Checked = true;
                else
                    RBiso180006B.Checked = true;
                if (Convert.ToInt32((Parameter[5]) & 0x02) == 0)
                    radioButton7.Checked = true;
                else
                {
                    if (Convert.ToInt32((Parameter[5] & 0x10)) == 0)
                        radioButton8.Checked = true;
                    else
                        radioButton20.Checked = true;
                }
                if (Convert.ToInt32((Parameter[5]) & 0x04) == 0)
                    rbActivateBuzzer.Checked = true;
                else
                    rbDisEnableBuzzer.Checked = true;
                if (Convert.ToInt32((Parameter[5]) & 0x08) == 0)
                    rbWordAddr.Checked = true;
                else
                    rbButeAddr.Checked = true;
                switch (Parameter[6])
                {
                    case 0:
                        rbPassword.Checked = true;
                        break;
                    case 1:
                        rbEPC.Checked = true;
                        break;
                    case 2:
                        rbTID.Checked = true;
                        break;
                    case 3:
                        rbUser.Checked = true;
                        break;
                    case 4:
                        rbMultiTag.Checked = true;
                        break;
                    case 5:
                        rbSingleTag.Checked = true;
                        break;
                    case 6:
                        rbEAS.Checked = true;
                        break;
                    default:
                        break;
                }
                txtFirstWordAddr.Text = Convert.ToString(Parameter[7], 16).PadLeft(2, '0');
                cbReadWordNumber.SelectedIndex = Convert.ToInt32(Parameter[8] - 1);
                cbSingleTagFilteringTime.SelectedIndex = Convert.ToInt32(Parameter[9]);
                cbEASAccuracy.SelectedIndex = Convert.ToInt32(Parameter[10]);
                comboBox_OffsetTime.SelectedIndex = Convert.ToInt32(Parameter[11]);
            }
            AddCmdLog("GetWorkModeParameter", "GetWorkModeParameter", fCmdRet);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (!timer1.Enabled)
            {
                butGetData.Text = "Get";
            }
            else
            {
                butGetData.Text = "Stop";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                fIsInventoryScan = true;
            GetData();
            if (fAppClosed)
                Close();
            fIsInventoryScan = false;
        }
        private void InitComList()
        {
            int i = 0;
            ComboBox_COM.Items.Clear();
            ComboBox_COM.Items.Add(" AUTO");
            for (i = 1; i < 13; i++)
                ComboBox_COM.Items.Add(" COM" + Convert.ToString(i));
            ComboBox_COM.SelectedIndex = 0;
            RefreshStatus();
        }
        private void InitReaderList()
        {
            int i = 0;
            // ComboBox_PowerDbm.SelectedIndex = 0;
            ComboBox_baud.SelectedIndex = 3;
            for (i = 0; i < 63; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 62;
            ComboBox_dminfre.SelectedIndex = 0;
            for (i = 0x03; i <= 0xff; i++)
                ComboBox_scantime.Items.Add(Convert.ToString(i) + "*100ms");
            ComboBox_scantime.SelectedIndex = 7;


            i = 40;
            while (i <= 300)
            {
                i = i + 10;
            }

            for (i = 0; i < 256; i++)
            {
                cbDataOutputInvterval.Items.Add(Convert.ToString(i) + "*10ms");
            }
            cbDataOutputInvterval.SelectedIndex = 30;
            for (i = 1; i < 256; i++)
            {
                cbPulseWidth.Items.Add(Convert.ToString(i) + "*10us");
            }
            cbPulseWidth.SelectedIndex = 9;
            for (i = 1; i < 256; i++)
            {
                cbPulseInterval.Items.Add(Convert.ToString(i) + "*100us");
            }
            cbPulseInterval.SelectedIndex = 14;
            for (i = 0; i < 256; i++)
            {
                cbSingleTagFilteringTime.Items.Add(Convert.ToString(i) + "*1s");
            }
            cbSingleTagFilteringTime.SelectedIndex = 0;
            for (i = 1; i < 33; i++)
            {
                cbReadWordNumber.Items.Add(Convert.ToString(i));
            }
            cbReadWordNumber.SelectedIndex = 0;
            cbWorkMode.SelectedIndex = 0;
            ComboBox_PowerDbm.SelectedIndex = 30;
            cbEASAccuracy.SelectedIndex = 8;
            for (i = 0; i < 101; i++)
            {
                comboBox_OffsetTime.Items.Add(Convert.ToString(i) + "*1ms");
            }
            comboBox_OffsetTime.SelectedIndex = 5;


            for (i = 0; i < 255; i++)
                comboBox_tigtime.Items.Add(Convert.ToString(i) + "*1s");
            comboBox_tigtime.SelectedIndex = 0;   //
        }
        private void RefreshStatus()
        {
            if (!(ComboBox_AlreadyOpenCOM.Items.Count != 0))
                StatusBar1.Panels[1].Text = "COM Closed";
            else
                StatusBar1.Panels[1].Text = " COM" + Convert.ToString(frmcomportindex);
            StatusBar1.Panels[0].Text = "";
            StatusBar1.Panels[2].Text = "";
        }
        private void ClearLastInfo()
        {
            ComboBox_AlreadyOpenCOM.Refresh();
            RefreshStatus();
            Edit_Type.Text = "";
            Edit_Version.Text = "";
            ISO180006B.Checked = false;
            EPCC1G2.Checked = false;
            Edit_ComAdr.Text = "";
            Edit_powerdBm.Text = "";
            Edit_scantime.Text = "";
            Edit_dminfre.Text = "";
            Edit_dmaxfre.Text = "";
            //  PageControl1.TabIndex = 0;
        }
        private void AddCmdLog(string CMD, string cmdStr, int cmdRet)
        {
            try
            {
                StatusBar1.Panels[0].Text = "";
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " " +
                                            cmdStr + ": " +
                                            GetReturnCodeDesc(cmdRet);
            }
            finally
            {
                ;
            }
        }
        private string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "Operation Successed";
                case 0x01:
                    return "Return before Inventory finished";
                case 0x02:
                    return "the Inventory-scan-time overflow";
                case 0x03:
                    return "More Data";
                case 0x04:
                    return "Reader module MCU is Full";
                case 0x05:
                    return "Access Password Error";
                case 0x09:
                    return "Destroy Password Error";
                case 0x0a:
                    return "Destroy Password Error Cannot be Zero";
                case 0x0b:
                    return "Tag Not Support the command";
                case 0x0c:
                    return "Use the commmand,Access Password Cannot be Zero";
                case 0x0d:
                    return "Tag is protected,cannot set it again";
                case 0x0e:
                    return "Tag is unprotected,no need to reset it";
                case 0x10:
                    return "There is some locked bytes,write fail";
                case 0x11:
                    return "can not lock it";
                case 0x12:
                    return "is locked,cannot lock it again";
                case 0x13:
                    return "Parameter Save Fail,Can Use Before Power";
                case 0x14:
                    return "Cannot adjust";
                case 0x15:
                    return "Return before Inventory finished";
                case 0x16:
                    return "Inventory-Scan-Time overflow";
                case 0x17:
                    return "More Data";
                case 0x18:
                    return "Reader module MCU is full";
                case 0x19:
                    return "Not Support Command Or AccessPassword Cannot be Zero";
                case 0xFA:
                    return "Get Tag,Poor Communication,Inoperable";
                case 0xFB:
                    return "No Tag Operable";
                case 0xFC:
                    return "Tag Return ErrorCode";
                case 0xFD:
                    return "Command length wrong";
                case 0xFE:
                    return "Illegal command";
                case 0xFF:
                    return "Parameter Error";
                case 0x30:
                    return "Communication error";
                case 0x31:
                    return "CRC checksummat error";
                case 0x32:
                    return "Return data length error";
                case 0x33:
                    return "Communication busy";
                case 0x34:
                    return "Busy,command is being executed";
                case 0x35:
                    return "ComPort Opened";
                case 0x36:
                    return "ComPort Closed";
                case 0x37:
                    return "Invalid Handle";
                case 0x38:
                    return "Invalid Port";
                case 0xEE:
                    return "Return command error";
                default:
                    return "";
            }
        }
        private string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "Other error";
                case 0x03:
                    return "Memory out or pc not support";
                case 0x04:
                    return "Memory Locked and unwritable";
                case 0x0b:
                    return "No Power,memory write operation cannot be executed";
                case 0x0f:
                    return "Not Special Error,tag not support special errorcode";
                default:
                    return "";
            }
        }
        private void GetData()
        {
            byte[] ScanModeData = new byte[40960];
            int ValidDatalength, i;
            string temp, temps;
            ValidDatalength = 0;
            fCmdRet = StaticClassReaderB.ReadActiveModeData(ScanModeData, ref ValidDatalength, frmcomportindex);
            if (fCmdRet == 0)
            {
                /*
                temp = "";
                temps = ByteArrayToHexString(ScanModeData);
                for (i = 0; i < ValidDatalength; i++)
                {
                    temp = temp + temps.Substring(i * 2, 2) + " ";
                }*/
                temp = ByteArrayToHexString(ScanModeData, 4, ValidDatalength - 6);
                if (ValidDatalength > 0)
                    listBox3.Items.Add(temp);
                listBox3.SelectedIndex = listBox3.Items.Count - 1;
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 操作成功";
            }
            else
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 操作失败";

        }

        public static string ByteArrayToHexString(byte[] bytArray, int start, int length)
        {
            StringBuilder stringBuilder = new StringBuilder(bytArray.Length * 2);
            for (int i = start; i < length + start; i++)
            {
                try
                {
                    stringBuilder.Append(Convert.ToString(bytArray[i], 16).PadLeft(2, '0'));
                }
                catch
                {
                }
            }

            return stringBuilder.ToString().ToUpper();
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }

        private void button21_Click(object sender, EventArgs e)
        {

            am.openport1();
            Thread.Sleep(60);
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            am.openportall();
            Thread.Sleep(50);
            return;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            am.closeportall();
            Thread.Sleep(50);
            return;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            am.openport2();
            Thread.Sleep(60);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            am.openport3();
            Thread.Sleep(60);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            am.openport4();
            Thread.Sleep(60);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            am.closeport1();
            Thread.Sleep(60);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            am.closeport2();
            Thread.Sleep(60);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            am.closeport3();
            Thread.Sleep(60);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            am.closeport4();
            Thread.Sleep(60);
        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void butSetWorkMode_Click(object sender, EventArgs e)
        {

            int Reader_bit0;
            int Reader_bit1;
            int Reader_bit2;
            int Reader_bit3;
            int Reader_bit4;
            byte[] Parameter = new byte[6];
            Parameter[0] = Convert.ToByte(cbWorkMode.SelectedIndex);
            if (rbEPCC1G2.Checked)
                Reader_bit0 = 0;
            else
                Reader_bit0 = 1;
            if (radioButton7.Checked)
                Reader_bit1 = 0;
            else
                Reader_bit1 = 1;
            if (rbActivateBuzzer.Checked)
                Reader_bit2 = 0;
            else
                Reader_bit2 = 1;
            if (rbWordAddr.Checked)
                Reader_bit3 = 0;
            else
                Reader_bit3 = 1;
            if (radioButton20.Checked)
                Reader_bit4 = 1;
            else
                Reader_bit4 = 0;
            Parameter[1] = Convert.ToByte(Reader_bit0 * 1 + Reader_bit1 * 2 + Reader_bit2 * 4 + Reader_bit3 * 8 + Reader_bit4 * 16);
            if (rbPassword.Checked)
                Parameter[2] = 0;
            if (rbEPC.Checked)
                Parameter[2] = 1;
            if (rbTID.Checked)
                Parameter[2] = 2;
            if (rbUser.Checked)
                Parameter[2] = 3;
            if (rbMultiTag.Checked)
                Parameter[2] = 4;
            if (rbSingleTag.Checked)
                Parameter[2] = 5;
            if (rbEAS.Checked)
                Parameter[2] = 6;
            if (txtFirstWordAddr.Text == "")
            {
                MessageBox.Show("Address is NULL!", "Information");
                return;
            }
            Parameter[3] = Convert.ToByte(txtFirstWordAddr.Text, 16);
            Parameter[4] = Convert.ToByte(cbReadWordNumber.SelectedIndex + 1);
            Parameter[5] = Convert.ToByte(cbSingleTagFilteringTime.SelectedIndex);

            fCmdRet = StaticClassReaderB.SetWorkMode(ref fComAdr, Parameter, frmcomportindex);
            if (fCmdRet == 0)
            {
                if ((cbWorkMode.SelectedIndex == 1) | (cbWorkMode.SelectedIndex == 2) | (cbWorkMode.SelectedIndex == 3))
                {
                    if (RBiso180006B.Checked)
                    {
                        rbMultiTag.Enabled = false;
                        rbEAS.Enabled = false;
                    }
                    else
                    {
                        if (radioButton20.Checked)
                        {
                            rbMultiTag.Enabled = false;
                            rbEAS.Enabled = false;
                        }
                    }
                    butGetData.Enabled = true;
                    butClearData.Enabled = true;
                }
                if (cbWorkMode.SelectedIndex == 0)
                {
                    butGetData.Enabled = false;
                    butClearData.Enabled = false;
                    butGetData.Text = "Get";
                    timer1.Enabled = false;
                }
            }
            AddCmdLog("SetWorkMode", "SetWorkMode", fCmdRet);
        }
        private string[] LoadCommunication()
        {
            IniSettings.LoadCommunication();

            string[] portNames = SerialPort.GetPortNames();
            return portNames;




        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {

                string[] portNames = LoadCommunication();
                this.groupBox4.Enabled = true;


                cmbAlarmPortName.Items.Clear();
                foreach (string st in portNames)
                {
                    int i = 0;
                    for (i = st.Length - 1; i > 3; i--)
                    {
                        if (AsadDuooSystemPub.IsUint(st.Substring(i, 1)))
                        {
                            break;
                        }
                    }
                    cmbAlarmPortName.Items.Add(st.Substring(0, (i + 1)));
                }

                try
                {
                    cmbAlarmPortName.Text = IniSettings.PortName;
                    cmbAlarmBaudRate.Text = IniSettings.BaudRate.ToString();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }


            }
            else
            {
                cmbAlarmPortName.Items.Clear();
                cmbAlarmBaudRate.Items.Clear();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (am.OpenAlarm())
            {
                am.openportall();
                Thread.Sleep(1000);
                am.closeportall();
                this.groupBox4.Enabled = true;
            }
            else
            {
                MessageBox.Show("Com通信失败，请检查线路");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
            {

                this.groupBox5.Enabled = true;

                string[] portNames = LoadCommunication();





                cmbBracodePortName.Items.Clear();
                foreach (string st in portNames)
                {
                    int i = 0;
                    for (i = st.Length - 1; i > 3; i--)
                    {
                        if (AsadDuooSystemPub.IsUint(st.Substring(i, 1)))
                        {
                            break;
                        }
                    }
                    cmbBracodePortName.Items.Add(st.Substring(0, (i + 1)));
                }

                try
                {
                    cmbBracodePortName.Text = IniSettings.PortName;
                    cmbBracodeBaudRate.Text = IniSettings.BaudRate.ToString();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }


            }
            else
            {
                cmbBracodePortName.Items.Clear();
                cmbBracodeBaudRate.Items.Clear();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (mIsStart)
            {
                ledAllTimes.Text = DateTime.Now.Subtract(dtStart).TotalSeconds.ToString("0.00");
                int nRunTimes = (int)DateTime.Now.Subtract(dtStart).TotalSeconds;
                if (nudRunTimes.Value != 0 && nRunTimes >= nudRunTimes.Value)
                {
                    ScanStop();
                }
            }
            switch (mIsStatus)
            {
                case READ_EXIT:
                    DataTagTableUpdate(true);

                    mIsStatus = READ_STOP;
                    mIsStart = false;

                    break;
                case READ_START:
                    mIsStart = true;

                    mIsStatus = READ_SETANT;
                    break;
                case READ_SETANT:
                    if (mIsStop) return;

                    mCurrentAnt = ChangeAnt();

                    if (mCurrentAnt == 0)
                    {
                        mIsStatus = READ_IDENTIFY;
                    }
                    else if (mCurrentAnt >= 1 && mCurrentAnt <= 16)
                    {
                        mIsStatus = READ_IDENTIFY;
                    }
                    Application.DoEvents();
                    break;
                case READ_SETANTOK:
                    mIsStatus = READ_IDENTIFY;
                    break;
                case READ_IDENTIFY:
                    if (mIsStop) return;
                    if (mLoopInterval++ < (nudInventoryInterval.Value / 10))
                    {
                        return;
                    }
                    mLoopDelay = 0;
                    mLoopInterval = 0;
                    if (mCurrentAnt == 0)
                    {
                        AsadDuooSystemPub.SendSio(new ProtocolPacket(AsadDuooSystemPub.RcpBase.Address, RcpBase.RCP_MM_READ_C_UII, RcpBase.RCP_MSG_CMD));
                    }
                    else if (mCurrentAnt >= 1 && mCurrentAnt <= 16)
                    {
                        List<byte> param = new List<byte>();
                        param.Add(1);
                        param.Add((byte)mCurrentAnt);
                        AsadDuooSystemPub.SendSio(new ProtocolPacket(AsadDuooSystemPub.RcpBase.Address, RcpBase.RCP_MM_READ_C_UII, RcpBase.RCP_MSG_CMD, param.ToArray()));
                    }
                    mIsStatus = READ_IDENTIFY_WAIT;
                    Application.DoEvents();
                    break;
                case READ_IDENTIFYOK:
                    mIsStatus = READ_SETANT;
                    break;
                case READ_SETANT_WAIT:
                case READ_IDENTIFY_WAIT:
                    if (mLoopDelay++ > ((mCommandDelay * 1000) / 10))
                    {
                        mIsStatus = READ_SETANT;
                        return;
                    }
                    break;
                default:
                    break;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (AsadDuooSystemPub.IsConnectedSio)
            {
                AsadDuooSystemPub.SioBase.onReceived -= SioBase_onReceived;
                AsadDuooSystemPub.DisConnectSio();
                AsadDuooSystemPub.RcpBase.Address = 65535;
                return;
            }
            InitSio(IniSettings.Communication);
            AsadDuooSystemPub.SioBase.Connect(IniSettings.HostName, IniSettings.HostPort);
            if (AsadDuooSystemPub.IsConnectedSio)
            {
                AsadDuooSystemPub.SioBase.onReceived += SioBase_onReceived;
            }
            button1.Text = AsadDuooSystemPub.IsConnectedSio ? "OPEN" : "CLOSE";


            /*

            string portName = cmbAsandRFIDPortName.SelectedItem.ToString();
            int BaudRate = Convert.ToInt32(cmbAsandRFIDBaudRate.Text);
            int conn = AsadHelper.connRfidCOM(portName,BaudRate);
           // InitSio(IniSettings.Communication);
         //   AsadDuooSystemPub.SioBase.Connect(IniSettings.HostName, IniSettings.HostPort);
            if (conn == 0)
            {


                //AsadDuooSystemPub.SioBase.onReceived += SioBase_onReceived;
                button3.Text = "OPEN";
            }
            else
            {
              //  AsadDuooSystemPub.SioBase.onReceived -= SioBase_onReceived;
            //    AsadDuooSystemPub.DisConnectSio();
              //  AsadDuooSystemPub.RcpBase.Address = 65535;

                button3.Text =  "CLOSE";
            }

            */


        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            if (mIsStart)
            {
                ScanStop();
            }
            else
            {
                ScanStart();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] ASportNames = AsadHelper.getConnComs();
            this.cmbAsandRFIDPortName.Items.Clear();
            foreach (var port in ASportNames)
            {
                this.cmbAsandRFIDPortName.Items.Add(port);
            }



            string[] portNames = LoadCommunication();
            ComboBox_COM.Items.Clear();
            foreach (string st in portNames)
            {
                int i = 0;
                for (i = st.Length - 1; i > 3; i--)
                {
                    if (AsadDuooSystemPub.IsUint(st.Substring(i, 1)))
                    {
                        break;
                    }
                }
                ComboBox_COM.Items.Add(st.Substring(0, (i + 1)));
            }

            try
            {
                ComboBox_COM.Text = IniSettings.PortName;
                ComboBox_baud2.Text = IniSettings.BaudRate.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            /*
            string[] portNames = LoadCommunication();

            cmbAsandRFIDPortName.Items.Clear();
            foreach (string st in portNames)
            {
                int i = 0;
                for (i = st.Length - 1; i > 3; i--)
                {
                    if (SystemPub.IsUint(st.Substring(i, 1)))
                    {
                        break;
                    }
                }
                cmbAsandRFIDPortName.Items.Add(st.Substring(0, (i + 1)));
            }

            try
            {
                cmbAsandRFIDPortName.Text = IniSettings.PortName;
                cmbAsandRFIDBaudRate.Text = IniSettings.BaudRate.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            */

        }

        private void RcpBase_RxRspParsed(object sender, ProtocolEventArgs e)
        {
            if (this.IsDisposed)
                return;

            if (!this.InvokeRequired)
            {
                __ParseRsp(e.Protocol);
                return;
            }

            this.Invoke(new MethodInvoker(delegate ()
            {
                try
                {
                    __ParseRsp(e.Protocol);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }));
        }

        public void InterfaceProtocolPacket(ProtocolPacket protocolPacket)
        {
            ucMMainPageParseRsp(protocolPacket);
        }
        public void ucMMainPageParseRsp(ProtocolPacket Data)
        {
            switch (Data.Code)
            {
                case RcpBase.RCP_MM_READ_C_UII:
                case RcpBase.RCP_MM_READ_C_DT:
                case RcpBase.RCP_MM_WRITE_C_DT:
                case RcpBase.RCP_MM_GET_ACCESS_EPC_MATCH:
                case RcpBase.RCP_MM_SET_ACCESS_EPC_MATCH:
                    ucMReadDemoParseRsp(Data);
                    break;
                default:
                    break;
            }
        }
        public void ucMReadDemoParseRsp(ProtocolPacket Data)
        {
            int rtn = (Data.Type & 0x7f);
            switch (Data.Code)
            {
                case RcpBase.RCP_MM_PARA:
                    if (Data.Length > 0)
                    {
                        isStopAutoRead = Data.Payload[1];

                    }
                    break;
                case RcpBase.RCP_MM_ANT:

                    if (Data.Length >= 3)
                    {

                    }
                    else
                    {
                        mIsStatus = READ_SETANTOK;
                    }
                    break;
                case RcpBase.RCP_MM_READ_C_UII:
                    if (rtn == RcpBase.RCP_MSG_NOTI || rtn == RcpBase.RCP_MSG_AUTO)
                    {
                        string ant = "";
                        string tag_rssi = "";
                        string epc = "";
                        string pc = "";
                        int pcepclen = 0;
                        int datalen = 0;
                        ant = Data.Payload[0].ToString();
                        pcepclen = GetCodelen(Data.Payload[1]);
                        datalen = Data.Length - 2;//去掉天线号去掉rssi
                        pc = ConvertData.ByteArrayToHexString(Data.Payload, 1, 2);
                        string dddc = ConvertData.ByteArrayToHexString(Data.Payload);

                        epc = ConvertData.ByteArrayToHexString(Data.Payload, 3, pcepclen - 2);

                        if ((datalen - pcepclen) > 0)
                            epc = ConvertData.ByteArrayToHexString(Data.Payload, 3, datalen);
                        else
                            epc = ConvertData.ByteArrayToHexString(Data.Payload, 3, pcepclen - 2);

                        tag_rssi = GetRssi(Data.Payload[Data.Length - 1]) + "dBm";

                        DataTagTableStringInsert(pc, epc, ant, tag_rssi);
                    }
                    else if (rtn == RcpBase.RCP_MSG_OK || rtn == RcpBase.RCP_MSG_ERR)
                    {
                        ledAllTags.Text = nAllTags.ToString();


                        ledInventoryTimes.Text = DateTime.Now.Subtract(dtStart).TotalSeconds.ToString("0.00");


                        if (mIsStatus == READ_IDENTIFY_WAIT)
                        {
                            mIsStatus = READ_IDENTIFYOK;
                        }

                    }
                    break;
            }
        }
        private void RcpBase_TxRspParsed(object sender, ProtocolEventArgs e)
        {

            byte[] data = e.Data;
            foreach (var item in data)
            {
                textBox1.AppendText(item.ToString());
            }

        }



        private void __ParseRsp(ProtocolPacket protocolPacket)
        {

            switch (protocolPacket.Code)
            {
                case RcpBase.RCP_CMD_INFO:
                    if (protocolPacket.Length > 30 && (protocolPacket.Type & 0x7f) == 0)
                    {
                        #region ---Parameter---
                        string strInfo = Encoding.ASCII.GetString(protocolPacket.Payload, 0, protocolPacket.Length);


                        #endregion
                    }
                    break;
            }
            InterfaceProtocolPacket(protocolPacket);
        }

        private void SioBase_onReceived(object sender, ADSioLib.ReceivedEventArgs e)
        {
            AsadDuooSystemPub.RcpBase.ReciveBytePkt(e.Data);
        }

        private void InitSio(int commType)
        {
            IniSettings.PortName = cmbAsandRFIDPortName.Text;
            IniSettings.BaudRate = Convert.ToInt32(cmbAsandRFIDBaudRate.Text);
            if (AsadDuooSystemPub.IsConnectedSio)
            {
                AsadDuooSystemPub.DisConnectSio();
            }
            AsadDuooSystemPub.SioBase = new SioCom();
        }



        private void SetAntHeadText(int ant)
        {
            if (ant > nMaxAnt)
            {
                nMaxAnt = ant;
                if (nMaxAnt > 0)
                {
                    cAnt.Text = "ANT1";
                    for (int m = 1; m < nMaxAnt; m++)
                    {
                        cAnt.Text += "/ANT" + (m + 1);
                    }
                    if (nMaxAnt <= 2)
                        cAnt.Width = 50 * nMaxAnt;
                    else
                        cAnt.Width = 40 * nMaxAnt;
                }
                else
                {
                    cAnt.Text = "ANT";
                    cAnt.Width = 40 * 1;
                }
            }
        }

        private int GetCodelen(byte iData)
        {
            return (((iData >> 3) + 1) * 2);
        }

        private string GetRssi(byte rssi)
        {
            int rssidBm = (sbyte)rssi; // rssidBm is negative && in bytes
            rssidBm -= Convert.ToInt32("-20", 10);
            rssidBm -= Convert.ToInt32("3", 10);
            return rssidBm.ToString();
        }

        private void ScanStart()
        {
            mIsStop = false;
            nReadShowRate = 50;
            mCommandDelay = 2;
            if (nudMaxTag.Value > 100)
            {
                mCommandDelay = (int)(nudMaxTag.Value / 100) * 2;
            }
            InitInventoryPara();
            InitAnt();
            mIsStatus = READ_START;
        }

        private void ScanStop()
        {
            mIsStatus = READ_EXIT;
        }

        private void InitInventoryPara()
        {
            this.listViewEPC.Items.Clear();

            this.m_dtTagTable.Table.Rows.Clear();
            nInventoryCount = 0;
            nInventoryTags = 0;
            nAllTags = 0;
            ledInventoryTags.Text = "0";
            ledInventoryTimes.Text = "0";
            ledAllTags.Text = "0";
            ledAllTimes.Text = "0";
            dtStart = DateTime.Now;
        }



        #region ---mult ant deal---
        int nAntIndex = 0;
        List<int> nAntChooseList = new List<int>();

        private void InitAnt()
        {
            nAntIndex = 0;
            nAntChooseList = AsadDuooSystemPub.AntCurrentListInt;
            if (nAntChooseList.Count > 0)
                ledCurrentAnt.Text = nAntChooseList[nAntIndex].ToString();
            else
                ledCurrentAnt.Text = "0";
        }

        private int ChangeAnt()
        {
            int CurrentAnt = 0xff;
            if (nudInventoryCount.Value != 0 && nInventoryCount >= nudInventoryCount.Value)
            {
                ScanStop();
                return CurrentAnt;
            }

            if (AsadDuooSystemPub.RcpBase.Type != "S" && AsadDuooSystemPub.RcpBase.Type != "M" && AsadDuooSystemPub.RcpBase.Type != "L")
            {
                CurrentAnt = 0;
                ledCurrentAnt.Text = CurrentAnt.ToString();
                return CurrentAnt;
            }

            if (nAntChooseList.Count == 0)
            {
                ScanStop();
                MessageBox.Show("Have not choose Ant!", "Ant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CurrentAnt;
            }

            byte[] ant = new byte[2] { 0xff, 0xff };
            byte current = (byte)(nAntChooseList[nAntIndex]);
            CurrentAnt = current;


            ledCurrentAnt.Text = current.ToString();
            nAntIndex++;
            if (nAntIndex >= nAntChooseList.Count)
            {
                nAntIndex = 0;
                nInventoryCount++;
            }
            return CurrentAnt;
        }

        #endregion

        TagsTable m_dtTagTable = new TagsTable();

        private string GetAntString(DataRow row)
        {
            string strAnt = row["ANT00"].ToString();
            if (nMaxAnt > 0)
            {
                strAnt = row[9].ToString().PadLeft(4, ' ');

                for (int m = 1; m < nMaxAnt; m++)
                {
                    strAnt += " / " + row[9 + m].ToString().PadLeft(4, ' ');
                }
            }
            return strAnt;
        }

        private void DataTagTableStringInsert(string pc, string epc, string ant, string rssi)
        {
            if (this.InvokeRequired)
            {
                TagsTableInsertStringUnsafe InvokeRefreshInsert = new TagsTableInsertStringUnsafe(DataTagTableStringInsert);
                this.Invoke(InvokeRefreshInsert, new object[] { pc, epc, ant, rssi });
            }
            else
            {
                nAllTags++;
                SetAntHeadText(Convert.ToInt32(ant));
                if (nInventoryTags >= nudMaxTag.Value)
                {
                    DataTagTableUpdate(false);
                    return;
                }
                if (m_dtTagTable.DataTagTableInsert(pc, epc, ant, rssi))
                {
                    nInventoryTags = m_dtTagTable.Table.Rows.Count; //标签总数
                    ledInventoryTags.Text = nInventoryTags.ToString(); //标签总数
                    ledInventoryTimes.Text = DateTime.Now.Subtract(dtStart).TotalSeconds.ToString("0.00");
                }
                if (nInventoryTags >= nudMaxTag.Value)
                {
                    ScanStop();
                }
                DataTagTableUpdate(false);
            }
        }

        private void DataTagTableUpdate(bool flag)
        {
            if (this.InvokeRequired)
            {
                TagsTableUpdateUnsafe InvokeRefreshShow = new TagsTableUpdateUnsafe(DataTagTableUpdate);
                this.Invoke(InvokeRefreshShow, new object[] { flag });
            }
            else
            {
                //形成列表
                int nEpcCountFS = listViewEPC.Items.Count;
                int nEpcLengthFS = m_dtTagTable.Table.Rows.Count;
                if (nEpcCountFS < nEpcLengthFS)
                {
                    DataRow rowfs = m_dtTagTable.Table.Rows[nEpcLengthFS - 1];
                    ListViewItem itemfs = new ListViewItem();
                    itemfs.Text = (nEpcCountFS + 1).ToString();
                    itemfs.SubItems.Add(rowfs["PC"].ToString());
                    itemfs.SubItems.Add(rowfs["EPC"].ToString());
                    itemfs.SubItems.Add(rowfs["COUNT"].ToString());

                    itemfs.SubItems.Add(GetAntString(rowfs));

                    itemfs.SubItems.Add(rowfs["RSSI"].ToString());
                    listViewEPC.Items.Add(itemfs);
                    listViewEPC.Items[nEpcCountFS].EnsureVisible();
                }

                DataTagTableShow(flag);
            }
        }

        private void DataTagTableShow(bool flag)
        {
            if (this.InvokeRequired)
            {
                TagsTableShowUnsafe InvokeRefreshUpdate = new TagsTableShowUnsafe(DataTagTableShow);
                this.Invoke(InvokeRefreshUpdate, new object[] { flag });
            }
            else
            {
                //更新列表中读取的次数
                if (nAllTags % nReadShowRate == 1 || DateTime.Now.Subtract(nReadShowTime).TotalSeconds > 2 || flag)
                {
                    int nIndex = 0;
                    foreach (DataRow rowfs in m_dtTagTable.Table.Rows)
                    {
                        ListViewItem itemfs = listViewEPC.Items[nIndex];
                        itemfs.SubItems[3].Text = rowfs["COUNT"].ToString();
                        itemfs.SubItems[4].Text = GetAntString(rowfs);
                        itemfs.SubItems[5].Text = rowfs["RSSI"].ToString();
                        nIndex++;
                    }
                    nReadShowTime = DateTime.Now;
                }
            }
        }

    }
}
