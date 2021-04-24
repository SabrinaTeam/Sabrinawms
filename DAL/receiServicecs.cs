using MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class receiServicecs
    {
        public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;

        public int writeReceiToData(DataTable dt)
        {

            this.isMany();

            string sqlValue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sqlValue = sqlValue +
                           "(\"" + dt.Rows[i]["org"].ToString() + "\",\""
                               + dt.Rows[i]["subinv"].ToString() + "\",\""
                               + dt.Rows[i]["line"].ToString() + "\",\""
                               + dt.Rows[i]["style"].ToString() + "\",\""
                               + dt.Rows[i]["color"].ToString() + "\",\""
                               + dt.Rows[i]["size"].ToString() + "\",\""
                               + dt.Rows[i]["qtyCount"].ToString() + "\",\""
                               + dt.Rows[i]["po"].ToString() + "\",\""
                               + dt.Rows[i]["boxCount"].ToString() + "\",\""
                               + dt.Rows[i]["receiNumber"].ToString() + "\",\""
                               + dt.Rows[i]["receiDate"].ToString() + "\",\""
                               + dt.Rows[i]["receiEmp"].ToString() + "\",\""
                               + dt.Rows[i]["mark"].ToString() + "\",\""
                               + dt.Rows[i]["receiInDate"].ToString() + "\",\""
                               + dt.Rows[i]["receiInPcName"].ToString() + "\",\""
                               + 0 + "\" ),";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) + ";";
            string sqlstr = @"INSERT INTO receis (org, subinv, line, style, color, size, qtyCount, po, boxCount, receiNumber, receiDate,receiEmp, mark, receiInDate, receiInPcName,isFull)  VALUES " + sqlValue;

            int result = 0;
            
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
        }


        public int updateReceiToData(DataTable dt)
        {
            string sqlWHEN = "";
            string sqlID = "";
            string sqlCASE = "";
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string value = dt.Rows[j][i].ToString();
                    if (i == 1)
                    {
                        sqlID = sqlID + dt.Rows[j]["ID"].ToString() + ",";
                    }
                    if (value.Length <= 0)
                    {
                        value = "";
                    }
                    sqlWHEN = sqlWHEN +
                        "  WHEN  " + dt.Rows[j]["ID"].ToString() + "  THEN  \"" + value + "\"";
                }
                sqlCASE = sqlCASE +
                    "  " + dt.Columns[i].ToString() + " = CASE id " + sqlWHEN + " END ,";
                sqlWHEN = "";

            }

            sqlCASE = sqlCASE.Substring(0, sqlCASE.Length - 1);
            sqlID = sqlID.Substring(0, sqlID.Length - 1);
            string sqlstr = @"UPDATE receis SET " + sqlCASE + " WHERE id IN (" + sqlID + ")";

            // org, subinv, line, style, color, size, qtyCount, po, boxCount, receiNumber, receiDate, mark, receiInDate, receiInPcName, receiEmp,isFull
            /*
             UPDATE recei
                        SET org = CASE id
                            WHEN 1 THEN 'SAA'
                            WHEN 2 THEN 'SAA'
                            WHEN 4 THEN 'SAA'
                        END,
		                     subinv = CASE id
                            WHEN 1 THEN 'S_HD'
                            WHEN 2 THEN 'S_HD'
                            WHEN 4 THEN 'S_HD'
                        END		
                    WHERE id IN (1,2,4)
             */
            int result =0;
           
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
        }


        public bool isMany()
        {
            return true;

        } 
 

        public DataTable getOrg()
        {
            string sqlstr = @"SELECT DISTINCT org,id FROM location  GROUP BY org";
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


        public DataTable getSubinvs(string org)
        {
            string sqlstr = @"SELECT DISTINCT subinv,id FROM location  WHERE org ='" + org + "' and subinv like '%HD'  GROUP BY subinv";
           
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

        public DataTable getLocations(string org, string subinv)
        {
            string sqlstr = @"SELECT DISTINCT location,id FROM location  WHERE org ='" + org + "' and subinv = '" + subinv + "'  AND LOCATION LIKE 'CF%D'  GROUP BY location";
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

        public DataTable getReceis(receiSearch rs)
        {
            string locations = "";
            string whereStr = "";
            if (rs.location.Count > 0)
            {
                for(int i = 0;i< rs.location.Count; i++)
                {
                    locations = locations + rs.location[i] + "','";
                }
               
            }
            if (locations.Length > 0)
            {
                locations = locations.Substring(0, locations.Length - 2);
            }
           
          // 厂区
            if (rs.org.Length > 0)
            {
                whereStr = whereStr + "  AND  org = '" + rs.org + "'";
            }
            // 仓库
            if (rs.subinv.Length > 0)
            {
                whereStr = whereStr + "  AND  subinv = '" + rs.subinv + "'";
            }
            // 线别
            if (locations.Length > 0)
            {
                whereStr = whereStr + "  AND  line in ( '" + locations + ")";
            }
            

            // 款式
            if (rs.style.Length > 0)
            {
                whereStr = whereStr + "  AND  style = '" + rs.style + "'";
            }
            //颜色
            if (rs.color.Length > 0)
            {
                whereStr = whereStr + "  AND  color = '" + rs.color + "'";
            }
            //PO
            if (rs.poNumber.Length > 0)
            {
                whereStr = whereStr + "  AND  PO = '" + rs.poNumber + "'";
            }
            // 送货单 号
            if (rs.ReceiNumber.Length > 0)
            {
                whereStr = whereStr + "  AND  receiNumber = '" + rs.ReceiNumber + "'";
            }
            if (rs.receiDate)
            {
                whereStr = whereStr + "  AND   DATE_FORMAT(receiInDate, '%Y-%m-%d')   BETWEEN '" + rs.starTime + "'  and '" + rs.stopTime + @"'";
            }


          //  whereStr = whereStr.Substring(0, whereStr.Length - 1);

            string sqlstr = @"
                            SELECT
	                            org,
	                            subinv,
	                            line,
	                            style,
	                            color,
	                            size,
	                            SUM( qtyCount ) qtyCount
	                             
                            FROM
	                            receis 
                            WHERE isFull = 0 " +   whereStr + @"
                            GROUP BY
	                            org,
	                            subinv,
	                            line,
	                            style,
	                            color,
	                            size";
            
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


        public DataTable getStyles(string org,string outLine)
        {
            DataTable asft = new DataTable();
            asft.Columns.Add("STYLE");
            asft.Columns.Add("QTY");
           
            string stylestr = "";
            string str705 = @"select  SHB10 AS STYLE , SUM(SHB111) AS QTY   from  "+ org+".SHB_FILE WHERE  SHB09 =  '"+outLine+ "'  GROUP BY  SHB10 ORDER BY  SHB10";
            DataTable asft705 =  ERP_SqlHelper.ExcuteTable(str705);
            for(int i = 0; i < asft705.Rows.Count; i++)
            {
                stylestr = asft705.Rows[i]["STYLE"].ToString().ToUpper();
                if (!isExStyles(stylestr, asft))
                    {
                        DataRow row = asft.NewRow();
                        row["STYLE"] = stylestr;
                    row["QTY"] = 0;
                    asft.Rows.Add(row);
                }
            }

            string str700 = @"select SGL05 AS STYLE, SUM(SGL08) AS QTY  from " + org + ".sgl_file where sgl13 = '" + outLine + "'  GROUP BY SGL05 ORDER BY SGL05";
            DataTable asft700 = ERP_SqlHelper.ExcuteTable(str700);
            for (int i = 0; i < asft700.Rows.Count; i++)
            {
                stylestr = asft700.Rows[i]["STYLE"].ToString().ToUpper();
                if (!isExStyles(stylestr, asft))
                {
                    DataRow row = asft.NewRow();
                    row["STYLE"] = stylestr;
                    row["QTY"] = 0;
                    asft.Rows.Add(row);
                }
            }


            int qty = 0;
            for (int i = 0; i < asft.Rows.Count; i++)
            {
                for (int j = 0; j < asft700.Rows.Count; j++)
                {
                    if (asft.Rows[i]["STYLE"].ToString().ToUpper() == asft700.Rows[j]["STYLE"].ToString().ToUpper())
                    {
                        qty = qty + Convert.ToInt32(asft700.Rows[j]["QTY"].ToString());
                    }
                }
                if (qty > 0)
                {
                    asft.Rows[i]["QTY"] = Convert.ToInt32( asft.Rows[i]["QTY"].ToString()) + qty;
                    qty = 0;
                }
                
            }

            int tty = 0;
            for (int i = 0; i < asft.Rows.Count; i++)
            {
                for (int j = 0; j < asft705.Rows.Count; j++)
                {
                    if (asft.Rows[i]["STYLE"].ToString().ToUpper() == asft705.Rows[j]["STYLE"].ToString().ToUpper())
                    {
                        tty = tty + Convert.ToInt32(asft705.Rows[j]["QTY"].ToString());
                    }
                }
                if (tty > 0)
                {
                    asft.Rows[i]["QTY"] = Convert.ToInt32(asft.Rows[i]["QTY"].ToString()) + tty;
                    tty = 0;
                }
               
            }
            return asft; 
             
        }

        public bool isExStyles(string style,DataTable dt)
        {
            bool isExStyle = false;
            if (dt.Rows.Count <= 0)
            {
                isExStyle = false;
            }
            else
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["STYLE"].ToString() == style)
                    {
                        isExStyle = true;
                        break;
                    }
                }
            }
            return isExStyle;
        }

        public DataTable getColorsByStyle( string style)
        {
            string sqlstr = @"SELECT  DISTINCT clr_no  FROM odb WHERE style_id='"+ style + "'	ORDER BY clr_no";
            return BEST_SqlHelper.ExcuteTable(sqlstr);
        }


        public DataTable getSizesByStyle(string style)
        {
            string sqlstr = @"
                                SELECT us01,
                                       us02,
                                       us03,
                                       us04,
                                       us05,
                                       us06,
                                       us07,
                                       us08,
                                       us09,
                                       us10,
                                       us11,
                                       us12
                                FROM dbo.odh h
                                    LEFT JOIN odb b
                                        ON h.od_no = b.od_no
                                WHERE b.style_id = '"+ style + "';";
            return BEST_SqlHelper.ExcuteTable(sqlstr);
        }
        public int delRowsByID(  int id)
        {
            string sqlstr = @"UPDATE receis set isFull =1 WHERE id =" + id ;

            int result = 0;
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
        }

        public DataTable getStyleCounts(string org, string outsubinv, string outLine)
        {
            string sqlstr = @"SELECT
	                                id,
	                                Org,
	                                subinv,
	                                line,
	                                style,
	                                stylecount 
                                FROM
	                                countreceis 
                                WHERE
	                                org = '" + org + @"' 
	                                AND subinv = '" + outsubinv + @"' 
	                                AND line = '" + outLine + @"' 
	                                AND STATUS = 0";
            
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

        public DataTable getStyleCounts(string org, string outsubinv, string outLine,string  style)
        {
            string sqlstr = @"SELECT    
                                       id,
                                    styleCount,
	                                qtyCount
                                FROM
	                                countreceis 
                                WHERE
	                                org = '" + org + @"' 
	                                AND subinv = '" + outsubinv + @"' 
	                                AND line = '" + outLine + @"' 
                                    AND style = '" + style + @"'
	                                AND STATUS = 0";
              
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


        public int insetByID(DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            string values = "";
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                values = values + "('" +
                         dt.Rows[i]["Org"].ToString() + "','" +
                         dt.Rows[i]["subinv"].ToString() + "','" +
                         dt.Rows[i]["line"].ToString() + "','" +
                         dt.Rows[i]["style"].ToString() + "','" +
                         dt.Rows[i]["stylecount"].ToString() + "'," +
                         0 + ",'" +
                         DateTime.Now.ToString("yyyy-MM-dd") + "'," + 
                         0 + "),";
            }
            values = values.Substring(0, values.Length - 1);

            string sqlstr = @"INSERT INTO countreceis ( Org, subinv, line, style, stylecount, qtyCount, receiInDate, STATUS )
                                                VALUES " + values;
           

            int result = 0;
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
        }

        public int updataByID(DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            string sqlID = "";
            string sqlCASE = "";           
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sqlID = sqlID + dt.Rows[i]["id"].ToString() + ",";
                    sqlCASE = sqlCASE + "   "
                           
                             +"  WHEN  " + dt.Rows[i]["id"].ToString()+"  THEN  "
                             + dt.Rows[i]["stylecount"].ToString();
                }
          
                sqlID = sqlID.Substring(0, sqlID.Length - 1);            
                sqlCASE = sqlCASE.Substring(0, sqlCASE.Length - 1);           
            string sqlstr = @"UPDATE countreceis SET stylecount = CASE id   " + sqlCASE + @"  END  WHERE id IN (" + sqlID + ")";
           
            int result = 0;
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
        }


        public int updataStyleCounts(int id, int qtyCount)
        {
            string sqlstr = @"UPDATE  countreceis   set  qtyCount = " + qtyCount + "  WHERE ID="+id;
           
            int result = 0;
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
            
        }


        public DataTable getReceiIns(string org, string subinv, string line, string style,string size, string color)
        {
            string sqlstr = @"SELECT
	                                id,
	                                org,
	                                subinv,
	                                line,
	                                style,
	                                color,
	                                size,
	                                qtyCount,
	                                po,
	                                boxCount,
	                                receiNumber,
	                                receiDate,
	                                receiEmp,
	                                mark,
	                                receiInDate,
	                                receiInPcName 
                                FROM
	                                receis 
                                WHERE
	                                org = '" + org + @"' 
	                                AND subinv = '"+ subinv + @"' 
	                                AND line = '"+ line + @"' 
	                                AND style = '"+ style + @"' 
                                    AND size ='"+ size + @"'
                                    AND color ='" + color + @"'
	                                AND isFull = 0";
          
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

        public int delStyleCount(string org, string subinv, string line, string style, string size, int delQty)
        {
            string sqlstr = @"UPDATE countreceis 
                                    SET qtyCount = qtyCount - " + delQty + @" 
                                WHERE
	                                org = '" + org + @"' 
	                                AND subinv = '" + subinv + @"' 
	                                AND line = '" + line + @"' 
	                                AND style = '" + style + "'";
            
            int result = 0;
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
        }

        public int updataReceiError(string org, string line, string style, int qtyCount, int styleCount,string mark)
        {
            string sqlstr = @"INSERT INTO receierror ( org, line, style, qtyCount, styleCount, createDate, mark )
                                VALUES
	                                (
		                                '"+ org + @"',
		                                '"+ line + @"',
		                                '"+ style + @"',
		                                "+ qtyCount + @",
		                                "+ styleCount + @",
		                                '"+ DateTime.Now.ToString("yyyy-MM-dd")+ @"',
	                                     '"+ mark + "')";
           
            int result = 0;
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
            
        }


    }
}
