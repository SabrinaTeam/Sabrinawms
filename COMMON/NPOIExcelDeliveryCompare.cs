using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace COMMON
{
    public class NPOIExcelDeliveryCompare
    {
       
        public DataTable ExcelRead(string file, string sheetname, int headno)
        {
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper(file))
                {
                    DataTable dt = excelHelper.ExcelToDataTable(sheetname, headno);
                    if (dt is null || dt.Rows.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        //要显示的文件的model 例emp
                        /*
                        delivertb[] dCompares = new delivertb[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dCompares[i] = ToModel(dt.Rows[i]);//这里转换过来
                        }
                      
                        return dCompares;
                          */
                        return dt;
                        //InterBoxLableWind IExcel = new InterBoxLableWind();
                        // IExcel.dvgInnerBox.ItemsSource = InnerBoxs;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        private delivertb ToModel(DataRow row)//建立要导入的文件的model
        {
            delivertb dCompare = new delivertb();
            if (row.ItemArray.Length > 0)
            {
                dCompare.id = Convert.ToString( row.ItemArray[0].ToString());
            }
            if (row.ItemArray.Length > 0)
            {
                dCompare.lineName = Convert.ToString(row.ItemArray[0].ToString());
            }
            if (row.ItemArray.Length > 1)
            {
                dCompare.deliveryDate = Convert.ToString(row.ItemArray[1].ToString());
            }
            if (row.ItemArray.Length > 2)
            {
                dCompare.invoiceNo = Convert.ToString(row.ItemArray[2].ToString());
            }
            if (row.ItemArray.Length > 3)
            {
                dCompare.styleId = Convert.ToString(row.ItemArray[3].ToString());
            }
            if (row.ItemArray.Length > 4)
            {
                dCompare.gtnPO = Convert.ToString(row.ItemArray[4].ToString());
            }
            if (row.ItemArray.Length > 5)
            {
                dCompare.idNoName = Convert.ToString(row.ItemArray[5].ToString());
            }
            if (row.ItemArray.Length > 6)
            {
                dCompare.colorId = Convert.ToString(row.ItemArray[6].ToString());
            }
            //如果大于6(第7列)  就为SIZENAME
            for(int i=0;i<row.ItemArray.Length;i++)
            {
                dCompare.sizeName = Convert.ToString(row.ItemArray[6].ToString());

            }


            if (row.ItemArray.Length > 7)
            {
                dCompare.sizeName = Convert.ToString(row.ItemArray[7].ToString());
            }
            if (row.ItemArray.Length > 8)
            {
                dCompare.qty = Convert.ToString(row.ItemArray[8].ToString());
            }
            return dCompare;
        }

        public string[] getExcelSheetSum(String filename)
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

    }
}
