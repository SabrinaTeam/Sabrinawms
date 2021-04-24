using BLL;
using MODEL;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmOutgoing : Form
    {
        private static FrmOutgoing frm;
        public outGoingManager ogm = new outGoingManager();
        public receiManager rm = new receiManager();
        public AccomplishTask TaskCallBack;
        public UpdateUI UpdateUIDelegate;
        public delegate void AccomplishTask();
        public delegate void UpdateUI(bar barstr);
        private delegate void AsynUpdateUI(bar barstr);

        private BackgroundWorker bw = new BackgroundWorker();
         
        private bar barstr = new bar();
        public bool isod_ouDB = true;

        public DataTable dt = new DataTable();

        public string org = "";
        public string subinv ="";
        public string location = "";
        public string starTime = "";
        public string stopTime = "";

        public FrmOutgoing()
        {
            InitializeComponent();
            this.dgvOutgoingTable.DoubleBufferedDataGirdView(true);
            this.dgvOutMWH.DoubleBufferedDataGirdView(true);
            this.dgvOutCount.DoubleBufferedDataGirdView(true);
            

            UpdateUIDelegate += UpdataUIStatus;//绑定更新任务状态的委托
            TaskCallBack += Accomplish;//绑定完成任务要调用的委托
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
        }
        public static FrmOutgoing GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmOutgoing();
            }
            return frm;
        }

        private void FrmOutgoing_Resize(object sender, EventArgs e)
        {
            if (this.Width <= 810)
            {
                this.Width = 810;
            }
            if (this.Height <= 710)
            {
                this.Height = 710;
            }

            bgSearch.Top = 5;
            bgSearch.Left = 5;
            bgSearch.Width = this.Width - 20;

            tbOut.Left = bgSearch.Left;
            tbOut.Width = bgSearch.Width;
            tbOut.Height = this.Height - this.bgSearch.Height - 55;
        }

        private void FrmOutgoing_Load(object sender, EventArgs e)
        {
            DateTime current_day = this.dtpkStarTime.Value;
            DateTime first_day = current_day.AddDays(1 - current_day.Day);
            DateTime last_day = current_day.AddDays(1 - current_day.Day).AddMonths(1).AddDays(-1);

            this.dtpkStarTime.Value = first_day;
            this.dtpkStopTime.Value = last_day;

            /*
             // 当前月份的第一天和最后一天
             DateTime current_day = dateTimePicker1.Value; 
             DateTime first_day = current_day.AddDays(1 - current_day.Day); 
             DateTime last_day = current_day.AddDays(1 - current_day.Day).AddMonths(1).AddDays(-1);

            //上个月的第一天和最后一天
            DateTime current_day = dateTimePicker1.Value; 
            DateTime first_day = current_day.AddDays(1 - current_day.Day).AddMonths(-1); 
            DateTime last_day = current_day.AddDays(1 - current_day.Day).AddDays(-1);
             */

        }

        private void rabutSAA_CheckedChanged(object sender, EventArgs e)
        {
            string org = "SAA";
            this.getSubinvs(org);
        }
        public void getSubinvs(string org)
        {
            this.progressBar1.Value = 10;
            this.progressBar2.Value = 10;
            this.labWorking.Text = "查询仓库....";
            Cursor = Cursors.WaitCursor;

            DataTable subinvs = ogm.getSubinvs(org);
            if (subinvs.Rows.Count <= 0)
            {

                this.progressBar2.Value = 100;
                MessageBox.Show("没有找到仓库");
                this.labWorking.Text = "没有找到仓库.";
                Cursor = Cursors.Default;
                return;
            }
            this.cbSubinv.Items.Clear();
            this.progressBar1.Value = 50;
            this.progressBar2.Value = 50;
            foreach (DataRow item in subinvs.Rows)
            {
                this.cbSubinv.Items.Add(item["subinv"].ToString());

            }
            this.labWorking.Text = "本次任务进度：";
            this.progressBar1.Value = 100;
            this.progressBar2.Value = 100;         
            Cursor = Cursors.Default;
        }

        private void rabutTOP_CheckedChanged(object sender, EventArgs e)
        {
            string org = "TOP";
            this.getSubinvs(org);
        }

        private void cbSubinv_SelectedIndexChanged(object sender, EventArgs e)
        {
           string location =  this.cbSubinv.SelectedItem.ToString();
            this.progressBar1.Value = 10;
            this.progressBar2.Value = 10;
            this.labWorking.Text = "查询储位...";
            Cursor = Cursors.WaitCursor;
            DataTable subinvs = ogm.getLocation(location);
            if (subinvs.Rows.Count <= 0)
            {
                this.progressBar1.Value = 100;
                this.progressBar2.Value = 100;
                this.labWorking.Text = "没有找到储位...";
                MessageBox.Show("没有找到储位");
                Cursor = Cursors.Default;
                return;
            }
            this.cbLocation.Items.Clear();
            this.progressBar1.Value = 50;
            this.progressBar2.Value = 50;
            foreach (DataRow item in subinvs.Rows)
            {
                this.cbLocation.Items.Add(item["Location"].ToString());

            }
            this.labWorking.Text = "本次任务进度：";
            this.progressBar1.Value = 100;
            this.progressBar2.Value = 100;          
            Cursor = Cursors.Default;
             
        }

        private void butSearch_Click(object sender, EventArgs e)
        {

           
            if (this.rabutSAA.Checked)
            {
                this.org = "SAA";
            }

            if(this.rabutTOP.Checked)            
            {
                this.org = "TOP";
            }
          
            this.subinv = this.cbSubinv.Text.Trim();
            this.location = this.cbLocation.Text.Trim();
            this.starTime = this.dtpkStarTime.Value.ToString("yyyy-MM-dd");
          
            this.stopTime = this.dtpkStopTime.Value.AddDays(1).ToString("yyyy-MM-dd");

            if (org.Length <= 0)
            {
                MessageBox.Show("厂区不能为空");
                return;
            }

            if(this.subinv.Length<=0)
            {
                MessageBox.Show("仓库不能为空");
                return;
            }

            if(this.location.Length<=0)
            {
                MessageBox.Show("储位不能为空");
                return; 
            }

            if (this.starTime.Length <= 0)
            {
                MessageBox.Show("开始时间不能为空");
                return;
            }

            if (this.stopTime.Length <= 0)
            {
                MessageBox.Show("结束时间不能为空");
                return;
            }
            this.dgvOutgoingTable.DataSource = null;
            this.dt.Clear();
            this.splitContainer1.Panel2Collapsed = false;
            this.tbOut.SelectedIndex= 0;

            Cursor = Cursors.WaitCursor;
          
                if (!bw.IsBusy)
                {
                   
                    bw.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("系统正在运行，请稍等...");
            }
               
             
        }


        public void UpdataUIStatus(bar barstr)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (bar s)
                {
                    this.progressBar1.Maximum = s.maxstep;
                    this.progressBar1.Value = s.step;
                    this.labWorking.Text = s.str + "    " + s.step.ToString() + "/" + s.maxstep.ToString() ;

                    this.progressBar2.Maximum = s.maxstep2;
                    this.progressBar2.Value = s.step2;
                    this.labWorking2.Text = s.str2 + "    " + s.step2.ToString() + "/" + s.maxstep2.ToString() ;

                }), barstr);
            }
            else
            {
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = barstr.maxstep;
                this.progressBar1.Value = barstr.step;
                this.labWorking.Text = barstr.str + "    " + barstr.step.ToString() + "/" + barstr.maxstep.ToString() ;

                this.progressBar2.Minimum = 0;
                this.progressBar2.Maximum = barstr.maxstep2;
                this.progressBar2.Value = barstr.step2;
                this.labWorking2.Text = barstr.str2 + "    " + barstr.step2.ToString() + "/" + barstr.maxstep2.ToString() ;
            }
        }

        //完成任务时需要调用
        private void Accomplish()
        {     
            if (this.dt != null)
            {
                this.dgvOutgoingTable.DataSource = null;
                this.dgvOutgoingTable.DataSource = this.dt;
                if (isod_ouDB)
                {
                    changOd_Ou_HeaderText();
                }
                else
                {
                    changOuHeaderText();
                }
               
                Cursor = Cursors.Default;
                this.dgvOutgoingTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            }
            TaskCallBack -= Accomplish; //取消侦听注册事件，避免多次侦听
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            barstr.str = "正在查询入库扫描资料...";
            barstr.step = 1;
            barstr.maxstep = 4;
            barstr.str2 = "查询入库扫描资料";
            barstr.step2 = 1;
            barstr.maxstep2 = 4;
            UpdateUIDelegate(barstr);


            DataTable outGoingDT = ogm.getOutgoing(org, subinv, location, starTime, stopTime);
            if (outGoingDT.Rows.Count <= 0)
            {
                barstr.str2 = "没有入库资料";
                barstr.step2 = 100;
                barstr.maxstep2 = 100;
                barstr.str = "没有入库资料.";
                barstr.step = 100;
                barstr.maxstep = 100;
                UpdateUIDelegate(barstr);
                // Cursor = Cursors.Default;
                //MessageBox.Show("没有入库资料");
                // return;

                // 查找 手工入库单 

                // select * from countreceis WHERE org='TOP' and subinv ='TA_HD' and line ='CF36D'  and  `status` = 0   
                //   public string org = "";
                DataTable od_ouDB = new DataTable();
                od_ouDB.Columns.Add(new DataColumn("org", Type.GetType("System.String"))); // 厂区
                od_ouDB.Columns.Add(new DataColumn("cust_id", Type.GetType("System.String"))); // 客户
                od_ouDB.Columns.Add(new DataColumn("my_no", Type.GetType("System.String"))); // 自编单号
                od_ouDB.Columns.Add(new DataColumn("season_id", Type.GetType("System.String"))); // 季节
                od_ouDB.Columns.Add(new DataColumn("OrderPO", Type.GetType("System.String"))); // 订单PO  成品扫描
                od_ouDB.Columns.Add(new DataColumn("GtnPO", Type.GetType("System.String"))); // 出货PO
                od_ouDB.Columns.Add(new DataColumn("MAIN_LINE", Type.GetType("System.String"))); // PO-LINE   
                od_ouDB.Columns.Add(new DataColumn("con_no", Type.GetType("System.String")));// 箱号
                od_ouDB.Columns.Add(new DataColumn("Buyer_Item", Type.GetType("System.String"))); // 款号
                od_ouDB.Columns.Add(new DataColumn("yymm", Type.GetType("System.String"))); // 订单月BUY
                od_ouDB.Columns.Add(new DataColumn("buy_cname", Type.GetType("System.String"))); // 订单月BUY 中文名
                od_ouDB.Columns.Add(new DataColumn("color_code", Type.GetType("System.String")));// 色组
                od_ouDB.Columns.Add(new DataColumn("Size1", Type.GetType("System.String"))); // 尺码
                od_ouDB.Columns.Add(new DataColumn("size_qty", Type.GetType("System.String"))); // 尺码件数
                od_ouDB.Columns.Add(new DataColumn("box_qty", Type.GetType("System.String"))); // 箱件数
                od_ouDB.Columns.Add(new DataColumn("kg", Type.GetType("System.String"))); // 箱重量
                od_ouDB.Columns.Add(new DataColumn("PO_qty", Type.GetType("System.String"))); // PO#订单数
                od_ouDB.Columns.Add(new DataColumn("subinv", Type.GetType("System.String"))); // 仓库码
                od_ouDB.Columns.Add(new DataColumn("location", Type.GetType("System.String"))); // 储位码
                od_ouDB.Columns.Add(new DataColumn("mark", Type.GetType("System.String"))); // 备注
                od_ouDB.Columns.Add(new DataColumn("TagNumber", Type.GetType("System.String"))); // 外箱条码
                od_ouDB.Columns.Add(new DataColumn("Create_Pc", Type.GetType("System.String"))); // 上传电脑名
                od_ouDB.Columns.Add(new DataColumn("ScanTime", Type.GetType("System.String"))); // 扫描时间 
                od_ouDB.Columns.Add(new DataColumn("Update_Date", Type.GetType("System.String"))); // 上传时间

                od_ouDB.Columns.Add(new DataColumn("po_no", Type.GetType("System.String"))); // 订单PO BEST 
                od_ouDB.Columns.Add(new DataColumn("od_no", Type.GetType("System.String"))); // 订单号 BEST
                od_ouDB.Columns.Add(new DataColumn("od_date", Type.GetType("System.String"))); // 接单日期
                od_ouDB.Columns.Add(new DataColumn("Bcust_id", Type.GetType("System.String"))); // 客户 BEST
                od_ouDB.Columns.Add(new DataColumn("w_id", Type.GetType("System.String")));// 厂区ID
                od_ouDB.Columns.Add(new DataColumn("release_who", Type.GetType("System.String"))); // 订单业务员
                od_ouDB.Columns.Add(new DataColumn("style_id", Type.GetType("System.String"))); // 款式
                od_ouDB.Columns.Add(new DataColumn("clr_no", Type.GetType("System.String")));  // 色组
                od_ouDB.Columns.Add(new DataColumn("od_type", Type.GetType("System.String")));// 订单类别
                od_ouDB.Columns.Add(new DataColumn("id", Type.GetType("System.String"))); //数据库ID

                //查询 
                DataTable noBarCodeDt = ogm.getReceiFromNoBarCode(org, subinv, location, starTime, stopTime);
                if (noBarCodeDt.Rows.Count > 0)
                {

                    /*
                     org,
                                subinv,
                                line,
                                style,
                                color,
                                size,
                                SUM( qtyCount ) qtyCount,
                                SUM( PO ) OffsetQty,
                                receiNumber ,
                                receiDate
                     */
                    for (int i = 0; i < noBarCodeDt.Rows.Count; i++)
                    {
                        DataRow dr = od_ouDB.NewRow();

                        dr["org"] = noBarCodeDt.Rows[i]["org"].ToString(); // 厂区
                        dr["cust_id"] = ""; // 客户
                        dr["season_id"] = ""; // 季节
                        dr["OrderPO"] = "无条码单"; // 订单PO  成品扫描 
                        dr["GtnPO"] = "无条码单"; // 出货PO
                        dr["MAIN_LINE"] = ""; // PO-LINE      
                        dr["con_no"] = "";// 箱号
                        dr["Buyer_Item"] = noBarCodeDt.Rows[i]["style"].ToString(); // 款号
                        dr["yymm"] = ""; // 订单月BUY
                        dr["buy_cname"] = ""; // 订单月BUY 中文名
                        dr["color_code"] = noBarCodeDt.Rows[i]["color"].ToString();// 色组 
                        dr["Size1"] = noBarCodeDt.Rows[i]["size"].ToString(); // 尺码
                        dr["size_qty"] = noBarCodeDt.Rows[i]["qtyCount"].ToString(); // 尺码件数
                        dr["box_qty"] = ""; // 箱件数
                        dr["kg"] = ""; // 箱重量
                        dr["PO_qty"] = ""; // 款式件数
                        dr["subinv"] = noBarCodeDt.Rows[i]["subinv"].ToString(); // 仓库码
                        dr["location"] = noBarCodeDt.Rows[i]["line"].ToString(); // 储位码
                        dr["mark"] = ""; // 备注
                        dr["TagNumber"] = ""; // 外箱条码
                        dr["Create_Pc"] = noBarCodeDt.Rows[i]["receiInPcName"].ToString(); // 上传电脑名
                        dr["ScanTime"] = noBarCodeDt.Rows[i]["receiDate"].ToString(); // 扫描时间
                        dr["Update_Date"] = ""; // 上传时间
                        dr["po_no"] = ""; // 订单PO BEST 
                                          //dr["od_no"] = od_infoDB.Rows[j]["od_no"].ToString(); // 订单号 BEST
                        dr["od_date"] = ""; // 接单日期
                        dr["Bcust_id"] = ""; // 客户 
                        dr["w_id"] = "";// 厂区ID
                        dr["release_who"] = ""; // 订单业务员
                        dr["style_id"] = noBarCodeDt.Rows[i]["style"].ToString(); // 款式
                        dr["clr_no"] = "";  // 色组 
                        dr["od_type"] = "";// 订单类别
                        dr["id"] = ""; //数据库ID
                        od_ouDB.Rows.Add(dr);
                    }

                    barstr.str = "正在查询订单系统资料.";
                    barstr.step = 4;
                    barstr.maxstep = 4;

                    barstr.str2 = "正在查询订单系统资料..";
                    barstr.step2 = 4;
                    barstr.maxstep2 = 4;
                    UpdateUIDelegate(barstr);

                    //  this.dgvOutgoingTable.DataSource = null;
                    //  this.dgvOutgoingTable.DataSource = od_ouDB;

                    this.isod_ouDB = true;
                    this.dt.Clear();
                    this.dt = od_ouDB;
                }



                }
                else
            {


                // 过滤沖銷的條碼號
                DataTable offSetDT = ogm.getOffSet(org, subinv, location, starTime, stopTime);
                if (offSetDT.Rows.Count > 0)
                {
                    for (int i = 0; i < offSetDT.Rows.Count; i++)
                    {
                        for (int j = 0; j < outGoingDT.Rows.Count; j++)
                        {
                            if (outGoingDT.Rows[j]["TagNumber"].ToString() == offSetDT.Rows[i]["TagNumber"].ToString())
                            {
                                outGoingDT.Rows.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                }
                // 建立list  PO  款号 色组    PO+款号+色组
                List<outGoing_pos> ogps = new List<outGoing_pos>();
                bool isOgps = true;

                barstr.str = "正在查询入库扫描记录.";
                barstr.step = 0;
                barstr.maxstep = outGoingDT.Rows.Count;

                barstr.str2 = "正在查询入库扫描记录";
                barstr.step2 = 2;
                barstr.maxstep2 = 4;
                UpdateUIDelegate(barstr);

                for (int i = 0; i < outGoingDT.Rows.Count; i++)
                {
                    barstr.step = i;
                    UpdateUIDelegate(barstr);

                    outGoing_pos ogp = new outGoing_pos();
                    if (outGoingDT.Rows[i]["OrderPO"].ToString().Length <= 0)
                    {
                        ogp.po_ids = outGoingDT.Rows[i]["GtnPO"].ToString();
                    }
                    else
                    {
                        ogp.po_ids = outGoingDT.Rows[i]["OrderPO"].ToString();

                    }

                    ogp.style_ids = outGoingDT.Rows[i]["Buyer_Item"].ToString();
                    ogp.clr_nos = outGoingDT.Rows[i]["color_code"].ToString();
                    ogp.OGACDate = outGoingDT.Rows[i]["OGACDate"].ToString();
                    ogp.Plant = outGoingDT.Rows[i]["Plant"].ToString();
                    ogp.pscs = ogp.po_ids + ogp.style_ids + ogp.clr_nos;
                    isOgps = ogpsIsExists(ogps, ogp.pscs);

                    if (!isOgps)
                    {
                        ogps.Add(ogp);
                    }
                }

                if (ogps.Count <= 0)
                {
                    barstr.str = "没有入库资料.";
                    barstr.step = 100;
                    barstr.maxstep = 100;

                    barstr.str2 = "没有入库资料..";
                    barstr.step2 = 100;
                    barstr.maxstep2 = 100;
                    UpdateUIDelegate(barstr);
                    MessageBox.Show("没有入库资料");
                    return;
                }

                barstr.str = "正在查询订单系统资料.";
                barstr.step = 3;
                barstr.maxstep = 4;

                barstr.str2 = "正在查询订单系统资料..";
                barstr.step2 = 3;
                barstr.maxstep2 = 4;
                UpdateUIDelegate(barstr);

                //查询 //測試網絡
                DataTable od_infoDB = ogm.getOD_POFromBestByPSC(ogps);
                barstr.str = "正在查询订单系统资料.";
                barstr.step = 0;
                barstr.maxstep = od_infoDB.Rows.Count;
                barstr.str2 = "正在查询订单系统资料..";
                barstr.step2 = 0;
                barstr.maxstep2 = outGoingDT.Rows.Count;

                UpdateUIDelegate(barstr);
                if (od_infoDB == null || od_infoDB.Rows.Count <= 0)
                {
                    barstr.str = "没有找到订单数据.";
                    barstr.step = 100;
                    barstr.maxstep = 100;

                    barstr.str2 = "没有找到订单数据..";
                    barstr.step2 = 100;
                    barstr.maxstep2 = 100;
                    UpdateUIDelegate(barstr);
                    MessageBox.Show("没有找到订单数据");
                    this.isod_ouDB = false;
                    this.dt.Clear();
                    this.dt = outGoingDT;
                }
                else
                {
                    DataTable od_ouDB = new DataTable();
                    od_ouDB.Columns.Add(new DataColumn("org", Type.GetType("System.String"))); // 厂区
                    od_ouDB.Columns.Add(new DataColumn("cust_id", Type.GetType("System.String"))); // 客户
                    od_ouDB.Columns.Add(new DataColumn("my_no", Type.GetType("System.String"))); // 自编单号
                    od_ouDB.Columns.Add(new DataColumn("season_id", Type.GetType("System.String"))); // 季节
                    od_ouDB.Columns.Add(new DataColumn("OrderPO", Type.GetType("System.String"))); // 订单PO  成品扫描
                    od_ouDB.Columns.Add(new DataColumn("GtnPO", Type.GetType("System.String"))); // 出货PO
                    od_ouDB.Columns.Add(new DataColumn("MAIN_LINE", Type.GetType("System.String"))); // PO-LINE   
                    od_ouDB.Columns.Add(new DataColumn("con_no", Type.GetType("System.String")));// 箱号
                    od_ouDB.Columns.Add(new DataColumn("Buyer_Item", Type.GetType("System.String"))); // 款号
                    od_ouDB.Columns.Add(new DataColumn("yymm", Type.GetType("System.String"))); // 订单月BUY
                    od_ouDB.Columns.Add(new DataColumn("buy_cname", Type.GetType("System.String"))); // 订单月BUY 中文名
                    od_ouDB.Columns.Add(new DataColumn("color_code", Type.GetType("System.String")));// 色组
                    od_ouDB.Columns.Add(new DataColumn("Size1", Type.GetType("System.String"))); // 尺码
                    od_ouDB.Columns.Add(new DataColumn("size_qty", Type.GetType("System.String"))); // 尺码件数
                    od_ouDB.Columns.Add(new DataColumn("box_qty", Type.GetType("System.String"))); // 箱件数
                    od_ouDB.Columns.Add(new DataColumn("kg", Type.GetType("System.String"))); // 箱重量
                    od_ouDB.Columns.Add(new DataColumn("PO_qty", Type.GetType("System.String"))); // PO#订单数
                    od_ouDB.Columns.Add(new DataColumn("subinv", Type.GetType("System.String"))); // 仓库码
                    od_ouDB.Columns.Add(new DataColumn("location", Type.GetType("System.String"))); // 储位码
                    od_ouDB.Columns.Add(new DataColumn("mark", Type.GetType("System.String"))); // 备注
                    od_ouDB.Columns.Add(new DataColumn("TagNumber", Type.GetType("System.String"))); // 外箱条码
                    od_ouDB.Columns.Add(new DataColumn("Create_Pc", Type.GetType("System.String"))); // 上传电脑名
                    od_ouDB.Columns.Add(new DataColumn("ScanTime", Type.GetType("System.String"))); // 扫描时间 
                    od_ouDB.Columns.Add(new DataColumn("Update_Date", Type.GetType("System.String"))); // 上传时间

                    od_ouDB.Columns.Add(new DataColumn("po_no", Type.GetType("System.String"))); // 订单PO BEST 
                    od_ouDB.Columns.Add(new DataColumn("od_no", Type.GetType("System.String"))); // 订单号 BEST
                    od_ouDB.Columns.Add(new DataColumn("od_date", Type.GetType("System.String"))); // 接单日期
                    od_ouDB.Columns.Add(new DataColumn("Bcust_id", Type.GetType("System.String"))); // 客户 BEST
                    od_ouDB.Columns.Add(new DataColumn("w_id", Type.GetType("System.String")));// 厂区ID
                    od_ouDB.Columns.Add(new DataColumn("release_who", Type.GetType("System.String"))); // 订单业务员
                    od_ouDB.Columns.Add(new DataColumn("style_id", Type.GetType("System.String"))); // 款式
                    od_ouDB.Columns.Add(new DataColumn("clr_no", Type.GetType("System.String")));  // 色组
                    od_ouDB.Columns.Add(new DataColumn("od_type", Type.GetType("System.String")));// 订单类别
                    od_ouDB.Columns.Add(new DataColumn("id", Type.GetType("System.String"))); //数据库ID

                    /*
                     b1.po_no,
               h1.cust_id,
               h1.season_id,
               h1.release_who,
               b1.style_id,
               b1.clr_no
                     */
                    DataTable infoDB = new DataTable();
                    infoDB.Columns.Add(new DataColumn("po_no", Type.GetType("System.String"))); // PO
                    infoDB.Columns.Add(new DataColumn("cust_id", Type.GetType("System.String"))); // 客户
                    infoDB.Columns.Add(new DataColumn("season_id", Type.GetType("System.String"))); // 季節號
                    infoDB.Columns.Add(new DataColumn("release_who", Type.GetType("System.String"))); // 更新人
                    infoDB.Columns.Add(new DataColumn("style_id", Type.GetType("System.String"))); // 款號
                    infoDB.Columns.Add(new DataColumn("clr_no", Type.GetType("System.String"))); // 色組

                    infoDB.Columns.Add(new DataColumn("yymm", Type.GetType("System.String"))); // 月BUY
                    infoDB.Columns.Add(new DataColumn("buy_cname", Type.GetType("System.String"))); // 月BUY 中文
                                                                                                    // b.po_no

                    // 查找訂單月BUY


                    for (int i = 0; i < od_infoDB.Rows.Count; i++)
                    {
                        string yymmStr = "";
                        string buyStr = "";
                        DataTable YYMM = ogm.getYYMMFromBestByPo(od_infoDB.Rows[i]["po_no"].ToString());
                        if (YYMM.Rows.Count > 0)
                        {
                            for (int j = 0; j < YYMM.Rows.Count; j++)
                            {
                                yymmStr = yymmStr + YYMM.Rows[j]["yymm"].ToString() + ',';
                                buyStr = buyStr + YYMM.Rows[j]["buy_cname"].ToString() + ',';

                            }
                        }
                        yymmStr = yymmStr.Substring(0, yymmStr.Length - 1);
                        buyStr = buyStr.Substring(0, buyStr.Length - 1);

                        DataRow dr = infoDB.NewRow();
                        dr["po_no"] = od_infoDB.Rows[i]["po_no"].ToString();
                        dr["cust_id"] = od_infoDB.Rows[i]["cust_id"].ToString();
                        dr["season_id"] = od_infoDB.Rows[i]["season_id"].ToString();
                        dr["release_who"] = od_infoDB.Rows[i]["release_who"].ToString();
                        dr["style_id"] = od_infoDB.Rows[i]["style_id"].ToString();
                        dr["clr_no"] = od_infoDB.Rows[i]["clr_no"].ToString();
                        dr["yymm"] = yymmStr;
                        dr["buy_cname"] = buyStr;
                        infoDB.Rows.Add(dr);

                        // yymmStr = "";
                        // buyStr = "";
                    }
                    od_infoDB.Clear();
                    od_infoDB = infoDB;



                    barstr.maxstep = od_infoDB.Rows.Count;
                    barstr.step = 0;
                    barstr.maxstep2 = outGoingDT.Rows.Count;
                    barstr.step2 = 0;
                    UpdateUIDelegate(barstr);
                    //合并订单数据与入库数据
                    for (int i = 0; i < outGoingDT.Rows.Count; i++)
                    {
                        //  barstr.step2 = Convert.ToInt32( Convert.ToDouble(i) / Convert.ToDouble(outGoingDT.Rows.Count)  * 100);
                        barstr.step2 = i;
                        //UpdateUIDelegate(barstr);


                        for (int j = 0; j < od_infoDB.Rows.Count; j++)
                        {
                            // barstr.step = Convert.ToInt32(Convert.ToDouble(j) / Convert.ToDouble(od_infoDB.Rows.Count) * 100);
                            //      barstr.step = j;
                            //   UpdateUIDelegate(barstr);

                            if ( //outGoingDT.Rows[i]["OrderPO"].ToString() == od_infoDB.Rows[j]["po_no"].ToString() &&
                                outGoingDT.Rows[i]["Buyer_Item"].ToString() == od_infoDB.Rows[j]["style_id"].ToString() &&
                                outGoingDT.Rows[i]["color_code"].ToString() == od_infoDB.Rows[j]["clr_no"].ToString() &&
                                //outGoingDT.Rows[i]["OGACDate"].ToString() == od_infoDB.Rows[j]["def_date"].ToString() &&
                                //outGoingDT.Rows[i]["Plant"].ToString() == od_infoDB.Rows[j]["area_id"].ToString() &&
                                //od_infoDB.Rows[j]["def_date"].ToString() != ""
                                outGoingDT.Rows[i]["GtnPO"].ToString() == od_infoDB.Rows[j]["po_no"].ToString()


                                )
                            {
                                DataRow dr = od_ouDB.NewRow();
                                dr["org"] = outGoingDT.Rows[i]["org"].ToString(); // 厂区
                                dr["cust_id"] = outGoingDT.Rows[i]["cust_id"].ToString(); // 客户
                                                                                          //  dr["my_no"] = od_infoDB.Rows[j]["my_no"].ToString(); // 自编单号
                                dr["season_id"] = od_infoDB.Rows[j]["season_id"].ToString(); // 季节
                                dr["OrderPO"] = outGoingDT.Rows[i]["OrderPO"].ToString(); // 订单PO  成品扫描 
                                dr["GtnPO"] = outGoingDT.Rows[i]["GtnPO"].ToString(); // 出货PO
                                dr["MAIN_LINE"] = outGoingDT.Rows[i]["MAIN_LINE"].ToString(); // PO-LINE      
                                dr["con_no"] = outGoingDT.Rows[i]["con_no"].ToString();// 箱号
                                dr["Buyer_Item"] = outGoingDT.Rows[i]["Buyer_Item"].ToString(); // 款号
                                dr["yymm"] = od_infoDB.Rows[j]["yymm"].ToString(); // 订单月BUY
                                dr["buy_cname"] = od_infoDB.Rows[j]["buy_cname"].ToString(); // 订单月BUY 中文名
                                dr["color_code"] = outGoingDT.Rows[i]["color_code"].ToString();// 色组 
                                dr["Size1"] = outGoingDT.Rows[i]["Size1"].ToString(); // 尺码

                                dr["size_qty"] = outGoingDT.Rows[i]["size_qty"].ToString(); // 尺码件数
                                dr["box_qty"] = outGoingDT.Rows[i]["box_qty"].ToString(); // 箱件数
                                dr["kg"] = outGoingDT.Rows[i]["kg"].ToString(); // 箱重量
                                dr["PO_qty"] = outGoingDT.Rows[i]["PO_qty"].ToString(); // 订单数
                                dr["subinv"] = outGoingDT.Rows[i]["subinv"].ToString(); // 仓库码
                                dr["location"] = outGoingDT.Rows[i]["location"].ToString(); // 储位码
                                                                                            // dr["mark"] = od_infoDB.Rows[j]["mark"].ToString(); // 备注
                                dr["TagNumber"] = outGoingDT.Rows[i]["TagNumber"].ToString(); // 外箱条码
                                dr["Create_Pc"] = outGoingDT.Rows[i]["Create_Pc"].ToString(); // 上传电脑名
                                dr["ScanTime"] = outGoingDT.Rows[i]["ScanTime"].ToString(); // 扫描时间
                                dr["Update_Date"] = outGoingDT.Rows[i]["Update_Date"].ToString(); // 上传时间


                                dr["po_no"] = od_infoDB.Rows[j]["po_no"].ToString(); // 订单PO BEST 
                                                                                     //dr["od_no"] = od_infoDB.Rows[j]["od_no"].ToString(); // 订单号 BEST
                                                                                     //dr["od_date"] = od_infoDB.Rows[j]["od_date"].ToString(); // 接单日期
                                dr["Bcust_id"] = od_infoDB.Rows[j]["cust_id"].ToString(); // 客户 
                                                                                          // dr["w_id"] = od_infoDB.Rows[j]["w_id"].ToString();// 厂区ID
                                dr["release_who"] = od_infoDB.Rows[j]["release_who"].ToString(); // 订单业务员
                                dr["style_id"] = od_infoDB.Rows[j]["style_id"].ToString(); // 款式
                                dr["clr_no"] = od_infoDB.Rows[j]["clr_no"].ToString();  // 色组 
                                                                                        //dr["od_type"] = od_infoDB.Rows[j]["od_type"].ToString();// 订单类别

                                dr["id"] = outGoingDT.Rows[i]["id"].ToString(); //数据库ID
                                od_ouDB.Rows.Add(dr);
                            }
                        }
                    }


                    // 查找 手工入库单 

                    // select * from countreceis WHERE org='TOP' and subinv ='TA_HD' and line ='CF36D'  and  `status` = 0   
                    //   public string org = "";

                    //查询 
                    DataTable noBarCodeDt = ogm.getReceiFromNoBarCode(org, subinv, location, starTime, stopTime);
                    if (noBarCodeDt.Rows.Count > 0)
                    {

                        /*
                         org,
                                    subinv,
                                    line,
                                    style,
                                    color,
                                    size,
                                    SUM( qtyCount ) qtyCount,
                                    SUM( PO ) OffsetQty,
                                    receiNumber ,
                                    receiDate
                         */
                        for (int i = 0; i < noBarCodeDt.Rows.Count; i++)
                        {
                            DataRow dr = od_ouDB.NewRow();

                            dr["org"] = noBarCodeDt.Rows[i]["org"].ToString(); // 厂区
                            dr["cust_id"] = ""; // 客户
                            dr["season_id"] = ""; // 季节
                            dr["OrderPO"] = "无条码单"; // 订单PO  成品扫描 
                            dr["GtnPO"] = "无条码单"; // 出货PO
                            dr["MAIN_LINE"] = ""; // PO-LINE      
                            dr["con_no"] = "";// 箱号
                            dr["Buyer_Item"] = noBarCodeDt.Rows[i]["style"].ToString(); // 款号
                            dr["yymm"] = ""; // 订单月BUY
                            dr["buy_cname"] = ""; // 订单月BUY 中文名
                            dr["color_code"] = noBarCodeDt.Rows[i]["color"].ToString();// 色组 
                            dr["Size1"] = noBarCodeDt.Rows[i]["size"].ToString(); // 尺码
                            dr["size_qty"] = noBarCodeDt.Rows[i]["qtyCount"].ToString(); // 尺码件数
                            dr["box_qty"] = ""; // 箱件数
                            dr["kg"] = ""; // 箱重量
                            dr["PO_qty"] = ""; // 款式件数
                            dr["subinv"] = noBarCodeDt.Rows[i]["subinv"].ToString(); // 仓库码
                            dr["location"] = noBarCodeDt.Rows[i]["line"].ToString(); // 储位码
                            dr["mark"] = ""; // 备注
                            dr["TagNumber"] = ""; // 外箱条码
                            dr["Create_Pc"] = noBarCodeDt.Rows[i]["receiInPcName"].ToString(); // 上传电脑名
                            dr["ScanTime"] = noBarCodeDt.Rows[i]["receiDate"].ToString(); // 扫描时间
                            dr["Update_Date"] = ""; // 上传时间
                            dr["po_no"] = ""; // 订单PO BEST 
                                              //dr["od_no"] = od_infoDB.Rows[j]["od_no"].ToString(); // 订单号 BEST
                            dr["od_date"] = ""; // 接单日期
                            dr["Bcust_id"] = ""; // 客户 
                            dr["w_id"] = "";// 厂区ID
                            dr["release_who"] = ""; // 订单业务员
                            dr["style_id"] = noBarCodeDt.Rows[i]["style"].ToString(); // 款式
                            dr["clr_no"] = "";  // 色组 
                            dr["od_type"] = "";// 订单类别
                            dr["id"] = ""; //数据库ID
                            od_ouDB.Rows.Add(dr);
                        }

                    }




                    barstr.str = "正在查询订单系统资料.";
                    barstr.step = 4;
                    barstr.maxstep = 4;

                    barstr.str2 = "正在查询订单系统资料..";
                    barstr.step2 = 4;
                    barstr.maxstep2 = 4;
                    UpdateUIDelegate(barstr);

                    //  this.dgvOutgoingTable.DataSource = null;
                    //  this.dgvOutgoingTable.DataSource = od_ouDB;

                    this.isod_ouDB = true;
                    this.dt.Clear();
                    this.dt = od_ouDB;

                }
            }
            barstr.str = "查询完成.";
            barstr.step = 0;
            barstr.maxstep = 0;

            barstr.str2 = "查询完成.";
            barstr.step2 = 0;
            barstr.maxstep2 = 0;
            UpdateUIDelegate(barstr);

          //  Cursor = Cursors.Default;
           // this.dgvOutgoingTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //  this.gbLoad.Text = "加载完成";
            barstr.str = "查询完成";
            barstr.step = 100;
            barstr.maxstep = 100;
            barstr.str2 = "查询完成";
            barstr.step2 = 100;
            barstr.maxstep2 = 100;
            UpdateUIDelegate(barstr);

            this.dgvOutgoingTable.DataSource = null;
            if (this.dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有资料");
                Cursor = Cursors.Default;
                return;
            }
            this.dgvOutgoingTable.DataSource = this.dt;
            
            if (isod_ouDB)
            {
                changOd_Ou_HeaderText();
            }
            else
            {
                changOuHeaderText();
            }
            Cursor = Cursors.Default;
             this.dgvOutgoingTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
        
        }

        public   List<outGoing_pos> removeDuplicate1(List<outGoing_pos> list)
        {
            for (int i = 0; i < list.Count; i++)  //外循环是循环的次数
            {
                for (int j = list.Count - 1; j > i; j--)  //内循环是 外循环一次比较的次数
                {

                    if (list[i] == list[j])
                    {
                        list.RemoveAt(j);
                    }

                }
            }
            return list;
        }

        public bool ogpsIsExists(List<outGoing_pos> ogps, string pscs)
        {
            bool t = false;
            if (ogps.Count <= 0)
            {
                t = false;
                return t;
               
            }
           
                for( int i=0; i< ogps.Count; i++)
                {
                    if(ogps[i].pscs == pscs)
                    {
                        t = true;
                        continue;
                    }
                    else
                    {
                        t = false;
                    }

                }
            return t;
            
            
        }

        public void changOuHeaderText()
        {
            this.dgvOutgoingTable.Columns["org"].HeaderText = "厂区"; 
            this.dgvOutgoingTable.Columns["cust_id"].HeaderText = "客户";
            this.dgvOutgoingTable.Columns["OrderPO"].HeaderText = "订单PO";
            this.dgvOutgoingTable.Columns["GtnPO"].HeaderText = "出货PO";
            this.dgvOutgoingTable.Columns["MAIN_LINE"].HeaderText = "PO-LINE";
            this.dgvOutgoingTable.Columns["con_no"].HeaderText = "箱号";

            this.dgvOutgoingTable.Columns["Buyer_Item"].HeaderText = "款号";            
            this.dgvOutgoingTable.Columns["color_code"].HeaderText = "色组"; 
            this.dgvOutgoingTable.Columns["Size1"].HeaderText = "尺码";
            this.dgvOutgoingTable.Columns["size_qty"].HeaderText = "尺码件数";
            this.dgvOutgoingTable.Columns["box_qty"].HeaderText = "箱件数";
            this.dgvOutgoingTable.Columns["kg"].HeaderText = "箱重量";

            this.dgvOutgoingTable.Columns["subinv"].HeaderText = "仓库码";
            this.dgvOutgoingTable.Columns["location"].HeaderText = "储位码";
            this.dgvOutgoingTable.Columns["TagNumber"].HeaderText = "外箱条码";

            this.dgvOutgoingTable.Columns["Create_Pc"].HeaderText = "上传电脑名";
            this.dgvOutgoingTable.Columns["ScanTime"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            this.dgvOutgoingTable.Columns["ScanTime"].HeaderText = "扫描时间";
            this.dgvOutgoingTable.Columns["Update_Date"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            this.dgvOutgoingTable.Columns["Update_Date"].HeaderText = "上传时间";

           
            this.dgvOutgoingTable.Columns["id"].Visible = false;
        }

        public void changOd_Ou_HeaderText()
        {          

            this.dgvOutgoingTable.Columns["org"].HeaderText = "厂区";
            this.dgvOutgoingTable.Columns["cust_id"].HeaderText = "客户";
            this.dgvOutgoingTable.Columns["my_no"].HeaderText = "自编单号";
            this.dgvOutgoingTable.Columns["season_id"].HeaderText = "季节";
            this.dgvOutgoingTable.Columns["OrderPO"].HeaderText = "订单PO";
            this.dgvOutgoingTable.Columns["GtnPO"].HeaderText = "出货PO";

            this.dgvOutgoingTable.Columns["MAIN_LINE"].HeaderText = "PO-LINE";
            this.dgvOutgoingTable.Columns["con_no"].HeaderText = "箱号";
            this.dgvOutgoingTable.Columns["Buyer_Item"].HeaderText = "款号";
            this.dgvOutgoingTable.Columns["yymm"].HeaderText = "订单月BUY";
            this.dgvOutgoingTable.Columns["buy_cname"].HeaderText = "订单月BUY中文名";
            this.dgvOutgoingTable.Columns["buy_cname"].Visible = false;
            this.dgvOutgoingTable.Columns["color_code"].HeaderText = "色组";
            this.dgvOutgoingTable.Columns["Size1"].HeaderText = "尺码";

            this.dgvOutgoingTable.Columns["size_qty"].HeaderText = "尺码件数";
            this.dgvOutgoingTable.Columns["box_qty"].HeaderText = "箱件数";
            this.dgvOutgoingTable.Columns["kg"].HeaderText = "箱重量";
            this.dgvOutgoingTable.Columns["PO_qty"].HeaderText = "PO订单数";
            this.dgvOutgoingTable.Columns["subinv"].HeaderText = "仓库码";
            this.dgvOutgoingTable.Columns["location"].HeaderText = "储位码";

            this.dgvOutgoingTable.Columns["mark"].HeaderText = "备注";
            this.dgvOutgoingTable.Columns["TagNumber"].HeaderText = "外箱条码";
            this.dgvOutgoingTable.Columns["Create_Pc"].HeaderText = "上传电脑名";

            this.dgvOutgoingTable.Columns["ScanTime"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            this.dgvOutgoingTable.Columns["ScanTime"].HeaderText = "扫描时间";

            this.dgvOutgoingTable.Columns["Update_Date"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            this.dgvOutgoingTable.Columns["Update_Date"].HeaderText = "上传时间";

            this.dgvOutgoingTable.Columns["po_no"].HeaderText = "订单PO BEST ";
            this.dgvOutgoingTable.Columns["od_no"].HeaderText = "订单号 BEST";
            this.dgvOutgoingTable.Columns["od_date"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            this.dgvOutgoingTable.Columns["od_date"].HeaderText = "接单日期";
            this.dgvOutgoingTable.Columns["Bcust_id"].HeaderText = "客户";
            this.dgvOutgoingTable.Columns["w_id"].HeaderText = "厂区ID";
            this.dgvOutgoingTable.Columns["release_who"].HeaderText = "订单业务员";
            this.dgvOutgoingTable.Columns["style_id"].HeaderText = "款式";
            this.dgvOutgoingTable.Columns["clr_no"].HeaderText = "色组";
            this.dgvOutgoingTable.Columns["od_type"].HeaderText = "订单类别";
            this.dgvOutgoingTable.Columns["id"].HeaderText = "数据库ID";

            this.dgvOutgoingTable.Columns["po_no"].Visible = false;
            this.dgvOutgoingTable.Columns["od_no"].Visible = false;
            this.dgvOutgoingTable.Columns["od_date"].Visible = false;
            this.dgvOutgoingTable.Columns["Bcust_id"].Visible = false;
            this.dgvOutgoingTable.Columns["w_id"].Visible = false;
            this.dgvOutgoingTable.Columns["release_who"].Visible = false;
            this.dgvOutgoingTable.Columns["style_id"].Visible = false;
            this.dgvOutgoingTable.Columns["clr_no"].Visible = false;
            this.dgvOutgoingTable.Columns["od_type"].Visible = false;
            this.dgvOutgoingTable.Columns["id"].Visible = false;
        }

        private void dgvOutgoingTable_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvOutgoingTable.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvOutgoingTable.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            int tcindex  = this.tbOut.SelectedIndex ;
            ImproExcel(tcindex);
        }
        public void ImproExcel(int tcIndex)
        {

            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";
            //   sdfExport.ShowDialog();
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            String tableName = "";
            NPOIExcelOutGoing NPOIexcel = new NPOIExcelOutGoing();
            DataTable tabl = new DataTable();
            if(tcIndex <0)
            {
                return;
            }
            if(tcIndex == 0)
            {
                tabl = GetDgvToTable(this.dgvOutgoingTable);
                 tableName = "dgvOutgoingTable";
            }

            if(tcIndex == 1)
            {
                tabl = GetDgvToTable(this.dgvOutCount);
                tableName = "dgvOutCount";

            }

          

            // DataTable dt = (StyleCodeInfodataGridView.DataSource as DataTable);
            NPOIexcel.ExcelWrite(filename, tabl, tableName);//excelhelper写出
            if (MessageBox.Show("导出成功，文件保存在" + filename.ToString() + ",是否打开此文件？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (File.Exists(filename))//文件是否存在
                {
                    Process.Start(filename);//执行打开导出的文件
                }
                else
                {
                    MessageBox.Show("文件不存在！", "提示");
                }
            }
        }
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
 
        public int hiedcolumnindex = -1; //是否选中外面

        private void dgvOutgoingTable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvOutgoingTable.Rows[e.RowIndex].Selected == false)
                    {
                        dgvOutgoingTable.ClearSelection();
                        dgvOutgoingTable.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvOutgoingTable.SelectedRows.Count == 1)
                    {
                        dgvOutgoingTable.CurrentCell = dgvOutgoingTable.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    MenuRight.Show(MousePosition.X, MousePosition.Y);
                    // MessageBox.Show("点右键了");
                }

                else if (e.ColumnIndex >= 0)
                {
                    this.hiedcolumnindex = e.ColumnIndex;
                    MenuRight.Show(MousePosition.X, MousePosition.Y);

                }

            }
        }

        private void dgvOutgoingTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tags = "";
            if (e.RowIndex < 0)
            {
                return; 
            }

            /*
            barstr.str = "正在查询PO多个自编单号的相关信息.";
            // 查询 订单号、自编单号、PO、PO数量
            for (int i = 0; i < od_ouDB.Rows.Count; i++)
            {
                barstr.step = i / od_ouDB.Rows.Count * 100;
                barstr.maxstep = 100;
                UpdateUIDelegate(barstr);
                string po_no = od_ouDB.Rows[i]["po_no"].ToString();
                string clr_no = od_ouDB.Rows[i]["color_code"].ToString();
                string style_id = od_ouDB.Rows[i]["Buyer_Item"].ToString();
                string area_id = od_ouDB.Rows[i]["area_id"].ToString();
                string def_date = od_ouDB.Rows[i]["def_date"].ToString();

                DataTable mynos = ogm.getMy_NoFromBest(po_no, clr_no, style_id, area_id, def_date);


            }
            */


            this.splitContainer1.Panel2Collapsed = true;
            tags = this.dgvOutgoingTable["TagNumber", e.RowIndex].Value.ToString();
            DataTable moveLocalDT = ogm.getMoveLocals(tags);
            this.dgvOutMWH.DataSource = null;
            this.dgvOutMWH.DataSource = moveLocalDT;
            this.dgvOutMWH.ReadOnly = true;
            this.dgvOutMWH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOutMWH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutMWH.AllowUserToAddRows = false;
            int k = 0;

            if (this.dgvOutMWH == null)
            {
                MessageBox.Show("没有数据，请更改查询条件！");
                Cursor = Cursors.Default;
                return;
            }

            k = this.dgvOutMWH.Rows.Count;
            if (k <= 0)
            {
                MessageBox.Show("没有数据，请更改查询条件！");
                Cursor = Cursors.Default;
                return;
            }

            this.dgvOutMWH.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            this.dgvOutMWH.Columns["id"].HeaderText = "ID";
            this.dgvOutMWH.Columns["TagNumber"].HeaderText = "条码号";           
            this.dgvOutMWH.Columns["max_time"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            this.dgvOutMWH.Columns["max_time"].HeaderText = "最后扫描时间";
            this.dgvOutMWH.Columns["Location"].HeaderText = "储位";  
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 更新总表的转厂数量
            DataTable styledt = rm.getOutCounts(org, location, subinv);
        }

        private void butExcelReport_Click(object sender, EventArgs e)
            
        {
            if (this.dt.Rows.Count <= 0)
            {
                return;
            }
            DataTable disDt = new DataTable();
            DataView myDataView = new DataView(this.dt);
            //此处可加任意数据项组合
            string[] strComuns = { "yymm", "Buyer_Item", "color_code", "GtnPO", "MAIN_LINE", "PO_qty" };
            disDt = myDataView.ToTable(true, strComuns);
            if (disDt.Rows.Count < 0)
            {
                return;
            }

            DataTable dataDt = new DataTable();
            string[] dataComuns = { "ScanTime" };
            myDataView.Sort = "ScanTime";
            dataDt = myDataView.ToTable(true, dataComuns);
          
            if (dataDt.Rows.Count <= 0)
            {
                return;
            }

            for(int i = 0; i < dataDt.Rows.Count; i++)
            {
                string columnName = dataDt.Rows[i]["ScanTime"].ToString();
                if(columnName.Length <= 0)
                {
                    disDt.Columns.Add("无条码入库单");
                }
                else
                {
                    disDt.Columns.Add(columnName);
                }
               
            }
            int size_qty = 0;
            // 填充数量
            for (int i = 0; i< disDt.Rows.Count; i++)
            {
                for( int j = 0; j < this.dt.Rows.Count; j++)
                {
                    if (disDt.Rows[i]["yymm"].ToString() == this.dt.Rows[j]["yymm"].ToString() &&
                        disDt.Rows[i]["Buyer_Item"].ToString() == this.dt.Rows[j]["Buyer_Item"].ToString() &&
                        disDt.Rows[i]["color_code"].ToString() == this.dt.Rows[j]["color_code"].ToString() &&
                        disDt.Rows[i]["GtnPO"].ToString() == this.dt.Rows[j]["GtnPO"].ToString() &&
                        disDt.Rows[i]["MAIN_LINE"].ToString() == this.dt.Rows[j]["MAIN_LINE"].ToString() 
                        )
                    {
                        for(int k = 0; k < disDt.Columns.Count; k++)
                        {
                            if (disDt.Columns[k].ToString() == this.dt.Rows[j]["ScanTime"].ToString())
                            {

                                string  Qtys = this.dt.Rows[j]["size_qty"].ToString();
                                if(Qtys.Length<=0)
                                {
                                    Qtys = "0";
                                }
                                if(disDt.Rows[i][k].ToString().Length <= 0)
                                {
                                    disDt.Rows[i][k] = 0;
                                }
                                disDt.Rows[i][k] = Convert.ToInt32(disDt.Rows[i][k]) + Convert.ToInt32( Qtys);
                            }

                        }

                    }
                }

            }

            disDt.Columns.Add("Count");
            int count = 0;
            for (int i = 0; i < disDt.Rows.Count; i++)
            {
                for(int j = 6; j < disDt.Columns.Count; j++)
                {
                    string Qtys =disDt.Rows[i][j].ToString();
                    if (Qtys.Length <= 0)
                    {
                        Qtys = "0";
                    }

                    count  = count + Convert.ToInt32( Qtys);
                }
                disDt.Rows[i]["Count"] = count;
                count = 0;
            }
            this.dgvOutCount.DataSource = null;
            this.dgvOutCount.DataSource = disDt;
            this.tbOut.SelectedIndex = 1;
           
            ImproExcel(1);

        }

        private void dgvOutCount_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvOutCount_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (this.dgvOutCount.Rows[e.RowIndex].Selected == false)
                    {
                        this.dgvOutCount.ClearSelection();
                        this.dgvOutCount.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (this.dgvOutCount.SelectedRows.Count == 1)
                    {
                        this.dgvOutCount.CurrentCell = this.dgvOutCount.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    MenuRight.Show(MousePosition.X, MousePosition.Y);
                    // MessageBox.Show("点右键了");
                }

                else if (e.ColumnIndex >= 0)
                {
                    this.hiedcolumnindex = e.ColumnIndex;
                    MenuRight.Show(MousePosition.X, MousePosition.Y);

                }

            }
        }
    }
}
