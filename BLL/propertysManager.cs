using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL
{
    public class propertysManager
    {
        public propertysService pts = new propertysService();
        //查询ERP财编资料
        public DataTable getPropertysByPnumber(string org, string propertyIDs)
        {
            return pts.getPropertysByPnumber(org,propertyIDs);
        }
        //查询本地库财编资料
        public DataTable getPropertysByPnumberFromLocalHost(string propertyIDs,bool IsDel)
        {
            return pts.getPropertysByPnumberFromLocalHost(  propertyIDs, IsDel);
        }

        public int delPropertysByPnumber( List<string> propertyIDs,string delnote)
        {
            return pts.delPropertysByPnumber(propertyIDs, delnote);
        }
        public int insertPropertys(DataTable dt)
        {
            return pts.insertPropertys(dt);
        }

        //查询要打印的本地库财编资料
        public DataTable getPropertysByPnumberFromLocalHost(string propertyIDs)
        {
            return pts.getPropertysByPnumberFromLocalHost(propertyIDs);
        }

        public int upPrintPropertysByPnumber(string[] pId)
        {
            return pts.upPrintPropertysByPnumber(pId);
        }
    }
}
