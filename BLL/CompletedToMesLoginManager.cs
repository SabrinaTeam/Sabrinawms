using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CompletedToMesLoginManager
    {
        CompletedToMesLoginService ctml = new CompletedToMesLoginService();
        public DataTable getLoginByAccount(string account, string pwd)
        {
            DataTable dt = ctml.getLoginByAccount(account,pwd);
            return dt;
        }
    }
}
