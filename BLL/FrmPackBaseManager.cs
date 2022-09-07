using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FrmPackBaseManager
    {
        public FrmPackBaseServer pbs = new FrmPackBaseServer();
        public DataTable getPackBase(string custid, string styleid,string boxName)
        {
            if (custid == "" && styleid == "" && boxName == "") return null;
            return pbs.getPackBase(custid, styleid, boxName);
        }

        public int savePackingBaseDBtoDatabase(DataTable db)
        {

            if (db.Rows.Count <= 0)
            {
                return 0;
            }


            DataTable insetDB = new DataTable();
            DataColumn cust_id = new DataColumn();
            cust_id.ColumnName = "cust_id";
            insetDB.Columns.Add(cust_id);


            DataColumn style_id = new DataColumn();
            style_id.ColumnName = "style_id";
            insetDB.Columns.Add(style_id);

            DataColumn box_name = new DataColumn();
            box_name.ColumnName = "box_name";
            insetDB.Columns.Add(box_name);

            DataColumn sizes = new DataColumn();
            sizes.ColumnName = "sizes";
            insetDB.Columns.Add(sizes);

            DataColumn qtys = new DataColumn();
            qtys.ColumnName = "qtys";
            insetDB.Columns.Add(qtys);

            DataColumn remark = new DataColumn();
            remark.ColumnName = "remark";
            insetDB.Columns.Add(remark);

            DataColumn deductionWeight = new DataColumn();
            deductionWeight.ColumnName = "deductionWeight";
            insetDB.Columns.Add(deductionWeight);

            DataColumn bagWeight = new DataColumn();
            bagWeight.ColumnName = "bagWeight";
            insetDB.Columns.Add(bagWeight);

            DataColumn clapBoardTotalWeight = new DataColumn();
            clapBoardTotalWeight.ColumnName = "clapBoardTotalWeight";
            insetDB.Columns.Add(clapBoardTotalWeight);

            DataColumn accessoriesTotalWeight = new DataColumn();
            accessoriesTotalWeight.ColumnName = "accessoriesTotalWeight";
            insetDB.Columns.Add(accessoriesTotalWeight);

            DataColumn isDel = new DataColumn();
            isDel.ColumnName = "isDel";
            insetDB.Columns.Add(isDel);

            DataColumn CreateUser = new DataColumn();
            CreateUser.ColumnName = "CreateUser";
            insetDB.Columns.Add(CreateUser);

            DataColumn CreateDate = new DataColumn();
            CreateDate.ColumnName = "CreateDate";
            insetDB.Columns.Add(CreateDate);

            DataColumn LastModified = new DataColumn();
            LastModified.ColumnName = "LastModified";
            insetDB.Columns.Add(LastModified);

            DataColumn LastModifyDate = new DataColumn();
            LastModifyDate.ColumnName = "LastModifyDate";
            insetDB.Columns.Add(LastModifyDate);



            /*****************************************************/
            DataTable updataDB = new DataTable();
            DataColumn Nid = new DataColumn();
            Nid.ColumnName = "id";
            updataDB.Columns.Add(Nid);

            DataColumn Ncust_id = new DataColumn();
            Ncust_id.ColumnName = "cust_id";
            updataDB.Columns.Add(Ncust_id);


            DataColumn Nstyle_id = new DataColumn();
            Nstyle_id.ColumnName = "style_id";
            updataDB.Columns.Add(Nstyle_id);

            DataColumn Nbox_name = new DataColumn();
            Nbox_name.ColumnName = "box_name";
            updataDB.Columns.Add(Nbox_name);

            DataColumn Nsizes = new DataColumn();
            Nsizes.ColumnName = "sizes";
            updataDB.Columns.Add(Nsizes);

            DataColumn Nqtys = new DataColumn();
            Nqtys.ColumnName = "qtys";
            updataDB.Columns.Add(Nqtys);

            DataColumn Nremark = new DataColumn();
            Nremark.ColumnName = "remark";
            updataDB.Columns.Add(Nremark);

            DataColumn NdeductionWeight = new DataColumn();
            NdeductionWeight.ColumnName = "deductionWeight";
            updataDB.Columns.Add(NdeductionWeight);

            DataColumn NbagWeight = new DataColumn();
            NbagWeight.ColumnName = "bagWeight";
            updataDB.Columns.Add(NbagWeight);

            DataColumn NclapBoardTotalWeight = new DataColumn();
            NclapBoardTotalWeight.ColumnName = "clapBoardTotalWeight";
            updataDB.Columns.Add(NclapBoardTotalWeight);

            DataColumn NaccessoriesTotalWeight = new DataColumn();
            NaccessoriesTotalWeight.ColumnName = "accessoriesTotalWeight";
            updataDB.Columns.Add(NaccessoriesTotalWeight);

            DataColumn NisDel = new DataColumn();
            NisDel.ColumnName = "isDel";
            updataDB.Columns.Add(NisDel);

            DataColumn NCreateUser = new DataColumn();
            NCreateUser.ColumnName = "CreateUser";
            updataDB.Columns.Add(NCreateUser);

            DataColumn NCreateDate = new DataColumn();
            NCreateDate.ColumnName = "CreateDate";
            updataDB.Columns.Add(NCreateDate);

            DataColumn NLastModified = new DataColumn();
            NLastModified.ColumnName = "LastModified";
            updataDB.Columns.Add(NLastModified);

            DataColumn NLastModifyDate = new DataColumn();
            NLastModifyDate.ColumnName = "LastModifyDate";
            updataDB.Columns.Add(NLastModifyDate);


            for (int i = 0; i < db.Rows.Count; i++)
            {
                // -1 需要新增 没有ID
                if (db.Rows[i]["ID"].ToString() == "-1")
                {
                    DataRow dr = insetDB.NewRow();
                    dr["cust_id"] = db.Rows[i]["cust_id"].ToString().ToUpper();
                    dr["style_id"] = db.Rows[i]["style_id"].ToString().ToUpper();
                    dr["box_name"] = db.Rows[i]["box_name"].ToString().ToUpper();
                    dr["sizes"] = db.Rows[i]["sizes"].ToString().ToUpper();
                    dr["qtys"] = db.Rows[i]["qtys"].ToString().ToUpper();
                    dr["remark"] = db.Rows[i]["remark"].ToString().ToUpper();
                    dr["deductionWeight"] = db.Rows[i]["deductionWeight"].ToString().ToUpper();
                    dr["bagWeight"] = db.Rows[i]["bagWeight"].ToString().ToUpper();
                    dr["clapBoardTotalWeight"] = db.Rows[i]["clapBoardTotalWeight"].ToString().ToUpper();
                    dr["accessoriesTotalWeight"] = db.Rows[i]["accessoriesTotalWeight"].ToString().ToUpper();
                    dr["isDel"] = "0";
                    dr["CreateUser"] = db.Rows[i]["CreateUser"].ToString().ToUpper();
                    dr["CreateDate"] = db.Rows[i]["CreateDate"].ToString();
                    dr["LastModified"] = db.Rows[i]["LastModified"].ToString().ToUpper();
                    dr["LastModifyDate"] = db.Rows[i]["LastModifyDate"].ToString().ToUpper();

                    insetDB.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = updataDB.NewRow();
                    dr["id"] = db.Rows[i]["id"].ToString().ToUpper();
                    dr["cust_id"] = db.Rows[i]["cust_id"].ToString().ToUpper();
                    dr["style_id"] = db.Rows[i]["style_id"].ToString().ToUpper();
                    dr["box_name"] = db.Rows[i]["box_name"].ToString().ToUpper();
                    dr["sizes"] = db.Rows[i]["sizes"].ToString().ToUpper();
                    dr["qtys"] = db.Rows[i]["qtys"].ToString().ToUpper();
                    dr["remark"] = db.Rows[i]["remark"].ToString().ToUpper();
                    dr["deductionWeight"] = db.Rows[i]["deductionWeight"].ToString().ToUpper();
                    dr["bagWeight"] = db.Rows[i]["bagWeight"].ToString().ToUpper();
                    dr["clapBoardTotalWeight"] = db.Rows[i]["clapBoardTotalWeight"].ToString().ToUpper();
                    dr["accessoriesTotalWeight"] = db.Rows[i]["accessoriesTotalWeight"].ToString().ToUpper();
                    dr["isDel"] = "0";
                    dr["CreateUser"] = db.Rows[i]["CreateUser"].ToString().ToUpper();
                    dr["CreateDate"] = db.Rows[i]["CreateDate"].ToString();
                    dr["LastModified"] = db.Rows[i]["LastModified"].ToString().ToUpper();
                    dr["LastModifyDate"] = db.Rows[i]["LastModifyDate"].ToString().ToUpper();
                    updataDB.Rows.Add(dr);

                }

            }
            int insetRows = this.insetToDb(insetDB);
            int updateRows = this.updateToDb(updataDB);

            return insetRows + updateRows;


        }
        public int insetToDb(DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            return pbs.insetRowsToDb(dt);

        }
        public int updateToDb(DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            return pbs.updateRowsToDb(dt);

        }


        public int isDelRows(int id)
        {
            return pbs.isDelRows(id);
        }

        public DataTable getAllCustidByFsgDB( )
        {
            DataTable db = pbs.getAllCustidByFsgDB();
            return db;
        }
        public DataTable getAllStyleIDByFsgDB(string custID)
        {
            DataTable db = pbs.getAllStyleIDByFsgDB(custID);
            return db;
        }
        public DataTable getAllBoxNamesIDByfsgDB(string custID)
        {
            DataTable db = pbs.getAllBoxNamesIDByfsgDB(custID);
            return db;
        }

        public DataTable getAllSizesByStyle(string custId,string styleId)
        {
            DataTable db = pbs.getAllSizesByStyle(custId, styleId);
            return db;
        }
    }
}
