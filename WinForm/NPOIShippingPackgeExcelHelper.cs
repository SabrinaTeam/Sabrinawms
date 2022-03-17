using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMMON;

namespace WinForm
{
     public class NPOIShippingPackgeExcelHelper
    {  

        //写入EXCEL表  导出
        public void ExcelWrite(string file, DataTable tabl,string sheetname)
        {
            try
            {
                using (ShippingPackgeExcelHelper excelHelper = new ShippingPackgeExcelHelper(file))
                {
                    int count = excelHelper.DataTableToExcel(tabl, sheetname, true);
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
