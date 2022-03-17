using MODEL;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CFOutputService
    {
        public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
        public DataTable getCFoutPut(CFOutput cfoutput)
        {
            int searchType = cfoutput.searchType;
            string org = cfoutput.org;
            string subinv = cfoutput.subinv;
            string style = cfoutput.style;
            string startDate = cfoutput.starDate;
            string stopDate = cfoutput.stopDate;
            bool checkDate = cfoutput.checkDate;
            
            string sql = "";
            DataTable dt = new DataTable();

            if (searchType == 0)
            {

                if (!checkDate && style.Length > 0)
                {
                    sql = @"
                        SELECT   shb.SHB02 AS CreateDate,  occ.occ02 as custName,sfb.TA_SFB01 AS myNumber,shb.shb10 as style ,shb.shb09 as line  ,
												sfb.sfb08 as OrderQty,sum(shb.shb111)  as qty ,                   
                        oea.TA_OEA01 as season,oea.ta_oea02 as  buy,                       
                        ima.ima02 as name
                        FROM " + org + @".SHB_FILE  shb
                         LEFT JOIN   " + org + @".SFB_FILE  sfb  on shb.shb05  = sfb.sfb01 
                         LEFT JOIN  " + org + @".OEA_FILE oea on oea.oea01 = sfb.sfb22
                         LEFT JOIN " + org + @".OCC_FILE occ on occ.occ07 = sfb.sfb223
                         LEFT JOIN " + org + @".IMA_FILE  ima on ima.ima01 =  shb.shb10
                        WHERE    1 = 1   
                        and sfb.sfb05 ='" + cfoutput.style + @"'
                        and shb.shb081 ='G022'  
                        and shb.SHBCONF ='Y'
                        GROUP BY  shb.SHB02, shb.shb05 ,shb.shb081  ,shb.shb10  ,shb.shb09  ,shb.shb07   ,
                        sfb.TA_SFB01 ,sfb.sfb05 ,sfb.sfb08 ,sfb.sfb22 ,sfb.sfb223 ,
                        oea.TA_OEA01 ,oea.ta_oea02 ,
                        occ.occ02 ,
                        ima.ima02 
                        ORDER BY  shb.SHB02 ,shb.shb09,oea.ta_oea02 ,sfb.sfb05,sfb.sfb22";
                }


                if (checkDate && style.Length > 0)
                {
                    sql = @"
                        SELECT   shb.SHB02 AS CreateDate,  occ.occ02 as custName,sfb.TA_SFB01 AS myNumber,shb.shb10 as style ,shb.shb09 as line  ,
												sfb.sfb08 as OrderQty,sum(shb.shb111)  as qty ,                   
                        oea.TA_OEA01 as season,oea.ta_oea02 as  buy,                       
                        ima.ima02 as name
                        FROM " + org + @".SHB_FILE  shb
                         LEFT JOIN " + org + @".SFB_FILE  sfb  on shb.shb05  = sfb.sfb01 
                         LEFT JOIN " + org + @".OEA_FILE oea on oea.oea01 = sfb.sfb22
                         LEFT JOIN " + org + @".OCC_FILE occ on occ.occ07 = sfb.sfb223
                         LEFT JOIN " + org + @".IMA_FILE  ima on ima.ima01 =  shb.shb10
                        WHERE    1 = 1   
                        and sfb.sfb05 ='" + cfoutput.style + @"'
                        and shb.SHB02 BETWEEN  TO_DATE('" + startDate + @"', 'yyyy-MM-dd') and   TO_DATE('" + stopDate + @"', 'yyyy-MM-dd')
                        and shb.shb081 ='G022'  
                        and shb.SHBCONF ='Y'
                        GROUP BY  shb.SHB02, shb.shb05 ,shb.shb081  ,shb.shb10  ,shb.shb09  ,shb.shb07   ,
                        sfb.TA_SFB01 ,sfb.sfb05 ,sfb.sfb08 ,sfb.sfb22 ,sfb.sfb223 ,
                        oea.TA_OEA01 ,oea.ta_oea02 ,
                        occ.occ02 ,
                        ima.ima02 
                        ORDER BY  shb.SHB02 ,shb.shb09,oea.ta_oea02 ,sfb.sfb05,sfb.sfb22";
                }

                if (checkDate && style.Length <= 0)
                {
                    sql = @"
                        SELECT   shb.SHB02 AS CreateDate,  occ.occ02 as custName,sfb.TA_SFB01 AS myNumber,shb.shb10 as style ,shb.shb09 as line  ,
												sfb.sfb08 as OrderQty,sum(shb.shb111)  as qty ,                   
                        oea.TA_OEA01 as season,oea.ta_oea02 as  buy,                       
                        ima.ima02 as name
                        FROM " + org + @".SHB_FILE  shb
                         LEFT JOIN " + org + @".SFB_FILE  sfb  on shb.shb05  = sfb.sfb01 
                         LEFT JOIN " + org + @".OEA_FILE oea on oea.oea01 = sfb.sfb22
                         LEFT JOIN " + org + @".OCC_FILE occ on occ.occ07 = sfb.sfb223
                         LEFT JOIN " + org + @".IMA_FILE  ima on ima.ima01 =  shb.shb10
                        WHERE    1 = 1                          
                        and shb.SHB02 BETWEEN  TO_DATE('" + startDate + @"', 'yyyy-MM-dd') and   TO_DATE('" + stopDate + @"', 'yyyy-MM-dd')
                        and shb.shb081 ='G022'  
                        and shb.SHBCONF ='Y'
                        GROUP BY  shb.SHB02, shb.shb05 ,shb.shb081  ,shb.shb10  ,shb.shb09  ,shb.shb07 ,
                        sfb.TA_SFB01 ,sfb.sfb05 ,sfb.sfb08 ,sfb.sfb22 ,sfb.sfb223 ,
                        oea.TA_OEA01 ,oea.ta_oea02 ,
                        occ.occ02 ,
                        ima.ima02 
                        ORDER BY  shb.SHB02 ,shb.shb09,oea.ta_oea02 ,sfb.sfb05,sfb.sfb22";
                }
                 dt = ERP_SqlHelper.ExcuteTable(sql);
                

            }
            else if( searchType == 1)
            {
                if (checkDate && style.Length > 0)
                {
                    sql = @"
                
							  SELECT i.org,DATE_FORMAT(i.scantime, '%y-%m-%d') scantime ,i.Cust_id,   c.PO,c.MAIN_LINE,
								 d.Buyer_Item,d.color_code,d.Size1 ,  CASE 	WHEN d.qty is null THEN	 0 	ELSE 		 sum(d.qty)  END   qty ,
								 d.Item_desc,d.pprfno , i.location
							 from inv i , (
							 SELECT con_no,min(ScanTime) ScanTime  FROM inv  i WHERE  con_no in (SELECT con_no FROM inv  i WHERE 
											i.scantime BETWEEN '" + startDate +@"'  and '" + stopDate + @"'
	                              and  i.org ='" + org + @"'
							      and  (i.subinv ='" + subinv + @"'   AND i.location ='HD' ) 
							 GROUP BY i.con_no )  GROUP BY i.con_no )  a		
							 left join 		con_ppr	 c on  a.con_no =c.Serial_From	 
							 left join 		 con_detail	 d on  a.con_no = d.Serial_From			
							 WHERE i.con_no=a. con_no and i.ScanTime=a.ScanTime 
                                and  i.org ='" + org + @"'
								and  a.scantime BETWEEN  '" + startDate + @"'  and '" + stopDate + @"'
								and (i.subinv   ='" + subinv + @"'     AND i.location ='HD'  )	
                                and  d.Buyer_Item ='"+ style + @"'
	                            and d.qty != 0
							 GROUP BY  DATE_FORMAT(i.scantime, '%y-%m-%d')   ,i.org,i.Cust_id     , i.location,
							 d.Buyer_Item,d.Item_desc,d.color_code,d.Size1 ,
							 c.PO,c.MAIN_LINE,
							 d.pprfno							 
							 ORDER BY  DATE_FORMAT(i.scantime, '%y-%m-%d'),d.Buyer_Item ,d.color_code,d.Size1 ,c.PO,c.MAIN_LINE,i.Cust_id,i.Location,d.pprfno ;
                            ";
                }else if (checkDate && style.Length <= 0)
                {
                    sql = @"
                
							  SELECT i.org,DATE_FORMAT(i.scantime, '%y-%m-%d') scantime ,i.Cust_id,   c.PO,c.MAIN_LINE,
								 d.Buyer_Item,d.color_code,d.Size1 ,  CASE 	WHEN d.qty is null THEN	 0 	ELSE 		 sum(d.qty)  END   qty ,
								 d.Item_desc,d.pprfno , i.location
							 from inv i , (
							 SELECT con_no,min(ScanTime) ScanTime  FROM inv  i WHERE  con_no in (SELECT con_no FROM inv  i WHERE 
											i.scantime BETWEEN '" + startDate + @"'  and '" + stopDate + @"'
	                              and  i.org ='" + org + @"'
							      and  (i.subinv ='" + subinv + @"'   AND i.location ='HD' ) 
							 GROUP BY i.con_no )  GROUP BY i.con_no )  a		
							 left join 		con_ppr	 c on  a.con_no =c.Serial_From	 
							 left join 		 con_detail	 d on  a.con_no = d.Serial_From			
							 WHERE i.con_no=a. con_no and i.ScanTime=a.ScanTime 
                                and  i.org ='" + org + @"'
								and  a.scantime BETWEEN  '" + startDate + @"'  and '" + stopDate + @"'
								and (i.subinv   ='" + subinv + @"'     AND i.location ='HD'  )	
                                and d.qty != 0
							 GROUP BY  DATE_FORMAT(i.scantime, '%y-%m-%d')   ,i.org,i.Cust_id     , i.location,
							 d.Buyer_Item,d.Item_desc,d.color_code,d.Size1 ,
							 c.PO,c.MAIN_LINE,
							 d.pprfno							 
							 ORDER BY  DATE_FORMAT(i.scantime, '%y-%m-%d'),d.Buyer_Item ,d.color_code,d.Size1 ,c.PO,c.MAIN_LINE,i.Cust_id,i.Location,d.pprfno ;
                            ";

                }
                dt = new DataTable();
                if (MiddleWare == "1")
                {
                    dt = MyCatfsg_SqlHelper.ExcuteTable(sql);
                }
                else
                {
                    dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
                }

                

            }
            else if( searchType ==2)
            {
                if (checkDate && style.Length > 0)
                {
                    sql = @"
                
							  SELECT  i.org,DATE_FORMAT(i.scantime, '%y-%m-%d') scantime ,i.Cust_id,   c.PO,c.MAIN_LINE,
								 d.Buyer_Item,d.color_code,d.Size1 ,  CASE 	WHEN d.qty is null THEN	 0 	ELSE 		 sum(d.qty)  END   qty ,
								 d.Item_desc,d.pprfno , i.location
							 from inv i , (
							 SELECT con_no,max(ScanTime) ScanTime  FROM inv  i WHERE  con_no in (SELECT con_no FROM inv  i WHERE 
											i.scantime BETWEEN '" + startDate + @"'  and '" + stopDate + @"'
	                              and  i.org ='" + org + @"'
							      and  (i.subinv ='" + subinv + @"'   AND i.location !='GD' ) 
							 GROUP BY i.con_no )  GROUP BY i.con_no )  a		
							 left join 		con_ppr	 c on  a.con_no =c.Serial_From	 
							 left join 		 con_detail	 d on  a.con_no = d.Serial_From			
							 WHERE i.con_no=a. con_no and i.ScanTime=a.ScanTime 
                                and  i.org ='" + org + @"'
								and  a.scantime BETWEEN  '" + startDate + @"'  and '" + stopDate + @"'
								and (i.subinv   ='" + subinv + @"'     AND i.location !='GD'  )	
                                and  d.Buyer_Item ='" + style + @"'
                                and d.qty != 0
							 GROUP BY  DATE_FORMAT(i.scantime, '%y-%m-%d')   ,i.org,i.Cust_id     , i.location,
							 d.Buyer_Item,d.Item_desc,d.color_code,d.Size1 ,
							 c.PO,c.MAIN_LINE,
							 d.pprfno							 
							 ORDER BY DATE_FORMAT(i.scantime, '%y-%m-%d'),d.Buyer_Item ,d.color_code,d.Size1 ,c.PO,c.MAIN_LINE,i.Cust_id,i.Location,d.pprfno  ;
                            ";
                }
                else if (checkDate && style.Length <= 0)
                {
                    sql = @"
                
							  SELECT  i.org,DATE_FORMAT(i.scantime, '%y-%m-%d') scantime ,i.Cust_id,   c.PO,c.MAIN_LINE,
								 d.Buyer_Item,d.color_code,d.Size1 ,  CASE 	WHEN d.qty is null THEN	 0 	ELSE 		 sum(d.qty)  END   qty ,
								 d.Item_desc,d.pprfno , i.location
							 from inv i , (
							 SELECT con_no,max(ScanTime) ScanTime  FROM inv  i WHERE  con_no in (SELECT con_no FROM inv  i WHERE 
											i.scantime BETWEEN '" + startDate + @"'  and '" + stopDate + @"'
	                              and  i.org ='" + org + @"'
							      and  (i.subinv ='" + subinv + @"'   AND i.location  !='GD'  ) 
							 GROUP BY i.con_no )  GROUP BY i.con_no )  a		
							 left join 		con_ppr	 c on  a.con_no =c.Serial_From	 
							 left join 		 con_detail	 d on  a.con_no = d.Serial_From			
							 WHERE i.con_no=a. con_no and i.ScanTime=a.ScanTime 
                                and  i.org ='" + org + @"'
								and  a.scantime BETWEEN  '" + startDate + @"'  and '" + stopDate + @"'
								and (i.subinv   ='" + subinv + @"'     AND i.location  !='GD'   )	
                                 and d.qty != 0
							 GROUP BY  DATE_FORMAT(i.scantime, '%y-%m-%d')   ,i.org,i.Cust_id     , i.location,
							 d.Buyer_Item,d.Item_desc,d.color_code,d.Size1 ,
							 c.PO,c.MAIN_LINE,
							 d.pprfno							 
							 ORDER BY  DATE_FORMAT(i.scantime, '%y-%m-%d'),d.Buyer_Item ,d.color_code,d.Size1 ,c.PO,c.MAIN_LINE,i.Cust_id,i.Location,d.pprfno ;
                            ";

                }
                dt = new DataTable();
                if (MiddleWare == "1")
                {
                    dt = MyCatfsg_SqlHelper.ExcuteTable(sql);
                }
                else
                {
                    dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
                }


            }

            return dt;

        }



        public DataTable getSubinv(string org,int searchType)
        {
            string sql = "";
            if (searchType == 1)
            {
                 
                sql = "SELECT subinv from location WHERE org ='" + org + @"' and subinv like '%HD%' GROUP BY subinv";
            }
            else
            {
                sql = "SELECT subinv from location WHERE org ='" + org + @"' and subinv not like '%HD%' GROUP BY subinv";
            }
            
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
