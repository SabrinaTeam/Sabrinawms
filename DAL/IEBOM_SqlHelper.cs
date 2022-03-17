using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IEBOM_SqlHelper
    {
        public static readonly string IEBomSQLconnstr = ConfigurationManager.ConnectionStrings["IEBomSQLconnstr"].ConnectionString;


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

        public static DataTable ExcuteTable(string sqlstr)
        {

            using (SqlConnection conn = new SqlConnection(IEBomSQLconnstr))
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

        public static DataTable ExcuteTable(  string sqlstr, params SqlParameter[] ps)
        {

            using (SqlConnection conn = new SqlConnection(IEBomSQLconnstr))
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


        public static string SqlBulkCopy(DataTable table)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(IEBomSQLconnstr))
            {
                bulkcopy.BulkCopyTimeout = 0;//超时设置
                bulkcopy.DestinationTableName = "StandardModulus";
                bulkcopy.ColumnMappings.Add("CureNamesID", "CureNamesID");
                bulkcopy.ColumnMappings.Add("CArea", "CArea");
                bulkcopy.ColumnMappings.Add("Clevel", "Clevel");
                bulkcopy.ColumnMappings.Add("CsingleMinute", "CsingleMinute");
                bulkcopy.ColumnMappings.Add("Cratio", "Cratio");

                bulkcopy.ColumnMappings.Add("COneday", "COneday");
                bulkcopy.ColumnMappings.Add("CTwoDay", "CTwoDay");
                bulkcopy.ColumnMappings.Add("CThreeDay", "CThreeDay");
                bulkcopy.ColumnMappings.Add("CFourDay", "CFourDay");
                bulkcopy.ColumnMappings.Add("CFiveDay", "CFiveDay");
                bulkcopy.ColumnMappings.Add("CSixDay", "CSixDay");
                bulkcopy.ColumnMappings.Add("CSevenDay", "CSevenDay");

                bulkcopy.ColumnMappings.Add("CEightDay", "CEightDay");
                bulkcopy.ColumnMappings.Add("CNineDay", "CNineDay");
                bulkcopy.ColumnMappings.Add("CTenDay", "CTenDay");
                bulkcopy.ColumnMappings.Add("CElevenDay", "CElevenDay");
                bulkcopy.ColumnMappings.Add("CTwelveDay", "CTwelveDay");
                bulkcopy.ColumnMappings.Add("CThirteenDay", "CThirteenDay");
                bulkcopy.ColumnMappings.Add("CFourteenDay", "CFourteenDay");


                bulkcopy.ColumnMappings.Add("Creator", "Creator");
                bulkcopy.ColumnMappings.Add("CreateDate", "CreateDate");
                bulkcopy.ColumnMappings.Add("modiyDate", "modiyDate");
                bulkcopy.ColumnMappings.Add("modiyed", "modiyed");
                bulkcopy.ColumnMappings.Add("isNewStyle", "isNewStyle");

                try
                {
                    bulkcopy.WriteToServer(table);
                    return "0";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }

        }

        public static int ExcuteScalar<T>(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(IEBomSQLconnstr))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandTimeout = 0;
                comm.Parameters.AddRange(ps);
                object dd = comm.ExecuteScalar();
                return  Convert.ToInt32(dd);

            }
        }


        public static int ExecuteNonQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(IEBomSQLconnstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(ps);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static string SqlBulkCopyMachineTypes(DataTable table)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(IEBomSQLconnstr))
            {
                bulkcopy.BulkCopyTimeout = 0;//超时设置
                bulkcopy.DestinationTableName = "machineTypes_tmp";
                bulkcopy.ColumnMappings.Add("machineClass", "machineClass");
                bulkcopy.ColumnMappings.Add("MachineName", "machineName");
                bulkcopy.ColumnMappings.Add("machineNameEN", "machineNameEN");
                bulkcopy.ColumnMappings.Add("machinesMark", "MachinesMark");

                bulkcopy.ColumnMappings.Add("isMachinesStatus", "isMachinesStatus");
                bulkcopy.ColumnMappings.Add("CreateDate", "CreateDate");
                bulkcopy.ColumnMappings.Add("Creator", "Creator");
                bulkcopy.ColumnMappings.Add("modify", "modify");
                bulkcopy.ColumnMappings.Add("modifor", "modifor");

                try
                {
                    bulkcopy.WriteToServer(table);
                    return "0";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }

        }
    }
}
