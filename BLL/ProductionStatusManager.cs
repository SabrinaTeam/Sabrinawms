using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class ProductionStatusManager
    {
        ProductionStatusServer pss = new ProductionStatusServer();
        public Tuple<List<DataTable>,int>  getProductionStatus(ProductionStatusSearch parameters, string serviceName)
        {
            List<DataTable> ldt = new List<DataTable>();

            // 取出BEST 数据
            DataTable BestDt = pss.getProductionStatus(parameters); 
            List<string> my_nos = new List<string>();
            int pages = 0;

            if (BestDt.Rows.Count <= 0)
            {
                MessageBox.Show("Best没有数据，谢谢!");
                return new Tuple<List<DataTable>, int>(null, 0);
               
            }

            // 自编单号集合
            foreach (DataRow dr in BestDt.Rows)
            {
                string my_no = dr["my_no"].ToString().Trim();
                if (!checkTextCH(my_no))
                {
                    my_no = my_no.Replace("'", "''");
                    if (!checkDoubleMy_nos(my_nos, my_no))
                    {
                        my_nos.Add(my_no);
                    }
                }
            }

              if (my_nos.Count > 1000)
            {
              //  MessageBox.Show("自编单号大于1000，将进行分页处理。谢谢!");
              
                if (my_nos.Count % 1000 == 0)
                {
                    pages = my_nos.Count % 1000;
                }
                else
                {
                    pages = my_nos.Count % 1000 + 1;
                }

                if (parameters.page > pages)
                {
                    parameters.page = pages;
                }

                List<string> mys = new List<string>();
                for (int j = parameters.page * 1000; j < (parameters.page + 1) * 1000 && j < my_nos.Count; j++)
                {
                    mys.Add(my_nos[j]);
                }
                my_nos = mys;
            }
           // my_nos.Add(pages.ToString());
            


            // 取出MES 数据
            DataTable MesDt = pss.getMesWorkTicketByMynos(my_nos, serviceName);
            if (MesDt.Rows.Count <= 0)
            {
                MessageBox.Show("MES没有数据，谢谢!");
                return new Tuple<List<DataTable>, int>(null, 0);
            }

            // 取出ERP 数据
            DataTable ERPDt = pss.getERPWorkTicketByMynos(my_nos, serviceName);
            if (ERPDt.Rows.Count <= 0)
            {
                MessageBox.Show("ERP没有数据，谢谢!");
                return new Tuple<List<DataTable>, int>(null, 0);
            }

            // 构造数据表存放合并数据
            DataTable ProductionStatus = new DataTable();
            DataColumn dc;

            // BEST 基础资料
            /*
            dc = new  DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "id";
            dc.Caption = "ID";
            ProductionStatus.Columns.Add(dc);
            */

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "myNumber";
            dc.Caption = "MyNumber";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "season_id";
            dc.Caption = "Season";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "cust_id";
            dc.Caption = "Cust";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "od_date";
            dc.Caption = "Order Date";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "yymm";
            dc.Caption = "yymm";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "style_Name";
            dc.Caption = "Style Name";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "style_description";
            dc.Caption = "Style Description";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "clr_Code";
            dc.Caption = "Color Code";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "clr_Name";
            dc.Caption = "Style Name";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "Order_Qty";
            dc.Caption = "Order Qty";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "panl_Date";
            dc.Caption = "Panl_Date";
            ProductionStatus.Columns.Add(dc);


            // MES 物流发裁片资料           
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "WLInStock";
            dc.Caption = "WL In Stock";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "WLOutStock";
            dc.Caption = "WL Out Stock";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ProcessID";
            dc.Caption = "ProcessID";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "processName";
            dc.Caption = "ProcessName";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "receiveLineName";
            dc.Caption = "LineName";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "receiveLineID";
            dc.Caption = "receiveLineID";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ReportPlaceID";
            dc.Caption = "ReportPlaceID";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ReportPlaceName";
            dc.Caption = "ReportPlaceName";
            ProductionStatus.Columns.Add(dc);


            // ERP 工单开立及车缝报工资料
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MakeOrderNo";
            dc.Caption = "MakeOrderNo";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OrderNo";
            dc.Caption = "OrderNo";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "create_Date";
            dc.Caption = "Create Date";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "ProduFinishQty";
            dc.Caption = "ProduFinishQty";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PlanMakeDate";
            dc.Caption = "PlanMakeDate";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Makechecked";
            dc.Caption = "Makechecked";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OrgName";
            dc.Caption = "OrgName";
            ProductionStatus.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FinishDate";
            dc.Caption = "FinishDate";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FinishMakeOrderNo";
            dc.Caption = "FinishMakeOrderNo";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "ProcessTypeID";
            dc.Caption = "ProcessTypeID";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OrderClass";
            dc.Caption = "OrderClass";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MakeLine";
            dc.Caption = "MakeLine";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ProcessType";
            dc.Caption = "ProcessType";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ProcessName";
            dc.Caption = "ProcessName";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "CFFinishQty";
            dc.Caption = "CFFinishQty";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "CFFinishBonus";
            dc.Caption = "CFFinishBonus";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FinishChecked";
            dc.Caption = "FinishChecked";
            ProductionStatus.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MakeOrgName";
            dc.Caption = "MakeOrgName";
            ProductionStatus.Columns.Add(dc);


            // 循环添加数据
            // 以MES 为基准
            for (int i = 0; i < MesDt.Rows.Count; i++)
            {
                DataRow dr = ProductionStatus.NewRow();
                int WLInStock = Convert.ToInt32(MesDt.Rows[i]["WLInStock"].ToString());
                int WLOutStock = Convert.ToInt32(MesDt.Rows[i]["WLOutStock"].ToString());
                string ProcessID = MesDt.Rows[i]["ProcessID"].ToString();
                if (ProcessID == "-1" || ProcessID == "0") { ProcessID = ""; }
                string ProcessName = MesDt.Rows[i]["ProcessName"].ToString();
                string LineName = MesDt.Rows[i]["LineName"].ToString();
                string receiveLineID = MesDt.Rows[i]["receiveLineID"].ToString();
                if (receiveLineID == "-1" || receiveLineID == "0") { receiveLineID = ""; }
                string ReportPlaceID = MesDt.Rows[i]["ReportPlaceID"].ToString();
                if (ReportPlaceID == "-1" || ReportPlaceID == "0") { ReportPlaceID = ""; }
                string ReportPlaceName = MesDt.Rows[i]["ReportPlaceName"].ToString();
                // MES 物流发裁片资料
                dr["WLInStock"] = WLInStock;
                dr["WLOutStock"] = WLOutStock;
                dr["ProcessID"] = ProcessID;
                dr["processName"] = ProcessName;
                dr["receiveLineName"] = LineName;
                dr["receiveLineID"] = receiveLineID;
                dr["ReportPlaceID"] = ReportPlaceID;
                dr["ReportPlaceName"] = ReportPlaceName;

                // BEST 基础资料
                for (int j = 0; j < BestDt.Rows.Count; j++)
                {
                    string mesMyNumber = MesDt.Rows[i]["orderSKU"].ToString().ToUpper();
                    string mesStyleID = MesDt.Rows[i]["productModel"].ToString().ToUpper();
                    string mesClrCode = MesDt.Rows[i]["colorName"].ToString().ToUpper();

                    string bestMyNumber = BestDt.Rows[j]["my_no"].ToString().ToUpper();
                    string bestStyleID = BestDt.Rows[j]["style_Name"].ToString().ToUpper();
                    string bestClrCode = BestDt.Rows[j]["clr_Code"].ToString().ToUpper();

                    // 处理颜色问题
                   if (mesClrCode.Length > bestClrCode.Length )
                    {
                        mesClrCode = mesClrCode.Substring(0, bestClrCode.Length);
                    }

                   

                    if (mesMyNumber == bestMyNumber && mesStyleID == bestStyleID && mesClrCode == bestClrCode)
                    {
                        dr["myNumber"] = BestDt.Rows[j]["my_no"].ToString().ToUpper();
                        dr["season_id"] = BestDt.Rows[j]["season_id"].ToString().ToUpper();
                        dr["cust_id"] = BestDt.Rows[j]["cust_id"].ToString().ToUpper();
                        dr["od_date"] = BestDt.Rows[j]["od_date"].ToString().ToUpper();
                        dr["yymm"] = BestDt.Rows[j]["yymm"].ToString().ToUpper();
                        dr["style_Name"] = BestDt.Rows[j]["style_Name"].ToString().ToUpper();
                        dr["style_description"] = BestDt.Rows[j]["Description"].ToString().ToUpper();
                        dr["clr_Code"] = BestDt.Rows[j]["clr_Code"].ToString().ToUpper();
                        dr["clr_Name"] = BestDt.Rows[j]["clr_name"].ToString().ToUpper();
                        dr["Order_Qty"] = Convert.ToInt32(BestDt.Rows[j]["OrderQty"]);
                        dr["panl_Date"] = BestDt.Rows[j]["PanlDate"].ToString().ToUpper();
                    }
                }
                int CFFinishQty = 0;
                int CFFinishBonus = 0;
                // ERP 工单开立及车缝报工资料
                for (int k = 0; k < ERPDt.Rows.Count; k++)
                {
                    string mesMyNumber = MesDt.Rows[i]["orderSKU"].ToString().ToUpper();
                    int mesLineID = Convert.ToInt32(MesDt.Rows[i]["receiveLineID"].ToString());
                    string ERPMyNumber = ERPDt.Rows[k]["myNo"].ToString().ToUpper();
                    string ERPMakeLineStr = ERPDt.Rows[k]["MakeLine"].ToString();
                    int ERPMakeLine = -1;
                    if (ERPMakeLineStr.Length >= 4)
                    {
                        ERPMakeLine = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(ERPMakeLineStr, @"[^0-9]+", ""));

                       // ERPMakeLine = Convert.ToInt32(ERPMakeLineStr.Substring(2, 2));
                    }
                   


                    if (ERPMyNumber == mesMyNumber && ERPMakeLine == mesLineID)
                    {
                        dr["MakeOrderNo"] = ERPDt.Rows[k]["MakeOrderNo"].ToString().ToUpper();
                        dr["OrderNo"] = ERPDt.Rows[k]["StyleName"].ToString().ToUpper();
                        dr["create_Date"] = ERPDt.Rows[k]["CreateDate"].ToString().ToUpper();
                        dr["ProduFinishQty"] = Convert.ToInt32(ERPDt.Rows[k]["ProduFinishQty"].ToString());
                        dr["PlanMakeDate"] = ERPDt.Rows[k]["PlanMakeDate"].ToString().ToUpper();
                        dr["Makechecked"] = ERPDt.Rows[k]["Makechecked"].ToString().ToUpper();
                        dr["OrgName"] = ERPDt.Rows[k]["OrgName"].ToString().ToUpper();
                        dr["FinishDate"] = ERPDt.Rows[k]["FinishDate"].ToString().ToUpper();
                        dr["FinishMakeOrderNo"] = ERPDt.Rows[k]["FinishMakeOrderNo"].ToString().ToUpper();
                        dr["ProcessTypeID"] = Convert.ToInt32(ERPDt.Rows[k]["ProcessTypeID"].ToString());
                        dr["OrderClass"] = ERPDt.Rows[k]["OrderClass"].ToString().ToUpper();
                        dr["MakeLine"] = ERPDt.Rows[k]["MakeLine"].ToString().ToUpper();
                        dr["ProcessType"] = ERPDt.Rows[k]["ProcessType"].ToString().ToUpper();
                        dr["ProcessName"] = ERPDt.Rows[k]["ProcessName"].ToString().ToUpper();

                        CFFinishQty += Convert.ToInt32(ERPDt.Rows[k]["CFFinishQty"].ToString());
                        dr["CFFinishQty"] = CFFinishQty;

                        CFFinishBonus += Convert.ToInt32(ERPDt.Rows[k]["CFFinishBonus"].ToString());
                        dr["CFFinishBonus"] = CFFinishBonus;

                        dr["FinishChecked"] = ERPDt.Rows[k]["FinishChecked"].ToString().ToUpper();
                        dr["MakeOrgName"] = ERPDt.Rows[k]["MakeOrgName"].ToString().ToUpper();
                    }
                }

                ProductionStatus.Rows.Add(dr);
            }


            // BEST 基础资料
            DataTable BestUnCorrespondMes = new DataTable();
            DataColumn BMUdc;
            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "my_no";
            BMUdc.Caption = "my_no";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "season_id";
            BMUdc.Caption = "season_id";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "cust_id";
            BMUdc.Caption = "cust_id";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "od_date";
            BMUdc.Caption = "od_date";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "yymm";
            BMUdc.Caption = "yymm";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "style_Name";
            BMUdc.Caption = "style_Name";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "Description";
            BMUdc.Caption = "Description";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "clr_Code";
            BMUdc.Caption = "clr_Code";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "clr_name";
            BMUdc.Caption = "clr_name";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.Int32");
            BMUdc.ColumnName = "OrderQty";
            BMUdc.Caption = "OrderQty";
            BestUnCorrespondMes.Columns.Add(BMUdc);

            BMUdc = new DataColumn();
            BMUdc.DataType = System.Type.GetType("System.String");
            BMUdc.ColumnName = "PanlDate";
            BMUdc.Caption = "PanlDate";
            BestUnCorrespondMes.Columns.Add(BMUdc);


            bool BUM = true;
            //  BEST  对不上的资料
            for (int j = 0; j < BestDt.Rows.Count; j++)
            {
                DataRow bdr = BestUnCorrespondMes.NewRow();
                for (int i = 0; i < MesDt.Rows.Count; i++)
                {
                    string mesMyNumber = MesDt.Rows[i]["orderSKU"].ToString().ToUpper();
                    string mesStyleID = MesDt.Rows[i]["productModel"].ToString().ToUpper();
                    string mesClrCode = MesDt.Rows[i]["colorName"].ToString().ToUpper();

                    string bestMyNumber = BestDt.Rows[j]["my_no"].ToString().ToUpper();
                    string bestStyleID = BestDt.Rows[j]["style_Name"].ToString().ToUpper();
                    string bestClrCode = BestDt.Rows[j]["clr_Code"].ToString().ToUpper();

                    // 处理颜色问题
                    if (mesClrCode.Length > bestClrCode.Length)
                    {
                        mesClrCode = mesClrCode.Substring(0, bestClrCode.Length);
                    }

                   // mesClrCode = mesClrCode.Substring(0, bestClrCode.Length);
                    if (mesMyNumber == bestMyNumber && mesStyleID == bestStyleID && mesClrCode == bestClrCode)
                    {
                        BUM = true;
                        break;
                    }
                    else
                    {
                        BUM = false;
                    }
                }
                if (!BUM)
                {
                    bdr["my_no"] = BestDt.Rows[j]["my_no"].ToString().ToUpper();
                    bdr["season_id"] = BestDt.Rows[j]["season_id"].ToString().ToUpper();
                    bdr["cust_id"] = BestDt.Rows[j]["cust_id"].ToString().ToUpper();
                    bdr["od_date"] = BestDt.Rows[j]["od_date"].ToString().ToUpper();
                    bdr["yymm"] = BestDt.Rows[j]["yymm"].ToString().ToUpper();
                    bdr["style_Name"] = BestDt.Rows[j]["style_Name"].ToString().ToUpper();
                    bdr["Description"] = BestDt.Rows[j]["Description"].ToString().ToUpper();
                    bdr["clr_Code"] = BestDt.Rows[j]["clr_Code"].ToString().ToUpper();
                    bdr["clr_name"] = BestDt.Rows[j]["clr_name"].ToString().ToUpper();
                    bdr["OrderQty"] = Convert.ToInt32(BestDt.Rows[j]["OrderQty"]);
                    bdr["PanlDate"] = BestDt.Rows[j]["PanlDate"].ToString().ToUpper();
                    BestUnCorrespondMes.Rows.Add(bdr);
                }
            }


            // ERP 基础资料
            DataTable ERPUnCorrespondMes = new DataTable();
            DataColumn EMUdc;
            // ERP 工单开立及车缝报工资料
            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "MakeOrderNo";
            EMUdc.Caption = "MakeOrderNo";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "OrderNo";
            EMUdc.Caption = "OrderNo";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "create_Date";
            EMUdc.Caption = "Create Date";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.Int32");
            EMUdc.ColumnName = "ProduFinishQty";
            EMUdc.Caption = "ProduFinishQty";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "PlanMakeDate";
            EMUdc.Caption = "PlanMakeDate";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "Makechecked";
            EMUdc.Caption = "Makechecked";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "OrgName";
            EMUdc.Caption = "OrgName";
            ERPUnCorrespondMes.Columns.Add(EMUdc);


            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "FinishDate";
            EMUdc.Caption = "FinishDate";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "FinishMakeOrderNo";
            EMUdc.Caption = "FinishMakeOrderNo";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.Int32");
            EMUdc.ColumnName = "ProcessTypeID";
            EMUdc.Caption = "ProcessTypeID";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "OrderClass";
            EMUdc.Caption = "OrderClass";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "MakeLine";
            EMUdc.Caption = "MakeLine";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "ProcessType";
            EMUdc.Caption = "ProcessType";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "ProcessName";
            EMUdc.Caption = "ProcessName";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.Int32");
            EMUdc.ColumnName = "CFFinishQty";
            EMUdc.Caption = "CFFinishQty";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.Int32");
            EMUdc.ColumnName = "CFFinishBonus";
            EMUdc.Caption = "CFFinishBonus";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "FinishChecked";
            EMUdc.Caption = "FinishChecked";
            ERPUnCorrespondMes.Columns.Add(EMUdc);

            EMUdc = new DataColumn();
            EMUdc.DataType = System.Type.GetType("System.String");
            EMUdc.ColumnName = "MakeOrgName";
            EMUdc.Caption = "MakeOrgName";
            ERPUnCorrespondMes.Columns.Add(EMUdc);


            //  ERPDt 对不上的资料
            bool EUM = true;
            for (int j = 0; j < ERPDt.Rows.Count; j++)
            {
                DataRow edr = ERPUnCorrespondMes.NewRow();
                for (int i = 0; i < MesDt.Rows.Count; i++)
                {
                    string mesMyNumber = MesDt.Rows[i]["orderSKU"].ToString().ToUpper();
                    int mesLineID = Convert.ToInt32(MesDt.Rows[i]["receiveLineID"].ToString());
                    string ERPMyNumber = ERPDt.Rows[j]["myNo"].ToString().ToUpper();

                    string ERPMakeLineStr = ERPDt.Rows[j]["MakeLine"].ToString();
                    int ERPMakeLine = -1;
                    if (ERPMakeLineStr.Length >= 4)
                    {
                        ERPMakeLine = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(ERPMakeLineStr, @"[^0-9]+", ""));

                        // ERPMakeLine = Convert.ToInt32(ERPMakeLineStr.Substring(2, 2));
                    }

                  //  int ERPMakeLine = Convert.ToInt32(ERPDt.Rows[j]["MakeLine"].ToString().Substring(2, 2));


                    //  ERPDt.Rows[k]  全部对不上的资料
                    if (ERPMyNumber == mesMyNumber && ERPMakeLine == mesLineID)
                    {
                        EUM = true;
                        break;
                    }
                    else
                    {
                        EUM = false;
                    }
                }
                if (!EUM)
                {
                    edr["MakeOrderNo"] = ERPDt.Rows[j]["MakeOrderNo"].ToString().ToUpper();
                    edr["OrderNo"] = ERPDt.Rows[j]["OrderNo"].ToString().ToUpper();
                    edr["create_Date"] = ERPDt.Rows[j]["CreateDate"].ToString().ToUpper();
                    edr["ProduFinishQty"] = Convert.ToInt32(ERPDt.Rows[j]["ProduFinishQty"].ToString().ToUpper());
                    edr["PlanMakeDate"] = ERPDt.Rows[j]["PlanMakeDate"].ToString().ToUpper();
                    edr["OrgName"] = ERPDt.Rows[j]["OrgName"].ToString().ToUpper();
                    edr["FinishDate"] = ERPDt.Rows[j]["FinishDate"].ToString().ToUpper();
                    edr["FinishMakeOrderNo"] = ERPDt.Rows[j]["FinishMakeOrderNo"].ToString().ToUpper();
                    edr["ProcessTypeID"] = ERPDt.Rows[j]["ProcessTypeID"].ToString().ToUpper();
                    edr["OrderClass"] = ERPDt.Rows[j]["OrderClass"].ToString().ToUpper();
                    edr["MakeLine"] = ERPDt.Rows[j]["MakeLine"].ToString().ToUpper();
                    edr["ProcessType"] = ERPDt.Rows[j]["ProcessType"].ToString().ToUpper();
                    edr["ProcessName"] = ERPDt.Rows[j]["ProcessName"].ToString().ToUpper();
                    edr["CFFinishQty"] = Convert.ToInt32(ERPDt.Rows[j]["CFFinishQty"].ToString().ToUpper());
                    edr["CFFinishBonus"] = Convert.ToInt32(ERPDt.Rows[j]["CFFinishBonus"].ToString().ToUpper());
                    edr["FinishChecked"] = ERPDt.Rows[j]["FinishChecked"].ToString().ToUpper();
                    edr["MakeOrgName"] = ERPDt.Rows[j]["MakeOrgName"].ToString().ToUpper();

                    ERPUnCorrespondMes.Rows.Add(edr);
                }

            }
            ldt.Add(ProductionStatus);
                ldt.Add(BestUnCorrespondMes);
                ldt.Add(ERPUnCorrespondMes);

                

            return new Tuple<List<DataTable>, int>(ldt, pages);

        }
  
        // 检测过滤自编单号带汉字的
        private bool checkTextCH( string text)
        {
            bool result = true;
            char[] c = text.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] >= 0x4e00 && c[i] <= 0x9fbb)
                {
                    result = true;
                    break;
                }
                else
                {
                    result = false;
                }
            }


/*
                for (int i = 0; i < text.Length; i++)
            {
                if (Regex.IsMatch(text.ToString(), @"[\u4E00-\u9FA5]+$"))
                {
                    result = true;
                    break;
                }

                else
                {
                    result = false;
                }
                   
            }
*/
            return result;
        }

        private bool checkDoubleMy_nos(List<string>  list , string value)
        {
            bool result = true;
            if(list.Count <=0)
            {
                result = false;
            }else{
                foreach (string l in list)
                {
                    if(l == value)
                    {
                        result = true;
                        break;
                    }
                    result = false;
                }
            }
            return result;
        }
        public DataTable getProductionWip(ProductionStatusSearch parameters)
        {
            return pss.getProductionWip(parameters);
        }
    }
}
