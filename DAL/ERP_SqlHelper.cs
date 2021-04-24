using MODEL;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{

    public class ERP_SqlHelper
    {
        public static readonly string ERPconnStr = ConfigurationManager.ConnectionStrings["ERPconnStr"].ConnectionString;
        //public static readonly string BESTconnStr = ConfigurationManager.ConnectionStrings["BESTconnStr"].ConnectionString;
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
        public static  DataTable ExcuteTable(string sqlstr )
        {

            OracleConnection conn = null;
            try
            {
                conn = OpenConn();
                var cmd = conn.CreateCommand();
                cmd.CommandText = sqlstr;
             //   cmd.Parameters.AddRange( parameters);
                cmd.CommandType = CommandType.Text;
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;
                var reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
              //  OracleDataReader dr = cmd.ExecuteReader();

                /*
                    adapter.Fill(dataset);
                  return dataset.Tables[0];
                  var reader = cmd.ExecuteReader();
                  while (reader.Read())
                      {
                         Console.WriteLine(string.Format("AwbPre:{0},AwbNo:{1}", reader["AwbPre"], reader["AwbNo"]));
                       }
                  return null;
                */

            }
            catch (Exception ex)
            {
              //  Console.WriteLine(ex.Message);
                DataTable tb = new DataTable();
                return tb;
            }
            finally
            {
                CloseConn(conn);
            }
            //  return null;
            /*
                    String ERPconnStr = @"Data Source =
                                       (DESCRIPTION =
                                       (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = topprod))
                                       (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.0.211)(PORT = 1521)));
                                            Persist Security Info = True;
                                            User ID = saa01;
                                            Password = saa01; ";
            */
        }
        public static OracleConnection OpenConn()
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ERPconnStr;
            conn.Open();
            return conn;
        }

        public static  void CloseConn(OracleConnection conn)
        {
            if (conn == null) { return; }
            try
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}
