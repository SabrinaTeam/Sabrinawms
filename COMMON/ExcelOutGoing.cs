using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace COMMON
{
    public class ExcelOutGoing : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private IWorkbook workbook1 = null;
        private FileStream fs = null;
        private bool disposed;



        public ExcelOutGoing(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

      
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten,string tableName)
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
                /*
                ICellStyle cellStyle = workbook.CreateCellStyle();
                cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                cellStyle.FillPattern = FillPattern.SolidForeground;
                */
                if (tableName== "dgvReceiData" || tableName == "dgvOutgoingTable")
                {
                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            switch (data.Columns[j].ColumnName.ToString())
                            {
                                case "size_qty"://整型
                                case "box_qty":
                                case "PO_qty":
                                case "qtyCount":
                                case "boxCount":
                                    string dd = Convert.ToString( data.Rows[i][j]);
                                    if (dd != "")
                                    {
                                        row.CreateCell(j).SetCellValue(Convert.ToInt32(dd));
                                    }
                                    else
                                    {
                                        row.CreateCell(j).SetCellValue(0);
                                    }
                                    
                                    break;
                                case "kg":
                                    string tt = Convert.ToString(data.Rows[i][j]);
                                    if (tt != "")
                                    {
                                        row.CreateCell(j).SetCellValue(Convert.ToDouble(tt));
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
                }
                if (tableName == "dgvOutCount")
                {
                    for (i = 0; i < data.Rows.Count; ++i)
                    {


                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            switch (data.Columns[j].ColumnName.ToString())
                            {
                                case "yymm"://整型
                                case "Buyer_Item":
                                case "color_code":
                                case "GtnPO":
                                case "MAIN_LINE":
                                    row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                                    break;
                                default://其它数字
                                    string dd = Convert.ToString(data.Rows[i][j]);
                                    if (dd != "")
                                    {
                                        row.CreateCell(j).SetCellValue(Convert.ToInt32(dd));
                                    }
                                    else
                                    {
                                        row.CreateCell(j).SetCellValue("");
                                    }
                                    break;
                            }
                        }
                        ++count;
                    }
                }
                if (tableName == "My_Noumber")
                {
                    for (i = 0; i < data.Rows.Count; ++i)
                    {


                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                           // string dd = Convert.ToString(data.Rows[i][j]);
                            switch (data.Columns[j].ColumnName.ToString())
                            {
                                case "qty"://整型
                                    string dd = Convert.ToString(data.Rows[i][j]);
                                    if (dd != "")
                                    {
                                        row.CreateCell(j).SetCellValue(Convert.ToDouble(dd));

                                    }
                                    else
                                    {
                                        row.CreateCell(j).SetCellValue("");
                                    }
                                    break;
                                default://其它字符串 
                                    row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString()); 
                                    break;
                            }
                        }
                        ++count;
                    }
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
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    //  int i = workbook.NumberOfSheets;////获取个数
                    //    string sheetname = workbook.GetSheetName(0);//获取名字
                    //sheet = workbook.GetSheetAt(0); //获取指定的那一个
                    //  MessageBox.Show(i.ToString());
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
                            // string cellValue = "";
                            /**********
                            //单元格的类型为公式，返回公式的值
                            if (cell.CellType == CellType.Numeric)
                            {                                
                                //是日期型
                                if (HSSFDateUtil.IsCellDateFormatted(cell))
                                {
                                    cellValue = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                //不是日期型
                                else
                                {
                                    cellValue = Convert.ToString(cell);
                                }
                            }
                            else
                            {
                                cellValue = Convert.ToString(cell);
                            }
                          
                            *****/
                            cellNo = cellNo + 1;
                            string cellValue = Convert.ToString(cell) + cellNo;

                            //   string cellValue = cell.StringCellValue;
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
                                        datastr = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                        dataRow[j] = datastr;
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).NumericCellValue.ToString();
                                    }
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString();
                                }
                                /**********/
                                string va = dataRow[j].ToString();
                                bool r = IsDateTime(va);
                                if (r)
                                {
                                    va = va.Replace(".", "-");
                                    va = va.Replace("/", "-");
                                    va = va.Replace("年", "-");
                                    va = va.Replace("月", "-");
                                    va = va.Replace("日", "");
                                    va = va.Replace("\\", "-");
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
       
    }
}
