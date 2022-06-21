using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StyleLearningCurveServer
    {
        public List<StyleLearingCurve> getCurveNames()
        {
            string sql = @"SELECT id,
                                   modulusName,
                                   isNewStyle,
                                   Remark
                            FROM dbo.CureNames
                            WHERE isDel = 0;";

            List<StyleLearingCurve> lists = new List<StyleLearingCurve>();
            DataTable modulusNames = IEBOM_SqlHelper.ExcuteTable(sql);
            if (modulusNames.Rows.Count > 0)
            {
                foreach (DataRow row in modulusNames.Rows)
                {
                    StyleLearingCurve c = new StyleLearingCurve();
                    cModulusNames(row, c);
                    lists.Add(c);
                }
            }
            return lists;

        }
        public void cModulusNames(DataRow dr, StyleLearingCurve list)
        {
            list.id = Convert.ToInt32(IEBOM_SqlHelper.FromDbValue(dr["id"]));
            list.modulusName = Convert.ToString(IEBOM_SqlHelper.FromDbValue(dr["modulusName"]));
            list.isNewStyle = Convert.ToBoolean(IEBOM_SqlHelper.FromDbValue(dr["isNewStyle"]));
            list.remark = Convert.ToString(IEBOM_SqlHelper.FromDbValue(dr["Remark"]));
        }

        public string saveLearningCurve(DataTable newDt)
        {
          string result = IEBOM_SqlHelper.SqlBulkCopy(newDt);
            return result;
        }

        public int saveLearningCurveName(string CurveName)
        {
            string sql = @"
                            INSERT INTO	dbo.CureNames
                            (
                               
                                modulusName,
                                isNewStyle,
                                isDel,
                                Remark
                            )
                            VALUES
                            (  
                                 @CurveName, 
                                0, 
                                0, 
                                N''
                                )  SELECT IDENT_CURRENT('CureNames')";

            SqlParameter[] ps =
               {
                new SqlParameter("CurveName", CurveName)
              };
            return IEBOM_SqlHelper.ExcuteScalar<int>(sql, ps);

        }

        public int getLearningByCurveName(string CurveName)
        {
            string sql = @"SELECT id FROM CureNames WHERE modulusName =@CurveName";
            int namesid = -1;

            SqlParameter[] ps =
               {
                new SqlParameter("CurveName", CurveName)
              };
            DataTable dt = IEBOM_SqlHelper.ExcuteTable(sql, ps);
            if(dt.Rows.Count > 0)
            {
                namesid = Convert.ToInt32( dt.Rows[0][0].ToString());
            }
            return namesid;

        }

        public int delStandardModulusByCurveNameID(int CurveNameID)
        {
            string sql = @"UPDATE dbo.StandardModulus SET isDel = 1 WHERE CureNamesID =@CurveNameID";


            SqlParameter[] ps =
               {
                new SqlParameter("CurveNameID", CurveNameID)
              };
            int result = IEBOM_SqlHelper.ExecuteNonQuery(sql, ps);

            return result;

        }

        public DataTable getStandardModulusByCurveNameID(int CurveNameID)
        {
            string sql = @"SELECT 
                                   CureNamesID,
                                   CArea,
                                   Clevel,
                                   CsingleMinute,
                                   Cratio,
                                   COneday,
                                   CTwoDay,
                                   CThreeDay,
                                   CFourDay,
                                   CFiveDay,
                                   CSixDay,
                                   CSevenDay,
                                   CEightDay,
                                   CNineDay,
                                   CTenDay,
                                   CElevenDay,
                                   CTwelveDay,
                                   CThirteenDay,
                                   CFourteenDay,
                                   Creator,
                                   CreateDate,
                                   modiyDate,
                                   modiyed,
                                   isNewStyle,
                                   isDel
                            FROM dbo.StandardModulus
                            WHERE CureNamesID = @CurveNameID
                                  AND isdel IS NULL;";


            SqlParameter[] ps =
               {
                new SqlParameter("CurveNameID", CurveNameID)
              };
            DataTable dt = IEBOM_SqlHelper.ExcuteTable(sql, ps);

            return dt;

        }

    }
}
