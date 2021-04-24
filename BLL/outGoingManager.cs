using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class outGoingManager
    {
        outGoingService ogs = new outGoingService();
        public DataTable  getSubinvs(string org)
        {
            return ogs.getSubinvs(org);
        }
        public DataTable getLocation(string subinv)
        {
            return ogs.getLocation(subinv);
        }
        public DataTable getOutgoing(string org,string subinv,string location,string starTime,string stopTime)
        {
            return ogs.getOutgoing(org, subinv, location, starTime, stopTime);
        }

        public DataTable getOffSet(string org, string subinv, string location, string starTime, string stopTime)
        {
            return ogs.getOffSet(org, subinv, location, starTime, stopTime);
        }

        public DataTable getMoveLocals(string tags)
        {
            return ogs.getMoveLocals(tags);
        }

        /// <summary>
        /// 从BEST取回订单资料
        /// </summary>
        /// <param name="po_no"></param>
        /// <param name="style_id"></param>
        /// <param name="clr_no"></param>
        /// <returns></returns>
        public DataTable getOD_POFromBestByPSC(List<outGoing_pos> pscs)
        {
            return ogs.getOD_POFromBestByPSC(pscs);
           
        }


        public DataTable getMy_NoFromBest(string po_no, string clr_no, string  style_id, string  area_id, string  def_date)
        {
            return ogs.getMy_NoFromBest(po_no,clr_no, style_id, area_id, def_date);
        }
        public DataTable getReceiFromNoBarCode(string org, string subinv, string location, string starTime, string stopTime)
        {
            return ogs.getReceiFromNoBarCode(org, subinv, location, starTime, stopTime);

        }

        public DataTable getYYMMFromBestByPo(string po)
        {
            return ogs.getYYMMFromBestByPo(po);

        }


    }
}
