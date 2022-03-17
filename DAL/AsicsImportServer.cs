using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AsicsImportServer
    {
		public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
		public int uploadToMysql(DataTable dt )
        {
			string value = "";
			for(int i=0;i< dt.Rows.Count; i++)
            {
				value = value +  " ('" + dt.Rows[i]["id"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Cust_id"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Serial_From"].ToString() + "' , " +
								   "'" + dt.Rows[i]["qty"].ToString() + "' , " +
								   "'" + dt.Rows[i]["org"].ToString() + "' , " +
								   "'" + dt.Rows[i]["PPrfNo"].ToString() + "' , " +
								   "'" + dt.Rows[i]["count1"].ToString() + "' , " +
								   "'" + dt.Rows[i]["create_pc"].ToString() + "' , " +
								   "'" + dt.Rows[i]["update_date"].ToString() + "' , " +
								   "'" + dt.Rows[i]["con_no"].ToString() + "' , " +
								   "'" + dt.Rows[i]["country_code"].ToString() + "' , " +
								   "'" + dt.Rows[i]["con_to"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Pkg_Code"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Scan_ID"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Net_Net"].ToString() + "' , " +
								   "'" + dt.Rows[i]["con_net"].ToString() + "' , " +
								   "'" + dt.Rows[i]["con_Gross"].ToString() + "' , " +
								   "'" + dt.Rows[i]["PO"].ToString() + "' , " +
								   "'" + dt.Rows[i]["MAIN_LINE"].ToString() + "' " +
								   " ),";
			}
			value = value.Substring(0, value.Length - 1);
			string sql = @"INSERT INTO `fsg`.`con_ppr`
									(`id`, `Cust_id`, `Serial_From`, `qty`, `org`, `PPrfNo`, `count1`, `create_pc`, `update_date`, 
									`con_no`, `country_code`, `con_to`, `Pkg_Code`, `Scan_ID`, `Net_Net`, `con_net`, `con_Gross`, 
									 `PO`, `MAIN_LINE`)
									VALUES "+ value  ;

			string valueD =  @" ON DUPLICATE KEY UPDATE
									`id` = VALUES(id),
									`Cust_id`  = VALUES(Cust_id),
									`Serial_From`  = VALUES(Serial_From),
									`qty` = VALUES(qty),
									`org` = VALUES(org),
									`PPrfNo` = VALUES(PPrfNo),
									`count1` = VALUES(count1),
									`create_pc` = VALUES(create_pc),
									`update_date` = VALUES(update_date),
									`con_no` = VALUES(con_no),
									`country_code` = VALUES(country_code),
									`con_to` = VALUES(con_to),
									`Pkg_Code` = VALUES(Pkg_Code),
									`Scan_ID` = VALUES(Scan_ID),
									`Net_Net` = VALUES(Net_Net),
									`con_net` = VALUES(con_net),
									`con_Gross` = VALUES(con_Gross),
									`PO` = VALUES(PO),
									`MAIN_LINE` = VALUES(MAIN_LINE) ";
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


		public int uploadCon_detailToMysql(DataTable dt)
		{
			string value = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				value = value + " ('" + dt.Rows[i]["id"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Cust_id"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Serial_From"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Buyer_Item"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Item_desc"].ToString() + "' , " +
								   "'" + dt.Rows[i]["color_code"].ToString() + "' , " +
								   "'" + dt.Rows[i]["Size1"].ToString() + "' , " +
								   "'" + dt.Rows[i]["con_Qty"].ToString() + "' , " +
								   "'" + dt.Rows[i]["qty"].ToString() + "' , " +
								   "'" + dt.Rows[i]["pprfno"].ToString() + "' " + 
								   " ),";
			} 
			value = value.Substring(0, value.Length - 1)  ;
			string sql = @"INSERT INTO `fsg`.`con_detail`
									(`id`, `Cust_id`, `Serial_From`, `Buyer_Item`, `Item_desc`, `color_code`,`Size1`, `con_Qty`, `qty`, `pprfno`)
									VALUES " + value;

			string valueD = @" ON DUPLICATE KEY UPDATE
									`id` = VALUES(id),
									`Cust_id`  = VALUES(Cust_id),
									`Serial_From`  = VALUES(Serial_From),
									`Buyer_Item` = VALUES(Buyer_Item),
									`Item_desc` = VALUES(Item_desc),
									`color_code` = VALUES(color_code),
									`Size1` = VALUES(Size1),
									`con_Qty` = VALUES(con_Qty),
									`qty` = VALUES(qty),
									`pprfno` = VALUES(pprfno) ";
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
