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
    public partial class FrmCFoutput : Form
    {
        private static FrmCFoutput frm;
        public int hiedcolumnindex = -1; 
        DataTable styleDt = new DataTable();
        CFoutputManager cfm = new CFoutputManager();
        public DataGridView dgv = null;

        public FrmCFoutput()
        {
            InitializeComponent();           
            this.dgvCFoutPut.DoubleBufferedDataGirdView(true);
        }
        public static FrmCFoutput GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmCFoutput();
            }
            return frm;
        }



        private void FrmCFoutput_Resize(object sender, EventArgs e)
        {
            gbCFoutPut.Width = this.Width - 10;
            gbCFoutPut.Height = this.Height - gbSearch.Height - 20;
         
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            CFOutput cfoutput = new CFOutput();
            Cursor = Cursors.WaitCursor;
            cfoutput.style = this.txtStyle.Text.Trim();

            if (this.cbSearchType.SelectedIndex <= -1)
            {
                MessageBox.Show("请先选择查询类别");
                Cursor = Cursors.Default;
                return;
            }

            if (this.cbOrg.SelectedIndex<=-1)
            {
                MessageBox.Show("请先选择厂别");
                Cursor = Cursors.Default;
                return;
            }
             
            if(this.cbSearchType.SelectedIndex != 0 && ( this.cbSubinv.Items.Count <= 0  ||  this.cbSubinv.SelectedItem.ToString().Length<=0))
            {
                MessageBox.Show("请先选择仓别");
                Cursor = Cursors.Default;
                return;

            }

            if (this.cbSearchType.SelectedIndex != 0 && !this.cbDate.Checked)
            {
                MessageBox.Show("成品入库查询不能没有时间段");
                Cursor = Cursors.Default;
                return;

            }


            cfoutput.searchType = this.cbSearchType.SelectedIndex;
            cfoutput.checkDate = this.cbDate.Checked;
            cfoutput.org = this.cbOrg.SelectedItem.ToString();

            if(this.cbSubinv.Items.Count > 0 && this.cbSubinv.SelectedItem.ToString().Length > -1)
            {
                cfoutput.subinv = this.cbSubinv.SelectedItem.ToString();
            }
            else
            {
                cfoutput.subinv = "";
            }
           
            cfoutput.starDate = this.dtpStarDate.Value.ToString("yyyy-MM-dd");
            cfoutput.stopDate = this.dtpStopDate.Value.AddDays(1).ToString("yyyy-MM-dd");

            DateTime dstarDate = this.dtpStarDate.Value;
            DateTime dstopDate = this.dtpStopDate.Value;
            if(this.cbDate.Checked &&  dstopDate < dstarDate)
            {
                MessageBox.Show("结束时间不能小于开始时间");
                Cursor = Cursors.Default;
                return;
            }


            if (cfoutput.style == "" && cbDate.Checked == false)
            {
                MessageBox.Show("请至少指定一个条件");
                Cursor = Cursors.Default;
                return;
            }

            DataTable dt = cfm.getCFoutPut(cfoutput);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据");
                this.dgvCFoutPut.DataSource = null;


            }
            else
            {
                this.dgvCFoutPut.DataSource = null;
                this.dgvCFoutPut.DataSource = dt;
            }
            Cursor = Cursors.Default;


        }

        private void FrmCFoutput_Load(object sender, EventArgs e)
        {
            this.cbOrg.SelectedIndex = 0;
            this.cbSearchType.SelectedIndex = 0;
            this.dtpStarDate.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            this.dtpStopDate.Value = DateTime.Now.AddMonths(1).AddDays(1 - DateTime.Now.Day - 1);
        }

        private void dtpStarDate_VisibleChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = true;
        }

        private void dtpStopDate_ValueChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = true;
        }

        private void dgvCFoutPut_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvCFoutPut.Rows[e.RowIndex].Selected == false)
                    {
                        dgvCFoutPut.ClearSelection();
                        dgvCFoutPut.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvCFoutPut.SelectedRows.Count == 1)
                    {
                        dgvCFoutPut.CurrentCell = dgvCFoutPut.Rows[e.RowIndex].Cells[e.ColumnIndex];
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

        private void dgvCFoutPut_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
            Clipboard.SetDataObject(dgvCFoutPut.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvCFoutPut.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            ImproExcel();
        }
        public void ImproExcel()
        {
            String tableName = this.cbSearchType.SelectedItem.ToString() +"-"+this.dtpStarDate.Value.ToString("yyyy-MM-dd") +"-"+ this.dtpStopDate.Value.ToString("yyyy-MM-dd") +".xlsx";
            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "Excel 2007文件|*.xlsx|Excel 97-2003文件|*.xls";
            sdfExport.FileName = tableName;
            //   sdfExport.ShowDialog();
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
           
            NPOIExcelCompletedToMes NPOIexcel = new NPOIExcelCompletedToMes();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(this.dgvCFoutPut);

           // tableName = "dgvOutgoingTable";
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

        private void cbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dgvCFoutPut.DataSource = null;
            this.cbSubinv.Items.Clear();
            int move = 110;
            if(this.cbSearchType.SelectedIndex == 0  )
            {
                this.cbDate.Text = "报工日期:";
                if(this.labSubinv.Visible == true)
                {
                    this.labStyle.Left = this.labStyle.Left - move;
                    this.txtStyle.Left = this.txtStyle.Left - move;
                    this.cbDate.Left = this.cbDate.Left - move;
                    this.dtpStarDate.Left = this.dtpStarDate.Left - move;
                    this.labD.Left = this.labD.Left - move;
                    this.dtpStopDate.Left = this.dtpStopDate.Left - move;
                    this.butSearch.Left = this.butSearch.Left - move;
                }
                this.labSubinv.Visible = false;
                this.cbSubinv.Visible = false;

            }
            else if(this.labSubinv.Visible == false)
            {
                this.cbDate.Text = "扫描日期:";
                this.labSubinv.Visible = true;
                this.cbSubinv.Visible = true;
                this.labStyle.Left = this.labStyle.Left + move;
                this.txtStyle.Left = this.txtStyle.Left + move;
                this.cbDate.Left = this.cbDate.Left + move;
                this.dtpStarDate.Left = this.dtpStarDate.Left + move;
                this.labD.Left = this.labD.Left + move;
                this.dtpStopDate.Left = this.dtpStopDate.Left + move;
                this.butSearch.Left = this.butSearch.Left + move;
            }
            else
            {
                this.cbDate.Text = "扫描日期:";
            }
        }

        private void cbSubinv_Click(object sender, EventArgs e)
        {
            string org = this.cbOrg.SelectedItem.ToString();
            int searchType = this.cbSearchType.SelectedIndex;
            if (org.Length <= 0 || searchType <= 0)
            {
                return;
            }

           DataTable dt = cfm.getSubinv(org, searchType);
            if(dt.Rows.Count <=0)
            {
                MessageBox.Show("没有找到仓库");
                return;
            }

            this.cbSubinv.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                this.cbSubinv.Items.Add(row[0].ToString());

            }


        }

        private void cbOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dgvCFoutPut.DataSource = null;
            this.cbSubinv.Items.Clear();
        }
    }
}
