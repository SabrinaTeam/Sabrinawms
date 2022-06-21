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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmRFIDNikeImport : Form
    {
        private static FrmRFIDNikeImport frm;
        public int hiedcolumnindex = -1; //是否选中外面

        private int headno = 21; // 行头
        private List<string> files = new List<string>(); // 文件夹下所有文件列表
        private int resultInSetRow = 0;

        public string fileNameStr = "";
        public string sheeTnameStr = "";
        public int headNoStr = -1;

        // public string srcFolderPath = @"D:\OneDrive\桌面\OK\";
        public string srcFolderPath = @"\\192.168.4.105\Users\17.SAA_共用區\RFID_NIKE\uploaded\";
        public string srcPath = @"\\192.168.4.105\Users\17.SAA_共用區\RFID_NIKE\";

        private bar barstr = new bar();
       // public xiaomingCommom myCommon = new xiaomingCommom();
        private FrmRFIDNikeImportManager NikeManager = new FrmRFIDNikeImportManager();


        private DataTable RfidTag = new DataTable();

        public AccomplishTask TaskCallBack;
        public UpdateUI UpdateUIDelegate;
        public delegate void AccomplishTask();
        public delegate void UpdateUI(bar barstr);
        private delegate void AsynUpdateUI(bar barstr);
        public FrmRFIDNikeImport()
        {
            InitializeComponent();
             this.dgvDirectoryFiles.DoubleBufferedDataGirdView(true);

            RfidTag.Columns.Add("CustID");
            RfidTag.Columns.Add("SKU");
            RfidTag.Columns.Add("ColorName");
            RfidTag.Columns.Add("SizeName");
            RfidTag.Columns.Add("Style");
            RfidTag.Columns.Add("PONumber");
            RfidTag.Columns.Add("Qtys");
            RfidTag.Columns.Add("Seanson");
            RfidTag.Columns.Add("StyleColor");
            RfidTag.Columns.Add("ColorCode");
            RfidTag.Columns.Add("Note");

            RfidTag.Columns.Add("ORDERNO");
            RfidTag.Columns.Add("CUST_TRACK");
            RfidTag.Columns.Add("TOTAL_QTY");
            RfidTag.Columns.Add("PACKAGING");
            RfidTag.Columns.Add("RowsNO");
            RfidTag.Columns.Add("COUNTRY_OF_ORIGIN");
            RfidTag.Columns.Add("KRAFT_HANGTAG");
            RfidTag.Columns.Add("FCT_CODE");
        }
        public static FrmRFIDNikeImport GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmRFIDNikeImport();
            }
            return frm;
        }

        private void FrmRFIDLuluImport_Resize(object sender, EventArgs e)
        {
            this.gbLoad.Width = this.Width - 20;
            this.gbLoad.Left = (this.Width - this.gbLoad.Width - 20 )/ 2;
            this.bgDirectoryFiles.Left = this.gbLoad.Left;
            this.bgDirectoryFiles.Width = this.gbLoad.Width;
            this.bgDirectoryFiles.Height = this.Height - this.gbLoad.Height - 50;
            this.btnSave.Left =this.gbLoad.Width - this.btnSave.Width -5;
            this.pgBar.Width = this.gbLoad.Width - 10 ;
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

        private void btnSelectDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dilog = new FolderBrowserDialog();
            this.files.Clear();
            dilog.SelectedPath = this.srcPath;
            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "PDF 文件|*.PDF";
            dilog.Description = "请选择文件夹";
            if (dilog.ShowDialog() == DialogResult.OK || dilog.ShowDialog() == DialogResult.Yes)
            {
                this.srcPath = dilog.SelectedPath;
            }
            if (!string.IsNullOrEmpty(this.srcPath))
            {
                this.txtSelectedDirectoryPath.Text = this.srcPath;
                DirectoryInfo folder = new DirectoryInfo(this.srcPath);
                /*
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
                */
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
                MessageBox.Show("目录下没有 UPC 资料");
                return;
            }
            this.labDirectoryNowFile.Text = "0";
            this.labDirectoryCounts.Text = this.files.Count.ToString();
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

        private void btnLoadDirectory_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.pgBar.Visible = true;
            this.pgBar.Value = 1;
            this.dgvDirectoryFiles.DataSource = null;
            Application.DoEvents();
            this.RfidTag.Rows.Clear();
            loadExcel();
            this.pgBar.Maximum = 100;
            this.pgBar.Value = 100;
            Cursor = Cursors.Default;
            this.btnLoadDirectory.Enabled = false;
            this.btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            resultInSetRow = 0;
            if (this.RfidTag.Rows.Count <= 0)
            {
                return;
            }
            resultInSetRow = resultInSetRow + NikeManager.uploadToMysql(this.RfidTag);

            string foldName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Dns.GetHostName().ToUpper() + "_NIKE(" + this.files.Count.ToString() + ")";
            if (!Directory.Exists(srcFolderPath + foldName))
            {
                Directory.CreateDirectory(srcFolderPath + foldName);
            }

            barstr.maxstep = this.files.Count;
            int i = 1;
            // 移动加载成功的文件
            foreach (string file in this.files)
            {
                barstr.str = "正在上传:" + file + "...";
                barstr.step = i;
                UpdateUIDelegate(barstr);
                i++;
                string[] fileName = file.Split('\\');
                string filestr = fileName[fileName.Length - 1];
                string drcPath = srcFolderPath + foldName + @"\" + filestr+".bak";
                string cc = "";
                string dd = "";
                 File.Copy(file, drcPath, true);
                File.Delete(file);

              //  int c = FileOperateProxy.CopyFile(file, drcPath, false, false, false, ref cc);
               // if (c == 0)
               // {
                    // 360会直接把程关闭删除会报错
                //    FileOperateProxy.DeleteFile(file, false, false, false, ref dd);
               // }
            }
            this.files.Clear();
            MessageBox.Show("保存成功");
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
            if (RfidTag != null)
            {
                this.dgvDirectoryFiles.DataSource = RfidTag;
                //  changHeaderText();
                Cursor = Cursors.Default;
                this.dgvDirectoryFiles.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                MessageBox.Show("加载完成");
            }
            TaskCallBack -= Accomplish; //取消侦听注册事件，避免多次侦听
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
            this.dgvDirectoryFiles.DataSource = this.RfidTag;

            this.gbLoad.Text = "导入条件";
            barstr.str = "导入条件";
            barstr.step = 0;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);
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
              //  string ext = myFileinfo.Extension.ToString().ToUpper();
                    string pdfText = pdf2txt(new FileInfo(this.files[i]));
                    if (pdfText.Length > 0)
                    {
                        string[] barcodestr = pdfText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        string firstLine = barcodestr[0];
                        List<int> styleIndexs = new List<int>();
                    // string Rows = "";// 行的明细资料
                    styleIndexs.Add(0);
                    for (int index = 0; index < barcodestr.Length - 1; index++)
                    {
                        if (barcodestr[index].Split(' ')[0] == "PAGE")
                        {
                            styleIndexs.Add(index);  // 一共有多少页  每一页的结束标志 'PAGE' 索引为 Index
                        }
                    }

                    if (styleIndexs.Count > 0)
                    {
                        for (int Z = 1; Z < styleIndexs.Count; Z++)
                        {
                            for (int index = styleIndexs[Z-1]; index < styleIndexs[Z]; index++)
                            {
                                if (barcodestr[index] == "ORDER AUDIT REPORT/PACKING LIST")
                                {
                                    // 第一种
                                    if(barcodestr[index + 56].ToString().Trim().Split(' ')[0] == "PACKAGING" &&
                                         barcodestr[index + 45].ToString().Trim().Split(' ')[0] == "MAXIM")
                                    {

                                        string ORDERNO = barcodestr[index + 20].ToString().Trim();
                                        string PO = barcodestr[index + 45].ToString().Trim();
                                        string CUST_TRACK = barcodestr[index + 47].ToString().Trim();
                                        string TOTAL_QTY = barcodestr[index + 49].ToString().Trim().Replace(",","");
                                        string PACKAGING = barcodestr[index + 54].ToString().Trim().Split(' ')[1];
                                        for (int X = index + 61; X < styleIndexs[Z]; X++)
                                        {
                                            string RowStr = barcodestr[X].ToString().Trim();  //  ++ 63 64 ... 一共有多少行
                                            string[] RowDetails = RowStr.Split(' ');
                                            if (RowDetails.Length < 11)
                                            {
                                                continue;
                                            }
                                            string RowsNO = RowDetails[8].ToString().Trim();
                                            string NIKEPO = RowDetails[0].ToString().Trim();
                                            string UPC = RowDetails[9].ToString().Trim();
                                            UPC = UPC.Substring(UPC.Length - 12, 12);
                                            string FCT_CODE = RowDetails[1].ToString().Trim();
                                            string SEA_COD = RowDetails[2].ToString().Trim();
                                            string STYLE = RowDetails[3].ToString().Trim();
                                            string COLOR = RowDetails[4].ToString().Trim();
                                            string SIZE = RowDetails[5].ToString().Trim();
                                            string COUNTRY_OF_ORIGIN = RowDetails[10].ToString().Trim();
                                            string XCOLORBAR = RowDetails[9].ToString().Trim();
                                            XCOLORBAR = XCOLORBAR.Substring(0, XCOLORBAR.Length - 12);
                                            string QUANTITY = RowDetails[7].ToString().Trim().Replace(",", "");
                                            string KRAFT_HANGTAG = RowDetails[6].ToString().Trim();

                                            DataRow RfidTagRow = this.RfidTag.NewRow();
                                            RfidTagRow["CustID"] = "NIKE";
                                            RfidTagRow["SKU"] = UPC;
                                            RfidTagRow["ColorName"] = XCOLORBAR;
                                            RfidTagRow["SizeName"] = SIZE;
                                            RfidTagRow["Style"] = STYLE;
                                            RfidTagRow["PONumber"] = NIKEPO;
                                            RfidTagRow["Qtys"] = QUANTITY;
                                            RfidTagRow["Seanson"] = SEA_COD;
                                            RfidTagRow["StyleColor"] = STYLE +"-"+ COLOR;
                                            RfidTagRow["ColorCode"] = COLOR;
                                            RfidTagRow["Note"] = "";

                                            RfidTagRow["ORDERNO"] = ORDERNO;
                                            RfidTagRow["CUST_TRACK"] = CUST_TRACK;
                                            RfidTagRow["TOTAL_QTY"] = TOTAL_QTY;
                                            RfidTagRow["PACKAGING"] = PACKAGING;
                                            RfidTagRow["RowsNO"] = RowsNO;
                                            RfidTagRow["COUNTRY_OF_ORIGIN"] = COUNTRY_OF_ORIGIN;
                                            RfidTagRow["KRAFT_HANGTAG"] = KRAFT_HANGTAG;
                                            RfidTagRow["FCT_CODE"] = FCT_CODE;

                                            RfidTag.Rows.Add(RfidTagRow);
                                        }

                                        // 第二种
                                    }
                                    else if(barcodestr[index + 55].ToString().Trim().Split(' ')[0] == "PACKAGING" &&
                                              barcodestr[index + 44].ToString().Trim().Split(' ')[0] == "MAXIM")
                                    {
                                        string ORDERNO = barcodestr[index + 20].ToString().Trim();
                                        string PO = barcodestr[index + 45].ToString().Trim();
                                        string CUST_TRACK = barcodestr[index + 47].ToString().Trim();
                                        string TOTAL_QTY = barcodestr[index + 49].ToString().Trim().Replace(",", "");
                                        string PACKAGING = barcodestr[index + 55].ToString().Trim().Split(' ')[1];
                                        for (int X = index + 62; X < styleIndexs[Z]; X++)
                                        {
                                            string RowStr = barcodestr[X].ToString().Trim();  //  ++ 63 64 ... 一共有多少行
                                            string[] RowDetails = RowStr.Split(' ');
                                            if (RowDetails.Length < 11)
                                            {
                                                continue;
                                            }
                                            string RowsNO = RowDetails[8].ToString().Trim();
                                            string NIKEPO = RowDetails[0].ToString().Trim();
                                            string UPC = RowDetails[9].ToString().Trim();
                                            UPC = UPC.Substring(UPC.Length - 12, 12);
                                            string FCT_CODE = RowDetails[1].ToString().Trim();
                                            string SEA_COD = RowDetails[2].ToString().Trim();
                                            string STYLE = RowDetails[3].ToString().Trim();
                                            string COLOR = RowDetails[4].ToString().Trim();
                                            string SIZE = RowDetails[5].ToString().Trim();
                                            string COUNTRY_OF_ORIGIN = RowDetails[10].ToString().Trim();
                                            string XCOLORBAR = RowDetails[9].ToString().Trim();
                                            XCOLORBAR = XCOLORBAR.Substring(0, XCOLORBAR.Length - 12);
                                            string QUANTITY = RowDetails[7].ToString().Trim().Replace(",", "");
                                            string KRAFT_HANGTAG = RowDetails[6].ToString().Trim();

                                            DataRow RfidTagRow = this.RfidTag.NewRow();
                                            RfidTagRow["CustID"] = "NIKE";
                                            RfidTagRow["SKU"] = UPC;
                                            RfidTagRow["ColorName"] = XCOLORBAR;
                                            RfidTagRow["SizeName"] = SIZE;
                                            RfidTagRow["Style"] = STYLE;
                                            RfidTagRow["PONumber"] = NIKEPO;
                                            RfidTagRow["Qtys"] = QUANTITY;
                                            RfidTagRow["Seanson"] = SEA_COD;
                                            RfidTagRow["StyleColor"] = STYLE + "-" + COLOR;
                                            RfidTagRow["ColorCode"] = COLOR;
                                            RfidTagRow["Note"] = "";

                                            RfidTagRow["ORDERNO"] = ORDERNO;
                                            RfidTagRow["CUST_TRACK"] = CUST_TRACK;
                                            RfidTagRow["TOTAL_QTY"] = TOTAL_QTY;
                                            RfidTagRow["PACKAGING"] = PACKAGING;
                                            RfidTagRow["RowsNO"] = RowsNO;
                                            RfidTagRow["COUNTRY_OF_ORIGIN"] = COUNTRY_OF_ORIGIN;
                                            RfidTagRow["KRAFT_HANGTAG"] = KRAFT_HANGTAG;
                                            RfidTagRow["FCT_CODE"] = FCT_CODE;
                                            RfidTag.Rows.Add(RfidTagRow);
                                        }
                                    }
                                    // 第三种 FOC
                                    else if (barcodestr[index + 54].ToString().Trim().Split(' ')[0] == "PACKAGING" &&
                                              barcodestr[index + 43].ToString().Trim().Split(' ')[0] == "MAXIM")
                                    {
                                        string ORDERNO = barcodestr[index + 37].ToString().Trim();
                                        string PO = barcodestr[index + 44].ToString().Trim();
                                        string CUST_TRACK = barcodestr[index + 46].ToString().Trim();
                                        string TOTAL_QTY = barcodestr[index + 48].ToString().Trim().Replace(",", "");
                                        string PACKAGING = barcodestr[index + 54].ToString().Trim().Split(' ')[1];
                                        string RowStrHead = "";
                                        string[] RowStrHeadDetails = new string[14];
                                        for (int X = index + 59; X < styleIndexs[Z]; X++)
                                        {
                                            if(RowStrHead == "")  // 第一次进来的时候才取值  第2行才不会跑掉
                                            {
                                                RowStrHead = barcodestr[X - 2].ToString().Trim();  //
                                               RowStrHeadDetails = RowStrHead.Split('│');

                                            }


                                            // 第三种 FOC 里面的第 1 种
                                            if (RowStrHeadDetails.Length ==14 && RowStrHeadDetails[4].Trim() == "SEQUENCE")
                                            {
                                                string RowStr = barcodestr[X].ToString().Trim();  //  ++ 63 64 ... 一共有多少行
                                                string[] RowDetails = RowStr.Split('│');
                                                if (RowDetails.Length != 14)
                                                {
                                                    continue;
                                                }
                                                string RowsNO = RowDetails[1].ToString().Trim();
                                                string NIKEPO = RowDetails[7].ToString().Trim();
                                                string UPC = RowDetails[6].ToString().Trim();
                                                //UPC = UPC.Substring(UPC.Length - 12, 12);
                                                string FCT_CODE = RowDetails[8].ToString().Trim();
                                                string SEA_COD = RowDetails[9].ToString().Trim();
                                                string STYLE = RowDetails[10].ToString().Trim();
                                                string COLOR = RowDetails[11].ToString().Trim();
                                                string SIZE = RowDetails[12].ToString().Trim();
                                                string COUNTRY_OF_ORIGIN = "";
                                                string XCOLORBAR = RowDetails[5].ToString().Trim();
                                                //XCOLORBAR = XCOLORBAR.Substring(0, XCOLORBAR.Length - 12);
                                                string QUANTITY = RowDetails[2].ToString().Trim().Replace(",", "");
                                                string KRAFT_HANGTAG = "";

                                                DataRow RfidTagRow = this.RfidTag.NewRow();
                                                RfidTagRow["CustID"] = "NIKE";
                                                RfidTagRow["SKU"] = UPC;
                                                RfidTagRow["ColorName"] = XCOLORBAR;
                                                RfidTagRow["SizeName"] = SIZE;
                                                RfidTagRow["Style"] = STYLE;
                                                RfidTagRow["PONumber"] = NIKEPO;
                                                RfidTagRow["Qtys"] = QUANTITY;
                                                RfidTagRow["Seanson"] = SEA_COD;
                                                RfidTagRow["StyleColor"] = STYLE + "-" + COLOR;
                                                RfidTagRow["ColorCode"] = COLOR;
                                                RfidTagRow["Note"] = "";

                                                RfidTagRow["ORDERNO"] = ORDERNO;
                                                RfidTagRow["CUST_TRACK"] = CUST_TRACK;
                                                RfidTagRow["TOTAL_QTY"] = TOTAL_QTY;
                                                RfidTagRow["PACKAGING"] = PACKAGING;
                                                RfidTagRow["RowsNO"] = RowsNO;
                                                RfidTagRow["COUNTRY_OF_ORIGIN"] = COUNTRY_OF_ORIGIN;
                                                RfidTagRow["KRAFT_HANGTAG"] = KRAFT_HANGTAG;
                                                RfidTagRow["FCT_CODE"] = FCT_CODE;
                                                RfidTag.Rows.Add(RfidTagRow);
                                            }
                                            // 第三种 FOC 里面的第 2 种
                                            else if (RowStrHeadDetails.Length == 14 && RowStrHeadDetails[4].Trim() == "XCOLORBAR")
                                            {
                                                string RowStr = barcodestr[X].ToString().Trim();  //  ++ 63 64 ... 一共有多少行
                                                string[] RowDetails = RowStr.Split('│');
                                                if (RowDetails.Length != 14)
                                                {
                                                    continue;
                                                }
                                                string RowsNO = RowDetails[1].ToString().Trim();
                                                string NIKEPO = RowDetails[6].ToString().Trim();
                                                string UPC = RowDetails[5].ToString().Trim();
                                                //UPC = UPC.Substring(UPC.Length - 12, 12);
                                                string FCT_CODE = RowDetails[7].ToString().Trim();
                                                string SEA_COD = RowDetails[8].ToString().Trim();
                                                string STYLE = RowDetails[9].ToString().Trim();
                                                string COLOR = RowDetails[10].ToString().Trim();
                                                string SIZE = RowDetails[11].ToString().Trim();
                                                string COUNTRY_OF_ORIGIN = RowDetails[12].ToString().Trim();
                                                string XCOLORBAR = RowDetails[4].ToString().Trim();
                                                //XCOLORBAR = XCOLORBAR.Substring(0, XCOLORBAR.Length - 12);
                                                string QUANTITY = RowDetails[2].ToString().Trim().Replace(",", "");
                                                string KRAFT_HANGTAG = "";

                                                DataRow RfidTagRow = this.RfidTag.NewRow();
                                                RfidTagRow["CustID"] = "NIKE";
                                                RfidTagRow["SKU"] = UPC;
                                                RfidTagRow["ColorName"] = XCOLORBAR;
                                                RfidTagRow["SizeName"] = SIZE;
                                                RfidTagRow["Style"] = STYLE;
                                                RfidTagRow["PONumber"] = NIKEPO;
                                                RfidTagRow["Qtys"] = QUANTITY;
                                                RfidTagRow["Seanson"] = SEA_COD;
                                                RfidTagRow["StyleColor"] = STYLE + "-" + COLOR;
                                                RfidTagRow["ColorCode"] = COLOR;
                                                RfidTagRow["Note"] = "";

                                                RfidTagRow["ORDERNO"] = ORDERNO;
                                                RfidTagRow["CUST_TRACK"] = CUST_TRACK;
                                                RfidTagRow["TOTAL_QTY"] = TOTAL_QTY;
                                                RfidTagRow["PACKAGING"] = PACKAGING;
                                                RfidTagRow["RowsNO"] = RowsNO;
                                                RfidTagRow["COUNTRY_OF_ORIGIN"] = COUNTRY_OF_ORIGIN;
                                                RfidTagRow["KRAFT_HANGTAG"] = KRAFT_HANGTAG;
                                                RfidTagRow["FCT_CODE"] = FCT_CODE;
                                                RfidTag.Rows.Add(RfidTagRow);
                                            }

                                            else  //  第三种 FOC 里面的默认不知道什么格式 ，先上传再修改  要不会有漏掉
                                            {
                                                string RowStr = barcodestr[X].ToString().Trim();  //  ++ 63 64 ... 一共有多少行
                                                string[] RowDetails = RowStr.Split('│');
                                                if (RowDetails.Length != 14)
                                                {
                                                    continue;
                                                }
                                                string RowsNO = RowDetails[1].ToString().Trim();
                                                string NIKEPO = RowDetails[6].ToString().Trim();
                                                string UPC = RowDetails[5].ToString().Trim();
                                                //UPC = UPC.Substring(UPC.Length - 12, 12);
                                                string FCT_CODE = RowDetails[7].ToString().Trim();
                                                string SEA_COD = RowDetails[8].ToString().Trim();
                                                string STYLE = RowDetails[9].ToString().Trim();
                                                string COLOR = RowDetails[10].ToString().Trim();
                                                string SIZE = RowDetails[11].ToString().Trim();
                                                string COUNTRY_OF_ORIGIN = RowDetails[12].ToString().Trim();
                                                string XCOLORBAR = RowDetails[4].ToString().Trim();
                                                //XCOLORBAR = XCOLORBAR.Substring(0, XCOLORBAR.Length - 12);
                                                string QUANTITY = RowDetails[2].ToString().Trim().Replace(",", "");
                                                string KRAFT_HANGTAG = "";

                                                DataRow RfidTagRow = this.RfidTag.NewRow();
                                                RfidTagRow["CustID"] = "NIKE";
                                                RfidTagRow["SKU"] = UPC;
                                                RfidTagRow["ColorName"] = XCOLORBAR;
                                                RfidTagRow["SizeName"] = SIZE;
                                                RfidTagRow["Style"] = STYLE;
                                                RfidTagRow["PONumber"] = NIKEPO;
                                                RfidTagRow["Qtys"] = QUANTITY;
                                                RfidTagRow["Seanson"] = SEA_COD;
                                                RfidTagRow["StyleColor"] = STYLE + "-" + COLOR;
                                                RfidTagRow["ColorCode"] = COLOR;
                                                RfidTagRow["Note"] = "";

                                                RfidTagRow["ORDERNO"] = ORDERNO;
                                                RfidTagRow["CUST_TRACK"] = CUST_TRACK;
                                                RfidTagRow["TOTAL_QTY"] = TOTAL_QTY;
                                                RfidTagRow["PACKAGING"] = PACKAGING;
                                                RfidTagRow["RowsNO"] = RowsNO;
                                                RfidTagRow["COUNTRY_OF_ORIGIN"] = COUNTRY_OF_ORIGIN;
                                                RfidTagRow["KRAFT_HANGTAG"] = KRAFT_HANGTAG;
                                                RfidTagRow["FCT_CODE"] = FCT_CODE;
                                                RfidTag.Rows.Add(RfidTagRow);
                                            }
                                        }

                                    }
                                    // 第四种
                                    else if (barcodestr[index + 54].ToString().Trim().Split(' ')[0] == "PACKAGING" &&
                                             barcodestr[index + 44].ToString().Trim().Split(' ')[0] == "MAXIM")
                                    {
                                        string ORDERNO = barcodestr[index + 20].ToString().Trim();
                                        string PO = barcodestr[index + 45].ToString().Trim();
                                        string CUST_TRACK = barcodestr[index + 47].ToString().Trim();
                                        string TOTAL_QTY = barcodestr[index + 49].ToString().Trim().Replace(",", "");
                                        string PACKAGING = barcodestr[index + 54].ToString().Trim().Split(' ')[1];
                                        for (int X = index + 61; X < styleIndexs[Z]; X++)
                                        {
                                            string RowStr = barcodestr[X].ToString().Trim();  //  ++ 63 64 ... 一共有多少行
                                            string[] RowDetails = RowStr.Split(' ');
                                            if (RowDetails.Length < 11)
                                            {
                                                continue;
                                            }
                                            string RowsNO = RowDetails[8].ToString().Trim();
                                            string NIKEPO = RowDetails[0].ToString().Trim();
                                            string UPC = RowDetails[9].ToString().Trim();
                                            UPC = UPC.Substring(UPC.Length - 12, 12);
                                            string FCT_CODE = RowDetails[1].ToString().Trim();
                                            string SEA_COD = RowDetails[2].ToString().Trim();
                                            string STYLE = RowDetails[3].ToString().Trim();
                                            string COLOR = RowDetails[4].ToString().Trim();
                                            string SIZE = RowDetails[5].ToString().Trim();
                                            string COUNTRY_OF_ORIGIN = RowDetails[10].ToString().Trim();
                                            string XCOLORBAR = RowDetails[9].ToString().Trim();
                                            XCOLORBAR = XCOLORBAR.Substring(0, XCOLORBAR.Length - 12);
                                            string QUANTITY = RowDetails[7].ToString().Trim().Replace(",", "");
                                            string KRAFT_HANGTAG = RowDetails[6].ToString().Trim();

                                            DataRow RfidTagRow = this.RfidTag.NewRow();
                                            RfidTagRow["CustID"] = "NIKE";
                                            RfidTagRow["SKU"] = UPC;
                                            RfidTagRow["ColorName"] = XCOLORBAR;
                                            RfidTagRow["SizeName"] = SIZE;
                                            RfidTagRow["Style"] = STYLE;
                                            RfidTagRow["PONumber"] = NIKEPO;
                                            RfidTagRow["Qtys"] = QUANTITY;
                                            RfidTagRow["Seanson"] = SEA_COD;
                                            RfidTagRow["StyleColor"] = STYLE + "-" + COLOR;
                                            RfidTagRow["ColorCode"] = COLOR;
                                            RfidTagRow["Note"] = "";

                                            RfidTagRow["ORDERNO"] = ORDERNO;
                                            RfidTagRow["CUST_TRACK"] = CUST_TRACK;
                                            RfidTagRow["TOTAL_QTY"] = TOTAL_QTY;
                                            RfidTagRow["PACKAGING"] = PACKAGING;
                                            RfidTagRow["RowsNO"] = RowsNO;
                                            RfidTagRow["COUNTRY_OF_ORIGIN"] = COUNTRY_OF_ORIGIN;
                                            RfidTagRow["KRAFT_HANGTAG"] = KRAFT_HANGTAG;
                                            RfidTagRow["FCT_CODE"] = FCT_CODE;
                                            RfidTag.Rows.Add(RfidTagRow);
                                        }
                                    }

                                    else  // 最后 默认不知道什么格式 ，先上传再修改  要不会有漏掉
                                    {

                                        string ORDERNO = barcodestr[index + 20].ToString().Trim();
                                        string PO = barcodestr[index + 46].ToString().Trim();
                                        string CUST_TRACK = barcodestr[index + 48].ToString().Trim();
                                        string TOTAL_QTY = barcodestr[index + 50].ToString().Trim().Replace(",", "");
                                        string PACKAGING = barcodestr[index + 55].ToString().Trim().Split(' ')[1];
                                        for (int X = index + 62; X < styleIndexs[Z]; X++)
                                        {
                                            string RowStr = barcodestr[X].ToString().Trim();  //  ++ 63 64 ... 一共有多少行
                                            string[] RowDetails = RowStr.Split(' ');
                                            if (RowDetails.Length < 11)
                                            {
                                                continue;
                                            }
                                            string RowsNO = RowDetails[8].ToString().Trim();
                                            string NIKEPO = RowDetails[0].ToString().Trim();
                                            string UPC = RowDetails[9].ToString().Trim();
                                            UPC = UPC.Substring(UPC.Length - 12, 12);
                                            string FCT_CODE = RowDetails[1].ToString().Trim();
                                            string SEA_COD = RowDetails[2].ToString().Trim();
                                            string STYLE = RowDetails[3].ToString().Trim();
                                            string COLOR = RowDetails[4].ToString().Trim();
                                            string SIZE = RowDetails[5].ToString().Trim();
                                            string COUNTRY_OF_ORIGIN = RowDetails[10].ToString().Trim();
                                            string XCOLORBAR = RowDetails[9].ToString().Trim();
                                            XCOLORBAR = XCOLORBAR.Substring(0, XCOLORBAR.Length - 12);
                                            string QUANTITY = RowDetails[7].ToString().Trim().Replace(",", "");
                                            string KRAFT_HANGTAG = RowDetails[6].ToString().Trim();

                                            DataRow RfidTagRow = this.RfidTag.NewRow();
                                            RfidTagRow["CustID"] = "NIKE";
                                            RfidTagRow["SKU"] = UPC;
                                            RfidTagRow["ColorName"] = XCOLORBAR;
                                            RfidTagRow["SizeName"] = SIZE;
                                            RfidTagRow["Style"] = STYLE;
                                            RfidTagRow["PONumber"] = NIKEPO;
                                            RfidTagRow["Qtys"] = QUANTITY;
                                            RfidTagRow["Seanson"] = SEA_COD;
                                            RfidTagRow["StyleColor"] = STYLE + "-" + COLOR;
                                            RfidTagRow["ColorCode"] = COLOR;
                                            RfidTagRow["Note"] = "";

                                            RfidTagRow["ORDERNO"] = ORDERNO;
                                            RfidTagRow["CUST_TRACK"] = CUST_TRACK;
                                            RfidTagRow["TOTAL_QTY"] = TOTAL_QTY;
                                            RfidTagRow["PACKAGING"] = PACKAGING;
                                            RfidTagRow["RowsNO"] = RowsNO;
                                            RfidTagRow["COUNTRY_OF_ORIGIN"] = COUNTRY_OF_ORIGIN;
                                            RfidTagRow["KRAFT_HANGTAG"] = KRAFT_HANGTAG;
                                            RfidTagRow["FCT_CODE"] = FCT_CODE;
                                            RfidTag.Rows.Add(RfidTagRow);
                                        }
                                    }




                                }
                            }

                        }
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
        }
    }
}
