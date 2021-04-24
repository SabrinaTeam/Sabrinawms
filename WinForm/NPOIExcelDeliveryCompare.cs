using COMMON;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinForm
{
    public class NPOIExcelDeliveryCompare
    {
        //写入EXCEL表  导出
        public void ExcelWrite(string file, DataTable tabl)
        {
            try
            {
                using (ExcelDeliveryCompare excelHelper = new ExcelDeliveryCompare(file))
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
