using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SabrinaVisa_SqlHelper
    {
        
        public static readonly string VisaSqlconnStr = ConfigurationManager.ConnectionStrings["SabrinaVisaSqlconnstr"].ConnectionString;
      //  public static readonly string connstr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
     //   public static int result;
        public static Object ToDbValue(Object value)
        {
            if (value == null)
            { return DBNull.Value; }
            else
            {
                return value;
            }
        }

        public static Object FromDbValue(Object value)
        {
            if (value == DBNull.Value)
            { return null; }
            else
            {
                return value;
            }
        }






        public static DataTable ExcuteTable(string sqlstr, string serviceName)
        {
           

            using (SqlConnection conn = new SqlConnection(serviceName))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = sqlstr;
                    DataSet dataset = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }

        public static DataTable ExcuteTable(string sqlstr, params SqlParameter[] ps)
        {

            using (SqlConnection conn = new SqlConnection(VisaSqlconnStr))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 180;
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.AddRange(ps);
                        DataSet dataset = new DataSet();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataset);
                        return dataset.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    DataTable tb = new DataTable();
                    return tb;
                }
            }
        }


        public static string SqlBulkCopyCertified(DataTable certified)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(VisaSqlconnStr))
            {
               
                bulkcopy.BulkCopyTimeout = 0;//超时设置
                bulkcopy.DestinationTableName = "T_certified";
                bulkcopy.ColumnMappings.Add("passportNumber", "passportNumber");
                bulkcopy.ColumnMappings.Add("passportIssueDate", "passportIssueDate");
                bulkcopy.ColumnMappings.Add("passportFinishDate", "passportFinishDate");
                bulkcopy.ColumnMappings.Add("passportSignArea", "passportSignArea");
                bulkcopy.ColumnMappings.Add("passportVisaNumber", "passportVisaNumber");
                bulkcopy.ColumnMappings.Add("passportVisaArea", "passportVisaArea");
                bulkcopy.ColumnMappings.Add("passportVisaTimeLimit", "passportVisaTimeLimit");
                bulkcopy.ColumnMappings.Add("passportVisaFinshDate", "passportVisaFinshDate");
                bulkcopy.ColumnMappings.Add("entryVisaDate", "entryVisaDate");
                bulkcopy.ColumnMappings.Add("workerCard", "workerCard");
                bulkcopy.ColumnMappings.Add("workerCardID", "workerCardID");
                bulkcopy.ColumnMappings.Add("healthCard", "healthCard");
                try
                {
                    bulkcopy.WriteToServer(certified);
                    return "200";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }


        public static string SqlBulkCopyEmployee(DataTable employee)
        { 
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(VisaSqlconnStr))
            {

                bulkcopy.BulkCopyTimeout = 0;//超时设置
                bulkcopy.DestinationTableName = "T_employee";
                bulkcopy.ColumnMappings.Add("passportNumber", "passportNumber");
                bulkcopy.ColumnMappings.Add("deptID", "deptID");
                bulkcopy.ColumnMappings.Add("subID", "subID");
                bulkcopy.ColumnMappings.Add("workID", "workID");
                bulkcopy.ColumnMappings.Add("userName", "userName");
                bulkcopy.ColumnMappings.Add("userNameEN", "userNameEN");
                bulkcopy.ColumnMappings.Add("userSex", "userSex");
                bulkcopy.ColumnMappings.Add("birthday", "birthday");
                bulkcopy.ColumnMappings.Add("education", "education");
                bulkcopy.ColumnMappings.Add("hometown", "hometown");
                bulkcopy.ColumnMappings.Add("phoneNumber", "phoneNumber");
                bulkcopy.ColumnMappings.Add("position", "position");

                bulkcopy.ColumnMappings.Add("entryDate", "entryDate");
                bulkcopy.ColumnMappings.Add("jobChange", "jobChange");
                bulkcopy.ColumnMappings.Add("assessDate", "assessDate");
                bulkcopy.ColumnMappings.Add("contractFinishDate", "contractFinishDate");
                bulkcopy.ColumnMappings.Add("tryFinishDate", "tryFinishDate");
                bulkcopy.ColumnMappings.Add("planResignDate", "planResignDate");
                bulkcopy.ColumnMappings.Add("resignDate", "resignDate");
                bulkcopy.ColumnMappings.Add("resignNote", "resignNote"); 
                try
                {
                    bulkcopy.WriteToServer(employee);
                    return "200";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }

        public static string SqlBulkCopyMsg(DataTable msg)
        { 
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(VisaSqlconnStr))
            {

                bulkcopy.BulkCopyTimeout = 0;//超时设置
                bulkcopy.DestinationTableName = "T_msg";
                bulkcopy.ColumnMappings.Add("workID", "workID");
                bulkcopy.ColumnMappings.Add("msgTxt", "msgTxt");
                bulkcopy.ColumnMappings.Add("msgCheck", "msgCheck"); 
                try
                {
                    bulkcopy.WriteToServer(msg);
                    return "200";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }
        public static int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(VisaSqlconnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = sql;  
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
