using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMMON
{
    public class AsicsExcelHelper : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private IWorkbook workbook1 = null;
        private FileStream fs = null;
        private bool disposed;



        public AsicsExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        //  row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        switch (data.Columns[j].ColumnName.ToString())
                        {
                            case "boxqty"://整型  
                            case "boxSizeQty":
                                string dd = Convert.ToString(data.Rows[i][j]);
                                if (dd != "")
                                {
                                    row.CreateCell(j).SetCellValue(Convert.ToInt32(dd));
                                }
                                else
                                {
                                    row.CreateCell(j).SetCellValue(0);
                                }

                                break;
                            default://空值处理
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                                break;
                        }

                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <param name="headno">要跳过的行</param>
        /// <returns>返回的DataTable</returns>
        public List<DataTable> ExcelToDataTable(string sheetName, int headno, string file)
        {
            List<DataTable> ld = new List<DataTable>();
            ISheet sheet = null;
            DataTable con_ppr_data = new DataTable();
            DataTable con_detail_data = new DataTable();
            DataTable error_data = new DataTable();
            error_data.Columns.Add("ErrorFileName");

            string PPrfNo = "";
            string PO = "";
            int startRow = 0;
            // 计时
            DateTime beforDT = System.DateTime.Now;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                // DateTime afterDT1 = System.DateTime.Now;
                // TimeSpan ts1 = afterDT1.Subtract(beforDT);
                //  MessageBox.Show("DateTime总共花费{0}ms." + Convert.ToString(ts1.TotalMilliseconds));
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                



                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum;//获得行数
                    int maxcellCount = 0;//最大列数量
                    int maxcellno = 0;//最大列的行号 

                    IRow PLANrow = sheet.GetRow(6); // [0]{ PACKING LIST BY PACK PLAN}

                    // 确认是不是ASICS 的新版本  PACKING LIST BY PACK PLAN
                    IRow Pagerow = sheet.GetRow(14); // [0]{ PACKING LIST BY PACK PLAN}
                    if (Pagerow.Cells.Count > 0 )
                    {
                        startRow = 21;
                    }
                    else
                    {
                        startRow = 22;
                    }
                    
                    string packlist = PLANrow.Cells[0].ToString().Trim();

                    if (packlist != "PACKING LIST BY PACK PLAN")
                    {

                        DataRow error_dataRow = error_data.NewRow();
                        error_dataRow["ErrorFileName"] = file;
                        error_data.Rows.Add(error_dataRow);
                        // return ld;
                    }
                    else
                    {



                        IRow PPRrow = sheet.GetRow(8);
                        IRow POrow = sheet.GetRow(10);  //15  10 5

                        PPrfNo = PPRrow.Cells[11].ToString();
                        if (PPRrow.Cells.Count == 12)
                        {
                            PPrfNo = PPRrow.Cells[8].ToString();
                        }
                        if (PPRrow.Cells.Count == 13)
                        {
                            PPrfNo = PPRrow.Cells[9].ToString();
                        }

                        if (PPRrow.Cells.Count == 14)
                        {
                            PPrfNo = PPRrow.Cells[10].ToString();
                        }

                        if (POrow.Cells.Count == 15)
                        {
                            PO = POrow.Cells[10].ToString();
                        } 
                        if (POrow.Cells.Count == 10)
                        {
                            PO = POrow.Cells[5].ToString();
                        }
                        if (POrow.Cells.Count == 9)
                        {
                            PO = POrow.Cells[5].ToString();
                        }
                        if (POrow.Cells.Count == 8)
                        {
                            PO = POrow.Cells[4].ToString();
                        }


                        /*

                        // 取得最长列的行与列数据
                        for (int i = startRow; i <= rowCount; ++i)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null)
                            {
                                continue; //没有数据的行默认是null　　 continue 结束单次循环　　　　　
                            }
                            int cellCount = row.LastCellNum; //一行最后一个cell的编号 这一行的总的列数
                            if (cellCount > maxcellCount)
                            {
                                maxcellCount = cellCount;//最大列数
                                maxcellno = i;
                            }
                        }
                        */

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

                        /*
                         id	Cust_id	Serial_From	Buyer_Item	Item_desc	color_code	Size1	con_Qty	qty	pprfno
                      */
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

                        //每一行的内容  最后2行总计不用
                        string conNumber = "";
                        string con_no = "0";
                        for (int i = startRow; i < rowCount - 1; i++)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null || row.Cells.Count == 0)
                            {
                                continue; //没有数据的行默认是null　　 continue 结束单次循环　　　　　
                            }
                            string Serial_From = "";
                           // 获取箱数量
                           int Ctns = 0;
                            if (row.Cells.Count == 28 && row.Cells[5] != null && row.Cells[5].ToString() != "")
                            {
                                Ctns = Convert.ToInt32(row.Cells[5].ToString());
                                Serial_From = row.Cells[1].ToString();
                            }

                            if (row.Cells.Count > 17 && row.Cells[3] != null && row.Cells[3].ToString() != "")
                            {
                                Ctns = Convert.ToInt32(row.Cells[3].ToString());
                                Serial_From = row.Cells[0].ToString();
                            }
                            if (row.Cells.Count == 14 && row.Cells[3] != null &&  row.Cells[3].ToString() != "")
                            {
                                Ctns = Convert.ToInt32(row.Cells[3].ToString());
                                Serial_From = row.Cells[0].ToString();
                            }
                            else  if (row.Cells.Count == 14 && row.Cells[1] != null && row.Cells[1].ToString() != "")
                            {
                                Ctns = Convert.ToInt32(row.Cells[1].ToString());
                                Serial_From = row.Cells[0].ToString();
                            }

                            if (row.Cells.Count == 13 && row.Cells[1] != null && row.Cells[3].ToString() != "")
                            {
                                Ctns = Convert.ToInt32(row.Cells[1].ToString());
                                Serial_From = row.Cells[0].ToString();
                            }

                             

                            if (Serial_From.Length == 17)
                            {
                                Serial_From = Serial_From.Substring(0, Serial_From.Length - 3);
                                con_no = Serial_From.Substring(Serial_From.Length - 4, 4);
                                con_no = con_no.TrimStart('0');
                               // con_no = Convert.ToString(Convert.ToInt32(con_no));

                                conNumber = Serial_From.Substring(Serial_From.Length - 4, 4);
                                //conNumber = Convert.ToString(Convert.ToInt32(conNumber));
                                conNumber = conNumber.TrimStart('0');
                            }


                            if (Ctns > 0)
                            {

                                string qty = "";
                                string style = "";
                                string color_code = "";
                                string SizeCode = "";
                                string con_Qty = "";

                                for (int z = 0; z < Ctns; z++)
                                {
                                    if (i + 2 >= rowCount)
                                    {
                                        continue;
                                    }

                                    if (z != 0)
                                    {
                                        conNumber = (Convert.ToInt32(conNumber) + 1).ToString();
                                        Serial_From = Serial_From.Substring(0, Serial_From.Length - conNumber.Length) + conNumber;
                                    }
                                    DataRow con_ppr_dataRow = con_ppr_data.NewRow();
                                    DataRow con_detail_dataRow = con_detail_data.NewRow();
                                    IRow QTYrow = sheet.GetRow( i + 2);

                                    if (QTYrow.Cells.Count == 8)
                                    {
                                        qty = QTYrow.Cells[6].ToString();
                                        qty = qty.Replace(",", "");
                                        qty = Convert.ToString(Convert.ToInt32(qty) / Convert.ToInt32(Ctns)); //总件数 / 箱数
                                        style = QTYrow.Cells[0].ToString();
                                        color_code = QTYrow.Cells[3].ToString();
                                        SizeCode = QTYrow.Cells[5].ToString();
                                        con_Qty = QTYrow.Cells[6].ToString();
                                        con_Qty = con_Qty.Replace(",", "");
                                    }
                                    if (QTYrow.Cells.Count == 27)
                                    {
                                        qty = QTYrow.Cells[12].ToString();
                                        qty = qty.Replace(",","");
                                        qty = Convert.ToString(Convert.ToInt32(qty) / Convert.ToInt32(Ctns)); //总件数 / 箱数
                                        style = QTYrow.Cells[6].ToString();
                                        color_code = QTYrow.Cells[9].ToString();
                                        SizeCode = QTYrow.Cells[11].ToString();
                                        con_Qty = QTYrow.Cells[12].ToString();
                                        con_Qty = con_Qty.Replace(",", "");
                                    }


                                    string Net_Net ="";
                                    string con_net = "";
                                    string con_Gross = "";
                                    string count1 = "";
                                    if (row.Cells.Count == 28)
                                    {
                                        Net_Net = row.Cells[14].ToString();
                                        con_net = row.Cells[18].ToString();
                                        con_Gross = row.Cells[23].ToString();
                                        count1 = row.Cells[5].ToString();
                                    }

                                    if (row.Cells.Count == 18)
                                    {
                                        Net_Net = row.Cells[4].ToString();
                                        con_net = row.Cells[8].ToString();
                                        con_Gross = row.Cells[13].ToString();
                                        count1 = row.Cells[3].ToString();
                                    }

                                  

                                    if (row.Cells.Count == 14 )
                                    {
                                         Net_Net = row.Cells[4].ToString();
                                         con_net = row.Cells[7].ToString();
                                         con_Gross = row.Cells[11].ToString();
                                         count1 = row.Cells[3].ToString();
                                    }

                                    if (row.Cells.Count == 13)
                                    {
                                        Net_Net = row.Cells[2].ToString();
                                        con_net = row.Cells[5].ToString();
                                        con_Gross = row.Cells[9].ToString();
                                        count1 = row.Cells[1].ToString();
                                    }

                                    con_ppr_dataRow["PPrfNo"] = PPrfNo;
                                    con_ppr_dataRow["PO"] = PO;
                                    con_ppr_dataRow["MAIN_LINE"] = "";
                                    con_ppr_dataRow["Serial_From"] = Serial_From;
                                    con_ppr_dataRow["id"] = "ASICS-" + Serial_From;
                                    con_ppr_dataRow["qty"] = qty;
                                    con_ppr_dataRow["Net_Net"] = Net_Net;
                                    con_ppr_dataRow["con_net"] = con_net;
                                    con_ppr_dataRow["con_Gross"] = con_Gross;
                                    con_ppr_dataRow["count1"] = count1;
                                    con_ppr_dataRow["con_no"] = con_no;
                                    con_ppr_dataRow["con_to"] = conNumber;
                                    con_ppr_dataRow["org"] = "SAA";
                                    con_ppr_dataRow["Cust_id"] = "ASICS";
                                    con_ppr_dataRow["create_pc"] = Dns.GetHostName().ToUpper();
                                    con_ppr_dataRow["update_date"] = DateTime.Now.ToString("yyyy-MM-dd");
                                    con_ppr_dataRow["country_code"] = "";
                                    con_ppr_dataRow["Pkg_Code"] = "";
                                    con_ppr_dataRow["Scan_ID"] = "";
                                    con_ppr_dataRow["con_L"] = "";
                                    con_ppr_dataRow["con_W"] = "";
                                    con_ppr_dataRow["con_H"] = "";
                                    con_ppr_dataRow["b_Volume"] = "";
                                    con_ppr_data.Rows.Add(con_ppr_dataRow);

                                    /* id	Cust_id	Serial_From	Buyer_Item	Item_desc	color_code	Size1	con_Qty	qty	pprfno */
                                    con_detail_dataRow["id"] = "ASICS-" + Serial_From;
                                    con_detail_dataRow["Cust_id"] = "ASICS";
                                    con_detail_dataRow["Serial_From"] = Serial_From;
                                    con_detail_dataRow["Buyer_Item"] = style;
                                    con_detail_dataRow["Item_desc"] = "";
                                    con_detail_dataRow["color_code"] = color_code;
                                    con_detail_dataRow["Size1"] = SizeCode;
                                    con_detail_dataRow["con_Qty"] = con_Qty;
                                    con_detail_dataRow["qty"] = qty;
                                    con_detail_dataRow["pprfno"] = PPrfNo;
                                    con_detail_data.Rows.Add(con_detail_dataRow);

                                    for (int j = 1; j < rowCount; j++) //  +3 +4 混装箱  J
                                    {
                                        IRow FQTYrow = sheet.GetRow(i + 2 + j);
                                        if (FQTYrow != null && (FQTYrow.Cells.Count == 8 || FQTYrow.Cells.Count == 27))
                                        {
                                            DataRow Fcon_ppr_dataRow = con_ppr_data.NewRow();
                                            DataRow Fcon_detail_dataRow = con_detail_data.NewRow();
                                            string dqty = "";
                                            string cqty = "";
                                            if (row.Cells.Count == 8)
                                            {
                                                 cqty = FQTYrow.Cells[6].ToString();
                                                cqty = Convert.ToString(Convert.ToInt32(cqty) / Convert.ToInt32(Ctns)); //总件数 / 箱数
                                            }
                                            else  if (FQTYrow.Cells.Count == 27   )
                                            {
                                                 dqty = FQTYrow.Cells[12].ToString();

                                                if(dqty != "")
                                                {
                                                     
                                                       cqty = Convert.ToString(Convert.ToInt32(dqty) / Convert.ToInt32(Ctns)); //总件数 / 箱数
                                                }
                                                
                                            }

                                           

                                            // Net_Net = row.Cells[4].ToString();
                                            //  con_net = row.Cells[8].ToString();
                                            //   con_Gross = row.Cells[13].ToString();
                                            //  count1 = row.Cells[3].ToString();

                                            if (row.Cells.Count == 27)
                                            {
                                                Net_Net = row.Cells[4].ToString();
                                                con_net = row.Cells[8].ToString();
                                                con_Gross = row.Cells[13].ToString();
                                                count1 = row.Cells[3].ToString();
                                            }


                                            if (row.Cells.Count == 18)
                                            {
                                                Net_Net = row.Cells[4].ToString();
                                                con_net = row.Cells[8].ToString();
                                                con_Gross = row.Cells[13].ToString();
                                                count1 = row.Cells[3].ToString();
                                            }



                                            if (row.Cells.Count == 14)
                                            {
                                                Net_Net = row.Cells[4].ToString();
                                                con_net = row.Cells[7].ToString();
                                                con_Gross = row.Cells[11].ToString();
                                                count1 = row.Cells[3].ToString();
                                            }

                                            if (row.Cells.Count == 13)
                                            {
                                                Net_Net = row.Cells[2].ToString();
                                                con_net = row.Cells[5].ToString();
                                                con_Gross = row.Cells[9].ToString();
                                                count1 = row.Cells[1].ToString();
                                            }
                                            if (FQTYrow.Cells.Count == 27 && FQTYrow.Cells[6].ToString() != "")
                                            {
                                                
                                                Fcon_ppr_dataRow["PPrfNo"] = PPrfNo;
                                                Fcon_ppr_dataRow["PO"] = PO;
                                                Fcon_ppr_dataRow["MAIN_LINE"] = "";
                                                Fcon_ppr_dataRow["Serial_From"] = Serial_From;
                                                Fcon_ppr_dataRow["id"] = "ASICS-" + Serial_From;
                                                Fcon_ppr_dataRow["qty"] = cqty;
                                                Fcon_ppr_dataRow["Net_Net"] = Net_Net;
                                                Fcon_ppr_dataRow["con_net"] = con_net;
                                                Fcon_ppr_dataRow["con_Gross"] = con_Gross;
                                                Fcon_ppr_dataRow["count1"] = count1;
                                                Fcon_ppr_dataRow["con_no"] = con_no;
                                                Fcon_ppr_dataRow["con_to"] = conNumber;
                                                Fcon_ppr_dataRow["org"] = "SAA";
                                                Fcon_ppr_dataRow["Cust_id"] = "ASICS";
                                                Fcon_ppr_dataRow["create_pc"] = Dns.GetHostName().ToUpper();
                                                Fcon_ppr_dataRow["update_date"] = DateTime.Now.ToString("yyyy-MM-dd");
                                                Fcon_ppr_dataRow["country_code"] = "";
                                                Fcon_ppr_dataRow["Pkg_Code"] = "";
                                                Fcon_ppr_dataRow["Scan_ID"] = "";
                                                Fcon_ppr_dataRow["con_L"] = "";
                                                Fcon_ppr_dataRow["con_W"] = "";
                                                Fcon_ppr_dataRow["con_H"] = "";
                                                Fcon_ppr_dataRow["b_Volume"] = "";
                                                con_ppr_data.Rows.Add(Fcon_ppr_dataRow);

                                            
                                                style = FQTYrow.Cells[6].ToString();
                                                color_code = FQTYrow.Cells[9].ToString();
                                                SizeCode = FQTYrow.Cells[11].ToString();
                                                Fcon_detail_dataRow["id"] = "ASICS-" + Serial_From;
                                                Fcon_detail_dataRow["Cust_id"] = "ASICS";
                                                Fcon_detail_dataRow["Serial_From"] = Serial_From;
                                                Fcon_detail_dataRow["Buyer_Item"] = style;
                                                Fcon_detail_dataRow["Item_desc"] = "";
                                                Fcon_detail_dataRow["color_code"] = color_code;
                                                Fcon_detail_dataRow["Size1"] = SizeCode;
                                                Fcon_detail_dataRow["con_Qty"] = FQTYrow.Cells[12].ToString();
                                                Fcon_detail_dataRow["qty"] = cqty;
                                                Fcon_detail_dataRow["pprfno"] = PPrfNo;
                                                con_detail_data.Rows.Add(Fcon_detail_dataRow);
                                            }
                                            else if(row.Cells.Count == 8)
                                            {
                                                Fcon_ppr_dataRow["PPrfNo"] = PPrfNo;
                                                Fcon_ppr_dataRow["PO"] = PO;
                                                Fcon_ppr_dataRow["MAIN_LINE"] = "";
                                                Fcon_ppr_dataRow["Serial_From"] = Serial_From;
                                                Fcon_ppr_dataRow["id"] = "ASICS-" + Serial_From;
                                                Fcon_ppr_dataRow["qty"] = cqty;
                                                Fcon_ppr_dataRow["Net_Net"] = Net_Net;
                                                Fcon_ppr_dataRow["con_net"] = con_net;
                                                Fcon_ppr_dataRow["con_Gross"] = con_Gross;
                                                Fcon_ppr_dataRow["count1"] = count1;
                                                Fcon_ppr_dataRow["con_no"] = con_no;
                                                Fcon_ppr_dataRow["con_to"] = conNumber;
                                                Fcon_ppr_dataRow["org"] = "SAA";
                                                Fcon_ppr_dataRow["Cust_id"] = "ASICS";
                                                Fcon_ppr_dataRow["create_pc"] = Dns.GetHostName().ToUpper();
                                                Fcon_ppr_dataRow["update_date"] = DateTime.Now.ToString("yyyy-MM-dd");
                                                Fcon_ppr_dataRow["country_code"] = "";
                                                Fcon_ppr_dataRow["Pkg_Code"] = "";
                                                Fcon_ppr_dataRow["Scan_ID"] = "";
                                                Fcon_ppr_dataRow["con_L"] = "";
                                                Fcon_ppr_dataRow["con_W"] = "";
                                                Fcon_ppr_dataRow["con_H"] = "";
                                                Fcon_ppr_dataRow["b_Volume"] = "";
                                                con_ppr_data.Rows.Add(Fcon_ppr_dataRow);

                                                style = FQTYrow.Cells[0].ToString();
                                                color_code = FQTYrow.Cells[3].ToString();
                                                SizeCode = FQTYrow.Cells[5].ToString();
                                                Fcon_detail_dataRow["id"] = "ASICS-" + Serial_From;
                                                Fcon_detail_dataRow["Cust_id"] = "ASICS";
                                                Fcon_detail_dataRow["Serial_From"] = Serial_From;
                                                Fcon_detail_dataRow["Buyer_Item"] = style;
                                                Fcon_detail_dataRow["Item_desc"] = "";
                                                Fcon_detail_dataRow["color_code"] = color_code;
                                                Fcon_detail_dataRow["Size1"] = SizeCode;
                                                Fcon_detail_dataRow["con_Qty"] = FQTYrow.Cells[6].ToString();
                                                Fcon_detail_dataRow["qty"] = cqty;
                                                Fcon_detail_dataRow["pprfno"] = PPrfNo;
                                                con_detail_data.Rows.Add(Fcon_detail_dataRow);
                                            }
                                        }
                                         
                                        else
                                        {
                                            break;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                // DateTime afterDT = System.DateTime.Now;
                //  TimeSpan ts = afterDT.Subtract(beforDT);
                //  MessageBox.Show("DateTime总共花费{0}ms."+Convert.ToString(ts.TotalMilliseconds));
                //  Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);
                ld.Add(con_ppr_data);
                ld.Add(con_detail_data);
                ld.Add(error_data);
                return ld;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 使用正则表达式判断是否为日期
        /// </summary>
        /// <param name="str" type=string></param>
        /// <returns name="isDateTime" type=bool></returns>
        public bool IsDateTime(string str)
        {
            bool isDateTime = false;
            // yyyy/MM/dd
            if (Regex.IsMatch(str, "^(?<year>\\d{2,4})/(?<month>\\d{1,2})/(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy\MM\dd
            if (Regex.IsMatch(str, "^(?<year>\\d{2,4})\\\\(?<month>\\d{1,2})\\\\(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy-MM-dd 
            else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})-(?<month>\\d{1,2})-(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy.MM.dd 
            else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})[.](?<month>\\d{1,2})[.](?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy年MM月dd
            else if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(?<month>\\d{1,2})月((?<day>\\d{1,2}))?$"))
                isDateTime = true;
            // yyyy年MM月dd日
            else if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(?<month>\\d{1,2})月((?<day>\\d{1,2})日)?$"))
                isDateTime = true;
            // yyyy年MM月dd日
            else if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}日)?$"))
                isDateTime = true;


            // yyyy年MM月dd日
            else if (Regex.IsMatch(str, "^(零|〇|一|二|三|四|五|六|七|八|九|十){2,4}年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
                isDateTime = true;
            // yyyy年
            //else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})年$"))
            //    isDateTime = true;

            // 农历1
            else if (Regex.IsMatch(str, "^(甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
                isDateTime = true;
            // 农历2
            else if (Regex.IsMatch(str, "^((甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月初(一|二|三|四|五|六|七|八|九|十)$"))
                isDateTime = true;

            // XX时XX分XX秒
            else if (Regex.IsMatch(str, "^(?<hour>\\d{1,2})(时|点)(?<minute>\\d{1,2})分((?<second>\\d{1,2})秒)?$"))
                isDateTime = true;
            // XX时XX分XX秒
            else if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})(时|点)((零|一|二|三|四|五|六|七|八|九|十){1,3})分(((零|一|二|三|四|五|六|七|八|九|十){1,3})秒)?$"))
                isDateTime = true;
            // XX分XX秒
            else if (Regex.IsMatch(str, "^(?<minute>\\d{1,2})分(?<second>\\d{1,2})秒$"))
                isDateTime = true;
            // XX分XX秒
            else if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})分((零|一|二|三|四|五|六|七|八|九|十){1,3})秒$"))
                isDateTime = true;

            // XX时
            else if (Regex.IsMatch(str, "\\b(?<hour>\\d{1,2})(时|点钟)\\b"))
                isDateTime = true;
            else
                isDateTime = false;

            return isDateTime;
        }

        public DataTable ReadExcelToDataTable(string fileName, string sheetName = null, bool isFirstRowColumn = true)
        {
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;

            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                //根据指定路径读取文件
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook1 = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook1 = new HSSFWorkbook(fs);

                //根据文件流创建excel数据结构
                //           Npoi.SS.UserModel.IWorkbook workbook = Npoi.SS.UserModel.WorkbookFactory.Create(fs);
                //IWorkbook workbook = new HSSFWorkbook(fs);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook1.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook1.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook1.GetSheetAt(0);
                }

                if (sheet != null)
                {
                    //Npoi.SS.UserModel.IRow firstRow = sheet.GetRow(0);
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            //    Npoi.SS.UserModel.ICell cell = firstRow.GetCell(i);
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        //           Npoi.SS.UserModel.IRow row = sheet.GetRow(i);
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }

        public string[] getExcelSheetName(string sheetName)
        {
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                // sheet = workbook.GetSheet(sheetName);
                int i = workbook.NumberOfSheets;////获取个数
                                                //    string sheetname = workbook.GetSheetName(0);//获取名字

                string[] sheetname = new string[i];//表名
                for (int t = 0; t < i; t++)
                {
                    sheetname[t] = workbook.GetSheetName(t);
                    // sheetname[t] = workbook.GetSheetName(t);
                }

                return sheetname;
                //  return i;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // Console.WriteLine("Exception: " + ex.Message);
                string[] sheetname = { "未找到表名" };
                return sheetname;
            }
        }

        //public string getExcelSheetName(int sheetNo)
        //{
        //    try
        //    {
        //        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        //        if (fileName.IndexOf(".xlsx") > 0) // 2007版本
        //            workbook = new XSSFWorkbook(fs);
        //        else if (fileName.IndexOf(".xls") > 0) // 2003版本
        //            workbook = new HSSFWorkbook(fs);
        //        // sheet = workbook.GetSheet(sheetName);
        //        string sheetname = workbook.GetSheetName(0);////获取个数
        //                                        //    string sheetname = workbook.GetSheetName(0);//获取名字
        //        return sheetname;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        // Console.WriteLine("Exception: " + ex.Message);
        //        return "";
        //    }
        //}

    }
}
