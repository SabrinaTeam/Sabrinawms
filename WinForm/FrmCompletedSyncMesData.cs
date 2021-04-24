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
    //test
    public partial class FrmCompletedSyncMesData : Form
    {
        private static FrmCompletedSyncMesData frm;
        public CompketedSyncMesDataManager csmm = new CompketedSyncMesDataManager();
        public DataGridView selectDgv = null;
        public int hiedcolumnindex = -1; //是否选中外面
        public FrmCompletedSyncMesData()
        {
            InitializeComponent();
            this.dataGridView1.DoubleBufferedDataGirdView(true); 
        }
        public static FrmCompletedSyncMesData GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmCompletedSyncMesData();
            }
            return frm;
        }
        private void butRefresh_Click(object sender, EventArgs e)
        {
           DataTable dt =  csmm.getCompketedSyncDataErrors();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = dt;
            MessageBox.Show("获取资料完成");
        }

        private void FrmCompletedSyncMesData_Resize(object sender, EventArgs e)
        {
            this.groupBox2.Width = this.Width - 20;
            this.groupBox2.Height = this.Height - 50;
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

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dataGridView1.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    selectDgv = dataGridView1;
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
                
                    tableName = "dataGridView1";
                
                

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
        }
}
