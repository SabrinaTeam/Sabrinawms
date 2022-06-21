using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class IEBomManager
    {
        public IEBomService ies = new IEBomService();
        public List<IEVersions> getIEVersions(string styleNumber)
        {
            return ies.getIEVersions(styleNumber);
        }


        public string uploadPicturel( )
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string picPath = "";
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "图像文件(*.jpg;*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picPath = dialog.FileName;

            }
            return picPath;
        }


        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable getCureNames()
        {
          return  ies.getCureNames();
        }

        public DataTable getStandardModulus(int CureNamesID,int NewOrOld)
        {
            return ies.getStandardModulus(CureNamesID, NewOrOld);
        }

        public int saveIEBomHead(IEBom iebomHead)
        {

            int inset =  ies.saveIEBomHead(iebomHead);
            return inset;
        }
        public string  saveIEBomProcesTables(List<IEBomProces> iebomProcesTables)
        {

            string result = ies.saveIEBomProcesTables(iebomProcesTables);
            return result;
        }


        public int savelearningCurves(NewStyleLearningCurve learningCurve)
        {

            int result = ies.savelearningCurves(learningCurve);
            return result;
        }
        public DataTable searchByStyle(string SearchStyle)
        {

           return  ies.searchByStyle(SearchStyle);

        }
        public DataTable searchByStyle(string SearchStyle,string ver)
        {

            return ies.searchByStyle(SearchStyle,ver);

        }
        public DataTable searchProcesByProcessNumber(string ProcessNumber)
        {

            return ies.searchProcesByProcessNumber(ProcessNumber);

        }

        public int getIEBomByStyleVer(string StyleName, string ver)
        {

            return ies.getIEBomByStyleVer(StyleName, ver);

        }


        public int updataIEBomHead(IEBom iebomHead,int id)
        {

            int inset = ies.updataIEBomHead(iebomHead,id);
            return inset;
        }
        public int updataIEBomProcesTables( string ProcessNumber)
        {

            int result = ies.updataIEBomProcesTables( ProcessNumber);
            return result;
        }

        public int updatalearningCurves(NewStyleLearningCurve learningCurve , string IEBomBodyID)
        {

            int result = ies.updatalearningCurves(learningCurve , IEBomBodyID);
            return result;
        }
        public string getLectraNumber(string styleName)
        {
            return ies.getLectraNumber(styleName);
        }

        public DataTable getStandardModulus(string  ModulusName, string Clevel, int IsNewStyle)
        {

            return ies.getStandardModulus(ModulusName, Clevel, IsNewStyle);

        }

        public List<string> getAllStyles( )
        {
            List <string> styles = new List <string>();
            DataTable DT = ies.getAllStyles();
            if( DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    styles.Add(DT.Rows[i][0].ToString());
                }
            }
            return styles;
        }

        public DataTable searchGroupByGroupID(int GroupID)
        {

            return ies.searchGroupByGroupID(GroupID);

        }
        public DataTable searchGroupAll( )
        {

            return ies.searchGroupAll( );

        }




        // 获取GROUPID
        public int  getGroupIDByGroupStyleName(string GroupStyleName)
        {
            int groupID = 0;
            string txtgroupID = "";
            DataTable group = ies.getGroupIDByGroupStyleName(GroupStyleName);
            if( group != null && group.Rows.Count > 0)
            {
                txtgroupID = group.Rows[0][0].ToString();
                if(txtgroupID != "")
                {
                    groupID = Convert.ToInt32(txtgroupID);
                }
            }
            return groupID;
        }

        public int getGroupMaxID()
        {
            int groupID = 0;
            string txtgroupID = "";
            DataTable group = ies.getGroupMaxID();
            if (group != null && group.Rows.Count > 0)
            {
                txtgroupID = group.Rows[0][0].ToString();
                if (txtgroupID != "")
                {
                    groupID = Convert.ToInt32(txtgroupID);
                }
            }
            return groupID;
        }

        public int insertIEBomGroup(iebomGroup Groups)
        {
            // 1、新增加进来
            // 2、更换群组，删除旧的群组信息 更新新的群组信息
            int inset = ies.insertIEBomGroup(Groups);
            return inset;
        }
    }
}
