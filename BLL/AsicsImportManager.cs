using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class AsicsImportManager
    {
        AsicsImportServer ais = new AsicsImportServer();
        public List<DataTable> ExcelRead(String filename, string sheetname, int headno)
        {
            COMMON.NPOIExcelAsicsImport NPOIexcel = new COMMON.NPOIExcelAsicsImport();
            List<DataTable> ld = NPOIexcel.ExcelRead(filename, sheetname, headno);
            return ld;            
        }
        public int uploadToMysql(DataTable dt)
        {
            DataTable OrderItemSum = new DataTable();
            if (dt.Rows.Count > 0)
            {
                OrderItemSum.Columns.Add("id");
                OrderItemSum.Columns.Add("Cust_id");
                OrderItemSum.Columns.Add("Serial_From");
                OrderItemSum.Columns.Add("qty");
                OrderItemSum.Columns.Add("org");
                OrderItemSum.Columns.Add("PPrfNo");
                OrderItemSum.Columns.Add("count1");
                OrderItemSum.Columns.Add("create_pc");
                OrderItemSum.Columns.Add("update_date");
                OrderItemSum.Columns.Add("con_no");
                OrderItemSum.Columns.Add("country_code");
                OrderItemSum.Columns.Add("con_to");
                OrderItemSum.Columns.Add("Pkg_Code");
                OrderItemSum.Columns.Add("Scan_ID");
                OrderItemSum.Columns.Add("Net_Net");
                OrderItemSum.Columns.Add("con_net");
                OrderItemSum.Columns.Add("con_Gross");
                OrderItemSum.Columns.Add("con_L");
                OrderItemSum.Columns.Add("con_W");
                OrderItemSum.Columns.Add("con_H");
                OrderItemSum.Columns.Add("b_Volume");
                OrderItemSum.Columns.Add("PO");
                OrderItemSum.Columns.Add("MAIN_LINE");

                List<string> ids = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["id"].ToString().Trim(); ;
                    if (!IDisExisted(ids, id))
                    {
                        ids.Add(id);

                        DataRow dr = OrderItemSum.NewRow();
                        dr["id"] = ids[ids.Count -1];
                        dr["Cust_id"] = dt.Rows[i]["Cust_id"].ToString();
                        dr["Serial_From"] = dt.Rows[i]["Serial_From"].ToString();
                        dr["qty"] = dt.Rows[i]["qty"].ToString();
                        dr["org"] = dt.Rows[i]["org"].ToString();
                        dr["PPrfNo"] = dt.Rows[i]["PPrfNo"].ToString();
                        dr["count1"] = dt.Rows[i]["count1"].ToString();
                        dr["create_pc"] = dt.Rows[i]["create_pc"].ToString();
                        dr["update_date"] = dt.Rows[i]["update_date"].ToString();
                        dr["con_no"] = dt.Rows[i]["con_no"].ToString();
                        dr["country_code"] = dt.Rows[i]["country_code"].ToString();
                        dr["con_to"] = dt.Rows[i]["con_to"].ToString();
                        dr["Pkg_Code"] = dt.Rows[i]["Pkg_Code"].ToString();
                        dr["Scan_ID"] = dt.Rows[i]["Scan_ID"].ToString();
                        dr["Net_Net"] = dt.Rows[i]["Net_Net"].ToString();
                        dr["con_net"] = dt.Rows[i]["con_net"].ToString();
                        dr["con_Gross"] = dt.Rows[i]["con_Gross"].ToString();
                        dr["con_L"] = dt.Rows[i]["con_L"].ToString();
                        dr["con_W"] = dt.Rows[i]["con_W"].ToString();
                        dr["con_H"] = dt.Rows[i]["con_H"].ToString();
                        dr["b_Volume"] = dt.Rows[i]["b_Volume"].ToString();
                        dr["PO"] = dt.Rows[i]["PO"].ToString();
                        dr["MAIN_LINE"] = dt.Rows[i]["MAIN_LINE"].ToString();
                        OrderItemSum.Rows.Add(dr);
                    }
                }
                int qty = 0;
                if(ids.Count > 0)
                {
                    for (int i = 0; i < ids.Count; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if(ids[i] == dt.Rows[j]["id"].ToString())
                            {
                                qty = qty + Convert.ToInt32( dt.Rows[j]["qty"].ToString()); 
                            }
                        }
                        OrderItemSum.Rows[i]["qty"] = qty.ToString();
                        qty = 0;
                    }
                }
               
            }
            return ais.uploadToMysql(OrderItemSum);
        }
        
        

        public bool IDisExisted(List<string> list ,string id)
        {
            bool result = false;
            if (list.Count() <=0)
            {
                result = false;
            }
            foreach (var l in list)
            {
                if(l == id)
                {
                    result = true;
                    break;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        public int uploadCon_detailToMysql(DataTable dt)
        {
            return ais.uploadCon_detailToMysql(dt);
        }

    }
}
