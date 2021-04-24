using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace BLL
{
    public class loadScanManager
    {
        loadScanService lss = new loadScanService();
        public DataTable ExcelRead(String filename, string sheetname, int headno, string selectedOrg, string selectedSubinv)
        {
            COMMON.NPOIExcelScan NPOIexcel = new COMMON.NPOIExcelScan();
            Scans[] scans = NPOIexcel.ExcelRead(filename, sheetname, headno);
            if (scans == null)
            {
                return null;
            }            
            //创建本地表
            DataTable table = new DataTable();
            table.Columns.Add("TagNumber", typeof(string));
            table.Columns.Add("ScanTime", typeof(string));
            table.Columns.Add("Kg", typeof(string));
            table.Columns.Add("Subinv", typeof(string));
            table.Columns.Add("Con_no", typeof(string));
            table.Columns.Add("Location", typeof(string));
            table.Columns.Add("Org", typeof(string));
            table.Columns.Add("Cust_Id", typeof(string)); 
            try
            {
                for (int i = 0; i < scans.Count(); i++)
                {
                    String tagNumber = Convert.ToString(scans[i].TagNumber);
                    String scanTime = Convert.ToString(scans[i].ScanTime);
                    String kg = Convert.ToString(scans[i].Kg);
                    String subinv = selectedSubinv;
                    String con_no = Convert.ToString(scans[i].Con_no);
                    String location = Convert.ToString(scans[i].Location);
                    //if(location is null ||  location.Length <= 0 || location.Length > 4)
                  //  {
                     //   return null;
                  //  }
                    String org = selectedOrg;
                    String cust_Id = Convert.ToString(scans[i].Cust_Id);

                    //本地表加入数据  Unique
                    DataRow row = table.NewRow();
                    row["TagNumber"] = tagNumber;
                    row["ScanTime"] = scanTime;
                    row["Kg"] = kg;
                    row["Subinv"] = subinv;
                    row["Con_no"] = con_no;
                    row["Location"] = location;
                    row["Org"] = org;
                    row["Cust_Id"] = cust_Id; 
                    table.Rows.Add(row);
                    /*************/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (table.Rows.Count <= 0)
            {
                return table;
            }

            
            //创建处理表
            DataTable tempTb = new DataTable();
            tempTb.Columns.Add("TagNumber", typeof(string));
            tempTb.Columns.Add("ScanTime", typeof(string));
            tempTb.Columns.Add("Kg", typeof(string));
            tempTb.Columns.Add("Subinv", typeof(string));
            tempTb.Columns.Add("Con_no", typeof(string));
            tempTb.Columns.Add("Location", typeof(string));
            tempTb.Columns.Add("Org", typeof(string));
            tempTb.Columns.Add("Cust_Id", typeof(string));
            string locat = "";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string tag = table.Rows[i]["TagNumber"].ToString().Trim().ToUpper();
                string custNumber = "";

                if (tag.Length>0 && tag.Length < 6)
                {
                    locat = tag;
                    continue;
                }
                else
                {
                    string custid = "";
                    string serialFrom = "";
                    if (tag.Length == 8)
                    {
                        custNumber = tag.Substring(0, 2).ToUpper();
                        if (custNumber == "Z0")
                        {
                            custid = "TNF";
                            serialFrom = tag;
                        }
                       
                    }else if(tag.Length == 12)
                    {
                        custNumber = tag.Substring(0, 1).ToUpper();
                        if (custNumber == "C")
                        {
                            custid = "FANATICS";
                            serialFrom = tag;
                        }
                        else
                        {
                            custid = "HURLEY";
                            serialFrom = tag;
                        }
                    }else if(tag.Length == 14 || tag.Length == 17)
                    {
                        custNumber = tag.Substring(0, 4).ToUpper();
                        if (custNumber == "1010")
                        {
                            custid = "ASICS";
                            serialFrom = tag;
                        }
                        else if (custNumber == "4500")
                        {
                            custid = "ASICS";
                            serialFrom = tag;
                        }
                    }else if(tag.Length == 20)
                    {
                        custNumber = tag.Substring(0, 5);
                        switch (custNumber)
                        {
                            case "00047":
                                custid = "NIKE";
                                serialFrom = tag.Substring(10, 9);
                                serialFrom = Convert.ToString(Convert.ToInt32(serialFrom));

                                break;
                            case "00006":
                                custid = "LULU";
                                serialFrom = tag.Substring(10, 9);
                                serialFrom = Convert.ToString(Convert.ToInt32(serialFrom));
                                break;
                            case "00008":
                                string custnumberall = tag.Substring(0, 8);
                                if (custnumberall == "00008848")
                                {
                                    custid = "TNF";
                                    serialFrom = tag;
                                }
                                else
                                {
                                    custid = "HURLEY";
                                    serialFrom = tag.Substring(10, 9);
                                }
                                break;
                            default:
                                custid = "NA";
                                break;
                        }
                    }
                    else
                    {
                        custid = "NA";
                    }

                    DataRow row = tempTb.NewRow();
                    row["TagNumber"] = table.Rows[i]["TagNumber"].ToString().Trim().ToUpper();
                    row["ScanTime"] = table.Rows[i]["ScanTime"].ToString().Trim().ToUpper();
                    row["Kg"] = table.Rows[i]["Kg"].ToString().Trim().ToUpper();
                    row["Subinv"] = table.Rows[i]["Subinv"].ToString().Trim().ToUpper();
                    row["Con_no"] = serialFrom;
                    row["Location"] = locat;
                    row["Org"] = table.Rows[i]["Org"].ToString().Trim().ToUpper();
                    row["Cust_Id"] = custid;
                    tempTb.Rows.Add(row);
                }
               

            }
            return tempTb;
        }

        public bool isHaveByTradingComanyPO(string Tpo)
        {
            int i = lss.isHaveByTradingComanyPO(Tpo);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool locationIsExists(List<string> locations, string Location)
        {
            bool result = true;
            if (locations.Count <= 0)
            {
                return false;
            }
            for (int i = 0; i < locations.Count; i++)
            {
                if (locations[i] == Location)
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

        private List<string>  isExistsLocatServeLocations(List<string> locations, List<string> serverLocations)
        {
            List<string> result = new List<string> ();
            result.Add("");
            result.Add("");


            // bool result = false;
            for (int i = 0; i < locations.Count; i++)
            {
                for(int j = 0; j < serverLocations.Count; j++)
                {
                    if(locations[i] != serverLocations[j])
                    {
                        result[0] = j.ToString();
                        result[1] = "-1";
                       // result = false;                       
                    }
                    else
                    {
                        result[0] = j.ToString();
                        result[1] = "0";
                        //result = true;
                        break;
                    }
                }
                if (result[1] == "-1")
                {
                    result[0] = locations[i];
                    result[1] = "-1";
                    break;
                }
            }
            return result;
        }

        public List<string> writeInvsToDb(DataTable table,string org,string subinv)
        {
            List<string> result = new List<string>();
            result.Add("");
            result.Add("");


            List<string> locations = new List<string>();
            foreach (DataRow dr in table.Rows)
            {
                if (!locationIsExists(locations, dr["Location"].ToString()))
                {
                    locations.Add(dr["Location"].ToString());

                }
            }

            DataTable serverLocationDT = lss.getLocationByOrgAndSubinv(org, subinv);
            List<string> serverLocations = new List<string>();
            foreach (DataRow dr in serverLocationDT.Rows)
            {
                if (!locationIsExists(serverLocations, dr["location"].ToString()))
                {
                    serverLocations.Add(dr["location"].ToString());

                }
            }


            List<string>  ServeLocations = isExistsLocatServeLocations(locations, serverLocations);
            if (ServeLocations[1] == "-1")
            {
                result[0] = ServeLocations[0];
                result[1] = "-4";
                return result;                
            }

            /*id ,TagNumber,Cust_id,Location,update_date,org,con_no,create_pc,kg,subinv,ScanTime,exeStatus*/
            DataTable invs = new DataTable(); 
            invs.Columns.Add("TagNumber", typeof(string));
            invs.Columns.Add("Cust_id", typeof(string));
            invs.Columns.Add("Location", typeof(string));
            invs.Columns.Add("update_date", typeof(string));
            invs.Columns.Add("org", typeof(string));
            invs.Columns.Add("con_no", typeof(string));
            invs.Columns.Add("create_pc", typeof(string));
            invs.Columns.Add("kg", typeof(string));
            invs.Columns.Add("subinv", typeof(string));
            invs.Columns.Add("ScanTime", typeof(string));
            invs.Columns.Add("exeStatus", typeof(string));

         

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string location = "";
                string tagNumber = table.Rows[i]["TagNumber"].ToString();
                
                    if (!this.barcodeCheck(tagNumber))
                    {
                        result[0] = (i+1).ToString();
                        result[1] = "-2";
                        return result;
                    }
                
              
                DataRow row = invs.NewRow(); 

                row["TagNumber"] = table.Rows[i]["TagNumber"].ToString();

                row["Cust_id"] = table.Rows[i]["Cust_id"].ToString();
                location =table.Rows[i]["Location"].ToString().ToUpper();
                if (location is null || location.Length <=0 ||  location.Length >4)
                {
                    result[0] = (i+1).ToString();
                    result[1] = "-1";
                    return result;
                }
                string ScanTime = table.Rows[i]["ScanTime"].ToString();
                if (ScanTime.Length <= 0)
                {
                    ScanTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                row["Location"] = table.Rows[i]["Location"].ToString().ToUpper();
                row["update_date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["org"] = table.Rows[i]["org"].ToString();
                row["con_no"] = table.Rows[i]["con_no"].ToString();
                row["create_pc"] = Dns.GetHostName();
                row["kg"] = table.Rows[i]["kg"].ToString();
                row["subinv"] = table.Rows[i]["subinv"].ToString();
                row["ScanTime"] = ScanTime;
                row["exeStatus"] = "N";
                invs.Rows.Add(row);
            }
            result[0] = table.Rows.Count.ToString();
            result[1] =  lss.writeInvsToDb(invs).ToString();
            return result;


        }

       public bool  barcodeCheck(string tagNumber)
        {
            if(tagNumber.Length <= 0)
            {
                return false;
            }

            if (tagNumber.Length == 20)
            {

                // let weight[];
                int CheckCode = -1; // 校验码
                int sum = 0; // 权位与位数相乘之后全部相加
                             // 从第3位开起算起 到 18位 第19位为校验码
                for (int i = 2; i < tagNumber.Length - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        // console.log('tagNumber[' + i + ']', tagNumber[i]);
                        //   sum = sum +   tagNumber[i] * 3
                        sum = sum + Convert.ToInt32(tagNumber.Substring(i, 1)) * 3;
                        //  power = 3;
                        // console.log('tagNumber[' + i + '] =>sum', sum);
                    }
                    else
                    {
                        //  tagNumber[i] * 1;
                        // tslint:disable-next-line: radix
                        //  sum = sum + Convert.ToInt32(tagNumber[i]);
                        sum = sum + Convert.ToInt32(tagNumber.Substring(i, 1));
                        // console.log('tagNumber[' + i + '] =>sum', sum);
                        //  power = 1;
                    }
                    // weight.push({
                    //   number:tagNumber[i],
                    //   power: power
                    // });
                }
                // console.log('CheckCode==>sum', sum);
                if (sum % 10 == 0)
                {
                    CheckCode = 0;
                }
                else
                {
                    CheckCode = 10 - sum % 10;
                }

                // console.log('CheckCode', CheckCode);
                // console.log('tagNumber[19]', tagNumber[19]);
                if (Convert.ToInt32(tagNumber.Substring(19, 1)) == CheckCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }else
            {
               if(tagNumber.IndexOf("+")==-1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public int delDoubleRows()
        {
            return lss.delDoubleRows();
        }
        public DataTable getSubinvByOrg(string org) 
        {
            return lss.getSubinvByOrg(org);
             
        }
    }
}
