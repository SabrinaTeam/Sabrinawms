using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FrmSingleWeightManager
    {
        public FrmSingleWeightService fws = new FrmSingleWeightService();
        public int saveSingleWeightDBtoDatabase(DataTable db)
        {

            if(db.Rows.Count <= 0)
            {
                return 0;
            }


            DataTable insetDB = new DataTable();
            DataColumn custID = new DataColumn();
            custID.ColumnName = "custID";
            insetDB.Columns.Add(custID);


            DataColumn StyleID = new DataColumn();
            StyleID.ColumnName = "StyleID";
            insetDB.Columns.Add(StyleID);


            DataColumn SizeID = new DataColumn();
            SizeID.ColumnName = "SizeID";
            insetDB.Columns.Add(SizeID);

            DataColumn singleWeight = new DataColumn();
            singleWeight.ColumnName = "singleWeight";
            insetDB.Columns.Add(singleWeight);

            DataColumn Note = new DataColumn();
            Note.ColumnName = "Note";
            insetDB.Columns.Add(Note);

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

            DataColumn isDel = new DataColumn();
            isDel.ColumnName = "isDel";
            insetDB.Columns.Add(isDel);

            /*****************************************************/
            DataTable updataDB = new DataTable();
            DataColumn NID = new DataColumn();
            NID.ColumnName = "NID";
            NID.DefaultValue = -1;
            updataDB.Columns.Add(NID);



            DataColumn NcustID = new DataColumn();
            NcustID.ColumnName = "NcustID";
            updataDB.Columns.Add(NcustID);


            DataColumn NStyleID = new DataColumn();
            NStyleID.ColumnName = "NStyleID";
            updataDB.Columns.Add(NStyleID);


            DataColumn NSizeID = new DataColumn();
            NSizeID.ColumnName = "NSizeID";
            updataDB.Columns.Add(NSizeID);

            DataColumn NsingleWeight = new DataColumn();
            NsingleWeight.ColumnName = "NsingleWeight";
            updataDB.Columns.Add(NsingleWeight);

            DataColumn NNote = new DataColumn();
            NNote.ColumnName = "NNote";
            updataDB.Columns.Add(NNote);

            DataColumn NCreateUser = new DataColumn();
            NCreateUser.ColumnName = "NCreateUser";
            updataDB.Columns.Add(NCreateUser);

            DataColumn NCreateDate = new DataColumn();
            NCreateDate.ColumnName = "NCreateDate";
            updataDB.Columns.Add(NCreateDate);

            DataColumn NLastModified = new DataColumn();
            NLastModified.ColumnName = "NLastModified";
            updataDB.Columns.Add(NLastModified);

            DataColumn NLastModifyDate = new DataColumn();
            NLastModifyDate.ColumnName = "NLastModifyDate";
            updataDB.Columns.Add(NLastModifyDate);

            DataColumn NisDel = new DataColumn();
            NisDel.ColumnName = "isDel";
            updataDB.Columns.Add(NisDel);


            for (int i = 0; i < db.Rows.Count; i++)
            {
                // -1 需要新增 没有ID
                if(db.Rows[i]["ID"].ToString() == "-1")
                {
                    DataRow dr = insetDB.NewRow();

                    dr["custID"] = db.Rows[i]["custID"].ToString().ToUpper();
                    dr["StyleID"] = db.Rows[i]["StyleID"].ToString().ToUpper();
                    dr["SizeID"] = db.Rows[i]["SizeID"].ToString().ToUpper();
                    dr["singleWeight"] = db.Rows[i]["singleWeight"].ToString().ToUpper();
                    dr["Note"] = db.Rows[i]["Note"].ToString().ToUpper();
                    dr["CreateUser"] = db.Rows[i]["CreateUser"].ToString().ToUpper();
                    dr["CreateDate"] = db.Rows[i]["CreateDate"].ToString();

                    dr["LastModified"] = db.Rows[i]["LastModified"].ToString().ToUpper();
                    dr["LastModifyDate"] = db.Rows[i]["LastModifyDate"].ToString().ToUpper();
                    dr["isDel"] = "0";
                    insetDB.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = updataDB.NewRow();
                    dr["NID"] = db.Rows[i]["ID"].ToString().ToUpper();
                    dr["NcustID"] = db.Rows[i]["custID"].ToString().ToUpper();
                    dr["NStyleID"] = db.Rows[i]["StyleID"].ToString().ToUpper();
                    dr["NSizeID"] = db.Rows[i]["SizeID"].ToString().ToUpper();
                    dr["NsingleWeight"] = db.Rows[i]["singleWeight"].ToString().ToUpper();
                    dr["NNote"] = db.Rows[i]["Note"].ToString().ToUpper();
                    dr["NCreateUser"] = db.Rows[i]["CreateUser"].ToString().ToUpper();
                    dr["NCreateDate"] = db.Rows[i]["CreateDate"].ToString().ToUpper();
                    dr["NLastModified"] = db.Rows[i]["LastModified"].ToString().ToUpper();
                    dr["NLastModifyDate"] = db.Rows[i]["LastModifyDate"].ToString().ToUpper();
                    dr["isDel"] ="0";
                    updataDB.Rows.Add(dr);

                }

            }
            int insetRows =  this.insetToDb(insetDB);
            int updateRows = this.updateToDb(updataDB);

            return insetRows + updateRows;


        }
        public int insetToDb(DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            return fws.insetRowsToDb(dt);

        }
        public int updateToDb(DataTable dt)
        {
            if(dt.Rows.Count <= 0)
            {
                return 0;
            }
            return fws.updateRowsToDb(dt);

        }

        public DataTable getSingleWeights(string custid, string styleid)
        {
            if ( custid == "" && styleid == "") return null;
            return fws.getSingleWeights(custid, styleid);
        }

        public int isDelRows(int id)
        {
            return fws.isDelRows(id);
        }
    }
}
