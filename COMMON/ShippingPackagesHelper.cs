using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace COMMON
{
    public class ShippingPackagesHelper
    {
        public static readonly string SPSqlconnStr = ConfigurationManager.ConnectionStrings["SPSqlconnStr"].ConnectionString;

        public string SqlBulkToSQL_sp_temp(DataTable t_sp_temp)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(SPSqlconnStr))
            {
               

                bulkcopy.BulkCopyTimeout = 0;//超时设置
                bulkcopy.DestinationTableName = "t_sp_temp"; 
                bulkcopy.ColumnMappings.Add("type", "type");
                bulkcopy.ColumnMappings.Add("ftyNo", "ftyNo");
                bulkcopy.ColumnMappings.Add("season", "season");
                bulkcopy.ColumnMappings.Add("BVPO", "BVPO");
                bulkcopy.ColumnMappings.Add("masterPO", "masterPO");
                bulkcopy.ColumnMappings.Add("GtnPO", "GtnPO");
                bulkcopy.ColumnMappings.Add("po_mainLine", "po_mainLine");
                bulkcopy.ColumnMappings.Add("styleNumber", "styleNumber");
                bulkcopy.ColumnMappings.Add("styleName", "styleName");

                bulkcopy.ColumnMappings.Add("color", "color");
                bulkcopy.ColumnMappings.Add("colDescription", "colDescription");
                bulkcopy.ColumnMappings.Add("channel", "channel");
                bulkcopy.ColumnMappings.Add("totalQty", "totalQty");
                bulkcopy.ColumnMappings.Add("HOD", "HOD");
                bulkcopy.ColumnMappings.Add("befoeHOD", "befoeHOD");
                bulkcopy.ColumnMappings.Add("newHOD", "newHOD");
                bulkcopy.ColumnMappings.Add("shipMode", "shipMode");

                bulkcopy.ColumnMappings.Add("sourceTag", "sourceTag");
                bulkcopy.ColumnMappings.Add("wwwt", "wwwt");
                bulkcopy.ColumnMappings.Add("citHangTag", "citHangTag");
                bulkcopy.ColumnMappings.Add("Fastener", "Fastener");
                bulkcopy.ColumnMappings.Add("steelNumber", "steelNumber");
                bulkcopy.ColumnMappings.Add("cup", "cup");
                bulkcopy.ColumnMappings.Add("cclable", "cclable");
                bulkcopy.ColumnMappings.Add("sensitive", "sensitive");
                bulkcopy.ColumnMappings.Add("remark", "remark");
                bulkcopy.ColumnMappings.Add("org", "org");
                bulkcopy.ColumnMappings.Add("isCancel", "isCancel");
                bulkcopy.ColumnMappings.Add("modify", "modify");
                bulkcopy.ColumnMappings.Add("overflow", "overflow"); 

                try
                {
                    bulkcopy.WriteToServer(t_sp_temp);
                    return "200";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }

        public string SqlBulkToSQL_spSize_temp(DataTable spSize_temp)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(SPSqlconnStr))
            {
                bulkcopy.BulkCopyTimeout = 0;//超时设置
                bulkcopy.DestinationTableName = "T_size_temp";               
                bulkcopy.ColumnMappings.Add("ftyNo", "ftyNo");
                bulkcopy.ColumnMappings.Add("season", "season");
                bulkcopy.ColumnMappings.Add("masterPo", "masterPo");
                bulkcopy.ColumnMappings.Add("GtnPO", "GtnPO");
                bulkcopy.ColumnMappings.Add("po_mainLine", "po_mainLine");
                bulkcopy.ColumnMappings.Add("styleNumber", "styleNumber"); 

                bulkcopy.ColumnMappings.Add("color", "color");
                bulkcopy.ColumnMappings.Add("sizeName", "sizeName");
                bulkcopy.ColumnMappings.Add("sizeAnother", "sizeAnother");
                bulkcopy.ColumnMappings.Add("sizeQty", "sizeQty");
                bulkcopy.ColumnMappings.Add("poQty", "poQty");
                bulkcopy.ColumnMappings.Add("org", "org");
                bulkcopy.ColumnMappings.Add("isCancel", "isCancel");
                bulkcopy.ColumnMappings.Add("overflow", "overflow");

                try
                {
                    bulkcopy.WriteToServer(spSize_temp);
                    return "200";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }


        public string SqlBulkToSQL_T_Booking_temp(DataTable changedShippingBookingStatusDT)
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(SPSqlconnStr))
            {
                bulkcopy.BulkCopyTimeout = 0; 
                bulkcopy.DestinationTableName = "T_Booking_temp";
                bulkcopy.ColumnMappings.Add("id", "id");
                bulkcopy.ColumnMappings.Add("BookingStatus", "BookingStatus");
                bulkcopy.ColumnMappings.Add("BookingData", "BookingData"); 

                try
                {
                    bulkcopy.WriteToServer(changedShippingBookingStatusDT);
                    return "200";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }
    }
}