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
    public class FrmNewStyleSearchManager
    {
        FrmNewStyleService NSS = new FrmNewStyleService();
        public DataTable getNewStyleByMynoDate(DataTable searchdt, string yyyymm)
        {
            /*
             * 解析DT为LIST 1个LIST 为一个款
             1、查询这个款式 月BUY 的最早的 MY_No 的 订单日期
             2、查询这个款式 月BUY 早于 MY_NO的订单日期 最后的 MY_No
             3、有 为旧款式  带出MY_NO
             4、没有为NewStyle  带出 "NEW"
             */
            List<DataTable> ldt = new List<DataTable>();
            if (searchdt.Rows.Count <= 0)
            {
                return null;
            }
            yyyymm = yyyymm.Trim();
            if (yyyymm == "")
            {
                return null;
            }
            // 去重
            string[] fieldNames = { "style_id" };
            DataTable SourceDT = this.Distinct(searchdt  , fieldNames);
            DataTable resultStyleDT = NSS.getNewStyleByMynoDate(SourceDT, yyyymm);

            if (resultStyleDT.Rows.Count <= 0)
            {
                return null;
            }
            List<styleOddate> StyleOdates = new List<styleOddate>();
            foreach (DataRow dr in resultStyleDT.Rows)
            {
                styleOddate StyleOdate = new styleOddate();
                StyleOdate.style_id = dr["style_id"].ToString().Trim().ToUpper();
                StyleOdate.od_date = dr["od_date"].ToString().Trim().ToUpper();
                StyleOdates.Add(StyleOdate);
            }

            DataTable resultNewOrOldDT = NSS.getStyleIsNewOrOld(StyleOdates);
            DataTable resultDT = new DataTable();
            DataColumn be_id = new DataColumn();
            be_id.ColumnName = "be_id";
            resultDT.Columns.Add(be_id);

            DataColumn cust_id = new DataColumn();
            cust_id.ColumnName = "cust_id";
            resultDT.Columns.Add(cust_id);

            DataColumn season_id = new DataColumn();
            season_id.ColumnName = "season_id";
            resultDT.Columns.Add(season_id);

            DataColumn yymm = new DataColumn();
            yymm.ColumnName = "yymm";
            resultDT.Columns.Add(yymm);

            DataColumn buy_cname = new DataColumn();
            buy_cname.ColumnName = "buy_cname";
            resultDT.Columns.Add(buy_cname);

            DataColumn style_id = new DataColumn();
            style_id.ColumnName = "style_id";
            resultDT.Columns.Add(style_id);

            DataColumn clr_no = new DataColumn();
            clr_no.ColumnName = "clr_no";
            resultDT.Columns.Add(clr_no);

            DataColumn qty = new DataColumn();
            qty.ColumnName = "qty";
            resultDT.Columns.Add(qty);

            DataColumn my_no = new DataColumn();
            my_no.ColumnName = "my_no";
            resultDT.Columns.Add(my_no);

            DataColumn type_id = new DataColumn();
            type_id.ColumnName = "type_id";
            resultDT.Columns.Add(type_id);

            DataColumn type_name = new DataColumn();
            type_name.ColumnName = "type_name";
            resultDT.Columns.Add(type_name);

            /*
            DataColumn od_date = new DataColumn();
            od_date.ColumnName = "od_date";
            resultDT.Columns.Add(od_date);


            DataColumn old_date = new DataColumn();  //旧款前次的 订单日期
            old_date.ColumnName = "old_date";
            resultDT.Columns.Add(old_date);
            */


            DataColumn old_Style_my_no = new DataColumn();
            old_Style_my_no.ColumnName = "old_Style_my_no";//老款式
            resultDT.Columns.Add(old_Style_my_no);

            DataColumn old_Style_Color_my_no = new DataColumn();
            old_Style_Color_my_no.ColumnName = "old_Style_Color_my_no";  //老款式 老颜色
            resultDT.Columns.Add(old_Style_Color_my_no);


            DataColumn old_season_id = new DataColumn();
            old_season_id.ColumnName = "old_season_id";
            resultDT.Columns.Add(old_season_id);





            if (resultNewOrOldDT.Rows.Count > 0)
            {
                for (int i = 0; i < resultStyleDT.Rows.Count; i++)
                {
                    DataRow row = resultDT.NewRow();
                    row["be_id"] = resultStyleDT.Rows[i]["be_id"].ToString();
                    row["cust_id"] = resultStyleDT.Rows[i]["cust_id"].ToString();
                    row["season_id"] = resultStyleDT.Rows[i]["season_id"].ToString();
                    row["yymm"] = resultStyleDT.Rows[i]["yymm"].ToString();
                    row["buy_cname"] = resultStyleDT.Rows[i]["buy_cname"].ToString();
                    row["style_id"] = resultStyleDT.Rows[i]["style_id"].ToString();
                    row["clr_no"] = resultStyleDT.Rows[i]["clr_no"].ToString();
                    row["qty"] = resultStyleDT.Rows[i]["qty"].ToString() ;
                    row["my_no"] = resultStyleDT.Rows[i]["my_no"].ToString();
                    row["type_id"] = resultStyleDT.Rows[i]["type_id"].ToString();
                    row["type_name"] = resultStyleDT.Rows[i]["type_name"].ToString();
                  //  row["od_date"] = resultStyleDT.Rows[i]["od_date"].ToString();

                    for (int j = 0; j < resultNewOrOldDT.Rows.Count; j++)
                    {

                        if (resultStyleDT.Rows[i]["style_id"].ToString() == resultNewOrOldDT.Rows[j]["style_id"].ToString())  // 老款式
                        {
                            //  row["old_date"] = resultNewOrOldDT.Rows[j]["od_date"].ToString();
                            row["old_Style_my_no"] = resultNewOrOldDT.Rows[j]["my_no"].ToString();
                            row["old_season_id"] = resultNewOrOldDT.Rows[j]["season_id"].ToString();
                        }
                        else
                        {
                           // row["old_Style_my_no"] = "新款式";
                        }

                        if (resultStyleDT.Rows[i]["style_id"].ToString() == resultNewOrOldDT.Rows[j]["style_id"].ToString()  &&
                            resultStyleDT.Rows[i]["clr_no"].ToString() == resultNewOrOldDT.Rows[j]["clr_no"].ToString())  // 老款式 老颜色
                        {
                          //  row["old_date"] = resultNewOrOldDT.Rows[j]["od_date"].ToString();
                            row["old_Style_Color_my_no"] = resultNewOrOldDT.Rows[j]["my_no"].ToString();
                            row["old_season_id"] = resultNewOrOldDT.Rows[j]["season_id"].ToString();
                        }
                        else
                        {
                          //  row["old_Style_Color_my_no"] = "新款新色";
                        }
                    }
                    resultDT.Rows.Add(row);

                }

            }
            return resultDT;
        }
        private bool isRepeatLists(List<string> list, string value)
        {
            bool result = true;
            if (list.Count <= 0)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == value)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public  DataTable Distinct(DataTable dt ,string[] filedNames)
        {
            DataView dv = dt.DefaultView;
            DataTable DistTable = dv.ToTable("Dist", true, filedNames);
            return DistTable;
        }
    }
}
