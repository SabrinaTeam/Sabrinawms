using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Pomelo.Data.MyCat;

namespace DAL
{
    class MyCatfsg_SqlHelper
    {
        // public static readonly string ERPconnStr = ConfigurationManager.ConnectionStrings["ERPconnStr"].ConnectionString;
        // public static readonly string BESTconnStr = ConfigurationManager.ConnectionStrings["BESTconnStr"].ConnectionString;
        public static readonly string MyCatconnStr_fsg = ConfigurationManager.ConnectionStrings["MyCatconnStr_fsg"].ConnectionString;

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

            MyCatConnection conn = null;
            try
            {
                conn = OpenConn();
                var cmd = conn.CreateCommand();
                cmd.CommandTimeout = 300;
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

       
        
        public static MyCatConnection OpenConn()
        {

            MyCatConnection conn = new MyCatConnection(); 
            conn.ConnectionString = MyCatconnStr_fsg;
            conn.Open();
            return conn;
        }

        public static void CloseConn(MyCatConnection conn)
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
        

        

        public static DataTable ExcuteTable(string sqlstr)
        {
            MyCatConnection conn = null;
            try
            {

                /*
                 * cmd.CommandTimeout = 180;
                 
                cmd.CommandText = sql;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                return dataset.Tables[0];
                */



                conn = OpenConn();
                var cmd = conn.CreateCommand();
                 cmd.CommandTimeout = 180;
                cmd.CommandText = sqlstr;
                cmd.CommandType = CommandType.Text;
                DataSet dataset = new DataSet();
                MyCatDataAdapter da = new MyCatDataAdapter();
                da.SelectCommand = cmd;
               // var reader = cmd.ExecuteReader();
             //   DataTable dt = new DataTable();
              //  da.Fill(dt);
              //  CloseConn(conn);
              da.Fill(dataset);
              return dataset.Tables[0];
             //   return dt;

              
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

        


        public static int ExecuteNonQuery(string sqlstr, MyCatParameter[] parameters)
        {

            MyCatConnection conn = null;
            try
            {
                conn = OpenConn();
                var cmd = conn.CreateCommand();
                cmd.CommandTimeout = 0;
                cmd.CommandText = sqlstr;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(parameters);
                MyCatDataAdapter da = new MyCatDataAdapter();
                da.SelectCommand = cmd;
                // var reader = cmd.ExecuteReader();
                int execute = cmd.ExecuteNonQuery();
                CloseConn(conn);
                //  DataTable dt = new DataTable();
                // da.Fill(dt);
                return execute;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                CloseConn(conn);
            }

        }

 
        public static DataTable ExcuteTable(string sqlstr, MyCatParameter[] parameters)
        {

            MyCatConnection conn = null;
            try
            {
                conn = OpenConn();
                var cmd = conn.CreateCommand();
                cmd.CommandTimeout = 0;
                cmd.CommandText = sqlstr;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(parameters);
                DataSet dataset = new DataSet();
                MyCatDataAdapter da = new MyCatDataAdapter();
                da.SelectCommand = cmd;
            //   var reader = cmd.ExecuteReader();
                // int execute = cmd.ExecuteNonQuery();
              //  CloseConn(conn);
             //   DataTable dt = new DataTable();
             //   da.Fill(dt);
             //   return dt;


               
             //   MyCatDataAdapter da = new MyCatDataAdapter();
             //   da.SelectCommand = cmd;
                // var reader = cmd.ExecuteReader();
                //   DataTable dt = new DataTable();
                //  da.Fill(dt);
                //  CloseConn(conn);
                da.Fill(dataset);
                return dataset.Tables[0];


            }
            catch (Exception ex)
            {
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
