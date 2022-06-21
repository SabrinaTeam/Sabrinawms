using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public partial class LuluSingleScanPacklist
    {
        public LuluSingleScanPacklist()
        { }


        private string _Cust_id;
        private string _Serial_From;
        private string _Buyer_Item;
        private string _Color_code;
        private string _Size1;
        private int _Qty;
        private string _Org;
        private string _Country_code;
        private string _Con_no;
        private double _Net_Net;
        private double _Con_net;
        private double _Con_Gross;
        private double _Con_L;
        private double _Con_W;
        private double _Con_H;

        private string _Bvolume;
        private string _Po;
        private int? _Main_Line;

        private int _SKU;
        private string _ColorName;
        private string _Seanson;


        public string Cust_id
        {
            set { _Cust_id = value; }
            get { return _Cust_id; }
        }

        public string Serial_From
        {
            set { _Serial_From = value; }
            get { return _Serial_From; }
        }

        public string Buyer_Item
        {
            set { _Buyer_Item = value; }
            get { return _Buyer_Item; }
        }
        public string Color_code
        {
            set { _Color_code = value; }
            get { return _Color_code; }
        }
        public string Size1
        {
            set { _Size1 = value; }
            get { return _Size1; }
        }
        public int? Qty
        {
            set { _Qty = Convert.ToInt32(value); }
            get { return _Qty; }
        }
        public string Org
        {
            set { _Org = value; }
            get { return _Org; }
        }
        public string Country_code
        {
            set { _Country_code = value; }
            get { return _Country_code; }
        }
        public string Con_no
        {
            set { _Con_no = value; }
            get { return _Con_no; }
        }
        public double Net_Net
        {
            set { _Net_Net = Convert.ToDouble(value); }
            get { return _Net_Net; }
        }
        public double Con_net
        {
            set { _Con_net = Convert.ToDouble(value); }
            get { return _Con_net; }
        }
        public double Con_Gross
        {
            set { _Con_Gross = Convert.ToDouble(value); }
            get { return _Con_Gross; }
        }
        public double Con_L
        {
            set { _Con_L = Convert.ToDouble(value); }
            get { return _Con_L; }
        }
        public double Con_W
        {
            set { _Con_W = Convert.ToDouble(value); }
            get { return _Con_W; }
        }
        public double Con_H
        {
            set { _Con_H = Convert.ToDouble(value); }
            get { return _Con_H; }
        }
        public String Bvolume
        {
            set { _Bvolume =value; }
            get { return _Bvolume; }
        }
        public string Po
        {
            set { _Po = value; }
            get { return _Po; }
        }
        public int? Main_Line
        {
            set { _Main_Line = Convert.ToInt32(value); }
            get { return _Main_Line; }
        }

        public int? SKU
        {
            set { _SKU = Convert.ToInt32(value); }
            get { return _SKU; }
        }
        public string ColorName
        {
            set { _ColorName = value; }
            get { return _ColorName; }
        }
        public string Seanson
        {
            set { _Seanson = value; }
            get { return _Seanson; }
        }

    }
}
