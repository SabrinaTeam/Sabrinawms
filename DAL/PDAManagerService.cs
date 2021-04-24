using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PDAManagerService
    {
        public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;

        public int writePMToData(DataTable dt)
        {
            string sqlValue = "";
            // devUUID, devNumber, buyDate, devName, devMode, userDept, userDate, userName, mark
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sqlValue = sqlValue +
                           "(\""   + dt.Rows[i]["devUUID"].ToString() + "\",\""
                                   + dt.Rows[i]["devNumber"].ToString() + "\",\""
                                   + dt.Rows[i]["buyDate"].ToString() + "\",\""
                                   + dt.Rows[i]["devName"].ToString() + "\",\""
                                   + dt.Rows[i]["devMode"].ToString() + "\",\""
                                   + dt.Rows[i]["userDept"].ToString() + "\",\""
                                   + dt.Rows[i]["userDate"].ToString() + "\",\""
                                   + dt.Rows[i]["userName"].ToString() + "\",\""
                                   + dt.Rows[i]["mark"].ToString() + "\" ),";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) + ";";
            string sqlstr = @"INSERT INTO pdamanager (devUUID, devNumber, buyDate, devName, devMode, userDept, userDate, userName, mark )  VALUES " + sqlValue;

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


        public int updatePMToData(DataTable dt)
        {
            string sqlWHEN = "";
            string sqlID = "";
            string sqlCASE = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string value = dt.Rows[j][i].ToString();
                    if (i == 1)
                    {
                        sqlID = sqlID + dt.Rows[j]["ID"].ToString() + ",";
                    }
                    if (value.Length <= 0)
                    {
                        value = "";
                    }
                    sqlWHEN = sqlWHEN +
                        "  WHEN  " + dt.Rows[j]["ID"].ToString() + "  THEN  \"" + value + "\"";
                }
                sqlCASE = sqlCASE +
                    "  " + dt.Columns[i].ToString() + " = CASE id " + sqlWHEN + " END ,";
                sqlWHEN = "";

            }

            sqlCASE = sqlCASE.Substring(0, sqlCASE.Length - 1);
            sqlID = sqlID.Substring(0, sqlID.Length - 1);
            string sqlstr = @"UPDATE pdamanager SET " + sqlCASE + " WHERE id IN (" + sqlID + ")";            
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

        public DataTable searchPDABuyUUID(List<string> devs, bool selected)
        {
            string sqlstr = "";
            if (selected)
            {
                 sqlstr = @" SELECT
	                                ID,devUUID, devNumber, buyDate, devName, devMode, userDept, userDate, userName, mark
                                FROM
	                                pdamanager  ";
            }
            else
            {
                sqlstr = @" SELECT
	                                ID,devUUID, devNumber, buyDate, devName, devMode, userDept, userDate, userName, mark
                                FROM
	                                pdamanager 
                                WHERE
	                                devUUID LIKE '%" + devs[0] + @"%' 
	                                AND devNumber LIKE '%" + devs[1] + @"%' 	                               
	                                AND devName LIKE '%" + devs[2] + @"%' 
	                                AND devMode LIKE '%" + devs[3] + @"%' 	
                                    AND userDept LIKE '%" + devs[4] + @"%'
	                                AND userName LIKE '%" + devs[5] + @"%' 
	                                AND mark LIKE '%" + devs[6] + @"%'  ";
            }
            

            
            DataTable dt = new DataTable();
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            return dt;

        }

    }
}
