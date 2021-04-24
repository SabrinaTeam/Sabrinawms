using BLL;
using COMMON;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmDeliveryCompare : Form
    {
        public AccomplishTask TaskCallBack;
        public UpdateUI UpdateUIDelegate;
        public delegate void AccomplishTask();
        public delegate void UpdateUI(bar barstr);
        private delegate void AsynUpdateUI(bar barstr);
        private delegate void testBackColor(Color controlColor);
        private bar barstr = new bar();
        public string fileNameStr = "";
        public string sheeTnameStr = "";
        public int headNoStr = -1;

        public string starTime = "";
        public string stopTime = "";
        public string org = "";
        public string subinv = "";
        List<string> location = new List<string>();
        // public string location = "";

        private DataTable dtFormExcel = new DataTable();
        private DataTable dtFromWriteExcel = new DataTable();
        private DataTable dtFromLocalHost = new DataTable();
        private DataTable dtnoEques = new DataTable();
        private DataTable dtEques = new DataTable();

        private static FrmDeliveryCompare frm;
        deliveryCompareManager dcm = new deliveryCompareManager();
        private String filename = null;
        public FrmDeliveryCompare()
        {
            InitializeComponent();
            dgvExcels.DoubleBufferedDataGirdView(true);
            dgvLocalHostDB.DoubleBufferedDataGirdView(true);
        }
        public static FrmDeliveryCompare GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmDeliveryCompare();
            }
            return frm;
        }
        private void FrmDeliveryCompare_Resize(object sender, EventArgs e)
        {
            // 852, 761

            if (this.Width <= 852)
            {
                this.Width = 852;
            }
            if (this.Height <= 761)
            {
                this.Height = 761;
            }         
            bgSearch.Width = this.Width - 20;

            tabControl1.Left = bgSearch.Left;
            tabControl1.Width = bgSearch.Width;
            tabControl1.Height = this.Height - this.bgSearch.Height - 55;
        }

        private void rabutSAA_CheckedChanged(object sender, EventArgs e)
        {
            this.org = "SAA";
            this.getSubinvs(org);
        }

        private void rabutTOP_CheckedChanged(object sender, EventArgs e)
        {
            this.org = "TOP";
            this.getSubinvs(org);
        }
        public void getSubinvs(string org)
        {
            this.progressBar1.Value = 10;
            this.labWorking.Text = "查询仓库....";
            Cursor = Cursors.WaitCursor;

            DataTable subinvs = dcm.getSubinv(org);
            if (subinvs.Rows.Count <= 0)
            {
                this.progressBar1.Value = 100;
                MessageBox.Show("没有找到仓库");
                this.labWorking.Text = "没有找到仓库.";
                Cursor = Cursors.Default;
                return;
            }
            this.cbSubinv.Items.Clear();
            this.clboxLocation.Items.Clear();
            this.progressBar1.Value = 50;
            List<string> li = new List<string>();
            foreach (DataRow item in subinvs.Rows)
            {

                if (!this.isExList(li, item["subinv"].ToString()))
                {
                    li.Add(item["subinv"].ToString());
                }
            }
            if (li.Count <= 0)
            {

            }
            else
            {
                for(int i = 0; i < li.Count; i++)
                {
                    this.cbSubinv.Items.Add(li[i]);
                }
            }
             
            
            this.labWorking.Text = "获取仓库完成...";
            this.progressBar1.Value = 100;
            Cursor = Cursors.Default;
        }
        public bool isExList(List<string> li,string str)
        {
            bool isExLi = false;
            for(int i = 0; i < li.Count; i++)
            {
                if(li[i] == str)
                {
                    isExLi = true;
                    break;
                }else
                {
                    isExLi = false;
                }
            }
            return isExLi;
            
        }

        private void cbSubinv_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.subinv = this.cbSubinv.SelectedItem.ToString();
            this.progressBar1.Value = 10;
            this.labWorking.Text = "查询线别...";
            Cursor = Cursors.WaitCursor;
            DataTable subinvs = dcm.getLocations(this.org ,this.subinv);
            if (subinvs.Rows.Count <= 0)
            {
                this.progressBar1.Value = 100;               
                this.labWorking.Text = "没有找到线别...";
                MessageBox.Show("没有找到线别.");
                Cursor = Cursors.Default;
                return;
            }
            this.clboxLocation.Items.Clear();
            this.progressBar1.Value = 50;


            List<string> li = new List<string>();
            foreach (DataRow item in subinvs.Rows)
            {

             //   this.clboxLocation.Items.Add(item["Location"].ToString());

                if (!this.isExList(li, item["Location"].ToString()))
                {
                    li.Add(item["Location"].ToString());
                }
            }
            if (li.Count <= 0)
            {

            }
            else
            {
                for (int i = 0; i < li.Count; i++)
                {
                    this.clboxLocation.Items.Add(li[i]);
                }
            }

         //   foreach (DataRow item in subinvs.Rows)
          //  {
          //      this.clboxLocation.Items.Add(item["Location"].ToString());
         //   }
            this.labWorking.Text = "获取线别完成...";
            this.progressBar1.Value = 100;
            Cursor = Cursors.Default;
        }

        private void btnSelected_Click(object sender, EventArgs e)
        {
            this.cbSelectSheet.Items.Clear();
            this.dgvExcels.DataSource = null;
            this.dgvLocalHostDB.DataSource = null;
            this.dgvCompareResult.DataSource = null;
            OpenFileDialog sdfExport = new OpenFileDialog();

            sdfExport.Filter = "Excel 2007文件|*.xlsx|Excel 97-2003文件|*.xls";
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            filename = sdfExport.FileName;
            string excelName = System.IO.Path.GetFileName(filename);//文件名

            this.txtSelectedFilePath.Text = excelName;

            string[] sheetnames = xiaomingCommom.getExcelSheetSum(filename);

            for (int t = 0; t < sheetnames.Length; t++)
            {
                this.cbSelectSheet.Items.Add(sheetnames[t]);//添加表名
            }
            MessageBox.Show("请选择工作表!");
            this.cbSelectSheet.DroppedDown = true;
            this.btnCompare.Enabled = true;
        }

        private void cbSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnCompare.Enabled = true;
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            //加载
            Cursor = Cursors.WaitCursor;
           if(this.org == "")
            {
                MessageBox.Show("请选择厂区");
                Cursor = Cursors.Default;
                return;
            }
            if (this.subinv == "")
            {
                MessageBox.Show("请选择仓库");
                Cursor = Cursors.Default;
                return;
            }
            if (this.location.Count <= 0)
            {
                MessageBox.Show("请选择线别");
                Cursor = Cursors.Default;
                return;
            }
            


            this.progressBar1.Visible = true;
            this.progressBar1.Value = 1;
            this.dgvExcels.DataSource = null;
            this.dgvLocalHostDB.DataSource = null;
            this.dgvCompareResult.DataSource = null;
            Application.DoEvents();
            loadExcel();
            this.progressBar1.Value = 100;
            Cursor = Cursors.Default;
            this.btnCompare.Enabled = false;

          
        }


        private void loadExcel()
        {
            this.dgvExcels.DataSource = null;
            this.dgvLocalHostDB.DataSource = null;
            this.dgvCompareResult.DataSource = null;
            if (this.txtSelectedFilePath.Text.Length <= 0)
            {
                MessageBox.Show("请选择文件！");
                Cursor = Cursors.Default;
                return;
            }
            if (this.cbSelectSheet.SelectedIndex == -1)
            {
                MessageBox.Show("请选择表名！");
                this.cbSelectSheet.DroppedDown = true;
                Cursor = Cursors.Default;
                return;
            }


          //  DateTime current_day = this.dtpStarTime.Value;
           // DateTime first_day = current_day.AddDays( current_day.Day);
          //  DateTime last_day = current_day.AddDays( current_day.Day);

          //  this.dtpStarTime.Value = first_day;
          //  this.dtpStopTime.Value = last_day;

            this.starTime = this.dtpStarTime.Value.ToString("yyyy-MM-dd");
            this.stopTime = this.dtpStopTime.Value.ToString("yyyy-MM-dd");
            string sheetname = this.cbSelectSheet.SelectedItem.ToString();            
            int headno = 0;
            /*
            // if(textHeadno.Text)
            Regex reg = new Regex("^[0-9]+$"); //正则表达式 检测是否数字
            Match ma = reg.Match(txtSheetHead.Text.ToString());
            if (!ma.Success)
            {
                // MessageBox.Show("部门编码不是数字！");
                // return;
                headno = 0;
                txtSheetHead.Text = headno.ToString();
            }
          
            if (Convert.ToInt32(txtSheetHead.Text) < 0 || txtSheetHead.Text == "")
            {
                headno = 0;
            }
            headno = Convert.ToInt32(txtSheetHead.Text);

              */
            this.dgvExcels.ReadOnly = true;
            this.dgvExcels.AllowUserToAddRows = false;

            UpdateUIDelegate += UpdataUIStatus;//绑定更新任务状态的委托
            TaskCallBack += Accomplish;//绑定完成任务要调用的委托
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerAsync();
            }
            this.fileNameStr = filename;
            this.sheeTnameStr = sheetname;
            this.headNoStr = headno;
        }
        public void UpdataUIStatus(bar barstr)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (bar s)
                {
                    this.progressBar1.Maximum = s.maxstep;
                    this.progressBar1.Value = s.step;
                    this.labWorking.Text = s.str + "    " + s.step.ToString() + "/" + s.maxstep.ToString();
                }), barstr);
            }
            else
            {
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = barstr.maxstep;
                this.progressBar1.Value = barstr.step;
                this.labWorking.Text = barstr.str + "    " + barstr.step.ToString() + "/" + barstr.maxstep.ToString();
            }
        }

        //完成任务时需要调用
        private void Accomplish()
        {            
            if (this.dtFromWriteExcel != null)
            {
                if (this.dtFromLocalHost.Rows == null || this.dtFromLocalHost.Rows.Count <= 0)
                {
                    return;
                }
                if (this.dtFormExcel.Rows == null || this.dtFormExcel.Rows.Count <= 0)
                {
                    return;
                }
                if (this.dtnoEques.Rows == null || this.dtnoEques.Rows.Count <= 0)
                {
                    return;
                }
                this.dgvExcels.DataSource = this.dtFromWriteExcel;
                this.dgvLocalHostDB.DataSource = this.dtFromLocalHost; ;
                this.dgvCompareResult.DataSource = this.dtnoEques;
                this.dgvCompareResult.EnableHeadersVisualStyles = false;
                for (int i = 0; i < dgvCompareResult.Rows.Count; i++)
                {
                    if (this.dgvCompareResult.Rows[i].Cells[8].Value.ToString() != "0")
                    {
                        //行背景色
                        // this.dgvCompareResult.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");

                        //单元格背景色
                        this.dgvCompareResult.Rows[i].Cells[8].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    }
                }
                this.dgvCompareResult.Refresh();
                changdgvExcelsHeaderText();                
                changdgvLocalHostDBHeaderText();               
                changdgvCompareResultHeaderText();
                Cursor = Cursors.Default;
                this.dgvExcels.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                this.dgvLocalHostDB.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                // this.dgvCompareResult.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                
                MessageBox.Show("加载完成");
            }
            TaskCallBack -= Accomplish; //取消侦听注册事件，避免多次侦听
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            barstr.str = "正在加载EXCEL...";
            barstr.step = 50;
            barstr.maxstep = 100;
            // FrmPacklist.UpdateUI(barstr);
            UpdateUIDelegate(barstr);
            if (this.fileNameStr == "")
            {
                return;
            }
            if (this.sheeTnameStr == "")
            {
                return;
            }
            if (this.headNoStr == -1)
            {
                return;
            }

            this.dtFormExcel = dcm.ExcelRead(this.fileNameStr, this.sheeTnameStr, this.headNoStr);
            if (this.dtFormExcel.Rows.Count <= 0)
            {
                this.dtFormExcel = null;
                MessageBox.Show(null, "读取Excel资料错误,请检查格式是否正确", "Excel文件", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            
            for(int t=0;  t < this.dtFormExcel.Rows.Count; t++){
                string dd = this.dtFormExcel.Rows[t]["deliveryDate"].ToString();
                string[] date = dd.Split('-');
                if (date.Length < 3)
                {
                    this.dtFormExcel = null;
                    this.dtFromWriteExcel = null;
                    
                    MessageBox.Show("EXCEL 日期格式不正確："+ dd);
                    
                    return;
                }
                while (date[0].Length < 4)
                {
                    date[0] = "0" + date[0];
                }
                while (date[1].Length < 2)
                {
                    date[1] = "0" + date[1];
                }
                while (date[2].Length < 2)
                {
                    date[2] = "0" + date[2];
                }

                this.dtFormExcel.Rows[t]["deliveryDate"] = date[0] +"-"+ date[1] +"-"+ date[2];

            }
            // 写入
            int WResult = dcm.writeCompareFileToDb(dtFormExcel);
            if (WResult <= 0)
            {

                MessageBox.Show(null, "送货资料保存失败", "保存资料失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //查询刚导入的EXCEL 
            string createPC = Dns.GetHostName().ToString();
            this.dtFromWriteExcel = dcm.getFromWriteExcel(createPC, this.starTime, this.stopTime);
            if (this.dtFromWriteExcel.Rows.Count <= 0)
            {
                MessageBox.Show(null, "没有找到当前时间段的送货信息,送货数量为 0", "数量错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //查询本地  
            this.dtFromLocalHost = dcm.getLocalHostDt(this.starTime, this.stopTime, this.org, this.subinv, this.location);
            if (dtFromLocalHost.Rows.Count <= 0)
            {
                MessageBox.Show(null, "没有找到当前时间段的入库信息,入库数量为  0", "数量错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //比较
            // 合并成ID  用ID 来比较是否相同的行 
            DataTable edt = new DataTable();
            edt.Columns.Add("ID", typeof(string));
            edt.Columns.Add("DeliveryDate", typeof(string));
            edt.Columns.Add("styleId", typeof(string));
            edt.Columns.Add("colorId", typeof(string));
            edt.Columns.Add("lineName", typeof(string));
            edt.Columns.Add("sizeName", typeof(string));
            edt.Columns.Add("Qty", typeof(int));
            
            for (int i = 0; i < dtFromWriteExcel.Rows.Count; i++)
            {
                DataRow erow = edt.NewRow();
                erow["ID"] = dtFromWriteExcel.Rows[i]["DeliveryDate"].ToString() +
                             dtFromWriteExcel.Rows[i]["styleId"].ToString() +
                             dtFromWriteExcel.Rows[i]["colorId"].ToString() +
                             dtFromWriteExcel.Rows[i]["sizeName"].ToString();
                erow["DeliveryDate"] = dtFromWriteExcel.Rows[i]["DeliveryDate"].ToString();
                erow["styleId"] = dtFromWriteExcel.Rows[i]["styleId"].ToString();
                erow["colorId"] = dtFromWriteExcel.Rows[i]["colorId"].ToString();
                erow["lineName"] = dtFromWriteExcel.Rows[i]["lineName"].ToString();
                erow["sizeName"] = dtFromWriteExcel.Rows[i]["sizeName"].ToString();
                erow["Qty"] = Convert.ToInt32(dtFromWriteExcel.Rows[i]["Qty"].ToString());
                edt.Rows.Add(erow);
            }


            DataTable ldt = new DataTable();
            ldt.Columns.Add("ID", typeof(string));
            ldt.Columns.Add("ScanTime", typeof(string));
            ldt.Columns.Add("Buyer_Item", typeof(string));
            ldt.Columns.Add("color_code", typeof(string));
            ldt.Columns.Add("location", typeof(string));
            ldt.Columns.Add("Size1", typeof(string));
            ldt.Columns.Add("Qty", typeof(int));

            for (int j = 0; j < dtFromLocalHost.Rows.Count; j++)
            {
                DataRow lrow = ldt.NewRow();
                lrow["ID"] = dtFromLocalHost.Rows[j]["ScanTime"].ToString() +
                             dtFromLocalHost.Rows[j]["Buyer_Item"].ToString() +
                             dtFromLocalHost.Rows[j]["color_code"].ToString() +
                             dtFromLocalHost.Rows[j]["Size1"].ToString();
                lrow["ScanTime"] = dtFromLocalHost.Rows[j]["ScanTime"].ToString();
                lrow["Buyer_Item"] = dtFromLocalHost.Rows[j]["Buyer_Item"].ToString();
                lrow["color_code"] = dtFromLocalHost.Rows[j]["color_code"].ToString();
                lrow["location"] = dtFromLocalHost.Rows[j]["location"].ToString();
                lrow["Size1"] = dtFromLocalHost.Rows[j]["Size1"].ToString();
                lrow["Qty"] = Convert.ToInt32(dtFromLocalHost.Rows[j]["size_qty"].ToString());
                ldt.Rows.Add(lrow);
            }

            DataTable adt = new DataTable();
            adt.Columns.Add("ID", typeof(string));
            adt.Columns.Add("ScanTime", typeof(string));
            adt.Columns.Add("Buyer_Item", typeof(string));
            adt.Columns.Add("color_code", typeof(string));
            adt.Columns.Add("location", typeof(string));
            adt.Columns.Add("Size1", typeof(string));
            adt.Columns.Add("eQty", typeof(int));
            adt.Columns.Add("lQty", typeof(int));
            adt.Columns.Add("Difference", typeof(int));
            
            for (int i=0;i< edt.Rows.Count; i++)
            {
                for(int j = 0; j < ldt.Rows.Count; j++)
                {
                    //相同的ID   数量合并到一起
                    if(edt.Rows[i]["ID"].ToString() == ldt.Rows[j]["ID"].ToString())
                    {
                        DataRow arow = adt.NewRow();
                        arow["ID"] = ldt.Rows[j]["ID"].ToString();
                        arow["ScanTime"] = ldt.Rows[j]["ScanTime"].ToString();
                        arow["Buyer_Item"] = ldt.Rows[j]["Buyer_Item"].ToString();
                        arow["color_code"] = ldt.Rows[j]["color_code"].ToString();
                        arow["location"] = ldt.Rows[j]["location"].ToString();
                        arow["Size1"] = ldt.Rows[j]["Size1"].ToString();
                        arow["eQty"] = Convert.ToInt32( edt.Rows[i]["Qty"].ToString());
                        arow["lQty"] = Convert.ToInt32(ldt.Rows[j]["Qty"].ToString());
                        arow["Difference"] = Convert.ToInt32( arow["eQty"].ToString()) - Convert.ToInt32( arow["lQty"].ToString());

                        adt.Rows.Add(arow);
                    }
                }
               
            }
            // adt 与 edt不相同的ID  写入bdt
            DataTable bdt = new DataTable();
            bdt.Columns.Add("ID", typeof(string));
            bdt.Columns.Add("ScanTime", typeof(string));
            bdt.Columns.Add("Buyer_Item", typeof(string));
            bdt.Columns.Add("color_code", typeof(string));
            bdt.Columns.Add("location", typeof(string));
            bdt.Columns.Add("Size1", typeof(string));
            bdt.Columns.Add("eQty", typeof(int));
            bdt.Columns.Add("lQty", typeof(int));
            bdt.Columns.Add("Difference", typeof(int));

            bool isEques = false;
            for (int i = 0; i < edt.Rows.Count; i++)
            {
                for( int j=0;j< adt.Rows.Count; j++)
                {
                    if (edt.Rows[i]["ID"].ToString() == adt.Rows[j]["ID"].ToString())
                    {
                        isEques = true;
                        break;
                    }
                    else
                    {
                        isEques = false;
                    }
                }
                if (!isEques)
                {
                    DataRow brow = bdt.NewRow();
                    brow["ID"] = edt.Rows[i]["ID"].ToString();
                    brow["ScanTime"] = edt.Rows[i]["DeliveryDate"].ToString();
                    brow["Buyer_Item"] = edt.Rows[i]["styleId"].ToString();
                    brow["color_code"] = edt.Rows[i]["colorId"].ToString();
                    brow["location"] = edt.Rows[i]["lineName"].ToString();
                    brow["Size1"] = edt.Rows[i]["sizeName"].ToString();
                    brow["eQty"] = Convert.ToInt32( edt.Rows[i]["Qty"].ToString());
                    brow["lQty"] = 0;
                    brow["Difference"] = Convert.ToInt32(brow["eQty"].ToString()) - Convert.ToInt32(brow["lQty"].ToString());
                    bdt.Rows.Add(brow);
                }
            }

            // adt 与 ldt不相同的ID  写入bdt 
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                for (int j = 0; j < adt.Rows.Count; j++)
                {
                    if (ldt.Rows[i]["ID"].ToString() == adt.Rows[j]["ID"].ToString())
                    {
                        isEques = true;
                        break;
                    }
                    else
                    {
                        isEques = false;
                    }
                }
                if (!isEques)
                {
                    DataRow brow = bdt.NewRow();
                    brow["ID"] = ldt.Rows[i]["ID"].ToString();
                    brow["ScanTime"] = ldt.Rows[i]["ScanTime"].ToString();
                    brow["Buyer_Item"] = ldt.Rows[i]["Buyer_Item"].ToString();
                    brow["color_code"] = ldt.Rows[i]["color_code"].ToString();
                    brow["location"] = ldt.Rows[i]["location"].ToString();
                    brow["Size1"] = ldt.Rows[i]["Size1"].ToString();
                    brow["eQty"] = 0;
                    brow["lQty"] = Convert.ToInt32(ldt.Rows[i]["Qty"].ToString());
                    brow["Difference"] = Convert.ToInt32(brow["eQty"].ToString()) - Convert.ToInt32(brow["lQty"].ToString());
                    bdt.Rows.Add(brow);
                }
            }

            for(int i = 0; i < bdt.Rows.Count; i++)
            {
                DataRow arow = adt.NewRow();
                arow["ID"] = bdt.Rows[i]["ID"].ToString();
                arow["ScanTime"] = bdt.Rows[i]["ScanTime"].ToString();
                arow["Buyer_Item"] = bdt.Rows[i]["Buyer_Item"].ToString();
                arow["color_code"] = bdt.Rows[i]["color_code"].ToString();
                arow["location"] = bdt.Rows[i]["location"].ToString();
                arow["Size1"] = bdt.Rows[i]["Size1"].ToString();
                arow["eQty"] = Convert.ToInt32(bdt.Rows[i]["eQty"].ToString());
                arow["lQty"] = Convert.ToInt32(bdt.Rows[i]["lQty"].ToString());
                arow["Difference"] = Convert.ToInt32(bdt.Rows[i]["Difference"].ToString());
                adt.Rows.Add(arow);

               
            }
           
            adt.DefaultView.Sort = "ScanTime";
            this.dtnoEques = adt;
           
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //  this.gbLoad.Text = "加载完成";
            barstr.str = "加载完成";
            barstr.step = 100;
            barstr.maxstep = 100;
            // FrmPacklist.UpdateUI(barstr);
            UpdateUIDelegate(barstr);
            this.dgvExcels.DataSource = "";
            if (this.dtFormExcel ==  null || this.dtFormExcel.Rows.Count <= 0)
            {
                return;
            }
            if (this.dtFromLocalHost == null || this.dtFromLocalHost.Rows.Count <= 0)
            {
                return;
            }
            if (this.dtnoEques.Rows == null || this.dtnoEques.Rows.Count <= 0)
            {
                return;
            }
           
            this.dgvExcels.DataSource = this.dtFromWriteExcel;
            this.dgvLocalHostDB.DataSource = "";
            this.dgvLocalHostDB.DataSource = this.dtFromLocalHost;
            this.dgvCompareResult.DataSource = "";
            this.dgvCompareResult.DataSource = this.dtnoEques;
            this.dgvCompareResult.EnableHeadersVisualStyles = false;
            for (int i = 0; i < dgvCompareResult.Rows.Count; i++)
            {
                if (this.dgvCompareResult.Rows[i].Cells[8].Value.ToString() != null && this.dgvCompareResult.Rows[i].Cells[8].Value.ToString() != "0")
                {
                    //行背景色
                    // this.dgvCompareResult.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");

                    //单元格背景色
                    this.dgvCompareResult.Rows[i].Cells[8].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                }
            }
            this.dgvExcels.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            this.dgvLocalHostDB.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            //this.dgvCompareResult.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            Thread.Sleep(1000);
            this.labWorking.Text = "任务进度：";
            barstr.str = "任务进度：";
            barstr.step = 0;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);
            changdgvExcelsHeaderText();
            changdgvLocalHostDBHeaderText();
            changdgvCompareResultHeaderText();
            this.dgvCompareResult.DataSource = "";
            this.dgvCompareResult.DataSource = this.dtnoEques;
            this.dgvCompareResult.EnableHeadersVisualStyles = false;
            for (int i = 0; i < dgvCompareResult.Rows.Count; i++)
            {
                if (this.dgvCompareResult.Rows[i].Cells[8].Value.ToString() != null && this.dgvCompareResult.Rows[i].Cells[8].Value.ToString() != "0")
                {
                    //单元格背景色
                    this.dgvCompareResult.Rows[i].Cells[8].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                }
            }
        }

        public void changdgvExcelsHeaderText()
        {
            this.dgvExcels.Columns["lineName"].HeaderText = "线别";
            this.dgvExcels.Columns["deliveryDate"].HeaderText = "送货时间";
            this.dgvExcels.Columns["styleId"].HeaderText = "款号";
            this.dgvExcels.Columns["colorId"].HeaderText = "颜色";
            this.dgvExcels.Columns["sizeName"].HeaderText = "尺码";
            this.dgvExcels.Columns["qty"].HeaderText = "数量";
            this.dgvExcels.Columns["deliveryDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
        }
        public void changdgvLocalHostDBHeaderText()
        { 
            this.dgvLocalHostDB.Columns["ScanTime"].HeaderText = "收货扫描日期";
            this.dgvLocalHostDB.Columns["Buyer_Item"].HeaderText = "款号";
            this.dgvLocalHostDB.Columns["color_code"].HeaderText = "颜色";
            this.dgvLocalHostDB.Columns["location"].HeaderText = "线别";
            this.dgvLocalHostDB.Columns["Size1"].HeaderText = "尺码";
            this.dgvLocalHostDB.Columns["size_qty"].HeaderText = "收货扫描数量";  
            this.dgvLocalHostDB.Columns["Scantime"].DefaultCellStyle.Format = "yyyy-MM-dd";


        }
        public void changdgvCompareResultHeaderText()
        {
            this.dgvCompareResult.Columns["ID"].Visible = false;
            this.dgvCompareResult.Columns["ID"].HeaderText = "ID";
            this.dgvCompareResult.Columns["ScanTime"].HeaderText = "收货扫描日期";
            this.dgvCompareResult.Columns["Buyer_Item"].HeaderText = "款号";
            this.dgvCompareResult.Columns["color_code"].HeaderText = "颜色";
            this.dgvCompareResult.Columns["location"].HeaderText = "线别";
            this.dgvCompareResult.Columns["eQty"].HeaderText = "送货清单数量";
            this.dgvCompareResult.Columns["lQty"].HeaderText = "收货扫描数量";
            this.dgvCompareResult.Columns["Difference"].HeaderText = "差异数量";
            this.dgvCompareResult.Columns["ScanTime"].DefaultCellStyle.Format = "yyyy-MM-dd";

        }

        private void dgvExcels_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void clboxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.location.Clear();
            for(int i = 0; i<  this.clboxLocation.Items.Count; i++)
            {
                if (this.clboxLocation.GetItemChecked(i))
                {
                    this.location.Add(this.clboxLocation.Items[i].ToString());
                }
            }
          
        }

        private void dtpStarTime_ValueChanged(object sender, EventArgs e)
        {
            this.btnCompare.Enabled = true;
        }

        private void dtpStopTime_ValueChanged(object sender, EventArgs e)
        {
            this.btnCompare.Enabled = true;
        }

        private void dgvCompareResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvLocalHostDB_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
            Clipboard.SetDataObject(this.dgvCompareResult.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dgvCompareResult.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            ImproExcel();
        }
        public void ImproExcel()
        {
            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";
            //   sdfExport.ShowDialog();
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            NPOIExcelDeliveryCompare NPOIexcel = new NPOIExcelDeliveryCompare();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(this.dgvCompareResult);

            // DataTable dt = (StyleCodeInfodataGridView.DataSource as DataTable);
            NPOIexcel.ExcelWrite(filename, tabl);//excelhelper写出
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
        private void dgvCompareResult_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (this.dgvCompareResult.Rows[e.RowIndex].Selected == false)
                    {
                        this.dgvCompareResult.ClearSelection();
                        this.dgvCompareResult.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (this.dgvCompareResult.SelectedRows.Count == 1)
                    {
                        this.dgvCompareResult.CurrentCell = this.dgvCompareResult.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
