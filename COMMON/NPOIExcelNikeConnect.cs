using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace COMMON
{
    public  class NPOIExcelNikeConnect
    {
        //读EXCEL   导入EXCEL表
        public nikeConnect[] ExcelRead(string file, string sheetname, int headno)
        {
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper(file))
                {
                    DataTable dt = excelHelper.ExcelToDataTable(sheetname, headno);
                    if (dt.Rows.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        nikeConnect[] nikeConnects = new nikeConnect[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            nikeConnects[i] = ToModel(dt.Rows[i]);//这里转换过来
                        }
                        return nikeConnects;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        private nikeConnect ToModel(DataRow row)//建立要导入的文件的model
        {
            nikeConnect nikeConnect = new nikeConnect();
            if (row.ItemArray.Length > 0)
            {
                nikeConnect.Vendor = Convert.ToString(row.ItemArray[0].ToString());
            }
            if (row.ItemArray.Length > 1)
            {
                nikeConnect.LiaisonOffice = Convert.ToString(row.ItemArray[1].ToString());
            }
            if (row.ItemArray.Length > 2)
            {
                nikeConnect.PONumber = Convert.ToString(row.ItemArray[2].ToString());
            }
            if (row.ItemArray.Length > 3)
            {
                nikeConnect.TradingCompanyPO = Convert.ToString(row.ItemArray[3].ToString());
            }
            if (row.ItemArray.Length > 4)
            {
                nikeConnect.POItem = Convert.ToString(row.ItemArray[4].ToString());
            }
            if (row.ItemArray.Length > 5)
            {
                nikeConnect.Customer1 = Convert.ToString(row.ItemArray[5].ToString());
            }

            if (row.ItemArray.Length > 6)
            {
                nikeConnect.Customer2 = Convert.ToString(row.ItemArray[6].ToString());
            }
            if (row.ItemArray.Length > 7)
            {
                nikeConnect.CustomerName = Convert.ToString(row.ItemArray[7].ToString());
            }
            if (row.ItemArray.Length > 8)
            {
                nikeConnect.CustomerCountry = Convert.ToString(row.ItemArray[8].ToString());
            }
            if (row.ItemArray.Length > 9)
            {
                nikeConnect.CustomerPO = Convert.ToString(row.ItemArray[9].ToString());
            }
            if (row.ItemArray.Length > 10)
            {
                nikeConnect.Material = Convert.ToString(row.ItemArray[10].ToString());
            }

            if (row.ItemArray.Length > 11)
            {
                nikeConnect.SourceType = Convert.ToString(row.ItemArray[11].ToString());
            }
            if (row.ItemArray.Length > 12)
            {
                nikeConnect.UOM = Convert.ToString(row.ItemArray[12].ToString());
            }
            if (row.ItemArray.Length > 13)
            {
                nikeConnect.Documentdate = Convert.ToString(row.ItemArray[13].ToString());
            }
            if (row.ItemArray.Length > 14)
            {
                nikeConnect.OGACDate = Convert.ToString(row.ItemArray[14].ToString());
            }
            if (row.ItemArray.Length > 15)
            {
                nikeConnect.GACDate = Convert.ToString(row.ItemArray[15].ToString());
            }

            if (row.ItemArray.Length > 16)
            {
                nikeConnect.InitialGACDate = Convert.ToString(row.ItemArray[16].ToString());
            }
            if (row.ItemArray.Length > 17)
            {
                nikeConnect.InitialGACReasonCode = Convert.ToString(row.ItemArray[17].ToString());
            }
            if (row.ItemArray.Length > 18)
            {
                nikeConnect.PurchaseGroup = Convert.ToString(row.ItemArray[18].ToString());
            }
            if (row.ItemArray.Length > 19)
            {
                nikeConnect.BuyGroup = Convert.ToString(row.ItemArray[19].ToString());
            }
            if (row.ItemArray.Length > 20)
            {
                nikeConnect.StatisticalDeliveryDate = Convert.ToString(row.ItemArray[20].ToString());
            }

            if (row.ItemArray.Length > 21)
            {
                nikeConnect.PreviousGAC = Convert.ToString(row.ItemArray[21].ToString());
            }
            if (row.ItemArray.Length > 22)
            {
                nikeConnect.Plant = Convert.ToString(row.ItemArray[22].ToString());
            }
            if (row.ItemArray.Length > 23)
            {
                nikeConnect.TradingCoPlantID = Convert.ToString(row.ItemArray[23].ToString());
            }
            if (row.ItemArray.Length > 24)
            {
                nikeConnect.TradingCoPlant = Convert.ToString(row.ItemArray[24].ToString());
            }
            if (row.ItemArray.Length > 25)
            {
                nikeConnect.DCILine = Convert.ToString(row.ItemArray[25].ToString());
            }

            if (row.ItemArray.Length > 26)
            {
                nikeConnect.DeliveryDate = Convert.ToString(row.ItemArray[26].ToString());
            }
            if (row.ItemArray.Length > 27)
            {
                nikeConnect.POCreateDate = Convert.ToString(row.ItemArray[27].ToString());
            }
            if (row.ItemArray.Length > 28)
            {
                nikeConnect.Createdby = Convert.ToString(row.ItemArray[28].ToString());
            }
            if (row.ItemArray.Length > 29)
            {
                nikeConnect.Mode = Convert.ToString(row.ItemArray[29].ToString());
            }
            if (row.ItemArray.Length > 30)
            {
                nikeConnect.TTMI1 = Convert.ToString(row.ItemArray[30].ToString());
            }

            if (row.ItemArray.Length > 31)
            {
                nikeConnect.TTMI2 = Convert.ToString(row.ItemArray[31].ToString());
            }
            if (row.ItemArray.Length > 32)
            {
                nikeConnect.GrossUnitPrice = Convert.ToString(row.ItemArray[32].ToString());
            }
            if (row.ItemArray.Length > 33)
            {
                nikeConnect.NetUnitPrice = Convert.ToString(row.ItemArray[33].ToString());
            }
            if (row.ItemArray.Length > 34)
            {
                nikeConnect.Qty = Convert.ToInt32(row.ItemArray[34].ToString());
            }
            if (row.ItemArray.Length > 35)
            {
                nikeConnect.QuantityShipped = Convert.ToInt32(row.ItemArray[35].ToString());
            }

            if (row.ItemArray.Length > 36)
            {
                nikeConnect.QuantityReceived = Convert.ToInt32(row.ItemArray[36].ToString());
            }
            if (row.ItemArray.Length > 37)
            {
                nikeConnect.IntransitQty = Convert.ToInt32(row.ItemArray[37].ToString());
            }
            if (row.ItemArray.Length > 38)
            {
                nikeConnect.TradingCoGrossUnitPrice = Convert.ToString(row.ItemArray[38].ToString());
            }
            if (row.ItemArray.Length > 39)
            {
                nikeConnect.TradingCoNetUnitPrice = Convert.ToString(row.ItemArray[39].ToString());
            }
                      
            return nikeConnect;
        }

        public string[] getExcelSheetSum(String filename)
        {
            ExcelHelper excelHelper = new ExcelHelper(filename);
            string[] sheetname = excelHelper.getExcelSheetName(filename);   
            return sheetname;
        }
    }
}
