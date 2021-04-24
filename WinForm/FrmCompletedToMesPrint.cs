using DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmCompletedToMesPrint : Form
    {
        private static FrmCompletedToMesPrint frm;
        public static string tagInvoice = "" ;
        public static Bitmap byteImage =null;
        private CompletedToMesService cms = new CompletedToMesService(); 
        public FrmCompletedToMesPrint()
        {
            InitializeComponent();
        }
        public static FrmCompletedToMesPrint GetSingleton(string tag, Bitmap QRcode)
        {
            tagInvoice  = tag;
            byteImage = QRcode;
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmCompletedToMesPrint();
            }
            return frm;
        }


        private void FrmCompletedToMesPrint_Load(object sender, EventArgs e)
        {
            
            //自定义数据源
            DataTable ScanSourceData = cms.getMesworktagscansByinvoiceGroup(tagInvoice);
            DataTable ScanSourceDataDetail = cms.getMesworktagscansByinvoiceDetail(tagInvoice);
            this.reportViewer1.RefreshReport(); 

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
            sizes = ScanSourceData.Rows[0]["tagSize"].ToString();
            qtys = ScanSourceData.Rows[0]["tagQty"].ToString();
           
            // }
            string[] ss = sizes.Split(',');
            string[] qs = qtys.ToString().Split(',');
            sizes = "";
            qtys = "";
            foreach (var s in ss)
            {
                sizes = sizes + " , " + string.Format("{0,-7:G}", s.Trim().ToUpper());
            }
            foreach (var q in qs)
            {
                qtys = qtys + " , " + string.Format("{0,-6:G}", q.Trim());
            }

            /*
            for (int i = 0; i < ScanSourceData.Rows.Count; i++)
            {
                styles = styles +"| "+ ScanSourceData.Rows[i]["tagStyle"].ToString();
                colors = colors + "| " + ScanSourceData.Rows[i]["tagColor"].ToString();
                sizes = sizes + "| " + ScanSourceData.Rows[i]["tagSize"].ToString();
                qtys = qtys + ScanSourceData.Rows[i]["tagQty"].ToString();
            }
            */
            sizes = sizes.Substring(2, sizes.Length - 2);
            qtys = qtys.Substring(2, qtys.Length - 2);
            styles = styles.Substring(1, styles.Length - 1);
            colors = colors.Substring(1, colors.Length - 1);
           // sizes = sizes.Substring(1, sizes.Length - 1);            
            ReportParameter invoice = new ReportParameter("tagInvoice", ScanSourceData.Rows[0]["tagInvoice"].ToString());            
            ReportParameter style = new ReportParameter("tagStyle", styles);           
            ReportParameter color = new ReportParameter("tagColor", colors);
            ReportParameter size = new ReportParameter("tagSize", sizes);
            ReportParameter qty = new ReportParameter("tagQty", qtys.ToString());
            ReportParameter dept = new ReportParameter("DeptName", ScanSourceData.Rows[0]["DeptName"].ToString());             
            ReportParameter org = new ReportParameter("tagOrg", ScanSourceData.Rows[0]["tagOrg"].ToString());
            ReportParameter taglocation = new ReportParameter("taglocation", location);
            ReportParameter part = new ReportParameter("part", ScanSourceData.Rows[0]["part"].ToString());

            byte[] imgBytes = bmpToBytes(byteImage);//（此为上述方法，bmp为实现绘制好的Bitmap对象或通过别的方式转化为来的Bitmap对象）
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
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\ReportCompletedToMes.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();
            //this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", ScanSourceData));//指定数据源1
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", ScanSourceDataDetail));//指定数据源2
                                                                                                                                                
              this.reportViewer1.LocalReport.SetParameters(list); //参数设置 
           // this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rpTest });//设置本地报表参数属性，reportViewer1为报表控件。

            this.reportViewer1.RefreshReport();
            


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
    }
}
