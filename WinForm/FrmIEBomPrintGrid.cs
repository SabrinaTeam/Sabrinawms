using BLL;
using DAL;
using gregn6Lib;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmIEBomPrintGrid : Form
    {
        private static FrmIEBomPrintGrid frm;
        public static string styleNumber = "";
        public static  string ieVersion = "";
        public static int isNewStyle = 0;
        public static int ep = 0; // 8 小时产能
        public static int tp = 0; // 10 小时产能

        public string reportPath = System.Windows.Forms.Application.StartupPath + "\\report.grf";
        public static string dataconnect = "Data Source=192.168.4.243; Initial Catalog=IEBOM; User ID=sa; Password=iwall123;Connect Timeout=180";
        public IEBomManager iem = new IEBomManager();

        private GridppReport Report = new GridppReport();

        public FrmIEBomPrintGrid()
        {
            InitializeComponent();
            Report.PrintEnd += isPrintEnd;
            //Report.PrintEnd += new PrintEventHandler(isPrintEnd);
        }
        public static  FrmIEBomPrintGrid GetSingleton(string styleNumberstr, string ieVersionstr, int isNewStylestr, string epstr, string tpstr)
        {
            styleNumber = styleNumberstr;
            ieVersion = ieVersionstr;
            isNewStyle = isNewStylestr;
            //  epstr =Convert.ToDecimal( epstr).ToString();
            if (epstr != null && epstr != "")
            {
                ep = Convert.ToInt32( Convert.ToDecimal(epstr));
            }
            if (tpstr != null && tpstr != "")
            {
                tp = Convert.ToInt32(Convert.ToDecimal( tpstr));
            }



            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmIEBomPrintGrid();
            }
            return frm;
        }
        public IEBom getIeBoms()
        {

            string newver = ieVersion;
            newver = newver.Substring(1, newver.Length - 1);
            DataTable IEBomDT = iem.searchByStyle(styleNumber, newver);
            IEBom ieboms = new IEBom();
            if (IEBomDT != null && IEBomDT.Rows.Count > 0)
            {
                ieboms.Id = Convert.ToInt32( IEBOM_SqlHelper.FromDbValue( IEBomDT.Rows[0]["Id"].ToString()));
                ieboms.IEBomName = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["IEBomName"].ToString()));
                ieboms.IEBomStyleName = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["IEBomStyleName"].ToString()));
                ieboms.LectraNumber = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["LectraNumber"].ToString()));
                ieboms.IEBomVersion = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["IEBomVersion"].ToString()));
                ieboms.IEBomCreateDate = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["IEBomCreateDate"].ToString()));
                ieboms.IEBomLastModifyDate = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["IEBomLastModifyDate"].ToString()));

                ieboms.IEBomCreator = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["IEBomCreator"].ToString()));
                ieboms.IEBomModifyHistoryNumber = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["IEBomModifyHistoryNumber"].ToString()));
                ieboms.IEBomProcessNumber = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["IEBomProcessNumber"].ToString()));
                ieboms.IEBomRatioID = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["IEBomRatioID"].ToString()));
                ieboms.StyleRemark = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["StyleRemark"].ToString()));
                ieboms.MakeGroup = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["MakeGroup"].ToString()));
                ieboms.TaktTime = Math.Round(Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["TaktTime"].ToString())),2);

                ieboms.MainPicture = (byte[])IEBomDT.Rows[0]["MainPicture"];
                ieboms.ReversePicture = (byte[])IEBomDT.Rows[0]["ReversePicture"];

                // public byte[] MainPicture { get; set; } //正面图
                //public byte[] ReversePicture { get; set; } //背面图
                ieboms.SinglePieceMakeTime = Math.Round(Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["SinglePieceMakeTime"].ToString())),2);
                ieboms.HourSingleMakes = Math.Round( Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["HourSingleMakes"].ToString())),2);
                ieboms.HourGroupMakes = Math.Round(Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["HourGroupMakes"].ToString())),2);
                ieboms.SewWorkmanCount = Convert.ToInt32(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["SewWorkmanCount"].ToString()));
                ieboms.EightMakePieces = Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["EightMakePieces"].ToString()));
                ieboms.TenMakePieces = Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["TenMakePieces"].ToString()));
                ieboms.Season = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["Season"].ToString()));
                ieboms.Difficultyx = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["Difficultyx"].ToString()));
                ieboms.Difficultyy = Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["Difficultyy"].ToString()));
                ieboms.StandardCoefficient = Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["StandardCoefficient"].ToString()));
                ieboms.StandardHourProductionCapacity = Convert.ToInt32(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["StandardHourProductionCapacity"].ToString()));
                ieboms.CureNames = Convert.ToString(IEBOM_SqlHelper.FromDbValue(IEBomDT.Rows[0]["CureNames"].ToString()));
            }
            return ieboms;
        }

        public StandardModulus getStandardModulus(string ModulusName, string Clevel, int IsNewStyle)
        {
            DataTable standardDT = iem.getStandardModulus(ModulusName,  Clevel, IsNewStyle);
            StandardModulus standards = new StandardModulus();
            if (standardDT != null && standardDT.Rows.Count > 0)
            {
                standards.id = Convert.ToInt32(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["Id"].ToString()));
                standards.CureNamesID = Convert.ToString(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["CureNamesID"].ToString()));
                standards.CArea = Convert.ToString(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["CArea"].ToString()));
                standards.Clevel = Convert.ToString(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["Clevel"].ToString()));
                standards.CsingleMinute = Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["CsingleMinute"].ToString()));
                standards.Cratio = Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["Cratio"].ToString()));

                standards.COneday = Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["COneday"].ToString()));

                /*



                ratio = Convert.ToDouble(dr["CTwoDay"].ToString() == "" ? "1" : dr["CTwoDay"].ToString());
                this.txt2day.Text = Convert.ToString(ratio * 100);
                this.txt2Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                this.txt2Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                */


                standards.COneday = Convert.ToDouble(standardDT.Rows[0]["COneday"]==DBNull.Value ? "1" : standardDT.Rows[0]["COneday"].ToString());
                standards.CTwoDay = Convert.ToDouble(standardDT.Rows[0]["CTwoDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CTwoDay"].ToString());
                standards.CThreeDay = Convert.ToDouble(standardDT.Rows[0]["CThreeDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CThreeDay"].ToString());
                standards.CFourDay = Convert.ToDouble(standardDT.Rows[0]["CFourDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CFourDay"].ToString());
                standards.CFiveDay = Convert.ToDouble(standardDT.Rows[0]["CFiveDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CFiveDay"].ToString());

                standards.CSixDay = Convert.ToDouble(standardDT.Rows[0]["CSixDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CSixDay"].ToString());
                standards.CSevenDay = Convert.ToDouble(standardDT.Rows[0]["CSevenDay"] == DBNull.Value  ? "1" : standardDT.Rows[0]["CSevenDay"].ToString());
                standards.CEightDay = Convert.ToDouble(standardDT.Rows[0]["CEightDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CEightDay"].ToString());
                standards.CNineDay = Convert.ToDouble(standardDT.Rows[0]["CNineDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CNineDay"].ToString());
                standards.CTenDay = Convert.ToDouble(standardDT.Rows[0]["CTenDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CTenDay"].ToString());

                standards.CElevenDay = Convert.ToDouble(standardDT.Rows[0]["CElevenDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CElevenDay"].ToString());
                standards.CTwelveDay = Convert.ToDouble(standardDT.Rows[0]["CTwelveDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CTwelveDay"].ToString());
                standards.CThirteenDay = Convert.ToDouble(standardDT.Rows[0]["CThirteenDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CThirteenDay"].ToString());
                standards.CFourteenDay = Convert.ToDouble(standardDT.Rows[0]["CFourteenDay"] == DBNull.Value ? "1" : standardDT.Rows[0]["CFourteenDay"].ToString());

                standards.Creator = Convert.ToString(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["Creator"].ToString()));
                standards.CreateDate = Convert.ToString(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["CreateDate"].ToString()));
                standards.modiyDate = Convert.ToString(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["modiyDate"].ToString()));
                standards.modiyed = Convert.ToString(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["modiyed"].ToString()));
                standards.isNewStyle = Convert.ToInt32(IEBOM_SqlHelper.FromDbValue(standardDT.Rows[0]["isNewStyle"].ToString()));
                standards.isDel = Convert.ToInt32(standardDT.Rows[0]["isDel"].ToString() == "" ? "0" : standardDT.Rows[0]["CFourteenDay"].ToString());




            }
            return standards;
        }


        private void FrmIEBomPrintGrid_Load(object sender, EventArgs e)
        {
            // MessageBox.Show(System.Windows.Forms.Application.StartupPath);
            //载入报表模板文件，必须保证 Grid++Report 的安装目录在‘C:\Grid++Report 6’下，
            //关于动态设置报表路径与数据绑定参数请参考其它例子程序
            if (styleNumber.Length <= 0  || ieVersion.Length <= 0)
            {
                MessageBox.Show("没有此款式");
                return;
            }
            IEBom ieboms = getIeBoms();
            //  int ModulusName  CureNames, string Clevel, int IsNewStyle)

            StandardModulus standards = getStandardModulus(ieboms.CureNames,ieboms.Difficultyx,isNewStyle);
            standards.Day1Hour8Makes = Convert.ToInt32(Math.Round((standards.COneday * ieboms.EightMakePieces),0,MidpointRounding.AwayFromZero));
            standards.Day2Hour8Makes = Convert.ToInt32(Math.Round((standards.CTwoDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day3Hour8Makes = Convert.ToInt32(Math.Round((standards.CThreeDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day4Hour8Makes = Convert.ToInt32(Math.Round((standards.CFourDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day5Hour8Makes = Convert.ToInt32(Math.Round((standards.CFiveDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day6Hour8Makes = Convert.ToInt32(Math.Round((standards.CSixDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day7Hour8Makes = Convert.ToInt32(Math.Round((standards.CSevenDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day8Hour8Makes = Convert.ToInt32(Math.Round((standards.CEightDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day9Hour8Makes = Convert.ToInt32(Math.Round((standards.CNineDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day10Hour8Makes = Convert.ToInt32(Math.Round((standards.CTenDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day11Hour8Makes = Convert.ToInt32(Math.Round((standards.CElevenDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day12Hour8Makes = Convert.ToInt32(Math.Round((standards.CElevenDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day13Hour8Makes = Convert.ToInt32(Math.Round((standards.CTwelveDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day14Hour8Makes = Convert.ToInt32(Math.Round((standards.CThirteenDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day14Hour8Makes = Convert.ToInt32(Math.Round((standards.CFourteenDay * ieboms.EightMakePieces), 0, MidpointRounding.AwayFromZero));

            standards.Day1Hour10Makes = Convert.ToInt32(Math.Round((standards.COneday * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day2Hour10Makes = Convert.ToInt32(Math.Round((standards.CTwoDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day3Hour10Makes = Convert.ToInt32(Math.Round((standards.CThreeDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day4Hour10Makes = Convert.ToInt32(Math.Round((standards.CFourDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day5Hour10Makes = Convert.ToInt32(Math.Round((standards.CFiveDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day6Hour10Makes = Convert.ToInt32(Math.Round((standards.CSixDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day7Hour10Makes = Convert.ToInt32(Math.Round((standards.CSevenDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day8Hour10Makes = Convert.ToInt32(Math.Round((standards.CEightDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day9Hour10Makes = Convert.ToInt32(Math.Round((standards.CNineDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day10Hour10Makes = Convert.ToInt32(Math.Round((standards.CTenDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day11Hour10Makes = Convert.ToInt32(Math.Round((standards.CElevenDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day12Hour10Makes = Convert.ToInt32(Math.Round((standards.CElevenDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day13Hour10Makes = Convert.ToInt32(Math.Round((standards.CTwelveDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day14Hour10Makes = Convert.ToInt32(Math.Round((standards.CThirteenDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));
            standards.Day14Hour10Makes = Convert.ToInt32(Math.Round((standards.CFourteenDay * ieboms.TenMakePieces), 0, MidpointRounding.AwayFromZero));

            string newver = ieVersion;
            newver = newver.Substring(1, newver.Length - 1);
            string Process = styleNumber + "V" + newver;
            string QuerySQL = @"SELECT p.id,
                                       p.ProcessNumber,
                                       p.Scope,
                                       p.partNumber,
                                       p.partName,
                                       p.importantPart,
                                       p.partRemark,
                                       p.partMachineTypeID,
                                       p.partMachineTypeName,
                                       p.averageSecond,
                                       p.standardSecond,
                                       p.standardHourproductionCapacity,
                                       p.assignmentAllocate,
                                       p.actualAllocate,
                                       p.remark,
                                       p.isDel,
                                       b.MainPicture,
                                       b.ReversePicture
                                FROM IEBOM.dbo.IEBomProces p
                                    LEFT JOIN IEBomBase b
                                        ON   IEBomStyleName = '" + styleNumber + @"'
                                           AND IEBomVersion = " + newver + @"
                                    WHERE ProcessNumber = '"+ Process + @"'
                                      AND isDel = 0;";


            Report.LoadFromFile(System.Windows.Forms.Application.StartupPath + "\\IEBomReport.grf");
            Report.DetailGrid.Recordset.ConnectionString = dataconnect;
            Report.DetailGrid.Recordset.QuerySQL = QuerySQL;
            Report.ParameterByName("Id").AsInteger = ieboms.Id;
            Report.ParameterByName("IEBomName").AsString = ieboms.IEBomName;
            Report.ParameterByName("IEBomStyleName").AsString = ieboms.IEBomStyleName;
            Report.ParameterByName("LectraNumber").AsString = ieboms.LectraNumber;
            Report.ParameterByName("IEBomVersion").AsString = ieboms.IEBomVersion;
            Report.ParameterByName("IEBomCreateDate").AsString = ieboms.IEBomCreateDate;
            Report.ParameterByName("IEBomLastModifyDate").AsString = ieboms.IEBomLastModifyDate;
            Report.ParameterByName("IEBomCreator").AsString = ieboms.IEBomCreator;
            Report.ParameterByName("IEBomModifyHistoryNumber").AsString = ieboms.IEBomModifyHistoryNumber;
            Report.ParameterByName("IEBomProcessNumber").AsString = ieboms.IEBomProcessNumber;
            Report.ParameterByName("IEBomRatioID").AsString = ieboms.IEBomRatioID;
            Report.ParameterByName("StyleRemark").AsString = ieboms.StyleRemark;
            Report.ParameterByName("MakeGroup").AsString = ieboms.MakeGroup;
            Report.ParameterByName("TaktTime").AsFloat = ieboms.TaktTime;
            Report.ParameterByName("MainPicture").Value = ieboms.MainPicture;
            Report.ParameterByName("ReversePicture").Value = ieboms.ReversePicture;

           // Report.ControlByName("PictureBox1").AsPictureBox.LoadFromFile("LOGO.png");
           // Report.ControlByName("PictureBox2").AsPictureBox.LoadFromBinary(ieboms.MainPicture);
           // Report.ControlByName("PictureBox2").AsPictureBox.LoadCursorFile(ieboms.MainPicture[0], ieboms.MainPicture.Length);
            // public byte[] MainPicture { get; set; } //正面图
            //public byte[] ReversePicture { get; set; } //背面图
            Report.ParameterByName("SinglePieceMakeTime").AsFloat = ieboms.SinglePieceMakeTime;
            Report.ParameterByName("HourSingleMakes").AsFloat = ieboms.HourSingleMakes;
            Report.ParameterByName("HourGroupMakes").AsFloat = ieboms.HourGroupMakes;
            Report.ParameterByName("SewWorkmanCount").AsInteger = ieboms.SewWorkmanCount;
            Report.ParameterByName("EightMakePieces").AsFloat = ieboms.EightMakePieces;
            Report.ParameterByName("TenMakePieces").AsFloat = ieboms.TenMakePieces;
            Report.ParameterByName("Season").AsString = ieboms.Season;
            Report.ParameterByName("Difficultyx").AsString = ieboms.Difficultyx;
            Report.ParameterByName("Difficultyy").AsFloat = ieboms.Difficultyy;
            Report.ParameterByName("StandardCoefficient").AsFloat = ieboms.StandardCoefficient;
            Report.ParameterByName("StandardHourProductionCapacity").AsInteger = ieboms.StandardHourProductionCapacity;
            Report.ParameterByName("CureNames").AsString = ieboms.CureNames;



            Report.ParameterByName("CArea").AsString = standards.CArea;
            Report.ParameterByName("Clevel").AsString = standards.Clevel;
            Report.ParameterByName("CsingleMinute").AsFloat = standards.CsingleMinute;
            Report.ParameterByName("Cratio").AsFloat = standards.Cratio;

            Report.ParameterByName("COneday").AsString = standards.COneday * 100 +"%";
            Report.ParameterByName("CTwoDay").AsString = standards.CTwoDay * 100 + "%";
            Report.ParameterByName("CThreeDay").AsString = standards.CThreeDay * 100 + "%";
            Report.ParameterByName("CFourDay").AsString = standards.CFourDay * 100 + "%";
            Report.ParameterByName("CFiveDay").AsString = standards.CFiveDay * 100 + "%";

            Report.ParameterByName("CSixDay").AsString = standards.CSixDay * 100 + "%";
            Report.ParameterByName("CSevenDay").AsString = standards.CSevenDay * 100 + "%";
            Report.ParameterByName("CEightDay").AsString = standards.CEightDay * 100 + "%";
            Report.ParameterByName("CNineDay").AsString = standards.CNineDay * 100 + "%";
            Report.ParameterByName("CTenDay").AsString = standards.CTenDay * 100 + "%";

            Report.ParameterByName("CElevenDay").AsString = standards.CElevenDay * 100 + "%";
            Report.ParameterByName("CTwelveDay").AsString = standards.CTwelveDay * 100 + "%";
            Report.ParameterByName("CThirteenDay").AsString = standards.CThirteenDay * 100 + "%";
            Report.ParameterByName("CFourteenDay").AsString = standards.CFourteenDay * 100 + "%";

            Report.ParameterByName("Creator").AsString = standards.Creator;
            Report.ParameterByName("CreateDate").AsString = standards.CreateDate;
            Report.ParameterByName("modiyDate").AsString = standards.modiyDate;
            Report.ParameterByName("modiyed").AsString = standards.modiyed;

            if(standards.isNewStyle == 0)
            {
                Report.ParameterByName("isNewStyle").AsString = "新款学习曲线目标";
            }
            else
            {
                Report.ParameterByName("isNewStyle").AsString = "老款学习曲线目标";
            }

            Report.ParameterByName("isDel").AsInteger = standards.isDel;

            Report.ParameterByName("Day1Hour8Makes").AsInteger = standards.Day1Hour8Makes;
            Report.ParameterByName("Day2Hour8Makes").AsInteger = standards.Day2Hour8Makes;
            Report.ParameterByName("Day3Hour8Makes").AsInteger = standards.Day3Hour8Makes;
            Report.ParameterByName("Day4Hour8Makes").AsInteger = standards.Day4Hour8Makes;
            Report.ParameterByName("Day5Hour8Makes").AsInteger = standards.Day5Hour8Makes;

            Report.ParameterByName("Day6Hour8Makes").AsInteger = standards.Day6Hour8Makes;
            Report.ParameterByName("Day7Hour8Makes").AsInteger = standards.Day7Hour8Makes;
            Report.ParameterByName("Day8Hour8Makes").AsInteger = standards.Day8Hour8Makes;
            Report.ParameterByName("Day9Hour8Makes").AsInteger = standards.Day9Hour8Makes;
            Report.ParameterByName("Day10Hour8Makes").AsInteger = standards.Day10Hour8Makes;

            Report.ParameterByName("Day11Hour8Makes").AsInteger = standards.Day11Hour8Makes;
            Report.ParameterByName("Day12Hour8Makes").AsInteger = standards.Day12Hour8Makes;
            Report.ParameterByName("Day13Hour8Makes").AsInteger = standards.Day13Hour8Makes;
            Report.ParameterByName("Day14Hour8Makes").AsInteger = standards.Day14Hour8Makes;

            Report.ParameterByName("Day1Hour10Makes").AsInteger = standards.Day1Hour10Makes;
            Report.ParameterByName("Day2Hour10Makes").AsInteger = standards.Day2Hour10Makes;
            Report.ParameterByName("Day3Hour10Makes").AsInteger = standards.Day3Hour10Makes;
            Report.ParameterByName("Day4Hour10Makes").AsInteger = standards.Day4Hour10Makes;
            Report.ParameterByName("Day5Hour10Makes").AsInteger = standards.Day5Hour10Makes;

            Report.ParameterByName("Day6Hour10Makes").AsInteger = standards.Day6Hour10Makes;
            Report.ParameterByName("Day7Hour10Makes").AsInteger = standards.Day7Hour10Makes;
            Report.ParameterByName("Day8Hour10Makes").AsInteger = standards.Day8Hour10Makes;
            Report.ParameterByName("Day9Hour10Makes").AsInteger = standards.Day9Hour10Makes;
            Report.ParameterByName("Day10Hour10Makes").AsInteger = standards.Day10Hour10Makes;

            Report.ParameterByName("Day11Hour10Makes").AsInteger = standards.Day11Hour10Makes;
            Report.ParameterByName("Day12Hour10Makes").AsInteger = standards.Day12Hour10Makes;
            Report.ParameterByName("Day13Hour10Makes").AsInteger = standards.Day13Hour10Makes;
            Report.ParameterByName("Day14Hour10Makes").AsInteger = standards.Day14Hour10Makes;

            this.axGRPrintViewer1.Report = Report;
            axGRPrintViewer1.Start();
        }

        private void FrmIEBomPrintGrid_Resize(object sender, EventArgs e)
        {

            this.axGRPrintViewer1.Width = this.Width - 20;
            this.axGRPrintViewer1.Height = this.Height - 50;
        }
        public void isPrintEnd()
        {
          //  cmm.updataIsPrints(tagInvoice, tagLocation);
            Report.PrintEnd -= isPrintEnd;//取消侦听注册事件，避免多次侦听
        }
    }
}
