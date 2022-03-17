using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductsFullSearchService
    {
        public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
        public DataTable getInventoryByStylePO(List<string> styles, List<List<string>> spo)
        {
            if (styles.Count <= 0 || spo.Count <= 0)
            {
                return null;
            }
            string buyerItems = "";
            string PPrfNobuyerItems = "";
            string po = "";

            for (int i = 0; i < styles.Count; i++)
            {
				string stylePos = "";

				if (spo[i].Count > 0)
				{
					for (int j = 0; j < spo[i].Count; j++)
					{
						stylePos = stylePos + ",'" + spo[i][j] + "'";

					}
					stylePos = stylePos.Substring(1);

				}
				po = po + @" (d.Buyer_Item = '"+ styles[i]+ @"'  and  c.PO in ("+ stylePos  + @"))  or  ";
				
				buyerItems = buyerItems + @"SELECT
													 
													org,
													Subinv,
													con_no,
													TagNumber,
													Cust_id,
													location,
													scantime
												FROM
													(
													SELECT
														c.Serial_From
													FROM
														(SELECT PPrfNo FROM con_detail WHERE Buyer_Item = '" + styles[i] + @"' GROUP BY PPrfNo) AS temp,
														con_ppr c
													WHERE
														c.PPrfNo = temp.PPrfNo
													and c.po in (" + stylePos +@")
													GROUP BY
														c.org,
														c.Cust_id,
														c.Serial_From,
														c.PPrfNo,
														c.po,
														c.MAIN_LINE,
														c.con_no
													) temp,
													inv i
												WHERE
													i.con_no = temp.Serial_From    UNION ALL   ";
                

                PPrfNobuyerItems = PPrfNobuyerItems + @"SELECT
																PPrfNo 
															FROM
																con_detail 
															WHERE
																Buyer_Item = '" + styles[i] + @"' 
															 
																GROUP BY
																PPrfNo   UNION ALL   ";

            }


          //  for (int i = 0; i < pos.Count; i++)
         //   {
        //        po = po + @"'" + pos[i] + "',";
         //   }
            buyerItems = buyerItems.Substring(0, buyerItems.Length - 12);
            PPrfNobuyerItems = PPrfNobuyerItems.Substring(0, PPrfNobuyerItems.Length - 12);
			//  po = po.Substring(0, po.Length - 1);

			// po = po + @" (d.Buyer_Item = '" + styles[i] + @"'  and  c.PO in (" + stylePos + @"))  or  ";
			po = po.Substring(0, po.Length - 4);

			string sql = @"
							SELECT
								c.cust_id,
								c.org,
								c.po,
								c.main_line,
								c.serial_from,
								c.con_no,
								c.qty,
								c.pprfno,
								a.con_no scanBoxs,
								i.id,
								i.subinv,
								i.location,
								d.Buyer_Item,
								d.color_code
							FROM
								con_ppr c
								LEFT JOIN (
								SELECT
									max( a.scantime ) scantime,
									con_no 
								FROM
									( " + buyerItems + @" ) a 
								GROUP BY
									a.con_no 
								) a ON a.con_no = c.serial_from
								LEFT JOIN inv i ON i.scantime = a.scantime 
								AND i.con_no = a.con_no
								LEFT JOIN con_detail d ON d.pprfno = c.pprfno 
								AND d.serial_from = c.serial_from 
							WHERE
								c.PPrfNo IN (
								SELECT
									a.PPrfNo 
								FROM
									(" + PPrfNobuyerItems + @") a 
								GROUP BY
									a.PPrfNo 
								) 
								and c.qty > 0 
							 

								AND ( " + po + @"    )

							GROUP BY
								c.org,
								c.Cust_id,
								c.Serial_From,
								c.PPrfNo,
								c.po,
								c.MAIN_LINE,
								c.con_no,
								d.color_code,
								i.subinv,
								i.location,
								d.Buyer_Item";


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
