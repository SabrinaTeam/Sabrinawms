using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class receiManager
    {
        public receiServicecs rs = new receiServicecs();
        public string writeReceiToData(DataTable dt)
        {
            DataTable isHaveId = new DataTable();
            isHaveId.Columns.Add("id", typeof(int));
            isHaveId.Columns.Add("org", typeof(string));
            isHaveId.Columns.Add("subinv", typeof(string));
            isHaveId.Columns.Add("line", typeof(string));

            isHaveId.Columns.Add("style", typeof(string));
            isHaveId.Columns.Add("color", typeof(string));
            isHaveId.Columns.Add("size", typeof(string));
            isHaveId.Columns.Add("qtyCount", typeof(int));

            isHaveId.Columns.Add("po", typeof(string));
            isHaveId.Columns.Add("boxCount", typeof(string));
            isHaveId.Columns.Add("receiNumber", typeof(string));
            isHaveId.Columns.Add("receiDate", typeof(string));
            isHaveId.Columns.Add("mark", typeof(string));

            isHaveId.Columns.Add("receiInDate", typeof(string));
            isHaveId.Columns.Add("receiInPcName", typeof(string));
            isHaveId.Columns.Add("receiEmp", typeof(string));

            DataTable isNotId = new DataTable();
            isNotId.Columns.Add("id", typeof(int));
            isNotId.Columns.Add("org", typeof(string));
            isNotId.Columns.Add("subinv", typeof(string));
            isNotId.Columns.Add("line", typeof(string));

            isNotId.Columns.Add("style", typeof(string));
            isNotId.Columns.Add("color", typeof(string));
            isNotId.Columns.Add("size", typeof(string));
            isNotId.Columns.Add("qtyCount", typeof(int));

            isNotId.Columns.Add("po", typeof(string));
            isNotId.Columns.Add("boxCount", typeof(string));
            isNotId.Columns.Add("receiNumber", typeof(string));
            isNotId.Columns.Add("receiDate", typeof(string));
            isNotId.Columns.Add("mark", typeof(string));

            isNotId.Columns.Add("receiInDate", typeof(string));
            isNotId.Columns.Add("receiInPcName", typeof(string));
            isNotId.Columns.Add("receiEmp", typeof(string));

           
            int wr = 0;
            int ur = 0;
            //有ID的更新 ，没有ID的新增
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["ID"].ToString() != "0")
                {
                    DataRow row = isHaveId.NewRow();
                    row["id"] = dt.Rows[i]["id"].ToString();
                    row["org"] = dt.Rows[i]["org"].ToString();
                    row["subinv"] = dt.Rows[i]["subinv"].ToString();
                    row["line"] = dt.Rows[i]["line"].ToString();
                    row["style"] = dt.Rows[i]["style"].ToString();
                    row["color"] = dt.Rows[i]["color"].ToString();
                    row["size"] = dt.Rows[i]["size"].ToString();
                    row["qtyCount"] = dt.Rows[i]["qtyCount"].ToString();
                    row["po"] = dt.Rows[i]["po"].ToString();
                    row["boxCount"] = dt.Rows[i]["boxCount"].ToString();
                    row["receiNumber"] = dt.Rows[i]["receiNumber"].ToString();
                    row["receiDate"] = dt.Rows[i]["receiDate"].ToString();
                    row["receiEmp"] = dt.Rows[i]["receiEmp"].ToString();
                    row["mark"] = dt.Rows[i]["mark"].ToString();
                    row["size"] = dt.Rows[i]["size"].ToString();
                    row["receiInDate"] = dt.Rows[i]["receiInDate"].ToString();
                    row["receiInPcName"] = dt.Rows[i]["receiInPcName"].ToString();
                    isHaveId.Rows.Add(row);
                }
                else
                {
                    DataRow row = isNotId.NewRow();
                    row["id"] = dt.Rows[i]["id"].ToString();
                    row["org"] = dt.Rows[i]["org"].ToString();
                    row["subinv"] = dt.Rows[i]["subinv"].ToString();
                    row["line"] = dt.Rows[i]["line"].ToString();
                    row["style"] = dt.Rows[i]["style"].ToString();
                    row["color"] = dt.Rows[i]["color"].ToString();
                    row["size"] = dt.Rows[i]["size"].ToString();
                    row["qtyCount"] = dt.Rows[i]["qtyCount"].ToString();
                    row["po"] = dt.Rows[i]["po"].ToString();
                    row["boxCount"] = dt.Rows[i]["boxCount"].ToString();
                    row["receiNumber"] = dt.Rows[i]["receiNumber"].ToString();
                    row["receiDate"] = dt.Rows[i]["receiDate"].ToString();
                    row["receiEmp"] = dt.Rows[i]["receiEmp"].ToString();
                    row["mark"] = dt.Rows[i]["mark"].ToString();
                    row["size"] = dt.Rows[i]["size"].ToString();
                    row["receiInDate"] = dt.Rows[i]["receiInDate"].ToString();
                    row["receiInPcName"] = dt.Rows[i]["receiInPcName"].ToString();
                    isNotId.Rows.Add(row);
                }
            }
            if (isHaveId.Rows.Count > 0)
            {    
               

                ur = rs.updateReceiToData(isHaveId);
            }
            if(isNotId.Rows.Count > 0)
            {
              wr = rs.writeReceiToData(isNotId); 
            }
            return "共新增"+ wr.ToString() + "条记录，更新 "+ur.ToString()+ "条记录";
        }

        public int updateReceiToData(DataTable dt)
        {
            return rs.updateReceiToData(dt);
        }

        public DataTable getOrg( )
        {
            return rs.getOrg();
        }


        public DataTable getSubinvs(string org)
        {
            return rs.getSubinvs(org);
        }

        public DataTable getLocations(string org, string subinv)
        {
            return rs.getLocations(org, subinv);
        }

        public DataTable getReceis(receiSearch rsp)
        {
            
            return rs.getReceis(rsp);
        }

        public DataTable getStyles(string org ,string outLine)
        {
            return rs.getStyles(org, outLine);
        }

        public DataTable getColorsByStyle(string style)
        {
            return rs.getColorsByStyle(style);
        }
        public DataTable getSizesByStyle(string style)
        {
            return rs.getSizesByStyle(style);
        }

        public int delRowsByID(int id)
        {

            return rs.delRowsByID(id);
        }

        public DataTable getStyleCounts(string org, string outsubinv,  string outLine)
        {
            return rs.getStyleCounts(org, outsubinv,outLine);
        }


        public int insetOrUpdataByID(DataTable dt)
        {

            //新增OR更新转厂二 款式数量资料
            DataTable insetDt = new DataTable();
            insetDt.Columns.Add("id", typeof(int));
            insetDt.Columns.Add("org", typeof(string));
            insetDt.Columns.Add("subinv", typeof(string));
            insetDt.Columns.Add("line", typeof(string));
            insetDt.Columns.Add("style", typeof(string));
            insetDt.Columns.Add("stylecount", typeof(int));

            DataTable updataDt = new DataTable();
            updataDt.Columns.Add("id", typeof(int));
            updataDt.Columns.Add("org", typeof(string));
            updataDt.Columns.Add("subinv", typeof(string));
            updataDt.Columns.Add("line", typeof(string));
            updataDt.Columns.Add("style", typeof(string));
            updataDt.Columns.Add("stylecount", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["id"].ToString() == "0")
                {
                    DataRow irow = insetDt.NewRow();
                    irow["id"] = 0;
                    irow["org"] = dt.Rows[i]["org"].ToString();
                    irow["subinv"] = dt.Rows[i]["subinv"].ToString();
                    irow["line"] = dt.Rows[i]["line"].ToString();
                    irow["style"] = dt.Rows[i]["style"].ToString();
                    irow["stylecount"] = dt.Rows[i]["stylecount"].ToString();
                    insetDt.Rows.Add(irow);
                }
                else
                {
                    DataRow urow = updataDt.NewRow();
                    urow["id"] = dt.Rows[i]["id"].ToString();
                    urow["org"] = dt.Rows[i]["org"].ToString();
                    urow["subinv"] = dt.Rows[i]["subinv"].ToString();
                    urow["line"] = dt.Rows[i]["line"].ToString();
                    urow["style"] = dt.Rows[i]["style"].ToString();
                    urow["stylecount"] = dt.Rows[i]["stylecount"].ToString();
                    updataDt.Rows.Add(urow);
                }
            }
            int k=  insetByID(insetDt);
            int j =  updataByID(updataDt);

            return  k+j;
        }

        public int insetByID(DataTable dt)
        {
            return rs.insetByID(dt);
        }
        public int updataByID(DataTable dt)
        {
            return rs.updataByID(dt);
        }

        /// <summary>
        /// 更新本地转厂2总数量
        /// </summary>
        /// <param name="org"></param>
        /// <param name="outLine"></param>
        /// <returns></returns>
        public DataTable getOutCounts(string org, string outLine, string outsubinv)
        {
            //获取已转厂二款式信息
            DataTable styledt = new DataTable(); //转厂二款式信息         
            styledt = this.getStyles(org, outLine);

            //新增OR更新转厂二 款式数量资料
            DataTable styleCountDt = new DataTable();
            styleCountDt.Columns.Add("id", typeof(int));
            styleCountDt.Columns.Add("org", typeof(string));
            styleCountDt.Columns.Add("subinv", typeof(string));
            styleCountDt.Columns.Add("line", typeof(string));
            styleCountDt.Columns.Add("style", typeof(string));
            styleCountDt.Columns.Add("stylecount", typeof(int));
            for (int i = 0; i < styledt.Rows.Count; i++)
            {
                DataRow row = styleCountDt.NewRow();
                row["id"] = 0;
                row["org"] = org;
                row["subinv"] = outsubinv;
                row["line"] = outLine;
                row["style"] = styledt.Rows[i]["STYLE"].ToString();
                row["stylecount"] = Convert.ToInt32(styledt.Rows[i]["QTY"]); ;
                styleCountDt.Rows.Add(row);

            }
            // 查询数据库已有的款式数量单号
            DataTable styleCounts = this.getStyleCounts(org, outsubinv, outLine);

            //更新或新增 ID
            for (int i = 0; i < styleCounts.Rows.Count; i++)
            {
                for (int j = 0; j < styleCountDt.Rows.Count; j++)
                {
                    if (styleCounts.Rows[i]["Org"].ToString() == styleCountDt.Rows[j]["org"].ToString() &&
                       styleCounts.Rows[i]["subinv"].ToString() == styleCountDt.Rows[j]["subinv"].ToString() &&
                       styleCounts.Rows[i]["line"].ToString() == styleCountDt.Rows[j]["line"].ToString() &&
                       styleCounts.Rows[i]["style"].ToString() == styleCountDt.Rows[j]["style"].ToString()
                        )
                    {
                        styleCountDt.Rows[j]["id"] = styleCounts.Rows[i]["id"].ToString();
                       // styleCountDt.Rows[j]["stylecount"] = styleCountDt.Rows[j]["stylecount"].ToString();

                    }
                }

            }

            //更新或新增总数量表
            int wsc = this.insetOrUpdataByID(styleCountDt);
            return styledt;
        }


      //  public int updataByID(DataTable dt)
      //  {
     //       return rs.updataByID(dt);
     //   }
        public DataTable getStyleCounts(string org, string outsubinv, string outLine, string style)
        {
          return rs.getStyleCounts(org, outsubinv, outLine,style);
             
        }
        public int updataStyleCounts(int id,int qtyCount)
        {
            return rs.updataStyleCounts(id, qtyCount);

        }

        public DataTable getReceiIns(string org, string subinv, string line, string style,string size,string color)
        {
            return rs.getReceiIns(org, subinv, line, style, size,color);
          
        }


        public int delStyleCount(string org, string subinv, string line, string style, string size,int delQty)
        {

            return rs.delStyleCount(org, subinv, line, style, size, delQty);
        }

        public int updataReceiError(string org, string line, string style, int qtyCount, int styleCount, string mark)
        {
            return rs.updataReceiError(org, line, style, qtyCount, styleCount,mark);

        }



    }
}
