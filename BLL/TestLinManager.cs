using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class TestLinManager
    {
        private DAL.TestLinServer tserver = new DAL.TestLinServer();

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
            return tserver.TestConnection(host, port, millisecondsTimeout);
        }

        #endregion 采用Socket方式，测试服务器连接

        /// <summary>
        /// 数据库连接操作，可替换为你自己的程序
        /// </summary>
        /// <param name="ConnectionString">连接字符串</param>
        /// <returns></returns>
        public List<string> TestConnection(string serName)
        {
            return tserver.TestConnection(serName);
        }

        public bool LinServer(string strIP)
        {
            return tserver.LinServer(strIP);
        }
    }
}
