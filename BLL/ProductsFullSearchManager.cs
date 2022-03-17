using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductsFullSearchManager
    {
        ProductsFullSearchService pfss = new ProductsFullSearchService();
        public List<DataTable> getInventoryByStylePO(DataTable searchdt)
        {
            List<DataTable> ldt = new List<DataTable>();
            if (searchdt.Rows.Count <= 0)
            {
                return null;
            }
            List<string> styles = new List<string>();
            List<string> POS = new List<string>();

            foreach (DataRow dr in searchdt.Rows)
            {
                string style = dr["style"].ToString().Trim().ToUpper();
                string po = dr["PO"].ToString().Trim().ToUpper();
                if (!this.isRepeatLists(styles, style))
                {
                    styles.Add(style);
                }
              
                if (!this.isRepeatLists(POS, po))
                {
                    POS.Add(po);
                }
            }
            List<List<string>> spo = new List<List<string>>();
            //  查找相同的款号的 PO
            for (int i = 0; i < styles.Count; i++)
            {
               DataRow[] dr = searchdt.Select("style='"+ styles[i]+"'");
                List<string> poj = new List<string>();
                if (dr.Length > 0)
                {
                    for (int j = 0; j < dr.Length; j++)
                    {
                        if (!isRepeatLists(poj, dr[j]["PO"].ToString()))
                        {
                            poj.Add(dr[j]["PO"].ToString());
                        }
                    }
                }
                spo.Add(poj);
            }

            DataTable dt =  pfss.getInventoryByStylePO(styles, spo);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }


            List<string> BuyerItemPOS = new List<string>();
           // List<string> con_no = new List<string>();
            // 汇总
            foreach (DataRow dr in dt.Rows)
            {
                string style = dr["Buyer_Item"].ToString().Trim().ToUpper();
                string po = dr["PO"].ToString().Trim().ToUpper();
               

                string BuyerItemPO = style + "_" + po;
                if (!this.isRepeatLists(BuyerItemPOS, BuyerItemPO))
                {
                    BuyerItemPOS.Add(BuyerItemPO);
                }
            }
            DataTable resultdt = new DataTable();
            resultdt.Columns.Add("Buyer_Item");
            resultdt.Columns.Add("PO");
            resultdt.Columns.Add("BoxsStatus");


            if (BuyerItemPOS.Count > 0)
            {
                for (int i = 0; i < BuyerItemPOS.Count; i++)
                {
                    string style = BuyerItemPOS[i].Substring(0, BuyerItemPOS[i].IndexOf("_"));
                    string po = BuyerItemPOS[i].Substring(BuyerItemPOS[i].IndexOf("_") + 1);
                    DataRow[] tempDR = dt.Select("Buyer_Item='" + style + "' and PO = '" + po + "'");
                    string con_no = "";
                    int boxs = tempDR.Length;
                    int qty = 0;

                    if (boxs > 0)
                    {
                        foreach (DataRow dr in tempDR)
                        {
                            qty = qty + Convert.ToInt32( dr["qty"].ToString());
                            if (dr["scanBoxs"].ToString().Length <= 0)
                            {
                                if (con_no == ",OK")
                                {
                                    //con_no.Add("OK");
                                    con_no = "";
                                }
                                // con_no.Add(dr["con_no"].ToString());
                                con_no = con_no + "," + dr["con_no"].ToString();
                            }
                            else
                            {
                                if (con_no.Length <= 0)
                                { 
                                    //con_no.Add("OK");
                                    con_no = ",OK";
                                } 
                            }
                        }
                        //  con_no = con_no.Substring(1);
                        con_no = "箱数:"+ boxs.ToString() + " , 件数:" + qty +" "+ con_no;
                    }
                   

                    DataRow rdr = resultdt.NewRow();
                    rdr["Buyer_Item"] = style;
                    rdr["PO"] = po;
                    rdr["BoxsStatus"] = con_no; 
                    //  rdr["BoxsStatus"] = string.Join(",", con_no); 
                    resultdt.Rows.Add(rdr);
                }
            }


            /*
            styles.Add(style); 
            POS.Add(po)
            */
            DataTable errordt = new DataTable();
            errordt.Columns.Add("Buyer_Item");
            errordt.Columns.Add("PO");
            errordt.Columns.Add("BoxsStatus");

         //   SearchDT.Columns.Remove("style");
           // SearchDT.Columns.Remove("PO");


            bool isSearched = false;
            // searchdt
            for (int i = 0; i < searchdt.Rows.Count; i++)
            {
                for (int j = 0; j < resultdt.Rows.Count; j++)
                {
                    //如果找到相同的
                    if (searchdt.Rows[i]["style"].ToString() == resultdt.Rows[j]["Buyer_Item"].ToString() && searchdt.Rows[i]["PO"].ToString() == resultdt.Rows[j]["PO"].ToString())
                    {
                        string Buyer_Item = resultdt.Rows[j]["Buyer_Item"].ToString();
                        string Buyer_Item2 = searchdt.Rows[i]["style"].ToString();
                        string po = resultdt.Rows[j]["PO"].ToString();
                        string po2 = searchdt.Rows[i]["PO"].ToString();

                        // for (int z = 0; z < POS.Count; z++)
                        // {
                        // if (resultdt.Rows[i]["PO"].ToString() == POS[z].ToString())
                        //  {
                        //    string po = resultdt.Rows[i]["PO"].ToString();
                        //     string po2 = POS[z].ToString();
                        //      isSearched = true;
                        //     break;
                        // }

                        //  }
                        isSearched = true;
                            break;
                    }
                    else
                    {
                        isSearched = false;
                    }
                }
                if (!isSearched)
                {
                    DataRow rdr = errordt.NewRow();
                    rdr["Buyer_Item"] = searchdt.Rows[i]["style"].ToString();
                    rdr["PO"] = searchdt.Rows[i]["PO"].ToString();
                    rdr["BoxsStatus"] = "ERROR:没有找到PPR,PPR可能没有上传";
                    errordt.Rows.Add(rdr);
                    isSearched = false;
                }                
            }
            foreach (DataRow dr in errordt.Rows)
            {
                resultdt.ImportRow(dr);
            }

            ldt.Add(dt);
            ldt.Add(resultdt);
            return ldt;
        }
        private bool isRepeatLists(List<string> list, string value)
        {
            bool result = true;
            if (list.Count <= 0)
            {
                result = false;
            }else{
                for (int i = 0; i < list.Count; i++)
                {
                    if(list[i] == value)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
