using BLL;
using gregn6Lib;
using System;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmAccessOryPrintGrid : Form
    {
        private static FrmAccessOryPrintGrid frm;
        public static string tagInvoice = "";
        public static string Part = "";
		public static string tagLocation = "";
        public static string taglines = "";

		public  string reportPath = System.Windows.Forms.Application.StartupPath + "\\report.grf"; 
        public static string dataconnect = "MYSQL;Database=mycat_fsg;Password=Sabrina123;Port=8066;Server=192.168.4.245;User=root";
		CompletedToMesManager cmm = new CompletedToMesManager();

		private GridppReport Report = new GridppReport();

        public FrmAccessOryPrintGrid()
        {
            InitializeComponent();
			Report.PrintEnd += isPrintEnd;
			//Report.PrintEnd += new PrintEventHandler(isPrintEnd);

		}
        public static FrmAccessOryPrintGrid GetSingleton(string tag ,string part ,string location,string lines)
        {
            tagInvoice = tag;
            Part = part;
			tagLocation = location;
			taglines = lines;
			if (frm == null || frm.IsDisposed)
            {
                frm = new FrmAccessOryPrintGrid();
            }
            return frm;
        }

        private void FrmAccessOryPrintGrid_Load(object sender, EventArgs e)
        {
            // MessageBox.Show(System.Windows.Forms.Application.StartupPath);
            //载入报表模板文件，必须保证 Grid++Report 的安装目录在‘C:\Grid++Report 6’下，
            //关于动态设置报表路径与数据绑定参数请参考其它例子程序  
            if (tagInvoice.Length <= 0)
            {
				MessageBox.Show("没有出库单号");
				return;
            }

			string QuerySQL = @"select tagOrg,
                                           tagLine,
                                           tagLocation,
                                           tagNumber,
                                           tagQty,
                                           Qty,
                                           tagStyle,
                                           tagColor,
                                           tagSize,
                                           tagInvoice,
                                           DeptName,
                                           tagScanAccount
                                    from ( 
								SELECT
										a.tagOrg,
										a.tagLine,
										a.tagLocation,
										a.tagNumber,
										a.tagQty,
										c.Qty,
										a.tagStyle,
										a.tagColor,
										a.tagSize,
										a.tagInvoice,
										a.DeptName,
										a.tagScanAccount 
									FROM
										(
										SELECT
											s.tagOrg,
											s.tagLine,
											s.tagLocation,
											s.tagNumber,
											s.tagQty,
											s.tagStyle,
											s.tagColor,
											s.tagSize,
											s.tagInvoice,
											d.DeptName,
											s.tagScanAccount 
										FROM
											mesworktagscans s
											LEFT JOIN mesdepts d ON d.DeptNumber = s.tagScanDeptID 
										WHERE
											s.tagInvoice = '" + tagInvoice + @"' 
											and s.tagLocation =  '" + tagLocation + @"'
										ORDER BY
											s.tagNumber 
										) a
										  right  JOIN (
										SELECT
											s.tagOrg,
											s.tagLine,
											s.tagLocation,
											SUM( s.tagQty ) Qty,
											s.tagStyle,
											s.tagColor,
											s.tagSize,
											s.tagInvoice,
											d.DeptName,
											s.tagScanAccount 
										FROM
											mesworktagscans s
											LEFT JOIN mesdepts d ON d.DeptNumber = s.tagScanDeptID 
										WHERE
											s.tagInvoice =  '" + tagInvoice + @"'  
											and s.tagLocation =  '" + tagLocation + @"'  
										GROUP BY
											s.tagOrg,
											s.tagLine,
											s.tagLocation,
											s.tagStyle,
											s.tagColor,
											s.tagSize,
											s.tagInvoice,
											d.DeptName,
											s.tagScanAccount 
										ORDER BY
											s.tagNumber 
										) c ON c.tagInvoice = a.tagInvoice 
										AND c.tagStyle = a.tagstyle 
										AND c.tagSize = a.tagsize 
										AND c.tagColor = a.tagcolor
                                        and c.tagLine = a.tagLine
                                    ) d group by  tagOrg,
                                           tagLine,
                                           tagLocation,
                                           tagNumber,
                                           tagQty, 
                                           tagStyle,
                                           tagColor,
                                           tagSize,
                                           tagInvoice,
                                           DeptName,
                                           tagScanAccount";

			Report.LoadFromFile(System.Windows.Forms.Application.StartupPath + "\\report.grf");
			Report.DetailGrid.Recordset.ConnectionString = dataconnect; 
			Report.DetailGrid.Recordset.QuerySQL = QuerySQL; 
			string p = "";
			if (Part.Length > 11)
            {
				p = Part.Substring(Part.Length-11,1);
            }
			Report.ParameterByName("tagPart").AsString = p;
            Report.ParameterByName("taglines").AsString = taglines;
			this.axGRPrintViewer1.Report = Report;
			axGRPrintViewer1.Start();
		}

        private void FrmAccessOryPrintGrid_Resize(object sender, EventArgs e)
        {
			this.axGRPrintViewer1.Width = this.Width - 20;
            this.axGRPrintViewer1.Height = this.Height - 50;
		}

		public void isPrintEnd( )
		{
			cmm.updataIsPrints(tagInvoice, tagLocation);		  
			Report.PrintEnd -= isPrintEnd;//取消侦听注册事件，避免多次侦听
		}
	}
}
