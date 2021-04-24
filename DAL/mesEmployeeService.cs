using MySql.Data.MySqlClient;
using Pomelo.Data.MyCat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;


 


namespace DAL
{
    public class mesEmployeeService
    {
        public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString; 

        public bool isExistsByaccount(string account)
        {
            string sql = @"SELECT id FROM mesusers WHERE account='" + account + "'";
          
            DataTable dt = new DataTable();
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sql);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            }

           

            if (dt.Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int addUser(string[] userInfo)
        {
          
            if (userInfo[2] == "")
            {
                return -2;
            };
            if (userInfo[5] == "")
            {
                return -3;
            };

            string sql = @"INSERT INTO mesusers (account,password,UserName,deptID,Marsk)VALUES(@account,@password,@UserName,@deptID,@Marsk);";
            
            int insets = 0;           
            if (MiddleWare == "1")
            {
                MyCatParameter[] p = {
                new MyCatParameter("account", userInfo[1]),
                new MyCatParameter("password", userInfo[2]),
                new MyCatParameter("UserName", userInfo[3]),
                new MyCatParameter("Marsk", userInfo[6]),
                new MyCatParameter("deptID", userInfo[5])
                };
                insets = MyCatfsg_SqlHelper.ExecuteNonQuery(sql, p);
            }
            else
            { 
                MySqlParameter[] p = {
                new MySqlParameter("account", userInfo[1]),
                new MySqlParameter("password", userInfo[2]),
                new MySqlParameter("UserName", userInfo[3]),
                new MySqlParameter("Marsk", userInfo[6]),
                new MySqlParameter("deptID", userInfo[5])
                };
                  insets = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql, p);
            }

            return insets;

        }

        public int updataUser(string[] userInfo)
        {
           
            string sql = "";
            if (userInfo[2] == "") {
                sql = @"UPDATE mesusers 
                                    SET account = @account,                                        
                                        UserName = @UserName,
                                        Marsk = @Marsk,
                                        deptID = @deptID 
                                    WHERE
	                                    ID = @ID;";
            }
            else
            {
                sql = @"UPDATE mesusers 
                                    SET account = @account, 
                                        password = @password,
                                        UserName = @UserName,
                                        Marsk = @Marsk,
                                        deptID = @deptID 
                                    WHERE
	                                    ID = @ID;";

            }
            int updatas = 0;
            if (MiddleWare == "1")
            {
                MyCatParameter[] p = {             
                new MyCatParameter("ID", userInfo[0]),
                new MyCatParameter("account", userInfo[1]),
                new MyCatParameter("password", userInfo[2]),
                new MyCatParameter("UserName", userInfo[3]),
                new MyCatParameter("deptID", userInfo[5]),
                new MyCatParameter("Marsk", userInfo[6])
                };
                updatas = MyCatfsg_SqlHelper.ExecuteNonQuery(sql, p);
            }
            else
            {
                MySqlParameter[] p = {
                new MySqlParameter("ID", userInfo[0]),
                new MySqlParameter("account", userInfo[1]),
                new MySqlParameter("password", userInfo[2]),
                new MySqlParameter("UserName", userInfo[3]),
                new MySqlParameter("deptID", userInfo[5]),
                new MySqlParameter("Marsk", userInfo[6])
            };
                  updatas = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql, p);
            }
            return updatas;
        }

        public DataTable getUserInfo(string[] searchParameter)
        {
            // { account, processID, userName };
            string deptID = searchParameter[1];
            MyCatParameter[] cp =new MyCatParameter[3];
            MySqlParameter[] mp = new MySqlParameter[3];
            if (deptID == "-1")
            {
                deptID = "";
            }


            if (MiddleWare == "1")
            {
                cp[0] = new MyCatParameter("account", searchParameter[0]);
                cp[1] = new MyCatParameter("deptID", deptID);
                cp[2] = new MyCatParameter("userName", searchParameter[2]);
               
            }
            else
            {
                mp[0] = new MySqlParameter("account", searchParameter[0]);
                mp[1] = new MySqlParameter("deptID", deptID);
                mp[2] = new MySqlParameter("userName", searchParameter[2]);
                

            };
            //  account ,processID, userName 
            string sql = @"SELECT
	                            ID,
	                            account,
	                            userName,
	                            deptID,
	                            marsk 
                            FROM
	                            mesusers 
                            WHERE 1 = 1 and  
                            if(@account ='',0=0, account = @account ) and 
                            if(@deptID ='',0=0, deptID = @deptID ) and 
                            if(@userName ='',0=0, userName = @userName )
                            ORDER BY
	                            Id;";
            DataTable users = new DataTable();
            if (MiddleWare == "1")
                users = MyCatfsg_SqlHelper.ExcuteTable(sql, cp);
            else
            {
                users = Mysqlfsg_SqlHelper.ExcuteTable(sql, mp);
            };
            return users;
        }

        public int insetMesDepts(string[] depts)
        {
            string sql = @"
                        INSERT IGNORE INTO mesdepts ( DeptName, DeptNumber, Marsk ) SELECT
                                            DeptName,
                                            DeptNumber,
                                            Marsk 
                                            FROM

	                                            ( SELECT """+ depts[4] + @""" AS DeptName, 
                                                         """+ depts[5] + @""" AS DeptNumber,
                                                         """+ depts[7] + @""" AS Marsk FROM DUAL ) AS q 
                                            WHERE
                                                NOT EXISTS(
                                                SELECT
                                                    DeptNumber
                                                FROM
                                                    mesdepts
                                                WHERE
                                                mesdepts.DeptNumber = q.DeptNumber ) ";
            int insets = 0;
            if (MiddleWare == "1")
            {
                insets = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
            }
            else
            {
                insets = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
            }

            return insets;
        }
    }
} 