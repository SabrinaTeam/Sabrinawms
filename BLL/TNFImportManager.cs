using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace BLL
{
    
    public class TNFImportManagers
    {
        public TNFImportService TNFImport = new TNFImportService();
        public DataTable getPODataFromScanService(string startDate, string StopDate)
        {
            DataTable tnfDb = TNFImport.getPODataFromScanService(startDate,StopDate);
            DataColumn dc = tnfDb.Columns.Add("Serial");
            if (tnfDb.Rows.Count >0)
            {

                int serial = 0;
                string  oldStyle ="";
                for (int i = 0; i < tnfDb.Rows.Count; i++)
                {                   
                    //SELECT  id,Barcode,CartonNo,PackQty,Style,Size,Color,MasterPO,StyleDescription,Country
                    string style = tnfDb.Rows[i]["Style"].ToString();
                    string Size = tnfDb.Rows[i]["Size"].ToString();
                    if(style.Length  >= 11)
                    {
                        tnfDb.Rows[i]["Style"] = style.Substring(3, 5);
                        tnfDb.Rows[i]["Color"] = style.Substring(8, 3);
                    }
                 
                    int index =Size.IndexOf("REG");
                    if(index >= 0)
                    {
                        Size = Size.Remove(index, 3).Trim();
                    }
                    Size = Size.TrimStart(new char[] { '0' });
                    
                    tnfDb.Rows[i]["Size"] = Size;
                    if(oldStyle == "")
                    {
                        oldStyle = style;
                        //serial =1;
                     //   tnfDb.Rows[i]["Serial"] = serial;
                      //  continue;
                    }
                    
                    
                    if(oldStyle == style)
                    {
                            serial++;
                            tnfDb.Rows[i]["Serial"] = serial;
                    }
                    else
                    {
                        serial =1;
                        tnfDb.Rows[i]["Serial"] = serial;
                        oldStyle = style;
                    } 
                }
            }
            //款式+颜色 = 箱顺序号 style

            return tnfDb;
        }
        public DataTable getPODataFromScanService(string PONumber)
        {
            DataTable tnfDb =  TNFImport.getPODataFromScanService(PONumber);
            DataColumn dc = tnfDb.Columns.Add("Serial");
            if (tnfDb.Rows.Count > 0)
            {

                int serial = 0;
                string oldStyle = "";
                for (int i = 0; i < tnfDb.Rows.Count; i++)
                {
                    //SELECT  id,Barcode,CartonNo,PackQty,Style,Size,Color,MasterPO,StyleDescription,Country
                    string style = tnfDb.Rows[i]["Style"].ToString();
                    string Size = tnfDb.Rows[i]["Size"].ToString();
                    if (style.Length >= 11)
                    {
                        tnfDb.Rows[i]["Style"] = style.Substring(3, 5);
                        tnfDb.Rows[i]["Color"] = style.Substring(8, 3);
                    }

                    int index = Size.IndexOf("REG");
                    if (index >= 0)
                    {
                        Size = Size.Remove(index, 3).Trim();
                    }
                    Size = Size.TrimStart(new char[] { '0' });

                    tnfDb.Rows[i]["Size"] = Size;
                    if (oldStyle == "")
                    {
                        oldStyle = style;
                        //serial =1;
                        //   tnfDb.Rows[i]["Serial"] = serial;
                        //  continue;
                    }


                    if (oldStyle == style)
                    {
                        serial++;
                        tnfDb.Rows[i]["Serial"] = serial;
                    }
                    else
                    {
                        serial = 1;
                        tnfDb.Rows[i]["Serial"] = serial;
                        oldStyle = style;
                    }
                }
            }
            //款式+颜色 = 箱顺序号 style
            return tnfDb;
        }
        public DataTable getPODataFromScanService(int Id)
        {
            DataTable tnfDb =   TNFImport.getPODataFromScanService(Id);
            DataColumn dc = tnfDb.Columns.Add("Serial");
            if (tnfDb.Rows.Count > 0)
            {

                int serial = 0;
                string oldStyle = "";
                for (int i = 0; i < tnfDb.Rows.Count; i++)
                {
                    //SELECT  id,Barcode,CartonNo,PackQty,Style,Size,Color,MasterPO,StyleDescription,Country
                    string style = tnfDb.Rows[i]["Style"].ToString();
                    string Size = tnfDb.Rows[i]["Size"].ToString();
                    if (style.Length >= 11)
                    {
                        tnfDb.Rows[i]["Style"] = style.Substring(3, 5);
                        tnfDb.Rows[i]["Color"] = style.Substring(8, 3);
                    }

                    int index = Size.IndexOf("REG");
                    if (index >= 0)
                    {
                        Size = Size.Remove(index, 3).Trim();
                    }
                    Size = Size.TrimStart(new char[] { '0' });

                    tnfDb.Rows[i]["Size"] = Size;
                    if (oldStyle == "")
                    {
                        oldStyle = style;
                        //serial =1;
                        //   tnfDb.Rows[i]["Serial"] = serial;
                        //  continue;
                    }


                    if (oldStyle == style)
                    {
                        serial++;
                        tnfDb.Rows[i]["Serial"] = serial;
                    }
                    else
                    {
                        serial = 1;
                        tnfDb.Rows[i]["Serial"] = serial;
                        oldStyle = style;
                    }
                }
            }
            //款式+颜色 = 箱顺序号 style
            return tnfDb;
        }
        public int getTnfMaxId()
        {
            return TNFImport.getTnfMaxId();
        }

        public int insetOrUpdataConPpr(DataTable dt)
        {
            string ids = "";
            int maxId = Convert.ToInt32(dt.AsEnumerable().Max(row => row["id"]));
            int OldId = TNFImport.getTnfMaxId();
            if(maxId > OldId)
            {
                TNFImport.upTnfMaxId(maxId);
            }
            
            
            foreach (DataRow row in dt.Rows)
            {
                ids = ids + "'TNF-" + row["id"].ToString()+"',";
            }
            if (ids == "")
            {
                return 0;
            }
           
            ids = ids.Substring(0, ids.Length - 1);
            DataTable result = TNFImport.getTnfDataFromFsgByConpprIds(ids);
            DataTable upDataDt = new DataTable();
            upDataDt.Columns.Add("id",typeof(string));
            upDataDt.Columns.Add("Cust_id", typeof(string));
            upDataDt.Columns.Add("Serial_From", typeof(string));
            upDataDt.Columns.Add("qty", typeof(string));
            upDataDt.Columns.Add("org", typeof(string));
            upDataDt.Columns.Add("PPrfNo", typeof(string));
            upDataDt.Columns.Add("count1", typeof(int));
            upDataDt.Columns.Add("create_pc", typeof(string));
            upDataDt.Columns.Add("update_date", typeof(string));
            upDataDt.Columns.Add("con_no", typeof(int));
            upDataDt.Columns.Add("country_code", typeof(string));
            upDataDt.Columns.Add("con_to", typeof(int));
            upDataDt.Columns.Add("Pkg_Code", typeof(string));
            upDataDt.Columns.Add("Scan_ID", typeof(string));
            upDataDt.Columns.Add("Net_Net", typeof(double));
            upDataDt.Columns.Add("Con_Net", typeof(double));
            upDataDt.Columns.Add("con_Gross", typeof(double));
            upDataDt.Columns.Add("con_l", typeof(double));
            upDataDt.Columns.Add("con_W", typeof(double));
            upDataDt.Columns.Add("con_H", typeof(double));
            upDataDt.Columns.Add("b_Volume", typeof(double));
            upDataDt.Columns.Add("PO", typeof(string));
            upDataDt.Columns.Add("MAIN_LINE", typeof(string)); 
            

            DataTable addDataDt = new DataTable();
            addDataDt.Columns.Add("id", typeof(string));
            addDataDt.Columns.Add("Cust_id", typeof(string));
            addDataDt.Columns.Add("Serial_From", typeof(string));
            addDataDt.Columns.Add("qty", typeof(string));
            addDataDt.Columns.Add("org", typeof(string));
            addDataDt.Columns.Add("PPrfNo", typeof(string));
            addDataDt.Columns.Add("count1", typeof(int));
            addDataDt.Columns.Add("create_pc", typeof(string));
            addDataDt.Columns.Add("update_date", typeof(string));
            addDataDt.Columns.Add("con_no", typeof(int));
            addDataDt.Columns.Add("country_code", typeof(string));
            addDataDt.Columns.Add("con_to", typeof(int));
            addDataDt.Columns.Add("Pkg_Code", typeof(string));
            addDataDt.Columns.Add("Scan_ID", typeof(string));
            addDataDt.Columns.Add("Net_Net", typeof(double));
            addDataDt.Columns.Add("Con_Net", typeof(double));
            addDataDt.Columns.Add("con_Gross", typeof(double));
            addDataDt.Columns.Add("con_l", typeof(double));
            addDataDt.Columns.Add("con_W", typeof(double));
            addDataDt.Columns.Add("con_H", typeof(double));
            addDataDt.Columns.Add("b_Volume", typeof(double));
            addDataDt.Columns.Add("PO", typeof(string));
            addDataDt.Columns.Add("MAIN_LINE", typeof(string));
            if (result.Rows.Count <= 0)
            {
                // 小于0 全部要新增
                // 大于0 部分新增   部分更新
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = addDataDt.NewRow();
                    dr["id"] = "TNF-" + dt.Rows[i]["id"].ToString();
                    dr["Cust_id"] = "TNF";
                    dr["Serial_From"] = dt.Rows[i]["CartonNo"].ToString();
                    dr["qty"] = dt.Rows[i]["PackQty"].ToString();
                    dr["org"] = "SAA";
                    dr["PPrfNo"] = dt.Rows[i]["MasterPO"].ToString();
                    dr["count1"] = 1;
                    dr["create_pc"] = Dns.GetHostName();
                    dr["update_date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dr["con_no"] = Convert.ToInt32( dt.Rows[i]["Serial"]); 
                    dr["country_code"] = null;
                    dr["con_to"] = Convert.ToInt32(dt.Rows[i]["Serial"]);
                    dr["Pkg_Code"] = dt.Rows[i]["Country"].ToString();
                    dr["Scan_ID"] = dt.Rows[i]["id"].ToString();
                    dr["Net_Net"] = 0.0;
                    dr["Con_Net"] = 0.0;
                    dr["con_Gross"] = 0.0;
                    dr["con_l"] = 0.0;
                    dr["con_W"] = 0.0;
                    dr["con_H"] = 0.0;
                    dr["b_Volume"] = 0.0;
                    dr["PO"] = dt.Rows[i]["MasterPO"].ToString();
                    dr["MAIN_LINE"] = null;
                    addDataDt.Rows.Add(dr);
                }
            }
            else
            {
                // 大于0 部分新增   部分更新
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < result.Rows.Count; j++)
                    {
                        // 相同ID部分 更新
                        if (result.Rows[j]["id"].ToString() == "TNF-" + dt.Rows[i]["id"].ToString())
                        {
                            // id,Barcode,CartonNo,PackQty,Style,Size,Color,MasterPO,StyleDescription 
                            DataRow dr = upDataDt.NewRow();
                            dr["id"] = "TNF-" + dt.Rows[i]["id"].ToString();
                            dr["Cust_id"] = "TNF";
                            dr["Serial_From"] = dt.Rows[i]["CartonNo"].ToString();
                            dr["qty"] = dt.Rows[i]["PackQty"].ToString();
                            dr["org"] = "SAA";
                            dr["PPrfNo"] = dt.Rows[i]["MasterPO"].ToString();
                            dr["count1"] = 1;
                            dr["create_pc"] = Dns.GetHostName();
                            dr["update_date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            dr["con_no"] = Convert.ToInt32(dt.Rows[i]["Serial"]);
                            dr["country_code"] = null;
                            dr["con_to"] = Convert.ToInt32( dt.Rows[i]["Serial"]);
                            dr["Pkg_Code"] = dt.Rows[i]["Country"].ToString();
                            dr["Scan_ID"] = dt.Rows[i]["id"].ToString();
                            dr["Net_Net"] = 0.0;
                            dr["Con_Net"] = 0.0;
                            dr["con_Gross"] = 0.0;
                            dr["con_l"] = 0.0;
                            dr["con_W"] = 0.0;
                            dr["con_H"] = 0.0;
                            dr["b_Volume"] = 0.0;
                            dr["PO"] = dt.Rows[i]["MasterPO"].ToString();
                            dr["MAIN_LINE"] = null;
                            upDataDt.Rows.Add(dr);
                        }
                    }
                }
                //删除相同的部分
                DataTable diffDt = new DataTable();
                diffDt = dt.Copy();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    for (int j = 0; j < diffDt.Rows.Count; j++)
                    {
                        if (result.Rows[i]["id"].ToString() == "TNF-" + diffDt.Rows[j]["id"].ToString())
                        {
                            diffDt.Rows.RemoveAt(j);
                            j--;
                        }
                    }
                }

                if (diffDt.Rows.Count > 0)
                {
                    for (int i = 0; i < diffDt.Rows.Count; i++)
                    {
                        DataRow dr = addDataDt.NewRow();
                        dr["id"] = "TNF-" + diffDt.Rows[i]["id"].ToString();
                        dr["Cust_id"] = "TNF";
                        dr["Serial_From"] = diffDt.Rows[i]["CartonNo"].ToString();
                        dr["qty"] = diffDt.Rows[i]["PackQty"].ToString();
                        dr["org"] = "SAA";
                        dr["PPrfNo"] = diffDt.Rows[i]["MasterPO"].ToString();
                        dr["count1"] = 1;
                        dr["create_pc"] = Dns.GetHostName();
                        dr["update_date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dr["con_no"] = Convert.ToInt32(dt.Rows[i]["Serial"]);
                        dr["country_code"] = null;
                        dr["con_to"] = Convert.ToInt32( dt.Rows[i]["Serial"]);
                        dr["Pkg_Code"] = diffDt.Rows[i]["Country"].ToString();
                        dr["Scan_ID"] = diffDt.Rows[i]["id"].ToString();
                        dr["Net_Net"] = 0.0;
                        dr["Con_Net"] = 0.0;
                        dr["con_Gross"] = 0.0;
                        dr["con_l"] = 0.0;
                        dr["con_W"] = 0.0;
                        dr["con_H"] = 0.0;
                        dr["b_Volume"] = 0.0;
                        dr["PO"] = diffDt.Rows[i]["MasterPO"].ToString();
                        dr["MAIN_LINE"] = null;
                        addDataDt.Rows.Add(dr);
                    }
                } 
            }
            int ups = 0;
            int insets = 0;
            if (upDataDt.Rows.Count > 0)
            {
                ups = TNFImport.UpdataTnfDataToFsgConppr(upDataDt);
            }
            if (addDataDt.Rows.Count > 0)
            {
                insets = TNFImport.insetTnfDataToFsgConppr(addDataDt);
            }
            return ups + insets;
        }
        public int insetOrUpdataConDetail(DataTable dt)
        {
            string ids = "";
            foreach (DataRow row in dt.Rows)
            {
                ids = ids + "'TNF-" + row["id"].ToString() + "',";
            }
            if (ids == "")
            {
                return 0;
            }

            // SELECT  id,Cust_id,Serial_From,Buyer_Item,Item_desc,color_code,Size1,con_Qty,qty,pprfno from con_detail  WHERE PPrfNo ='79795851196' 
            ids = ids.Substring(0, ids.Length - 1);
            DataTable result = TNFImport.getTnfDataFromFsgByConDetailIds(ids);
            DataTable upDataDt = new DataTable();
            upDataDt.Columns.Add("id", typeof(string));
            upDataDt.Columns.Add("Cust_id", typeof(string));
            upDataDt.Columns.Add("Serial_From", typeof(string));
            upDataDt.Columns.Add("Buyer_Item", typeof(string));
            upDataDt.Columns.Add("Item_desc", typeof(string));
            upDataDt.Columns.Add("color_code", typeof(string));
            upDataDt.Columns.Add("Size1", typeof(string));
            upDataDt.Columns.Add("con_Qty", typeof(string));
            upDataDt.Columns.Add("qty", typeof(int));
            upDataDt.Columns.Add("pprfno", typeof(string)); 


            DataTable addDataDt = new DataTable();
            addDataDt.Columns.Add("id", typeof(string));
            addDataDt.Columns.Add("Cust_id", typeof(string));
            addDataDt.Columns.Add("Serial_From", typeof(string));
            addDataDt.Columns.Add("Buyer_Item", typeof(string));
            addDataDt.Columns.Add("Item_desc", typeof(string));
            addDataDt.Columns.Add("color_code", typeof(string));
            addDataDt.Columns.Add("Size1", typeof(string));
            addDataDt.Columns.Add("con_Qty", typeof(string));
            addDataDt.Columns.Add("qty", typeof(int));
            addDataDt.Columns.Add("pprfno", typeof(string));
            if (result.Rows.Count <= 0)
            {
                // 小于0 全部要新增
                // 大于0 部分新增   部分更新
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = addDataDt.NewRow();
                    dr["id"] = "TNF-" + dt.Rows[i]["id"].ToString();
                    dr["Cust_id"] = "TNF";
                    dr["Serial_From"] = dt.Rows[i]["CartonNo"].ToString();
                    dr["Buyer_Item"] = dt.Rows[i]["Style"].ToString();
                    dr["Item_desc"] = dt.Rows[i]["StyleDescription"].ToString(); 
                    dr["color_code"] = dt.Rows[i]["Color"].ToString();
                    dr["Size1"] = dt.Rows[i]["Size"].ToString();
                    dr["con_Qty"] = dt.Rows[i]["PackQty"].ToString();
                    dr["qty"] = dt.Rows[i]["PackQty"].ToString();
                    dr["pprfno"] = dt.Rows[i]["MasterPO"].ToString();
                    addDataDt.Rows.Add(dr);
                }
            }
            else
            {
                // 大于0 部分新增   部分更新
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < result.Rows.Count; j++)
                    {
                        // 相同ID部分 更新
                        if (result.Rows[j]["id"].ToString() == "TNF-" + dt.Rows[i]["id"].ToString())
                        {
                            DataRow dr = upDataDt.NewRow();
                            dr["id"] = "TNF-" + dt.Rows[i]["id"].ToString();
                            dr["Cust_id"] = "TNF";
                            dr["Serial_From"] = dt.Rows[i]["CartonNo"].ToString();
                            dr["Buyer_Item"] = dt.Rows[i]["Style"].ToString();
                            dr["Item_desc"] = dt.Rows[i]["StyleDescription"].ToString();
                            dr["color_code"] = dt.Rows[i]["Color"].ToString();
                            dr["Size1"] = dt.Rows[i]["Size"].ToString();
                            dr["con_Qty"] = dt.Rows[i]["PackQty"].ToString();
                            dr["qty"] = dt.Rows[i]["PackQty"].ToString();
                            dr["pprfno"] = dt.Rows[i]["MasterPO"].ToString();
                            upDataDt.Rows.Add(dr); 
                        }
                    }
                }
                //删除相同的部分
                DataTable diffDt = dt.Copy();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    for (int j = 0; j < diffDt.Rows.Count; j++)
                    {
                        if (result.Rows[i]["id"].ToString() == "TNF-" + diffDt.Rows[j]["id"].ToString())
                        {
                            diffDt.Rows.RemoveAt(j);
                            j--;
                        }
                    }
                }

                if (diffDt.Rows.Count > 0)
                {
                    for (int i = 0; i < diffDt.Rows.Count; i++)
                    {
                        DataRow dr = addDataDt.NewRow();                     
                        dr["id"] = "TNF-" + dt.Rows[i]["id"].ToString();
                        dr["Cust_id"] = "TNF";
                        dr["Serial_From"] = dt.Rows[i]["CartonNo"].ToString();
                        dr["Buyer_Item"] = dt.Rows[i]["Style"].ToString();
                        dr["Item_desc"] = dt.Rows[i]["StyleDescription"].ToString();
                        dr["color_code"] = dt.Rows[i]["Color"].ToString();
                        dr["Size1"] = dt.Rows[i]["Size"].ToString();
                        dr["con_Qty"] = dt.Rows[i]["PackQty"].ToString();
                        dr["qty"] = dt.Rows[i]["PackQty"].ToString();
                        dr["pprfno"] = dt.Rows[i]["MasterPO"].ToString(); 
                        addDataDt.Rows.Add(dr);
                    }
                }
            }
            int ups = 0;
            int insets = 0;
            if (upDataDt.Rows.Count > 0)
            {
                ups = TNFImport.UpdataTnfDataToFsgConDetail(upDataDt);
            }
            if (addDataDt.Rows.Count > 0)
            {
                insets = TNFImport.insetTnfDataToFsgConDetail(addDataDt);
            }
            return ups + insets;
        }

    }
}
