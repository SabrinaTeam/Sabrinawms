using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class NikeImportService
	{
		public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
	   // public static readonly string MiddleWare="0";
		public int getNikeMaxId()
		{
			string sql = @"SELECT nikedataid FROM nikemaxid";
			//  DataTable result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			DataTable dt = new DataTable();
			if (MiddleWare == "1")
			{
				dt = MyCatfsg_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
			}
			// return dt;

			return Convert.ToInt32(dt.Rows[0][0].ToString());
		}

		public DataTable getPODataFromScanService(string org,string PONumber, string main_line)
		{
			string sql1 = @"SELECT 
								   ds.EBELN,
								   ds.PO_REF,
								   ds.FFS_CRTN_QTY,
								   counts.countQty,
								   ds.TKNUM,
								   ds.EBELP,
								   ds.VENUM,
								   ds.ETENR,
								   ds.J_3ASIZE,
								   ds.MENGE,
								   ds.EAN11,
								   ds.J_4KSCAT,
								   ds.EXIDV,
								   ds.Pkg_Code,
								   ds.BRGEW,
								   ds.NTGEW,
								   ds.MATNR,
								   ds.PPrfNo,
								   ds.FFS_LENGTH_OUTER,
								   ds.FFS_WIDTH_OUTER,
								   ds.FFS_HEIGHT_OUTER,
								   ds.KUNNR
							FROM
							(
								SELECT e.BEDAT,
									   e.EBELN,
									   CASE
										   WHEN
										   (
											   e.PO_REF = ''
											   OR e.PO_REF IS NULL
										   ) THEN
											   e.EBELN
										   ELSE
											   e.PO_REF
									   END PO_REF,
									   v.FFS_CRTN_QTY,
									   v.TKNUM,
									   v.EBELP,
									   v.VENUM,
									   EK.ETENR,
									   EK.J_3ASIZE,
									   EK.MENGE,
									   likp.kunnr EAN11,
									   EK.J_4KSCAT,
									   k.EXIDV,
									   k.FFS_CRTN_TYP Pkg_Code,
									   CASE
										   WHEN (k.BRGEW IS NULL) THEN
											   '0'
										   ELSE
											   k.BRGEW
									   END BRGEW,
									   k.NTGEW,
									   CASE
										   WHEN
										   (
											   l.MATNR = ''
											   OR l.MATNR IS NULL
										   ) THEN
												mz.MATNR
										   ELSE
											   l.MATNR
									   END MATNR,
									   CASE
										   WHEN
										   (
											   l.VERUR = ''
											   OR l.VERUR IS NULL
										   ) THEN
											   EK.EAN11
										   ELSE
											   l.VERUR
									   END PPrfNo,
									   f.FFS_LENGTH_OUTER,
									   f.FFS_WIDTH_OUTER,
									   f.FFS_HEIGHT_OUTER,
									   likp.KUNNR
								FROM EKKO e
									LEFT JOIN VEPO v
										ON e.EBELN = v.EBELN
									LEFT JOIN EKET EK
										ON EK.EBELN = v.EBELN
										   AND EK.EBELP = v.EBELP
										   AND EK.ETENR = v.ETENR
									LEFT JOIN MARASZ mz on '00'+ek.EAN11   =  mz.UPC
									LEFT JOIN VEKP k
										ON v.VENUM = k.VENUM
										   AND
										   (
											   v.TKNUM = k.TKNUM
											   OR
											   (
												   v.TKNUM IS NULL
												   AND k.TKNUM IS NULL
											   )
										   )
									LEFT JOIN
									(
										SELECT VBELN,
											   VGPOS,
											   MAX(BookingRequestID) MaxBookingRequestID,
											   TKNUM maxTKNUM,
											   MATNR,
											   MAX(VERUR) VERUR,
											   MAX(AEDAT) AEDAT
										FROM LIPS
										GROUP BY VBELN,TKNUM,
												 VGPOS,
												 MATNR
									) l
										ON l.VBELN = e.EBELN
										   AND l.VGPOS = v.EBELP
											AND l.maxTKNUM = k.TKNUM
									LEFT JOIN FFSCRTN f
										ON f.FFS_CRTN_TYP = k.FFS_CRTN_TYP
									LEFT JOIN LIKP likp
										ON likp.VBELN = v.VBELN
										   AND likp.TKNUM = v.TKNUM                            
								WHERE  v.EBELP like '%" + main_line + @"%'
									  AND  ( e.EBELN IN ( '" + PONumber + @"'  ) OR e.PO_REF IN ( '" + PONumber + @"'  ) )
									  AND v.FFS_CRTN_QTY IS NOT NULL
									  AND v.FFS_CRTN_QTY > 0
									  AND ( l.MATNR IS NOT NULL  OR  l.MATNR !=''  OR mz.MATNR = ''   OR mz.MATNR   IS NOT NULL  )
							) ds
								JOIN
								(
									SELECT SUM(FFS_CRTN_QTY) countQty,
										   BEDAT,
										   EBELN,
										   PO_REF,
										   TKNUM,
										   EBELP,
										   VENUM,
										   J_4KSCAT,
										   Pkg_Code,
										   BRGEW,
										   NTGEW,
										   MATNR,
										   PPrfNo,
										   FFS_LENGTH_OUTER,
										   FFS_WIDTH_OUTER,
										   FFS_HEIGHT_OUTER,
										   KUNNR
									FROM
									(
										SELECT e.BEDAT,
											   e.EBELN,
											   CASE
												   WHEN
												   (
													   e.PO_REF = ''
													   OR e.PO_REF IS NULL
												   ) THEN
													   e.EBELN
												   ELSE
													   e.PO_REF
											   END PO_REF,
											   v.FFS_CRTN_QTY,
											   v.TKNUM,
											   v.EBELP,
											   v.VENUM,
											   EK.ETENR,
											   EK.J_3ASIZE,
											   EK.MENGE,
											   likp.kunnr EAN11,
											   EK.J_4KSCAT,
											   k.EXIDV,
											   k.FFS_CRTN_TYP Pkg_Code,
											   CASE
												   WHEN (k.BRGEW IS NULL) THEN
													   '0'
												   ELSE
													   k.BRGEW
											   END BRGEW,
											   k.NTGEW,
											   CASE
												   WHEN
												   (
													   l.MATNR = ''
													   OR l.MATNR IS NULL
												   ) THEN
													   mz.MATNR
												   ELSE
													   l.MATNR
											   END MATNR,
											   CASE
												   WHEN
												   (
													   l.VERUR = ''
													   OR l.VERUR IS NULL
												   ) THEN
													   EK.EAN11
												   ELSE
													   l.VERUR
											   END PPrfNo,
											   f.FFS_LENGTH_OUTER,
											   f.FFS_WIDTH_OUTER,
											   f.FFS_HEIGHT_OUTER,
											   likp.KUNNR
										FROM EKKO e
											LEFT JOIN VEPO v
												ON e.EBELN = v.EBELN
											LEFT JOIN EKET EK
												ON EK.EBELN = v.EBELN
												   AND EK.EBELP = v.EBELP
												   AND EK.ETENR = v.ETENR
											LEFT JOIN MARASZ mz on '00'+ek.EAN11   =  mz.UPC
											LEFT JOIN VEKP k
												ON v.VENUM = k.VENUM
												   AND
												   (
													   v.TKNUM = k.TKNUM
													   OR
													   (
														   v.TKNUM IS NULL
														   AND k.TKNUM IS NULL
													   )
												   )
											LEFT JOIN
											(
												SELECT VBELN,
													   VGPOS,
													   MAX(BookingRequestID) MaxBookingRequestID,
													   TKNUM maxTKNUM,
													   MATNR,
													   MAX(VERUR) VERUR,
													   MAX(AEDAT) AEDAT
												FROM LIPS
												GROUP BY VBELN,TKNUM,
														 VGPOS,
														 MATNR
											) l
												ON l.VBELN = e.EBELN
												   AND l.VGPOS = v.EBELP
												   AND l.maxTKNUM = k.TKNUM
											LEFT JOIN FFSCRTN f
												ON f.FFS_CRTN_TYP = k.FFS_CRTN_TYP
											LEFT JOIN LIKP likp
												ON likp.VBELN = v.VBELN
												   AND likp.TKNUM = v.TKNUM
						 WHERE  v.EBELP like '%" + main_line + @"%'
												  AND ( e.EBELN IN ( '"+ PONumber + @"'  ) OR e.PO_REF IN ( '"+ PONumber + @"'  ) )
												  AND v.FFS_CRTN_QTY IS NOT NULL
												  AND v.FFS_CRTN_QTY > 0
												  AND ( l.MATNR IS NOT NULL  OR  l.MATNR !=''  OR mz.MATNR = ''   OR mz.MATNR   IS NOT NULL  )

									) a
									GROUP BY BEDAT,
											 EBELN,
											 PO_REF,
											 TKNUM,
											 EBELP,
											 VENUM,
											 J_4KSCAT,
											 Pkg_Code,
											 BRGEW,
											 NTGEW,
											 MATNR,
											 PPrfNo,
											 FFS_LENGTH_OUTER,
											 FFS_WIDTH_OUTER,
											 FFS_HEIGHT_OUTER,
											 KUNNR
								) counts
									ON counts.BEDAT = ds.BEDAT
									   AND counts.EBELN = ds.EBELN
									   AND counts.PO_REF = ds.PO_REF
									   AND counts.EBELP = ds.EBELP
									   AND counts.VENUM = ds.VENUM
									   AND counts.J_4KSCAT = ds.J_4KSCAT
									   AND counts.Pkg_Code = ds.Pkg_Code
									   AND counts.MATNR = ds.MATNR
									   AND counts.PPrfNo = ds.PPrfNo ;	 ";


			DataTable result = Nike_SqlHelper.ExcuteTable(org,sql1);
			return result;
		}




		public DataTable getDateFromScanService(string org,string startDate, string StopDate)
		{

			string sql1 = @" SELECT 
								   ds.EBELN,
								   ds.PO_REF,
								   ds.FFS_CRTN_QTY,
								   counts.countQty,
								   ds.TKNUM,
								   ds.EBELP,
								   ds.VENUM,
								   ds.ETENR,
								   ds.J_3ASIZE,
								   ds.MENGE,
								   ds.EAN11,
								   ds.J_4KSCAT,
								   ds.EXIDV,
								   ds.Pkg_Code,
								   ds.BRGEW,
								   ds.NTGEW,
								   ds.MATNR,
								   ds.PPrfNo,
								   ds.FFS_LENGTH_OUTER,
								   ds.FFS_WIDTH_OUTER,
								   ds.FFS_HEIGHT_OUTER,
								   ds.KUNNR
							FROM
							(
								SELECT e.BEDAT,
									   e.EBELN,
									   CASE
										   WHEN
										   (
											   e.PO_REF = ''
											   OR e.PO_REF IS NULL
										   ) THEN
											   e.EBELN
										   ELSE
											   e.PO_REF
									   END PO_REF,
									   v.FFS_CRTN_QTY,
									   v.TKNUM,
									   v.EBELP,
									   v.VENUM,
									   EK.ETENR,
									   EK.J_3ASIZE,
									   EK.MENGE,
									   likp.kunnr EAN11,
									   EK.J_4KSCAT,
									   k.EXIDV,
									   k.FFS_CRTN_TYP Pkg_Code,
									   CASE
										   WHEN (k.BRGEW IS NULL) THEN
											   '0'
										   ELSE
											   k.BRGEW
									   END BRGEW,
									   k.NTGEW,
									   CASE
										   WHEN
										   (
											   l.MATNR = ''
											   OR l.MATNR IS NULL
										   ) THEN
											   mz.MATNR
										   ELSE
											   l.MATNR
									   END MATNR,
									   CASE
										   WHEN
										   (
											   l.VERUR = ''
											   OR l.VERUR IS NULL
										   ) THEN
											   EK.EAN11
										   ELSE
											   l.VERUR
									   END PPrfNo,
									   f.FFS_LENGTH_OUTER,
									   f.FFS_WIDTH_OUTER,
									   f.FFS_HEIGHT_OUTER,
									   likp.KUNNR
								FROM EKKO e
									LEFT JOIN VEPO v
										ON e.EBELN = v.EBELN
									LEFT JOIN EKET EK
										ON EK.EBELN = v.EBELN
										   AND EK.EBELP = v.EBELP
										   AND EK.ETENR = v.ETENR
									LEFT JOIN MARASZ mz on '00'+ek.EAN11   =  mz.UPC
									LEFT JOIN VEKP k
										ON v.VENUM = k.VENUM
										   AND
										   (
											   v.TKNUM = k.TKNUM
											   OR
											   (
												   v.TKNUM IS NULL
												   AND k.TKNUM IS NULL
											   )
										   )
									LEFT JOIN
									(
										SELECT VBELN,
											   VGPOS,
											   MAX(BookingRequestID) MaxBookingRequestID,
											   TKNUM maxTKNUM,
											   MATNR,
											   MAX(VERUR) VERUR,
											   MAX(AEDAT) AEDAT
										FROM LIPS
										GROUP BY VBELN,TKNUM,
												 VGPOS,
												 MATNR
									) l
										ON l.VBELN = e.EBELN
										   AND l.VGPOS = v.EBELP
										   AND l.maxTKNUM = k.TKNUM
									LEFT JOIN FFSCRTN f
										ON f.FFS_CRTN_TYP = k.FFS_CRTN_TYP
									LEFT JOIN LIKP likp
										ON likp.VBELN = v.VBELN
										   AND likp.TKNUM = v.TKNUM 
									WHERE ((e.BEDAT BETWEEN  '" + startDate + @"' AND '" + StopDate + @"' )
										or (v.ffs_chng_dttm  BETWEEN  '" + startDate + @"' AND '" + StopDate + @"')
										   )

										AND v.FFS_CRTN_QTY IS NOT NULL
										AND v.FFS_CRTN_QTY >0
										AND ( l.MATNR IS NOT NULL  OR  l.MATNR !=''  OR mz.MATNR = ''   OR mz.MATNR   IS NOT NULL  )



							) ds
								JOIN
								(
									SELECT SUM(FFS_CRTN_QTY) countQty,
										   BEDAT,
										   EBELN,
										   PO_REF,
										   TKNUM,
										   EBELP,
										   VENUM,
										   J_4KSCAT,
										   Pkg_Code,
										   BRGEW,
										   NTGEW,
										   MATNR,
										   PPrfNo,
										   FFS_LENGTH_OUTER,
										   FFS_WIDTH_OUTER,
										   FFS_HEIGHT_OUTER,
										   KUNNR
									FROM
									(
										SELECT e.BEDAT,
											   e.EBELN,
											   CASE
												   WHEN
												   (
													   e.PO_REF = ''
													   OR e.PO_REF IS NULL
												   ) THEN
													   e.EBELN
												   ELSE
													   e.PO_REF
											   END PO_REF,
											   v.FFS_CRTN_QTY,
											   v.TKNUM,
											   v.EBELP,
											   v.VENUM,
											   EK.ETENR,
											   EK.J_3ASIZE,
											   EK.MENGE,
											   likp.kunnr EAN11,
											   EK.J_4KSCAT,
											   k.EXIDV,
											   k.FFS_CRTN_TYP Pkg_Code,
											   CASE
												   WHEN (k.BRGEW IS NULL) THEN
													   '0'
												   ELSE
													   k.BRGEW
											   END BRGEW,
											   k.NTGEW,
											   CASE
												   WHEN
												   (
													   l.MATNR = ''
													   OR l.MATNR IS NULL
												   ) THEN
													   mz.MATNR
												   ELSE
													   l.MATNR
											   END MATNR,
											   CASE
												   WHEN
												   (
													   l.VERUR = ''
													   OR l.VERUR IS NULL
												   ) THEN
													   EK.EAN11
												   ELSE
													   l.VERUR
											   END PPrfNo,
											   f.FFS_LENGTH_OUTER,
											   f.FFS_WIDTH_OUTER,
											   f.FFS_HEIGHT_OUTER,
											   likp.KUNNR
										FROM EKKO e
											LEFT JOIN VEPO v
												ON e.EBELN = v.EBELN
											LEFT JOIN EKET EK
												ON EK.EBELN = v.EBELN
												   AND EK.EBELP = v.EBELP
												   AND EK.ETENR = v.ETENR
											LEFT JOIN MARASZ mz on '00'+ek.EAN11   =  mz.UPC
											LEFT JOIN VEKP k
												ON v.VENUM = k.VENUM
												   AND
												   (
													   v.TKNUM = k.TKNUM
													   OR
													   (
														   v.TKNUM IS NULL
														   AND k.TKNUM IS NULL
													   )
												   )
											LEFT JOIN
											(
												SELECT VBELN,
													   VGPOS,
													   MAX(BookingRequestID) MaxBookingRequestID,
													   TKNUM maxTKNUM,
													   MATNR,
													   MAX(VERUR) VERUR,
													   MAX(AEDAT) AEDAT
												FROM LIPS
												GROUP BY VBELN,TKNUM,
														 VGPOS,
														 MATNR
											) l
												ON l.VBELN = e.EBELN
												   AND l.VGPOS = v.EBELP
												   AND l.maxTKNUM = k.TKNUM
											LEFT JOIN FFSCRTN f
												ON f.FFS_CRTN_TYP = k.FFS_CRTN_TYP
											LEFT JOIN LIKP likp
												ON likp.VBELN = v.VBELN
												   AND likp.TKNUM = v.TKNUM
									WHERE ((e.BEDAT BETWEEN  '" + startDate + @"' AND '" + StopDate + @"' )
										or (v.ffs_chng_dttm  BETWEEN  '" + startDate + @"' AND '" + StopDate + @"')
										   )
										AND v.FFS_CRTN_QTY IS NOT NULL
										AND v.FFS_CRTN_QTY >0
										AND ( l.MATNR IS NOT NULL  OR  l.MATNR !=''  OR mz.MATNR = ''   OR mz.MATNR   IS NOT NULL  )

									) a
									GROUP BY BEDAT,
											 EBELN,
											 PO_REF,
											 TKNUM,
											 EBELP,
											 VENUM,
											 J_4KSCAT,
											 Pkg_Code,
											 BRGEW,
											 NTGEW,
											 MATNR,
											 PPrfNo,
											 FFS_LENGTH_OUTER,
											 FFS_WIDTH_OUTER,
											 FFS_HEIGHT_OUTER,
											 KUNNR
								) counts
									ON counts.BEDAT = ds.BEDAT
									   AND counts.EBELN = ds.EBELN
									   AND counts.PO_REF = ds.PO_REF
									   AND counts.EBELP = ds.EBELP
									   AND counts.VENUM = ds.VENUM
									   AND counts.J_4KSCAT = ds.J_4KSCAT
									   AND counts.Pkg_Code = ds.Pkg_Code
									   AND counts.MATNR = ds.MATNR
									   AND counts.PPrfNo = ds.PPrfNo ";


			DataTable result = Nike_SqlHelper.ExcuteTable(org, sql1);
			return result;
		}



		public DataTable getNikeDataFromFsgByConpprIds(string ids)
		{
			string sql = @"SELECT
								id 
							FROM
								con_ppr 
							WHERE
								id IN ( " + ids + ")";
			// DataTable result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
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
			// return result;
		}

		public int UpdataNikeDataToFsgConppr(DataTable dt)
		{
			string wherstr = "";
			string columnstr = "";
			string ids = "";
		  //  foreach (DataRow row in dt.Rows)
		  //  {
		  //      ids = ids + "'" + row["id"].ToString() + "',";
		  //  }
		 //   ids = ids.Substring(0, ids.Length - 1);

			int pages = 0;
			int results = 0;
			int pageSize = 10000;



			if (dt.Rows.Count > pageSize)
			{

				if( dt.Rows.Count % pageSize == 0)
				{
					pages = dt.Rows.Count / pageSize;
				}
				else
				{
					pages = (dt.Rows.Count / pageSize) + 1;
				}


				for (int p = 1; p < pages; p++)
				{

					if ( ((pages -1) * pageSize)  <  dt.Rows.Count)
					{
						for (int j = 1; j < dt.Columns.Count; j++)
						{
							for (int i = ( p -1 ) * pageSize;  i < p * pageSize; i++)
							{
								wherstr = wherstr + "  WHEN   '" + dt.Rows[i]["id"] + "'  THEN   '" + dt.Rows[i][j].ToString() + "'  ";
								ids = ids + "'" + dt.Rows[i]["id"].ToString() + "',";

							}
							ids = ids.Substring(0, ids.Length - 1);
							columnstr = columnstr + dt.Columns[j].ColumnName + "  =   CASE  id  " + wherstr + "  END,";
							wherstr = "";
						}
						columnstr = columnstr.Substring(0, columnstr.Length - 1);
						string sql = @"UPDATE con_ppr SET  " + columnstr + "   WHERE id IN(" + ids + ");";
						//  int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
						int result = 0;
						if (MiddleWare == "1")
						{
							result = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
						}
						else
						{
							result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
						}
						results = results + result;
						ids ="";
						columnstr = "";
					}
					else
					{
						for (int j = 1; j < dt.Columns.Count; j++)
						{
							for (int i = (p - 1) * pageSize; i < dt.Rows.Count; i++)
							{
								wherstr = wherstr + "  WHEN   '" + dt.Rows[i]["id"] + "'  THEN   '" + dt.Rows[i][j].ToString() + "'  ";
								ids = ids + "'" + dt.Rows[i]["id"].ToString() + "',";
							}
							columnstr = columnstr + dt.Columns[j].ColumnName + "  =   CASE  id  " + wherstr + "  END,";
							wherstr = "";
							ids = ids.Substring(0, ids.Length - 1);
						}
						columnstr = columnstr.Substring(0, columnstr.Length - 1);
						string sql = @"UPDATE con_ppr SET  " + columnstr + "   WHERE id IN(" + ids + ");";
						//  int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
						int result = 0;
						if (MiddleWare == "1")
						{
							result = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
						}
						else
						{
							result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
						}

						results = results + result;
						ids = "";
						columnstr = "";


					}
				}


			}
			else
			{
				for (int j = 1; j < dt.Columns.Count; j++)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						wherstr = wherstr + "  WHEN   '" + dt.Rows[i]["id"] + "'  THEN   '" + dt.Rows[i][j].ToString() + "'  ";
						ids = ids + "'" + dt.Rows[i]["id"].ToString() + "',";
					}
					columnstr = columnstr + dt.Columns[j].ColumnName + "  =   CASE  id  " + wherstr + "  END,";
					wherstr = "";
					ids = ids.Substring(0, ids.Length - 1);
				}
				columnstr = columnstr.Substring(0, columnstr.Length - 1);
				string sql = @"UPDATE con_ppr SET  " + columnstr + "   WHERE id IN(" + ids + ");";
				//  int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
				int result = 0;
				if (MiddleWare == "1")
				{
					result = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
				}
				else
				{
					result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
				}
				results = result;
				ids = "";
				columnstr = "";
			}
			return results;
		}

		public int insetNikeDataToFsgConppr(DataTable dt)
		{


		   // dr["Con_Net"] = Convert.ToDouble(dt.Rows[i]["con_net"]);
			// dr["con_Gross"] = 0.00;

			string values = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				values = values + "('" + dt.Rows[i]["id"] + "'," +
								  "'" + dt.Rows[i]["Cust_id"] + "'," +
								  "'" + dt.Rows[i]["Serial_From"] + "'," +
								  "'" + dt.Rows[i]["qty"] + "'," +
								  "'" + dt.Rows[i]["org"] + "'," +
								  "'" + dt.Rows[i]["PPrfNo"] + "'," +
								  "'" + dt.Rows[i]["count1"] + "'," +
								  "'" + dt.Rows[i]["create_pc"] + "'," +
								  "'" + dt.Rows[i]["update_date"] + "'," +
								  "'" + dt.Rows[i]["con_no"] + "'," +
								  "'" + dt.Rows[i]["country_code"] + "'," +
								  "'" + dt.Rows[i]["con_to"] + "'," +
								  "'" + dt.Rows[i]["Pkg_Code"] + "'," +
								  "'" + dt.Rows[i]["Scan_ID"] + "'," +
								  "'" + dt.Rows[i]["Net_Net"] + "'," +
								  "'" + dt.Rows[i]["Con_Net"] + "'," +

								  "'" +  0.00 + "'," +
								  "'" + dt.Rows[i]["con_l"] + "'," +
								  "'" + dt.Rows[i]["con_W"] + "'," +
								  "'" + dt.Rows[i]["con_H"] + "'," +
								  "'" + dt.Rows[i]["b_Volume"] + "'," +
								  "'" + dt.Rows[i]["PO"] + "'," +
								  "'" + dt.Rows[i]["MAIN_LINE"] + "'),";
			}
			values = values.Substring(0, values.Length - 1);

			string sql = @"INSERT INTO con_ppr (
												id,
												Cust_id,
												Serial_From,
												qty,
												org,
												PPrfNo,
												count1,
												create_pc,
												update_date,
												con_no,
												country_code,
												con_to,
												Pkg_Code,
												Scan_ID,
												Net_Net,
												Con_Net,
												con_Gross,
												con_l,
												con_W,
												con_H,
												b_Volume,
												PO,
												MAIN_LINE 
											)
											VALUES
												" + values  ;
			// int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
			//   DataTable dt = new DataTable();
			sql = sql + @" ON DUPLICATE KEY UPDATE id=VALUES(id),Cust_id=VALUES(Cust_id),Serial_From=VALUES(Serial_From) ,qty=VALUES(qty)
	,org=VALUES(org),PPrfNo=VALUES(PPrfNo) ,count1=VALUES(count1) ,create_pc=VALUES(create_pc) ,update_date=VALUES(update_date)
	,con_no=VALUES(con_no),country_code=VALUES(country_code),con_to=VALUES(con_to),Pkg_Code=VALUES(Pkg_Code),Scan_ID=VALUES(Scan_ID)	
	,Net_Net=VALUES(Net_Net),Con_Net=VALUES(Con_Net),con_Gross=VALUES(con_Gross),con_l=VALUES(con_l),con_W=VALUES(con_W)
	,con_H=VALUES(con_H),b_Volume=VALUES(b_Volume),PO=VALUES(PO),MAIN_LINE=VALUES(MAIN_LINE) ";


			int result = 0;

			if (MiddleWare != "1")
			{
				result = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
			}

			return result;
		}


		public int insetNikeDataToFsgConDetail(DataTable dt)
		{
			string values = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{

				values = values + "('" + dt.Rows[i]["id"] + "'," +
								  "'" + dt.Rows[i]["Cust_id"] + "'," +
								  "'" + dt.Rows[i]["Serial_From"] + "'," +
								  "'" + dt.Rows[i]["Buyer_Item"] + "'," +
								  "'" + dt.Rows[i]["Item_desc"] + "'," +
								  "'" + dt.Rows[i]["color_code"] + "'," +
								  "'" + dt.Rows[i]["Size1"] + "'," +
								  "'" + dt.Rows[i]["con_Qty"] + "'," +
								  "'" + dt.Rows[i]["qty"] + "'," +
								  "'" + dt.Rows[i]["pprfno"] + "'),";
			}
			values = values.Substring(0, values.Length - 1);

			string sql = @"INSERT INTO con_detail (
												id,
												Cust_id,
												Serial_From,
												Buyer_Item,
												Item_desc,
												color_code,
												Size1,
												con_Qty,
												qty,
												pprfno 
											)
											VALUES
												" + values    ;
			// int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
			sql = sql + @" 	ON DUPLICATE KEY UPDATE id=VALUES(id), Cust_id=VALUES(Cust_id),Serial_From=VALUES(Serial_From) ,Buyer_Item=VALUES(Buyer_Item)
	,Item_desc=VALUES(Item_desc),color_code=VALUES(color_code) ,Size1=VALUES(Size1) ,con_Qty=VALUES(con_Qty) ,qty=VALUES(qty)
	,pprfno=VALUES(pprfno) ";
			int result = 0;
			if (MiddleWare != "1")
			{
				result = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
			}
			else
			{
				result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
			}

			return result;
		}
		public int UpdataNikeDataToFsgConDetail(DataTable dt)
		{
			string wherstr = "";
			string columnstr = "";
			string ids = "";
			foreach (DataRow row in dt.Rows)
			{
				ids = ids + "'" + row["id"].ToString() + "',";
			}
			ids = ids.Substring(0, ids.Length - 1);

			for (int j = 1; j < dt.Columns.Count; j++)
			{
				if (dt.Columns[j].ColumnName == "con_Qty")
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{

						wherstr = wherstr + "  WHEN   '" + dt.Rows[i]["id"] + "'  THEN   " + Convert.ToInt32(dt.Rows[i][j].ToString()) + "  ";

					}
				}
				else
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{

						wherstr = wherstr + "  WHEN   '" + dt.Rows[i]["id"] + "'  THEN   '" + dt.Rows[i][j].ToString() + "'  ";

					}
				}

				columnstr = columnstr + dt.Columns[j].ColumnName + "  =   CASE  id  " + wherstr + "  END,";
				wherstr = "";
			}
			columnstr = columnstr.Substring(0, columnstr.Length - 1);
			string sql = @"UPDATE con_detail SET  " + columnstr + "   WHERE id IN(" + ids + ");";
			//  int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
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

		public DataTable getNikeDataFromFsgByConDetailIds(string ids)
		{

			string sql = @"SELECT
								id 
							FROM
								con_detail 
							WHERE
								id IN ( " + ids + ")";
			//  DataTable result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
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
			//return result;
		}

		public DataTable getFsgTagNumber( )
		{

			string sql = @"SELECT
							tagnumber 
						FROM
							(
							SELECT
								b1.tagnumber,
								b1.con_no,
								ROUND( c.con_no, 2 ),
								b1.location,
								b1.cust_id,
								c.po,
								c.buyer_item,
								c.color_code,
								c.size1,
								c.qty,
								b1.kg,
								b1.subinv,
								b1.lastscantime,
								b2.completed_no,
							IF
								( isnull( b2.qty ), IF ( isnull( b2.completed_no ), NULL, c.qty ), b2.qty ),
								c.gtn_po,
								c.corg,
								c.pprfno,
								b1.updatedate,
								c.main_line AS po_line 
							FROM
								tag_stock_info b1
								LEFT JOIN con_no c ON b1.cust_id = c.cust_id 
								AND b1.con_no = c.serial_from
								LEFT JOIN tag_inf b2 ON b1.tagnumber = b2.tagnumber 
								AND c.buyer_item = b2.buyer_item 
								AND c.color_code = b2.color_code 
								AND c.size1 = b2.size1 
							WHERE
								1 = 1 
								AND b1.org = 'TOP' 
								AND c.po IS NULL 
								AND b1.cust_id = 'NIKE' 
								AND b1.lastScanTime > '2020-12-31' 
							) a 
						GROUP BY
							tagnumber";
			//  DataTable result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
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
			//return result;
		}

		public DataTable getDateFromScanService2(string org, string tags)
		{

			string sql2 = @" SELECT 
								   ds.EBELN,
								   ds.PO_REF,
								   ds.FFS_CRTN_QTY,
								   counts.countQty,
								   ds.TKNUM,
								   ds.EBELP,
								   ds.VENUM,
								   ds.ETENR,
								   ds.J_3ASIZE,
								   ds.MENGE,
								   ds.EAN11,
								   ds.J_4KSCAT,
								   ds.EXIDV,
								   ds.Pkg_Code,
								   ds.BRGEW,
								   ds.NTGEW,
								   ds.MATNR,
								   ds.PPrfNo,
								   ds.FFS_LENGTH_OUTER,
								   ds.FFS_WIDTH_OUTER,
								   ds.FFS_HEIGHT_OUTER,
								   ds.KUNNR
							FROM
							(
								SELECT e.BEDAT,
									   e.EBELN,
									   CASE
										   WHEN
										   (
											   e.PO_REF = ''
											   OR e.PO_REF IS NULL
										   ) THEN
											   e.EBELN
										   ELSE
											   e.PO_REF
									   END PO_REF,
									   v.FFS_CRTN_QTY,
									   v.TKNUM,
									   v.EBELP,
									   v.VENUM,
									   EK.ETENR,
									   EK.J_3ASIZE,
									   EK.MENGE,
									   likp.kunnr EAN11,
									   EK.J_4KSCAT,
									   k.EXIDV,
									   k.FFS_CRTN_TYP Pkg_Code,
									   CASE
										   WHEN (k.BRGEW IS NULL) THEN
											   '0'
										   ELSE
											   k.BRGEW
									   END BRGEW,
									   k.NTGEW,
									   CASE
										   WHEN
										   (
											   l.MATNR = ''
											   OR l.MATNR IS NULL
										   ) THEN
											   mz.MATNR
										   ELSE
											   l.MATNR
									   END MATNR,
									   CASE
										   WHEN
										   (
											   l.VERUR = ''
											   OR l.VERUR IS NULL
										   ) THEN
											   EK.EAN11
										   ELSE
											   l.VERUR
									   END PPrfNo,
													   CASE
					WHEN
						(
							f.FFS_LENGTH_OUTER IS NULL
							) THEN
						0.00
					ELSE
						f.FFS_LENGTH_OUTER
					END        FFS_LENGTH_OUTER,

				CASE
					WHEN
						(
							f.FFS_WIDTH_OUTER IS NULL
							) THEN
						0.00
					ELSE
						f.FFS_WIDTH_OUTER
					END        FFS_WIDTH_OUTER,
				CASE
					WHEN
						(
							f.FFS_HEIGHT_OUTER IS NULL
							) THEN
						0.00
					ELSE
						f.FFS_HEIGHT_OUTER
					END        FFS_HEIGHT_OUTER,

				CASE
					WHEN
						(
							likp.KUNNR IS NULL
							) THEN
						''
					ELSE
						likp.KUNNR
					END        KUNNR
								FROM EKKO e
									LEFT JOIN VEPO v
										ON e.EBELN = v.EBELN
									LEFT JOIN EKET EK
										ON EK.EBELN = v.EBELN
										   AND EK.EBELP = v.EBELP
										   AND EK.ETENR = v.ETENR
									LEFT JOIN MARASZ mz on '00'+ek.EAN11   =  mz.UPC
									LEFT JOIN VEKP k
										ON v.VENUM = k.VENUM
										   AND
										   (
											   v.TKNUM = k.TKNUM
											   OR
											   (
												   v.TKNUM IS NULL
												   AND k.TKNUM IS NULL
											   )
										   )
									LEFT JOIN
									(
										SELECT VBELN,
											   VGPOS,
											   MAX(BookingRequestID) MaxBookingRequestID,
											   TKNUM maxTKNUM,
											   MATNR,
											   MAX(VERUR) VERUR,
											   MAX(AEDAT) AEDAT
										FROM LIPS
										GROUP BY VBELN,TKNUM,
												 VGPOS,
												 MATNR
									) l
										ON l.VBELN = e.EBELN
										   AND l.VGPOS = v.EBELP  AND l.maxTKNUM = k.TKNUM
									LEFT JOIN FFSCRTN f
										ON f.FFS_CRTN_TYP = k.FFS_CRTN_TYP
									LEFT JOIN LIKP likp
										ON likp.VBELN = v.VBELN
										   AND likp.TKNUM = v.TKNUM
									WHERE v.EBELP LIKE '%%'
											AND v.FFS_CRTN_QTY IS NOT NULL
											AND v.FFS_CRTN_QTY > 0                                         
											AND k.EXIDV IN ( " + tags + @" )
											AND ( l.MATNR IS NOT NULL  OR  l.MATNR !=''  OR mz.MATNR = ''   OR mz.MATNR   IS NOT NULL  )



							) ds
								JOIN
								(
									SELECT SUM(FFS_CRTN_QTY) countQty,
										   BEDAT,
										   EBELN,
										   PO_REF,
										   TKNUM,
										   EBELP,
										   VENUM,
										   J_4KSCAT,
										   Pkg_Code,
										   BRGEW,
										   NTGEW,
										   MATNR,
										   PPrfNo,
										   FFS_LENGTH_OUTER,
										   FFS_WIDTH_OUTER,
										   FFS_HEIGHT_OUTER,
										   KUNNR
									FROM
									(
										SELECT e.BEDAT,
											   e.EBELN,
											   CASE
												   WHEN
												   (
													   e.PO_REF = ''
													   OR e.PO_REF IS NULL
												   ) THEN
													   e.EBELN
												   ELSE
													   e.PO_REF
											   END PO_REF,
											   v.FFS_CRTN_QTY,
											   v.TKNUM,
											   v.EBELP,
											   v.VENUM,
											   EK.ETENR,
											   EK.J_3ASIZE,
											   EK.MENGE,
											   likp.kunnr EAN11,
											   EK.J_4KSCAT,
											   k.EXIDV,
											   k.FFS_CRTN_TYP Pkg_Code,
											   CASE
												   WHEN (k.BRGEW IS NULL) THEN
													   '0'
												   ELSE
													   k.BRGEW
											   END BRGEW,
											   k.NTGEW,
											   CASE
												   WHEN
												   (
													   l.MATNR = ''
													   OR l.MATNR IS NULL
												   ) THEN
													   mz.MATNR
												   ELSE
													   l.MATNR
											   END MATNR,
											   CASE
												   WHEN
												   (
													   l.VERUR = ''
													   OR l.VERUR IS NULL
												   ) THEN
													   EK.EAN11
												   ELSE
													   l.VERUR
											   END PPrfNo,
											   f.FFS_LENGTH_OUTER,
											   f.FFS_WIDTH_OUTER,
											   f.FFS_HEIGHT_OUTER,
											   likp.KUNNR
										FROM EKKO e
											LEFT JOIN VEPO v
												ON e.EBELN = v.EBELN
											LEFT JOIN EKET EK
												ON EK.EBELN = v.EBELN
												   AND EK.EBELP = v.EBELP
												   AND EK.ETENR = v.ETENR
											LEFT JOIN MARASZ mz on '00'+ek.EAN11   =  mz.UPC
											LEFT JOIN VEKP k
												ON v.VENUM = k.VENUM
												   AND
												   (
													   v.TKNUM = k.TKNUM
													   OR
													   (
														   v.TKNUM IS NULL
														   AND k.TKNUM IS NULL
													   )
												   )
											LEFT JOIN
											(
												SELECT VBELN,
													   VGPOS,
													   MAX(BookingRequestID) MaxBookingRequestID,
													   TKNUM maxTKNUM,
													   MATNR,
													   MAX(VERUR) VERUR,
													   MAX(AEDAT) AEDAT
												FROM LIPS
												GROUP BY VBELN,TKNUM,
														 VGPOS,
														 MATNR
											) l
												ON l.VBELN = e.EBELN
												   AND l.VGPOS = v.EBELP AND l.maxTKNUM = k.TKNUM
											LEFT JOIN FFSCRTN f
												ON f.FFS_CRTN_TYP = k.FFS_CRTN_TYP
											LEFT JOIN LIKP likp
												ON likp.VBELN = v.VBELN
												   AND likp.TKNUM = v.TKNUM
											WHERE v.EBELP LIKE '%%'
												  AND v.FFS_CRTN_QTY IS NOT NULL
												  AND v.FFS_CRTN_QTY > 0
												  AND k.EXIDV IN ( " + tags + @" )
												  AND ( l.MATNR IS NOT NULL  OR  l.MATNR !=''  OR mz.MATNR = ''   OR mz.MATNR   IS NOT NULL  )

									) a
									GROUP BY BEDAT,
											 EBELN,
											 PO_REF,
											 TKNUM,
											 EBELP,
											 VENUM,
											 J_4KSCAT,
											 Pkg_Code,
											 BRGEW,
											 NTGEW,
											 MATNR,
											 PPrfNo,
											 FFS_LENGTH_OUTER,
											 FFS_WIDTH_OUTER,
											 FFS_HEIGHT_OUTER,
											 KUNNR
								) counts
									ON counts.BEDAT = ds.BEDAT
									   AND counts.EBELN = ds.EBELN
									   AND counts.PO_REF = ds.PO_REF
									   AND counts.EBELP = ds.EBELP
									   AND counts.VENUM = ds.VENUM
									   AND counts.J_4KSCAT = ds.J_4KSCAT
									   AND counts.Pkg_Code = ds.Pkg_Code
									   AND counts.MATNR = ds.MATNR
									   AND counts.PPrfNo = ds.PPrfNo ";



			DataTable result = Nike_SqlHelper.ExcuteTable(org, sql2);
			return result;
		}
	}
}
