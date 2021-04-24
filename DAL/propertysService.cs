using MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;

namespace DAL
{
    public class propertysService
    {
        public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;

        public DataTable getPropertysByPnumber(string org, string propertyIDs)
        {
             
            string[] propertyID = propertyIDs.Split('|');
            string wherestr = "";
            if (propertyID.Length > 0)
            {
                for(int i = 0; i < propertyID.Length; i++)
                {
                    //('AE9O-0184','AE9O-0183','AE9O-0189')
                    wherestr = wherestr + "'" + propertyID[i] + "',";
                }

            }
            wherestr = wherestr.Remove(wherestr.Length -1,1);
            string sql = @" SELECT
	                        FAJ.FAJ01 erpid,
	                        FAJ.FAJ02 propertyID,
	                        FAJ.FAJ22 org,
	                        FAJ.FAJ06 propertyName,
	                        FAJ.FAJ08 propertyMode,
	                        FAJ.FAJ93 propertyType,
	                       TO_CHAR(FAJ.FAJ25,'yyyy-mm-dd hh24:mi:ss')  buyDate,
	                        FAJ.FAJ20 propertyDept,
	                        FAJ.FAJ21,
	                        FAJ.FAJ47 propertyBuyID,
	                        FAJ.FAJ19,
	                        FAJ.FAJ18 propertyUnit,
	                        FAF.FAF02,
	                        GEN.GEN02 
                        FROM
	                        " + org + @".FAJ_FILE FAJ
                            LEFT JOIN  " + org + @".FAF_FILE FAF ON FAJ.FAJ21 = FAF.FAF01
	                        LEFT JOIN  " + org + @".GEN_FILE GEN ON FAJ.FAJ19 = GEN.GEN01 
                        WHERE
	                        FAJ.FAJ02 IN ( " + wherestr + ")";  
            DataTable dt = ERP_SqlHelper.ExcuteTable(sql);

            return dt;
            /*
            List<propertys> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<propertys>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.propertys c = new MODEL.propertys();
                    propertys(row, c);
                    lists.Add(c);
                }
            }
            return lists;
            */

           
        }
        public void propertys(DataRow dr, MODEL.propertys list)
        {
            list.id = 0; // 索引
            list.erpid = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ01"])); // ERP索引
            list.org = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ22"])); // 厂别
            list.propertyID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ02"])); // 财编
            list.propertyName = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ06"])); // 资产名称
            list.propertyMode = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ08"])); // 资产型号
            list.propertyType = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ93"])); // 固资分类
            // DateTime.Now.ToString("yyyy/MM/dd  hh:mm:ss")   
            list.buyDate = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ25"])); // 购入日期            
            list.propertyDept = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ20"])); // 资产归属部门
            list.propertyLocal = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ21"])) +"-"+ Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAF02"])); // 资产存放位置
            list.propertyBuyID = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ47"])); // 资产采购单号
            list.propertySavePerson = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ19"])) + "-"+ Convert.ToString(ERP_SqlHelper.FromDbValue(dr["GEN02"])); // 资产保管人
            list.propertyUnit = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["FAJ18"])); // 资产单位
            list.propertyPrintTims = 0; // 财编打印次数
            list.propertyPrintPC = ""; // 资产建立者PC名
            list.propertyIsDel = 0; // 删除标记
            list.propertyDelPC = ""; // 删除者PC
            list.propertyDelDate = ""; // 删除日期 
            list.propertyDelNote = ""; // 删除备注 
        }


        public int delPropertysByPnumber(List<string> propertyIDs,string delnote)
        {
            if (propertyIDs.Count <= 0)
            {
                return 0;
            }
            string sqlValue = "";
            string sqlstr = "";
            string propertyDelPC = Dns.GetHostName();
            string propertyDelDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            for (int i = 0; i < propertyIDs.Count; i++)
            {
                sqlValue = sqlValue +"'"+ propertyIDs[i].ToString() + "',";


            }
           sqlValue = sqlValue.Substring(0, sqlValue.Length - 1);
           sqlstr = @"UPDATE propertys set propertyIsDel = 1 , propertyDelPC = '"+ propertyDelPC  + "' , propertyDelNote='"+ delnote + "',propertyDelDate ='"+ propertyDelDate + "' WHERE propertyID in (" + sqlValue +")";
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


        public int insertPropertys(DataTable dt)
        {
            
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }

            //保存前查询是否已有此财编号

            string sqlValue = "";
            string sqlstr = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //  str=str.Replace("abc","ABC"); 
                sqlValue = sqlValue + "(\"" +
                             dt.Rows[i]["erpid"].ToString().Replace("\"","'") + "\",\"" +
                             dt.Rows[i]["org"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyID"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyName"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyMode"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyType"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["buyDate"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyDept"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyLocal"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyBuyID"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertySavePerson"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyUnit"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyPrintTims"].ToString().Replace("\"", "'") + "\",\"" +
                             dt.Rows[i]["propertyIsDel"].ToString().Replace("\"", "'") + "\"),";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) + ";";

            sqlstr = @"INSERT INTO propertys (
                                            erpid,
                                            org,
                                            propertyID,
                                            propertyName,
                                            propertyMode,
                                            propertyType,
                                            buyDate,
                                            propertyDept,
                                            propertyLocal,
                                            propertyBuyID,
                                            propertySavePerson,
                                            propertyUnit,
                                            propertyPrintTims,
                                            propertyIsDel
                                            )  VALUES " + sqlValue;

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


        public DataTable getPropertysByPnumberFromLocalHost(  string propertyIDs ,bool IsDel)
        {

            string[] propertyID = propertyIDs.Split('|');
            string sql = "";
            string wherestr = "";
            if (propertyID.Length > 1)
            {
                for (int i = 0; i < propertyID.Length; i++)
                {
                    //('AE9O-0184','AE9O-0183','AE9O-0189')
                    wherestr = wherestr + "'" + propertyID[i] + "',";
                }


                wherestr = wherestr.Remove(wherestr.Length - 1, 1);
                if (IsDel)
                {
                    sql = @"SELECT * from  propertys WHERE   propertyID in (" + wherestr + ")  and propertyIsDel = 1";
                }
                else
                {
                    sql = @"SELECT * from  propertys WHERE   propertyID in (" + wherestr + ")";
                }
            }
            else
            {
                for (int i = 0; i < propertyID.Length; i++)
                {
                    //('AE9O-0184','AE9O-0183','AE9O-0189')
                    wherestr = wherestr + "'" + propertyID[i] + "',";
                }


                wherestr = wherestr.Remove(wherestr.Length - 1, 1);
                if (IsDel)
                {
                    sql = @"SELECT * from  propertys WHERE   propertyID like (" + wherestr + ")  and propertyIsDel = 1";
                }
                else
                {
                    sql = @"SELECT * from  propertys WHERE   propertyID like (" + wherestr + ")";
                }
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

        public DataTable getPropertysByPnumberFromLocalHost(string propertyIDs )
        {

            string[] propertyID = propertyIDs.Split('|');
            string sql = "";
            string wherestr = "";
            if (propertyID.Length > 0)
            {
                for (int i = 0; i < propertyID.Length; i++)
                {
                    //('AE9O-0184','AE9O-0183','AE9O-0189')
                    wherestr = wherestr + "'" + propertyID[i] + "',";
                }

            }
            wherestr = wherestr.Remove(wherestr.Length - 1, 1);
            sql = @"SELECT * from  propertys WHERE   propertyID in (" + wherestr + ")";
             
             

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


        public int upPrintPropertysByPnumber(string[] pId)
        {
            if (pId.Length <= 0)
            {
                return 0;
            }
            string sqlValue = "";
            string sqlstr = "";
            string propertyPrintPC = Dns.GetHostName();
            for (int i = 0; i < pId.Length; i++)
            {
                sqlValue = sqlValue + "'" + pId[i].ToString() + "',";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1);
            sqlstr = @"UPDATE propertys set propertyPrintTims = propertyPrintTims+1 ,propertyPrintPC ='"+ propertyPrintPC + "' WHERE propertyID in (" + sqlValue + ")";
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
