using BLL;
using COMMON;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmTNFScan : Form
    {
        public   string filename = ConfigurationManager.ConnectionStrings["FilePath"].ConnectionString;
        public   string sheetname = ConfigurationManager.ConnectionStrings["SheetName"].ConnectionString;
       // public string subinv = ConfigurationManager.ConnectionStrings["SubinvName"].ConnectionString;
      //  public string org = ConfigurationManager.ConnectionStrings["OrgName"].ConnectionString;
        // private String filename = @"C:\Scan\scan.xlsx";
        bool isSheet = false;
        public string subinv="";
        public string org = "";

        private loadScanManager lScan = new loadScanManager();
        private DataTable table = new DataTable();
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
        private static FrmTNFScan frm;
        public xiaomingCommom myCommon = new xiaomingCommom();

        public FrmTNFScan()
        {
            InitializeComponent();
            if (this.org == "")
            {
                this.gbLoad.Visible = true;
                this.gbLoad.Enabled = false;
            }
            else
            {
                this.gbLoad.Visible = false;
                this.gbLoad.Enabled = false;
            }
            this.dgvExcels.DoubleBufferedDataGirdView(true);
           
        }
        public static FrmTNFScan GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmTNFScan();
            }
            return frm;
        }

        private void btnSelected_Click(object sender, EventArgs e)
        {
            this.cbSelectSheet.Items.Clear();
            this.dgvExcels.DataSource = null;
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
            this.btnLoadExcel.Enabled = true;
        }

        private void cbSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnLoadExcel.Enabled = true;
            this.txtSheetHead.Text = "0";
            if (this.cbSelectSheet.SelectedIndex >= 0)
            {
                this.isSheet = true;
            }
        }

        private void txtSheetHead_TextChanged(object sender, EventArgs e)
        {
            this.btnLoadExcel.Enabled = true;
        }

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.pgBar.Visible = true;
            this.pgBar.Value = 1;
            this.dgvExcels.DataSource = null;
            Application.DoEvents();
            loadExcel();
            this.pgBar.Value = 100;
            Cursor = Cursors.Default;
            this.btnLoadExcel.Enabled = false;
            this.btnUpload.Enabled = true;
        }
 

        private void butDelDoubleRows_Click(object sender, EventArgs e)
        {
            int delRows = lScan.delDoubleRows();
            MessageBox.Show("共删除 " + delRows.ToString() + " 行数据");
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


        private void loadExcel()
        {
            
            if (this.org == "SAA")
            {
               
                this.rbOrgSAA.Checked = true;

            }
            else if (this.org == "TOP")
            {
                this.rbOrgTOP.Checked = true;
            }
            else
            {
               // this.gbLoad.Visible = true;
                this.btnLoadExcel.Enabled = true;
                MessageBox.Show("请先设置默认厂区或选择厂区"); 
                return;
            }

            if (this.subinv == "")
            {
                //this.gbLoad.Visible = true;
                this.subinv = this.cbSubinv.Text.Trim().ToUpper();
            }
            else
            {
                bool isSubinv = false;
                for (int i = 0; i < this.cbSubinv.Items.Count; i++)
                {

                    if (this.subinv == this.cbSubinv.Items[i].ToString())
                    {
                        isSubinv = true;
                        this.btnLoadExcel.Enabled = true;
                        break;
                    }

                }

                if (!isSubinv)
                {
                    MessageBox.Show( "配置的仓库代号"+ this.subinv +"不正确! 请修改后重新运行本程式");
                    return;
                }


              //  this.cbSubinv.Items.Clear();
              //  this.cbSubinv.Items.Add(this.subinv);
                this.cbSubinv.SelectedItem = this.subinv;
            }
           

            this.dgvExcels.DataSource = null;
            if(!File.Exists(filename))
            {
                this.gbLoad.Visible = true;
                if (this.txtSelectedFilePath.Text.Length <= 0)
                {

                    MessageBox.Show("请选择文件！");
                    Cursor = Cursors.Default;
                    this.btnLoadExcel.Enabled = true;
                    this.txtSelectedFilePath.Enabled = true;
                    this.txtSelectedFilePath.Text = "";
                    return;
                }
            }
            else
            {
                this.txtSelectedFilePath.Enabled = false;
                this.txtSelectedFilePath.Text = filename;
                
            }
           
            string[] sheetnames = xiaomingCommom.getExcelSheetSum(filename);
            for(int i = 0; i < sheetnames.Length; i++)
            {
                if (sheetnames[i]== this.sheetname)
                {
                   // this.gbLoad.Visible = true;
                    isSheet = true;
                    this.cbSelectSheet.Enabled = false;
                    this.cbSelectSheet.Text = this.sheetname;
                    this.cbSelectSheet.Items.Clear();
                    this.cbSelectSheet.Items.Add(sheetnames[i]);//添加表名     
                    this.cbSelectSheet.SelectedIndex = i;
                    break;
                }
            }
           // string sheetname ="";
            if (!isSheet)
            {
                this.gbLoad.Visible = true;

                this.cbSelectSheet.Enabled = true;
                this.cbSelectSheet.Text = "";
                this.cbSelectSheet.Items.Clear();
                for (int i = 0; i < sheetnames.Length; i++)
                {
                     this.cbSelectSheet.Items.Add(sheetnames[i]);//添加表名                    
                }

                if (this.cbSelectSheet.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择表名！");
                    this.cbSelectSheet.DroppedDown = true;
                    this.btnLoadExcel.Enabled = true; 
                    return; 
                }
            }

            this.sheetname = this.cbSelectSheet.SelectedItem.ToString();
            if(this.sheetname == "")
            {
                this.gbLoad.Visible = true;
                MessageBox.Show("请选择表名！");
                this.cbSelectSheet.DroppedDown = true;
                this.btnLoadExcel.Enabled = true;
                isSheet = false;
                Cursor = Cursors.Default;
                    return;

            }

           

          
        

            int headno = 0; 
            Regex reg = new Regex("^[0-9]+$"); //正则表达式 检测是否数字
            Match ma = reg.Match(txtSheetHead.Text.ToString());
            if (!ma.Success)
            {
                this.gbLoad.Visible = true;
                headno = 0;
                txtSheetHead.Text = headno.ToString();
            }

            if (Convert.ToInt32(txtSheetHead.Text) < 0 || txtSheetHead.Text == "")
            {
                headno = 0;
            }
            headno = Convert.ToInt32(txtSheetHead.Text);

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
            this.sheeTnameStr = this.sheetname;
            this.headNoStr = headno;

           

        }

        public void UpdataUIStatus(bar barstr)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (bar s)
                {
                    this.pgBar.Maximum = s.maxstep;
                    this.pgBar.Value = s.step;
                    this.gbLoad.Text = s.str + "    " + s.step.ToString() + "/" + s.maxstep.ToString();
                }), barstr);
            }
            else
            {
                this.pgBar.Minimum = 0;
                this.pgBar.Maximum = barstr.maxstep;
                this.pgBar.Value = barstr.step;
                this.gbLoad.Text = barstr.str + "    " + barstr.step.ToString() + "/" + barstr.maxstep.ToString();
            }
        }

        //完成任务时需要调用
        private void Accomplish()
        {
            if (table != null)
            {
                this.dgvExcels.DataSource = table;
                changHeaderText();
                Cursor = Cursors.Default;
                this.dgvExcels.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                MessageBox.Show("加载完成");
            }
            TaskCallBack -= Accomplish; //取消侦听注册事件，避免多次侦听
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            barstr.str = "加载完成";
            barstr.step = 100;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);
            this.dgvExcels.DataSource = "";
            this.dgvExcels.DataSource = this.table;
            Thread.Sleep(1000);
            this.gbLoad.Text = "导入条件";
            barstr.str = "导入条件";
            barstr.step = 0;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);
            this.btnLoadExcel.Enabled = true;
            this.btnUpload.Enabled = true;


        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            barstr.str = "正在加载EXCEL...";
            barstr.step = 50;
            barstr.maxstep = 100;
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
           
            if (this.subinv.Length <= 0)
            {
                 
                return;
            }
            
            this.table = lScan.ExcelRead(this.fileNameStr, this.sheeTnameStr, this.headNoStr, this.org,  this.subinv);
            if(this.table is null)
            {
                MessageBox.Show("EXCEL内容有误，请查证后再上传...", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void changHeaderText()
        {
            this.dgvExcels.Columns["tagNumber"].HeaderText = "外箱条码号";
            this.dgvExcels.Columns["ScanTime"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            this.dgvExcels.Columns["ScanTime"].HeaderText = "扫描时间";
            this.dgvExcels.Columns["Kg"].HeaderText = "重量";
            this.dgvExcels.Columns["Subinv"].HeaderText = "仓库代码";
            this.dgvExcels.Columns["Con_no"].HeaderText = "外箱号";
            this.dgvExcels.Columns["Location"].HeaderText = "储位";
            this.dgvExcels.Columns["Org"].HeaderText = "厂别";
            this.dgvExcels.Columns["Cust_Id"].HeaderText = "客户";
        }

        private void FrmTNFScan_Resize(object sender, EventArgs e)
        {
            //1188, 834 
            if (this.Width <= 1188)
            {
                this.Width = 1188;
            }
            if (this.Height <= 834)
            {
                this.Height = 834;
            }

            this.gbLoad.Width = this.Width - 20;
            this.bgExcel.Width = this.gbLoad.Width;
            this.bgExcel.Height = this.Height - this.gbLoad.Height - 50;
        }

        private void rbOrgSAA_CheckedChanged(object sender, EventArgs e)
        {
            this.cbSubinv.Items.Clear();
            DataTable subinvs = new DataTable();
            if (rbOrgSAA.Checked)
            {
                subinvs = lScan.getSubinvByOrg("SAA");
                this.org = "SAA";
            }
            if (subinvs.Rows.Count <= 0)

            {
                this.org = "";
                return;
            }
            else
            {
                
                for (int i = 0; i < subinvs.Rows.Count; i++)
                {
                    this.cbSubinv.Items.Add(subinvs.Rows[i]["subinv"].ToString());

                }
                
            }
            if (this.subinv == "")
            {
                this.gbLoad.Visible = true;
                this.cbSubinv.DroppedDown = true;
            }
           

        }

        private void rbOrgTOP_CheckedChanged(object sender, EventArgs e)
        {
            this.cbSubinv.Items.Clear();
            DataTable subinvs = new DataTable();
            if (rbOrgTOP.Checked)
            {
                subinvs =  lScan.getSubinvByOrg("TOP");
                this.org = "SAA";
            }
            if (subinvs.Rows.Count <= 0)
            {
                this.org = "";
                return;
            }
            else
            {
                for (int i = 0; i < subinvs.Rows.Count; i++)
                {
                    this.cbSubinv.Items.Add(subinvs.Rows[i]["subinv"].ToString());
                }

            }
            this.cbSubinv.DroppedDown = true;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (this.dgvExcels.RowCount == 0)
            {
                MessageBox.Show("请先加载excel数据！");
                Cursor = Cursors.Default;
                return;
            }




            List<string> result = lScan.writeInvsToDb(table,this.org,this.subinv);
            Cursor = Cursors.Default;
            if (result[1] == "-1")
            {
                MessageBox.Show("行:" + result[0].ToString() + " 数据有问题，请查证后再上传", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else if (result[1]== "-2") {
                MessageBox.Show("行:" + result[0].ToString() + "  外箱条码有错误，请查证后再上传", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(result[1] == "-3" )
            {
                MessageBox.Show("行:" + result[0].ToString() + " 保存失败,请检查EXCEL文档", "错误",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
            else if (result[1] == "-4")
            {
                MessageBox.Show("没有储位:" + result[0].ToString() + "  保存失败,请检查EXCEL文档", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("共保存:" + result[1].ToString() + " 行。 保存成功", "提示");
            }
           
            this.btnUpload.Enabled = false;
        }
       

        private void cbSubinv_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnLoadExcel.Enabled = true;
            this.subinv = this.cbSubinv.SelectedItem.ToString();
        }

        private void FrmTNFScan_Load(object sender, EventArgs e)
        {

        }
    }
}
