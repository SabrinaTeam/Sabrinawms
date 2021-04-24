using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace COMMON
{
   public class NPOIExcelExample
    {
        //public void ExcelWrite(string file)
        //{
        //    try
        //    {
        //        using (ExcelHelper excelHelper = new ExcelHelper(file))
        //        {
        //            DataTable data = GenerateData();
        //            deptBLL getdataDept = new deptBLL();    //写一个从数据库查出来的表

        //            DataTable data = getdataDept.GetdatagetdataDept();
        //            int count = excelHelper.DataTableToExcel(data, "MySheet", true);
        //            if (count > 0)
        //                Console.WriteLine("Number of imported data is {0} ", count);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception: " + ex.Message);
        //    }
        //}

        //读EXCEL   导入EXCEL表
        public tradingComanyPO[] ExcelRead(string file, string sheetname, int headno)
        {
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper(file))
                {
                    DataTable dt = excelHelper.ExcelToDataTable(sheetname, headno);
                    if (dt.Rows.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        //要显示的文件的model 例emp
                        tradingComanyPO[] gtnPOS = new tradingComanyPO[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gtnPOS[i] = ToModel(dt.Rows[i]);//这里转换过来
                        }
                        return gtnPOS;
                        //InterBoxLableWind IExcel = new InterBoxLableWind();
                        // IExcel.dvgInnerBox.ItemsSource = InnerBoxs;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        private tradingComanyPO ToModel(DataRow row)//建立要导入的文件的model
        {
            tradingComanyPO gtnPO = new tradingComanyPO();

            /*
            if (row.ItemArray.Length > 0)
            {
                gtnPO.id = Convert.ToInt32( row.ItemArray[0].ToString());
            }
            */
            
            if (row.ItemArray.Length > 0)
            {
                gtnPO.PO = Convert.ToString(row.ItemArray[0].ToString());
            }

            if (row.ItemArray.Length > 1)
            {
                gtnPO.GTN_PO = Convert.ToString( row.ItemArray[1].ToString());
            }
            if (row.ItemArray.Length > 2)
            {
                gtnPO.fCreate_Date = Convert.ToString( row.ItemArray[2].ToString());
            }

            if (row.ItemArray.Length > 3)
            {
                gtnPO.fIssue_Date =Convert.ToString( row.ItemArray[3].ToString());
            }
            if (row.ItemArray.Length > 4)
            {
                gtnPO.fOrder_Status = Convert.ToString( row.ItemArray[4].ToString());
            }
            if (row.ItemArray.Length > 5)
            {
                gtnPO.fOrder_Total_Qty = Convert.ToString(row.ItemArray[5].ToString());
            }
            if (row.ItemArray.Length > 6)
            {
                gtnPO.fInvoiced_Item_Qty = Convert.ToString(row.ItemArray[6].ToString());
            }
            return gtnPO;
        }

        public string[] getExcelSheetSum(String filename)
        {
            ExcelHelper excelHelper = new ExcelHelper(filename);
            string[] sheetname = excelHelper.getExcelSheetName(filename);
            // string[] sheetname = new string[k];//表名
            for (int t = 0; t < sheetname.Count(); t++)
            {
                // sheetname[t] = excelHelper.getExcelSheetName(t);
                // sheetname[t] = workbook.GetSheetName(t);
            }

            return sheetname;
        }

        //public string getExcelSheetName(String filename,int sheetNo)
        //{
        // //   ExcelHelper excelHelper = new ExcelHelper(filename);
        //   // return excelHelper.getExcelSheetName(sheetNo);
        //}
    }
}
