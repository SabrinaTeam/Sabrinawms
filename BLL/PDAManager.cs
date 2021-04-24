using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PDAManager
    {
        public PDAManagerService ps = new PDAManagerService();

        public string writePDAManagerToData(DataTable dt)
        {
           
            DataTable isHaveId = new DataTable();
            isHaveId.Columns.Add("ID", typeof(int));
            isHaveId.Columns.Add("devUUID", typeof(string));
            isHaveId.Columns.Add("devNumber", typeof(string));
            isHaveId.Columns.Add("buyDate", typeof(string));
            isHaveId.Columns.Add("devName", typeof(string));
            isHaveId.Columns.Add("devMode", typeof(string));
            isHaveId.Columns.Add("userDept", typeof(string));
            isHaveId.Columns.Add("userDate", typeof(string));
            isHaveId.Columns.Add("userName", typeof(string));
            isHaveId.Columns.Add("mark", typeof(string)); 

            DataTable isNotId = new DataTable();
            isNotId.Columns.Add("ID", typeof(int));
            isNotId.Columns.Add("devUUID", typeof(string));
            isNotId.Columns.Add("devNumber", typeof(string));
            isNotId.Columns.Add("buyDate", typeof(string));
            isNotId.Columns.Add("devName", typeof(string));
            isNotId.Columns.Add("devMode", typeof(string));
            isNotId.Columns.Add("userDept", typeof(string));
            isNotId.Columns.Add("userDate", typeof(string));
            isNotId.Columns.Add("userName", typeof(string));
            isNotId.Columns.Add("mark", typeof(string)); 

            int wr = 0;
            int ur = 0;
            //有ID的更新 ，没有ID的新增
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ID"].ToString() != "0")
                {
                    DataRow row = isHaveId.NewRow();
                    row["ID"] = dt.Rows[i]["ID"].ToString();
                    row["devUUID"] = dt.Rows[i]["devUUID"].ToString();
                    row["devNumber"] = dt.Rows[i]["devNumber"].ToString();
                    row["buyDate"] = dt.Rows[i]["buyDate"].ToString();
                    row["devName"] = dt.Rows[i]["devName"].ToString();
                    row["devMode"] = dt.Rows[i]["devMode"].ToString();
                    row["userDept"] = dt.Rows[i]["userDept"].ToString();
                    row["userDate"] = dt.Rows[i]["userDate"].ToString();
                    row["userName"] = dt.Rows[i]["userName"].ToString();
                    row["mark"] = dt.Rows[i]["mark"].ToString();                    
                    isHaveId.Rows.Add(row);
                }
                else
                {
                    DataRow row = isNotId.NewRow();
                    row["ID"] = dt.Rows[i]["ID"].ToString();
                    row["devUUID"] = dt.Rows[i]["devUUID"].ToString();
                    row["devNumber"] = dt.Rows[i]["devNumber"].ToString();
                    row["buyDate"] = dt.Rows[i]["buyDate"].ToString();
                    row["devName"] = dt.Rows[i]["devName"].ToString();
                    row["devMode"] = dt.Rows[i]["devMode"].ToString();
                    row["userDept"] = dt.Rows[i]["userDept"].ToString();
                    row["userDate"] = dt.Rows[i]["userDate"].ToString();
                    row["userName"] = dt.Rows[i]["userName"].ToString();
                    row["mark"] = dt.Rows[i]["mark"].ToString();
                    isNotId.Rows.Add(row);
                }
            }
            if (isHaveId.Rows.Count > 0)
            {


                ur = ps.updatePMToData(isHaveId);
            }
            if (isNotId.Rows.Count > 0)
            {
                wr = ps.writePMToData(isNotId);
            }
            return "共新增" + wr.ToString() + "条记录，更新 " + ur.ToString() + "条记录";
        }
        public DataTable searchPDABuyUUID(List<string> devs,bool selected)
        {
            return ps.searchPDABuyUUID(devs, selected);
        }
       
    }
}
