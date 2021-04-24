using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class nikeConnectService
    {
        public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;
        public int isHaveByNikeConnect(string Npo)
        {
            string sql = @"SELECT PONumber from nikeconnect WHERE PONumber ='" + Npo + "'";
            DataTable result = new DataTable();            
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExcuteTable(sql);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExcuteTable(sql);
            }
            return result.Rows.Count;
        }
        public int writeNcsToDb(DataTable dt)
        {
            string sqlstr = "";
            string sqlValue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Vendor,LiaisonOffice,PONumber,TradingCompanyPO,POItem,Customer1,
                sqlValue = sqlValue +
                           "(\"" + dt.Rows[i]["Vendor"].ToString() + "\",\""  
                               + dt.Rows[i]["LiaisonOffice"].ToString() + "\",\""
                               + dt.Rows[i]["PONumber"].ToString() + "\",\""
                               + dt.Rows[i]["TradingCompanyPO"].ToString() + "\",\""
                               + dt.Rows[i]["POItem"].ToString() + "\",\""
                               + dt.Rows[i]["Customer1"].ToString() + "\",\""

                               // Customer2,CustomerName,CustomerCountry,CustomerPO,Material,SourceType,

                               + dt.Rows[i]["Customer2"].ToString() + "\",\""
                               + dt.Rows[i]["CustomerName"].ToString() + "\",\""
                               + dt.Rows[i]["CustomerCountry"].ToString() + "\",\""
                               + dt.Rows[i]["CustomerPO"].ToString() + "\",\""
                               + dt.Rows[i]["Material"].ToString() + "\",\""
                               + dt.Rows[i]["SourceType"].ToString() + "\",\""

                              // UOM,Documentdate,OGACDate,GACDate,InitialGACDate,InitialGACReasonCode,
                               + dt.Rows[i]["UOM"].ToString() + "\",\""
                               + dt.Rows[i]["Documentdate"].ToString() + "\",\""
                               + dt.Rows[i]["OGACDate"].ToString() + "\",\""
                               + dt.Rows[i]["GACDate"].ToString() + "\",\""
                               + dt.Rows[i]["InitialGACDate"].ToString() + "\",\""
                               + dt.Rows[i]["InitialGACReasonCode"].ToString() + "\",\""

                               //PurchaseGroup,BuyGroup,StatisticalDeliveryDate,PreviousGAC,Plant,TradingCoPlantID,   
                               + dt.Rows[i]["PurchaseGroup"].ToString() + "\",\""
                               + dt.Rows[i]["BuyGroup"].ToString() + "\",\""
                               + dt.Rows[i]["StatisticalDeliveryDate"].ToString() + "\",\""
                               + dt.Rows[i]["PreviousGAC"].ToString() + "\",\""
                               + dt.Rows[i]["Plant"].ToString() + "\",\""
                               + dt.Rows[i]["TradingCoPlantID"].ToString() + "\",\""

                               // TradingCoPlant,DCILine,DeliveryDate,POCreateDate,Createdby,Mode,TTMI1,TTMI2,
                               + dt.Rows[i]["TradingCoPlant"].ToString() + "\",\""
                               + dt.Rows[i]["DCILine"].ToString() + "\",\""
                               + dt.Rows[i]["DeliveryDate"].ToString() + "\",\""
                               + dt.Rows[i]["POCreateDate"].ToString() + "\",\""
                               + dt.Rows[i]["Createdby"].ToString() + "\",\""
                               + dt.Rows[i]["Mode"].ToString() + "\",\""
                               + dt.Rows[i]["TTMI1"].ToString() + "\",\""
                               + dt.Rows[i]["TTMI2"].ToString() + "\",\""

                               //GrossUnitPrice,NetUnitPrice,Qty,QuantityShipped,QuantityReceived,IntransitQty,
                               + dt.Rows[i]["GrossUnitPrice"].ToString() + "\",\""
                               + dt.Rows[i]["NetUnitPrice"].ToString() + "\",\""
                               + dt.Rows[i]["Qty"].ToString() + "\",\""
                               + dt.Rows[i]["QuantityShipped"].ToString() + "\",\""
                               + dt.Rows[i]["QuantityReceived"].ToString() + "\",\""
                               + dt.Rows[i]["IntransitQty"].ToString() + "\",\""

                               //TradingCoGrossUnitPrice,TradingCoNetUnitPrice
                               + dt.Rows[i]["TradingCoGrossUnitPrice"].ToString() + "\",\""
                               + dt.Rows[i]["TradingCoNetUnitPrice"].ToString() + "\" ),";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) + ";";
            sqlstr = @"INSERT INTO nikeconnect ( Vendor,LiaisonOffice,PONumber,TradingCompanyPO,POItem,Customer1,
                                                 Customer2,CustomerName,CustomerCountry,CustomerPO,Material,SourceType,
                                                 UOM,Documentdate,OGACDate,GACDate,InitialGACDate,InitialGACReasonCode,
                                                 PurchaseGroup,BuyGroup,StatisticalDeliveryDate,PreviousGAC,Plant,TradingCoPlantID,                                       
                                                 TradingCoPlant,DCILine,DeliveryDate,POCreateDate,Createdby,Mode,TTMI1,TTMI2,
                                                 GrossUnitPrice,NetUnitPrice,Qty,QuantityShipped,QuantityReceived,IntransitQty,
                                                 TradingCoGrossUnitPrice,TradingCoNetUnitPrice
                    )  VALUES " + sqlValue;

            int result =0; 
           
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sqlstr);
            }
            return result;
        }


        public int delDoubleRows()
        {
            string sql = @"
                            DELETE 
                                    FROM
	                                    nikeconnect 
                                    WHERE
	                                    id IN (
	                                    SELECT
		                                    a.id 
	                                    FROM
		                                    (
		                                    SELECT
			                                    id 
		                                    FROM
			                                    nikeconnect a 
		                                    WHERE
			                                    ( a.PONumber, a.TradingCompanyPO, a.POItem, a.OGACDate, a.Plant ) IN (
			                                    SELECT
				                                    PONumber,
				                                    TradingCompanyPO,
				                                    POItem,
				                                    OGACDate,
				                                    Plant 
			                                    FROM
				                                    nikeconnect 
			                                    GROUP BY
				                                    PONumber,
				                                    TradingCompanyPO,
				                                    POItem,
				                                    OGACDate,
				                                    Plant 
			                                    HAVING
				                                    count(*) > 1 
			                                    ) 
		                                    ) a 
	                                    ) 
	                                    AND ID NOT IN (
	                                    SELECT
		                                    b.id 
	                                    FROM
	                                    ( SELECT max( ID ) id FROM nikeconnect GROUP BY PONumber, TradingCompanyPO, POItem, OGACDate, Plant HAVING count(*)> 1 ) b 
	                                    )";
            int result = 0;
            

            
            if (MiddleWare == "1")
            {
                result = MyCatfsg_SqlHelper.ExecuteNonQuery(sql);
            }
            else
            {
                result = Mysqlfsg_SqlHelper.ExecuteNonQuery(sql);
            }
            return result;
        }
    }
}
