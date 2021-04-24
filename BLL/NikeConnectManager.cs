using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BLL
{
   public class NikeConnectManager
    {
        nikeConnectService ncs = new nikeConnectService();
        public DataTable ExcelRead(String filename, string sheetname, int headno)
        {
            COMMON.NPOIExcelNikeConnect NPOIexcel = new COMMON.NPOIExcelNikeConnect();
            nikeConnect[] nikeConnects = NPOIexcel.ExcelRead(filename, sheetname, headno);
            if (nikeConnects == null)
            {
                return null;
            }
            /*本地表*/
            //创建本地表
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("Vendor", typeof(string));
            table.Columns.Add("LiaisonOffice", typeof(string));
            table.Columns.Add("PONumber", typeof(string));
            table.Columns.Add("TradingCompanyPO", typeof(string));
            table.Columns.Add("POItem", typeof(string));
            table.Columns.Add("Customer1", typeof(string));
            table.Columns.Add("Customer2", typeof(string));

            table.Columns.Add("CustomerName", typeof(string));
            table.Columns.Add("CustomerCountry", typeof(string));
            table.Columns.Add("CustomerPO", typeof(string));
            table.Columns.Add("Material", typeof(string));
            table.Columns.Add("SourceType", typeof(string));
            table.Columns.Add("UOM", typeof(string));
            table.Columns.Add("Documentdate", typeof(string));
            table.Columns.Add("OGACDate", typeof(string));
            table.Columns.Add("GACDate", typeof(string));
            table.Columns.Add("InitialGACDate", typeof(string));
            table.Columns.Add("InitialGACReasonCode", typeof(string));
            table.Columns.Add("PurchaseGroup", typeof(string));

            table.Columns.Add("BuyGroup", typeof(string));
            table.Columns.Add("StatisticalDeliveryDate", typeof(string));
            table.Columns.Add("PreviousGAC", typeof(string));
            table.Columns.Add("Plant", typeof(string));
            table.Columns.Add("TradingCoPlantID", typeof(string));
            table.Columns.Add("TradingCoPlant", typeof(string));
            table.Columns.Add("DCILine", typeof(string));
            table.Columns.Add("DeliveryDate", typeof(string));
            table.Columns.Add("POCreateDate", typeof(string));
            table.Columns.Add("Createdby", typeof(string));
            table.Columns.Add("Mode", typeof(string));

            table.Columns.Add("TTMI1", typeof(string));
            table.Columns.Add("TTMI2", typeof(string));
            table.Columns.Add("GrossUnitPrice", typeof(string));
            table.Columns.Add("NetUnitPrice", typeof(string));
            table.Columns.Add("Qty", typeof(int));
            table.Columns.Add("QuantityShipped", typeof(int));
            table.Columns.Add("QuantityReceived", typeof(int));
            table.Columns.Add("IntransitQty", typeof(int));
            table.Columns.Add("TradingCoGrossUnitPrice", typeof(string));
            table.Columns.Add("TradingCoNetUnitPrice", typeof(string));

            try
            {
                for (int i = 0; i < nikeConnects.Count(); i++)
                {                 
                    int nid = 0;
                    String nVendor = Convert.ToString(nikeConnects[i].Vendor);
                    String nLiaisonOffice = Convert.ToString(nikeConnects[i].LiaisonOffice);
                    String nPONumber = Convert.ToString(nikeConnects[i].PONumber);
                    String nTradingCompanyPO = Convert.ToString(nikeConnects[i].TradingCompanyPO);
                    String nPOItem = Convert.ToString(nikeConnects[i].POItem);
                    String nCustomer1 = Convert.ToString(nikeConnects[i].Customer1);
                    String nCustomer2 = Convert.ToString(nikeConnects[i].Customer2);
                    String nCustomerName = Convert.ToString(nikeConnects[i].CustomerName);
                    String nCustomerCountry = Convert.ToString(nikeConnects[i].CustomerCountry);
                    String nCustomerPO = Convert.ToString(nikeConnects[i].CustomerPO);
                    String nMaterial = Convert.ToString(nikeConnects[i].Material);
                    String nSourceType = Convert.ToString(nikeConnects[i].SourceType);
                    String nUOM = Convert.ToString(nikeConnects[i].UOM);
                    String nDocumentdate = Convert.ToString(nikeConnects[i].Documentdate);
                    String nOGACDate = Convert.ToString(nikeConnects[i].OGACDate);
                    String nGACDate = Convert.ToString(nikeConnects[i].GACDate);
                    String nInitialGACDate = Convert.ToString(nikeConnects[i].InitialGACDate);
                    String nInitialGACReasonCode = Convert.ToString(nikeConnects[i].InitialGACReasonCode);
                    String nPurchaseGroup = Convert.ToString(nikeConnects[i].PurchaseGroup);
                    String nBuyGroup = Convert.ToString(nikeConnects[i].BuyGroup);
                    String nStatisticalDeliveryDate = Convert.ToString(nikeConnects[i].StatisticalDeliveryDate);
                    String nPreviousGAC = Convert.ToString(nikeConnects[i].PreviousGAC);
                    String nPlant = Convert.ToString(nikeConnects[i].Plant);
                    String nTradingCoPlantID = Convert.ToString(nikeConnects[i].TradingCoPlantID);
                    String nTradingCoPlant = Convert.ToString(nikeConnects[i].TradingCoPlant);
                    String nDCILine = Convert.ToString(nikeConnects[i].DCILine);
                    String nDeliveryDate = Convert.ToString(nikeConnects[i].DeliveryDate);
                    String nPOCreateDate = Convert.ToString(nikeConnects[i].POCreateDate);
                    String nCreatedby = Convert.ToString(nikeConnects[i].Createdby);
                    String nMode = Convert.ToString(nikeConnects[i].Mode);
                    String nTTMI1 = Convert.ToString(nikeConnects[i].TTMI1);
                    String nTTMI2 = Convert.ToString(nikeConnects[i].TTMI2);
                    String nGrossUnitPrice = Convert.ToString(nikeConnects[i].GrossUnitPrice);

                    String nNetUnitPrice = Convert.ToString(nikeConnects[i].NetUnitPrice);
                    int nQty = Convert.ToInt32(nikeConnects[i].Qty);
                    int nQuantityShipped = Convert.ToInt32(nikeConnects[i].QuantityShipped);
                    int nQuantityReceived = Convert.ToInt32(nikeConnects[i].QuantityReceived);
                    int nIntransitQty = Convert.ToInt32(nikeConnects[i].IntransitQty);
                    String nTradingCoGrossUnitPrice = Convert.ToString(nikeConnects[i].TradingCoGrossUnitPrice);
                    String nTradingCoNetUnitPrice = Convert.ToString(nikeConnects[i].TradingCoNetUnitPrice);

                    //本地表加入数据  Unique
                    DataRow row = table.NewRow();
                    row["id"] = nid;                   
                    row["Vendor"] = nVendor.Replace("\"","-");
                    row["LiaisonOffice"] = nLiaisonOffice.Replace("\"","-");
                    row["PONumber"] = nPONumber.Replace("\"","-");
                    row["TradingCompanyPO"] = nTradingCompanyPO.Replace("\"","-");
                    row["POItem"] = nPOItem.Replace("\"","-");
                    row["Customer1"] = nCustomer1.Replace("\"","-");
                    row["Customer2"] = nCustomer2.Replace("\"","-");

                    row["CustomerName"] = nCustomerName.Replace("\"","-");
                    row["CustomerCountry"] = nCustomerCountry.Replace("\"","-");
                    row["CustomerPO"] = nCustomerPO.Replace("\"","-");
                    row["Material"] = nMaterial.Replace("\"","-");
                    row["SourceType"] = nSourceType.Replace("\"","-");
                    row["UOM"] = nUOM.Replace("\"","-");
                    row["Documentdate"] = nDocumentdate.Replace("\"","-");
                    row["OGACDate"] = nOGACDate.Replace("\"","-");
                    row["GACDate"] = nGACDate.Replace("\"","-");
                    row["InitialGACDate"] = nInitialGACDate.Replace("\"","-");
                    row["InitialGACReasonCode"] = nInitialGACReasonCode.Replace("\"","-");
                    row["PurchaseGroup"] = nPurchaseGroup.Replace("\"","-");

                    row["BuyGroup"] = nBuyGroup.Replace("\"","-");
                    row["StatisticalDeliveryDate"] = nStatisticalDeliveryDate.Replace("\"","-");
                    row["PreviousGAC"] = nPreviousGAC.Replace("\"","-");
                    row["Plant"] = nPlant.Replace("\"","-");
                    row["TradingCoPlantID"] = nTradingCoPlantID.Replace("\"","-");
                    row["TradingCoPlant"] = nTradingCoPlant.Replace("\"","-");
                    row["DCILine"] = nDCILine.Replace("\"","-");
                    row["DeliveryDate"] = nDeliveryDate.Replace("\"","-");
                    row["POCreateDate"] = nPOCreateDate.Replace("\"","-");
                    row["Createdby"] = nCreatedby.Replace("\"","-");
                    row["Mode"] = nMode.Replace("\"","-");

                    row["TTMI1"] = nTTMI1.Replace("\"","-");
                    row["TTMI2"] = nTTMI2.Replace("\"","-");
                    row["GrossUnitPrice"] = nGrossUnitPrice.Replace("\"","-");
                    row["NetUnitPrice"] = nNetUnitPrice.Replace("\"","-");
                    row["Qty"] = nQty;
                    row["QuantityShipped"] = nQuantityShipped;
                    row["QuantityReceived"] = nQuantityReceived;
                    row["IntransitQty"] = nIntransitQty;
                    row["TradingCoGrossUnitPrice"] = nTradingCoGrossUnitPrice.Replace("\"","-");
                    row["TradingCoNetUnitPrice"] = nTradingCoNetUnitPrice.Replace("\"","-");
                    table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //如果表的第一行 Vendor	Liaison Office 不为空  下一行数据 Vendor	Liaison Office 为空 复制上一行的 Vendor	Liaison Office 
            //如果表的下一行数据 PO Number为空  复制上一行的 PO Number	Trading Co PO Number 数据下来
            //如果表的下一行数据 PO Number不为空  Trading Co PO Number 为空  那行 Trading Co PO Number = PO Number
            //如果表的下一行数据 PO Number为 Result 删除这一行
            for (int i = 0; i < table.Rows.Count; i++)
            {
                //      row["Vendor"] = nVendor;
                //      row["LiaisonOffice"] = nLiaisonOffice;
                if (table.Rows[0]["Vendor"].ToString() != "")
                {
                    table.Rows[i]["Vendor"] = table.Rows[0]["Vendor"];
                }
                if (table.Rows[0]["LiaisonOffice"].ToString() != "")
                {
                    table.Rows[i]["LiaisonOffice"] = table.Rows[0]["LiaisonOffice"];
                }

                // row["PONumber"] = nPONumber;
                // row["TradingCompanyPO"] = nTradingCompanyPO;
               

                if ( table.Rows[i]["TradingCompanyPO"].ToString() == "" && (table.Rows[i]["PONumber"].ToString() == "" || table.Rows[i]["PONumber"].ToString() == "#"))
                {
                    table.Rows[i]["PONumber"] = table.Rows[i - 1]["PONumber"];
                    table.Rows[i]["TradingCompanyPO"] = table.Rows[i - 1]["TradingCompanyPO"];
                }

                if ((table.Rows[i]["TradingCompanyPO"].ToString() == "" || table.Rows[i]["TradingCompanyPO"].ToString() == "#")  && table.Rows[i]["PONumber"].ToString() != "")
                {
                    table.Rows[i]["TradingCompanyPO"] = table.Rows[i]["PONumber"];
                }

                if ( table.Rows[i]["PONumber"].ToString() == "Result" )
                {
                    table.Rows[i].Delete();
                }
                if ( table.Rows[i]["Vendor"].ToString() == "Overall Result")
                {
                    table.Rows[i].Delete();
                }
            }

            return table;
        }

        public bool isHaveByNikeConnect(string Npo)
        {
            int i = ncs.isHaveByNikeConnect(Npo);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int writeNcsToDb(DataTable table)
        {

            return ncs.writeNcsToDb(table);


        }

        public int delDoubleRows()
        {
            return ncs.delDoubleRows();
        }
    }
}

