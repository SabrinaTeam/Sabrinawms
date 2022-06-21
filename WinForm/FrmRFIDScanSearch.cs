using BLL;
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

namespace WinForm
{
    public partial class FrmRFIDScanSearch : Form
    {

        private static FrmRFIDScanSearch frm;
        public FrmRFIDScanSearchManager rssm = new FrmRFIDScanSearchManager();
        public int hiedcolumnindex = -1; //是否选中外面
        public DataGridView dgv = null;
        public FrmRFIDScanSearch()
        {
            InitializeComponent();
            this.dgvBoxsHeads.DoubleBufferedDataGirdView(true);
            this.dgvScanDetails.DoubleBufferedDataGirdView(true);
        }
        public static FrmRFIDScanSearch GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmRFIDScanSearch();
            }
            return frm;
        }

        private void FrmRFIDScanSearch_Load(object sender, EventArgs e)
        {
            this.cbScanType.SelectedIndex = 0;
        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            if (this.dgv == null)
            {
                return;
            }
            Clipboard.SetDataObject(this.dgv.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            if (this.dgv == null)
            {
                return;
            }

            Clipboard.SetDataObject(this.dgv.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            if (this.dgv == null)
            {
                return;
            }

            ImproExcel(this.dgv);
        }
        public void ImproExcel(DataGridView dgv)
        {
            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";
            //   sdfExport.ShowDialog();
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            NPOIExcelHelper NPOIexcel = new NPOIExcelHelper();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(dgv);

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
            List<string> parameters = new List<string>();

            string ScanType = this.cbScanType.SelectedIndex.ToString();
            string style = this.txtStyle.Text.Trim().ToUpper();
            string color = this.txtColor.Text.Trim().ToUpper();
            string po = this.txtPO.Text.Trim().ToUpper();
            string cartonNumber = this.txtCartonNumber.Text.Trim().ToUpper();

            string starData = "";
            string stopData = "";
            string scanData = "0";
            if (this.ckScanDate.Checked)
            {
                starData = this.dtpStartDate.Value.ToString("yyyy-MM-dd");
                stopData = this.dtpStopDate.Value.ToString("yyyy-MM-dd");
                scanData = "1";
            }
            stopData = this.dtpStopDate.Value.AddDays(+1).ToString("yyyy-MM-dd");
            parameters.Add(ScanType);
            parameters.Add(style);
            parameters.Add(color);
            parameters.Add(po);
            parameters.Add(cartonNumber);
            parameters.Add(scanData);
            parameters.Add(starData);
            parameters.Add(stopData);


            DataTable ScanBoxs =rssm.getRfidScanBox(parameters);
            if(cbScanType.SelectedIndex == 0)
            {
                this.splitContainer1.Panel1Collapsed = true;
                this.dgvBoxsHeads.DataSource = null;
                this.dgvScanDetails.DataSource = null;
                this.dgvScanDetails.DataSource = ScanBoxs;

            }
            else if(cbScanType.SelectedIndex == 1)
            {
                this.splitContainer1.Panel1Collapsed = false;
                this.dgvBoxsHeads.DataSource = null;
                this.dgvScanDetails.DataSource = null;
                if (ScanBoxs != null && ScanBoxs.Rows.Count > 0)
                {
                    this.dgvBoxsHeads.DataSource = ScanBoxs;
                    string BoxheadID = ScanBoxs.Rows[0]["boxheadid"].ToString();
                    this.getBoxScanDetailsByHeadID(BoxheadID);

                }
            }


            MessageBox.Show("查询完成");
        }

        private void dgvBoxsHeads_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }

            this.tomenuRight(dgv, e);
        }

        private void dgvBoxsHeads_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
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

        private void FrmRFIDScanSearch_Resize(object sender, EventArgs e)
        {
            this.gbSearchFilter.Width = this.Width - 40;
            this.butSearch.Left = this.gbSearchFilter.Width - this.butSearch.Width - 20;
            this.gbScanDetails.Width = this.gbSearchFilter.Width;
            this.gbSearchFilter.Left = (this.Width -this.gbSearchFilter.Width) / 2;
            this.gbScanDetails.Left = this.gbSearchFilter.Left;
            this.gbScanDetails.Height = this.Height - this.gbSearchFilter.Height - 50;
        }

        private void dgvScanDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvBoxsHeads_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string BoxheadID = "";
            string l = "";
            if (e.RowIndex < 0)
            {
                return;
            }
            BoxheadID = this.dgvBoxsHeads["BoxheadID", e.RowIndex].Value.ToString();

            this.getBoxScanDetailsByHeadID(BoxheadID);



        }

        public void getBoxScanDetailsByHeadID(string BoxheadID)
        {
            if (BoxheadID.Length <=0)
            {
                return ;

            }
            DataTable ScanBoxs = rssm.getBoxScanDetailsByHeadID(BoxheadID);
            this.dgvScanDetails.DataSource = null;
            this.dgvScanDetails.DataSource = ScanBoxs;

        }

        private void dgvScanDetails_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }

            this.tomenuRight(dgv, e);
        }

    }
}
