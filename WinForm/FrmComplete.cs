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
    public partial class FrmComplete : Form
    {
        private static FrmComplete frm;
        CompeleteManager cm = new CompeleteManager();
        public DataGridView selectDgv = null;
        public int hiedcolumnindex = -1; //是否选中外面
        public FrmComplete()
        {
            InitializeComponent();
            this.dgvMesDataDetails.DoubleBufferedDataGirdView(true);
            this.dgvERPDataDetails.DoubleBufferedDataGirdView(true);
            this.dgvAllDataDetails.DoubleBufferedDataGirdView(true);
        }
        public static FrmComplete GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmComplete();
            }
            return frm;
        }

        private void FrmComplete_Load(object sender, EventArgs e)
        {

            this.dtpStarDate.Value = DateTime.Now.AddDays(-7);

        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            string style = this.txtStyle.Text.Trim();
            string myNumber = this.txtMyNumber.Text.Trim();
            bool ckdate = this.cbScanDate.Checked;
            string stardate = this.dtpStarDate.Value.ToString("yyyy-MM-dd");
            string stopdate = this.dtpStopDate.Value.AddDays(1).ToString("yyyy-MM-dd");

            this.butSearch.Enabled = false;
            Cursor = Cursors.WaitCursor;
          

            // 查询MES 
            List<CompeleteMes> compeleteDatas = cm.getMesScanData(style, myNumber, ckdate, stardate, stopdate);
            if (compeleteDatas.Count <= 0)
            {
                MessageBox.Show("not data!");
                Cursor = Cursors.Default;
                this.butSearch.Enabled = Enabled;
                return;
            }

            this.dgvMesDataDetails.DataSource = null;
            this.dgvMesDataDetails.DataSource = compeleteDatas;


            // 查询ERP
            List<CompeleteERP> compeleteERPDatas = this.getCompeleteERP(compeleteDatas);
            if (compeleteERPDatas.Count <= 0)
            {
                MessageBox.Show("not ERP data!");
                Cursor = Cursors.Default;
                this.butSearch.Enabled = Enabled;
                return;
            }

            this.dgvERPDataDetails.DataSource = null;
            this.dgvERPDataDetails.DataSource = compeleteERPDatas;


            // 合并MES ERP

            List<meshMesERPCompelete> meshMesERPS = cm.meshMesERPCompelete(compeleteDatas, compeleteERPDatas);
            if (meshMesERPS.Count <= 0)
            {
                MessageBox.Show("not ERP data!");
                Cursor = Cursors.Default;
                this.butSearch.Enabled = Enabled;
                return;
            }
            this.dgvERPDataDetails.DataSource = null;
            this.dgvERPDataDetails.DataSource = compeleteERPDatas;

            this.dgvAllDataDetails.DataSource = null;
            this.dgvAllDataDetails.DataSource = meshMesERPS;

            Cursor = Cursors.Default;
            this.butSearch.Enabled = Enabled;

        }

        private List<CompeleteERP> getCompeleteERP(List<CompeleteMes> compeleteMes)
        {
            return cm.getCompeleteERP(compeleteMes);
        }



        private void dgvMesDataDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvERPDataDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvAllDataDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void FrmComplete_Resize(object sender, EventArgs e)
        {
            this.gbDataDetails.Width = this.Width - 20;
            this.gbDataDetails.Height = this.Height - 100;
            this.splitContainer1.SplitterDistance = 450;
            this.splitContainer1.FixedPanel = FixedPanel.Panel1;

            this.splitContainer2.SplitterDistance = 350;
            this.splitContainer1.FixedPanel = FixedPanel.Panel1;
        }

        private void dgvAllDataDetails_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
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
            if (selectDgv != null)
            {
                Clipboard.SetDataObject(selectDgv.CurrentCell.Value.ToString());
            }
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            if (selectDgv != null)
            {
                Clipboard.SetDataObject(selectDgv.GetClipboardContent());
            }
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            if (this.selectDgv != null)
            {
                ImproExcel(this.selectDgv);
            }
        }
        public void ImproExcel(DataGridView selectDgv)
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
            tabl = GetDgvToTable(this.selectDgv);
            if (selectDgv == this.dgvMesDataDetails)
            {
                tableName = "dgvMesDataDetails";
            }
            else if(selectDgv == this.dgvERPDataDetails)
            {
                tableName = "dgvERPDataDetails"; 
            }else if (selectDgv == this.dgvAllDataDetails)
            {
                tableName = "dgvAllDataDetails"; 
            }

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
                    return;
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



        private void dgvMesDataDetails_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvMesDataDetails.Rows[e.RowIndex].Selected == false)
                    {
                        dgvMesDataDetails.ClearSelection();
                        dgvMesDataDetails.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvMesDataDetails.SelectedRows.Count == 1)
                    {
                        dgvMesDataDetails.CurrentCell = dgvMesDataDetails.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    selectDgv = dgvMesDataDetails;
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

        private void dgvERPDataDetails_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvERPDataDetails.Rows[e.RowIndex].Selected == false)
                    {
                        dgvERPDataDetails.ClearSelection();
                        dgvERPDataDetails.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvERPDataDetails.SelectedRows.Count == 1)
                    {
                        dgvERPDataDetails.CurrentCell = dgvERPDataDetails.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    selectDgv = dgvERPDataDetails;
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

        private void dgvAllDataDetails_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvAllDataDetails.Rows[e.RowIndex].Selected == false)
                    {
                        dgvAllDataDetails.ClearSelection();
                        dgvAllDataDetails.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvAllDataDetails.SelectedRows.Count == 1)
                    {
                        dgvAllDataDetails.CurrentCell = dgvAllDataDetails.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    selectDgv = dgvAllDataDetails;
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
