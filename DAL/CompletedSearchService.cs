using MODEL;
using MySql.Data.MySqlClient;
using Pomelo.Data.MyCat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CompletedSearchService
    {
		public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
		public DataTable getLocation(string org)
        {
            string sql = @"SELECT
	                            tagLocation 
                            FROM
	                            mesworktagscans 
                            WHERE
	                            tagOrg = 'SAA' 
	                            AND tagLocation != '' 
	                            AND tagLocation IS NOT NULL 
                            GROUP BY
	                            tagLocation";

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

        public DataTable getMesWorktagScans(List<CompletedSearch> parameters)
        {
            string sql = @"SELECT
								s.ID,
								s.tagOrg,
								d.DeptName,
								s.tagLine,
								s.tagLocation,
								s.tagNumber,
								s.tagStyle,
								tagColor,
								s.tagSize,
								s.tagQty,
								s.tagScanAccount,
								s.tagScanDateTime,
								s.tagScanPDAUUID,
							CASE
									s.isInOrOut 
									WHEN 0 THEN
									'IN' ELSE 'OUT' 
								END AS isInOrOut,
								s.tagInvoice,
								s.tagReceiptNumber,
								s.isPrints 
							FROM
								mesworktagscans s
								LEFT JOIN mesdepts d ON s.tagScanDeptID = d.DeptNumber 
							WHERE 1 = 1   and
								if (@tagOrg = 'SAA',tagNumber like 'A%', 0 = 0 ) and
								if (@tagOrg = 'TOP',tagNumber like 'T%', 0 = 0 ) and
								if (@tagScanDeptID = '',0 = 0, tagScanDeptID = @tagScanDeptID ) and
								if (@tagLocation = '',0 = 0, tagLocation = @tagLocation ) and
								if (@isCheckDate = 'False',0 = 0, tagScanDateTime  BETWEEN @starDate AND  @stopDate) and
								if (@searchType = '0' , isInOrOut = @searchType ,0 = 0 ) and
								if (@searchType = '1',  isInOrOut = @searchType ,0 = 0) and
								if (@searchType = '-1', isDels != @searchType  and  isInOrOut = 0 , 0 = 0 ) and
								if (@searchType = ''  , 0 = 0, 0 = 0 ) 
								ORDER BY  Id";
			string tagOrg = "";
			string tagScanDeptID = "";
			string tagLocation = "";
			string isCheckDate = "";
			string starDate = "";
			string stopDate = "";
			string searchType = ""; 

			foreach (CompletedSearch item in parameters)
			{
				switch (item.key)
				{
					case "org":
						tagOrg = item.value;
						break;
					case "dept":
						tagScanDeptID = item.value;
						break;
					case "location":
						tagLocation = item.value;
						break;
					case "starDate":
						starDate = item.value;
						break;
					case "stopDate":
						stopDate = item.value;
						break;
					case "isCheckDate":
						isCheckDate = item.value;
						break;
					case "searchType":
						searchType = item.value;
						break;
				}
			}

			DataTable users = new DataTable();
			if (MiddleWare == "1")
			{

				MyCatParameter[] p = {
				new MyCatParameter("tagOrg", tagOrg),
				new MyCatParameter("tagScanDeptID", tagScanDeptID),
				new MyCatParameter("tagLocation", tagLocation),
				new MyCatParameter("isCheckDate", isCheckDate),
				new MyCatParameter("starDate", starDate),
				new MyCatParameter("stopDate", stopDate),
				new MyCatParameter("searchType", searchType)
			};
				users = MyCatfsg_SqlHelper.ExcuteTable(sql, p);
			}
			else
			{
				MySqlParameter[] p = {
				new MySqlParameter("tagOrg", tagOrg),
				new MySqlParameter("tagScanDeptID", tagScanDeptID),
				new MySqlParameter("tagLocation", tagLocation),
				new MySqlParameter("isCheckDate", isCheckDate),
				new MySqlParameter("starDate", starDate),
				new MySqlParameter("stopDate", stopDate),
				new MySqlParameter("searchType", searchType)
			};
				users = Mysqlfsg_SqlHelper.ExcuteTable(sql, p);
			}

			return users;
			 
		}
    }
}
