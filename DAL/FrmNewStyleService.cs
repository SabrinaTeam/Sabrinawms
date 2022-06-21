using MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public  class FrmNewStyleService
    {
		public string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
		public DataTable getNewStyleByMynoDate(DataTable SourceDT ,string yymm)
		{
			string style_id = "";
			for (int i = 0; i < SourceDT.Rows.Count; i++)
			{
				style_id = style_id + @" '"+ SourceDT.Rows[i]["style_id"].ToString() + @"' ,";
			}
			style_id = style_id.Substring(0,style_id.Length-1);

			string sql  = @"select
								h.be_id,h.cust_id, h.season_id,
								buyid.yymm, buyid.buy_cname,
								b.style_id,b.clr_no,
								sum(ceiling(b.qty)) qty,h.my_no,
								h.type_id,
								t.type_name, 
								CONVERT(varchar(10),h.od_date,120)  as od_date
						from odh h
						left join odb b on h.od_no = b.od_no
						left join   types t on t.type_id = h.type_id
						Left join tb_sfcbuy buyid ON CONVERT (datetime,h.od_date)
							 between buyid.begin_day AND buyid.end_day
							 AND buyid.cust_buy_id= case
								 h.cust_id when 'A0000'
									 then 'A0001'
								 else 'SAB'
								 end
						where  1 = 1
						and b.style_id in (" + style_id + @")
						   and  buyid.yymm ='"+ yymm + @"'
						AND T.TYPE_TT = '002' and b.qty >0
						group by     h.od_date,h.type_id,h.cust_id,h.season_id,h.my_no,
								b.style_id,b.clr_no,   h.be_id,
								 t.type_name,
								 buyid.yymm, buyid.buy_cname
						order by    b.style_id , h.od_date  ,h.my_no  , b.clr_no";
			DataTable dt = new DataTable();
			if (MiddleWare == "1")
			{
				dt = BEST_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				dt = BEST_SqlHelper.ExcuteTable(sql);
			}
			return dt;

		}

		public DataTable getStyleIsNewOrOld(List<styleOddate> styleOddates)
		{
			string where = "";

			for (int i = 0; i < styleOddates.Count; i++)
			{
				where = where + @"( b.style_id = '"+ styleOddates[i].style_id +@"' and h.od_date < '" + styleOddates[i].od_date + @"') or " ;
			}
			where = where.Substring(0, where.Length-4);
			string sql = @"select CONVERT(varchar(10),max(h.od_date),120)  as od_date ,
								   h.be_id,h.cust_id, h.season_id,
								   b.style_id,b.clr_no,
								   h.my_no,
									h.type_id
									from odb b
							left join  odh h on h.od_no = b.od_no
							left join   types t on t.type_id = h.type_id
							where  (" + where + @")
							AND T.TYPE_TT = '002' and b.qty >0
							group by   h.be_id,h.cust_id, h.season_id,
								   b.style_id,b.clr_no,
								   h.my_no,
									h.type_id,
									h.od_date ,b.clr_no;";
			DataTable dt = new DataTable();
			if (MiddleWare == "1")
			{
				dt = BEST_SqlHelper.ExcuteTable(sql);
			}
			else
			{
				dt = BEST_SqlHelper.ExcuteTable(sql);
			}
			return dt;

		}
	}
}
