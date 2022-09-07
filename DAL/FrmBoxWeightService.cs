using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FrmBoxWeightService
    {
        public DataTable getAllCustidByBaseDB()
        {
            string sql = @"select DISTINCT cust_abbr  from cust_dom;";
            DataTable dt = BEST_SqlHelper.ExcuteTable(sql);
            return dt;
        }
        public DataTable getAllBoxNames()
        {
            string sql = @"  SELECT DISTINCT box_name from boxweight ORDER BY box_name;";
            DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        public DataTable getAllSizeIDByBaseDB()
        {
            string sql = @"    SELECT DISTINCT  t_change from  size_sort	  ORDER BY t_change;";
            DataTable dt = BEST_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        public DataTable getBoxWeights(string custid, string styleid)
        {
            string sql = @"SELECT
	                                id,
	                                box_name,
	                                box_weight,
	                                box_l,
	                                box_w,
	                                box_h,
	                                cust_id,
	                                season,
	                                Remark,
	                                isDel,
	                                createUser,
	                                DATE_FORMAT( createdate, '%Y-%m-%d %H:%i:%s' ) createdate,
	                                lastmodified,
	                                DATE_FORMAT( lastmodifydate, '%Y-%m-%d %H:%i:%s' ) lastmodifydate 
                                FROM
	                                boxweight 
                                WHERE
	                                1 = 1 
	                                AND cust_id LIKE '%" + custid + @"%'
	                                AND box_name LIKE '%" + styleid + @"%'
	                                AND isDel = 0;";

            DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);

            return dt;
        }

        public int insetRowsToDb(DataTable dt)
        {
            string sqlValue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sqlValue = sqlValue +
                          "(\""
                                  + dt.Rows[i]["box_name"].ToString() + "\",\""
                                  + dt.Rows[i]["box_weight"].ToString() + "\",\""
                                  + dt.Rows[i]["box_l"].ToString() + "\",\""
                                  + dt.Rows[i]["box_w"].ToString() + "\",\""
                                  + dt.Rows[i]["box_h"].ToString() + "\",\""
                                  + dt.Rows[i]["cust_id"].ToString() + "\",\""
                                  + dt.Rows[i]["season"].ToString() + "\",\""
                                  + dt.Rows[i]["Remark"].ToString() + "\",\""
                                  + dt.Rows[i]["isDel"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateUser"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateDate"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModified"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModifyDate"].ToString() + "\" ),";

            }

            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) + ";";
            string sqlstr = @"INSERT INTO boxweight (
                                                        box_name,	box_weight,	
	                                                    box_l,	box_w,	box_h,	cust_id,
	                                                    season,	Remark,	isDel,
	                                                    createUser, createdate,	lastmodified,lastmodifydate
                                                    )  VALUES " + sqlValue;
            int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            return result;
        }
        public int updateRowsToDb(DataTable dt)
        {
            string sqlValue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sqlValue = sqlValue +
                               "(\""
                                  + dt.Rows[i]["ID"].ToString() + "\",\""
                                  + dt.Rows[i]["box_name"].ToString() + "\",\""
                                  + dt.Rows[i]["box_weight"].ToString() + "\",\""
                                  + dt.Rows[i]["box_l"].ToString() + "\",\""
                                  + dt.Rows[i]["box_w"].ToString() + "\",\""
                                  + dt.Rows[i]["box_h"].ToString() + "\",\""
                                  + dt.Rows[i]["cust_id"].ToString() + "\",\""
                                  + dt.Rows[i]["season"].ToString() + "\",\""
                                  + dt.Rows[i]["Remark"].ToString() + "\",\""
                                  + dt.Rows[i]["isDel"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateUser"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateDate"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModified"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModifyDate"].ToString() + "\" ),";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1);
            string sqlstr = @"INSERT INTO boxweight  (ID,
                                                        	box_name,	box_weight,	
	                                                    box_l,	box_w,	box_h,	cust_id,
	                                                    season,	Remark,	isDel,
	                                                    createUser, createdate,	lastmodified,lastmodifydate
                                                        )  VALUES " + sqlValue;

            sqlstr = sqlstr + @" 	ON DUPLICATE KEY UPDATE ID=VALUES(ID),                                                               
                                                                box_name=VALUES(box_name) ,
                                                                box_weight=VALUES(box_weight)	,
                                                                box_l=VALUES(box_l),

                                                                box_w=VALUES(box_w),
                                                                box_h=VALUES(box_h),
                                                                cust_id=VALUES(cust_id),
                                                                season=VALUES(season),
                                                                Remark=VALUES(Remark) ,
                                                                isDel=VALUES(isDel) ,

                                                                CreateUser=VALUES(CreateUser) ,
                                                                CreateDate=VALUES(CreateDate) ,
                                                                LastModified=VALUES(LastModified)	,
                                                                LastModifyDate=VALUES(LastModifyDate) ";

            int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);

            return result;

        }

        public int isDelRows(int id)
        {
            string sql = @" 	UPDATE boxweight set isDel =1 WHERE id = " + id;
            int delRows = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
            return delRows;
        }
    }
}
