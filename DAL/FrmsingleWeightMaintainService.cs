using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FrmsingleWeightMaintainService
    {
        public DataTable getAllCustidByBaseDB()
        {
            string sql = @"select DISTINCT cust_abbr  from cust_dom;";
			DataTable dt = BEST_SqlHelper.ExcuteTable(sql);
			return dt;
		}
        public DataTable getAllStyleIDByBaseDB()
        {
            string sql = @" SELECT DISTINCT style_id ,style_name,cust_id from  style;";
            DataTable dt = BEST_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        public DataTable getAllSizeIDByBaseDB()
        {
            string sql = @"    SELECT DISTINCT  t_change from  size_sort	  ORDER BY t_change;";
            DataTable dt = BEST_SqlHelper.ExcuteTable(sql);
            return dt;
        }

    }
}
