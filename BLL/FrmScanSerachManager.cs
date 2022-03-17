using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FrmScanSerachManager
    {
        public FrmScanSearchService fsss = new FrmScanSearchService();
        public List<string> getSubinbsByOrg(string org)
        {
            List<string> subinvs = new List<string>();
            DataTable dt = fsss.getSubinbsByOrg(org);
            if (dt.Rows.Count <= 0)
            {
                return subinvs;
            }
            foreach (DataRow r in dt.Rows)
            {
                subinvs.Add(r["subinv"].ToString());
            }
            return subinvs;

        }

        public List<string> getLocationsBysubinv(string org, string subinv)
        {
            List<string> locations = new List<string>();
            DataTable dt = fsss.getLocationsBysubinv(org, subinv);
            if (dt.Rows.Count <= 0)
            {
                return locations;
            }
            foreach (DataRow r in dt.Rows)
            {
                locations.Add(r["location"].ToString());
            }
            return locations;
        }
     
        public List<locationData> getScanByQuery(string org, string subinv, string location,  string startDate, string stopDate,string styleCode, string colorCode)
        {
            List<locationData> locations = new List<locationData>();
            
            DataTable dt = fsss.getScanByQuery( org,  subinv,  location,  startDate,  stopDate, styleCode, colorCode);
            if (dt.Rows.Count <= 0)
            {
                return locations;
            }
            foreach (DataRow r in dt.Rows)
            {
                locationData locationScanData = new locationData();
                locationScanData.TagNumber = r["TagNumber"].ToString();
                locationScanData.Cust_id = r["Cust_id"].ToString();
                locationScanData.Location = r["Location"].ToString();
                locationScanData.update_date = r["update_date"].ToString();
                locationScanData.org = r["ORG"].ToString();
                locationScanData.con_no = r["con_no"].ToString();
                locationScanData.create_pc = r["create_pc"].ToString();                
                locationScanData.subinv = r["subinv"].ToString();
                locationScanData.scantime = r["scantime"].ToString();
                locationScanData.kg = r["kg"].ToString();
                locationScanData.po = r["po"].ToString();
                locationScanData.MAIN_LINE = r["MAIN_LINE"].ToString();
                locationScanData.style = r["Buyer_Item"].ToString();
                locationScanData.color_code = r["color_code"].ToString();
                locationScanData.Size1 = r["Size1"].ToString();
                locationScanData.QTY = r["QTY"].ToString();
                locations.Add(locationScanData);
            }
            return locations;

        }

    }
}
