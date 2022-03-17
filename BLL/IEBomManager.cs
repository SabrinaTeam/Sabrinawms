using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IEBomManager
    {
        public IEBomService ies = new IEBomService();
        public List<string> getIEVersions(string styleNumber)
        {
            return ies.getIEVersions(styleNumber);
        }

    }
}
