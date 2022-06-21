using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class IEBom
    {
        public int Id { get; set; }
        public string IEBomName { get; set; } //Bom名称
        public string IEBomStyleName { get; set; }  //款式名
        public string LectraNumber { get; set; } //力克版号
        public string IEBomVersion { get; set; } //Bom款式版本号
        public string IEBomCreateDate { get; set; } //BOM 创建日期
        public string IEBomLastModifyDate { get; set; } //BOM 最后修改日期
        public string IEBomCreator { get; set; } //BOM 创建人
        public string IEBomModifyHistoryNumber { get; set; } //BOM 修改历史记录ID编号
        public string IEBomProcessNumber { get; set; } //工序表ID
        public string IEBomRatioID { get; set; } //IE系数表ID
        public byte[] MainPicture { get; set; } //正面图
        public byte[] ReversePicture { get; set; } //背面图
        public string StyleRemark { get; set; } //款号描述
        public string MakeGroup { get; set; } //生产组别
        public double TaktTime { get; set; }//节拍时间
        public double SinglePieceMakeTime { get; set; } //单件时间
        public double HourSingleMakes { get; set; } //时产能-单人
        public double HourGroupMakes { get; set; } //时产能-小组
        public int SewWorkmanCount { get; set; } //车缝人数
        public double EightMakePieces { get; set; } //8小时产能-组
        public double TenMakePieces { get; set; } //10小时产能-组


        public string Season { get; set; } //季节
        public string Difficultyx { get; set; } //难易级别 字母
        public double Difficultyy { get; set; } //难易级别  数字
        public double StandardCoefficient { get; set; } //宽放系数
        public int StandardHourProductionCapacity { get; set; } //标准时秒数
        public string CureNames { get; set; } //标准系数表
        public int? GroupID { get; set; } //标准系数表

    }
    public class IEVersions
    {
        public int VersionNumber { get; set; }
        public string VersionDisplay { get; set; }
    }

    public class IEBomProces
    {
        public int id { get; set; } //IE表对应头ID
        public string ProcessNumber { get; set; } //IE表对应头ID
        public string Scope { get; set; } //部件
        public int partNumber { get; set; } //工段号
        public string partName { get; set; }//工段名称

        public int importantPart { get; set; } //重要工段
        public string partRemark { get; set; }//工段备注
        public int partMachineTypeID { get; set; } //机器ID号
        public string partMachineTypeName { get; set; } //机器名称

        public double averageSecond { get; set; }//平均时长(秒)
        public double standardSecond { get; set; }//标准时长(秒)
        public double standardHourproductionCapacity { get; set; }//标准时产能(件)
        public double assignmentAllocate { get; set; }//作业分配(人)
        public double actualAllocate { get; set; }//实际配置(人)
        public string remark { get; set; }//备注
        public int isDel { get; set; }//是否删除

    }

    public class NewStyleLearningCurve
    {
        public  int ID { get; set; }
        public string IEBomLearningCurveID { get; set; }
        public string CureNamesID { get; set; }
        public int day1 { get; set; }
        public int day2 { get; set; }
        public int day3 { get; set; }
        public int day4 { get; set; }
        public int day5 { get; set; }
        public int day6 { get; set; }
        public int day7 { get; set; }
        public int day8 { get; set; }
        public int day9 { get; set; }
        public int day10 { get; set; }
        public int day11 { get; set; }
        public int day12 { get; set; }
        public int day13 { get; set; }
        public int day14 { get; set; }

        public int hour8Day1Makes { get; set; }
        public int hour8Day2Makes { get; set; }
        public int hour8Day3Makes { get; set; }
        public int hour8Day4Makes { get; set; }
        public int hour8Day5Makes { get; set; }
        public int hour8Day6Makes { get; set; }
        public int hour8Day7Makes { get; set; }
        public int hour8Day8Makes { get; set; }
        public int hour8Day9Makes { get; set; }
        public int hour8Day10Makes { get; set; }
        public int hour8Day11Makes { get; set; }
        public int hour8Day12Makes { get; set; }
        public int hour8Day13Makes { get; set; }
        public int hour8Day14Makes { get; set; }

        public int hour10Day1Makes { get; set; }
        public int hour10Day2Makes { get; set; }
        public int hour10Day3Makes { get; set; }
        public int hour10Day4Makes { get; set; }
        public int hour10Day5Makes { get; set; }
        public int hour10Day6Makes { get; set; }
        public int hour10Day7Makes { get; set; }
        public int hour10Day8Makes { get; set; }
        public int hour10Day9Makes { get; set; }
        public int hour10Day10Makes { get; set; }
        public int hour10Day11Makes { get; set; }
        public int hour10Day12Makes { get; set; }
        public int hour10Day13Makes { get; set; }
        public int hour10Day14Makes { get; set; }


    }
    public class pictureNames
    {
        string pictureName { get; set; }
    }

    public class StandardModulus
    {
        public int id { get; set; }
        public string CureNamesID { get; set; }
        public string  CArea { get; set; }
        public string  Clevel { get; set; }
        public double  CsingleMinute { get; set; }
        public double Cratio { get; set; }
        public double COneday { get; set; }
        public double CTwoDay { get; set; }
        public double CThreeDay { get; set; }
        public double CFourDay {  get; set; }
        public double CFiveDay { get; set; }
        public double CSixDay { get; set; }
        public double CSevenDay { get; set; }
        public double CEightDay { get; set; }
        public double CNineDay { get; set; }
        public double CTenDay { get; set; }
        public double CElevenDay { get; set; }
        public double CTwelveDay { get; set; }
        public double CThirteenDay { get; set; }
        public double CFourteenDay { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
        public string  modiyDate { get; set; }
        public string  modiyed { get; set; }
        public int  isNewStyle { get; set; }
        public int  isDel { get; set; }



        public int Day1Hour8Makes { get; set; }
        public int Day2Hour8Makes { get; set; }
        public int Day3Hour8Makes { get; set; }
        public int Day4Hour8Makes { get; set; }
        public int Day5Hour8Makes { get; set; }
        public int Day6Hour8Makes { get; set; }
        public int Day7Hour8Makes { get; set; }
        public int Day8Hour8Makes { get; set; }
        public int Day9Hour8Makes { get; set; }
        public int Day10Hour8Makes { get; set; }
        public int Day11Hour8Makes { get; set; }
        public int Day12Hour8Makes { get; set; }
        public int Day13Hour8Makes { get; set; }
        public int Day14Hour8Makes { get; set; }


        public int Day1Hour10Makes { get; set; }
        public int Day2Hour10Makes { get; set; }
        public int Day3Hour10Makes { get; set; }
        public int Day4Hour10Makes { get; set; }
        public int Day5Hour10Makes { get; set; }
        public int Day6Hour10Makes { get; set; }
        public int Day7Hour10Makes { get; set; }
        public int Day8Hour10Makes { get; set; }
        public int Day9Hour10Makes { get; set; }
        public int Day10Hour10Makes { get; set; }
        public int Day11Hour10Makes { get; set; }
        public int Day12Hour10Makes { get; set; }
        public int Day13Hour10Makes { get; set; }
        public int Day14Hour10Makes { get; set; }


    }

    public class iebomGroup
    {
        public int id { get; set; }
        public int groupid { get; set; }
        public string groupname { get; set; }
        public string groupstyle { get; set; }
        public string note { get; set; }
    }
}
