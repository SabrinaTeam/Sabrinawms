using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace COMMON
{
    public class NPOIExcelScan
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
        public Scans[] ExcelRead(string file, string sheetname, int headno)
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
                        Scans[] scan = new Scans[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            scan[i] = ToModel(dt.Rows[i]);//这里转换过来
                        }
                        return scan; 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        private Scans ToModel(DataRow row)//建立要导入的文件的model
        { 
        Scans scan = new Scans(); 
            if (row.ItemArray.Length > 0)
            {
                scan.TagNumber = Convert.ToString(row.ItemArray[0].ToString());
            }

            if (row.ItemArray.Length > 1)
            {
                scan.ScanTime = Convert.ToString(row.ItemArray[1].ToString());
            }
            if (row.ItemArray.Length > 2)
            {
                scan.Kg = Convert.ToString(row.ItemArray[2].ToString());
            }

            if (row.ItemArray.Length > 3)
            {
                scan.Subinv = Convert.ToString(row.ItemArray[3].ToString());
            }
            if (row.ItemArray.Length > 4)
            {
                scan.Con_no = Convert.ToString(row.ItemArray[4].ToString());
            }
            if (row.ItemArray.Length > 5)
            {
                scan.Location = Convert.ToString(row.ItemArray[5].ToString());
            }
            if (row.ItemArray.Length > 6)
            {
                scan.Org = Convert.ToString(row.ItemArray[6].ToString());
            }
            if (row.ItemArray.Length > 6)
            {
                scan.Cust_Id = Convert.ToString(row.ItemArray[6].ToString());
            }
            return scan;
        }

        public string[] getExcelSheetSum(String filename)
        {
            ExcelHelper excelHelper = new ExcelHelper(filename);
            string[] sheetname = excelHelper.getExcelSheetName(filename);
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
