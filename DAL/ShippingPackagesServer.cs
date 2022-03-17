using COMMON;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ShippingPackagesServer
    {
        public ShippingPackagesHelper sph = new ShippingPackagesHelper();
        public string insetShippingPackages_sp_temp(DataTable sp_temp)
        {
            string mesg = sph.SqlBulkToSQL_sp_temp(sp_temp);
            return mesg;
            
        }

        public string insetShippingPackageSizes_sp_temp(DataTable spSize_temp)
        {
            string mesg = sph.SqlBulkToSQL_spSize_temp(spSize_temp);
            return mesg;

        }
        public int  clear_sp_temp( )
        {
            string sql = @" DELETE  FROM t_sp_temp ";
            int result = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
            return result;

        }

        public int clear_spError()
        {
            string sql = @" DELETE  FROM T_SP   WHERE ftyNo  IS NULL or season IS null  or hod IS null ";
            int result = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
            return result;

        }
        public int clear_spsize_temp()
        {
            string sql = @"   DELETE  FROM T_size_temp ";
            int result = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
            return result;

        }

        public int clear_spsizeError()
        {
            string sql = @" DELETE   FROM T_size  	  WHERE ftyNo  IS NULL or season IS null  or sizeQty IS null ";
            int result = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
            return result;

        }



        public int updata_T_sp()
        {
            string sql = @"UPDATE a
                                    SET a.[type] = b.typestr,
                                        a.ftyNo = b.ftyNo,
                                        a.season = b.season,
                                        a.BVPO = b.BVPO,
                                        a.styleName = b.styleName,
                                        a.colDescription = b.colDescription,
                                        a.channel = b.channel,
                                        a.totalQty = b.totalQty,
                                        a.HOD = b.HOD,
                                        a.befoeHOD = b.befoeHOD,
                                        a.newHOD = b.newHOD,
                                        a.shipMode = b.shipMode,
                                        a.sourceTag = b.sourceTag,
                                        a.wwwt = b.wwwt,
                                        a.citHangTag = b.citHangTag,
                                        a.Fastener = b.Fastener,
                                        a.steelNumber = b.steelNumber,
                                        a.cup = b.cup,
                                        a.cclable = b.cclable,
                                        a.sensitive = b.sensitive,
                                        a.remark = b.remark,
                                        a.isCancel = b.isCancel,
                                        a.[modify] = b.modifyPO,
                                        a.overflow = b.overflow
                                    FROM T_SP a
                                        INNER JOIN
                                        (
                                            SELECT b.id AS bid,
                                                   a.id AS aid,
                                                   a.[type] AS typestr,
                                                   a.ftyNo,
                                                   a.season,
                                                   a.BVPO,
                                                   a.masterPO,
                                                   a.GtnPO,
                                                   a.po_mainLine,
                                                   a.styleNumber,
                                                   a.styleName,
                                                   a.color,
                                                   a.colDescription,
                                                   a.channel,
                                                   a.totalQty,
                                                   a.HOD,
                                                   a.befoeHOD,
                                                   a.newHOD,
                                                   a.shipMode,
                                                   a.sourceTag,
                                                   a.wwwt,
                                                   a.citHangTag,
                                                   a.Fastener,
                                                   a.steelNumber,
                                                   a.cup,
                                                   a.cclable,
                                                   a.sensitive,
                                                   a.remark,
                                                   a.org,
                                                   CASE LEN(a.isCancel)
                                                       WHEN 0 THEN
                                                           0
                                                       ELSE
                                                           1
                                                   END isCancel, 
                                                   a.[modify] modifyPO,
                                                   a.overflow
                                            FROM t_sp_temp a
                                                INNER JOIN T_SP b
                                                    ON a.masterPO = b.masterPO
                                                       AND a.GtnPO = b.GtnPO
                                                       AND a.po_mainLine = b.po_mainLine
                                                       AND a.styleNumber = b.styleNumber
                                                       AND a.color = b.color
                                                       AND a.org = b.org
                                                       AND a.totalQty = b.totalQty			
                                        ) AS b
                                            ON a.id = b.bid;";
            int result = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
            return result;

        }


        public int updata_T_spIsCancel()
        {
            string sql = @"UPDATE t
                                SET t.isCancel = 1
                                FROM dbo.T_SP t,
                                (
                                    SELECT a.season,
                                           a.styleNumber,
		                                   a.po_mainLine,
                                           a.color,
                                           a.totalQty,
                                           a.[modify] modifyPO
                                    FROM t_sp_temp a
                                    WHERE a.[modify] != ''
                                    GROUP BY a.season,
                                             a.styleNumber,
			                                 a.po_mainLine,
                                             a.color,
                                             a.totalQty,
                                             a.[modify]
                                ) AS b
                                WHERE t.season = b.season
                                      AND t.styleNumber = b.styleNumber
                                      AND t.po_mainLine = b.po_mainLine
                                      AND t.color = b.color
                                      AND t.GtnPO = b.modifyPO
                                      AND t.totalQty = b.totalQty; ";
            int result = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
            return result;

        }


        public int updata_T_spSize()
        {
            string sql = @"UPDATE a
                                SET a.ftyNo = b.ftyNo,
                                    a.season = b.season,
                                    a.sizeAnother = b.sizeAnother,
                                    a.sizeQty = b.sizeQty,
                                    a.isCancel = b.isCancel,
                                    a.overflow = b.overflow,
                                    a.poQty = b.poQty
                                FROM T_size a
                                    INNER JOIN
                                    (
                                        SELECT b.id AS bid,
                                               a.id AS aid,
                                               a.ftyNo,
                                               a.season,
                                               a.masterPo,
                                               a.GtnPO,
                                               a.po_mainLine,
                                               a.styleNumber,
                                               a.color,
                                               a.sizeName,
                                               a.sizeAnother,
                                               a.sizeQty,
                                               a.poQty,
                                               a.org,
                                               CASE LEN(a.isCancel)
                                                   WHEN 0 THEN
                                                       0
                                                   ELSE
                                                       1
                                               END isCancel,
                                               a.overflow
                                        FROM T_size_temp a
                                            INNER JOIN T_size b
                                                ON a.masterPo = b.masterPo
                                                   AND a.GtnPO = b.GtnPO
                                                   AND a.po_mainLine = b.po_mainLine
                                                   AND a.styleNumber = b.styleNumber
                                                   AND a.color = b.color
                                                   AND a.sizeName = b.sizeName
                                                   AND a.org = b.org
                                    ) AS b
                                        ON a.id = b.bid;";
            int result = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
            return result;

        }

        public int adddata_T_sp()
        {
            string sql = @"INSERT INTO [dbo].[T_SP]
                                                    (
                                                        [type],
                                                        [ftyNo],
                                                        [season],
                                                        [BVPO],
                                                        [masterPO],
                                                        [GtnPO],
                                                        [po_mainLine],
                                                        [styleNumber],
                                                        [styleName],
                                                        [color],
                                                        [colDescription],
                                                        [channel],
                                                        [totalQty],
                                                        [HOD],
                                                        [befoeHOD],
                                                        [newHOD],
                                                        [shipMode],
                                                        [sourceTag],
                                                        [wwwt],
                                                        [citHangTag],
                                                        [Fastener],
                                                        [steelNumber],
                                                        [cup],
                                                        [cclable],
                                                        [sensitive],
                                                        [remark],
                                                        [org],
                                                        [isCancel],
                                                        [bookingStatus],
                                                        [bookingData],
                                                        [modify],
                                                        [overflow]
                                                    )
                                                    SELECT 
	 
	                                                    a.[type],
                                                           a.ftyNo,
                                                           a.season,
                                                           a.BVPO,
                                                           a.masterPO,
                                                           a.GtnPO,
                                                           a.po_mainLine,
                                                           a.styleNumber,
                                                           a.styleName,
                                                           a.color,
                                                           a.colDescription,
                                                           a.channel,
                                                           a.totalQty,
                                                           a.HOD,
                                                           a.befoeHOD,
                                                           a.newHOD,
                                                           a.shipMode,
                                                           a.sourceTag,
                                                           a.wwwt,
                                                           a.citHangTag,
                                                           a.Fastener,
                                                           a.steelNumber,
                                                           a.cup,
                                                           a.cclable,
                                                           a.sensitive,
                                                           a.remark,
                                                           a.org,
                                                           0,
                                                           a.[bookingStatus],
                                                           a.[bookingData],
                                                           a.[modify],
                                                           a.[overflow]
                                                        FROM t_sp_temp a
                                                            LEFT JOIN T_SP b
                                                                ON a.masterPO = b.masterPO
                                                                   AND a.GtnPO = b.GtnPO
                                                                   AND a.po_mainLine = b.po_mainLine
                                                                   AND a.styleNumber = b.styleNumber
                                                                   AND a.color = b.color
                                                                   AND a.totalQty = b.totalQty 
                                                        WHERE b.id IS NULL ;";
            int result = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
            return result;

        }


        public int adddata_T_spSize()
        {
            string sql = @"INSERT INTO [dbo].[T_size]
                                                        (
                                                            ftyNo,
                                                            season,
                                                            masterPo,
                                                            GtnPO,
                                                            po_mainLine,
                                                            styleNumber,
                                                            color,
                                                            sizeName,
                                                            sizeAnother,
                                                            sizeQty,
                                                            poQty,
                                                            org,
                                                            isCancel,
                                                            overflow
                                                        )
                                                        SELECT a.ftyNo,
                                                               a.season,
                                                               a.masterPo,
                                                               a.GtnPO,
                                                               a.po_mainLine,
                                                               a.styleNumber,
                                                               a.color,
                                                               a.sizeName,
                                                               a.sizeAnother,
                                                               a.sizeQty,
                                                               a.poQty,
                                                               a.org,
                                                               CASE LEN(a.isCancel)
                                                                    WHEN 0 THEN
                                                                       0
                                                                    ELSE
                                                                       1
                                                                    END isCancel,
                                                               a.overflow
                                                        FROM T_size_temp a
                                                            LEFT JOIN T_size b
                                                                ON a.masterPo = b.masterPo
                                                                   AND a.GtnPO = b.GtnPO
                                                                   AND a.po_mainLine = b.po_mainLine
                                                                   AND a.styleNumber = b.styleNumber
                                                                   AND a.color = b.color
                                                        WHERE 1 = 1
                                                              AND b.id IS NULL;";
            int result = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
            return result;

        }


        public DataTable getShippingPackagesBySparameters(ShippingParameter sparameters)
        {
            string sql = "";
            int bookingStatus = 0;
            if (sparameters.bookingStatus)
            {
                bookingStatus = 0;
            }
            else
            {
                bookingStatus = 1;
            }
            if (sparameters.checkedDate)
            {
                sql = @"SELECT a.id AS aid,
                                       a.[type],
                                       a.ftyNo,
                                       a.season,
                                       a.BVPO,
                                       a.masterPO,
                                       a.GtnPO,
                                       a.po_mainLine,
                                       CASE a.bookingStatus
                                           WHEN 0 THEN
                                               'No'
                                           ELSE
                                               'Yes'
                                       END bookingStatus,
	                                   a.bookingData ,
                                       a.styleNumber,
                                       a.styleName,
                                       a.color,
                                       a.colDescription,
                                       a.channel,
                                       a.totalQty,
                                        a.overflow,
                                       a.HOD,
                                       a.befoeHOD,
                                       a.newHOD,
                                       a.shipMode,
                                       a.sourceTag,
                                       a.wwwt,
                                       a.citHangTag,
                                       a.Fastener,
                                       a.steelNumber,
                                       a.cup,
                                       a.cclable,
                                       a.sensitive,
                                       a.remark,
                                       a.org,
                                       a.isCancel,
	                                   a.[modify] modifyPO 
                                FROM t_sp a
                                WHERE 1 = 1
                                      AND a.org = 'SAA'
                                      AND Hod
                                      BETWEEN '" + sparameters.startDate + @"' AND '" + sparameters.stopDate + @"'
                                      AND a.bookingStatus = "+ bookingStatus.ToString() + @"
                                      AND a.gtnPO LIKE '%"+ sparameters.gtnPo+ @"%'
                                      AND a.styleNumber LIKE '%" + sparameters.styleNumber + @"%'
                                      AND a.isCancel = 0  ;";
            }
            else
            {
                sql = @"SELECT a.id AS aid,
                                       a.[type],
                                       a.ftyNo,
                                       a.season,
                                       a.BVPO,
                                       a.masterPO,
                                       a.GtnPO,
                                       a.po_mainLine,
                                       CASE a.bookingStatus
                                           WHEN 0 THEN
                                               'No'
                                           ELSE
                                               'Yes'
                                       END bookingStatus,
	                                   a.bookingData ,
                                       a.styleNumber,
                                       a.styleName,
                                       a.color,
                                       a.colDescription,
                                       a.channel,
                                       a.totalQty,
                                        a.overflow,
                                       a.HOD,
                                       a.befoeHOD,
                                       a.newHOD,
                                       a.shipMode,
                                       a.sourceTag,
                                       a.wwwt,
                                       a.citHangTag,
                                       a.Fastener,
                                       a.steelNumber,
                                       a.cup,
                                       a.cclable,
                                       a.sensitive,
                                       a.remark,
                                       a.org,
                                       a.isCancel,
	                                   a.[modify] modifyPO 
                                FROM t_sp a
                                WHERE 1 = 1
                                      AND a.org = 'SAA'                                      
                                      AND a.bookingStatus = " + bookingStatus.ToString() + @"
                                      AND a.gtnPO LIKE '%" + sparameters.gtnPo + @"%'
                                      AND a.styleNumber LIKE '%" + sparameters.styleNumber + @"%'
                                      AND a.isCancel = 0 ;";
            }
            DataTable resultDt = ShippingPackagesSqlHelper.ExcuteTable(sql);
            return resultDt;

        }


        public int updataShippingPackagesBySparameters(List<ShippingBookingStatus> sbs)
        {
            string bookingID = "";
            string NoBookingID = "";
            for (int i = 0; i < sbs.Count; i++)
            {
                if(sbs[i].BookingStatus == true)
                {
                    bookingID = bookingID + "'"+ sbs[i].id + "',";
                }
                else
                {
                    NoBookingID = NoBookingID + "'" + sbs[i].id + "',";
                }
            }
            if (bookingID.Length > 0)
            {
                bookingID = bookingID.Substring(0, bookingID.Length - 1);
            }

            if (NoBookingID.Length > 0)
            {
                NoBookingID = NoBookingID.Substring(0, NoBookingID.Length - 1);
            }
            string sql1 = " UPDATE dbo.T_SP SET bookingStatus =1 WHERE id IN ("+ bookingID + ");";
            string sql2 = " UPDATE dbo.T_SP SET bookingStatus =0 WHERE id IN (" + NoBookingID + ");";
            int c = 0;
            int s = 0;
            if (bookingID.Length > 0)
            {
                 c = ShippingPackagesSqlHelper.ExecuteNonQuery(sql1);
            }
            if (NoBookingID.Length > 0)
            {
                 s = ShippingPackagesSqlHelper.ExecuteNonQuery(sql2);
            }
           
            return c+s;

        }

        public int updataShippingPackagesBySparameters()
        {
            string sql = @"UPDATE T_SP
                            SET bookingStatus = a.bookingStatus,
                                bookingData = a.bookingData
                            FROM T_SP b,
                                 T_Booking_temp a
                            WHERE b.id = a.id; ";
          
            
               int s = ShippingPackagesSqlHelper.ExecuteNonQuery(sql);  
            return   s;

        }


        public void clear_T_Booking_temp()
        {
            string sql = @"DELETE FROM T_Booking_temp;";
                ShippingPackagesSqlHelper.ExecuteNonQuery(sql);
           

        }
        public DataTable getShippingPackagesSize(string GtnPO, string po_mainLine, string styleNumber, string color)
        {
            string sql = @"SELECT id,
                                   ftyNo,
                                   season,
                                   masterPo,
                                   GtnPO,
                                   po_mainLine,
                                   styleNumber,
                                   color,
                                   sizeName,
                                   sizeAnother,
                                   sizeQty,
                                   poQty,
                                   org
                            FROM dbo.T_size
                            WHERE GtnPO = '"+ GtnPO + @"'                                 
                                  AND po_mainLine = '"+ po_mainLine + @"'
                                  AND styleNumber = '"+ styleNumber + @"'
                                  AND color = '"+ color + @"'
                                  AND sizeQty > 0
	                              ORDER BY id";

            DataTable resultDt = ShippingPackagesSqlHelper.ExcuteTable(sql);
            return resultDt;
        }

        public DataTable getShippingPackageStatusByPO(List<ShippingPackageStatusParameter> lspsps)
        {
            string po = "";
            string color_code = "";
            string MAIN_LINE = "";
            string Buyer_Item = "";
            for (int i = 0; i < lspsps.Count; i++)
            {
                po = po + @"'" + lspsps[i].GtnPO + "',";
                color_code = color_code + @"'" + lspsps[i].color + "',";
                if(lspsps[i].po_mainLine.Length > 0)
                {
                    MAIN_LINE = MAIN_LINE + @"'" + lspsps[i].po_mainLine + "',";
                }                
                Buyer_Item = Buyer_Item + @"'" + lspsps[i].styleNumber + "',";
            }

            po = po.Substring(0,po.Length - 1);
            color_code = color_code.Substring(0, color_code.Length - 1);
            if (MAIN_LINE.Length > 3)
            {
                MAIN_LINE = MAIN_LINE.Substring(0, MAIN_LINE.Length - 1);
            }
            else
            {
                MAIN_LINE = "''";
            }            
            Buyer_Item = Buyer_Item.Substring(0, Buyer_Item.Length - 1);
            string sql = @"SELECT
	                                a.POqty,
	                                a.POboxs,
                                CASE		
		                                WHEN b.InQty IS NULL THEN
		                                0 ELSE b.InQty 
	                                END InQty,
	
                                CASE		
		                                WHEN b.InBoxs IS NULL THEN
		                                0 ELSE b.InBoxs 
	                                END InBoxs,
	
                                CASE		
		                                WHEN d.OutQty IS NULL THEN
		                                0 ELSE d.OutQty 
	                                END CHQty,
	
                                CASE		
		                                WHEN d.OutBoxs IS NULL THEN
		                                0 ELSE d.OutBoxs 
	                                END CHBoxs,
	
                                CASE		
		                                WHEN c.con_no IS NULL THEN
		                                '' ELSE c.con_no 
	                                END con_no,
	                                a.po,
	                                a.MAIN_LINE,
	                                a.Buyer_Item,
	                                a.color_code,
	                                a.pprfno,
	                                b.pprfno,
	                                c.pprfno 
                                FROM
	                                ( 
	                                SELECT
		                                a.po,
		                                a.MAIN_LINE,
		                                a.Buyer_Item,
		                                a.color_code,
		                                a.PPrfNo,
		                                b.POqty,
		                                b.POboxs 
	                                FROM
		                                (
		                                SELECT
			                                po,
			                                MAIN_LINE,
			                                Buyer_Item,
			                                color_code,
			                                max( PPrfNo ) PPrfNo 
		                                FROM
			                                (
			                                SELECT
				                                sum( qty ) POqty,
				                                COUNT( Serial_From ) POboxs,
				                                a.po,
			                                CASE
					
					                                WHEN a.MAIN_LINE IS NULL THEN
					                                '' ELSE a.MAIN_LINE 
				                                END MAIN_LINE,
	                                a.Buyer_Item,
	                                a.color_code,
	                                a.PPrfNo 
                                FROM
	                                (
	                                SELECT
		                                c.id,
		                                c.Serial_From,
		                                c.qty,
		                                c.org,
		                                c.PPrfNo,
		                                c.con_no,
		                                c.po,
		                                c.MAIN_LINE,
		                                d.Buyer_Item,
		                                d.Item_desc,
		                                d.color_code 
	                                FROM
		                                con_ppr c
		                                LEFT JOIN con_detail d ON c.PPrfNo = d.pprfno 
		                                AND d.id = c.id 
	                                WHERE
                                        c.PO IN ( " + po + @" ) 
                                        AND d.color_code IN ( " + color_code + @" ) 
                                        AND (	c.MAIN_LINE IS NULL OR c.MAIN_LINE IN ( " + MAIN_LINE + @" )) 
                                        AND d.Buyer_Item IN ( " + Buyer_Item + @" )
	                                GROUP BY
		                                c.Cust_id,
		                                c.Serial_From,
		                                c.qty,
		                                c.org,
		                                c.PPrfNo,
		                                c.con_no,
		                                c.po,
		                                c.MAIN_LINE,
		                                d.Buyer_Item,
		                                d.Item_desc,
		                                d.color_code 
	                                ) a 
                                GROUP BY
	                                a.po,
	                                a.MAIN_LINE,
	                                a.Buyer_Item,
	                                a.color_code,
	                                a.PPrfNo 
	                                ) a 
                                GROUP BY
	                                po,
	                                MAIN_LINE,
	                                Buyer_Item,
	                                color_code 
	                                ) a
	                                LEFT JOIN (
	                                SELECT
		                                sum( qty ) POqty,
		                                COUNT( Serial_From ) POboxs,
		                                a.po,
	                                CASE
			
			                                WHEN a.MAIN_LINE IS NULL THEN
			                                '' ELSE a.MAIN_LINE 
		                                END MAIN_LINE,
	                                a.Buyer_Item,
	                                a.color_code,
	                                a.PPrfNo 
                                FROM
	                                (
	                                SELECT
		                                c.id,
		                                c.Serial_From,
		                                c.qty,
		                                c.org,
		                                c.PPrfNo,
		                                c.con_no,
		                                c.po,
		                                c.MAIN_LINE,
		                                d.Buyer_Item,
		                                d.Item_desc,
		                                d.color_code 
	                                FROM
		                                con_ppr c
		                                LEFT JOIN con_detail d ON c.PPrfNo = d.pprfno 
		                                AND d.id = c.id 
	                                WHERE
                                        c.PO IN ( " + po + @" ) 
                                        AND d.color_code IN ( " + color_code + @" ) 
                                        AND (	c.MAIN_LINE IS NULL OR c.MAIN_LINE IN ( " + MAIN_LINE + @" )) 
                                        AND d.Buyer_Item IN ( " + Buyer_Item + @" )
	                                GROUP BY
		                                c.Cust_id,
		                                c.Serial_From,
		                                c.qty,
		                                c.org,
		                                c.PPrfNo,
		                                c.con_no,
		                                c.po,
		                                c.MAIN_LINE,
		                                d.Buyer_Item,
		                                d.Item_desc,
		                                d.color_code 
	                                ) a 
                                GROUP BY
	                                a.po,
	                                a.MAIN_LINE,
	                                a.Buyer_Item,
	                                a.color_code,
	                                a.PPrfNo 
	                                ) b ON a.Buyer_Item = b.Buyer_Item 
	                                AND a.MAIN_LINE = b.MAIN_LINE 
	                                AND a.po = b.po 
	                                AND a.color_code = b.color_code 
	                                AND a.PPrfNo = b.PPrfNo 
	                                ) a
	                                LEFT JOIN ( 
	                                SELECT
		                                a.InQty,
		                                a.InBoxs,
		                                a.PPrfNo,
		                                a.Buyer_Item,
		                                a.color_code,
		                                p.po,
	                                CASE			
			                                WHEN p.MAIN_LINE IS NULL THEN
			                                '' ELSE p.MAIN_LINE 
		                                END MAIN_LINE 
                                FROM
	                                (
	                                SELECT
		                                sum( d.qty ) InQty,
		                                cdd.InBoxs,
		                                d.PPrfNo,
		                                d.Buyer_Item,
		                                d.color_code 
	                                FROM
		                                con_detail d
		                                LEFT JOIN (
			                                SELECT 
			                                COUNT( a.TagNumber ) InBoxs,
			                                a.Buyer_Item,
			                                a.color_code,
			                                a.pprfno 
		                                FROM
			                                (
			                                SELECT DISTINCT
				                                c.TagNumber,
				                                c.con_no,
				                                d.Buyer_Item,
				                                d.color_code,
				                                d.id,
				                                d.pprfno 
			                                FROM
				                                (
				                                SELECT DISTINCT
					                                TagNumber,
					                                con_no 
				                                FROM
					                                inv 
				                                WHERE
					                                con_no IN (
					                                SELECT
						                                d.Serial_From 
					                                FROM
						                                con_ppr c
						                                LEFT JOIN con_detail d ON c.id = d.id 
					                                WHERE
                                                        c.PO IN ( " + po + @" ) 
                                                        AND d.color_code IN ( " + color_code + @" ) 
                                                        AND (	c.MAIN_LINE IS NULL OR c.MAIN_LINE IN ( " + MAIN_LINE + @" )) 
                                                        AND d.Buyer_Item IN ( " + Buyer_Item + @" )
					                                GROUP BY
						                                d.Buyer_Item,
						                                d.Serial_From,
						                                d.pprfno,
						                                d.color_code 
					                                ) 
					
				                                ) c
				                                LEFT JOIN con_detail d ON c.con_no = d.Serial_From  
				
			                                ) a 
		                                GROUP BY
			                                a.Buyer_Item,
			                                a.color_code,
			                                a.pprfno  
			
		                                ) cdd ON cdd.pprfno = d.pprfno 
		                                AND cdd.color_code = d.color_code 
		                                AND cdd.Buyer_Item = d.Buyer_Item 
	                                WHERE
		                                Serial_From IN (
			                                SELECT 
			                                i.con_no 
		                                FROM
			                                inv i 
		                                WHERE
			                                con_no IN (
				                                SELECT 
				                                c.Serial_From 
			                                FROM
				                                con_ppr c
				                                LEFT JOIN con_detail d ON c.PPrfNo = d.pprfno 
				                                AND d.id = c.id 
			                                WHERE
                                                c.PO IN ( " + po + @" ) 
                                                AND d.color_code IN ( " + color_code + @" ) 
                                                AND (	c.MAIN_LINE IS NULL OR c.MAIN_LINE IN ( " + MAIN_LINE + @" )) 
                                                AND d.Buyer_Item IN ( " + Buyer_Item + @" )
			                                GROUP BY
				                                c.Cust_id,
				                                c.Serial_From,
				                                c.qty,
				                                c.org,
				                                c.PPrfNo,
				                                c.con_no,
				                                c.po,
				                                c.MAIN_LINE,
				                                d.Buyer_Item,
				                                d.Item_desc,
				                                d.color_code  
				
			                                ) 
		                                GROUP BY
			                                i.TagNumber,
			                                i.Cust_id,
			                                i.org,
			                                i.con_no  
			
		                                ) 
	                                GROUP BY
		                                d.pprfno,
		                                d.color_code 
	                                ) a
	                                LEFT JOIN con_ppr p ON a.pprfno = p.PPrfNo 
                                GROUP BY
	                                PPrfNo,
	                                po,
	                                MAIN_LINE,
	                                color_code 
	                                ) b ON a.po = b.po 
	                                AND a.MAIN_LINE = b.MAIN_LINE 
	                                AND a.Buyer_Item = b.Buyer_Item 
	                                AND a.color_code = b.color_code 
	                                AND a.PPrfNo = b.PPrfNo
	                                LEFT JOIN (
		                                SELECT 
		                                group_concat( a.con_no SEPARATOR ',' ) AS 'con_no',
		                                a.po,
	                                CASE
			
			                                WHEN a.MAIN_LINE IS NULL THEN
			                                '' ELSE a.MAIN_LINE 
		                                END MAIN_LINE,
	                                a.PPrfNo,
	                                a.Buyer_Item,
	                                a.color_code 
                                FROM
	                                (
		                                SELECT 
		                                c.Serial_From,
		                                c.con_no,
		                                c.po,
		                                c.MAIN_LINE,
		                                c.PPrfNo,
		                                d.Buyer_Item,
		                                d.color_code 
	                                FROM
		                                con_ppr c
		                                LEFT JOIN con_detail d ON c.PPrfNo = d.pprfno 
		                                AND d.id = c.id 
	                                WHERE
                                        c.PO IN ( " + po + @" ) 
                                        AND d.color_code IN ( " + color_code + @" ) 
                                        AND (	c.MAIN_LINE IS NULL OR c.MAIN_LINE IN ( " + MAIN_LINE + @" )) 
                                        AND d.Buyer_Item IN ( " + Buyer_Item + @" )
	                                GROUP BY
		                                c.Cust_id,
		                                c.Serial_From,
		                                c.qty,
		                                c.org,
		                                c.PPrfNo,
		                                c.con_no,
		                                c.po,
		                                c.MAIN_LINE,
		                                d.Buyer_Item,
		                                d.Item_desc,
		                                d.color_code  
		
	                                ) A
	                                LEFT JOIN (
		                                SELECT 
		                                i.con_no 
	                                FROM
		                                inv i 
	                                WHERE
		                                con_no IN (
		                                SELECT
			                                c.Serial_From 
			
		                                FROM
			                                con_ppr c
			                                LEFT JOIN con_detail d ON c.PPrfNo = d.pprfno 
			                                AND d.id = c.id 
		                                WHERE
                                            c.PO IN ( " + po + @" ) 
                                            AND d.color_code IN ( " + color_code + @" ) 
                                            AND (	c.MAIN_LINE IS NULL OR c.MAIN_LINE IN ( " + MAIN_LINE + @" )) 
                                            AND d.Buyer_Item IN ( " + Buyer_Item + @" )
		                                GROUP BY
			                                c.Cust_id,
			                                c.Serial_From,
			                                c.qty,
			                                c.org,
			                                c.PPrfNo,
			                                c.con_no,
			                                c.po,
			                                c.MAIN_LINE,
			                                d.Buyer_Item,
			                                d.Item_desc,
			                                d.color_code 			
		                                ) 
	                                GROUP BY
		                                i.TagNumber,
		                                i.Cust_id,
		                                i.org,
		                                i.con_no  		
	                                ) b ON a.Serial_From = b.con_no 
                                WHERE
	                                B.con_no IS NULL 
                                GROUP BY
	                                po,
	                                MAIN_LINE,
	                                a.PPrfNo,
	                                a.Buyer_Item,
	                                a.color_code 
                                ORDER BY
	                                a.con_no  
	
	                                ) c ON a.po = c.po 
	                                AND a.MAIN_LINE = c.MAIN_LINE 
	                                AND a.Buyer_Item = c.Buyer_Item 
	                                AND a.color_code = c.color_code 
	                                AND a.PPrfNo = c.PPrfNo
	                                LEFT JOIN ( 
	                                SELECT
		                                a.OutQty,
		                                a.OutBoxs,
		                                a.PPrfNo,
		                                a.Buyer_Item,
		                                a.color_code,
		                                p.po,
	                                CASE
			
			                                WHEN p.MAIN_LINE IS NULL THEN
			                                '' ELSE p.MAIN_LINE 
		                                END MAIN_LINE 
                                FROM
	                                (
	                                SELECT
		                                sum( d.qty ) OutQty,
	                                CASE
			
			                                WHEN cdd.OutBoxs IS NULL THEN
			                                0 ELSE cdd.OutBoxs 
		                                END OutBoxs,
	                                d.PPrfNo,
	                                d.Buyer_Item,
	                                d.color_code 
                                FROM
	                                con_detail d
	                                LEFT JOIN (
		                                SELECT 
		                                COUNT( a.TagNumber ) OutBoxs,
		                                a.Buyer_Item,
		                                a.color_code,
		                                a.pprfno 
	                                FROM
		                                (
			                                SELECT DISTINCT 
			                                c.TagNumber,
			                                c.con_no,
			                                d.Buyer_Item,
			                                d.color_code,
			                                d.id,
			                                d.pprfno 
		                                FROM
			                                (
				                                SELECT DISTINCT 
				                                TagNumber,
				                                con_no 
			                                FROM
				                                inv 
			                                WHERE
				                                con_no IN (
				                                SELECT
					                                d.Serial_From 
				                                FROM
					                                con_ppr c
					                                LEFT JOIN con_detail d ON c.id = d.id 
				                                WHERE
                                                    c.PO IN ( " + po + @" ) 
                                                    AND d.color_code IN ( " + color_code + @" ) 
                                                    AND (	c.MAIN_LINE IS NULL OR c.MAIN_LINE IN ( " + MAIN_LINE + @" )) 
                                                    AND d.Buyer_Item IN ( " + Buyer_Item + @" )
				                                GROUP BY
					                                d.Buyer_Item,
					                                d.Serial_From,
					                                d.pprfno,
					                                d.color_code 
				                                ) 
				                                AND location = 'CH' 
				
			                                ) c
			                                LEFT JOIN con_detail d ON c.con_no = d.Serial_From  
			
		                                ) a 
	                                GROUP BY
		                                a.Buyer_Item,
		                                a.color_code,
		                                a.pprfno  
		
	                                ) cdd ON cdd.pprfno = d.pprfno 
	                                AND cdd.color_code = d.color_code 
	                                AND cdd.Buyer_Item = d.Buyer_Item 
                                WHERE
	                                Serial_From IN (
		                                SELECT 
		                                i.con_no 
	                                FROM
		                                inv i 
	                                WHERE
		                                con_no IN (
			                                SELECT 
			                                c.Serial_From 
		                                FROM
			                                con_ppr c
			                                LEFT JOIN con_detail d ON c.PPrfNo = d.pprfno 
			                                AND d.id = c.id 
		                                WHERE
                                            c.PO IN ( " + po + @" ) 
                                            AND d.color_code IN ( " + color_code + @" ) 
                                            AND (	c.MAIN_LINE IS NULL OR c.MAIN_LINE IN ( " + MAIN_LINE + @" )) 
                                            AND d.Buyer_Item IN ( " + Buyer_Item + @" )
		                                GROUP BY
			                                c.Cust_id,
			                                c.Serial_From,
			                                c.qty,
			                                c.org,
			                                c.PPrfNo,
			                                c.con_no,
			                                c.po,
			                                c.MAIN_LINE,
			                                d.Buyer_Item,
			                                d.Item_desc,
			                                d.color_code 
			
		                                ) 
		                                AND i.location = 'CH' 
	                                GROUP BY
		                                i.TagNumber,
		                                i.Cust_id,
		                                i.org,
		                                i.con_no  
		
	                                ) 
                                GROUP BY
	                                d.pprfno,
	                                d.color_code 
	                                ) a
	                                LEFT JOIN con_ppr p ON a.pprfno = p.PPrfNo 
                                GROUP BY
	                                PPrfNo,
	                                po,
	                                MAIN_LINE,
	                                color_code 
	                                ) d ON a.po = d.po 
	                                AND a.MAIN_LINE = d.MAIN_LINE 
	                                AND a.Buyer_Item = d.Buyer_Item 
	                                AND a.color_code = d.color_code 
	                                AND a.PPrfNo = d.PPrfNo 
                                WHERE
	                                b.InQty > 0 
	                                AND b.InBoxs > 0 
	                                AND a.POqty >0 ";
            DataTable resultDt = MyCatfsg_SqlHelper.ExcuteTable(sql);
            return resultDt;

        }


        public string SqlBulkToSQL_T_Booking_temp(DataTable changedShippingBookingStatusDT)
        {
            string mes =  sph.SqlBulkToSQL_T_Booking_temp(changedShippingBookingStatusDT);
            return mes;
        }

    }
}
