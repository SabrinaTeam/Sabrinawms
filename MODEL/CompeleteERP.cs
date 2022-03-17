using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
   public  class CompeleteERP
    {
        public string org { set; get; }
        public string myNumber { set; get; }
        public string orderID { set; get; }
        public string moveOrderID { set; get; }
        public string finishID { set; get; }
        public string custID { set; get; }
        public string custName { set; get; }

        public string Style { set; get; }
        public string taskProcessName { set; get; }
        public int OrderQty { set; get; } // 订单数量
        public int makeQty { set; get; }  // 工单数量
        public string FinishDate { set; get; }
        public string emploreeID { set; get; }
        public string caption { set; get; }

        public string ProcessID { set; get; }
        public string processName { set; get; }
        public string lineID { set; get; }
        public string WorkID { set; get; }
        public string workMachineID { set; get; }
        public int finishQty { set; get; }// 报工数量
        public int BonusQty { set; get; } // 溢出数量

        public string checkedID { set; get; }

        

    }
}
