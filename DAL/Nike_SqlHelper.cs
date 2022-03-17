using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Nike_SqlHelper
    {
        public static readonly string TOP_MercuryConnStr = ConfigurationManager.ConnectionStrings["TOP_MercurySQLconnstr"].ConnectionString;
        public static readonly string SAA_MercuryConnStr = ConfigurationManager.ConnectionStrings["SAA_MercurySQLconnstr"].ConnectionString;

        public static Object ToDbValue(Object value)
        {
            if (value == null)
            { return DBNull.Value; }
            else
            {
                return value;
            }
        }

        public static Object FromDbValue(Object value)
        {
            if (value == DBNull.Value)
            { return null; }
            else
            {
                return value;
            }
        }

        public static DataTable ExcuteTable(string org, string sqlstr)
        { 
            if (org == "SAA")
            {
                using (SqlConnection conn = new SqlConnection(SAA_MercuryConnStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = sqlstr;
                        DataSet dataset = new DataSet();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataset);
                        return dataset.Tables[0];
                    }

                }
            }
            else if (org == "TOP")
            {
                using (SqlConnection conn = new SqlConnection(TOP_MercuryConnStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = sqlstr;
                        DataSet dataset = new DataSet();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataset);
                        return dataset.Tables[0];
                    }
                }
            }
            else
            {
                DataTable dt = new DataTable();
                return dt; 
            }
        }
    }
}
