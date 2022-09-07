using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FrmSingleWeightMaintainManager
    {
        FrmsingleWeightMaintainService fws = new FrmsingleWeightMaintainService();
        public DataTable  getAllCustidByBaseDB(string linkServer)
        {
            DataTable db = fws.getAllCustidByBaseDB();

            return db;

        }
        public DataTable getAllStyleIDByBaseDB(string linkServer)
        {
            DataTable db = fws.getAllStyleIDByBaseDB();

            return db;

        }

        public DataTable getAllSizeIDByBaseDB(string linkServer)
        {
            DataTable db = fws.getAllSizeIDByBaseDB();

            return db;

        }


    }
}
