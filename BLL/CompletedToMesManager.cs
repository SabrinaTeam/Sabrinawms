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
        public DataTable getMesworktagscansByinvoice(string tagInvoice)
        {

            return cms.getMesworktagscansByinvoice(tagInvoice);
        }
    }
}
