using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
   public  class groupCloNames
    { 
        public string od_no { set; get; } // 客户订单号
        public string in_date { set; get; } // 订单日期
        public string my_no { set; get; } // 自编单号
        public string season_id { set; get; } // 季节号
        public string release_who { set; get; } // 审核员 
        public string style_id { set; get; } // 成衣款号
        public string clr_no { set; get; } // 成衣色组号         
        public string style_name { set; get; } // 款式名称
        public string sex_name { set; get; } // 款式性别
        public string sample_no { set; get; } // 马克版本号 
        public string w_id { set; get; } // 生产厂区
    }

    public class groupPONumber
    {
       
        public string po_no { set; get; } // 原始订单PO号
        
    }

    public class groupSizeNmae
    {

        public string SizeName01 { set; get; } // 原始订单PO号
        public string SizeName02 { set; get; } // 原始订单PO号
        public string SizeName03 { set; get; } // 原始订单PO号
        public string SizeName04 { set; get; } // 原始订单PO号
        public string SizeName05 { set; get; } // 原始订单PO号
        public string SizeName06 { set; get; } // 原始订单PO号
        public string SizeName07 { set; get; } // 原始订单PO号
        public string SizeName08 { set; get; } // 原始订单PO号
        public string SizeName09 { set; get; } // 原始订单PO号
        public string SizeName10 { set; get; } // 原始订单PO号
        public string SizeName11 { set; get; } // 原始订单PO号
        public string SizeName12 { set; get; } // 原始订单PO号

    }


    public class materialSize
    {
         

        public string od_no { set; get; } // 客户订单号
        public string style_id { set; get; } // 款式
        public string mas_id { set; get; } // 物料ID
        public string color_no { set; get; } // 物料颜色
        public string size { set; get; } // 物料Size
        

    }

    /*
    public class materials
    {
        public string be_id { set; get; }//生产厂区
        public string my_no { set; get; }//自编单号
        public string od_no { set; get; } // BEST订单号码
        public string style_id { set; get; }// 款号
        public string clr_no { set; get; }//成衣色组号
        public string mas_sortNumber { set; get; }//物料分群码
        public string mas_sortName { set; get; }//物料分群名称
        public string mas_id { set; get; }//物料ID
        public string mas_name { set; get; }//物料名称
        public string color_no { set; get; }//物料颜色
        public string color_name2 { set; get; }// 颜色中文名称
        public string size { set; get; }//物料尺码
        public string unit_qty { set; get; }//单位用量        
        public string pu_qty { set; get; }//采购数量
        public string style_mas_qty { set; get; }//款式总用量
        public string unit_id { set; get; }//物料单位
        public string trans_rate { set; get; }//单位转换比率
        public string mas_qty { set; get; }//应发料量
        public string unit_id_p { set; get; }//发料单位
        public string pu_no { set; get; }//采购单号
        public string vend_id { set; get; }//供应商编码
        public string vend_abbr { set; get; }//供应商简称
        public string per_id { set; get; }//审核人员？
    }
    */
    public class materials
    {
        public int? id { set; get; }//本表主ID
        //--------------------------------------订单基本信息-------------------------------------------------
        public string be_id { set; get; }//生产厂区
        public string my_no { set; get; }//自编单号      
        public string style_id { set; get; }// 款号
        public string od_no { set; get; } // BEST订单号码


        //--------------------------------------订单数量信息-------------------------------------------------
        public int? od_qty { set; get; } // 订单数量
      //  public int? od_Finished_qty { set; get; } // 已生产数量
      //  public int? od_Unfinished_qty { set; get; } // 剩余数量
      //  public int? od_make_qty { set; get; } // 本批生产数量
      //  public int? od_Exceed_qty { set; get; } // 溢出数量


        //-----------------------------------------订单类型基本信息----------------------------------------------
        public string season_id { set; get; } // 季节号
        public string style_name { set; get; } // 款式名称
        public string sex_name { set; get; } // 款式性别
        public string sample_no { set; get; } // 马克版本号 
        public string in_date { set; get; } // 订单日期 
        public string release_who { set; get; } // 审核员    


        //----------------------------------------订单物料基本信息-----------------------------------------------
        public string clr_no { set; get; }//成衣色组号
        public string mas_sortNumber { set; get; }//物料分群码
        public string mas_sortName { set; get; }//物料分群名称
        public string item_id { set; get; }  //分群码     
        public string item_name { set; get; }  //分群码名称  
        public string mas_id { set; get; }//物料ID
        public string mas_name { set; get; }//物料名称
        public string color_no { set; get; }//物料颜色
        public string color_name2 { set; get; }// 颜色中文名称
        public string color_name { set; get; } //物料颜色英文         
        public string mas_type { set; get; }  //物料类别  


        //---------------------------------------订单数量信息------------------------------------------------
        public string size { set; get; }//物料尺码 
        public double? loss { set; get; }//预补数量        
        public double? unit_qty { set; get; }//单位用量        
        public double? pu_qty { set; get; }//采购数量
        public double? style_mas_qty { set; get; }//款式总用量
        public string unit_id { set; get; }//物料单位
        public double? trans_rate { set; get; }//单位转换比率


        //---------------------------------------订单发料信息------------------------------------------------
        public double? pu_trans_qty { set; get; } //发料单位采购数量
        public double? masFinished_qty { set; get; } //累计已发数量
        public double? masUnfinished_qty { set; get; } //剩余数量
        public double? mas_qty { set; get; } //本次发料量  
        public double? masExceed_qty { set; get; } //超发料量


        //------------------------------------------物料供应商信息---------------------------------------------
        public string unit_id_p { set; get; }//发料单位
        public string pu_no { set; get; }//采购单号
        public string vend_id { set; get; }//供应商编码
        public string vend_abbr { set; get; }//供应商简称
        public string per_id { set; get; }//审核人员？


        //-------------------------------------------单据信息--------------------------------------------       
        public string createPerson { set; get; }  // 创建单据人员        
        public string createDate { set; get; } // 创建单据日期时间         
        public string changePerson { set; get; }  // 单据最后修改人员        
        public string changeDate { set; get; }  // 单据最后修改日期时间       
        public string receiveNumber { set; get; }  // 领料单号 DA(领料)+03(辅料)+年度(2码)+第几星期(2码)+ 4码流水号       
        public string receiveNumberBatch { set; get; }  // 发料批号  3码流水      
        public string materialStatus { set; get; }   // 物料发放状态  O完成  N未发料  P发了一部分  进行中        
        public int? printTimes { set; get; }// 打印次数
        public string note  { set; get; }  //备注
    }

    public class materialhs
    {
        public int? id { set; get; }//本表主ID
                                    //--------------------------------------订单基本信息-------------------------------------------------
        public int a_id { set; get; }//本表主ID
        public string be_id { set; get; }//生产厂区
        public string my_no { set; get; }//自编单号      
        public string style_id { set; get; }// 款号
        public string od_no { set; get; } // BEST订单号码


        //--------------------------------------订单数量信息-------------------------------------------------
        public int? od_qty { set; get; } // 订单数量
                                         //  public int? od_Finished_qty { set; get; } // 已生产数量
                                         //  public int? od_Unfinished_qty { set; get; } // 剩余数量
                                         //  public int? od_make_qty { set; get; } // 本批生产数量
                                         //  public int? od_Exceed_qty { set; get; } // 溢出数量


        //-----------------------------------------订单类型基本信息----------------------------------------------
        public string season_id { set; get; } // 季节号
        public string style_name { set; get; } // 款式名称
        public string sex_name { set; get; } // 款式性别
        public string sample_no { set; get; } // 马克版本号 
        public string in_date { set; get; } // 订单日期 
        public string release_who { set; get; } // 审核员    


        //----------------------------------------订单物料基本信息-----------------------------------------------
        public string clr_no { set; get; }//成衣色组号
        public string mas_sortNumber { set; get; }//物料分群码
        public string mas_sortName { set; get; }//物料分群名称
        public string item_id { set; get; }  //分群码     
        public string item_name { set; get; }  //分群码名称  
        public string mas_id { set; get; }//物料ID
        public string mas_name { set; get; }//物料名称
        public string color_no { set; get; }//物料颜色
        public string color_name2 { set; get; }// 颜色中文名称
        public string color_name { set; get; } //物料颜色英文         
        public string mas_type { set; get; }  //物料类别  


        //---------------------------------------订单数量信息------------------------------------------------
        public string size { set; get; }//物料尺码 
        public double? loss { set; get; }//预补数量        
        public double? unit_qty { set; get; }//单位用量        
        public double? pu_qty { set; get; }//采购数量
        public double? style_mas_qty { set; get; }//款式总用量
        public string unit_id { set; get; }//物料单位
        public double? trans_rate { set; get; }//单位转换比率


        //---------------------------------------订单发料信息------------------------------------------------
        public double? pu_trans_qty { set; get; } //发料单位采购数量
        public double? masFinished_qty { set; get; } //累计已发数量
        public double? masUnfinished_qty { set; get; } //剩余数量
        public double? mas_qty { set; get; } //本次发料量  
        public double? masExceed_qty { set; get; } //超发料量


        //------------------------------------------物料供应商信息---------------------------------------------
        public string unit_id_p { set; get; }//发料单位
        public string pu_no { set; get; }//采购单号
        public string vend_id { set; get; }//供应商编码
        public string vend_abbr { set; get; }//供应商简称
        public string per_id { set; get; }//审核人员？


        //-------------------------------------------单据信息--------------------------------------------       
        public string createPerson { set; get; }  // 创建单据人员        
        public string createDate { set; get; } // 创建单据日期时间         
        public string changePerson { set; get; }  // 单据最后修改人员        
        public string changeDate { set; get; }  // 单据最后修改日期时间       
        public string receiveNumber { set; get; }  // 领料单号 DA(领料)+03(辅料)+年度(2码)+第几星期(2码)+ 4码流水号       
        public string receiveNumberBatch { set; get; }  // 发料批号  3码流水      
        public string materialStatus { set; get; }   // 物料发放状态  O完成  N未发料  P发了一部分  进行中        
        public int? printTimes { set; get; }// 打印次数
        public string note { set; get; }  //备注
    }


    public class GroupColor
    { 
        public string od_no { set; get; } // BEST订单号码
        public string style_id { set; get; }// 款号 
        public string mas_id { set; get; }//物料ID     
        public string mas_name { set; get; }//物料名称      
        public string text2 { set; get; }//2号色组的物料的颜色
        public string text3 { set; get; }//3号色组的物料的颜色
        public string text4 { set; get; }//4号色组的物料的颜色       
        public string text5 { set; get; }//5号色组的物料的颜色
        public string text6 { set; get; }//6号色组的物料的颜色       
        public string text7 { set; get; }//7号色组的物料的颜色
        public string text8 { set; get; }//8号色组的物料的颜色
        public string text9 { set; get; }//9号色组的物料的颜色
        public string text10 { set; get; }//10号色组的物料的颜色
        public string text11{ set; get; }//11号色组的物料的颜色
        public string mas_type { set; get; }//物料类别
    }

    public class odb_pur
    {
        public string od_no { set; get; } // BEST订单号码
        public string style_id { set; get; } // 款号 
        public string mas_id { set; get; } //物料ID     
        public string color_no { set; get; } //物料颜色代码      
        public string color_name { set; get; } //物料颜色英文  
        public string color_name2 { set; get; } //物料颜色中文
        public string size { set; get; } //物料SIZE     
        public string pu_no { set; get; } //采购单号
        public string vend_id { set; get; } //供应商代码       
        public string vend_abbr { set; get; } //供应商名称     
        public string mas_name { set; get; } //物料名称     
        public string mas_type { set; get; }  //物料类别     
        public string item_id { set; get; }  //分群码     
        public string item_name { set; get; }  //分群码名称   

        public double? unit_qty { set; get; }  // 单位用量     
        public double? loss { set; get; }  //预补数量     
        public double? od_qty { set; get; }  // 订购数量     
        public double? qty { set; get; }  //采购数量      -- 发料数量
        public double? trans_rate { set; get; }  // 转换比率    
    }

    public class punos
    {
        public string mas_id { set; get; } //物料ID     
        public string color_no { set; get; } //物料颜色代码      
        public string pu_no { set; get; } //采购单号     

    }

    public class allqtys
    {
      
        public string od_no { set; get; } // BEST订单号码
        public string style_id { set; get; } // 款号 
        public string clr_no { set; get; } //色组号码     
        public int? qty01 { set; get; } //1号SIZE的数量   
        public int? qty02 { set; get; } //2号SIZE的数量
        public int? qty03 { set; get; } //3号SIZE的数量
        public int? qty04 { set; get; } //4号SIZE的数量
        public int? qty05 { set; get; } //5号SIZE的数量
        public int? qty06 { set; get; } //6号SIZE的数量
        public int? qty07 { set; get; } //7号SIZE的数量
        public int? qty08 { set; get; } //8号SIZE的数量
        public int? qty09 { set; get; } //9号SIZE的数量
        public int? qty10 { set; get; } //10号SIZE的数量
        public int? qty11 { set; get; } //11号SIZE的数量
        public int? qty12 { set; get; } //12号SIZE的数量
        public int? qty { set; get; } //一个  area_id, PO_NO的数量
        public string wip { set; get; } // 出口洲？
        public string dm { set; get; } // 出口代码？
        public string area_id { set; get; } // 港口代码？
        public string po_no { set; get; } // PO号码
        public string id { set; get; } // PO交期
        public string mark { set; get; } // 标备注
        public string od_type { set; get; } // 订单类型
         
       
    }
}
