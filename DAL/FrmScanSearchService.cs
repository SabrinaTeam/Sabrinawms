using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FrmScanSearchService
    {
        public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
        public DataTable getSubinbsByOrg(string org)
        {
            string sql = @"select  subinv   from location where  org = '"+ org + "'  group by  subinv;";

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

        public DataTable getLocationsBysubinv(string org, string subinv)
        {
            string sql = @"select  location  from location where  org = '"+ org +"'  and subinv = '" + subinv + "'  group by  location;";

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

        public DataTable getScanByQuery(string org, string subinv, string location,  string startDate, string stopDate,string styleCode,string colorCode)
        {

            string sql = @"SELECT   i.ORG,
                                    i.Cust_id,
                                    i.subinv,
                                    i.Location,
                                    d.Buyer_Item,
                                    p.po,
                                    p.MAIN_LINE,
                                    d.color_code,
                                    d.Size1,
                                    d.QTY,
                                    i.con_no,
                                    b.kg,
                                    i.TagNumber,
                                    DATE_FORMAT( i.scantime,'%Y-%m-%d %H:%m:%S') scantime ,
                                    DATE_FORMAT( i.update_date,'%Y-%m-%d %T') update_date ,
                                    i.create_pc
                                FROM inv i
                                     LEFT JOIN (
                                SELECT  max(kg) kg ,
                                       TagNumber
                                FROM inv
                                WHERE ORG = '" + org + @"'
                                  AND subinv = '"+ subinv + @"'
                                  AND update_date BETWEEN '"+ startDate + @"'
                                    AND '"+ stopDate + @"'

                                  AND kg IS NOT NULL
                                GROUP BY TagNumber
                            ) b ON b.TagNumber = i.TagNumber
                                     left join con_ppr p on p.Serial_From = i.con_no and p.Cust_id = i.Cust_id
                                     left join con_detail d on d.Serial_From = i.con_no and d.Cust_id = i.Cust_id
                            WHERE i.ORG =   '" + org + @"'
                              AND i.subinv ='" + subinv + @"'
                              AND i.Location ='" + location + @"'
                              AND i.update_date BETWEEN '" + startDate + @"'
                                AND  '" + stopDate + @"'
                              and d.Buyer_Item like '%" + styleCode + @"%'
                              and d.color_code like '%" + colorCode + @"%'
                            GROUP BY i.TagNumber,
                                     i.Cust_id,
                                     i.Location,
                                     i.ORG,
                                     i.con_no,
                                     i.create_pc,
                                     
                                     i.subinv,
                                    b.KG,
                                     p.qty,
                                     p.po,
                                     p.MAIN_LINE,
                                     p.Cust_id,
                                     d.Buyer_Item,
                                     d.color_code,
                                     d.Size1,
                                     d.QTY
                            order by org, Cust_id, subinv, Location, Buyer_Item, PO, MAIN_LINE, 
                                     color_code, Size1, con_no, scantime;";

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
