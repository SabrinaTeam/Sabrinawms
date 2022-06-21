using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace BLL
{
    public class NIKEImportManager
    {
        public NikeImportService NikeImport = new NikeImportService();

        public List<DataTable> getPODataFromScanService(string org, string PONumber, string main_line,int type)
        {
            DataTable MercuryDb;
            if (type == 0)
            {
                 MercuryDb = NikeImport.getPODataFromScanService(org, PONumber, main_line);
            }
            else
            {
                 MercuryDb = NikeImport.getDateFromScanService(org, PONumber, main_line);
            }

            /*****************con_pprDB**********************/

            DataTable con_pprDB = new DataTable();
            DataColumn dc;
            dc = new DataColumn();
            dc.ColumnName = "id";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Cust_id";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Serial_From";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "qty";
            dc.DataType = System.Type.GetType("System.Int32");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "org";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "PPrfNo";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "count1";
            dc.DataType = System.Type.GetType("System.Int32");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "create_pc";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "update_date";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_no";
            dc.DataType = System.Type.GetType("System.Int32");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "country_code";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_to";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Pkg_Code";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Scan_ID";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Net_Net";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_net";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_Gross";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_L";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_W";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_H";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "b_Volume";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "PO";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "MAIN_LINE";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            /**************************************************/

            /*****************con_detailDB**********************/

            DataTable con_detailDB = new DataTable();
            DataColumn dcd;

            dcd = new DataColumn();
            dcd.ColumnName = "id";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Cust_id";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "org";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Serial_From";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Buyer_Item";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Item_desc";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "color_code";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Size1";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "con_Qty";
            dcd.DataType = System.Type.GetType("System.Int32");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "qty";
            dcd.DataType = System.Type.GetType("System.Int32");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "pprfno";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);


            /************************* CON_TO *********************************/
            DataTable CON_TODB = new DataTable();
            DataColumn ct;

            ct = new DataColumn();
            ct.ColumnName = "id";
            ct.DataType = System.Type.GetType("System.String");
            CON_TODB.Columns.Add(ct);

            ct = new DataColumn();
            ct.ColumnName = "con_to";
            ct.DataType = System.Type.GetType("System.String");
            CON_TODB.Columns.Add(ct);

            /*******************************************************/
            DataTable PO_MIANLINE_DB = new DataTable();
            DataColumn PC;

            PC = new DataColumn();
            PC.ColumnName = "id";
            PC.DataType = System.Type.GetType("System.String");
            PO_MIANLINE_DB.Columns.Add(PC);

            PC = new DataColumn();
            PC.ColumnName = "po";
            PC.DataType = System.Type.GetType("System.String");
            PO_MIANLINE_DB.Columns.Add(PC);

            PC = new DataColumn();
            PC.ColumnName = "main_line";
            PC.DataType = System.Type.GetType("System.String");
            PO_MIANLINE_DB.Columns.Add(PC);

            /******************************************************/
            string size = "";
            string Pkg_Code = "";
            string qtys = "";
            string lastsize = "";
            string lastPkg_Code = "";
            string lastqtys = "";

            string po = "";
            string mainline = "";
            string lastpo = "";
            string lastmainline = "";
            int c = 1;


            if (MercuryDb.Rows.Count > 0)
            {
                for (int i = 0; i < MercuryDb.Rows.Count; i++)
                {
                    size = MercuryDb.Rows[i]["J_3ASIZE"].ToString();
                    Pkg_Code = MercuryDb.Rows[i]["Pkg_Code"].ToString();
                    qtys = MercuryDb.Rows[i]["FFS_CRTN_QTY"].ToString();
                    po = MercuryDb.Rows[i]["EBELN"].ToString();
                  //  po = MercuryDb.Rows[i]["PO_REF"].ToString();
                    mainline = MercuryDb.Rows[i]["EBELP"].ToString();

                    if ( i == 0)
                    {
                        lastsize = MercuryDb.Rows[i ]["J_3ASIZE"].ToString();
                        lastPkg_Code = MercuryDb.Rows[i ]["Pkg_Code"].ToString();
                        lastqtys = MercuryDb.Rows[i]["FFS_CRTN_QTY"].ToString();

                        lastpo = MercuryDb.Rows[i]["EBELN"].ToString();
                      //  lastpo = MercuryDb.Rows[i]["PO_REF"].ToString();
                        lastmainline = MercuryDb.Rows[i]["EBELP"].ToString();

                    }
                    else
                    {
                        lastsize = MercuryDb.Rows[i - 1]["J_3ASIZE"].ToString();
                        lastPkg_Code = MercuryDb.Rows[i - 1]["Pkg_Code"].ToString();
                        lastqtys = MercuryDb.Rows[i-1]["FFS_CRTN_QTY"].ToString();

                        lastpo = MercuryDb.Rows[i - 1]["EBELN"].ToString();
                      //  lastpo = MercuryDb.Rows[i-1]["PO_REF"].ToString();
                        lastmainline = MercuryDb.Rows[i-1]["EBELP"].ToString();

                    }

                    string sp = size + Pkg_Code + qtys;
                    string lsp = lastsize + lastPkg_Code + lastqtys ;

                    if (sp != lsp)
                    {
                        DataRow dr = CON_TODB.NewRow();
                        dr["id"] = c;
                        dr["con_to"] = MercuryDb.Rows[i-1]["VENUM"].ToString();
                        c++;
                        CON_TODB.Rows.Add(dr);
                    }
                    /************************************/

                    string pm = po + mainline ;
                    string lspm = lastpo + lastmainline  ;

                    if (pm != lspm)
                    {
                        DataRow dr = PO_MIANLINE_DB.NewRow();
                        dr["id"] = c;

                          dr["po"] = MercuryDb.Rows[i - 1]["EBELN"].ToString();
                       // dr["po"] = MercuryDb.Rows[i - 1]["PO_REF"].ToString();
                        dr["main_line"] = MercuryDb.Rows[i - 1]["EBELP"].ToString();
                        c++;
                        PO_MIANLINE_DB.Rows.Add(dr);
                        //MercuryDb.Rows[0]["EBELN"].ToString() + MercuryDb.Rows[0]["EBELP"].ToString() ==  MercuryDb.Rows[MercuryDb.Rows.Count-1]["EBELN"].ToString() + MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString()
                    }
                   // else if (  MercuryDb.Rows[0]["PO_REF"].ToString() + MercuryDb.Rows[0]["EBELP"].ToString() ==
                     //          MercuryDb.Rows[MercuryDb.Rows.Count-1]["PO_REF"].ToString() + MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString())

                      else if (MercuryDb.Rows[0]["EBELN"].ToString() + MercuryDb.Rows[0]["EBELP"].ToString() ==
                               MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELN"].ToString() + MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString())

                            {
                        DataRow dr = PO_MIANLINE_DB.NewRow();
                        dr["id"] = c;
                        dr["po"] = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELN"].ToString();
                      //  dr["po"] = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["PO_REF"].ToString();
                        dr["main_line"] = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString();
                        c++;
                        PO_MIANLINE_DB.Rows.Add(dr);
                    }
                }

                DataRow drc = CON_TODB.NewRow();
                drc["id"] = c;
                drc["con_to"] = MercuryDb.Rows[MercuryDb.Rows.Count -1]["VENUM"].ToString();
                CON_TODB.Rows.Add(drc);
                int conrow = 0;

                if (PO_MIANLINE_DB.Rows.Count <= 0)
                {
                    return null;
                }
                string podbpo = PO_MIANLINE_DB.Rows[PO_MIANLINE_DB.Rows.Count - 1]["po"].ToString();
                string podbmain_line = PO_MIANLINE_DB.Rows[PO_MIANLINE_DB.Rows.Count - 1]["main_line"].ToString();


                if (MercuryDb.Rows.Count <= 0)
                {
                    return null;
                }

                 //string Mdbpo = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["PO_REF"].ToString();
                 string Mdbpo = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELN"].ToString();
                string Mdbmain_line = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString();

                if(podbpo != Mdbpo || podbmain_line != Mdbmain_line)
                {
                    DataRow dr = PO_MIANLINE_DB.NewRow();
                    dr["id"] = c;
                    dr["po"] = Mdbpo;
                    dr["main_line"] = Mdbmain_line;
                    c++;
                    PO_MIANLINE_DB.Rows.Add(dr);
                }

                /********************************************/
                for (int i = 0; i < MercuryDb.Rows.Count; i++)
                {


                    int count =  MercuryDb.Select("EBELN = " + MercuryDb.Rows[i]["EBELN"].ToString() +
                       " and  EBELP = " + MercuryDb.Rows[i]["EBELP"].ToString() +
                       " and  ETENR = " + MercuryDb.Rows[i]["ETENR"].ToString()).Length;
                  //  int count = MercuryDb.Select("PO_REF = " + MercuryDb.Rows[i]["PO_REF"].ToString() + " and  EBELP = " + MercuryDb.Rows[i]["EBELP"].ToString() + " and  ETENR = " + MercuryDb.Rows[i]["ETENR"].ToString()).Length;

                    DataRow dr = con_pprDB.NewRow();

                    dr["id"] = "NIKE-" + MercuryDb.Rows[i]["VENUM"].ToString() +
                        // MercuryDb.Rows[i]["ETENR"].ToString() +
                        MercuryDb.Rows[i]["EBELN"].ToString().Substring(0, 2) +
                        MercuryDb.Rows[i]["PPrfNo"].ToString().Substring(MercuryDb.Rows[i]["PPrfNo"].ToString().Length - 4, 4);


                    dr["Cust_id"] = "NIKE";
                    dr["Serial_From"] = MercuryDb.Rows[i]["VENUM"].ToString();
                    dr["qty"] = Convert.ToInt32( MercuryDb.Rows[i]["countQty"]);
                    dr["org"] = org;
                    dr["PPrfNo"] = MercuryDb.Rows[i]["PPrfNo"].ToString();
                    dr["count1"] = count;
                    dr["create_pc"] = Dns.GetHostName().ToString().ToUpper();
                    dr["update_date"] = System.DateTime.Now.ToString("yyyy-MM-dd");
                    dr["con_no"] = MercuryDb.Rows[i]["VENUM"].ToString();
                    dr["country_code"] = MercuryDb.Rows[i]["EAN11"].ToString();

                    if(conrow != 0)
                    {
                        size = MercuryDb.Rows[i]["J_3ASIZE"].ToString();
                        Pkg_Code = MercuryDb.Rows[i]["Pkg_Code"].ToString();
                        qtys = MercuryDb.Rows[i]["FFS_CRTN_QTY"].ToString();


                        lastsize = MercuryDb.Rows[i-1]["J_3ASIZE"].ToString();
                        lastPkg_Code = MercuryDb.Rows[i-1]["Pkg_Code"].ToString();
                        lastqtys = MercuryDb.Rows[i - 1]["FFS_CRTN_QTY"].ToString();


                        if ((size + Pkg_Code + qtys  ) != (lastsize + lastPkg_Code + lastqtys ))
                        {
                            conrow++;
                        }
                        dr["con_to"] = CON_TODB.Rows[conrow-1]["con_to"].ToString();
                    }
                    else
                    {
                        dr["con_to"] = CON_TODB.Rows[conrow]["con_to"].ToString();
                        conrow++;
                    }

                    dr["Pkg_Code"] = MercuryDb.Rows[i]["Pkg_Code"].ToString();
                    dr["Scan_ID"] = MercuryDb.Rows[i]["EXIDV"].ToString();
                    dr["Net_Net"] = "0";
                    dr["con_net"] = MercuryDb.Rows[i]["NTGEW"].ToString();
                    dr["con_Gross"] = MercuryDb.Rows[i]["BRGEW"].ToString();
                    string con_L = MercuryDb.Rows[i]["FFS_LENGTH_OUTER"].ToString()  ;
                    if (con_L.Length <= 0)
                    {
                        con_L = "0";
                    }

                    dr["con_L"] = Convert.ToDouble(con_L) / 100;

                    string con_W = MercuryDb.Rows[i]["FFS_WIDTH_OUTER"].ToString();
                    if (con_W.Length <= 0)
                    {
                        con_W = "0";
                    }
                    dr["con_W"] = Convert.ToDouble(con_W) / 100;

                    string con_H = MercuryDb.Rows[i]["FFS_HEIGHT_OUTER"].ToString();
                    if (con_H.Length <= 0)
                    {
                        con_H = "0";
                    }
                    dr["con_H"] = Convert.ToDouble(con_H) /100;

                    dr["b_Volume"] = Convert.ToDouble(con_L) * Convert.ToDouble(con_W) * Convert.ToDouble(con_H) / 1000000;

                    dr["PO"] = MercuryDb.Rows[i]["EBELN"].ToString();
                    dr["MAIN_LINE"] = MercuryDb.Rows[i]["EBELP"].ToString();
                    con_pprDB.Rows.Add(dr);


                    DataRow cdr = con_detailDB.NewRow();
                    cdr["id"] = "NIKE-" + MercuryDb.Rows[i]["VENUM"].ToString() +
                       // MercuryDb.Rows[i]["ETENR"].ToString() +
                       MercuryDb.Rows[i]["EBELN"].ToString().Substring(0, 2) +
                       MercuryDb.Rows[i]["PPrfNo"].ToString().Substring(MercuryDb.Rows[i]["PPrfNo"].ToString().Length - 4, 4);


                    cdr["Cust_id"] = "NIKE" ;
                    cdr["org"] = org;
                    cdr["Serial_From"] = MercuryDb.Rows[i]["VENUM"].ToString();
                    cdr["Buyer_Item"] = MercuryDb.Rows[i]["MATNR"].ToString().Split('-')[0].ToString();
                    cdr["Item_desc"] = MercuryDb.Rows[i]["MATNR"].ToString();
                    cdr["color_code"] = MercuryDb.Rows[i]["MATNR"].ToString().Split('-')[1].ToString();
                    cdr["Size1"] = MercuryDb.Rows[i]["J_3ASIZE"].ToString();
                    cdr["con_Qty"] = Convert.ToInt32( MercuryDb.Rows[i]["countQty"]);
                    cdr["qty"] = Convert.ToInt32(MercuryDb.Rows[i]["FFS_CRTN_QTY"]);
                    cdr["pprfno"] = MercuryDb.Rows[i]["PPrfNo"].ToString();
                    con_detailDB.Rows.Add(cdr);
                }
            }
            List<DataTable> ld = new List<DataTable>();
            ld.Add(con_pprDB);
            ld.Add(con_detailDB);

            return ld;
        }




        public List<DataTable> getPODataFromScanService2(string org, string PONumber, string main_line, int type)
        {
            DataTable MercuryDb;
            //先查询 成品扫描里PO 为空的条码号
            DataTable fsgTags = NikeImport.getFsgTagNumber();
            if (fsgTags.Rows.Count <= 0)
            {
                return null;
            }
            string tags = "";
            for (int i = 0; i < fsgTags.Rows.Count; i++)
            {
                string tagnumber = fsgTags.Rows[i]["tagnumber"].ToString();
                if (tagnumber.Length > 0)
                {
                    tags = tags + "'" + tagnumber + "',";
                }


            }
            tags = tags.Substring(0, tags.Length - 1);

            MercuryDb = NikeImport.getDateFromScanService2(org, tags);


            /*****************con_pprDB**********************/

            DataTable con_pprDB = new DataTable();
            DataColumn dc;
            dc = new DataColumn();
            dc.ColumnName = "id";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Cust_id";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Serial_From";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "qty";
            dc.DataType = System.Type.GetType("System.Int32");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "org";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "PPrfNo";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "count1";
            dc.DataType = System.Type.GetType("System.Int32");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "create_pc";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "update_date";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_no";
            dc.DataType = System.Type.GetType("System.Int32");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "country_code";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_to";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Pkg_Code";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Scan_ID";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Net_Net";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_net";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_Gross";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_L";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_W";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "con_H";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "b_Volume";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "PO";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "MAIN_LINE";
            dc.DataType = System.Type.GetType("System.String");
            con_pprDB.Columns.Add(dc);

            /**************************************************/

            /*****************con_detailDB**********************/

            DataTable con_detailDB = new DataTable();
            DataColumn dcd;

            dcd = new DataColumn();
            dcd.ColumnName = "id";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Cust_id";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "org";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Serial_From";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Buyer_Item";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Item_desc";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "color_code";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "Size1";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "con_Qty";
            dcd.DataType = System.Type.GetType("System.Int32");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "qty";
            dcd.DataType = System.Type.GetType("System.Int32");
            con_detailDB.Columns.Add(dcd);

            dcd = new DataColumn();
            dcd.ColumnName = "pprfno";
            dcd.DataType = System.Type.GetType("System.String");
            con_detailDB.Columns.Add(dcd);


            /************************* CON_TO *********************************/
            DataTable CON_TODB = new DataTable();
            DataColumn ct;

            ct = new DataColumn();
            ct.ColumnName = "id";
            ct.DataType = System.Type.GetType("System.String");
            CON_TODB.Columns.Add(ct);

            ct = new DataColumn();
            ct.ColumnName = "con_to";
            ct.DataType = System.Type.GetType("System.String");
            CON_TODB.Columns.Add(ct);

            /*******************************************************/
            DataTable PO_MIANLINE_DB = new DataTable();
            DataColumn PC;

            PC = new DataColumn();
            PC.ColumnName = "id";
            PC.DataType = System.Type.GetType("System.String");
            PO_MIANLINE_DB.Columns.Add(PC);

            PC = new DataColumn();
            PC.ColumnName = "po";
            PC.DataType = System.Type.GetType("System.String");
            PO_MIANLINE_DB.Columns.Add(PC);

            PC = new DataColumn();
            PC.ColumnName = "main_line";
            PC.DataType = System.Type.GetType("System.String");
            PO_MIANLINE_DB.Columns.Add(PC);

            /******************************************************/
            string size = "";
            string Pkg_Code = "";
            string qtys = "";
            string lastsize = "";
            string lastPkg_Code = "";
            string lastqtys = "";

            string po = "";
            string mainline = "";
            string lastpo = "";
            string lastmainline = "";
            int c = 1;


            if (MercuryDb.Rows.Count > 0)
            {
                for (int i = 0; i < MercuryDb.Rows.Count; i++)
                {
                    size = MercuryDb.Rows[i]["J_3ASIZE"].ToString();
                    Pkg_Code = MercuryDb.Rows[i]["Pkg_Code"].ToString();
                    qtys = MercuryDb.Rows[i]["FFS_CRTN_QTY"].ToString();
                    po = MercuryDb.Rows[i]["EBELN"].ToString();
                    // po = MercuryDb.Rows[i]["PO_REF"].ToString();
                    mainline = MercuryDb.Rows[i]["EBELP"].ToString();

                    if (i == 0)
                    {
                        lastsize = MercuryDb.Rows[i]["J_3ASIZE"].ToString();
                        lastPkg_Code = MercuryDb.Rows[i]["Pkg_Code"].ToString();
                        lastqtys = MercuryDb.Rows[i]["FFS_CRTN_QTY"].ToString();

                        lastpo = MercuryDb.Rows[i]["EBELN"].ToString();
                        // lastpo = MercuryDb.Rows[i]["PO_REF"].ToString();
                        lastmainline = MercuryDb.Rows[i]["EBELP"].ToString();

                    }
                    else
                    {
                        lastsize = MercuryDb.Rows[i - 1]["J_3ASIZE"].ToString();
                        lastPkg_Code = MercuryDb.Rows[i - 1]["Pkg_Code"].ToString();
                        lastqtys = MercuryDb.Rows[i - 1]["FFS_CRTN_QTY"].ToString();

                        lastpo = MercuryDb.Rows[i - 1]["EBELN"].ToString();
                        // lastpo = MercuryDb.Rows[i - 1]["PO_REF"].ToString();
                        lastmainline = MercuryDb.Rows[i - 1]["EBELP"].ToString();

                    }

                    string sp = size + Pkg_Code + qtys;
                    string lsp = lastsize + lastPkg_Code + lastqtys;

                    if (sp != lsp)
                    {
                        DataRow dr = CON_TODB.NewRow();
                        dr["id"] = c;
                        dr["con_to"] = MercuryDb.Rows[i - 1]["VENUM"].ToString();
                        c++;
                        CON_TODB.Rows.Add(dr);
                    }
                    /************************************/

                    string pm = po + mainline;
                    string lspm = lastpo + lastmainline;

                    if (pm != lspm)
                    {
                        DataRow dr = PO_MIANLINE_DB.NewRow();
                        dr["id"] = c;

                        dr["po"] = MercuryDb.Rows[i - 1]["EBELN"].ToString();
                        //dr["po"] = MercuryDb.Rows[i - 1]["PO_REF"].ToString();
                        dr["main_line"] = MercuryDb.Rows[i - 1]["EBELP"].ToString();
                        c++;
                        PO_MIANLINE_DB.Rows.Add(dr);
                        //MercuryDb.Rows[0]["EBELN"].ToString() + MercuryDb.Rows[0]["EBELP"].ToString() ==  MercuryDb.Rows[MercuryDb.Rows.Count-1]["EBELN"].ToString() + MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString()
                    }
                    /*
                     else if (MercuryDb.Rows[0]["PO_REF"].ToString() + MercuryDb.Rows[0]["EBELP"].ToString() ==
                         MercuryDb.Rows[MercuryDb.Rows.Count - 1]["PO_REF"].ToString() + MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString())
                    */

                    else if (MercuryDb.Rows[0]["EBELN"].ToString() + MercuryDb.Rows[0]["EBELP"].ToString() ==
                   MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELN"].ToString() + MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString())
                    {
                        DataRow dr = PO_MIANLINE_DB.NewRow();
                        dr["id"] = c;
                        dr["po"] = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELN"].ToString();
                        //dr["po"] = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["PO_REF"].ToString();
                        dr["main_line"] = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString();
                        c++;
                        PO_MIANLINE_DB.Rows.Add(dr);
                    }
                }

                DataRow drc = CON_TODB.NewRow();
                drc["id"] = c;
                drc["con_to"] = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["VENUM"].ToString();
                CON_TODB.Rows.Add(drc);
                int conrow = 0;

                if (PO_MIANLINE_DB.Rows.Count <= 0)
                {
                    return null;
                }
                string podbpo = PO_MIANLINE_DB.Rows[PO_MIANLINE_DB.Rows.Count - 1]["po"].ToString();
                string podbmain_line = PO_MIANLINE_DB.Rows[PO_MIANLINE_DB.Rows.Count - 1]["main_line"].ToString();


                if (MercuryDb.Rows.Count <= 0)
                {
                    return null;
                }

                //  string Mdbpo = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["PO_REF"].ToString();
                string Mdbpo = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELN"].ToString();
                string Mdbmain_line = MercuryDb.Rows[MercuryDb.Rows.Count - 1]["EBELP"].ToString();

                if (podbpo != Mdbpo || podbmain_line != Mdbmain_line)
                {
                    DataRow dr = PO_MIANLINE_DB.NewRow();
                    dr["id"] = c;
                    dr["po"] = Mdbpo;
                    dr["main_line"] = Mdbmain_line;
                    c++;
                    PO_MIANLINE_DB.Rows.Add(dr);
                }

                /********************************************/
                for (int i = 0; i < MercuryDb.Rows.Count; i++)
                {
                    int count = MercuryDb.Select("EBELN = " + MercuryDb.Rows[i]["EBELN"].ToString() + " and  EBELP = " + MercuryDb.Rows[i]["EBELP"].ToString() + " and  ETENR = " + MercuryDb.Rows[i]["ETENR"].ToString()).Length;
                    //  int count = MercuryDb.Select("PO_REF = " + MercuryDb.Rows[i]["PO_REF"].ToString() + " and  EBELP = " + MercuryDb.Rows[i]["EBELP"].ToString() + " and  ETENR = " + MercuryDb.Rows[i]["ETENR"].ToString()).Length;

                    DataRow dr = con_pprDB.NewRow();
                    /*   dr["id"] = "NIKE-" + MercuryDb.Rows[i]["VENUM"].ToString() +
                         MercuryDb.Rows[i]["ETENR"].ToString() +
                         MercuryDb.Rows[i]["PO_REF"].ToString().Substring(0, 2) +
                         MercuryDb.Rows[i]["PPrfNo"].ToString().Substring(MercuryDb.Rows[i]["PPrfNo"].ToString().Length - 4, 4);
                    */


                    dr["id"] = "NIKE-" + MercuryDb.Rows[i]["VENUM"].ToString() +
                    // MercuryDb.Rows[i]["ETENR"].ToString() +
                    MercuryDb.Rows[i]["EBELN"].ToString().Substring(0, 2) +
                    MercuryDb.Rows[i]["PPrfNo"].ToString().Substring(MercuryDb.Rows[i]["PPrfNo"].ToString().Length - 4, 4);

                    //  dr["id"] = "NIKE-" + MercuryDb.Rows[i]["VENUM"].ToString() + MercuryDb.Rows[i]["ETENR"].ToString();
                    dr["Cust_id"] = "NIKE";
                    dr["Serial_From"] = MercuryDb.Rows[i]["VENUM"].ToString();
                    dr["qty"] = Convert.ToInt32(MercuryDb.Rows[i]["FFS_CRTN_QTY"]);
                    dr["org"] = org;
                    dr["PPrfNo"] = MercuryDb.Rows[i]["PPrfNo"].ToString();
                    dr["count1"] = count;
                    dr["create_pc"] = Dns.GetHostName().ToString().ToUpper();
                    dr["update_date"] = System.DateTime.Now.ToString("yyyy-MM-dd");
                    dr["con_no"] = MercuryDb.Rows[i]["VENUM"].ToString();
                    dr["country_code"] = MercuryDb.Rows[i]["EAN11"].ToString();

                    if (conrow != 0)
                    {
                        size = MercuryDb.Rows[i]["J_3ASIZE"].ToString();
                        Pkg_Code = MercuryDb.Rows[i]["Pkg_Code"].ToString();
                        qtys = MercuryDb.Rows[i]["FFS_CRTN_QTY"].ToString();


                        lastsize = MercuryDb.Rows[i - 1]["J_3ASIZE"].ToString();
                        lastPkg_Code = MercuryDb.Rows[i - 1]["Pkg_Code"].ToString();
                        lastqtys = MercuryDb.Rows[i - 1]["FFS_CRTN_QTY"].ToString();


                        if ((size + Pkg_Code + qtys) != (lastsize + lastPkg_Code + lastqtys))
                        {
                            conrow++;
                        }
                        dr["con_to"] = CON_TODB.Rows[conrow - 1]["con_to"].ToString();
                    }
                    else
                    {
                        dr["con_to"] = CON_TODB.Rows[conrow]["con_to"].ToString();
                        conrow++;
                    }

                    dr["Pkg_Code"] = MercuryDb.Rows[i]["Pkg_Code"].ToString();
                    dr["Scan_ID"] = MercuryDb.Rows[i]["EXIDV"].ToString();
                    dr["Net_Net"] = "0";
                    dr["con_net"] = MercuryDb.Rows[i]["NTGEW"].ToString();
                    dr["con_Gross"] = MercuryDb.Rows[i]["BRGEW"].ToString();
                    //  buyer.Guid = (Guid)SqlHelper.FromDbValue(dr["Guid"]);

                    dr["con_L"] = Convert.ToDouble(MercuryDb.Rows[i]["FFS_LENGTH_OUTER"].ToString()) / 100;
                    dr["con_W"] = Convert.ToDouble(MercuryDb.Rows[i]["FFS_WIDTH_OUTER"].ToString()) / 100;
                    dr["con_H"] = Convert.ToDouble(MercuryDb.Rows[i]["FFS_HEIGHT_OUTER"].ToString()) / 100;
                    dr["b_Volume"] = Convert.ToDouble(MercuryDb.Rows[i]["FFS_LENGTH_OUTER"].ToString()) *
                                     Convert.ToDouble(MercuryDb.Rows[i]["FFS_WIDTH_OUTER"].ToString()) *
                                     Convert.ToDouble(MercuryDb.Rows[i]["FFS_HEIGHT_OUTER"].ToString()) / 1000000;


                    //  dr["PO"] = MercuryDb.Rows[i]["PO_REF"].ToString();
                    dr["PO"] = MercuryDb.Rows[i]["EBELN"].ToString();
                    dr["MAIN_LINE"] = MercuryDb.Rows[i]["EBELP"].ToString();
                    con_pprDB.Rows.Add(dr);


                    DataRow cdr = con_detailDB.NewRow();
                    /*  dr["id"] = "NIKE-" + MercuryDb.Rows[i]["VENUM"].ToString() +
                        MercuryDb.Rows[i]["ETENR"].ToString() +
                        MercuryDb.Rows[i]["PO_REF"].ToString().Substring(0, 2) +
                        MercuryDb.Rows[i]["PPrfNo"].ToString().Substring(MercuryDb.Rows[i]["PPrfNo"].ToString().Length - 4, 4);
                    */

                    cdr["id"] = "NIKE-" + MercuryDb.Rows[i]["VENUM"].ToString() +
                     // MercuryDb.Rows[i]["ETENR"].ToString() +
                     MercuryDb.Rows[i]["EBELN"].ToString().Substring(0, 2) +
                     MercuryDb.Rows[i]["PPrfNo"].ToString().Substring(MercuryDb.Rows[i]["PPrfNo"].ToString().Length - 4, 4);

                    //  cdr["id"] = "NIKE-" + MercuryDb.Rows[i]["VENUM"].ToString() + MercuryDb.Rows[i]["ETENR"].ToString();
                    cdr["Cust_id"] = "NIKE";
                    cdr["org"] = org;
                    cdr["Serial_From"] = MercuryDb.Rows[i]["VENUM"].ToString();
                    cdr["Buyer_Item"] = MercuryDb.Rows[i]["MATNR"].ToString().Split('-')[0].ToString();
                    cdr["Item_desc"] = MercuryDb.Rows[i]["MATNR"].ToString();
                    cdr["color_code"] = MercuryDb.Rows[i]["MATNR"].ToString().Split('-')[1].ToString();
                    cdr["Size1"] = MercuryDb.Rows[i]["J_3ASIZE"].ToString();
                    cdr["con_Qty"] = Convert.ToInt32(MercuryDb.Rows[i]["countQty"]);
                    cdr["qty"] = Convert.ToInt32(MercuryDb.Rows[i]["FFS_CRTN_QTY"]);
                    cdr["pprfno"] = MercuryDb.Rows[i]["PPrfNo"].ToString();
                    con_detailDB.Rows.Add(cdr);
                }
            }
            List<DataTable> ld = new List<DataTable>();
            ld.Add(con_pprDB);
            ld.Add(con_detailDB);

            return ld;
        }

        public int insetOrUpdataConDetail(DataTable dt)
        {
            /*
            string ids = "";
            foreach (DataRow row in dt.Rows)
            {
                string id = row["id"].ToString();
                if (id.Length > 0)
                {
                    ids = ids + "'" + row["id"].ToString() + "',";
                }


            }
            if (ids.Length >0)
            {
                ids = ids.Substring(0, ids.Length - 1);
            }

            // SELECT  id,Cust_id,Serial_From,Buyer_Item,Item_desc,color_code,Size1,con_Qty,qty,pprfno from con_detail  WHERE PPrfNo ='79795851196'

            DataTable result = NikeImport.getNikeDataFromFsgByConDetailIds(ids);
            DataTable upDataDt = new DataTable();
            upDataDt.Columns.Add("id", typeof(string));
            upDataDt.Columns.Add("Cust_id", typeof(string));
            upDataDt.Columns.Add("Serial_From", typeof(string));
            upDataDt.Columns.Add("Buyer_Item", typeof(string));
            upDataDt.Columns.Add("Item_desc", typeof(string));
            upDataDt.Columns.Add("color_code", typeof(string));
            upDataDt.Columns.Add("Size1", typeof(string));
            upDataDt.Columns.Add("con_Qty", typeof(string));
            upDataDt.Columns.Add("qty", typeof(int));
            upDataDt.Columns.Add("pprfno", typeof(string));


            DataTable addDataDt = new DataTable();
            addDataDt.Columns.Add("id", typeof(string));
            addDataDt.Columns.Add("Cust_id", typeof(string));
            addDataDt.Columns.Add("Serial_From", typeof(string));
            addDataDt.Columns.Add("Buyer_Item", typeof(string));
            addDataDt.Columns.Add("Item_desc", typeof(string));
            addDataDt.Columns.Add("color_code", typeof(string));
            addDataDt.Columns.Add("Size1", typeof(string));
            addDataDt.Columns.Add("con_Qty", typeof(string));
            addDataDt.Columns.Add("qty", typeof(int));
            addDataDt.Columns.Add("pprfno", typeof(string));
            if (result.Rows.Count <= 0)
            {
                // 小于0 全部要新增
                // 大于0 部分新增   部分更新
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = addDataDt.NewRow();

                    dr["id"] = dt.Rows[i]["id"].ToString();
                    dr["Cust_id"] = dt.Rows[i]["Cust_id"].ToString();
                    dr["Serial_From"] = dt.Rows[i]["Serial_From"].ToString();
                    dr["Buyer_Item"] = dt.Rows[i]["Buyer_Item"].ToString();
                    dr["Item_desc"] = dt.Rows[i]["Item_desc"].ToString();
                    dr["color_code"] = dt.Rows[i]["color_code"].ToString();
                    dr["Size1"] = dt.Rows[i]["Size1"].ToString();
                    dr["con_Qty"] = dt.Rows[i]["con_Qty"].ToString();
                    dr["qty"] = dt.Rows[i]["qty"].ToString();
                    dr["pprfno"] = dt.Rows[i]["pprfno"].ToString();
                    addDataDt.Rows.Add(dr);
                }
            }
            else
            {
                // 大于0 部分新增   部分更新
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < result.Rows.Count; j++)
                    {
                        // 相同ID部分 更新
                        if (result.Rows[j]["id"].ToString() == dt.Rows[i]["id"].ToString())
                        {
                            DataRow dr = upDataDt.NewRow();
                            dr["id"] = dt.Rows[i]["id"].ToString();
                            dr["Cust_id"] = dt.Rows[i]["Cust_id"].ToString();
                            dr["Serial_From"] = dt.Rows[i]["Serial_From"].ToString();
                            dr["Buyer_Item"] = dt.Rows[i]["Buyer_Item"].ToString();
                            dr["Item_desc"] = dt.Rows[i]["Item_desc"].ToString();
                            dr["color_code"] = dt.Rows[i]["color_code"].ToString();
                            dr["Size1"] = dt.Rows[i]["Size1"].ToString();
                            dr["con_Qty"] = dt.Rows[i]["con_Qty"].ToString();
                            dr["qty"] = dt.Rows[i]["qty"].ToString();
                            dr["pprfno"] = dt.Rows[i]["pprfno"].ToString();
                            upDataDt.Rows.Add(dr);
                        }
                    }
                }
                //删除相同的部分
                DataTable diffDt = dt.Copy();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    for (int j = 0; j < diffDt.Rows.Count; j++)
                    {
                        if (result.Rows[i]["id"].ToString() == diffDt.Rows[j]["id"].ToString())
                        {
                            diffDt.Rows.RemoveAt(j);
                            j--;
                        }
                    }
                }

                if (diffDt.Rows.Count > 0)
                {
                    for (int i = 0; i < diffDt.Rows.Count; i++)
                    {
                        DataRow dr = addDataDt.NewRow();
                        dr["id"] = diffDt.Rows[i]["id"].ToString();
                        dr["Cust_id"] = diffDt.Rows[i]["Cust_id"].ToString();
                        dr["Serial_From"] = diffDt.Rows[i]["Serial_From"].ToString();
                        dr["Buyer_Item"] = diffDt.Rows[i]["Buyer_Item"].ToString();
                        dr["Item_desc"] = diffDt.Rows[i]["Item_desc"].ToString();
                        dr["color_code"] = diffDt.Rows[i]["color_code"].ToString();
                        dr["Size1"] = diffDt.Rows[i]["Size1"].ToString();
                        dr["con_Qty"] = diffDt.Rows[i]["con_Qty"].ToString();
                        dr["qty"] = diffDt.Rows[i]["qty"].ToString();
                        dr["pprfno"] = diffDt.Rows[i]["pprfno"].ToString();
                        addDataDt.Rows.Add(dr);
                    }
                }
            }
            int ups = 0;
            int insets = 0;
            if (upDataDt.Rows.Count > 0)
            {

                ups = NikeImport.UpdataNikeDataToFsgConDetail(upDataDt);
            }
            if (addDataDt.Rows.Count > 0)
            {
                insets = NikeImport.insetNikeDataToFsgConDetail(addDataDt);
            }
            return ups + insets;

            */
            int insets = NikeImport.insetNikeDataToFsgConDetail(dt);
            return  insets;
        }

        public int insetOrUpdataConPpr(DataTable dt)
        {
            /*
            string ids = "";
            foreach (DataRow row in dt.Rows)
            {

                string id = row["id"].ToString();
                if (id.Length > 0)
                {
                    ids = ids + "'" + row["id"].ToString() + "',";
                }
            }
            if (ids.Length > 0)
            {
                ids = ids.Substring(0, ids.Length - 1);
            }

           // ids = ids.Substring(0, ids.Length - 1);
            DataTable result = NikeImport.getNikeDataFromFsgByConpprIds(ids);  // 已有的箱号
            DataTable upDataDt = new DataTable();
            upDataDt.Columns.Add("id", typeof(string));
            upDataDt.Columns.Add("Cust_id", typeof(string));
            upDataDt.Columns.Add("Serial_From", typeof(string));
            upDataDt.Columns.Add("qty", typeof(string));
            upDataDt.Columns.Add("org", typeof(string));
            upDataDt.Columns.Add("PPrfNo", typeof(string));
            upDataDt.Columns.Add("count1", typeof(int));
            upDataDt.Columns.Add("create_pc", typeof(string));
            upDataDt.Columns.Add("update_date", typeof(string));
            upDataDt.Columns.Add("con_no", typeof(int));
            upDataDt.Columns.Add("country_code", typeof(string));
            upDataDt.Columns.Add("con_to", typeof(int));
            upDataDt.Columns.Add("Pkg_Code", typeof(string));
            upDataDt.Columns.Add("Scan_ID", typeof(string));
            upDataDt.Columns.Add("Net_Net", typeof(double));
            upDataDt.Columns.Add("Con_Net", typeof(double));
            upDataDt.Columns.Add("con_Gross", typeof(double));
            upDataDt.Columns.Add("con_l", typeof(double));
            upDataDt.Columns.Add("con_W", typeof(double));
            upDataDt.Columns.Add("con_H", typeof(double));
            upDataDt.Columns.Add("b_Volume", typeof(double));
            upDataDt.Columns.Add("PO", typeof(string));
            upDataDt.Columns.Add("MAIN_LINE", typeof(string));


            DataTable addDataDt = new DataTable();
            addDataDt.Columns.Add("id", typeof(string));
            addDataDt.Columns.Add("Cust_id", typeof(string));
            addDataDt.Columns.Add("Serial_From", typeof(string));
            addDataDt.Columns.Add("qty", typeof(string));
            addDataDt.Columns.Add("org", typeof(string));
            addDataDt.Columns.Add("PPrfNo", typeof(string));
            addDataDt.Columns.Add("count1", typeof(int));
            addDataDt.Columns.Add("create_pc", typeof(string));
            addDataDt.Columns.Add("update_date", typeof(string));
            addDataDt.Columns.Add("con_no", typeof(int));
            addDataDt.Columns.Add("country_code", typeof(string));
            addDataDt.Columns.Add("con_to", typeof(int));
            addDataDt.Columns.Add("Pkg_Code", typeof(string));
            addDataDt.Columns.Add("Scan_ID", typeof(string));
            addDataDt.Columns.Add("Net_Net", typeof(double));
            addDataDt.Columns.Add("Con_Net", typeof(double));
            addDataDt.Columns.Add("con_Gross", typeof(double));
            addDataDt.Columns.Add("con_l", typeof(double));
            addDataDt.Columns.Add("con_W", typeof(double));
            addDataDt.Columns.Add("con_H", typeof(double));
            addDataDt.Columns.Add("b_Volume", typeof(double));
            addDataDt.Columns.Add("PO", typeof(string));
            addDataDt.Columns.Add("MAIN_LINE", typeof(string));
            if (result.Rows.Count <= 0)
            {
                // 小于0 全部要新增
                // 大于0 部分新增   部分更新
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = addDataDt.NewRow();
                    dr["id"] =  dt.Rows[i]["id"].ToString();
                    dr["Cust_id"] = dt.Rows[i]["Cust_id"].ToString();
                    dr["Serial_From"] = dt.Rows[i]["Serial_From"].ToString();
                    dr["qty"] = dt.Rows[i]["qty"].ToString();
                    dr["org"] = dt.Rows[i]["org"].ToString();
                    dr["PPrfNo"] = dt.Rows[i]["PPrfNo"].ToString();
                    dr["count1"] = dt.Rows[i]["count1"].ToString();
                    dr["create_pc"] = dt.Rows[i]["create_pc"].ToString();
                    dr["update_date"] = dt.Rows[i]["update_date"].ToString();
                    dr["con_no"] = Convert.ToInt32(dt.Rows[i]["con_no"]);
                    dr["country_code"] = dt.Rows[i]["country_code"].ToString();
                    dr["con_to"] = Convert.ToInt32(dt.Rows[i]["con_to"]);
                    dr["Pkg_Code"] = dt.Rows[i]["Pkg_Code"].ToString();
                    dr["Scan_ID"] = dt.Rows[i]["Scan_ID"].ToString();
                    dr["Net_Net"] = Convert.ToDouble(dt.Rows[i]["Net_Net"]);
                    dr["Con_Net"] = Convert.ToDouble(dt.Rows[i]["con_net"]);
                    dr["con_Gross"] = 0.00;
                    dr["con_l"] = Convert.ToDouble(dt.Rows[i]["con_L"]);
                    dr["con_W"] = Convert.ToDouble(dt.Rows[i]["con_W"]);
                    dr["con_H"] = Convert.ToDouble(dt.Rows[i]["con_H"]);
                    dr["b_Volume"] = Convert.ToDouble(dt.Rows[i]["b_Volume"]);
                    dr["PO"] = dt.Rows[i]["PO"].ToString();
                    dr["MAIN_LINE"] = dt.Rows[i]["MAIN_LINE"].ToString();
                    addDataDt.Rows.Add(dr);
                }
            }
            else
            {

                // 太慢 去掉循环
                // 大于0 部分新增   部分更新
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < result.Rows.Count; j++)
                    {

                        //
                        // 相同ID部分 更新
                        if (result.Rows[j]["id"].ToString() ==   dt.Rows[i]["id"].ToString())
                        {
                            // id,Barcode,CartonNo,PackQty,Style,Size,Color,MasterPO,StyleDescription
                            DataRow dr = upDataDt.NewRow();
                            dr["id"] = dt.Rows[i]["id"].ToString();
                            dr["Cust_id"] = dt.Rows[i]["Cust_id"].ToString();
                            dr["Serial_From"] = dt.Rows[i]["Serial_From"].ToString();
                            dr["qty"] = dt.Rows[i]["qty"].ToString();
                            dr["org"] = dt.Rows[i]["org"].ToString();
                            dr["PPrfNo"] = dt.Rows[i]["PPrfNo"].ToString();
                            dr["count1"] = dt.Rows[i]["count1"].ToString();
                            dr["create_pc"] = dt.Rows[i]["create_pc"].ToString();
                            dr["update_date"] = dt.Rows[i]["update_date"].ToString();
                            dr["con_no"] = Convert.ToInt32(dt.Rows[i]["con_no"]);
                            dr["country_code"] = dt.Rows[i]["country_code"].ToString();
                            dr["con_to"] = Convert.ToInt32(dt.Rows[i]["con_to"]);
                            dr["Pkg_Code"] = dt.Rows[i]["Pkg_Code"].ToString();
                            dr["Scan_ID"] = dt.Rows[i]["Scan_ID"].ToString();
                            dr["Net_Net"] = Convert.ToDouble(dt.Rows[i]["Net_Net"]);
                            dr["Con_Net"] = Convert.ToDouble(dt.Rows[i]["con_net"]);
                            dr["con_Gross"] = 0.00;
                            dr["con_l"] = Convert.ToDouble(dt.Rows[i]["con_L"]);
                            dr["con_W"] = Convert.ToDouble(dt.Rows[i]["con_W"]);
                            dr["con_H"] = Convert.ToDouble(dt.Rows[i]["con_H"]);
                            dr["b_Volume"] = Convert.ToDouble(dt.Rows[i]["b_Volume"]);
                            dr["PO"] = dt.Rows[i]["PO"].ToString();
                            dr["MAIN_LINE"] = dt.Rows[i]["MAIN_LINE"].ToString();
                            upDataDt.Rows.Add(dr);
                        }
                    }
                }
                //删除相同的部分
                DataTable diffDt = new DataTable();
                diffDt = dt.Copy();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    for (int j = 0; j < diffDt.Rows.Count; j++)
                    {
                        if (result.Rows[i]["id"].ToString() ==   diffDt.Rows[j]["id"].ToString())
                        {
                            diffDt.Rows.RemoveAt(j);
                            j--;
                        }
                    }
                }

                if (diffDt.Rows.Count > 0)
                {
                    for (int i = 0; i < diffDt.Rows.Count; i++)
                    {
                        DataRow dr = addDataDt.NewRow();
                        dr["id"] = diffDt.Rows[i]["id"].ToString();
                        dr["Cust_id"] = diffDt.Rows[i]["Cust_id"].ToString();
                        dr["Serial_From"] = diffDt.Rows[i]["Serial_From"].ToString();
                        dr["qty"] = diffDt.Rows[i]["qty"].ToString();
                        dr["org"] = diffDt.Rows[i]["org"].ToString();
                        dr["PPrfNo"] = diffDt.Rows[i]["PPrfNo"].ToString();
                        dr["count1"] = diffDt.Rows[i]["count1"].ToString();
                        dr["create_pc"] = diffDt.Rows[i]["create_pc"].ToString();
                        dr["update_date"] = diffDt.Rows[i]["update_date"].ToString();
                        dr["con_no"] = Convert.ToInt32(diffDt.Rows[i]["con_no"]);
                        dr["country_code"] = diffDt.Rows[i]["country_code"].ToString();
                        dr["con_to"] = Convert.ToInt32(diffDt.Rows[i]["con_to"]);
                        dr["Pkg_Code"] = diffDt.Rows[i]["Pkg_Code"].ToString();
                        dr["Scan_ID"] = diffDt.Rows[i]["Scan_ID"].ToString();
                        dr["Net_Net"] = Convert.ToDouble(diffDt.Rows[i]["Net_Net"]);
                        dr["Con_Net"] = Convert.ToDouble(diffDt.Rows[i]["con_net"]);
                        dr["con_Gross"] = 0.00;
                        dr["con_l"] = Convert.ToDouble(diffDt.Rows[i]["con_L"]);
                        dr["con_W"] = Convert.ToDouble(diffDt.Rows[i]["con_W"]);
                        dr["con_H"] = Convert.ToDouble(diffDt.Rows[i]["con_H"]);
                        dr["b_Volume"] = Convert.ToDouble(diffDt.Rows[i]["b_Volume"]);
                        dr["PO"] = diffDt.Rows[i]["PO"].ToString();
                        dr["MAIN_LINE"] = diffDt.Rows[i]["MAIN_LINE"].ToString();
                        addDataDt.Rows.Add(dr);
                    }
                }
            }
            int ups = 0;
            int insets = 0;

            string[] strComuns = {
                "id",
                "Cust_id",
                "Serial_From",
                "qty",
                "org",
                "PPrfNo",
                "count1",
                "create_pc",
                "update_date",
                "con_no",
                "country_code",

                "con_to",
                "Pkg_Code",
                "Scan_ID",
                "Net_Net",
                "Con_Net",
                "con_Gross",
                "con_l",
                "con_W",
                "con_H",
                "b_Volume",
                "PO",
                "MAIN_LINE"
            };



            if (upDataDt.Rows.Count > 0)
            {
                DataView myDataView = new DataView(upDataDt);
               DataTable newUpDataDt = myDataView.ToTable(true, strComuns);
                ups = NikeImport.UpdataNikeDataToFsgConppr(newUpDataDt);
                //ups = NikeImport.UpdataNikeDataToFsgConppr(upDataDt);
            }
            if (addDataDt.Rows.Count > 0)
            {
                DataView myDataView = new DataView(addDataDt);
                DataTable newAddDataDt = myDataView.ToTable(true, strComuns);
                insets = NikeImport.insetNikeDataToFsgConppr(newAddDataDt);
               // insets = NikeImport.insetNikeDataToFsgConppr(addDataDt);
            }
              return ups + insets;
            */
            int insets = NikeImport.insetNikeDataToFsgConppr(dt);
            return  insets;
        }
    }
}