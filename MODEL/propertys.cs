using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class propertys
    {
        public int id { set; get; }  // 索引
        public string erpid { set; get; }  // ERP索引
        public string org { set; get; } // 厂别
        public string propertyID { set; get; } //财编
        public string propertyName { set; get; } //资产名称
        public string propertyMode { set; get; } //资产型号
        public string propertyType { set; get; } //固资分类
        public string buyDate { set; get; } //购入日期
        public string propertyDept { set; get; } //资产归属部门
        public string propertyLocal { set; get; } //资产存放位置
        public string propertyBuyID { set; get; } //资产采购单号
        public string propertySavePerson { set; get; } //资产保管人
        public string propertyUnit { set; get; } //资产单位
        public int propertyPrintTims { set; get; } //财编打印次数
        public string propertyPrintPC { set; get; } //资产建立者PC名
        public int propertyIsDel { set; get; } //删除标记
        public string propertyDelPC { set; get; } //删除者PC
        public string propertyDelDate { set; get; } //删除日期
        public string propertyDelNote { set; get; } //删除备注
         
    }
}
