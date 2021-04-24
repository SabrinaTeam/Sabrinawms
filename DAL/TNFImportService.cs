using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class TNFImportService
    {
        public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;

        public DataTable getPODataFromScanService(string startDate, string StopDate)
        {
            string sql = @" SELECT  id,Barcode,CartonNo,PackQty,Style,Size,Color,MasterPO,StyleDescription,Country FROM U_NGC   WHERE   AddDate BETWEEN ' " + startDate +"' AND ' "+ StopDate+"'";
            DataTable result = TNF_SqlHelper.ExcuteTable(sql);
            return result;
        }

        public DataTable getPODataFromScanService(string PONumber)
        {
            string sql = @" SELECT  id,Barcode,CartonNo,PackQty,Style,Size,Color,MasterPO,StyleDescription,Country FROM U_NGC   WHERE   MasterPO LIKE '" + PONumber + @"%'";
            DataTable result = TNF_SqlHelper.ExcuteTable(sql);
            return result;
        }
        public DataTable getPODataFromScanService( int Id )
        {
            string sql = @" SELECT  id,Barcode,CartonNo,PackQty,Style,Size,Color,MasterPO,StyleDescription,Country FROM U_NGC   WHERE       ID >  " + Id ;
            DataTable result = TNF_SqlHelper.ExcuteTable(sql);
            return result;
        }

        public int getTnfMaxId()
        {
            string sql = @"SELECT tnfDataId FROM tnfmaxid";
          //  DataTable result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            DataTable dt = new DataTable();
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sql);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            }
           // return dt;

            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }

        public void upTnfMaxId(int maxId)
        {
            string sql = @"UPDATE tnfmaxid set tnfDataId=" + maxId + " WHERE id=1";
            // DataTable result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            // DataTable dt = new DataTable();
            int result = 0;
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
            }
           // return result;

        } 

        public DataTable getTnfDataFromFsgByConpprIds(string ids)
        {

            string sql = @"SELECT
	                            id 
                            FROM
	                            con_ppr 
                            WHERE
	                            id IN ( "+ ids +")";
           // DataTable result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            DataTable dt = new DataTable();
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sql);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            }
            return dt;
           // return result;
        }
        public DataTable getTnfDataFromFsgByConDetailIds(string ids)
        {

            string sql = @"SELECT
	                            id 
                            FROM
	                            con_detail 
                            WHERE
	                            id IN ( " + ids + ")";
          //  DataTable result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            DataTable dt = new DataTable();
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sql);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            }
            return dt;
            //return result;
        }

        public int insetTnfDataToFsgConppr(DataTable dt)
        {
            string values = "";
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                values = values + "('" + dt.Rows[i]["id"] + "'," +
                                  "'" + dt.Rows[i]["Cust_id"] + "'," +
                                  "'" + dt.Rows[i]["Serial_From"] + "'," +
                                  "'" + dt.Rows[i]["qty"] + "'," +
                                  "'" + dt.Rows[i]["org"] + "'," +
                                  "'" + dt.Rows[i]["PPrfNo"] + "'," +
                                  "'" + dt.Rows[i]["count1"] + "'," +
                                  "'" + dt.Rows[i]["create_pc"] + "'," +
                                  "'" + dt.Rows[i]["update_date"] + "'," +
                                  "'" + dt.Rows[i]["con_no"] + "'," +
                                  "'" + dt.Rows[i]["country_code"] + "'," +
                                  "'" + dt.Rows[i]["con_to"] + "'," +
                                  "'" + dt.Rows[i]["Pkg_Code"] + "'," +
                                  "'" + dt.Rows[i]["Scan_ID"] + "'," +
                                  "'" + dt.Rows[i]["Net_Net"] + "'," +
                                  "'" + dt.Rows[i]["Con_Net"] + "'," +
                                  "'" + dt.Rows[i]["con_Gross"] + "'," +
                                  "'" + dt.Rows[i]["con_l"] + "'," +
                                  "'" + dt.Rows[i]["con_W"] + "'," +
                                  "'" + dt.Rows[i]["con_H"] + "'," +
                                  "'" + dt.Rows[i]["b_Volume"] + "'," +
                                  "'" + dt.Rows[i]["PO"] + "'," +
                                  "'" + dt.Rows[i]["MAIN_LINE"] + "'),";
            }
            values = values.Substring(0, values.Length  - 1);

            string sql = @"INSERT INTO con_ppr (
	                                            id,
	                                            Cust_id,
	                                            Serial_From,
	                                            qty,
	                                            org,
	                                            PPrfNo,
	                                            count1,
	                                            create_pc,
	                                            update_date,
	                                            con_no,
	                                            country_code,
	                                            con_to,
	                                            Pkg_Code,
	                                            Scan_ID,
	                                            Net_Net,
	                                            Con_Net,
	                                            con_Gross,
	                                            con_l,
	                                            con_W,
	                                            con_H,
	                                            b_Volume,
	                                            PO,
	                                            MAIN_LINE 
                                            )
                                            VALUES
	                                            "+ values + ";";
            // int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
            //   DataTable dt = new DataTable();
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
        public int UpdataTnfDataToFsgConppr(DataTable dt)
        {
            /*
              UPDATE recei
                        SET org = CASE id
                            WHEN 1 THEN 'SAA'
                            WHEN 2 THEN 'SAA'
                            WHEN 4 THEN 'SAA'
                        END,
		                     subinv = CASE id
                            WHEN 1 THEN 'S_HD'
                            WHEN 2 THEN 'S_HD'
                            WHEN 4 THEN 'S_HD'
                        END		
                    WHERE id IN (1,2,4)
             */
            string wherstr = "";
            string columnstr = "";
            string ids = "";           
            foreach (DataRow row in dt.Rows)
            {
                ids = ids + "'" + row["id"].ToString() + "',";
            }
            ids = ids.Substring(0, ids.Length - 1);

            for (int j = 1; j < dt.Columns.Count; j++)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    wherstr = wherstr + "  WHEN   '" + dt.Rows[i]["id"] + "'  THEN   '" + dt.Rows[i][j].ToString() + "'  ";

                }
                columnstr = columnstr + dt.Columns[j].ColumnName + "  =   CASE  id  " + wherstr + "  END,";
                wherstr = "";
            } 
            columnstr = columnstr.Substring(0, columnstr.Length - 1); 
            string sql = @"UPDATE con_ppr SET  " + columnstr + "   WHERE id IN(" + ids + ");";
          //  int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
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



        public int insetTnfDataToFsgConDetail(DataTable dt)
        {
            string values = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               
                values = values + "('" + dt.Rows[i]["id"] + "'," +
                                  "'" + dt.Rows[i]["Cust_id"] + "'," +
                                  "'" + dt.Rows[i]["Serial_From"] + "'," +
                                  "'" + dt.Rows[i]["Buyer_Item"] + "'," +
                                  "'" + dt.Rows[i]["Item_desc"] + "'," +
                                  "'" + dt.Rows[i]["color_code"] + "'," +
                                  "'" + dt.Rows[i]["Size1"] + "'," +
                                  "'" + dt.Rows[i]["con_Qty"] + "'," +
                                  "'" + dt.Rows[i]["qty"] + "'," +
                                  "'" + dt.Rows[i]["pprfno"] +"'),";
            }
            values = values.Substring(0, values.Length - 1);

            string sql = @"INSERT INTO con_detail (
	                                            id,
	                                            Cust_id,
	                                            Serial_From,
	                                            Buyer_Item,
	                                            Item_desc,
	                                            color_code,
	                                            Size1,
	                                            con_Qty,
	                                            qty,
	                                            pprfno 
                                            )
                                            VALUES
	                                            " + values + ";";
           // int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
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
        public int UpdataTnfDataToFsgConDetail(DataTable dt)
        { 
            string wherstr = "";
            string columnstr = "";
            string ids = "";
            // SELECT  id,Cust_id,Serial_From,Buyer_Item,Item_desc,color_code,Size1,con_Qty,qty,pprfno from con_detail  WHERE PPrfNo ='79795851196' 
            foreach (DataRow row in dt.Rows)
            {
                ids = ids + "'" + row["id"].ToString() + "',";
            }
            ids = ids.Substring(0, ids.Length - 1);

            for (int j = 1; j < dt.Columns.Count; j++)
            {
                if (dt.Columns[j].ColumnName == "con_Qty")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        wherstr = wherstr + "  WHEN   '" + dt.Rows[i]["id"] + "'  THEN   " + Convert.ToInt32( dt.Rows[i][j].ToString()) + "  ";

                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        wherstr = wherstr + "  WHEN   '" + dt.Rows[i]["id"] + "'  THEN   '" + dt.Rows[i][j].ToString() + "'  ";

                    }
                }
                
                columnstr = columnstr + dt.Columns[j].ColumnName + "  =   CASE  id  " + wherstr + "  END,";
                wherstr = "";
            }
            columnstr = columnstr.Substring(0, columnstr.Length - 1);
            string sql = @"UPDATE con_detail SET  " + columnstr + "   WHERE id IN(" + ids + ");";
          //  int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
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
