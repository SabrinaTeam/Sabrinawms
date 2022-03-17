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
    public class MachinesTableManager
    {
        MachinesTypeServices MTS = new MachinesTypeServices();
        public DataTable getAllMachineTypes()
        {

           return  MTS.getAllMachineTypes();
        }
        public List<string> saveMachineTypes(DataTable machineDT)
        {
            return MTS.saveMachineTypes(machineDT);
        }
        public int delMachineTypeByNames(string machineName)
        {
            return MTS.delMachineTypeByNames(machineName);
        }
    }
}
