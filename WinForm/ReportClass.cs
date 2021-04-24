using DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public class ReportClass
    {
        /** 自动打印 **/
        private static int m_currentPageIndex;  //用来记录当前打印到第几页了
        private static IList<Stream> m_streams;  //LocalReport对象的Render方法会将报表按页输出为多个Stream对象。
        private static CompletedToMesService cms = new CompletedToMesService();

        public static void Print(LocalReport report,DataTable data)
        {
            Export(report);
            m_currentPageIndex = 0;
            if (m_streams == null || m_streams.Count == 0)
                return;

            //声明PrintDocument对象用于数据的打印 
            PrintDocument printDoc = new PrintDocument();
            //指定需要使用的打印机的名称，使用空字符串""来指定默认打印机 
            // printDoc.PrinterSettings.PrinterName = "HP LaserJet 1020";  
            //判断指定的打印机是否可用  
            if (!printDoc.PrinterSettings.IsValid)
            {
                MessageBox.Show("Can't find printer");
                return;
            }
            for(int i=0;i< data.Rows.Count; i++)
            {
                string id = data.Rows[i]["Id"].ToString();
                if (id.Length > 0)
                {
                    cms.updataMesworktagscansByinvoiceIsprint(id);
                }
               
            }

            //声明PrintDocument对象的PrintPage事件，具体的打印操作需要在这个事件中处理。  
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            //执行打印操作，Print方法将触发PrintPage事件。 
            printDoc.Print();
        }



        private static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。  
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }


        private static void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>8.5in</PageWidth>" +
              "  <PageHeight>11in</PageHeight>" +
              "  <MarginTop>0.25in</MarginTop>" +
              "  <MarginLeft>0.25in</MarginLeft>" +
              "  <MarginRight>0.25in</MarginRight>" +
              "  <MarginBottom>0.25in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
    }
}
