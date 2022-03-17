
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON
{
    public class NPOIExcelAsicsImport
    { //读EXCEL   导入EXCEL表
        public List<DataTable> ExcelRead(string file, string sheetname, int headno)
        {
            try
            {
                using (AsicsExcelHelper excelHelper = new AsicsExcelHelper(file))
                {
                    List<DataTable> ld = excelHelper.ExcelToDataTable(sheetname, headno, file);
                    return ld;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        private AsicsImport_Con_ppr ToModel_Con_ppr(DataRow row)//建立要导入的文件的model
        {
            AsicsImport_Con_ppr asicsImport_Con_ppr = new AsicsImport_Con_ppr();
           
            if (row.ItemArray.Length > 0)
            {
                asicsImport_Con_ppr.id = Convert.ToString(row.ItemArray[0].ToString());
                asicsImport_Con_ppr.Cust_id = Convert.ToString(row.ItemArray[0].ToString());
                asicsImport_Con_ppr.Serial_From = Convert.ToString(row.ItemArray[0].ToString());
                asicsImport_Con_ppr.qty = Convert.ToInt32(row.ItemArray[0].ToString());
                asicsImport_Con_ppr.org = Convert.ToString(row.ItemArray[0].ToString());
            }            

            return asicsImport_Con_ppr;
        }

        public string[] getExcelSheetSum(String filename)
        {
            ExcelHelper excelHelper = new ExcelHelper(filename);
            string[] sheetname = excelHelper.getExcelSheetName(filename);
            return sheetname;
        }
    }
}
