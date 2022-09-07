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
    public partial class FrmPackingBase : Form
    {
        private static  FrmPackingBase frm = null;
        DataTable packbaseDB = new DataTable();
        public FrmPackBaseManager pbm = new FrmPackBaseManager();
        DataGridView selecteddgv = null;
        public int hiedcolumnindex = -1;
        public static FrmPackingBase GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmPackingBase();
            }
            return frm;
        }
        public FrmPackingBase()
        {
            InitializeComponent();
            initpackbaseDB();
            this.dgvPackBase.DoubleBufferedDataGirdView(true);
        }
        public void initpackbaseDB()
        {


            DataColumn ID = new DataColumn();
            ID.ColumnName = "ID";
            ID.DefaultValue = -1;
            packbaseDB.Columns.Add(ID);

            DataColumn cust_id = new DataColumn();
            cust_id.ColumnName = "cust_id";
            packbaseDB.Columns.Add(cust_id);

            DataColumn style_id = new DataColumn();
            style_id.ColumnName = "style_id";
            packbaseDB.Columns.Add(style_id);

            DataColumn box_name = new DataColumn();
            box_name.ColumnName = "box_name";
            packbaseDB.Columns.Add(box_name);

            DataColumn sizes = new DataColumn();
            sizes.ColumnName = "sizes";
            packbaseDB.Columns.Add(sizes);

            DataColumn qtys = new DataColumn();
            qtys.ColumnName = "qtys";
            packbaseDB.Columns.Add(qtys);

            DataColumn remark = new DataColumn();
            remark.ColumnName = "remark";
            packbaseDB.Columns.Add(remark);

            DataColumn deductionWeight = new DataColumn();
            deductionWeight.ColumnName = "deductionWeight";
            packbaseDB.Columns.Add(deductionWeight);

            DataColumn bagWeight = new DataColumn();
            bagWeight.ColumnName = "bagWeight";
            packbaseDB.Columns.Add(bagWeight);

            DataColumn clapBoardTotalWeight = new DataColumn();
            clapBoardTotalWeight.ColumnName = "clapBoardTotalWeight";
            packbaseDB.Columns.Add(clapBoardTotalWeight);

            DataColumn accessoriesTotalWeight = new DataColumn();
            accessoriesTotalWeight.ColumnName = "accessoriesTotalWeight";
            packbaseDB.Columns.Add(accessoriesTotalWeight);

            DataColumn isDel = new DataColumn();
            isDel.ColumnName = "isDel";
            packbaseDB.Columns.Add(isDel);

            DataColumn CreateUser = new DataColumn();
            CreateUser.ColumnName = "CreateUser";
            packbaseDB.Columns.Add(CreateUser);

            DataColumn CreateDate = new DataColumn();
            CreateDate.ColumnName = "CreateDate";
            packbaseDB.Columns.Add(CreateDate);

            DataColumn LastModified = new DataColumn();
            LastModified.ColumnName = "LastModified";
            packbaseDB.Columns.Add(LastModified);

            DataColumn LastModifyDate = new DataColumn();
            LastModifyDate.ColumnName = "LastModifyDate";
            packbaseDB.Columns.Add(LastModifyDate);

            this.dgvPackBase.DataSource = packbaseDB;
        }
        private void FrmPackingBase_Resize(object sender, EventArgs e)
        {

            gbMenu.Width = this.Width - gbFunction.Width - 40;
            gbModify.Width = gbMenu.Width;
            gbPackBase.Width = gbMenu.Width;
            gbPackBase.Height = this.Height - gbMenu.Height - gbModify.Height - 60;
            gbFunction.Height = this.Height - 50;
            gbFunction.Left = this.gbMenu.Width + 10;
        }

        private void ButBoxWeight_Click(object sender, EventArgs e)
        {
            FrmSingleWeightMaintain frm = FrmSingleWeightMaintain.GetSingleton();
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

        private void butPackBase_Click(object sender, EventArgs e)
        {
            FrmPackingBase frm = FrmPackingBase.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }

        private void FrmPackingBase_Load(object sender, EventArgs e)
        {
            this.gbModify.Enabled = false;

                this.getAllCustID();
                this.getAllStyleID("");
                this.getAllBoxs("");
                this.getAllSizes("","");


            this.dgvPackBase.ReadOnly = true;
        }
        public void getAllCustID()
        {
            DataTable custDBS = pbm.getAllCustidByFsgDB( );
            List<string> strList = new List<string>();
            if (custDBS != null && custDBS.Rows.Count > 0)
            {
                foreach (DataRow dr in custDBS.Rows)
                {
                    strList.Add(dr["custID"].ToString());
                }
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
        public void getAllStyleID(string custID)
        {

            DataTable AllStyle = pbm.getAllStyleIDByFsgDB(custID);
            List<string> strList = new List<string>();
            if (AllStyle != null && AllStyle.Rows.Count > 0)
            {
                foreach (DataRow dr in AllStyle.Rows)
                {
                    strList.Add(dr["styleID"].ToString());
                }
                var source = new AutoCompleteStringCollection();
                source.AddRange(strList.ToArray());

                this.txtStyle_id.AutoCompleteCustomSource = source;
                this.txtStyle_id.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtStyle_id.AutoCompleteSource = AutoCompleteSource.CustomSource;

                this.txtModify_StyleID.AutoCompleteCustomSource = source;
                this.txtModify_StyleID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtModify_StyleID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }
        public void getAllBoxs(string custID)
        {
            DataTable StyleBox = pbm.getAllBoxNamesIDByfsgDB(custID);
            List<string> strList = new List<string>();
            if (StyleBox != null && StyleBox.Rows.Count > 0)
            {
                foreach (DataRow dr in StyleBox.Rows)
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

        public void getAllSizes(string custId,string styleId)
        {
            DataTable sizesDB = pbm.getAllSizesByStyle(custId, styleId);
            List<string> strList = new List<string>();
            if (sizesDB != null && sizesDB.Rows.Count > 0)
            {
                foreach (DataRow dr in sizesDB.Rows)
                {
                    strList.Add(dr["SizeID"].ToString());
                }
                var source = new AutoCompleteStringCollection();
                source.AddRange(strList.ToArray());

                this.txtModify_Size.AutoCompleteCustomSource = source;
                this.txtModify_Size.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtModify_Size.AutoCompleteSource = AutoCompleteSource.CustomSource;


            }
        }

        private void ButSearch_Click(object sender, EventArgs e)
        {
            string custid = this.txtCust_ID.Text.Trim();
            string styleID = this.txtStyle_id.Text.Trim();
            string boxName = this.txtBoxName.Text.Trim();
            this.getPackBase(custid, styleID, boxName);
        }
        public void getPackBase(string custid, string styleid, string boxName)
        {
            DataTable dt = pbm.getPackBase(custid, styleid, boxName);
            this.dgvPackBase.DataSource = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                this.dgvPackBase.DataSource = dt;
                this.dgvPackBase.Columns["CreateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                this.dgvPackBase.Columns["LastModifyDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
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
                this.addPackingBaseWeight();
            }
        }
        public void addPackingBaseWeight()
        {
            if (this.dgvPackBase.Rows.Count <= 0)
            {
                DataRow dr = packbaseDB.NewRow();
                dr["id"] = -1;
                dr["cust_id"] = txtModify_CustID.Text.Trim().ToUpper();
                dr["style_id"] = txtModify_StyleID.Text.Trim().ToUpper();
                dr["box_name"] = txtModify_BoxName.Text.Trim().ToUpper();
                dr["sizes"] = txtModify_Size.Text.Trim();

                dr["qtys"] = txtModify_Qtys.Text.Trim();
                dr["remark"] = txtRemark.Text.Trim();
                dr["deductionWeight"] = txtAntiTheftDeductionWeight.Text.Trim();
                dr["bagWeight"] = txtBagWeight.Text.Trim();
                dr["clapBoardTotalWeight"] = txtClapBoardWeight.Text.Trim();
                dr["accessoriesTotalWeight"] = txtAccessoriesWeight.Text.Trim();
                dr["isDel"] = 0;
                dr["CreateUser"] = Dns.GetHostName();
                dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["LastModified"] = Dns.GetHostName();
                dr["LastModifyDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                packbaseDB.Rows.Add(dr);
                this.dgvPackBase.DataSource = packbaseDB;
                this.dgvPackBase.Refresh();
            }
            else
            {
                DataTable dt = GetDgvToTable(this.dgvPackBase);
                DataRow dr = dt.NewRow();
                dr["id"] = -1;
                dr["cust_id"] = txtModify_CustID.Text.Trim().ToUpper();
                dr["style_id"] = txtModify_StyleID.Text.Trim().ToUpper();
                dr["box_name"] = txtModify_BoxName.Text.Trim().ToUpper();
                dr["sizes"] = txtModify_Size.Text.Trim();

                dr["qtys"] = txtModify_Qtys.Text.Trim();
                dr["remark"] = txtRemark.Text.Trim();
                dr["deductionWeight"] = txtAntiTheftDeductionWeight.Text.Trim();
                dr["bagWeight"] = txtBagWeight.Text.Trim();
                dr["clapBoardTotalWeight"] = txtClapBoardWeight.Text.Trim();
                dr["accessoriesTotalWeight"] = txtAccessoriesWeight.Text.Trim();
                dr["isDel"] = 0;
                dr["CreateUser"] = Dns.GetHostName();
                dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["LastModified"] = Dns.GetHostName();
                dr["LastModifyDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                dt.Rows.Add(dr);
                this.dgvPackBase.DataSource = dt;
                this.dgvPackBase.Refresh();
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
            this.savePackingBaseWeight();
        }
        public void savePackingBaseWeight()
        {
            DataTable dt = GetDgvToTable(this.dgvPackBase);

            int insetRows = pbm.savePackingBaseDBtoDatabase(dt);
            string msg = "共更新 " + insetRows.ToString() + " 行数据";
            MessageBox.Show(msg);
        }

        private void butRepeal_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = this.dgvPackBase.CurrentRow;
            if (row != null) return;
            int index = row.Index;
            if (index < 0) return;
            string id = this.dgvPackBase.Rows[index].Cells["ID"].Value.ToString();

            this.isDelRows(id);
        }
        public void isDelRows(string id)
        {
            if (Convert.ToInt32(id) <= 0) return;
            int delRows = pbm.isDelRows(Convert.ToInt32(id));
            if (delRows > 0)
            {
                MessageBox.Show("id：" + id.ToString() + " 已删除");
                string custid = this.txtCust_ID.Text.Trim();
                string styleID = this.txtStyle_id.Text.Trim();
                string boxName = this.txtBoxName.Text.Trim();
                this.getPackBase(custid, styleID, boxName);


            }
            else
            {
                MessageBox.Show("删除失败");
            }


        }

        private void txtCust_ID_Leave(object sender, EventArgs e)
        {
            string custID = this.txtCust_ID.Text.Trim().ToUpper();
            if (custID == "")
            {
                MessageBox.Show("请先选择客户，谢谢！");
                return;
            }
            this.getAllStyleID(custID);
        }

        private void txtStyle_id_Leave(object sender, EventArgs e)
        {
            string custID = this.txtCust_ID.Text.Trim().ToUpper();
            if (custID == "")
            {
                MessageBox.Show("请先选择客户，谢谢！");
                return;
            }
            this.getAllBoxs(custID);
        }

        private void txtBoxName_Leave(object sender, EventArgs e)
        {
            string custID = this.txtCust_ID.Text.Trim().ToUpper();
            string styleId = this.txtCust_ID.Text.Trim().ToUpper();
            if (custID == "")
            {
                MessageBox.Show("请先选择客户，谢谢！");
                return;
            }
            if (styleId == "")
            {
                MessageBox.Show("请先选择款式，谢谢！");
                return;
            }
            this.getAllSizes(custID, styleId);
        }

        private void txtModify_CustID_Leave(object sender, EventArgs e)
        {
            string custID = this.txtModify_CustID.Text.Trim().ToUpper();
            if (custID == "")
            {
                MessageBox.Show("请先选择客户，谢谢！");
                return;
            }
            this.getAllStyleID(custID);
        }

        private void txtModify_StyleID_Leave(object sender, EventArgs e)
        {
            string custID = this.txtModify_CustID.Text.Trim().ToUpper();
            if (custID == "")
            {
                MessageBox.Show("请先选择客户，谢谢！");
                return;
            }
            this.getAllBoxs(custID);
        }

        private void txtModify_BoxName_Leave(object sender, EventArgs e)
        {
            string custID = this.txtModify_CustID.Text.Trim().ToUpper();
            string styleId = this.txtModify_StyleID.Text.Trim().ToUpper();
            if (custID == "")
            {
                MessageBox.Show("请先选择客户，谢谢！");
                return;
            }
            if (styleId == "")
            {
                MessageBox.Show("请先选择款式，谢谢！");
                return;
            }
            this.getAllSizes(custID, styleId);
        }

        private void dgvPackBase_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvPackBase.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.dgvPackBase.Rows.Remove(row);
                }
            }
        }

        private void dgvPackBase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvPackBase.ReadOnly = false;
            this.dgvPackBase.Columns[0].ReadOnly = true;
            this.dgvPackBase.Columns[11].ReadOnly = true;
            this.dgvPackBase.Columns[12].ReadOnly = true;
            this.dgvPackBase.Columns[13].ReadOnly = true;
            this.dgvPackBase.Columns[14].ReadOnly = true;
            this.dgvPackBase.Columns[15].ReadOnly = true;
        }

        private void dgvPackBase_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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
    }
}
