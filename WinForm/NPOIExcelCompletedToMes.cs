using COMMON;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    public class NPOIExcelCompletedToMes
    {
        public void ExcelWrite(string file, DataTable tabl, string tableName)
        {
            try
            {

                using (ExcelCompletedToMes excelHelper = new ExcelCompletedToMes(file))
                {
                    int count = excelHelper.DataTableToExcel(tabl, "MySheet", true, tableName);
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
