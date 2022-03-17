﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace COMMON
{
    public class ShippingPackgeExcelHelper : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private IWorkbook workbook1 = null;
        private FileStream fs = null;
        private bool disposed;



        public ShippingPackgeExcelHelper(string fileName)
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

                //4.创建CellStyle与DataFormat并加载格式样式
                IDataFormat dataformat = workbook.CreateDataFormat();

                ICellStyle style0 = workbook.CreateCellStyle();
                style0.DataFormat = dataformat.GetFormat("[DbNum2][$-804]General");//转化为汉字大写

                ICellStyle style1 = workbook.CreateCellStyle();
                style1.DataFormat = dataformat.GetFormat("0.0"); //改变小数精度【小数点后有几个0表示精确到小数点后几位】

                ICellStyle style2 = workbook.CreateCellStyle();
                style2.DataFormat = dataformat.GetFormat("#,##0.0");//分段添加，号

                ICellStyle style3 = workbook.CreateCellStyle();
                style3.DataFormat = dataformat.GetFormat("0.00E+00");//科学计数法

                ICellStyle style4 = workbook.CreateCellStyle();
                style4.DataFormat = dataformat.GetFormat("0.00;[Red]-0.00");//正数与负数的区分

                ICellStyle style5 = workbook.CreateCellStyle();
                style5.DataFormat = dataformat.GetFormat("# ??/??");//整数部分+分数

                ICellStyle style6 = workbook.CreateCellStyle();
                style6.DataFormat = dataformat.GetFormat("??/??");//分数

                ICellStyle style7 = workbook.CreateCellStyle();
                style7.DataFormat = dataformat.GetFormat("0.00%");//百分数【小数点后有几个0表示精确到显示小数点后几位】 

                // 数据对不上的颜色 
                ICellStyle QtyRowStyle = workbook.CreateCellStyle();
                QtyRowStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Tan.Index;
                QtyRowStyle.FillPattern = FillPattern.SolidForeground;

                // Booking 没有做的颜色 
                ICellStyle BookingRowStyle = workbook.CreateCellStyle();
                BookingRowStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                BookingRowStyle.FillPattern = FillPattern.SolidForeground;

                // 出货日期少于7天 入库少于80% 的颜色 
                ICellStyle lastIn7RowStyle = workbook.CreateCellStyle();
                lastIn7RowStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index;
                lastIn7RowStyle.FillPattern = FillPattern.SolidForeground;

                // 出货日期少于2天 入库少于90% 的颜色 
                ICellStyle lastIn2RowStyle = workbook.CreateCellStyle();
                lastIn2RowStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                lastIn2RowStyle.FillPattern = FillPattern.SolidForeground;


                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        //  row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        switch (data.Columns[j].ColumnName.ToString())
                        {
                            case "ShipPack Qty":  
                            case "Overflow Qty":
                            case "PO Qty":
                            case "PO Boxs":
                            case "In Qty":
                            case "In Boxs":
                            case "CH Qty":
                            case "CH Boxs":
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
                                 

                            case "CompletionRate":
                            case "ShipmentRatio":
                                string rate = Convert.ToString(data.Rows[i][j]);
                                

                                if (rate != "")
                                {
                                   double  t = Convert.ToDouble(rate) / 100;
                                    row.CreateCell(j).SetCellValue(String.Format("{0:0.##}", t));
                                }
                                else
                                {
                                    row.CreateCell(j).SetCellValue(0.00);
                                }
                                row.Cells[j].CellStyle = style7;
                                break;


                            default://空值处理
                                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                                break;
                        }
                        // 入库百分比率  入库数 / PO订单数
                        string CompletionRate = data.Rows[i]["CompletionRate"].ToString();
                        double CRate = Convert.ToDouble(CompletionRate);
                        DateTime nowdate = DateTime.Now;
                        string HODateStr = data.Rows[i]["HOD"].ToString(); // 出货日期
                        DateTime d1 = DateTime.Now;
                        DateTime d2 = Convert.ToDateTime(HODateStr);
                        TimeSpan d3 = d2.Subtract(d1);
                        string DifferenceDate = d3.Days.ToString();

                      
                        string shipQty = data.Rows[i]["ShipPack Qty"].ToString(); // 16 +17 出货单数量
                        string overQty = data.Rows[i]["Overflow Qty"].ToString();  //  出货单溢出数量
                        string GtnQty = data.Rows[i]["PO Qty"].ToString();  // POqty  PPR 装箱单 数量
                                                                            //x < 0 ? y = 10 : z = 20;
                        int sQty = shipQty.Length <= 0 ? 0 : Convert.ToInt32(shipQty);
                        int oQty = overQty.Length <= 0 ? 0 : Convert.ToInt32(overQty);
                        int gQty = GtnQty.Length <= 0 ? 0 : Convert.ToInt32(GtnQty);

                    
                        //booking没有做
                        if ( data.Rows[i]["Booking Status"].ToString() != "Yes")
                        {
                         //   row.Cells[j].CellStyle = BookingRowStyle;
                        }

                        // 数量对不上
                        if ((sQty + oQty) != gQty)
                        {
                            row.Cells[j].CellStyle = QtyRowStyle;
                        }


                        if (Convert.ToInt32(DifferenceDate) <= 7 && CRate <= 80)
                        {
                            // 出货日期少于7天 入库比率少于 80 %  橙色
                            row.Cells[j].CellStyle = lastIn7RowStyle;
                        }
                        if (Convert.ToInt32(DifferenceDate) <= 2 && CRate <= 90)
                        {
                            // 出货日期少于2天 入库比率少于 90 %  橙色
                            row.Cells[j].CellStyle = lastIn2RowStyle;
                        }


                        if (data.Columns[j].ColumnName.ToString() == "CompletionRate" || data.Columns[j].ColumnName.ToString() == "ShipmentRatio")
                        {
                            string CellsValue = row.Cells[j].ToString();
                            row.Cells[j].SetCellValue(Convert.ToDouble(String.Format("{0:0.##}", CellsValue)));
                          //  row.Cells[j].CellStyle = style7;
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
        public DataTable ExcelToDataTable(string sheetName, int headno)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
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
                DateTime afterDT1 = System.DateTime.Now;
                TimeSpan ts1 = afterDT1.Subtract(beforDT);
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
                if (headno != 0)
                {
                    startRow = sheet.FirstRowNum + headno;//第一行
                }
                else
                {
                    startRow = sheet.FirstRowNum;
                }

                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum;//获得行数
                    int maxcellCount = 0;//最大列数量
                    int maxcellno = 0;//最大列的行号                       
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

                    IRow firstRow = sheet.GetRow(maxcellno);
                    int cellNo = 0;
                    for (int i = firstRow.FirstCellNum; i < maxcellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {

                            cellNo = cellNo + 1;
                            string cellValue = Convert.ToString(cell) + cellNo;
                            if (cellValue != null)
                            {
                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }

                        }
                    }
                    //最后一列的标号
                    //int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null || row.Cells.Count == 0)
                        {
                            continue; //没有数据的行默认是null　　 continue 结束单次循环　　　　　
                        }
                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < maxcellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            {
                                string datastr = "";
                                /**********/
                                //单元格的类型为公式，返回公式的值  CellType.Numeric
                                if (row.GetCell(j).CellType == CellType.Numeric)
                                {
                                    //是日期型
                                    if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))
                                    {
                                        datastr = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                                        dataRow[j] = datastr;
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).NumericCellValue.ToString();
                                    }
                                }
                                else if (row.GetCell(j).CellType == CellType.Formula)
                                {
                                    dataRow[j] = row.GetCell(j).NumericCellValue.ToString();
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString();
                                }
                                /**********/

                                string va = dataRow[j].ToString();
                                System.Type d = dataRow[j].GetType();
                                while (j == 6 && va.Length <= 2)
                                {
                                    va = "0" + va;

                                }

                                bool r = IsDateTime(va);
                                // int r = va.IndexOf("月");

                                if (r)
                                {

                                    va = va.Replace(".", "-");
                                    va = va.Replace("/", "-");
                                    va = va.Replace("年", "-");
                                    va = va.Replace("月", "-");
                                    va = va.Replace("日", "");
                                    va = va.Replace("\\", "-");
                                    va = va.Replace("--", "-");

                                }

                                if (va.Length > 0 && va.Substring(va.Length - 1, 1) == "-")
                                {
                                    va = va.Substring(0, va.Length - 1);
                                }
                                dataRow[j] = va;
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }


                DateTime afterDT = System.DateTime.Now;
                TimeSpan ts = afterDT.Subtract(beforDT);
                //  MessageBox.Show("DateTime总共花费{0}ms."+Convert.ToString(ts.TotalMilliseconds));
                //  Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);
                return data;
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
            // dd-MM月-yyyy
            if (Regex.IsMatch(str, "^?((?<day>\\d{1,2}))?-(?<month>\\d{1,2})月?-((?<year>\\d{2,4}))?$"))
                isDateTime = true;

            // yyyy/MM/dd
            if (Regex.IsMatch(str, "^(?<year>\\d{2,4})/(?<month>\\d{1,2})/(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy\MM\dd
            if (Regex.IsMatch(str, "^(?<year>\\d{2,4})\\\\(?<month>\\d{1,2})\\\\(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy-MM-dd 
            if (Regex.IsMatch(str, "^(?<year>\\d{2,4})-(?<month>\\d{1,2})-(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy.MM.dd 
            if (Regex.IsMatch(str, "^(?<year>\\d{2,4})[.](?<month>\\d{1,2})[.](?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy年MM月dd
            if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(?<month>\\d{1,2})月((?<day>\\d{1,2}))?$"))
                isDateTime = true;
            // yyyy年MM月dd日
            if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(?<month>\\d{1,2})月((?<day>\\d{1,2})日)?$"))
                isDateTime = true;
            // yyyy年MM月dd日
            if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}日)?$"))
                isDateTime = true;


            // yyyy年MM月dd日
            if (Regex.IsMatch(str, "^(零|〇|一|二|三|四|五|六|七|八|九|十){2,4}年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
                isDateTime = true;
            // yyyy年
            //else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})年$"))
            //    isDateTime = true;

            // 农历1
            if (Regex.IsMatch(str, "^(甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
                isDateTime = true;
            // 农历2
            if (Regex.IsMatch(str, "^((甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月初(一|二|三|四|五|六|七|八|九|十)$"))
                isDateTime = true;

            // XX时XX分XX秒
            if (Regex.IsMatch(str, "^(?<hour>\\d{1,2})(时|点)(?<minute>\\d{1,2})分((?<second>\\d{1,2})秒)?$"))
                isDateTime = true;
            // XX时XX分XX秒
            if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})(时|点)((零|一|二|三|四|五|六|七|八|九|十){1,3})分(((零|一|二|三|四|五|六|七|八|九|十){1,3})秒)?$"))
                isDateTime = true;
            // XX分XX秒
            if (Regex.IsMatch(str, "^(?<minute>\\d{1,2})分(?<second>\\d{1,2})秒$"))
                isDateTime = true;
            // XX分XX秒
            if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})分((零|一|二|三|四|五|六|七|八|九|十){1,3})秒$"))
                isDateTime = true;

            // XX时
            if (Regex.IsMatch(str, "\\b(?<hour>\\d{1,2})(时|点钟)\\b"))
                isDateTime = true;


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
