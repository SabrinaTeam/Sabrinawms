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
    public class Mes_SqlHelper
    {
        public static readonly string SAAconnStr = ConfigurationManager.ConnectionStrings["SAAMesSqlConnStr"].ConnectionString;
        public static readonly string TOPconnStr = ConfigurationManager.ConnectionStrings["TOPMesSqlConnStr"].ConnectionString;

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






        public static DataTable ExcuteTable(string sqlstr, string serviceName)
        {
            if (serviceName == "SAA")
            {
                serviceName = SAAconnStr;
            }
            else if (serviceName == "TOP")
            {
                serviceName = TOPconnStr;
            }

            using (SqlConnection conn = new SqlConnection(serviceName))
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

        public static DataTable ExcuteTable(string serviceName , string sqlstr, params SqlParameter[] ps)
        {

            if (serviceName == "SAA")
            {
                serviceName = SAAconnStr;
            }
            else if (serviceName == "TOP")
            {
                serviceName = TOPconnStr;
            }

            using (SqlConnection conn = new SqlConnection(serviceName))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 180;
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.AddRange(ps);
                        DataSet dataset = new DataSet();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataset);
                        return dataset.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    DataTable tb = new DataTable();
                    return tb;
                }
            }
        }
    }
}
