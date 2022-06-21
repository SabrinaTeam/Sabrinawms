using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BoxsScanManager
    {
        public BoxsScanServer bss = new BoxsScanServer();

        public int SaveRFIDBoxScanLogs(DataTable dt)
        {
            return this.bss.SaveRFIDBoxScanLogs(dt);

        }
        public int SaveBoxDetailsLog(DataTable dt, int MaxRow)
        {
            return this.bss.SaveBoxDetailsLog(dt, MaxRow);

        }

        public int getRFIDBoxScanLogsMaxID( )
        {
            return this.bss.getRFIDBoxScanLogsMaxID();

        }

    }
}
