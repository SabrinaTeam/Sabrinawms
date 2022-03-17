using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductSearchService
    {
        public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
        public DataTable getInvByP(List<string> ps)
        { 
            string style = ps[0];
            string color = ps[1];
            string size = ps[2];
            string org = ps[3];
            string subinv = ps[4];
            string types = ps[5];
            string scanData = ps[6];
            string starData = ps[7];
            string stopData = ps[8];
            string po = ps[9];

            string type ="";
            string warehouse = "";
            string location = "";

            // build A
            // build D
            if (subinv == "build A" &&  types == "入库")
            {
                warehouse = "TA_HD";
                location = "HD";
            }
            if (subinv == "build A" && types == "出货")
            {
                warehouse = "TA10";
                location = "CH";
            }
            if (subinv == "build D" && types == "入库")
            {
                warehouse = "TD_HD";
                location = "HD";
            }
            if (subinv == "build D" && types == "出货")
            {
                warehouse = "TD10";
                location = "CH";
            }

            if (subinv == "build SAA" && types == "入库")
            {
                warehouse = "S_HD";
                location = "HD";
            }
            if (subinv == "build SAA" && types == "出货")
            {
                warehouse = "S010";
                location = "CH";
            }

            // this.cbScanDate.Checked    scanData = "1"; 
            string sqlstr = "";
            if ( scanData == "1")
            {
                sqlstr = @" SELECT i.org,i.Cust_id, d.Buyer_Item,d.color_code,  c.po,
                              CASE 	WHEN c.MAIN_LINE is null THEN	 '' 	ELSE 		 c.MAIN_LINE  END   MAIN_LINE
                             ,c.con_no,c.qty as boxqty, d.Size1,d.qty as boxSizeQty   ,
							 i.TagNumber, DATE_FORMAT(i.scantime, '%Y-%m-%d') scantime,	 c.PPrfNo,	d.Item_desc
							 from inv i , (
							 SELECT con_no,min(ScanTime) ScanTime  FROM inv  i WHERE  con_no in (SELECT con_no FROM inv  i WHERE 
							  i.scantime BETWEEN '" + starData  + "' and '"+ stopData + @"' 
							  and i.subinv ='"+ warehouse + @"'   AND i.location ='"+ location  + @"' 
							 GROUP BY i.con_no)  GROUP BY i.con_no )  a		
							 left join 		con_ppr	 c on  a.con_no =c.Serial_From	 
							 left join 		 con_detail	 d on  a.con_no = d.Serial_From			
							 WHERE i.con_no=a. con_no and i.ScanTime=a.ScanTime 	 
							  and  a.scantime BETWEEN  '" + starData + "'  and '" + stopData + @"'
                              and i.subinv ='" + warehouse + @"'   AND i.location ='" + location + @"' 
							 ORDER BY i.TagNumber";
            }
            //按PO查询
            if (scanData == "0")
            {
                sqlstr = @" SELECT i.org,i.Cust_id, d.Buyer_Item,d.color_code,  c.po,
                            CASE 	WHEN c.MAIN_LINE is null THEN	   '' 	ELSE 		 c.MAIN_LINE  END   MAIN_LINE,
                            c.con_no,c.qty as boxqty, d.Size1,d.qty as boxSizeQty   ,
							 i.TagNumber, DATE_FORMAT(i.scantime, '%Y-%m-%d') scantime,	 c.PPrfNo,	d.Item_desc
							 from inv i , (
							 SELECT con_no,min(ScanTime) ScanTime  FROM inv  i WHERE  con_no in (SELECT con_no FROM inv  i WHERE  
							   i.subinv ='" + warehouse + "'  AND i.location ='"+ location + @"'
							 GROUP BY i.con_no)  GROUP BY i.con_no )  a		
							 left join 		con_ppr	 c on  a.con_no =c.Serial_From	 
							 left join 		 con_detail	 d on  a.con_no = d.Serial_From			
							 WHERE i.con_no=a. con_no and i.ScanTime=a.ScanTime 	 
                             and i.subinv ='" + warehouse + @"'  AND i.location = '"+ location + @"'  and c.PO= '"+ po +@"'
                             ORDER BY i.TagNumber  ";
            }


            DataTable result = new DataTable();

            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            return result;


        }

        public List<string> getOrg()
        {
            List<string> orgs = new List<string>();
            string sqlstr = "SELECT org FROM  location GROUP BY org";             

            DataTable dt = new DataTable();

            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            if (dt.Rows.Count <= 0)
            {
                orgs.Add("没有数据");
                return orgs;
            }
            foreach (DataRow dr in dt.Rows)
            {
                orgs.Add(dr["org"].ToString().Trim().ToUpper());
            }
            return orgs;
        }



        public List<string> getSubinv(string org)
        {
            List<string> subinvs = new List<string>();
            string sqlstr = "SELECT subinv FROM  location  WHERE org = '"+ org + "' GROUP BY subinv ";

            DataTable dt = new DataTable();

            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            if (dt.Rows.Count <= 0)
            {
                subinvs.Add("没有数据");
                return subinvs;
            }
            foreach (DataRow dr in dt.Rows)
            {
                subinvs.Add(dr["subinv"].ToString().Trim().ToUpper());
            }
            return subinvs;
        }

        public DataTable getStyles()
        {
           
            string sqlstr = "SELECT Buyer_Item  from con_detail GROUP BY  Buyer_Item ORDER BY Buyer_Item";

            DataTable dt = new DataTable();

            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            } 
            return dt;
        }

        public List<string> getColors(string style)
        {
            List<string> colors = new List<string>();
            string sqlstr = "SELECT color_code  from con_detail WHERE Buyer_Item='"+ style + "' GROUP BY  color_code ORDER BY color_code";

            DataTable dt = new DataTable();

            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            if (dt.Rows.Count <= 0)
            {
                colors.Add("没有数据");
                return colors;
            }
            foreach (DataRow dr in dt.Rows)
            {
                colors.Add(dr["color_code"].ToString().Trim().ToUpper());
            }
            return colors;
        }
    }
}
