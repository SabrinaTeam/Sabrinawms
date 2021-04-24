using MODEL;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DAL
{
    public class accessoryOutService
    {
        public static readonly string MiddleWare = ConfigurationManager.ConnectionStrings["EnableMiddleWare"].ConnectionString;

        public   List<accessoryOut> getAccessoryOutByParameters(List<parameter> parameter,string Org)
        {

            // select top 100 * from mas   where mas_id = 'VSC-56 NA9LH2'--分群

            // select top 100 * from item    --分群

            string sqlstr = "";
            if (Org == "SAA")
            { 
                sqlstr = @"SELECT
	                    b.sfb01 as manufacturingNumber ,
	                    b.sfb05  as Style,
                        O.TA_OEA01 as season,
						O.TA_OEA02 as monthBuy,
	                    b.sfb071,
	                    b.sfb08 as products ,
	                    b.sfb13 ,
	                    b.sfb22 as orderNumber ,
	                    b.sfb36 ,
	                    b.sfb38 ,
	                    b.sfb28 ,
	                    b.sfb223 as custName,
	                    b.TA_SFB01 as myNumber ,
	                    a.sfa03 as materialID ,
	                    AC.SFAC04 AS colorCode,   
	                    AC.SFAC06 AS Quantity,
	                    AC.SFAC12 AS unit,
                        a.sfa16  as standardQuantity,
	                    a.sfa161 as practicalQuantity,
	                    a.TA_SFA02 as bodyDescription ,
	                    i.ima02 as materialName ,
	                    i.ima021 as spec ,
	                    i.ima06  as sortNumber,
	                    z.imz02 as sortName ,
	                    a.sfa013 as bigType 
                    FROM saa.SFB_FILE   B
                    LEFT JOIN  saa.SFA_FILE A ON A.SFA01 =  B.SFB01
                    LEFT JOIN  saa.SFAC_FILE AC ON AC.SFAC01 =  B.SFB01  AND AC.SFAC03 =  A.SFA03
                    LEFT JOIN  saa.OEA_FILE O ON O.OEA01 = B.sfb22
                    LEFT JOIN  saa.ima_file  i ON i.IMA01 =  a.SFA03
                    LEFT JOIN  saa.imz_file z on i.ima06 = z.imz01 ";
            }
            if( Org == "TOP")
            {
                sqlstr = @"SELECT
	                    b.sfb01 as manufacturingNumber ,
	                    b.sfb05  as Style,
                        O.TA_OEA01 as season,
						O.TA_OEA02 as monthBuy,
	                    b.sfb071,
	                    b.sfb08 as products ,
	                    b.sfb13 ,
	                    b.sfb22 as orderNumber ,
	                    b.sfb36 ,
	                    b.sfb38 ,
	                    b.sfb28 ,
	                    b.sfb223 as custName,
	                    b.TA_SFB01 as myNumber ,
	                    a.sfa03 as materialID ,
	                    AC.SFAC04 AS colorCode,   
	                    AC.SFAC06 AS Quantity,
	                    AC.SFAC12 AS unit,
                        a.sfa16  as standardQuantity,
	                    a.sfa161 as practicalQuantity,
	                    a.TA_SFA02 as bodyDescription ,
	                    i.ima02 as materialName ,
	                    i.ima021 as spec ,
	                    i.ima06  as sortNumber,
	                    z.imz02 as sortName ,
	                    a.sfa013 as bigType 
                    FROM top.SFB_FILE   B
                    LEFT JOIN  top.SFA_FILE A ON A.SFA01 =  B.SFB01
                    LEFT JOIN  top.SFAC_FILE AC ON AC.SFAC01 =  B.SFB01  AND AC.SFAC03 =  A.SFA03
                    LEFT JOIN  top.OEA_FILE O ON O.OEA01 = B.sfb22
                    LEFT JOIN  top.ima_file  i ON i.IMA01 =  a.SFA03
                    LEFT JOIN  top.imz_file z on i.ima06 = z.imz01 ";
            }
            string sqlwhere = "";
            if (parameter.Count > 0)
            {
                for( int i =0; i< parameter.Count; i++)
                {
                    if (parameter[i].pkey == "myNumber")
                    {
                        sqlwhere = sqlwhere + " B.TA_SFB01 =  '"+ parameter[i].pvalue.ToString() +"' and ";
                    }
                    if (parameter[i].pkey == "Style")
                    {
                        sqlwhere = sqlwhere + " B.SFB05 =  '" + parameter[i].pvalue.ToString() + "' and ";
                    }
                    if (parameter[i].pkey == "purNumber")
                    {
                        sqlwhere = sqlwhere + " B.SFB05 =  '" + parameter[i].pvalue.ToString() + "'  and ";
                    }
                    if (parameter[i].pkey == "receiveNumber")
                    {
                        sqlwhere = sqlwhere + " B.SFB05 =  '" + parameter[i].pvalue.ToString() + "'  and ";
                    }

                }
                sqlwhere = "  where  " + sqlwhere + " a.sfa013 ='55' and  1 = 1 ";


            }
            sqlstr = sqlstr + sqlwhere;

            /*
        OracleParameter[] ps = {
                      new OracleParameter("pName",parameter[0].pkey.ToString()),
                      new OracleParameter("pValue",parameter[0].pvalue.ToString())
                                };
             DataTable dt = ERP_SqlHelper.ExcuteTable(sqlstr, ps);
            */


            DataTable dt = new DataTable();
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            } 
            List<MODEL.accessoryOut> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.accessoryOut>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.accessoryOut c = new MODEL.accessoryOut();
                    accessoryOut(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }
        public void accessoryOut(DataRow dr, MODEL.accessoryOut list)
        {       

            list.id = Convert.ToInt32( ERP_SqlHelper.FromDbValue(dr["id"]));  //ID 
            list.be_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["be_id"])); // 厂区
            list.my_no = Convert.ToString( ERP_SqlHelper.FromDbValue(dr["my_no"])); // 自编单号
            list.style_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_id"]));   // 款号
            list.od_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_no"]));  // 订单号 
            list.od_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_qty"])); // 订单数量
            list.od_Finished_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_Finished_qty"])); // 订单完成数量
            list.od_Unfinished_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_Unfinished_qty"]));  // 订单待生产数量           
            list.od_make_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_make_qty"]));   // 订单本次生产数量 
            list.od_Exceed_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_Exceed_qty"]));  //  订单生产溢出数量
            list.season_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["season_id"]));   // 季节号
            list.style_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_name"]));  // 款式名称
            list.sex_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["sex_name"]));   // 款式性别
            
            list.sample_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["sample_no"]));  //马克版号 
            list.in_date = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["in_date"])); // 接单日期
            list.release_who = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["release_who"])); // 接单人员
            list.clr_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["clr_no"]));   // 成品色组
            list.mas_sortNumber = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_sortNumber"]));  // 物料分群码 
            list.mas_sortName = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_sortName"])); // 物料分群名称
            list.item_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["item_id"])); // 物料分群码--1
            list.item_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["item_name"]));  // 物料分群名称--1           
            list.mas_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_id"]));   // 物料ID  
            
            list.mas_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_name"]));  //物料名称 
            list.color_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_no"])); // 物料色号
            list.color_name2 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_name2"])); // 物料颜色中文名称
            list.color_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_name"]));   // 物料颜色英文名称            
            list.mas_type = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_type"])); // 物料大类别
            list.size = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["size"])); // 物料尺码
            list.loss = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["loss"]));  // 预补数         
            list.unit_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_qty"]));   // 物料库存数量  
            list.pu_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["pu_qty"]));   // 物料采购数量  
            list.style_mas_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_mas_qty"]));  // 物料款式总用量 
            
            list.unit_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_id"]));  //物料库存单位 
            list.trans_rate = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["trans_rate"])); // 物料转换比率
            list.pu_trans_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["pu_trans_qty"])); // 物料转换采购量
            list.masFinished_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["masFinished_qty"]));   // 已发物料数量            
            list.masUnfinished_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["masUnfinished_qty"])); // 待发物料数量
            list.mas_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_qty"])); // 本次物料发放数量
            list.masExceed_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["masExceed_qty"]));  // 超发物料数量         
            list.unit_id_p = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_id_p"]));   // 物料发放单位  
            list.pu_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["pu_no"]));   // 采购单号  
            list.vend_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["vend_id"]));  // 供应商代码 
            list.vend_abbr = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["vend_abbr"]));  // 供应商简称 
            list.per_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["per_id"]));  //
            
            list.createPerson = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["createPerson"]));  //开单人 
            list.createDate = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["createDate"])); // 开单日期时间
            list.changePerson = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["changePerson"])); // 最后修改单人
            list.changeDate = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["changeDate"]));   // 最后修改日期时间            
            list.receiveNumber = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["receiveNumber"])); // 领料单号
            list.receiveNumberBatch = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["receiveNumberBatch"])); // 发料批号
            list.materialStatus = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["materialStatus"]));  // 物料发放状态         
            list.printTimes = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["printTimes"]));   // 打印次数  
            list.note = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["note"]));   // 备注  


        }

        public int writeAccessoryToDB (List<materials> accessorytb)
        {
            string sqlstr = "";
            string sqlValue = "";
            for (int i = 0; i < accessorytb.Count; i++)
            {

                sqlValue = sqlValue +
                           "(\"" + accessorytb[i].be_id + "\",\""
                               + accessorytb[i].my_no + "\",\""
                               + accessorytb[i].style_id + "\",\""
                               + accessorytb[i].od_no + "\",\""
                               + accessorytb[i].od_qty + "\",\""
                               + accessorytb[i].season_id + "\",\""
                               + accessorytb[i].style_name + "\",\""

                               + accessorytb[i].sex_name + "\",\""
                               + accessorytb[i].sample_no + "\",\""
                               + accessorytb[i].in_date + "\",\""
                               + accessorytb[i].release_who + "\",\""
                               + accessorytb[i].clr_no + "\",\""
                               + accessorytb[i].mas_sortNumber + "\",\""
                               + accessorytb[i].mas_sortName + "\",\""

                               + accessorytb[i].item_id + "\",\""
                               + accessorytb[i].item_name + "\",\""
                               + accessorytb[i].mas_id + "\",\""
                               + accessorytb[i].mas_name + "\",\""
                               + accessorytb[i].color_no + "\",\""
                               + accessorytb[i].color_name2 + "\",\""
                               + accessorytb[i].color_name + "\",\""

                               + accessorytb[i].mas_type + "\",\""
                               + accessorytb[i].size + "\",\""
                               + accessorytb[i].loss + "\",\""
                               + accessorytb[i].unit_qty + "\",\""
                               + accessorytb[i].pu_qty + "\",\""
                               + accessorytb[i].style_mas_qty + "\",\""
                               + accessorytb[i].unit_id + "\",\""
                               + accessorytb[i].trans_rate + "\",\""
                               + accessorytb[i].pu_trans_qty + "\",\""
                               + accessorytb[i].masFinished_qty + "\",\""

                               + accessorytb[i].mas_qty + "\",\""
                               + accessorytb[i].masExceed_qty + "\",\""
                               + accessorytb[i].unit_id_p + "\",\""
                               + accessorytb[i].pu_no + "\",\""
                               + accessorytb[i].vend_id + "\",\""
                               + accessorytb[i].vend_abbr + "\",\""
                               + accessorytb[i].per_id + "\",\""

                               + accessorytb[i].createPerson + "\",\""
                               + accessorytb[i].createDate + "\",\""
                               + accessorytb[i].changePerson + "\",\""
                               + accessorytb[i].changeDate + "\",\""
                               + accessorytb[i].receiveNumber + "\",\""
                               + accessorytb[i].receiveNumberBatch + "\",\""
                               + accessorytb[i].materialStatus + "\",\""
                               + accessorytb[i].printTimes + "\",\""
                               + accessorytb[i].note +"\"),";
            }
            sqlValue = sqlValue.Substring( 0,sqlValue.Length - 1) + ";" ;

            sqlstr = @"INSERT INTO accessoryout (

                              be_id ,  
                              my_no ,
                              style_id ,
                              od_no ,
                              od_qty ,
                              season_id ,
                              style_name ,

                              sex_name ,
                              sample_no ,
                              in_date ,
                              release_who ,
                              clr_no ,
                              mas_sortNumber ,
                              mas_sortName ,

                              item_id ,
                              item_name ,
                              mas_id ,
                              mas_name ,
                              color_no ,
                              color_name2 ,
                              color_name ,

                              mas_type ,
                              size ,
                              loss ,
                              unit_qty ,
                              pu_qty ,
                              style_mas_qty ,
                              unit_id ,
                              trans_rate ,
                              pu_trans_qty ,
                              masFinished_qty ,

                              mas_qty ,
                              masExceed_qty ,
                              unit_id_p ,
                              pu_no ,
                              vend_id ,
                              vend_abbr ,
                              per_id ,

                              createPerson ,
                              createDate ,
                              changePerson ,
                              changeDate ,
                              receiveNumber ,
                              receiveNumberBatch ,
                              materialStatus ,
                              printTimes ,
                              note 

                    )  VALUES " + sqlValue ;

            int result = 0;             
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

        public int updataAccessoryToDB(List<materials> accessorytb)
        {
           
                
            string masFinished_Value = "";
            string masUnfinished_Value = "";
            string receiveNumberBatch_Value = "";
            string id_Value = "";
            for (int i = 0; i < accessorytb.Count; i++)
            {
                masFinished_Value = masFinished_Value + " WHEN " + accessorytb[i].id + " THEN " + accessorytb[i].masFinished_qty;
                masUnfinished_Value = masUnfinished_Value + " WHEN " + accessorytb[i].id + " THEN " + accessorytb[i].masUnfinished_qty;
                receiveNumberBatch_Value = receiveNumberBatch_Value + " WHEN " + accessorytb[i].id + " THEN '" + accessorytb[i].receiveNumberBatch +"'";
                id_Value = id_Value + ","+ accessorytb[i].id ;
            }
            id_Value = id_Value.Remove(0, 1);
            string sqlstr = @"
                    UPDATE accessoryout
                         SET masFinished_qty = CASE id " +
                            masFinished_Value + @"
                         END,
                         masUnfinished_qty = CASE id " + 
                           masUnfinished_Value  + @"
                        END,
                        receiveNumberBatch = CASE id " +
                           receiveNumberBatch_Value + @"
                        END  
                    WHERE id IN (" + id_Value + ")"; 

            int result = 0;
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

        public List<accessoryOut> getAccessoryOutByLocalHostDB(string mynumber)
        {
            string sqlstr = @"SELECT * FROM  `accessoryout`  where   my_no= '" + mynumber +"'" ;
            DataTable dt = new DataTable();
 
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            }

         

            List<MODEL.accessoryOut> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.accessoryOut>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.accessoryOut c = new MODEL.accessoryOut();
                    accessoryOut(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }

        public int delAccessoryOutFromLocalHostDBByMyNumber(string mynumber)
        {
            string sqlstr = @"delete FROM accessoryout where my_no='" + mynumber + "'";
          
            int result = 0;
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

        public int updataAccessoryOutFromLocalHostDBByMyNumber(string mynumber)
        {
            string sqlstr = @"UPDATE accessoryout set materialStatus='E' where  my_no='" + mynumber + "'";
            
            int result = 0;
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


        public List<accessoryOut> getAccessoryOutByLocalHostDB(List<parameter> parameter, string Org)
        {
            string sqlstr = @"SELECT * FROM `accessoryout`";              
            string sqlwhere = "";
            if (parameter.Count > 0)
            {
                for (int i = 0; i < parameter.Count; i++)
                {
                    if (parameter[i].pkey == "my_no")
                    {
                        sqlwhere = sqlwhere + "  my_no =  '" + parameter[i].pvalue.ToString() + "' and ";
                    }
                    if (parameter[i].pkey == "style_id")
                    {
                        sqlwhere = sqlwhere + "  style_id =  '" + parameter[i].pvalue.ToString() + "' and ";
                    }
                    if (parameter[i].pkey == "pu_no")
                    {
                        sqlwhere = sqlwhere + "  pu_no =  '" + parameter[i].pvalue.ToString() + "'  and ";
                    }
                    if (parameter[i].pkey == "receiveNumber")
                    {
                        sqlwhere = sqlwhere + "  receiveNumber =  '" + parameter[i].pvalue.ToString() + "'  and ";
                    }
                    if (parameter[i].pkey == "receiveNumberBatch")
                    {
                        sqlwhere = sqlwhere + "  receiveNumberBatch =  '" + parameter[i].pvalue.ToString() + "'  and ";
                    }

                }
                sqlwhere = "  where  " + sqlwhere + "  materialStatus != 'E' and  1 = 1 ";


            }
            sqlstr = sqlstr + sqlwhere;
            DataTable dt = new DataTable();            
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            }   

            List<MODEL.accessoryOut> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.accessoryOut>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.accessoryOut c = new MODEL.accessoryOut();
                    accessoryOut(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }


        public List<groupCloNames> getColorFromBestDBByMyNo(string mynumber,string serviceName)
        {
            string sqlstr = @"
                         select h.od_no, CONVERT(VARCHAR, h.in_date, 120)   in_date
                         ,h.my_no,h.season_id,h.release_who,b.clr_no
                         ,b.style_id
                         ,s.style_name,sex_name,sample_no
                         , h.w_id
                         from odh  h
                         left join odb b on  h.od_no=b.od_no 
                         left join style s on b.style_id =  s.style_id 
                         where h.my_no like '%" + mynumber + @"%'     
						 group by h.od_no,h.in_date,h.my_no,h.season_id,h.release_who,b.clr_no
                         ,b.style_id ,s.style_name,sex_name,sample_no, h.w_id
						 order by clr_no";
            
            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr, serviceName);

            List<MODEL.groupCloNames> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.groupCloNames>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.groupCloNames c = new MODEL.groupCloNames();
                    groupColors(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }
        public void groupColors(DataRow dr, MODEL.groupCloNames list)
        {
           
            list.od_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_no"]));
           // list.createDate = DateTime.Now.ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss"); // 开单日期时间
            list.in_date = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["in_date"]).ToString());
            list.my_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["my_no"])); 
            list.season_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["season_id"])); 
            list.release_who = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["release_who"]));
            list.clr_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["clr_no"]));
            
            list.style_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_name"]));
            list.sex_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["sex_name"]));
            list.sample_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["sample_no"]));
            list.w_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["w_id"]));
        }

        public List<groupPONumber> getPOFromBestDBByOd_no(string orderNumber, string serviceName)
        {
            string sqlstr = @"select   po_no ,od_type from odb   where   od_no='"+ orderNumber + @"' group by po_no ,od_type order by od_type,  po_no";
            
            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr, serviceName);

            List<MODEL.groupPONumber> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.groupPONumber>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.groupPONumber c = new MODEL.groupPONumber();
                    groupOrderNumber(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }
        public void groupOrderNumber(DataRow dr, MODEL.groupPONumber list)
        {

           
            list.po_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["po_no"]));
            
        }


        public List<groupSizeNmae> getSizeFromBestDBByOd_no(string orderNumber,string serviceName)
        {
            string sqlstr = @"select us01,us02, us03, us04,us05,us06,us07,us08,us09,us10,us11,us12 from  odh where od_no = '" + orderNumber + "'";

            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr, serviceName);

            List<MODEL.groupSizeNmae> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.groupSizeNmae>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.groupSizeNmae c = new MODEL.groupSizeNmae();
                    groupSizeName(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }
        public void groupSizeName(DataRow dr, MODEL.groupSizeNmae list)
        {


            list.SizeName01 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us01"]));
            list.SizeName02 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us02"]));
            list.SizeName03 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us03"]));
            list.SizeName04 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us04"]));
            list.SizeName05 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us05"]));
            list.SizeName06 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us06"]));
            list.SizeName07 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us07"]));
            list.SizeName08 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us08"]));
            list.SizeName09 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us09"]));
            list.SizeName10 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us10"]));
            list.SizeName11 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us11"]));
            list.SizeName12 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["us12"]));

        }

        public List<materialSize> getMaterialSizeFromBestDBByOd_no(string orderNumber)
        {
            /*
                  SELECT od_no, style_id, mas_id, color_no, size from v_od_mats  where od_no = 'SFC2000523'
and(size != '' or size is null)  group by od_no,style_id,mas_id,color_no,size--size ?
                  */
            string sqlstr = @" SELECT od_no, style_id, mas_id, color_no, size from v_od_mats  
                                where od_no ='" + orderNumber + @"' and(size != '' or size is null)  
                                group by od_no,style_id,mas_id,color_no,size ";

            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr);

            List<MODEL.materialSize> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.materialSize>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.materialSize c = new MODEL.materialSize();
                    materialSizeName(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }
        public void materialSizeName(DataRow dr, MODEL.materialSize list)
        {
            list.od_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_no"])); // 客户订单号
            list.style_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_id"]));// 款式
            list.mas_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_id"])); // 物料ID
            list.color_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_no"])); // 物料颜色
            list.size = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["size"]));   // 物料Size
        }


        public List<materials> getMaterialsFromBestDBByOd_no(string myNumber)
        {
            string sqlstr = @"
	                       SELECT v.be_id,v.my_no,v.od_no,v.style_id,v.clr_no,v.mas_id
                                 ,m.mas_name,m.unit_id,v.color_no,p.color_name2,v.size                                 
	                             ,v.unit_qty,v.pu_qty,v.style_mas_qty	
                                 ,m.trans_rate ,Ceiling((v.style_mas_qty / m.trans_rate)) as  mas_qty  
                                 ,m.unit_id_p 
	                             ,p.pu_no,v.vend_id ,d.vend_abbr 
                                 ,v.per_id
	                       from v_od_mats  v	   
	                        left join  odb_pur p    on p.style_id= v.style_id
							                       and p.color_no = v.color_no
							                       and p.size = v.size
							                       and p.vend_id = v.vend_id
							                       and p.od_no = v.od_no
							                       and p.pu_no is not null
		                    left join vend_dom d on d.vend_id = v.vend_id
		                     left join mats m on m.od_no = v.od_no
							                    and m.style_id = v.style_id
							                    and m.mas_id = v.mas_id
							                    and m.color_no = v.color_no
							                    and m.clr_no = v.clr_no
							                    and m.size = v.size
							                    and m.vend_id = v.vend_id		
	                       where v.my_no=@myNumber	                       
	                       group by  v.od_no,v.style_id,v.clr_no,v.mas_id
	                       ,v.color_no,v.size,v.unit_qty,v.pu_qty
	                       ,v.my_no,v.per_id,v.be_id
	                       ,v.vend_id,v.style_mas_qty
	                       ,p.color_name2, p.pu_no
	                       ,d.vend_abbr
	                       ,m.trans_rate,m.mas_name,m.unit_id_p,m.unit_id
	                       order by v.mas_id, v.clr_no,v.size,v.color_no,d.vend_abbr;";

            SqlParameter[] ps = {
                                new SqlParameter("myNumber",myNumber)
                                                      };
          
            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr, ps);

            List<MODEL.materials> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.materials>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.materials c = new MODEL.materials();
                    groupMaterial(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }



        /*
        public void groupMaterial(DataRow dr, MODEL.materials list)
        {            
            list.od_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_no"])); // BEST订单号码
            list.style_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_id"])); // 款号
            list.clr_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["clr_no"])); //成衣色组号
            list.mas_sortNumber = ""; //物料分群码 要从ERP 导入
            list.mas_sortName = ""; //物料群名称 要从ERP 导入
            list.mas_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_id"])); //物料ID
            list.color_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_no"])); //物料颜色
            list.size = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["size"]));//物料尺码
            list.unit_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_qty"])); //单位用量
            list.unit_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_id"])); //物料单位
            list.pu_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["pu_qty"])); //采购数量
            list.my_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["my_no"])); //自编单号
            list.per_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["per_id"])); //审核人员？
            list.be_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["be_id"]));    //生产厂区
            list.vend_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["vend_id"])); //供应商编码
            list.style_mas_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_mas_qty"])); //款式总用量
            list.color_name2 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_name2"])); // 颜色中文名称
            list.pu_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["pu_no"])); //采购单号
            list.vend_abbr = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["vend_abbr"])); //供应商简称
            list.trans_rate = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["trans_rate"])); //单位转换比率
            list.mas_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_name"])); //物料名称
            list.unit_id_p = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_id_p"])); //发料单位
            list.mas_qty = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_qty"])); //应发料量
    }
        */


        public List<materials> getMaterialsFromBestDBByOd_no(string od_no, string serviceName)
        {
             // 物料 SIZE表 颜色
            string sqlstr = @" 
				      					select m.od_no,m.style_id ,m.mas_id,m.mas_name,m.color_no,m.color_name2
					    ,m.unit_id,m.unit_id_p,m.trans_rate 
					  ,m.size 	 ,m.unit_qty ,m.od_qty							
					  from mats  m					       
					  where m.od_no like'%" + od_no + @"%'   
					  group by m.od_no,m.style_id,m.mas_id,m.mas_name,m.color_no,m.color_name2
					    ,m.unit_id,m.unit_id_p,m.trans_rate 
					  ,m.size 	 ,m.unit_qty	,m.od_qty	
					order by m.mas_id,m.color_no;";

            

            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr, serviceName);

            List<MODEL.materials> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.materials>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.materials c = new MODEL.materials();
                    groupMaterial(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }



        public void groupMaterial(DataRow dr, MODEL.materials list)
        {
            list.od_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_no"])); // BEST订单号码
            list.style_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_id"])); // 款号           
            list.mas_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_id"])); //物料ID
            list.mas_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_name"])); //物料名称
            list.color_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_no"])); //物料颜色
            list.size = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["size"]));//物料尺码            
            list.unit_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_id"])); //物料单位           
            list.color_name2 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_name2"])); // 颜色中文名称           
            list.trans_rate = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["trans_rate"])); //单位转换比率           
            list.unit_id_p = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_id_p"])); //发料单位    
            list.unit_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["unit_qty"])); //单件用量 
            list.od_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_qty"])); //订单数量   
        }



        public List<GroupColor> getGroupColorFromBestDBByOd_no(string od_no, string serviceName, int type,int i)
        {
            string sqlstr = ""; 
            if(type == 0)
            {
                // 色组的名称
                 sqlstr = @"
                            select  od_no,style_id,mas_id,text1 as mas_name,text2,text3,text4,text5
                                        ,text6,text7,text8,text9,text10,text11,mas_type 
			                    from od_color    
                                where od_no='" + od_no + @"' and text1 = '配色表';";
            }
            else
            {
                string p = "' and text" + i.ToString() + " is not null and text" + i.ToString() + " != '' and mas_type = 2 ";
                // 色组的物料
                 sqlstr = @" 
				     		  select  od_no,style_id,mas_id,text1 as mas_name,text2,text3,text4,text5
                                        ,text6,text7,text8,text9,text10,text11,mas_type 
			                    from od_color    
                                where od_no='" + od_no +   p;
            }
            
            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr, serviceName);
            List<MODEL.GroupColor> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.GroupColor>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.GroupColor c = new MODEL.GroupColor();
                    groupColor(row, c);
                    lists.Add(c);
                }
            }
            return lists;
        }

        public void groupColor(DataRow dr, MODEL.GroupColor list)
        {
            list.od_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_no"])); // BEST订单号码
            list.style_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_id"])); // 款号           
            list.mas_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_id"])); 
            list.mas_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_name"]));
            list.text2 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text2"]));
            list.text3 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text3"]));
            list.text4 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text4"]));
            list.text5 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text5"]));  
            list.text6 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text6"])); 
            list.text7 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text7"]));
            list.text8 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text8"]));
            list.text9 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text9"]));
            list.text10 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text10"]));
            list.text11 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["text11"]));
            list.mas_type = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_type"]));
        }


        public List<odb_pur> getODPuNoFromBestDBByPu_no(string od_no, string serviceName)
        {
            string sqlstr = @"
                            SELECT m.od_no,
                                   m.style_id,
                                   m.mas_id,
                                   m.color_no,
                                   m.color_name,
                                   m.size,
                                   m.vend_id,
                                   p.pu_no,
                                   m.color_name2,
                                   d.vend_abbr,
                                   m.mas_name,
                                   a.mas_type,
                                   i.item_id,
                                   i.item_name,
                                   m.unit_qty,
                                   m.loss,
                                   m.od_qty,
                                   m.qty,
                                   m.trans_rate
                            FROM mats m
                                LEFT JOIN vend_dom d
                                    ON m.vend_id = d.vend_id
                                LEFT JOIN mas a
                                    ON m.mas_id = a.mas_id
                                LEFT JOIN item i
                                    ON a.item_id = i.item_id
                                LEFT JOIN odb_pur p
                                    ON m.mas_id = p.mas_id
                                       AND m.od_no = p.od_no
                                       AND m.color_no = p.color_no
                                       AND m.size = p.size
                            WHERE m.od_no = '" + od_no + @"'
                                  AND a.mas_type = 2
                            ORDER BY i.item_id,
                                     m.color_no,
                                     m.size;";
            
            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr, serviceName);
            List<MODEL.odb_pur> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.odb_pur>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.odb_pur c = new MODEL.odb_pur();
                    pursno(row, c);
                    lists.Add(c);
                }
            }
            return lists;
        }

        public void pursno(DataRow dr, MODEL.odb_pur list)
        {
            list.od_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_no"])); // BEST订单号码
            list.style_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_id"])); // 款号           
            list.mas_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_id"]));
            list.color_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_no"]));
            list.color_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_name"]));
            list.color_name2 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_name2"]));
            list.size = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["size"]));           
            list.pu_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["pu_no"]));
            list.vend_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["vend_id"]));
            list.vend_abbr = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["vend_abbr"]));

            list.mas_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_name"]));
            list.mas_type = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_type"]));
            list.item_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["item_id"]));
            list.item_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["item_name"]));

            list.unit_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["unit_qty"]));
            list.loss = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["loss"]));
            list.od_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["od_qty"]));
            list.qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["qty"]));  //采购数量      -- 发料数量
            list.trans_rate = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["trans_rate"]));
        }

        public List<allqtys> getAllQtysFromBestDBByPu_no(string od_no, string serviceName)
        {
            string sqlstr = @"
                            SELECT od_no,
                                   style_id,
                                   clr_no,
                                   qty01,
                                   qty02,
                                   qty03,
                                   qty04,
                                   qty05,
                                   qty06,
                                   qty07,
                                   qty08,
                                   qty09,
                                   qty10,
                                   qty11,
                                   qty12,
                                   qty,
                                   wip,
                                   dm,
                                   area_id,
                                   po_no,
                                   id,
                                   mark,
                                   od_type
                            FROM odb
                            WHERE od_no = '"+ od_no + "';";

            DataTable dt = BEST_SqlHelper.ExcuteTable(sqlstr, serviceName);
            List<MODEL.allqtys> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.allqtys>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.allqtys c = new MODEL.allqtys();
                    allqty(row, c);
                    lists.Add(c);
                }
            }
            return lists;
        }

        public void allqty(DataRow dr, MODEL.allqtys list)
        {
            list.od_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_no"])); // BEST订单号码
            list.style_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_id"])); // 款号     
            list.clr_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["clr_no"])); // 色组号码
            list.qty01 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty01"])); 
            list.qty02 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty02"])); 
            list.qty03 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty03"])); 
            list.qty04 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty04"])); 
            list.qty05 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty05"])); 
            list.qty06 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty06"])); 
            list.qty07 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty07"])); 
            list.qty08 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty08"])); 
            list.qty09 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty09"])); 
            list.qty10 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty10"])); 
            list.qty11 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty11"])); 
            list.qty12 = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty12"]));
            list.qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["qty"])); // 一个 area_id, PO_NO的数量
            list.wip = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["wip"]));
            list.dm = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["dm"]));
            list.area_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["area_id"]));
            list.po_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["po_no"]));
            list.id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["id"]));
            list.mark = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mark"]));
            list.od_type = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_type"]));
        }

        public List<materials> getAccessoryOutByOd_no(string od_no )
        {
            string sqlstr = @"
                            SELECT
	                                id,
	                                be_id,
	                                my_no,
	                                style_id,
	                                od_no,
	                                od_qty,
	                                od_Finished_qty,
	                                od_Unfinished_qty,
	                                od_make_qty,
	                                od_Exceed_qty,
	                                season_id,
	                                style_name,
	                                sex_name,
	                                sample_no,
	                                in_date,
	                                release_who,
	                                clr_no,
	                                mas_sortNumber,
	                                mas_sortName,
	                                item_id,
	                                item_name,
	                                mas_id,
	                                mas_name,
	                                color_no,
	                                color_name2,
	                                color_name,
	                                mas_type,
	                                size,
	                                loss,
	                                unit_qty,
	                                pu_qty,
	                                style_mas_qty,
	                                unit_id,
	                                trans_rate,
	                                pu_trans_qty,
	                                masFinished_qty,
	                                masUnfinished_qty,
	                                mas_qty,
	                                masExceed_qty,
	                                unit_id_p,
	                                pu_no,
	                                vend_id,
	                                vend_abbr,
	                                per_id,
	                                createPerson,
	                                createDate,
	                                changePerson,
	                                changeDate,
	                                receiveNumber,
	                                receiveNumberBatch,
	                                materialStatus,
	                                printTimes,
	                                note 
                                FROM
	                                accessoryout 
                                WHERE
	                                od_no = '" + od_no + @"' 
                                ORDER BY
	                                clr_no,
	                                item_id,
	                                color_name,
	                                size,
	                                pu_no";

            DataTable dt = new DataTable();            
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            }
          

            List<MODEL.materials> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.materials>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.materials c = new MODEL.materials();
                    toAccessoryOut(row, c);
                    lists.Add(c);
                }
            }
            return lists;
        }

        public List<materials>  materialsDataToList(DataTable dt)
        {
            List<MODEL.materials> lists = null;
            if (dt.Rows.Count > 0)
            {
                lists = new List<MODEL.materials>();
                foreach (DataRow row in dt.Rows)
                {
                    MODEL.materials c = new MODEL.materials();
                    toAccessoryOut(row, c);
                    lists.Add(c);
                }
            }
            return lists;
        }

        public void toAccessoryOut(DataRow dr, MODEL.materials list)
        {
            list.id = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["id"])); // 本地库的索引ID
            list.be_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["be_id"])); // 厂区
            list.my_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["my_no"])); // 自编单号
            list.style_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_id"])); // 款式号码
            list.od_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["od_no"])); // BEST订单号码
            list.od_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_qty"])); // BEST订单数量
           // list.od_Finished_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_Finished_qty"])); // 已生产完成数量
           // list.od_Unfinished_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_Unfinished_qty"])); // 待生产数量
          //  list.od_make_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_make_qty"])); // 本次生产数量
         //   list.od_Exceed_qty = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["od_Exceed_qty"])); // 生产溢出数量

            list.season_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["season_id"])); // 季节号
            list.style_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["style_name"])); // 款式名称
            list.sex_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["sex_name"])); // 款式性别名称
            list.sample_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["sample_no"])); // 马克版本号
            list.in_date = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["in_date"])); // 订单接单日期
            list.release_who = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["release_who"])); // 订单审核人员
            list.clr_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["clr_no"])); // 订单成衣色组号

            list.mas_sortNumber = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_sortNumber"])); // 物料分群号
            list.mas_sortName = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_sortName"])); // 物料分群名称
            list.item_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["item_id"])); // 物料分群号
            list.item_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["item_name"])); // 物料分群名称
            list.mas_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["item_name"])); // 物料ID
            list.mas_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["item_name"])); // 物料名称
            list.color_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_no"])); // 物料色号
            list.color_name2 = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_name2"])); // 物料颜色中文名称
            list.color_name = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["color_name"])); // 物料颜色英文名称

            list.mas_type = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["mas_type"])); // 物料大类码
            list.size = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["size"])); // 物料SIZE
            list.loss = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["loss"])); // 物料预补百分比数
            list.unit_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["unit_qty"])); // 物料单位(件)用量
            list.pu_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["pu_qty"])); // 物料采购数量
            list.style_mas_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["style_mas_qty"])); // 款式物料总用量
            list.unit_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_id"])); // 物料单位
            list.trans_rate = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["trans_rate"])); // 物料转换比率
            list.pu_trans_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["pu_trans_qty"])); // 物料发料单位采购数量
            list.masFinished_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["masFinished_qty"])); // 订单物料已发料数量
            list.masUnfinished_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["masUnfinished_qty"])); // 订单物料待发料数量
            list.mas_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["mas_qty"])); // 订单物料本次发料量
            list.masExceed_qty = Convert.ToDouble(ERP_SqlHelper.FromDbValue(dr["masExceed_qty"])); // 订单物料超发料量

            list.unit_id_p = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["unit_id_p"])); // 订单物料发料单位
            list.pu_no = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["pu_no"])); // 订单物料采购单号
            list.vend_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["vend_id"])); // 订单物料供应商代码
            list.vend_abbr = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["vend_abbr"])); // 订单物料供应商简称
            list.per_id = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["per_id"])); // 订单审核人员ID

            list.createPerson = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["createPerson"])); // 发料单创建人员(电脑名称)
            list.createDate = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["createDate"])); // 发料单创建日期时间
            list.changePerson = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["changePerson"])); // 发料单最后修改人员(电脑名称)
            list.changeDate = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["changeDate"])); // 发料单最后修改日期时间
            list.receiveNumber = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["receiveNumber"])); // 领料单号 DA(领料)+03(辅料)+年度(2码)+第几星期(2码)+ 4码流水号  
            list.receiveNumberBatch = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["receiveNumberBatch"])); // 发料批号  3码流水 
            list.materialStatus = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["materialStatus"])); // 物料发放状态  O完成  N未发料  P发了一部分  进行中        
            list.printTimes = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["printTimes"])); //打印次数
            list.note = Convert.ToString(ERP_SqlHelper.FromDbValue(dr["note"])); // 备注
        }


        public int getAccessoryOutByReceiveNumber(string receive)
        {
            string sqlstr = @"select MAX(receiveNumber) FROM accessoryout  WHERE receiveNumber LIKE '"+ receive + "%'";
            int receives = 0;
            string reis = "";
            DataTable dt = new DataTable(); 
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            } 


            if (dt.Rows.Count > 0)
            {
              reis = dt.Rows[0][0].ToString();
            }
            if (reis.Trim().Length <= 0)
            {
                receives = 0;
            }
            else
            {
                receives = Convert.ToInt32(reis.Substring(6, 6));
            }
           
           // 


            return receives;
           
        }


        // 写入流水账数据库
        public int writeAccessoryhToDB(List<materialhs> accessoryhtb)
        {
            string sqlstr = "";
            string sqlValue = "";
            for (int i = 0; i < accessoryhtb.Count; i++)
            {

                sqlValue = sqlValue +
                           "(\""
                               + accessoryhtb[i].a_id + "\",\""
                               + accessoryhtb[i].be_id + "\",\""
                               + accessoryhtb[i].my_no + "\",\""
                               + accessoryhtb[i].style_id + "\",\""
                               + accessoryhtb[i].od_no + "\",\""
                               + accessoryhtb[i].od_qty + "\",\""
                               + accessoryhtb[i].season_id + "\",\""
                               + accessoryhtb[i].style_name + "\",\""

                               + accessoryhtb[i].sex_name + "\",\""
                               + accessoryhtb[i].sample_no + "\",\""
                               + accessoryhtb[i].in_date + "\",\""
                               + accessoryhtb[i].release_who + "\",\""
                               + accessoryhtb[i].clr_no + "\",\""
                               + accessoryhtb[i].mas_sortNumber + "\",\""
                               + accessoryhtb[i].mas_sortName + "\",\""

                               + accessoryhtb[i].item_id + "\",\""
                               + accessoryhtb[i].item_name + "\",\""
                               + accessoryhtb[i].mas_id + "\",\""
                               + accessoryhtb[i].mas_name + "\",\""
                               + accessoryhtb[i].color_no + "\",\""
                               + accessoryhtb[i].color_name2 + "\",\""
                               + accessoryhtb[i].color_name + "\",\""

                               + accessoryhtb[i].mas_type + "\",\""
                               + accessoryhtb[i].size + "\",\""
                               + accessoryhtb[i].loss + "\",\""
                               + accessoryhtb[i].unit_qty + "\",\""
                               + accessoryhtb[i].pu_qty + "\",\""
                               + accessoryhtb[i].style_mas_qty + "\",\""
                               + accessoryhtb[i].unit_id + "\",\""
                               + accessoryhtb[i].trans_rate + "\",\""
                               + accessoryhtb[i].pu_trans_qty + "\",\""
                               + accessoryhtb[i].masFinished_qty + "\",\""

                               + accessoryhtb[i].mas_qty + "\",\""
                               + accessoryhtb[i].masExceed_qty + "\",\""
                               + accessoryhtb[i].unit_id_p + "\",\""
                               + accessoryhtb[i].pu_no + "\",\""
                               + accessoryhtb[i].vend_id + "\",\""
                               + accessoryhtb[i].vend_abbr + "\",\""
                               + accessoryhtb[i].per_id + "\",\""

                               + accessoryhtb[i].createPerson + "\",\""
                               + accessoryhtb[i].createDate + "\",\""
                               + accessoryhtb[i].changePerson + "\",\""
                               + accessoryhtb[i].changeDate + "\",\""
                               + accessoryhtb[i].receiveNumber + "\",\""
                               + accessoryhtb[i].receiveNumberBatch + "\",\""
                               + accessoryhtb[i].materialStatus + "\",\""
                               + accessoryhtb[i].printTimes + "\",\""
                               + accessoryhtb[i].note + "\"),";
            }
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) + ";";

            sqlstr = @"INSERT INTO accessoryouth (
                              a_id ,
                              be_id ,  
                              my_no ,
                              style_id ,
                              od_no ,
                              od_qty ,
                              season_id ,
                              style_name ,

                              sex_name ,
                              sample_no ,
                              in_date ,
                              release_who ,
                              clr_no ,
                              mas_sortNumber ,
                              mas_sortName ,

                              item_id ,
                              item_name ,
                              mas_id ,
                              mas_name ,
                              color_no ,
                              color_name2 ,
                              color_name ,

                              mas_type ,
                              size ,
                              loss ,
                              unit_qty ,
                              pu_qty ,
                              style_mas_qty ,
                              unit_id ,
                              trans_rate ,
                              pu_trans_qty ,
                              masFinished_qty ,

                              mas_qty ,
                              masExceed_qty ,
                              unit_id_p ,
                              pu_no ,
                              vend_id ,
                              vend_abbr ,
                              per_id ,

                              createPerson ,
                              createDate ,
                              changePerson ,
                              changeDate ,
                              receiveNumber ,
                              receiveNumberBatch ,
                              materialStatus ,
                              printTimes ,
                              note 

                    )  VALUES " + sqlValue;

            int result = 0; 
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


        public  DataTable getAccessoryhByreceiveNumber(string reno, string renoBatch)
        {
            //SELECT * from accessoryouth where receiveNumber ='DA2010000001'  and receiveNumberBatch='04'
            string sqlstr = @"SELECT * from accessoryouth where receiveNumber ='" + reno + "' and receiveNumberBatch= '" + renoBatch + "'";
           
           
            DataTable dt = new DataTable();
            if (MiddleWare == "1")
            {
                dt = MyCatfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            else
            {
                dt = Mysqlfsg_SqlHelper.ExcuteTable(sqlstr);
            }
            return dt;

        }
    }
}
