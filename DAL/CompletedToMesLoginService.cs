using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CompletedToMesLoginService
    {
		public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
		public DataTable  getLoginByAccount(string account, string pwd)
        {
            string sql = @"SELECT
								u.id,
								u.UserName,
								u.deptID,
								u.marsk,
								d.DeptName,
								d.DeptNumber 
							FROM
								mesusers u
								LEFT JOIN mesdepts d ON d.DeptNumber = u.deptID 
							WHERE
								u.account = '" + account + @"' 
								AND u.PASSWORD = '"+ pwd + "'";
			DataTable dt = new DataTable();
			if (MiddleWare == "1")
			{
				dt = MyCatfsg_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			}

			return dt;
		 
		}
    }
}
