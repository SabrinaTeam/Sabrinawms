using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class CFOutput
    {
        public int searchType { set; get; }
        public string org { set; get; }
        public string subinv { set; get; }
        public string style { set; get; }
        public bool checkDate { set; get; }
        public string starDate { set; get; }
        public string stopDate { set; get; }
    }
}
