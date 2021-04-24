using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
   public class Mysql_SqlHelper
    {
        // public static readonly string ERPconnStr = ConfigurationManager.ConnectionStrings["ERPconnStr"].ConnectionString;
        // public static readonly string BESTconnStr = ConfigurationManager.ConnectionStrings["BESTconnStr"].ConnectionString;
        public static readonly string MySqlconnStr = ConfigurationManager.ConnectionStrings["MySqlconnStr"].ConnectionString;

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
        public static int ExecuteNonQuery(string sqlstr)
        {

           MySqlConnection conn = null; 
            try
            {
                conn = OpenConn();
                var cmd = conn.CreateCommand();
                cmd.CommandText = sqlstr;
                cmd.CommandType = CommandType.Text; 
                int result = cmd.ExecuteNonQuery();

                CloseConn(conn);
                return result;
            }
            catch (Exception ex)
            {
                //  Console.WriteLine(ex.Message);               
                return -1;
            }
            finally
            {
                CloseConn(conn);
            }
           
        }
        public static MySqlConnection OpenConn()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = MySqlconnStr;
            conn.Open();
            return conn;
        }

        public static void CloseConn(MySqlConnection conn)
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

        public static   DataTable ExcuteTable(string sqlstr)
        {

            MySqlConnection conn = null;
            try
            {
                conn = OpenConn();
                var cmd = conn.CreateCommand();
                cmd.CommandText = sqlstr;
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
               var reader  = cmd.ExecuteReader();
                CloseConn(conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                
                return dt;
            }
            catch (Exception ex)
            {
                //  Console.WriteLine(ex.Message);   
                DataTable dt = new DataTable();
                return dt;
            }
            finally
            {
                CloseConn(conn);
            }

        }

       
    }
}
