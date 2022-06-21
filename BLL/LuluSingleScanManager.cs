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
    public  class LuluSingleScanManager
    {
        public LuluSingleScanServer lscs = new LuluSingleScanServer();
        public List<LuluSingleScanPacklist> GetCartonBarcode(string cartonBarcode)
        {
            return lscs.GetCartonBarcode(cartonBarcode);
        }
        public DataTable ScanLogDB()
        {
            DataTable ScanLogDB = new DataTable();
            DataColumn dc;

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "CustID";
            dc.Caption = "CustID";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "CartonNumber";
            dc.Caption = "CartonNumber";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "PolyBagNumber";
            dc.Caption = "PolyBagNumber";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "RFIDNumber";
            dc.Caption = "RFIDNumber";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "WWMTNumber";
            dc.Caption = "WWMTNumber";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Buyer_item";
            dc.Caption = "Buyer_item";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Color_code";
            dc.Caption = "Color_code";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Size1";
            dc.Caption = "Size";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Qty";
            dc.Caption = "Qty";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Org";
            dc.Caption = "Org";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "PO";
            dc.Caption = "PO";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "ScanTime";
            dc.Caption = "ScanTime";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "ScanHost";
            dc.Caption = "ScanHost";
            dc.ReadOnly = true;
            dc.Unique = false;
            ScanLogDB.Columns.Add(dc);



            return ScanLogDB;
        }

        public DataTable RFIDNUMBERS()
        {
            DataTable RFIDNUMBERS = new DataTable();
            DataColumn dc;

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "RFIDNumber";
            dc.Caption = "RFIDNumber";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDNUMBERS.Columns.Add(dc);
            return RFIDNUMBERS;
        }


        public DataTable getRFIDBoxs()
        {
            DataTable RFIDBoxs = new DataTable();
            DataColumn dc;

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "CustID";
            dc.Caption = "CustID";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDBoxs.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "CartonNumber";
            dc.Caption = "CartonNumber";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDBoxs.Columns.Add(dc);



            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Buyer_item";
            dc.Caption = "Buyer_item";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDBoxs.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Color_code";
            dc.Caption = "Color_code";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDBoxs.Columns.Add(dc);



            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Qty";
            dc.Caption = "Qty";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDBoxs.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Org";
            dc.Caption = "Org";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDBoxs.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "PO";
            dc.Caption = "PO";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDBoxs.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "ScanTime";
            dc.Caption = "ScanTime";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDBoxs.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "ScanHost";
            dc.Caption = "ScanHost";
            dc.ReadOnly = true;
            dc.Unique = false;
            RFIDBoxs.Columns.Add(dc);


            return RFIDBoxs;
        }

        //    二进制是Binary，简写为B；八进制是Octal，简写为O；十进制为Decimal，简写为D；十六进制为Hexadecimal，简写为H。

        /// <summary>
        /// //16转2方法
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public  string HexString2BinString(string hexString)
        {


            try
            {
                string result = string.Empty;
                foreach (char c in hexString)
                {
                    int v = Convert.ToInt32(c.ToString(), 16);
                    int v2 = int.Parse(Convert.ToString(v, 2));
                    // 去掉格式串中的空格，即可去掉每个4位二进制数之间的空格，
                    result += string.Format("{0:d4} ", v2);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }



        public bool IsHexadecimal(string str)
        {
            const string PATTERN = @"[A-Fa-f0-9]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
        }


        /// <summary>
        /// 将二进制转成字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public  string BinaryToHex(string s)
        {
            System.Text.RegularExpressions.CaptureCollection cs =
                System.Text.RegularExpressions.Regex.Match(s, @"([01]{8})+").Groups[1].Captures;
            byte[] data = new byte[cs.Count];
            for (int i = 0; i < cs.Count; i++)
            {
                data[i] = Convert.ToByte(cs[i].Value, 2);
            }
            return Encoding.Unicode.GetString(data, 0, data.Length);
        }


        /*
        /// <summary>
        /// 解码过程接收16进制编码转换成16进制
        /// </summary>
        /// <param name="data"></param>
        public void AddData(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            if (data.Length > 9)
            {
                for (int i = 0; i < 9; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", data[i]);
                }
                AddContent(sb.ToString());
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", data[i]);
                }
                AddContent(sb.ToString());
            }
        }
        */

        public bool checkRfids(List<string> list ,string tag)
        {
            bool result = false;

            if(list == null ||  list.Count == 0)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                   if(list[i] == tag)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public DataTable getNewCartonPackModel()
        {
            DataTable dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn();
            dc.ColumnName = "Cust_id";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Serial_From";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Buyer_Item";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "color_code";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Size1";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "qty";
            dc.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "org";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "country_code";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_no";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Net_Net";
            dc.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_net";
            dc.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_Gross";
            dc.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_L";
            dc.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_W";
            dc.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_H";
            dc.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "b_Volume";
            dc.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "PO";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "MAIN_LINE";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "SKU";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "ColorName";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Seanson";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);

            return dt;
        }

        public int saveScanLog(DataTable dt)
        {
            return this.lscs.saveScanLog(dt);

        }


    }
}
