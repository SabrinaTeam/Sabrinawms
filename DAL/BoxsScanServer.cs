using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BoxsScanServer
    {

        public int SaveBoxDetailsLog(DataTable saveScanLog, int MaxRow)
        {
            if (saveScanLog.Rows.Count <= 0)
            {
                return 0;
            }
            string value = "";
            for (int i = 0; i < saveScanLog.Rows.Count; i++)
            {
                value = value + "( '" + MaxRow + "' ,"
                               + " '" + saveScanLog.Rows[i]["CustID"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["CartonNumber"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["PolyBagNumber"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["RFIDNumber"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["WWMTNumber"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Buyer_item"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Color_code"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Size1"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Qty"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Org"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["PO"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["ScanTime"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["ScanHost"] + "' ),";
            }
            value = value.Substring(0, value.Length - 1);
            string sql = @" insert into rfidboxsscandetails (BoxHeadID,CustID , CartonNumber, PolyBagNumber, RFIDNumber, WWMTNumber, Buyer_item, Color_code, Size1,
                              Qty, Org, PO, ScanTime, ScanHost)  values   " + value + ";";

            int insertBoxDetails = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);

            return insertBoxDetails;

        }

        public int SaveRFIDBoxScanLogs(DataTable BoxScanLog)
        {
            if (BoxScanLog.Rows.Count <= 0)
            {
                return 0;
            }
            string value = "";
            int rows = BoxScanLog.Rows.Count -1;
            for (int i = rows; i < BoxScanLog.Rows.Count; i++)
            {
                value = value + "( '" + BoxScanLog.Rows[i]["CustID"] + "' ,"
                               + " '" + BoxScanLog.Rows[i]["CartonNumber"] + "' ,"
                               + " '" + BoxScanLog.Rows[i]["Buyer_item"] + "' ,"
                               + " '" + BoxScanLog.Rows[i]["Color_code"] + "' ,"
                               + " '" + BoxScanLog.Rows[i]["Qty"] + "' ,"
                               + " '" + BoxScanLog.Rows[i]["Org"] + "' ,"
                               + " '" + BoxScanLog.Rows[i]["PO"] + "' ,"
                               + " '" + BoxScanLog.Rows[i]["ScanTime"] + "' ,"
                               + " '" + BoxScanLog.Rows[i]["ScanHost"] + "' ),";
            }
            value = value.Substring(0, value.Length - 1);
            string sql = @" insert into rfidboxsscanheads (CustID , CartonNumber, Buyer_item, Color_code, 
                              Qty, Org, PO, ScanTime, ScanHost)  values   " + value + @";";
            int insertBoxScan = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
            return insertBoxScan;

        }


        public int getRFIDBoxScanLogsMaxID( )
        {


            string sql = @" select id from rfidboxsscanheads  ORDER BY id DESC LIMIT 0,1 ;";

            DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            int MaxRos = -1;
            if(dt.Rows.Count > 0)
            {
                MaxRos =Convert.ToInt32( dt.Rows[0][0].ToString());
            }

            return MaxRos;

        }

    }
}
