using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CompeleteManager
    {
        CompeleteService cs = new CompeleteService();
        public List<CompeleteMes> getMesScanData(string style, string myNumber, bool ckdate, string stardate, string stopdate)
        {
            List<CompeleteMes> compeleteMess = new List<CompeleteMes>();
            List<CompeleteERP> compeleteERPs = new List<CompeleteERP>();
            List<string> myNumbers = new List<string>();
            if (ckdate)
            {
                compeleteMess = cs.getMesScanData(style, myNumber, stardate, stopdate);
            }
            else
            {
                compeleteMess = cs.getMesScanData(style, myNumber);
            }

            if (compeleteMess.Count <= 0)
            {
                return compeleteMess;
            }

            //自编单号集合
            foreach (CompeleteMes mes in compeleteMess)
            {
                if (!checkMyNumbers(myNumbers, mes.my_no))
                {
                    myNumbers.Add(mes.my_no);
                }
            }
            // 查询ERP相等自编单号的数据
            compeleteERPs = cs.getERPData(myNumbers);

            // MES 与 ERP  日期相等的合并成一行，不相等的另一行
            if (compeleteMess.Count > 0 && compeleteERPs.Count > 0)
            {

            }

            return compeleteMess;
        }

        public List<CompeleteERP> getCompeleteERP(List<CompeleteMes> compeleteMes)
        {

            List<CompeleteERP> compeleteERPs = new List<CompeleteERP>();
            List<string> myNumbers = new List<string>();
            //自编单号集合
            foreach (CompeleteMes mes in compeleteMes)
            {
                if (!checkMyNumbers(myNumbers, mes.my_no))
                {
                    myNumbers.Add(mes.my_no);
                }
            }
            // 查询ERP相等自编单号的数据
            compeleteERPs = cs.getERPData(myNumbers);
            return compeleteERPs;
        }


        public bool checkMyNumbers(List<string> list, string value)
        {
            bool result = true;
            if (list.Count <= 0)
            {
                result = false;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == value)
                {
                    result = true;
                    break;
                }
                else
                {
                    result = false;
                }

            }
            return result;
        }



        public List<meshMesERPCompelete> meshMesERPCompelete(List<CompeleteMes> Mes, List<CompeleteERP> ERP)
        {

            List<CompeleteMes> compeleteMes = new List<CompeleteMes>(Mes.ToArray());
            List<CompeleteERP> compeleteERP =  new List<CompeleteERP>(ERP.ToArray());
          //  compeleteMes = Mes.;
          //  compeleteERP = ERP;
         List <meshMesERPCompelete> meshMesERPS = new List<meshMesERPCompelete>();



            // 日期与自编单号一样的 合并到一起  
            // 差集的部分  添加进去
            // 按照 日期，自编单号排序

            // 1、循环  compeleteMes 、 compeleteERP
            //  日期与自编单号相同部分  给 ALL

            // 2、循环  compeleteMes   ALL
            // compeleteMes 不同部分  给ALL 

            // 3、循环  compeleteERP   ALL
            // compeleteERP 不同部分  给ALL 

            // 返回ALL 为合并后的集合

            for (int i = 0; i < compeleteMes.Count; i++)
            {
                for (int j = 0; j < compeleteERP.Count; j++)
                {
                    if (compeleteMes[i].my_no == compeleteERP[j].myNumber && compeleteMes[i].sysAddTime == compeleteERP[j].FinishDate)
                    {
                        meshMesERPCompelete meshMesERP = new meshMesERPCompelete();
                        meshMesERP.org = compeleteERP[j].org;
                        meshMesERP.myNumber = compeleteERP[j].myNumber;
                        meshMesERP.orderID = compeleteERP[j].orderID;
                        meshMesERP.moveOrderID = compeleteERP[j].moveOrderID;
                        meshMesERP.finishID = compeleteERP[j].finishID;
                        meshMesERP.custID = compeleteERP[j].custID;
                        meshMesERP.custName = compeleteERP[j].custName;

                        meshMesERP.Style = compeleteERP[j].Style;
                        meshMesERP.taskProcessName = compeleteERP[j].taskProcessName;
                        meshMesERP.OrderQty = compeleteERP[j].OrderQty;
                        meshMesERP.makeQty = compeleteERP[j].makeQty;
                        meshMesERP.FinishDate = compeleteERP[j].FinishDate;
                        meshMesERP.emploreeID = compeleteERP[j].emploreeID;
                        meshMesERP.caption = compeleteERP[j].caption;

                        meshMesERP.ProcessID = compeleteERP[j].ProcessID;
                        meshMesERP.processName = compeleteERP[j].processName;
                        meshMesERP.lineID = compeleteERP[j].lineID;
                        meshMesERP.WorkID = compeleteERP[j].WorkID;
                        meshMesERP.workMachineID = compeleteERP[j].workMachineID;
                        meshMesERP.finishQty = compeleteERP[j].finishQty;
                        meshMesERP.BonusQty = compeleteERP[j].BonusQty;
                        meshMesERP.checkedID = compeleteERP[j].checkedID;

                        meshMesERP.orderSKU = compeleteMes[i].orderSKU;
                        meshMesERP.my_no = compeleteMes[i].my_no;
                        meshMesERP.productModel = compeleteMes[i].productModel;
                        meshMesERP.partName = compeleteMes[i].partName;
                        meshMesERP.QTY = compeleteMes[i].QTY;
                        meshMesERP.sysAddTime = compeleteMes[i].sysAddTime;

                        meshMesERPS.Add(meshMesERP);
                        compeleteERP.RemoveAt(j);
                        j--;
                                          
                    }
                }
            }


            for (int i = 0; i < meshMesERPS.Count; i++)
            { 
                for(int j =0;j< compeleteMes.Count; j++)
                {
                    if(meshMesERPS[i].my_no == compeleteMes[j].my_no && meshMesERPS[i].sysAddTime == compeleteMes[j].sysAddTime)
                    {
                        compeleteMes.RemoveAt(j);
                        j--;
                    }
                }
            
            }


                for (int i = 0; i < compeleteMes.Count; i++)
            {
                meshMesERPCompelete meshMesERP = new meshMesERPCompelete();
                meshMesERP.orderSKU = compeleteMes[i].orderSKU;
                meshMesERP.my_no = compeleteMes[i].my_no;
                meshMesERP.productModel = compeleteMes[i].productModel;
                meshMesERP.partName = compeleteMes[i].partName;
                meshMesERP.QTY = compeleteMes[i].QTY;
                meshMesERP.sysAddTime = compeleteMes[i].sysAddTime;

                meshMesERPS.Add(meshMesERP);
            }
            for (int i = 0; i < compeleteERP.Count; i++)
            {
                meshMesERPCompelete meshMesERP = new meshMesERPCompelete();
                meshMesERP.org = compeleteERP[i].org;
                meshMesERP.myNumber = compeleteERP[i].myNumber;
                meshMesERP.orderID = compeleteERP[i].orderID;
                meshMesERP.moveOrderID = compeleteERP[i].moveOrderID;
                meshMesERP.finishID = compeleteERP[i].finishID;
                meshMesERP.custID = compeleteERP[i].custID;
                meshMesERP.custName = compeleteERP[i].custName;

                meshMesERP.Style = compeleteERP[i].Style;
                meshMesERP.taskProcessName = compeleteERP[i].taskProcessName;
                meshMesERP.OrderQty = compeleteERP[i].OrderQty;
                meshMesERP.makeQty = compeleteERP[i].makeQty;
                meshMesERP.FinishDate = compeleteERP[i].FinishDate;
                meshMesERP.emploreeID = compeleteERP[i].emploreeID;
                meshMesERP.caption = compeleteERP[i].caption;

                meshMesERP.ProcessID = compeleteERP[i].ProcessID;
                meshMesERP.processName = compeleteERP[i].processName;
                meshMesERP.lineID = compeleteERP[i].lineID;
                meshMesERP.WorkID = compeleteERP[i].WorkID;
                meshMesERP.workMachineID = compeleteERP[i].workMachineID;
                meshMesERP.finishQty = compeleteERP[i].finishQty;
                meshMesERP.BonusQty = compeleteERP[i].BonusQty;
                meshMesERP.checkedID = compeleteERP[i].checkedID;

                meshMesERPS.Add(meshMesERP);
            }


            //自编单号集合
            return meshMesERPS;

        }
    }
}
