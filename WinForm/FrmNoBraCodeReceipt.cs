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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmNoBraCodeReceipt : Form
    {
        private static FrmNoBraCodeReceipt frm;
        public receiManager rm = new receiManager();
        DataTable newReceiDT = new DataTable();
        public int hiedcolumnindex = -1; //是否选中外面
        public int delID = 0; //要删除的ID行号
        public int rowIndex = -1; //表的行索引



        public FrmNoBraCodeReceipt()
        {
            InitializeComponent();
             this.dgvReceiData.DoubleBufferedDataGirdView(true);

            newReceiDT.Columns.Add("id", typeof(int));
            newReceiDT.Columns.Add("receiNumber", typeof(string));
            newReceiDT.Columns.Add("org", typeof(string));
            newReceiDT.Columns.Add("subinv", typeof(string));
            newReceiDT.Columns.Add("line", typeof(string));

            newReceiDT.Columns.Add("style", typeof(string));
            newReceiDT.Columns.Add("color", typeof(string));
            newReceiDT.Columns.Add("size", typeof(string));
            newReceiDT.Columns.Add("qtyCount", typeof(string));

            newReceiDT.Columns.Add("po", typeof(string));
            newReceiDT.Columns.Add("boxCount", typeof(string));
            newReceiDT.Columns.Add("receiDate", typeof(string));
            newReceiDT.Columns.Add("receiEmp", typeof(string));
            newReceiDT.Columns.Add("mark", typeof(string));

            newReceiDT.Columns.Add("receiInDate", typeof(string));
            newReceiDT.Columns.Add("receiInPcName", typeof(string));
            // this.dgvOutMWH.DoubleBufferedDataGirdView(true);
        }
        public static FrmNoBraCodeReceipt GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmNoBraCodeReceipt();
            }
            return frm;
        }

        private void FrmNoBraCodeReceipt_Resize(object sender, EventArgs e)
        {
            // 1274, 769
            if(this.Width < 1274)
            {
                this.Width = 1274;
            }
            if(this.Height < 300)
            {
                this.Height = 300;
            }

            this.gbReceiData.Width = this.Width - 20;
            this.gbReceiData.Height = this.Height - this.bgSearch.Height - 50;
            this.progressBar1.Left = this.gbReceiData.Left + 95;
            this.progressBar1.Width = this.gbReceiData.Width - 100;
            this.btnSave.Left = this.Width - this.btnSave.Width - 20;

            /*
            dgvReceiData.EnableHeadersVisualStyles = false;//这一句很重要，否则下面的列头设置不起作用
            dgvReceiData.Columns[0].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
            dgvReceiData.Columns[1].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
            dgvReceiData.Columns[2].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
            dgvReceiData.Columns[3].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
            dgvReceiData.Columns[4].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
            dgvReceiData.Columns[5].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
            dgvReceiData.Columns[6].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
            */
            //  this.dgvReceiData.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Red;
        }

        private void dgvReceiData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.dgvReceiData.Rows.Count <= 0)
            {
                return;
            }
            DataTable receiDT = new DataTable();
            receiDT.Columns.Add("id", typeof(int));
            receiDT.Columns.Add("receiNumber", typeof(string));
            receiDT.Columns.Add("org", typeof(string));
            receiDT.Columns.Add("subinv", typeof(string));
            receiDT.Columns.Add("line", typeof(string));

            receiDT.Columns.Add("style", typeof(string));
            receiDT.Columns.Add("color", typeof(string));
            receiDT.Columns.Add("size", typeof(string));
            receiDT.Columns.Add("qtyCount", typeof(string));

            receiDT.Columns.Add("po", typeof(string));
            receiDT.Columns.Add("boxCount", typeof(string));
            receiDT.Columns.Add("receiDate", typeof(string));
            receiDT.Columns.Add("receiEmp", typeof(string));
            receiDT.Columns.Add("mark", typeof(string));

            receiDT.Columns.Add("receiInDate", typeof(string));
            receiDT.Columns.Add("receiInPcName", typeof(string));
           

            for (int i = 0; i < this.dgvReceiData.Rows.Count; i++)
            {
                //选择资料
                string idstr = Convert.ToString(dgvReceiData.Rows[i].Cells[0].Value);
                string receiNumberstr = Convert.ToString(dgvReceiData.Rows[i].Cells[1].Value);
                string orgstr = Convert.ToString( dgvReceiData.Rows[i].Cells[2].Value);
                string subinvstr = Convert.ToString(dgvReceiData.Rows[i].Cells[3].Value);
                string linestr = Convert.ToString(dgvReceiData.Rows[i].Cells[4].Value);

                //不能为空资料
                string stylestr = Convert.ToString(dgvReceiData.Rows[i].Cells[5].Value);
                string colorstr = Convert.ToString(dgvReceiData.Rows[i].Cells[6].Value);
                string sizestr = Convert.ToString(dgvReceiData.Rows[i].Cells[7].Value);
                string qtyCountstr = Convert.ToString( dgvReceiData.Rows[i].Cells[8].Value);

                string postr = Convert.ToString(dgvReceiData.Rows[i].Cells[9].Value);
                string boxCountstr = Convert.ToString(dgvReceiData.Rows[i].Cells[10].Value);               
                string receiDatestr = Convert.ToString(dgvReceiData.Rows[i].Cells[11].Value);
                string receiEmpstr = Convert.ToString(dgvReceiData.Rows[i].Cells[12].Value);
                string markstr = Convert.ToString(dgvReceiData.Rows[i].Cells[13].Value);
                 
                string receiInDatestr = DateTime.Now.ToString("yyyy-MM-dd");
                string receiInPcNamestr = Dns.GetHostName().ToString();
               

                if (orgstr.Length <= 0)
                {
                    MessageBox.Show("收货厂区不能为空");
                    return;
                }
                if (orgstr != "SAA" && orgstr != "TOP")
                {
                    MessageBox.Show("收货厂区只能为 SAA  或 TOP");
                    return;
                }

                if (subinvstr.Length <= 0)
                {
                    MessageBox.Show("收货仓库不能为空");
                    return;
                }

                if (linestr.Length <= 0)
                {
                    MessageBox.Show("转厂线别不能为空");
                    return;
                }
                if (stylestr.Length <= 0)
                {
                    MessageBox.Show("款式不能为空");
                    return;
                }
                if (colorstr.Length <= 0)
                {
                    MessageBox.Show("颜色不能为空");
                    return;
                }
                if (sizestr.Length <= 0)
                {
                    MessageBox.Show("尺码不能为空");
                    return;
                }
                if (qtyCountstr.ToString().Length <= 0 ||  qtyCountstr =="")
                {
                    MessageBox.Show("数量不能为空 ");
                    return;
                }
                if (receiNumberstr.ToString().Length <= 0 || receiNumberstr == "")
                {
                    MessageBox.Show("送貨單號不能為空");
                    return;
                }

                Regex reg = new Regex("^[0-9]+$"); //正则表达式 检测是否数字
                Match mq = reg.Match(qtyCountstr.ToString());
                if (!mq.Success)
                {
                    MessageBox.Show("收货件数只能为数字 ");
                }
                if (boxCountstr.Length > 0)
                {
                    Match mb = reg.Match(boxCountstr.ToString());
                    if (!mb.Success)
                    {
                        MessageBox.Show("收货箱数只能为数字 ");
                    }
                } 

                DataRow row = receiDT.NewRow();
                row["id"] = idstr;
                row["receiNumber"] = receiNumberstr;
                row["org"] = orgstr;
                row["subinv"] = subinvstr;
                row["line"] = linestr;
                row["style"] = stylestr;
                row["color"] = colorstr;
                row["size"] = sizestr;
                row["qtyCount"] = qtyCountstr;
                row["po"] = postr;
                row["boxCount"] = boxCountstr;                
                row["receiDate"] = receiDatestr;
                row["receiEmp"] = receiEmpstr;
                row["mark"] = markstr;
                row["receiInDate"] = receiInDatestr;
                row["receiInPcName"] = receiInPcNamestr;
                receiDT.Rows.Add(row);
            }

            if (receiDT.Rows.Count <= 0)
            {
                return;
            }

            //----------------------------------------------------
            // 查询 receiDT 是否有大于转厂数量
            //  查询转厂数量 比较 大于不保存  小于保存
            // 组合成单行表
            DataTable styleLength = new DataTable();
            styleLength.Columns.Add("org", typeof(string));
            styleLength.Columns.Add("subinv", typeof(string));
            styleLength.Columns.Add("line", typeof(string));
            styleLength.Columns.Add("style", typeof(string));
            styleLength.Columns.Add("qtyCount", typeof(int));
            for(int i=0;i< receiDT.Rows.Count; i++)
            {
                if (!isExstyleLength(receiDT.Rows[i], styleLength))
                {
                    DataRow row = styleLength.NewRow();
                    row["org"] = receiDT.Rows[i]["org"].ToString();
                    row["subinv"] = receiDT.Rows[i]["subinv"].ToString();
                    row["line"] = receiDT.Rows[i]["line"].ToString();
                    row["style"] = receiDT.Rows[i]["style"].ToString();
                    row["qtyCount"] = 0;
                    styleLength.Rows.Add(row);
                }
            }

            //计算单行表格的收库数量
            for (int i = 0; i < styleLength.Rows.Count; i++)
            {
                for (int j = 0; j < receiDT.Rows.Count; j++)
                {
                    if( styleLength.Rows[i]["org"].ToString() == receiDT.Rows[j]["org"].ToString() &&
                        styleLength.Rows[i]["subinv"].ToString() == receiDT.Rows[j]["subinv"].ToString() &&
                        styleLength.Rows[i]["line"].ToString() == receiDT.Rows[j]["line"].ToString() &&
                        styleLength.Rows[i]["style"].ToString() == receiDT.Rows[j]["style"].ToString()
                        )
                    {
                        styleLength.Rows[i]["qtyCount"] = Convert.ToInt32(styleLength.Rows[i]["qtyCount"]) + Convert.ToInt32(receiDT.Rows[j]["qtyCount"]);
                    }
                }
            }

            // 查询 receiDT 是否有大于转厂数量
            string org = "";
            string subinv = "";
            string line = "";
            string style = "";
            for (int i = 0; i < styleLength.Rows.Count; i++)
            {
                org = styleLength.Rows[i]["org"].ToString();
                subinv = styleLength.Rows[i]["subinv"].ToString();
                line = styleLength.Rows[i]["line"].ToString();
                style = styleLength.Rows[i]["style"].ToString();
                DataTable qtyDT = rm.getStyleCounts(org, subinv, line, style);
                if (qtyDT.Rows.Count <= 0)
                {
                    MessageBox.Show("没有找到转厂数据");
                    return;
                }

                int qtyCount = Convert.ToInt32(qtyDT.Rows[0]["qtyCount"]);
                int styleCount = Convert.ToInt32(qtyDT.Rows[0]["styleCount"]);
                int id = Convert.ToInt32(qtyDT.Rows[0]["id"]);

                qtyCount = qtyCount + Convert.ToInt32(styleLength.Rows[i]["qtyCount"]);
                if ( qtyCount  > styleCount)
                {

                    /*
                    MessageBox.Show("转厂入库数信息:" + "\r\n" + @"
                                            厂区:" + org + @"      \r\n  
                                            线别: " + line + @"   \r\n  
                                            款式: " + style + @" \r\n
                                            入库数量: " + qtyCount.ToString() + "  大于 转厂数量  " + styleCount.ToString()
                                            ) ;
                    */
                    string mark = "转厂入库数信息--厂区: " + org + " 线别: " + line + " 款式: " + style + " 入库数量: " + qtyCount.ToString() + "  大于 转厂数量  " + styleCount.ToString();
                      rm.updataReceiError(org, line, style, qtyCount, styleCount,mark);
                    //  return;
                }
               // else
              //  {
                   int w = rm.updataStyleCounts(id, qtyCount);
                    if(w <= 0)
                    {
                        MessageBox.Show("保存失败,请找厂务规划课 小明查找原因 ");
                        return;
                    }
              //  }

            }

            //DataTable outStyleDT =  rm.getStyleCounts(orgstr, subinvstr, linestr);


            //----------------------------------------------------------
            string result = rm.writeReceiToData(receiDT);
            MessageBox.Show(result);

            // 保存了清空
            this.newReceiDT.Clear();
            this.dgvReceiData.DataSource = null;
        }
        private bool isExstyleLength(DataRow row ,DataTable dt)
        {
            if(dt.Rows.Count <= 0)
            {
                return false;
            }
            bool isEx = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["org"].ToString() == row["org"].ToString() &&
                        dt.Rows[i]["subinv"].ToString() == row["subinv"].ToString() &&
                        dt.Rows[i]["line"].ToString() == row["line"].ToString() &&
                        dt.Rows[i]["style"].ToString() == row["style"].ToString()
                        )
                    {
                        isEx = true;
                        break;
                    }
                    else
                    {
                        isEx = false;
                    }
                }
            return isEx;


        }

        private void dgvReceiData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) //键盘按方向下
            {
                if (this.dgvReceiData.Rows.Count <= 0)
                {
                    this.dgvReceiData.Rows.Add();


                }
                else
                {
                    int rowindex = dgvReceiData.CurrentCell.RowIndex + 1;
                    if(rowindex>= this.dgvReceiData.Rows.Count)
                    {
                        int index = this.dgvReceiData.Rows.Add();
                        for (int i = 0; i < this.dgvReceiData.Columns.Count; i++)
                        {
                            this.dgvReceiData.Rows[index].Cells[i].Value = this.dgvReceiData.Rows[index - 1].Cells[i].Value;
                        }
                    }
                   
                }
            }
            if (e.KeyCode == Keys.Up) //键盘按方向上
            {
               // int index = dgvReceiData.CurrentCell.RowIndex;

            
            }

        }

     

        private void cbOrg_Click(object sender, EventArgs e)
        {

           
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            List<string> locations = new List<string>();
            for (int i = 0; i < this.cklbLocation.Items.Count; i++)
            {
                if (this.cklbLocation.GetItemChecked(i))
                {
                    locations.Add(this.cklbLocation.Items[i].ToString());
                }
            }
            receiSearch rs = new receiSearch();

            if (this.cbOrg.SelectedIndex < 0)
            {
                MessageBox.Show("请选择厂区");
                return;
            }
            rs.org = this.cbOrg.SelectedItem.ToString().Trim();

           if(this.cbSubinv.SelectedIndex < 0)
            {
                MessageBox.Show("请选择仓库");
                return;
            }
                rs.subinv = this.cbSubinv.SelectedItem.ToString();
             
         
            rs.location = locations;
            rs.style = this.txtStyle.Text.Trim();
            rs.color = this.txtColor.Text.Trim();
            if (chbReceiDate.Checked)
            {
                rs.receiDate = true;
            }
            else
            {
                rs.receiDate = false;
            }
           
            rs.starTime = this.dtpStartDate.Value.ToString("yyyy-MM-dd");
            rs.stopTime = this.dtpStopDate.Value.ToString("yyyy-MM-dd");
            rs.poNumber = this.txtPO.Text.Trim();
            rs.ReceiNumber = this.txtReceiNumber.Text.Trim();
            this.splitContainer1.Panel1Collapsed = false;
            this.splitContainer1.SplitterDistance = 300;


            DataTable dt = rm.getReceis(rs);
            this.dgvSearchDate.DataSource = null;
            this.dgvSearchDate.DataSource = dt;
            this.dgvSearchDate.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            this.dgvSearchDate.ReadOnly = true;
            chang_SearchDateHeaderText();
            /*
           for (int i = 0; i < 6; i++)
           {
               this.dgvSearchDate.Columns[i].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
               this.dgvSearchDate.Columns[i].ReadOnly = true;
           }

           for (int i = 6; i < 11; i++)
           {
               this.dgvSearchDate.Columns[i].ReadOnly = false;
           }
           this.dgvSearchDate.Columns[11].ReadOnly = true;
           this.dgvSearchDate.Columns[12].ReadOnly = true; 
           */
        }

        private void FrmNoBraCodeReceipt_Load(object sender, EventArgs e)
        {
           DataTable orgs = rm.getOrg();
            this.cbNewOrg.Items.Clear();
            this.cbOrg.Items.Clear();
            foreach (DataRow item in orgs.Rows)
            {
                this.cbNewOrg.Items.Add(item["Org"].ToString());
                this.cbOrg.Items.Add(item["Org"].ToString());
            }
            this.splitContainer1.Panel1Collapsed = true;


        }

        private void cbOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string org = this.cbNewOrg.SelectedItem.ToString();
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            DataTable subinvs = new DataTable(); //仓库别信息          
          
            if (org.Length > 0)
            {
                //获取仓库别信息
                subinvs = rm.getSubinvs(org);             
            }
            Cursor = Cursors.Default;
            //仓库别信息添加到选择项
            if (subinvs.Rows.Count <= 0)
            {
                MessageBox.Show("没有找到仓库");
                return;
            }
            this.cbNewSubinv.Items.Clear();
            foreach (DataRow item in subinvs.Rows)
            {
                this.cbNewSubinv.Items.Add(item["subinv"].ToString());
            }
        }
        

        private void cbSubinv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string org = this.cbOrg.SelectedItem.ToString();
            string subinv = this.cbSubinv.SelectedItem.ToString();
            DataTable locations = new DataTable();
            if (org.Length > 0 && subinv.Length >0)
            {
                locations = rm.getLocations(org, subinv);
            }
            if (locations.Rows.Count <= 0)
            {
                MessageBox.Show("没有找到转厂线别");
                return;
            }
            this.cklbLocation.Items.Clear();
            foreach (DataRow item in locations.Rows)
            {
                this.cklbLocation.Items.Add(item["location"].ToString());
            }
        }

      

        private void cbOrg_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string org = this.cbOrg.SelectedItem.ToString();
            DataTable subinvs = new DataTable();
            if (org.Length > 0)
            {
                subinvs = rm.getSubinvs(org);
            }
            if (subinvs.Rows.Count <= 0)
            {
                MessageBox.Show("没有找到仓库");
                return;
            }
            this.cbSubinv.Items.Clear();
            foreach (DataRow item in subinvs.Rows)
            {
                this.cbSubinv.Items.Add(item["subinv"].ToString());
            }
        }

        private void cbNewSubinv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbNewOrg.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择厂区");
                return;
            }
            if (this.cbNewSubinv.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择仓库");
                return;
            }

            string org = this.cbNewOrg.SelectedItem.ToString();
            string subinv = this.cbNewSubinv.SelectedItem.ToString();
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            DataTable locations = new DataTable();
           
            if (org.Length > 0 && subinv.Length > 0)
            {
                locations = rm.getLocations(org, subinv);
            }
            Cursor = Cursors.Default;
            if (locations.Rows.Count <= 0)
            {
                MessageBox.Show("没有找到转厂线别");
                return;
            }
            this.cbNewLine.Items.Clear();
            foreach (DataRow item in locations.Rows)
            {
                this.cbNewLine.Items.Add(item["location"].ToString());
            }
        }

        private void cbNewLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbNewOrg.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择厂区");
                return;
            }

            if (this.cbNewLine.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择转厂二线");
                return;
            }

            string org = this.cbNewOrg.SelectedItem.ToString();
            string outLine = this.cbNewLine.SelectedItem.ToString();
            string outsubinv = this.cbNewSubinv.SelectedItem.ToString();
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            //  更新本地转厂2总数量
            DataTable styledt = rm.getOutCounts(org, outLine, outsubinv);


            Cursor = Cursors.Default;
            // 转厂二款式信息添加到选择项            
            if (styledt.Rows.Count <= 0)
            {
                MessageBox.Show("本厂 " + org + " 没有转厂二"+ outLine + "的款式");
                return;
            }
            this.cbNewStyle.Items.Clear();
            foreach (DataRow item in styledt.Rows)
            {
                this.cbNewStyle.Items.Add(item["STYLE"].ToString());
            }

        }

        private void cbNewStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取颜色 尺码

            if (this.cbNewStyle.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择款式");
                return;
            }
            string style = this.cbNewStyle.SelectedItem.ToString();          
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            //获取款式的颜色信息
            DataTable colors = new DataTable(); //转厂二款式颜色
            colors = rm.getColorsByStyle(style);         
            // 转厂二款式信息添加到选择项
            if (colors.Rows.Count <= 0)
            {
                MessageBox.Show("本款式 " + style + " 没有找到色组信息");
                return;
            }
            this.cbNewColor.Items.Clear();
            foreach (DataRow item in colors.Rows)
            {
                this.cbNewColor.Items.Add(item["clr_no"].ToString());
            }


            //获取款式的尺码信息
            DataTable sizes = new DataTable(); //转厂二款式尺码
            sizes = rm.getSizesByStyle(style);
            if (sizes.Rows.Count <= 0)
            {
                MessageBox.Show("本款式 " + style + " 没有找到尺码信息");
                return;
            }

            List<string> listSize = new List<string>();
            for(int i=0;i< sizes.Rows.Count; i++)
            {
                for (int j = 0; j < sizes.Columns.Count; j++)
                {
                    string size = sizes.Rows[i][j].ToString().Trim();
                    

                    if (!isExList(listSize, size) && size.Length>0)
                    {
                        listSize.Add(sizes.Rows[i][j].ToString());

                    }
                }

            }
            

            this.cbNewSize.Items.Clear();
            foreach (string item in listSize)
            {
                this.cbNewSize.Items.Add(item);
            }



            Cursor = Cursors.Default;
        }

        private bool isExList(List<string> li, string str)
        {
            bool isExLi = false;
            for (int i = 0; i < li.Count; i++)
            {
                if (li[i] == str)
                {
                    isExLi = true;
                    break;
                }
                else
                {
                    isExLi = false;
                }
            }
            return isExLi;

        }

        private void butNew_Click(object sender, EventArgs e)
        {
            if (this.cbNewOrg.SelectedIndex <= -1)
            {
                MessageBox.Show("请选择入库厂区");
                return;
            }
            if (this.cbNewSubinv.SelectedIndex <= -1)
            {
                MessageBox.Show("请选择入库仓库");
                return;
            }
            if (this.cbNewLine.SelectedIndex <= -1)
            {
                MessageBox.Show("请选择转厂线别");
                return;
            }
            if (this.cbNewStyle.SelectedIndex <= -1)
            {
                MessageBox.Show("请选择入库款式");
                return;
            }
            if (this.cbNewColor.SelectedIndex <= -1)
            {
                MessageBox.Show("请选择入库色组");
                return;
            }
            if (this.cbNewSize.SelectedIndex <= -1)
            {
                MessageBox.Show("请选择入库尺码");
                return;
            }
            if (this.txtNewReceiNumber.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请輸入送貨單號");
                return;
            }

            string newOrg = this.cbNewOrg.SelectedItem.ToString();
            string newSubinv = this.cbNewSubinv.SelectedItem.ToString();
            string newLine = this.cbNewLine.SelectedItem.ToString();
            string newStyle = this.cbNewStyle.SelectedItem.ToString();
            string newColor = this.cbNewColor.SelectedItem.ToString();
            string newSize = this.cbNewSize.SelectedItem.ToString();
            int newSizeQty = 0;
            Regex reg = new Regex("^[0-9]+$"); //正则表达式 检测是否数字
            Match ma = reg.Match( this.txtNewSizeQty.Text.Trim());
            if (!ma.Success)
            {
                MessageBox.Show("入库数量只能是数字");
                return;
            }
            newSizeQty = Convert.ToInt32(this.txtNewSizeQty.Text.Trim());
            if (newSizeQty <= 0)
            {
                MessageBox.Show("请输入入库数量");
                return;
            } 
            this.splitContainer1.Panel1Collapsed = true;
            this.splitContainer1.Panel2.Show();

            string newBoxQty =  this.txtNewBoxQty.Text.Trim();
            string newPo = this.txtNewPo.Text.Trim();
            string newReceiDate = this.dtpNewReceiDate.Value.ToString("yyyy-MM-dd");
            string newReceiNumber = this.txtNewReceiNumber.Text.Trim();
            string newReceiEmp = this.txtNewReceiEmp.Text.Trim();

            string newInDate = DateTime.Now.ToString("yyyy-MM-dd");
            string newInPcName = Dns.GetHostName().ToString();
            string newMark = this.txtMark.Text.Trim();

            DataRow row = this.newReceiDT.NewRow();
            row["id"] = 0;
            row["receiNumber"] = newReceiNumber;
            row["org"] = newOrg;
            row["subinv"] = newSubinv;
            row["line"] = newLine;
            row["style"] = newStyle;
            row["color"] = newColor;
            row["size"] = newSize;
            row["qtyCount"] = newSizeQty;           
            row["boxCount"] = newBoxQty;
            row["po"] = newPo;           
            row["receiDate"] = newReceiDate;
            row["receiEmp"] = newReceiEmp;
            row["mark"] = newMark;            
            row["receiInDate"] = newInDate;
            row["receiInPcName"] = newInPcName;
            this.newReceiDT.Rows.Add(row);

            this.dgvReceiData.DataSource = null;
            this.dgvReceiData.DataSource = this.newReceiDT;
            this.dgvReceiData.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            chang_HeaderText();
            for (int i = 0; i < 10; i++)
            {
                this.dgvReceiData.Columns[i].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
                this.dgvReceiData.Columns[i].ReadOnly = true;
            }
            for (int i= 10; i < 13; i++)
            {
                this.dgvReceiData.Columns[i].ReadOnly = false;
            }
            this.dgvReceiData.Columns[14].ReadOnly = true;
            this.dgvReceiData.Columns[15].ReadOnly = true;
           
        }

        public void chang_HeaderText()
        {

            this.dgvReceiData.Columns["id"].HeaderText = "ID";
            this.dgvReceiData.Columns["id"].Visible = false;

            this.dgvReceiData.Columns["org"].HeaderText = "入库厂区";
            this.dgvReceiData.Columns["subinv"].HeaderText = "入库仓库";
            this.dgvReceiData.Columns["line"].HeaderText = "转厂线别";
            this.dgvReceiData.Columns["receiDate"].HeaderText = "送货日期";
            this.dgvReceiData.Columns["receiNumber"].HeaderText = "送货单号";
            this.dgvReceiData.Columns["style"].HeaderText = "款式";
            this.dgvReceiData.Columns["color"].HeaderText = "色组";
            this.dgvReceiData.Columns["size"].HeaderText = "尺码";

            this.dgvReceiData.Columns["qtyCount"].HeaderText = "件数";

            this.dgvReceiData.Columns["boxCount"].HeaderText = "箱数";
            this.dgvReceiData.Columns["po"].HeaderText = "已沖銷件數";            
            this.dgvReceiData.Columns["receiEmp"].HeaderText = "收货人";
            this.dgvReceiData.Columns["mark"].HeaderText = "备注";

            this.dgvReceiData.Columns["receiInDate"].HeaderText = "录入日期";
            this.dgvReceiData.Columns["receiInPcName"].HeaderText = "录入电脑";
           
        }

        public void chang_SearchDateHeaderText()
        { 

            this.dgvSearchDate.Columns["org"].HeaderText = "入库厂区";
            this.dgvSearchDate.Columns["subinv"].HeaderText = "入库仓库";
            this.dgvSearchDate.Columns["line"].HeaderText = "转厂线别";
            //this.dgvSearchDate.Columns["receiDate"].HeaderText = "送货日期";
            this.dgvSearchDate.Columns["style"].HeaderText = "款式";
            this.dgvSearchDate.Columns["color"].HeaderText = "色组";

            this.dgvSearchDate.Columns["size"].HeaderText = "尺码";
            this.dgvSearchDate.Columns["qtyCount"].HeaderText = "件数";
          //  this.dgvSearchDate.Columns["boxCount"].HeaderText = "箱数";
           // this.dgvSearchDate.Columns["po"].HeaderText = "已沖銷件數";
            //this.dgvSearchDate.Columns["receiEmp"].HeaderText = "收货人";

            //this.dgvSearchDate.Columns["receiInDate"].HeaderText = "录入日期";
            //this.dgvSearchDate.Columns["receiInPcName"].HeaderText = "录入电脑";
        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dgvReceiData.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dgvReceiData.GetClipboardContent());
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
           
            NPOIExcelOutGoing NPOIexcel = new NPOIExcelOutGoing();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(this.dgvReceiData);
            String tableName = "dgvReceiData";

            // DataTable dt = (StyleCodeInfodataGridView.DataSource as DataTable);
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

      
        private void dgvReceiData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (this.dgvReceiData.Rows[e.RowIndex].Selected == false)
                    {
                        this.dgvReceiData.ClearSelection();
                        this.dgvReceiData.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (this.dgvReceiData.SelectedRows.Count == 1)
                    {
                        this.dgvReceiData.CurrentCell = this.dgvReceiData.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                     this.delID =  Convert.ToInt32( this.dgvReceiData.Rows[e.RowIndex].Cells[0].Value.ToString());
                     this.rowIndex = e.RowIndex;
                  //  MessageBox.Show(ids);
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

        private void RmeDelRow_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("确认要删除此行资料吗？", "删除资料", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string org = this.dgvReceiData["org", rowIndex].Value.ToString();
                string subinv = this.dgvReceiData["subinv", rowIndex].Value.ToString();
                string line = this.dgvReceiData["line", rowIndex].Value.ToString();
                string style = this.dgvReceiData["style", rowIndex].Value.ToString();
                string size = this.dgvReceiData["size", rowIndex].Value.ToString();
                int delQty = Convert.ToInt32( this.dgvReceiData["qtyCount", rowIndex].Value);


                int delRows = rm.delRowsByID(delID);
                int delCountQty = 0;
                // 总数量减少
                if (delRows > 0)
                {
                    delCountQty = rm.delStyleCount(org, subinv, line, style, size, delQty);
                }
                if (delCountQty <= 0)
                {
                    MessageBox.Show("更新总数量错误。");
                    return;
                }
               
                for(int i=0;i < this.dgvReceiData.Rows.Count; i++)
                {
                    if(this.dgvReceiData.Rows[i].Cells[0].Value.ToString() == delID.ToString())
                    {
                        this.dgvReceiData.Rows.RemoveAt(i);
                    }
                }
               // MessageBox.Show("共删除 " + delRows + "行资料.");
            }
            else if (dialogResult == DialogResult.No)
            {
                 
            } 
        }

        private void dgvSearchDate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { 
            if (e.RowIndex < 0)
            {
                return;
            } 
            string org = this.dgvSearchDate["org", e.RowIndex].Value.ToString();
            string subinv = this.dgvSearchDate["subinv", e.RowIndex].Value.ToString();
            string line = this.dgvSearchDate["line", e.RowIndex].Value.ToString();
            string style = this.dgvSearchDate["style", e.RowIndex].Value.ToString();
            string size = this.dgvSearchDate["size", e.RowIndex].Value.ToString();
            string color = this.dgvSearchDate["color", e.RowIndex].Value.ToString();

            DataTable insResult = rm.getReceiIns(org, subinv, line, style,size,color);

            this.dgvReceiData.DataSource = null;
            this.dgvReceiData.DataSource = insResult;
            if (this.dgvReceiData == null)
            {
                MessageBox.Show("没有数据，请更改查询条件！");
                Cursor = Cursors.Default;
                return;
            }

            int k = this.dgvReceiData.Rows.Count;
            if (k <= 0)
            {
                MessageBox.Show("没有数据，请更改查询条件！");
                Cursor = Cursors.Default;
                return;
            }
           
            chang_HeaderText();
            for (int i = 0; i < 7; i++)
            {
                this.dgvReceiData.Columns[i].HeaderCell.Style.ForeColor = System.Drawing.Color.Red;
                this.dgvReceiData.Columns[i].ReadOnly = true;
            }
            for (int i = 7; i < 13; i++)
            {
                this.dgvReceiData.Columns[i].ReadOnly = false;
            }
            this.dgvReceiData.Columns[14].ReadOnly = true;
            this.dgvReceiData.Columns[15].ReadOnly = true;           
        }

        private void dgvSearchDate_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
