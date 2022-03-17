using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CompletedToMesManager
    {
        CompletedToMesService cms = new CompletedToMesService();
        public DataTable getTagInvoiceById(string Mid)
        {
            
            return cms.getTagInvoiceById(Mid);
        }
        public DataTable getMesworktagscansByinvoice(string tagInvoice,string location)
        {
            return cms.getMesworktagscansByinvoice(tagInvoice, location);
        }

        public List<string> gettagLocations(string taginvoice, bool isAuto)
        {
            List<string> tagLocations = new List<string>();
            DataTable dt = cms.gettagLocations(taginvoice,  isAuto);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tagLocations.Add(dr["tagLocation"].ToString());
                }
            }
            return tagLocations;
        }

        public void updataIsPrints(string tagInvoice, string location)
        {
            cms.updataIsPrints(tagInvoice, location);
        }
    }
}
