using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PoNumberService
    {
        public DataTable getPoNumbersByODdate(string startDate, string stopDate)
        {
            string sqlstr = @"
                                SELECT h.my_no,b.po_no,SUM(b.qty) qty,b.mark ,h.cust_id ,c.cust_abbr,c.cust_name FROM 	odb b
                                LEFT JOIN dbo.odh h ON b.od_no = h.od_no 
                                LEFT JOIN dbo.cust_dom	c ON c.cust_id = h.cust_id 
                                LEFT JOIN dbo.types t ON t.type_id=h.type_id
                                WHERE h.od_date BETWEEN '"+ startDate + "' AND '"+ stopDate + @"'
                                AND t.type_tt LIKE '002%'
                                GROUP BY 	 b.po_no,b.mark  ,h.my_no  ,h.cust_id 	  ,c.cust_abbr,c.cust_name 
                                ORDER BY h.my_no";

            DataTable result = BEST_SqlHelper.ExcuteTable(sqlstr);
            return result; 
        }
    }
}
