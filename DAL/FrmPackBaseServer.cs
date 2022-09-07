using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class FrmPackBaseServer
	{
		public DataTable getPackBase(string custid, string styleid, string boxName)
		{
			string sql = @"SELECT
									id,
									cust_id,
									style_id,
									box_name,
									sizes,
									qtys,
									remark,
									deductionWeight,
									bagWeight,
									clapBoardTotalWeight,
									accessoriesTotalWeight,
									isDel,
									createUser,
									DATE_FORMAT(createdate,'%Y-%m-%d %H:%i:%s') createdate,
									lastmodified,
									DATE_FORMAT(lastmodifydate,'%Y-%m-%d %H:%i:%s') lastmodifydate
								FROM
									packbaseweight 
								WHERE
									1 = 1 
									AND cust_id like '%" + custid + @"%'
									AND style_id like '%" + styleid + @"%'
									AND box_name like '%" + boxName + @"%'
									and isDel = 0;";

			DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);

			return dt;
		}

        public int insetRowsToDb(DataTable dt)
        {
            string sqlValue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sqlValue = sqlValue +
                          "(\"" + dt.Rows[i]["cust_id"].ToString() + "\",\""
                                  + dt.Rows[i]["style_id"].ToString() + "\",\""
                                  + dt.Rows[i]["box_name"].ToString() + "\",\""
                                  + dt.Rows[i]["sizes"].ToString() + "\",\""
                                  + dt.Rows[i]["qtys"].ToString() + "\",\""
                                  + dt.Rows[i]["remark"].ToString() + "\",\""
                                  + dt.Rows[i]["deductionWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["bagWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["clapBoardTotalWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["accessoriesTotalWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["isDel"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateUser"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateDate"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModified"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModifyDate"].ToString() + "\" ),";

            }

            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) + ";";
            string sqlstr = @"INSERT INTO packbaseweight (
                                                        cust_id, style_id, box_name, sizes, qtys, remark, 
                                                        deductionWeight, bagWeight,clapBoardTotalWeight,
                                                        accessoriesTotalWeight,  isDel, CreateUser, 
                                                        CreateDate, LastModified,LastModifyDate
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
                         "(\"" + dt.Rows[i]["id"].ToString() + "\",\""
                                  + dt.Rows[i]["cust_id"].ToString() + "\",\""
                                  + dt.Rows[i]["style_id"].ToString() + "\",\""
                                  + dt.Rows[i]["box_name"].ToString() + "\",\""
                                  + dt.Rows[i]["sizes"].ToString() + "\",\""
                                  + dt.Rows[i]["qtys"].ToString() + "\",\""
                                  + dt.Rows[i]["remark"].ToString() + "\",\""
                                  + dt.Rows[i]["deductionWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["bagWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["clapBoardTotalWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["accessoriesTotalWeight"].ToString() + "\",\""
                                  + dt.Rows[i]["isDel"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateUser"].ToString() + "\",\""
                                  + dt.Rows[i]["CreateDate"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModified"].ToString() + "\",\""
                                  + dt.Rows[i]["LastModifyDate"].ToString() + "\" ),";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1);
            string sqlstr = @"INSERT INTO packbaseweight  (ID,
                                                        cust_id, style_id, box_name, sizes, qtys, remark, 
                                                        deductionWeight, bagWeight,clapBoardTotalWeight,
                                                        accessoriesTotalWeight,  isDel, CreateUser, 
                                                        CreateDate, LastModified,LastModifyDate
                                                    )  VALUES " + sqlValue;

            sqlstr = sqlstr + @" 	ON DUPLICATE KEY UPDATE ID=VALUES(ID),
                                                                cust_id=VALUES(cust_id),
                                                                style_id=VALUES(style_id) ,
                                                                box_name=VALUES(box_name)	,
                                                                sizes=VALUES(sizes),
                                                                qtys=VALUES(qtys) ,
                                                                remark=VALUES(remark) ,
                                                                deductionWeight=VALUES(deductionWeight) ,
                                                                bagWeight=VALUES(bagWeight) ,
                                                                clapBoardTotalWeight=VALUES(clapBoardTotalWeight) ,
                                                                accessoriesTotalWeight=VALUES(accessoriesTotalWeight) ,
                                                                isDel=VALUES(isDel) , 
                                                                CreateUser=VALUES(CreateUser) ,
                                                                CreateDate=VALUES(CreateDate) ,
                                                                LastModified=VALUES(LastModified)	,
                                                                LastModifyDate=VALUES(LastModifyDate)  ";

            int result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            return result;
        }
        public int isDelRows(int id)
        {
            string sql = @" 	UPDATE packbaseweight set isDel =1 WHERE id = " + id;
            int delRows = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
            return delRows;
        }

        public DataTable getAllCustidByFsgDB()
        {
            string sql = @"select DISTINCT custID from singleweight;";
            DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            return dt;
        }
        public DataTable getAllStyleIDByFsgDB(string custid)
        {
            string sql = @"select DISTINCT styleID from singleweight WHERE custID like '%"+ custid + @"%';";
            DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            return dt;
        }
        public DataTable getAllBoxNamesIDByfsgDB(string custid)
        {
            string sql = @"select DISTINCT box_name from boxweight WHERE cust_id  like  '%" + custid + @"%'";
            DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            return dt;
        }
        public DataTable getAllSizesByStyle(string custId, string styleId)
        {
            string sql = @"select DISTINCT SizeID from singleweight WHERE custID like '%" + custId + @"%' and styleID like '%"+ styleId + @"%'";
            DataTable dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            return dt;
        }
    }
}
