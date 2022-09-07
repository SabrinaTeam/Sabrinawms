using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmSingleWeight : Form
    {
        public bool isbusy = false;
        public FrmSingleWeightMaintainManager fwm = new FrmSingleWeightMaintainManager();
        private static FrmSingleWeight frm;
        private static FrmMain mainfrom;
        DataTable singWeightDB = new DataTable();
        public FrmSingleWeightManager swm = new FrmSingleWeightManager();

        DataGridView selecteddgv = null;

        public int hiedcolumnindex = -1;
        public FrmSingleWeight()
        {
            InitializeComponent();
            initSingleWeightDB();
              this.dgvSingleWeight.DoubleBufferedDataGirdView(true);
            // this.dgvResultNewStyle.DoubleBufferedDataGirdView(true);
        }
        public void initSingleWeightDB()
        {
            DataColumn ID = new DataColumn();
            ID.ColumnName = "ID";
            ID.DefaultValue = -1;
            singWeightDB.Columns.Add(ID);

            DataColumn custID = new DataColumn();
            custID.ColumnName = "custID";
            singWeightDB.Columns.Add(custID);

            DataColumn StyleID = new DataColumn();
            StyleID.ColumnName = "StyleID";
            singWeightDB.Columns.Add(StyleID);

            DataColumn SizeID = new DataColumn();
            SizeID.ColumnName = "SizeID";
            singWeightDB.Columns.Add(SizeID);

            DataColumn singleWeight = new DataColumn();
            singleWeight.ColumnName = "singleWeight";
            singWeightDB.Columns.Add(singleWeight);

            DataColumn Note = new DataColumn();
            Note.ColumnName = "Note";
            singWeightDB.Columns.Add(Note);

            DataColumn CreateUser = new DataColumn();
            CreateUser.ColumnName = "CreateUser";
            singWeightDB.Columns.Add(CreateUser);

            DataColumn CreateDate = new DataColumn();
            CreateDate.ColumnName = "CreateDate";
            singWeightDB.Columns.Add(CreateDate);

            DataColumn LastModified = new DataColumn();
            LastModified.ColumnName = "LastModified";
            singWeightDB.Columns.Add(LastModified);

            DataColumn LastModifyDate = new DataColumn();
            LastModifyDate.ColumnName = "LastModifyDate";
            singWeightDB.Columns.Add(LastModifyDate);

            this.dgvSingleWeight.DataSource = singleWeight;
        }
        public static FrmSingleWeight GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmSingleWeight();
            }
            return frm;
        }

        private void FrmSingleWeight_Load(object sender, EventArgs e)
        {
            this.gbModify.Enabled = false;
            this.getAllCustID();
            this.getAllStyleID();
            this.getAllSizeID();
            this.dgvSingleWeight.ReadOnly = true;


        }

        private void ButBoxBase_Click(object sender, EventArgs e)
        {
            FrmSingleWeightMaintain frm = FrmSingleWeightMaintain.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();


        }

        private void FrmSingleWeight_Resize(object sender, EventArgs e)
        {
            gbMenu.Width = this.Width - gbFunction.Width - 40;
            gbModify.Width = gbMenu.Width;
            gbWeight.Width = gbMenu.Width;
            gbWeight.Height = this.Height - gbMenu.Height - gbModify.Height - 60;
            gbFunction.Height = this.Height - 50;
            gbFunction.Left = this.gbMenu.Width + 10;
        }
        /// <summary>
        /// 获取系统所有客户
        /// </summary>
        public void getAllCustID()
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
                this.ButSearch.Enabled = true;
                return;
            }
            DataTable Cust_Abbr = fwm.getAllCustidByBaseDB(linkServer);

            List<string> strList = new List<string>();
            if (Cust_Abbr != null && Cust_Abbr.Rows.Count > 0)
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

                this.txtCust_ID.AutoCompleteCustomSource = source;
                this.txtCust_ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtCust_ID.AutoCompleteSource = AutoCompleteSource.CustomSource;

                this.txtModify_CustID.AutoCompleteCustomSource = source;
                this.txtModify_CustID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtModify_CustID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }

        }

        /// <summary>
        /// 获取系统所有客户
        /// </summary>
        public void getAllStyleID()
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
                this.ButSearch.Enabled = true;
                return;
            }
            DataTable AllStyle = fwm.getAllStyleIDByBaseDB(linkServer);

            List<string> strList = new List<string>();
            if (AllStyle != null && AllStyle.Rows.Count > 0)
            {

                // this.cbcust_abbr.Items.Clear();
                foreach (DataRow dr in AllStyle.Rows)
                {
                    //    this.cbcust_abbr.Items.Add(dr["cust_abbr"].ToString());
                    strList.Add(dr["style_id"].ToString());
                }

                //   string[] source = strList.ToArray();
                var source = new AutoCompleteStringCollection();
                source.AddRange(strList.ToArray());

                this.txtStyle_ID.AutoCompleteCustomSource = source;
                this.txtStyle_ID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtStyle_ID.AutoCompleteSource = AutoCompleteSource.CustomSource;

                this.txtModify_StyleID.AutoCompleteCustomSource = source;
                this.txtModify_StyleID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtModify_StyleID.AutoCompleteSource = AutoCompleteSource.CustomSource;


            }

        }
        /// <summary>
        /// 获取系统所有客户
        /// </summary>
        public void getAllSizeID()
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
                this.ButSearch.Enabled = true;
                return;
            }
            DataTable AllSizes = fwm.getAllSizeIDByBaseDB(linkServer);

            List<string> strList = new List<string>();
            if (AllSizes != null && AllSizes.Rows.Count > 0)
            {

                // this.cbcust_abbr.Items.Clear();
                foreach (DataRow dr in AllSizes.Rows)
                {
                    //    this.cbcust_abbr.Items.Add(dr["cust_abbr"].ToString());
                    strList.Add(dr["t_change"].ToString());
                }

                //   string[] source = strList.ToArray();
                var source = new AutoCompleteStringCollection();
                source.AddRange(strList.ToArray());

                this.txtModify_SizesID.AutoCompleteCustomSource = source;
                this.txtModify_SizesID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtModify_SizesID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }

        }

        private void butCreate_Click(object sender, EventArgs e)
        {
            if (!this.gbModify.Enabled)
            {
                this.gbModify.Enabled = true;
            }
            else
            {
                this.addSingleWeight();
                //  this.dgvSingleWeight.Rows.Add();
            }

        }

        private void butSubmit_Click(object sender, EventArgs e)
        {
               this.saveSingleWeight();
        }
        public void addSingleWeight()
        {
            if(this.dgvSingleWeight.Rows.Count <= 0)
            {
                DataRow dr = singWeightDB.NewRow();
                dr["ID"] = -1;
                dr["custID"] = txtModify_CustID.Text.Trim().ToUpper();
                dr["StyleID"] = txtModify_StyleID.Text.Trim().ToUpper();
                dr["SizeID"] = txtModify_SizesID.Text.Trim().ToUpper();
                dr["singleWeight"] = txtModify_Weight.Text.Trim().ToUpper();
                dr["Note"] = txtNote.Text.Trim();
                dr["CreateUser"] = Dns.GetHostName();
                dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["LastModified"] = Dns.GetHostName();
                dr["LastModifyDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                singWeightDB.Rows.Add(dr);
                this.dgvSingleWeight.DataSource = singWeightDB;
                this.dgvSingleWeight.Refresh();
            }
            else
            {
                DataTable dt = GetDgvToTable(this.dgvSingleWeight);
                DataRow dr = dt.NewRow();
                dr["ID"] = -1;
                dr["custID"] = txtModify_CustID.Text.Trim().ToUpper();
                dr["StyleID"] = txtModify_StyleID.Text.Trim().ToUpper();
                dr["SizeID"] = txtModify_SizesID.Text.Trim().ToUpper();
                dr["singleWeight"] = txtModify_Weight.Text.Trim().ToUpper();
                dr["Note"] = txtNote.Text.Trim();
                dr["CreateUser"] = Dns.GetHostName();
                dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["LastModified"] = Dns.GetHostName();
                dr["LastModifyDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                dt.Rows.Add(dr);
                this.dgvSingleWeight.DataSource = dt;
                this.dgvSingleWeight.Refresh();
            }
        }

        private void ButSearch_Click(object sender, EventArgs e)
        {
            string custid = this.txtCust_ID.Text.Trim();
            string styleID =  this.txtStyle_ID.Text.Trim();
            this.getSingleWeight(custid,styleID);
        }
        public void getSingleWeight(string custid,string styleid)
        {
           DataTable dt =   swm.getSingleWeights(custid,styleid);
            this.dgvSingleWeight.DataSource = null;
            if(dt != null && dt.Rows.Count > 0)
            {
                this.dgvSingleWeight.DataSource= dt;
                this.dgvSingleWeight.Columns["CreateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                this.dgvSingleWeight.Columns["LastModifyDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }
            MessageBox.Show("查询完成");
        }

        private void dgvSingleWeight_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvSingleWeight.ReadOnly = false;
            this.dgvSingleWeight.Columns[0].ReadOnly = true;
            this.dgvSingleWeight.Columns[6].ReadOnly = true;
            this.dgvSingleWeight.Columns[7].ReadOnly = true;
            this.dgvSingleWeight.Columns[8].ReadOnly = true;
            this.dgvSingleWeight.Columns[9].ReadOnly = true;
        }
        public void saveSingleWeight()
        {
           DataTable dt = GetDgvToTable(this.dgvSingleWeight);

           int insetRows=    swm.saveSingleWeightDBtoDatabase(dt);
           string msg = "共更新 " +insetRows.ToString()+" 行数据";
           MessageBox.Show(msg);
        }

        private void butRepeal_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = this.dgvSingleWeight.CurrentRow;
            if (row != null) return;
            int index = row.Index ;
            if (index < 0) return;
            string id = this.dgvSingleWeight.Rows[index].Cells["ID"].Value.ToString();

            this.isDelRows(id);
        }
        public void isDelRows(string id)
        {
            if(Convert.ToInt32(id) <= 0) return;
            int delRows = swm.isDelRows(Convert.ToInt32(id));
            if (delRows > 0)
            {
                MessageBox.Show("id："+ id.ToString() + " 已删除");
                string custid = this.txtCust_ID.Text.Trim();
                string styleID = this.txtStyle_ID.Text.Trim();
                this.getSingleWeight(custid,styleID);
            }
            else
            {
                MessageBox.Show("删除失败");
            }


        }

        private void butModify_Click(object sender, EventArgs e)
        {

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

        private void dgvSingleWeight_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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
                    // MenuRight.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void dgvSingleWeight_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
            if (this.selecteddgv == null)
            {
                return;
            }
            ImproExcel(this.selecteddgv);
        }

        public void ImproExcel(DataGridView dgv)
        {
            string sheetname = "SAA-"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");



            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.FileName = sheetname + ".xlsx";
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";

            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            NPOIShippingPackgeExcelHelper NPOIexcel = new NPOIShippingPackgeExcelHelper();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(dgv);

            // DataTable dt = (StyleCodeInfodataGridView.DataSource as DataTable);
            NPOIexcel.ExcelWrite(filename, tabl, sheetname);//excelhelper写出
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

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvSingleWeight.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.dgvSingleWeight.Rows.Remove(row);
                }
            }
        }



        private void SingleWeight_Click(object sender, EventArgs e)
        {
            FrmSingleWeightMaintain frm = FrmSingleWeightMaintain.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }

        private void ButPackingBase_Click(object sender, EventArgs e)
        {

            FrmPackingBase frm = FrmPackingBase.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }

        private void ButSingleWeight_Click(object sender, EventArgs e)
        {
            FrmSingleWeight frm = FrmSingleWeight.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }

        private void ButBoxWeight_Click(object sender, EventArgs e)
        {
            FrmSingleWeightMaintain frm = FrmSingleWeightMaintain.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }

        private void butPackBase_Click(object sender, EventArgs e)
        {
            FrmPackingBase frm = FrmPackingBase.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }
    }
}

