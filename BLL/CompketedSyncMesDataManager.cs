using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   
    public class CompketedSyncMesDataManager
    {
        public CompketedSyncMesDataService csms = new CompketedSyncMesDataService();
        public DataTable getCompketedSyncDataErrors()
        {
            return csms.getCompketedSyncDataErrors();
        }
    }
}
