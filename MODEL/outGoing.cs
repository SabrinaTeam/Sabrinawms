using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class outGoing_pos
    {
        /*
         b1.po_no = '4507208884'
     --  AND h1.od_no = 'SFC2008142'
         AND b1.style_id = 'CZ4315'
         AND b1.clr_no = '071';
        									n.OGACDate,
									n.Plant,
         */
        public string po_ids { set; get; }
        public string style_ids { set; get; }
        public string clr_nos { set; get; }
        public string OGACDate { set; get; }
        public string Plant { set; get; }
        public string pscs { set; get; }
    }
}
