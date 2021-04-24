using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CompketedSyncMesDataService
    {
		public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
		public DataTable getCompketedSyncDataErrors()
        {
			 

			string sql = @"SELECT
								s.id,
								s.tagOrg,
								d.DeptName,
								s.tagScanDeptID,
								s.tagScanAccount,
								s.tagLine,
								s.tagStyle,
								s.tagColor,
								s.tagSize,
								s.tagQty,
								s.tagLocation,
								s.tagNumber,
								s.tagScanDateTime,
								s.tagUploadDateTime,
								CASE s.isInOrOut 
								WHEN 0 THEN
									'入库'
								ELSE
									'出库'
							END as isInOrOut
							FROM
								mesworktagscans s
								LEFT JOIN mesdepts d ON s.tagScanDeptID = d.DeptNumber 
							WHERE
								s.isSyncMesData =0 
								and s.tagUploadDateTime< date_sub(now(), interval 1 hour)
								ORDER BY s.id,s.tagNumber";

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
