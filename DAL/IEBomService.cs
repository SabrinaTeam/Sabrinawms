using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IEBomService
    {

        public List<string> getIEVersions(string styleNumber)
        {
            List<string> versions = new List<string>();
            string sql = @"SELECT IEBomVersion  FROM dbo.IEBomBase	 WHERE IEBomStyleName=@styleNumber";

            SqlParameter[] paras =   {
                    new SqlParameter("@styleNumber", styleNumber)

                 };

            DataTable dt = IEBOM_SqlHelper.ExcuteTable(sql, paras);
            if(dt != null && dt.Rows.Count >0)
            {

            }
            List<string> IEVersions

              return

        }
    }
}
