using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public  class ProductionStatusSearch2
    {
        public string mynumber { get; set; }
        public string buyid { get; set; }
        public string season { get; set; }
        public int datetype { get; set; }
        public string stardate { get; set; }
        public string enddate { get; set; }
        public bool checkedDate { get; set; }
        public int page { get; set; }
    }

    public class betweenDate2
    {
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }
}

