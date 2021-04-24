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
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmPO_MyNo : Form
    {
        private static FrmPO_MyNo frm;
        private PoNumberManager pn = new PoNumberManager();
        public static FrmPO_MyNo GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmPO_MyNo();
            }
            return frm;
        }

        public FrmPO_MyNo()
        {
            InitializeComponent();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            string startDate = this.dtpStarDate.Value.ToString("yyyy-MM-dd");
            string stopDate = this.dtpStopDate.Value.AddDays(1).ToString("yyyy-MM-dd");

            DataTable PoNumbers = pn.getPoNumbersByODdate(startDate, stopDate);
            this.dgvMyNoumber.DataSource = null;
            this.dgvMyNoumber.DataSource = PoNumbers;
        }

        private void FrmPO_MyNo_Resize(object sender, EventArgs e)
        {
            this.groupBox1.Width = this.Width - 20;
            this.groupBox1.Height = this.Height - 80;
        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            
                Clipboard.SetDataObject(dgvMyNoumber.CurrentCell.Value.ToString());
             
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvMyNoumber.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            
            ImproExcel();
        }
        public void ImproExcel( )
        {
            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            String tableName = "";
            NPOIExcelOutGoing NPOIexcel = new NPOIExcelOutGoing();
            DataTable tabl = new DataTable();
          
                tabl = GetDgvToTable(this.dgvMyNoumber);
                tableName = "My_Noumber";
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
        public int hiedcolumnindex = -1; //是否选中外面
        private void dgvMyNoumber_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvMyNoumber.Rows[e.RowIndex].Selected == false)
                    {
                        dgvMyNoumber.ClearSelection();
                        dgvMyNoumber.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvMyNoumber.SelectedRows.Count == 1)
                    {
                        dgvMyNoumber.CurrentCell = dgvMyNoumber.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
