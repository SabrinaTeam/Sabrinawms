using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class FrmBoxWeightManager
    {
        FrmBoxWeightService fbs = new FrmBoxWeightService();
        public DataTable getAllCustidByBaseDB(string linkServer)
        {
            DataTable db = fbs.getAllCustidByBaseDB();

            return db;

        }
        public DataTable getAllBoxNames( )
        {
            DataTable db = fbs.getAllBoxNames();

            return db;

        }


        public int saveSingleWeightDBtoDatabase(DataTable db)
        {

            if (db.Rows.Count <= 0)
            {
                return 0;
            }


            DataTable insetDB = new DataTable();
            DataColumn box_name = new DataColumn();
            box_name.ColumnName = "box_name";
            insetDB.Columns.Add(box_name);


            DataColumn box_weight = new DataColumn();
            box_weight.ColumnName = "box_weight";
            insetDB.Columns.Add(box_weight);

            DataColumn box_l = new DataColumn();
            box_l.ColumnName = "box_l";
            insetDB.Columns.Add(box_l);

            DataColumn box_w = new DataColumn();
            box_w.ColumnName = "box_w";
            insetDB.Columns.Add(box_w);

            DataColumn box_h = new DataColumn();
            box_h.ColumnName = "box_h";
            insetDB.Columns.Add(box_h);

            DataColumn cust_id = new DataColumn();
            cust_id.ColumnName = "cust_id";
            insetDB.Columns.Add(cust_id);

            DataColumn season = new DataColumn();
            season.ColumnName = "season";
            insetDB.Columns.Add(season);

            DataColumn Remark = new DataColumn();
            Remark.ColumnName = "Remark";
            insetDB.Columns.Add(Remark);

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

            DataColumn Nbox_name = new DataColumn();
            Nbox_name.ColumnName = "box_name";
            updataDB.Columns.Add(Nbox_name);


            DataColumn Nbox_weight = new DataColumn();
            Nbox_weight.ColumnName = "box_weight";
            updataDB.Columns.Add(Nbox_weight);

            DataColumn Nbox_l = new DataColumn();
            Nbox_l.ColumnName = "box_l";
            updataDB.Columns.Add(Nbox_l);

            DataColumn Nbox_w = new DataColumn();
            Nbox_w.ColumnName = "box_w";
            updataDB.Columns.Add(Nbox_w);

            DataColumn Nbox_h = new DataColumn();
            Nbox_h.ColumnName = "box_h";
            updataDB.Columns.Add(Nbox_h);

            DataColumn Ncust_id = new DataColumn();
            Ncust_id.ColumnName = "cust_id";
            updataDB.Columns.Add(Ncust_id);

            DataColumn Nseason = new DataColumn();
            Nseason.ColumnName = "season";
            updataDB.Columns.Add(Nseason);

            DataColumn NRemark = new DataColumn();
            NRemark.ColumnName = "Remark";
            updataDB.Columns.Add(NRemark);

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
                    dr["box_name"] = db.Rows[i]["box_name"].ToString().ToUpper();
                    string  boxWeight = db.Rows[i]["box_weight"].ToString().ToUpper();
                    if (!IsUnsign(boxWeight))
                    {
                        boxWeight = "0";
                    }
                    dr["box_weight"] = boxWeight;
                    dr["box_l"] = db.Rows[i]["box_l"].ToString().ToUpper();
                    dr["box_w"] = db.Rows[i]["box_w"].ToString().ToUpper();
                    dr["box_h"] = db.Rows[i]["box_h"].ToString().ToUpper();
                    dr["cust_id"] = db.Rows[i]["cust_id"].ToString().ToUpper();
                    dr["season"] = db.Rows[i]["season"].ToString().ToUpper();
                    dr["Remark"] = db.Rows[i]["Remark"].ToString().ToUpper();
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
                    dr["box_name"] = db.Rows[i]["box_name"].ToString().ToUpper();
                    string boxWeight = db.Rows[i]["box_weight"].ToString().ToUpper();
                    if (!IsUnsign(boxWeight))
                    {
                        boxWeight = "0";
                    }
                    dr["box_weight"] = boxWeight;
                    //dr["box_weight"] = db.Rows[i]["box_weight"].ToString().ToUpper();
                    dr["box_l"] = db.Rows[i]["box_l"].ToString().ToUpper();
                    dr["box_w"] = db.Rows[i]["box_w"].ToString().ToUpper();
                    dr["box_h"] = db.Rows[i]["box_h"].ToString().ToUpper();
                    dr["cust_id"] = db.Rows[i]["cust_id"].ToString().ToUpper();
                    dr["season"] = db.Rows[i]["season"].ToString().ToUpper();
                    dr["Remark"] = db.Rows[i]["Remark"].ToString().ToUpper();
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

        public static bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^/d*[.]?/d*$");
        }
        public int insetToDb(DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            return fbs.insetRowsToDb(dt);

        }
        public int updateToDb(DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            return fbs.updateRowsToDb(dt);

        }

        public DataTable getBoxWeights(string custid, string boxName)
        {
            if (custid == "" && boxName == "") return null;
            return fbs.getBoxWeights(custid, boxName);
        }

        public int isDelRows(int id)
        {
            return fbs.isDelRows(id);
        }
    }
}
