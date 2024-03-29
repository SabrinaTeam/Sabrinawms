﻿using BLL;
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
using org.omg.PortableServer;

namespace WinForm
{
    public partial class FrmSizeRun : Form
    {
        private static FrmSizeRun frm;
        public sizeRunManager sizem = new sizeRunManager();
        public DataGridView dgv = null;

        public FrmSizeRun()
        {
            InitializeComponent();
            dgvSizeRun.DoubleBufferedDataGirdView(true);
            dgvSizeRunAll.DoubleBufferedDataGirdView(true);
        }

        public static FrmSizeRun GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmSizeRun();
            }
            return frm;
        }

        private void FrmSizeRun_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.gbSearch.Width = this.Width - 20;
            this.gbSize.Width = this.gbSearch.Width;
         //   this.gbPO.Width = this.gbSearch.Width;
            //this.gbPO.Height = this.Height - 335;
           // this.txtLogs.Visible = false;
            this.gbLogs.Visible = false;
            this.ckLogs.Left = this.gbSize.Width - this.ckLogs.Width -20 ;
            this.getCustAbbr();


        }

        private void FrmSizeRun_Resize(object sender, EventArgs e)
        {
            if (this.Width <= 620)
            {
                this.Width = 620;
            }
            if (this.Height <= 490)
            {
                this.Height = 490;
            }

            this.gbSearch.Width = this.Width - 20;
            this.gbSize.Width = this.gbSearch.Width  ;
            this.gbSize.Height = this.Height - this.gbSearch.Height - 50;
            this.gbLogs.Height = this.gbSize.Height;
        }
        public bool isbusy = false;
        private void btSearch_Click(object sender, EventArgs e)
        {

            if (!this.isbusy)
            {
                this.getByMyno();
            }

        }
        public void getByMyno()
        {
            Cursor = Cursors.WaitCursor;
            this.isbusy = true;
            this.btSearch.Enabled = false;
            string my_no = this.txtMyNumber.Text.Trim();
            string yymm = this.txtYYMM.Text.Trim();
            string my_style = this.txtStyle.Text.Trim();
            string cust_abbr = this.txtcust_abbr.Text.Trim();
            bool onlyStyle = false;
            if (my_no.Length <= 0 && my_style.Length <=0 && yymm.Length <= 0)
            {
                Cursor = Cursors.Default;
                this.isbusy = false;
                this.btSearch.Enabled = true;
                return;
            }

            this.txtLogs.Text = "";
            this.txtLogs.Visible = false;
            TestLinManager tl = new TestLinManager();
            string serverIP = "192.168.0.254";
            string linkServer = "";
            bool LinkSuccess = false;
            bool testlink = tl.LinServer(serverIP);
            if (testlink)
            {
                linkServer = "BESTconnStr"; //BESTconnStr_KM
                LinkSuccess = true;
            }
            if (!LinkSuccess)
            {
                serverIP = "192.168.4.122";
                testlink = tl.LinServer(serverIP);
                if (testlink)
                {
                    linkServer = "BESTconnStr_KM";
                    LinkSuccess = true;

                }
            }
            if (!LinkSuccess)
            {
                MessageBox.Show("连接服务器 " + serverIP + "失败！请找IT确认网络服务是否OK", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
                this.isbusy = false;
                this.btSearch.Enabled = true;
                return;
            }


            if (my_no.Length <= 0)
            {
                my_no = null;

            }
            if (yymm.Length <= 0)
            {
                yymm = null;

            }
            if (my_style.Length <= 0)
            {
                my_style = null;
            }
            if (cust_abbr.Length <= 0)
            {
                cust_abbr = null;
            }
            if (my_no == null &&  my_style != null)
            {
               // this.gbPO.Visible = false;
                onlyStyle = true;
                this.gbSize.Height =  this.Height-150;

            }
            else
            {
               // this.gbPO.Visible = true;
                onlyStyle = false;
               // this.gbSize.Height = this.Height - this.gbPO.Height - 150;
            }


            string[] parameters = { my_no, yymm, my_style, cust_abbr };

            // 查询色组
            DataTable clr_dt = sizem.getClr_noByMy_no(parameters, linkServer);
            if (clr_dt.Rows.Count <= 0)
            {
                this.txtLogs.Visible = true;
                this.ckLogs.Checked =  true;
                this.txtLogs.AppendText("没有色组数据，请确认自编单号或款式是否正确 \r\n");


            //    MessageBox.Show("没有色组数据，请确认自编单号或款式是否正确");
                Cursor = Cursors.Default;
                this.isbusy = false;
                this.btSearch.Enabled = true;
                return;
            }
            // 查询size组
            string my_nos = "";
            string myno = "";
            foreach (DataRow dr in clr_dt.Rows)
            {
                myno = dr["my_no"].ToString().Replace("'","''");
                   my_nos = my_nos + "'" + myno + "',";
            }
            my_nos = my_nos.Substring(0, my_nos.Length-1);


            DataTable size_dt = sizem.getSizeByMy_no(my_nos,linkServer);
            if (size_dt.Rows.Count <= 0)
            {
                this.txtLogs.Visible = true;
                this.ckLogs.Checked = true;
                this.txtLogs.AppendText("没有size组数据，请确认自编单或款式号是否正确 \r\n");

               // MessageBox.Show("没有size组数据，请确认自编单或款式号是否正确");
                Cursor = Cursors.Default;
                this.isbusy = false;
                this.btSearch.Enabled = true;
                return;
            }
            DataTable sizeRunDT = new DataTable();
            List< string> sizes = new List<string>() ;
            for (int z = 0; z < size_dt.Rows.Count; z++)
            {
                for (int i = 1; i < size_dt.Columns.Count; i++)
                {
                    sizes.Add(size_dt.Rows[z][i].ToString());
                    //sizeRunDT.Columns.Add(size_dt.Rows[z][i].ToString());

                }
            }
            sizes= sizes.Distinct().ToList();
            for(int i = 0; i < sizes.Count; i++)
            {
                if (sizes[i] == "")
                {
                    sizes.RemoveAt(i);
                    i--;
                }


            }
            foreach(string s in sizes)
            {
                sizeRunDT.Columns.Add(s);
            }
            sizeRunDT.Columns.Add("my_no");
            sizeRunDT.Columns["my_no"].SetOrdinal(0);
            sizeRunDT.Columns.Add("style_id");
            sizeRunDT.Columns["style_id"].SetOrdinal(1);
            sizeRunDT.Columns.Add("cust_id");
            sizeRunDT.Columns["cust_id"].SetOrdinal(2);
            sizeRunDT.Columns.Add("clr_no");
            sizeRunDT.Columns["clr_no"].SetOrdinal(3);
            // 生成表SizeRun框架
            for (int i = 0; i < clr_dt.Rows.Count; i++)
            {
                DataRow dr = sizeRunDT.NewRow();//定义一个新行
                dr["clr_no"] = clr_dt.Rows[i]["clr_no"].ToString();
                dr["style_id"] = clr_dt.Rows[i]["style_id"].ToString();
                dr["my_no"] = clr_dt.Rows[i]["my_no"].ToString();
                dr["cust_id"] = clr_dt.Rows[i]["cust_id"].ToString();
                sizeRunDT.Rows.Add(dr);
            }

            // 生成表框架
            for (int z = 0; z < size_dt.Rows.Count; z++)
            {


                //查询sizeRun数量
                DataTable sizeRunCount_dt = sizem.getSizeRunByMy_no(size_dt.Rows[z]["my_no"].ToString(),linkServer);
                if (sizeRunCount_dt.Rows.Count <= 0)
                {
                    this.txtLogs.Visible = true;
                    this.ckLogs.Checked = true;
                    this.txtLogs.AppendText("  没有 " + size_dt.Rows[z]["my_no"].ToString() + " 的 sizeRun 数据,请确认自编单号是否正确...\r\n");

                 //   MessageBox.Show("没有 " + size_dt.Rows[z]["my_no"].ToString()+" 的sizeRun数量数据，请确认自编单号是否正确");
                //    Cursor = Cursors.Default;
                   // this.isbusy = false;
                   // this.btSearch.Enabled = true;
                  //  return;
                  continue;

                }

                // 填入数量
                string clr_Size = ""; // 这行的色组的SIZE
                for (int i = 0; i < sizeRunDT.Rows.Count; i++)
                {
                    for (int j = 0; j < sizeRunCount_dt.Rows.Count; j++)
                    {

                        if (sizeRunCount_dt.Rows[j]["my_no"].ToString() == sizeRunDT.Rows[i]["my_no"].ToString())
                        {

                            if (sizeRunCount_dt.Rows[j]["clr_no"].ToString() == sizeRunDT.Rows[i]["clr_no"].ToString())
                            {

                                if (sizeRunCount_dt.Rows[j]["clr_no"].ToString() == sizeRunDT.Rows[i]["clr_no"].ToString())
                                {
                                    clr_Size = sizeRunCount_dt.Rows[j]["size_code"].ToString();
                                    for (int k = 0; k < sizeRunDT.Columns.Count; k++)
                                    {
                                        if (clr_Size == sizeRunDT.Columns[k].ColumnName.ToString())
                                        {
                                            sizeRunDT.Rows[i][k] = Convert.ToDouble(sizeRunCount_dt.Rows[j]["qty"].ToString()).ToString();

                                        }
                                    }

                                }

                            }
                        }
                    }

                }
              //  DataTable alldt = new DataTable();
               // alldt.Rows

            }



                //添加一列合计
                sizeRunDT.Columns.Add("count");
                // 添加一行合计
                DataRow countdr = sizeRunDT.NewRow();//定义一个新行
                countdr["my_no"] = "Count";
                sizeRunDT.Rows.Add(countdr);

                // 计算行合计
                string rowCountstr = "";
                for (int i = 0; i < sizeRunDT.Rows.Count - 1; i++)
                {
                    double rowCount = 0;
                    for (int j = 4; j < sizeRunDT.Columns.Count-1; j++)
                    {
                        rowCountstr = sizeRunDT.Rows[i][j].ToString();
                        if (rowCountstr.Length > 0)
                        {
                            rowCount = rowCount + Convert.ToDouble(rowCountstr);
                        }
                        sizeRunDT.Rows[i][sizeRunDT.Columns.Count-1] = rowCount.ToString();
                    }
                }
                // 计算列合计
                rowCountstr = "";
                for (int i = 4; i < sizeRunDT.Columns.Count; i++)
                {
                    double rowCount = 0;
                    for (int j = 0; j < sizeRunDT.Rows.Count - 1; j++)
                    {
                        rowCountstr = sizeRunDT.Rows[j][i].ToString();
                        if (rowCountstr.Length > 0)
                        {
                            rowCount = rowCount + Convert.ToDouble(rowCountstr);
                        }

                    }
                    sizeRunDT.Rows[sizeRunDT.Rows.Count - 1][i] = rowCount.ToString();
                }


                this.dgvSizeRun.DataSource = sizeRunDT;
                // 禁用排序
                for (int i = 0; i < this.dgvSizeRun.Columns.Count; i++)
                {
                    this.dgvSizeRun.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

            if (!onlyStyle)
            {
                //所有原始资料
                DataTable allSizerun = sizem.getAllSizeRunByMy_no(my_no, linkServer);
                if (allSizerun.Rows.Count <= 0)
                {
                    this.txtLogs.Visible = true;
                    this.ckLogs.Checked = true;
                    this.txtLogs.AppendText("没有详细数据，请确认自编单号是否正确 \r\n");

                //    MessageBox.Show("没有详细数据，请确认自编单号是否正确");
                    Cursor = Cursors.Default;
                    this.isbusy = false;
                    this.btSearch.Enabled = true;
                    return;
                }

                this.dgvSizeRunAll.DataSource = allSizerun;
            }


            Cursor = Cursors.Default;
            this.btSearch.Enabled = true;
            this.isbusy = false;
        }
        public void getCustAbbr()
        {
            TestLinManager tl = new TestLinManager();
            string serverIP = "192.168.0.254";
            string linkServer = "";
            bool LinkSuccess = false;
            bool testlink = tl.LinServer(serverIP);
            if (testlink)
            {
                linkServer = "BESTconnStr"; //BESTconnStr_KM
                LinkSuccess = true;
            }
            if (!LinkSuccess)
            {
                serverIP = "192.168.4.122";
                testlink = tl.LinServer(serverIP);
                if (testlink)
                {
                    linkServer = "BESTconnStr_KM";
                    LinkSuccess = true;

                }
            }
            if (!LinkSuccess)
            {
                MessageBox.Show("连接服务器 " + serverIP + "失败！请找IT确认网络服务是否OK", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
                this.isbusy = false;
                this.btSearch.Enabled = true;
                return;
            }
           DataTable Cust_Abbr =  sizem.getCustAbbr(  linkServer);

            List<string> strList = new List<string>();
            if (Cust_Abbr != null && Cust_Abbr.Rows.Count >0)
            {
                
               // this.cbcust_abbr.Items.Clear();
                foreach (DataRow dr in Cust_Abbr.Rows)
                {
                //    this.cbcust_abbr.Items.Add(dr["cust_abbr"].ToString());
                    strList.Add(dr["cust_abbr"].ToString());
                }
                
             //   string[] source = strList.ToArray();
                var source = new AutoCompleteStringCollection();
                source.AddRange(strList.ToArray());

                this.txtcust_abbr.AutoCompleteCustomSource = source;
                this.txtcust_abbr.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtcust_abbr.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }

        }


        private void RmeCopyCells_Click(object sender, EventArgs e)
        {


              if (this.dgv  == null)
             {
                 return;
              }


            //  Clipboard.SetDataObject(dgv.CurrentCell.Value.ToString());
            Clipboard.SetDataObject(this.dgv.CurrentCell.Value.ToString());
        }


        public int hiedcolumnindex = -1; //是否选中外面
        private void dgvSizeRun_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
           this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }

            this.tomenuRight(dgv,e);

        }

        private void dgvSizeRun_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
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

        private void txtMyNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getByMyno();

            }
        }

        private void dgvSizeRunAll_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvSizeRunAll_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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
                    MenuRight.Show(MousePosition.X, MousePosition.Y);

                }
            }
        }

        private void ckLogs_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckLogs.Checked)
            {
                this.gbSize.Width = this.Width - this.gbLogs.Width - 30;
                this.gbLogs.Left = this.gbSize.Width + 10;
                this.gbLogs.Top = this.gbSize.Top ;
                this.gbLogs.Visible = true;
            }
            else
            {
                this.gbSize.Width = this.gbSearch.Width;
                this.gbLogs.Visible = false;
            }
        }

        private void gbSize_Resize(object sender, EventArgs e)
        {
            this.ckLogs.Left = this.gbSize.Width - this.ckLogs.Width - 20;
        }

        private void gbLogs_Resize(object sender, EventArgs e)
        {
            this.txtLogs.Width = this.gbLogs.Width - 20;
            this.txtLogs.Height = this.gbLogs.Height - 40;
        }
    }
}



