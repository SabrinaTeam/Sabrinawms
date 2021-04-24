using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace COMMON
{
    public   class xiaomingCommom
    {         
        /// <summary>
        /// 数字验证
        /// </summary>
        /// <param name="strNumber">被验证信息</param>
        /// <returns></returns>
        public static bool IsWholeNumber(string strNumber)
        {
            Regex notWholePattern = new Regex(@"^[-]?\d+[.]?\d*$");
            return notWholePattern.IsMatch(strNumber, 0);
        }

        public static int GetDayofWeek(DateTime date, string year)
        {
            //該年需要加多少數字
            int thisYear = Convert.ToInt32(year);
            DateTime dt = new DateTime(thisYear, 1, 1);
            string num = dt.DayOfWeek.ToString("d");
            int j = date.DayOfYear + Convert.ToInt16(num) - 1;
            return j / 7 + 1 + (thisYear - 2000) * 100;
        }

        /// <summary>
        ///  使用時間到毫秒傳回唯一的18位數字
        /// </summary>
        /// <returns></returns>
        public string myGuid()
        {
            DateTime dt = DateTime.Now;
            string yyyy = Convert.ToString(dt.Year);
            string mm = Convert.ToString(Convert.ToInt32(dt.Month) + 10);
            string dd = Convert.ToString(Convert.ToInt32(dt.Day) + 10);
            string hh = Convert.ToString(Convert.ToInt32(dt.Hour) + 10);
            string ff = Convert.ToString(Convert.ToInt32(dt.Minute) + 10);
            string ss = Convert.ToString(Convert.ToInt32(dt.Second) + 10);
            string ee = Convert.ToString(Convert.ToInt32(dt.Millisecond + 1000));

            string mynum = yyyy + mm + dd + hh + ff + ss + ee;
            //     System.Windows.Forms.MessageBox.Show(mynum);
            return mynum;
        }

        public static string[] getExcelSheetSum(String filename)
        {
            ExcelHelper excelHelper = new ExcelHelper(filename);
            string[] sheetname = excelHelper.getExcelSheetName(filename);
            // string[] sheetname = new string[k];//表名
            for (int t = 0; t < sheetname.Count(); t++)
            {
                // sheetname[t] = excelHelper.getExcelSheetName(t);
                // sheetname[t] = workbook.GetSheetName(t);
            }

            return sheetname;
        }

        public class NPOIProgram
        {
            private static void PrintData(DataTable data)
            {
                if (data == null) return;
                for (int i = 0; i < data.Rows.Count; ++i)
                {
                    for (int j = 0; j < data.Columns.Count; ++j)
                        Console.Write("{0} ", data.Rows[i][j]);
                    Console.Write("\n");
                }
            }

            //写入EXCEL表  导出
            public void ExcelWrite(string file, DataTable tabl)
            {
                try
                {
                    using (ExcelHelper excelHelper = new ExcelHelper(file))
                    {
                        //DataTable data = GenerateData();
                        //deptBLL getdataDept = new deptBLL();    //写一个从数据库查出来的表

                        //  DataTable data = getdataDept.GetdatagetdataDept();

                        int count = excelHelper.DataTableToExcel(tabl, "MySheet", true);
                        if (count > 0)
                            Console.WriteLine("Number of imported data is {0} ", count);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }

            // 读EXCEL 导入EXCEL表

            public DataTable ExcelReadEmpTable(String filename, string sheetname, int headno)
            {
                List<tradingComanyPO> emplists = ExcelReadEmp(filename, sheetname, headno);
                if (emplists == null)
                {
                    return null;
                }
                //  /*本地表
                //创建本地表
                DataTable table = new DataTable();
                table.Columns.Add("id");
                table.Columns.Add("PO");
                table.Columns.Add("GTN_PO");
                table.Columns.Add("create_pc");
                table.Columns.Add("update_date");

                try
                {
                    for (int i = 0; i < emplists.Count(); i++)
                    {
                        String AID = Convert.ToString(emplists[i].id);
                        String APO = Convert.ToString(emplists[i].PO);
                        String AGTN_PO = Convert.ToString(emplists[i].GTN_PO);
                        String Acreate_pc = Convert.ToString(emplists[i].create_pc);
                        String Aupdate_date = Convert.ToString(emplists[i].update_date); 
                        //本地表加入数据  Unique
                        DataRow row = table.NewRow();
                        row["id"] = AID;
                        row["PO"] = APO;
                        row["GTN_PO"] = AGTN_PO;
                        row["create_pc"] = Acreate_pc;
                        row["update_date"] = Aupdate_date;                        
                        table.Rows.Add(row);
                        /*************/
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                return table;
            }

            public List<tradingComanyPO> ExcelReadEmp(string file, string sheetname, int headno)
            {
                try
                {
                    using (ExcelHelper excelHelper = new ExcelHelper(file))
                    {
                        COMMON.NPOIExcelExample NPOIexcel = new COMMON.NPOIExcelExample();
                        List<tradingComanyPO> gtnpos = new List<tradingComanyPO>();
                        DataTable dt = excelHelper.ReadExcelToDataTable(file, sheetname);
                        if (dt.Rows.Count <= 0)
                        {
                            return null;
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                MODEL.tradingComanyPO gtnpo = new MODEL.tradingComanyPO();
                                gtnpo = EmpToModel(dt.Rows[i]);//这里转换过来
                                gtnpos.Add(gtnpo);
                            }
                            return gtnpos;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    return null;
                }
            }

            private tradingComanyPO EmpToModel(DataRow row)//建立要导入的文件的model
            {
                tradingComanyPO emps = new tradingComanyPO();
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    if (row.ItemArray.Length > 0)
                    {
                        emps.id = Convert.ToInt32(row.ItemArray[0].ToString());
                    }
                    if (row.ItemArray.Length > 1)
                    {
                        emps.PO = Convert.ToString(row.ItemArray[1].ToString());
                    }
                    if (row.ItemArray.Length > 2)
                    {
                        emps.GTN_PO = Convert.ToString(row.ItemArray[2].ToString());
                    }
                    if (row.ItemArray.Length > 3)
                    {
                        emps.create_pc = Convert.ToString(row.ItemArray[3].ToString());
                    }
                    if (row.ItemArray.Length > 4)
                    {
                        emps.update_date = Convert.ToString( row.ItemArray[4].ToString());
                    } 
                     
                }
                return emps;
            }

            public string[] getExcelSheetSum(String filename)
            {
                ExcelHelper excelHelper = new ExcelHelper(filename);
                string[] sheetname = excelHelper.getExcelSheetName(filename);
                for (int t = 0; t < sheetname.Count(); t++)
                {
                    // sheetname[t] = excelHelper.getExcelSheetName(t);
                    // sheetname[t] = workbook.GetSheetName(t);
                }

                return sheetname;
            }

            private static void Main(string[] args)
            {
                // string file = "..\\..\\myTest.xlsx";
                // TestExcelWrite(file);
                // TestExcelRead(file);
            }
        }

        public void ImproExcel(string VG)
        {
            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";
            //   sdfExport.ShowDialog();

            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            String filename = sdfExport.FileName;
            xiaomingCommom.NPOIProgram NPOIexcel = new xiaomingCommom.NPOIProgram();
            DataTable tabl = new DataTable();
            if (VG == "Order")
            {
                //     tabl = GetDgvToTable(dgvOrderSize);
            }
            else if (VG == "SizeRun")
            {
                //    tabl = GetDgvToTable(dataGridViewSizeRun);
            }

            // DataTable dt = (StyleCodeInfodataGridView.DataSource as DataTable);
            NPOIexcel.ExcelWrite(filename, tabl);//excelhelper写出
            if (MessageBox.Show("导出成功，文件保存在" + filename.ToString()
                   + ",是否打开此文件？", "提示", MessageBoxButtons.YesNo) ==
                   DialogResult.Yes)
            {
                if (File.Exists(filename))//文件是否存在
                {
                    Process.Start(filename);//执行打开导出的文件
                }
                else
                {
                    MessageBox.Show("文件不存在！", "提示");
                }
            }
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

        public static Object ToDbValue(Object value)
        {
            if (value == null)
            { return DBNull.Value; }
            else
            {
                return value;
            }
        }

        public static Object FromDbValue(Object value)
        {
            if (value == DBNull.Value)
            { return null; }
            else
            {
                return value;
            }
        }
         

        

    }
}
