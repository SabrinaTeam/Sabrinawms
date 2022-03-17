using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CFoutputManager
    {
        CFOutputService cfs = new CFOutputService();
        public DataTable getCFoutPut(CFOutput cfoutput)
        {
           return cfs.getCFoutPut(cfoutput);
        }

        public DataTable getSubinv(string org,int searchType)
        {
            return cfs.getSubinv(org, searchType);
        }

    }
}
