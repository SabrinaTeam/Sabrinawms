using ADRcpLib;
using ADSioLib;
using ADUtilsLib.Initializer;
using ADUtilsLib.Utils;
using BLL;
using COMMON;
using MODEL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmRFIDBoxScan : Form
    {
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



        private static FrmRFIDBoxScan frm;
        public AlarmManager am = new AlarmManager();
        public LuluSingleScanManager lscm = new LuluSingleScanManager();
        public BoxsScanManager bsm = new BoxsScanManager();

        private static readonly string strOpenAlarm = ConfigurationManager.ConnectionStrings["OpenAlarm"].ConnectionString; //报警器灯光
        private static readonly string strOpenMedia = ConfigurationManager.ConnectionStrings["OpenMedia"].ConnectionString;//报警器声音
        private static readonly string BoxCartonCOM = ConfigurationManager.ConnectionStrings["BoxCartonCOM"].ConnectionString; //
        private static readonly string BoxRFIDCOM = ConfigurationManager.ConnectionStrings["BoxRFIDCOM"].ConnectionString; //

        private string responseStr = ""; //扫描回来的外箱条码号
        private string scanno = "";
        public static DataTable newCarton;
        DataTable BoxDetailsLog;
        List<int> sizesQtys;
        List<int> ScanSQtys;
        private List<LuluSingleScanPacklist> dpllist = new List<LuluSingleScanPacklist>();
        List<string> rfids = new List<string>(); // RFID 单件集合，计算扫了多少件

        System.Timers.Timer Mytimer;
        private long mytimeCount = 0; // 计时器
        int scanTimes = 120;//RFID感应时长设置
        public delegate void SetControlValue(DataTable RFIDTables);

        DataTable RFIDNUMBERS;
        DataTable RFIDBoxs;  // 一共扫描的箱子


        public FrmRFIDBoxScan()
        {
            InitializeComponent();

            if (newCarton != null)
            {
                newCarton.Rows.Clear();
                newCarton.Columns.Clear();
            }
            AsadDuooSystemPub.RcpBase.RxRspParsed += RcpBase_RxRspParsed;
            AsadDuooSystemPub.RcpBase.TxRspParsed += RcpBase_TxRspParsed;

          //  this.dgvBoxHeads.DoubleBufferedDataGirdView(true);
            this.dgvBoxDetails.DoubleBufferedDataGirdView(true);
        }


        private void RcpBase_TxRspParsed(object sender, ProtocolEventArgs e)
        {

            byte[] data = e.Data;
            foreach (var item in data)
            {
                textBox1.AppendText(item.ToString());
            }

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
        private void __ParseRsp(ProtocolPacket protocolPacket)
        {

            switch (protocolPacket.Code)
            {
                case RcpBase.RCP_CMD_INFO:
                    if (protocolPacket.Length > 30 && (protocolPacket.Type & 0x7f) == 0)
                    {
                        string strInfo = Encoding.ASCII.GetString(protocolPacket.Payload, 0, protocolPacket.Length);
                    }
                    break;
            }
            InterfaceProtocolPacket(protocolPacket);
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
        TagsTable m_dtTagTable = new TagsTable();
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
        private void ScanStop()
        {
            mIsStatus = READ_EXIT;
            this.butReScan.Enabled = true;
            this.butReScan.Text = "重新扫描";

            this.RFIDCheck();
            // 开始比较是不是这个箱子与数量
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
                int nEpcCountFS = dgvBoxHeads.Items.Count;
                int nEpcLengthFS = m_dtTagTable.Table.Rows.Count;
                if (nEpcCountFS < nEpcLengthFS)
                {
                    DataRow rowfs = m_dtTagTable.Table.Rows[nEpcLengthFS - 1];
                    ListViewItem itemfs = new ListViewItem();
                    itemfs.Text = (nEpcCountFS + 1).ToString();
                   // itemfs.SubItems.Add(rowfs["PC"].ToString());
                    itemfs.SubItems.Add(rowfs["EPC"].ToString());
                  //  itemfs.SubItems.Add(rowfs["COUNT"].ToString());
                   // itemfs.SubItems.Add(GetAntString(rowfs));
                  //  itemfs.SubItems.Add(rowfs["RSSI"].ToString());
                    dgvBoxHeads.Items.Add(itemfs);
                    dgvBoxHeads.Items[nEpcCountFS].EnsureVisible();
                }
                DataTagTableShow(flag);
            }

        }
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

        private void SetAntHeadText(int ant)
        {
           // if (ant > nMaxAnt)
         //   {
                nMaxAnt = ant;
              //  if (nMaxAnt > 0)
               // {
                  //  cAnt.Text = "ANT1";
                 //   for (int m = 1; m < nMaxAnt; m++)
                  //  {
                   //     cAnt.Text += "/ANT" + (m + 1);
                 //   }
                  //  if (nMaxAnt <= 2)
                   //     cAnt.Width = 50 * nMaxAnt;
                  //  else
                     //   cAnt.Width = 40 * nMaxAnt;
              //  }
              ////  else
             //   {
                  //  cAnt.Text = "ANT";
                 //   cAnt.Width = 40 * 1;
              //  }
          //  }
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
                        ListViewItem itemfs = dgvBoxHeads.Items[nIndex];
                     //   itemfs.SubItems[3].Text = rowfs["COUNT"].ToString();
                     //   itemfs.SubItems[4].Text = GetAntString(rowfs);
                     //   itemfs.SubItems[5].Text = rowfs["RSSI"].ToString();
                        nIndex++;
                    }
                    nReadShowTime = DateTime.Now;
                }
            }
        }

        public static FrmRFIDBoxScan GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmRFIDBoxScan();
            }
            return frm;
        }


        private void FrmRFIDBoxScan_Resize(object sender, EventArgs e)
        {
            this.gbBoxs.Left = ((this.Width - (this.bgCartonInfo.Width + this.gbBoxs.Width + 20)) / 2 + this.bgCartonInfo.Width);
            this.bgCartonInfo.Left = this.gbBoxs.Left - this.bgCartonInfo.Width - 5;
            this.gbScanPort.Left = this.bgCartonInfo.Left;
            this.gbBoxInfo.Left = this.gbBoxs.Left;
            this.groupBox1.Left = this.gbBoxInfo.Left;


        }

        private void txtCartonNumber_KeyDown(object sender, KeyEventArgs e)
        {
            //條碼有帶回車健
            if (e.KeyCode == Keys.Enter)
            {
                responseStr = this.txtCartonNumber.Text;
                this.Invoke(new Action(
                  delegate
                  {
                      scanno = responseStr.Trim();
                      this.txtCartonNumber.Text = "";
                      scancheck(scanno);
                  }));
            }
        }

        private void txtCartonNumber_TextChanged(object sender, EventArgs e)
        {
            //USB使用
            //手動填資料顯示
            responseStr = this.txtCartonNumber.Text;
            int dd = this.txtCartonNumber.Text.Length;
            this.Invoke(new Action(
             delegate
             {
                 if (System.Text.RegularExpressions.Regex.IsMatch(responseStr, @".*\r\n$"))
                 {
                     scanno = responseStr;
                     scanno = scanno.Trim();
                     scancheck(scanno);
                 }
             }));
        }

        /// <summary>
        /// 正式计算业务逻辑
        /// </summary>
        /// <param name="responseStr"></param>
        public void scancheck(string responseStr)
        {
            string noScanQty = "0";
            if (this.txtCartonNoscanQty.Text != "")
                noScanQty = this.txtCartonNoscanQty.Text;
            if (responseStr == null || responseStr.Length < 20 || !checkedCarton(responseStr))
            {
                /* 报警器*/
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    this.am.closeportall();
                    Thread.Sleep(50);
                    this.am.openport2();
                    Thread.Sleep(300);
                    this.am.closeport2();
                    Thread.Sleep(50);
                }

                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "非外箱条码,请重新扫描外箱贴纸";
                this.cleannewCartonDB();
                this.labCartonNumber.BackColor = System.Drawing.Color.DarkRed;
                this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
                this.cleanAllText();
                this.setWrong();
                return;
            }

            if (this.txtStyle.Text == "" )
            {
                scanCarton(responseStr, null);
               // this.ScanStop();
            }

            else if (this.gbBoxInfo.Text != responseStr && Convert.ToInt32(noScanQty) != 0)
            {
                this.setWrong();
                //  报警器
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    this.am.closeportall();
                    Thread.Sleep(50);
                    this.am.openport2();
                    Thread.Sleep(50);
                }


                DialogResult dialogResult = MessageBox.Show("本箱未满箱,请确认。", "未满箱", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    this.setDefault();
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(true);
                    }
                    if (strOpenAlarm == "1")
                    {
                        this.am.closeportall();
                        Thread.Sleep(50);
                    }



                    this.labCartonNumber.Text = responseStr;
                    this.cleanAllText();
                    this.scancheck(responseStr);
                }


            }

            else if(this.gbBoxInfo.Text == responseStr )
            {
                /* 报警器*/
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    this.am.closeportall();
                    Thread.Sleep(50);
                    this.am.openport2();
                    Thread.Sleep(300);
                    this.am.closeport2();
                    Thread.Sleep(50);
                }

                this.setDefault();
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "外箱条码重複刷";
                this.labCartonNumber.BackColor = System.Drawing.Color.DarkOrange;
                this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
                return;
            }
            else
            {
                scanCarton(responseStr, null);
              //  this.ScanStop();
            }
        }

        public bool checkedCarton(string carton)
        {
            if (carton == "")
            {
                return false;
            }
            int CheckCode = -1; // 校验码
            int sum = 0; // 权位与位数相乘之后全部相加
                         // 从第3位开起算起 到 18位 第19位为校验码
            int dd = Convert.ToInt32(carton.Substring(19, 1));
            for (int i = 2; i < carton.Length - 1; i++)
            {
                if (i % 2 == 0)
                {
                    sum = sum + carton[i] * 3;
                }
                else
                {
                    sum = sum + carton[i];
                }
            }
            if (sum % 10 == 0)
            {
                CheckCode = 0;
            }
            else
            {
                CheckCode = 10 - sum % 10;
            }
            if (dd == CheckCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void setDefault()
        {
            this.BackColor = SystemColors.Control;
            this.bgCartonInfo.BackColor = SystemColors.Control;
            this.gbScanPort.BackColor = SystemColors.Control;
            this.labCartonNumber.BackColor = SystemColors.Control;
            this.labSizeQtys.BackColor = SystemColors.Control;
            this.labScanSizeQtys.BackColor = SystemColors.Control;
            this.labSizeQtys.ForeColor = SystemColors.ControlText;
            //this.txtSizeQtys.ForeColor = SystemColors.ControlText;
            this.labScanSizeQtys.ForeColor = SystemColors.ControlText;
            this.gbScanPort.ForeColor = SystemColors.ControlText;
            this.bgCartonInfo.ForeColor = SystemColors.ControlText;
            this.labColor.ForeColor = SystemColors.ControlText;
            this.labSizes.ForeColor = SystemColors.ControlText;
            this.labCartonQty.ForeColor = SystemColors.ControlText;
            this.labCartonScanQty.ForeColor = SystemColors.ControlText;
            this.labCartonNoscanQty.ForeColor = SystemColors.ControlText;
            this.labCarton.ForeColor = SystemColors.ControlText;
            this.labCartonNumber.ForeColor = SystemColors.ControlText;
            this.labPO.ForeColor = SystemColors.ControlText;
            this.labStyle.ForeColor = SystemColors.ControlText;
            this.labCustID.ForeColor = SystemColors.ControlText;
            this.labRFID.ForeColor = SystemColors.ControlText;
            this.gbBoxs.ForeColor = SystemColors.ControlText;
            this.gbBoxInfo.ForeColor = SystemColors.ControlText;

            this.dgvBoxDetails.ForeColor = SystemColors.ControlText;
            this.dgvBoxDetails.BackgroundColor = SystemColors.Control;
            this.labScanQtys.Text = "";

            this.dgvBoxDetails.BackgroundColor = SystemColors.Control;
        }
        public void setAllright()
        {
            this.BackColor = System.Drawing.Color.Green;
            this.bgCartonInfo.BackColor = System.Drawing.Color.Green;
            this.gbScanPort.BackColor = System.Drawing.Color.Green;
            this.labCartonNumber.BackColor = System.Drawing.Color.Green;
            this.labSizeQtys.BackColor = System.Drawing.Color.Green;
            this.labScanSizeQtys.BackColor = System.Drawing.Color.Green;
            this.labSizeQtys.ForeColor = System.Drawing.Color.White;
            this.labScanSizeQtys.ForeColor = System.Drawing.Color.White;
            this.gbScanPort.ForeColor = System.Drawing.Color.White;
            this.bgCartonInfo.ForeColor = System.Drawing.Color.White;
            this.labColor.ForeColor = System.Drawing.Color.White;
            this.labSizes.ForeColor = System.Drawing.Color.White;
            this.labCartonQty.ForeColor = System.Drawing.Color.White;
            this.labCartonScanQty.ForeColor = System.Drawing.Color.White;
            this.labCartonNoscanQty.ForeColor = System.Drawing.Color.White;
            this.labCarton.ForeColor = System.Drawing.Color.White;
            this.labCartonNumber.ForeColor = System.Drawing.Color.White;
            this.labPO.ForeColor = System.Drawing.Color.White;
            this.labStyle.ForeColor = System.Drawing.Color.White;
            this.labCustID.ForeColor = System.Drawing.Color.White;
            this.labScanQtys.BackColor = System.Drawing.Color.Green;
            this.labScanQtys.ForeColor = System.Drawing.Color.White;
            this.labMsg.ForeColor = System.Drawing.Color.White;
            this.labRFID.ForeColor = System.Drawing.Color.White;
            this.gbBoxs.ForeColor = Color.White;
            this.gbBoxInfo.ForeColor = Color.White;

            this.dgvBoxDetails.DefaultCellStyle.ForeColor = Color.Black;
            this.dgvBoxDetails.DefaultCellStyle.BackColor = Color.White;
            //  this.dgvBoxDetails.ForeColor= Color.White;
            // this.dgvBoxDetails.BackgroundColor = System.Drawing.Color.Green;


        }
        public void setWrong()
        {
            this.BackColor = System.Drawing.Color.DarkRed;
            this.bgCartonInfo.BackColor = System.Drawing.Color.DarkRed;
            this.gbScanPort.BackColor = System.Drawing.Color.DarkRed;
            this.labCartonNumber.BackColor = System.Drawing.Color.DarkRed;
            this.labSizeQtys.BackColor = System.Drawing.Color.DarkRed;
            this.labScanSizeQtys.BackColor = System.Drawing.Color.DarkRed;
            this.gbScanPort.ForeColor = System.Drawing.Color.Yellow;
            this.bgCartonInfo.ForeColor = System.Drawing.Color.Yellow;
            this.labColor.ForeColor = System.Drawing.Color.Yellow;
            this.labSizes.ForeColor = System.Drawing.Color.Yellow;
            this.labCartonQty.ForeColor = System.Drawing.Color.Yellow;
            this.labCartonScanQty.ForeColor = System.Drawing.Color.Yellow;
            this.labCartonNoscanQty.ForeColor = System.Drawing.Color.Yellow;
            this.labCarton.ForeColor = System.Drawing.Color.Yellow;

            this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
            this.labPO.ForeColor = System.Drawing.Color.Yellow;
            this.labStyle.ForeColor = System.Drawing.Color.Yellow;
            this.labSizeQtys.ForeColor = System.Drawing.Color.Yellow;
            this.labScanSizeQtys.ForeColor = System.Drawing.Color.Yellow;
            this.labCustID.ForeColor = System.Drawing.Color.Yellow;
            this.gbBoxs.ForeColor = System.Drawing.Color.Yellow;
            this.gbBoxInfo.ForeColor = System.Drawing.Color.Yellow;

            // this.dgvBoxDetails.ForeColor = Color.Black;
            // this.dgvBoxDetails.DefaultCellStyle = System.Drawing.Color.DarkRed;
        }

        private void cleanAllText()
        {

            this.txtPO.Text = "";
            this.txtStyle.Text = "";
            this.txtColor.Text = "";
            this.txtSize.Text = "";
            this.txtCartonQty.Text = "";
            this.txtCartonScanQty.Text = "0";
            this.txtCartonNoscanQty.Text = "";
            this.txtCartonNumber.Text = "";
            this.labCartonNumber.Text = "";
            this.txtCustID.Text = "";

            string SKUNumber = "";
            string RFIDNumber = "";
            this.setDefault(); // 默认显示界面
            rfids.Clear();
            this.txtScanSizeQtys.Text = "";
            this.labScanQtys.Text = "";
            //this.dgvBoxHeads.Clear();
            this.txtSizeQtys.Text="";

            this.dgvBoxDetails.DataSource = null;
            this.ledInventoryTags.Text = "0";
            this.ledCurrentAnt.Text = "0";
            this.ledInventoryTimes.Text = "0";
            this.ledAllTags.Text = "0";
            this.ledAllTimes.Text = "0";
            this.txtRFIDNumber.Text = "";
            this.gbBoxInfo.Text = "外箱信息";

        }

        private void scanCarton(string tscanno, int? results)
        {

            newCarton = lscm.getNewCartonPackModel();
            if (tscanno.Length < 20 || !checkedCarton(tscanno))
            {
                /* 报警器*/
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    this.am.closeportall();
                    Thread.Sleep(50);
                    this.am.openport2();
                    Thread.Sleep(300);
                    this.am.closeport2();
                    Thread.Sleep(50);
                }

                this.labMsg.ForeColor = Color.Red;
                this.cleannewCartonDB();
                this.labMsg.Text = "非外箱条码,请重新扫描外箱贴纸";
                this.labCartonNumber.BackColor = System.Drawing.Color.DarkRed;
                this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
                return;
            }
            // 截取箱号 并去掉前面的0
            scanno = tscanno.Substring(10, 9);
            scanno = scanno.TrimStart('0');


            // this.dgvBoxDetails.Rows.Clear(); //已扫描的每一箱记录
            // this.dgvBoxDetails.Rows.Clear(); //每一箱的明细（每一件记录）
            cleanAllText();
            this.setDefault();
            if (results != null && results > 0)
            {
                dpllist = lscm.GetCartonBarcode(scanno);
                string skus = "";
                for (int i = 0; i < dpllist.Count; i++)
                {
                    skus = dpllist[0].SKU.Value.ToString();
                    if (skus == "")
                    {
                        break;
                    }
                }

                if (dpllist == null || skus == "")
                {
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        Thread.Sleep(50);
                        am.openport3();
                        Thread.Sleep(50);
                    }


                    this.setWrong();
                    this.cleanAllText();
                    this.labMsg.ForeColor = Color.Red;
                    this.cleannewCartonDB();
                    this.labMsg.Text = "没有此外箱资料 1、请确认扫描出的条码号是否正确。2、请确认是否已导入PPR资料及RFID资料。";
                    MessageBox.Show("1、请确认扫描出的条码号是否正确.\r\n2、请确认是否已导入PPR资料及RFID资料", "没有此外箱资料");
                    this.labCartonNumber.BackColor = System.Drawing.Color.DarkRed;
                    this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
                    return;
                }
            }
            else
            {
                dpllist = lscm.GetCartonBarcode(scanno);
            }

            if (dpllist == null || dpllist.Count <= 0)
            {
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport3();
                    Thread.Sleep(50);
                }


                this.setWrong();
                this.cleanAllText();
                this.labMsg.ForeColor = Color.Red;
                this.cleannewCartonDB();
                this.labMsg.Text = "没有此外箱资料 1、请确认扫描出的条码号是否正确。2、请确认是否已导入PPR资料及RFID资料。";
                MessageBox.Show("1、请确认扫描出的条码号是否正确.\r\n2、请确认是否已导入PPR资料及RFID资料", "没有此外箱资料");
                this.labCartonNumber.BackColor = System.Drawing.Color.DarkRed;
                this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
                return;
            }

                string sku = "";
            for (int i = 0; i < dpllist.Count; i++)
            {
                sku = dpllist[0].SKU.Value.ToString();
                if (sku == "")
                {
                    break;
                }
            }


            if (dpllist != null && sku != "")//如果没有外箱数据，返回值为null 需要导入订单号
            {
                this.setDefault();
                this.txtSizeQtys.BackColor = Color.White;
                this.txtScanSizeQtys.BackColor = Color.White;
                this.txtSizeQtys.ForeColor = Color.Black;
                this.txtScanSizeQtys.ForeColor = Color.Black;

                if (this.BoxDetailsLog != null && this.BoxDetailsLog.Rows.Count > 0)
                {
                    this.BoxDetailsLog.Rows.Clear();
                    this.BoxDetailsLog.Columns.Clear();
                }

                this.labMsg.ForeColor= Color.Red;

                // this.dgvBoxDetails.Rows.Clear(); //已扫描的每一箱记录
                // this.dgvBoxDetails.Rows.Clear(); //!每一箱的明细（每一件记录）
                this.dgvBoxDetails.DataSource = null;

                this.BoxDetailsLog = lscm.ScanLogDB();
                this.RFIDNUMBERS = lscm.RFIDNUMBERS();
                rfids = new List<string>();
                this.labMsg.ForeColor = Color.DarkGreen;
                this.labMsg.Text = "新外箱";
                this.labCartonNumber.BackColor = System.Drawing.Color.DarkGreen;
                this.labCartonNumber.ForeColor = System.Drawing.Color.White;
                this.gbBoxInfo.Text = tscanno.ToString();

                this.labScanQtys.BackColor = System.Drawing.Color.DarkGreen;
                this.labScanQtys.ForeColor = System.Drawing.Color.White;
                this.labScanQtys.Text = "0";
                if (strOpenMedia == "1")
                {
                    am.palyMedia(true);
                }
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport2();
                    Thread.Sleep(300);
                    am.closeport2();
                    Thread.Sleep(50);
                }


                ///顯示外箱訊息
                ///

                string custid = "";
                string cartonHead = tscanno.Substring(0, 5);
                switch (cartonHead)
                {
                    case "00047":
                    case "00147":
                    case "00247":
                    case "00347":
                    case "00447":
                    case "00547":
                    case "00647":
                    case "00747":
                    case "00847":
                    case "00947":
                    case "00012":
                    case "00004":
                        custid = "NIKE";

                        break;
                    case "00006":
                        custid = "LULU";
                        break;
                    default:
                        custid = "NA";
                        break;
                }

                if (custid == "NA")
                {
                    this.labMsg.ForeColor = Color.Red;
                    this.labMsg.Text = "扫描的外箱贴纸号码不正确，谢重新扫描，谢谢";
                    //错误
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        Thread.Sleep(50);
                        am.openport3();
                        Thread.Sleep(50);
                    }

                    return;
                }



                this.txtCustID.Text = custid;
                this.txtPO.Text = dpllist[0].Po;
                this.txtStyle.Text = dpllist[0].Buyer_Item;
                this.txtColor.Text = dpllist[0].Color_code;
                string sizes = "";
                string sizeQtys = "";
                string ScanSQ = "";
                int cartonQty = 0;
                sizesQtys = new List<int>();
                ScanSQtys = new List<int>();
                foreach (LuluSingleScanPacklist list in dpllist)
                {
                    sizes = sizes + "   " + list.Size1;
                    sizeQtys = sizeQtys + "   " + list.Qty;
                    ScanSQ = ScanSQ + "   " + "0";
                    cartonQty = cartonQty + Convert.ToInt32(list.Qty);
                    sizesQtys.Add(Convert.ToInt32(list.Qty));
                    ScanSQtys.Add(0);
                }
                sizes = sizes.Substring(3, sizes.Length - 3);
                sizeQtys = sizeQtys.Substring(3, sizeQtys.Length - 3);

                this.txtSize.Text = sizes;  //SIZE 多个
                this.txtSizeQtys.Text = sizeQtys;  // 每个SIZE有多少件
                this.txtCartonQty.Text = cartonQty.ToString();  // 总共有多少件
                this.txtCartonNoscanQty.Text = this.txtCartonQty.Text;
                this.labCartonNumber.Text = scanno;
                this.txtScanSizeQtys.Text = ScanSQ;

                /*

                DataRow rdr = RFIDBoxs.NewRow();
                rdr["CustID"] = dpllist[0].Cust_id;
                rdr["CartonNumber"] = dpllist[0].Serial_From;
                rdr["Buyer_item"] = dpllist[0].Buyer_Item;
                rdr["Color_code"] = dpllist[0].Color_code;
                rdr["Qty"] = cartonQty;
                rdr["Org"] = dpllist[0].Org;
                rdr["PO"] = dpllist[0].Po;
                rdr["ScanTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                rdr["ScanHost"] = Dns.GetHostName().ToUpper();
                RFIDBoxs.Rows.Add(rdr);
                */


             //   this.dgvBoxHeads.DataSource = null;
             //   this.dgvBoxHeads.DataSource = RFIDBoxs;

                for (int i = 0; i < dpllist.Count; i++)
                {
                    DataRow dr = newCarton.NewRow();
                    dr["Cust_id"] = dpllist[i].Cust_id;
                    dr["Serial_From"] = dpllist[i].Serial_From;
                    dr["Buyer_Item"] = dpllist[i].Buyer_Item;
                    dr["color_code"] = dpllist[i].Color_code;
                    dr["Size1"] = dpllist[i].Size1;
                    dr["qty"] = dpllist[i].Qty;
                    dr["org"] = dpllist[i].Org;
                    dr["country_code"] = dpllist[i].Country_code;
                    dr["con_no"] = dpllist[i].Con_no;
                    dr["Net_Net"] = dpllist[i].Net_Net;
                    dr["con_net"] = dpllist[i].Con_net;
                    dr["con_Gross"] = dpllist[i].Con_Gross;
                    dr["con_L"] = dpllist[i].Con_L;
                    dr["con_W"] = dpllist[i].Con_W;
                    dr["con_H"] = dpllist[i].Con_H;
                    dr["b_Volume"] = dpllist[i].Bvolume;
                    dr["PO"] = dpllist[i].Po;
                    dr["MAIN_LINE"] = dpllist[i].Main_Line;
                    dr["SKU"] = dpllist[i].SKU;
                    dr["ColorName"] = dpllist[i].ColorName;
                    dr["Seanson"] = dpllist[i].Seanson;
                    newCarton.Rows.Add(dr);
                }
                this.butReScan.Enabled = false;
                this.butReScan.Text = "正在扫描...";
                string SKUNumber = "";
                string RFIDNumber = "";
                this.setDefault(); // 默认显示界面
                this.ScanStart();


            }
            else
            {
                if (strOpenAlarm == "1")
                {
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport3();
                    Thread.Sleep(50);
                }

                this.labMsg.ForeColor = Color.Red;
                this.cleannewCartonDB();
                this.labMsg.Text = "没有此外箱资料 1、请确认扫描出的条码号是否正确。2、请确认是否已导入PPR资料及RFID资料。";
                MessageBox.Show("1、请确认扫描出的条码号是否正确.\r\n2、请确认是否已导入PPR资料及RFID资料", "没有此外箱资料");

                this.setWrong();
                this.cleanAllText();
                return;
            }
        }

        public void cleannewCartonDB()
        {
            if (newCarton != null)
            {
                newCarton.Rows.Clear();
                newCarton.Columns.Clear();
            }
        }

        private void FrmRFIDBoxScan_Load(object sender, EventArgs e)
        {
            this.initConnCom();

        }

        public void initConnCom()
        {


            if (strOpenAlarm == "1")
            {
                if (am.OpenAlarm())
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openportall();
                    Thread.Sleep(500);
                    am.closeportall();
                    Thread.Sleep(50);
                    labAlarmStatus.BackColor = Color.LightGreen;
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(true);
                    }

                }
                else
                {
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    labAlarmStatus.BackColor = Color.Red;
                    MessageBox.Show("警报灯 Com通信失败，请检查线路");
                    return;
                }
            }
            this.txtCartonNumber.Focus();
            this.serialPortRFIDNumber.PortName = BoxRFIDCOM;





            openRfidReader();
            if (ComOpen)
            {
                labRFIDReaderStatus.BackColor = Color.LightGreen;
                 //   ScanStart();
            }
            else
            {
                labRFIDReaderStatus.BackColor = Color.Red;
            }
            // timer1.Enabled = !timer1.Enabled;



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
        private void InitInventoryPara()
        {
            this.dgvBoxHeads.Items.Clear();

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

        private void openRfidReader()
        {
           //  timer1.Enabled = false;
            if (AsadDuooSystemPub.IsConnectedSio)
            {
                AsadDuooSystemPub.SioBase.onReceived -= SioBase_onReceived;
                AsadDuooSystemPub.DisConnectSio();
                AsadDuooSystemPub.RcpBase.Address = 65535;
                this.ComOpen = false;
                return;
            }
            InitSio(IniSettings.Communication);
            AsadDuooSystemPub.SioBase.Connect(IniSettings.HostName, IniSettings.HostPort);
            if (AsadDuooSystemPub.IsConnectedSio)
            {
                AsadDuooSystemPub.SioBase.onReceived += SioBase_onReceived;
                this.ComOpen = true;
            }
        }


        private void CloseRfidReader()
        {
            int dd = -1;
         //   string PortName = BoxRFIDCOM;
            AsadDuooSystemPub.SioBase.onReceived -= SioBase_onReceived;
            AsadDuooSystemPub.DisConnectSio();
            AsadDuooSystemPub.RcpBase.Address = 65535;

            if (!AsadDuooSystemPub.IsConnectedSio)
            {
                dd = 0;
            }

            if (dd == 0)
            {
                MessageBox.Show("断开连接", "Information");

            }
            else if (dd == -1)
            {
                MessageBox.Show("Serial Communication Error", "Information");
            }


        }
    private void SioBase_onReceived(object sender, ADSioLib.ReceivedEventArgs e)
    {
        AsadDuooSystemPub.RcpBase.ReciveBytePkt(e.Data);
    }
        private void InitSio(int commType)
        {
            IniSettings.PortName = BoxRFIDCOM;
            IniSettings.BaudRate = 57600;
            if (AsadDuooSystemPub.IsConnectedSio)
            {
                AsadDuooSystemPub.DisConnectSio();
            }
            AsadDuooSystemPub.SioBase = new SioCom();
        }



        private void FrmRFIDBoxScan_Activated(object sender, EventArgs e)
        {
            this.txtCartonNumber.Focus();

        }

        private void txtCartonNumber_Leave(object sender, EventArgs e)
        {
            if (this.txtCartonNumber.Text == "")
                this.txtCartonNumber.Focus();
        }

        private void serialPortRFIDNumber_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] bytes = new byte[2 * 1024];//创建缓存区接收数据
            int len = serialPortRFIDNumber.Read(bytes, 0, bytes.Length);
            //串口的相应数据
            string responseStr1 = Encoding.Default.GetString(bytes, 0, len);
            this.Invoke(new Action(
              delegate
              {
                  this.txtRFIDNumber.AppendText(responseStr1);
                  if (System.Text.RegularExpressions.Regex.IsMatch(this.txtRFIDNumber.Text, @".*\r\n$"))
                  {
                      string RFIDNumber = this.txtRFIDNumber.Text;
                      RFIDNumber = RFIDNumber.Trim();
                      this.addRFIDNumberToDB(RFIDNumber);
                      this.txtRFIDNumber.Text = "";
                  }
              }));
        }
        private void addRFIDNumberToDB(string RFIDNumber)
        {
            bool isDoubleRFID = checkRFIDDouble(this.RFIDNUMBERS, RFIDNumber);
            if (!isDoubleRFID)
            {
                DataRow dr = this.RFIDNUMBERS.NewRow();
                dr["RFIDNumber"] = RFIDNumber;
                this.RFIDNUMBERS.Rows.Add(dr);

                Mytimer.Start();
                mytimeCount = 0;
                // 当扫描到第一件的时候 计时器开始计时
                // 到计时器设定时间时 开始计算
                // 每一件比较是不是这个箱子  private void RFIDCheck(string RFIDNumber)
                //计算数量 混码装要看 SIZE 数量
            }

        }
        private bool checkRFIDDouble(DataTable dt, string tag)
        {
            bool isDoubleRFID = false;


            if (dt == null || dt.Rows.Count <= 0)
            {
                isDoubleRFID = false;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == tag)
                    {
                        isDoubleRFID = true;
                        break;
                    }
                }
            }
            return isDoubleRFID;
        }

        private void RFIDCheck()
        {
            //Mytimer.Stop();
            mytimeCount = 0;
            // 停止计时 开始运算
            // 下一箱再进来的时候  再进行计算感应RFID
            if (this.dgvBoxHeads.Items.Count <= 0)
            {
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "没有感应到RFID资料，请检查设备是否已连接并设置好";
                this.setWrong();
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport2();
                    Thread.Sleep(300);
                    am.closeport2();
                    Thread.Sleep(50);
                }
                return;
            }
            DataTable RFIDTables = new DataTable();
            RFIDTables.Columns.Add("RFIDNumber");

            for (int i = 0; i < this.dgvBoxHeads.Items.Count; i++)
            {
                DataRow row = RFIDTables.NewRow();
                row["RFIDNumber"] = this.dgvBoxHeads.Items[i].SubItems[1].Text.ToString().ToUpper();
                //row["RFIDNumber"] = this.dgvBoxHeads.Items[i].ToString().ToUpper();
                RFIDTables.Rows.Add(row);
            }



            // string RFIDNumber//
            if (newCarton == null || newCarton.Rows.Count <= 0)
            {
                this.setWrong();
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "请先扫描外箱贴纸，谢谢";
                //错误
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport3();
                    Thread.Sleep(50);
                }

                return;
            }
            // this.dgvBoxDetails.DataSource = ScanLogDB;
            if (this.txtCustID.Text != "LULU" && this.txtCustID.Text != "NIKE")
            {
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "非 LULU , NIKE 品牌， 暂时不能进行整箱扫描。谢谢";
                this.setWrong();
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport2();
                    Thread.Sleep(300);
                    am.closeport2();
                    Thread.Sleep(50);
                }

                // 非 NIKE  LULU 外箱
                return;

            }



            string SKUNumber = "";
            string RFIDNumber = "";
            this.setDefault(); // 默认显示界面

            if (strOpenMedia == "1")
            {
                am.palyMedia(true);
            }
            if (strOpenAlarm == "1")
            {
                am.closeportall();
                Thread.Sleep(50);
                am.openport2();
                Thread.Sleep(50);
                // am.closeport2();
                //  Thread.Sleep(30);
            }


            int qty = 0;
            int SizeIndex = -1;
            bool skuIsTrue = false;
            bool doubleScan = true;
            int scanQtys = 0;

            for (int z = 0; z < RFIDTables.Rows.Count; z++)
            {
                RFIDNumber = RFIDTables.Rows[z]["RFIDNumber"].ToString();

                if (this.txtCustID.Text == "LULU")
                {
                    SKUNumber = this.getLuluSKUByRFID(RFIDNumber);
                }
                else if (this.txtCustID.Text == "NIKE")
                {
                    SKUNumber = this.getNikeSKUByRFID(RFIDNumber);
                }

                // 2、解析本件 RFID号码为 SKU
                // 解析RFID号码到SKU
                if (!lscm.IsHexadecimal(RFIDNumber))
                {
                    /*
                    this.labMsg.ForeColor = Color.Red;
                    this.labMsg.Text = "不是有效的RFID号码，请检查。";
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        am.openport2();
                        Thread.Sleep(300);
                        am.closeport2();
                        Thread.Sleep(30);
                    }
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                   // return;
                   // 记录起来? 或是直接过虑
                     */

                    // 记录起来 错误的成衣
                    // 本件不是这个箱子的
                    //  this.labMsg.ForeColor = Color.Red;
                    this.setWrong();
                    this.labMsg.Text = "不是有效的RFID号码";
                    rfids.Add(RFIDNumber);
                    this.labMsg.Text = RFIDNumber;//  "错误，标记并继续扫描下一件";
                    this.labScanQtys.Text = rfids.Count.ToString(); // 本次一共扫描到多少件  （感应到多少个RFID芯片）
                    this.txtRFIDNumber.Text = RFIDNumber;


                    DataRow dr = BoxDetailsLog.NewRow();
                   // dr["BoxHeadID"] = -1;
                    dr["CustID"] = this.txtCustID.Text;
                    dr["CartonNumber"] = this.labCartonNumber.Text;
                    dr["PolyBagNumber"] = SKUNumber;
                    dr["RFIDNumber"] = RFIDNumber;
                    dr["WWMTNumber"] = SKUNumber;// this.txtWwmtNumber.Text;
                    dr["Buyer_item"] = "-1";
                    dr["Color_code"] = "-1";
                    dr["Size1"] = "-1";
                    dr["Qty"] = "1";
                    dr["Org"] = newCarton.Rows[0]["org"].ToString();
                    dr["PO"] = "-1";
                    dr["ScanTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dr["ScanHost"] = Dns.GetHostName().ToUpper();
                    BoxDetailsLog.Rows.Add(dr);
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        Thread.Sleep(50);
                        am.openport3();
                        Thread.Sleep(50);
                    }
                    rfids.Add(RFIDNumber);
                    continue;

                }


                for (int i = 0; i < newCarton.Rows.Count; i++)
                {
                    qty = qty + Convert.ToInt32(newCarton.Rows[i]["Qty"].ToString());
                }

                for (int i = 0; i < newCarton.Rows.Count; i++)
                {
                    if (SKUNumber == newCarton.Rows[i]["SKU"].ToString())
                    {
                        skuIsTrue = true;

                        if (!lscm.checkRfids(rfids, RFIDNumber))
                        {
                            SizeIndex = i;
                            doubleScan = false;
                            break;
                        }
                        else
                        {
                            doubleScan = true;
                            break;
                        }
                    }
                    else
                    {
                        skuIsTrue = false;
                    }

                }


                if (skuIsTrue)
                {
                    if (!doubleScan)
                    {
                        scanQtys++;
                        rfids.Add(RFIDNumber);

                        // this.txtCartonScanQty.BackColor = SystemColors.Control;
                        // this.txtCartonScanQty.ForeColor = SystemColors.ControlText;
                        //  this.txtRFIDNumber.BackColor = SystemColors.Control;
                        //  this.txtRFIDNumber.ForeColor = SystemColors.ControlText;
                        // this.labMsg.ForeColor = Color.DarkGreen;
                        this.labMsg.Text = RFIDNumber;//  "正确，请扫描下一件";
                        this.txtCartonScanQty.Text = scanQtys.ToString();  // 这一箱里面正确的有多少件
                        this.labScanQtys.Text = rfids.Count.ToString(); // 本次一共扫描到多少件  （感应到多少个RFID芯片）
                        this.txtRFIDNumber.Text = RFIDNumber;
                        this.txtCartonNoscanQty.Text = Convert.ToString(Convert.ToInt32(this.txtCartonQty.Text) - scanQtys); // 这一箱正确的还有多少件没有扫描到
                                                                                                                             // this.labCartonNumber.BackColor = System.Drawing.Color.DarkGreen;
                                                                                                                             //  this.labCartonNumber.ForeColor = System.Drawing.Color.White;

                        string ScanSQ = "";
                        ScanSQtys[SizeIndex] = ScanSQtys[SizeIndex] + 1;
                        for (int i = 0; i < ScanSQtys.Count; i++)
                        {
                            ScanSQ = ScanSQ + "   " + ScanSQtys[i].ToString();
                        }
                        ScanSQ = ScanSQ.Substring(3, ScanSQ.Length - 3);
                        this.txtScanSizeQtys.Text = ScanSQ;

                        // 添加日志 Details
                        // 添加日志 Heads
                        DataRow dr = BoxDetailsLog.NewRow();
                       // dr["BoxHeadID"] = -1;
                        dr["CustID"] = this.txtCustID.Text;
                        dr["CartonNumber"] = this.labCartonNumber.Text;
                        dr["PolyBagNumber"] = SKUNumber;
                        dr["RFIDNumber"] = RFIDNumber;
                        dr["WWMTNumber"] = SKUNumber;// this.txtWwmtNumber.Text;
                        dr["Buyer_item"] = newCarton.Rows[0]["Buyer_Item"].ToString();
                        dr["Color_code"] = newCarton.Rows[0]["color_code"].ToString();
                        dr["Size1"] = newCarton.Rows[SizeIndex]["Size1"].ToString();
                        dr["Qty"] = "1";
                        dr["Org"] = newCarton.Rows[0]["org"].ToString();
                        dr["PO"] = newCarton.Rows[0]["PO"].ToString();
                        dr["ScanTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dr["ScanHost"] = Dns.GetHostName().ToUpper();
                        BoxDetailsLog.Rows.Add(dr);

                        /*
                         * if (strOpenAlarm == "1")
                         {
                             am.closeportall();
                             am.openport2();
                             Thread.Sleep(300);
                             am.closeport2();
                             Thread.Sleep(30);
                         }
                         if (strOpenMedia == "1")
                         {
                             am.palyMedia(true);
                         }

                         */

                        //   this.dgvBoxDetails.DataSource = ScanLogDB;
                    }
                    // else
                    //  {
                    // 重复感应到的 直接过虑
                    //  }

                }
                else
                {
                    // 记录起来 错误的成衣
                    // 本件不是这个箱子的
                    //  this.labMsg.ForeColor = Color.Red;
                    this.setWrong();
                    this.labMsg.Text = "本件成品非此箱,请检查";
                    //     MessageBox.Show("1、请确认扫描出的条码号与RFID是否正确.\r\n2、请确认是否已导入外箱资料与本件是否正确", "此件非本箱成品");
                    rfids.Add(RFIDNumber);
                    this.labMsg.Text = RFIDNumber;//  "错误，标记并继续扫描下一件";
                                                  // this.txtCartonScanQty.Text = scanQtys.ToString();  // 这一箱里面正确的有多少件
                    this.labScanQtys.Text = rfids.Count.ToString(); // 本次一共扫描到多少件  （感应到多少个RFID芯片）
                    this.txtRFIDNumber.Text = RFIDNumber;
                    // this.txtCartonNoscanQty.Text = Convert.ToString(Convert.ToInt32(this.txtCartonQty.Text) - scanQtys);// 这一箱正确的还有多少件没有扫描到
                    // this.labCartonNumber.BackColor = System.Drawing.Color.DarkGreen;
                    //  this.labCartonNumber.ForeColor = System.Drawing.Color.White;



                    DataRow dr = BoxDetailsLog.NewRow();
                    //dr["BoxHeadID"] = -1;
                    dr["CustID"] = this.txtCustID.Text;
                    dr["CartonNumber"] = this.labCartonNumber.Text;
                    dr["PolyBagNumber"] = SKUNumber;
                    dr["RFIDNumber"] = RFIDNumber;
                    dr["WWMTNumber"] = SKUNumber;// this.txtWwmtNumber.Text;
                    dr["Buyer_item"] = "-1";
                    dr["Color_code"] = "-1";
                    dr["Size1"] = "-1";
                    dr["Qty"] = "1";
                    dr["Org"] = newCarton.Rows[0]["org"].ToString();
                    dr["PO"] = "-1";
                    dr["ScanTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dr["ScanHost"] = Dns.GetHostName().ToUpper();
                    BoxDetailsLog.Rows.Add(dr);
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        Thread.Sleep(50);
                        am.openport3();
                        Thread.Sleep(50);
                    }

                }
            }

            this.dgvBoxDetails.DataSource = null;
            this.dgvBoxDetails.DataSource = BoxDetailsLog;

            // 提示扫描完成
            // 确认是否满箱

            this.saveScanBoxLogs();
            /*
            if (this.txtCartonQty.Text.ToString() == rfids.Count.ToString())
            {


                int BoxHeads = this.SaveRFIDBoxScanLogs(RFIDBoxs);  //保存箱信息
                int BoxDetails =  this.SaveBoxDetailsLog(BoxDetailsLog);  // 保存箱子每一件信息
                if (BoxHeads <= 0)
                {
                    this.setWrong();
                    this.labMsg.ForeColor = Color.Red;
                    this.labMsg.Text = "保存外箱记录失败，请重试保存";
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        am.openport2();
                        Thread.Sleep(300);
                        am.closeport2();
                        Thread.Sleep(30);
                    }
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    this.butSaveLogs.Visible = true;

                }
                if (BoxDetails <= 0)
                {
                    this.setWrong();
                    this.labMsg.ForeColor = Color.Red;
                    this.labMsg.Text = "保存RFID明细记录失败，请重试保存";

                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        am.openport2();
                        Thread.Sleep(300);
                        am.closeport2();
                        Thread.Sleep(30);
                    }
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    this.butSaveLogs.Visible = true;
                }

                if(BoxHeads > 0 && BoxDetails > 0)
                {
                    this.butSaveLogs.Visible = false;
                    this.setAllright();
                    this.BoxDetailsLog.Rows.Clear();
                    this.BoxDetailsLog.Columns.Clear();
                    this.labMsg.Text = "满箱，请扫描下一箱";
                    this.labScanQtys.Text = "OK";

                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        am.openport2();
                        Thread.Sleep(300);
                        am.closeport2();
                        Thread.Sleep(30);
                    }
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(true);
                    }

                    // 保存资料
                    this.RFIDNUMBERS.Clear(); //完事了清空，以便下一箱进行扫描
                }
            }
            else
            {

                this.setWrong();
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    am.openport1();
                    Thread.Sleep(300);
                    am.closeport1();
                    Thread.Sleep(30);
                }
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
            }

            string Buyer_item = "";
            for (int i = 0; i < this.dgvBoxDetails.Rows.Count; i++)
            {
                Buyer_item = this.dgvBoxDetails.Rows[i].Cells["Buyer_item"].ToString();
                if (Buyer_item == "-1")
                {
                    this.dgvBoxDetails.Rows[i].DefaultCellStyle.ForeColor = Color.Yellow;
                    this.dgvBoxDetails.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            */


        }

        public string getLuluSKUByRFID(string RFIDNumber)
        {
            string SKUNumber = "";
            if (RFIDNumber.Length != 24)
            {
                return SKUNumber;
            }
            string[] RFIDCode = new string[4];


            string RHEX = lscm.HexString2BinString(RFIDNumber); //16转二进制
            RHEX = RHEX.Replace(" ", "");

            RFIDCode[0] = RHEX.Substring(30, 8);  // 第一组二进制
            RFIDCode[1] = RHEX.Substring(38, 8);
            RFIDCode[2] = RHEX.Substring(46, 8);
            RFIDCode[3] = RHEX.Substring(54, 8);

            string BinString = (RFIDCode[0] + RFIDCode[1] + RFIDCode[2] + RFIDCode[3]);
            // 2 转 10
            SKUNumber = Convert.ToInt32(BinString, 2).ToString();
            return SKUNumber;
        }

        public string getNikeSKUByRFID(string RFIDNumber)
        {
            string SKUNumber = "";
            if (RFIDNumber.Length != 24)
            {
                return SKUNumber;
            }
            string[] RFIDCode = new string[6];


            string RHEX = lscm.HexString2BinString(RFIDNumber); //16转二进制
            RHEX = RHEX.Replace(" ", "");

            RFIDCode[0] = RHEX.Substring(14, 8);  // 第一组二进制
            RFIDCode[1] = RHEX.Substring(22, 8);
            RFIDCode[2] = RHEX.Substring(30, 8);
            RFIDCode[3] = RHEX.Substring(38, 8);
            RFIDCode[4] = RHEX.Substring(46, 8);
            RFIDCode[5] = RHEX.Substring(54, 4);

            string BinString = (RFIDCode[0] + RFIDCode[1] + RFIDCode[2]);            // 2 转 10
            string ItemReference = Convert.ToInt32(BinString, 2).ToString();


            string SerialString = (RFIDCode[3] + RFIDCode[4] + RFIDCode[5]);
            string Serial = Convert.ToInt32(SerialString, 2).ToString();
            SKUNumber = ItemReference + Serial;
            return SKUNumber;
        }



        public int SaveBoxDetailsLog(DataTable BoxDetailsLogDB,int MaxRow)
        {

            if (BoxDetailsLogDB.Rows.Count <= 0)
            {
                return 0;
            }
            int BoxDetails = this.bsm.SaveBoxDetailsLog(BoxDetailsLogDB, MaxRow);
            return BoxDetails;

        }
        public int SaveRFIDBoxScanLogs(DataTable dt)
        {
            if(dt.Rows.Count <= 0)
            {
                return 0;
            }
            int insertBoxScan = this.bsm.SaveRFIDBoxScanLogs(dt);
            return insertBoxScan;
        }


        public void saveScanBoxLogs()
        {
            string[] ScanSizeQtys = txtScanSizeQtys.Text.Split(' ');
            string[] SizeQtys = txtSizeQtys.Text.Split(' ');
            if (this.txtCartonQty.Text.ToString() == rfids.Count.ToString()  && Enumerable.SequenceEqual(ScanSizeQtys, SizeQtys))
            {
                int BoxHeads = this.SaveRFIDBoxScanLogs(BoxDetailsLog);  //保存箱信息
                int MaxRow = this.getRFIDBoxScanLogsMaxID();  //保存箱信息

                if (BoxHeads <= 0 || MaxRow <=0)
                {
                    this.setWrong();
                    this.labMsg.ForeColor = Color.Red;
                    this.labMsg.Text = "保存外箱记录失败，请重试保存";
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        Thread.Sleep(50);
                        am.openport2();
                        Thread.Sleep(300);
                        am.closeport2();
                        Thread.Sleep(50);
                    }

                    this.butSaveLogs.Visible = true;

                }


                int BoxDetails = this.SaveBoxDetailsLog(BoxDetailsLog, MaxRow);  // 保存箱子每一件信息
                if (BoxDetails <= 0)
                {
                    this.setWrong();
                    this.labMsg.ForeColor = Color.Red;
                    this.labMsg.Text = "保存RFID明细记录失败，请重试保存";
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(false);
                    }
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        Thread.Sleep(50);
                        am.openport2();
                        Thread.Sleep(300);
                        am.closeport2();
                        Thread.Sleep(50);
                    }

                    this.butSaveLogs.Visible = true;
                }

                if (BoxHeads > 0 && BoxDetails > 0)
                {



                    this.butSaveLogs.Visible = false;
                    this.setAllright();

                    this.labMsg.Text = "满箱，请扫描下一箱";
                    this.labScanQtys.Text = "OK";
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(true);
                    }
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        Thread.Sleep(50);
                        am.openport2();
                        Thread.Sleep(300);
                        am.closeport2();
                        Thread.Sleep(50);
                    }


                    // 保存资料
                    this.RFIDNUMBERS.Clear(); //完事了清空，以便下一箱进行扫描
                }
            }else if(!Enumerable.SequenceEqual(ScanSizeQtys, SizeQtys))
            {
                string[] Sizes = txtSize.Text.Split(' ');
                for (int i = 0; i < Sizes.Length; i++)
                {
                   if( ScanSizeQtys[i] != SizeQtys[i])
                    {
                        for (int j = 0; j < this.dgvBoxDetails.Rows.Count; j++)
                        {
                            if (Sizes[i] == this.dgvBoxDetails.Rows[j].Cells["Size1"].Value.ToString())
                            {
                                this.dgvBoxDetails.Rows[j].DefaultCellStyle.ForeColor = Color.Yellow;
                                this.dgvBoxDetails.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                            }
                        }
                    }
                }

                this.txtSizeQtys.BackColor = System.Drawing.Color.Red;
                this.txtScanSizeQtys.BackColor = System.Drawing.Color.Red;
                this.txtSizeQtys.ForeColor = System.Drawing.Color.Yellow;
                this.txtScanSizeQtys.ForeColor = System.Drawing.Color.Yellow;
                this.labMsg.ForeColor = System.Drawing.Color.Yellow;
                this.labRFID.ForeColor = System.Drawing.Color.Yellow;

                this.setWrong();
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport3();
                    Thread.Sleep(50);

                }

            }
            else
            {
                this.txtSizeQtys.BackColor = System.Drawing.Color.Red;
                this.txtScanSizeQtys.BackColor = System.Drawing.Color.Red;
                this.txtSizeQtys.ForeColor = System.Drawing.Color.Yellow;
                this.txtScanSizeQtys.ForeColor = System.Drawing.Color.Yellow;
                this.labMsg.ForeColor = System.Drawing.Color.Yellow;
                this.labRFID.ForeColor = System.Drawing.Color.Yellow;
                this.setWrong();
                if (strOpenMedia == "1")
                {
                    am.palyMedia(false);
                }
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport3();
                    Thread.Sleep(50);

                }

            }

            string Buyer_item = "";
            for (int i = 0; i < this.dgvBoxDetails.Rows.Count; i++)
            {
                Buyer_item = this.dgvBoxDetails.Rows[i].Cells["Buyer_item"].Value.ToString();
                if (Buyer_item == "-1")
                {
                    this.dgvBoxDetails.Rows[i].DefaultCellStyle.ForeColor = Color.Yellow;
                    this.dgvBoxDetails.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }

    }
        private void butSaveLogs_Click(object sender, EventArgs e)
        {
            this.saveScanBoxLogs();
        }

        private void txtRFIDNumber_KeyDown(object sender, KeyEventArgs e)
        {
            //條碼有帶回車健
            if (e.KeyCode == Keys.Enter)
            {
                responseStr = this.txtRFIDNumber.Text;
                this.Invoke(new Action(
                  delegate
                  {
                      this.addRFIDNumberToDB(responseStr);
                      this.txtRFIDNumber.Text = "";
                  }));
            }
        }

        private void dgvBoxDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvBoxHeads_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }
        public int getRFIDBoxScanLogsMaxID( )
        {

            int MaxRow = this.bsm.getRFIDBoxScanLogsMaxID();
            return MaxRow;
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
        public void closingRfidComm()
        {
            if (AsadDuooSystemPub.IsConnectedSio)
            {
                AsadDuooSystemPub.SioBase.onReceived -= SioBase_onReceived;
                AsadDuooSystemPub.DisConnectSio();
                AsadDuooSystemPub.RcpBase.Address = 65535;
                return;
            }
        }

        private void FrmRFIDBoxScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.closingRfidComm();


        }

        private void butReScan_Click(object sender, EventArgs e)
        {

            this.cleanAllText();
            /*

            if ( mIsStatus != 0)
            {
                ScanStop();
                this.butReScan.Text = "开始扫描";
                labStatusRuning.BackColor = Color.Red;
                labStatusRuning.Text = "STOP";

            }
            else if (mIsStatus == 0)
            {

                this.ScanStart();
                this.butReScan.Enabled = false;
                this.butReScan.Text = "正在扫描...";
                if ( AsadDuooSystemPub.IsConnectedSio && ComOpen && timer2.Enabled)
                {
                    labStatusRuning.BackColor = Color.Green;
                    labStatusRuning.Text = "RUNING...";
                }
            }
            */
        }
    }
}
