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
    public class DelScanHURLEYService
    {
		public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
		public DataTable getDelByS(string starDate, string stopDate, string cust_id, string tagNumber,string org, int pageRows, int pages)
		{
			MyCatParameter[] mc = new MyCatParameter[7];
			MySqlParameter[] ms = new MySqlParameter[7];

			if (MiddleWare == "1")
			{
				mc[0] = new MyCatParameter("starDate", starDate);
				mc[1] = new MyCatParameter("stopDate", stopDate);
				mc[2] = new MyCatParameter("cust_id", cust_id);
				mc[3] = new MyCatParameter("tagNumber", tagNumber);
				mc[4] = new MyCatParameter("org", org);
				mc[5] = new MyCatParameter("pageRows", pageRows);
				mc[6] = new MyCatParameter("pages", pages);

			}
			else
			{
				ms[0] = new MySqlParameter("starDate", starDate);
				ms[1] = new MySqlParameter("stopDate", stopDate);
				ms[2] = new MySqlParameter("cust_id", cust_id);
				ms[3] = new MySqlParameter("tagNumber", tagNumber);
				ms[4] = new MySqlParameter("org", org);
				ms[5] = new MySqlParameter("pageRows", pageRows);
				ms[6] = new MySqlParameter("pages", pages);


			};
			//  account ,processID, userName 
			string sql = @"SELECT
								id,
								TagNumber,
								Cust_id,
								Location,
								org,
								scantime,
								con_no,
								create_pc 
							FROM
								inv 
							WHERE 1 = 1 and 								 
							IF	( @org != '', org = @org , 1 = 1 ) AND
                            IF	( @starDate != '', scantime BETWEEN  @starDate AND  @stopDate , 1 = 1 ) AND
							IF	(@cust_id != '', cust_id = @cust_id , 1 = 1 ) AND
							IF	(@tagNumber != '', tagNumber = @tagNumber , 1 = 1 ) 
                            ORDER BY
								scantime,
	         					tagnumber,
								cust_id,
								id
							LIMIT " + (pages - 1) * pageRows + @",
						" + pageRows + ";";
			DataTable dt = new DataTable();
			if (MiddleWare == "1")
				dt = MyCatfsg_SqlHelper.ExcuteTable(sql, mc);
			else
			{
				dt = Mysqlfsg_SqlHelper.ExcuteTable(sql, ms);
			};			 
			return dt;
		}

		public int delTNF_Hurley(List<string> ids)
		{
			string id = "";
            foreach (string i in ids)
            {
				id = id + "," + i  ;
			}
			id = id.Substring(1);

			string sql = @"	DELETE FROM  inv WHERE  ID IN("+ id + ");";
			int result = 0;
			if (MiddleWare == "1")
			{
				result = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
			}
			return result;

		}
		public DataTable getCustIDs()
		{
			string sql = @"	SELECT cust_id  from inv  GROUP BY cust_id;";
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

		public DataTable getOrgs()
		{
			string sql = @"SELECT org  from inv  GROUP BY org;";
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
