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
    public partial class FrmNewStyleSearch : Form
    {
        private static FrmNewStyleSearch frm;

        public DataGridView selectDgv = null;
        public int hiedcolumnindex = -1; //是否选中外面
        DataTable SearchDT = new DataTable();
        FrmNewStyleSearchManager NSM = new FrmNewStyleSearchManager();
        DataGridView selecteddgv = null;
        public FrmNewStyleSearch()
        {
            InitializeComponent();
            this.dgvSearch.DoubleBufferedDataGirdView(true);
            this.dgvResultNewStyle.DoubleBufferedDataGirdView(true);
        }
        public static FrmNewStyleSearch GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmNewStyleSearch();
            }
            return frm;
        }
        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            if (this.selecteddgv == null)
            {
                return;
            }
            Clipboard.SetDataObject(this.selecteddgv.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            if (this.selecteddgv == null)
            {
                return;
            }
            Clipboard.SetDataObject(this.selecteddgv.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            if (this.dgvResultNewStyle == null)
            {
                return;
            }
            ImproExcel(this.selecteddgv);
        }
        public void ImproExcel(DataGridView dgv)
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
            tabl = GetDgvToTable(dgv);
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

        private void FrmNewStyleSearch_Resize(object sender, EventArgs e)
        {
            this.gbResult.Width = this.Width - this.gbSearchPO.Width - 30;
            this.gbResult.Left = this.gbSearchPO.Left + this.gbSearchPO.Width + 5;
            this.gbResult.Height = this.Height - 100;
            this.gbSearchPO.Height = this.gbResult.Height;
            this.gbResult.Height = this.gbResult.Height;
        }

        private void FrmNewStyleSearch_MouseLeave(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 100);
            //注销Id号为101的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 101);
        }

        private void FrmNewStyleSearch_MouseEnter(object sender, EventArgs e)
        {
            //注册热键Ctrl+C，Id号为100。。
            HotKeysManager.RegisterHotKey(Handle, 100, HotKeysManager.KeyModifiers.Ctrl, Keys.C);
            //注册热键Ctrl+V，Id号为101。
            HotKeysManager.RegisterHotKey(Handle, 101, HotKeysManager.KeyModifiers.Ctrl, Keys.V);
        }

        private void dgvSearch_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }
            this.tomenuRight(selecteddgv, e);
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
                }

            }

        }
        private void dgvSearch_MouseEnter(object sender, EventArgs e)
        {
            //注册热键Ctrl+C，Id号为100。。
            HotKeysManager.RegisterHotKey(Handle, 100, HotKeysManager.KeyModifiers.Ctrl, Keys.C);
            //注册热键Ctrl+V，Id号为101。
            HotKeysManager.RegisterHotKey(Handle, 101, HotKeysManager.KeyModifiers.Ctrl, Keys.V);
        }

        private void dgvSearch_MouseLeave(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 100);
            //注销Id号为101的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 101);
        }

        private void dgvSearch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvResult_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        private void dgvResult_MouseEnter(object sender, EventArgs e)
        {
            //注册热键Ctrl+C，Id号为100。。
            HotKeysManager.RegisterHotKey(Handle, 100, HotKeysManager.KeyModifiers.Ctrl, Keys.C);
            //注册热键Ctrl+V，Id号为101。
            HotKeysManager.RegisterHotKey(Handle, 101, HotKeysManager.KeyModifiers.Ctrl, Keys.V);
        }

        private void dgvResult_MouseLeave(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 100);
            //注销Id号为101的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 101);
        }

        private void dgvResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 100);
            //注销Id号为101的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 101);
        }

        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
            //注册热键Ctrl+C，Id号为100。。
            HotKeysManager.RegisterHotKey(Handle, 100, HotKeysManager.KeyModifiers.Ctrl, Keys.C);
            //注册热键Ctrl+V，Id号为101。
            HotKeysManager.RegisterHotKey(Handle, 101, HotKeysManager.KeyModifiers.Ctrl, Keys.V);
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

        private void butClear_Click(object sender, EventArgs e)
        {
            this.SearchDT.Clear();
            this.dgvSearch.DataSource = null;
            this.dgvResultNewStyle.DataSource = null;
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            /*
           // m.WParam.ToInt32() 要和 注册热键时的第2个参数一样
           if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == 101) //判断热键
           {
               MessageBox.Show("1");
           }
           base.WndProc(ref m);

            */

            //按快捷键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:

                            // Clipboard.SetText(this.dgvSearch.Rows[0].Cells[0].Value.ToString());
                            break;
                        case 101:
                            this.pasteData();
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);

        }

        private void butPaste_Click(object sender, EventArgs e)
        {
            this.pasteData();
        }
        public void pasteData()
        {
            if (SearchDT.Columns.Count > 0)
            {
                SearchDT.Rows.Clear();
                SearchDT.Columns.Remove("style_id");
            }

            try
            {
                string clipboardText = Clipboard.GetText(); //获取剪贴板中的内容
                if (string.IsNullOrEmpty(clipboardText))
                {
                    MessageBox.Show("剪贴板中无内容");
                    return;
                }
                int colnum = 0;
                int rownum = 0;
                for (int i = 0; i < clipboardText.Length; i++)
                {
                    if (clipboardText.Substring(i, 1) == "\t")
                    {
                        colnum++;
                    }
                    if (clipboardText.Substring(i, 1) == "\n")
                    {
                        rownum++;
                    }
                }
                //粘贴板上的数据来源于EXCEL时，每行末尾都有\n，来源于DataGridView是，最后一行末尾没有\n
                if (clipboardText.Substring(clipboardText.Length - 1, 1) == "\n")
                {
                    rownum--;
                }
                colnum = colnum / (rownum + 1);
                object[,] data; //定义object类型的二维数组
                data = new object[rownum + 1, colnum + 1];  //根据剪贴板的行列数实例化数组
                SearchDT.Columns.Add("style_id");
                string rowStr = "";

                //对数组各元素赋值
                for (int i = 0; i <= rownum; i++)
                {
                    DataRow dr = SearchDT.NewRow();
                    for (int j = 0; j <= colnum; j++)
                    {
                        //一行中的其它列
                        if (j != colnum)
                        {
                            rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\t"));
                            clipboardText = clipboardText.Substring(clipboardText.IndexOf("\t") + 1);
                            dr["style_id"] = rowStr;
                        }
                        //一行中的最后一列
                        if (j == colnum && clipboardText.IndexOf("\r") != -1)
                        {
                            rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\r"));
                            dr["style_id"] = rowStr;
                        }
                        //最后一行的最后一列
                        if (j == colnum && clipboardText.IndexOf("\r") == -1)
                        {
                            rowStr = clipboardText.Substring(0);
                        }
                        //  dr["style"]=
                        data[i, j] = rowStr;
                    }
                    //截取下一行及以后的数据
                    clipboardText = clipboardText.Substring(clipboardText.IndexOf("\n") + 1);
                    SearchDT.Rows.Add(dr);
                }

                this.dgvSearch.DataSource = SearchDT;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }
        private void butSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.butClear.Enabled = false;
            this.butPaste.Enabled = false;
            this.butSearch.Enabled = false;
            this.butExport.Enabled = false;

            if (this.SearchDT.Rows.Count <= 0)
            {
                MessageBox.Show("请先粘贴要查询的月BUY,款号。谢谢！");
                this.Cursor = Cursors.Default;
                this.butClear.Enabled = true;
                this.butPaste.Enabled = true;
                this.butSearch.Enabled = true;
                this.butExport.Enabled = true;
                return;
            }
            string yymm = this.txtyymm.Text;
            DataTable resultdtall = NSM.getNewStyleByMynoDate(this.SearchDT, yymm);
            if (resultdtall is null)
            {
                MessageBox.Show("没有资料，请重试。谢谢！");
                this.Cursor = Cursors.Default;
                this.butClear.Enabled = true;
                this.butPaste.Enabled = true;
                this.butSearch.Enabled = true;
                this.butExport.Enabled = true;
                return;
            }

            if (resultdtall.Rows.Count <= 0)
            {
                this.dgvResultNewStyle.DataSource = null;
                MessageBox.Show("没有资料");
            }
            else
            {
                foreach (DataRow dr in resultdtall.Rows)
                {
                    if(dr["old_Style_my_no"].ToString() == "")
                    {
                        dr["old_Style_my_no"] ="新款式";
                    }
                    if (dr["old_Style_Color_my_no"].ToString() == "")
                    {
                        dr["old_Style_Color_my_no"] = "新颜色";
                    }
                }
                this.dgvResultNewStyle.DataSource = resultdtall;
                this.changHeaderText();
                //  this.dgvResultNewColor.Columns[2].Width = 300;
                //ataGridView1.Columns[1].FillWeight = 30
            }
            this.butClear.Enabled = true;
            this.butPaste.Enabled = true;
            this.butSearch.Enabled = true;
            this.butExport.Enabled = true;




            this.Cursor = Cursors.Default;
        }

        private void butExport_Click(object sender, EventArgs e)
        {
            if (this.dgvResultNewStyle == null)
            {
                return;
            }
            ImproExcel(this.dgvResultNewStyle);
        }

        private void dgvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            string yymm = this.txtyymm.Text;
            if(e.KeyCode ==Keys.Enter)
            {
                DataTable resultdtall = NSM.getNewStyleByMynoDate(this.SearchDT , yymm);
            }

        }

        public void changHeaderText()
        {
            this.dgvResultNewStyle.Columns["be_id"].HeaderText = "厂区ID";
            this.dgvResultNewStyle.Columns["cust_id"].HeaderText = "客户";
            this.dgvResultNewStyle.Columns["cust_id"].Visible = false;

            this.dgvResultNewStyle.Columns["season_id"].HeaderText = "季节";
            this.dgvResultNewStyle.Columns["yymm"].HeaderText = "月BUY";
            this.dgvResultNewStyle.Columns["buy_cname"].HeaderText = "月BUY中文名";
            this.dgvResultNewStyle.Columns["buy_cname"].Visible = false;

            this.dgvResultNewStyle.Columns["style_id"].HeaderText = "款式";
            this.dgvResultNewStyle.Columns["clr_no"].HeaderText = "色组";
            this.dgvResultNewStyle.Columns["qty"].HeaderText = "数量";
            this.dgvResultNewStyle.Columns["my_no"].HeaderText = "自编单号";
            this.dgvResultNewStyle.Columns["type_id"].HeaderText = "订单类别ID";
            this.dgvResultNewStyle.Columns["type_id"].Visible = false;

            this.dgvResultNewStyle.Columns["type_name"].HeaderText = "订单类别";
            this.dgvResultNewStyle.Columns["old_Style_my_no"].HeaderText = "老款式自编单号";
            this.dgvResultNewStyle.Columns["old_Style_Color_my_no"].HeaderText = "老款式老颜色自编单号";
            this.dgvResultNewStyle.Columns["old_season_id"].HeaderText = "老款式自编单号季节";

        }
    }
}
