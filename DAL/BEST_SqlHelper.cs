using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DAL
{
    public class BEST_SqlHelper
    {


        // public static readonly string ERPconnStr = ConfigurationManager.ConnectionStrings["ERPconnStr"].ConnectionString;
        public static readonly string BESTconnStr = ConfigurationManager.ConnectionStrings["BESTconnStr"].ConnectionString;
        public static readonly string BESTconnStr_KM = ConfigurationManager.ConnectionStrings["BESTconnStr_KM"].ConnectionString;
        // public static readonly string MySqlconnStr = ConfigurationManager.ConnectionStrings["MySqlconnStr"].ConnectionString;

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
            if(serviceName == "BESTconnStr")
            {
                serviceName =BESTconnStr;
            }else
            {
                serviceName =BESTconnStr_KM;
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

        public static DataTable ExcuteTable(string sqlstr, params SqlParameter[] ps   )
        {

            using (SqlConnection conn = new SqlConnection(BESTconnStr))
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
