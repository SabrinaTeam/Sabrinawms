using BLL;
using Microsoft.VisualBasic;
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
    public partial class FrmDelScanHURLEY : Form
    {
        private static FrmDelScanHURLEY frm;
        public DelScanHURLEYManager dhm = new DelScanHURLEYManager();
        public string cust_id = "";
        public string org_id = "";
        public int pageRows = 1000;
        public int pages = 1;
        public bool isbusy = false;
        DataTable dtr = new DataTable();
        public DataGridView dgv = null;
        
        public int hiedcolumnindex = -1;
        public FrmDelScanHURLEY()
        {
            InitializeComponent();
            this.dgvHURLEY.DoubleBufferedDataGirdView(true);

            this.dtr.Columns.Add("id");
            this.dtr.Columns.Add("TagNumber");
            this.dtr.Columns.Add("Cust_id");
            this.dtr.Columns.Add("Location");
            this.dtr.Columns.Add("org");
            this.dtr.Columns.Add("scantime");  
            this.dtr.Columns.Add("con_no");
            this.dtr.Columns.Add("create_pc");

        }
        public static FrmDelScanHURLEY GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmDelScanHURLEY();
            }
            return frm;
        }
        private void butSearch_Click(object sender, EventArgs e)
        {
            if (!isbusy)
            {
                getDels();
            }

        }

        private void getDels()
        {
            this.isbusy = true;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.butSearch.Enabled = false;
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            string starDate = this.dtpStarDate.Value.ToString("yyyy-MM-dd");
            string stopDate = this.dtpStopDate.Value.AddDays(1).ToString("yyyy-MM-dd");
            string tagNumber = this.txtTabNumber.Text.Trim();
            string pageRow = this.txtPageRows.Text.Trim();
            string page = this.txtPages.Text.Trim();
            if (this.comboBox2.SelectedIndex <= -1)
            {
                MessageBox.Show("请选择厂别后再查询");
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.butSearch.Enabled = true;
                this.button1.Enabled = true;
                this.button2.Enabled = true;
                this.isbusy = false;

                return;
            }
            this.org_id = this.comboBox2.SelectedItem.ToString();
            if (pageRow == "" && Convert.ToInt32(pageRows) <= 0)
            {
                pageRow = "1000";
                this.txtPageRows.Text = pageRow;
            }
            if (page == "" && Convert.ToInt32(page) <= 0)
            {
                pageRow = "1";
                this.txtPageRows.Text = pageRow;
            }
            this.pageRows = Convert.ToInt32(pageRow);
            this.pages = Convert.ToInt32(page);



            if (tagNumber != "")
            {
                this.org_id = "";
                this.cust_id = "";
                this.checkBox1.Checked = false;
                this.txtPages.Text = "1";

            }
            if (this.org_id == "" && tagNumber == "")
            {
                MessageBox.Show("请选择厂别后再查询");
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.butSearch.Enabled = true;
                this.button1.Enabled = true;
                this.button2.Enabled = true;
                this.isbusy = false;

                return;
            }

            if (!checkBox1.Checked)
            {
                starDate = "";
                stopDate = "";
            }

            DataTable dt = dhm.getDelByS(starDate, stopDate, this.cust_id, tagNumber, this.org_id, this.pageRows, this.pages);
            if (dt.Rows.Count <= 0)
            {
                this.checkBox2.Visible = false;
                this.dgvHURLEY.DataSource = null;
                MessageBox.Show("没有数据");
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.butSearch.Enabled = true;
                this.button1.Enabled = true;
                this.button2.Enabled = true;
                this.isbusy = false;
                return;
            }
            this.checkBox2.Visible = true;
            dt.Columns.Add("select", Type.GetType("System.Boolean")).SetOrdinal(0);
            this.dgvHURLEY.DataSource = null;
            this.dgvHURLEY.DataSource = dt;
            for (int i = 1; i < this.dgvHURLEY.Columns.Count; i++)
            {
                this.dgvHURLEY.Columns[i].ReadOnly = true;
                this.dgvHURLEY.Columns["scantime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            }


            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.butSearch.Enabled = true;
            this.button1.Enabled = true;
            this.button2.Enabled = true;
            this.isbusy = false;
        }

        public int dels = 0;
        private void butDeleteScan_Click(object sender, EventArgs e)
        {
            if(this.dels == 0)
            {
                this.groupBox1.Visible = true;
             
                
            }
            else
            {
                // return;

                DataTable dt = this.dgvrigth.DataSource as DataTable;
                if (dt == null)
                {
                    MessageBox.Show("没有数据");
                    return;

                }
                List<string> ids = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    ids.Add(row["id"].ToString());
                }
                const string message = "确认要删除吗？此删除不可逆!";
                const string caption = "危险操作";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning);


                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    int i = dhm.delTNF_Hurley(ids);
                    MessageBox.Show("删除完成，共删除 " + i.ToString() + " 行数据");

                    this.dgvrigth.DataSource = null;
                    this.dgvHURLEY.DataSource = null;
                    this.dtr.Rows.Clear();
                    this.dgvrigth.Refresh();

                }
                this.dels = 0;
            }
           
           

        }

        private void FrmDelScanHURLEY_Resize(object sender, EventArgs e)
        {
            this.splitContainer1.Width = this.Width - 20;
            this.splitContainer1.Height = this.Height - 100;
            this.checkBox2.Visible = false;
            this.groupBox1.Left = this.Width - this.splitContainer1.Panel1.Width - this.groupBox1.Width - 50;
        }

        private void dgvHURLEY_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            this.txtTabNumber.Text = "";
            DataTable dt = dhm.getCustIDs();
            if (dt.Rows.Count <= 0)
            {

                MessageBox.Show("没有数据");
                return;

            }
            comboBox1.Items.Add("").ToString();
            foreach (DataRow row in dt.Rows)
            {
                comboBox1.Items.Add(row["cust_id"]).ToString();
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex <= -1)
            {
                return;
            }
            this.cust_id = this.comboBox1.SelectedItem.ToString();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedIndex <= -1)
            {
                return;
            }
            this.org_id = this.comboBox2.SelectedItem.ToString();
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {

            comboBox2.Items.Clear();
            this.txtTabNumber.Text = "";

            DataTable dt = dhm.getOrgs();
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据");
                return;

            }
            foreach (DataRow row in dt.Rows)
            {
                comboBox2.Items.Add(row["org"]).ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(this.txtPages.Text);
            if (page <= 1)
            {
                MessageBox.Show("最小不能小于 1 页 ");
                return;
            }
            this.pages = Convert.ToInt32(this.txtPages.Text) - 1;
            this.txtPages.Text = this.pages.ToString();

            if (!isbusy)
            {
                getDels();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(this.txtPages.Text);

            this.pages = Convert.ToInt32(this.txtPages.Text) + 1;
            this.txtPages.Text = this.pages.ToString();
            if (!isbusy)
            {
                getDels();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            this.txtTabNumber.Text = "";
        }
     
        private void dgvHURLEY_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
            else
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {

                    if (dgv.Rows[e.RowIndex].Cells[0].Value.ToString() == "True")
                    {
                        dgv.Rows[e.RowIndex].Cells[0].Value = false;
                        // 从右边删除 
                        delRigthTB(dgv.Rows[e.RowIndex]);
                    }
                    else
                    {
                        dgv.Rows[e.RowIndex].Cells[0].Value = true;
                        // 添加到右边 
                        addRigthTB(dgv.Rows[e.RowIndex]);
                    }
                }
            }
        }
        public void addRigthTB(DataGridViewRow dvr )
        {
            DataRow dr = this.dtr.NewRow();
            dr["id"] = dvr.Cells["id"].Value.ToString();
            dr["TagNumber"] = dvr.Cells["TagNumber"].Value.ToString();
            dr["Cust_id"] = dvr.Cells["Cust_id"].Value.ToString();
            dr["Location"] = dvr.Cells["Location"].Value.ToString();
            dr["org"] = dvr.Cells["org"].Value.ToString();
            dr["scantime"] = dvr.Cells["scantime"].Value.ToString();            
            dr["con_no"] = dvr.Cells["con_no"].Value.ToString();
            dr["create_pc"] = dvr.Cells["create_pc"].Value.ToString();
            this.dtr.Rows.Add(dr);
            this.dgvrigth.DataSource = this.dtr; 
            for (int i = 1; i < this.dgvrigth.Columns.Count; i++)
            {
                this.dgvrigth.Columns["scantime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }
        }

        public void delRigthTB(DataGridViewRow dvr)
        {
           
            if (this.dtr.Rows.Count <= 0)
            {
                return;
            }         
            for(int i = 0; i < this.dtr.Rows.Count; i++)
            {
                if(  dvr.Index >= 0 && this.dtr.Rows[i]["id"].ToString() == dvr.Cells["id"].Value.ToString() )
                {
                    this.dtr.Rows.RemoveAt(i);
                    i--;
                    break;
                }
            }
            this.dgvrigth.DataSource = this.dtr;
        }


        private void dgvHURLEY_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {

            if (this.dgv == null)
            {
                return;
            }


            //  Clipboard.SetDataObject(dgv.CurrentCell.Value.ToString());
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

        private void dgvrigth_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }
          

            for (int i = 0; i < this.dgvHURLEY.Rows.Count; i++)
            {
                string s1 = this.dgvHURLEY.Rows[i].Cells["id"].Value.ToString();
                string s2 = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (  s1 == s2)
                {
                    this.dgvHURLEY.Rows[i].Cells[0].Value = false;
                    break;
                }
            }
            delRigthTB(dgv.Rows[e.RowIndex]);
            


        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.groupBox1.Visible = false;
            this.txtpwd.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool checkedPWD = checkedDelManagerPassword();
            if (!checkedPWD)
            {
                return;
            }
            // System.Threading.Thread.Sleep(1000);
            this.groupBox1.Visible = false;
            this.dels = 1;


        }
        private bool checkedDelManagerPassword()
        {
            string password = this.txtpwd.Text.Trim();
            bool result = false;
            if (this.comboBox2.SelectedIndex <= -1)
            {
                this.label8.Text = "请先选择厂区";
                return false; ;
            }
            this.org_id = this.comboBox2.SelectedItem.ToString();
            //this.groupBox1.Visible = true;

            // String M = Interaction.InputBox("请输入管理密码", "密码保护", "", 100, 100);
            if (this.org_id == "SAA")
            {
                if (password != "S010DDC")
                {
                   // MessageBox.Show("请输入删除密码");
                    this.label8.Text = "管理密码错误";
                    result = false;
                }
                else
                {
                    this.label8.Text = "密码正确";
                    result = true;
                }
            }
            else
            {
                if (password != "TA10CCC")
                {
                   // MessageBox.Show("请输入删除密码");
                    this.label8.Text = "管理密码错误";
                    result = false;
                }
                else
                {
                    this.label8.Text = "密码正确";
                    result = true;
                }
            }
            
            return result;
        }
 

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //  dt = (DataTable)this.dgvHURLEY.DataSource ;
            //  if (dt == null)
            //  {
            //      return;
            //  }

            //    this.tomenuRight(dgv, e);

            if (this.checkBox2.Checked)
            {
                foreach (DataGridViewRow row in this.dgvHURLEY.Rows)
                {
                    row.Cells["select"].Value = true;
                    
                    addRigthTB(row);

                }
            }
            else
            {
                foreach (DataGridViewRow row in this.dgvHURLEY.Rows)
                {
                    row.Cells["select"].Value = false;
                    delRigthTB(row);
                }
            }
           
             

        }

        private void groupBox1_VisibleChanged(object sender, EventArgs e)
        {
            this.groupBox1.Left = this.Width - this.splitContainer1.Panel1.Width -this.groupBox1.Width -50;
        }
    }
}
 
