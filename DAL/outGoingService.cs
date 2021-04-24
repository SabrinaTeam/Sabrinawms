using MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
   public  class outGoingService
    {
		public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;

		public DataTable getSubinvs(string org)
        {
			string sql = @"SELECT DISTINCT subinv FROM location  WHERE org='" + org + "'";
           
         

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

        public DataTable getLocation(string subinv)
        {
			string sql = @"SELECT DISTINCT Location FROM location  WHERE subinv='" + subinv + "'";
            
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

        public DataTable getOutgoing(string org, string subinv, string location, string starTime, string stopTime)
        {

			string sql = @"
							SELECT
									 DATE_FORMAT( a.ScanTime, '%Y-%m-%d' ) AS ScanTime,
									a.cust_id,
									d.Buyer_Item,
									n.PONumber AS OrderPO,
									g.po as GtnPO,
									 
									p.MAIN_LINE,
									d.color_code ,
									DATE_FORMAT( str_to_date(n.OGACDate, '%m/%d/%Y'), '%Y-%m-%d' ) AS OGACDate,
									
									n.Plant,
									a.con_no,
									d.Size1,
									d.qty size_qty,
									cd.po_qty,
									a.org,
									a.subinv,
									a.location,
									a.TagNumber,
										p.qty box_qty,
										a.kg,
										DATE_FORMAT( a.Update_Date, '%Y-%m-%d' ) AS Update_Date,
										a.Create_Pc,
										A.id
									FROM
									(
									SELECT
										A.id,
										a.TagNumber,
										a.cust_id,
										a.location,
										a.Update_Date,
										a.org,
										a.con_no,
										a.Create_Pc,
										a.kg,
										a.subinv,
										a.ScanTime 
									FROM
										inv A,
										(
										SELECT
											id,
											min( ScanTime ) min_time 
										FROM
											inv 
										WHERE

									 scantime    BETWEEN    STR_TO_DATE('" + starTime + @"','%Y-%m-%d') 
												       AND  STR_TO_DATE('" + stopTime + @"','%Y-%m-%d' )
											 
											AND org = '" + org + @"' 
											AND subinv = '" + subinv + @"' 
											AND location = '" + location + @"' 
										GROUP BY
											ScanTime,
											id 
										ORDER BY
											ScanTime 
										) B 
									WHERE
										A.id = B.id 
										AND A.ScanTime = B.min_time 
									) a
									LEFT JOIN con_ppr p ON a.con_no = p.Serial_From
									LEFT JOIN con_detail d ON a.con_no = d.Serial_From 
														AND p.PPrfNo = d.pprfno
									LEFT JOIN ( SELECT SUM( qty ) AS po_qty, pprfno FROM con_detail GROUP BY pprfno ) cd ON cd.PPrfNo = p.pprfno
									
									LEFT JOIN nikeconnect n ON n.TradingCompanyPO =  p.PO 
														 and n.POItem = p.MAIN_LINE
									LEFT JOIN gtn_po  g on g.GTN_PO =  p.PO
									ORDER BY a.con_no, d.Size1 + 0";
			 
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

		public DataTable getMoveLocals(string tags)
		{
			string sql = @"SELECT
								id,
								TagNumber,
								max( ScanTime ) max_time ,								
								Location
							FROM
								inv  
							WHERE
								TagNumber = '" + tags + @"'
	 
							GROUP BY
								ScanTime,
								id 
							ORDER BY
								ScanTime DESC 
								LIMIT 0,
								1";
			 
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

		/// <summary>
		/// 从BEST取回订单资料
		/// </summary>
		/// <param name="po_no"></param>
		/// <param name="style_id"></param>
		/// <param name="clr_no"></param>
		/// <returns></returns>
		public DataTable getOD_POFromBestByPSC(List<outGoing_pos> pscs)
		{
			string style_id = "";
			string clr_no = "";
			string po_no = "";
			if (pscs.Count <= 0)
            {
				DataTable dt = new DataTable();
				return dt;
            }
			for(int i = 0; i < pscs.Count; i++)
            {
				style_id = style_id + "'" + pscs[i].style_ids + "',";
				clr_no = clr_no + "'" + pscs[i].clr_nos + "',";
				po_no = po_no + "'" + pscs[i].po_ids + "',";

			}
			style_id = style_id.Substring(0, style_id.Length - 1);
			clr_no = clr_no.Substring(0, clr_no.Length - 1);
			po_no = po_no.Substring(0, po_no.Length - 1);


			string sql = @" 
							   SELECT b1.po_no,
							   h1.cust_id,
							   h1.season_id,
							   h1.release_who,
							   b1.style_id,
							   b1.clr_no
							FROM dbo.odh h1
								LEFT JOIN odb b1
									ON h1.od_no = b1.od_no
									   AND b1.style_id IN ( " + style_id+ @" )
									   AND b1.clr_no IN ( " + clr_no + @"  )
								 
							WHERE b1.po_no IN (  " + po_no + @" )
								  AND b1.style_id IN ( " + style_id + @" )
								  AND b1.clr_no IN ( " + clr_no + @"  )
								  AND b1.po_no != ''
							  
							GROUP BY b1.po_no,	            
									 h1.cust_id,
									 h1.season_id,
									 h1.release_who,
									 b1.style_id,
									 b1.clr_no;";
			DataTable result = BEST_SqlHelper.ExcuteTable(sql);
			return result;
		}

		public DataTable getMy_NoFromBest(string po_no, string clr_no, string style_id, string area_id, string def_date)
		{ 

			string sql = @"
							SELECT b.po_no,
									   mark = (STUFF(
											   (
												   SELECT b.po_no,
														  b.od_no,
														  b.qty,
														  h.my_no
												   FROM dbo.odb b
													   LEFT JOIN odh h
														   ON b.od_no = h.od_no
												   WHERE b.po_no = '"+ po_no + @"'
														 AND b.clr_no = '"+ clr_no + @"'
														 AND b.style_id = '"+ style_id  + @"'
														 AND b.area_id = '"+ area_id + @"'
														 AND b.def_date='"+ def_date + @"'
												   FOR XML PATH('')
											   ),
											   1,
											   0,
											   ''
													)
											  )
								FROM odb b
								WHERE b.po_no = '" + po_no + @"'
														 AND b.clr_no = '" + clr_no + @"'
														 AND b.style_id = '" + style_id + @"'
														 AND b.area_id = '" + area_id + @"'
														 AND b.def_date='" + def_date + @"'
								GROUP BY po_no;";
			DataTable result = BEST_SqlHelper.ExcuteTable(sql);
			return result;
		}

		public DataTable getReceiFromNoBarCode(string org, string subinv, string location, string starTime, string stopTime)
		{

			string sql = @"
							 
							SELECT
								org,
								subinv,
								line,
								style,
								color,
								size,
								SUM( qtyCount ) qtyCount,
								SUM( PO ) OffsetQty,
								receiNumber ,
								receiDate,
								receiInPcName
							FROM
								receis 
							WHERE
								org = '" + org  + @"' 
								AND subinv = '"+ subinv + @"'
								AND line = '"+ location + @"' 
								AND qtyCount > 0 
								AND isFull = 0
								AND receiDate BETWEEN '" + starTime + "' and '"+ stopTime + @"'
							GROUP BY
								org,
								subinv,
								line,
								style,
								color,
								size,
								receiNumber,
								receiDate,
								receiInPcName
							ORDER BY 
								org,
								subinv,
								line,
								receiDate,
								style,
								color,
								size;";
			 
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


		/// <summary>
		/// 从BEST取回月BUY
		/// </summary>
		/// <param name="po_no"></param>
		/// <param name="style_id"></param>
		/// <param name="clr_no"></param>
		/// <returns></returns>
		public DataTable getYYMMFromBestByPo(string po)
		{
			string sql = @" SELECT buyid.yymm,
								   buyid.buy_cname,
								   b.po_no
							FROM tb_sfcbuy buyid
								LEFT JOIN odh h
									ON h.od_date
									   BETWEEN buyid.begin_day AND buyid.end_day
									   AND buyid.cust_buy_id = 'A0001'
								LEFT JOIN odb b
									ON b.od_no = h.od_no
							WHERE b.po_no = '"+ po  + @"'
							GROUP BY buyid.yymm,
									 buyid.buy_cname,
									 b.po_no;";
			DataTable result = BEST_SqlHelper.ExcuteTable(sql);
			return result;
		}

		public DataTable getOffSet(string org, string subinv, string location, string starTime, string stopTime)
		{

			string sql = @"
							SELECT
									TagNumber
								FROM
									invreceis 
								WHERE
									org = '"+ org +@"' 
									AND subinv = '"+ subinv +@"' 
									AND line = '"+ location +@"'
									AND scantime BETWEEN STR_TO_DATE( '"+ starTime + @"', '%Y-%m-%d' ) 
									AND STR_TO_DATE( '"+ stopTime+"','%Y-%m-%d' )";
			 
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
