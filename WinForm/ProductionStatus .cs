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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class ProductionStatus : Form
    {
        private static ProductionStatus frm;
        ProductionStatusManager psm = new ProductionStatusManager();
        public DataGridView selectDgv = null;
        public int hiedcolumnindex = -1; //是否选中外面
        public int LastWeeks = 2;
        DataGridView selecteddgv = null;
        public ProductionStatus()
        {
            InitializeComponent();
            this.dgvProductionStatus.DoubleBufferedDataGirdView(true);
            this.dataGridView1.DoubleBufferedDataGirdView(true);
            this.dataGridView2.DoubleBufferedDataGirdView(true);

        }
        public static ProductionStatus GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new ProductionStatus();
            }
            return frm;
        }

        private void ProductionStatus_Load(object sender, EventArgs e)
        {
            this.cbDateType.SelectedIndex = 0;
            this.cbDate.Checked = false;
            //  this.dtpStartDate.Value = DateTime.Now.addw
            betweenDate bdate  = this.getDate(LastWeeks, 0);
            this.dtpStartDate.Value = bdate.start_time;
            this.dtpStopDate.Value = bdate.end_time;
            this.rbWIP.Checked = true;

            this.splitContainer1.SplitterDistance = Convert.ToInt32( this.splitContainer1.Height * 0.8);


        }

        private void ProductionStatus_Resize(object sender, EventArgs e)
        {
            this.gbProductionStatus.Width = this.Width - 20;
            this.gbProductionStatus.Height = this.Height - 115;
            this.gbSeach.Width = this.gbProductionStatus.Width;
            this.cbDate.Checked = false;

        }

        private betweenDate  getDate(int weeks,int type)
        {
            DateTime currentTime = DateTime.Now;            
            int week = Convert.ToInt32(currentTime.DayOfWeek);
            week = week == 0 ? 7 : week;
            betweenDate bdate = new betweenDate();
            
            switch (type)
            {
                case 0:
                    //获取上周星期一/星期天
                    bdate.start_time = currentTime.AddDays(1 - week - 7 * weeks);//上周星期一
                    bdate.end_time = currentTime.AddDays(7 - week - 7 * weeks);//上周星期天  -1 星期六                                                                                            //   Console.WriteLine("上周:" + start_time_last_week + "|" + end_time_last_week);
                    break;             
                case 1:
                    //获取本周星期一/星期天
                    bdate.start_time = currentTime.AddDays(1 - week);//本周星期一
                    bdate.end_time = currentTime.AddDays(7 - week);//本周星期天 
                    //  Console.WriteLine("本周:" + start_time_current_week + "|" + end_time_current_week);
                    break;
               
                case 2:
                    //3-5;6-8;9-11;12-2  startQuarter
                    bdate.start_time = currentTime.AddMonths(0 - (currentTime.Month % 3)).AddDays(1 - currentTime.Day);  //本季度初
                    bdate.end_time = bdate.start_time.AddMonths(3).AddDays(-1);  //本季度末
                    break;
                case 3:
                    //1-3;4-6;7-9;10-12  startQuarter
                    bdate.start_time = currentTime.AddMonths(0 - (currentTime.Month - 1) % 3).AddDays(1 - currentTime.Day);  //本季度初
                    bdate.end_time = bdate.start_time.AddMonths(3).AddDays(-1);  //本季度末 
                    break;
                default:
                    bdate.start_time = currentTime;
                    bdate.end_time = currentTime;
                    break;
            }

            return bdate;

        }

        private void butLastWeek_Click(object sender, EventArgs e)
        {
            this.LastWeeks = LastWeeks + 1;
            betweenDate bdate = this.getDate(LastWeeks , 0);
            this.dtpStartDate.Value = bdate.start_time;
            this.dtpStopDate.Value = bdate.end_time;
            this.cbDate.Checked = true;
        }

        private void butNextWeek_Click(object sender, EventArgs e)
        {
            this.LastWeeks = LastWeeks - 1;
            betweenDate bdate = this.getDate(LastWeeks, 0);
            this.dtpStartDate.Value = bdate.start_time;
            this.dtpStopDate.Value = bdate.end_time;
            this.cbDate.Checked = true;
        }

        private void butCurrentWeek_Click(object sender, EventArgs e)
        {
            this.LastWeeks = 0;
            betweenDate bdate = this.getDate(LastWeeks, 1);
            this.dtpStartDate.Value = bdate.start_time;
            this.dtpStopDate.Value = bdate.end_time;
            this.cbDate.Checked = true;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            this.getStatus();
        }

        private void cbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbDate.Checked)
            {
                this.dtpStartDate.Enabled = true;
                this.dtpStopDate.Enabled = true;
            }else
            {
                this.dtpStartDate.Enabled = false;
                this.dtpStopDate.Enabled = false;
            }
        }

        private void cbDateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = true;
        }

        private void dgvProductionStatus_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void txtMyNumber_TextChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = false;
        }

        private void txtSeason_TextChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = false;
        }

        private void txtBuyID_TextChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = false;
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = true;
        }

        private void dtpStopDate_ValueChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = true;
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
        
        private void dgvCompareResult_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (this.dgvProductionStatus.Rows[e.RowIndex].Selected == false)
                    {
                        this.dgvProductionStatus.ClearSelection();
                        this.dgvProductionStatus.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (this.dgvProductionStatus.SelectedRows.Count == 1)
                    {
                        this.dgvProductionStatus.CurrentCell = this.dgvProductionStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
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

        private void dgvProductionStatus_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e); 
        }

        public void tomenuRight(DataGridView dgv, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgv.Rows[e.RowIndex].Selected == false)
                    {
                        dgv.ClearSelection();
                        dgv.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgv.SelectedRows.Count == 1)
                    {
                        dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    }
                    //弹出操作菜单
                    MenuRight.Show(MousePosition.X, MousePosition.Y);
                    // MessageBox.Show("点右键了");
                }

                else if (e.ColumnIndex >= 0)
                {
                    this.hiedcolumnindex = e.ColumnIndex;
                    //  MenuRight.Show(MousePosition.X, MousePosition.Y);

                }

            }
             
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            label7.Top = this.dataGridView1.Top;
            label7.Left = this.dataGridView1.Width-100;

            label8.Top = label7.Top;
            label8.Left = this.dataGridView2.Width - 100;
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            label7.Top = this.dataGridView1.Top;
            label7.Left = this.dataGridView1.Width - 100;

            label8.Top = label7.Top;
            label8.Left = this.dataGridView2.Width - 100;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        private void butPrevious_Click(object sender, EventArgs e)
        {
            string page = this.txtPage.Text.ToString();
            if (page.Length <= 0)
            {
                page = "0";
            }
            if (Convert.ToInt32(page) <= 0)
            {
                return;
            }
            this.txtPage.Text = Convert.ToString(Convert.ToInt32(page) - 1);
            this.getStatus();
        }

        private void butNext_Click(object sender, EventArgs e)
        {
            string page = this.txtPage.Text.ToString();
            if (page.Length <= 0)
            {
                page = "0";
            }
            this.txtPage.Text = Convert.ToString(Convert.ToInt32(page) + 1);
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
            int datetype = this.cbDateType.SelectedIndex;
            bool ckdate = this.cbDate.Checked;
            string stardate = this.dtpStartDate.Value.ToString("yyyy-MM-dd");
            string enddate = this.dtpStopDate.Value.ToString("yyyy-MM-dd");
            string pagestr = this.txtPage.Text.Trim();
            if (pagestr.Length <= 0)
            {
                pagestr = "0";
            }

            int page = 1;
            if (Regex.IsMatch(pagestr, @"^[0-9]*$"))
            {
                page = Convert.ToInt32(pagestr);
            }
            else
            {
                page = 1;
            }




            int serviceID = this.cbOperationsCenter.SelectedIndex;

            if (serviceID <= -1)
            {
                MessageBox.Show("请先选择营运中心，谢谢!");
                return;
            }
            string serviceName = this.cbOperationsCenter.SelectedItem.ToString(); ;

            this.butSearch.Enabled = false;
            Cursor = Cursors.WaitCursor;
            ProductionStatusSearch pss = new ProductionStatusSearch();
            pss.mynumber = mynumber;
            pss.buyid = buyid;
            pss.season = season;
            pss.datetype = datetype;
            pss.stardate = stardate;
            pss.enddate = enddate;
            pss.checkedDate = ckdate;
            pss.page = page;

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
            this.butSearch.Enabled = true;
            Cursor = Cursors.Default;
        }
    }
}

