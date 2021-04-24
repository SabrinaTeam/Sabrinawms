using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
   public class sizeRunService
    {
        public DataTable getSizeRunByMy_no(string my_no)
        {
            string sqlstr = @"
                                SELECT 
                                        a.type_tt,
                                               a.cust_abbr,
                                              a.od_date,
                                               a.season_id,
                                               a.my_no,
                                               a.org,
                                               a.yymm,
                                               a.type_name,
                                               a.od_no,
	                                           a.style_id,
	                                           a.clr_no,
	                                           size_code,
	                                           sum(a.qty) qty
                                        FROM (
                                        SELECT type.type_tt,
                                               cust_dom.cust_abbr,
                                               h1.od_date,
                                               h1.season_id,
                                               h1.my_no,
                                               CASE h1.be_id
                                                   WHEN 'SAA' THEN
                                                       'SAA'
                                                   ELSE
                                                       'TOP'
                                               END AS org,
                                               buyid.yymm,
                                               type.type_name,
                                               t.od_no,
	                                           t.style_id,
	                                           t.clr_no,
	                                           size_code,
	                                           t.qty  
                                        from  (  select b.od_no,b.style_id,b.clr_no,size_code=h.us01,qty=b.qty01 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us02,qty=b.qty02 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us03,qty=b.qty03 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us04,qty=b.qty04 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us05,qty=b.qty05 from odb b,odh h where  b.od_no=h.od_no 
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us06,qty=b.qty06 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us07,qty=b.qty07 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us08,qty=b.qty08 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us09,qty=b.qty09 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us10,qty=b.qty10 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us11,qty=b.qty11 from odb b,odh h where  b.od_no=h.od_no  
                                                     union all select b.od_no,b.style_id,b.clr_no,size_code=h.us12,qty=b.qty12 from odb b,odh h where  b.od_no=h.od_no  ) t
                                                     ,odh h1  
                                          INNER join cust_dom ON h1.cust_id = cust_dom.cust_id  
                                          Left join tb_sfcbuy buyid ON CONVERT (datetime,h1.od_date) between buyid.begin_day AND buyid.end_day AND buyid.cust_buy_id= case h1.cust_id when 'A0000' then 'A0001' else 'SAB' end  
                                          left join types type on h1.type_id=type.type_id  
                                             where 1=1  
                                                 and qty>0 
                                                 and h1.od_no=t.od_no  
                                                 and h1.my_no = '"+ my_no + @"' 
                                        ) a
                                        WHERE a.my_no  = '" + my_no + @"' 
                                        GROUP BY 
                                        a.type_tt,
                                               a.cust_abbr,
                                              a.od_date,
                                               a.season_id,
                                               a.my_no,
                                               a.org,
                                               a.yymm,
                                               a.type_name,
                                               a.od_no,
	                                           a.style_id,
	                                           a.clr_no,
	                                           a.size_code
                                        ORDER BY a.type_tt,a.cust_abbr,a.season_id,a.my_no;";

            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr);
            return dt;
        }
        public DataTable getClr_noByMy_no(string[] parameters)
        {
            string sqlstr = @"SELECT DISTINCT
                                       b.clr_no	,
	                                   b.style_id,
	                                   h.my_no,
	                                   h.cust_id
                                FROM odh h
                                    LEFT JOIN odb b
                                        ON h.od_no = b.od_no
                                WHERE  1=1 
                                and   h.my_no LIKE +'%'+  ISNULL(@my_no,h.my_no)  +'%'  
                                and   b.style_id LIKE +'%'+  ISNULL(@style_id,b.style_id) +'%'";

            object my_no =  DBValue(parameters[0]);
            object style_id = DBValue(parameters[1]);
            SqlParameter[] paras =   {
                    new SqlParameter("@my_no", my_no),
                    new SqlParameter("@style_id", style_id)
                 };             
           
            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr, paras);
            return dt;
        }

        public  object DBValue(object obj)
        {
            if (obj == null)
            {
                return DBNull.Value;
            }
            else
            {
                return obj;
            }
        }

        public DataTable getSizeByMy_no(string my_nos)
        {
            string sqlstr = @"SELECT my_no,
                                       us01,
                                       us02,
                                       us03,
                                       us04,
                                       us05,
                                       us06,
                                       us07,
                                       us08,
                                       us09,
                                       us10,
                                       us11,
                                       us12
                                FROM odh
                                WHERE 1 = 1
                                      AND my_no IN ( " + my_nos + @" );";

            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr);
            return dt;
        }



        public DataTable getAllSizeRunByMy_no(string my_no)
        {
            string sqlstr = @"
                                select type.type_tt,cust_dom.cust_abbr,h1.od_date,h1.season_id,h1.my_no,         
                                          case h1.be_id when 'SAA' then 'SAA' else 'TOP' end as org ,         
                                          buyid.yymm,type.type_name,t.* 
                                from  (                  select b.od_no,b.style_id,b.clr_no,size_code=h.us01,qty=b.qty01,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us02,qty=b.qty02,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us03,qty=b.qty03,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us04,qty=b.qty04,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us05,qty=b.qty05,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no 
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us06,qty=b.qty06,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us07,qty=b.qty07,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us08,qty=b.qty08,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us09,qty=b.qty09,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us10,qty=b.qty10,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us11,qty=b.qty11,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  
                                             union all select b.od_no,b.style_id,b.clr_no,size_code=h.us12,qty=b.qty12,b.def_date,b.po_no from odb b,odh h where  b.od_no=h.od_no  ) t
                                             ,odh h1  
                                Inner join cust_dom ON h1.cust_id = cust_dom.cust_id  
                                  Left join tb_sfcbuy buyid ON CONVERT (datetime,h1.od_date) between buyid.begin_day AND buyid.end_day AND buyid.cust_buy_id= case h1.cust_id when 'A0000' then 'A0001' else 'SAB' end  
                                  left join types type on h1.type_id=type.type_id  
                                     where 1=1  
                                         and qty>0 
                                         and h1.od_no=t.od_no  
                                         and h1.my_no = '"+ my_no + @"'  
                                 order by type.type_tt,cust_dom.cust_name,h1.season_id,h1.my_no;";

            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr);
            return dt;
        }
    }
}
