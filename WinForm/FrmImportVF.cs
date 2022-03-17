
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmImportVF : Form
    {
        public int hiedcolumnindex = -1;
        public List<DataTable> NikeDatas;
        public NIKEImportManager NIKEImport = new NIKEImportManager();
        public TNFImportManagers TNFImport = new TNFImportManagers();
        private static FrmImportVF frm;

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

        private void butImport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (this.rbCustTNF.Checked)
            {
                DataTable dt = (DataTable)this.dataGridView1.DataSource;
                if (dt == null)
                {
                    Cursor = Cursors.Default;
                    return;
                }
                this.butImport.Enabled = false;
                this.butSearch.Enabled = false;

                int con_Ppr = TNFImport.insetOrUpdataConPpr(dt);
                int con_Detail = TNFImport.insetOrUpdataConDetail(dt);
                MessageBox.Show("保存到 Con_Ppr 表 " + con_Ppr.ToString() + "条数据 \r\n " +
                                "保存到 Con_Detail 表 " + con_Detail.ToString() + "条数据 \r\n ", "保存成功");

                this.butImport.Enabled = true;
                this.butSearch.Enabled = true;
                MessageBox.Show("上传成功", "上传资料", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (this.rbCustNike.Checked)
            {
                // DataTable dt = (DataTable)this.dataGridView1.DataSource;
                if (NikeDatas == null || NikeDatas[0].Rows.Count <= 0 || NikeDatas[1].Rows.Count <= 0)
                {
                    Cursor = Cursors.Default;
                    return;
                }
                this.butImport.Enabled = false;
                this.butSearch.Enabled = false;

                int con_Ppr = NIKEImport.insetOrUpdataConPpr(NikeDatas[0]);
                int con_Detail = NIKEImport.insetOrUpdataConDetail(NikeDatas[1]);
                MessageBox.Show("保存到 Con_Ppr 表 " + con_Ppr.ToString() + "条数据 \r\n " +
                                "保存到 Con_Detail 表 " + con_Detail.ToString() + "条数据 \r\n ", "保存成功");

                this.butImport.Enabled = true;
                this.butSearch.Enabled = true;
                MessageBox.Show("上传成功", "上传资料", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Cursor = Cursors.Default;
        }

        private void butImport2_Click(object sender, EventArgs e)
        {
            string custid = "";
            string org = "";
            if (this.rbOrgSAA.Checked)
            {
                org = "SAA";
            }
            if (this.rbOrgTOP.Checked)
            {
                org = "TOP";
            }

            if (this.rbCustTNF.Checked)
            {
                custid = "TNF";
                this.panel1.Enabled = false;
                this.butSearch2.Enabled = false;
            }
            if (this.rbCustNike.Checked)
            {
                custid = "NIKE";
                this.panel1.Enabled = true;
                this.butSearch2.Enabled = true;
            }

            if (custid == "NIKE")  // NIKE
            {
                this.butSearch.Enabled = false;
                this.cbOnlyAdd.Checked = false;
                this.cbOnlyAdd.Enabled = false;
                string StartDate = this.dtpStartDate.Value.ToString("yyyy-MM-dd");
                string StopDate = this.dtpStopDate.Value.AddDays(+1).ToString("yyyy-MM-dd");
                NikeDatas = NIKEImport.getPODataFromScanService2(org, StartDate, StopDate, 1);
                Cursor = Cursors.WaitCursor;
                if (NikeDatas != null)
                {
                    this.dataGridView1.DataSource = NikeDatas[0];
                }
            }
            Cursor = Cursors.Default;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            this.butSearch.Enabled = true;
            MessageBox.Show("查询完成");
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string custid = "";
            string org = "";
            if (this.rbOrgSAA.Checked)
            {
                org = "SAA";
            }
            if (this.rbOrgTOP.Checked)
            {
                org = "TOP";
            }

            if (this.rbCustTNF.Checked)
            {
                custid = "TNF";
                this.panel1.Enabled = false;
            }
            if (this.rbCustNike.Checked)
            {
                custid = "NIKE";
                this.panel1.Enabled = true;
            }

            if (custid == "TNF") //TNF
            {
                this.butSearch.Enabled = false;
                if (this.cbOnlyAdd.Checked)
                {
                    //查已导入的ID号
                    int Id = TNFImport.getTnfMaxId();
                    if (Id <= 0)
                    {
                        Cursor = Cursors.Default;
                        this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                        this.butSearch.Enabled = true;
                        MessageBox.Show("无新增资料");
                        return;
                    }
                    DataTable TnfDate = TNFImport.getPODataFromScanService(Id);

                    if (TnfDate == null || TnfDate.Rows.Count <= 0)
                    {
                        Cursor = Cursors.Default;
                        this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                        this.butSearch.Enabled = true;
                        MessageBox.Show("无新增资料");
                        return;
                    }
                    this.dataGridView1.DataSource = TnfDate;
                }
                if (this.cbJustPO.Checked)
                {
                    string PONumber = this.txtPo.Text.Trim();
                    if (PONumber.Length <= 0)
                    {
                        Cursor = Cursors.Default;
                        this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                        this.butSearch.Enabled = true;
                        MessageBox.Show("无资料");
                        return;
                    }

                    DataTable TnfDate = TNFImport.getPODataFromScanService(PONumber);

                    this.dataGridView1.DataSource = TnfDate;
                }
                if (this.cbJustDate.Checked)
                {
                    string StartDate = this.dtpStartDate.Value.ToString("yyyy-MM-dd");
                    string StopDate = this.dtpStopDate.Value.AddDays(+1).ToString("yyyy-MM-dd");
                    DataTable TnfDate = TNFImport.getPODataFromScanService(StartDate, StopDate);

                    this.dataGridView1.DataSource = TnfDate;
                }
            }
            else if (custid == "NIKE")  // NIKE
            {
                this.butSearch.Enabled = false;
                this.cbOnlyAdd.Checked = false;
                this.cbOnlyAdd.Enabled = false;

                if (this.cbJustPO.Checked)
                {
                    string PONumber = this.txtPo.Text.Trim();
                    string main_line = this.txtLine.Text.Trim();
                    if (PONumber.Length <= 0)
                    {
                        Cursor = Cursors.Default;
                        this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                        this.butSearch.Enabled = true;
                        MessageBox.Show("请输入PO#,谢谢！");
                        return;
                    }

                    NikeDatas = NIKEImport.getPODataFromScanService(org, PONumber, main_line, 0);

                    this.dataGridView1.DataSource = NikeDatas[0];
                }
                if (this.cbJustDate.Checked)
                {
                    string StartDate = this.dtpStartDate.Value.ToString("yyyy-MM-dd");
                    string StopDate = this.dtpStopDate.Value.AddDays(+1).ToString("yyyy-MM-dd");
                    NikeDatas = NIKEImport.getPODataFromScanService(org, StartDate, StopDate, 1);
                    Cursor = Cursors.WaitCursor;
                    this.dataGridView1.DataSource = NikeDatas[0];
                }
            }
            Cursor = Cursors.Default;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            this.butSearch.Enabled = true;
            MessageBox.Show("查询完成");
        }

        private void cbJustDate_Click(object sender, EventArgs e)
        {
            if (this.cbJustDate.Checked)
            {
                this.cbOnlyAdd.Checked = false;
                this.cbJustPO.Checked = false;
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

        private void cbOnlyAdd_Click(object sender, EventArgs e)
        {
            if (this.cbOnlyAdd.Checked)
            {
                this.cbJustPO.Checked = false;
                this.cbJustDate.Checked = false;
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

        private void FrmImportVF_Resize(object sender, EventArgs e)
        {
            this.groupBox1.Width = this.Width - 20;
            this.groupBox1.Height = this.Height - 90;
        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbCustTNF.Checked)
            {
                this.txtLine.Enabled = false;
                this.cbOnlyAdd.Enabled = true;
                this.rbOrgSAA.Checked = true;
                this.panel1.Enabled = false;
                this.butSearch2.Enabled = false;
            }
            else if (this.rbCustNike.Checked)
            {
                this.txtLine.Enabled = true;
                this.cbOnlyAdd.Enabled = false;
                this.panel1.Enabled = true;
                this.rbOrgTOP.Checked = true;
                this.butSearch2.Enabled = true;
            }
        }

        //是否选中外面
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

        private void txtPo_MouseDown(object sender, MouseEventArgs e)
        {
            this.cbJustPO.Checked = true;
            this.cbOnlyAdd.Checked = false;
            this.cbJustDate.Checked = false;
        }
    }
}