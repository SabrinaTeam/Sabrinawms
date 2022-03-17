using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductSearchManager
    {
        ProductSearchService pss = new ProductSearchService();
        public List<DataTable> getInvByP(List<string> ps)
        {
            List<DataTable> dts = new List<DataTable>();
            int noQtyBox = 0; // 没有件数的箱子数量
            int boxQty = 0; // 箱数量

            // 按PO汇总
            DataTable countPoDT = new DataTable();
            countPoDT.Columns.Add("Cust_id");
            countPoDT.Columns.Add("Buyer_Item");
            countPoDT.Columns.Add("color_code");
            countPoDT.Columns.Add("po");
            countPoDT.Columns.Add("MAIN_LINE");
            countPoDT.Columns.Add("Qty");
            countPoDT.Columns.Add("boxQtys");

            // 按日期汇总
            DataTable DateCount = new DataTable();
            DateCount.Columns.Add("ScanDate");
            DateCount.Columns.Add("Qty");
            DateCount.Columns.Add("boxQtys");


            DataTable dt = pss.getInvByP(ps);
            if (dt.Rows.Count <= 0)
            {
                dts.Add(dt);
                dts.Add(countPoDT);
                dts.Add(DateCount);
                return dts;
            }

            List<Mpo> pos = new List<Mpo>();
            List<string> scanDates = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Mpo mpo = new Mpo();
                mpo.po = dt.Rows[i]["PO"].ToString().Trim();                 
                mpo.main_line = dt.Rows[i]["MAIN_LINE"].ToString().Trim();  
                string scandate= dt.Rows[i]["scantime"].ToString().Trim();
                // string pom = mpo.po + mpo.main_line;
                if (!this.isRepeatLists(pos, mpo.po ,mpo.main_line))
                {
                    pos.Add(mpo);
                }
                if (!this.isRepeatLists(scanDates, scandate))
                {
                    scanDates.Add(scandate);
                }
            }

            if (pos.Count <= 0)
            {
                dts.Add(dt);
                dts.Add(countPoDT);
                dts.Add(DateCount);
                return dts;
            }

            for (int i = 0; i < pos.Count; i++)
            {
                int sizeQty = 0;
                DataRow[] selectPO = dt.Select("po='" + pos[i].po + "' and MAIN_LINE = '" + pos[i].main_line + "'");
                if (selectPO.Length <= 0)
                {
                    break;
                }
                string  qtystr = "";
                foreach (DataRow sdr in selectPO)
                {
                    qtystr = sdr["boxSizeQty"].ToString();
                    if (qtystr == "")
                    {
                        qtystr = "0";
                    }
                    boxQty++;
                    sizeQty = sizeQty + Convert.ToInt32(qtystr);
                  
                }
                DataRow dr = countPoDT.NewRow();
                dr["Cust_id"] = selectPO[0]["Cust_id"].ToString();
                dr["Buyer_Item"] = selectPO[0]["Buyer_Item"].ToString();
                dr["color_code"] = selectPO[0]["color_code"].ToString();
                dr["po"] = selectPO[0]["po"].ToString();
                dr["MAIN_LINE"] = selectPO[0]["MAIN_LINE"].ToString();
                dr["Qty"] = sizeQty.ToString();
                dr["boxQtys"] = boxQty.ToString();
                countPoDT.Rows.Add(dr);
                boxQty = 0;
            }

            for (int i = 0; i < scanDates.Count; i++)
            {
                int sizeQty = 0;
                DataRow[] selectPO = dt.Select("scantime = '" + scanDates[i].ToString()+"'");
                if (selectPO.Length <= 0)
                {
                    break;
                }
                string qtystr = "";
                foreach (DataRow sdr in selectPO)
                {
                    qtystr = sdr["boxSizeQty"].ToString();
                    if (qtystr == "")
                    {
                        qtystr = "0";
                        noQtyBox++;
                    }
                    boxQty++;
                    sizeQty = sizeQty + Convert.ToInt32(qtystr); 
                }
                DataRow dr = DateCount.NewRow();
                dr["ScanDate"] = selectPO[0]["scantime"].ToString();              
                dr["Qty"] = sizeQty.ToString();
                dr["boxQtys"] = boxQty.ToString();                 
                DateCount.Rows.Add(dr);
                if(noQtyBox > 0)
                {
                    DataRow ndr = DateCount.NewRow();
                    ndr["ScanDate"] = selectPO[0]["scantime"].ToString();
                    ndr["Qty"] = "0";
                    ndr["boxQtys"] = noQtyBox.ToString() +",箱没有PPR资讯";
                    DateCount.Rows.Add(ndr);
                    noQtyBox = 0;
                }
            }

            DataTable dtCopy = dt.Copy();
            DataView dv = dt.DefaultView;
            dv.Sort = "scantime";
            dtCopy = dv.ToTable();
            dts.Add(dtCopy);

            DataTable countPoDTCopy = countPoDT.Copy();
            DataView dc = countPoDT.DefaultView;
            dc.Sort = "po";
            countPoDTCopy = dc.ToTable();
            dts.Add(countPoDTCopy);

            DataTable DateCountCopy = DateCount.Copy();
            DataView da = DateCount.DefaultView;
            da.Sort = "ScanDate";
            DateCountCopy = da.ToTable();
            dts.Add(DateCountCopy);
            return dts;  //noQtyBox 返回回去看是不是有没有数量的

        }

        private bool isRepeatLists(List<string> list, string value )
        {
            bool result = true;
            if (list.Count <= 0)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == value)
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

        private bool isRepeatLists(List<Mpo> list, string po , string main_line)
        {
            bool result = true;
            if (list.Count <= 0)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].po == po && list[i].main_line == main_line)
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


        public List<string> getOrg()
        {
            return pss.getOrg();

        }
        public List<string> getSubinv( string org)
        {
            return pss.getSubinv(org);

        }

        public DataTable getStyles()
        {
            return pss.getStyles();

        }

        public List<string> getColors( string style)
        {
            return pss.getColors(style);

        }
       
    }
}

public class Mpo { 
    public string po { set; get; }
    public string main_line { set; get; }
}