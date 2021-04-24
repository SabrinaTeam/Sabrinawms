using BLL;
using DAL;
using Microsoft.Reporting.WinForms;
using MODEL;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmCompletedToMes : Form
    {
        private static FrmCompletedToMes frm;
        private CompletedToMesService cms = new CompletedToMesService();
        //1、实例化打印文档
        private PrintDocument pdDocument = new PrintDocument();
        private PrintPreviewDialog m_printPreview = new PrintPreviewDialog();//打印预览UI
        private int value = 0;//
        private int pageCount = 0;//页数
        private PrintDocument m_printDoc = new PrintDocument();
        private float m_pageWidth = 210F;//纸张宽度 mm单位
        private float m_pageHeight = 297F;//纸张高度 mm单位
        private int linesPrinted;//
        private string barcode; //条码
        private Image barcodeimg; //工票条码

        public DataGridView selectDgv = null;



        //   public int intervalTime = 0;
        private MessageQueue queue;

        public static DataTable user = new DataTable();

        public List<mesEmployee> emps;
        public List<mesOrg> Orgs;
        mesEmployeeManager empm = new mesEmployeeManager();
        CompletedToMesManager cmm = new CompletedToMesManager();
        mesOrgManager orgm = new mesOrgManager();
        public string SAAUlr = "http://192.168.4.251:5000/api/process";
        public string SAATest = "http://192.168.4.251:5001/api/reportPlace";
        public string TOPUlr = "http://192.168.7.240:5000/api/process";
        public string TOPTest = "http://192.168.7.240:5001/api/reportPlace";
       
        bool isRead = false;
        public string orgName = "";
        public string processID = "";
        public string  org = "";


        DataTable invoiceDataSource = new DataTable();
        DataTable ScanDataDataSource = new DataTable();

        ConnectionFactory factory = new ConnectionFactory();
        IConnection connection;
        IModel channel;
        public string HostName = "172.16.1.219";
        public string UserName = "sabrina";
        public string Password = "sabrina";
        public bool autoRecovery = true;


        public FrmCompletedToMes()
        {
            InitializeComponent();          
            factory.HostName = this.HostName;
            factory.UserName = this.UserName;
            factory.Password = this.Password;
            factory.AutomaticRecoveryEnabled = this.autoRecovery;
            factory.RequestedHeartbeat = 10;
            this.dgvInvoice.DoubleBufferedDataGirdView(true);
            this.dgvWorkTagScans.DoubleBufferedDataGirdView(true);

            //2、订阅事件
            //订阅 PinrtPage 事件,绘制内容
            pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage);
            //订阅 BeginPrint 事件,得到打印内容
            pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
            //订阅 EndPrint 事件,释放资源
            pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);

        }
        public static FrmCompletedToMes GetSingleton(DataTable dt)
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmCompletedToMes();
            }
            user = dt;
            if (user.Rows.Count > 0)
            {
                frm.Text = frm.Text + "_欢迎您:" + dt.Rows[0]["UserName"].ToString();
            }
            return frm;
        }

       

        private async void cbProcessName_ClickAsync(object sender, EventArgs e)
        {
            if (this.cbOrg.Items.Count <= 0)
            {
                return;
            }
            string Org = this.cbOrg.SelectedItem.ToString();
            if (Org.IndexOf("SAA") != -1)
            {
                this.emps = await empm.GetAllProducts(this.SAAUlr);
            }
            else
            {
                this.emps = await empm.GetAllProducts(this.TOPUlr);
            }

            if (emps.Count <= 0)
            {
                MessageBox.Show("获取制程失败,请查询是否正常连线服务器");
                return;
            }
            this.cbProcessName.Items.Clear();
            for (int i = 0; i < emps.Count; i++)
            {
                this.cbProcessName.Items.Add(emps[i].Name);
            }
        }

        private void cbProcessName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int processNameIndex = this.cbProcessName.SelectedIndex;
            if (processNameIndex >= 0)
            {
                this.labProcessID.Text = this.emps[processNameIndex].ID;
            }
        }

        private async void cbOrg_ClickAsync(object sender, EventArgs e)
        {
            if (this.org == "SAA")
            {
                this.Orgs = await orgm.getOrgs(SAATest);
            }else if(this.org == "TOP")
            {
                this.Orgs = await orgm.getOrgs(TOPTest);
            }
            else
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
        }

        private void cbOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PlaceNameIndex = this.cbOrg.SelectedIndex;
            if (PlaceNameIndex >= 0)
            {
                this.labOrgID.Text = this.Orgs[PlaceNameIndex].ReportPlaceId.ToString();
            }
        } 
        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }

        public void receivedkibaQueue( )
        {
            string Org = this.orgName.Substring(0, 3);           
            string queueName = Org +"-"+ this.processID;
            string exchangeName = Org;
            this.connection = factory.CreateConnection();

            if (this.connection.IsOpen)
            {
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(queueName, exchangeName, ExchangeType.Direct, null);
                channel.BasicQos(0, 1, false);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    string orders = ByteArrayToObject(body).ToString();                  
                 
                    addDgvInvoice(orders);
                    channel.BasicAck(ea.DeliveryTag, true);
                };
                channel.BasicConsume(queue: queueName, noAck: false, consumer: consumer);
            }
        }      

        private void addDgvInvoice(string Mid)
        {             
            DataTable invoiceData = cmm.getTagInvoiceById(Mid);
            if (invoiceData.Rows.Count <= 0)
            {
                return;
            }
            if(this.invoiceDataSource.Rows.Count <= 0)
            {
                this.invoiceDataSource = invoiceData;
            }
            else
            {
                foreach (DataRow row in invoiceData.Rows)
                {
                     this.invoiceDataSource.ImportRow(row); 
                }
            }
            SetDgvInvoice(invoiceDataSource);
            if (cbAutoPrint.Checked)
            {
                string invoice = invoiceDataSource.Rows[this.invoiceDataSource.Rows.Count - 1]["tagInvoice"].ToString();
                getMesworktagscansByinvoice(invoice);
            }
           
        }

        private void getMesworktagscansByinvoice(string tagInvoice)
        {
            if (tagInvoice.Length <= 0)
            {
                return;
            }

            DataTable ScanData = cmm.getMesworktagscansByinvoice(tagInvoice);
            if (ScanData.Rows.Count <= 0)
            {
                return;
            }
            this.ScanDataDataSource = ScanData;
            SetScanData(this.ScanDataDataSource);
            //  print(this.ScanDataDataSource,0);
            // this.dgvWorkTagScans.DataSource = this.ScanDataDataSource;

            if (this.ScanDataDataSource.Rows.Count <= 0)
            {
                return;
            }

            if (!this.cbAutoPrint.Checked)
            {
                return;
            }
            /*
            //调用打印程式
            
           
            */
            //声明一个LocalReport对象并加载一个报表文件
            LocalReport report = new LocalReport();

            // 设置需要打印的报表的文件名称。
            report.ReportPath = Application.StartupPath + "\\ReportCompletedToMes.rdlc";
            //传递报表参数
            DataTable ScanSourceData = cms.getMesworktagscansByinvoiceGroup(tagInvoice);
            DataTable ScanSourceDataDetail = cms.getMesworktagscansByinvoiceDetail(tagInvoice);
            //this.reportViewer1.RefreshReport();

            string invoicetag = this.ScanDataDataSource.Rows[0]["tagInvoice"].ToString().Trim().ToUpper();
            Bitmap QRCode = QRCodeHelper.GenQRCode(invoicetag, 120, 120);
            //自定义参数
            List<ReportParameter> list = new List<ReportParameter>();
            string styles = "";
            string colors = "";
            string sizes = "";
            string qtys = "";
            string location = "";
            location = ScanSourceData.Rows[0]["taglocation"].ToString();
            if (location == "")
            {
                location = "NA";
            }

            // String.Format("{0,-10}", str) String.format("%-12s",str);
         //   for (int i = 0; i < ScanSourceData.Rows.Count; i++)
           // {
                styles = ScanSourceData.Rows[0]["tagStyle"].ToString();
                colors = ScanSourceData.Rows[0]["tagColor"].ToString();
                sizes =  ScanSourceData.Rows[0]["tagSize"].ToString();
                qtys =   ScanSourceData.Rows[0]["tagQty"].ToString();
           // }
            string[] ss =  sizes.Split(',');
            string[] qs = qtys.ToString().Split(',');
            sizes = "";
            qtys = "";
            foreach (var s in ss)
            {
                sizes = sizes + " , " + string.Format("{0,-7:G}", s.Trim());
            }
            foreach (var q in qs)
            {
                qtys = qtys + " , " + string.Format("{0,-6:G}", q.Trim());
            }
            // sizes = sizes + " | " + string.Format("%-10s", ScanSourceData.Rows[i]["tagSize"].ToString());
            // qtys = qtys + Convert.ToInt32(string.Format("%-10s", (ScanSourceData.Rows[i]["tagQty"].ToString())));
            sizes = sizes.Substring(2, sizes.Length - 2);
            qtys = qtys.Substring(2, qtys.Length - 2);
            styles = styles.Substring(1, styles.Length - 1);
            colors = colors.Substring(1, colors.Length - 1);
            //sizes = sizes.Substring(1, sizes.Length - 1);
            ReportParameter invoice = new ReportParameter("tagInvoice", ScanSourceData.Rows[0]["tagInvoice"].ToString());
            ReportParameter style = new ReportParameter("tagStyle", styles);
            ReportParameter color = new ReportParameter("tagColor", colors);
            ReportParameter size = new ReportParameter("tagSize", sizes);
            ReportParameter qty = new ReportParameter("tagQty", qtys.ToString());
            ReportParameter dept = new ReportParameter("DeptName", ScanSourceData.Rows[0]["DeptName"].ToString());
            ReportParameter org = new ReportParameter("tagOrg", ScanSourceData.Rows[0]["tagOrg"].ToString());
            ReportParameter taglocation = new ReportParameter("taglocation", location);
            ReportParameter part = new ReportParameter("part", ScanSourceData.Rows[0]["part"].ToString());


            byte[] imgBytes = bmpToBytes(QRCode);
            string strB64 = Convert.ToBase64String(imgBytes);
            ReportParameter rpTest = new ReportParameter("RPTest", strB64);//（rpTest为定义的报表参数变量，RPTest为在【报表】->【报表参数】中添加的参数。
            list.Add(rpTest);
            list.Add(part);
            list.Add(invoice);
            list.Add(style);
            list.Add(color);
            list.Add(size);
            list.Add(qty);
            list.Add(dept);
            list.Add(org);
            list.Add(taglocation);
            //创建要打印的数据源 

            ReportDataSource source = new ReportDataSource("DataSet1", ScanSourceDataDetail); // 指定数据源
            report.DataSources.Clear();
            report.DataSources.Add(source);
            report.SetParameters(list); //参数设置 
            //刷新报表中的需要呈现的数据 
             styles = "";
             colors = "";
             sizes = "";
             qtys = "";
            report.Refresh(); 
            ReportClass.Print(report, ScanSourceDataDetail); 
        }

        public byte[] bmpToBytes(Bitmap bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Bmp);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }

        private delegate void SetDgvInvoiceCallback(DataTable invoiceData);
        //在给textBox1.text赋值的地方调用以下方法即可
        private void SetDgvInvoice(DataTable invoiceData)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.dgvInvoice.InvokeRequired)
            {
                SetDgvInvoiceCallback d = new SetDgvInvoiceCallback(SetDgvInvoice);
                this.Invoke(d, new object[] { invoiceData });
            }
            else
            {
                this.dgvInvoice.DataSource = invoiceData;
                // 变动一下宽度  要不不能点击表格  不知原因
                this.splitContainer1.SplitterDistance = this.splitContainer1.SplitterDistance - 1;
                this.splitContainer1.SplitterDistance = this.splitContainer1.SplitterDistance + 1;
                this.splitContainer1.Refresh();
                if(this.dgvInvoice.Rows.Count <= 0){
                    return;
                }
                this.dgvInvoice.Rows[this.dgvInvoice.Rows.Count - 1].Selected = true;                
            }           
        }

        private delegate void SetScanDataCallback(DataTable ScanData);
        //在给textBox1.text赋值的地方调用以下方法即可
        private void SetScanData(DataTable ScanData)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.dgvWorkTagScans.InvokeRequired)
            {
                SetScanDataCallback d = new SetScanDataCallback(SetScanData);
                this.Invoke(d, new object[] { ScanData });
            }
            else
            {
                this.dgvWorkTagScans.DataSource = ScanData;
                // 变动一下宽度  要不不能点击表格  不知原因
                this.splitContainer1.SplitterDistance = this.splitContainer1.SplitterDistance - 1;
                this.splitContainer1.SplitterDistance = this.splitContainer1.SplitterDistance + 1;
                this.splitContainer1.Refresh();
                if (this.dgvWorkTagScans.Rows.Count <= 0)
                {
                    return;
                }
                this.dgvWorkTagScans.MultiSelect = false;
                this.dgvWorkTagScans.Rows[this.dgvWorkTagScans.Rows.Count - 1].Selected = true;
                this.dgvWorkTagScans.ReadOnly = true;
                this.dgvWorkTagScans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.dgvWorkTagScans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.dgvWorkTagScans.AllowUserToAddRows = false;

                /*
                this.dgvOutMWH.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                this.dgvOutMWH.Columns["id"].HeaderText = "ID";
                this.dgvOutMWH.Columns["TagNumber"].HeaderText = "条码号";
                this.dgvOutMWH.Columns["max_time"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
                this.dgvOutMWH.Columns["max_time"].HeaderText = "最后扫描时间";
                this.dgvOutMWH.Columns["Location"].HeaderText = "储位";
                */
            }
        }

        private void butStartReceiv_Click(object sender, EventArgs e)
        {
            if (this.cbOrg.Items.Count <= 0)
            {
                return;
            }
            this.orgName = this.cbOrg.SelectedItem.ToString();
            this.processID =  this.labProcessID.Text;
            if (this.processID.Length <= 0)
            {
                return;
            }
            this.butStartReceiv.Enabled = false;
            this.butStopReceiv.Enabled = true;
            ThreadStart childref = new ThreadStart(receivedkibaQueue);           
            Thread childThread = new Thread(childref);
            childThread.IsBackground = true;
            childThread.Start();
        }

        private void butStopReceiv_Click(object sender, EventArgs e)
        {
            this.connection.Close();
            this.butStartReceiv.Enabled = true;
            this.butStopReceiv.Enabled = false;
        }

        private void dgvInvoice_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvWorkTagScans_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvInvoice_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            if (selectDgv  != null)
            {
                Clipboard.SetDataObject(selectDgv.CurrentCell.Value.ToString());
            }
           
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            if (selectDgv != null)
            {
                Clipboard.SetDataObject(selectDgv.GetClipboardContent());
            }
           
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
           if(this.selectDgv != null)
            {
                ImproExcel(this.selectDgv);
            }
           
        }
        public void ImproExcel( DataGridView selectDgv)
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
            tabl = GetDgvToTable(this.selectDgv);
            if(selectDgv == this.dgvInvoice)
            {
                tableName = "dgvInvoiceTable";
            }
            else
            {
                tableName = "dgvOutgoingTable";
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
                    return;
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

        private void FrmCompletedToMes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.connection !=null &&  this.connection.IsOpen)
            {
                this.connection.Close();
            }
         
            this.butStartReceiv.Enabled = true;
            this.butStopReceiv.Enabled = false;

           
        }
        public int hiedcolumnindex = -1; //是否选中外面
        private void dgvWorkTagScans_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvWorkTagScans.Rows[e.RowIndex].Selected == false)
                    {
                        dgvWorkTagScans.ClearSelection();
                        dgvWorkTagScans.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvWorkTagScans.SelectedRows.Count == 1)
                    {
                        dgvWorkTagScans.CurrentCell = dgvWorkTagScans.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    this.selectDgv = this.dgvWorkTagScans;
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

        private void FrmCompletedToMes_Resize(object sender, EventArgs e)
        {
            gbDetail.Width = this.Width - 20;
            gbDetail.Height = this.Height - 110;
            splitContainer1.Width = this.gbDetail.Width - 10;
            splitContainer1.Height = this.gbDetail.Height - 15;

        }

        private void butPrint_Click(object sender, EventArgs e)
        { 
            if (this.ScanDataDataSource.Rows.Count <= 0)
            {
                return;
            } 
            string invoice = this.ScanDataDataSource.Rows[0]["tagInvoice"].ToString().Trim().ToUpper();
            Bitmap QRCode = QRCodeHelper.GenQRCode(invoice, 120, 120); 
            FrmCompletedToMesPrint frm = FrmCompletedToMesPrint.GetSingleton(invoice, QRCode); 
            frm.Show();
            frm.Activate(); 

        }

       


        /// <summary>
        /// 打印程式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="aoutPrint"> 0 自动打印   1 手动打印</param>
        private void print(DataTable dt,int aoutPrint)
        {            
            if (aoutPrint == 1)
            {
                
                try
                {

                    PrintDialog printDialog1 = new PrintDialog();
                    //将PrintDialog.UseEXDialog属性设置为True，才可显示出打印对话框
                    printDialog1.UseEXDialog = true;
                    //将printDocument1对象赋值给打印对话框的Document属性
                    printDialog1.Document = pdDocument;
                    //打开打印对话框
                    DialogResult result = printDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        //pdDocument.Print();//开始打印 
                        //調用打印
                        m_printPreview.Document = pdDocument;
                        try
                        {
                            m_printPreview.ShowDialog();
                        }
                        catch (InvalidOperationException ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        catch (Win32Exception wx)
                        {
                            return;
                            // MessageBox.Show("已取消打印作业");
                        }
                    }
                }
                catch (InvalidPrinterException ex)
                {
                    MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
                finally
                {
                    pdDocument.Dispose();
                }
            }
            else if(aoutPrint == 0)
            {

                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }
                try
                {
                    PrintDialog printDialog1 = new PrintDialog();
                    printDialog1.UseEXDialog = false;
                    printDialog1.Document = pdDocument;                     
                    m_printPreview.Document = pdDocument;
                }
                catch (InvalidPrinterException ex)
                {
                    MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
                finally
                {
                    pdDocument.Dispose();
                }

            }


        }

        /// <summary>
        /// 3、得到打印內容
        /// 每個打印任務衹調用OnBeginPrint()一次。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pdDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            //显示生成条码进度条
            msg.Visible = true;
            progressBar1.Visible = true;
            msg.Text = "正在加载送货清单内容，请稍等...";
          //  this.barcode = "F20040E2728B2B9F-0820210306-0054";
          //  Bitmap QRCode = QRCodeHelper.GenQRCode("F20040E2728B2B9F-0820210306-0054", 120, 120);

            this.barcode = this.ScanDataDataSource.Rows[0]["tagInvoice"].ToString().Trim().ToUpper();
            Bitmap QRCode = QRCodeHelper.GenQRCode(this.barcode, 120, 120);

            pictureBox1.Image = QRCode;
            this.barcodeimg = pictureBox1.Image;
            //自定义纸张大小
            pdDocument.DefaultPageSettings.PaperSize = new PaperSize("newPage_A4"
               , (int)(m_pageWidth / 25.4 * 100)
               , (int)(m_pageHeight / 25.4 * 100));
            //自定义图片内容整体上间距/左间距
            pdDocument.OriginAtMargins = true;
            pdDocument.DefaultPageSettings.Margins.Top = (int)(20 / 25.4 * 100);//顶边距离
            pdDocument.DefaultPageSettings.Margins.Left = (int)(20 / 25.4 * 100);  //左边距离
            msg.Text = "条码生成完成";
            msg.Visible = false;
            progressBar1.Visible = false;
            Application.DoEvents();
        }

        /// <summary>
        /// 4、繪制多個打印頁面
        /// printDocument的PrintPage事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            /*
             * 得到TextBox中每行的字符串數組
             * \n換行
             * \r回車
             */
            if (this.ScanDataDataSource == null || this.ScanDataDataSource.Rows.Count <= 0)
            {
                pdDocument.Dispose();
                return;
            }
            msg.Visible = true;
            progressBar1.Visible = true;
            msg.Text = "正在生成条码，请稍等...";

            // this.ScanDataDataSource.Rows[0]["tagInvoice"].ToString().Trim().ToUpper();
            barcode = this.ScanDataDataSource.Rows[0]["tagInvoice"].ToString().Trim().ToUpper();
            string org = this.ScanDataDataSource.Rows[0]["tagorg"].ToString().Trim().ToUpper();
            string line = this.ScanDataDataSource.Rows[0]["tagLine"].ToString().Trim().ToUpper();
            string scanDeptID = this.ScanDataDataSource.Rows[0]["tagScanDeptID"].ToString().Trim().ToUpper();
            string scanDateTime = this.ScanDataDataSource.Rows[0]["tagScanDateTime"].ToString().Trim().ToUpper();
            // string scanDateTime = this.ScanDataDataSource.Rows[0]["tagScanDateTime"].ToString().Trim().ToUpper();

            int left = Convert.ToInt32(55 / 25.4 * 100);//条码左距离  5 2CM左右
            int top = Convert.ToInt32(0 / 25.4 * 100);//条码顶距离
            e.Graphics.DrawString("工票对点清单", new Font("黑体", 22), Brushes.Black, left, top);//号码

            // 二维码
            left = Convert.ToInt32(2 / 25.4 * 100);//条码左距离  5 2CM左右 
            top = Convert.ToInt32(7 / 25.4 * 100);//条码顶距离
            e.Graphics.DrawImage(barcodeimg, left, top);//左，顶

            // 厂区 tagorg
            left = Convert.ToInt32(40 / 25.4 * 100);//左距离
            top = Convert.ToInt32(12 / 25.4 * 100);//顶距离
            e.Graphics.DrawString("厂区:" + org, new Font("黑体", 12), Brushes.Black, left, top);//号码

            // 线别 tagLine
            left = Convert.ToInt32(90 / 25.4 * 100);//左距离
            top = Convert.ToInt32(12 / 25.4 * 100);//顶距离
            e.Graphics.DrawString("线别:" + line, new Font("黑体", 12), Brushes.Black, left, top);//号码

            // 送货单位 tagScanDeptID
            left = Convert.ToInt32(140 / 25.4 * 100);//左距离
            top = Convert.ToInt32(12 / 25.4 * 100);//顶距离
            e.Graphics.DrawString("送货单位:" + scanDeptID, new Font("黑体", 12), Brushes.Black, left, top);//号码

            // 送票单号
            left = Convert.ToInt32(40 / 25.4 * 100);//左距离
            top = Convert.ToInt32(28 / 25.4 * 100);//顶距离
            e.Graphics.DrawString("送票单号:"+ barcode, new Font("黑体", 15), Brushes.Black, left, top);//号码
            
            // 扫描时间 tagScanDateTime
            left = Convert.ToInt32(40 / 25.4 * 100);//左距离
            top = Convert.ToInt32(20 / 25.4 * 100);//顶距离
            e.Graphics.DrawString("扫描时间:" + scanDateTime, new Font("黑体", 12), Brushes.Black, left, top);//号码

            /*
            // 款式 tagStyle
            left = Convert.ToInt32(42 / 25.4 * 100);//左距离
            top = Convert.ToInt32(7 / 25.4 * 100);//顶距离
            e.Graphics.DrawString("款式:" + barcode, new Font("黑体", 10), Brushes.Black, left, top);//号码

            // 色组 tagColor
            left = Convert.ToInt32(42 / 25.4 * 100);//左距离
            top = Convert.ToInt32(14 / 25.4 * 100);//顶距离
            e.Graphics.DrawString("色组:" + barcode, new Font("黑体", 10), Brushes.Black, left, top);//号码

            // 尺码 tagSize
            left = Convert.ToInt32(42 / 25.4 * 100);//左距离
            top = Convert.ToInt32(21 / 25.4 * 100);//顶距离
            e.Graphics.DrawString("尺码:" + barcode, new Font("黑体", 10), Brushes.Black, left, top);//号码
                                                                                                 
            // 数量 tagSize
            left = Convert.ToInt32(42 / 25.4 * 100);//左距离
            top = Convert.ToInt32(7 / 25.4 * 100);//顶距离
            e.Graphics.DrawString("数量:" + barcode, new Font("黑体", 10), Brushes.Black, left, top);//号码

           
            List<string> styles =new List<string>();
            for (int i = 0; i < this.ScanDataDataSource.Rows.Count; i++)
            {

                string style = this.ScanDataDataSource.Rows[i]["tagStyle"].ToString();
                if (!styleIsExist(styles, style))
                {
                    styles.Add(style);
                }

            }

            
            this.ScanDataDataSource.
            */

            //linesPrinted 现在要画的第几个条码
            while (linesPrinted < barcode.Length)
            {
                //其它条码格式
                    //繪製要打印的頁面
                    //创建文本信息
                    
                    // y += 55;
                    linesPrinted++;
                    value = Convert.ToInt32(Convert.ToDouble(linesPrinted) / barcode.Length * 100);
                    progressBar1.Value = value;
                    msg.Text = "生成条码" + value + " %";
                    Application.DoEvents();
                    // msg.Text = "";
                    //progressBar1.Value = 0;
                    // pictureBox1.Image = null;                   
                }
                //判斷超過一頁時，允許進行多頁打印
                if (barcode.Length > linesPrinted)
                {
                    //允許多頁打印
                    e.HasMorePages = true;
                    /*
                     * PrintPageEventArgs類的HaeMorePages屬性為True時，通知控件器，必須再次調用OnPrintPage()方法，打印一個頁面。
                     * PrintLoopI()有一個用於每個要打印的頁面的序例。如果HasMorePages是False，PrintLoop()就會停止。
                     */
                    return;
                }
             
            
            msg.Text = "条码生成完成";
            msg.Visible = false;
            progressBar1.Visible = false;
            linesPrinted = 0;
            //繪制完成後，關閉多頁打印功能
            e.HasMorePages = false;
        }

        /// <summary>
        ///5、EndPrint事件,釋放資源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pdDocument_EndPrint(object sender, PrintEventArgs e)
        {
            //變量Lines占用和引用的字符串數組，現在釋放
          //  custompo = null;
       //     cutno = null;
        //    boxno = null;
         //   barcode = null;
         //   boxnostr = null;
        }

        private bool styleIsExist(List<string> lists, string tag)
        {
            if (lists.Count <= 0)
            {
                return false;
            }
            bool result = true;
           for(int i = 0; i < lists.Count; i++)
            {
                if(lists[i] == tag)
                {
                    result = true;
                    break;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void cbAutoPrint_Click(object sender, EventArgs e)
        {
            this.cbAutoPrint.Checked = !this.cbAutoPrint.Checked;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
           if (this.cbOrg.Items.Count <= 0)
            {
                MessageBox.Show("请先选择厂区与制程");
                return;
            }
            string starDataTime = this.dtpStarScanDate.Value.ToString("yyyy-MM-dd");
            string stopDataTime = this.dtpStopScanDate.Value.AddDays(1).ToString("yyyy-MM-dd");
            string receiptNumber = this.txtReceiptNumber.Text.Trim().ToUpper();
            string orgstr = this.cbOrg.SelectedItem.ToString();
            string[] orgstrs = orgstr.Split('-');
            string org = orgstrs[0];
            string deptId = this.labProcessID.Text.ToString();
            DataTable dt = new DataTable();
            if ( !this.cbCheckScanDate.Checked  && receiptNumber.Length <= 0)
            {
                MessageBox.Show("请输入至少1个查询条件");
                return;
            }
            else if (this.cbCheckScanDate.Checked )
            {
                dt = cms.getMesworktagscansSearch(starDataTime, stopDataTime, receiptNumber, true, org, deptId);
            }
            else
            {
                 dt = cms.getMesworktagscansSearch(starDataTime, stopDataTime, receiptNumber, false, org, deptId);
            }
            this.dgvInvoice.DataSource = dt;
            this.dgvInvoice.Refresh();


        }

        

        private void dgvInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tagInvoice = "";
            if (e.RowIndex < 0)
            {
                return;
            }
            //   this.splitContainer1.Panel2Collapsed = true;
            tagInvoice = this.dgvInvoice["tagInvoice", e.RowIndex].Value.ToString();
            DataTable ScanData = cmm.getMesworktagscansByinvoice(tagInvoice);
            if (ScanData.Rows.Count <= 0)
            {
                this.ScanDataDataSource = null;
                SetScanData(this.ScanDataDataSource);
                return;
            }
            this.ScanDataDataSource = ScanData;
            this.dgvWorkTagScans.DataSource = null;
            SetScanData(this.ScanDataDataSource);
        }

        private async void FrmCompletedToMes_LoadAsync(object sender, EventArgs e)
        {
             
            if (user.Rows.Count <= 0)
            {
                return;
            }   
            
            string LonginUserName = user.Rows[0]["UserName"].ToString();
           
            if (LonginUserName.IndexOf("SAA") != -1)
            {
                 this.org = "SAA";
            }
            if (LonginUserName.IndexOf("TOP") != -1)
            {
                this.org = "TOP";
            }

            int LonginDeptID = Convert.ToInt32(user.Rows[0]["deptID"]);
            int orgId =Convert.ToInt32( user.Rows[0]["marsk"]);
            string LonginDeptName = user.Rows[0]["DeptName"].ToString();
           bool isorg =  await getOrgAsync(orgId);
            if (isorg)
            {
                await getProductsAsync(LonginDeptID);
            }
            
        }

        public async  Task<bool>  getOrgAsync( int orgId)
        {
            int orgIndex = -1;
            string logingOrg = "";
            if (this.org == "SAA")
            {
                logingOrg =SAATest;
            }
            else if (this.org == "TOP")
            {
                logingOrg = TOPTest;
            }
            else
            {

                MessageBox.Show("获取厂区失败,请查询是否正常连线服务器");
                return false;
            }
            this.Orgs = await orgm.getOrgs(logingOrg);
            if (this.Orgs.Count <= 0)
            {
                MessageBox.Show("获取厂区失败,请查询是否正常连线服务器");
                return false;
            }
            this.cbOrg.Items.Clear();
            for (int i = 0; i < this.Orgs.Count; i++)  // ReportPlaceId = 2
            { 
                this.cbOrg.Items.Add(Orgs[i].ReportPlaceName);
                if(Orgs[i].ReportPlaceId == orgId  || Orgs[i].ReportPlaceName.IndexOf(this.org) != -1)
                {
                    orgIndex = i;
                }
            }
            if(orgIndex != -1)
            {
                this.cbOrg.SelectedIndex = orgIndex;
                this.cbOrg.Enabled = false;
                return true;
            }
            else
            {
                MessageBox.Show("厂区选择失败");
             //   this.Orgs = await orgm.getOrgs(logingOrg);
                return false;
            } 
        }

        public async Task getProductsAsync(int deptID)
        {
            int orgIndex = -1;
            if (this.cbOrg.Items.Count <= 0)
            {
                return;
            }
            string Org = this.cbOrg.SelectedItem.ToString();
            if (Org.IndexOf("SAA") != -1)
            {
                this.emps = await empm.GetAllProducts(this.SAAUlr); 
            }
            else
            {
                this.emps = await empm.GetAllProducts(this.TOPUlr);
            }

            if (emps.Count <= 0)
            {
                MessageBox.Show("获取制程失败,请查询是否正常连线服务器");
                return;
            }
            this.cbProcessName.Items.Clear();
            for (int i = 0; i < emps.Count; i++)
            {
                this.cbProcessName.Items.Add(emps[i].Name); // ID = "1"
                if (emps[i].ID == deptID.ToString())
                {
                    orgIndex = i;
                }
            }
            this.cbProcessName.SelectedIndex = orgIndex;
            this.cbProcessName.Enabled = false;
        }

        private void dgvInvoice_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvInvoice.Rows[e.RowIndex].Selected == false)
                    {
                        dgvInvoice.ClearSelection();
                        dgvInvoice.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvInvoice.SelectedRows.Count == 1)
                    {
                        dgvInvoice.CurrentCell = dgvInvoice.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    selectDgv = dgvInvoice;
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
