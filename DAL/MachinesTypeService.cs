using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public  class MachinesTypeServices
    {

        public  DataTable getAllMachineTypes()
        {
            string sql = @"SELECT  
                                   machineClass,
                                   MachineName,
                                   machineNameEN,
                                   machinesMark,
                                   CreateDate,
                                   Creator,
                                   modify,
                                   modifor,
                                   ismachinesStatus
                            FROM machinetypes
                            ORDER BY ismachinesStatus,                                    
                                     machineClass; ";


           DataTable dt = IEBOM_SqlHelper.ExcuteTable(sql);
            return dt;

            /*

            List<Machines> machinesLists = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                machinesLists = new List<Machines>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.Machines c = new MODEL.Machines();
                    dataToMachines(row, c);
                    machinesLists.Add(c);
                }
            }

            return machinesLists;
            */

        }

        public void dataToMachines(DataRow dr, MODEL.Machines list)
        {

            list.machineClass = Convert.ToString(IEBOM_SqlHelper.FromDbValue(dr["machineClass"])); // 机器类别
            list.machineName = Convert.ToString(IEBOM_SqlHelper.FromDbValue(dr["MachineName"])); // 机器类别名称
            list.machineNameEN = Convert.ToString(IEBOM_SqlHelper.FromDbValue(dr["machineNameEN"]));   // 机器类别名称英文
            list.MachinesMark = Convert.ToString(IEBOM_SqlHelper.FromDbValue(dr["machinesMark"]));  // 机器备注
            list.isMachinesStatus = Convert.ToInt32(IEBOM_SqlHelper.FromDbValue(dr["isMachinesStatus"])); // 机器类别状态
            list.CreateDate = Convert.ToDateTime(IEBOM_SqlHelper.FromDbValue(dr["CreateDate"])); // 创建时间
            list.Creator = Convert.ToString(IEBOM_SqlHelper.FromDbValue(dr["Creator"]));  // 创建者
            list.modify = Convert.ToDateTime(IEBOM_SqlHelper.FromDbValue(dr["modify"]));   // 最后修改时间
            list.modifor = Convert.ToString(IEBOM_SqlHelper.FromDbValue(dr["modifor"]));  //  最后修改者

        }


        public List<string> saveMachineTypes(DataTable machineDT)
        {
            List<string> results = new List<string>();

            DataTable newDt = new DataTable();
            //  newDt.Columns.Add(new DataColumn("machineID", typeof(int))); //
            newDt.Columns.Add(new DataColumn("machineClass", typeof(string))); //机器类别
            newDt.Columns.Add(new DataColumn("MachineName", typeof(string))); //机器类别名称
            newDt.Columns.Add(new DataColumn("machineNameEN", typeof(string)));//机器类别名称英文
            newDt.Columns.Add(new DataColumn("machinesMark", typeof(string)));//机器备注
            newDt.Columns.Add(new DataColumn("isMachinesStatus", typeof(int)));//机器类别状态
            newDt.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));// 创建时间
            newDt.Columns.Add(new DataColumn("Creator", typeof(string)));// 创建者
            newDt.Columns.Add(new DataColumn("modify", typeof(DateTime))); // 最后修改时间
            newDt.Columns.Add(new DataColumn("modifor", typeof(string)));  //  最后修改者


            for (int i = 0; i < machineDT.Rows.Count; i++)
            {
                DataRow dr = newDt.NewRow();
                dr["machineClass"] = machineDT.Rows[i]["machineClass"];
                dr["MachineName"] = machineDT.Rows[i]["MachineName"];
                dr["machineNameEN"] = machineDT.Rows[i]["machineNameEN"];
                dr["machinesMark"] = machineDT.Rows[i]["machinesMark"];

                dr["isMachinesStatus"] = machineDT.Rows[i]["ismachinesStatus"];
                dr["CreateDate"] = machineDT.Rows[i]["CreateDate"];
                dr["Creator"] = machineDT.Rows[i]["Creator"];
                dr["modify"] = machineDT.Rows[i]["modify"];
                dr["modifor"] = machineDT.Rows[i]["modifor"];

                newDt.Rows.Add(dr);
            }

            // 清空临时表
            string empTmp = @"DELETE FROM machineTypes_tmp";
            IEBOM_SqlHelper.ExecuteNonQuery(empTmp);


            //写入临时表
            string result = IEBOM_SqlHelper.SqlBulkCopyMachineTypes(newDt);
            int newData = 0;


            // 写入成功 调用存储过程 失败返回原因
            if (result == "0")
            {
                string callStudent = @"exec get_NewMachines;";
                newData = IEBOM_SqlHelper.ExecuteNonQuery(callStudent);
              //  return newData.ToString();
                results.Add("0");
                results.Add(newData.ToString());
            }
            else
            {
                results.Add(result);
                results.Add("0");
            }

            return results;
        }


        public int delMachineTypeByNames (string machineName)
        {
            string sql = @"DELETE FROM machineTypes WHERE machineName ='" + machineName + "'";
            int delRows = IEBOM_SqlHelper.ExecuteNonQuery (sql);
            return delRows;

        }
    }
}
