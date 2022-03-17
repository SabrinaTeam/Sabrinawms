using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MODEL;

namespace WinForm
{
    public partial class ProductionStatus2 : Form
    {
        private static ProductionStatus2 frm;
       // ProductionStatusManager psm = new ProductionStatusManager();
        public DataGridView selectDgv = null;
        public int hiedcolumnindex = -1; //是否选中外面
        public int LastWeeks = 2;
        DataGridView selecteddgv = null;
        public ProductionStatus2()
        {
            InitializeComponent();
            this.dgvProductionStatus.DoubleBufferedDataGirdView(true);        }
        public static ProductionStatus2 GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new ProductionStatus2();
            }
            return frm;
        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dgvProductionStatus.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dgvProductionStatus.GetClipboardContent());
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
            tabl = GetDgvToTable(this.dgvProductionStatus);

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

        private void butSearch_Click(object sender, EventArgs e)
        {
            this.getStatus();

        }
        public void getStatus()
        {
            string mynumber = this.txtMyNumber.Text.Trim();
            if (mynumber != "")
            {
                mynumber = mynumber.ToUpper();
            }
            string buyid = this.txtBuyID.Text.Trim();
            if (buyid != "")
            {
                buyid = buyid.ToUpper();
            }
            string season = this.txtSeason.Text.Trim();
            if (season != "")
            {
                season = season.ToUpper();
            }
            bool ckdate = this.cbDate.Checked;
            string stardate = this.dtpStartDate.Value.ToString("yyyy-MM-dd");
            string enddate = this.dtpStopDate.Value.ToString("yyyy-MM-dd");
            
           

            int page = 1; 



            int serviceID = this.cbOperationsCenter.SelectedIndex;

            if (serviceID <= -1)
            {
                MessageBox.Show("请先选择营运中心，谢谢!");
                return;
            }
            string serviceName = this.cbOperationsCenter.SelectedItem.ToString(); ;

            this.butSearch.Enabled = false;
            Cursor = Cursors.WaitCursor;
            ProductionStatusSearch2 pss = new ProductionStatusSearch2();
            pss.mynumber = mynumber;
            pss.buyid = buyid;
            pss.season = season;
            
            pss.stardate = stardate;
            pss.enddate = enddate;
            pss.checkedDate = ckdate;
            pss.page = page;

            /*
            if (rbWIP.Checked)
            {
                //   this.dgvProductionStatus.DataSource = psm.getProductionStatus(pss, serviceName);
            }
            else if (rbMakeStatus.Checked)
            {
                List<DataTable> ldt = new List<DataTable>();
                int pages = 0;
                Tuple<List<DataTable>, int> tup = new Tuple<List<DataTable>, int>(ldt, pages);
                tup = psm.getProductionStatus(pss, serviceName);
                ldt = tup.Item1;
                pages = tup.Item2; ;
                //  tup = psm.getProductionStatus(pss, serviceName);
                //  List<DataTable> ldt = psm.getProductionStatus(pss, serviceName);
                this.page.Text = page + " / " + pages.ToString();
                this.txtPage.Text = page.ToString();
                if (ldt != null && ldt.Count == 3)
                {
                    this.dgvProductionStatus.DataSource = ldt[0];
                    this.dataGridView1.DataSource = ldt[1];
                    this.dataGridView2.DataSource = ldt[2];
                }
            }
            */
            this.butSearch.Enabled = true;
            Cursor = Cursors.Default;
        }
    }
}
