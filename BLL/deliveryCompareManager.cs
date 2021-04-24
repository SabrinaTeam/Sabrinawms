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
    public class deliveryCompareManager
    {
        deliveryCompareService dcs = new deliveryCompareService();
       
        public DataTable getSubinv(string subinv)
        {
            return dcs.getSubinv(subinv);
        }
        public DataTable getLocations(string org, string subinv)
        {
            return dcs.getLocations(org, subinv);
        }
        public DataTable getLocalHostDt(string starTime, string stopTime, string org, string subinv, List<string> location)
        {
              return dcs.getLocalHostDt( starTime, stopTime , org, subinv, location);
           
        }

        public DataTable getFromWriteExcel(string createPC, string starTime, string stopTime)
        {
              return dcs.getFromWriteExcel(createPC, starTime, stopTime);
           
        }

        /// <summary>
        /// 从BEST取回订单资料
        /// </summary>
        /// <param name="po_no"></param>
        /// <param name="style_id"></param>
        /// <param name="clr_no"></param>
        /// <returns></returns>
        public DataTable getOD_POFromBestByPSC(List<outGoing_pos> pscs)
        {
            //  return dcs.getOD_POFromBestByPSC(pscs);

            DataTable dt = new DataTable();
            return dt;

        }

        public DataTable ExcelRead(String filename, string sheetname, int headno)
        {
            COMMON.NPOIExcelDeliveryCompare NPOIexcel = new COMMON.NPOIExcelDeliveryCompare();
            DataTable dcQtys = NPOIexcel.ExcelRead(filename, sheetname, headno);
            if (dcQtys == null)
            {
                DataTable dt = new DataTable();
                return dt;               
            }
            //创建本地表
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("lineName", typeof(string));
            table.Columns.Add("deliveryDate", typeof(string));
            table.Columns.Add("invoiceNo", typeof(string));
            table.Columns.Add("styleId", typeof(string));

            table.Columns.Add("gtnPO", typeof(string));
            table.Columns.Add("idNoName", typeof(string));
            table.Columns.Add("colorId", typeof(string));
            table.Columns.Add("sizeName", typeof(string));
            table.Columns.Add("qty", typeof(string));

            try
            {
                // 从第一行开始  过滤掉列名称
                for (int i = 3; i < dcQtys.Rows.Count; i++)
                {
                    // String AID = dcQtys.Rows[i][0].ToString();
                    String AlineName = dcQtys.Rows[i][0].ToString();
                    String AdeliveryDate = dcQtys.Rows[i][1].ToString();
                    String AinvoiceNo = dcQtys.Rows[i][2].ToString();
                    String AstyleId = dcQtys.Rows[i][3].ToString();
                    String AgtnPO = dcQtys.Rows[i][4].ToString();
                    String AidNoName = dcQtys.Rows[i][5].ToString();
                    String AcolorId = dcQtys.Rows[i][6].ToString();

                    /*转换为行排SIZE表*/
                    if (dcQtys.Columns.Count > 7)
                    {
                        for (int  j= 7; j < dcQtys.Columns.Count; j++)
                        {
                            // Qty                                           Size                                 gtnPO                                colorId                 
                            if (dcQtys.Rows[i][j].ToString() == "" || dcQtys.Rows[1][j].ToString() == "" || dcQtys.Rows[i][4].ToString() == "" || dcQtys.Rows[i][6].ToString() == "")
                            {
                                continue;
                            }
                            String AsizeName = dcQtys.Rows[1][j].ToString();
                            DataRow row = table.NewRow();
                            row["lineName"] = AlineName;
                            row["deliveryDate"] = AdeliveryDate;
                            row["invoiceNo"] = AinvoiceNo;
                            row["styleId"] = AstyleId;
                            row["gtnPO"] = AgtnPO;
                            row["idNoName"] = AidNoName;
                            row["colorId"] = AcolorId;
                            row["sizeName"] = AsizeName;
                            row["qty"] = dcQtys.Rows[i][j].ToString();                            
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                DataTable dt = new DataTable();
                return dt;
            }
            return table;
        }

        
        public int writeCompareFileToDb(DataTable table)
        {

            return dcs.writeCompareFileToDb(table);
        }

        public int delDoubleRows()
        {
            return 0;
           // return dcs.delDoubleRows();
        }
    }
}
