
using BLL;
using COMMON;
using MODEL;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace WinForm
{
    public partial class FrmaccessoryOut : Form
    {
        private static FrmaccessoryOut frm;
        private BLL.accessoryOutManager accoryOut = new accessoryOutManager();
        public   xiaomingCommom myCommon = new xiaomingCommom();
        private BackgroundWorker bgWorker = new BackgroundWorker();
        private Secont secont = new Secont();
        private TestLinManager LinSerTest = new TestLinManager();
        private string serviceIP = "";
        private string serviceName = "";
        private bool isLinSer = false;
        public DataTable accessoryDT = new DataTable();
        List<allqtys> allqtys = new List<allqtys>();
        
        public string reNo = ""; //发料单号
        public string reNoBatch = "";//发料单批号

        public List<parameter> items = new List<parameter>();
        public string Org = "SAA";
         
        public FrmaccessoryOut()
        {
            InitializeComponent();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += DoWork_getAccessOut;
            bgWorker.ProgressChanged += ProgressChanged_Handler;
            bgWorker.RunWorkerCompleted += RunWorkerCompleted_Handler;



            DoubleBufferDataGridView.DoubleBufferedDataGirdView(this.dgvAccessory,true);


        }
        public static FrmaccessoryOut GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmaccessoryOut();
            }
            return frm;
        }

        private void FrmaccessoryOut_Resize(object sender, EventArgs e)
        {
            if (this.Width <= 1150)
            {
                this.Width = 1150;
            }
            if (this.Height <= 511)
            {
                this.Height = 511;
            }

            gbSearchLists.Top = 5;
            gbSearchLists.Left = 5;
            //gbSearchLists.Width = this.Width - 20;

            btPrint.Left = this.Width - 110;
            //btConfirmOut.Left = gbSearchLists.Width - 130;

            gbViews.Width = this.Width - 20;
            gbViews.Height = this.Height - gbSearchLists.Height - 60;
        }

        private void btSearch_Click(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor;
            if (bgWorker.IsBusy)
            {
                MessageBox.Show("系统正在运行，请稍等...");
            }
            else
            {
                bgWorker.RunWorkerAsync();
            }


            /*

            BackgroundWorker worker = sender as BackgroundWorker; 

            parameter p = new parameter();
            this.items.Clear();
            // 自编单号
            if (cbMyNo.Checked && txtMyNo.Text.Trim() != "")
            {
                p.pkey = "myNumber";
                p.pvalue = txtMyNo.Text;
                this.items.Add(p);
            }
            else
            {
                var whereRemove = items.FirstOrDefault(t => t.pkey == "myNumber");
                this.items.Remove(whereRemove);
            }
            // 款号           
            if (cbStyle.Checked && txtStyle.Text.Trim() != "")
            {
                p.pkey = "Style";
                p.pvalue = txtStyle.Text;
                this.items.Add(p);
            }
            else
            {
                var whereRemove = items.FirstOrDefault(t => t.pkey == "Style");
                this.items.Remove(whereRemove);
            }

            // 采购单号
            if (cbPurNo.Checked && txtPurNo.Text != "")
            {
                p.pkey = "purNumber";
                p.pvalue = txtPurNo.Text;
                this.items.Add(p);
            }
            else
            {
                var whereRemove = items.FirstOrDefault(t => t.pkey == "purNumber");
                this.items.Remove(whereRemove);
               
            }

            // 领料单号
            if (cbReceiveNumber.Checked && txtReceiveNumber.Text != "")
            {
                p.pkey = "receiveNumber";
                p.pvalue = txtReceiveNumber.Text;
                this.items.Add(p);
            }
            else
            {
                var whereRemove = items.FirstOrDefault(t => t.pkey == "receiveNumber");
                this.items.Remove(whereRemove);
            }
            if (this.items.Count <= 0)
            {
                MessageBox.Show("请至少指定一个查询条件");
                return;
            }
          
            Cursor = Cursors.WaitCursor;


            secont.Status = "正在获取数据...";
            secont.Note1 = "---";
            secont.Statusname = "进度：";
            secont.Value = 1;
            secont.Now = "1";
            secont.All = "100";
            secont.Note2 = " /  ";
            secont.Maxvalue = 100;
            worker.ReportProgress(1 * 100 / 1, secont);

            // 从本地数据库查询资料，如果没有再从远程库查询，并写入（本库）
            List<accessoryOut> accessorytb = accoryOut.getAccessoryOutByLocalHostDB(items, Org);

            if (accessorytb != null && accessorytb.Count > 0)
            {

                this.dgvAccessory.DataSource = accessorytb;

                secont.Status = "获取数据完成";
                secont.Note1 = "---";
                secont.Statusname = "进度：";
                secont.Value = 100;
                secont.Now = "100";
                secont.All = "100";
                secont.Note2 = " /  ";
                secont.Maxvalue = 100;
                worker.ReportProgress(1 * 100 / 100, secont);
            }
            else
            {
                this.items.Clear();
                // 自编单号
                if (cbMyNo.Checked && txtMyNo.Text.Trim() != "")
                {
                    p.pkey = "myNumber";
                    p.pvalue = txtMyNo.Text;
                    this.items.Add(p);
                }
                else
                {
                    var whereRemove = items.FirstOrDefault(t => t.pkey == "myNumber");
                    this.items.Remove(whereRemove);
                }
                if (items.Count <= 0)
                {
                    MessageBox.Show("没有资料，需要自编单号ERP资料库获取数据。");

                    secont.Status = "获取数据失败";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 100;
                    secont.Now = "0";
                    secont.All = "100";
                    secont.Note2 = " /  ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(1 * 100 / 1, secont);
                    return;
                }
                else
                {
                    secont.Status = "正在从BEST获取色组数据...";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 100;
                    secont.Now = "10";
                    secont.All = "100";
                    secont.Note2 = " /  ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(1 * 100 / 10, secont);

                    //从BEST 查询订单号
                    //查询色组
                    List<groupCloNames> groupCloNames = accoryOut.getColorFromBestDBByMyNo(txtMyNo.Text);
                    string od_no = "";

                    this.cbGroupColor.Items.Clear(); // 色组
                    this.cklistBoxPO.Items.Clear();  // PO
                    this.cklistSize.Items.Clear();   // 成衣SIZE      

                    if (groupCloNames != null && groupCloNames.Count > 0)
                    {

                        List<string> groupColorNames = new List<string>();
                        for (int i = 0; i < groupCloNames.Count; i++)
                        {
                            string groupColorName = Convert.ToString(groupCloNames[i].clr_no); // 色组  
                            od_no = Convert.ToString(groupCloNames[i].od_no).Trim();
                            bool igName = Isgroups(groupColorNames, groupColorName);
                            if (!igName)
                            {

                                this.cbGroupColor.Items.Add(groupColorName);
                                groupColorNames.Add(groupColorName);
                            }
                        }
                    }

                  
                    if (od_no.Length <= 0)
                    {
                        MessageBox.Show("没有找到订单号");
                        secont.Status = "获取订单号失败";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 100;
                        secont.Now = "100";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(1 * 100 / 100, secont);

                        return;
                    }

                    //查询PO#
                    secont.Status = "正在从BEST获取PO数据...";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 100;
                    secont.Now = "20";
                    secont.All = "100";
                    secont.Note2 = " /  ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(1 * 100 / 20, secont);

                    List<groupPONumber> groupPONumber = accoryOut.getPOFromBestDBByOd_no(od_no);
                    if (groupPONumber != null && groupPONumber.Count > 0)
                    {
                        List<string> groupPONumbers = new List<string>();
                        for (int i = 0; i < groupPONumber.Count; i++)
                        {
                            string PONumber = Convert.ToString(groupPONumber[i].po_no);
                            bool igName = Isgroups(groupPONumbers, PONumber);
                            if (!igName)
                            {
                                this.cklistBoxPO.Items.Add(PONumber);
                                groupPONumbers.Add(PONumber);
                            }
                        }

                        // this.cbGroupColor.Items.Add(groupColorNames);
                    }
            

                    // 查询成衣Size
                    secont.Status = "正在从BEST获取尺码数据...";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 100;
                    secont.Now = "30";
                    secont.All = "100";
                    secont.Note2 = " /  ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(1 * 100 / 30, secont);

                    List<groupSizeNmae> groupSizeName = accoryOut.getSizeFromBestDBByOd_no(od_no);
                    if (groupSizeName != null && groupSizeName.Count > 0)
                    {
                        List<string> groupSizeNames = new List<string>();
                        for (int i = 0; i < groupSizeName.Count; i++)
                        {
                            string SizeName1 = Convert.ToString(groupSizeName[i].SizeName01);
                            if (SizeName1.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName1);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName1);
                                    groupSizeNames.Add(SizeName1);
                                }
                            }

                            string SizeName2 = Convert.ToString(groupSizeName[i].SizeName02);
                            if (SizeName2.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName2);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName2);
                                    groupSizeNames.Add(SizeName2);
                                }
                            }
                            string SizeName3 = Convert.ToString(groupSizeName[i].SizeName03);
                            if (SizeName3.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName3);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName3);
                                    groupSizeNames.Add(SizeName3);
                                }
                            }
                            string SizeName4 = Convert.ToString(groupSizeName[i].SizeName04);
                            if (SizeName4.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName4);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName4);
                                    groupSizeNames.Add(SizeName4);
                                }
                            }
                            string SizeName5 = Convert.ToString(groupSizeName[i].SizeName05);
                            if (SizeName5.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName5);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName5);
                                    groupSizeNames.Add(SizeName5);
                                }
                            }
                            string SizeName6 = Convert.ToString(groupSizeName[i].SizeName06);
                            if (SizeName6.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName6);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName6);
                                    groupSizeNames.Add(SizeName6);
                                }
                            }
                            string SizeName7 = Convert.ToString(groupSizeName[i].SizeName07);
                            if (SizeName7.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName7);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName7);
                                    groupSizeNames.Add(SizeName7);
                                }
                            }
                            string SizeName8 = Convert.ToString(groupSizeName[i].SizeName08);
                            if (SizeName8.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName8);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName8);
                                    groupSizeNames.Add(SizeName8);
                                }
                            }
                            string SizeName9 = Convert.ToString(groupSizeName[i].SizeName09);
                            if (SizeName9.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName9);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName9);
                                    groupSizeNames.Add(SizeName9);
                                }
                            }
                            string SizeName10 = Convert.ToString(groupSizeName[i].SizeName10);
                            if (SizeName10.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName10);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName10);
                                    groupSizeNames.Add(SizeName10);
                                }
                            }
                            string SizeName11 = Convert.ToString(groupSizeName[i].SizeName11);
                            if (SizeName11.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName11);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName11);
                                    groupSizeNames.Add(SizeName11);
                                }
                            }
                            string SizeName12 = Convert.ToString(groupSizeName[i].SizeName12);
                            if (SizeName12.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName12);
                                if (!igName)
                                {
                                    this.cklistSize.Items.Add(SizeName12);
                                    groupSizeNames.Add(SizeName12);
                                }
                            }

                        }

                        // this.cbGroupColor.Items.Add(groupColorNames);
                    }

                    // 查询有SIZE的物料   查出SIZE
                    /*
                    
                    List<materialSize> MaterialSizeName = accoryOut.getMaterialSizeFromBestDBByOd_no(od_no);
                    if (MaterialSizeName != null && MaterialSizeName.Count > 0)
                    {
                        this.dgvAccessory.DataSource = MaterialSizeName;
                    }
                     
                    // 查询所有物料  总数量  不分PO  分SIZE 颜色  materialID     
                    

                    string myNumber = txtMyNo.Text.Trim();
                    if (myNumber.Length <= 0)
                    {
                        MessageBox.Show("请输入自编单号");
                        secont.Status = "没有自编单号...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 100;
                        secont.Now = "100";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(1 * 100 / 100, secont);
                        return;
                    }

                    secont.Status = "正在从BEST获取物料数据...";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 100;
                    secont.Now = "40";
                    secont.All = "100";
                    secont.Note2 = " /  ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(1 * 100 / 40, secont);

                    List<materials> groupMaterials = accoryOut.getMaterialsFromBestDBByOd_no(myNumber);
                    if (groupMaterials != null && groupMaterials.Count > 0)
                    {
                        // 1、查询出ERP库的数据

                        secont.Status = "正在从ERP获取工单信息...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 100;
                        secont.Now = "50";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(1 * 100 / 50, secont);

                        List<accessoryOut> materiDBFromERP = accoryOut.getAccessoryOutByParameters(this.items, this.Org);
                        if (materiDBFromERP is null || materiDBFromERP.Count < 0)
                        {
                            MessageBox.Show("ERP 没有资料,可能工单还未开立");

                            secont.Status = "获取工单信息失败.未找到工单号";
                            secont.Note1 = "---";
                            secont.Statusname = "进度：";
                            secont.Value = 100;
                            secont.Now = "100";
                            secont.All = "100";
                            secont.Note2 = " /  ";
                            secont.Maxvalue = 100;
                            worker.ReportProgress(1 * 100 / 100, secont);

                            return;
                        }

                        secont.Status = "获取数据完成，正在合并计算...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 100;
                        secont.Now = "60";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(1 * 100 / 60, secont);

                        int? qtys = materiDBFromERP[0].products;
                        this.labQtys.Text = "订单数量："+ qtys;
                        string ERPmas_id = "";
                        for (int i = 0; i< groupMaterials.Count; i++)
                        {
                            for( int j = 0; j < materiDBFromERP.Count; j++)
                            {
                                ERPmas_id = materiDBFromERP[j].materialID;
                                if(ERPmas_id.Substring(0,1) == "-")
                                {
                                    ERPmas_id = ERPmas_id.Substring(1, ERPmas_id.Length - 1);
                                }
                                if (groupMaterials[i].mas_id == ERPmas_id)
                                {
                                    groupMaterials[i].mas_sortNumber = materiDBFromERP[j].sortNumber;
                                    groupMaterials[i].mas_sortName = materiDBFromERP[j].sortName;
                                }

                                secont.Status = "获取数据完成，正在合并计算...";
                                secont.Note1 = "---";
                                secont.Statusname = "进度：";
                                secont.Value = i;
                                secont.Now = i.ToString();
                                secont.All = materiDBFromERP.Count.ToString();
                                secont.Note2 = " /  ";
                                secont.Maxvalue = materiDBFromERP.Count;
                                worker.ReportProgress(100 * i / materiDBFromERP.Count, secont);
                            }

                        }
                        secont.Status = "获取数据完成，正在合并计算...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 100;
                        secont.Now = "80";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(1 * 100 / 80, secont);

                        groupMaterials.Sort(
                           delegate (materials n1, materials n2)
                           {
                               return n1.mas_sortNumber.CompareTo(n2.mas_sortNumber);
                           }
                         );

                       
                        DisplayCol(dgvAccessory, "be_id", "生产厂区");
                        DisplayCol(dgvAccessory, "my_no", "自编单号");
                        DisplayCol(dgvAccessory, "od_no", "BEST订单号码");
                        DisplayCol(dgvAccessory, "style_id", "款号");
                        DisplayCol(dgvAccessory, "clr_no", "成衣色组号");
                        DisplayCol(dgvAccessory, "mas_sortNumber", "物料分群码");
                        DisplayCol(dgvAccessory, "mas_sortName", "物料分群名称");
                        DisplayCol(dgvAccessory, "mas_id", "物料ID");
                        DisplayCol(dgvAccessory, "mas_name", "物料名称");
                        DisplayCol(dgvAccessory, "color_no", "物料颜色");
                        DisplayCol(dgvAccessory, "color_name2", "颜色中文名称");

                        DisplayCol(dgvAccessory, "size", "物料尺码");
                        DisplayCol(dgvAccessory, "unit_qty", "单位用量");
                        DisplayCol(dgvAccessory, "pu_qty", "采购数量");
                        DisplayCol(dgvAccessory, "style_mas_qty", "款式总用量");
                        DisplayCol(dgvAccessory, "unit_id", "物料单位");
                        DisplayCol(dgvAccessory, "trans_rate", "单位转换比率");
                        DisplayCol(dgvAccessory, "mas_qty", "应发料量");
                        DisplayCol(dgvAccessory, "unit_id_p", "应发料单位");

                        DisplayCol(dgvAccessory, "pu_no", "采购单号");
                        DisplayCol(dgvAccessory, "vend_id", "供应商编码");
                        DisplayCol(dgvAccessory, "vend_abbr", "供应商简称");
                        DisplayCol(dgvAccessory, "per_id", "审核人员");

                        secont.Status = "计算数据完成";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 100;
                        secont.Now = "100";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(1 * 100 / 100, secont);

                        this.dgvAccessory.DataSource = groupMaterials;
                    }

                }
            }        
            //this.dgvAccessory.DataSource = accessorytbH;
            this.dgvAccessory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            Cursor = Cursors.Default;
            */
        }

        public bool Isgroups(List<string> groups, string groupName)
        {

            if (groups.Count <= 0)
            {
                return false;
            }
            for (int i = 0; i < groups.Count; i++)
            {
                if (groupName.Equals(groups[i]))
                {
                    return true;
                }

            }
            return false;
        }

        public void DoWork_getAccessOut(object sender, DoWorkEventArgs args)
        {
            /*

            BackgroundWorker worker = sender as BackgroundWorker;

            secont.Status =  "正在从ERP获取数据...";
            secont.Note1 = "---";
            secont.Statusname = "进度：";
            secont.Value = 50;
            secont.Now = "50";
            secont.All = "100";
            secont.Note2 = " /  ";
            secont.Maxvalue = 100; 
            worker.ReportProgress(1 * 100 / 20, secont);
           
            // 1、查询出ERP库的数据
            List<accessoryOut> accessorytb = accoryOut.getAccessoryOutByParameters(this.items, this.Org);


            if (accessorytb is null ||  accessorytb.Count < 0){
                MessageBox.Show("没有资料");
                return;
            }
            
                this.Invoke(
               new Action(
                       delegate
                       {
                           this.dgvAccessory.DataSource = null;
                           //出问题的代码块
                         
                           DisplayCol(dgvAccessory, "myNumber", "自编单号");
                           DisplayCol(dgvAccessory, "Style", "款号");
                           DisplayCol(dgvAccessory, "manufacturingNumber", "工单号");
                           DisplayCol(dgvAccessory, "custName", "客户");
                           DisplayCol(dgvAccessory, "season", "季节");
                           DisplayCol(dgvAccessory, "monthBuy", "月BUY");

                           DisplayCol(dgvAccessory, "firstPO", "原始PO");                           
                           DisplayCol(dgvAccessory, "purNumber", "BEST采购单号");
                           DisplayCol(dgvAccessory, "orderNumber", "订单号码");

                           DisplayCol(dgvAccessory, "products", "订单数量");
                           DisplayCol(dgvAccessory, "materialID", "物料ID");
                           DisplayCol(dgvAccessory, "materialName", "物料号称");
                           DisplayCol(dgvAccessory, "spec", "规格");
                           DisplayCol(dgvAccessory, "colorCode", "物料颜色");
                           DisplayCol(dgvAccessory, "standardQuantity", "标准用量");
                           DisplayCol(dgvAccessory, "practicalQuantity", "实际用量");
                           DisplayCol(dgvAccessory, "Quantity", "应发料量");
                           DisplayCol(dgvAccessory, "alreadyQuantity", "已发料量");
                           DisplayCol(dgvAccessory, "lastQuantity", "未发料量");
                           DisplayCol(dgvAccessory, "unit", "物料单位");
                           DisplayCol(dgvAccessory, "bodyDescription", "部位说明");
                           DisplayCol(dgvAccessory, "sortNumber", "分群码");
                           DisplayCol(dgvAccessory, "sortName", "分群名称");
                           DisplayCol(dgvAccessory, "bigType", "大类码");

                           DisplayCol(dgvAccessory, "sizeName", "成品尺码");
                           DisplayCol(dgvAccessory, "groupColor", "成品色组");

                           DisplayCol(dgvAccessory, "createPerson", "开单人");
                           DisplayCol(dgvAccessory, "createDate", "开单日期时间");
                           DisplayCol(dgvAccessory, "changePerson", "最后修改人");
                           DisplayCol(dgvAccessory, "changeDate", "最后修改时间");

                           DisplayCol(dgvAccessory, "receiveNumber", "领料单号"); // 领料单号  日期月+料类别+6码流水
                           DisplayCol(dgvAccessory, "receiveNumberBatch", "发料批号"); // 4码流水
                           DisplayCol(dgvAccessory, "materialStatus", "物料发放状态"); //  O完成  N未发料  P发了一部分  进行中
                           DisplayCol(dgvAccessory, "printTimes", "打印次数");
                           this.dgvAccessory.DataSource = accessorytb;
                       }
                   )); 
            this.dgvAccessory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

            //3、 保存到本系统数据库 outassist 表 accessoryout
            accoryOut.writeAccessoryToDB(accessorytb);
            // 3.2 从2.1里保存的本地资料库  更新新数据

            // 4、根据ERP的数据据  查询BEST的数据
            //
           // List<accessoryOut> accessorytb = accoryOut.getAccessoryOutByParameters(this.items, this.Org);


            // 5、更新到本系统数据库   outassist 表 accessoryout

            */
            //parameter p = new parameter();
            BackgroundWorker worker = sender as BackgroundWorker;
            this.Invoke(
               new Action(
                       delegate
                       {
                           this.items.Clear();
                           // 自编单号
                           if (cbMyNo.Checked && txtMyNo.Text.Trim() != "")
                           {
                               parameter p = new parameter();
                               p.pkey = "my_no";
                               p.pvalue = txtMyNo.Text.Trim();
                               this.items.Add(p);
                           }
                           else
                           {
                               var whereRemove = items.FirstOrDefault(t => t.pkey == "my_no");
                               this.items.Remove(whereRemove);
                           }
                           // 款号           
                           if (cbStyle.Checked && txtStyle.Text.Trim() != "")
                           {
                               parameter p = new parameter();
                               p.pkey = "style_id";
                               p.pvalue = txtStyle.Text.Trim();
                               this.items.Add(p);
                           }
                           else
                           {
                               var whereRemove = items.FirstOrDefault(t => t.pkey == "style_id");
                               this.items.Remove(whereRemove);
                           }

                           // 采购单号
                           if (cbPurNo.Checked && txtPurNo.Text.Trim() != "")
                           {
                               parameter p = new parameter();
                               p.pkey = "pu_no";
                               p.pvalue = txtPurNo.Text.Trim();
                               this.items.Add(p);
                           }
                           else
                           {
                               var whereRemove = items.FirstOrDefault(t => t.pkey == "pu_no");
                               this.items.Remove(whereRemove);

                           }

                           // 领料单号
                           if (cbReceiveNumber.Checked && txtReceiveNumber.Text.Trim() != "" && txtReceiveBatch.Text.Trim() != "")
                           {
                               parameter p = new parameter();
                               p.pkey = "receiveNumber";
                               p.pvalue = txtReceiveNumber.Text.Trim();
                               this.items.Add(p);

                               parameter s = new parameter();
                               s.pkey = "receiveNumberBatch";
                               s.pvalue = txtReceiveBatch.Text.Trim();
                               this.items.Add(s);
                           }
                           else
                           {
                               var whereRemove = items.FirstOrDefault(t => t.pkey == "receiveNumberBatch");
                               this.items.Remove(whereRemove);

                                whereRemove = items.FirstOrDefault(t => t.pkey == "receiveNumberBatch");
                               this.items.Remove(whereRemove);
                           }
                        //   if (this.items.Count <= 0)
                         //  {
                             //  MessageBox.Show("请至少指定一个查询条件");
                              // return;
                         //  }
                          // this.items.Clear();

                           nulldv();
                       }
                   ));

            if (this.items.Count <= 0)
            {
                MessageBox.Show("请至少指定一个查询条件");
                return;
            }
            //  Cursor = Cursors.WaitCursor;
            secont.Status = "正在获取数据...";
            secont.Note1 = "---";
            secont.Statusname = "进度：";
            secont.Value = 1;
            secont.Now = "1";
            secont.All = "100";
            secont.Note2 = " /  ";
            secont.Maxvalue = 100;
            worker.ReportProgress(1 / 100 * 100, secont);

            // 从本地数据库查询资料，如果没有再从远程库查询，并写入（本库）
            List<accessoryOut> accessorytb = accoryOut.getAccessoryOutByLocalHostDB(items, Org);

            if (accessorytb != null && accessorytb.Count > 0)
            {
                this.Invoke(
                    new Action(
                      delegate
                      {
                       //   this.dgvAccessory.DataSource = null;
                        //  this.dgvAccessory.DataSource = accessorytb;

                          DataTable dt = this.accoryOut.ToDataTable(accessorytb);

                          dt.Columns.Add("select", typeof(bool)).SetOrdinal(0);
                          foreach (DataRow row in dt.Rows)
                          {
                              row["select"] = true;
                          }
                          this.dgvAccessory.DataSource = dt;

                          accessoryDT = null;
                          accessoryDT = dt;
                          nulldv();
                          this.dgvAccessory.DataSource = null;
                          this.dgvAccessory.DataSource = accessoryDT;
                          foreach (DataGridViewColumn dgvc in dgvAccessory.Columns)
                          {
                              if (dgvc.DataPropertyName == "select" || dgvc.DataPropertyName == "mas_qty")
                              {
                                  dgvc.ReadOnly = false;

                              }
                              else
                              {
                                  dgvc.ReadOnly = true;

                              }
                          }

                          changHeaderText();

                          Cursor = Cursors.Default;
                          this.dgvAccessory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                        //  this.btConfirmOut.Enabled = true;
                      }
                  ));


             

                secont.Status = "获取数据完成";
                secont.Note1 = "---";
                secont.Statusname = "进度：";
                secont.Value = 100;
                secont.Now = "100";
                secont.All = "100";
                secont.Note2 = " /  ";
                secont.Maxvalue = 100;
                worker.ReportProgress(1 * 100 / 100, secont);
            }
            else
            {
                this.Invoke(
                   new Action(
                     delegate
                     {
                         this.items.Clear();
                 
              
                        // 自编单号
                        if (cbMyNo.Checked && txtMyNo.Text.Trim() != "")
                         {
                             parameter p = new parameter();
                             p.pkey = "my_no";
                            p.pvalue = txtMyNo.Text.Trim();
                            this.items.Add(p);
                        }
                        else
                        {
                            var whereRemove = items.FirstOrDefault(t => t.pkey == "my_no");
                            this.items.Remove(whereRemove);
                        }
                     }
                 ));

                if (items.Count <= 0)
                {
                    MessageBox.Show("没有资料，需要自编单号ERP资料库获取数据。");

                    secont.Status = "获取数据失败";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 0;
                    secont.Now = "0";
                    secont.All = "100";
                    secont.Note2 = " / ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(1 / 100 * 100, secont);
                    return;
                }
                else
                {
                    secont.Status = "正在从BEST获取色组数据...";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 10;
                    secont.Now = "10";
                    secont.All = "100";
                    secont.Note2 = " /  ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(10 / 100 * 100, secont);


                    // 测试服务器连接情况
                    if (testLinServer("192.168.0.254"))
                    {
                        secont.Status = "连接台北BEST服务器 192.168.0.254 成功！";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 50;
                        secont.Now = "50";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(50 / 100 * 100, secont);
                        if (testOpenDB("BESTconnStr"))
                        {
                            secont.Status = "连接台北 BEST 数据库成功！";
                            secont.Note1 = "---";
                            secont.Statusname = "进度：";
                            secont.Value = 100;
                            secont.Now = "100";
                            secont.All = "100";
                            secont.Note2 = " /  ";
                            secont.Maxvalue = 100;
                            worker.ReportProgress(100 / 100 * 100, secont);
                            isLinSer = true;
                            serviceName = "BESTconnStr";
                        }
                        else
                        {
                            secont.Status = "连接台北BEST服务器 192.168.0.254 成功,但是连接数据库失败！尝试连接本地服务器...";
                            secont.Note1 = "---";
                            secont.Statusname = "进度：";
                            secont.Value = 1;
                            secont.Now = "1";
                            secont.All = "100";
                            secont.Note2 = " /  ";
                            secont.Maxvalue = 100;
                            worker.ReportProgress(1 / 100 * 100, secont);
                        }
                    }

                    if (!isLinSer)
                    {
                        if (testLinServer("192.168.4.122"))
                        {
                            secont.Status = "连接本地BEST服务器 192.168.4.122 成功.";
                            secont.Note1 = "---";
                            secont.Statusname = "进度：";
                            secont.Value = 50;
                            secont.Now = "50";
                            secont.All = "100";
                            secont.Note2 = " /  ";
                            secont.Maxvalue = 100;
                            worker.ReportProgress(50 / 100 * 100, secont);

                            if (testOpenDB("BESTconnStr_KM"))
                            {
                                secont.Status = "连接本地BEST服务器 192.168.4.122 成功.连接数据库成功.正在获取数据";
                                secont.Note1 = "---";
                                secont.Statusname = "进度：";
                                secont.Value = 100;
                                secont.Now = "100";
                                secont.All = "100";
                                secont.Note2 = " /  ";
                                secont.Maxvalue = 100;
                                worker.ReportProgress(100 / 100 * 100, secont);
                                isLinSer = true;
                                serviceName = "BESTconnStr_KM";
                            }
                        }
                    }

                    if (!isLinSer)
                    {
                        secont.Status = "所有服务器都连接失败，请检查网络。";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 0;
                        secont.Now = "0";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(0 / 100 * 100, secont);
                        isLinSer = true;
                        serviceName = "BESTconnStr_KM";
                        MessageBox.Show("服务器都连接失败！请检查网络。");
                        return;
                    }
                    //从BEST 查询订单号
                    //查询色组
                    List<groupCloNames> groupCloNames = accoryOut.getColorFromBestDBByMyNo(txtMyNo.Text.Trim(), serviceName);
                    string od_no = "";
                    this.Invoke(
               new Action(
                       delegate
                       {
                           this.cbGroupColor.Items.Clear(); // 色组
                           this.cklistBoxPO.Items.Clear();  // PO
                           this.cklistSize.Items.Clear();   // 成衣SIZE      
                       }
                   ));

                    if (groupCloNames != null && groupCloNames.Count > 0)
                    {
                        List<string> groupColorNames = new List<string>();
                        for (int i = 0; i < groupCloNames.Count; i++)
                        {
                            string groupColorName = Convert.ToString(groupCloNames[i].clr_no); // 色组  
                            od_no = Convert.ToString(groupCloNames[i].od_no).Trim();
                            bool igName = Isgroups(groupColorNames, groupColorName);
                            if (!igName)
                            {
                                this.Invoke(
                                    new Action(
                                        delegate
                                        {
                                            this.cbGroupColor.Items.Add(groupColorName);
                                        }
                                        ));
                                groupColorNames.Add(groupColorName);
                            }
                        }

                        this.Invoke(
                            new Action(
                                delegate
                                {
                                    this.cbGroupColor.Items.Add("");
                                }
                                ));
                    }

                    if (od_no.Length <= 0)
                    {
                        MessageBox.Show("没有找到订单号");
                        secont.Status = "获取订单号失败";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 100;
                        secont.Now = "100";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(100 / 100 * 100, secont);
                        return;
                    }

                    //查询PO#
                    secont.Status = "正在从BEST获取PO数据...";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 20;
                    secont.Now = "20";
                    secont.All = "100";
                    secont.Note2 = " /  ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(20 / 100 * 100, secont);

                    List<groupPONumber> groupPONumber = accoryOut.getPOFromBestDBByOd_no(od_no, serviceName);
                    if (groupPONumber != null && groupPONumber.Count > 0)
                    {
                        List<string> groupPONumbers = new List<string>();
                        for (int i = 0; i < groupPONumber.Count; i++)
                        {
                            string PONumber = Convert.ToString(groupPONumber[i].po_no);
                            bool igName = Isgroups(groupPONumbers, PONumber);
                            if (!igName)
                            {
                                this.Invoke(
                                    new Action(
                                        delegate
                                        {
                                            this.cklistBoxPO.Items.Add(PONumber);
                                        }
                                        ));
                                groupPONumbers.Add(PONumber);
                            }
                        }
                    }

                    // 查询成衣Size
                    secont.Status = "正在从BEST获取尺码数据...";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 30;
                    secont.Now = "30";
                    secont.All = "100";
                    secont.Note2 = " /  ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(30 / 100 * 100, secont);

                    List<groupSizeNmae> groupSizeName = accoryOut.getSizeFromBestDBByOd_no(od_no, serviceName);
                    if (groupSizeName != null && groupSizeName.Count > 0)
                    {
                        List<string> groupSizeNames = new List<string>();
                        for (int i = 0; i < groupSizeName.Count; i++)
                        {
                            string SizeName1 = Convert.ToString(groupSizeName[i].SizeName01);
                            if (SizeName1.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName1);
                                if (!igName)
                                {
                                    this.Invoke(
                                        new Action(
                                            delegate
                                            {
                                                this.cklistSize.Items.Add(SizeName1);
                                            }
                                            ));
                                    groupSizeNames.Add(SizeName1);
                                }
                            }

                            string SizeName2 = Convert.ToString(groupSizeName[i].SizeName02);
                            if (SizeName2.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName2);
                                if (!igName)
                                {
                                    this.Invoke(
                                        new Action(
                                            delegate
                                            {
                                                this.cklistSize.Items.Add(SizeName2);
                                            }
                                            ));
                                    groupSizeNames.Add(SizeName2);
                                }
                            }
                            string SizeName3 = Convert.ToString(groupSizeName[i].SizeName03);
                            if (SizeName3.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName3);
                                if (!igName)
                                {
                                    this.Invoke(
                                        new Action(
                                            delegate
                                            {
                                                this.cklistSize.Items.Add(SizeName3);
                                            }
                                            ));
                                    groupSizeNames.Add(SizeName3);
                                }
                            }
                            string SizeName4 = Convert.ToString(groupSizeName[i].SizeName04);
                            if (SizeName4.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName4);
                                if (!igName)
                                {
                                    this.Invoke(
                                            new Action(
                                                    delegate
                                                    {
                                                        this.cklistSize.Items.Add(SizeName4);
                                                    }
                                                ));

                                    groupSizeNames.Add(SizeName4);
                                }
                            }
                            string SizeName5 = Convert.ToString(groupSizeName[i].SizeName05);
                            if (SizeName5.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName5);
                                if (!igName)
                                {
                                    this.Invoke(
                                         new Action(
                                                 delegate
                                                 {
                                                     this.cklistSize.Items.Add(SizeName5);
                                                 }
                                             ));

                                    groupSizeNames.Add(SizeName5);
                                }
                            }
                            string SizeName6 = Convert.ToString(groupSizeName[i].SizeName06);
                            if (SizeName6.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName6);
                                if (!igName)
                                {
                                    this.Invoke(
                                        new Action(
                                                delegate
                                                {
                                                    this.cklistSize.Items.Add(SizeName6);
                                                }
                                            ));

                                    groupSizeNames.Add(SizeName6);
                                }
                            }
                            string SizeName7 = Convert.ToString(groupSizeName[i].SizeName07);
                            if (SizeName7.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName7);
                                if (!igName)
                                {
                                    this.Invoke(
                                       new Action(
                                               delegate
                                               {
                                                   this.cklistSize.Items.Add(SizeName7);
                                               }
                                           ));

                                    groupSizeNames.Add(SizeName7);
                                }
                            }
                            string SizeName8 = Convert.ToString(groupSizeName[i].SizeName08);
                            if (SizeName8.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName8);
                                if (!igName)
                                {

                                    this.Invoke(
                                       new Action(
                                               delegate
                                               {
                                                   this.cklistSize.Items.Add(SizeName8);
                                               }
                                           ));

                                    groupSizeNames.Add(SizeName8);
                                }
                            }
                            string SizeName9 = Convert.ToString(groupSizeName[i].SizeName09);
                            if (SizeName9.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName9);
                                if (!igName)
                                {
                                    this.Invoke(
                                      new Action(
                                              delegate
                                              {
                                                  this.cklistSize.Items.Add(SizeName9);
                                              }
                                          ));

                                    groupSizeNames.Add(SizeName9);
                                }
                            }
                            string SizeName10 = Convert.ToString(groupSizeName[i].SizeName10);
                            if (SizeName10.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName10);
                                if (!igName)
                                {
                                    this.Invoke(
                                   new Action(
                                           delegate
                                           {
                                               this.cklistSize.Items.Add(SizeName10);
                                           }
                                       ));

                                    groupSizeNames.Add(SizeName10);
                                }
                            }
                            string SizeName11 = Convert.ToString(groupSizeName[i].SizeName11);
                            if (SizeName11.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName11);
                                if (!igName)
                                {
                                    this.Invoke(
                                 new Action(
                                         delegate
                                         {
                                             this.cklistSize.Items.Add(SizeName11);
                                         }
                                     ));

                                    groupSizeNames.Add(SizeName11);
                                }
                            }
                            string SizeName12 = Convert.ToString(groupSizeName[i].SizeName12);
                            if (SizeName12.Length > 0)
                            {
                                bool igName = Isgroups(groupSizeNames, SizeName12);
                                if (!igName)
                                {
                                    this.Invoke(
                                 new Action(
                                         delegate
                                         {
                                             this.cklistSize.Items.Add(SizeName12);
                                         }
                                     ));

                                    groupSizeNames.Add(SizeName12);
                                }
                            }
                        }
                    }

                    // 查询有SIZE的物料   查出SIZE
                    /*
                    
                    List<materialSize> MaterialSizeName = accoryOut.getMaterialSizeFromBestDBByOd_no(od_no);
                    if (MaterialSizeName != null && MaterialSizeName.Count > 0)
                    {
                        this.dgvAccessory.DataSource = MaterialSizeName;
                    }
                     */
                    // 查询所有物料  总数量  不分PO  分SIZE 颜色  materialID 
                    string myNumber = txtMyNo.Text.Trim();
                    if (myNumber.Length <= 0)
                    {
                        MessageBox.Show("请输入自编单号");
                        secont.Status = "没有自编单号...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 100;
                        secont.Now = "100";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(100 / 100 * 100, secont);
                        return;
                    }

                    secont.Status = "正在从BEST获取物料数据...";
                    secont.Note1 = "---";
                    secont.Statusname = "进度：";
                    secont.Value = 40;
                    secont.Now = "40";
                    secont.All = "100";
                    secont.Note2 = " /  ";
                    secont.Maxvalue = 100;
                    worker.ReportProgress(40 / 100 * 100, secont);

                    // 1、台北服务器 2、柬埔寨服务器  慢  备用
                    //  List<materials> groupMaterials = accoryOut.getMaterialsFromBestDBByOd_no(myNumber);
                    //   List<materials_KH> groupMaterials = accoryOut.getMaterialsFromKHBestDBByOd_no(od_no);



                    /*
                    if (groupMaterials != null && groupMaterials.Count > 0)
                    {
                        // 1、查询出ERP库的数据

                        secont.Status = "正在从ERP获取工单信息...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 50;
                        secont.Now = "50";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(50 / 100 * 100, secont);

                        List<accessoryOut> materiDBFromERP = accoryOut.getAccessoryOutByParameters(this.items, this.Org);
                        if (materiDBFromERP is null || materiDBFromERP.Count < 0)
                        {
                            MessageBox.Show("ERP 没有资料,可能工单还未开立");

                            secont.Status = "获取工单信息失败.未找到工单号";
                            secont.Note1 = "---";
                            secont.Statusname = "进度：";
                            secont.Value = 100;
                            secont.Now = "100";
                            secont.All = "100";
                            secont.Note2 = " /  ";
                            secont.Maxvalue = 100;
                            worker.ReportProgress( 100 / 100 * 100, secont);

                            return;
                        }

                        secont.Status = "获取数据完成，正在合并计算...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 60;
                        secont.Now = "60";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(60 / 100 * 100, secont);

                        int? qtys = materiDBFromERP[0].products;
                        this.Invoke(
                               new Action(
                                       delegate
                                       {
                                           this.labQtys.Text = "订单数量：" + qtys;
                                       }
                                   ));
                       
                       
                        string ERPmas_id = "";
                        for (int i = 0; i < groupMaterials.Count; i++)
                        {
                            for (int j = 0; j < materiDBFromERP.Count; j++)
                            {
                                ERPmas_id = materiDBFromERP[j].materialID;
                                if (ERPmas_id.Substring(0, 1) == "-")
                                {
                                    ERPmas_id = ERPmas_id.Substring(1, ERPmas_id.Length - 1);
                                }
                                if (groupMaterials[i].mas_id == ERPmas_id)
                                {
                                  //  groupMaterials[i].mas_sortNumber = materiDBFromERP[j].sortNumber;
                                  //  groupMaterials[i].mas_sortName = materiDBFromERP[j].sortName;
                                }

                                secont.Status = "获取数据完成，正在合并计算...";
                                secont.Note1 = "---";
                                secont.Statusname = "进度：";
                                secont.Value = i;
                                secont.Now = i.ToString();
                                secont.All = groupMaterials.Count.ToString();
                                secont.Note2 = " /  ";
                                secont.Maxvalue = groupMaterials.Count;
                                worker.ReportProgress( i / groupMaterials.Count * 100, secont);
                            }                          

                        }
                         

                        secont.Status = "获取数据完成，正在合并计算...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = secont.Maxvalue - 10;
                        secont.Now = "80";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(80 / 100 * 100, secont);
                        
                        groupMaterials.Sort(
                           delegate (materials n1, materials n2)
                           {
                               return n1.mas_sortNumber.CompareTo(n2.mas_sortNumber);
                           }
                         );
                        

                        this.Invoke(
                              new Action(
                                      delegate
                                      {
                                          nulldv();
                                          DisplayCol(dgvAccessory, "be_id", "生产厂区");
                                          DisplayCol(dgvAccessory, "my_no", "自编单号");
                                          DisplayCol(dgvAccessory, "od_no", "BEST订单号码");
                                          DisplayCol(dgvAccessory, "style_id", "款号");
                                          DisplayCol(dgvAccessory, "clr_no", "成衣色组号");
                                          DisplayCol(dgvAccessory, "mas_sortNumber", "物料分群码");
                                          DisplayCol(dgvAccessory, "mas_sortName", "物料分群名称");
                                          DisplayCol(dgvAccessory, "mas_id", "物料ID");
                                          DisplayCol(dgvAccessory, "mas_name", "物料名称");
                                          DisplayCol(dgvAccessory, "color_no", "物料颜色");
                                          DisplayCol(dgvAccessory, "color_name2", "颜色中文名称");

                                          DisplayCol(dgvAccessory, "size", "物料尺码");
                                          DisplayCol(dgvAccessory, "unit_qty", "单位用量");
                                          DisplayCol(dgvAccessory, "pu_qty", "采购数量");
                                          DisplayCol(dgvAccessory, "style_mas_qty", "款式总用量");
                                          DisplayCol(dgvAccessory, "unit_id", "物料单位");
                                          DisplayCol(dgvAccessory, "trans_rate", "单位转换比率");
                                          DisplayCol(dgvAccessory, "mas_qty", "应发料量");
                                          DisplayCol(dgvAccessory, "unit_id_p", "应发料单位");

                                          DisplayCol(dgvAccessory, "pu_no", "采购单号");
                                          DisplayCol(dgvAccessory, "vend_id", "供应商编码");
                                          DisplayCol(dgvAccessory, "vend_abbr", "供应商简称");
                                          DisplayCol(dgvAccessory, "per_id", "审核人员");
                                      }
                                  ));

                        

                        secont.Status = "计算数据完成";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 100;
                        secont.Now = "100";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(100 / 100 * 100, secont);

                        this.Invoke(
                               new Action(
                                       delegate
                                       {
                                          // nulldv();
                                           this.dgvAccessory.DataSource = groupMaterials;
                                           Cursor = Cursors.Default;
                                           this.dgvAccessory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

                                       }
                                   ));
                       
                    }
                    */

                    // 柬埔寨服务器  慢  备用
                    // 查询自编单号下的所有物料
                    List<materials> groupMaterials = accoryOut.getMaterialsFromBestDBByOd_no(od_no, serviceName);
                    if (groupMaterials != null && groupMaterials.Count > 0)
                    {

                        secont.Status = "正在从BEST获取配色表...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 55;
                        secont.Now = "50";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(55 / 100 * 100, secont);

                        // 获取配色表  色组名称
                        //od_no 订购单号 , serviceName 服务器, type 0 色组名称  1 配色物料 , 0 物料的text*
                        List<GroupColor> groupColorsName = accoryOut.getGroupColorFromBestDBByOd_no(od_no, serviceName, 0, 0);
                        if (groupColorsName is null || groupColorsName.Count < 0)
                        {
                            MessageBox.Show("没有找到配色表,请找开发人员查证（小明）");
                            secont.Status = "获取配色表失败";
                            secont.Note1 = "---";
                            secont.Statusname = "进度：";
                            secont.Value = 100;
                            secont.Now = "100";
                            secont.All = "100";
                            secont.Note2 = " /  ";
                            secont.Maxvalue = 100;
                            worker.ReportProgress(100 / 100 * 100, secont);
                            return;
                        }

                        secont.Status = "获取配色表完成，正在合并计算...";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 60;
                        secont.Now = "60";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(60 / 100 * 100, secont);

                        List<string> groupColorsNames = new List<string>();
                        if (groupColorsName[0].text2 != null && groupColorsName[0].text2.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text2);
                        }
                        if (groupColorsName[0].text3 != null && groupColorsName[0].text3.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text3);
                        }
                        if (groupColorsName[0].text4 != null && groupColorsName[0].text4.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text4);
                        }
                        if (groupColorsName[0].text5 != null && groupColorsName[0].text5.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text5);
                        }
                        if (groupColorsName[0].text6 != null && groupColorsName[0].text6.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text6);
                        }
                        if (groupColorsName[0].text7 != null && groupColorsName[0].text7.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text7);
                        }
                        if (groupColorsName[0].text8 != null && groupColorsName[0].text8.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text8);
                        }
                        if (groupColorsName[0].text9 != null && groupColorsName[0].text9.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text9);
                        }
                        if (groupColorsName[0].text10 != null && groupColorsName[0].text10.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text10);
                        }
                        if (groupColorsName[0].text11 != null && groupColorsName[0].text11.Length > 0)
                        {
                            groupColorsNames.Add(groupColorsName[0].text11);
                        }

                        // 新建一个数组存放色组的物料 所有物料
                        List<materials> groupColorsMaterialAll = new List<materials>();
                        for (int i = 0; i < groupColorsNames.Count; i++)
                        {
                            // 获取配色表  色组物料
                            List<GroupColor> groupColorsMaterials = accoryOut.getGroupColorFromBestDBByOd_no(od_no, serviceName, 1, (i + 2)); // i+2为TEXT? 的名字
                            if (groupColorsMaterials is null || groupColorsMaterials.Count < 0)
                            {
                                MessageBox.Show("没有找到配色表,请找开发人员查证（小明）");
                                secont.Status = "获取配色表失败";
                                secont.Note1 = "---";
                                secont.Statusname = "进度：";
                                secont.Value = 100;
                                secont.Now = "100";
                                secont.All = "100";
                                secont.Note2 = " /  ";
                                secont.Maxvalue = 100;
                                worker.ReportProgress(100 / 100 * 100, secont);
                                return;
                            }
                            else
                            {
                                for (int j = 0; j < groupColorsMaterials.Count; j++)
                                {
                                    string colorName = "";

                                    switch (i)
                                    {
                                        case 0:
                                            colorName = groupColorsMaterials[j].text2;
                                            break;
                                        case 1:
                                            colorName = groupColorsMaterials[j].text3;
                                            break;
                                        case 2:
                                            colorName = groupColorsMaterials[j].text4;
                                            break;
                                        case 3:
                                            colorName = groupColorsMaterials[j].text5;
                                            break;
                                        case 4:
                                            colorName = groupColorsMaterials[j].text6;
                                            break;
                                        case 5:
                                            colorName = groupColorsMaterials[j].text7;
                                            break;
                                        case 6:
                                            colorName = groupColorsMaterials[j].text8;
                                            break;
                                        case 7:
                                            colorName = groupColorsMaterials[j].text9;
                                            break;
                                        case 8:
                                            colorName = groupColorsMaterials[j].text10;
                                            break;
                                        case 9:
                                            colorName = groupColorsMaterials[j].text11;
                                            break;
                                        default:
                                            colorName = "";
                                            break;
                                    }

                                    // 如果这个 色组的物料ID与颜色 跟所有物料集合里的ID 颜色 对得上 添加上去
                                    // 如果 所有物料集合里的某个物料跟所有色组里的物料都对不上，添加上去， 注明  改
                                    for (int k = 0; k < groupMaterials.Count; k++)
                                    {
                                        // 添加色组的物料信息
                                        if (groupColorsMaterials[j].mas_id == groupMaterials[k].mas_id && colorName.Contains(groupMaterials[k].color_no))
                                        {
                                            materials gc = new materials();
                                            gc.mas_id = groupColorsMaterials[j].mas_id;
                                            gc.mas_name = groupColorsMaterials[j].mas_name;
                                            gc.style_id = groupColorsMaterials[j].style_id;

                                            gc.color_no = groupMaterials[k].color_no;
                                            gc.color_name2 = groupMaterials[k].color_name2;
                                            gc.unit_id = groupMaterials[k].unit_id;
                                            gc.unit_id_p = groupMaterials[k].unit_id_p;
                                            gc.unit_qty = groupMaterials[k].unit_qty;
                                            gc.trans_rate = groupMaterials[k].trans_rate;
                                            gc.size = groupMaterials[k].size;
                                            gc.od_qty = groupMaterials[k].od_qty;
                                            

                                            gc.clr_no = groupColorsNames[i];
                                            gc.be_id = groupCloNames[0].w_id;
                                            gc.my_no = groupCloNames[0].my_no;
                                            gc.od_no = groupCloNames[0].od_no;
                                            gc.in_date = groupCloNames[0].in_date;
                                            gc.season_id = groupCloNames[0].season_id;
                                            gc.release_who = groupCloNames[0].release_who;
                                            gc.style_name = groupCloNames[0].style_name;
                                            gc.sex_name = groupCloNames[0].sex_name;
                                            gc.sample_no = groupCloNames[0].sample_no;

                                            groupColorsMaterialAll.Add(gc);
                                        }
                                    }
                                }
                            }
                        }

                        // 如果色组不同  但是物料的颜色相同  数量是加在一起的
                        // 添加物料的采购单 供应商 等信息  分群 单位用量

                        List<odb_pur> purs = accoryOut.getODPuNoFromBestDBByPu_no(od_no, serviceName);
                        if (purs.Count <= 0)
                        {
                            MessageBox.Show("没有采购单信息");
                        }
                        else
                        {
                            //  把相同物料颜色的采购单号 合并成一行数据
                            List<odb_pur> purs_masid = purs;
                            List<odb_pur> purs_puno = purs;
                            List<punos> maspunos = new List<punos>(); // 存放采购单号

                            for (int x = 0; x < purs_masid.Count; x++)
                            {
                                List<string> pus = new List<string>(); // 存放采购单号
                                for (int y = 0; y < purs_puno.Count; y++)
                                {

                                    if (purs_masid[x].mas_id == purs_puno[y].mas_id && purs_masid[x].color_no == purs_puno[y].color_no) //&& groupColorsMaterialAll[i].color_no == maspunos[j].color_no
                                    {
                                        if (purs_puno[y].pu_no != null && purs_puno[y].pu_no != "")
                                        {
                                            // 列表里是否已有这个值 
                                            if (this.exists(pus, purs_puno[y].pu_no))
                                            {

                                            }
                                            else
                                            {
                                                pus.Add(purs_puno[y].pu_no);
                                            }

                                        }
                                    }
                                }
                                punos puno = new punos();
                                string pustr = "";
                                puno.mas_id = purs_masid[x].mas_id;
                                puno.color_no = purs_masid[x].color_no;
                                if (pus.Count > 0)
                                {
                                    for (int z = 0; z < pus.Count; z++)
                                    {
                                        pustr = pustr + "," + pus[z];
                                    }
                                    pus.Clear();
                                }
                                puno.pu_no = pustr;
                                maspunos.Add(puno);
                            }

                            // 采购单号回填至物料表
                            for (int i = 0; i < groupColorsMaterialAll.Count; i++)
                            {
                                for (int j = 0; j < maspunos.Count; j++)
                                {
                                    if (groupColorsMaterialAll[i].mas_id == maspunos[j].mas_id && groupColorsMaterialAll[i].color_no == maspunos[j].color_no )
                                    {
                                        groupColorsMaterialAll[i].pu_no = maspunos[j].pu_no;
                                    }
                                }
                            }                          

                            // 供应商回填至物料表
                            for (int i = 0; i < groupColorsMaterialAll.Count; i++)
                            {
                                for (int j = 0; j < purs.Count; j++)
                                {
                                    if (groupColorsMaterialAll[i].mas_id == purs[j].mas_id && 
                                        groupColorsMaterialAll[i].color_no == purs[j].color_no && 
                                        groupColorsMaterialAll[i].size == purs[j].size && 
                                        groupColorsMaterialAll[i].unit_qty == purs[j].unit_qty && 
                                        groupColorsMaterialAll[i].od_qty == purs[j].od_qty)
                                    {
                                        groupColorsMaterialAll[i].vend_id = purs[j].vend_id;
                                        groupColorsMaterialAll[i].vend_abbr = purs[j].vend_abbr;
                                        groupColorsMaterialAll[i].item_id = purs[j].item_id;
                                        groupColorsMaterialAll[i].item_name = purs[j].item_name;

                                        groupColorsMaterialAll[i].loss  = purs[j].loss;  // 预补数
                                        groupColorsMaterialAll[i].unit_qty = purs[j].unit_qty;
                                        groupColorsMaterialAll[i].pu_qty = purs[j].qty;
                                        groupColorsMaterialAll[i].trans_rate = purs[j].trans_rate;
                                        groupColorsMaterialAll[i].style_mas_qty = purs[j].od_qty; 

                                        //---------------------------------------------
                                        groupColorsMaterialAll[i].od_qty = 0; //总订单数量
                                      //  groupColorsMaterialAll[i].od_Finished_qty = 0; //已生产数量 本地库拉出累计生产数量
                                      //  groupColorsMaterialAll[i].od_Unfinished_qty = 0; //剩余数量  总订单数量 - 已生产数                                        
                                       // groupColorsMaterialAll[i].od_make_qty = Convert.ToInt32(this.txtManufactures.Text.Trim()); //本批生产数量 
                                       // groupColorsMaterialAll[i].od_make_qty = 0; //本批生产数量 

                                      //  groupColorsMaterialAll[i].od_Exceed_qty = groupColorsMaterialAll[i].od_make_qty - (groupColorsMaterialAll[i].od_Unfinished_qty + groupColorsMaterialAll[i].od_make_qty); //溢出数量

                                        groupColorsMaterialAll[i].pu_trans_qty = 0; //发料单位采购数量
                                        groupColorsMaterialAll[i].masFinished_qty = 0; //累计已发数量
                                        groupColorsMaterialAll[i].masUnfinished_qty = 0; //剩余数量
                                        groupColorsMaterialAll[i].mas_qty = 0; //本次应发料量
                                        groupColorsMaterialAll[i].masExceed_qty = 0; //超发料量

                                        // private string hname = Dns.GetHostName(); //得到本机的主机名
                                        groupColorsMaterialAll[i].createPerson = Dns.GetHostName(); //创建单据人员
                                        groupColorsMaterialAll[i].createDate = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"); //创建单据日期时间
                                        groupColorsMaterialAll[i].changePerson = Dns.GetHostName(); ; //单据最后修改人员
                                        groupColorsMaterialAll[i].changeDate = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");//单据最后修改日期时间
                                        groupColorsMaterialAll[i].receiveNumber = "DA20360001"; // 领料单号  日期月+料类别+6码流水   
                                        //      DA         2036             0001
                                        // 辅料发料    2020年第36周     流水号4位

                                        groupColorsMaterialAll[i].receiveNumberBatch = "01"; //发料批号  2码流水  
                                        groupColorsMaterialAll[i].materialStatus = "N"; //物料发放状态  O完成  N未发料  P发了一部分  进行中 
                                        groupColorsMaterialAll[i].printTimes = 0; // 打印次数     
                                        groupColorsMaterialAll[i].note = ""; // 备注          
                                    }
                                }
                            }


                            // 计算应发料量
                            for (int i = 0; i < groupColorsMaterialAll.Count; i++)
                            {
                                switch (groupColorsMaterialAll[i].unit_id_p)
                                {
                                    case "PCS": 
                                        groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(groupColorsMaterialAll[i].pu_qty) / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate)));
                                        break;
                                    case "PC":
                                        groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(groupColorsMaterialAll[i].pu_qty) / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate)));
                                        break;
                                    case "PR":
                                        groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(Convert.ToDouble
                                            (groupColorsMaterialAll[i].pu_qty) / Convert.ToDouble(
                                                groupColorsMaterialAll[i].trans_rate)));
                                        break;
                                    case "M":
                                        groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(groupColorsMaterialAll[i].pu_qty) / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate)));
                                        break;
                                    case "CONE":
                                        groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(groupColorsMaterialAll[i].pu_qty) / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate)));
                                        break;
                                    default:
                                        groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Round(Convert.ToDouble(groupColorsMaterialAll[i].pu_qty) / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate), 4, MidpointRounding.AwayFromZero));
                                        break;
                                }
                                //

                            }
                        }
                        //
                        // 对比出所有采购单号 
                        secont.Status = "计算数据完成";
                        secont.Note1 = "---";
                        secont.Statusname = "进度：";
                        secont.Value = 95;
                        secont.Now = "95";
                        secont.All = "100";
                        secont.Note2 = " /  ";
                        secont.Maxvalue = 100;
                        worker.ReportProgress(95 / 100 * 100, secont);

                       

                        // 获取总数量 PO数量  SIZE数量      
                        this.allqtys.Clear();
                        this.allqtys = accoryOut.getAllQtysFromBestDBByPu_no(od_no, serviceName);
                        if (allqtys.Count <= 0)
                        {
                            MessageBox.Show("没有数量信息");
                            secont.Status = "没有数量信息";
                            secont.Note1 = "---";
                            secont.Statusname = "进度：";
                            secont.Value = 100;
                            secont.Now = "100";
                            secont.All = "100";
                            secont.Note2 = " /  ";
                            secont.Maxvalue = 100;
                            worker.ReportProgress(100 / 100 * 100, secont);
                        }
                        int? qty = 0;
                        for (int i = 0; i < allqtys.Count; i++)
                        {
                            qty = qty + allqtys[i].qty;
                        }
                        this.Invoke(
                              new Action(
                                      delegate
                                      {
                                          this.labPuQty.Text = qty.ToString();
                                           
                                              for (int i = 0; i < this.cklistBoxPO.Items.Count; i++)
                                              {
                                                  this.cklistBoxPO.SetItemChecked(i, true);
                                              }

                                          for (int i = 0; i < this.cklistSize.Items.Count; i++)
                                          {
                                              this.cklistSize.SetItemChecked(i, true);
                                          }

                                          this.txtManufactures.Text = qty.ToString();

                                         
                                      }));
                        // 获取 色组 的 SIZE 的成衣件数 
                        if(this.allqtys.Count <= 0 || groupColorsMaterialAll.Count<=0)
                        {
                            MessageBox.Show("没有订单数量或物料数量");
                            return;
                        }
                        for(int i = 0; i < this.allqtys.Count; i++)
                        {
                            for(int j = 0; j < groupColorsMaterialAll.Count; j++)
                            {
                             //   if(gro)
                            }
                        }
                        

                        this.runMake(groupColorsMaterialAll, od_no,qty, qty);
                        /*
                        //获取本地数据
                        List<materials> localhostMaterialAll = new List<materials>();
                        localhostMaterialAll = accoryOut.getAccessoryOutByOd_no(od_no);
                        if (localhostMaterialAll == null || localhostMaterialAll.Count <= 0)
                        {
                            for(int i=0;i< groupColorsMaterialAll.Count; i++)
                            { 
                                groupColorsMaterialAll[i].od_qty = qty; //总订单数量
                                groupColorsMaterialAll[i].od_Finished_qty = 0; //已生产数量  
                                groupColorsMaterialAll[i].od_Unfinished_qty = groupColorsMaterialAll[i].od_qty - groupColorsMaterialAll[i].od_Finished_qty; //剩余数量  总订单数量 - 已生产数                                        
                                
                                groupColorsMaterialAll[i].od_make_qty = qty; 

                                groupColorsMaterialAll[i].od_Exceed_qty = groupColorsMaterialAll[i].od_make_qty - (groupColorsMaterialAll[i].od_Finished_qty + groupColorsMaterialAll[i].od_make_qty); //溢出数量

                                groupColorsMaterialAll[i].pu_trans_qty = 0; //发料单位采购数量
                                groupColorsMaterialAll[i].masFinished_qty = 0; //累计已发数量
                                groupColorsMaterialAll[i].masUnfinished_qty = 0; //剩余数量
                                groupColorsMaterialAll[i].mas_qty = 0; //本次应发料量
                                groupColorsMaterialAll[i].masExceed_qty = 0; //超发料量

                                // private string hname = Dns.GetHostName(); //得到本机的主机名
                                groupColorsMaterialAll[i].createPerson = Dns.GetHostName(); //创建单据人员
                                groupColorsMaterialAll[i].createDate = DateTime.Now.ToString("G"); //创建单据日期时间
                                groupColorsMaterialAll[i].changePerson = Dns.GetHostName(); ; //单据最后修改人员
                                groupColorsMaterialAll[i].changeDate = DateTime.Now.ToString("G");//单据最后修改日期时间
                                groupColorsMaterialAll[i].receiveNumber = "DA20360001"; // 领料单号  日期月+料类别+6码流水   
                                                                                        //      DA         2036             0001
                                                                                        // 辅料发料    2020年第36周     流水号4位

                                groupColorsMaterialAll[i].receiveNumberBatch = "01"; //发料批号  2码流水  
                                groupColorsMaterialAll[i].materialStatus = "N"; //物料发放状态  O完成  N未发料  P发了一部分  进行中 
                                groupColorsMaterialAll[i].printTimes = 0; // 打印次数     
                                groupColorsMaterialAll[i].note = ""; // 备注     
                            }                            
                        }

                        this.Invoke(
                              new Action(
                                      delegate
                                      {
                                          DataTable dt = this.accoryOut.ToDataTable(groupColorsMaterialAll);
                                          dt.Columns.Add("select", typeof(bool)).SetOrdinal(0);
                                          foreach (DataRow row in dt.Rows)
                                          {
                                              row["select"] = true;
                                          }
                                          this.dgvAccessory.DataSource = dt;
                                          accessoryDT = null;
                                          accessoryDT = dt;
                                          nulldv();
                                          this.dgvAccessory.DataSource = null;
                                          this.dgvAccessory.DataSource = accessoryDT;
                                          foreach (DataGridViewColumn dgvc in dgvAccessory.Columns)
                                          {
                                              if (dgvc.DataPropertyName != "select")
                                              {
                                                  dgvc.ReadOnly = true;
                                              }
                                              else
                                              {
                                                  dgvc.ReadOnly = false;
                                              }
                                          }
                                          changHeaderText();
                                          Cursor = Cursors.Default;
                                          this.dgvAccessory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                                      }));
                        */
                        /*
                         *  this.Invoke(
                             new Action(
                                     delegate
                                     {
                                        
                                     }));

                       // 最后的数据统计
                                       groupColorsMaterialAll[i].vend_id = purs[j].vend_id;
                                       groupColorsMaterialAll[i].vend_abbr = purs[j].vend_abbr;
                                       groupColorsMaterialAll[i].item_id = purs[j].item_id;
                                       groupColorsMaterialAll[i].item_name = purs[j].item_name;

                                       groupColorsMaterialAll[i].loss  = purs[j].loss;  // 预补数
                                       groupColorsMaterialAll[i].unit_qty = purs[j].unit_qty;
                                       groupColorsMaterialAll[i].pu_qty = purs[j].qty;
                                       groupColorsMaterialAll[i].trans_rate = purs[j].trans_rate;
                                       groupColorsMaterialAll[i].style_mas_qty = purs[j].od_qty; 

                                       //---------------------------------------------
                                       groupColorsMaterialAll[i].od_qty = 0; //总订单数量
                                       groupColorsMaterialAll[i].od_Finished_qty = 0; //已生产数量 本地库拉出累计生产数量
                                       groupColorsMaterialAll[i].od_Unfinished_qty = 0; //剩余数量  总订单数量 - 已生产数                                        
                                      // groupColorsMaterialAll[i].od_make_qty = Convert.ToInt32(this.txtManufactures.Text.Trim()); //本批生产数量 
                                       groupColorsMaterialAll[i].od_make_qty = 0; //本批生产数量 

                                       groupColorsMaterialAll[i].od_Exceed_qty = groupColorsMaterialAll[i].od_make_qty - (groupColorsMaterialAll[i].od_Unfinished_qty + groupColorsMaterialAll[i].od_make_qty); //溢出数量

                                       groupColorsMaterialAll[i].pu_trans_qty = 0; //发料单位采购数量
                                       groupColorsMaterialAll[i].masFinished_qty = 0; //累计已发数量
                                       groupColorsMaterialAll[i].masUnfinished_qty = 0; //剩余数量
                                       groupColorsMaterialAll[i].mas_qty = 0; //本次应发料量
                                       groupColorsMaterialAll[i].masExceed_qty = 0; //超发料量

                                       // private string hname = Dns.GetHostName(); //得到本机的主机名
                                       groupColorsMaterialAll[i].createPerson = Dns.GetHostName(); //创建单据人员
                                       groupColorsMaterialAll[i].createDate = DateTime.Now.ToString("G"); //创建单据日期时间
                                       groupColorsMaterialAll[i].changePerson = Dns.GetHostName(); ; //单据最后修改人员
                                       groupColorsMaterialAll[i].changeDate = DateTime.Now.ToString("G");//单据最后修改日期时间
                                       groupColorsMaterialAll[i].receiveNumber = "DA20360001"; // 领料单号  日期月+料类别+6码流水   
                                       //      DA         2036             0001
                                       // 辅料发料    2020年第36周     流水号4位

                                       groupColorsMaterialAll[i].receiveNumberBatch = "01"; //发料批号  2码流水  
                                       groupColorsMaterialAll[i].materialStatus = "N"; //物料发放状态  O完成  N未发料  P发了一部分  进行中 
                                       groupColorsMaterialAll[i].printTimes = 0; // 打印次数     
                                       groupColorsMaterialAll[i].note = ""; // 备注     

                       */
                    }
                }
            }
        }
        public bool exists(List<string> ls, string str)
        {
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i] == str)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        
       private void runMake(List<materials> groupColorsMaterialAll,string od_no,int? qty, int? makeQty)
        {
            if (qty == null) qty = 0;
            
            double? masUnfinishedQty = 0.0; //溢出料量
            double? masExceedQty = 0.0;  //超发料量

            //获取本地数据
            List<materials> localhostMaterialAll = new List<materials>();
            string date = "";
            string receive = "";
            string receiveNumber = "";
            localhostMaterialAll = accoryOut.getAccessoryOutByOd_no(od_no);
            
            if (localhostMaterialAll == null || localhostMaterialAll.Count <= 0)
            {
                for (int i = 0; i < groupColorsMaterialAll.Count; i++)
                {
                    groupColorsMaterialAll[i].od_qty = qty; //总订单数量 

                    //采购数量(换算)  采购数量  /  单位转换比率
                    groupColorsMaterialAll[i].pu_trans_qty = Convert.ToDouble(Math.Round(
                              Convert.ToDouble(groupColorsMaterialAll[i].pu_qty)
                              / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate), 4, MidpointRounding.AwayFromZero));                 
                    groupColorsMaterialAll[i].masFinished_qty = 0; //累计已发数量

                    // 计算应发料量
                    // groupColorsMaterialAll[i].mas_qty = groupColorsMaterialAll[i].unit_qty * groupColorsMaterialAll[i].style_mas_qty * (groupColorsMaterialAll[i].loss / 100 + 1) / groupColorsMaterialAll[i].trans_rate; //本次应发料量
                    int? nowmake = makeQty;
                    if (nowmake > groupColorsMaterialAll[i].style_mas_qty)
                    {
                        nowmake = Convert.ToInt32(groupColorsMaterialAll[i].style_mas_qty);
                    }
                    
                    switch (groupColorsMaterialAll[i].unit_id_p)
                    {
                        case "PCS":
                            groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(
                                Convert.ToDouble(groupColorsMaterialAll[i].unit_qty)
                              //  * Convert.ToDouble(groupColorsMaterialAll[i].style_mas_qty)
                                * Convert.ToDouble(nowmake)
                                * (Convert.ToDouble(groupColorsMaterialAll[i].loss) / 100 + 1)
                                / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate)));
                            break;
                        case "PC":
                            groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(
                                 Convert.ToDouble(groupColorsMaterialAll[i].unit_qty)
                                //  * Convert.ToDouble(groupColorsMaterialAll[i].style_mas_qty)
                                * Convert.ToDouble(nowmake)
                                 * (Convert.ToDouble(groupColorsMaterialAll[i].loss) / 100 + 1)
                                 / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate)));
                            break;
                        case "PR":
                            groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(
                                Convert.ToDouble(groupColorsMaterialAll[i].unit_qty)
                                //  * Convert.ToDouble(groupColorsMaterialAll[i].style_mas_qty)
                                * Convert.ToDouble(nowmake)
                                * (Convert.ToDouble(groupColorsMaterialAll[i].loss) / 100 + 1)
                                / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate)));
                            break;
                        case "M":
                            groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(
                                Convert.ToDouble(groupColorsMaterialAll[i].unit_qty)
                                //  * Convert.ToDouble(groupColorsMaterialAll[i].style_mas_qty)
                                * Convert.ToDouble(nowmake)
                                * (Convert.ToDouble(groupColorsMaterialAll[i].loss) / 100 + 1)
                                / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate)));
                            break;
                        case "CONE":
                            groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Ceiling(
                                Convert.ToDouble(groupColorsMaterialAll[i].unit_qty)
                                //  * Convert.ToDouble(groupColorsMaterialAll[i].style_mas_qty)
                                * Convert.ToDouble(nowmake)
                                * (Convert.ToDouble(groupColorsMaterialAll[i].loss) / 100 + 1)
                                / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate)));
                            break;
                        default:
                            groupColorsMaterialAll[i].mas_qty = Convert.ToDouble(Math.Round(Convert.ToDouble(
                                groupColorsMaterialAll[i].unit_qty)
                                //  * Convert.ToDouble(groupColorsMaterialAll[i].style_mas_qty)
                                * Convert.ToDouble(nowmake)
                                 * (Convert.ToDouble(groupColorsMaterialAll[i].loss) / 100 + 1)
                                 / Convert.ToDouble(groupColorsMaterialAll[i].trans_rate), 4, MidpointRounding.AwayFromZero));
                            break;
                    }
                    masUnfinishedQty = Convert.ToDouble(Math.Round(Convert.ToDouble(
                               groupColorsMaterialAll[i].pu_trans_qty)
                                -( Convert.ToDouble(groupColorsMaterialAll[i].masFinished_qty)
                                + Convert.ToDouble(groupColorsMaterialAll[i].mas_qty) )
                                , 4, MidpointRounding.AwayFromZero));
                  //  masUnfinishedQty = groupColorsMaterialAll[i].pu_trans_qty - (groupColorsMaterialAll[i].masFinished_qty + groupColorsMaterialAll[i].mas_qty); //剩余料量

                    if (masUnfinishedQty > 0)
                    {
                        groupColorsMaterialAll[i].masUnfinished_qty = masUnfinishedQty; //剩余料量
                    }
                    else
                    {
                        groupColorsMaterialAll[i].masUnfinished_qty = 0; //剩余料量
                    }

                    masExceedQty = Convert.ToDouble(Math.Round((Convert.ToDouble(
                              groupColorsMaterialAll[i].masFinished_qty)
                               + Convert.ToDouble(groupColorsMaterialAll[i].mas_qty))
                               - Convert.ToDouble(groupColorsMaterialAll[i].pu_trans_qty),4, MidpointRounding.AwayFromZero));

                  //  masExceedQty = (groupColorsMaterialAll[i].masFinished_qty + groupColorsMaterialAll[i].mas_qty) - groupColorsMaterialAll[i].pu_trans_qty; //超发料量

                    if (masExceedQty > 0)
                    {
                        
                        groupColorsMaterialAll[i].masExceed_qty = masExceedQty; //超发料量
                    }
                    else
                    {
                        groupColorsMaterialAll[i].masExceed_qty = 0; //超发料量
                        
                    }

                

                    // private string hname = Dns.GetHostName(); //得到本机的主机名
                    groupColorsMaterialAll[i].createPerson = Dns.GetHostName(); //创建单据人员
                    groupColorsMaterialAll[i].createDate = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"); //创建单据日期时间
                    groupColorsMaterialAll[i].changePerson = Dns.GetHostName(); ; //单据最后修改人员
                    groupColorsMaterialAll[i].changeDate = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");//单据最后修改日期时间
                    //
                    date = DateTime.Now.ToString("yyMM");//当前日期   
                    receive = "DA" + date;// 发料日期
                    int receives =  accoryOut.getAccessoryOutByReceiveNumber(receive);//最大流水号
                    if(receives <=0 )
                    {
                        receiveNumber = "000001";
                    }
                    else
                    {
                        receives = receives + 1;
                        receiveNumber = receives.ToString();
                    }
                   

                    while (receiveNumber.ToString().Length<6)
                    {
                        receiveNumber = "0"+receiveNumber;
                    }
                     

                    groupColorsMaterialAll[i].receiveNumber = receive+ receiveNumber; // 领料单号  日期月+料类别+6码流水   
                                                                            //      DA         2036             0001
                                                                            // 辅料发料    2020年第36周     流水号4位

                    groupColorsMaterialAll[i].receiveNumberBatch = "01"; //发料批号  2码流水  
                    groupColorsMaterialAll[i].materialStatus = "N"; //物料发放状态  O完成  N未发料  P发了一部分  进行中 
                    groupColorsMaterialAll[i].printTimes = 0; // 打印次数     
                    groupColorsMaterialAll[i].note = ""; // 备注     


                }

                // 这里进行过滤重复的值
              //  List<materials> lister = groupColorsMaterialAll; 
                List<materials> filterMaterial = new List<materials>(); // 过滤后重复数据后的物料
                List<string> clrNos = new List<string>();//色组
                bool isExists = false;
                bool clrNoExists = false;
                for (int i = 0; i < groupColorsMaterialAll.Count; i++)
                {
                   if(filterMaterial.Count <= 0)
                    {
                        filterMaterial.Add(groupColorsMaterialAll[i]);
                    }
                    else
                    {
                        for (int j = 0; j < filterMaterial.Count; j++)
                        {
                            if (groupColorsMaterialAll[i].mas_id == filterMaterial[j].mas_id 
                                && groupColorsMaterialAll[i].color_no == filterMaterial[j].color_no 
                                && groupColorsMaterialAll[i].size == filterMaterial[j].size 
                                && groupColorsMaterialAll[i].style_mas_qty == filterMaterial[j].style_mas_qty) 
                            {
                                if (clrNos.Count <= 0)
                                {
                                    clrNos.Add(groupColorsMaterialAll[i].clr_no);
                                }
                                else
                                {
                                    for(int k = 0; k < clrNos.Count; k++)
                                    {
                                        if(clrNos[k]== groupColorsMaterialAll[i].clr_no)
                                        {

                                            clrNoExists = true;
                                            break;
                                        }else
                                        {
                                            clrNoExists = false;
                                        }    
                                    }

                                    if (!clrNoExists)
                                    {
                                        clrNos.Add(groupColorsMaterialAll[i].clr_no);
                                    }

                                }


                                if (clrNos.Count > 0)
                                {
                                    string clrNoStr = "";
                                    for ( int z = 0; z < clrNos.Count; z++)
                                    {
                                        clrNoStr = clrNoStr + clrNos[z] + "|";
                                    }
                                   
                                    filterMaterial[j].clr_no = clrNoStr;
                                }
                                else
                                {
                                    filterMaterial[j].clr_no = "";
                                }
                               
                                isExists = true;
                                break;
                            }
                            else
                            {
                                isExists = false;                                 
                            }
                           
                          
                        } 
                        if (!isExists)
                        {
                            filterMaterial.Add(groupColorsMaterialAll[i]);
                        }

                    }

                   
                }

                //对 字段的特殊处理
                string str = "";
                for(int i = 0; i < filterMaterial.Count; i++)
                {
                    str = filterMaterial[i].season_id.ToString();
                    str = str.Replace("'","\'");
                    str = str.Replace("\"", "\\\"");
                    filterMaterial[i].season_id = str;

                    str = filterMaterial[i].mas_name.ToString();
                    str = str.Replace("'", "\'");
                    str = str.Replace("\"", "\\\"");
                    filterMaterial[i].mas_name = str;
                }

                // 保存在本地
                accoryOut.writeAccessoryToDB(filterMaterial);
                groupColorsMaterialAll = filterMaterial;
              
            }

            this.Invoke(
                  new Action(
                          delegate
                          {
                              DataTable dt = this.accoryOut.ToDataTable(groupColorsMaterialAll);
                             
                            dt.Columns.Add("select", typeof(bool)).SetOrdinal(0);
                            foreach (DataRow row in dt.Rows)
                            {
                                row["select"] = true;
                            }
                            this.dgvAccessory.DataSource = dt;

                            accessoryDT = null;
                            accessoryDT = dt;
                            nulldv();
                            this.dgvAccessory.DataSource = null;
                            this.dgvAccessory.DataSource = accessoryDT;
                             // this.btConfirmOut.Enabled = true;
                              foreach (DataGridViewColumn dgvc in dgvAccessory.Columns)
                            {
                                if (dgvc.DataPropertyName == "select"  || dgvc.DataPropertyName == "mas_qty")
                                  {
                                      dgvc.ReadOnly = false;
                                      
                                }
                                else 
                                {
                                    dgvc.ReadOnly = true;
                                      
                                }
                            }
                           
                              changHeaderText();
                               
                              Cursor = Cursors.Default;
                              this.dgvAccessory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");


                               
                          }));

            // 从本地数据库查询资料，如果没有再从远程库查询，并写入（本库）
            List<accessoryOut> accessorytb = accoryOut.getAccessoryOutByLocalHostDB(items, Org);

            if (accessorytb != null && accessorytb.Count > 0)
            {
                this.Invoke(
                    new Action(
                      delegate
                      {
                          //   this.dgvAccessory.DataSource = null;
                          //  this.dgvAccessory.DataSource = accessorytb;

                          DataTable dt = this.accoryOut.ToDataTable(accessorytb);

                          dt.Columns.Add("select", typeof(bool)).SetOrdinal(0);
                          foreach (DataRow row in dt.Rows)
                          {
                              row["select"] = true;
                          }
                          this.dgvAccessory.DataSource = dt;

                          accessoryDT = null;
                          accessoryDT = dt;
                          nulldv();
                          this.dgvAccessory.DataSource = null;
                          this.dgvAccessory.DataSource = accessoryDT;
                          foreach (DataGridViewColumn dgvc in dgvAccessory.Columns)
                          {
                              if (dgvc.DataPropertyName == "select" || dgvc.DataPropertyName == "mas_qty")
                              {
                                  dgvc.ReadOnly = false;

                              }
                              else
                              {
                                  dgvc.ReadOnly = true;

                              }
                          }

                          changHeaderText();

                          Cursor = Cursors.Default;
                          this.dgvAccessory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                          
                      }
                  ));




                 
            }



        }


        private void nulldv()
        {
            this.dgvAccessory.DataSource = null;
            int j = dgvAccessory.ColumnCount;
            for (int i = 0; i < j; j--)
            {
                dgvAccessory.Columns.RemoveAt(j - 1);
            }
        }

        private void DisplayCol(DataGridView dgv, String dataPropertyName, String headerText)
        {
            dgv.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn obj = new DataGridViewTextBoxColumn();
            obj.DataPropertyName = dataPropertyName;
            obj.HeaderText = headerText;
            obj.Name = dataPropertyName;
            obj.Resizable = DataGridViewTriState.True;
            dgv.Columns.AddRange(new DataGridViewColumn[] { obj });
        }


        private void ProgressChanged_Handler(object sender, ProgressChangedEventArgs args)
        {
            toolStatusProgressBar.Value = secont.Value;
            toolStatusProgressBar.Maximum = secont.Maxvalue;
            toolStatusLabelStatus.Text = secont.Status;
            toolStatusLabelNote1.Text = secont.Note1;
            toolStatusLabelStatusName.Text = secont.Statusname;
            toolStatusLabelNow.Text = secont.Now;
            toolStatusLabelNote2.Text = secont.Note2;
            toolStatusLabelAll.Text = secont.All;
        }

        private void RunWorkerCompleted_Handler(object sender, RunWorkerCompletedEventArgs args)
        {
            toolStatusProgressBar.Value = 0;
            if (args.Error != null)
            {
                MessageBox.Show(args.Error.Message);
            }
            else if (args.Cancelled)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("获取资料取消。", "消息");

            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("获取资料完成。", "消息");
            }
            this.cbStyle.Enabled = true;
            this.txtStyle.Enabled = true;
            this.cbPurNo.Enabled = true;
            this.txtPurNo.Enabled = true;
            this.cbReceiveNumber.Enabled = true;
            this.txtReceiveNumber.Enabled = true;
        }


        private void txtMyNo_TextChanged(object sender, EventArgs e)
        {
            if (txtMyNo.Text.Trim().Length > 0)
            {
                this.cbMyNo.Checked = true;
            }
            else
            {
                this.cbMyNo.Checked = false;
            }

        }

        private void txtStyle_TextChanged(object sender, EventArgs e)
        {
            if (txtStyle.Text.Trim().Length > 0)
            {
                this.cbStyle.Checked = true;
            }
            else
            {
                this.cbStyle.Checked = false;
            }
        }

        private void txtPurNo_TextChanged(object sender, EventArgs e)
        {
            if (txtPurNo.Text.Trim().Length > 0)
            {
                this.cbPurNo.Checked = true;
            }
            else
            {
                this.cbPurNo.Checked = false;
            }
        }

        private void txtReceiveNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtReceiveNumber.Text.Trim().Length > 0)
            {
                this.cbReceiveNumber.Checked = true;
            }
            else
            {
                this.cbReceiveNumber.Checked = false;
            }
        }

        private void dgvAccessory_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
        private void dgvAccessory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvAccessory.Rows[e.RowIndex].Selected == false)
                    {
                        dgvAccessory.ClearSelection();
                        dgvAccessory.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvAccessory.SelectedRows.Count == 1)
                    {
                        dgvAccessory.CurrentCell = dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
            Clipboard.SetDataObject(dgvAccessory.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvAccessory.GetClipboardContent());
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
            tabl = GetDgvToTable(dgvAccessory);

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

        /*
        private void btUpdata_Click(object sender, EventArgs e)
        {
            this.cbStyle.Enabled = false;
            this.txtStyle.Enabled = false;
            this.cbPurNo.Enabled = false;
            this.txtPurNo.Enabled = false;
            this.cbReceiveNumber.Enabled = false;
            this.txtReceiveNumber.Enabled = false;
            this.upDataFromERP();
        }
        */

        public void upDataFromERP()
        {
            parameter p = new parameter();
            this.items.Clear();
            // 自编单号
            if (cbMyNo.Checked && txtMyNo.Text.Trim() != "")
            {
                p.pkey = "my_no";
                p.pvalue = txtMyNo.Text.Trim();
                this.items.Add(p);
            }
            else
            {
                var whereRemove = items.FirstOrDefault(t => t.pkey == "my_no");
                this.items.Remove(whereRemove);
            }

            if (this.items.Count <= 0)
            {
                MessageBox.Show("请输入自编单号");
                return;
            }

            Cursor = Cursors.WaitCursor;

            // 先从本地数据库查询是否有资料
            List<accessoryOut> accessorytb = accoryOut.getAccessoryOutByLocalHostDB(txtMyNo.Text.Trim());
            if (accessorytb != null && accessorytb.Count > 0)
            {
                // 2、有资料 已有数据  是否需要更新
                // 软删除 
                if (MessageBox.Show("已有数据  是否需要更新?", "提示", MessageBoxButtons.YesNo) ==
                    DialogResult.Yes)
                {
                    //  标记状态为E  表示有错误，已不再使用了  。软删除
                    // 物料发放状态  O完成  N未发料  P发了一部分  进行中 E表示有错误  有修改  不再使用的
                    int i = accoryOut.updataAccessoryOutFromLocalHostDBByMyNumber(txtMyNo.Text.Trim());
                    MessageBox.Show("共删除" + i.ToString() + "行数据");
                }
                else
                {
                    return;
                }
            }
            // 1、无资料
            // 从ERP数据库查询并写入
            bgWorker.RunWorkerAsync();
        }


        private void cbGroupColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show("dd");
            string clr_nostr = cbGroupColor.Text.Trim();
            string selectstr = "";
            if (clr_nostr.Length <= 0)
            {
                selectstr = "clr_no LIKE '%'";
            }
            else
            {
                selectstr = "clr_no LIKE '%" + clr_nostr + "%'";
            }

            if (accessoryDT.Rows.Count <= 0)
            {
                return;
            }
            DataTable dt = accessoryDT;
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select(selectstr);
            if (dr == null || dr.Length == 0)
            { }
            else
            {
                for (int i = 0; i < dr.Length; i++)
                {
                  //  dr[i].ItemArray[18]=;
                    newdt.ImportRow((DataRow)dr[i]);
                }
            }
           
           

            // 重新计算数量
            int? qty = 0;
            if (this.allqtys.Count <= 0)
            {
                return;
            }

            List<string> groupPONumbers = new List<string>();
            this.cklistBoxPO.Items.Clear();
            for (int i = 0; i < allqtys.Count; i++)
            {
                if (clr_nostr=="")
                {
                    qty = qty + allqtys[i].qty;

                    string PONumber = Convert.ToString(allqtys[i].po_no);
                    bool igName = Isgroups(groupPONumbers, PONumber);
                    if (!igName)
                    {
                        this.cklistBoxPO.Items.Add(PONumber);
                        groupPONumbers.Add(PONumber);
                    }
                }
                else if (allqtys[i].clr_no == clr_nostr)
                {
                    qty = qty + allqtys[i].qty;                    
                    string PONumber = Convert.ToString(allqtys[i].po_no);
                    bool igName = Isgroups(groupPONumbers, PONumber);
                    if (!igName)
                    {  
                        this.cklistBoxPO.Items.Add(PONumber);
                        groupPONumbers.Add(PONumber);

                    }
                }
            }
            this.labPuQty.Text = qty.ToString();
            this.txtManufactures.Text = qty.ToString();
            // nulldv();
            // 重新计算应发料量
            
            int? reqty = 0;
            if(qty != null)
            {
                reqty = qty;
            }

            DataTable redt = reCheckMasQTY(newdt, reqty);
            this.dgvAccessory.DataSource = null;
            this.dgvAccessory.DataSource = redt;

            foreach (DataGridViewColumn dgvc in dgvAccessory.Columns)
            {
                if (dgvc.DataPropertyName == "select" || dgvc.DataPropertyName == "mas_qty")
                {
                    dgvc.ReadOnly = false;

                }
                else
                {
                    dgvc.ReadOnly = true;

                }
            } 

            changHeaderText();
            Cursor = Cursors.Default;
            this.dgvAccessory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            for (int i = 0; i < this.cklistBoxPO.Items.Count; i++)
            {
                this.cklistBoxPO.SetItemChecked(i, true);
            }

        }

        
        /// <summary>
        /// 改变生产数量后需要重新计算发料数量
        /// </summary>
        /// <param name="dt"></param>
        public DataTable reCheckMasQTY(DataTable dt,int? qty)
        {
            // 计算应发料量
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["unit_id_p"].ToString())
                {
                    case "PCS":
                        dt.Rows[i]["mas_qty"] = Convert.ToString(Math.Ceiling(Convert.ToDouble(dt.Rows[i]["unit_qty"])
                            * Convert.ToDouble(qty)
                            * (Convert.ToDouble(dt.Rows[i]["loss"]) / 100 + 1)
                            / Convert.ToDouble(dt.Rows[i]["trans_rate"])));
                        break;
                    case "PC":
                        dt.Rows[i]["mas_qty"] = Convert.ToString(Math.Ceiling(Convert.ToDouble(dt.Rows[i]["unit_qty"])
                            * Convert.ToDouble(qty)
                            * (Convert.ToDouble(dt.Rows[i]["loss"]) / 100 + 1)
                            / Convert.ToDouble(dt.Rows[i]["trans_rate"])));
                        break;
                    case "PR":
                        dt.Rows[i]["mas_qty"] = Convert.ToString(Math.Ceiling(Convert.ToDouble(dt.Rows[i]["unit_qty"])
                           * Convert.ToDouble(qty)
                           * (Convert.ToDouble(dt.Rows[i]["loss"]) / 100 + 1)
                           / Convert.ToDouble(dt.Rows[i]["trans_rate"]))); 
                        break;
                    case "M":
                        dt.Rows[i]["mas_qty"] = Convert.ToString(Math.Ceiling(Convert.ToDouble(dt.Rows[i]["unit_qty"])
                            * Convert.ToDouble(qty)
                            * (Convert.ToDouble(dt.Rows[i]["loss"]) / 100 + 1)
                            / Convert.ToDouble(dt.Rows[i]["trans_rate"])));
                        break;
                    case "CONE":
                        dt.Rows[i]["mas_qty"] = Convert.ToString(Math.Ceiling(Convert.ToDouble(dt.Rows[i]["unit_qty"])
                            * Convert.ToDouble(qty)
                            * (Convert.ToDouble(dt.Rows[i]["loss"]) / 100 + 1)
                            / Convert.ToDouble(dt.Rows[i]["trans_rate"])));
                        break;
                    default:

                        dt.Rows[i]["mas_qty"] = Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[i]["unit_qty"])
                             * Convert.ToDouble(qty)
                             * (Convert.ToDouble(dt.Rows[i]["loss"]) / 100 + 1)
                             / Convert.ToDouble(dt.Rows[i]["trans_rate"]), 4, MidpointRounding.AwayFromZero));
                        break;
                }
            }
            return dt;
        }

        /// <summary>
        /// 测试连接服务器情况
        /// </summary>
        /// <returns></returns>
        private bool testLinServer(string serIP)
        {
            if (LinSerTest.LinServer(serIP))
            {
                //   MessageBox.Show("连接服务器" + serIP + "成功！");                 
                return true;
            }
            else
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (!LinSerTest.LinServer(serIP))
                    {
                         MessageBox.Show("服务器连接失败！正在尝试重新连接，第 " + i.ToString() + " 次...");
                        
                    }
                    else
                    {
                         MessageBox.Show("服务器已连接");
                        break;
                    }
                }

                if (!LinSerTest.LinServer(serIP))
                {
                    MessageBox.Show("服务器连接失败！请检查网络连接!");
                    return false;
                }

                return true;
            }
        }

        public bool testOpenDB(string serName )
        {
            List<string> list = LinSerTest.TestConnection(serName);
            //错误连接处理
            if (list[0].ToString() == "连接数据库错误" || list[0].ToString() == "未知错误")
            {
               // MessageBox.Show("服务器连接成功，但数据库连接失败");                 
                return false;
            }else
            {
               // for (int i = 0; i < list.Count; i++)
              //  {
                //    MessageBox.Show("服务器连接成功,数据库打开成功");
               // }
                return true;
            }
        }

        private void txtMyNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter){
                Cursor = Cursors.WaitCursor;
                if (bgWorker.IsBusy)
                {
                    MessageBox.Show("系统正在运行，请稍等...");
                }
                else
                {
                    bgWorker.RunWorkerAsync();
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

        private void cbboxseleceall_CheckedChanged(object sender, EventArgs e)
        {
            DataTable tb = dgvAccessory.DataSource as DataTable;
            dgvAccessory.DataSource = null;

           // DataTable tb = GetDgvToTable(this.dgvAccessory);     
           
            if (cbboxseleceall.Checked)
            {
                foreach (DataRow row in tb.Rows)
                {
                    row["select"] = true;
                }
            }
            else
            {
                foreach (DataRow row in tb.Rows)
                {
                    row["select"] = false;
                }
            }
            //  this.dgvAccessory.DataSource = null;
            //   this.dgvAccessory.DataSource = tb;

          //  tb.Columns.Add("select", typeof(bool)).SetOrdinal(0);
         //   foreach (DataRow row in tb.Rows)
          //  {
          //      row["select"] = true;
         //   }
            this.dgvAccessory.DataSource = tb;
          

            accessoryDT = null;
            accessoryDT = tb;            
            nulldv(); 

            this.dgvAccessory.DataSource = null;
            this.dgvAccessory.DataSource = accessoryDT;
            foreach (DataGridViewColumn dgvc in dgvAccessory.Columns)
            {
                if (dgvc.DataPropertyName != "select")
                {
                    dgvc.ReadOnly = true;
                }
                else
                {
                    dgvc.ReadOnly = false;
                }
            }
            changHeaderText();
             Cursor = Cursors.Default;
          //  this.dgvAccessory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
        }

        public void changHeaderText()
        {
            this.dgvAccessory.Columns["id"].Visible = false;

            this.dgvAccessory.Columns["select"].HeaderText = "选择";
            this.dgvAccessory.Columns["be_id"].HeaderText = "生产厂区";
            this.dgvAccessory.Columns["my_no"].HeaderText = "自编单号";
            this.dgvAccessory.Columns["od_no"].HeaderText = "BEST订单号码";
            this.dgvAccessory.Columns["style_id"].HeaderText = "款号";
            this.dgvAccessory.Columns["clr_no"].HeaderText = "成衣色组号";

            this.dgvAccessory.Columns["mas_sortNumber"].HeaderText = "物料分群码";
            this.dgvAccessory.Columns["mas_sortNumber"].Visible = false;
            this.dgvAccessory.Columns["mas_sortName"].HeaderText = "物料分群名称";
            this.dgvAccessory.Columns["mas_sortName"].Visible = false;

            this.dgvAccessory.Columns["item_id"].HeaderText = "物料分群码";
            this.dgvAccessory.Columns["item_name"].HeaderText = "物料分群名称";
            this.dgvAccessory.Columns["mas_id"].HeaderText = "物料ID";
            this.dgvAccessory.Columns["mas_name"].HeaderText = "物料名称";
            this.dgvAccessory.Columns["color_no"].HeaderText = "物料颜色";
            this.dgvAccessory.Columns["color_name2"].HeaderText = "颜色中文名称";

            this.dgvAccessory.Columns["size"].HeaderText = "物料尺码";
            this.dgvAccessory.Columns["unit_qty"].HeaderText = "单位用量";
            this.dgvAccessory.Columns["loss"].HeaderText = "预补%";
            this.dgvAccessory.Columns["pu_qty"].HeaderText = "采购数量";
            this.dgvAccessory.Columns["style_mas_qty"].HeaderText = "款式总用量";
            this.dgvAccessory.Columns["unit_id"].HeaderText = "物料单位";
            this.dgvAccessory.Columns["trans_rate"].HeaderText = "单位转换比率";
            this.dgvAccessory.Columns["mas_qty"].HeaderText = "应发料量";
            this.dgvAccessory.Columns["unit_id_p"].HeaderText = "应发料单位";


            this.dgvAccessory.Columns["od_qty"].HeaderText = "总订单数量";
           // this.dgvAccessory.Columns["od_Finished_qty"].HeaderText = "已生产数量"; 
         //   this.dgvAccessory.Columns["od_Unfinished_qty"].HeaderText = "待生产数量";
          //  this.dgvAccessory.Columns["od_make_qty"].HeaderText = "本批生产数量";
        //    this.dgvAccessory.Columns["od_make_qty"].DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
         //   this.dgvAccessory.Columns["od_Exceed_qty"].HeaderText = "溢出数量";
         //   this.dgvAccessory.Columns["od_Exceed_qty"].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            
            this.dgvAccessory.Columns["pu_trans_qty"].HeaderText = "采购数量(换算)";
            this.dgvAccessory.Columns["pu_trans_qty"].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            this.dgvAccessory.Columns["masFinished_qty"].HeaderText = "累计已发物料数量";
            this.dgvAccessory.Columns["masUnfinished_qty"].HeaderText = "剩余物料数量";
            this.dgvAccessory.Columns["mas_qty"].HeaderText = "本次发料量";
            this.dgvAccessory.Columns["mas_qty"].DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.dgvAccessory.Columns["masExceed_qty"].HeaderText = "超发料量";
            this.dgvAccessory.Columns["masExceed_qty"].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;


            this.dgvAccessory.Columns["pu_no"].HeaderText = "采购单号";
            this.dgvAccessory.Columns["vend_id"].HeaderText = "供应商编码";
            this.dgvAccessory.Columns["vend_abbr"].HeaderText = "供应商简称";

            this.dgvAccessory.Columns["per_id"].HeaderText = "审核人员ID";
            this.dgvAccessory.Columns["per_id"].Visible = false;
            this.dgvAccessory.Columns["in_date"].Visible = false;

            this.dgvAccessory.Columns["season_id"].HeaderText = "季节";
            this.dgvAccessory.Columns["release_who"].HeaderText = "审核人员";
            this.dgvAccessory.Columns["style_name"].HeaderText = "品名";
            this.dgvAccessory.Columns["sex_name"].HeaderText = "品类";
            this.dgvAccessory.Columns["sample_no"].HeaderText = "版号";

            this.dgvAccessory.Columns["color_name"].Visible = false;
            this.dgvAccessory.Columns["mas_type"].Visible = false;

            this.dgvAccessory.Columns["createPerson"].HeaderText = "建单人";
            this.dgvAccessory.Columns["createDate"].HeaderText = "建单日期"; 
            this.dgvAccessory.Columns["changePerson"].HeaderText = "最后修改人";
            this.dgvAccessory.Columns["changeDate"].HeaderText = "最后修改日期";
            this.dgvAccessory.Columns["receiveNumber"].HeaderText = "领料单号";

            this.dgvAccessory.Columns["receiveNumberBatch"].HeaderText = "发料批号";
            this.dgvAccessory.Columns["materialStatus"].HeaderText = "物料发放状态";
            this.dgvAccessory.Columns["printTimes"].HeaderText = "打印次数";
            this.dgvAccessory.Columns["note"].HeaderText = "备注";

        }

        private void cklistBoxPO_SelectedIndexChanged(object sender, EventArgs e)
        {

            ArrayList result = GetCheckedItemsText(); // 获取PO

            string clr_nostr = cbGroupColor.Text.Trim();
           // string selectstr = "";
          //  if (clr_nostr.Length <= 0)
           // {
         //       selectstr = "clr_no LIKE '%'";
          //  }
          //  else
          //  {
          //      selectstr = "clr_no LIKE '%" + clr_nostr + "%'";
          //  }


            // 重新计算数量
            int? qty = 0;
            if (this.allqtys.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < allqtys.Count; j++)
                {
                    if (result[i].ToString() == allqtys[j].po_no &&   allqtys[j].clr_no == clr_nostr)
                    {
                        qty = qty + allqtys[j].qty;
                    }
                }
            }
            this.labPuQty.Text = qty.ToString();
        }
        private ArrayList GetCheckedItemsText( )
        {
            ArrayList result = new ArrayList();
            for (int i = 0; i <this.cklistBoxPO.Items.Count; i++)
            {
                if (this.cklistBoxPO.GetItemChecked(i))
                {
                    result.Add(this.cklistBoxPO.Items[i].ToString());
                }
            }
            return result;
        }

        private void cklistSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtManufactures_TextChanged(object sender, EventArgs e)
        {
            string tmpny = this.txtManufactures.Text.Trim();
            Regex r = new Regex(@"^[0-9]+$"); 
            if (!r.Match(tmpny).Success)
            {
                MessageBox.Show("请输入数字");
                return;
            }
            labQty.Text = tmpny;
        }

        private void txtManufactures_KeyDown(object sender, KeyEventArgs e)
        {
            string tmpny = this.txtManufactures.Text.Trim();
            Regex r = new Regex(@"^[0-9]+$");
            if (!r.Match(tmpny).Success)
            {
                MessageBox.Show("请输入数字");
                return;
            }
            labQty.Text = tmpny;



            if (e.KeyCode == Keys.Enter)
            {
                Cursor = Cursors.WaitCursor;
                if (bgWorker.IsBusy)
                {
                    MessageBox.Show("系统正在运行，请稍等...");
                }
                else
                {
                    // MessageBox.Show("重新计算生产数量");                    
                    if (this.dgvAccessory.Rows.Count <= 0)
                    {
                        return;
                    }
                    DataTable dt = (DataTable) this.dgvAccessory.DataSource;
                    List<materials> groupColorsMaterialAll = new List<materials>();
                    materials material = new materials();
                   // for ( int i=0;i< dt.Rows.Count; i++)
                  //  {
                     //   material.id = dt.Rows[i][]
                 //   }

                   // List<materials> groupColorsMaterialAll  = accoryOut.materialsDataToList(dt);


                    string od_no = groupColorsMaterialAll[0].od_no;
                    int? od_qty = groupColorsMaterialAll[0].od_qty;
                    this.runMake(groupColorsMaterialAll, od_no, od_qty, Convert.ToInt32( tmpny));
                    Cursor = Cursors.Default;
                }

            }
        }

        
        private void dgvAccessory_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //可编辑的列
            if (e.ColumnIndex != 34)
            {
                return;
            }
            double outDb = 0;
           
            if (double.TryParse(e.FormattedValue.ToString(), out outDb))
            {
                e.Cancel = false;
              //  this.cellstr = this.dgvAccessory.CurrentCell.Value.ToString();
                double x = Convert.ToDouble( this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value);
                double y = Convert.ToDouble(this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = x - y;
                //this.dgvAccessory.Rows
            }
            else
            {
                e.Cancel = true;//数据格式不正确则还原
                dgvAccessory.CancelEdit();
            }
           

        }
 

        private void dgvAccessory_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //  MessageBox.Show(this.cellstr);
            // this.dgvAccessory.CurrentCell.Value.ToString();
            double s = 0.00;
            double c = 0.00;

            if (e.ColumnIndex > 10)
            {
                double x = Convert.ToDouble(this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value);
               
                double y = Convert.ToDouble(this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                double z = Convert.ToDouble(this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value);
                s = y - x;
                s = Math.Round(s, 4, MidpointRounding.AwayFromZero);
                if (s > 0)
                {
                    this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = s;
                }
                else
                {
                    this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = 0;
                }

                c = x - (y+z);
                c = Math.Round(c, 4, MidpointRounding.AwayFromZero);
                if (c > 0)
                {
                    this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = c;
                }
                else
                {
                    this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = 0;
                }

              
            }
          
        }

        private void btConfirmOut_Click(object sender, EventArgs e)
        {

            DataTable tb = dgvAccessory.DataSource as DataTable;
            List<materialhs> makeMaterial = new List<materialhs>();

            List<materials> mMaterial = new List<materials>();
            int batch = 0;
            int maxBatch = 0;
            string  batchstr = "";
            if (tb == null ||  tb.Rows.Count <= 0)
            {
                return;
            }
           
            // 取出最大的批号
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                batch = Convert.ToInt32(tb.Rows[i]["receiveNumberBatch"].ToString());
                if(batch > maxBatch)
                {
                    maxBatch = batch;
                }
            }

                for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (tb.Rows[i]["select"].ToString() == "True")
                {

                    tb.Rows[i]["masFinished_qty"] = Convert.ToDouble( tb.Rows[i]["masFinished_qty"].ToString()) + Convert.ToDouble(tb.Rows[i]["mas_qty"].ToString());
                    tb.Rows[i]["masUnfinished_qty"] = Convert.ToDouble(tb.Rows[i]["pu_trans_qty"].ToString()) - Convert.ToDouble(tb.Rows[i]["masFinished_qty"].ToString());

                    batch = maxBatch + 1;
                    batchstr = batch.ToString();
                    while (batchstr.ToString().Length < 2)
                    {
                        batchstr = "0" + batch.ToString();
                    }

                    tb.Rows[i]["receiveNumberBatch"] = batchstr;

                    materialhs item = new materialhs();
                   
                    //tb.Rows[i]["mas_qty"] = 0;    
                    item.a_id =Convert.ToInt32( tb.Rows[i]["id"].ToString());
                    item.be_id = tb.Rows[i]["be_id"].ToString();
                    item.my_no = tb.Rows[i]["my_no"].ToString();
                    item.style_id = tb.Rows[i]["style_id"].ToString();
                    item.od_no = tb.Rows[i]["od_no"].ToString();
                    item.od_qty =Convert.ToInt32( tb.Rows[i]["od_qty"].ToString());

                    item.season_id = tb.Rows[i]["season_id"].ToString();
                    item.style_name = tb.Rows[i]["style_name"].ToString();
                    item.sex_name = tb.Rows[i]["sex_name"].ToString();
                    item.sample_no = tb.Rows[i]["sample_no"].ToString();
                    item.in_date = tb.Rows[i]["in_date"].ToString();
                    item.release_who = tb.Rows[i]["release_who"].ToString();

                    item.clr_no = tb.Rows[i]["clr_no"].ToString();
                    item.mas_sortNumber = tb.Rows[i]["mas_sortNumber"].ToString();
                    item.mas_sortName = tb.Rows[i]["mas_sortName"].ToString();
                    item.item_id = tb.Rows[i]["item_id"].ToString();
                    item.item_name = tb.Rows[i]["item_name"].ToString();
                    item.mas_id = tb.Rows[i]["mas_id"].ToString();

                    item.mas_name = tb.Rows[i]["mas_name"].ToString();
                    item.color_no = tb.Rows[i]["color_no"].ToString();
                    item.color_name2 = tb.Rows[i]["color_name2"].ToString();
                    item.color_name = tb.Rows[i]["color_name"].ToString();
                    item.item_name = tb.Rows[i]["item_name"].ToString();
                    item.mas_type = tb.Rows[i]["mas_type"].ToString();

                    item.size = tb.Rows[i]["size"].ToString();
                    item.loss =Convert.ToDouble( tb.Rows[i]["loss"].ToString());
                    item.unit_qty =Convert.ToDouble( tb.Rows[i]["unit_qty"].ToString());
                    item.pu_qty = Convert.ToDouble( tb.Rows[i]["pu_qty"].ToString());
                    item.style_mas_qty =Convert.ToDouble( tb.Rows[i]["style_mas_qty"].ToString());
                    item.unit_id =  tb.Rows[i]["unit_id"].ToString();
                    item.trans_rate = Convert.ToDouble( tb.Rows[i]["trans_rate"].ToString());
                    
                    item.pu_trans_qty = Convert.ToDouble(tb.Rows[i]["pu_trans_qty"].ToString());
                    item.masFinished_qty = Convert.ToDouble(tb.Rows[i]["masFinished_qty"].ToString());
                    item.masUnfinished_qty = Convert.ToDouble(tb.Rows[i]["masUnfinished_qty"].ToString());
                    item.mas_qty = Convert.ToDouble(tb.Rows[i]["mas_qty"].ToString());
                    item.masExceed_qty =Convert.ToDouble( tb.Rows[i]["masExceed_qty"].ToString());

                    item.unit_id_p = tb.Rows[i]["unit_id_p"].ToString();
                    item.pu_no = tb.Rows[i]["pu_no"].ToString();
                    item.vend_id = tb.Rows[i]["vend_id"].ToString();
                    item.vend_abbr = tb.Rows[i]["vend_abbr"].ToString();
                    item.per_id = tb.Rows[i]["per_id"].ToString();


                    item.createPerson = tb.Rows[i]["createPerson"].ToString();
                    item.createDate = tb.Rows[i]["createDate"].ToString();
                    item.changePerson = tb.Rows[i]["changePerson"].ToString();
                    item.changeDate = tb.Rows[i]["changeDate"].ToString();
                    item.receiveNumber = tb.Rows[i]["receiveNumber"].ToString();
                    item.receiveNumberBatch = tb.Rows[i]["receiveNumberBatch"].ToString();
                    item.materialStatus = tb.Rows[i]["materialStatus"].ToString();
                    item.printTimes =Convert.ToInt32( tb.Rows[i]["printTimes"].ToString());
                    item.note = tb.Rows[i]["note"].ToString();

                    makeMaterial.Add(item);

                    //----------------------------------------------------
                    materials mitem = new materials();

                    mitem.id =Convert.ToInt32( tb.Rows[i]["id"].ToString());
                    mitem.be_id = tb.Rows[i]["be_id"].ToString();
                    mitem.my_no = tb.Rows[i]["my_no"].ToString();
                    mitem.style_id = tb.Rows[i]["style_id"].ToString();
                    mitem.od_no = tb.Rows[i]["od_no"].ToString();
                    mitem.od_qty = Convert.ToInt32(tb.Rows[i]["od_qty"].ToString());

                    mitem.season_id = tb.Rows[i]["season_id"].ToString();
                    mitem.style_name = tb.Rows[i]["style_name"].ToString();
                    mitem.sex_name = tb.Rows[i]["sex_name"].ToString();
                    mitem.sample_no = tb.Rows[i]["sample_no"].ToString();
                    mitem.in_date = tb.Rows[i]["in_date"].ToString();
                    mitem.release_who = tb.Rows[i]["release_who"].ToString();

                    mitem.clr_no = tb.Rows[i]["clr_no"].ToString();
                    mitem.mas_sortNumber = tb.Rows[i]["mas_sortNumber"].ToString();
                    mitem.mas_sortName = tb.Rows[i]["mas_sortName"].ToString();
                    mitem.item_id = tb.Rows[i]["item_id"].ToString();
                    mitem.item_name = tb.Rows[i]["item_name"].ToString();
                    mitem.mas_id = tb.Rows[i]["mas_id"].ToString();

                    mitem.mas_name = tb.Rows[i]["mas_name"].ToString();
                    mitem.color_no = tb.Rows[i]["color_no"].ToString();
                    mitem.color_name2 = tb.Rows[i]["color_name2"].ToString();
                    mitem.color_name = tb.Rows[i]["color_name"].ToString();
                    mitem.item_name = tb.Rows[i]["item_name"].ToString();
                    mitem.mas_type = tb.Rows[i]["mas_type"].ToString();

                    mitem.size = tb.Rows[i]["size"].ToString();
                    mitem.loss = Convert.ToDouble(tb.Rows[i]["loss"].ToString());
                    mitem.unit_qty = Convert.ToDouble(tb.Rows[i]["unit_qty"].ToString());
                    mitem.pu_qty = Convert.ToDouble(tb.Rows[i]["pu_qty"].ToString());
                    mitem.style_mas_qty = Convert.ToDouble(tb.Rows[i]["style_mas_qty"].ToString());
                    mitem.unit_id = tb.Rows[i]["unit_id"].ToString();
                    mitem.trans_rate = Convert.ToDouble(tb.Rows[i]["trans_rate"].ToString());

                    mitem.pu_trans_qty = Convert.ToDouble(tb.Rows[i]["pu_trans_qty"].ToString());
                    mitem.masFinished_qty = Convert.ToDouble(tb.Rows[i]["masFinished_qty"].ToString());
                    mitem.masUnfinished_qty = Convert.ToDouble(tb.Rows[i]["masUnfinished_qty"].ToString());
                    mitem.mas_qty = Convert.ToDouble(tb.Rows[i]["mas_qty"].ToString());
                    mitem.masExceed_qty = Convert.ToDouble(tb.Rows[i]["masExceed_qty"].ToString());

                    mitem.unit_id_p = tb.Rows[i]["unit_id_p"].ToString();
                    mitem.pu_no = tb.Rows[i]["pu_no"].ToString();
                    mitem.vend_id = tb.Rows[i]["vend_id"].ToString();
                    mitem.vend_abbr = tb.Rows[i]["vend_abbr"].ToString();
                    mitem.per_id = tb.Rows[i]["per_id"].ToString();


                    mitem.createPerson = tb.Rows[i]["createPerson"].ToString();
                    mitem.createDate = tb.Rows[i]["createDate"].ToString();
                    mitem.changePerson = tb.Rows[i]["changePerson"].ToString();
                    mitem.changeDate = tb.Rows[i]["changeDate"].ToString();
                    mitem.receiveNumber = tb.Rows[i]["receiveNumber"].ToString();
                    mitem.receiveNumberBatch = tb.Rows[i]["receiveNumberBatch"].ToString();
                    mitem.materialStatus = tb.Rows[i]["materialStatus"].ToString();
                    mitem.printTimes = Convert.ToInt32(tb.Rows[i]["printTimes"].ToString());
                    mitem.note = tb.Rows[i]["note"].ToString();

                    mMaterial.Add(mitem);
                }
                //this.btPrint.Visible = true;
                //this.btPrint.Enabled = true;
            }

            if (makeMaterial.Count <= 0)
            {
                return;
            }
            //对 字段的特殊处理
            string str = "";
            for (int i = 0; i < makeMaterial.Count; i++)
            {
                str = makeMaterial[i].season_id.ToString();
                str = str.Replace("'", "\'");
                str = str.Replace("\"", "\\\"");
                makeMaterial[i].season_id = str;
                mMaterial[i].season_id = str;

                str = makeMaterial[i].mas_name.ToString();
                str = str.Replace("'", "\'");
                str = str.Replace("\"", "\\\"");
                makeMaterial[i].mas_name = str;
                mMaterial[i].mas_name = str;
            }

             this.reNo = makeMaterial[0].receiveNumber  ; //发料单号
             this.reNoBatch = makeMaterial[0].receiveNumberBatch;//发料单批号

        accoryOut.writeAccessoryhToDB(makeMaterial);
            accoryOut.updataAccessoryToDB(mMaterial);
            //把选择好的写入数据库
            // 累计发料=累计发料+本次发料
            // 剩余物料= 采购物料 -累计发料
            // 本次发料 =0
            // 基它不动
            MessageBox.Show("保存完成");
            // this.btConfirmOut.Enabled = false;
        }

        private void FrmaccessoryOut_Load(object sender, EventArgs e)
        {
           // this.btPrint.Enabled = false;
           // this.btPrint.Visible = false;
          //  this.btConfirmOut.Enabled = false;
        }

        private void btPrint_Click(object sender, EventArgs e)
        {

           

            FrmAccessOryPrint frm = FrmAccessOryPrint.GetSingleton(this.reNo,this.reNoBatch);
           // frm.MdiParent = this;
            frm.Show();
            frm.Activate();



            //this.btPrint.Enabled = false;

             
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
public static class DoubleBufferDataGridView
{
    /// <summary>
    /// 双缓冲，解决闪烁问题
    /// </summary>
    /// <param name="dgv"></param>
    /// <param name="flag"></param>
    public static void DoubleBufferedDataGirdView(this DataGridView dgv, bool flag)
    {
        Type dgvType = dgv.GetType();
        PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        pi.SetValue(dgv, flag, null);
    }
}