using BLL;
using COMMON;
using MODEL;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmAsicsImport : Form
    {
        private static FrmAsicsImport frm;
        public int hiedcolumnindex = -1; //是否选中外面
        private int headno = 21; // 行头
        private List<string> files = new List<string>(); // 文件夹下所有文件列表
        private int resultInSetRow = 0;

        public string fileNameStr = "";
        public string sheeTnameStr = "";
        public int headNoStr = -1;

        // public string srcFolderPath = @"D:\OneDrive\桌面\OK\";
        public string srcFolderPath = @"\\192.168.4.105\Users\17.SAA_共用區\13)成品異動掃描\AllPackingPlan_Files\";

        public string srcPath = @"\\192.168.4.105\Users\17.SAA_共用區\13)成品異動掃描\GTN_PackingPlan\";

        private bar barstr = new bar();
        public xiaomingCommom myCommon = new xiaomingCommom();
        private AsicsImportManager asicsIM = new AsicsImportManager();

        //  ld[0] con_ppr_data   ld[1] con_detail_data   ld[0] error_data
        private DataTable con_ppr_data = new DataTable();

        private DataTable con_detail_data = new DataTable();
        private DataTable error_data = new DataTable();

        public AccomplishTask TaskCallBack;
        public UpdateUI UpdateUIDelegate;

        public delegate void AccomplishTask();

        public delegate void UpdateUI(bar barstr);

        private delegate void AsynUpdateUI(bar barstr);

        public FrmAsicsImport()
        {
            InitializeComponent();

            error_data.Columns.Add("ErrorFileName");

            con_ppr_data.Columns.Add("id");
            con_ppr_data.Columns.Add("Cust_id");
            con_ppr_data.Columns.Add("Serial_From");
            con_ppr_data.Columns.Add("qty");
            con_ppr_data.Columns.Add("org");
            con_ppr_data.Columns.Add("PPrfNo");
            con_ppr_data.Columns.Add("count1");
            con_ppr_data.Columns.Add("create_pc");
            con_ppr_data.Columns.Add("update_date");
            con_ppr_data.Columns.Add("con_no");
            con_ppr_data.Columns.Add("country_code");
            con_ppr_data.Columns.Add("con_to");
            con_ppr_data.Columns.Add("Pkg_Code");
            con_ppr_data.Columns.Add("Scan_ID");
            con_ppr_data.Columns.Add("Net_Net");
            con_ppr_data.Columns.Add("con_net");
            con_ppr_data.Columns.Add("con_Gross");
            con_ppr_data.Columns.Add("con_L");
            con_ppr_data.Columns.Add("con_W");
            con_ppr_data.Columns.Add("con_H");
            con_ppr_data.Columns.Add("b_Volume");
            con_ppr_data.Columns.Add("PO");
            con_ppr_data.Columns.Add("MAIN_LINE");

            con_detail_data.Columns.Add("id");
            con_detail_data.Columns.Add("Cust_id");
            con_detail_data.Columns.Add("Serial_From");
            con_detail_data.Columns.Add("Buyer_Item");
            con_detail_data.Columns.Add("Item_desc");
            con_detail_data.Columns.Add("color_code");
            con_detail_data.Columns.Add("Size1");
            con_detail_data.Columns.Add("con_Qty");
            con_detail_data.Columns.Add("qty");
            con_detail_data.Columns.Add("pprfno");

            this.dgvDirectoryFiles.DoubleBufferedDataGirdView(true);
        }

        public static FrmAsicsImport GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmAsicsImport();
            }
            return frm;
        }

        private void dgvDirectoryFiles_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvDirectoryFiles_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvDirectoryFiles.Rows[e.RowIndex].Selected == false)
                    {
                        dgvDirectoryFiles.ClearSelection();
                        dgvDirectoryFiles.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvDirectoryFiles.SelectedRows.Count == 1)
                    {
                        dgvDirectoryFiles.CurrentCell = dgvDirectoryFiles.Rows[e.RowIndex].Cells[e.ColumnIndex];
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

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvDirectoryFiles.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvDirectoryFiles.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            // int tcindex = this.tbOut.SelectedIndex;
            ImproExcel(0);
        }

        public void ImproExcel(int tcIndex)
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
            NPOIExcelOutGoing NPOIexcel = new NPOIExcelOutGoing();
            DataTable tabl = new DataTable();
            if (tcIndex < 0)
            {
                return;
            }
            if (tcIndex == 0)
            {
                tabl = GetDgvToTable(this.dgvDirectoryFiles);
                tableName = "dgvOutgoingTable";
            }

            if (tcIndex == 1)
            {
                tabl = GetDgvToTable(this.dgvDirectoryFiles);
                tableName = "dgvOutCount";
            }

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

        private void FrmAsicsImport_Resize(object sender, EventArgs e)
        {
            this.gbLoad.Width = this.Width - 20;
            this.bgDirectoryFiles.Width = this.Width - 20;
            this.bgDirectoryFiles.Height = this.Height - this.gbLoad.Height - 50;
        }

        private void btnSelectDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dilog = new FolderBrowserDialog();
            this.files.Clear();
            dilog.SelectedPath = this.srcPath;
            // sdfExport.Filter = "Excel 2007文件|*.xlsx|Excel 97-2003文件|*.xls";
            dilog.Description = "请选择文件夹";
            if (dilog.ShowDialog() == DialogResult.OK || dilog.ShowDialog() == DialogResult.Yes)
            {
                this.srcPath = dilog.SelectedPath;
            }
            if (!string.IsNullOrEmpty(this.srcPath))
            {
                this.txtSelectedDirectoryPath.Text = this.srcPath;
                DirectoryInfo folder = new DirectoryInfo(this.srcPath);
                string[] xlsxfiles = Directory.GetFiles(this.srcPath, "*.xlsx", SearchOption.AllDirectories);// System.IO.Directory.GetFiles("c:\","(*.exe | *.txt)");
                foreach (string file in xlsxfiles)
                {
                    this.files.Add(file);
                }
                string[] xlsfiles = Directory.GetFiles(this.srcPath, "*.xls", SearchOption.AllDirectories);
                foreach (string file in xlsfiles)
                {
                    this.files.Add(file);
                }
                string[] pdffiles = Directory.GetFiles(this.srcPath, "*.pdf", SearchOption.AllDirectories);
                foreach (string file in pdffiles)
                {
                    this.files.Add(file);
                }
            }
            else
            {
                this.txtSelectedDirectoryPath.Text = "";
                MessageBox.Show("请选择路径");
                return;
            }
            if (this.files.Count <= 0)
            {
                this.labDirectoryNowFile.Text = "0";
                this.labDirectoryCounts.Text = "0";
                MessageBox.Show("目录下没有装箱单资料");
                return;
            }
            this.labDirectoryNowFile.Text = "0";
            this.labDirectoryCounts.Text = this.files.Count.ToString();
        }

        private void btnLoadDirectory_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.pgBar.Visible = true;
            this.pgBar.Value = 1;
            this.dgvDirectoryFiles.DataSource = null;
            Application.DoEvents();
            this.con_ppr_data.Rows.Clear();
            this.con_detail_data.Rows.Clear();
            this.error_data.Rows.Clear();
            loadExcel();
            this.pgBar.Value = 100;
            Cursor = Cursors.Default;
            this.btnLoadDirectory.Enabled = false;
            this.btnSave.Enabled = false;
        }

        private void loadExcel()
        {
            this.dgvDirectoryFiles.AllowUserToAddRows = false;
            UpdateUIDelegate += UpdataUIStatus;//绑定更新任务状态的委托
            TaskCallBack += Accomplish;//绑定完成任务要调用的委托
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerAsync();
            }
        }

        public void UpdataUIStatus(bar barstr)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (bar s)
                {
                    this.pgBar.Maximum = s.maxstep;
                    this.pgBar.Value = s.step;
                    this.gbLoad.Text = s.str + "    " + s.step.ToString() + "/" + s.maxstep.ToString();
                }), barstr);
            }
            else
            {
                this.pgBar.Minimum = 0;
                this.pgBar.Maximum = barstr.maxstep;
                this.pgBar.Value = barstr.step;
                this.gbLoad.Text = barstr.str + "    " + barstr.step.ToString() + "/" + barstr.maxstep.ToString();
            }
        }

        //完成任务时需要调用
        private void Accomplish()
        {
            if (con_ppr_data != null)
            {
                this.dgvDirectoryFiles.DataSource = con_ppr_data;
                //  changHeaderText();
                Cursor = Cursors.Default;
                this.dgvDirectoryFiles.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                MessageBox.Show("加载完成");
            }
            TaskCallBack -= Accomplish; //取消侦听注册事件，避免多次侦听
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataTable> ld = new List<DataTable>();
            // 加载文件
            for (int i = 0; i < this.files.Count; i++)
            {
                barstr.str = "正在加载:" + this.files[i] + "...";
                barstr.step = i;
                barstr.maxstep = this.files.Count;
                UpdateUIDelegate(barstr);
                FileInfo myFileinfo = new FileInfo(this.files[i]);
                string ext = myFileinfo.Extension.ToString().ToUpper();
                if (ext == ".XLS" || ext == ".XLSX")
                {
                    ld = asicsIM.ExcelRead(this.files[i], "Sheet1", this.headno);

                    if (ld[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ld[0].Rows)
                        {
                            this.con_ppr_data.ImportRow(r);
                        }
                    }
                    if (ld[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ld[1].Rows)
                        {
                            this.con_detail_data.ImportRow(r);
                        }
                    }
                    if (ld[2].Rows.Count > 0)
                    {
                        foreach (DataRow r in ld[2].Rows)
                        {
                            this.error_data.ImportRow(r);
                        }
                    }
                }
                else if (ext == ".PDF")
                {
                    string pdfText = pdf2txt(new FileInfo(this.files[i]));

                    if (pdfText.Length > 0)
                    {
                        string[] barcodestr = pdfText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        string firstLine = barcodestr[0];
                        List<int> styleIndexs = new List<int>();

                        for (int index = 0; index < barcodestr.Length -1; index++)
                        {
                            // 第一种
                            if (barcodestr[index] == "STYLE:")
                            {
                                //混码装
                                if (barcodestr[index + 2] == "MIXED")
                                {
                                    string PO = barcodestr[index + 4].ToString().Substring(4, barcodestr[index + 4].ToString().Length - 4).Trim();
                                    string PPrfNo = PO + "-2";
                                    string MAIN_LINE = "";
                                    string Serial_From = barcodestr[index + 6].ToString().Trim();
                                    string id = "ASICS-" + barcodestr[index + 6].ToString().Trim();
                                    string Net_Net = "0.00";
                                    string con_net = "0.00";
                                    string con_Gross = "0.00";
                                    string count1 = "1";
                                    string con_no = Convert.ToInt32( Serial_From.Substring(Serial_From.Length-4, 4)).ToString();
                                    string con_to = con_no.ToString();
                                    // string con_no = (index + 1).ToString();
                                    // string con_to = (index + 1).ToString();
                                    string org = "SAA";
                                    string Cust_id = "ASICS";
                                    string create_pc = Dns.GetHostName().ToString().ToUpper();
                                    string update_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    string country_code = "";
                                    string Pkg_Code = "ASC";
                                    string Scan_ID = "";
                                    string con_L = "0.00";
                                    string con_W = "0.00";
                                    string con_H = "0.00";
                                    string b_Volume = "0.00";
                                    string Buyer_Item = barcodestr[index + 9].ToString().Trim();
                                    string Item_desc = "";
                                    string color_code = barcodestr[index + 10].ToString().Trim();
                                    string[] counts = barcodestr[index + 11].ToString().Trim().Split(' ');
                                    string[] sizes = barcodestr[index + 7].Split(':')[1].Trim().Split(' ');
                                    int countQty = 0;
                                    foreach (string cqty in counts)
                                    {
                                        countQty = countQty + Convert.ToInt32(cqty);
                                    }
                                    string qty = countQty.ToString();
                                    string con_Qty = qty;
                                    DataRow con_ppr_dataRow = this.con_ppr_data.NewRow();
                                    con_ppr_dataRow["PPrfNo"] = PPrfNo;
                                    con_ppr_dataRow["PO"] = PO;
                                    con_ppr_dataRow["MAIN_LINE"] = MAIN_LINE;
                                    con_ppr_dataRow["Serial_From"] = Serial_From;
                                    con_ppr_dataRow["id"] = id;
                                    con_ppr_dataRow["qty"] = qty;
                                    con_ppr_dataRow["Net_Net"] = Net_Net;
                                    con_ppr_dataRow["con_net"] = con_net;
                                    con_ppr_dataRow["con_Gross"] = con_Gross;
                                    con_ppr_dataRow["count1"] = count1;
                                    con_ppr_dataRow["con_no"] = con_no;
                                    con_ppr_dataRow["con_to"] = con_to;
                                    con_ppr_dataRow["org"] = org;
                                    con_ppr_dataRow["Cust_id"] = Cust_id;
                                    con_ppr_dataRow["create_pc"] = create_pc;
                                    con_ppr_dataRow["update_date"] = update_date;
                                    con_ppr_dataRow["country_code"] = country_code;
                                    con_ppr_dataRow["Pkg_Code"] = Pkg_Code;
                                    con_ppr_dataRow["Scan_ID"] = Scan_ID;
                                    con_ppr_dataRow["con_L"] = con_L;
                                    con_ppr_dataRow["con_W"] = con_W;
                                    con_ppr_dataRow["con_H"] = con_H;
                                    con_ppr_dataRow["b_Volume"] = b_Volume;
                                    con_ppr_data.Rows.Add(con_ppr_dataRow);

                                    for (int k = 0; k < sizes.Length; k++)
                                    {
                                        DataRow con_detail_dataRow = this.con_detail_data.NewRow();
                                        con_detail_dataRow["id"] = "ASICS-" + Serial_From;
                                        con_detail_dataRow["Cust_id"] = "ASICS";
                                        con_detail_dataRow["Serial_From"] = Serial_From;
                                        con_detail_dataRow["Buyer_Item"] = Buyer_Item;
                                        con_detail_dataRow["Item_desc"] = Item_desc;
                                        con_detail_dataRow["color_code"] = color_code;
                                        con_detail_dataRow["Size1"] = sizes[k];
                                        con_detail_dataRow["con_Qty"] = con_Qty;
                                        con_detail_dataRow["qty"] = counts[k];
                                        con_detail_dataRow["pprfno"] = PPrfNo;
                                        con_detail_data.Rows.Add(con_detail_dataRow);
                                    }
                                }
                                //单码装
                                else if (barcodestr[index + 5] == "QUANTITY:")
                                {
                                    string PO = barcodestr[index + 9].ToString().Trim();
                                    string PPrfNo = PO + "-2";
                                    string MAIN_LINE = "";
                                    string Serial_From = barcodestr[index + 11].ToString().Trim();
                                    string id = "ASICS-" + barcodestr[index + 11].ToString().Trim();
                                    string Net_Net = "0.00";
                                    string con_net = "0.00";
                                    string con_Gross = "0.00";
                                    string count1 = "1";
                                    string con_no = Convert.ToInt32(Serial_From.Substring(Serial_From.Length - 4, 4)).ToString();
                                    string con_to = con_no.ToString();
                                    // string con_no = (index + 1).ToString();
                                    // string con_to = (index + 1).ToString();
                                    string org = "SAA";
                                    string Cust_id = "ASICS";
                                    string create_pc = Dns.GetHostName().ToString().ToUpper();
                                    string update_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    string country_code = "";
                                    string Pkg_Code = "ASC";
                                    string Scan_ID = "";
                                    string con_L = "0.00";
                                    string con_W = "0.00";
                                    string con_H = "0.00";
                                    string b_Volume = "0.00";
                                    string Buyer_Item = barcodestr[index + 2].ToString().Trim();
                                    string Item_desc = "";
                                    string color_code = barcodestr[index + 4].ToString().Trim();
                                    string counts = barcodestr[index + 8].ToString().Trim();
                                    string sizes = barcodestr[index + 3].ToString().Trim();
                                    string con_Qty = counts;
                                    string qty = counts;

                                    DataRow con_ppr_dataRow = this.con_ppr_data.NewRow();
                                    con_ppr_dataRow["PPrfNo"] = PPrfNo;
                                    con_ppr_dataRow["PO"] = PO;
                                    con_ppr_dataRow["MAIN_LINE"] = MAIN_LINE;
                                    con_ppr_dataRow["Serial_From"] = Serial_From;
                                    con_ppr_dataRow["id"] = id;
                                    con_ppr_dataRow["qty"] = qty;
                                    con_ppr_dataRow["Net_Net"] = Net_Net;
                                    con_ppr_dataRow["con_net"] = con_net;
                                    con_ppr_dataRow["con_Gross"] = con_Gross;
                                    con_ppr_dataRow["count1"] = count1;
                                    con_ppr_dataRow["con_no"] = con_no;
                                    con_ppr_dataRow["con_to"] = con_to;
                                    con_ppr_dataRow["org"] = org;
                                    con_ppr_dataRow["Cust_id"] = Cust_id;
                                    con_ppr_dataRow["create_pc"] = create_pc;
                                    con_ppr_dataRow["update_date"] = update_date;
                                    con_ppr_dataRow["country_code"] = country_code;
                                    con_ppr_dataRow["Pkg_Code"] = Pkg_Code;
                                    con_ppr_dataRow["Scan_ID"] = Scan_ID;
                                    con_ppr_dataRow["con_L"] = con_L;
                                    con_ppr_dataRow["con_W"] = con_W;
                                    con_ppr_dataRow["con_H"] = con_H;
                                    con_ppr_dataRow["b_Volume"] = b_Volume;
                                    con_ppr_data.Rows.Add(con_ppr_dataRow);

                                    DataRow con_detail_dataRow = this.con_detail_data.NewRow();
                                    con_detail_dataRow["id"] = "ASICS-" + Serial_From;
                                    con_detail_dataRow["Cust_id"] = "ASICS";
                                    con_detail_dataRow["Serial_From"] = Serial_From;
                                    con_detail_dataRow["Buyer_Item"] = Buyer_Item;
                                    con_detail_dataRow["Item_desc"] = Item_desc;
                                    con_detail_dataRow["color_code"] = color_code;
                                    con_detail_dataRow["Size1"] = sizes;
                                    con_detail_dataRow["con_Qty"] = con_Qty;
                                    con_detail_dataRow["qty"] = counts;
                                    con_detail_dataRow["pprfno"] = PPrfNo;
                                    con_detail_data.Rows.Add(con_detail_dataRow);
                                }
                            }
                            // 第二种
                            else if (barcodestr[index + 1].Length >12  &&  barcodestr[index+1].Substring(0,5) == "STYLE")
                            {
                                // 单码装
                                if (barcodestr[index + 6].Split(':')[0].Trim() == "MANF DATE")
                                {
                                    string PO = barcodestr[index + 5].Split(':')[1].Trim().Split(' ')[0].Trim();
                                    string PPrfNo = PO + "-2";
                                    string MAIN_LINE = "";
                                    string Serial_From = barcodestr[index].Trim().Substring(1, barcodestr[index].Length - 2);
                                    string id = "ASICS-" + Serial_From;
                                    string Net_Net = "0.00";
                                    string con_net = "0.00";
                                    string con_Gross = "0.00";
                                    string count1 = "1";
                                    string con_no = Convert.ToInt32(Serial_From.Substring(Serial_From.Length - 4, 4)).ToString();
                                    string con_to = con_no.ToString();
                                    // string con_no = (index + 1).ToString();
                                    // string con_to = (index + 1).ToString();
                                    string org = "SAA";
                                    string Cust_id = "ASICS";
                                    string create_pc = Dns.GetHostName().ToString().ToUpper();
                                    string update_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    string country_code = "";
                                    string Pkg_Code = "ASC";
                                    string Scan_ID = "";
                                    string con_L = "0.00";
                                    string con_W = "0.00";
                                    string con_H = "0.00";
                                    string b_Volume = "0.00";
                                    string Buyer_Item = barcodestr[index + 1].Split(':')[1].Trim();
                                    string Item_desc = "";
                                    string color_code = barcodestr[index + 2].Split(':')[1].Trim();

                                    string counts = Convert.ToInt32( barcodestr[index + 4].ToString().Trim().Split(':')[1]).ToString();
                                    string sizes = barcodestr[index + 3].Trim().Split(':')[1].Trim();
                                    string con_Qty = counts;
                                    string qty = counts;

                                    DataRow con_ppr_dataRow = this.con_ppr_data.NewRow();
                                    con_ppr_dataRow["PPrfNo"] = PPrfNo;
                                    con_ppr_dataRow["PO"] = PO;
                                    con_ppr_dataRow["MAIN_LINE"] = MAIN_LINE;
                                    con_ppr_dataRow["Serial_From"] = Serial_From;
                                    con_ppr_dataRow["id"] = id;
                                    con_ppr_dataRow["qty"] = qty;
                                    con_ppr_dataRow["Net_Net"] = Net_Net;
                                    con_ppr_dataRow["con_net"] = con_net;
                                    con_ppr_dataRow["con_Gross"] = con_Gross;
                                    con_ppr_dataRow["count1"] = count1;
                                    con_ppr_dataRow["con_no"] = con_no;
                                    con_ppr_dataRow["con_to"] = con_to;
                                    con_ppr_dataRow["org"] = org;
                                    con_ppr_dataRow["Cust_id"] = Cust_id;
                                    con_ppr_dataRow["create_pc"] = create_pc;
                                    con_ppr_dataRow["update_date"] = update_date;
                                    con_ppr_dataRow["country_code"] = country_code;
                                    con_ppr_dataRow["Pkg_Code"] = Pkg_Code;
                                    con_ppr_dataRow["Scan_ID"] = Scan_ID;
                                    con_ppr_dataRow["con_L"] = con_L;
                                    con_ppr_dataRow["con_W"] = con_W;
                                    con_ppr_dataRow["con_H"] = con_H;
                                    con_ppr_dataRow["b_Volume"] = b_Volume;
                                    con_ppr_data.Rows.Add(con_ppr_dataRow);

                                    DataRow con_detail_dataRow = this.con_detail_data.NewRow();
                                    con_detail_dataRow["id"] = "ASICS-" + Serial_From;
                                    con_detail_dataRow["Cust_id"] = "ASICS";
                                    con_detail_dataRow["Serial_From"] = Serial_From;
                                    con_detail_dataRow["Buyer_Item"] = Buyer_Item;
                                    con_detail_dataRow["Item_desc"] = Item_desc;
                                    con_detail_dataRow["color_code"] = color_code;
                                    con_detail_dataRow["Size1"] = sizes;
                                    con_detail_dataRow["con_Qty"] = con_Qty;
                                    con_detail_dataRow["qty"] = counts;
                                    con_detail_dataRow["pprfno"] = PPrfNo;
                                    con_detail_data.Rows.Add(con_detail_dataRow);

                                }
                                // 混码装
                                else if (barcodestr[index + 6].Split(':')[0].Trim() == "SIZE")
                                {
                                    string PO = barcodestr[index + 5].Split(':')[1].Trim().Split(' ')[0].Trim();
                                    string PPrfNo = PO + "-2";
                                    string MAIN_LINE = "";
                                    string Serial_From = barcodestr[index].Trim().Substring(1, barcodestr[index].Length - 2);
                                    string id = "ASICS-" + Serial_From;
                                    string Net_Net = "0.00";
                                    string con_net = "0.00";
                                    string con_Gross = "0.00";
                                    string count1 = "1";
                                    string con_no = Convert.ToInt32(Serial_From.Substring(Serial_From.Length - 4, 4)).ToString();
                                    string con_to = con_no.ToString();
                                    // string con_no = (index + 1).ToString();
                                    // string con_to = (index + 1).ToString();
                                    string org = "SAA";
                                    string Cust_id = "ASICS";
                                    string create_pc = Dns.GetHostName().ToString().ToUpper();
                                    string update_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    string country_code = "";
                                    string Pkg_Code = "ASC";
                                    string Scan_ID = "";
                                    string con_L = "0.00";
                                    string con_W = "0.00";
                                    string con_H = "0.00";
                                    string b_Volume = "0.00";
                                    string Buyer_Item = barcodestr[index + 1].Split(':')[1].Trim();
                                    string Item_desc = "";
                                    string color_code = barcodestr[index + 2].Split(':')[1].Trim();

                                    string[] counts = barcodestr[index + 9].ToString().Trim().Split(' ');
                                    string[] sizes = barcodestr[index + 8].ToString().Trim().Split(' ');
                                    int countQty = 0;
                                    foreach (string cqty in counts)
                                    {
                                        countQty = countQty + Convert.ToInt32(cqty);
                                    }
                                    string qty = countQty.ToString();
                                    string con_Qty = qty;
                                    DataRow con_ppr_dataRow = this.con_ppr_data.NewRow();
                                    con_ppr_dataRow["PPrfNo"] = PPrfNo;
                                    con_ppr_dataRow["PO"] = PO;
                                    con_ppr_dataRow["MAIN_LINE"] = MAIN_LINE;
                                    con_ppr_dataRow["Serial_From"] = Serial_From;
                                    con_ppr_dataRow["id"] = id;
                                    con_ppr_dataRow["qty"] = qty;
                                    con_ppr_dataRow["Net_Net"] = Net_Net;
                                    con_ppr_dataRow["con_net"] = con_net;
                                    con_ppr_dataRow["con_Gross"] = con_Gross;
                                    con_ppr_dataRow["count1"] = count1;
                                    con_ppr_dataRow["con_no"] = con_no;
                                    con_ppr_dataRow["con_to"] = con_to;
                                    con_ppr_dataRow["org"] = org;
                                    con_ppr_dataRow["Cust_id"] = Cust_id;
                                    con_ppr_dataRow["create_pc"] = create_pc;
                                    con_ppr_dataRow["update_date"] = update_date;
                                    con_ppr_dataRow["country_code"] = country_code;
                                    con_ppr_dataRow["Pkg_Code"] = Pkg_Code;
                                    con_ppr_dataRow["Scan_ID"] = Scan_ID;
                                    con_ppr_dataRow["con_L"] = con_L;
                                    con_ppr_dataRow["con_W"] = con_W;
                                    con_ppr_dataRow["con_H"] = con_H;
                                    con_ppr_dataRow["b_Volume"] = b_Volume;
                                    con_ppr_data.Rows.Add(con_ppr_dataRow);

                                    for (int k = 0; k < sizes.Length; k++)
                                    {
                                        DataRow con_detail_dataRow = this.con_detail_data.NewRow();
                                        con_detail_dataRow["id"] = "ASICS-" + Serial_From;
                                        con_detail_dataRow["Cust_id"] = "ASICS";
                                        con_detail_dataRow["Serial_From"] = Serial_From;
                                        con_detail_dataRow["Buyer_Item"] = Buyer_Item;
                                        con_detail_dataRow["Item_desc"] = Item_desc;
                                        con_detail_dataRow["color_code"] = color_code;
                                        con_detail_dataRow["Size1"] = sizes[k];
                                        con_detail_dataRow["con_Qty"] = con_Qty;
                                        con_detail_dataRow["qty"] = counts[k];
                                        con_detail_dataRow["pprfno"] = PPrfNo;
                                        con_detail_data.Rows.Add(con_detail_dataRow);
                                    }
                                }

                            }

                        }

                    }
                }
            }
            // 删除加载失败的文件
            for (int i = 0; i < this.files.Count; i++)
            {
                foreach (DataRow r in error_data.Rows)
                {
                    if (this.files[i] == r["ErrorFileName"].ToString())
                    {
                        this.files.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public string pdf2txt(FileInfo pdffile)
        {
            PDDocument doc = PDDocument.load(pdffile.FullName);
            PDFTextStripper pdfStripper = new PDFTextStripper();
            string text = pdfStripper.getText(doc);

            return text.ToUpper();
            //   StreamWriter swPdfChange = new StreamWriter(txtfile.FullName, false, Encoding.GetEncoding("gb2312"));
            //  swPdfChange.Write(text);
            //  swPdfChange.Close();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnLoadDirectory.Enabled = true;
            this.btnSave.Enabled = true;
            barstr.str = "加载完成";
            barstr.step = 100;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);
            this.dgvDirectoryFiles.DataSource = "";
            this.dgvDirectoryFiles.DataSource = this.con_ppr_data;

            this.gbLoad.Text = "导入条件";
            barstr.str = "导入条件";
            barstr.step = 0;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            resultInSetRow = 0;
           // MessageBox.Show("开始上传文件,如果遇到程自动关闭,请关闭360等杀毒，电脑管家再运行...");
            if (this.con_ppr_data.Rows.Count <= 0)
            {
                return;
            }
            resultInSetRow = resultInSetRow + asicsIM.uploadToMysql(this.con_ppr_data);

            if (this.con_detail_data.Rows.Count <= 0)
            {
                return;
            }
            resultInSetRow = resultInSetRow + asicsIM.uploadCon_detailToMysql(this.con_detail_data);
            //   string foldName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Dns.GetHostName() + "_ASICS(" + this.files.Count.ToString() + ")";
            string foldName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Dns.GetHostName().ToUpper() + "_ASICS(" + this.files.Count.ToString() + ")";
            if (!Directory.Exists(srcFolderPath + foldName))
            {
                Directory.CreateDirectory(srcFolderPath + foldName);
            }
            /*
        //  barstr.maxstep = this.files.Count;
          for (int i = 0; i < this.files.Count; i++)
          {
             // barstr.str = "正在复制:" + this.files[i] + "...";
           //   barstr.step = i;
           //   UpdateUIDelegate(barstr);

              string[] fileName = this.files[i].Split('\\');
              string filestr = fileName[fileName.Length - 1];
              string drcPath = srcFolderPath + foldName + @"\" + filestr;
              string cc = "";
              FileOperateProxy.CopyFile(this.files[i], drcPath, false, false, false, ref cc);
              FileOperateProxy.DeleteFile(this.files[i], false, false, false, ref cc);
          }
            */
            barstr.maxstep = this.files.Count;
            int i = 1;
            // 移动加载成功的文件
            foreach (string file in this.files)
            {
                barstr.str = "正在复制:" + file + "...";
                barstr.step = i;
                UpdateUIDelegate(barstr);
                i++;
                string[] fileName = file.Split('\\');
                string filestr = fileName[fileName.Length - 1];
                string drcPath = srcFolderPath + foldName + @"\" + filestr;
                string cc = "";
                string dd = "";
                int c = FileOperateProxy.CopyFile(file, drcPath, false, false, false, ref cc);
                if (c == 0)
                {
                    // 360会直接把程关闭删除会报错
                    FileOperateProxy.DeleteFile(file, false, false, false, ref dd);
                }
            }
            MessageBox.Show("保存成功");
        }

        private void FrmAsicsImport_Load(object sender, EventArgs e)
        {
            //this.txtSelectedDirectoryPath.Text = @"\\192.168.4.105\Users\17.SAA_共用區\13)成品異動掃描\GTN_PackingPlan";
        }
    }
}