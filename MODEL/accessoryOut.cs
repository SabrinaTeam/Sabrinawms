using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;

namespace MODEL
{
    public class accessoryOut
    {
        // ID
        public int id { set; get; }   
        // 厂区
        public string be_id { set; get; }
        // 自编单号
        public string my_no { set; get; }
        //款号
        public string style_id { set; get; }
        //订单号
        public string od_no { set; get; }
        // 订单数量
        public int? od_qty { set; get; }
        // 订单完成数量
        public int? od_Finished_qty { set; get; }
        // 订单待生产数量
        public int? od_Unfinished_qty { set; get; }
        //订单本次生产数量 
        public int? od_make_qty { set; get; }
         //订单生产溢出数量
        public int? od_Exceed_qty { set; get; }
        //  季节号 
        public string season_id { set; get; }
        // 款式名称
        public string style_name { set; get; }
        //款式性别
        public string sex_name { set; get; }
        //马克版号
        public string sample_no { set; get; }
        // 接单日期
        public string in_date { set; get; }
        // 接单人员
        public string release_who { set; get; }
        // 成品色组
        public string clr_no { set; get; }
        // 物料分群码
        public string mas_sortNumber { set; get; }
        // 物料分群名称
        public string mas_sortName { set; get; }
        // 物料分群码 --1  
        public string item_id { set; get; }
        // 物料分群名称 --1
        public string item_name { set; get; }
        // 物料ID
        public string mas_id { set; get; }
        // 物料名称
        public string mas_name { set; get; }
        // 物料色号
        public string color_no { set; get; }
        // 物料颜色中文名称
        public string color_name2 { set; get; }
        // 物料颜色英文名称
        public string color_name { set; get; }
        // 物料大类别
        public string mas_type { set; get; }
        // 物料尺码
        public string size { set; get; }
        // 预补数
        public string loss { set; get; }
        // 物料库存数量
        public string unit_qty { set; get; }
        // 物料采购数量
        public string pu_qty { set; get; }
        // 物料款式总用量
        public string style_mas_qty { set; get; }
        // 物料库存单位
        public string unit_id { set; get; }
        // 物料转换比率
        public string trans_rate { set; get; }
        // 物料转换采购量
        public string pu_trans_qty { set; get; }
        // 已发物料数量
        public string masFinished_qty { set; get; }
        // 待发物料数量
        public string masUnfinished_qty { set; get; }
        // 本次物料发放数量
        public string mas_qty { set; get; }
        // 超发物料数量
        public string masExceed_qty { set; get; }
        // 物料发放单位
        public string unit_id_p { set; get; }
        // 采购单号
        public string pu_no { set; get; }
        // 供应商代码
        public string vend_id { set; get; }
        // 供应商简称
        public string vend_abbr { set; get; }
        //  
        public string per_id { set; get; }



        // 开单人
        public string createPerson { set; get; }
        // 开单日期时间
        public string createDate { set; get; }
        // 开单人
        public string changePerson { set; get; }
        // 开单日期时间
        public string changeDate { set; get; }
        // 领料单号  日期月+料类别+6码流水
        public string receiveNumber { set; get; }
        // 发料批号  2码流水
        public string receiveNumberBatch { set; get; }
        // 物料发放状态  O完成  N未发料  P发了一部分  进行中
        public string materialStatus { set; get; }
        // 打印次数
        public int? printTimes { set; get; }
        //备注
        public string note { set; get; }



    }
}
