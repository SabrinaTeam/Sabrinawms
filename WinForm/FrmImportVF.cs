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
    public partial class FrmImportVF : Form
    {
        private static FrmImportVF frm;
        public TNFImportManagers TNFImport = new TNFImportManagers();
        
        public FrmImportVF()
        {
            InitializeComponent();
            this.dataGridView1.DoubleBufferedDataGirdView(true); 
        }
        public static FrmImportVF GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmImportVF();
            }
            return frm;
        }


        private void butSearch_Click(object sender, EventArgs e)
        {
           
            this.butSearch.Enabled = false;
            if (this.cbOnlyAdd.Checked)
            {
                //查已导入的ID号
                int Id = TNFImport.getTnfMaxId();
                if(Id<=0)
                {
                    this.butSearch.Enabled = true;
                    return;
                }
                DataTable TnfDate = TNFImport.getPODataFromScanService(Id);
                
                    Cursor = Cursors.WaitCursor;
                    this.dataGridView1.DataSource = TnfDate;
                
              
            }
            if (this.cbJustPO.Checked)
            {
                string PONumber = this.txtPo.Text.Trim();
                if (PONumber.Length <= 0)
                {
                    return;
                }
                 
                DataTable TnfDate = TNFImport.getPODataFromScanService(PONumber);
                Cursor = Cursors.WaitCursor;
                this.dataGridView1.DataSource = TnfDate;
            }
            if (this.cbJustDate.Checked)
            {
                string StartDate = this.dtpStartDate.Value.ToString("yyyy-MM-dd");
                string StopDate = this.dtpStopDate.Value.ToString("yyyy-MM-dd");
                DataTable TnfDate = TNFImport.getPODataFromScanService(StartDate, StopDate);
                Cursor = Cursors.WaitCursor;
                this.dataGridView1.DataSource = TnfDate;
            }


        
            Cursor = Cursors.Default;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            this.butSearch.Enabled = true;
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

        private void groupBox1_Resize(object sender, EventArgs e)
        {
           
        }

        private void FrmImportVF_Resize(object sender, EventArgs e)
        {
            this.groupBox1.Width = this.Width - 20;
            this.groupBox1.Height = this.Height - 90;
        }

        private void cbOnlyAdd_Click(object sender, EventArgs e)
        {
            if (this.cbOnlyAdd.Checked)
            {
                this.cbJustPO.Checked = false;
                this.cbJustDate.Checked = false;
            }
           
        }

        private void cbJustPO_Click(object sender, EventArgs e)
        {
            if (this.cbJustPO.Checked)
            {
                this.cbOnlyAdd.Checked = false;
                this.cbJustDate.Checked = false;
            }
        }

        private void cbJustDate_Click(object sender, EventArgs e)
        {
            if (this.cbJustDate.Checked)
            {
                this.cbOnlyAdd.Checked = false;
                this.cbJustPO.Checked = false;
            }
        }

        private void txtPo_MouseDown(object sender, MouseEventArgs e)
        {
            this.cbJustPO.Checked = true;
            this.cbOnlyAdd.Checked = false;
            this.cbJustDate.Checked = false;
        }

        private void dtpStartDate_MouseDown(object sender, MouseEventArgs e)
        {
            this.cbJustPO.Checked = false;
            this.cbOnlyAdd.Checked = false;
            this.cbJustDate.Checked = true;
        }

        private void dtpStopDate_MouseDown(object sender, MouseEventArgs e)
        {
            this.cbJustPO.Checked = false;
            this.cbOnlyAdd.Checked = false;
            this.cbJustDate.Checked = true;

        }
        public int hiedcolumnindex = -1; //是否选中外面
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

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dataGridView1.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            ImproExcel();
        }
        public void ImproExcel()
        {
            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";           
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            NPOIExcelHelper NPOIexcel = new NPOIExcelHelper();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(dataGridView1);

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

        private void butImport_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.dataGridView1.DataSource ;
            if (dt == null)
            {
                return;
            }
            this.butImport.Enabled = false;
            this.butSearch.Enabled = false;
            Cursor = Cursors.Default;
            int con_Ppr= TNFImport.insetOrUpdataConPpr(dt);
            int con_Detail =  TNFImport.insetOrUpdataConDetail(dt);
            MessageBox.Show("保存到 Con_Ppr 表 " + con_Ppr.ToString() + "条数据 \r\n " +
                            "保存到 Con_Detail 表 " + con_Detail.ToString() + "条数据 \r\n ","保存成功");
            Cursor = Cursors.Default;
            this.butImport.Enabled = true;
            this.butSearch.Enabled = true;


        }
    }
}
 