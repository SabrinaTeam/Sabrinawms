using DAL;
using MODEL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BLL
{
    public class accessoryOutManager
    {
        private accessoryOutService aos = new accessoryOutService();
        
        /// <summary>
        ///  从ERP数据库获取数据资料
        /// </summary>
        /// <param name="Parameters"> where 条件 </param>
        /// <param name="Org"> 厂区 </param>
        /// <returns></returns>
        public List<accessoryOut> getAccessoryOutByParameters(List<parameter> Parameters,string Org)
        {
            return aos.getAccessoryOutByParameters(Parameters,Org);
        }

        /// <summary>
        ///  写入本地数据库 accessoryout
        /// </summary>
        /// <param name="accessorytb"> 需要写入库的数据资料 </param>
        /// <returns></returns>
        public int writeAccessoryToDB(List<materials> accessorytb)
        {
            return  aos.writeAccessoryToDB(accessorytb);
        }
        public int updataAccessoryToDB(List<materials> accessorytb)
        {
            return aos.updataAccessoryToDB(accessorytb);
        }

        public List<accessoryOut> getAccessoryOutByLocalHostDB(string od_no)
        {
            return aos.getAccessoryOutByLocalHostDB(od_no);
        }

        public DataTable getAccessoryhByreceiveNumber(string reno, string renoBatch)
        {
            return aos.getAccessoryhByreceiveNumber(reno, renoBatch);
        }

        public int delAccessoryOutFromLocalHostDBByMyNumber(string mynumber)
        {
            return aos.delAccessoryOutFromLocalHostDBByMyNumber(mynumber);
        }

        public int updataAccessoryOutFromLocalHostDBByMyNumber(string mynumber)
        {
            return aos.updataAccessoryOutFromLocalHostDBByMyNumber(mynumber);
        }


        public List<accessoryOut> getAccessoryOutByLocalHostDB(List<parameter>  mynumber,string Org)
        {
             
            return aos.getAccessoryOutByLocalHostDB(mynumber,Org);
        }


        public List<groupCloNames> getColorFromBestDBByMyNo(string mynumber,string serviceName)
        {

            return aos.getColorFromBestDBByMyNo(mynumber, serviceName);
        }


        public List<groupPONumber> getPOFromBestDBByOd_no(string orderNumber,string serviceName)
        {

            return aos.getPOFromBestDBByOd_no(orderNumber, serviceName);
        }

        public List<groupSizeNmae> getSizeFromBestDBByOd_no(string orderNumber,string serviceName)
        {

            return aos.getSizeFromBestDBByOd_no(orderNumber, serviceName);
        }

        public List<materialSize> getMaterialSizeFromBestDBByOd_no(string orderNumber)
        {

            return aos.getMaterialSizeFromBestDBByOd_no(orderNumber);
        }


        public List<materials> getMaterialsFromBestDBByOd_no(string myNumber)
        {

            return aos.getMaterialsFromBestDBByOd_no(myNumber);
        }

        public List<materials> getMaterialsFromBestDBByOd_no(string od_no,string  serviceName)
        {

            return aos.getMaterialsFromBestDBByOd_no(od_no, serviceName);
        }

        public List<GroupColor> getGroupColorFromBestDBByOd_no(string od_no,string serviceName,int type,int i)
        {

            return aos.getGroupColorFromBestDBByOd_no(od_no, serviceName, type,i);
        }

        public List<odb_pur> getODPuNoFromBestDBByPu_no(string od_no, string serviceName )
        {

            return aos.getODPuNoFromBestDBByPu_no(od_no, serviceName );
        }

        public List<allqtys> getAllQtysFromBestDBByPu_no(string od_no, string serviceName)
        {

            return aos.getAllQtysFromBestDBByPu_no(od_no, serviceName);
        }

        public  DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            //dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

            //原本dt.Columns.Add(property.Name, property.PropertyType);
            dt.Columns.AddRange(props.Select(p => new DataColumn( p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType)).ToArray()); //解決DataSet 不支援 System.Nullable<>

            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }

        

        public List<materials> getAccessoryOutByOd_no(string od_no )
        {

            return aos.getAccessoryOutByOd_no(od_no);
        }


        public List<materials> materialsDataToList(DataTable dt)
        {

            return aos.materialsDataToList(dt);
        }


        public int getAccessoryOutByReceiveNumber(string receive)
        {

            return aos.getAccessoryOutByReceiveNumber(receive);
        }



        /// <summary>
        ///  写入发料明细流水账数据库 accessoryouth
        /// </summary>
        /// <param name="accessorytb"> 写入发料明细流水账数据库 </param>
        /// <returns></returns>
        public int writeAccessoryhToDB(List<materialhs> accessorytb)
        {
            return aos.writeAccessoryhToDB(accessorytb);
        }
    }
}
