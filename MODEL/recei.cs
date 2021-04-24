using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class recei
    {
    }
    public class receiSearch
    {
        public string org { set; get; }
        public string subinv { set; get; }
        public List<string > location { set; get; }
      
        public string style { set; get; }
        public string color { set; get; }
        public bool receiDate { set; get; }
        public string starTime { set; get; }
        public string stopTime { set; get; }
        public string poNumber { set; get; }
        public string ReceiNumber { set; get; }

        
    }
}
