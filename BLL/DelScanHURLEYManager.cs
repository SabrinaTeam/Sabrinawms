using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DelScanHURLEYManager
    {
        DelScanHURLEYService dhs = new DelScanHURLEYService();
        public DataTable getDelByS(string starDate,string stopDate,string cust_id,string tagNumber,string org,int pageRows,int pages)
        {
          return  dhs.getDelByS(starDate, stopDate, cust_id, tagNumber,org, pageRows, pages);

        }
        public int delTNF_Hurley( List<string> ids)
        {
            return dhs.delTNF_Hurley(ids);

        }
        public DataTable getCustIDs()
        {
            return dhs.getCustIDs();

        }
        public DataTable getOrgs()
        {
            return dhs.getOrgs();

        }
    }
}
