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
	public class IEBomService
	{

		public List<IEVersions> getIEVersions(string styleNumber)
		{
			List<IEVersions> versions = new List<IEVersions>();

			string sql = @"SELECT IEBomVersion  FROM dbo.IEBomBase	 WHERE IEBomStyleName=@styleNumber";

			SqlParameter[] paras =   {
					new SqlParameter("@styleNumber", styleNumber)

				 };


			DataTable dt = IEBOM_SqlHelper.ExcuteTable(sql, paras);
			if(dt != null && dt.Rows.Count >0)
			{

				foreach (DataRow row in dt.Rows)
				{
					IEVersions ver = new IEVersions();
					cversions(row, ver);
					versions.Add(ver);
				}

			}

			return versions;

		}

		public void cversions(DataRow dr, MODEL.IEVersions list)
		{
			list.VersionNumber = Convert.ToInt32(ERP_SqlHelper.FromDbValue(dr["IEBomVersion"]));
			list.VersionDisplay = "V" + Convert.ToString(ERP_SqlHelper.FromDbValue(dr["IEBomVersion"]));

		}


		public DataTable getCureNames()
		{
			string sql = @"SELECT id,
								   modulusName
							FROM CureNames
							WHERE isDel = 0
							ORDER BY id;";
			DataTable dt = IEBOM_SqlHelper.ExcuteTable(sql);
			return dt;
		}

		public DataTable getStandardModulus(int CureNamesID,int NewOrOld)
		{
			string sql = @"SELECT id,
								   CureNamesID,
								   CArea,
								   Clevel,
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
								   CFourteenDay
							FROM dbo.StandardModulus
							WHERE CureNamesID = @CureNamesID
								  AND isNewStyle = "+NewOrOld+@"
								  AND isDel IS NULL;";

			SqlParameter[] ps =
			  {
				new SqlParameter("CureNamesID", CureNamesID)
			  };

			DataTable dt = IEBOM_SqlHelper.ExcuteTable(sql,ps);
			return dt;
		}

		public int saveIEBomHead(IEBom iebomHead)
		{
			string sql = @"INSERT INTO IEBomBase
											   (
													IEBomName
												   ,IEBomStyleName
												   ,LectraNumber
												   ,IEBomVersion
												   ,IEBomCreateDate
												   ,IEBomLastModifyDate
												   ,IEBomCreator
												   ,IEBomModifyHistoryNumber
												   ,IEBomProcessNumber
												   ,IEBomRatioID
												   ,MainPicture
												   ,ReversePicture
												   ,StyleRemark
												   ,MakeGroup
												   ,TaktTime
												   ,SinglePieceMakeTime
												   ,HourSingleMakes
												   ,HourGroupMakes
												   ,SewWorkmanCount
												   ,EightMakePieces
												   ,TenMakePieces
												   ,Season
												   ,Difficultyx
												   ,Difficultyy
												   ,StandardCoefficient
												   ,StandardHourProductionCapacity
												   ,CureNames
												   ,GroupID
											   )
										 VALUES
											   (
													@IEBomName
												   ,@IEBomStyleName
												   ,@LectraNumber
												   ,@IEBomVersion
												   ,@IEBomCreateDate
												   ,@IEBomLastModifyDate
												   ,@IEBomCreator
												   ,@IEBomModifyHistoryNumber
												   ,@IEBomProcessNumber
												   ,@IEBomRatioID
												   ,@MainPicture
												   ,@ReversePicture
												   ,@StyleRemark
												   ,@MakeGroup
												   ,@TaktTime
												   ,@SinglePieceMakeTime
												   ,@HourSingleMakes
												   ,@HourGroupMakes
												   ,@SewWorkmanCount
												   ,@EightMakePieces
												   ,@TenMakePieces
												   ,@Season
												   ,@Difficultyx
												   ,@Difficultyy
												   ,@StandardCoefficient
												   ,@StandardHourProductionCapacity
												   ,@CureNames
												   ,@GroupID
											   );";

			SqlParameter[] ps =
			  {
				new SqlParameter("@IEBomName", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomName)),
				new SqlParameter("@IEBomStyleName", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomStyleName)),
				new SqlParameter("@LectraNumber", IEBOM_SqlHelper.ToDbValue( iebomHead.LectraNumber)),
				new SqlParameter("@IEBomVersion",IEBOM_SqlHelper.ToDbValue(  iebomHead.IEBomVersion)),
				new SqlParameter("@IEBomCreateDate", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomCreateDate)),
				new SqlParameter("@IEBomLastModifyDate", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomLastModifyDate)),
				new SqlParameter("@IEBomCreator", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomCreator)),
				new SqlParameter("@IEBomModifyHistoryNumber",IEBOM_SqlHelper.ToDbValue(  iebomHead.IEBomModifyHistoryNumber)),
				new SqlParameter("@IEBomProcessNumber", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomProcessNumber)),
				new SqlParameter("@IEBomRatioID", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomRatioID)),


				new SqlParameter("@MainPicture",iebomHead.MainPicture),
				new SqlParameter("@ReversePicture",iebomHead.ReversePicture),


				new SqlParameter("@StyleRemark",IEBOM_SqlHelper.ToDbValue(  iebomHead.StyleRemark)),
				new SqlParameter("@MakeGroup", IEBOM_SqlHelper.ToDbValue( iebomHead.MakeGroup)),
				new SqlParameter("@TaktTime",IEBOM_SqlHelper.ToDbValue(  iebomHead.TaktTime)),
				new SqlParameter("@SinglePieceMakeTime",IEBOM_SqlHelper.ToDbValue(  iebomHead.SinglePieceMakeTime)),
				new SqlParameter("@HourSingleMakes",IEBOM_SqlHelper.ToDbValue(  iebomHead.HourSingleMakes)),
				new SqlParameter("@HourGroupMakes",IEBOM_SqlHelper.ToDbValue(  iebomHead.HourGroupMakes)),
				new SqlParameter("@SewWorkmanCount", IEBOM_SqlHelper.ToDbValue( iebomHead.SewWorkmanCount)),
				new SqlParameter("@EightMakePieces",IEBOM_SqlHelper.ToDbValue(  iebomHead.EightMakePieces)),
				new SqlParameter("@TenMakePieces",IEBOM_SqlHelper.ToDbValue(  iebomHead.TenMakePieces)),
				new SqlParameter("@Season", IEBOM_SqlHelper.ToDbValue( iebomHead.Season)),
				new SqlParameter("@Difficultyx", IEBOM_SqlHelper.ToDbValue( iebomHead.Difficultyx)),
				new SqlParameter("@Difficultyy", IEBOM_SqlHelper.ToDbValue( iebomHead.Difficultyy)),
				new SqlParameter("@StandardCoefficient", IEBOM_SqlHelper.ToDbValue( iebomHead.StandardCoefficient)),
				new SqlParameter("@StandardHourProductionCapacity", IEBOM_SqlHelper.ToDbValue( iebomHead.StandardHourProductionCapacity)),
				new SqlParameter("@CureNames",IEBOM_SqlHelper.ToDbValue(  iebomHead.CureNames)),
				new SqlParameter("@GroupID",IEBOM_SqlHelper.ToDbValue(  iebomHead.GroupID))
			  };

			int  result = IEBOM_SqlHelper.ExecuteNonQuery(sql, ps);
			return result;
		}


		public string saveIEBomProcesTables(List<IEBomProces> iebomProcesTables)
		{
			DataTable newDt = new DataTable();
			newDt.Columns.Add(new DataColumn("ProcessNumber", typeof(string))); //IE表对应头ID
			newDt.Columns.Add(new DataColumn("Scope", typeof(string))); //部件
			newDt.Columns.Add(new DataColumn("partNumber", typeof(int))); //工段号
			newDt.Columns.Add(new DataColumn("partName", typeof(string)));//工段名称
			newDt.Columns.Add(new DataColumn("importantPart", typeof(int)));//重要工段

			newDt.Columns.Add(new DataColumn("partRemark", typeof(string))); //工段备注
			newDt.Columns.Add(new DataColumn("partMachineTypeID", typeof(int))); //机器ID号
			newDt.Columns.Add(new DataColumn("partMachineTypeName", typeof(string))); //机器名称
			newDt.Columns.Add(new DataColumn("averageSecond", typeof(double))); //平均时长(秒)
			newDt.Columns.Add(new DataColumn("standardSecond", typeof(double))); //标准时长(秒)

			newDt.Columns.Add(new DataColumn("standardHourproductionCapacity", typeof(double))); //标准时产能(件)
			newDt.Columns.Add(new DataColumn("assignmentAllocate", typeof(double))); //作业分配(人)
			newDt.Columns.Add(new DataColumn("actualAllocate", typeof(double))); //实际配置(人)
			newDt.Columns.Add(new DataColumn("remark", typeof(string))); //备注
			newDt.Columns.Add(new DataColumn("isDel", typeof(int))); //是否删除



			for (int i = 0; i < iebomProcesTables.Count; i++)
			{
				DataRow dr = newDt.NewRow();
				dr["ProcessNumber"] = IEBOM_SqlHelper.ToDbValue( iebomProcesTables[i].ProcessNumber);
				dr["Scope"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].Scope);
				dr["partNumber"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].partNumber);
				dr["partName"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].partName);
				dr["importantPart"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].importantPart);

				dr["partRemark"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].partRemark);
				dr["partMachineTypeID"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].partMachineTypeID);
				dr["partMachineTypeName"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].partMachineTypeName);
				dr["averageSecond"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].averageSecond);
				dr["standardSecond"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].standardSecond);
				dr["standardHourproductionCapacity"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].standardHourproductionCapacity);
				dr["assignmentAllocate"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].assignmentAllocate);

				dr["actualAllocate"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].actualAllocate);
				dr["remark"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].remark);
				dr["isDel"] = IEBOM_SqlHelper.ToDbValue(iebomProcesTables[i].isDel);

				newDt.Rows.Add(dr);
			}


			string result = IEBOM_SqlHelper.SqlBulkCopyIEBomProcesTables(newDt);
			return  result;
		}



		public int savelearningCurves(NewStyleLearningCurve learningCurve)
		{
			string sql = @"INSERT INTO  NewStyleLearningCurve 
										   (
												  
												   IEBomLearningCurveID,
												   CureNamesID,
												   day1,
												   day2,
												   day3,
												   day4,
												   day5,
												   day6,
												   day7,
												   day8,
												   day9,
												   day10,
												   day11,
												   day12,
												   day13,
												   day14,
												   hour8Day1Makes,
												   hour8Day2Makes,
												   hour8Day3Makes,
												   hour8Day4Makes,
												   hour8Day5Makes,
												   hour8Day6Makes,
												   hour8Day7Makes,
												   hour8Day8Makes,
												   hour8Day9Makes,
												   hour8Day10Makes,
												   hour8Day11Makes,
												   hour8Day12Makes,
												   hour8Day13Makes,
												   hour8Day14Makes,
												   hour10Day1Makes,
												   hour10Day2Makes,
												   hour10Day3Makes,
												   hour10Day4Makes,
												   hour10Day5Makes,
												   hour10Day6Makes,
												   hour10Day7Makes,
												   hour10Day8Makes,
												   hour10Day9Makes,
												   hour10Day10Makes,
												   hour10Day11Makes,
												   hour10Day12Makes,
												   hour10Day13Makes,
												   hour10Day14Makes
										   )
										 VALUES
											   ( 
												   @IEBomLearningCurveID,
												   @CureNamesID,
												   @day1,
												   @day2,
												   @day3,
												   @day4,
												   @day5,
												   @day6,
												   @day7,
												   @day8,
												   @day9,
												   @day10,
												   @day11,
												   @day12,
												   @day13,
												   @day14,
												   @hour8Day1Makes,
												   @hour8Day2Makes,
												   @hour8Day3Makes,
												   @hour8Day4Makes,
												   @hour8Day5Makes,
												   @hour8Day6Makes,
												   @hour8Day7Makes,
												   @hour8Day8Makes,
												   @hour8Day9Makes,
												   @hour8Day10Makes,
												   @hour8Day11Makes,
												   @hour8Day12Makes,
												   @hour8Day13Makes,
												   @hour8Day14Makes,
												   @hour10Day1Makes,
												   @hour10Day2Makes,
												   @hour10Day3Makes,
												   @hour10Day4Makes,
												   @hour10Day5Makes,
												   @hour10Day6Makes,
												   @hour10Day7Makes,
												   @hour10Day8Makes,
												   @hour10Day9Makes,
												   @hour10Day10Makes,
												   @hour10Day11Makes,
												   @hour10Day12Makes,
												   @hour10Day13Makes,
												   @hour10Day14Makes
											   );";

			SqlParameter[] ps =
			  {
				new SqlParameter("@IEBomLearningCurveID", IEBOM_SqlHelper.ToDbValue( learningCurve.IEBomLearningCurveID)),
				new SqlParameter("@CureNamesID", IEBOM_SqlHelper.ToDbValue( learningCurve.CureNamesID)),
				new SqlParameter("@day1", IEBOM_SqlHelper.ToDbValue( learningCurve.day1)),
				new SqlParameter("@day2", IEBOM_SqlHelper.ToDbValue( learningCurve.day2)),
				new SqlParameter("@day3", IEBOM_SqlHelper.ToDbValue( learningCurve.day3)),
				new SqlParameter("@day4", IEBOM_SqlHelper.ToDbValue( learningCurve.day4)),
				new SqlParameter("@day5", IEBOM_SqlHelper.ToDbValue( learningCurve.day5)),
				new SqlParameter("@day6", IEBOM_SqlHelper.ToDbValue( learningCurve.day6)),
				new SqlParameter("@day7", IEBOM_SqlHelper.ToDbValue( learningCurve.day7)),
				new SqlParameter("@day8", IEBOM_SqlHelper.ToDbValue( learningCurve.day8)),
				new SqlParameter("@day9", IEBOM_SqlHelper.ToDbValue( learningCurve.day9)),
				new SqlParameter("@day10", IEBOM_SqlHelper.ToDbValue( learningCurve.day10)),
				new SqlParameter("@day11", IEBOM_SqlHelper.ToDbValue( learningCurve.day11)),
				new SqlParameter("@day12", IEBOM_SqlHelper.ToDbValue( learningCurve.day12)),
				new SqlParameter("@day13", IEBOM_SqlHelper.ToDbValue( learningCurve.day13)),
				new SqlParameter("@day14", IEBOM_SqlHelper.ToDbValue( learningCurve.day14)),

				new SqlParameter("@hour8Day1Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day1Makes)),
				new SqlParameter("@hour8Day2Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day2Makes)),
				new SqlParameter("@hour8Day3Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day3Makes)),
				new SqlParameter("@hour8Day4Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day4Makes)),
				new SqlParameter("@hour8Day5Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day5Makes)),
				new SqlParameter("@hour8Day6Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day6Makes)),
				new SqlParameter("@hour8Day7Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day7Makes)),
				new SqlParameter("@hour8Day8Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day8Makes)),
				new SqlParameter("@hour8Day9Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day9Makes)),
				new SqlParameter("@hour8Day10Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day10Makes)),
				new SqlParameter("@hour8Day11Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day11Makes)),
				new SqlParameter("@hour8Day12Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day12Makes)),
				new SqlParameter("@hour8Day13Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day13Makes)),
				new SqlParameter("@hour8Day14Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day14Makes)),

				new SqlParameter("@hour10Day1Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day1Makes)),
				new SqlParameter("@hour10Day2Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day2Makes)),
				new SqlParameter("@hour10Day3Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day3Makes)),
				new SqlParameter("@hour10Day4Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day4Makes)),
				new SqlParameter("@hour10Day5Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day5Makes)),
				new SqlParameter("@hour10Day6Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day6Makes)),
				new SqlParameter("@hour10Day7Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day7Makes)),
				new SqlParameter("@hour10Day8Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day8Makes)),
				new SqlParameter("@hour10Day9Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day9Makes)),
				new SqlParameter("@hour10Day10Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day10Makes)),
				new SqlParameter("@hour10Day11Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day11Makes)),
				new SqlParameter("@hour10Day12Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day12Makes)),
				new SqlParameter("@hour10Day13Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day13Makes)),
				new SqlParameter("@hour10Day14Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day14Makes))
			  };

			int result = IEBOM_SqlHelper.ExecuteNonQuery(sql, ps);
			return result;
		}



		public DataTable searchByStyle(string SearchStyle)
		{
			string sql = @"SELECT Id,
								   IEBomStyleName,
								   LectraNumber,
								   IEBomVersion ,
								   GroupID
							FROM dbo.IEBomBase
							WHERE 1 = 1
								  AND IEBomStyleName = @SearchStyle;";

			SqlParameter[] ps =
			  {
				new SqlParameter("@SearchStyle", IEBOM_SqlHelper.ToDbValue( SearchStyle))

			  };

			DataTable VersionDT = IEBOM_SqlHelper.ExcuteTable(sql, ps);
			return VersionDT;
		}


		public DataTable searchByStyle(string SearchStyle,string ver)
		{
			string sql = @"SELECT  Id,
								   IEBomName,
								   IEBomStyleName,
								   LectraNumber,
								   IEBomVersion,
								   convert(varchar, IEBomCreateDate, 20)  AS IEBomCreateDate,
								   convert(varchar, IEBomLastModifyDate, 20) AS  IEBomLastModifyDate,
								   IEBomCreator,
								   IEBomModifyHistoryNumber,
								   IEBomProcessNumber,
								   IEBomRatioID,
								   MainPicture,
								   ReversePicture,
								   StyleRemark,
								   MakeGroup,
								   TaktTime,
								   SinglePieceMakeTime,
								   HourSingleMakes,
								   HourGroupMakes,
								   SewWorkmanCount,
								   EightMakePieces,
								   TenMakePieces,
								   Season,
								   Difficultyx,
								   Difficultyy,
								   StandardCoefficient,
								   StandardHourProductionCapacity,
								   CureNames,
								   GroupID
							FROM dbo.IEBomBase
							WHERE 1 = 1
								  AND IEBomStyleName = @IEBomStyleName
								  AND IEBomVersion = @IEBomVersion;";
			SqlParameter[] ps =
			  {
				new SqlParameter("@IEBomStyleName", IEBOM_SqlHelper.ToDbValue( SearchStyle)),
				new SqlParameter("@IEBomVersion", IEBOM_SqlHelper.ToDbValue( ver))

			  };

			DataTable VersionDT = IEBOM_SqlHelper.ExcuteTable(sql, ps);
			return VersionDT;
		}

		public DataTable searchProcesByProcessNumber(string SearchStyle)
		{
			string sql = @"SELECT id,
								   ProcessNumber,
								   Scope,
								   partNumber,
								   partName,
								   importantPart,
								   partRemark,
								   partMachineTypeID,
								   partMachineTypeName,
								   averageSecond,
								   standardSecond,
								   standardHourproductionCapacity,
								   assignmentAllocate,
								   actualAllocate,
								   remark
							FROM IEBomProces 
							WHERE  1 = 1
								 AND ProcessNumber = '" + SearchStyle + @"'
								 AND isDel = 0;";

			DataTable ProcessDT = IEBOM_SqlHelper.ExcuteTable(sql);
			return ProcessDT;
		}

		public int getIEBomByStyleVer(string StyleName, string ver)
		{
			string sql = @"SELECT Id
							FROM IEBomBase
							WHERE IEBomStyleName = '"+ StyleName + @"'
								  AND IEBomVersion = '"+ ver + @"';";
			DataTable DT = IEBOM_SqlHelper.ExcuteTable(sql);
			int id = -1;
			if (DT != null && DT.Rows.Count > 0)
			{
				id = Convert.ToInt32( DT.Rows[0]["ID"].ToString());

			}
			return id;
		}


		public int updataIEBomHead(IEBom iebomHead,int id)
		{
			string sql = @"UPDATE  IEBomBase
									   SET IEBomName =  						  @IEBomName
										  ,IEBomStyleName =  					  @IEBomStyleName
										  ,LectraNumber = 						  @LectraNumber
										  ,IEBomVersion =  						  @IEBomVersion
										  ,IEBomCreateDate =  					  @IEBomCreateDate
										  ,IEBomLastModifyDate =  				  @IEBomLastModifyDate
										  ,IEBomCreator =  						  @IEBomCreator
										  ,IEBomModifyHistoryNumber =  			  @IEBomModifyHistoryNumber
										  ,IEBomProcessNumber = 				  @IEBomProcessNumber
										  ,IEBomRatioID =  						  @IEBomRatioID
										  ,MainPicture =  						  @MainPicture
										  ,ReversePicture = 					  @ReversePicture
										  ,StyleRemark = 						  @StyleRemark
										  ,MakeGroup =  						  @MakeGroup
										  ,TaktTime = 							  @TaktTime
										  ,SinglePieceMakeTime =  				  @SinglePieceMakeTime
										  ,HourSingleMakes =  					  @HourSingleMakes
										  ,HourGroupMakes =  					  @HourGroupMakes
										  ,SewWorkmanCount =  					  @SewWorkmanCount
										  ,EightMakePieces =  					  @EightMakePieces
										  ,TenMakePieces =  					  @TenMakePieces
										  ,Season =  							  @Season
										  ,Difficultyx = 						  @Difficultyx
										  ,Difficultyy =  						  @Difficultyy
										  ,StandardCoefficient =  				  @StandardCoefficient
										  ,StandardHourProductionCapacity = 	  @StandardHourProductionCapacity
										  ,CureNames =  						  @CureNames
										  ,GroupID =  								@GroupID
								 WHERE   id = @id ;";

			SqlParameter[] ps =
			  {
				new SqlParameter("@id", IEBOM_SqlHelper.ToDbValue( id)),
				new SqlParameter("@IEBomName", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomName)),
				new SqlParameter("@IEBomStyleName", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomStyleName)),
				new SqlParameter("@LectraNumber", IEBOM_SqlHelper.ToDbValue( iebomHead.LectraNumber)),
				new SqlParameter("@IEBomVersion",IEBOM_SqlHelper.ToDbValue(  iebomHead.IEBomVersion)),
				new SqlParameter("@IEBomCreateDate", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomCreateDate)),
				new SqlParameter("@IEBomLastModifyDate", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomLastModifyDate)),
				new SqlParameter("@IEBomCreator", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomCreator)),
				new SqlParameter("@IEBomModifyHistoryNumber",IEBOM_SqlHelper.ToDbValue(  iebomHead.IEBomModifyHistoryNumber)),
				new SqlParameter("@IEBomProcessNumber", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomProcessNumber)),
				new SqlParameter("@IEBomRatioID", IEBOM_SqlHelper.ToDbValue( iebomHead.IEBomRatioID)),


				new SqlParameter("@MainPicture",iebomHead.MainPicture),
				new SqlParameter("@ReversePicture",iebomHead.ReversePicture),


				new SqlParameter("@StyleRemark",IEBOM_SqlHelper.ToDbValue(  iebomHead.StyleRemark)),
				new SqlParameter("@MakeGroup", IEBOM_SqlHelper.ToDbValue( iebomHead.MakeGroup)),
				new SqlParameter("@TaktTime",IEBOM_SqlHelper.ToDbValue(  iebomHead.TaktTime)),
				new SqlParameter("@SinglePieceMakeTime",IEBOM_SqlHelper.ToDbValue(  iebomHead.SinglePieceMakeTime)),
				new SqlParameter("@HourSingleMakes",IEBOM_SqlHelper.ToDbValue(  iebomHead.HourSingleMakes)),
				new SqlParameter("@HourGroupMakes",IEBOM_SqlHelper.ToDbValue(  iebomHead.HourGroupMakes)),
				new SqlParameter("@SewWorkmanCount", IEBOM_SqlHelper.ToDbValue( iebomHead.SewWorkmanCount)),
				new SqlParameter("@EightMakePieces",IEBOM_SqlHelper.ToDbValue(  iebomHead.EightMakePieces)),
				new SqlParameter("@TenMakePieces",IEBOM_SqlHelper.ToDbValue(  iebomHead.TenMakePieces)),
				new SqlParameter("@Season", IEBOM_SqlHelper.ToDbValue( iebomHead.Season)),
				new SqlParameter("@Difficultyx", IEBOM_SqlHelper.ToDbValue( iebomHead.Difficultyx)),
				new SqlParameter("@Difficultyy", IEBOM_SqlHelper.ToDbValue( iebomHead.Difficultyy)),
				new SqlParameter("@StandardCoefficient", IEBOM_SqlHelper.ToDbValue( iebomHead.StandardCoefficient)),
				new SqlParameter("@StandardHourProductionCapacity", IEBOM_SqlHelper.ToDbValue( iebomHead.StandardHourProductionCapacity)),
				new SqlParameter("@CureNames",IEBOM_SqlHelper.ToDbValue(  iebomHead.CureNames)),
				new SqlParameter("@GroupID",IEBOM_SqlHelper.ToDbValue(  iebomHead.GroupID))
			  };

			int result = IEBOM_SqlHelper.ExecuteNonQuery(sql, ps);
			return result;
		}


		public int updataIEBomProcesTables(string ProcessNumber)
		{
			string sql = @"UPDATE IEBomProces
								SET isDel = 1
								WHERE ProcessNumber = '"+ ProcessNumber + @"';";


			int result = IEBOM_SqlHelper.ExecuteNonQuery(sql);
			return result;
		}


		public int updatalearningCurves(NewStyleLearningCurve learningCurve, string IEBomBodyID)
		{
			string sql = @"UPDATE dbo.NewStyleLearningCurve
													   SET  
														   CureNamesID = @CureNamesID 
														  ,day1 = @day1 
														  ,day2 = @day2 
														  ,day3 = @day3 
														  ,day4 = @day4 
														  ,day5 = @day5 
														  ,day6 = @day6 
														  ,day7 = @day7 
														  ,day8 = @day8 
														  ,day9 = @day9 
														  ,day10 = @day10  
														  ,day11 = @day11  
														  ,day12 = @day12  
														  ,day13 = @day13  
														  ,day14 = @day14  
														  ,hour8Day1Makes = @hour8Day1Makes 
														  ,hour8Day2Makes = @hour8Day2Makes 
														  ,hour8Day3Makes = @hour8Day3Makes 
														  ,hour8Day4Makes = @hour8Day4Makes 
														  ,hour8Day5Makes = @hour8Day5Makes 
														  ,hour8Day6Makes = @hour8Day6Makes 
														  ,hour8Day7Makes = @hour8Day7Makes 
														  ,hour8Day8Makes = @hour8Day8Makes 
														  ,hour8Day9Makes = @hour8Day9Makes 
														  ,hour8Day10Makes = @hour8Day10Makes 
														  ,hour8Day11Makes = @hour8Day11Makes 
														  ,hour8Day12Makes = @hour8Day12Makes 
														  ,hour8Day13Makes = @hour8Day13Makes 
														  ,hour8Day14Makes = @hour8Day14Makes 
														  ,hour10Day1Makes = @hour10Day1Makes 
														  ,hour10Day2Makes = @hour10Day2Makes 
														  ,hour10Day3Makes = @hour10Day3Makes 
														  ,hour10Day4Makes = @hour10Day4Makes 
														  ,hour10Day5Makes = @hour10Day5Makes 
														  ,hour10Day6Makes = @hour10Day6Makes 
														  ,hour10Day7Makes = @hour10Day7Makes 
														  ,hour10Day8Makes = @hour10Day8Makes 
														  ,hour10Day9Makes = @hour10Day9Makes 
														  ,hour10Day10Makes = @hour10Day10Makes 
														  ,hour10Day11Makes = @hour10Day11Makes 
														  ,hour10Day12Makes = @hour10Day12Makes 
														  ,hour10Day13Makes = @hour10Day13Makes 
														  ,hour10Day14Makes = @hour10Day14Makes 
													 WHERE  IEBomLearningCurveID = @IEBomLearningCurveID ;";

			SqlParameter[] ps =
			  {


				new SqlParameter("@IEBomLearningCurveID", IEBomBodyID),
				new SqlParameter("@CureNamesID", IEBOM_SqlHelper.ToDbValue( learningCurve.CureNamesID)),
				new SqlParameter("@day1", IEBOM_SqlHelper.ToDbValue( learningCurve.day1)),
				new SqlParameter("@day2", IEBOM_SqlHelper.ToDbValue( learningCurve.day2)),
				new SqlParameter("@day3", IEBOM_SqlHelper.ToDbValue( learningCurve.day3)),
				new SqlParameter("@day4", IEBOM_SqlHelper.ToDbValue( learningCurve.day4)),
				new SqlParameter("@day5", IEBOM_SqlHelper.ToDbValue( learningCurve.day5)),
				new SqlParameter("@day6", IEBOM_SqlHelper.ToDbValue( learningCurve.day6)),
				new SqlParameter("@day7", IEBOM_SqlHelper.ToDbValue( learningCurve.day7)),
				new SqlParameter("@day8", IEBOM_SqlHelper.ToDbValue( learningCurve.day8)),
				new SqlParameter("@day9", IEBOM_SqlHelper.ToDbValue( learningCurve.day9)),
				new SqlParameter("@day10", IEBOM_SqlHelper.ToDbValue( learningCurve.day10)),
				new SqlParameter("@day11", IEBOM_SqlHelper.ToDbValue( learningCurve.day11)),
				new SqlParameter("@day12", IEBOM_SqlHelper.ToDbValue( learningCurve.day12)),
				new SqlParameter("@day13", IEBOM_SqlHelper.ToDbValue( learningCurve.day13)),
				new SqlParameter("@day14", IEBOM_SqlHelper.ToDbValue( learningCurve.day14)),

				new SqlParameter("@hour8Day1Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day1Makes)),
				new SqlParameter("@hour8Day2Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day2Makes)),
				new SqlParameter("@hour8Day3Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day3Makes)),
				new SqlParameter("@hour8Day4Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day4Makes)),
				new SqlParameter("@hour8Day5Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day5Makes)),
				new SqlParameter("@hour8Day6Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day6Makes)),
				new SqlParameter("@hour8Day7Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day7Makes)),
				new SqlParameter("@hour8Day8Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day8Makes)),
				new SqlParameter("@hour8Day9Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day9Makes)),
				new SqlParameter("@hour8Day10Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day10Makes)),
				new SqlParameter("@hour8Day11Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day11Makes)),
				new SqlParameter("@hour8Day12Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day12Makes)),
				new SqlParameter("@hour8Day13Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day13Makes)),
				new SqlParameter("@hour8Day14Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour8Day14Makes)),

				new SqlParameter("@hour10Day1Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day1Makes)),
				new SqlParameter("@hour10Day2Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day2Makes)),
				new SqlParameter("@hour10Day3Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day3Makes)),
				new SqlParameter("@hour10Day4Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day4Makes)),
				new SqlParameter("@hour10Day5Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day5Makes)),
				new SqlParameter("@hour10Day6Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day6Makes)),
				new SqlParameter("@hour10Day7Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day7Makes)),
				new SqlParameter("@hour10Day8Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day8Makes)),
				new SqlParameter("@hour10Day9Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day9Makes)),
				new SqlParameter("@hour10Day10Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day10Makes)),
				new SqlParameter("@hour10Day11Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day11Makes)),
				new SqlParameter("@hour10Day12Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day12Makes)),
				new SqlParameter("@hour10Day13Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day13Makes)),
				new SqlParameter("@hour10Day14Makes", IEBOM_SqlHelper.ToDbValue( learningCurve.hour10Day14Makes))
			  };

			int result = IEBOM_SqlHelper.ExecuteNonQuery(sql, ps);
			return result;
		}
		public string getLectraNumber(string styleName)
		{
			string sql = @"SELECT sample_no FROM  style WHERE style_id	='"+ styleName + @"';";
			DataTable dt = BEST_SqlHelper.ExcuteTable(sql);
			string lectraNumber = "";
			if(dt.Rows.Count > 0)
			{
				lectraNumber = dt.Rows[0]["sample_no"].ToString().ToUpper();

			}
			return lectraNumber;
		}

		public DataTable getStandardModulus(string ModulusName, string Clevel,int IsNewStyle)
		{
			string sql = @"SELECT  s.id,
								   s.CureNamesID,
								   s.CArea,
								   s.Clevel,
								   s.CsingleMinute,
								   s.Cratio,
								   s.COneday,
								   s.CTwoDay,
								   s.CThreeDay,
								   s.CFourDay,
								   s.CFiveDay,
								   s.CSixDay,
								   s.CSevenDay,
								   s.CEightDay,
								   s.CNineDay,
								   s.CTenDay,
								   s.CElevenDay,
								   s.CTwelveDay,
								   s.CThirteenDay,
								   s.CFourteenDay,
								   s.Creator,
								   s.CreateDate,
								   s.modiyDate,
								   s.modiyed,
								   s.isNewStyle,
								   s.isDel
							FROM StandardModulus s
								LEFT JOIN CureNames c
									ON c.id = s.CureNamesID
							WHERE 1 = 1
								  AND c.ModulusName = @modulusName
								  AND s.Clevel = @clevel
								  AND s.IsNewStyle = @isNewStyle
								  AND s.isDel IS NULL;";
			SqlParameter[] ps =
			  {
				new SqlParameter("@modulusName", ModulusName),
				new SqlParameter("@clevel", Clevel),
				new SqlParameter("@isNewStyle", IsNewStyle)

			  };

			DataTable VersionDT = IEBOM_SqlHelper.ExcuteTable(sql, ps);
			return VersionDT;
		}

		public DataTable getAllStyles()
		{
			string sql = @"SELECT  IEBomStyleName FROM dbo.IEBomBase GROUP BY IEBomStyleName ;";
			DataTable VersionDT = IEBOM_SqlHelper.ExcuteTable(sql);
			return VersionDT;
		}

		public DataTable searchGroupByGroupID(int GroupID)
		{
			string sql = @"SELECT id,
								   groupid,
								   groupname,
								   groupstyle,
								   note
							FROM iebomGroup
							WHERE groupid = "+GroupID+@";";

			DataTable GroupStyleDT = IEBOM_SqlHelper.ExcuteTable(sql);
			return GroupStyleDT;
		}
		public DataTable searchGroupAll()
		{
			string sql = @"SELECT DISTINCT
								   groupid,
								   groupname,
								   groupstyle
							FROM iebomGroup;";

			DataTable GroupStyleDT = IEBOM_SqlHelper.ExcuteTable(sql);
			return GroupStyleDT;
		}





		public DataTable getGroupIDByGroupStyleName(string GroupStyleName)
		{
			string sql = @"SELECT groupid FROM iebomGroup WHERE groupname = '"+ GroupStyleName + @"';";
			DataTable dt = IEBOM_SqlHelper.ExcuteTable(sql);
			return dt;
		}

		public DataTable getGroupMaxID( )
		{
			string sql = @"SELECT MAX(groupid )   groupid  FROM iebomGroup;";
			DataTable dt = IEBOM_SqlHelper.ExcuteTable(sql);
			return dt;
		}



		public int insertIEBomGroup(iebomGroup Groups)
		{
			int GroupID = Groups.groupid;
			string GroupStyle = Groups.groupstyle;

			int result = 0;
			// 查询有没有 这个款式在这个群组里  没有才新增  有 直接返回
			string deleteSql = @"DELETE FROM IEBomGroup	WHERE groupstyle = '" + GroupStyle + @"';";
			IEBOM_SqlHelper.ExecuteNonQuery(deleteSql);

			string sql = @"INSERT INTO IEBomGroup
									   (groupID
									   ,groupName
									   ,groupStyle
									   ,note)
								 VALUES
									   ( @groupID,
										 @groupName,
										 @groupStyle,
										 @note
										);";

				SqlParameter[] ps =
				  {
				new SqlParameter("@groupID", IEBOM_SqlHelper.ToDbValue(  Groups.groupid)),
				new SqlParameter("@groupName",IEBOM_SqlHelper.ToDbValue(  Groups.groupname)),
				new SqlParameter("@groupStyle",IEBOM_SqlHelper.ToDbValue(  Groups.groupstyle)),
				new SqlParameter("@note",IEBOM_SqlHelper.ToDbValue(  Groups.note))
			  };

				  result = IEBOM_SqlHelper.ExecuteNonQuery(sql, ps);

			return result;
		}
	}
}
