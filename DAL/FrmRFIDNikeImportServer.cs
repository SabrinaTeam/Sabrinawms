using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public  class FrmRFIDNikeImportServer
    {
		public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
		public int uploadToMysql(DataTable dt)
		{
			string value = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				value = value + " ('" + dt.Rows[i]["CustID"].ToString() + "' , " +
								   "'" + dt.Rows[i]["SKU"].ToString() + "' , " +
								   "'" + dt.Rows[i]["ColorName"].ToString() + "' , " +
								   "'" + dt.Rows[i]["SizeName"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Style"].ToString() + "' , " +
								   "'" + dt.Rows[i]["PONumber"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Qtys"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Seanson"].ToString() + "' , " +
								   "'" + dt.Rows[i]["StyleColor"].ToString() + "' , " +
								   "'" + dt.Rows[i]["ColorCode"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Note"].ToString() + "' , " +

								   "'" + dt.Rows[i]["ORDERNO"].ToString() + "' , " +
								   "'" + dt.Rows[i]["CUST_TRACK"].ToString() + "' , " +
								   "'" + dt.Rows[i]["TOTAL_QTY"].ToString() + "' , " +
								   "'" + dt.Rows[i]["PACKAGING"].ToString() + "' , " +
								   "'" + dt.Rows[i]["RowsNO"].ToString() + "' , " +
								   "'" + dt.Rows[i]["COUNTRY_OF_ORIGIN"].ToString() + "' , " +
								   "'" + dt.Rows[i]["KRAFT_HANGTAG"].ToString() + "' , " +
								   "'" + dt.Rows[i]["FCT_CODE"].ToString() + "' ),";
			}
			value = value.Substring(0, value.Length - 1);
			string sql = @"insert into rfidtag 
									( CustID, SKU, ColorName, SizeName, Style, PONumber, Qtys, Seanson, 
									StyleColor, ColorCode, Note,ORDERNO, CUST_TRACK, TOTAL_QTY, PACKAGING,
									RowsNO,COUNTRY_OF_ORIGIN, KRAFT_HANGTAG ,FCT_CODE )  values " + value;

			string valueD = @" ON DUPLICATE KEY UPDATE									 
									`CustID`  = VALUES(CustID),
									`SKU`  = VALUES(SKU),
									`ColorName` = VALUES(ColorName),
									`SizeName` = VALUES(SizeName),
									`Style` = VALUES(Style),
									`PONumber` = VALUES(PONumber),
									`Qtys` = VALUES(Qtys),
									`Seanson` = VALUES(Seanson),
									`StyleColor` = VALUES(StyleColor),
									`ColorCode` = VALUES(ColorCode),
									`Note` = VALUES(Note) ,

									`ORDERNO` = VALUES(ORDERNO) ,
									`CUST_TRACK` = VALUES(CUST_TRACK) ,
									`TOTAL_QTY` = VALUES(TOTAL_QTY) ,
									`PACKAGING` = VALUES(PACKAGING) ,
									`RowsNO` = VALUES(RowsNO) ,
									`COUNTRY_OF_ORIGIN` = VALUES(COUNTRY_OF_ORIGIN) ,
									`KRAFT_HANGTAG` = VALUES(KRAFT_HANGTAG) ,
									`FCT_CODE` = VALUES(FCT_CODE) ";
			sql = sql + valueD + ";";
			int result = 0;
			if (MiddleWare == "1")
			{
				result = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
			}
			return result;
		}


	}
}
