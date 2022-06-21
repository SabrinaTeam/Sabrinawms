using MODEL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public  class LuluSingleScanServer
    {
        public List<LuluSingleScanPacklist> GetCartonBarcode(string CartonBarcode)
        {
            string sql = @"SELECT
	                            d.Cust_id,
	                            d.Serial_From,
	                            d.Buyer_Item,
	                            d.color_code,
	                            d.Size1,
	                            d.qty,
	                            p.org,
	                            p.country_code,
	                            p.con_no,
	                            p.Net_Net,
	                            p.con_net,
	                            p.con_Gross,
	                            p.con_L,
	                            p.con_W,
	                            p.con_H,
	                            p.b_Volume as  Bvolume,
	                            p.PO,
	                            p.MAIN_LINE,
	                            r.SKU,
	                            r.ColorName,
	                            r.Seanson
                            FROM
	                            con_detail d
	                            LEFT JOIN con_ppr p ON d.Serial_From = p.Serial_From 
	                            LEFT JOIN rfidtagcolor c on c.ColorCode+0 = d.color_code +0
	                            LEFT JOIN  rfidtag r on r.PONumber = p.PO and r.SizeName = d.Size1  and r.ColorName = c.ColorName	                             
                            WHERE d.Serial_From =@CartonBarcode

                                GROUP BY    d.Cust_id,
	                            d.Serial_From,
	                            d.Buyer_Item,
	                            d.color_code,
	                            d.Size1,
	                            d.qty,
	                            p.org,
	                            p.country_code,
	                            p.con_no,
	                            p.Net_Net,
	                            p.con_net,
	                            p.con_Gross,
	                            p.con_L,
	                            p.con_W,
	                            p.con_H,
	                            p.b_Volume  ,
	                            p.PO,
	                            p.MAIN_LINE,
	                            r.SKU,
	                            r.ColorName
                            ;";

            MySqlParameter[] ps = {
                               new MySqlParameter("@CartonBarcode",CartonBarcode)
                                };

            DataTable dt = Mysql_SqlHelper.ExcuteTable(sql, ps);

            List<LuluSingleScanPacklist> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<LuluSingleScanPacklist>();
                foreach (DataRow row in dt.Rows)
                {
                    LuluSingleScanPacklist c = new LuluSingleScanPacklist();
                    LoadLuluSingleScanToList(row, c);
                    lists.Add(c);
                }
            }
            return lists;
        }

        public void LoadLuluSingleScanToList(DataRow dr, LuluSingleScanPacklist packlist)
        {
            packlist.Cust_id = Convert.ToString( Mysql_SqlHelper.FromDbValue(dr["Cust_id"]));
            packlist.Serial_From = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Serial_From"]));
            packlist.Buyer_Item = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Buyer_Item"]));
            packlist.Color_code = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Color_code"]));
            packlist.Size1 = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Size1"]));
            packlist.Qty = Convert.ToInt32( Mysql_SqlHelper.FromDbValue(dr["Qty"]));
            packlist.Org = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Org"]));
            packlist.Country_code = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Country_code"])); //出口地
            packlist.Con_no = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Con_no"]));

            packlist.Net_Net = Convert.ToDouble(Mysql_SqlHelper.FromDbValue(dr["Net_Net"]));
            packlist.Con_net = Convert.ToDouble(Mysql_SqlHelper.FromDbValue(dr["Con_net"]));
            packlist.Con_Gross = Convert.ToDouble(Mysql_SqlHelper.FromDbValue(dr["Con_Gross"]));
            packlist.Con_L = Convert.ToDouble(Mysql_SqlHelper.FromDbValue(dr["Con_L"]));
            packlist.Con_W = Convert.ToDouble(Mysql_SqlHelper.FromDbValue(dr["Con_W"]));
            packlist.Con_H = Convert.ToDouble(Mysql_SqlHelper.FromDbValue(dr["Con_H"]));

            packlist.Bvolume = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Bvolume"]));
            packlist.Po = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Po"]));
            packlist.Main_Line = Convert.ToInt32(Mysql_SqlHelper.FromDbValue(dr["MAIN_LINE"]));

            packlist.SKU = Convert.ToInt32(Mysql_SqlHelper.FromDbValue(dr["SKU"]));
            packlist.ColorName = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["ColorName"]));
            packlist.Seanson = Convert.ToString(Mysql_SqlHelper.FromDbValue(dr["Seanson"]));



        }


        public int saveScanLog(DataTable saveScanLog)
        {
            if(saveScanLog.Rows.Count <= 0)
            {
                return 0;
            }
            string value = "";
            for (int i = 0; i < saveScanLog.Rows.Count; i++)
            {
                value = value + "( '" + saveScanLog.Rows[i]["CustID"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["CartonNumber"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["PolyBagNumber"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["RFIDNumber"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["WWMTNumber"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Buyer_item"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Color_code"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Size1"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Qty"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["Org"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["PO"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["ScanTime"] + "' ,"
                               + " '" + saveScanLog.Rows[i]["ScanHost"] + "' ),";
            }
            value = value.Substring(0, value.Length - 1);
            string sql = @" insert into rfidscanlogs (CustID , CartonNumber, PolyBagNumber, RFIDNumber, WWMTNumber, Buyer_item, Color_code, Size1,
                              Qty, Org, PO, ScanTime, ScanHost)  values   " + value + ";";

            int insertRows = Mysql_SqlHelper.ExecuteNonQuery(sql);

            return insertRows;

        }


    }
}
