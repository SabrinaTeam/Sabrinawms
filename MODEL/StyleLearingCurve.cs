using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class StyleLearingCurve
    {
        public int id { get; set; } 
        public string modulusName { get; set; }
        public  bool isNewStyle { get; set; }
        public bool isDel { get; set; }
        public string remark { get; set; }
    }

    public class CalculatProductivityParameters
    {
        public bool styleNew { get; set; }
        public bool styleOld { get; set; }
        public bool Hour8 { get; set; }
        public bool Hour10 { get; set; }
        public int counts { get; set; }

        
    }
}
