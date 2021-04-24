using BLL;
using COMMON;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmPoTradingComanyPO : Form
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


        private static FrmPoTradingComanyPO frm;
        private tradingComanyPOManager BgtnPO = new tradingComanyPOManager();
        private DataTable table = new DataTable();
        private String filename = null;
        public xiaomingCommom myCommon = new xiaomingCommom();
       
        public FrmPoTradingComanyPO()
        {
            InitializeComponent();
            this.dgvExcels.DoubleBufferedDataGirdView(true);
        }
        public static FrmPoTradingComanyPO GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmPoTradingComanyPO();
            }
            return frm;
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            //查询是否已有此文件//GUID报错  已有此文件
            if (this.dgvExcels.RowCount == 0)
            {
                MessageBox.Show("请先加载excel数据！");
                Cursor = Cursors.Default;
                return;
            }
            string Tpo = "";
            for (int i = 0; i < this.dgvExcels.Rows.Count; i++)
            {
                if (Tpo == "")
                {
                    Tpo = Convert.ToString(this.dgvExcels.Rows[i].Cells["GTN_PO"].Value);
                }
                else
                {
                    continue;
                }
            }
           /*
            if (BgtnPO.isHaveByTradingComanyPO(Tpo))
            {
                MessageBox.Show("此表已保存完成！");
                Cursor = Cursors.Default;
                return;
            }
           */
            BgtnPO.writeGtnsToDb(table);
            Cursor = Cursors.Default;
            MessageBox.Show("保存成功", "提示");
            this.btnSave.Enabled = false;
        }

 
        private void butSelected_Click(object sender, EventArgs e)
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

        private void butLoadExcel_Click(object sender, EventArgs e)
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
            this.btnSave.Enabled = true;
           // this.pgBar.Visible = false;

        }
       
        
 
        private void loadExcel()
        {
            this.dgvExcels.DataSource = null;
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
             
            string sheetname = this.cbSelectSheet.SelectedItem.ToString();

                        //  bool yesno = false;//第一行是否数据列名
                        ////  if (radYES.Checked)
                        //  {
                        //      yesno = true;
                        //  }
                        //  else if (radNO.Checked)
                        //  {
                        //      yesno = false;
                        //  }
                        int headno = 0;
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

         /*
            table = BgtnPO.ExcelRead(filename, sheetname, headno);
          
            if (table != null)
            {
                this.dgvExcels.DataSource = table;
                changHeaderText();
                Cursor = Cursors.Default;
                this.dgvExcels.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                MessageBox.Show("加载完成");
            }
         */

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
            // Color controlColor = Control.DefaultBackColor;
            //  SetControlColor(controlColor);
            //还可以进行其他的一些完任务完成之后的逻辑处理
            //  MessageBox.Show("接转完成OK");
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

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            barstr.str = "正在加载EXCEL..." ;
            barstr.step = 50;
            barstr.maxstep = 100;
            // FrmPacklist.UpdateUI(barstr);
            UpdateUIDelegate(barstr);
            if(this.fileNameStr == "")
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

            this.table = BgtnPO.ExcelRead(this.fileNameStr, this.sheeTnameStr, this.headNoStr);
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
            this.dgvExcels.DataSource = this.table;
            Thread.Sleep(1000);

            this.gbLoad.Text = "导入条件";
            barstr.str = "导入条件";
            barstr.step = 0;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);


        }

        public void changHeaderText()
        {
            this.dgvExcels.Columns["id"].Visible = false;
            this.dgvExcels.Columns["PO"].HeaderText = "PO#";
            this.dgvExcels.Columns["GTN_PO"].HeaderText = "Trading Comany PO";
            this.dgvExcels.Columns["create_pc"].HeaderText = "创建电脑名";
            this.dgvExcels.Columns["update_date"].HeaderText = "上传时间";
            this.dgvExcels.Columns["fCreate_Date"].HeaderText = "PackLister创建时间";
            this.dgvExcels.Columns["fIssue_Date"].HeaderText = "Issue_Date";
            this.dgvExcels.Columns["fOrder_Status"].HeaderText = "Order_Status";
            this.dgvExcels.Columns["fOrder_Total_Qty"].HeaderText = "Order_Total_Qty";
            this.dgvExcels.Columns["fInvoiced_Item_Qty"].HeaderText = "Invoiced_Item_Qty";

            this.dgvExcels.Columns["update_date"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            this.dgvExcels.Columns["fCreate_Date"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            this.dgvExcels.Columns["fIssue_Date"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
        }

        private void cbSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnLoadExcel.Enabled = true;
            this.txtSheetHead.Text = "3";
        }

        private void txtSheetHead_TextChanged(object sender, EventArgs e)
        {
            this.btnLoadExcel.Enabled = true;
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

        private void FrmPoTradingComanyPO_Resize(object sender, EventArgs e)
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

        private void butDelDoubleRows_Click(object sender, EventArgs e)
        {
          int delRows =   BgtnPO.delDoubleRows();
            MessageBox.Show("共删除 "+delRows.ToString()+" 行数据");
        }
    }
}
