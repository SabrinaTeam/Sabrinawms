using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CompletedToMesService
    {
		public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
		public DataTable getTagInvoiceById(string Mid)
        {
            string sql = @"
							SELECT
									concat_ws(
										'-',
										ifNULL( s.tagScanPDAUUID, '0' ),
										concat(
											ifNULL((
												SELECT
												CASE
														LENGTH( tagScanDeptID ) 
														WHEN 1 THEN
														concat( '0', tagScanDeptID ) 
														ELSE
															tagScanDeptID
													END AS tagScanDeptID 
														
												FROM
													mesworktagscanreceipts 
												WHERE
													id =" + Mid + @"
													),
												'0' 
											),
											ifNULL( DATE_FORMAT( s.tagScanDateTime, '%Y%m%d' ), '0' ) 
										),
										ifNULL( `Version`, '0' ) 
									) AS tagInvoice,
									s.tagScanDeptID,
									s.Version,
									s.tagScanAccount,
									s.tagScanDateTime 
								FROM
									mesworktagscanreceipts s 
								WHERE
									s.id =" + Mid;
			DataTable result = new DataTable();
          
			if (MiddleWare == "1")
			{
				result = MyCatfsg_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			}

			return result;
		}

		public DataTable getMesworktagscansByinvoice(string tagInvoice)
		{
			string sql = @"
							SELECT
									id,
									isPrints,
									tagOrg,
									tagScanDeptID,
									tagLine,
									tagLocation,
									tagStyle,
									tagColor,
									tagSize,
									tagQty,
									tagScanAccount,
									tagNumber,
									tagInvoice,
									tagScanDateTime,
									tagUploadDateTime,
									tagScanPDASerial,
									isUploaded,
									isSyncMesData,
									isDels 
								FROM
									mesworktagscans 
								WHERE	
									isInOrOut = 1									
									and tagInvoice = '" + tagInvoice+ @"'
										ORDER BY    id";
			 
			DataTable result = new DataTable();

			if (MiddleWare == "1")
			{
				result = MyCatfsg_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			}

			return result;
		}



		public DataTable getMesworktagscansByinvoiceGroup(string tagInvoice)
		{
			string sql = @"
							SELECT
									
									s.tagInvoice,
									s.tagStyle,
									s.tagColor,
									SUBSTRING(s.tagNumber,-11,1) as part ,
									a.tagSize,
									a.tagQty,
									s.taglocation,
									d.DeptName,
									s.tagOrg,
									s.tagScanDeptID 
								FROM
									mesworktagscans s
									LEFT JOIN mesdepts d ON d.DeptNumber = s.tagScanDeptID 
									LEFT JOIN (
										SELECT
											group_concat( a.tagSize SEPARATOR ' , ' ) AS tagSize,
											group_concat( a.tagQty SEPARATOR ' , ' ) AS tagQty,
											tagInvoice 
										FROM
											(
											SELECT
												s.tagSize,
												sum( s.tagQty ) tagQty,
												s.tagInvoice 
											FROM
												mesworktagscans s 
											WHERE
												s.tagInvoice =  '" + tagInvoice + @"' 
											GROUP BY
												s.tagInvoice,
												s.tagInvoice,
												s.tagSize 
											ORDER BY
												s.tagInvoice,
												s.tagSize 
											) a 
										) a ON a.tagInvoice = s.tagInvoice 

								WHERE
									s.tagInvoice = '" + tagInvoice + @"' 
								GROUP BY
									s.tagOrg,
									s.tagInvoice,
									s.taglocation,
									d.DeptName,
									s.tagScanDeptID,
									s.tagStyle,
									s.tagColor,
									a.tagSize 
								ORDER BY
									s.tagOrg,
									s.tagScanDeptID,
									s.tagStyle,
									s.tagColor,
									a.tagSize ";
			 

			DataTable result = new DataTable();

			if (MiddleWare == "1")
			{
				result = MyCatfsg_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			}

			return result;
		}


		public DataTable getMesworktagscansByinvoiceDetail(string tagInvoice)
		{
			string sql = @"
							SELECT	s.Id,
									s.tagNumber ,
									s.tagScanDateTime ,
									s.tagStyle ,
									s.tagColor ,
									s.tagSize ,
									s.tagQty ,
									s.tagOrg ,
									d.DeptName ,
									s.tagScanAccount 
								FROM
									mesworktagscans s
									LEFT JOIN mesdepts d ON d.DeptNumber = s.tagScanDeptID 
								WHERE
									tagInvoice = '" + tagInvoice + @"'
										ORDER BY    id";
		 

			DataTable result = new DataTable();

			if (MiddleWare == "1")
			{
				result = MyCatfsg_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			}

			return result;
		}

		public DataTable updataMesworktagscansByinvoiceIsprint(string id)
		{
			string sql = @" UPDATE mesworktagscans set isPrints = 1 WHERE ID = '" + id + "'";
			DataTable result = new DataTable();

			if (MiddleWare == "1")
			{
				result = MyCatfsg_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			}

			return result; 
		}

		//getMesworktagscansByinvoice

		public DataTable getMesworktagscansSearch(string starDataTime, string stopDataTime, string receiptNumber,bool isCheckScanDate, string orgstr, string deptId) 
		{
			string wherestr = "";
			if (isCheckScanDate && receiptNumber.Length > 0)
			{
				wherestr = @" s.tagInvoice ='" + receiptNumber + @"'
								 and isInOrOut = 1
								 and tagScanDateTime BETWEEN '" + starDataTime + @"' and '" + stopDataTime + @"'
								 and tagOrg = '"+ orgstr + @"'
								 and tagScanDeptID = "+ deptId ;
				 

			}
			if(isCheckScanDate && receiptNumber.Length <= 0)
            {
				wherestr = @" s.tagInvoice !=''
								 and isInOrOut = 1
								 and tagScanDateTime BETWEEN '" + starDataTime + @"' and '" + stopDataTime + @"'
								 and tagOrg = '"+ orgstr + @"'
								 and tagScanDeptID = "+ deptId ;
			}
            if (!isCheckScanDate && receiptNumber.Length > 0)
            {
				wherestr = @" s.tagInvoice ='"+ receiptNumber + @"'
								 and isInOrOut = 1";
			}
			if (!isCheckScanDate && receiptNumber.Length <= 0)
			{
				wherestr = "";
			}
			string sql = @"
							 SELECT
									s.tagInvoice,
									s.tagStyle,
									s.tagColor,
									a.tagSize,
									sum( s.tagQty ) tagQty,
									s.taglocation,
									d.DeptName,
									s.tagOrg,
									s.tagScanDeptID 
								FROM
									mesworktagscans s
									LEFT JOIN mesdepts d ON d.DeptNumber = s.tagScanDeptID 
									LEFT JOIN (
											SELECT
													group_concat( tagSize SEPARATOR ',' ) AS tagSize,
													tagInvoice 
												FROM
													(
														SELECT DISTINCT
																	s.tagInvoice,
																	s.tagSize 
																FROM
																	mesworktagscans s 
																WHERE " + wherestr + @"
																GROUP BY
																	s.tagSize,
																	s.tagInvoice 
																ORDER BY
																	s.tagSize 
																) a 
															GROUP BY
																tagInvoice 
											) a ON a.tagInvoice = s.tagInvoice  

								WHERE " + wherestr + @"
								GROUP BY
									s.tagOrg,
									s.tagInvoice,
									s.taglocation,
									d.DeptName,
									s.tagScanDeptID,
									s.tagStyle,
									s.tagColor
								ORDER BY
									s.tagLocation,
									s.tagOrg,
									s.tagScanDeptID,
									s.tagStyle,
									s.tagColor";
			 
			 
			DataTable result = new DataTable();

			if (MiddleWare == "1")
			{
				result = MyCatfsg_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			}

			return result;
		}
	}
}
