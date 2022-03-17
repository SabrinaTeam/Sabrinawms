using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BLL
{
    public class ShippingPackagesManager
    {
        private ShippingPackagesServer sps = new ShippingPackagesServer();

        public List<DataTable> ExcelRead(String filename, string sheetname, string Org, string headno)
        {
            COMMON.NPOIExcelShippingPackages NPOIexcel = new COMMON.NPOIExcelShippingPackages();
            Spks spks = NPOIexcel.ExcelRead(filename, sheetname, Org, headno);
            if (spks.sppack == null)
            {
                return null;
            }
            ShippingPackages[] shippingPackages = spks.sppack;
            //创建本地表
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("isCancel", typeof(string)); // 取消PO
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("ftyNo", typeof(string));
            table.Columns.Add("season", typeof(string));
            table.Columns.Add("BVPO", typeof(string));
            table.Columns.Add("masterPO", typeof(string));
            table.Columns.Add("GtnPO", typeof(string));
            table.Columns.Add("Modify", typeof(string)); // 修改PO 旧的PO号 需要删除的

            table.Columns.Add("po_mainLine", typeof(string));
            table.Columns.Add("styleNumber", typeof(string));
            table.Columns.Add("styleName", typeof(string));

            table.Columns.Add("color", typeof(string));
            table.Columns.Add("colDescription", typeof(string));
            table.Columns.Add("totalQty", typeof(string));
            table.Columns.Add("overflow", typeof(string));  // 溢出数量
            table.Columns.Add("channel", typeof(string));
            table.Columns.Add("HOD", typeof(string));
            table.Columns.Add("befoeHOD", typeof(string));
            table.Columns.Add("newHOD", typeof(string));
            table.Columns.Add("shipMode", typeof(string));

            table.Columns.Add("sourceTag", typeof(string));
            table.Columns.Add("wwwt", typeof(string));
            table.Columns.Add("citHangTag", typeof(string));
            table.Columns.Add("Fastener", typeof(string));
            table.Columns.Add("steelNumber", typeof(string));
            table.Columns.Add("cup", typeof(string));
            table.Columns.Add("cclable", typeof(string));
            table.Columns.Add("sensitive", typeof(string));
            table.Columns.Add("remark", typeof(string));
            table.Columns.Add("org", typeof(string));

            ShippingPackageSizes[] shippingPackageSizes = spks.spsize;
            //创建本地表
            DataTable Sizetable = new DataTable();
            Sizetable.Columns.Add("id", typeof(int));
            Sizetable.Columns.Add("isCancel", typeof(string)); // 取消PO
            Sizetable.Columns.Add("ftyNO", typeof(string));
            Sizetable.Columns.Add("season", typeof(string));
            Sizetable.Columns.Add("masterPO", typeof(string));
            Sizetable.Columns.Add("gtnPO", typeof(string));
            Sizetable.Columns.Add("Modify", typeof(string)); // 修改PO 旧的PO号 需要删除的
            Sizetable.Columns.Add("po_MainLine", typeof(string));
            Sizetable.Columns.Add("styleNumber", typeof(string));
            Sizetable.Columns.Add("color", typeof(string));
            Sizetable.Columns.Add("sizeName", typeof(string));
            Sizetable.Columns.Add("sizeAnother", typeof(string));
            Sizetable.Columns.Add("sizeQty", typeof(string));
            Sizetable.Columns.Add("poQty", typeof(string));
            Sizetable.Columns.Add("overflow", typeof(string)); // 溢出数量
            Sizetable.Columns.Add("org", typeof(string));

            try
            {
                for (int i = 0; i < shippingPackages.Length; i++)
                {
                    if (Convert.ToString(shippingPackages[i].masterPO).Length <= 0 &&
                        Convert.ToString(shippingPackages[i].GtnPO).Length <= 0)
                    {
                        continue;
                    }

                    int nid = -1;
                    string NisCancel = Convert.ToString(shippingPackages[i].isCancel);
                    string ntype = Convert.ToString(shippingPackages[i].type);
                    string nftyNo = Convert.ToString(shippingPackages[i].ftyNo);
                    string nseason = Convert.ToString(shippingPackages[i].season);
                    string nBVPO = Convert.ToString(shippingPackages[i].BVPO);
                    string nmasterPO = Convert.ToString(shippingPackages[i].masterPO);
                    string nGtnPO = Convert.ToString(shippingPackages[i].GtnPO);
                    string nModify = Convert.ToString(shippingPackages[i].Modify);
                    string npo_mainLine = Convert.ToString(shippingPackages[i].po_mainLine);
                    string nstyleNumber = Convert.ToString(shippingPackages[i].styleNumber);
                    string nstyleName = Convert.ToString(shippingPackages[i].styleName);

                    string ncolor = Convert.ToString(shippingPackages[i].color);
                    string ncolDescription = Convert.ToString(shippingPackages[i].colDescription);
                    string nchannel = Convert.ToString(shippingPackages[i].channel);
                    string ntotalQty = Convert.ToString(shippingPackages[i].totalQty);
                    string noverflow = Convert.ToString(shippingPackages[i].overflow);
                    string nHOD = Convert.ToString(shippingPackages[i].HOD);
                    string nbefoeHOD = Convert.ToString(shippingPackages[i].befoeHOD);
                    string nnewHOD = Convert.ToString(shippingPackages[i].newHOD);
                    string nshipMode = Convert.ToString(shippingPackages[i].shipMode);

                    string nsourceTag = Convert.ToString(shippingPackages[i].sourceTag);
                    string nwwwt = Convert.ToString(shippingPackages[i].wwwt);
                    string ncitHangTag = Convert.ToString(shippingPackages[i].citHangTag);
                    string nFastener = Convert.ToString(shippingPackages[i].Fastener);
                    string nsteelNumber = Convert.ToString(shippingPackages[i].steelNumber);
                    string ncup = Convert.ToString(shippingPackages[i].cup);
                    string ncclable = Convert.ToString(shippingPackages[i].cclable);
                    string nsensitive = Convert.ToString(shippingPackages[i].sensitive);
                    string nremark = Convert.ToString(shippingPackages[i].remark);
                    string norg = Org;

                    //本地表加入数据  Unique
                    DataRow row = table.NewRow();
                    row["id"] = nid;
                    row["isCancel"] = NisCancel;
                    row["type"] = ntype;
                    row["ftyNo"] = nftyNo;
                    row["season"] = nseason;
                    row["BVPO"] = nBVPO;
                    row["masterPO"] = nmasterPO;
                    row["GtnPO"] = nGtnPO;
                    row["Modify"] = nModify;
                    row["po_mainLine"] = npo_mainLine;
                    row["styleNumber"] = nstyleNumber;
                    row["styleName"] = nstyleName;

                    row["color"] = ncolor;
                    row["colDescription"] = ncolDescription;
                    row["channel"] = nchannel;
                    row["totalQty"] = ntotalQty;
                    row["overflow"] = noverflow;
                    row["HOD"] = nHOD.Length > 10 ? nHOD.Substring(0, 10) : nHOD;
                    row["befoeHOD"] = nbefoeHOD.Length > 10 ? nbefoeHOD.Substring(0, 10) : nbefoeHOD;
                    row["newHOD"] = nnewHOD.Length > 10 ? nnewHOD.Substring(0, 10) : nnewHOD;
                    row["shipMode"] = nshipMode;

                    row["sourceTag"] = nsourceTag;
                    row["wwwt"] = nwwwt;
                    row["citHangTag"] = ncitHangTag;
                    row["Fastener"] = nFastener;
                    row["steelNumber"] = nsteelNumber;
                    row["cup"] = ncup;
                    row["cclable"] = ncclable;
                    row["sensitive"] = nsensitive;
                    row["remark"] = nremark;
                    row["org"] = norg;
                    table.Rows.Add(row);
                }

                /***********************************************/
                for (int i = 0; i < shippingPackageSizes.Length; i++)
                {
                    if (Convert.ToString(shippingPackageSizes[i].masterPO).Length <= 0 &&
                       Convert.ToString(shippingPackageSizes[i].gtnPO).Length <= 0)
                    {
                        continue;
                    }
                    int sid = -1;
                    string sisCancel = Convert.ToString(shippingPackageSizes[i].isCancel);
                    string sftyNo = Convert.ToString(shippingPackageSizes[i].ftyNO);
                    string sseason = Convert.ToString(shippingPackageSizes[i].season);
                    string sMasterPO = Convert.ToString(shippingPackageSizes[i].masterPO);
                    string sGtnPO = Convert.ToString(shippingPackageSizes[i].gtnPO);
                    string sModify = Convert.ToString(shippingPackageSizes[i].Modify);
                    string spo_mainLine = Convert.ToString(shippingPackageSizes[i].po_MainLine);
                    string sstyleNumber = Convert.ToString(shippingPackageSizes[i].styleNumber);
                    string scolor = Convert.ToString(shippingPackageSizes[i].color);

                    string ssizeName = Convert.ToString(shippingPackageSizes[i].sizeName);
                    string ssizeAnother = Convert.ToString(shippingPackageSizes[i].sizeAnother);
                    string ssizeQty = Convert.ToString(shippingPackageSizes[i].sizeQty);
                    string spoQty = Convert.ToString(shippingPackageSizes[i].poQty);
                    string soverflow = Convert.ToString(shippingPackageSizes[i].overflow);
                    string sorg = Convert.ToString(shippingPackageSizes[i].org);

                    DataRow SizeRow = Sizetable.NewRow();
                    SizeRow["id"] = sid;
                    SizeRow["isCancel"] = sisCancel;
                    SizeRow["ftyNO"] = sftyNo;
                    SizeRow["season"] = sseason;
                    SizeRow["masterPO"] = sMasterPO;
                    SizeRow["gtnPO"] = sGtnPO;
                    SizeRow["Modify"] = sModify;
                    SizeRow["po_MainLine"] = spo_mainLine;
                    SizeRow["styleNumber"] = sstyleNumber;
                    SizeRow["color"] = scolor;
                    SizeRow["sizeName"] = ssizeName;
                    SizeRow["sizeAnother"] = ssizeAnother;
                    SizeRow["sizeQty"] = ssizeQty;
                    SizeRow["poQty"] = spoQty;
                    SizeRow["overflow"] = soverflow;
                    SizeRow["org"] = sorg;

                    Sizetable.Rows.Add(SizeRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            List<DataTable> ldt = new List<DataTable>();
            ldt.Add(table);
            ldt.Add(Sizetable);
            return ldt;
        }

        public int inset_sp_temp(DataTable sp_temp)
        {
            string results = sps.insetShippingPackages_sp_temp(sp_temp);
            int result = 0;
            if (results == "200")
            {
                result = sp_temp.Rows.Count;
            }
            return result;
        }

        public int inset_Sizesp_temp(DataTable spSize_temp)
        {
            string results = sps.insetShippingPackageSizes_sp_temp(spSize_temp);
            int result = 0;
            if (results == "200")
            {
                result = spSize_temp.Rows.Count;
            }
            return result;
        }

        public int clear_sp_temp()
        {
            return sps.clear_sp_temp();
        }
        public int clear_spError()
        {
            return sps.clear_spError();
        }

        public int clear_spsize_temp()
        {
            return sps.clear_spsize_temp();
        }
        public int clear_spsizeError()
        {
            return sps.clear_spsizeError();
        }

        public int updata_T_sp()
        {
            return sps.updata_T_sp();
        }

        public int updata_T_spIsCancel()
        {
            return sps.updata_T_spIsCancel();
        }

        public int updata_T_spSize()
        {
            return sps.updata_T_spSize();
        }

        public int adddata_T_sp()
        {
            return sps.adddata_T_sp();
        }

        public int adddata_T_spSize()
        {
            return sps.adddata_T_spSize();
        }

        public DataTable getShippingPackagesBySparameters(ShippingParameter sparameters)
        {
            DataTable dt = sps.getShippingPackagesBySparameters(sparameters);

            return dt;
        }

        public DataTable getShippingPackagesStatus(ShippingParameter sparameters)
        {
            DataTable dt = sps.getShippingPackagesBySparameters(sparameters);
            if (dt.Rows.Count > 0)
            {
                //ShippingPackageStatus
                List<ShippingPackageStatusParameter> lspsps = new List<ShippingPackageStatusParameter>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ShippingPackageStatusParameter spsp = new ShippingPackageStatusParameter();
                    spsp.GtnPO = dt.Rows[i]["GtnPO"].ToString();
                    spsp.po_mainLine = dt.Rows[i]["po_mainLine"].ToString();
                    spsp.styleNumber = dt.Rows[i]["styleNumber"].ToString();
                    spsp.color = dt.Rows[i]["color"].ToString();
                    lspsps.Add(spsp);
                }
                /*获取扫描系统数据*/
                // List<ShippingPackageStatus> lspss = new List<ShippingPackageStatus>();
                DataTable shipPackStatusDT = new DataTable();
                shipPackStatusDT.Columns.Add(new DataColumn("aid", Type.GetType("System.Int32")));
                shipPackStatusDT.Columns.Add(new DataColumn("isCancel", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("type", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("ftyNo", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("season", Type.GetType("System.String")));

                shipPackStatusDT.Columns.Add(new DataColumn("masterPO", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("GtnPO", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("Modify", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("po_mainLine", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("HOD", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("befoeHOD", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("newHOD", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("bookingStatus", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("BookingData", Type.GetType("System.String")));

                shipPackStatusDT.Columns.Add(new DataColumn("styleNumber", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("color", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("totalQty", Type.GetType("System.Int32")));
                shipPackStatusDT.Columns.Add(new DataColumn("overflow", Type.GetType("System.Int32")));
                shipPackStatusDT.Columns.Add(new DataColumn("pprfno", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("POqty", Type.GetType("System.Int32")));
                shipPackStatusDT.Columns.Add(new DataColumn("POboxs", Type.GetType("System.Int32")));
                shipPackStatusDT.Columns.Add(new DataColumn("InQty", Type.GetType("System.Int32")));
                shipPackStatusDT.Columns.Add(new DataColumn("InBoxs", Type.GetType("System.Int32")));
                shipPackStatusDT.Columns.Add(new DataColumn("CHQty", Type.GetType("System.Int32")));
                shipPackStatusDT.Columns.Add(new DataColumn("CHBoxs", Type.GetType("System.Int32")));

                shipPackStatusDT.Columns.Add(new DataColumn("con_no", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("CompletionRate", Type.GetType("System.String"))); // 入库比率
                shipPackStatusDT.Columns.Add(new DataColumn("ShipmentRatio", Type.GetType("System.String"))); // 出货比率

                shipPackStatusDT.Columns.Add(new DataColumn("BVPO", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("styleName", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("colDescription", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("channel", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("shipMode", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("sourceTag", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("wwwt", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("citHangTag", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("Fastener", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("steelNumber", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("cup", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("cclable", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("sensitive", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("remark", Type.GetType("System.String")));
                shipPackStatusDT.Columns.Add(new DataColumn("org", Type.GetType("System.String")));

                DataTable ScanDt = sps.getShippingPackageStatusByPO(lspsps);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < ScanDt.Rows.Count; j++)
                    {
                        string shipPo = dt.Rows[i]["GtnPO"].ToString().ToUpper();
                        string shipPo_MainLine = dt.Rows[i]["po_mainLine"].ToString().ToUpper();
                        string shipStyleNumber = dt.Rows[i]["styleNumber"].ToString().ToUpper();
                        string shipColor_code = dt.Rows[i]["color"].ToString().ToUpper();

                        string scanPo = ScanDt.Rows[j]["po"].ToString().ToUpper();
                        string scanPo_MainLine = ScanDt.Rows[j]["MAIN_LINE"].ToString().ToUpper();
                        string scanStyleNumber = ScanDt.Rows[j]["Buyer_Item"].ToString().ToUpper();
                        string scanColor_code = ScanDt.Rows[j]["color_code"].ToString().ToUpper();

                        string totalQty = dt.Rows[i]["totalQty"].ToString();
                        string overflow = dt.Rows[i]["overflow"].ToString();
                        string POqty = ScanDt.Rows[j]["POqty"].ToString();
                        string POboxs = ScanDt.Rows[j]["POboxs"].ToString();
                        string InQty = ScanDt.Rows[j]["InQty"].ToString();
                        string InBoxs = ScanDt.Rows[j]["InBoxs"].ToString();
                        string CHQty = ScanDt.Rows[j]["CHQty"].ToString();
                        string CHBoxs = ScanDt.Rows[j]["CHBoxs"].ToString(); 

                        int tlQty = totalQty.Length <= 0 ? 0 : Convert.ToInt32(totalQty);
                        int tOQty = overflow.Length <= 0 ? 0 : Convert.ToInt32(overflow);

                        int Pqty = POqty.Length <= 0 ? 0 : Convert.ToInt32(POqty);
                        int Pboxs = POboxs.Length <= 0 ? 0 : Convert.ToInt32(POboxs);
                        int IQty = InQty.Length <= 0 ? 0 : Convert.ToInt32(InQty);
                        int IBoxs = InBoxs.Length <= 0 ? 0 : Convert.ToInt32(InBoxs);
                        int CQty = CHQty.Length <= 0 ? 0 : Convert.ToInt32(CHQty);
                        int CBoxs = CHBoxs.Length <= 0 ? 0 : Convert.ToInt32(CHBoxs);

                        if (shipPo == scanPo &&
                            shipPo_MainLine == scanPo_MainLine &&
                            shipStyleNumber == scanStyleNumber &&
                            shipColor_code == scanColor_code
                            )
                        {
                            DataRow dr = shipPackStatusDT.NewRow();
                            dr["aid"] = Convert.ToInt32(dt.Rows[i]["aid"].ToString());
                            dr["isCancel"] = dt.Rows[i]["isCancel"].ToString();
                            dr["type"] = dt.Rows[i]["type"].ToString().ToUpper();
                            dr["ftyNo"] = dt.Rows[i]["ftyNo"].ToString().ToUpper();
                            dr["season"] = dt.Rows[i]["season"].ToString().ToUpper();
                            dr["masterPO"] = dt.Rows[i]["masterPO"].ToString().ToUpper();
                            dr["GtnPO"] = dt.Rows[i]["GtnPO"].ToString().ToUpper();
                            dr["Modify"] = dt.Rows[i]["modifyPO"].ToString().ToUpper();
                            dr["po_mainLine"] = dt.Rows[i]["po_mainLine"].ToString().ToUpper();
                            dr["HOD"] = dt.Rows[i]["HOD"].ToString().ToUpper();
                            dr["befoeHOD"] = dt.Rows[i]["befoeHOD"].ToString().ToUpper();
                            dr["newHOD"] = dt.Rows[i]["newHOD"].ToString().ToUpper();
                            dr["bookingStatus"] = dt.Rows[i]["bookingStatus"].ToString().ToUpper();
                            dr["BookingData"] = dt.Rows[i]["BookingData"].ToString().ToUpper();
                            dr["styleNumber"] = dt.Rows[i]["styleNumber"].ToString().ToUpper();
                            dr["color"] = dt.Rows[i]["color"].ToString().ToUpper();
                            dr["totalQty"] = tlQty;
                            dr["overflow"] = tOQty;
                            dr["pprfno"] = ScanDt.Rows[j]["pprfno"].ToString();
                            dr["POqty"] = Pqty;
                            dr["POboxs"] = Pboxs;
                            dr["InQty"] = IQty;
                            dr["InBoxs"] = IBoxs;
                            dr["CHQty"] = CQty;
                            dr["CHBoxs"] = CBoxs;
                            dr["con_no"] = ScanDt.Rows[j]["con_no"].ToString().ToUpper();

                            if (IQty <= 0)
                            {
                                dr["CompletionRate"] = "0.00";
                            }
                            else
                            {
                                dr["CompletionRate"] = (Convert.ToDouble(IQty) / Convert.ToDouble(Pqty) * 100).ToString("#0.00");
                            }

                            if (CQty <= 0)
                            {
                                dr["ShipmentRatio"] = "0.00";
                            }
                            else
                            {
                                dr["ShipmentRatio"] = (Convert.ToDouble(CQty) / Convert.ToDouble(Pqty) * 100).ToString("#0.00");
                            }

                            dr["BVPO"] = dt.Rows[i]["BVPO"].ToString().ToUpper();
                            dr["styleName"] = dt.Rows[i]["styleName"].ToString().ToUpper();
                            dr["colDescription"] = dt.Rows[i]["colDescription"].ToString().ToUpper();
                            dr["channel"] = dt.Rows[i]["channel"].ToString().ToUpper();
                            dr["shipMode"] = dt.Rows[i]["shipMode"].ToString().ToUpper();
                            dr["sourceTag"] = dt.Rows[i]["sourceTag"].ToString().ToUpper();
                            dr["wwwt"] = dt.Rows[i]["wwwt"].ToString().ToUpper();
                            dr["citHangTag"] = dt.Rows[i]["citHangTag"].ToString().ToUpper();
                            dr["Fastener"] = dt.Rows[i]["Fastener"].ToString().ToUpper();
                            dr["steelNumber"] = dt.Rows[i]["steelNumber"].ToString().ToUpper();
                            dr["cup"] = dt.Rows[i]["cup"].ToString().ToUpper();
                            dr["cclable"] = dt.Rows[i]["cclable"].ToString().ToUpper();
                            dr["sensitive"] = dt.Rows[i]["sensitive"].ToString().ToUpper();
                            dr["remark"] = dt.Rows[i]["remark"].ToString().ToUpper();
                            dr["org"] = dt.Rows[i]["org"].ToString().ToUpper();
                            shipPackStatusDT.Rows.Add(dr);
                        }
                    }
                }
                // 转table
                return shipPackStatusDT;
            }
            return null;
        }

        public int updataShippingPackagesBySparameters()
        {
            int j = sps.updataShippingPackagesBySparameters();
            return j;
        }

        public void clear_T_Booking_temp()
        {
            sps.clear_T_Booking_temp();
        }

        public int updataShippingPackagesBySparameters(DataTable changedShippingBookingStatusDT)
        {
            List<ShippingBookingStatus> sbs = new List<ShippingBookingStatus>();

            for (int i = 0; i < changedShippingBookingStatusDT.Rows.Count; i++)
            {
                ShippingBookingStatus sb = new ShippingBookingStatus();
                if (changedShippingBookingStatusDT.Rows[i][9].ToString() == "Yes")
                {
                    sb.id = Convert.ToInt32(changedShippingBookingStatusDT.Rows[i][1].ToString());
                    sb.BookingStatus = true;
                    sb.BookingData = changedShippingBookingStatusDT.Rows[i][10].ToString();
                }
                else
                {
                    sb.id = Convert.ToInt32(changedShippingBookingStatusDT.Rows[i][1].ToString());
                    sb.BookingStatus = false;
                    sb.BookingData = "";
                }
                sbs.Add(sb);
            }
            int j = sps.updataShippingPackagesBySparameters(sbs);
            return j;
        }

        public DataTable getShippingPackagesSize(string GtnPO, string po_mainLine, string styleNumber, string color)
        {
            return sps.getShippingPackagesSize(GtnPO, po_mainLine, styleNumber, color);
        }

        public string SqlBulkToSQL_T_Booking_temp(DataTable changedShippingBookingStatusDT)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "id";
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "BookingStatus";
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "BookingData";
            dt.Columns.Add(dc);

            for (int i = 0; i < changedShippingBookingStatusDT.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = Convert.ToInt32(changedShippingBookingStatusDT.Rows[i][1].ToString());
                string status = changedShippingBookingStatusDT.Rows[i][9].ToString();
                int s = 0;
                if (status == "Yes")
                {
                    s = 1;
                }
                dr["BookingStatus"] = s;
                dr["BookingData"] = changedShippingBookingStatusDT.Rows[i][10].ToString();
                dt.Rows.Add(dr);
            }
            string results = sps.SqlBulkToSQL_T_Booking_temp(dt);
            return results;
        }
    }
}