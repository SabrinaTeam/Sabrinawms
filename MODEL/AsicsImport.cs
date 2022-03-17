using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class AsicsImport_Con_ppr
    {
        public string id { set; get; }
        public string Cust_id { set; get; }
        public string Serial_From { set; get; }
        public int qty { set; get; }
        public string org { set; get; }
        public string PPrfNo { set; get; }
        public int count1 { set; get; }
        public string create_pc { set; get; }
        public string update_date { set; get; }
        public int con_no { set; get; }
        public string country_code { set; get; }
        public int con_to { set; get; }
        public string Pkg_Code { set; get; }
        public string Scan_ID { set; get; }
        public float Net_Net { set; get; }
        public float con_net { set; get; }
        public float con_Gross { set; get; }
        public float con_L { set; get; }
        public float con_W { set; get; }
        public float con_H { set; get; }
        public float b_Volume { set; get; }
        public string PO { set; get; }
        public string MAIN_LINE { set; get; }
    }

    public class AsicsImport_Con_detail
    {

        /*
         id	Cust_id	Serial_From	Buyer_Item	Item_desc	
        color_code	Size1	con_Qty	qty	pprfno
         */
        public string id { set; get; }
        public string Cust_id { set; get; }
        public string Serial_From { set; get; }
        public string Buyer_Item { set; get; }
        public string Item_desc { set; get; }
        public string color_code { set; get; }
        public string Size1 { set; get; }
        public int con_Qty { set; get; }
        public int qty { set; get; }
        public string pprfno { set; get; }

    }
}
