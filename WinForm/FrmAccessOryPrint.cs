

using BLL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WinForm
{
    public partial class FrmAccessOryPrint : Form
    {
        private static FrmAccessOryPrint frm;
        public static string reno = "";
        public static string renoBatch = "";
        private BLL.accessoryOutManager accoryOut = new accessoryOutManager();
        public FrmAccessOryPrint()
        {
            InitializeComponent();
        }

        public static FrmAccessOryPrint GetSingleton(string  reNo, string  reNoBatch)
        {
             renoBatch = reNoBatch;
             reno = reNo;
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmAccessOryPrint();
            }
            return frm;
        }

       
        private void FrmAccessOryPrint_Load(object sender, EventArgs e)
        {
           
            //自定义数据源
            DataTable accessorydt = accoryOut.getAccessoryhByreceiveNumber(reno, renoBatch);
            this.reportViewer1.RefreshReport(); 

            //自定义参数
            List<ReportParameter> list = new List<ReportParameter>();
            ReportParameter rp = new ReportParameter("tagInvoice", "11");
            list.Add(rp);

            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\ReportAccessOryOut.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", accessorydt));//指定数据源
            this.reportViewer1.LocalReport.SetParameters(list); //参数设置 
            this.reportViewer1.RefreshReport();
        }
    }
}
