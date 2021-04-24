using BLL;
using MODEL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmFactoryplanning : Form
    {
        private static FrmFactoryplanning frm;
        public propertysManager pm = new propertysManager();
        public DataGridView dgv = null;
        public string org = "SAA";
        public bool isSaveAndPrint = false;

        public string propertyType = "";

        //-----------------------打印代码开始-----------------------
        //１、實例化打印文檔
        private PrintDocument pdDocument = new PrintDocument();
        private int linesPrinted;
        private PrintPreviewDialog m_printPreview = new PrintPreviewDialog();//打印预览UI
        private BarCodeClass bcc = new BarCodeClass();
        private string[] orgs;//厂区
        private string[] pId;//财编号
        private string[] pName; //财产名称
        private string[] pMode; //财产型号
        private string[] pBuyData; //财产购买日期  
        private Image[] pIDimg; // 财编号二维码
        private int value = 0;//
        private int pageCount = 0;//页数
        private PrintDocument m_printDoc = new PrintDocument();
        private float m_pageWidth = 60F;//纸张宽度 mm单位
        private float m_pageHeight = 40F;//纸张高度 mm单位
        //-----------------------打印代码结束-----------------------


        public FrmFactoryplanning()
        {
            InitializeComponent();
            dgvPropertys.DoubleBufferedDataGirdView(true);

            //２、訂閱事件
            pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage);
            pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
            pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);
        }
        public static FrmFactoryplanning GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmFactoryplanning();
            }
            return frm;
        }

        private void FrmFactoryplanning_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
          //  this.gbSearch.Width = this.Width - 20;
         //   this.splitContainer1.Width = this.gbSearch.Width;           
           // this.splitContainer1.Height = this.Height - 335;
        }

        private void FrmFactoryplanning_Resize(object sender, EventArgs e)
        {
            if (this.Width <= 1066)
            {
                this.Width = 1066;
            }
            if (this.Height <= 747)
            {
                this.Height = 747;
            }
         
            this.bgProperty.Width = this.Width - 20;
            this.bgProperty.Height = this.Height - 110;

            
            this.butPrints.Left = this.Width - 120;
            this.butSave.Left = this.Width - (130 + this.butPrints.Width);

            this.progressBar1.Width = this.Width - this.gbSearch.Width - this.butPrints.Width - 150;

            this.splitContainer1.SplitterDistance = 80;

            this.gblist.Height = this.splitContainer1.Panel2.Height - 10;
            this.gblist.Width = this.splitContainer1.Panel2.Width - 10;

            this.butDeleteSelected.Left = this.splitContainer1.Panel2.Width - 100;
            //  this.splitContainer1.Height = this.Height - 335;


        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            string propertyNumbers = this.txtPropertyNumber.Text.Trim();
            if (propertyNumbers.Length <= 0)
            {
                return;
            }
            searchByPropertyIDs(propertyNumbers,0);
        }

        public void searchByPropertyIDs(string propertyNumbers, string[] pId)
        {
            // 查询本地资料
            // 查询ERP资料
            this.msg.Visible = true;
            this.progressBar1.Visible = true;

            Application.DoEvents();
            progressBar1.Value = 10;
            msg.Text = "正在查询相关财编资料,请稍等 ...";


            bool IsDel = false;
            if (cbIsDel.Checked)
            {
                IsDel = true;
            }
            else
            {
                IsDel = false;
            }
            DataTable pt = pm.getPropertysByPnumberFromLocalHost(propertyNumbers, IsDel);
            if (pt.Rows.Count <= 0)
            {
                this.dgvPropertys.DataSource = null;
                progressBar1.Value = 100;
                msg.Text = "查询完成";
                Application.DoEvents();
                this.msg.Visible = false;
                this.progressBar1.Visible = false;
                return;
            }
              //  pt.Columns.Add("propertyLocal", typeof(string));
              //  pt.Columns.Add("propertySavePerson", typeof(string));
              //  pt.Columns.Add("propertyPrintTims", typeof(int));
              //  pt.Columns.Add("propertyIsDel", typeof(int));
                foreach (DataRow row in pt.Rows)
                {
                //    row["select"] = true;
                    //row["propertyLocal"] = row["FAJ21"].ToString() + "-" + row["FAF02"].ToString();
                 //   row["propertySavePerson"] = row["FAJ19"].ToString() + "-" + row["GEN02"].ToString();
                 //   row["propertyPrintTims"] = 0;
                  //  row["propertyIsDel"] = 0;
                }
            pt.Columns.Add("select", typeof(bool)).SetOrdinal(0);

            for(int i = 0; i < pId.Length; i++)
            {
                for(int j = 0; j < pt.Rows.Count; j++)
                {
                    if (pt.Rows[j]["propertyID"].ToString() == pId[i])
                    {
                        pt.Rows[j]["select"] = true;
                    }
                }
            }
            
            for (int i = 0; i < pt.Columns.Count; i++)
            {
                if (pt.Columns[i].ColumnName != "select")
                {
                    pt.Columns[i].ReadOnly = true;
                }
            }
            this.dgvPropertys.DataSource = pt;
            changHeaderText();
            Cursor = Cursors.Default;
            this.dgvPropertys.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

            progressBar1.Value = 100;
            msg.Text = "查询完成";
            // MessageBox.Show("查询完成");
            Application.DoEvents();
            this.msg.Visible = false;
            this.progressBar1.Visible = false;
        }


        public void searchByPropertyIDs(string propertyNumbers,int isLocalHost )
        {
            // 查询本地资料
            // 查询ERP资料
            this.msg.Visible = true;
            this.progressBar1.Visible = true;

            Application.DoEvents();
            progressBar1.Value = 10;
            msg.Text = "正在查询相关财编资料,请稍等 ...";
           

            bool IsDel = false;
            if (cbIsDel.Checked)
            {
                IsDel = true;
            }
            else
            {
                IsDel = false;
            }
            DataTable pt = pm.getPropertysByPnumberFromLocalHost( propertyNumbers, IsDel);          
            if (pt.Rows.Count <= 0   )
            {
                if (isLocalHost == 0 && IsDel == false)
                {
                    pt = pm.getPropertysByPnumber(this.org, propertyNumbers);
                }
                else
                {
                    this.dgvPropertys.DataSource = null;                   
                    progressBar1.Value = 100;
                    msg.Text = "查询完成";
                    Application.DoEvents();
                  //  MessageBox.Show("查询完成");
                    this.msg.Visible = false;
                    this.progressBar1.Visible = false;
                    return;
                }
              
                pt.Columns.Add("propertyLocal", typeof(string));
                pt.Columns.Add("propertySavePerson", typeof(string));
                pt.Columns.Add("propertyPrintTims", typeof(int));
                pt.Columns.Add("propertyIsDel", typeof(int));

                foreach (DataRow row in pt.Rows)
                {
                //    row["select"] = true;
                    row["propertyLocal"] = row["FAJ21"].ToString() + "-" + row["FAF02"].ToString();
                    row["propertySavePerson"] = row["FAJ19"].ToString() + "-" + row["GEN02"].ToString();
                    row["propertyPrintTims"] = 0;
                    row["propertyIsDel"] = 0;
                }
            }

            pt.Columns.Add("select", typeof(bool)).SetOrdinal(0);
            foreach (DataRow row in pt.Rows)
            {
                if(row["propertyIsDel"].ToString() == "0")
                {
                    row["select"] = true;
                }
                                
            }

            for (int i = 0; i < pt.Columns.Count; i++)
            {
                if (pt.Columns[i].ColumnName != "select")
                {
                    pt.Columns[i].ReadOnly = true;
                }
            }
            this.dgvPropertys.DataSource = null;
            this.dgvPropertys.DataSource = pt;
            this.propertyType = "";
            changHeaderText();
            Cursor = Cursors.Default;
            this.dgvPropertys.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
          
            progressBar1.Value = 100;
            msg.Text = "查询完成";
           // MessageBox.Show("查询完成");
            Application.DoEvents();
            this.msg.Visible = false;
            this.progressBar1.Visible = false;
        }
        public void changHeaderText()
        {
           // this.dgvPropertys.Columns["id"].Visible = false;

            this.dgvPropertys.Columns["select"].HeaderText = "选择";
            this.dgvPropertys.Columns["erpid"].HeaderText = "ERP序号";
            this.dgvPropertys.Columns["org"].HeaderText = "厂别";
            this.dgvPropertys.Columns["propertyID"].HeaderText = "财编";
            this.dgvPropertys.Columns["propertyName"].HeaderText = "资产名称";
            this.dgvPropertys.Columns["propertyMode"].HeaderText = "资产型号";
            this.dgvPropertys.Columns["propertyType"].HeaderText = "固资分类";

            this.dgvPropertys.Columns["buyDate"].HeaderText = "购入日期";            
            this.dgvPropertys.Columns["propertyDept"].HeaderText = "资产归属部门";
            this.dgvPropertys.Columns["propertyLocal"].HeaderText = "资产存放位置";
            this.dgvPropertys.Columns["propertyBuyID"].HeaderText = "资产采购单号";
            this.dgvPropertys.Columns["propertySavePerson"].HeaderText = "资产保管人";
            this.dgvPropertys.Columns["propertyUnit"].HeaderText = "资产单位";

            this.dgvPropertys.Columns["propertyPrintTims"].HeaderText = "财编打印次数";
            this.dgvPropertys.Columns["propertyIsDel"].HeaderText = "是否已报废";
           



            //   this.dgvPropertys.Columns["propertyPrintTims"].HeaderText = "财编打印次数";
            //  this.dgvPropertys.Columns["propertyPrintPC"].HeaderText = "资产建立者PC名";

            // this.dgvPropertys.Columns["propertyIsDel"].HeaderText = "删除标记";
            //  this.dgvPropertys.Columns["propertyDelPC"].HeaderText = "删除者PC";
            //  this.dgvPropertys.Columns["propertyDelDate"].HeaderText = "删除日期";
            //   this.dgvPropertys.Columns["propertyDelNote"].HeaderText = "删除原因";

            //   this.dgvPropertys.Columns["propertyIsDel"].Visible = false;
            //   this.dgvPropertys.Columns["propertyDelPC"].Visible = false;
            //   this.dgvPropertys.Columns["propertyDelDate"].Visible = false;
            //   this.dgvPropertys.Columns["propertyDelNote"].Visible = false;

              // this.dgvPropertys.Columns["FAJ21"].Visible = false;
             //  this.dgvPropertys.Columns["FAF02"].Visible = false;
             //  this.dgvPropertys.Columns["FAJ19"].Visible = false;
             //  this.dgvPropertys.Columns["GEN02"].Visible = false;
        }

        private void rdbutTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbutTOP.Checked)
            {
                this.org = "TOP";
            }
            
        }

        private void rdbutSAA_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbutSAA.Checked)
            {
                this.org = "SAA";
            }
        }

        private void cbSelected_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.dgvPropertys.DataSource;
            if(dt.Rows.Count <= 0)
            {
                return;
            }
            if (cbSelected.Checked)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["select"] = true;
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["select"] = false;
                }
            }
        }

        private void butDeleteSelected_Click(object sender, EventArgs e)
        {           

            this.gbDelNote.Visible = true;
        }

        public void deleteSelected(string delnote)
        {
            List<string> propertyIDs = new List<string>();
            DataTable dt = (DataTable)this.dgvPropertys.DataSource;
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["select"].ToString() == "True" && dt.Rows[i]["propertyIsDel"].ToString() =="0")
                {
                    propertyIDs.Add(dt.Rows[i]["propertyID"].ToString());
                }
            }
            if (propertyIDs.Count <= 0)
            {
                MessageBox.Show("所选择的资料已全部报废过了");
                return;
            }
            int insertcounts = pm.delPropertysByPnumber(propertyIDs, delnote);
            if (insertcounts == -1)
            {
                MessageBox.Show("报废失败，未找到相关财编号");
            }
            else
            {
                MessageBox.Show("报废成功，共报废 " + insertcounts + " 笔财编");
            }
            this.dgvPropertys.DataSource = null;
            //显示界面更新掉
            string propertyNumbers = this.txtPropertyNumber.Text.Trim();
            if (propertyNumbers.Length <= 0)
            {
                return;
            }
            searchByPropertyIDs(propertyNumbers, 1);// 1 不查询ERP库
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            isSaveAndPrint = false;
            DataTable dt = (DataTable)this.dgvPropertys.DataSource;
            
            if (dt ==null ||  dt.Rows.Count <= 0)
            {
                return;
            }
            this.savePropertysToDB(dt, isSaveAndPrint);
            string propertyNumbers = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                propertyNumbers = propertyNumbers + "|" + dt.Rows[i]["propertyID"].ToString();
            }
           
            if (propertyNumbers.Length <= 0)
            {
                return;
            }
            searchByPropertyIDs(propertyNumbers, 0);



        }
        public int savePropertysToDB(DataTable dt,bool isSaveAndPrint)
        {
            int insertcounts = pm.insertPropertys(dt);
            if(!isSaveAndPrint )
            {
                if ( insertcounts == -1)
                {
                    MessageBox.Show("保存失败，有重复财编号");
                }
                else
                {
                    MessageBox.Show("保存成功，共保存 " + insertcounts + " 笔财编");
                }
            }
            return insertcounts;
        }

        private void txtPropertyNumber_KeyDown(object sender, KeyEventArgs e)
        {
            string propertyNumbers = this.txtPropertyNumber.Text.Trim();
            if (propertyNumbers.Length <= 0)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {              
                searchByPropertyIDs(propertyNumbers, 0);

            }
        }

        private void butDelCancel_Click(object sender, EventArgs e)
        {
            this.gbDelNote.Visible = false;
        }

        private void butDelNote_Click(object sender, EventArgs e)
        {
            string delnote = this.txtDelNote.Text.Trim();
            if (delnote.Length <= 0)
            {
                 MessageBox.Show("请输入报废原因");
                return;
            }
            this.txtDelNote.Text = "";
            this.gbDelNote.Visible = false;
            deleteSelected(delnote);
        }

        private void butGeneratePropertyNumber_Click(object sender, EventArgs e)
        {

            string org = this.cbORG.Text.Trim();
            string name = this.txtPropertyName.Text.Trim();
            string mode = this.txtPropertyMode.Text.Trim();
            string type = this.txtPropertyType.Text.Trim();
            string first = this.txtPropertyFirstNumber.Text.Trim();
            string counts= this.txtPropertyCounts.Text.Trim();
           // this.dtpPropertyBuyDate.CustomFormat = "dd-MM-yyyy";
            this.dtpPropertyBuyDate.Format = DateTimePickerFormat.Custom;
            this.dtpPropertyBuyDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            string buydate = this.dtpPropertyBuyDate.Text.Trim();

            string dept = this.txtPropertyDept.Text.Trim();
            string local = this.txtPropertyLocal.Text.Trim();
            string buyid = this.txtPropertyBuyID.Text.Trim();
            string saveperson = this.txtPropertySavePerson.Text.Trim();
            string unid = this.txtUnid.Text.Trim();

            if (org.Length <= 0)
            {
                MessageBox.Show("财产归属厂区不能为空！");
                return;
            }
            if (name.Length <= 0)
            {
                MessageBox.Show("财产名称不能为空！");
                return ;
            }
            if (mode.Length <= 0)
            {
                MessageBox.Show("财产型号不能为空！");
                return;
            }
            if (type.Length <= 0)
            {
                MessageBox.Show("财产类别不能为空！");
                return;
            }
            if (type.Length != 4)
            {
                MessageBox.Show("财产类别只能为4位！");
                return;
            }
            if (first.Length <= 0)
            {
                MessageBox.Show("财产开始编号不能为空！");
                return;
            }
            if (counts.Length <= 0)
            {
                MessageBox.Show("财产数量不能为空！");
                return;
            }
            if (buydate.Length <= 0)
            {
                MessageBox.Show("财产入库日期不能为空！");
                return;
            }

            
            progressBar1.Value = 1;
            msg.Text = "生成财编中....";
            Application.DoEvents();            
            this.msg.Visible = true;
            this.progressBar1.Visible = true;
          //  MessageBox.Show("生成财编中....");

            //计算出流水号码
            
            if (!IsWholeNumber(first))
            {
                MessageBox.Show("开始编号只能是数字！");
                return;

            }
            if (!IsWholeNumber(counts))
            {
                MessageBox.Show("财产总数只能是数字！");
                return;

            }

            this.dgvPropertys.DataSource = null;

            int fi = Convert.ToInt32(first);
            int count = Convert.ToInt32(counts);
            List<propertys> plist = new List<propertys>();

          

          //  string propertyID = "";
            for (int i = fi; i< count + fi; i++)
            {
                propertys p = new propertys();
                string pID = i.ToString();
                while (pID.Length < 4)
                {
                    pID = "0" + pID;
                }
                string propertyID = type + "-" + pID;               
                p.erpid = "";
                p.org = org;
                p.propertyID = propertyID;
                p.propertyName = name;
                p.propertyMode = mode;
                p.propertyType = type;
                p.buyDate = buydate;
                p.propertyDept = dept;
                p.propertyLocal = local;
                p.propertyBuyID = buyid;
                p.propertySavePerson = saveperson;
                p.propertyUnit = unid;
                p.propertyPrintTims = 0;
                p.propertyPrintPC = "";
                p.propertyIsDel = 0;
                p.propertyDelPC = "";
                p.propertyDelDate ="";                
                p.propertyDelNote = "";
                plist.Add(p);
            }
            // list 转datatable

            DataTable result = ToDataTableTow(plist);
            this.dgvPropertys.DataSource = result;
            progressBar1.Value = 100;
            msg.Text = "财编生成完成";
            //  this.savePropertysToDB(result);
        }
        public static DataTable ToDataTableTow(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }


        public static bool IsWholeNumber(string strNumber)
        {
            Regex g = new Regex(@"^[0-9]\d*$");
            return g.IsMatch(strNumber);
        }

        private void butPrints_Click(object sender, EventArgs e)
        {
            try
            {
                //初始化打印对话框对象
                PrintDialog printDialog1 = new PrintDialog();
                //将PrintDialog.UseEXDialog属性设置为True，才可显示出打印对话框
                printDialog1.UseEXDialog = true;
                //将printDocument1对象赋值给打印对话框的Document属性
                printDialog1.Document = pdDocument;
                //打开打印对话框
                DialogResult result = printDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //pdDocument.Print();//开始打印
                    //}

                    //調用打印
                    m_printPreview.Document = pdDocument;
                    try
                    {
                        m_printPreview.ShowDialog();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    catch (Win32Exception wx)
                    {
                        return;
                        // MessageBox.Show("已取消打印作业");
                    }
                }
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                pdDocument.Dispose();
            }         
        }

        /// <summary>
        /// ３、得到打印內容
        /// 每個打印任務衹調用OnBeginPrint()一次。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pdDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            //通用
            List<string> cartonBarcodes = new List<string>();
            string CartonBarcode = "";          

            //显示生成条码进度条
            msg.Visible = true;
            progressBar1.Visible = true;
            this.msg.Text ="正在加载条码内容，请稍等...";
            Application.DoEvents();

            for (int i = 0; i < dgvPropertys.Rows.Count; i++)
            {
                // if (dgvPropertys.Rows[i].Cells[0].Value == null)
                //{
                    //break;
               // }
                //else if (dgvPropertys.Rows[i].Cells[0].Value.ToString() == "True")
                //{
                    CartonBarcode = dgvPropertys.Rows[i].Cells["propertyID"].Value.ToString();
                    cartonBarcodes.Add(CartonBarcode);
                //}
                value = Convert.ToInt32(Convert.ToDouble(i) / dgvPropertys.Rows.Count * 100);
                progressBar1.Value = value;
                msg.Text = "加载进度" + value + " %";
                Application.DoEvents();
            }
            this.msg.Text = "加载完成";           
            msg.Visible = false;
            progressBar1.Visible = false;
            Application.DoEvents();

            if (cartonBarcodes.Count <= 0)
            {
                MessageBox.Show("请勾选要打印的条码");
                return;
            }
            //-------------------
            DataTable dt = (DataTable)this.dgvPropertys.DataSource;

            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有此财编");
                return;
            }
            this.isSaveAndPrint = true;
            this.savePropertysToDB(dt, isSaveAndPrint);
            string propertyNumbers = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                propertyNumbers = propertyNumbers + "|" + dt.Rows[i]["propertyID"].ToString();
            }

            if (propertyNumbers.Length <= 0)
            {
                return;
            }
            searchByPropertyIDs(propertyNumbers, 0);
            //---------------------


            string barcodes = "";
            for (int i=0;i< cartonBarcodes.Count; i++)
            {
                barcodes = barcodes + "|" + cartonBarcodes[i];
            }

            this.propertyType = barcodes;
            // 取出其他内容
            DataTable ibox = pm.getPropertysByPnumberFromLocalHost(barcodes);
            if (ibox == null || ibox.Rows.Count <= 0)
            {
                MessageBox.Show("没有此财编");
                pdDocument.Dispose();
                return;
            }

            progressBar1.Maximum = 100;
            pageCount = ibox.Rows.Count;
            progressBar1.Minimum = 0;
            orgs = new string[ibox.Rows.Count];
            pId = new string[ibox.Rows.Count];
            pName = new string[ibox.Rows.Count];
            pMode = new string[ibox.Rows.Count];
            pBuyData = new string[ibox.Rows.Count];

            pIDimg = new Image[ibox.Rows.Count];//条码集合   

           msg.Visible = true;
            progressBar1.Visible = true;
            msg.Text = "正在生成条码，请稍等...";        
            Application.DoEvents();

            for (int i = 0; i < ibox.Rows.Count; i++)
            {
                value = Convert.ToInt32(Convert.ToDouble(i) / ibox.Rows.Count * 100);
                progressBar1.Value = value;
                msg.Text = "完成进度： " + value + " % , 正在生成第 " + i + " 张 ，共 " + ibox.Rows.Count + " 张";
                Application.DoEvents(); 
                orgs[i] = Convert.ToString(ibox.Rows[i]["org"]);
                pId[i] = Convert.ToString(ibox.Rows[i]["propertyID"]);
                pName[i] = Convert.ToString(ibox.Rows[i]["propertyName"]);
                pMode[i] = Convert.ToString(ibox.Rows[i]["propertyMode"]);
                pBuyData[i] = Convert.ToString(ibox.Rows[i]["buyDate"]);

                 //  pIDimg[i] = Convert.ToString(ibox.Rows[i]["propertyID"]);

                //把条码存放到内存里barcode[]
               // pIDimg[i] = Convert.ToString(ibox.Rows[i]["propertyID"]);

                    this.pictureBox1.Width = (int)(40 / 25.4 * 100);//生成条码的长度 单位mm
                    this.pictureBox1.Height = (int)(7 / 25.4 * 100);//生成条码的长度 单位mm

                    bcc.CreateBarCode(pictureBox1, pId[i].ToUpper());//创建条码

                    if (pictureBox1.Image == null)
                    {
                        MessageBox.Show("创建条码失败");
                        return;
                    }
                    else
                    {
                    //把条码图片存放到内存里barcodeimg[]
                    pIDimg[i] = pictureBox1.Image;
                        //  pdDocument = new PrintDocument();//实例打印文档对象
                        ////m_printPreview = new PrintPreviewDialog();
                        ////m_printPreview.PrintPreviewControl.AutoZoom = false;
                        //////取得屏幕大小
                        ////m_printPreview.PrintPreviewControl.Zoom = 1;

                        ////m_printPreview.Width = Screen.PrimaryScreen.Bounds.Width;
                        ////m_printPreview.Height = Screen.PrimaryScreen.Bounds.Height;

                        //自定义纸张大小
                        pdDocument.DefaultPageSettings.PaperSize = new PaperSize("newPage40X60"
                       , (int)(m_pageWidth / 25.4 * 100)
                       , (int)(m_pageHeight / 25.4 * 100));

                        //自定义图片内容整体上间距/左间距
                        pdDocument.OriginAtMargins = true;
                        pdDocument.DefaultPageSettings.Margins.Top = (int)(0 / 25.4 * 100);//顶边距离
                        pdDocument.DefaultPageSettings.Margins.Left = (int)(0 / 25.4 * 100);  //左边距离
                                                                                              //currentPageIndex = i;
                    }
                    //m_printPreview.Document = pdDocument;
                    // m_printPreview.ShowDialog();
                
            }
            msg.Text = "条码生成完成";           
            msg.Visible = false;
            progressBar1.Visible = false;
            Application.DoEvents();

            //写入条码打印次数
            //MessageBox.Show("打印条码完成");
            //
            this.uppropertysPrint(pId);
        }
        

        
        public void uppropertysPrint(string[] pId)
        {
            if(pId.Length<=0)
            {
                return;
            }
            for(int i = 0; i < pId.Length; i++)
            {
                string pids = pId[i];
            }
             
            int insertcounts = pm.upPrintPropertysByPnumber(pId);
            if (insertcounts == -1)
            {
                MessageBox.Show("打印失败，未找到相关财编号");
            }
            else
            {
              //  MessageBox.Show("打印成功，共打印 " + insertcounts + " 笔财编");
            }
            this.dgvPropertys.DataSource = null;
            string propertyNumbers = "";
            if (this.propertyType.Length <= 0)
            {
                propertyNumbers = this.txtPropertyNumber.Text.Trim();
            }
            else
            {
                propertyNumbers = this.propertyType;
            }
            
            //显示界面更新掉
           
            if (propertyNumbers.Length <= 0)
            {
                return;
            }
            searchByPropertyIDs(propertyNumbers,  pId);// 1 不查询ERP库
        }


        /// <summary>
        /// ４、繪制多個打印頁面
        /// printDocument的PrintPage事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            /*
             * 得到TextBox中每行的字符串數組
             * \n換行
             * \r回車
             */

            //int x = 20;
            //int y = 20;
            if (pIDimg == null || pIDimg.Length <= 0)
            {
                //   MessageBox.Show("取消打印");
                pdDocument.Dispose();
                return;
            }
            msg.Visible = true;
            progressBar1.Visible = true;
            msg.Text = "正在生成条码，请稍等...";
           
            //linesPrinted 现在要画的第几个条码
            while (linesPrinted < pIDimg.Length)
            {
                //其它条码格式
                //繪製要打印的頁面
                //创建文本信息

                int left = Convert.ToInt32(2 / 25.4 * 100);//左距离
                int top = Convert.ToInt32(2 / 25.4 * 100);//顶距离
                e.Graphics.DrawString("Sabrina", new Font("Arial Black", 10), Brushes.Black, left, top);//厂区

                 left = Convert.ToInt32(2 / 25.4 * 100);//左距离
                 top = Convert.ToInt32(6 / 25.4 * 100);//顶距离
                 e.Graphics.DrawString(orgs[linesPrinted].ToUpper()+"固定資產標示卡:", new Font("微軟正黑體", 8,FontStyle.Bold), Brushes.Black, left, top);//厂区

                left = Convert.ToInt32(9 / 25.4 * 100);//左距离
                    top = Convert.ToInt32(10 / 25.4 * 100);//顶距离
                    e.Graphics.DrawString("財產編號:" + pId[linesPrinted].ToUpper(), new Font("微軟正黑體", 8, FontStyle.Bold), Brushes.Black, left, top);//财产编号  
                
                left = Convert.ToInt32(20 / 25.4 * 100);//左距离
                top = Convert.ToInt32(34 / 25.4 * 100);//顶距离
                e.Graphics.DrawString( pId[linesPrinted].ToUpper(), new Font("微軟正黑體", 6, FontStyle.Bold), Brushes.Black, left, top);//财产编号  
                

                left = Convert.ToInt32(9 / 25.4 * 100);//左距离
                top = Convert.ToInt32(14 / 25.4 * 100);//顶距离
                e.Graphics.DrawString("財產名稱:" + pName[linesPrinted].ToUpper(), new Font("微軟正黑體", 8, FontStyle.Bold), Brushes.Black, left, top);//财产名称

                left = Convert.ToInt32(9 / 25.4 * 100);//左距离
                top = Convert.ToInt32(18 / 25.4 * 100);//顶距离
                e.Graphics.DrawString("財產型號:" + pMode[linesPrinted].ToUpper(), new Font("微軟正黑體", 8, FontStyle.Bold), Brushes.Black, left, top);//财产型号

                left = Convert.ToInt32(9 / 25.4 * 100);//左距离
                top = Convert.ToInt32(22 / 25.4 * 100);//顶距离
                e.Graphics.DrawString("購入日期:" + pBuyData[linesPrinted].Substring(0,10), new Font("微軟正黑體", 8, FontStyle.Bold), Brushes.Black, left, top);//购入日期

                left = Convert.ToInt32(42 / 25.4 * 100);//左距离
                top = Convert.ToInt32(36 / 25.4 * 100);//顶距离
                e.Graphics.DrawString("Ownership by SAB", new Font("微軟正黑體", 5, FontStyle.Bold), Brushes.Black, left, top);//购入日期


                // 画框
                //画表格横线


                //画表格竖线
                Pen myPen = new Pen(Color.FromArgb(255, Color.Black), 0.8F);
                //    for (int i = 1; i < 2; i++)
                //   {
                //画表格横线
                e.Graphics.DrawLine(myPen, new Point(3, 3), new Point(230, 3));
                    e.Graphics.DrawLine(myPen, new Point(3, 152), new Point(230, 152));

                //画表格竖线
                e.Graphics.DrawLine(myPen, new Point(3, 3), new Point(3, 152));
                e.Graphics.DrawLine(myPen, new Point(230, 3), new Point(230, 152));
                //  e.Graphics.DrawLine(myPen,)
                //  }

                // for (int i = 0; i < 40; i ++)
                // {
                //     e.Graphics.DrawLine(myPen, new Point(i, 200), new Point(i, 350));
                // }


                left = Convert.ToInt32(5 / 25.4 * 100);//条码左距离  
                    top = Convert.ToInt32(27 / 25.4 * 100);//条码顶距离
                    e.Graphics.DrawImage(pIDimg[linesPrinted], left, top);//左，顶   财产编码二维码
                    // y += 55;
                    linesPrinted++;
                    value = Convert.ToInt32(Convert.ToDouble(linesPrinted) / pIDimg.Length * 100);
                    progressBar1.Value = value;
                    msg.Text = "生成条码" + value + " %";
                    Application.DoEvents(); 

                //判斷超過一頁時，允許進行多頁打印
                if (pIDimg.Length > linesPrinted)
                {
                    //允許多頁打印
                    e.HasMorePages = true;
                    /*
                     * PrintPageEventArgs類的HaeMorePages屬性為True時，通知控件器，必須再次調用OnPrintPage()方法，打印一個頁面。
                     * PrintLoopI()有一個用於每個要打印的頁面的序例。如果HasMorePages是False，PrintLoop()就會停止。
                     */
                    return;
                }
            }
            msg.Text = "条码生成完成";           
            msg.Visible = false;
            progressBar1.Visible = false;
            linesPrinted = 0;
            //繪制完成後，關閉多頁打印功能
            e.HasMorePages = false;
           
        }
        /// <summary>
        /// 在图片上画框
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="RectColor"></param>
        /// <param name="LineWidth"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static Bitmap DrawRectangleInPicture(Bitmap bmp, Point p0, Point p1, Color RectColor, int LineWidth, DashStyle ds)
        {
            if (bmp == null) return null;


            Graphics g = Graphics.FromImage(bmp);

            Brush brush = new SolidBrush(RectColor);
            Pen pen = new Pen(brush, LineWidth);
            pen.DashStyle = ds;

            g.DrawRectangle(pen, new Rectangle(p0.X, p0.Y, Math.Abs(p0.X - p1.X), Math.Abs(p0.Y - p1.Y)));

            g.Dispose();

            return bmp;
        }



        /// <summary>
        ///５、EndPrint事件,釋放資源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pdDocument_EndPrint(object sender, PrintEventArgs e)
        {
            //變量Lines占用和引用的字符串數組，現在釋放
            orgs = null;
            pId = null;
            pName = null;
            pMode = null;
            pBuyData = null;
            pIDimg = null;

        }

        private void dgvPropertys_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        public int hiedcolumnindex = -1; //是否选中外面
        private void dgvPropertys_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (this.dgvPropertys.Rows[e.RowIndex].Selected == false)
                    {
                        this.dgvPropertys.ClearSelection();
                        this.dgvPropertys.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (this.dgvPropertys.SelectedRows.Count == 1)
                    {
                        this.dgvPropertys.CurrentCell = this.dgvPropertys.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    MenuRight.Show(MousePosition.X, MousePosition.Y);
                    // MessageBox.Show("点右键了");
                }

                else if (e.ColumnIndex >= 0)
                {
                    this.hiedcolumnindex = e.ColumnIndex;
                    MenuRight.Show(MousePosition.X, MousePosition.Y);

                }

            }
        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dgvPropertys.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dgvPropertys.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            ImproExcel();
        }
        public void ImproExcel()
        {
            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";
            //   sdfExport.ShowDialog();
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            NPOIExcelHelper NPOIexcel = new NPOIExcelHelper();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(this.dgvPropertys);

            // DataTable dt = (StyleCodeInfodataGridView.DataSource as DataTable);
            NPOIexcel.ExcelWrite(filename, tabl);//excelhelper写出
            if (MessageBox.Show("导出成功，文件保存在" + filename.ToString() + ",是否打开此文件？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
    }
}
