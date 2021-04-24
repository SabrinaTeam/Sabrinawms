using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PoNumberManager
    {
        public PoNumberService pn = new PoNumberService();
        public DataTable  getPoNumbersByODdate(string startDate,string stopDate)
        {
            return pn.getPoNumbersByODdate(startDate, stopDate);
        }
    }
}
