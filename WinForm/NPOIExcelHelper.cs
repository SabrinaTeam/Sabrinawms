using COMMON;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinForm
{
     public class NPOIExcelHelper
    {
        //static void PrintData(DataTable data)
        //{
        //    if (data == null) return;
        //    for (int i = 0; i < data.Rows.Count; ++i)
        //    {
        //        for (int j = 0; j < data.Columns.Count; ++j)
        //            Console.Write("{0} ", data.Rows[i][j]);
        //        Console.Write("\n");
        //    }
        //}

        //写入EXCEL表  导出
        public void ExcelWrite(string file, DataTable tabl)
        {
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper(file))
                {
                    int count = excelHelper.DataTableToExcel(tabl, "MySheet", true);
                    if (count > 0)
                        Console.WriteLine("Number of imported data is {0} ", count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

         
    }
}
