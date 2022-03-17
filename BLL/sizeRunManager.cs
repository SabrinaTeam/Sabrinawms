using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class sizeRunManager
    {
        sizeRunService sizeS = new sizeRunService();
        public DataTable getSizeRunByMy_no(string my_no,string linkServer)
        {
            return sizeS.getSizeRunByMy_no(my_no, linkServer);
          
        }
        public DataTable getClr_noByMy_no(string[] parameters,string linkServer)
        {
            return sizeS.getClr_noByMy_no(parameters, linkServer);

        }
        public DataTable getSizeByMy_no(string my_nos,string linkServer)
        {
            return sizeS.getSizeByMy_no(my_nos, linkServer);

        }

        public DataTable getAllSizeRunByMy_no(string my_no ,string linkServer)
        {
            return sizeS.getAllSizeRunByMy_no(my_no, linkServer);

        }
    }
}
