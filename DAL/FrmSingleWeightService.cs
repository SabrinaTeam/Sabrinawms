using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FrmSingleWeightService
    {
        public int insetRowsToDb(DataTable dt)
        {
            string sqlValue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sqlValue = sqlValue +
                          "(\"" + dt.Rows[i]["custID"].ToString() + "\",\""
                                  + dt.Rows[i]["StyleID"].ToString() + "\",\""
                                  + dt.Rows[i]["SizeID"].ToString() + "\",\""
                                  + dt.Rows[i]["singleWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["Note"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateUser"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateDate"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModified"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModifyDate"].ToString() + "\",\""
                                  + dt.Rows[i]["isDel"].ToString() + "\" ),";

            }

            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) + ";";
            string sqlstr = @"INSERT INTO singleweight (
                                                        custID, StyleID, SizeID, 
                                                        singleWeight, Note, CreateUser, 
                                                        CreateDate, LastModified, 
                                                        LastModifyDate,isDel )  VALUES " + sqlValue;
            int  result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            return result;
        }
        public int updateRowsToDb(DataTable dt)
        {
            string sqlValue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sqlValue = sqlValue +
                              "(\"" + dt.Rows[i]["NID"].ToString() + "\",\""
                                  + dt.Rows[i]["NcustID"].ToString() + "\",\""
                                  + dt.Rows[i]["NStyleID"].ToString() + "\",\""
                                  + dt.Rows[i]["NSizeID"].ToString() + "\",\""
                                  + dt.Rows[i]["NsingleWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["NNote"].ToString() + "\",\""
                                  + dt.Rows[i]["NCreateUser"].ToString() + "\",\""
                                  + dt.Rows[i]["NCreateDate"].ToString() + "\",\""
                                  + dt.Rows[i]["NLastModified"].ToString() + "\",\""
                                   + dt.Rows[i]["NLastModifyDate"].ToString() + "\",\""
                                  + dt.Rows[i]["isDel"].ToString() + "\" ),";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) ;
            string sqlstr = @"INSERT INTO singleweight  (ID,
                                                        custID, StyleID, SizeID, singleWeight, 
                                                        Note, CreateUser, CreateDate, LastModified,
                                                        LastModifyDate,isDel )  VALUES " + sqlValue;

            sqlstr = sqlstr + @" 	ON DUPLICATE KEY UPDATE ID=VALUES(ID),
                                                                custID=VALUES(custID),
                                                                StyleID=VALUES(StyleID) ,
                                                                SizeID=VALUES(SizeID)	,
                                                                singleWeight=VALUES(singleWeight),
                                                                Note=VALUES(Note) ,
                                                                CreateUser=VALUES(CreateUser) ,
                                                                CreateDate=VALUES(CreateDate) ,
                                                                LastModified=VALUES(LastModified)	,
                                                                LastModifyDate=VALUES(LastModifyDate) ,
                                                                isDel=VALUES(isDel) ";

            int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);

            return result;

        }
        public DataTable getSingleWeights(string custid, string styleid)
        {
            string sql = @"SELECT
	                                id,
	                                CustID,
	                                styleId,
	                                sizeID,
	                                singleweight,
	                                Note,
	                                createUser,
	                                DATE_FORMAT(createdate,'%Y-%m-%d %H:%i:%s') createdate,
	                                lastmodified,
	                                DATE_FORMAT(lastmodifydate,'%Y-%m-%d %H:%i:%s') lastmodifydate
                                FROM
	                                singleweight 
                                WHERE
	                                1 = 1 
	                                AND CustID like '%" + custid + @"%'
	                                AND styleId like '%" + styleid + @"%'
	                                and isDel = 0;";

            DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);

            return dt;
        }
        public int isDelRows(int id)
        {
            string sql = @" 	UPDATE singleweight set isDel =1 WHERE id = " + id;
            int delRows = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
            return delRows;
        }
    }
}
