using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class tradingComanyPO
    {
        public int id { set; get; }
        public string PO { set; get; }
        public string GTN_PO { set; get; }
        public string create_pc { set; get; }
        public string update_date { set; get; }

        public string fCreate_Date { set; get; }
        public string fIssue_Date { set; get; }
        public string fOrder_Status { set; get; }
        public string fOrder_Total_Qty { set; get; }
        public string fInvoiced_Item_Qty { set; get; }
    }
}
