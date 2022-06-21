using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public class FrmRFIDScanSearchManager
    {
        public FrmRFIDScanSearchServer rsss = new FrmRFIDScanSearchServer();

        public DataTable getRfidScanBox(List<string> parameters)
        {
            DataTable dt = new DataTable();
            if (parameters[0] == "0")
            {
                dt  = rsss.getRfidScanSingle(parameters);
            }
            else
            {
                 dt = rsss.getRfidScanBox(parameters);
            }

            if(dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dt;
            }

        }

        public DataTable getBoxScanDetailsByHeadID(string BoxheadID)
        {
            DataTable dt = rsss.getBoxScanDetailsByHeadID(BoxheadID);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dt;
            }

        }

    }
}
