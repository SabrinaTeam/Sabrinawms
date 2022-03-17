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
    public partial class FrmProductSearch : Form
    {
        private static FrmProductSearch frm;

        //  public DataGridView selectDgv = null;
          public int hiedcolumnindex = -1; //是否选中外面
          DataTable styleDt = new DataTable();
        ProductSearchManager psm = new ProductSearchManager();
        //  DataGridView selecteddgv = null;
        public DataGridView dgv = null;

        public FrmProductSearch()
        {
            InitializeComponent();
            this.dgvData.DoubleBufferedDataGirdView(true); 
            this.dataGridView1.DoubleBufferedDataGirdView(true);
        }
        public static FrmProductSearch GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmProductSearch();
            }
            return frm;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            List<string> parameters = new List<string>();
            string po = this.txtPO.Text.Trim().ToUpper();
            if (po.Length <= 0)
            {
                this.cbScanDate.Checked = true;
            }
            else
            {
                this.cbScanDate.Checked = false;
            }
            
             string style = "";
            if (this.cbStyle.Items.Count > 0 && this.cbStyle.SelectedIndex > -1)
            {
                style = this.cbStyle.SelectedItem.ToString().Trim();
            }
            string color = "";
            if (this.cbColor.Items.Count > 0 && this.cbColor.SelectedIndex > -1)
            {
                color = this.cbColor.SelectedItem.ToString().Trim();
            }
            string size = "";
            if (this.cbSize.Items.Count > 0 && this.cbSize.SelectedIndex > -1 )
            {
                 size = this.cbSize.SelectedItem.ToString().Trim();
            }
            string org = "";
            if (this.cbOrg.SelectedIndex > -1 && this.cbOrg.SelectedIndex > -1)
            {
                org = this.cbOrg.SelectedItem.ToString().Trim();
            }
            string subinv = "";
            if (this.cbSubinv.SelectedIndex > -1 && this.cbSubinv.SelectedIndex > -1)
            {
                subinv = this.cbSubinv.SelectedItem.ToString().Trim();
            }

            string type = "";
            if(this.cbType.SelectedIndex > -1)
            {
                type = this.cbType.SelectedItem.ToString().Trim();
            }
           

            string scanData = "0";
            if (this.cbScanDate.Checked)
            {
                scanData = "1";
            }
            else
            {
                scanData = "0";
            }
            
            string starData = "";
            string stopData = "";
            if (this.cbScanDate.Checked)
            {
                starData = this.dpStarDate.Value.ToString("yyyy-MM-dd");
                stopData = this.dpStopDate.Value.AddDays(1).ToString("yyyy-MM-dd");
            }
            parameters.Add(style);
            parameters.Add(color);
            parameters.Add(size);
            parameters.Add(org);
            parameters.Add(subinv);
            parameters.Add(type);
            parameters.Add(scanData);
            parameters.Add(starData);
            parameters.Add(stopData);
            parameters.Add(po);

            List<DataTable> dts = psm.getInvByP(parameters);
            this.dgvData.DataSource = null;
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;

            this.dataGridView1.DataSource = dts[0];
            this.dataGridView2.DataSource = dts[1];
            this.dgvData.DataSource = dts[2];
            MessageBox.Show("查询完成");

        }

        private void cbOrg_Click(object sender, EventArgs e)
        {
            List<string> orgs =  psm.getOrg();
            this.cbOrg.Items.Clear();
            foreach (string org in orgs)
            {
                this.cbOrg.Items.Add(org);
            }
        }
 

        private void cbSubinv_Click(object sender, EventArgs e)
        {
          //  this.getSubinv();
        }

        private void cbOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
             * build SAA
                build A
                build D
             */
            this.cbSubinv.Items.Clear();
            if (this.cbOrg.Items == null || this.cbOrg.Items.Count <= 0 || this.cbOrg.SelectedIndex <= -1)
            {
                return;
            }
            string org = this.cbOrg.SelectedItem.ToString();
            if (org == "SAA")
            {
                this.cbSubinv.Items.Add("build SAA");
            }

            if (org == "TOP")
            {
                this.cbSubinv.Items.Add("build A");
                this.cbSubinv.Items.Add("build D");
            }
            //  this.getSubinv();
              this.cbSubinv.DroppedDown = true;
              Cursor.Current = Cursors.Default;
        }
        public void getSubinv()
        {
            if (this.cbOrg.Items == null || this.cbOrg.Items.Count <= 0 || this.cbOrg.SelectedIndex <= -1)
            {
                return;
            }
            string org = this.cbOrg.SelectedItem.ToString();
            List<string> subinvs = psm.getSubinv(org);
            this.cbSubinv.Items.Clear();
            foreach (string subinv in subinvs)
            {
                this.cbSubinv.Items.Add(subinv);
            }
        }

        private void FrmProductSearch_Resize(object sender, EventArgs e)
        {
            this.gbData.Width = this.Width - 20;
            this.gbData.Height = this.Height - this.groupBox1.Height - 50;
            this.splitContainer1.SplitterDistance = Convert.ToInt32(this.gbData.Width * 0.17);

        }

        private void dpStopDate_ValueChanged(object sender, EventArgs e)
        {
            this.cbScanDate.Checked = true;
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
           // this.cbScanDate.Checked = false;
        }

        private void cbSize_RightToLeftChanged(object sender, EventArgs e)
        {
           
        }

        private void txtStyle_TextChanged(object sender, EventArgs e)
        { 

            if (this.styleDt.Rows.Count <= 0)
            {
                this.styleDt = psm.getStyles();
            }
            if (this.styleDt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据");
                return;
            }
            string styleSearch = this.txtStyle.Text.Trim().ToUpper();

            if (styleSearch.Length > 0)
            {
                 

                DataRow[] Qstyle = this.styleDt.Select("Buyer_Item like '%" + styleSearch + "%'");
                this.cbStyle.Items.Clear();
                foreach (DataRow dr in Qstyle)
                {
                    this.cbStyle.Items.Add(dr["Buyer_Item"].ToString());
                }
                this.cbStyle.DroppedDown = true;

                Cursor.Current = Cursors.Default;

            }
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }

            this.tomenuRight(dgv, e);
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

        private void cbStyle_Click(object sender, EventArgs e)
        {
            if (this.styleDt.Rows.Count <= 0)
            {
                this.styleDt = psm.getStyles();
            
          
            if (this.styleDt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据");
                return;
            }
            this.cbStyle.Items.Clear();
            foreach (DataRow dr in this.styleDt.Rows)
            {
                this.cbStyle.Items.Add(dr["Buyer_Item"].ToString());
            }
            }

        }

        private void cbColor_Click(object sender, EventArgs e)
        {
            if(this.cbStyle.Items == null || this.cbStyle.Items.Count<=0 || this.cbStyle.SelectedIndex <= -1)
            {
                return;
            }
            string style = this.cbStyle.SelectedItem.ToString();
            List<string> colors = psm.getColors(style);
            this.cbColor.Items.Clear();
            foreach (string color in colors)
            {
                this.cbColor.Items.Add(color);
            }
             
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }

            this.tomenuRight(dgv, e);
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

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
           
            this.label10.Left = this.splitContainer1.Panel1.Width + 10;

            this.label9.Left = this.splitContainer1.Panel1.Width + this.splitContainer2.Panel1.Width + 10;
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


        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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
