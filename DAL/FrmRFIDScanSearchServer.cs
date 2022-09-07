using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FrmRFIDScanSearchServer
    {
        public DataTable getRfidScanBox(List<string> parameters)
        {
			string sql = "";
			if (parameters[5] =="1")
            {
				sql = @"SELECT
								CustID,
								CartonNumber,
								Buyer_item,
								Color_code,
								Qty,
								Org,
								po,
								ScanTime,
								ScanHost 
							FROM
								rfidboxsscanheads 
							WHERE
								1 = 1 
								AND Buyer_item LIKE '%" + parameters[1] + @"%'
								AND Color_code LIKE '%" + parameters[2] + @"%'
								AND PO LIKE '%" + parameters[3] + @"%'
								AND ScanTime BETWEEN '" + parameters[6] + @"'
								AND '" + parameters[7] + @"'
							ORDER BY
								ORG,
								Buyer_item,
								Color_code,
								PO;";
            }
            else
            {
				sql = @"SELECT
								CustID,
								CartonNumber,
								Buyer_item,
								Color_code,
								Qty,
								Org,
								po,
								ScanTime,
								ScanHost 
							FROM
								rfidboxsscanheads 
							WHERE
								1 = 1 
								AND Buyer_item LIKE '%" + parameters[1] + @"%'
								AND Color_code LIKE '%" + parameters[2] + @"%'
								AND PO LIKE '%" + parameters[3] + @"%'

							ORDER BY
								ORG,
								Buyer_item,
								Color_code,
								PO;";
			}

			DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			return dt;
        }

		public DataTable getBoxScanDetailsByHeadID(string BoxheadID)
		{

				string sql = @"SELECT
									boxheadid,
									custid,
									CartonNumber,
									PolyBagNumber,
									RFIDNumber,
									WWMTNumber,
									Buyer_item,
									Color_code,
									Size1,
									Qty,
									org,
									po,
									ScanTime,
									ScanHost 
								FROM
									rfidboxsscandetails 
								WHERE
									boxheadid = '"+ BoxheadID + @"'
								ORDER BY
									ScanTime;";


			DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			return dt;
		}


		public DataTable getRfidScanSingle(List<string> parameters)
		{
			string sql = "";
			if (parameters[5] == "1")
			{
				sql = @"SELECT
							CustID,
							CartonNumber,
							PolyBagNumber,
							RFIDNumber,
							WWMTNumber,
							Buyer_item,
							Color_code,
							Size1,
							Qty,
							Org,
							po,
							ScanTime,
							ScanHost 
						FROM
							rfidscanlogs 
						WHERE
							1 = 1 
							AND Buyer_item LIKE '%" + parameters[1] + @"%'
							AND Color_code LIKE '%" + parameters[2] + @"%'
							AND PO LIKE '%" + parameters[3] + @"%'
							AND CartonNumber LIKE '%" + parameters[4] + @"%'
							AND ScanTime BETWEEN LIKE '%" + parameters[6] + @"%'
							AND '" + parameters[7] + @"';";
			}
			else
			{
				sql = @"SELECT
							CustID,
							CartonNumber,
							PolyBagNumber,
							RFIDNumber,
							WWMTNumber,
							Buyer_item,
							Color_code,
							Size1,
							Qty,
							Org,
							po,
							ScanTime,
							ScanHost 
						FROM
							rfidscanlogs 
						WHERE
							1 = 1 
							AND Buyer_item LIKE '%" + parameters[1] + @"%'
							AND Color_code LIKE '%" + parameters[2] + @"%'
							AND PO LIKE '%" + parameters[3] + @"%'
							AND CartonNumber LIKE '%" + parameters[4] + @"%' ";
			}

			DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			return dt;
		}
	}
}
