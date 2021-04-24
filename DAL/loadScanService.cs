using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class loadScanService
    {
        public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;

        public int isHaveByTradingComanyPO(string Tpo)
        {
            string sql = @"SELECT GTN_PO FROM `gtn_po` WHERE GTN_PO ='" + Tpo + "'";
            DataTable result = new DataTable(); 
            if (MiddleWare == "1")
            {
                 result = MyCatfsg_SqlHelper.ExcuteTable(sql);             
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            }
            return result.Rows.Count;           
        }
        public int writeInvsToDb(DataTable dt)
        { 
            string sqlstr = "";
            string sqlValue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string kg = dt.Rows[i]["kg"].ToString();
                if(kg == "")
                {
                    kg = "null";
                } 
                sqlValue = sqlValue +
                           "select  \"" + dt.Rows[i]["TagNumber"].ToString() + "\"  AS TagNumber , \""
                               + dt.Rows[i]["Cust_id"].ToString() + "\"  AS Cust_id,\""
                               + dt.Rows[i]["Location"].ToString() + "\"  AS Location,\""
                               + dt.Rows[i]["update_date"].ToString() + "\" AS update_date,\""
                               + dt.Rows[i]["org"].ToString() + "\" AS org ,\""
                               + dt.Rows[i]["con_no"].ToString() + "\" AS con_no,\""
                               + dt.Rows[i]["create_pc"].ToString() + "\" AS create_pc,"
                               + kg + " AS kg,\""
                               + dt.Rows[i]["subinv"].ToString() + "\"  AS subinv,\""
                               + dt.Rows[i]["ScanTime"].ToString() + "\" AS ScanTime,\""
                               + dt.Rows[i]["exeStatus"].ToString() + "\"  AS exeStatus  UNION ALL   ";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 12) ;
            sqlstr = @"INSERT INTO inv ( TagNumber,Cust_id,Location,update_date,org,con_no,create_pc,kg,subinv,ScanTime,exeStatus ) SELECT
                            TagNumber,
                            Cust_id,
                            Location,
                            update_date,
                            org,
                            con_no,
                            create_pc,
                            kg,
                            subinv,
                            ScanTime,
                            exeStatus 
                            FROM
	                            (   " + sqlValue;

            string sqlw = @"
	                        FROM
	                        DUAL 
	                        ) AS q 
                        WHERE
	                        NOT EXISTS ( SELECT TagNumber FROM inv WHERE inv.TagNumber = q.TagNumber AND q.Location = ""HD"" );
                        ";
            sqlstr = sqlstr + sqlw;

            int result = 0; 
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
        }


        public int delDoubleRows()
        {
            string sql = @"
                            DELETE 
                            FROM
	                            GTN_PO 
                            WHERE
	                            id IN (
	                            SELECT
		                            a.id 
	                            FROM
		                            (
		                            SELECT
			                            id 
		                            FROM
			                            gtn_po a 
		                            WHERE
			                            ( a.po, a.GTN_PO ) IN ( SELECT po, gtn_po FROM gtn_po GROUP BY po, gtn_po HAVING count(*) > 1 ) 
		                            ) a 
	                            ) 
	                            AND ID NOT IN ( select b.id from ( SELECT max( ID ) id FROM GTN_PO GROUP BY po, gtn_po HAVING count(*)> 1 ) b )

                            ";
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
        public DataTable getSubinvByOrg(string org)
        {
            string sql = @"SELECT DISTINCT subinv from Location WHERE org='"+ org + "'";
            
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


        }

        public DataTable getLocationByOrgAndSubinv(string org, string subinv)
        {
            string sql = @"SELECT DISTINCT location from Location WHERE org='" + org + "'  AND Subinv = '" + subinv + "'"; 
           
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


        }
    }
}
