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
    public partial class FrmCompletedSearch : Form
    {
        private static FrmCompletedSearch frm;
        public CompletedSearchManager csm = new CompletedSearchManager();
        public List<mesOrg> Orgs;
        public List<mesEmployee> emps;
        public string SAAUlr = "http://192.168.4.251:5000/api/process";
        public string SAATest = "http://192.168.4.251:5001/api/reportPlace";
        public string TOPUlr = "http://192.168.7.240:5000/api/process";
        public string TOPTest = "http://192.168.7.240:5001/api/reportPlace";
        public string org = "";
        public FrmCompletedSearch()
        {  
            InitializeComponent();
            this.dataGridView1.DoubleBufferedDataGirdView(true);
        }
        public static FrmCompletedSearch GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmCompletedSearch();
            }
            return frm;
        }
        private void FrmCompletedSearch_Load(object sender, EventArgs e)
        {
          
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            if (this.cbOrg.Items.Count <= 0)
            {
                MessageBox.Show("请选择厂区");
                return;
            }
            string org = this.cbOrg.SelectedItem.ToString();

            if (this.cbDept.Items.Count <= 0)
            {
                MessageBox.Show("请选择制程");
                return;
            }
            string dept = this.labDeptMsg.Text.Trim();

            if (this.cbLocation.Items.Count <= 0 || this.cbLocation.SelectedIndex <= -1)
            {
                MessageBox.Show("请选择储位");
                return;
            }
            if (this.cbType.SelectedIndex  <= -1)
            {
                MessageBox.Show("请选择查询类别");
                return;
            }

            string location = this.cbLocation.SelectedItem.ToString();
            string starDate = this.dtpStarDate.Value.ToString("yyyy-MM-dd");
            string stopDate = this.dtpStopDate.Value.AddDays(1).ToString("yyyy-MM-dd");
            string isCheckDate = this.cbCheackedDate.Checked.ToString();
            string searchType = this.cbType.SelectedItem.ToString();



            List<CompletedSearch> cs = new List<CompletedSearch>();
            CompletedSearch a = new CompletedSearch();
            a.key = "org";
            a.value = org;
            cs.Add(a);
            CompletedSearch b = new CompletedSearch();
            b.key = "dept";
            b.value = dept;
            cs.Add(b);
            CompletedSearch c = new CompletedSearch();
            c.key = "location";
            c.value = location;
            cs.Add(c);
            CompletedSearch d = new CompletedSearch();
            d.key = "starDate";
            d.value = starDate;
            cs.Add(d);
            CompletedSearch f = new CompletedSearch();
            f.key = "stopDate";
            f.value = stopDate;
            cs.Add(f);
            CompletedSearch g = new CompletedSearch();
            g.key = "isCheckDate";
            g.value = isCheckDate;
            cs.Add(g);
            CompletedSearch h = new CompletedSearch();
            h.key = "searchType";

            /*
             IN  0
            OUT  1
            ALL  ''
            库存  -1
             */
            if (searchType == "IN")
            {
                h.value = "0";
                cs.Add(h);
            }
            if (searchType == "OUT")
            {
                h.value = "1";
                cs.Add(h);
            }
            if (searchType == "ALL")
            {
                h.value = "";
                cs.Add(h);
            }
            if (searchType == "库存")
            {
                h.value = "-1";
                cs.Add(h);
            } 
            DataTable dt = csm.getMesWorktagScans(cs);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据");
                return;
            }
            this.dataGridView1.DataSource = dt;
        }

        private void cbCheackedDate_Click(object sender, EventArgs e)
        {
           // this.cbCheackedDate.Checked = !this.cbCheackedDate.Checked;
        }

        private async void cbOrg_ClickAsync(object sender, EventArgs e)
        {
            
         //   if(this.org == "SAA")
           // {
                this.Orgs = await csm.getOrgs(SAATest);
           // }else if(this.org == "TOP")
           if(this.Orgs.Count<=0)
            {
                this.Orgs = await csm.getOrgs(TOPTest);
            }
            if (this.Orgs.Count <= 0)
            {
                MessageBox.Show("获取厂区失败,请查询是否正常连线服务器");
                return;
            }
           
            if (this.Orgs.Count <= 0)
            {
                MessageBox.Show("获取厂区失败,请查询是否正常连线服务器");
                return;
            }
            this.cbOrg.Items.Clear();
            for (int i = 0; i < this.Orgs.Count; i++)
            {
                this.cbOrg.Items.Add(Orgs[i].ReportPlaceName);
            }

            //
        }

        private void cbOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PlaceNameIndex = this.cbOrg.SelectedIndex;
            if (PlaceNameIndex >= 0)
            {
                this.labOrgMsg.Text = this.Orgs[PlaceNameIndex].ReportPlaceId.ToString();
            }
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            int processNameIndex = this.cbDept.SelectedIndex;
            if (processNameIndex >= 0)
            {
                this.labDeptMsg.Text = this.emps[processNameIndex].ID;
            }
        }

        private async void cbDept_Click(object sender, EventArgs e)
        {
            if (this.cbOrg.Items.Count <= 0)
            {
                return;
            }
            string Org = this.cbOrg.SelectedItem.ToString();
            if (Org.IndexOf("SAA") != -1)
            {
                this.emps = await csm.GetAllProducts(this.SAAUlr);
            }
            else
            {
                this.emps = await csm.GetAllProducts(this.TOPUlr);
            }

            if (emps.Count <= 0)
            {
                MessageBox.Show("获取制程失败,请查询是否正常连线服务器");
                return;
            }
            this.cbDept.Items.Clear();
            for (int i = 0; i < emps.Count; i++)
            {
                this.cbDept.Items.Add(emps[i].Name);
            }
        }

        private void cbLocation_Click(object sender, EventArgs e)
        {
            if (this.cbOrg.Items.Count <= 0)
            {
                return;
            }
            string org = this.cbOrg.SelectedItem.ToString();
            this.cbLocation.Items.Clear();
            List<string> locations = csm.getLocation(org);
            if (locations.Count > 0)
            {
                this.cbLocation.Items.Clear();
                foreach (string location in locations)
                {
                    this.cbLocation.Items.Add(location);
                }
            }
            this.cbLocation.Items.Add("");
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

        private void FrmCompletedSearch_Resize(object sender, EventArgs e)
        {
            this.groupBox2.Width = this.Width - 20;
            this.groupBox2.Height = this.Height - 150;
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
            //   sdfExport.ShowDialog();
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            String tableName = "";
            NPOIExcelCompletedToMes NPOIexcel = new NPOIExcelCompletedToMes();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(this.dataGridView1);
            tableName = "dgvOutgoingTable";
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
    }
}
