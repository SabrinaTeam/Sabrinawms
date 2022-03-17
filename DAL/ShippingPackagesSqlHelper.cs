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
    public class ShippingPackagesSqlHelper
    {

        public static readonly string SPSqlconnStr = ConfigurationManager.ConnectionStrings["SPSqlconnStr"].ConnectionString;
        
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

        public static DataTable ExcuteTable(string sqlstr, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(SPSqlconnStr))
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

        public static int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(SPSqlconnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}