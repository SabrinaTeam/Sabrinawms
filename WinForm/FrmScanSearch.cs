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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmScanSearch : Form
    {
        private static FrmScanSearch frm;
        public FrmScanSerachManager fssm = new FrmScanSerachManager();
        public int hiedcolumnindex = -1; //是否选中外面
        public FrmScanSearch()
        {
            InitializeComponent();
            this.dgvData.DoubleBufferedDataGirdView(true);
        }
        public static FrmScanSearch GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmScanSearch();
            }
            return frm;
        }


        private void FrmScanSearch_Resize(object sender, EventArgs e)
        {
            this.gbData.Width = this.Width - 15;
            this.gbData.Height = this.Height - 110;

        }

        private void cbOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbsubinv.Items.Clear();
            if(this.cbOrg.SelectedIndex < 0)
            {
                this.cbOrg.SelectedIndex = 0;
            }
            string org = this.cbOrg.SelectedItem.ToString();
            List<string> subinvs = fssm.getSubinbsByOrg(org);
            if (subinvs.Count <= 0)
            {
                MessageBox.Show("没有找到仓库代号");
                return;
            }
            foreach (string subinv in subinvs)
            {
                this.cbsubinv.Items.Add(subinv);
            }
            this.cbsubinv.DroppedDown = true;

        }

        private void cbsubinv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cbsubinv.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择仓库");
                return;
            }
            this.cbLocation.Items.Clear();
            if (this.cbsubinv.SelectedIndex < 0)
            {
                this.cbsubinv.SelectedIndex = 0;
            }
            string org = this.cbOrg.SelectedItem.ToString();
            string subinv = this.cbsubinv.SelectedItem.ToString();
            List<string> locations = fssm.getLocationsBysubinv(org,subinv);
            if (locations.Count <= 0)
            {
                MessageBox.Show("没有找到仓库代号");
                return;
            }
            foreach (string location in locations)
            {
                this.cbLocation.Items.Add(location);
            }
            this.cbLocation.DroppedDown = true;

        }
        private void FrmScanSearch_Load(object sender, EventArgs e)
        {
            this.dtpStarDate.Value = DateTime.Now.AddDays(-7);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
        
            
            if (this.cbOrg.SelectedIndex < 0)
            {
                MessageBox.Show("厂区不能为空！");
                return;
            }
            if(this.cbsubinv.SelectedIndex < 0)
            {
                MessageBox.Show("仓库不能为空！");
                return;
            }
            if (this.cbLocation.SelectedIndex < 0)
            {
                MessageBox.Show("储位不能为空！");
                return;
            }
            string org = this.cbOrg.SelectedItem.ToString();
            string subinv = this.cbsubinv.SelectedItem.ToString();
            string location = this.cbLocation.SelectedItem.ToString();          
            string startDate = this.dtpStarDate.Value.ToString("yyyy-MM-dd");
            string stopDate = this.dtpStopDate.Value.AddDays(1).ToString("yyyy-MM-dd");
            string styleCode = this.txtStyle.Text.Trim();
            string colorCode = this.txtColor.Text.Trim();
            

            List<locationData> locationDatas = fssm.getScanByQuery(org, subinv, location,  startDate, stopDate, styleCode, colorCode);

            if (locationDatas.Count > 0)
            {

                this.dgvData.DataSource = locationDatas;
                this.dgvData.Columns["ORG"].HeaderText = "厂区";
                this.dgvData.Columns["Cust_id"].HeaderText = "客户";
                this.dgvData.Columns["subinv"].HeaderText = "仓库";
                this.dgvData.Columns["Location"].HeaderText = "储位";
                this.dgvData.Columns["style"].HeaderText = "款式";
                this.dgvData.Columns["po"].HeaderText = "PO#";
                this.dgvData.Columns["MAIN_LINE"].HeaderText = "PO_LINE";
                this.dgvData.Columns["color_code"].HeaderText = "色组";
                this.dgvData.Columns["Size1"].HeaderText = "尺码";
                this.dgvData.Columns["QTY"].HeaderText = "数量";
                this.dgvData.Columns["con_no"].HeaderText = "箱号";
                this.dgvData.Columns["kg"].HeaderText = "重量";
                this.dgvData.Columns["TagNumber"].HeaderText = "外箱条码号";
                this.dgvData.Columns["scantime"].HeaderText = "扫描时间";
                this.dgvData.Columns["update_date"].HeaderText = "上传时间";  
                this.dgvData.Columns["create_pc"].HeaderText = "上传设备";
            }
            else
            {
                MessageBox.Show("no data");
                return;
            }
            MessageBox.Show("查询完成");

        }

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvData.Rows[e.RowIndex].Selected == false)
                    {
                        dgvData.ClearSelection();
                        dgvData.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvData.SelectedRows.Count == 1)
                    {
                        dgvData.CurrentCell = dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
            Clipboard.SetDataObject(dgvData.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvData.GetClipboardContent());
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
            String tableName = "";
            NPOIExcelCompletedToMes NPOIexcel = new NPOIExcelCompletedToMes();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(this.dgvData);
            tableName = "dgvOutgoingTable";
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

    }
}
