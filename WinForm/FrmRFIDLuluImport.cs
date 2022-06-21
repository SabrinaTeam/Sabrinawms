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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmRFIDLuluImport : Form
    {
        private static FrmRFIDLuluImport frm;
        public int hiedcolumnindex = -1; //是否选中外面
        private int headno = 21; // 行头
        private List<string> files = new List<string>(); // 文件夹下所有文件列表
        private int resultInSetRow = 0;
        public string fileNameStr = "";
        public string sheeTnameStr = "";
        public int headNoStr = -1;
        public string srcFolderPath = @"\\192.168.4.105\Users\17.SAA_共用區\RFID_LULU\uploaded\";
        public string srcPath = @"\\192.168.4.105\Users\17.SAA_共用區\RFID_LULU\";
        private bar barstr = new bar();
        private FrmRFIDLuluImportManager LuluManager = new FrmRFIDLuluImportManager();
        private DataTable RfidTag = new DataTable();
        public AccomplishTask TaskCallBack;
        public UpdateUI UpdateUIDelegate;
        public delegate void AccomplishTask();
        public delegate void UpdateUI(bar barstr);
        private delegate void AsynUpdateUI(bar barstr);
        public FrmRFIDLuluImport()
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


        public static FrmRFIDLuluImport GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmRFIDLuluImport();
            }
            return frm;
        }

        private void FrmRFIDNikeImport_Resize(object sender, EventArgs e)
        {
            this.gbLoad.Width = this.Width - 20;
            this.gbLoad.Left = (this.Width - this.gbLoad.Width - 20) / 2;
            this.bgDirectoryFiles.Left = this.gbLoad.Left;
            this.bgDirectoryFiles.Width = this.gbLoad.Width;
            this.bgDirectoryFiles.Height = this.Height - this.gbLoad.Height - 50;
            this.btnSave.Left = this.gbLoad.Width - this.btnSave.Width - 5;
            this.pgBar.Width = this.gbLoad.Width - 10;
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
            sdfExport.Filter =  "B01R-RFID文件|*.B01R | " +
                                "DB01R-RFID文件|*.DB01R | " +
                                "DW01R-RFID文件|*.DW01R | " +
                                "DWDER-RFID文件|*.DWDER | " +
                                "W01-RFID文件|*.W01 | " +
                                "W01R-RFID文件|*.W01R | " +
                                "WDE-RFID文件|*.WDE | " +
                                "WDER-RFID文件|*.WDER | " +
                                "DWDER-RFID文件|*.DWDER " ;
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
                string[] RFIDfiles = Directory.GetFiles(this.srcPath, "*.*", SearchOption.AllDirectories);
                foreach (string file in RFIDfiles)
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
            resultInSetRow = resultInSetRow + LuluManager.uploadToMysql(this.RfidTag);

            string foldName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Dns.GetHostName().ToUpper() + "_LULU(" + this.files.Count.ToString() + ")";
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
                string drcPath = srcFolderPath + foldName + @"\" + filestr + ".bak";
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
            string Ponumber = "";
            // 加载文件
            for (int i = 0; i < this.files.Count; i++)
            {
                barstr.str = "正在加载:" + this.files[i] + "...";
                barstr.step = i;
                barstr.maxstep = this.files.Count;
                UpdateUIDelegate(barstr);
                FileInfo myFileinfo = new FileInfo(this.files[i]);
                string file  = this.files[i];
                string fileName = file;
                var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var sr = new StreamReader(fs, Encoding.UTF8);
                string line = String.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    string f = myFileinfo.Name;
                    string fileSuffix = file.Split('.')[file.Split('.').Length - 1].ToUpper();
                    if (fileSuffix == "P01") //poly文件
                    {

                        DataRow RfidTagRow = this.RfidTag.NewRow();
                        string[] lines = line.Split('|');
                        if (lines.Length > 94)
                        {
                            RfidTagRow["CustID"] = "LULU";
                            RfidTagRow["SKU"] = lines[15];
                            RfidTagRow["ColorName"] = lines[2];
                            RfidTagRow["SizeName"] = lines[3];
                            RfidTagRow["Style"] = lines[5];

                            Ponumber = f.Split('-')[0];
                            if (Ponumber.Length <= 0)
                            {
                                Ponumber = "0";
                            }
                            RfidTagRow["PONumber"] = Ponumber;
                            RfidTagRow["Qtys"] = lines[14];
                            RfidTagRow["Seanson"] = "";
                            RfidTagRow["StyleColor"] ="";
                            RfidTagRow["ColorCode"] = "";




                            RfidTagRow["Note"] = "";

                            RfidTagRow["ORDERNO"] = "";
                            RfidTagRow["CUST_TRACK"] = "";
                            RfidTagRow["TOTAL_QTY"] = 0;
                            RfidTagRow["PACKAGING"] = "";
                            RfidTagRow["RowsNO"] = 0;
                            RfidTagRow["COUNTRY_OF_ORIGIN"] = "";
                            RfidTagRow["KRAFT_HANGTAG"] = "";
                            RfidTagRow["FCT_CODE"] = "";




                            RfidTag.Rows.Add(RfidTagRow);
                        }
                    }
                    else
                    {

                        DataRow RfidTagRow = this.RfidTag.NewRow();
                        string[] lines = line.Split('|');
                        if (lines.Length > 94)
                        {
                            RfidTagRow["CustID"] = "LULU";
                            RfidTagRow["SKU"] = lines[0];
                            RfidTagRow["ColorName"] = lines[2];
                            RfidTagRow["SizeName"] = lines[3];
                            RfidTagRow["Style"] = lines[5];
                            Ponumber = lines[6];
                            if (Ponumber.Length <= 0)
                            {
                                Ponumber = "0";
                            }
                            RfidTagRow["PONumber"] = Ponumber;
                            RfidTagRow["Qtys"] = lines[9];
                            RfidTagRow["Seanson"] = lines[11];
                            RfidTagRow["StyleColor"] = lines[92];
                            RfidTagRow["ColorCode"] = lines[93];

                            RfidTagRow["Note"] = "";

                            RfidTagRow["ORDERNO"] = "";
                            RfidTagRow["CUST_TRACK"] = "";
                            RfidTagRow["TOTAL_QTY"] = 0;
                            RfidTagRow["PACKAGING"] = "";
                            RfidTagRow["RowsNO"] = 0;
                            RfidTagRow["COUNTRY_OF_ORIGIN"] = "";
                            RfidTagRow["KRAFT_HANGTAG"] = "";
                            RfidTagRow["FCT_CODE"] = "";
                            RfidTag.Rows.Add(RfidTagRow);
                        }
                    }


                }
            }
        }

    }
}
