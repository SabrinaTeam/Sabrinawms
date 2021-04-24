using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class nikeConnect
    {
        public int id { set; get; }
        public string Vendor { set; get; }
        public string LiaisonOffice { set; get; }
        public string PONumber { set; get; }
        public string TradingCompanyPO { set; get; }
        public string POItem { set; get; }
        public string Customer1 { set; get; }
        public string Customer2 { set; get; }
        public string CustomerName { set; get; }
        public string CustomerCountry { set; get; }

        public string CustomerPO { set; get; }
        public string Material { set; get; }
        public string SourceType { set; get; }
        public string UOM { set; get; }
        public string Documentdate { set; get; }
        public string OGACDate { set; get; }
        public string GACDate { set; get; }
        public string InitialGACDate { set; get; }
        public string InitialGACReasonCode { set; get; }
        public string PurchaseGroup { set; get; }
        public string BuyGroup { set; get; }

        public string StatisticalDeliveryDate { set; get; }
        public string PreviousGAC { set; get; }
        public string Plant { set; get; }
        public string TradingCoPlantID { set; get; }
        public string TradingCoPlant { set; get; }
        public string DCILine { set; get; }
        public string DeliveryDate { set; get; }
        public string POCreateDate { set; get; }
        public string Createdby { set; get; }
        public string Mode { set; get; }
        public string TTMI1 { set; get; }
        public string TTMI2 { set; get; }

        public string GrossUnitPrice { set; get; }
        public string NetUnitPrice { set; get; }
        public int? Qty { set; get; }
        public int? QuantityShipped { set; get; }
        public int? QuantityReceived { set; get; }
        public int? IntransitQty { set; get; }
        public string TradingCoGrossUnitPrice { set; get; }
        public string TradingCoNetUnitPrice { set; get; }
      
    }
}
