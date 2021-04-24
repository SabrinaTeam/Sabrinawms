using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace DAL
{
    public class TestLinServer
    {
       
        public static readonly string ERPconnStr = ConfigurationManager.ConnectionStrings["ERPconnStr"].ConnectionString;
        public static readonly string BESTconnStr = ConfigurationManager.ConnectionStrings["BESTconnStr"].ConnectionString;
        public static readonly string BESTconnStr_KM = ConfigurationManager.ConnectionStrings["BESTconnStr_KM"].ConnectionString;
        public static readonly string MySqlconnStr = ConfigurationManager.ConnectionStrings["MySqlconnStr"].ConnectionString;


        #region 采用Socket方式，测试服务器连接

        /// <summary>
        /// 采用Socket方式，测试服务器连接
        /// </summary>
        /// <param name="host">服务器主机名或IP</param>
        /// <param name="port">端口号</param>
        /// <param name="millisecondsTimeout">等待时间：毫秒</param>
        /// <returns></returns>
        public bool TestConnection(string host, int port, int millisecondsTimeout)
        {
            TcpClient client = new TcpClient();
            try
            {
                var ar = client.BeginConnect(host, port, null, null);
                ar.AsyncWaitHandle.WaitOne(millisecondsTimeout);
                return client.Connected;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                client.Close();
            }
        }

        #endregion 采用Socket方式，测试服务器连接

        /// <summary>
        /// 数据库连接操作，可替换为你自己的程序
        /// </summary>
        /// <param name="ConnectionString">连接字符串</param>
        /// <returns></returns>
        public List<string> TestConnection(string serName)
        {
            List<string> lists = new List<string>();
            switch (serName)
            {
                case "ERPconnStr":
                    try
                    {
                        string sql = "select TABLE_NAME from all_tab_comments where ROWNUM <20";
                        DataTable dt = ERP_SqlHelper.ExcuteTable(sql);
                        if (dt.Rows.Count <= 0)
                        {
                            lists.Add("连接数据库错误");
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lists.Add(dt.Rows[i]["TABLE_NAME"].ToString());
                            }
                        }

                        return lists;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());                     
                        lists.Add("连接数据库错误");
                        return lists;
                    }

                case "BESTconnStr":
                    try
                    {
                        string sql = "Select Name TABLE_NAME From Master..SysDatabases order By Name";
                        DataTable dt = BEST_SqlHelper.ExcuteTable(sql, serName);
                        if (dt.Rows.Count <= 0)
                        {
                            lists.Add("连接数据库错误");
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lists.Add(dt.Rows[i]["TABLE_NAME"].ToString());
                            }
                        }
                      
                        return lists;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                        lists.Add("连接数据库错误");
                        return lists;
                    }

                case "BESTconnStr_KM":
                    try
                    {
                        string sql = "Select Name TABLE_NAME From Master..SysDatabases order By Name";
                        DataTable dt = BEST_SqlHelper.ExcuteTable(sql, serName);
                        if (dt.Rows.Count <= 0)
                        {
                            lists.Add("连接数据库错误");
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lists.Add(dt.Rows[i]["TABLE_NAME"].ToString());
                            }
                        }

                        return lists;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                        lists.Add("连接数据库错误");
                        return lists;
                    }



                case "MySqlconnStr":

                    try
                    {
                        string sql = "SHOW TABLES; ";
                        DataTable dt = Mysql_SqlHelper.ExcuteTable(sql);
                        if (dt.Rows.Count <= 0)
                        {
                            lists.Add("连接数据库错误");
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lists.Add(dt.Rows[i]["TABLE_NAME"].ToString());
                            }
                        }

                        return lists;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                        lists.Add("连接数据库错误");
                        return lists;
                    }
                default:                    
                    lists.Add("未知错误");
                    return lists;
                    
            } 
        }

        public bool LinServer(string strIP)
        {
            if (TestConnection(strIP, 1433, 500))
            {
                ////连接服务器成功
                ////    MessageBox.Show("连接服务器成功！");
                //// 数据库操作，我这里用了连接测试。
                ////测试连接数据库
                //if (TestConnection(connstr))
                //{
                //    //连接数据库成功
                //    return true;
                //}
                //else
                //{
                //    //连接数据库失败
                //    return false;
                //}
                return true;
            }
            else
            {
                //   MessageBox.Show("连接服务器失败！");
                return false;
            }
        }
    }
}
