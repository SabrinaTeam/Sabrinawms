using BLL;
using COMMON;
using MODEL;
using ReaderB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmLuluSingleScan : Form
    {

        private static FrmLuluSingleScan frm;
        private string responseStr = ""; //扫描回来的外箱条码号
        private string scanno = "";
        public int mtotal = 0;
        public static DataTable newCarton;

        public AlarmManager am = new AlarmManager();
        public LuluSingleScanManager lscm = new LuluSingleScanManager();
        private static readonly string strOpenAlarm = ConfigurationManager.ConnectionStrings["OpenAlarm"].ConnectionString; //报警器灯光
        private static readonly string strOpenMedia = ConfigurationManager.ConnectionStrings["OpenMedia"].ConnectionString;//报警器声音
        private static readonly string PolybagCOM = ConfigurationManager.ConnectionStrings["PolybagCOM"].ConnectionString; //单件条码扫描COM口
        private static readonly string SingleRFIDCOM = ConfigurationManager.ConnectionStrings["SingleRFIDCOM"].ConnectionString;//单件RFID扫描COM口

        private List<LuluSingleScanPacklist> dpllist = new List<LuluSingleScanPacklist>();
        List<string> rfids = new List<string>(); // RFID 单件集合，计算扫了多少件
        DataTable ScanLogDB;
        List<int> sizesQtys;
        List<int> ScanSQtys;



        // public receiManager rm = new receiManager();
        //  DataTable newReceiDT = new DataTable();
        public int hiedcolumnindex = -1; //是否选中外面
                                         // public int delID = 0; //要删除的ID行号
                                         // public int rowIndex = -1; //表的行索引
        public FrmLuluSingleScan()
        {
            InitializeComponent();
            if (newCarton != null)
            {
                newCarton.Rows.Clear();
                newCarton.Columns.Clear();
            }

            this.dgvScanLogs.DoubleBufferedDataGirdView(true);
        }


        private void FrmLuluSingleScan_Resize(object sender, EventArgs e)
        {
            this.bgScaninfo.Left = ((this.Width - this.bgScaninfo.Width - 10) / 2);
            this.bgCartonInfo.Left = this.bgScaninfo.Left - this.bgCartonInfo.Width - 5;
            this.bgScanNumbers.Left = this.bgCartonInfo.Left;
            this.bgCartonLogs.Left = this.bgScaninfo.Left + this.bgScaninfo.Width + 5;



        }
        public static FrmLuluSingleScan GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmLuluSingleScan();
            }
            return frm;
        }

        /// <summary>
        /// USB输入带回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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

        /// <summary>
        /// 手动输入时
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
                    this.am.openport3();
                    Thread.Sleep(500);

                }

                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "非外箱条码,请重新扫描外箱贴纸";
                // this.cleannewCartonDB();
                this.labCartonNumber.BackColor = System.Drawing.Color.DarkRed;
                this.labCartonN.BackColor = System.Drawing.Color.DarkRed;
                this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
                this.labCartonN.ForeColor = System.Drawing.Color.Yellow;
                //this.cleanAllText();
                this.setWrong();
                return;
            }

            if (this.txtStyle.Text == "" || this.labQtys.Text =="OK")

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
                    this.am.openport1();
                    Thread.Sleep(50);
                }
                this.labCartonNumber.Text = responseStr;
                this.cleanAllText();
                scanCarton(responseStr, null);
            }
            else if (this.labCartonNumber.Text != responseStr && Convert.ToInt32(noScanQty) > 0)
            {
                this.setWrong();
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
                    this.am.openport3();
                    Thread.Sleep(50);
                }


                DialogResult dialogResult = MessageBox.Show("本箱还未满箱,请确认是否继续向下流动。", "未满箱", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        this.am.openport1();
                        Thread.Sleep(50);
                    }
                    this.labCartonNumber.Text = responseStr;
                    this.cleanAllText();
                    this.scancheck(responseStr);
                }
            }
            else
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
                this.labCartonN.BackColor = System.Drawing.Color.DarkOrange;
                this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
                this.labCartonN.ForeColor = System.Drawing.Color.Yellow;
                // msgDiv.MsgDivShow(" 外箱条码重複刷", 3);
                return;
            }
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
                    this.am.openport3();
                    Thread.Sleep(50);

                }

                this.labMsg.ForeColor = Color.Red;
                this.cleannewCartonDB();
                this.labMsg.Text = "非外箱条码,请重新扫描外箱贴纸";
                this.labCartonNumber.BackColor = System.Drawing.Color.DarkRed;
                this.labCartonN.BackColor = System.Drawing.Color.DarkRed;
                this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
                this.labCartonN.ForeColor = System.Drawing.Color.Yellow;
                return;
            }
            // 截取箱号 并去掉前面的0
            scanno = tscanno.Substring(10, 9);
            scanno = scanno.TrimStart('0');
            // scanno = Convert.ToString(Convert.ToInt32(scanno));

            this.dgvScanLogs.Rows.Clear(); //本箱已扫描的LOG每一件记录
            cleanAllText();
            this.setDefault();
            if (results != null && results > 0)
            {
                dpllist = lscm.GetCartonBarcode(scanno);
                if (dpllist == null)
                {
                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(true);
                    }
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        Thread.Sleep(50);
                        am.openport1();
                        Thread.Sleep(300);
                        am.closeport1();
                        Thread.Sleep(50);
                    }


                    this.setWrong();
                    this.cleanAllText();
                    this.labMsg.ForeColor = Color.Red;
                    this.cleannewCartonDB();
                    this.labMsg.Text = "没有此外箱资料 1、请确认扫描出的条码号是否正确。2、请确认是否已导入PPR资料及RFID资料。";
                    MessageBox.Show("1、请确认扫描出的条码号是否正确.\r\n2、请确认是否已导入PPR资料及RFID资料", "没有此外箱资料");
                    this.labCartonNumber.BackColor = System.Drawing.Color.DarkRed;
                    this.labCartonN.BackColor = System.Drawing.Color.DarkRed;
                    this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
                    this.labCartonN.ForeColor = System.Drawing.Color.Yellow;

                    return;
                }
            }
            else
            {
                dpllist = lscm.GetCartonBarcode(scanno);
            }

            if (dpllist != null)//如果没有外箱数据，返回值为null 需要导入订单号
            {
                this.setDefault();
                this.dgvScanLogs.DataSource = null;

                this.ScanLogDB = lscm.ScanLogDB();
                rfids = new List<string>();

                this.labMsg.ForeColor = Color.DarkGreen;
                this.labMsg.Text = "新外箱";

                this.labCartonNumber.BackColor = System.Drawing.Color.DarkGreen;
                this.labCartonN.BackColor = System.Drawing.Color.DarkGreen;
                this.labCartonNumber.ForeColor = System.Drawing.Color.White;
                this.labCartonN.ForeColor = System.Drawing.Color.White;
                if (strOpenMedia == "1")
                {
                    am.palyMedia(true);
                }
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport1();
                    Thread.Sleep(300);
                    am.closeport1();
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
                    this.txtPolybagNumber.BackColor = SystemColors.Control;
                    this.txtPolybagNumber.ForeColor = SystemColors.ControlText;
                    this.txtPolybagNumber.Text = "";
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
                this.labCartonN.Text = dpllist[0].Con_no;
                this.txtCartonNoscanQty.Text = this.txtCartonQty.Text;
                this.labCartonNumber.Text = tscanno;
                this.txtScanSizeQtys.Text = ScanSQ;

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




                /*

                //判断是否有上次错误未处理
                //如果有  显示出错误
                for (int i = 0; i < dpllist.Count; i++)
                {
                    if (dpllist[i].SizeNo != null)
                    {
                        if (strOpenAlarm == "1")
                        {
                            closeportall();
                            openport2();
                        }

                        txtError.PasswordChar = '#';
                        txtPwd.Enabled = false;
                        labinfo.Text = "请解锁后再刷，注意是否已替换掉错误内盒";
                        laberrorbarcode.Text = dpllist[i].SizeNo;
                        btntempsavebox.Visible = false;
                        gbError.BackColor = System.Drawing.Color.LightBlue;

                        changColor(System.Drawing.Color.LightBlue);  //锁定

                        gbError.Visible = true;
                        txtError.Focus();

                        break;
                    }
                }
                */
            }
            else
            {
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

                this.labMsg.ForeColor = Color.Red;
                this.cleannewCartonDB();
                this.labMsg.Text = "没有此外箱资料 1、请确认扫描出的条码号是否正确。2、请确认是否已导入PPR资料及RFID资料。";
                MessageBox.Show("1、请确认扫描出的条码号是否正确.\r\n2、请确认是否已导入PPR资料及RFID资料", "没有此外箱资料");

                this.setWrong();
                this.cleanAllText();
                return;
            }
        }



        private void PolybagCheck(string PolybagNumber)
        {
            /*
            DataGridViewRow row = (DataGridViewRow)this.dgvScanLogs.Rows[0].Clone();
            row.Cells[0].Value = PolybagNumber;
            this.dgvScanLogs.Rows.Add(row);
            */

            if (PolybagNumber == "") { return; }
            PolybagNumber = PolybagNumber.TrimStart('0');
            // PolybagNumber = Convert.ToString(Convert.ToInt32(PolybagNumber));
            // 1、本件内包装袋是不是与本箱SKU相等
            if (newCarton != null && newCarton.Rows.Count > 0)
            {
                bool isTrue = false;
                int SizeIndex = -1;
                for (int i = 0; i < newCarton.Rows.Count; i++)
                {
                    if (PolybagNumber == newCarton.Rows[i]["SKU"].ToString())
                    {
                        isTrue = true;
                        SizeIndex = i;
                        break;
                    }
                }
                if (isTrue)
                {
                    this.setDefault();
                    this.labMsg.ForeColor = Color.DarkGreen;
                    this.labMsg.Text = "包装内袋正确，请扫描 RFID";
                    this.labRFIDNumber.Text = "";
                    this.txtPolybagNumber.BackColor = SystemColors.Control;
                    this.txtPolybagNumber.ForeColor = SystemColors.ControlText;
                    this.txtPolybagNumber.Text = PolybagNumber;
                    this.labPolybagNumber.BackColor = System.Drawing.Color.DarkGreen;
                    this.labPolybagNumber.ForeColor = System.Drawing.Color.White;
                    this.labPolybagNumber.Text = PolybagNumber;
                    this.labCartonNumber.BackColor = System.Drawing.Color.DarkGreen;
                    this.labCartonNumber.ForeColor = System.Drawing.Color.White;
                    this.labCartonN.BackColor = System.Drawing.Color.DarkGreen;
                    this.labCartonN.ForeColor = System.Drawing.Color.White;
                    this.labPolySize.BackColor = System.Drawing.Color.DarkGreen;
                    this.labSizeQty.BackColor = System.Drawing.Color.DarkGreen;
                    this.labPolySize.ForeColor = System.Drawing.Color.White;
                    this.labSizeQty.ForeColor = System.Drawing.Color.White;

                    this.labPolySize.Text = newCarton.Rows[SizeIndex]["Size1"].ToString();
                    this.labSizeQty.Text = newCarton.Rows[SizeIndex]["Qty"].ToString();

                    if (strOpenMedia == "1")
                    {
                        am.palyMedia(true);
                    }

                    // 正确
                    if (strOpenAlarm == "1")
                    {
                        am.closeportall();
                        Thread.Sleep(50);
                        am.openport1();
                        Thread.Sleep(300);
                        am.closeport1();
                        Thread.Sleep(50);

                    }


                }
                else
                {
                    this.labMsg.ForeColor = Color.Red;
                    this.labMsg.Text = "包装内袋错语，请检查";
                    this.txtPolybagNumber.BackColor = SystemColors.Control;
                    this.txtPolybagNumber.ForeColor = SystemColors.ControlText;
                    this.txtPolybagNumber.Text = "";

                    this.labPolybagNumber.BackColor = System.Drawing.Color.DarkRed;
                    this.labPolybagNumber.ForeColor = System.Drawing.Color.Yellow;
                    this.labPolybagNumber.Text = "";
                    this.labPolySize.Text = "";
                    this.labSizeQty.Text = "";

                    this.labPolySize.BackColor = System.Drawing.Color.DarkRed;
                    this.labSizeQty.BackColor = System.Drawing.Color.DarkRed;
                    this.labPolySize.ForeColor = System.Drawing.Color.Yellow;
                    this.labSizeQty.ForeColor = System.Drawing.Color.Yellow;


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
            }
            else
            {
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "请先扫描外箱贴纸，谢谢";
                this.txtPolybagNumber.BackColor = SystemColors.Control;
                this.txtPolybagNumber.ForeColor = SystemColors.ControlText;
                this.txtPolybagNumber.Text = "";
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



        }
        private void RFIDCheck(string RFIDNumber)
        {
            if (newCarton == null || newCarton.Rows.Count <= 0)
            {
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "请先扫描外箱贴纸，谢谢";

                this.txtPolybagNumber.BackColor = SystemColors.Control;
                this.txtPolybagNumber.ForeColor = SystemColors.ControlText;
                this.txtPolybagNumber.Text = "";
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
            if (this.labPolybagNumber.Text == "")
            {
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "请先扫描 Polybag 条码，谢谢";
                this.labPolybagNumber.BackColor = Color.DarkRed;
                this.labPolybagNumber.ForeColor = Color.Yellow;
                this.labPolybagNumber.Text = "";
                this.labPolySize.Text = "";
                this.labSizeQty.Text = "";

                this.labPolySize.BackColor = System.Drawing.Color.DarkRed;
                this.labSizeQty.BackColor = System.Drawing.Color.DarkRed;
                this.labPolySize.ForeColor = System.Drawing.Color.Yellow;
                this.labSizeQty.ForeColor = System.Drawing.Color.Yellow;
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
            // 2、解析本件 RFID号码为 SKU
            string SKUNumber = "";
            // 解析RFID号码到SKU
            if (!lscm.IsHexadecimal(RFIDNumber))
            {
                // 本件已扫描过
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "不是有效的RFID号码，请检查。";
                this.labRFIDNumber.BackColor = System.Drawing.Color.DarkRed;
                this.labRFIDNumber.ForeColor = System.Drawing.Color.Yellow;
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

            if (this.txtCustID.Text == "LULU")
            {
                SKUNumber = this.getLuluSKUByRFID(RFIDNumber);
            }
            else if (this.txtCustID.Text == "NIKE")
            {
                SKUNumber = this.getNikeSKUByRFID(RFIDNumber);
            }
            else
            {
                // 包装袋与吊卡不一样
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "非 LULU , NIKE 品牌， 暂时不能进行单件扫描。谢谢";
                this.labRFIDNumber.BackColor = System.Drawing.Color.DarkRed;
                this.labRFIDNumber.ForeColor = System.Drawing.Color.Yellow;
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

            int qty = 0;
            int SizeIndex = -1;
            bool skuIsTrue = false;
            bool doubleScan = true;
            bool bagSureWWMT = false;
            for (int i = 0; i < newCarton.Rows.Count; i++)
            {
                qty = qty + Convert.ToInt32(newCarton.Rows[i]["Qty"].ToString());
            }

            for (int i = 0; i < newCarton.Rows.Count; i++)
            {
                if (SKUNumber == newCarton.Rows[i]["SKU"].ToString())
                {
                    skuIsTrue = true;
                    if (this.labPolybagNumber.Text != "" && SKUNumber == this.labPolybagNumber.Text)
                    {
                        bagSureWWMT = true;
                    }
                    else
                    {
                        bagSureWWMT = false;
                    }
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
                if (!bagSureWWMT)
                {
                    // 包装袋与吊卡不一样
                    this.labMsg.ForeColor = Color.Red;
                    this.labMsg.Text = "包装袋与吊卡一不至";
                    this.labRFIDNumber.BackColor = System.Drawing.Color.DarkRed;
                    this.labRFIDNumber.ForeColor = System.Drawing.Color.Yellow;
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
                if (!doubleScan)
                {
                    if ((ScanSQtys[SizeIndex] + 1) > sizesQtys[SizeIndex])
                    {
                        // 包装袋与吊卡不一样
                        this.labMsg.ForeColor = Color.Red;
                        this.labMsg.Text = "本箱此 SIZE 【" + newCarton.Rows[SizeIndex]["Size1"].ToString() + "】 已满，请检查！";
                        this.labRFIDNumber.BackColor = System.Drawing.Color.DarkRed;
                        this.labRFIDNumber.ForeColor = System.Drawing.Color.Yellow;
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




                    rfids.Add(RFIDNumber);
                    this.setDefault();
                    this.txtCartonScanQty.BackColor = SystemColors.Control;
                    this.txtCartonScanQty.ForeColor = SystemColors.ControlText;
                    this.txtRFIDNumber.BackColor = SystemColors.Control;
                    this.txtRFIDNumber.ForeColor = SystemColors.ControlText;
                    this.txtWwmtNumber.BackColor = SystemColors.Control;
                    this.txtWwmtNumber.ForeColor = SystemColors.ControlText;




                    this.labMsg.ForeColor = Color.DarkGreen;
                    this.labMsg.Text = "正确，请扫描下一件";
                    this.labPolybagNumber.Text = "";
                    this.labPolySize.Text = "";
                    this.labSizeQty.Text = "";
                    this.txtCartonScanQty.Text = rfids.Count.ToString();
                    this.labQtys.Text = rfids.Count.ToString();
                    this.txtRFIDNumber.Text = RFIDNumber;
                    this.txtWwmtNumber.Text = SKUNumber;
                    this.txtCartonNoscanQty.Text = Convert.ToString(Convert.ToInt32(this.txtCartonQty.Text) - rfids.Count);

                    this.labRFIDNumber.BackColor = System.Drawing.Color.DarkGreen;
                    this.labRFIDNumber.ForeColor = System.Drawing.Color.White;
                    this.labRFIDNumber.Text = RFIDNumber;

                    this.labCartonNumber.BackColor = System.Drawing.Color.DarkGreen;
                    this.labCartonNumber.ForeColor = System.Drawing.Color.White;
                    this.labCartonN.BackColor = System.Drawing.Color.DarkGreen;
                    this.labCartonN.ForeColor = System.Drawing.Color.White;
                    this.labPolybagNumber.BackColor = System.Drawing.Color.DarkGreen;
                    this.labPolybagNumber.ForeColor = System.Drawing.Color.White;

                    this.labPolySize.BackColor = System.Drawing.Color.DarkGreen;
                    this.labSizeQty.BackColor = System.Drawing.Color.DarkGreen;
                    this.labPolySize.ForeColor = System.Drawing.Color.White;
                    this.labSizeQty.ForeColor = System.Drawing.Color.White;


                    string ScanSQ = "";
                    ScanSQtys[SizeIndex] = ScanSQtys[SizeIndex] + 1;
                    for (int i = 0; i < ScanSQtys.Count; i++)
                    {
                        ScanSQ = ScanSQ + "   " + ScanSQtys[i].ToString();
                    }
                    ScanSQ = ScanSQ.Substring(3, ScanSQ.Length - 3);
                    this.txtScanSizeQtys.Text = ScanSQ;


                    // 添加日志
                    DataRow dr = ScanLogDB.NewRow();
                    dr["CustID"] = this.txtCustID.Text;
                    dr["CartonNumber"] = this.labCartonNumber.Text;
                    dr["PolyBagNumber"] = SKUNumber;
                    dr["RFIDNumber"] = RFIDNumber;
                    dr["WWMTNumber"] = this.txtWwmtNumber.Text;
                    dr["Buyer_item"] = newCarton.Rows[0]["Buyer_Item"].ToString();
                    dr["Color_code"] = newCarton.Rows[0]["color_code"].ToString();
                    dr["Size1"] = newCarton.Rows[SizeIndex]["Size1"].ToString();
                    dr["Qty"] = "1";// newCarton.Rows[SizeIndex]["qty"].ToString();
                    dr["Org"] = newCarton.Rows[0]["org"].ToString();
                    dr["PO"] = newCarton.Rows[0]["PO"].ToString();
                    dr["ScanTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dr["ScanHost"] = Dns.GetHostName().ToUpper();
                    ScanLogDB.Rows.Add(dr);
                    this.dgvScanLogs.DataSource = ScanLogDB;


                    // 确认是否满箱
                    if (this.txtCartonQty.Text.ToString() == rfids.Count.ToString())
                    {
                        this.SaveScanLogs();
                    }
                    else
                    {
                        if (strOpenMedia == "1")
                        {
                            am.palyMedia(true);
                        }
                        if (strOpenAlarm == "1")
                        {
                            am.closeportall();
                            Thread.Sleep(50);
                            am.openport1();
                            Thread.Sleep(300);
                            am.closeport1();
                            Thread.Sleep(50);
                        }

                    }
                }
                else
                {
                    // 本件已扫描过
                    this.labMsg.ForeColor = Color.Red;
                    this.labMsg.Text = "本件已扫描过，请扫描下一件";
                    this.labRFIDNumber.BackColor = System.Drawing.Color.DarkRed;
                    this.labRFIDNumber.ForeColor = System.Drawing.Color.Yellow;
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

                }

            }
            else
            {
                this.labRFIDNumber.BackColor = System.Drawing.Color.DarkRed;
                this.labRFIDNumber.ForeColor = System.Drawing.Color.Yellow;
                // 本件不是这个箱子的
                this.labMsg.ForeColor = Color.Red;
                this.setWrong();
                this.labMsg.Text = "本件成品非此箱,请检查";
                //  MessageBox.Show("1、请确认扫描出的条码号与RFID是否正确.\r\n2、请确认是否已导入外箱资料与本件是否正确", "此件非本箱成品");
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



        public void setDefault()
        {
            this.BackColor = SystemColors.Control;
            this.bgCartonInfo.BackColor = SystemColors.Control;
            this.bgScanNumbers.BackColor = SystemColors.Control;
            this.bgScaninfo.BackColor = SystemColors.Control;
            this.bgCartonLogs.BackColor = SystemColors.Control;
            this.labCartonNumber.BackColor = SystemColors.Control;
            this.labQtys.BackColor = SystemColors.Control;
            this.labCartonN.BackColor = SystemColors.Control;
            //this.dgvScanLogs.BackColor = SystemColors.Control;
            this.labPolybagNumber.BackColor = SystemColors.Control;
            this.labRFIDNumber.BackColor = SystemColors.Control;

            this.labPolySize.BackColor = SystemColors.Control;
            this.labSizeQty.BackColor = SystemColors.Control;
            this.labSizeQtys.BackColor = SystemColors.Control;
            //this.txtSizeQtys.BackColor = SystemColors.Control;
            this.labScanSizeQtys.BackColor = SystemColors.Control;
            // this.txtScanSizeQtys.BackColor = SystemColors.Control;

            this.labPolySize.ForeColor = SystemColors.ControlText;
            this.labSizeQty.ForeColor = SystemColors.ControlText;
            this.labSizeQtys.ForeColor = SystemColors.ControlText;
            //this.txtSizeQtys.ForeColor = SystemColors.ControlText;
            this.labScanSizeQtys.ForeColor = SystemColors.ControlText;
            //this.txtScanSizeQtys.ForeColor = SystemColors.ControlText;

            this.bgScaninfo.ForeColor = SystemColors.ControlText;
            this.bgScanNumbers.ForeColor = SystemColors.ControlText;
            this.bgCartonInfo.ForeColor = SystemColors.ControlText;
            this.bgCartonLogs.ForeColor = SystemColors.ControlText;
            this.labColor.ForeColor = SystemColors.ControlText;
            this.labSizes.ForeColor = SystemColors.ControlText;
            this.labCartonQty.ForeColor = SystemColors.ControlText;
            this.labCartonScanQty.ForeColor = SystemColors.ControlText;
            this.labCartonNoscanQty.ForeColor = SystemColors.ControlText;
            this.labCarton.ForeColor = SystemColors.ControlText;
            this.labPolybag.ForeColor = SystemColors.ControlText;
            this.labWWMT.ForeColor = SystemColors.ControlText;
            this.labRFID.ForeColor = SystemColors.ControlText;

            this.labCartonNumber.ForeColor = SystemColors.ControlText;
            this.labCartonN.ForeColor = SystemColors.ControlText;
            this.labQtys.ForeColor = SystemColors.ControlText;
            this.labPO.ForeColor = SystemColors.ControlText;
            this.labCantons.ForeColor = SystemColors.ControlText;
            this.labStyle.ForeColor = SystemColors.ControlText;

            this.labCartonNs.ForeColor = SystemColors.ControlText;
            this.labPolybagNumber.ForeColor = SystemColors.ControlText;
            this.labRFIDNumber.ForeColor = SystemColors.ControlText;

            this.labCustID.ForeColor = SystemColors.ControlText;
            this.label5.ForeColor = SystemColors.ControlText;
            this.label3.ForeColor = SystemColors.ControlText;
            this.label4.ForeColor = SystemColors.ControlText;
            this.label6.ForeColor = SystemColors.ControlText;
        }
        public void setAllright()
        {
            this.BackColor = System.Drawing.Color.Green;
            this.bgCartonInfo.BackColor = System.Drawing.Color.Green;
            this.bgScanNumbers.BackColor = System.Drawing.Color.Green;
            this.bgScaninfo.BackColor = System.Drawing.Color.Green;
            this.bgCartonLogs.BackColor = System.Drawing.Color.Green;
            this.labCartonNumber.BackColor = System.Drawing.Color.Green;
            this.labQtys.BackColor = System.Drawing.Color.Green;
            this.labCartonN.BackColor = System.Drawing.Color.Green;
            // this.dgvScanLogs.BackColor = System.Drawing.Color.Green;
            this.labPolybagNumber.BackColor = System.Drawing.Color.Green;
            this.labRFIDNumber.BackColor = System.Drawing.Color.Green;

            this.labPolySize.BackColor = System.Drawing.Color.Green;
            this.labSizeQty.BackColor = System.Drawing.Color.Green;
            this.labSizeQtys.BackColor = System.Drawing.Color.Green;
            //this.txtSizeQtys.BackColor = System.Drawing.Color.Green;
            this.labScanSizeQtys.BackColor = System.Drawing.Color.Green;
            //this.txtScanSizeQtys.BackColor = System.Drawing.Color.Green;

            this.labPolySize.ForeColor = System.Drawing.Color.White;
            this.labSizeQty.ForeColor = System.Drawing.Color.White;
            this.labSizeQtys.ForeColor = System.Drawing.Color.White;
            //this.txtSizeQtys.ForeColor = System.Drawing.Color.White;
            this.labScanSizeQtys.ForeColor = System.Drawing.Color.White;
            //this.txtScanSizeQtys.ForeColor = System.Drawing.Color.White;
            this.bgScaninfo.ForeColor = System.Drawing.Color.White;
            this.bgScanNumbers.ForeColor = System.Drawing.Color.White;
            this.bgCartonInfo.ForeColor = System.Drawing.Color.White;
            this.bgCartonLogs.ForeColor = System.Drawing.Color.White;
            this.labColor.ForeColor = System.Drawing.Color.White;
            this.labSizes.ForeColor = System.Drawing.Color.White;
            this.labCartonQty.ForeColor = System.Drawing.Color.White;
            this.labCartonScanQty.ForeColor = System.Drawing.Color.White;
            this.labCartonNoscanQty.ForeColor = System.Drawing.Color.White;
            this.labCarton.ForeColor = System.Drawing.Color.White;
            this.labPolybag.ForeColor = System.Drawing.Color.White;
            this.labWWMT.ForeColor = System.Drawing.Color.White;
            this.labRFID.ForeColor = System.Drawing.Color.White;

            this.labCartonNumber.ForeColor = System.Drawing.Color.White;
            this.labCartonN.ForeColor = System.Drawing.Color.White;
            this.labQtys.ForeColor = System.Drawing.Color.White;
            this.labPO.ForeColor = System.Drawing.Color.White;
            this.labCantons.ForeColor = System.Drawing.Color.White;
            this.labStyle.ForeColor = System.Drawing.Color.White;

            this.labCartonNs.ForeColor = System.Drawing.Color.White;
            this.labPolybagNumber.ForeColor = System.Drawing.Color.White;
            this.labRFIDNumber.ForeColor = System.Drawing.Color.White;

            this.labCustID.ForeColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label6.ForeColor = System.Drawing.Color.White;



        }
        public void setWrong()
        {
            this.BackColor = System.Drawing.Color.DarkRed;
            this.bgCartonInfo.BackColor = System.Drawing.Color.DarkRed;
            this.bgScanNumbers.BackColor = System.Drawing.Color.DarkRed;
            this.bgScaninfo.BackColor = System.Drawing.Color.DarkRed;
            this.bgCartonLogs.BackColor = System.Drawing.Color.DarkRed;
            this.labCartonNumber.BackColor = System.Drawing.Color.DarkRed;
            this.labQtys.BackColor = System.Drawing.Color.DarkRed;
            this.labCartonN.BackColor = System.Drawing.Color.DarkRed;
            // this.dgvScanLogs.BackColor = System.Drawing.Color.DarkRed;
            this.labPolybagNumber.BackColor = System.Drawing.Color.DarkRed;
            this.labRFIDNumber.BackColor = System.Drawing.Color.DarkRed;

            this.labPolySize.BackColor = System.Drawing.Color.DarkRed;
            this.labSizeQty.BackColor = System.Drawing.Color.DarkRed;
            this.labSizeQtys.BackColor = System.Drawing.Color.DarkRed;
            //this.txtSizeQtys.BackColor = System.Drawing.Color.DarkRed;
            this.labScanSizeQtys.BackColor = System.Drawing.Color.DarkRed;
            //this.txtScanSizeQtys.BackColor = System.Drawing.Color.DarkRed;
            this.bgScaninfo.ForeColor = System.Drawing.Color.Yellow;
            this.bgScanNumbers.ForeColor = System.Drawing.Color.Yellow;
            this.bgCartonInfo.ForeColor = System.Drawing.Color.Yellow;
            this.bgCartonLogs.ForeColor = System.Drawing.Color.Yellow;
            this.labColor.ForeColor = System.Drawing.Color.Yellow;
            this.labSizes.ForeColor = System.Drawing.Color.Yellow;
            this.labCartonQty.ForeColor = System.Drawing.Color.Yellow;
            this.labCartonScanQty.ForeColor = System.Drawing.Color.Yellow;
            this.labCartonNoscanQty.ForeColor = System.Drawing.Color.Yellow;
            this.labCarton.ForeColor = System.Drawing.Color.Yellow;
            this.labPolybag.ForeColor = System.Drawing.Color.Yellow;
            this.labWWMT.ForeColor = System.Drawing.Color.Yellow;
            this.labRFID.ForeColor = System.Drawing.Color.Yellow;

            this.labCartonNumber.ForeColor = System.Drawing.Color.Yellow;
            this.labCartonN.ForeColor = System.Drawing.Color.Yellow;
            this.labQtys.ForeColor = System.Drawing.Color.Yellow;
            this.labPO.ForeColor = System.Drawing.Color.Yellow;
            this.labCantons.ForeColor = System.Drawing.Color.Yellow;
            this.labStyle.ForeColor = System.Drawing.Color.Yellow;

            this.labCartonNs.ForeColor = System.Drawing.Color.Yellow;
            this.labPolybagNumber.ForeColor = System.Drawing.Color.Yellow;
            this.labRFIDNumber.ForeColor = System.Drawing.Color.Yellow;

            this.labPolySize.ForeColor = System.Drawing.Color.Yellow;
            this.labSizeQty.ForeColor = System.Drawing.Color.Yellow;
            this.labSizeQtys.ForeColor = System.Drawing.Color.Yellow;
            //this.txtSizeQtys.ForeColor = System.Drawing.Color.Yellow;
            this.labScanSizeQtys.ForeColor = System.Drawing.Color.Yellow;
            //this.txtScanSizeQtys.ForeColor = System.Drawing.Color.Yellow;

            this.labCustID.ForeColor = System.Drawing.Color.Yellow;
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label6.ForeColor = System.Drawing.Color.Yellow;
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
            this.txtPolybagNumber.Text = "";
            this.txtWwmtNumber.Text = "";
            this.txtRFIDNumber.Text = "";
            this.labCartonNumber.Text = "";
            this.labCartonN.Text = "";
            this.labQtys.Text = "0";
            this.labPolybagNumber.Text = "";
            this.labRFIDNumber.Text = "";


            this.labPolySize.Text = "";
            this.labSizeQty.Text = "";
            this.txtCustID.Text = "";


        }
        public void cleannewCartonDB()
        {
            if (newCarton != null)
            {
                newCarton.Rows.Clear();
                newCarton.Columns.Clear();
            }
        }

        private void serialPortPolybagNumber_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {


            byte[] bytes = new byte[2 * 1024];//创建缓存区接收数据
            int len = serialPortPolybagNumber.Read(bytes, 0, bytes.Length);
            //串口的相应数据
            string responseStr1 = Encoding.Default.GetString(bytes, 0, len);
            this.Invoke(new Action(
              delegate
              {
                  this.txtPolybagNumber.AppendText(responseStr1);
                  //txtError.AppendText(responseStr1);
                  if (System.Text.RegularExpressions.Regex.IsMatch(this.txtPolybagNumber.Text, @".*\r\n$"))
                  {
                      string PolybagNumber = this.txtPolybagNumber.Text;
                      PolybagNumber = PolybagNumber.Trim();
                      this.PolybagCheck(PolybagNumber);
                      this.txtPolybagNumber.Text = "";
                  }
              }));
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
                      this.RFIDCheck(RFIDNumber);
                      this.txtRFIDNumber.Text = "";
                  }
              }));

        }

        private void FrmLuluSingleScan_Load(object sender, EventArgs e)
        {
            this.initConnCom();

        }

        /// <summary>
        /// 计算校验码
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
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

        private void txtPolybagNumber_KeyDown(object sender, KeyEventArgs e)
        {
            //條碼有帶回車健
            if (e.KeyCode == Keys.Enter)
            {
                responseStr = this.txtPolybagNumber.Text;
                this.Invoke(new Action(
                  delegate
                  {
                      scanno = responseStr.Trim();
                      this.PolybagCheck(scanno);
                      this.txtPolybagNumber.Text = "";
                  }));
            }
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
                      scanno = responseStr.Trim();
                      this.RFIDCheck(scanno);
                      this.txtRFIDNumber.Text = "";
                  }));
            }
        }

        private void txtPolybagNumber_TextChanged(object sender, EventArgs e)
        {
            //USB使用
            //手動填資料顯示
            responseStr = this.txtPolybagNumber.Text;
            int dd = this.txtPolybagNumber.Text.Length;
            this.Invoke(new Action(
             delegate
             {
                 if (System.Text.RegularExpressions.Regex.IsMatch(responseStr, @".*\r\n$"))
                 {
                     scanno = responseStr;
                     scanno = scanno.Trim();
                     this.PolybagCheck(scanno);
                     this.txtPolybagNumber.Text = "";
                 }
             }));
        }

        private void txtRFIDNumber_TextChanged(object sender, EventArgs e)
        {  //USB使用
            //手動填資料顯示
            responseStr = this.txtRFIDNumber.Text;
            int dd = this.txtRFIDNumber.Text.Length;
            this.Invoke(new Action(
             delegate
             {
                 if (System.Text.RegularExpressions.Regex.IsMatch(responseStr, @".*\r\n$"))
                 {
                     scanno = responseStr;
                     scanno = scanno.Trim();
                     this.RFIDCheck(scanno);
                     this.txtRFIDNumber.Text = "";
                 }
             }));

        }

        private void dgvScanLogs_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void butSaveLogs_Click(object sender, EventArgs e)
        {
            if (ScanLogDB.Rows.Count <= 0)
            {
                this.setAllright();
                return;

            }
            this.SaveScanLogs();

        }

        public void SaveScanLogs()
        {
            int insetLogs = this.lscm.saveScanLog(ScanLogDB);

            if (insetLogs <= 0)
            {
                this.setWrong();
                // 本件已扫描过
                this.labMsg.ForeColor = Color.Red;
                this.labMsg.Text = "保存扫描记录失败，请重试保存";
                this.labRFIDNumber.BackColor = System.Drawing.Color.DarkRed;
                this.labRFIDNumber.ForeColor = System.Drawing.Color.Yellow;
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
            else
            {
                this.butSaveLogs.Visible = false;
                this.setAllright();
                this.ScanLogDB.Rows.Clear();
                this.ScanLogDB.Columns.Clear();
                this.labMsg.Text = "满箱，请扫描下一箱";
                this.labQtys.Text = "OK";

                //保存扫描 LOG日志文件

                if (strOpenMedia == "1")
                {
                    am.palyMedia(true);
                }
                if (strOpenAlarm == "1")
                {
                    am.closeportall();
                    Thread.Sleep(50);
                    am.openport1();
                    Thread.Sleep(300);

                }

            }
        }

        private void FrmLuluSingleScan_FormClosing(object sender, FormClosingEventArgs e)
        {

            this.CloseFrom();
        }


        private byte fBaud;
        private bool ComOpen = false;
        private int frmcomportindex;
        private int fOpenComIndex; //打开的串口索引号
        WyuanRFIDReaderHelper WyuanHelper = new WyuanRFIDReaderHelper();
        private void openRfidReader()
        {

            byte[] TrType = new byte[2];
            byte[] VersionInfo = new byte[2];
            byte ReaderType = 0;
            byte ScanTime = 0;
            byte dmaxfre = 0;
            byte dminfre = 0;
            byte powerdBm = 0;
            byte FreBand = 0;

            string PortName = SingleRFIDCOM;
            string BaudRate = "57600";
            string ReaderAddr = "FF";
            fBaud = 5;

            int dd = WyuanHelper.connRfidCOM(PortName, fBaud, ReaderAddr);
            if (dd == 0)
            {
                ComOpen = true;
                frmcomportindex = Convert.ToInt32(PortName.Substring(3, PortName.Length - 3));
                fOpenComIndex = frmcomportindex;
                WyuanInfos info = WyuanHelper.GetReaderInfo();

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
                    FreBand = info._FreBand;
                }
                else if (dd == 1)
                {
                    ComOpen = false;
                    //  COM 口已被占用
                    MessageBox.Show("COM Opened", "Information");
                    return;
                }
                else if (dd == -1)
                {
                    ComOpen = false;
                    //  COM 连接失败
                    MessageBox.Show("Serial Communication Error or Occupied", "Information");
                    return;
                }
                else
                {
                    ComOpen = false;
                    //  COM 未知错误
                    MessageBox.Show("Serial Communication Error or Occupied", "Information");
                    return;
                }
                ComOpen = true;
                WyuanWorkMode WorkMode = WyuanHelper.getRFIDWorkMode();

            }


        }
        private void CloseRfidReader()
        {
            string PortName = SingleRFIDCOM;
            int dd = WyuanHelper.CloseRfidReader(PortName );
            if (dd == 0)
            {
                MessageBox.Show("断开连接", "Information");

            } else if(dd == -1)
            {
                MessageBox.Show("Serial Communication Error", "Information");
            }


        }




        private void labStatusRuning_Click(object sender, EventArgs e)
        {
            if (labStatusRuning.Text == "STOP")
            {
                this.initConnCom();
            }
            else
            {
                this.CloseFrom();
            }

        }
        public void CloseFrom()
        {
            this.labStatusRuning.Text = "STOP";
            this.timer1.Enabled = false;
            //timer1.Enabled = !timer1.Enabled;

            am.CloseAlarm();

            this.serialPortPolybagNumber.Close();
            CloseRfidReader();
            this.labStatusRuning.BackColor = Color.Red;
            this.labAlarmStatus.BackColor = Color.Red;
            this.labPolybagStatus.BackColor = Color.Red;
            this.labRFIDReaderStatus.BackColor = Color.Red;
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
            this.serialPortPolybagNumber.PortName = PolybagCOM;
            this.serialPortRFIDNumber.PortName = SingleRFIDCOM;


            try
            {
                if (!this.serialPortPolybagNumber.IsOpen)
                {
                    this.serialPortPolybagNumber.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.labMsg.Text = "内包装袋条码阅读器 " + PolybagCOM + " 连接失败";
                labPolybagStatus.BackColor = Color.Red;


                //   throw;
            }
            if (this.serialPortPolybagNumber.IsOpen)
            {
                labPolybagStatus.BackColor = Color.LightGreen;
            }
            else
            {
                labPolybagStatus.BackColor = Color.Red;
            }

            openRfidReader();
            if (ComOpen)
            {
                labRFIDReaderStatus.BackColor = Color.LightGreen;
            }
            else
            {
                labRFIDReaderStatus.BackColor = Color.Red;
            }
            // timer1.Enabled = !timer1.Enabled;
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;

            }
            if (am.OpenAlarm() && this.serialPortPolybagNumber.IsOpen && ComOpen && timer1.Enabled)
            {
                labStatusRuning.BackColor = Color.Green;
                labStatusRuning.Text = "RUNING...";
            }
            else
            {
                labStatusRuning.BackColor = Color.Red;
                labStatusRuning.Text = "STOP";
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          String RfidStr =   WyuanHelper.GetData();
            if(RfidStr != "")
            {
                RfidStr = RfidStr + "\r\n";
                this.txtRFIDNumber.Text = RfidStr;
            }

        }

    }

}