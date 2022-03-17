namespace MODEL
{
    public class ShippingPackages
    {
        public int id { set; get; }
        public string isCancel { set; get; } //取消PO
        public string type { set; get; } //类型
        public string ftyNo { set; get; } //自编单号
        public string season { set; get; } //季节
        public string BVPO { set; get; }//BV验货PO
        public string masterPO { set; get; } //MasterPO
        public string GtnPO { set; get; } //GTNPO
        public string Modify { set; get; } //修改PO 旧的PO号 需要删除的
        public string po_mainLine { set; get; } //Line
        public string styleNumber { set; get; } //款号
        public string styleName { set; get; } //款式描述
        public string color { set; get; } //颜色
        public string colDescription { set; get; } //电脑标缩写
        public string channel { set; get; }//出货地
        public string totalQty { set; get; } //数量总和
        public string overflow { set; get; } //溢出数量
        public string HOD { set; get; } //出货日
        public string befoeHOD { set; get; } //船务提前出货
        public string newHOD { set; get; } //报延新交期
        public string shipMode { set; get; }//出货方式
        public string sourceTag { set; get; } //防盗扣
        public string wwwt { set; get; } //WWMT吊卡
        public string citHangTag { set; get; } //CIT吊卡
        public string Fastener { set; get; } //子弹
        public string steelNumber { set; get; }//钢板号
        public string cup { set; get; }//加罩杯
        public string cclable { set; get; }//洗标
        public string sensitive { set; get; } //敏感色
        public string remark { set; get; } //备注
        public string DeliveryDate { set; get; }
        public string org { set; get; } //厂区
    }

    public class ShippingParameter
    {
        public string org { set; get; } //厂区
        public bool checkedDate { set; get; } //ByShipDate
        public string startDate { set; get; } //开始时间
        public string stopDate { set; get; }//结束时间
        public bool bookingStatus { set; get; } //Booking 状态
        public string gtnPo { set; get; } //GTNPO
        public string styleNumber { set; get; } //款式
    }

    public class ShippingBookingStatus
    {
        public int id { set; get; } //厂区
        public bool BookingStatus { set; get; } //ByShipDate
        public string BookingData { set; get; } //booking 日期
    }

    public class ShippingPackageSizes
    {
        public int id { set; get; } //ID
        public string ftyNO { set; get; } //自编单号
        public string isCancel { set; get; } // 取消PO
        public string season { set; get; } //季节
        public string masterPO { set; get; } //Master PO#
        public string gtnPO { set; get; } //GtnPO
        public string Modify { set; get; } // 修改PO 旧的PO号 需要删除的
        public string po_MainLine { set; get; } //po_mainLine
        public string styleNumber { set; get; } //款式
        public string color { set; get; } //颜色
        public string sizeName { set; get; } //尺码名称
        public string sizeAnother { set; get; } //尺码别称       
        public string sizeQty { set; get; } //单尺码订单数量
        public string poQty { set; get; } //PO 订单数量
        public string overflow { set; get; } //溢出数量
        public string org { set; get; } //厂区
    }

    public class Spks
    {
        public ShippingPackages[] sppack { set; get; }
        public ShippingPackageSizes[] spsize { set; get; }

    }

    public class ShippingPackageStatus
    {
        public int id { set; get; }
        public string isCancel { set; get; } // 取消PO
        public string type { set; get; } //类型
        public string ftyNo { set; get; } //自编单号
        public string season { set; get; } //季节

        public string masterPO { set; get; } //MasterPO
        public string GtnPO { set; get; } //GTNPO
        public string Modify { set; get; } //修改PO 旧的PO号 需要删除的
        public string po_mainLine { set; get; } //Line
        public string styleNumber { set; get; } //款号
        public string color { set; get; } //颜色
        public string HOD { set; get; } //出货日
        public string bookingStatus { set; get; } //booking状态
        public string BookingData { set; get; } //booking 日期
        public string totalQty { set; get; } //出货单数量总和   PO 件数
        public string overflow { set; get; } //出货单溢出数量
        public int POqty { set; get; } // 装箱单 PO 件数 包括溢出数量
        public int POboxs { set; get; } // PO 箱数
        public int InQty { set; get; } // 已入库 件数
        public int InBoxs { set; get; } //已入库 箱数
        public string con_no { set; get; } //未入库 箱号

        public string BVPO { set; get; }//BV验货PO
        public string styleName { set; get; } //款式描述
        public string colDescription { set; get; } //电脑标缩写
        public string channel { set; get; }//出货地
        public string befoeHOD { set; get; } //船务提前出货
        public string newHOD { set; get; } //报延新交期
        public string shipMode { set; get; }//出货方式
        public string sourceTag { set; get; } //防盗扣
        public string wwwt { set; get; } //WWMT吊卡
        public string citHangTag { set; get; } //CIT吊卡
        public string Fastener { set; get; } //子弹
        public string steelNumber { set; get; }//钢板号
        public string cup { set; get; }//加罩杯
        public string cclable { set; get; }//洗标
        public string sensitive { set; get; } //敏感色
        public string remark { set; get; } //备注 
        public string org { set; get; } //厂区

       
    }

    public class ShippingPackageStatusParameter
    {
        public string GtnPO { set; get; } //GTNPO
        public string po_mainLine { set; get; } //Line
        public string styleNumber { set; get; } //款号 
        public string color { set; get; } //颜色 
    }


}