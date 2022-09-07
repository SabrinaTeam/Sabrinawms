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
    public partial class FrmSingleWeightMaintain : Form
    {
        private static FrmSingleWeightMaintain frm;
        public FrmBoxWeightManager fbm = new FrmBoxWeightManager();
        DataTable BoxWeightDB = new DataTable();

        public bool isbusy = false;
        public int hiedcolumnindex = -1;
        DataGridView selecteddgv = null;
        public FrmSingleWeightMaintain()
        {
            InitializeComponent();
        }
        public static FrmSingleWeightMaintain GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmSingleWeightMaintain();
            }
            return frm;
        }
        private void FrmSingleWeightMaintain_Load(object sender, EventArgs e)
        {
            this.initialization();
            this.initBoxWeightDB();
            this.dgvBoxsBase.DoubleBufferedDataGirdView(true);
        }
        public void initBoxWeightDB()
        {
            DataColumn ID = new DataColumn();
            ID.ColumnName = "ID";
            ID.DefaultValue = -1;
            BoxWeightDB.Columns.Add(ID);

            DataColumn box_name = new DataColumn();
            box_name.ColumnName = "box_name";
            BoxWeightDB.Columns.Add(box_name);

            DataColumn box_weight = new DataColumn();
            box_weight.ColumnName = "box_weight";
            BoxWeightDB.Columns.Add(box_weight);

            DataColumn box_l = new DataColumn();
            box_l.ColumnName = "box_l";
            BoxWeightDB.Columns.Add(box_l);

            DataColumn box_w = new DataColumn();
            box_w.ColumnName = "box_w";
            BoxWeightDB.Columns.Add(box_w);

            DataColumn box_h = new DataColumn();
            box_h.ColumnName = "box_h";
            BoxWeightDB.Columns.Add(box_h);

            DataColumn cust_id = new DataColumn();
            cust_id.ColumnName = "cust_id";
            BoxWeightDB.Columns.Add(cust_id);

            DataColumn season = new DataColumn();
            season.ColumnName = "season";
            BoxWeightDB.Columns.Add(season);

            DataColumn Remark = new DataColumn();
            Remark.ColumnName = "Remark";
            BoxWeightDB.Columns.Add(Remark);

            DataColumn CreateUser = new DataColumn();
            CreateUser.ColumnName = "CreateUser";
            BoxWeightDB.Columns.Add(CreateUser);

            DataColumn CreateDate = new DataColumn();
            CreateDate.ColumnName = "CreateDate";
            BoxWeightDB.Columns.Add(CreateDate);

            DataColumn LastModified = new DataColumn();
            LastModified.ColumnName = "LastModified";
            BoxWeightDB.Columns.Add(LastModified);

            DataColumn LastModifyDate = new DataColumn();
            LastModifyDate.ColumnName = "LastModifyDate";
            BoxWeightDB.Columns.Add(LastModifyDate);

            this.dgvBoxsBase.DataSource = BoxWeightDB;
        }
        private void initialization() {
            this.gbModify.Enabled = false;
            this.getAllCustID();
            this.getAllBoxNames();
            this.dgvBoxsBase.ReadOnly = true;
        }

        private void FrmSingleWeightMaintain_Resize(object sender, EventArgs e)
        {
            gbMenu.Width = this.Width - gbFunction.Width - 40;
            gbModify.Width = gbMenu.Width;
            gbMainTain.Width = gbMenu.Width;
            gbMainTain.Height = this.Height - gbMenu.Height - gbModify.Height - 60;
            gbFunction.Height = this.Height - 50;
            gbFunction.Left = this.gbMenu.Width + 10;

        }

        /// <summary>
          /// 获取系统所有客户
          /// </summary>


        private void ButBoxBase_Click(object sender, EventArgs e)
        {
            FrmSingleWeight frm = FrmSingleWeight.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();

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
            DataTable Cust_Abbr = fbm.getAllCustidByBaseDB(linkServer);

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
        /// 获取系统所有外箱类型
        /// </summary>
        public void getAllBoxNames()
        {

            DataTable AllBoxNames= fbm.getAllBoxNames();

            List<string> strList = new List<string>();
            if (AllBoxNames != null && AllBoxNames.Rows.Count > 0)
            {
                foreach (DataRow dr in AllBoxNames.Rows)
                {
                    strList.Add(dr["box_name"].ToString());
                }
                var source = new AutoCompleteStringCollection();
                source.AddRange(strList.ToArray());

                this.txtBoxName.AutoCompleteCustomSource = source;
                this.txtBoxName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtBoxName.AutoCompleteSource = AutoCompleteSource.CustomSource;

                this.txtModify_BoxName.AutoCompleteCustomSource = source;
                this.txtModify_BoxName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtModify_BoxName.AutoCompleteSource = AutoCompleteSource.CustomSource;


            }

        }

        private void ButSearch_Click(object sender, EventArgs e)
        {
            string custid = this.txtCust_ID.Text.Trim();
            string styleID = this.txtBoxName.Text.Trim();
            this.getBoxWeight(custid, styleID);
        }
        public void getBoxWeight(string custid, string boxName)
        {
            DataTable dt = fbm.getBoxWeights(custid, boxName);
            this.dgvBoxsBase.DataSource = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                this.dgvBoxsBase.DataSource = dt;
                this.dgvBoxsBase.Columns["CreateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                this.dgvBoxsBase.Columns["LastModifyDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }
            MessageBox.Show("查询完成");
        }

        private void butCreate_Click(object sender, EventArgs e)
        {
            if (!this.gbModify.Enabled)
            {
                this.gbModify.Enabled = true;
            }
            else
            {
                this.addBoxsWeight();
                //  this.dgvSingleWeight.Rows.Add();
            }
        }
        public void addBoxsWeight()
        {
            if (this.dgvBoxsBase.Rows.Count <= 0)
            {
                DataRow dr = BoxWeightDB.NewRow();
                dr["ID"] = -1;
                dr["box_name"] = txtModify_BoxName.Text.Trim().ToUpper();
                dr["box_weight"] = txtModify_BoxWeight.Text.Trim().ToUpper();
                dr["box_l"] = txtModify_BoxLong.Text.Trim().ToUpper();
                dr["box_w"] = txtModify_BoxWidth.Text.Trim();
                dr["box_h"] = txtModify_BoxHigh.Text.Trim();
                dr["cust_id"] = txtModify_CustID.Text.Trim();
                dr["season"] = txtModify_Season.Text.Trim();
                dr["Remark"] = txtBoxRemark.Text.Trim();
                dr["CreateUser"] = Dns.GetHostName();
                dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["LastModified"] = Dns.GetHostName();
                dr["LastModifyDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BoxWeightDB.Rows.Add(dr);
                this.dgvBoxsBase.DataSource = BoxWeightDB;
                this.dgvBoxsBase.Refresh();
            }
            else
            {
                DataTable dt = GetDgvToTable(this.dgvBoxsBase);
                DataRow dr = dt.NewRow();
                dr["ID"] = -1;
                dr["box_name"] = txtModify_BoxName.Text.Trim().ToUpper();
                dr["box_weight"] = txtModify_BoxWeight.Text.Trim().ToUpper();
                dr["box_l"] = txtModify_BoxLong.Text.Trim().ToUpper();
                dr["box_w"] = txtModify_BoxWidth.Text.Trim();
                dr["box_h"] = txtModify_BoxHigh.Text.Trim();
                dr["cust_id"] = txtModify_CustID.Text.Trim();
                dr["season"] = txtModify_Season.Text.Trim();
                dr["Remark"] = txtBoxRemark.Text.Trim();
                dr["CreateUser"] = Dns.GetHostName();
                dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["LastModified"] = Dns.GetHostName();
                dr["LastModifyDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                dt.Rows.Add(dr);
                this.dgvBoxsBase.DataSource = dt;
                this.dgvBoxsBase.Refresh();
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



        private void butSubmit_Click(object sender, EventArgs e)
        {
            this.saveBoxeWeight();
        }
        public void saveBoxeWeight()
        {
            DataTable dt = GetDgvToTable(this.dgvBoxsBase);

            int insetRows = fbm.saveSingleWeightDBtoDatabase(dt);
            string msg = "共更新 " + insetRows.ToString() + " 行数据";
            MessageBox.Show(msg);
        }

        private void butRepeal_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = this.dgvBoxsBase.CurrentRow;
            if (row != null) return;
            int index = row.Index;
            if (index < 0) return;
            string id = this.dgvBoxsBase.Rows[index].Cells["ID"].Value.ToString();

            this.isDelRows(id);
        }

        public void isDelRows(string id)
        {
            if (Convert.ToInt32(id) <= 0) return;
            int delRows = fbm.isDelRows(Convert.ToInt32(id));
            if (delRows > 0)
            {
                MessageBox.Show("id：" + id.ToString() + " 已删除");
                string custid = this.txtCust_ID.Text.Trim();
                string boxname = this.txtBoxName.Text.Trim();
                this.getBoxWeight(custid, boxname);
            }
            else
            {
                MessageBox.Show("删除失败");
            }


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

        private void dgvBoxsBase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvBoxsBase.ReadOnly = false;
            this.dgvBoxsBase.Columns[0].ReadOnly = true;
            this.dgvBoxsBase.Columns[9].ReadOnly = true;
            this.dgvBoxsBase.Columns[10].ReadOnly = true;
            this.dgvBoxsBase.Columns[11].ReadOnly = true;
            this.dgvBoxsBase.Columns[12].ReadOnly = true;
            this.dgvBoxsBase.Columns[13].ReadOnly = true;
        }

        private void dgvBoxsBase_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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

        private void dgvBoxsBase_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvBoxsBase.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.dgvBoxsBase.Rows.Remove(row);
                }
            }
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
            string sheetname = "SAA-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");



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

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            if (this.selecteddgv == null)
            {
                return;
            }

            Clipboard.SetDataObject(this.selecteddgv.GetClipboardContent());
        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            if (this.selecteddgv == null)
            {
                return;
            }

            Clipboard.SetDataObject(this.selecteddgv.CurrentCell.Value.ToString());
        }
    }
    }
