using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class StyleLearningCurveManager
    {
        public StyleLearningCurveServer slcs = new StyleLearningCurveServer();

        public List<StyleLearingCurve> getCurveNames()
        {
            List<StyleLearingCurve> names =  slcs.getCurveNames();
            return names;
        }

        public DataTable addStandardModulus()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("COldArea", typeof(string))); //区间
            table.Columns.Add(new DataColumn("COldlevel", typeof(char))); //难易等级
            table.Columns.Add(new DataColumn("COldSingleMinute", typeof(int)));//单件时长
            table.Columns.Add(new DataColumn("COldratio", typeof(double)));//学系率
            table.Columns.Add(new DataColumn("COldOneday", typeof(double)));
            table.Columns.Add(new DataColumn("COldTwoDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldThreeDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldFourDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldFiveDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldSixDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldSevenDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldEightDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldNineDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldTenDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldElevenDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldTwelveDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldThirteenDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldFourteenDay", typeof(double)));

            DataRow dr1 = table.NewRow();
            dr1["COldArea"] = "1-5";
            dr1["COldlevel"] = 'A';
            dr1["COldSingleMinute"] = 5;
            dr1["COldratio"] = 0.86603;
            dr1["COldOneday"] = 0.75;
            dr1["COldTwoDay"] = 0.85;
            dr1["COldThreeDay"] = 0.95;
            dr1["COldFourDay"] = 1;
            table.Rows.Add(dr1);

            DataRow dr2 = table.NewRow();
            dr2["COldArea"] = "6-10";
            dr2["COldlevel"] = 'B';
            dr2["COldSingleMinute"] = 10;
            dr2["COldratio"] = 0.83666;
            dr2["COldOneday"] = 0.70;
            dr2["COldTwoDay"] = 0.80;
            dr2["COldThreeDay"] = 0.90;
            dr2["COldFourDay"] = 1;
            table.Rows.Add(dr2);

            DataRow dr3 = table.NewRow();
            dr3["COldArea"] = "11-15";
            dr3["COldlevel"] = 'C';
            dr3["COldSingleMinute"] = 15;
            dr3["COldratio"] = 0.83067;
            dr3["COldOneday"] = 0.65;
            dr3["COldTwoDay"] = 0.75;
            dr3["COldThreeDay"] = 0.85;
            dr3["COldFourDay"] = 0.95;
            dr3["COldFiveDay"] = 1;
            table.Rows.Add(dr3);

            DataRow dr4 = table.NewRow();
            dr4["COldArea"] = "16-20";
            dr4["COldlevel"] = 'D';
            dr4["COldSingleMinute"] = 20;
            dr4["COldratio"] = 0.80252;
            dr4["COldOneday"] = 0.60;
            dr4["COldTwoDay"] = 0.70;
            dr4["COldThreeDay"] = 0.80;
            dr4["COldFourDay"] = 0.90;
            dr4["COldFiveDay"] = 1;
            table.Rows.Add(dr4);


            DataRow dr5 = table.NewRow();
            dr5["COldArea"] = "21-25";
            dr5["COldlevel"] = 'E';
            dr5["COldSingleMinute"] = 25;
            dr5["COldratio"] = 0.79352;
            dr5["COldOneday"] = 0.55;
            dr5["COldTwoDay"] = 0.65;
            dr5["COldThreeDay"] = 0.75;
            dr5["COldFourDay"] = 0.85;
            dr5["COldFiveDay"] = 0.95;
            dr5["COldSixDay"] = 1;
            table.Rows.Add(dr5);

            DataRow dr6 = table.NewRow();
            dr6["COldArea"] = "26-30";
            dr6["COldlevel"] = 'F';
            dr6["COldSingleMinute"] = 30;
            dr6["COldratio"] = 0.76480;
            dr6["COldOneday"] = 0.50;
            dr6["COldTwoDay"] = 0.60;
            dr6["COldThreeDay"] = 0.70;
            dr6["COldFourDay"] = 0.80;
            dr6["COldFiveDay"] = 0.90;
            dr6["COldSixDay"] = 1;
            table.Rows.Add(dr6);

            DataRow dr7 = table.NewRow();
            dr7["COldArea"] = "31-35";
            dr7["COldlevel"] = 'G';
            dr7["COldSingleMinute"] = 35;
            dr7["COldratio"] = 0.75244;
            dr7["COldOneday"] = 0.45;
            dr7["COldTwoDay"] = 0.55;
            dr7["COldThreeDay"] = 0.65;
            dr7["COldFourDay"] = 0.75;
            dr7["COldFiveDay"] = 0.85;
            dr7["COldSixDay"] = 0.95;
            dr7["COldSevenDay"] = 1;
            table.Rows.Add(dr7);

            DataRow dr8 = table.NewRow();
            dr8["COldArea"] = "36-40";
            dr8["COldlevel"] = 'H';
            dr8["COldSingleMinute"] = 40;
            dr8["COldratio"] = 0.72152;
            dr8["COldOneday"] = 0.40;
            dr8["COldTwoDay"] = 0.50;
            dr8["COldThreeDay"] = 0.60;
            dr8["COldFourDay"] = 0.70;
            dr8["COldFiveDay"] = 0.80;
            dr8["COldSixDay"] = 0.90;
            dr8["COldSevenDay"] = 1;
            table.Rows.Add(dr8);

            DataRow dr9 = table.NewRow();
            dr9["COldArea"] = "41-45";
            dr9["COldlevel"] = 'I';
            dr9["COldSingleMinute"] = 45;
            dr9["COldratio"] = 0.70473;
            dr9["COldOneday"] = 0.35;
            dr9["COldTwoDay"] = 0.45;
            dr9["COldThreeDay"] = 0.55;
            dr9["COldFourDay"] = 0.65;
            dr9["COldFiveDay"] = 0.75;
            dr9["COldSixDay"] = 0.85;
            dr9["COldSevenDay"] = 0.95;
            dr9["COldEightDay"] = 1;
            table.Rows.Add(dr9);

            DataRow dr10 = table.NewRow();
            dr10["COldArea"] = "46-50";
            dr10["COldlevel"] = 'J';
            dr10["COldSingleMinute"] =50 ;
            dr10["COldratio"] = 0.66943;
            dr10["COldOneday"] = 0.30;
            dr10["COldTwoDay"] = 0.40;
            dr10["COldThreeDay"] = 0.50;
            dr10["COldFourDay"] = 0.60;
            dr10["COldFiveDay"] = 0.70;
            dr10["COldSixDay"] = 0.80;
            dr10["COldSevenDay"] = 0.90;
            dr10["COldEightDay"] = 1;
            table.Rows.Add(dr10);

            DataRow dr11 = table.NewRow();
            dr11["COldArea"] = "51-55";
            dr11["COldlevel"] = 'K';
            dr11["COldSingleMinute"] = 55;
            dr11["COldratio"] = 0.64576;
            dr11["COldOneday"] = 0.25;
            dr11["COldTwoDay"] = 0.35;
            dr11["COldThreeDay"] = 0.45;
            dr11["COldFourDay"] = 0.55;
            dr11["COldFiveDay"] = 0.65;
            dr11["COldSixDay"] = 0.75;
            dr11["COldSevenDay"] = 0.85;
            dr11["COldEightDay"] = 0.95;
            dr11["COldNineDay"] = 1;
            table.Rows.Add(dr11);

            DataRow dr12 = table.NewRow();
            dr12["COldArea"] = "56-60";
            dr12["COldlevel"] = 'L';
            dr12["COldSingleMinute"] = 60;
            dr12["COldratio"] = 0.60187;
            dr12["COldOneday"] = 0.20;
            dr12["COldTwoDay"] = 0.30;
            dr12["COldThreeDay"] = 0.40;
            dr12["COldFourDay"] = 0.50;
            dr12["COldFiveDay"] = 0.60;
            dr12["COldSixDay"] = 0.70;
            dr12["COldSevenDay"] = 0.80;
            dr12["COldEightDay"] = 0.90;
            dr12["COldNineDay"] = 1;
            table.Rows.Add(dr12);


            DataRow dr13 = table.NewRow();
            dr13["COldArea"] = "61-65";
            dr13["COldlevel"] = 'M';
            dr13["COldSingleMinute"] = 65;
            dr13["COldratio"] = 0.56491;
            dr13["COldOneday"] = 0.15;
            dr13["COldTwoDay"] = 0.25;
            dr13["COldThreeDay"] = 0.35;
            dr13["COldFourDay"] = 0.45;
            dr13["COldFiveDay"] = 0.55;
            dr13["COldSixDay"] = 0.65;
            dr13["COldSevenDay"] = 0.75;
            dr13["COldEightDay"] = 0.85;
            dr13["COldNineDay"] = 0.95;
            dr13["COldTenDay"] = 1;
            table.Rows.Add(dr13);

            DataRow dr14 = table.NewRow();
            dr14["COldArea"] = "66-70";
            dr14["COldlevel"] = 'N';
            dr14["COldSingleMinute"] = 70;
            dr14["COldratio"] = 0.50000;
            dr14["COldOneday"] = 0.10;
            dr14["COldTwoDay"] = 0.20;
            dr14["COldThreeDay"] = 0.30;
            dr14["COldFourDay"] = 0.40;
            dr14["COldFiveDay"] = 0.50;
            dr14["COldSixDay"] = 0.60;
            dr14["COldSevenDay"] = 0.70;
            dr14["COldEightDay"] = 0.80;
            dr14["COldNineDay"] = 0.90;
            dr14["COldTenDay"] = 1;
            table.Rows.Add(dr14);

            DataRow dr15 = table.NewRow();
            dr15["COldArea"] = "71-75";
            dr15["COldlevel"] = 'O';
            dr15["COldSingleMinute"] = 75;
            dr15["COldratio"] = 0.42065;
            dr15["COldOneday"] = 0.5;
            dr15["COldTwoDay"] = 0.15;
            dr15["COldThreeDay"] = 0.25;
            dr15["COldFourDay"] = 0.35;
            dr15["COldFiveDay"] = 0.45;
            dr15["COldSixDay"] = 0.55;
            dr15["COldSevenDay"] = 0.65;
            dr15["COldEightDay"] = 0.75;
            dr15["COldNineDay"] = 0.85;
            dr15["COldTenDay"] = 0.95;
            dr15["COldElevenDay"] = 1;
            table.Rows.Add(dr15);

            DataRow dr16 = table.NewRow();
            dr16["COldArea"] = "76-80";
            dr16["COldlevel"] = 'P';
            dr16["COldSingleMinute"] = 80;
            dr16["COldratio"] = 0.20971;
            dr16["COldOneday"] = 0.0;
            dr16["COldTwoDay"] = 0.10;
            dr16["COldThreeDay"] = 0.20;
            dr16["COldFourDay"] = 0.30;
            dr16["COldFiveDay"] = 0.40;
            dr16["COldSixDay"] = 0.50;
            dr16["COldSevenDay"] = 0.60;
            dr16["COldEightDay"] = 0.70;
            dr16["COldNineDay"] = 0.80;
            dr16["COldTenDay"] = 0.90;
            dr16["COldElevenDay"] = 1;
            table.Rows.Add(dr16);

            return table;
        }


        public DataTable addOldStandardModulus()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("COldArea", typeof(string))); //区间
            table.Columns.Add(new DataColumn("COldlevel", typeof(char))); //难易等级
            table.Columns.Add(new DataColumn("COldSingleMinute", typeof(int)));//单件时长
            table.Columns.Add(new DataColumn("COldratio", typeof(double)));//学系率
            table.Columns.Add(new DataColumn("COldOneday", typeof(double)));
            table.Columns.Add(new DataColumn("COldTwoDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldThreeDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldFourDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldFiveDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldSixDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldSevenDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldEightDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldNineDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldTenDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldElevenDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldTwelveDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldThirteenDay", typeof(double)));
            table.Columns.Add(new DataColumn("COldFourteenDay", typeof(double)));

            DataRow dr1 = table.NewRow();
            dr1["COldArea"] = "1-5";
            dr1["COldlevel"] = 'A';
            dr1["COldSingleMinute"] = 5;
            dr1["COldratio"] = 1.00;
            dr1["COldOneday"] = 1.00;
            table.Rows.Add(dr1);

            DataRow dr2 = table.NewRow();
            dr2["COldArea"] = "6-10";
            dr2["COldlevel"] = 'B';
            dr2["COldSingleMinute"] = 10;
            dr2["COldratio"] = 1.00;
            dr2["COldOneday"] = 1.00;
            table.Rows.Add(dr2);

            DataRow dr3 = table.NewRow();
            dr3["COldArea"] = "11-15";
            dr3["COldlevel"] = 'C';
            dr3["COldSingleMinute"] = 15;
            dr3["COldratio"] = 1.00;
            dr3["COldOneday"] = 0.95;
            dr3["COldTwoDay"] = 1.00;
            table.Rows.Add(dr3);

            DataRow dr4 = table.NewRow();
            dr4["COldArea"] = "16-20";
            dr4["COldlevel"] = 'D';
            dr4["COldSingleMinute"] = 20;
            dr4["COldratio"] = 1.00;
            dr4["COldOneday"] = 0.90;
            dr4["COldTwoDay"] = 1.00;
            table.Rows.Add(dr4);


            DataRow dr5 = table.NewRow();
            dr5["COldArea"] = "21-25";
            dr5["COldlevel"] = 'E';
            dr5["COldSingleMinute"] = 25;
            dr5["COldratio"] = 0.90254;
            dr5["COldOneday"] = 0.85;
            dr5["COldTwoDay"] = 0.95;
            dr5["COldThreeDay"] = 1.00;
            table.Rows.Add(dr5);

            DataRow dr6 = table.NewRow();
            dr6["COldArea"] = "26-30";
            dr6["COldlevel"] = 'F';
            dr6["COldSingleMinute"] = 30;
            dr6["COldratio"] = 0.86867;
            dr6["COldOneday"] = 0.80;
            dr6["COldTwoDay"] = 0.90;
            dr6["COldThreeDay"] = 1.00;
            table.Rows.Add(dr6);

            DataRow dr7 = table.NewRow();
            dr7["COldArea"] = "31-35";
            dr7["COldlevel"] = 'G';
            dr7["COldSingleMinute"] = 35;
            dr7["COldratio"] = 0.86603;
            dr7["COldOneday"] = 0.75;
            dr7["COldTwoDay"] = 0.85;
            dr7["COldThreeDay"] = 0.95;
            dr7["COldFourDay"] = 1.00;
            table.Rows.Add(dr7);

            DataRow dr8 = table.NewRow();
            dr8["COldArea"] = "36-40";
            dr8["COldlevel"] = 'H';
            dr8["COldSingleMinute"] = 40;
            dr8["COldratio"] = 0.83666;
            dr8["COldOneday"] = 0.70;
            dr8["COldTwoDay"] = 0.80;
            dr8["COldThreeDay"] = 0.90;
            dr8["COldFourDay"] = 1.00;
            table.Rows.Add(dr8);

            DataRow dr9 = table.NewRow();
            dr9["COldArea"] = "41-45";
            dr9["COldlevel"] = 'I';
            dr9["COldSingleMinute"] = 45;
            dr9["COldratio"] = 0.83067;
            dr9["COldOneday"] = 0.65;
            dr9["COldTwoDay"] = 0.75;
            dr9["COldThreeDay"] = 0.85;
            dr9["COldFourDay"] = 0.95;
            dr9["COldFiveDay"] = 1.00;
            table.Rows.Add(dr9);

            DataRow dr10 = table.NewRow();
            dr10["COldArea"] = "46-50";
            dr10["COldlevel"] = 'J';
            dr10["COldSingleMinute"] = 50;
            dr10["COldratio"] = 0.80252;
            dr10["COldOneday"] = 0.60;
            dr10["COldTwoDay"] = 0.70;
            dr10["COldThreeDay"] = 0.80;
            dr10["COldFourDay"] = 0.90;
            dr10["COldFiveDay"] = 1.00;
            table.Rows.Add(dr10);

            DataRow dr11 = table.NewRow();
            dr11["COldArea"] = "51-55";
            dr11["COldlevel"] = 'K';
            dr11["COldSingleMinute"] = 55;
            dr11["COldratio"] = 0.79352;
            dr11["COldOneday"] = 0.55;
            dr11["COldTwoDay"] = 0.65;
            dr11["COldThreeDay"] = 0.75;
            dr11["COldFourDay"] = 0.85;
            dr11["COldFiveDay"] = 0.95;
            dr11["COldSixDay"] = 1.00;
            table.Rows.Add(dr11);

            DataRow dr12 = table.NewRow();
            dr12["COldArea"] = "56-60";
            dr12["COldlevel"] = 'L';
            dr12["COldSingleMinute"] = 60;
            dr12["COldratio"] = 0.76480;
            dr12["COldOneday"] = 0.50;
            dr12["COldTwoDay"] = 0.60;
            dr12["COldThreeDay"] = 0.70;
            dr12["COldFourDay"] = 0.80;
            dr12["COldFiveDay"] = 0.90;
            dr12["COldSixDay"] = 1.00;
            table.Rows.Add(dr12);


            DataRow dr13 = table.NewRow();
            dr13["COldArea"] = "61-65";
            dr13["COldlevel"] = 'M';
            dr13["COldSingleMinute"] = 65;
            dr13["COldratio"] = 0.75244;
            dr13["COldOneday"] = 0.45;
            dr13["COldTwoDay"] = 0.55;
            dr13["COldThreeDay"] = 0.65;
            dr13["COldFourDay"] = 075;
            dr13["COldFiveDay"] = 0.85;
            dr13["COldSixDay"] = 0.95;
            dr13["COldSevenDay"] = 1.00;
            table.Rows.Add(dr13);

            DataRow dr14 = table.NewRow();
            dr14["COldArea"] = "66-70";
            dr14["COldlevel"] = 'N';
            dr14["COldSingleMinute"] = 70;
            dr14["COldratio"] = 0.72152;
            dr14["COldOneday"] = 0.40;
            dr14["COldTwoDay"] = 0.50;
            dr14["COldThreeDay"] = 0.60;
            dr14["COldFourDay"] = 0.70;
            dr14["COldFiveDay"] = 0.80;
            dr14["COldSixDay"] = 0.90;
            dr14["COldSevenDay"] = 1.00;
            table.Rows.Add(dr14);

            DataRow dr15 = table.NewRow();
            dr15["COldArea"] = "71-75";
            dr15["COldlevel"] = 'O';
            dr15["COldSingleMinute"] = 75;
            dr15["COldratio"] = 0.70069;
            dr15["COldOneday"] = 0.35;
            dr15["COldTwoDay"] = 0.45;
            dr15["COldThreeDay"] = 0.55;
            dr15["COldFourDay"] = 0.65;
            dr15["COldFiveDay"] = 0.75;
            dr15["COldSixDay"] = 0.85;
            dr15["COldSevenDay"] = 0.95;
            dr15["COldEightDay"] = 1.00;
            table.Rows.Add(dr15);

            DataRow dr16 = table.NewRow();
            dr16["COldArea"] = "76-80";
            dr16["COldlevel"] = 'P';
            dr16["COldSingleMinute"] = 80;
            dr16["COldratio"] = 0.67854;
            dr16["COldOneday"] = 0.30;
            dr16["COldTwoDay"] = 0.40;
            dr16["COldThreeDay"] = 0.50;
            dr16["COldFourDay"] = 0.60;
            dr16["COldFiveDay"] = 0.70;
            dr16["COldSixDay"] = 0.80;
            dr16["COldSevenDay"] = 0.90;
            dr16["COldEightDay"] = 1.00;
            table.Rows.Add(dr16);

            return table;
        }

        public string saveLearningCurve(int CureNamesID, DataTable newt, string Creator, string CreateDate,int isNewStyle)
        {
            DataTable newDt = new DataTable();
            newDt.Columns.Add(new DataColumn("CureNamesID", typeof(int))); //名称ID
            newDt.Columns.Add(new DataColumn("CArea", typeof(string))); //区间
            newDt.Columns.Add(new DataColumn("Clevel", typeof(char))); //难易等级
            newDt.Columns.Add(new DataColumn("CSingleMinute", typeof(int)));//单件时长
            newDt.Columns.Add(new DataColumn("Cratio", typeof(double)));//学系率

            newDt.Columns.Add(new DataColumn("COneday", typeof(double)));
            newDt.Columns.Add(new DataColumn("CTwoDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CThreeDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CFourDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CFiveDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CSixDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CSevenDay", typeof(double)));

            newDt.Columns.Add(new DataColumn("CEightDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CNineDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CTenDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CElevenDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CTwelveDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CThirteenDay", typeof(double)));
            newDt.Columns.Add(new DataColumn("CFourteenDay", typeof(double)));

            newDt.Columns.Add(new DataColumn("Creator", typeof(string)));
            newDt.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            newDt.Columns.Add(new DataColumn("modiyDate", typeof(DateTime)));
            newDt.Columns.Add(new DataColumn("modiyed", typeof(string)));
            newDt.Columns.Add(new DataColumn("isNewStyle", typeof(int)));


            for (int i = 0; i < newt.Rows.Count; i++)
                {
                    DataRow dr = newDt.NewRow();
                    dr["CureNamesID"] = CureNamesID;
                    dr["CArea"] = newt.Rows[i]["COldArea"];
                    dr["Clevel"] = newt.Rows[i]["COldlevel"].ToString().Trim().ToUpper();
                    dr["CsingleMinute"] = newt.Rows[i]["COldsingleMinute"];
                    dr["Cratio"] = newt.Rows[i]["COldratio"];

                    dr["COneday"] = newt.Rows[i]["COldOneday"];
                    dr["CTwoDay"] = newt.Rows[i]["COldTwoDay"];
                    dr["CThreeDay"] = newt.Rows[i]["COldThreeDay"];
                    dr["CFourDay"] = newt.Rows[i]["COldFourDay"];
                    dr["CFiveDay"] = newt.Rows[i]["COldFiveDay"];
                    dr["CSixDay"] = newt.Rows[i]["COldSixDay"];
                    dr["CSevenDay"] = newt.Rows[i]["COldSevenDay"];

                    dr["CEightDay"] = newt.Rows[i]["COldEightDay"];
                    dr["CNineDay"] = newt.Rows[i]["COldNineDay"];
                    dr["CTenDay"] = newt.Rows[i]["COldTenDay"];
                    dr["CElevenDay"] = newt.Rows[i]["COldElevenDay"];
                    dr["CTwelveDay"] = newt.Rows[i]["COldTwelveDay"];
                    dr["CThirteenDay"] = newt.Rows[i]["COldThirteenDay"];
                    dr["CFourteenDay"] = newt.Rows[i]["COldFourteenDay"];
                    if (Creator.Length <= 0)
                    {
                        dr["Creator"] = Dns.GetHostName().Trim().ToUpper();
                    }
                    else
                    {
                        dr["Creator"] = Creator;
                    }
                    if (CreateDate.Length <= 0)
                    {
                        dr["CreateDate"] = DateTime.Now.ToString();
                    }
                    else
                    {
                        dr["CreateDate"] = CreateDate;
                    }
                    dr["modiyDate"] = DateTime.Now.ToString();
                    dr["modiyed"] = Dns.GetHostName().Trim().ToUpper();
                    dr["isNewStyle"] = isNewStyle;
                newDt.Rows.Add(dr);
                }
            string result = slcs.saveLearningCurve(newDt);
            return result;
        }


        public int saveLearningCurveName(string CurveName)
        {
            return slcs.saveLearningCurveName(CurveName);
        }

        public int getLearningByCurveName(string CurveName)
        {
            return slcs.getLearningByCurveName(CurveName);
        }
        public int delStandardModulusByCurveNameID(int CurveNameID)
        {
            return slcs.delStandardModulusByCurveNameID(CurveNameID);
        }

        public List<DataTable> getStandardModulusByCurveNameID(int CurveNameID)
        {
            List<DataTable> ldt = new List<DataTable> ();
            DataTable StandardModulusDT = new DataTable();
            DataTable newStyle = new DataTable();
            newStyle.Columns.Add(new DataColumn("COldArea", typeof(string))); //区间
            newStyle.Columns.Add(new DataColumn("COldlevel", typeof(char))); //难易等级
            newStyle.Columns.Add(new DataColumn("COldSingleMinute", typeof(int)));//单件时长
            newStyle.Columns.Add(new DataColumn("COldratio", typeof(double)));//学系率
            newStyle.Columns.Add(new DataColumn("COldOneday", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldTwoDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldThreeDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldFourDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldFiveDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldSixDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldSevenDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldEightDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldNineDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldTenDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldElevenDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldTwelveDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldThirteenDay", typeof(double)));
            newStyle.Columns.Add(new DataColumn("COldFourteenDay", typeof(double)));

            DataTable oldStyle = new DataTable();
            oldStyle.Columns.Add(new DataColumn("COldArea", typeof(string))); //区间
            oldStyle.Columns.Add(new DataColumn("COldlevel", typeof(char))); //难易等级
            oldStyle.Columns.Add(new DataColumn("COldSingleMinute", typeof(int)));//单件时长
            oldStyle.Columns.Add(new DataColumn("COldratio", typeof(double)));//学系率
            oldStyle.Columns.Add(new DataColumn("COldOneday", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldTwoDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldThreeDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldFourDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldFiveDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldSixDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldSevenDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldEightDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldNineDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldTenDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldElevenDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldTwelveDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldThirteenDay", typeof(double)));
            oldStyle.Columns.Add(new DataColumn("COldFourteenDay", typeof(double)));

            StandardModulusDT =  slcs.getStandardModulusByCurveNameID(CurveNameID);
            if (StandardModulusDT != null && StandardModulusDT.Rows.Count > 0)
            {
                for (int i = 0; i < StandardModulusDT.Rows.Count; i++)
                {
                    if ( StandardModulusDT.Rows[i]["isNewStyle"].ToString() == "0")  //新款
                    {
                        DataRow dr = newStyle.NewRow();
                        dr["COldArea"] = StandardModulusDT.Rows[i]["CArea"].ToString();
                        dr["COldlevel"] = StandardModulusDT.Rows[i]["Clevel"].ToString();
                        dr["COldSingleMinute"] = StandardModulusDT.Rows[i]["CsingleMinute"].ToString();
                        dr["COldratio"] = StandardModulusDT.Rows[i]["Cratio"].ToString();

                        dr["COldOneday"] = IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["COneday"]) ;
                        dr["COldTwoDay"] = IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CTwoDay"]);
                        dr["COldThreeDay"] = IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CThreeDay"]);
                        dr["COldFourDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CFourDay"]);
                        dr["COldFiveDay"] = IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CFiveDay"] );
                            //Convert.ToDouble(StandardModulusDT.Rows[i]["CFiveDay"]);
                        dr["COldSixDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CSixDay"]);
                        dr["COldSevenDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CSevenDay"]);


                        dr["COldEightDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CEightDay"]);
                        dr["COldNineDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CNineDay"]);
                        dr["COldTenDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CTenDay"]);
                        dr["COldElevenDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CElevenDay"]);
                        dr["COldTwelveDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CTwelveDay"]);
                        dr["COldThirteenDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CThirteenDay"]);
                        dr["COldFourteenDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CFourteenDay"]);
                        newStyle.Rows.Add(dr);
                    }
                    else //老款
                    {
                        DataRow dr = oldStyle.NewRow();
                        dr["COldArea"] = StandardModulusDT.Rows[i]["CArea"].ToString();
                        dr["COldlevel"] = StandardModulusDT.Rows[i]["Clevel"].ToString();
                        dr["COldSingleMinute"] = StandardModulusDT.Rows[i]["CsingleMinute"].ToString();
                        dr["COldratio"] = StandardModulusDT.Rows[i]["Cratio"].ToString();

                        dr["COldOneday"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["COneday"]);
                        dr["COldTwoDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CTwoDay"]);
                        dr["COldThreeDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CThreeDay"]);
                        dr["COldFourDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CFourDay"]);
                        dr["COldFiveDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CFiveDay"]);
                        dr["COldSixDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CSixDay"]);
                        dr["COldSevenDay"]=IEBOM_SqlHelper.ToDbValue( StandardModulusDT.Rows[i]["CSevenDay"]);


                        dr["COldEightDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CEightDay"]);
                        dr["COldNineDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CNineDay"]);
                        dr["COldTenDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CTenDay"]);
                        dr["COldElevenDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CElevenDay"]);
                        dr["COldTwelveDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CTwelveDay"]);
                        dr["COldThirteenDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CThirteenDay"]);
                        dr["COldFourteenDay"] =IEBOM_SqlHelper.ToDbValue(StandardModulusDT.Rows[i]["CFourteenDay"]);
                        oldStyle.Rows.Add(dr);
                    }
                }
                ldt.Add(newStyle);
                ldt.Add(oldStyle);

            }
            return ldt;
        }


        /// <summary>
        /// 计算产能
        /// </summary>
        /// <param name="cpps"></param>
        /// <returns></returns>
        public DataTable CalculatProductivity(CalculatProductivityParameters cpps, DataTable StandardNew, DataTable StandardOld)
        {

            DataTable cpdt = new DataTable();
            cpdt.Columns.Add(new DataColumn("StyleHours", typeof(string))); //款式 工作时间
            cpdt.Columns.Add(new DataColumn("CArea", typeof(string))); //区间
            cpdt.Columns.Add(new DataColumn("Clevel", typeof(char))); //难易等级
            cpdt.Columns.Add(new DataColumn("CSingleMinute", typeof(int)));//单件时长
            cpdt.Columns.Add(new DataColumn("Cratio", typeof(double)));//学系率
            cpdt.Columns.Add(new DataColumn("CalculatProductivitys", typeof(int)));//人均日产能（8 / 10 小时）

            cpdt.Columns.Add(new DataColumn("COnedayGroup", typeof(int)));//组日产能
            cpdt.Columns.Add(new DataColumn("CTwoDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CThreeDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CFourDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CFiveDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CSixDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CSevenDayGroup", typeof(int)));

            cpdt.Columns.Add(new DataColumn("CEightDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CNineDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CTenDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CElevenDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CTwelveDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CThirteenDayGroup", typeof(int)));
            cpdt.Columns.Add(new DataColumn("CFourteenDayGroup", typeof(int)));


            int hours =  0;

            if (cpps.styleNew && cpps.Hour8 && StandardNew != null) //新款8小时
            {
                DataRow hdr = cpdt.NewRow();
                hdr["StyleHours"] = "New Style 8 Hour";
                cpdt.Rows.Add(hdr);
                hours =  8;

                for (int i = 0; i < StandardNew.Rows.Count; i++)
                {
                    DataRow dr = cpdt.NewRow();
                    dr["StyleHours"] =Convert.ToString( IEBOM_SqlHelper.FromDbValue(  StandardNew.Rows[i]["COldArea"]));
                    dr["CArea"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldArea"]));
                    dr["Clevel"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldlevel"]));
                    dr["CSingleMinute"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]));
                    dr["Cratio"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldratio"]));

                    dr["CalculatProductivitys"] = 60 * hours /  Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"])));
                    dr["COnedayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue( StandardNew.Rows[i]["COldOneday"])));
                    dr["CTwoDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldTwoDay"])));
                    dr["CThreeDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldThreeDay"])));
                    dr["CFourDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldFourDay"])));
                    dr["CFiveDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldFiveDay"])));
                    dr["CSixDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSixDay"])));
                    dr["CSevenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSevenDay"])));

                    dr["CEightDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldEightDay"])));
                    dr["CNineDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldNineDay"])));
                    dr["CTenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldTenDay"])));
                    dr["CElevenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldElevenDay"])));
                    dr["CTwelveDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldTwelveDay"])));
                    dr["CThirteenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldThirteenDay"])));
                    dr["CFourteenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldFourteenDay"])));

                    cpdt.Rows.Add(dr);
                }
            }
            if (cpps.styleNew && cpps.Hour10 && StandardNew != null) //新款10小时
            {
                DataRow hdr = cpdt.NewRow();
                hdr["StyleHours"] = "New Style 10 Hour";
                cpdt.Rows.Add(hdr);
                hours = 10;

                for (int i = 0; i < StandardNew.Rows.Count; i++)
                {
                    DataRow dr = cpdt.NewRow();
                    dr["StyleHours"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldArea"]));
                    dr["CArea"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldArea"]));
                    dr["Clevel"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldlevel"]));
                    dr["CSingleMinute"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]));
                    dr["Cratio"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldratio"]));

                    dr["CalculatProductivitys"] = 60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"])));
                    dr["COnedayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldOneday"])));
                    dr["CTwoDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldTwoDay"])));
                    dr["CThreeDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldThreeDay"])));
                    dr["CFourDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldFourDay"])));
                    dr["CFiveDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldFiveDay"])));
                    dr["CSixDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSixDay"])));
                    dr["CSevenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSevenDay"])));

                    dr["CEightDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldEightDay"])));
                    dr["CNineDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldNineDay"])));
                    dr["CTenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldTenDay"])));
                    dr["CElevenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldElevenDay"])));
                    dr["CTwelveDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldTwelveDay"])));
                    dr["CThirteenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldThirteenDay"])));
                    dr["CFourteenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardNew.Rows[i]["COldFourteenDay"])));

                    cpdt.Rows.Add(dr);
                }

            }


            if (cpps.styleOld && cpps.Hour8 && StandardOld != null)//老款8小时
            {
                DataRow hdr = cpdt.NewRow();
                hdr["StyleHours"] = "Old Style 8 Hour";
                cpdt.Rows.Add(hdr);
                hours = 8;

                for (int i = 0; i < StandardOld.Rows.Count; i++)
                {
                    DataRow dr = cpdt.NewRow();
                    dr["StyleHours"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldArea"]));
                    dr["CArea"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldArea"]));
                    dr["Clevel"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldlevel"]));
                    dr["CSingleMinute"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]));
                    dr["Cratio"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldratio"]));

                    dr["CalculatProductivitys"] = 60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"])));
                    dr["COnedayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldOneday"])));
                    dr["CTwoDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldTwoDay"])));
                    dr["CThreeDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldThreeDay"])));
                    dr["CFourDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldFourDay"])));
                    dr["CFiveDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldFiveDay"])));
                    dr["CSixDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSixDay"])));
                    dr["CSevenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSevenDay"])));

                    dr["CEightDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldEightDay"])));
                    dr["CNineDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldNineDay"])));
                    dr["CTenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldTenDay"])));
                    dr["CElevenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldElevenDay"])));
                    dr["CTwelveDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldTwelveDay"])));
                    dr["CThirteenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldThirteenDay"])));
                    dr["CFourteenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldFourteenDay"])));

                    cpdt.Rows.Add(dr);
                }
            }
            if (cpps.styleOld && cpps.Hour10 && StandardOld != null)//老款10小时
            {
                DataRow hdr = cpdt.NewRow();
                hdr["StyleHours"] = "Old Style 10 Hour";
                cpdt.Rows.Add(hdr);
                hours = 10;

                for (int i = 0; i < StandardOld.Rows.Count; i++)
                {
                    DataRow dr = cpdt.NewRow();
                    dr["StyleHours"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldArea"]));
                    dr["CArea"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldArea"]));
                    dr["Clevel"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldlevel"]));
                    dr["CSingleMinute"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]));
                    dr["Cratio"] = Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldratio"]));

                    dr["CalculatProductivitys"] = 60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"])));
                    dr["COnedayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldOneday"])));
                    dr["CTwoDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldTwoDay"])));
                    dr["CThreeDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldThreeDay"])));
                    dr["CFourDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldFourDay"])));
                    dr["CFiveDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldFiveDay"])));
                    dr["CSixDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSixDay"])));
                    dr["CSevenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSevenDay"])));

                    dr["CEightDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldEightDay"])));
                    dr["CNineDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldNineDay"])));
                    dr["CTenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldTenDay"])));
                    dr["CElevenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldElevenDay"])));
                    dr["CTwelveDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldTwelveDay"])));
                    dr["CThirteenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldThirteenDay"])));
                    dr["CFourteenDayGroup"] = cpps.counts * (60 * hours / Convert.ToInt32(Convert.ToString(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldSingleMinute"]))) * Convert.ToDouble(IEBOM_SqlHelper.FromDbValue(StandardOld.Rows[i]["COldFourteenDay"])));

                    cpdt.Rows.Add(dr);
                }

            }



                return cpdt;
        }
    }
}
