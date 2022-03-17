using MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CompeleteService
    {
      
        public List<CompeleteMes> getMesScanData(string style, string myNumber, string stardate, string stopdate)
        {

            string sql = @"SELECT a.orderSKU,
                                   tbb.my_no,                                   
                                   a.productModel,
                                   b.partName,
                                   SUM(b.SIZ_MULTIPLIER * b.Actual_HEIGHT) QTY,
                                   convert(varchar(11), a.sysAddTime, 23)AS sysAddTime
                            FROM t_ManufactureOrderList a
                                INNER JOIN t_ManufactureOrderDetails b
                                    ON a.MOLID = b.MOLID
                                LEFT JOIN
                                (
                                      SELECT orderSKU,
                                           my_no,
                                           style_id
                                    FROM dbo.t_BestBand
                                    WHERE orderSKU LIKE  '%" + myNumber + @"%'
		                            AND  style_id LIKE '%"+ style + @"%'
                                    GROUP BY orderSKU,
                                             my_no,
                                             style_id
                                ) tbb
                                    ON tbb.orderSKU = a.orderSKU
                                       AND tbb.style_id = a.productModel
                            WHERE CONVERT(DATE, a.sysAddTime, 23) >= CONVERT(DATE, '"+ stardate + @"', 23)
                                  AND CONVERT(DATE, a.sysAddTime, 23) <= CONVERT(DATE, '" + stopdate + @"', 23)
                                  AND b.partName = 'A'
                                  AND a.orderSKU  LIKE  '%" + myNumber + @"%'
	                              AND a.productModel LIKE   '%" + style + @"%'
                            GROUP BY a.orderSKU,
                                     tbb.my_no,
                                     a.productModel,
                                     b.partName,
                                      convert(varchar(11), a.sysAddTime, 23)
                            ORDER BY 
                                     a.orderSKU,
                                     tbb.my_no,
                                     sysAddTime;	  ";

            List<CompeleteMes> lists = new List<CompeleteMes>();
            DataTable SAAdt = Mes_SqlHelper.ExcuteTable(sql, "SAA");            
            if (SAAdt.Rows.Count > 0)
            {               
                foreach (DataRow row in SAAdt.Rows)
                {
                    CompeleteMes c = new CompeleteMes();
                    cMes(row, c);
                    lists.Add(c);
                }
            }

            DataTable TOPdt = Mes_SqlHelper.ExcuteTable(sql, "TOP");             
            if (TOPdt.Rows.Count > 0)
            {                
                foreach (DataRow row in TOPdt.Rows)
                {
                    CompeleteMes c = new CompeleteMes();
                    cMes(row, c);
                    lists.Add(c);
                }
            }
            return lists;
        }


        public List<CompeleteMes> getMesScanData(string style, string myNumber)
        {
            string sql = @"SELECT a.orderSKU,
                                   tbb.my_no,
                                   a.productModel,
                                   b.partName,
                                   SUM(b.SIZ_MULTIPLIER * b.Actual_HEIGHT) QTY,
                                   convert(varchar(11), a.sysAddTime, 23) AS sysAddTime
                            FROM t_ManufactureOrderList a
                                INNER JOIN t_ManufactureOrderDetails b
                                    ON a.MOLID = b.MOLID
                                LEFT JOIN
                                (
                                      SELECT orderSKU,
                                           my_no,
                                           style_id
                                    FROM dbo.t_BestBand
                                    WHERE orderSKU LIKE  '%" + myNumber + @"%'
		                            AND  style_id LIKE '%" + style + @"%'
                                    GROUP BY orderSKU,
                                             my_no,
                                             style_id
                                ) tbb
                                    ON tbb.orderSKU = a.orderSKU
                                       AND tbb.style_id = a.productModel
                            WHERE  
                                    b.partName = 'A'
                                  AND a.orderSKU  LIKE  '%" + myNumber + @"%'
	                              AND a.productModel LIKE   '%" + style + @"%'
                            GROUP BY a.orderSKU,
                                     tbb.my_no,
                                     a.productModel,
                                     b.partName,
                                     convert(varchar(11), a.sysAddTime, 23)
                            ORDER BY 
                                     a.orderSKU,
                                     tbb.my_no,
                                     sysAddTime;	  ";

            List<CompeleteMes> lists = new List<CompeleteMes>();
            DataTable SAAdt = Mes_SqlHelper.ExcuteTable(sql, "SAA");           
            if (SAAdt.Rows.Count > 0)
            {               
                foreach (DataRow row in SAAdt.Rows)
                {
                    CompeleteMes c = new CompeleteMes();
                    cMes(row, c);
                    lists.Add(c);
                }
            }
            DataTable TOPdt = Mes_SqlHelper.ExcuteTable(sql, "TOP");           
            if (SAAdt.Rows.Count > 0)
            {               
                foreach (DataRow row in TOPdt.Rows)
                {
                    CompeleteMes c = new CompeleteMes();
                    cMes(row, c);
                    lists.Add(c);
                }
            }
            return lists;
        }


        public void cMes(DataRow dr, MODEL.CompeleteMes list)
        {
            list.orderSKU = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["orderSKU"]));
            list.my_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["my_no"]));
            list.productModel = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["productModel"]));
            list.partName = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["partName"]));
            list.QTY = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["QTY"]));
            list.sysAddTime = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["sysAddTime"]));
        }



        public List<CompeleteERP> getERPData(List<string> myNumbers)
        {
            string Sql_myNumbers = "";
            foreach (string myNumber in myNumbers)
            {
                Sql_myNumbers = Sql_myNumbers + "'" +myNumber +"',"; 
            }
            Sql_myNumbers = Sql_myNumbers.Substring(0, Sql_myNumbers.Length - 1);

            string sql = @"SELECT shb.shbplant as org,
                                   sfb.TA_SFB01 AS myNumber,
                                   sfb.sfb22    AS orderID,
                                   shb.shb01    as moveOrderID,
                                   shb.shb05    as finishID,
                                   oea.oea03    as custID,
                                   oea.oea032   as custName,
                                   sfb.sfb05    AS Style,
                                   eca.eca02    as taskProcessName,
                                   sfb.sfb08    AS OrderQty,
                                   ecm.ecm65    as makeQty,
                                    to_char(shb02,'YYYY-MM-DD')       as FinishDate,
                                   shb.shb04    as emploreeID,
                                   ecm.ecm45    as caption,
                                   shb.shb06    as ProcessID,
                                   shb.shb07    as processName,
                                   shb.shb08    as lineID,
                                   shb.shb081   as WorkID,
                                   shb.shb09    as workMachineID,
                                   shb.shb111   as finishQty,
                                   shb.shb115   as BonusQty,
                                   shb.shbacti  as checkedID
                            FROM SAA.SFB_FILE sfb
                                     LEFT JOIN SAA.OEA_FILE oea ON oea.oea01 = sfb.sfb22
                                     left join SAA.ECM_FILE ecm ON ecm.ecm01 = sfb.SFB01
                                     left join SAA.ECA_FILE eca ON eca.ECA01 = ecm.ecm06
                                     LEFT JOIN SAA.SHB_FILE shb on shb.SHB05 = sfb.SFB01 and shb.shb07 = sfb.ecm.ecm06
                            WHERE sfb.TA_SFB01 in (" + Sql_myNumbers + @")
                              and ecm.ecm06 = 'CJ'
                            union

                            SELECT shb.shbplant as org,
                                   sfb.TA_SFB01 AS myNumber,
                                   sfb.sfb22    AS orderID,
                                   shb.shb01    as moveOrderID,
                                   shb.shb05    as finishID,
                                   oea.oea03    as custID,
                                   oea.oea032   as custName,
                                   sfb.sfb05    AS Style,
                                   eca.eca02    as taskProcessName,
                                   sfb.sfb08    AS OrderQty,
                                   ecm.ecm65    as makeQty,
                                    to_char(shb02,'YYYY-MM-DD')        as FinishDate,
                                   shb.shb04    as emploreeID,
                                   ecm.ecm45    as caption,
                                   shb.shb06    as ProcessID,
                                   shb.shb07    as processName,
                                   shb.shb08    as lineID,
                                   shb.shb081   as WorkID,
                                   shb.shb09    as workMachineID,
                                   shb.shb111   as finishQty,
                                   shb.shb115   as BonusQty,
                                   shb.shbacti  as checkedID
                            FROM TOP.SFB_FILE sfb
                                     LEFT JOIN TOP.OEA_FILE oea ON oea.oea01 = sfb.sfb22
                                     left join TOP.ECM_FILE ecm ON ecm.ecm01 = sfb.SFB01
                                     left join TOP.ECA_FILE eca ON eca.ECA01 = ecm.ecm06
                                     LEFT JOIN TOP.SHB_FILE shb on shb.SHB05 = sfb.SFB01 and shb.shb07 = sfb.ecm.ecm06
                            WHERE sfb.TA_SFB01 in (" + Sql_myNumbers + @")
                              and ecm.ecm06 = 'CJ' ";

           // sql = "  SELECT  sfb01  FROM TOP.SFB_FILE sfb  WHERE sfb.TA_SFB01 ='NKB-21-01-0322'";

            List<CompeleteERP> lists = new List<CompeleteERP>();
            DataTable ERPdt = ERP_SqlHelper.ExcuteTable(sql);
            if (ERPdt.Rows.Count > 0)
            {
                foreach (DataRow row in ERPdt.Rows)
                {
                    CompeleteERP c = new CompeleteERP();
                    cERP(row, c);
                    lists.Add(c);
                }
            }
            
            return lists;
        }

        public void cERP(DataRow dr, MODEL.CompeleteERP list)
        {
            list.org = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["org"]));
            list.myNumber = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["myNumber"]));
            list.orderID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["orderID"]));
            list.moveOrderID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["moveOrderID"]));
            list.finishID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["finishID"]));
            list.custID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["custID"]));
            list.custName = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["custName"]));

            list.Style = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["Style"]));
            list.taskProcessName = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["taskProcessName"]));
            list.OrderQty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["OrderQty"]));
            list.makeQty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["makeQty"]));
            list.FinishDate = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FinishDate"]));
            list.emploreeID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["emploreeID"]));
            list.caption = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["caption"]));

            list.ProcessID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["ProcessID"]));
            list.processName = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["processName"]));
            list.lineID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["lineID"]));
            list.WorkID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["WorkID"]));
            list.workMachineID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["workMachineID"]));
            list.finishQty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["finishQty"]));
            list.BonusQty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["BonusQty"]));
            list.checkedID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["checkedID"]));
        }
    }
}
