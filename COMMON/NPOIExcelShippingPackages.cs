using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON
{
    public class NPOIExcelShippingPackages
    { //读EXCEL   导入EXCEL表
        public Spks ExcelRead(string file, string sheetname,string Org, string headno)
        {
            try
            {
                using (ShippingPackagesExcelHelper excelHelper = new ShippingPackagesExcelHelper(file))
                {
                    DataTable dt = excelHelper.ExcelToDataTable(sheetname, Convert.ToInt32( headno));
                   
                    if (dt is null || dt.Rows.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        
                        ShippingPackages[] ShippingPackages = new ShippingPackages[dt.Rows.Count-1];
                        ShippingPackageSizes[] shippingPackageSizes = new ShippingPackageSizes[(dt.Rows.Count - 1) * 21];
                       
                        for (int i = 1; i < dt.Rows.Count; i++)
                        {   
                            ShippingPackages[i-1] = ToModel(dt.Rows[i], Org);//这里转换过来

                           
                            for (int j = 0; j < 21; j++)
                            {
                                ShippingPackageSizes shippingPackageSize = new ShippingPackageSizes();
                                shippingPackageSize.isCancel = dt.Rows[i][0].ToString();
                                shippingPackageSize.ftyNO = dt.Rows[i][2].ToString();
                                shippingPackageSize.season = dt.Rows[i][3].ToString();
                                shippingPackageSize.masterPO = dt.Rows[i][5].ToString();
                                shippingPackageSize.gtnPO = dt.Rows[i][6].ToString();
                                shippingPackageSize.Modify = dt.Rows[i][7].ToString();
                                shippingPackageSize.po_MainLine = dt.Rows[i][8].ToString();
                                shippingPackageSize.styleNumber = dt.Rows[i][9].ToString();
                                shippingPackageSize.color = dt.Rows[i][11].ToString();
                                shippingPackageSize.sizeName = dt.Rows[0][14+j].ToString();
                                shippingPackageSize.sizeAnother = "";
                                shippingPackageSize.sizeQty = dt.Rows[i][14 + j].ToString();
                                shippingPackageSize.poQty = dt.Rows[i][35].ToString();
                                shippingPackageSize.overflow = dt.Rows[i][36].ToString();
                                shippingPackageSize.org = Org; 
                                shippingPackageSizes[(i - 1) * 21 + j ] = shippingPackageSize;
                            } 
                        }
                        Spks  spks= new Spks();
                        spks.sppack = ShippingPackages;
                        spks.spsize = shippingPackageSizes;
                        return spks;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        private ShippingPackages  ToModel(DataRow row,string Org)//建立要导入的文件的model
        {
            ShippingPackages Shippings = new ShippingPackages();


            Shippings.isCancel = row[0].ToString();
            Shippings.type =  row[1].ToString() ;
            Shippings.ftyNo = row[2].ToString();
            Shippings.season = row[3].ToString();
            Shippings.BVPO = row[4].ToString();
            Shippings.masterPO = row[5].ToString();
            Shippings.GtnPO = row[6].ToString();
            Shippings.Modify = row[7].ToString();
            Shippings.po_mainLine = row[8].ToString();
            Shippings.styleNumber = row[9].ToString();
            Shippings.styleName = row[10].ToString();
            Shippings.color = row[11].ToString();
            Shippings.colDescription = row[12].ToString();
            Shippings.channel = row[13].ToString();
            Shippings.totalQty = row[35].ToString() ;
            Shippings.overflow = row[36].ToString();
            Shippings.HOD = row[37].ToString();
            Shippings.befoeHOD = row[38].ToString();
            Shippings.newHOD = row[39].ToString();
            Shippings.shipMode = row[40].ToString();
            Shippings.sourceTag = row[41].ToString();
            Shippings.wwwt = row[42].ToString();
            Shippings.citHangTag = row[43].ToString();
            Shippings.Fastener = row[44].ToString();
            Shippings.steelNumber = row[45].ToString();
            Shippings.cup = row[46].ToString();
            Shippings.cclable = row[47].ToString();
            Shippings.sensitive = row[48].ToString();
            Shippings.remark = row[49].ToString();
            Shippings.org = Org; 


            return Shippings;
        }

        public string[] getExcelSheetSum(String filename)
        {
            ExcelHelper excelHelper = new ExcelHelper(filename);
            string[] sheetname = excelHelper.getExcelSheetName(filename);
            return sheetname;
        }
    }
}