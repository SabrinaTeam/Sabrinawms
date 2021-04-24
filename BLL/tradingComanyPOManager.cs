using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace BLL
{
    public class tradingComanyPOManager
    {
        tradingComanyPOService tcs = new tradingComanyPOService();
        public DataTable ExcelRead(String filename, string sheetname, int headno) 
        {
            COMMON.NPOIExcelExample NPOIexcel = new COMMON.NPOIExcelExample();
            tradingComanyPO[] gtnPOS = NPOIexcel.ExcelRead(filename, sheetname, headno);
            if (gtnPOS == null)
            {
                
                return null;
            }
            /*本地表*/
            //创建本地表
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("PO",typeof(string));
            table.Columns.Add("GTN_PO", typeof(string));
            table.Columns.Add("create_pc", typeof(string));
            table.Columns.Add("update_date", typeof(string));

            table.Columns.Add("fCreate_Date", typeof(string));
            table.Columns.Add("fIssue_Date", typeof(string));
            table.Columns.Add("fOrder_Status", typeof(string));
            table.Columns.Add("fOrder_Total_Qty", typeof(string));
            table.Columns.Add("fInvoiced_Item_Qty", typeof(string));
            try
            {
                for (int i = 0; i < gtnPOS.Count(); i++)
                {
                    
                    String AID = Convert.ToString(gtnPOS[i].id);
                    String APO = Convert.ToString(gtnPOS[i].PO);
                    String AGTN_PO = Convert.ToString(gtnPOS[i].GTN_PO);
                    String Acreate_pc = Dns.GetHostName().ToString();
                    String Aupdate_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
                    
                    String fCreate_Date = Convert.ToString(gtnPOS[i].fCreate_Date);
                    String fIssue_Date = Convert.ToString(gtnPOS[i].fIssue_Date);
                    String fOrder_Status = Convert.ToString(gtnPOS[i].fOrder_Status);
                    String fOrder_Total_Qty = Convert.ToString(gtnPOS[i].fOrder_Total_Qty);
                    String fInvoiced_Item_Qty = Convert.ToString(gtnPOS[i].fInvoiced_Item_Qty);

                    //本地表加入数据  Unique
                    DataRow row = table.NewRow();
                    row["id"] = AID;
                    row["PO"] = APO;
                    row["GTN_PO"] = AGTN_PO;
                    row["create_pc"] = Acreate_pc;
                    row["update_date"] = Aupdate_date;

                    row["fCreate_Date"] = fCreate_Date;
                    row["fIssue_Date"] = fIssue_Date;
                    row["fOrder_Status"] = fOrder_Status;
                    row["fOrder_Total_Qty"] = fOrder_Total_Qty;
                    row["fInvoiced_Item_Qty"] = fInvoiced_Item_Qty;

                    table.Rows.Add(row);
                    /*************/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return table;
        }

        public bool isHaveByTradingComanyPO(string Tpo)
        {
            int i = tcs.isHaveByTradingComanyPO(Tpo);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int writeGtnsToDb(DataTable table)
        {

            DataTable gtnPODT = new DataTable();
            gtnPODT.Columns.Add("PO", typeof(string));
            gtnPODT.Columns.Add("GTN_PO", typeof(string));
            gtnPODT.Columns.Add("create_pc", typeof(string));
            gtnPODT.Columns.Add("update_date", typeof(string));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow erow = gtnPODT.NewRow();
                erow["PO"] = table.Rows[i]["PONumber"].ToString() ;
                erow["GTN_PO"] = table.Rows[i]["TradingCompanyPO"].ToString();
                erow["create_pc"] = Dns.GetHostName().ToString();
                erow["update_date"] = DateTime.Now.ToString("yyyy-MM-dd");
                gtnPODT.Rows.Add(erow);
            }
            return tcs.writeGtnsToDb(gtnPODT);


        }

        public int delDoubleRows()
        { 
           return  tcs.delDoubleRows();
        }
    }
}
