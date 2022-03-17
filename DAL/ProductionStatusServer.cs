using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductionStatusServer
    {
        public DataTable getProductionStatus(ProductionStatusSearch parameters)
        {
            string sql = "";
            int type = parameters.datetype;
            if (type == 0)
            {
                if (parameters.checkedDate)
                {
                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   convert(varchar, h.od_date, 23) od_date,	                                   
                                   buyid.yymm,                                 
                                   s.style_id style_Name,
                                   s.style_name Description,
                                   b.clr_no clr_Code,
                                   c.clr_name,
                                   SUM(b.qty) OrderQty,
                                   convert(varchar,  MIN(b.def_date), 23)  PanlDate
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END
                            WHERE  1 = 1  AND     
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   h.season_id like '' + @season + '%'   AND 
                                   b.po_no NOT LIKE '%樣%' AND
                                   CONVERT (datetime,  h.od_date)   BETWEEN  @stardate AND @enddate  
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";
                }
                else
                {

                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   convert(varchar, h.od_date, 23) od_date,	                                   
                                   buyid.yymm,                                 
                                   s.style_id style_Name,
                                   s.style_name Description,
                                   b.clr_no clr_Code,
                                   c.clr_name,
                                   SUM(b.qty) OrderQty,
                                   convert(varchar,  MIN(b.def_date), 23)  PanlDate
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END
                            WHERE  1 = 1 AND 
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   b.po_no NOT LIKE '%樣%' AND
                                   h.season_id like '' + @season + '%' 
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";

                }

            }
            else if (type == 1)
            {

                if (parameters.checkedDate)
                {
                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   convert(varchar, h.od_date, 23) od_date,	                                   
                                   buyid.yymm,                                 
                                   s.style_id style_Name,
                                   s.style_name Description,
                                   b.clr_no clr_Code,
                                   c.clr_name,
                                   SUM(b.qty) OrderQty,
                                   convert(varchar,  MIN(b.def_date), 23)  PanlDate
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END
                            WHERE  1 = 1 AND                            
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   h.season_id like '' + @season + '%'   AND 
                                   b.po_no NOT LIKE '%樣%' AND
                                   CONVERT (datetime,  b.def_date)   BETWEEN  @stardate AND @enddate  
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";
                }
                else
                {
                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   convert(varchar, h.od_date, 23) od_date,	                                   
                                   buyid.yymm,                                 
                                   s.style_id style_Name,
                                   s.style_name Description,
                                   b.clr_no clr_Code,
                                   c.clr_name,
                                   SUM(b.qty) OrderQty,
                                   convert(varchar,  MIN(b.def_date), 23)  PanlDate
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END 

                            WHERE  1 = 1 AND
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   b.po_no NOT LIKE '%樣%' AND
                                   h.season_id like '' + @season + '%'  
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";

                }
            }
            else if (type == 2)
            {

                if (parameters.checkedDate)
                {
                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   convert(varchar, h.od_date, 23) od_date,	                                   
                                   buyid.yymm,                                 
                                   s.style_id style_Name,
                                   s.style_name Description,
                                   b.clr_no clr_Code,
                                   c.clr_name,
                                   SUM(b.qty) OrderQty,
                                   convert(varchar,  MIN(b.def_date), 23)  PanlDate
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END
                            WHERE  1 = 1 AND                            
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   h.season_id like '' + @season + '%'   AND 
                                   b.po_no NOT LIKE '%樣%' AND
                                   CONVERT (datetime,  b.def_date)   BETWEEN  @stardate AND @enddate  
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";
                }
                else
                {
                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   convert(varchar, h.od_date, 23) od_date,	                                   
                                   buyid.yymm,                                 
                                   s.style_id style_Name,
                                   s.style_name Description,
                                   b.clr_no clr_Code,
                                   c.clr_name,
                                   SUM(b.qty) OrderQty,
                                   convert(varchar,  MIN(b.def_date), 23)  PanlDate
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END 

                            WHERE  1 = 1 AND
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   b.po_no NOT LIKE '%樣%' AND
                                   h.season_id like '' + @season + '%'  
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";

                }
            }


            SqlParameter[] ps = {
                                new SqlParameter("myNumber",parameters.mynumber),
                                new SqlParameter("buyid",parameters.buyid),
                                new SqlParameter("season",parameters.season), 
                                new SqlParameter("stardate",parameters.stardate),
                                new SqlParameter("enddate",parameters.enddate)
                                };

            DataTable dt = BEST_SqlHelper.ExcuteTable(sql, ps);
            return dt;
        }


        public DataTable getMesWorkTicketByMynos(List<string> my_nos, string serviceName)
        {
            string my_no = "";
            foreach (string my in my_nos)
            {
                my_no = my_no + "'" + my + "',";
            }
            if (my_no.Length <= 0)
            {
                
                return new DataTable();
            }
            my_no = my_no.Substring(0, my_no.Length - 1);           
            string sql = @"SELECT a.orderSKU,
                                   a.productModel,                                 
                                   a.colorName,
                                   a.partName,
                                   SUM(a.OrderQTY) WLInStock,
                                    CASE
                                           WHEN SUM(a.FinishQty) IS NULL THEN
                                               0
                                           ELSE
                                               SUM(a.FinishQty)
                                       END WLOutStock,
                                       CASE
                                           WHEN a.ProcessID IS NULL THEN
                                               '-1'
                                           ELSE
                                               a.ProcessID
                                       END ProcessID,
                                       CASE
                                           WHEN a.ProcessName IS NULL THEN
                                               ''
                                           ELSE
                                               a.ProcessName
                                       END ProcessName,
                                       CASE
                                           WHEN a.LineName IS NULL THEN
                                               ''
                                           ELSE
                                               a.LineName
                                       END LineName,
                                       CASE
                                           WHEN a.LineID IS NULL THEN
                                               '-1'
                                           ELSE
                                               a.LineID
                                       END receiveLineID,
                                       CASE
                                           WHEN a.ReportPlaceID IS NULL THEN
                                              '-1'
                                           ELSE
                                               a.ReportPlaceID
                                       END ReportPlaceID,
                                       CASE
                                           WHEN a.ReportPlaceName IS NULL THEN
                                               ''
                                           ELSE
                                               a.ReportPlaceName
                                       END ReportPlaceName
                            FROM
                            (
                                SELECT a.orderSKU,
                                       a.productModel,                                      
                                       b.colorName,
                                       b.partName,
                                       SUM(m2.QTY) OrderQTY,
                                       SUM(r.QTY) FinishQty,
                                       r.ProcessID,
                                       r.ProcessName,
                                       r.LineName,
                                       r.LineID,
                                       r.ReportPlaceID,
                                       r.ReportPlaceName
                                FROM t_ManufactureOrderList a
                                    INNER JOIN t_ManufactureOrderDetails b
                                        ON a.MOLID = b.MOLID
                                    LEFT JOIN t_ManufactureOrderDetails2 m2
                                        ON m2.MODID = b.MODID
                                    LEFT JOIN t_ReportOutputDetail r
                                        ON r.MOD2ID = m2.MOD2ID
                                           AND r.TypeID = 1
                                           AND r.ProcessID = 10
                                WHERE a.orderSKU IN ( " + my_no + @"	)	 
                                      AND b.partName = 'A'
                                      AND a.Invalid = 0                                
                                GROUP BY a.orderSKU,
                                         a.productModel,                                        
                                         b.colorName,
                                         b.partName,
                                         r.ProcessID,
                                         r.ProcessName,
                                         r.LineName,
                                         r.LineID,
                                         r.ReportPlaceID,
                                         r.ReportPlaceName
                            ) a
                            GROUP BY a.orderSKU,
                                     a.productModel,                                   
                                     a.colorName,
                                     a.partName,
                                     a.ProcessID,
                                     a.ProcessName,
                                     a.ReportPlaceID,
                                     a.LineName,
                                     a.LineID,
                                     a.ReportPlaceName
                            ORDER BY a.orderSKU,
                                     a.productModel,
                                     a.colorName,
                                     a.ReportPlaceID";  

            DataTable dt = Mes_SqlHelper.ExcuteTable(sql  , serviceName);

            

            return dt;
        }

        public DataTable getERPWorkTicketByMynos(List<string> my_nos, string serviceName)
        {
            string my_no = "";
            foreach (string my in my_nos)
            {
                my_no = my_no + "'" + my + "',";
            }
            my_no = my_no.Substring(0, my_no.Length - 1);
            string sql = @"SELECT   sf.TA_SFB01  AS myNo,
	                                sf.SFB01 AS MakeOrderNo,
	                                sf.sfb05 AS StyleName,
	                                sf.sfb22 AS OrderNo ,
	                                to_char(sf.sfb071, 'YYYY-MM-DD') CreateDate,
	                                sf.sfb08 AS MakeQty,
	                                sf.sfb09 AS ProduFinishQty,
	                                to_char(sf.sfb13, 'YYYY-MM-DD') PlanMakeDate,
	                                sf.sfb87 AS Makechecked,
	                                sf.sfbud04 AS OrgName,
	                                sf.sfb223 AS CustName,
	                                to_char(min(sh.shb02), 'YYYY-MM-DD') FinishDate,
	                                sh.SHB05 AS FinishMakeOrderNo,
	                                sh.SHB06 AS ProcessTypeID,
	                                sh.shb08 AS OrderClass,
	                                sh.shb09 AS MakeLine,
	                                sh.shb081 AS ProcessType,
	                                sh.shb082 AS ProcessName,
	                                sh.shb10 AS StyleName,
	                                sum(sh.shb111) AS CFFinishQty,
	                                sum(sh.shb115) AS CFFinishBonus,
	                                sh.shbacti AS FinishChecked,
	                                sh.shbplant AS MakeOrgName
                                FROM   
	                                " + serviceName + @".SFB_FILE sf
                                LEFT JOIN   " + serviceName + @".SHB_FILE sh ON
	                                sf.SFB01 = sh.shb05
                                WHERE
	                                sf.TA_SFB01 in("+ my_no + @")
                                    AND sf.sfb87 = 'Y'
                                    AND sh.shbacti = 'Y'
                                    AND sh.shb06 = 55
                                GROUP BY
                                    sf.TA_SFB01 ,
	                                sf.SFB01 ,
	                                sf.sfb05 ,
	                                sf.sfb22 ,
	                                sf.sfb071,
	                                sf.sfb08 ,
	                                sf.sfb09,
	                                sf.sfb13,
	                                sf.sfb87 ,
	                                sf.sfbud04 ,
	                                sf.sfb223 ,
	                                sh.SHB05 ,
	                                sh.SHB06 ,
	                                sh.shb08,
	                                sh.shb09 ,
	                                sh.shb081 ,
	                                sh.shb082 ,
	                                sh.shb10 ,
	                                sh.shbacti ,
	                                sh.shbplant
                                ORDER BY
                                    sf.TA_SFB01 ,
	                                sh.shb05,
	                                sh.shb09";

            DataTable dt = ERP_SqlHelper.ExcuteTable(sql);
            return dt;
        }




        public DataTable getProductionWip(ProductionStatusSearch parameters)
        {
            string sql = "";
            int type = parameters.datetype;
            if (type == 0)
            {
                if (parameters.checkedDate)
                {
                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   h.od_date,
                                   buyid.yymm,
                                   s.style_name,
                                   s.style_id,
                                   b.clr_no,
                                   c.clr_name,
                                   SUM(b.qty) qty,
                                   MIN(b.def_date) def_date
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END
                            WHERE  1 = 1  AND     
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   h.season_id like '' + @season + '%'   AND 
                                   b.po_no NOT LIKE '%樣%' AND
                                   CONVERT (datetime,  h.od_date)   BETWEEN  @stardate AND @enddate  
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";
                }
                else
                {

                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   h.od_date,
                                   buyid.yymm,
                                   s.style_name,
                                   s.style_id,
                                   b.clr_no,
                                   c.clr_name,
                                   SUM(b.qty) qty,
                                   MIN(b.def_date) def_date
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END
                            WHERE  1 = 1 AND 
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   b.po_no NOT LIKE '%樣%' AND
                                   h.season_id like '' + @season + '%' 
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";

                }

            }
            else if (type == 1)
            {

                if (parameters.checkedDate)
                {
                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   h.od_date,
                                   buyid.yymm,
                                   s.style_name,
                                   s.style_id,
                                   b.clr_no,
                                   c.clr_name,
                                   SUM(b.qty) qty,
                                   MIN(b.def_date) def_date
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END
                            WHERE  1 = 1 AND                            
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   h.season_id like '' + @season + '%'   AND 
                                   b.po_no NOT LIKE '%樣%' AND
                                   CONVERT (datetime,  b.def_date)   BETWEEN  @stardate AND @enddate  
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";
                }
                else
                {
                    sql = @"SELECT h.my_no,
                                   h.season_id,
                                   h.cust_id,
                                   h.od_date,
                                   buyid.yymm,
                                   s.style_name,
                                   s.style_id,
                                   b.clr_no,
                                   c.clr_name,
                                   SUM(b.qty) qty,
                                   MIN(b.def_date) def_date
                            FROM odh h
                                LEFT JOIN odb b
                                    ON h.od_no = b.od_no
                                LEFT JOIN style s
                                    ON b.style_id = s.style_id
                                LEFT JOIN clr c
                                    ON c.clr_no = b.clr_no
                                LEFT JOIN tb_sfcbuy buyid
                                    ON CONVERT(DATETIME, h.od_date)
                                       BETWEEN buyid.begin_day AND buyid.end_day
                                       AND buyid.cust_buy_id = CASE h.cust_id
                                                                   WHEN 'A0000' THEN
                                                                       'A0001'
                                                                   ELSE
                                                                       'SAB'
                                                               END 

                            WHERE  1 = 1 AND
                                   h.my_no like '' + @myNumber + '%' AND
                                   buyid.yymm like '' + @buyid + '%'  AND
                                   b.po_no NOT LIKE '%樣%' AND
                                   h.season_id like '' + @season + '%'  
                            GROUP BY h.my_no,
                                     h.season_id,
                                     h.cust_id,
                                     h.od_date,
                                     s.style_name,
                                     s.style_id,
                                     b.clr_no,
                                     buyid.yymm,
                                     c.clr_name";

                }
            }


            SqlParameter[] ps = {
                                new SqlParameter("myNumber",parameters.mynumber),
                                new SqlParameter("buyid",parameters.buyid),
                                new SqlParameter("season",parameters.season),
                                new SqlParameter("stardate",parameters.stardate),
                                new SqlParameter("enddate",parameters.enddate)
                                };

            DataTable dt = BEST_SqlHelper.ExcuteTable(sql, ps);
            return dt;
        }



    }
}
