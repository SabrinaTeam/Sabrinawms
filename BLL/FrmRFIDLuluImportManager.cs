using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FrmRFIDLuluImportManager
    {
        FrmRFIDLuluImportServer RfidServer = new FrmRFIDLuluImportServer();
        public List<DataTable> ExcelRead(String filename, string sheetname, int headno)
        {
            COMMON.NPOIExcelAsicsImport NPOIexcel = new COMMON.NPOIExcelAsicsImport();
            List<DataTable> ld = NPOIexcel.ExcelRead(filename, sheetname, headno);
            return ld;
        }
        public int uploadToMysql(DataTable dt)
        {

            return RfidServer.uploadToMysql(dt);
        }



        public bool IDisExisted(List<string> list, string id)
        {
            bool result = false;
            if (list.Count() <= 0)
            {
                result = false;
            }
            foreach (var l in list)
            {
                if (l == id)
                {
                    result = true;
                    break;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
