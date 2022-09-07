using BLL;
using COMMON;
using DataGridViewAutoFilter;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmShippingPackages : Form
    {
        private static FrmShippingPackages frm;
        private String filename = null;
        public string fileNameStr = "";
        public string sheeTnameStr = "";
        public int headNoStr = -1;
        public int hiedcolumnindex = -1;
        public int LastWeeks = 2;
        public DataTable shippingPackagesDT;
        private DataGridView selecteddgv = null;

        private bar barstr = new bar();

        public xiaomingCommom myCommon = new xiaomingCommom();
        private ShippingPackagesManager spm = new ShippingPackagesManager();

        // private tradingComanyPOManager BgtnPO = new tradingComanyPOManager();
        private DataTable table = new DataTable();

        private DataTable sizeTable = new DataTable();

        public AccomplishTask TaskCallBack;
        public UpdateUI UpdateUIDelegate;

        public delegate void AccomplishTask();

        public delegate void UpdateUI(bar barstr);

        private delegate void AsynUpdateUI(bar barstr);

        public FrmShippingPackages()
        {
            InitializeComponent();
            this.dgvExcels.DoubleBufferedDataGirdView(true);
            this.dgvBookingStatusOld.DoubleBufferedDataGirdView(true);
            this.dgvBookingStatusChanges.DoubleBufferedDataGirdView(true);
            this.dgvPackageStatus.DoubleBufferedDataGirdView(true);
            this.dgvPackageSizes.DoubleBufferedDataGirdView(true);
        }

        private void FrmShippingPackages_Load(object sender, EventArgs e)
        {
            this.rbSAA.Checked = true;
            this.rbSAA2.Checked = true;
            this.rbSAA3.Checked = true;
            this.cbData2.Checked = true;
            this.cbData3.Checked = true;
            this.cbBookStatus2.SelectedIndex = 1;
            this.cbBookStatus3.SelectedIndex = 1;

            // this.dtpStartDate.Value = DateTime.Now.addw
            betweenDate bdate = this.getDate(LastWeeks, 1);
            this.dtpStartDate.Value = bdate.start_time;
            this.dtpStartDate3.Value = bdate.start_time;

            this.dtpStopDate.Value = bdate.end_time.AddDays(21);
            this.dtpStopDate3.Value = bdate.end_time.AddDays(21);
            this.splitContainer1.SplitterDistance = Convert.ToInt32(this.splitContainer1.Height * 0.5);

            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new DrawItemEventHandler(this.tabControl1_DrawItem);
        }

        private betweenDate getDate(int weeks, int type)
        {
            DateTime currentTime = DateTime.Now;
            int week = Convert.ToInt32(currentTime.DayOfWeek);
            week = week == 0 ? 7 : week;
            betweenDate bdate = new betweenDate();

            switch (type)
            {
                case 0:
                    //获取上周星期一/星期天
                    bdate.start_time = currentTime.AddDays(1 - week - 7 * weeks);//上周星期一
                    bdate.end_time = currentTime.AddDays(7 - week - 7 * weeks);//上周星期天  -1 星期六                                                                                            //   Console.WriteLine("上周:" + start_time_last_week + "|" + end_time_last_week);
                    break;

                case 1:
                    //获取本周星期一/星期天
                    bdate.start_time = currentTime.AddDays(1 - week);//本周星期一
                    bdate.end_time = currentTime.AddDays(7 - week);//本周星期天
                    // Console.WriteLine("本周:" + start_time_current_week + "|" + end_time_current_week);
                    break;

                case 2:
                    //3-5;6-8;9-11;12-2  startQuarter
                    bdate.start_time = currentTime.AddMonths(0 - (currentTime.Month % 3)).AddDays(1 - currentTime.Day);  //本季度初
                    bdate.end_time = bdate.start_time.AddMonths(3).AddDays(-1);  //本季度末
                    break;

                case 3:
                    //1-3;4-6;7-9;10-12  startQuarter
                    bdate.start_time = currentTime.AddMonths(0 - (currentTime.Month - 1) % 3).AddDays(1 - currentTime.Day);  //本季度初
                    bdate.end_time = bdate.start_time.AddMonths(3).AddDays(-1);  //本季度末
                    break;

                default:
                    bdate.start_time = currentTime;
                    bdate.end_time = currentTime;
                    break;
            }

            return bdate;
        }

        public static FrmShippingPackages GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmShippingPackages();
            }
            return frm;
        }

        private void btnSelected_Click(object sender, EventArgs e)
        {
            this.cbSelectSheet.Items.Clear();
            this.dgvExcels.DataSource = null;

            OpenFileDialog sdfExport = new OpenFileDialog();

            sdfExport.Filter = "Excel 2007文件|*.xlsx|Excel 97-2003文件|*.xls";
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            filename = sdfExport.FileName;
            string excelName = System.IO.Path.GetFileName(filename);//文件名

            this.txtSelectedFilePath.Text = excelName;

            string[] sheetnames = xiaomingCommom.getExcelSheetSum(filename);

            for (int t = 0; t < sheetnames.Length; t++)
            {
                this.cbSelectSheet.Items.Add(sheetnames[t]);//添加表名
            }
            MessageBox.Show("请选择工作表!");
            this.cbSelectSheet.DroppedDown = true;
            this.btnLoadExcel.Enabled = true;
        }

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.pgBar.Visible = true;
            this.pgBar.Value = 1;
            this.dgvExcels.DataSource = null;
            Application.DoEvents();
            loadExcel();
            this.pgBar.Value = 100;
            Cursor = Cursors.Default;
            this.btnLoadExcel.Enabled = false;
            this.btnSave.Enabled = true;
        }

        private void loadExcel()
        {
            this.dgvExcels.DataSource = null;
            this.table.Rows.Clear();
            this.sizeTable.Rows.Clear();
            if (this.txtSelectedFilePath.Text.Length <= 0)
            {
                MessageBox.Show("请选择文件！");
                Cursor = Cursors.Default;
                return;
            }
            if (this.cbSelectSheet.SelectedIndex == -1)
            {
                MessageBox.Show("请选择表名！");
                this.cbSelectSheet.DroppedDown = true;
                Cursor = Cursors.Default;
                return;
            }

            string sheetname = this.cbSelectSheet.SelectedItem.ToString();
            int headno = 0;

            this.dgvExcels.ReadOnly = true;
            this.dgvExcels.AllowUserToAddRows = false;
            UpdateUIDelegate += UpdataUIStatus;//绑定更新任务状态的委托
            TaskCallBack += Accomplish;//绑定完成任务要调用的委托
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerAsync();
            }
            this.fileNameStr = filename;
            this.sheeTnameStr = sheetname;
            this.headNoStr = headno;
        }

        public void UpdataUIStatus(bar barstr)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (bar s)
                {
                    this.pgBar.Maximum = s.maxstep;
                    this.pgBar.Value = s.step;
                    this.gbLoad.Text = s.str + "    " + s.step.ToString() + "/" + s.maxstep.ToString();
                }), barstr);
            }
            else
            {
                this.pgBar.Minimum = 0;
                this.pgBar.Maximum = barstr.maxstep;
                this.pgBar.Value = barstr.step;
                this.gbLoad.Text = barstr.str + "    " + barstr.step.ToString() + "/" + barstr.maxstep.ToString();
            }
        }

        //完成任务时需要调用
        private void Accomplish()
        {
            if (table != null)
            {
                this.dgvExcels.DataSource = table;
                changHeaderText();
                Cursor = Cursors.Default;
                this.dgvExcels.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                MessageBox.Show("加载完成");
            }
            TaskCallBack -= Accomplish; //取消侦听注册事件，避免多次侦听
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            barstr.str = "正在加载EXCEL...";
            barstr.step = 50;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);
            if (this.fileNameStr == "")
            {
                return;
            }
            if (this.sheeTnameStr == "")
            {
                return;
            }
            string org = "SAA";
            if (this.rbSAA.Checked)
            {
                org = "SAA";
            }
            if (this.rbTOP.Checked)
            {
                org = "TOP";
            }
            string headno = this.txtHeadNo.Text.Trim();
            if (headno.Length <= 0) headno = "0";
            List<DataTable> ldt = spm.ExcelRead(this.fileNameStr, this.sheeTnameStr, org, headno);

            this.table = ldt[0];
            this.sizeTable = ldt[1];
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            barstr.str = "加载完成";
            barstr.step = 100;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);
            this.dgvExcels.DataSource = "";
            this.dgvExcels.DataSource = this.table;
            Thread.Sleep(1000);
            this.gbLoad.Text = "导入条件";
            barstr.str = "导入条件";
            barstr.step = 0;
            barstr.maxstep = 100;
            UpdateUIDelegate(barstr);
            changHeaderText();
        }

        public void changHeaderText()
        {
            this.dgvExcels.Columns["id"].Visible = false;
            this.dgvExcels.Columns["isCancel"].HeaderText = "取消";
            this.dgvExcels.Columns["type"].HeaderText = "类型";
            this.dgvExcels.Columns["ftyNo"].HeaderText = "自编单号";
            this.dgvExcels.Columns["season"].HeaderText = "季节";
            this.dgvExcels.Columns["BVPO"].HeaderText = "BV验货 PO";
            this.dgvExcels.Columns["masterPO"].HeaderText = "Master PO";
            this.dgvExcels.Columns["GtnPO"].HeaderText = "Gtn PO";
            this.dgvExcels.Columns["Modify"].HeaderText = "旧 PO#";
            this.dgvExcels.Columns["po_mainLine"].HeaderText = "PO_MainLine";
            this.dgvExcels.Columns["styleNumber"].HeaderText = "款号";
            this.dgvExcels.Columns["styleName"].HeaderText = "Style Name ";

            this.dgvExcels.Columns["color"].HeaderText = " 顏色";
            this.dgvExcels.Columns["colDescription"].HeaderText = "電腦標縮寫";
            this.dgvExcels.Columns["channel"].HeaderText = "出貨地";
            this.dgvExcels.Columns["totalQty"].HeaderText = "總和";
            this.dgvExcels.Columns["overflow"].HeaderText = "溢出";
            this.dgvExcels.Columns["HOD"].HeaderText = "出货日";
            this.dgvExcels.Columns["befoeHOD"].HeaderText = "船务提前出货";
            this.dgvExcels.Columns["newHOD"].HeaderText = "报延新交期";
            this.dgvExcels.Columns["shipMode"].HeaderText = "出货方式";
            this.dgvExcels.Columns["sourceTag"].HeaderText = "防盜扣";

            this.dgvExcels.Columns["wwwt"].HeaderText = "WWMT 吊卡";
            this.dgvExcels.Columns["citHangTag"].HeaderText = "CIT吊卡";
            this.dgvExcels.Columns["Fastener"].HeaderText = "子彈";
            this.dgvExcels.Columns["steelNumber"].HeaderText = "钢板号";
            this.dgvExcels.Columns["cup"].HeaderText = "加罩杯";
            this.dgvExcels.Columns["cclable"].HeaderText = "洗標";
            this.dgvExcels.Columns["sensitive"].HeaderText = "敏感色";
            this.dgvExcels.Columns["remark"].HeaderText = "备注";
            this.dgvExcels.Columns["org"].HeaderText = "厂区";

            this.dgvExcels.Columns["HOD"].DefaultCellStyle.Format = "yyyy-MM-dd";
            this.dgvExcels.Columns["befoeHOD"].DefaultCellStyle.Format = "yyyy-MM-dd";
            this.dgvExcels.Columns["newHOD"].DefaultCellStyle.Format = "yyyy-MM-dd";
            this.dgvExcels.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
        }

        private void cbSelectSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnLoadExcel.Enabled = true;
        }

        private void FrmShippingPackages_Resize(object sender, EventArgs e)
        {
            this.tabControl1.Width = this.Width - 10;
            this.tabControl1.Height = this.Height - 45;

            this.tabPage1.Width = this.Width - 20;
            this.tabPage1.Height = this.Height - 20;
            this.groupBox2.Width = this.tabPage1.Width - 10;
            this.groupBox2.Height = this.tabPage1.Height - 75;

            this.gbLoad.Width = this.tabControl1.Width - 20;
            this.pgBar.Width = this.gbLoad.Width;
            this.pgBar.Height = 5;

            this.tabPage2.Width = this.Width - 20;
            this.tabPage2.Height = this.Height - 20;
            this.groupBox3.Width = this.tabPage1.Width - 10;
            this.groupBox3.Height = this.tabPage1.Height - 75;

            this.tabPage3.Width = this.Width - 20;
            this.tabPage3.Height = this.Height - 20;
            this.groupBox6.Width = this.tabPage1.Width - 10;
            this.groupBox6.Height = this.tabPage1.Height - 75;

            this.splitContainer2.Panel2Collapsed = true;
        }

        private void dgvExcels_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            if (this.selecteddgv == null)
            {
                return;
            }

            Clipboard.SetDataObject(this.selecteddgv.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            if (this.selecteddgv == null)
            {
                return;
            }

            Clipboard.SetDataObject(this.selecteddgv.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            if (this.selecteddgv == null)
            {
                return;
            }
            ImproExcel(this.selecteddgv);
        }

        public void ImproExcel(DataGridView dgv)
        {
            string sheetname = "";
            if (this.rbSAA3.Checked)
            {
                sheetname = "SAA-";
            }
            if (this.rbTOP3.Checked)
            {
                sheetname = "TOP-";
            }

            string starDate = dtpStartDate3.Value.ToString("yyyy-MM-dd");
            string stopDate = dtpStopDate3.Value.ToString("yyyy-MM-dd");
            sheetname = sheetname + starDate + "_" + stopDate;

            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.FileName = sheetname + ".xlsx";
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";

            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            NPOIShippingPackgeExcelHelper NPOIexcel = new NPOIShippingPackgeExcelHelper();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(dgv);

            // DataTable dt = (StyleCodeInfodataGridView.DataSource as DataTable);
            NPOIexcel.ExcelWrite(filename, tabl, sheetname);//excelhelper写出
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

        private void dgvExcels_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        public void tomenuRight(DataGridView dgv, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgv.Rows[e.RowIndex].Selected == false)
                    {
                        dgv.ClearSelection();
                        dgv.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgv.SelectedRows.Count == 1)
                    {
                        dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    MenuRight.Show(MousePosition.X, MousePosition.Y);
                    // MessageBox.Show("点右键了");
                }
                else if (e.ColumnIndex >= 0)
                {
                    this.hiedcolumnindex = e.ColumnIndex;
                    // MenuRight.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font fntTab;
            Brush bshBack;
            Brush bshFore;

            if (e.Index == this.tabControl1.SelectedIndex)
            {
                fntTab = e.Font;
                bshBack = new SolidBrush(Color.FromArgb(255, 102, 110));
                bshFore = new SolidBrush(Color.Black);
            }
            else
            {
                // fntTab = new Font(e.Font, FontStyle.Bold);
                fntTab = e.Font;
                bshBack = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, SystemColors.Control, SystemColors.Control, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
                bshFore = Brushes.Black;
            }

            string tabName = this.tabControl1.TabPages[e.Index].Text;
            StringFormat sftTab = new StringFormat();
            sftTab.Alignment = StringAlignment.Center;
            sftTab.LineAlignment = StringAlignment.Center;
            e.Graphics.FillRectangle(bshBack, e.Bounds);
            Rectangle recTab = e.Bounds;
            recTab = new Rectangle(recTab.X, recTab.Y + 4, recTab.Width, recTab.Height - 4);
            e.Graphics.DrawString(tabName, fntTab, bshFore, recTab, sftTab);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.dgvExcels.DataSource;
            if (dt == null || this.sizeTable.Rows.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if( dt.Rows[i]["masterPO"].ToString() == "#N/A" || dt.Rows[i]["masterPO"].ToString() == "" ||
                    dt.Rows[i]["GtnPO"].ToString() == "#N/A" || dt.Rows[i]["GtnPO"].ToString() == "" ||
                    dt.Rows[i]["styleNumber"].ToString() == "#N/A" || dt.Rows[i]["styleNumber"].ToString() == "" ||
                    dt.Rows[i]["totalQty"].ToString() == "#N/A" || dt.Rows[i]["totalQty"].ToString() == ""
                    )
                {
                    dt.Rows.RemoveAt(i);
                    i--;
                }
            }
            this.btnLoadExcel.Enabled = false;
            this.btnSave.Enabled = false;
            Cursor = Cursors.Default;
            //// 清空 T_SP_temp
            int clearResult = spm.clear_sp_temp();
            int clearSizeResult = spm.clear_spsize_temp();

            int insetSp_temp = spm.inset_sp_temp(dt);
            int insetSizeSp_temp = spm.inset_Sizesp_temp(this.sizeTable);

            // 更新 t_sp 表已有的数据
            int upresult = spm.updata_T_sp();
            int upresultisCancel = spm.updata_T_spIsCancel();

            int upSizeresult = spm.updata_T_spSize();

            // 插入 t_sp 表没有的数据
            int addresult = spm.adddata_T_sp();
            int addSizeResult = spm.adddata_T_spSize();

            // 清空 T_SP_temp
            clearResult = spm.clear_sp_temp();
            clearSizeResult = spm.clear_spsize_temp();

            // 清空上传有问题的行
            int clearSPError = spm.clear_spError();
            int clearSizeError = spm.clear_spsizeError();

            MessageBox.Show("@上传出货单到 T_SP 表成功.\r\n " +
                                "           更新 " + upresult.ToString() + " 条数据 \r\n " +
                                "           新增 " + addresult.ToString() + " 条数据  \r\n " +
                                "           修改 " + upresultisCancel.ToString() + " 条数据  \r\n " +
                            "@上传出货单到 T_size 表成功.\r\n " +
                                "           更新 " + upSizeresult.ToString() + " 条数据 \r\n " +
                                "           新增 " + addSizeResult.ToString() + " 条数据 \r\n ",
                              "保存成功");
            Cursor = Cursors.Default;
            this.btnLoadExcel.Enabled = true;
            this.btnSave.Enabled = true;
            MessageBox.Show("上传成功", "上传资料", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void butSearch2_Click(object sender, EventArgs e)
        {
            this.dgvBookingStatusOld.DataSource = null;
            this.dgvBookingStatusChanges.DataSource = null;
            this.dgvBookingStatusChanges.Refresh();
            this.dgvBookingStatusOld.Refresh();
            this.dgvBookingStatusChanges.Rows.Clear();
            string org = "";
            if (this.rbSAA2.Checked)
            {
                org = "SAA";
            }
            else if (this.rbTOP2.Checked)
            {
                org = "TOP";
            }
            bool checkedDate = this.cbData2.Checked;
            string startDate = this.dtpStartDate.Value.ToString("yyyy-MM-dd");
            string stopDate = this.dtpStopDate.Value.ToString("yyyy-MM-dd");
            bool bookingStatus = false;
            if (this.cbBookStatus2.SelectedIndex == 0)
            {
                bookingStatus = false;
            }
            else if (this.cbBookStatus2.SelectedIndex == 1)
            {
                bookingStatus = true;
            }
            string gtnPo = this.txtGtnPO2.Text.Trim().ToUpper();
            string styleNumber = this.txtStyleNumber2.Text.Trim().ToUpper();

            ShippingParameter sparameters = new ShippingParameter();
            sparameters.org = org;
            sparameters.checkedDate = checkedDate;
            sparameters.startDate = startDate;
            sparameters.stopDate = stopDate;
            sparameters.bookingStatus = bookingStatus;
            sparameters.gtnPo = gtnPo;
            sparameters.styleNumber = styleNumber;
            this.getShippingPackages(sparameters);
        }

        public void getShippingPackages(ShippingParameter sparameters)
        {
            shippingPackagesDT = spm.getShippingPackagesBySparameters(sparameters);
            this.dgvBookingStatusOld.DataSource = null;
            if (shippingPackagesDT.Rows.Count <= 0)
            {
                return;
            }
            BindingSource source = new BindingSource();
            source.DataSource = shippingPackagesDT;
            foreach (DataColumn col in this.shippingPackagesDT.Columns)
            {
                DataGridViewAutoFilterTextBoxColumn commonColumn = new DataGridViewAutoFilterTextBoxColumn();
                commonColumn.DataPropertyName = col.ColumnName;
                commonColumn.HeaderText = col.ColumnName;
                commonColumn.Resizable = DataGridViewTriState.True;
                this.dgvBookingStatusOld.Columns.Add(commonColumn);
            }
            this.dgvBookingStatusOld.DataSource = null;
            if (this.dgvBookingStatusOld.Columns.Count > 0)
            {
                this.dgvBookingStatusOld.Columns.RemoveAt(0);
            }
            this.dgvBookingStatusOld.DataSource = source;

            DataGridViewCheckBoxColumn dc = new DataGridViewCheckBoxColumn();
            dc.HeaderText = "选择";
            this.dgvBookingStatusOld.Columns.Insert(0, dc);
            this.changBookingHeaderText();
        }

        public void changBookingHeaderText()
        {
            this.dgvBookingStatusOld.Columns[1].HeaderText = "ID";
            this.dgvBookingStatusOld.Columns[2].HeaderText = "月BUY";
            this.dgvBookingStatusOld.Columns[3].HeaderText = "自编单号";
            this.dgvBookingStatusOld.Columns[4].HeaderText = "季节";
            this.dgvBookingStatusOld.Columns[5].HeaderText = "BV验货 PO#";
            this.dgvBookingStatusOld.Columns[6].HeaderText = "Master PO#";
            this.dgvBookingStatusOld.Columns[7].HeaderText = "Gtn PO#";
            this.dgvBookingStatusOld.Columns[8].HeaderText = "PO_MainLine";
            this.dgvBookingStatusOld.Columns[9].HeaderText = "Booking 状态";
            this.dgvBookingStatusOld.Columns[10].HeaderText = "booking 日期";
            this.dgvBookingStatusOld.Columns[11].HeaderText = "款号";
            this.dgvBookingStatusOld.Columns[12].HeaderText = "款式描述";
            this.dgvBookingStatusOld.Columns[13].HeaderText = "顏色";

            this.dgvBookingStatusOld.Columns[14].HeaderText = "電腦標縮寫";
            this.dgvBookingStatusOld.Columns[15].HeaderText = "出貨地";
            this.dgvBookingStatusOld.Columns[16].HeaderText = "總和";
            this.dgvBookingStatusOld.Columns[17].HeaderText = "溢出";
            this.dgvBookingStatusOld.Columns[18].HeaderText = "出货日";
            this.dgvBookingStatusOld.Columns[19].HeaderText = "船务提前出货";
            this.dgvBookingStatusOld.Columns[20].HeaderText = "报延新交期";
            this.dgvBookingStatusOld.Columns[21].HeaderText = "出货方式";
            this.dgvBookingStatusOld.Columns[22].HeaderText = "防盜扣";
            this.dgvBookingStatusOld.Columns[23].HeaderText = "WWMT 吊卡";

            this.dgvBookingStatusOld.Columns[24].HeaderText = "CIT 吊卡";
            this.dgvBookingStatusOld.Columns[25].HeaderText = "子彈";
            this.dgvBookingStatusOld.Columns[26].HeaderText = "钢板号";
            this.dgvBookingStatusOld.Columns[27].HeaderText = "加罩杯";
            this.dgvBookingStatusOld.Columns[28].HeaderText = "洗標";
            this.dgvBookingStatusOld.Columns[29].HeaderText = "敏感色";
            this.dgvBookingStatusOld.Columns[30].HeaderText = "备注";
            this.dgvBookingStatusOld.Columns[31].HeaderText = "厂区";

            this.dgvBookingStatusOld.Columns[32].HeaderText = "取消";
            this.dgvBookingStatusOld.Columns[32].Visible = false;
            this.dgvBookingStatusOld.Columns[33].HeaderText = "改 PO#";
            this.dgvBookingStatusOld.Columns[33].Visible = false;

            this.dgvBookingStatusOld.Columns[18].DefaultCellStyle.Format = "yyyy-MM-dd";
            this.dgvBookingStatusOld.Columns[19].DefaultCellStyle.Format = "yyyy-MM-dd";
            this.dgvBookingStatusOld.Columns[20].DefaultCellStyle.Format = "yyyy-MM-dd";

            for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
            {
                this.dgvBookingStatusOld.Rows[i].Cells[0].Value = false;
                if (this.dgvBookingStatusOld.Rows[i].Cells[9].Value.ToString() == "Yes")
                {
                    this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
                }
                else
                {
                    this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFED97");
                }
                this.dgvBookingStatusOld.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            }
            this.dgvBookingStatusOld.ReadOnly = false;
            for (int i = 1; i < this.dgvBookingStatusOld.Columns.Count; i++)
            {
                this.dgvBookingStatusOld.Columns[i].ReadOnly = true;
            }
        }

        private void butCurrentWeek_Click(object sender, EventArgs e)
        {
            this.LastWeeks = 0;
            betweenDate bdate = this.getDate(LastWeeks, 1);
            this.dtpStartDate.Value = bdate.start_time;
            this.dtpStopDate.Value = bdate.end_time;
            this.cbData2.Checked = true;
        }

        private void butLastWeek_Click(object sender, EventArgs e)
        {
            this.LastWeeks = LastWeeks + 1;
            betweenDate bdate = this.getDate(LastWeeks, 0);
            this.dtpStartDate.Value = bdate.start_time;
            this.dtpStopDate.Value = bdate.end_time;
            this.cbData2.Checked = true;
        }

        private void butNextWeek_Click(object sender, EventArgs e)
        {
            this.LastWeeks = LastWeeks - 1;
            betweenDate bdate = this.getDate(LastWeeks, 0);
            this.dtpStartDate.Value = bdate.start_time;
            this.dtpStopDate.Value = bdate.end_time;
            this.cbData2.Checked = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void dgvBookingStatusOld_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        private void dgvBookingStatusOld_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvBookingStatusChanges_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        private void dgvBookingStatusChanges_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void cbSelectALL_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbSelectALL.Checked)
            {
                for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
                {
                    this.dgvBookingStatusOld.Rows[i].Cells[0].Value = true;
                    this.dgvBookingStatusOld.Rows[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
                {
                    this.dgvBookingStatusOld.Rows[i].Cells[0].Value = false;
                    this.dgvBookingStatusOld.Rows[i].Selected = false;
                }
            }
        }

        private void butInvertSelection_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
            {
                if (this.dgvBookingStatusOld.Rows[i].Selected)
                {
                    this.dgvBookingStatusOld.Rows[i].Cells[0].Value = true;
                }
            }
        }

        private void rbBookingYes_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbBookingYes_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
            {
                if (this.dgvBookingStatusOld.Rows[i].Selected)
                {
                    this.dgvBookingStatusOld.Rows[i].Cells[0].Value = true;

                    this.dgvBookingStatusOld.Rows[i].Cells[9].Value = "Yes";
                    this.dgvBookingStatusOld.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
                    this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
                    this.dgvBookingStatusOld.Rows[i].Cells[10].Value = this.dtpBookingDate.Value.ToString("yyyy-MM-dd");
                }

                //  object dd = this.dgvBookingStatusOld.Rows[i].Cells[0].Value;
                //  string d = "";
                //   if (dd != null)
                //   {
                //      d = dd.ToString();
                //     }

                //   if (d == "True")
                //  {
                //    this.dgvBookingStatusOld.Rows[i].Cells[9].Value = "Yes";
                // }
                //  else
                //  {
                //      this.dgvBookingStatusOld.Rows[i].Cells[9].Value = "No";
                //  }

                //  if (this.dgvBookingStatusOld.Rows[i].Cells[9].Value.ToString() == "Yes")
                //  {
                //  this.dgvBookingStatusOld.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
                //   this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
                //   }
                //   else
                //{
                //   this.dgvBookingStatusOld.Rows[i].DefaultCellStyle.BackColor = this.dgvBookingStatusOld.RowsDefaultCellStyle.BackColor;
                //this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = Color.Red;  #FFED97
                //  this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFED97");
                // }
            }
        }

        private void rbBookingNo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
            {
                if (this.dgvBookingStatusOld.Rows[i].Selected)
                {
                    this.dgvBookingStatusOld.Rows[i].Cells[0].Value = false;

                    this.dgvBookingStatusOld.Rows[i].Cells[9].Value = "No";
                    this.dgvBookingStatusOld.Rows[i].DefaultCellStyle.BackColor = this.dgvBookingStatusOld.RowsDefaultCellStyle.BackColor;
                    this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFED97");
                    this.dgvBookingStatusOld.Rows[i].Cells[10].Value = "";
                }
            }
            /*
            for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
        {
            if (this.dgvBookingStatusOld.Rows[i].Selected)
            {
                this.dgvBookingStatusOld.Rows[i].Cells[0].Value = true;
            }

            object dd = this.dgvBookingStatusOld.Rows[i].Cells[0].Value;
            string d = "";
            if (dd != null)
            {
                d = dd.ToString();
            }

            if (d == "True")
            {
                this.dgvBookingStatusOld.Rows[i].Cells[9].Value = "No";
            }

            if (this.dgvBookingStatusOld.Rows[i].Cells[9].Value.ToString() == "Yes")
            {
                this.dgvBookingStatusOld.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
                this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
            }
            else
            {
                this.dgvBookingStatusOld.Rows[i].DefaultCellStyle.BackColor = this.dgvBookingStatusOld.RowsDefaultCellStyle.BackColor;
                //  this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = Color.Red;
                this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFED97");
            }
        }
            */
        }

        private void butBookingStatusChecked_Click(object sender, EventArgs e)
        {
            this.dgvBookingStatusChanges.Rows.Clear();

            if (this.rbBookingYes.Checked)
            {
                for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
                {
                    if (this.dgvBookingStatusOld.Rows[i].Selected)
                    {
                        this.dgvBookingStatusOld.Rows[i].Cells[0].Value = true;

                        this.dgvBookingStatusOld.Rows[i].Cells[9].Value = "Yes";
                        this.dgvBookingStatusOld.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
                        this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
                        this.dgvBookingStatusOld.Rows[i].Cells[10].Value = this.dtpBookingDate.Value.ToString("yyyy-MM-dd");
                    }
                }
            }
            else if (this.rbBookingNo.Checked)
            {
                for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
                {
                    if (this.dgvBookingStatusOld.Rows[i].Selected)
                    {
                        this.dgvBookingStatusOld.Rows[i].Cells[0].Value = true;
                        this.dgvBookingStatusOld.Rows[i].Cells[9].Value = "No";
                        this.dgvBookingStatusOld.Rows[i].DefaultCellStyle.BackColor = this.dgvBookingStatusOld.RowsDefaultCellStyle.BackColor;
                        this.dgvBookingStatusOld.Rows[i].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFED97");
                        this.dgvBookingStatusOld.Rows[i].Cells[10].Value = "";
                    }
                }
            }

            if (this.dgvBookingStatusChanges.Columns.Count <= 0)
            {
                for (int i = 0; i < this.dgvBookingStatusOld.Columns.Count; i++)
                {
                    DataGridViewCheckBoxColumn dc = new DataGridViewCheckBoxColumn();
                    dc.HeaderText = this.dgvBookingStatusOld.Columns[i].HeaderText;
                    this.dgvBookingStatusChanges.Columns.Add(dc);
                }
            }

            // this.dgvBookingStatusChanges.Columns.Clear();
            for (int i = 0; i < this.dgvBookingStatusOld.Rows.Count; i++)
            {
                object dd = this.dgvBookingStatusOld.Rows[i].Cells[0].Value;
                string d = "";
                if (dd != null)
                {
                    d = dd.ToString();
                }

                if (d == "True")
                {
                    DataGridViewRow clonedRow = (DataGridViewRow)this.dgvBookingStatusOld.Rows[i].Clone();
                    for (int index = 0; index < this.dgvBookingStatusOld.Rows[i].Cells.Count; index++)
                    {
                        // clonedRow.Cells[index].Value = this.dgvBookingStatusOld.Rows[i].Cells[index].Value;
                        clonedRow.Cells[index].Value = this.dgvBookingStatusOld.Rows[i].Cells[index].Value.ToString();
                    }
                    this.dgvBookingStatusChanges.Rows.Add(clonedRow);
                }
            }
            this.changBookingStatusChangeHeaderText();
            this.dgvBookingStatusChanges.Refresh();
            this.dgvBookingStatusChanges.ClearSelection();
        }

        public void changBookingStatusChangeHeaderText()
        {
            if (this.dgvBookingStatusChanges is null || this.dgvBookingStatusChanges.Rows.Count <= 0)
            {
                return;
            }

            this.dgvBookingStatusChanges.Columns[0].Visible = false;
            this.dgvBookingStatusChanges.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            this.dgvBookingStatusChanges.ReadOnly = true;
        }

        private void butSaveBooking2_Click(object sender, EventArgs e)
        {
            DataTable changedShippingBookingStatusDT = GetDgvToTable(this.dgvBookingStatusChanges);
            if (changedShippingBookingStatusDT.Rows.Count <= 0)
            {
                return;
            }
            spm.clear_T_Booking_temp();
            int i = 0;
            string T_Booking_temp = spm.SqlBulkToSQL_T_Booking_temp(changedShippingBookingStatusDT);
            if (T_Booking_temp == "200")
            {
                i = spm.updataShippingPackagesBySparameters();
                spm.clear_T_Booking_temp();
            }

            //  int i = spm.updataShippingPackagesBySparameters(changedShippingBookingStatusDT);
            MessageBox.Show("共更新 " + i.ToString() + "个PO 的 BOOKING STATUS,谢谢！", "保存完成");
        }

        private void txtHeadNo_TextChanged(object sender, EventArgs e)
        {
            this.btnLoadExcel.Enabled = true;
        }

        private void dgvPackageStatus_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        private void dgvPackageStatus_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void butCurrentWeek3_Click(object sender, EventArgs e)
        {
            this.LastWeeks = 0;
            betweenDate bdate = this.getDate(LastWeeks, 1);
            this.dtpStartDate3.Value = bdate.start_time;
            this.dtpStopDate3.Value = bdate.end_time;
            this.cbData3.Checked = true;
        }

        private void butLastWeek3_Click(object sender, EventArgs e)
        {
            this.LastWeeks = LastWeeks + 1;
            betweenDate bdate = this.getDate(LastWeeks, 0);
            this.dtpStartDate3.Value = bdate.start_time;
            this.dtpStopDate3.Value = bdate.end_time;
            this.cbData3.Checked = true;
        }

        private void butNextWeek3_Click(object sender, EventArgs e)
        {
            this.LastWeeks = LastWeeks - 1;
            betweenDate bdate = this.getDate(LastWeeks, 0);
            this.dtpStartDate3.Value = bdate.start_time;
            this.dtpStopDate3.Value = bdate.end_time;
            this.cbData3.Checked = true;
        }

        private void butSearch3_Click(object sender, EventArgs e)
        {
            string org = "";
            if (this.rbSAA3.Checked)
            {
                org = "SAA";
            }
            else if (this.rbTOP3.Checked)
            {
                org = "TOP";
            }
            bool checkedDate = this.cbData3.Checked;
            string startDate = this.dtpStartDate3.Value.ToString("yyyy-MM-dd");
            string stopDate = this.dtpStopDate3.Value.ToString("yyyy-MM-dd");
            bool bookingStatus = false;
            if (this.cbBookStatus3.SelectedIndex == 0)
            {
                bookingStatus = false;
            }
            else if (this.cbBookStatus3.SelectedIndex == 1)
            {
                bookingStatus = true;
            }
            string gtnPo = this.txtGtnPO3.Text.Trim().ToUpper();
            string styleNumber = this.txtStyleNumber3.Text.Trim().ToUpper();

            ShippingParameter sparameters = new ShippingParameter();
            sparameters.org = org;
            sparameters.checkedDate = checkedDate;
            sparameters.startDate = startDate;
            sparameters.stopDate = stopDate;
            sparameters.bookingStatus = bookingStatus;
            sparameters.gtnPo = gtnPo;
            sparameters.styleNumber = styleNumber;

            Cursor = Cursors.WaitCursor;
            this.getShippingPackagesStatus(sparameters);
            Cursor = Cursors.Default;
        }

        public void getShippingPackagesStatus(ShippingParameter sparameters)
        {
            shippingPackagesDT = spm.getShippingPackagesStatus(sparameters);
            this.dgvPackageStatus.DataSource = null;
            if (shippingPackagesDT is null || shippingPackagesDT.Rows.Count <= 0)
            {
                MessageBox.Show("没有成品扫描资料");
                return;
            }
            BindingSource source = new BindingSource();
            source.DataSource = shippingPackagesDT;
            foreach (DataColumn col in this.shippingPackagesDT.Columns)
            {
                DataGridViewAutoFilterTextBoxColumn commonColumn = new DataGridViewAutoFilterTextBoxColumn();
                commonColumn.DataPropertyName = col.ColumnName;
                commonColumn.HeaderText = col.ColumnName;
                commonColumn.Resizable = DataGridViewTriState.True;
                this.dgvPackageStatus.Columns.Add(commonColumn);
            }
            this.dgvPackageStatus.DataSource = null;
            if (this.dgvPackageStatus.Columns.Count > 0)
            {
                this.dgvPackageStatus.Columns.RemoveAt(0);
            }
            this.dgvPackageStatus.DataSource = source;
            this.changBookingStatusHeaderText();
        }

        public void changBookingStatusHeaderText()
        {
            this.dgvPackageStatus.Columns[0].HeaderText = "aid";
            this.dgvPackageStatus.Columns[1].HeaderText = "是否取消";
            this.dgvPackageStatus.Columns[2].HeaderText = "月BUY";
            this.dgvPackageStatus.Columns[3].HeaderText = "自编单号";
            this.dgvPackageStatus.Columns[4].HeaderText = "季节";

            this.dgvPackageStatus.Columns[5].HeaderText = "MasterPO";
            this.dgvPackageStatus.Columns[6].HeaderText = "GtnPO";
            this.dgvPackageStatus.Columns[7].HeaderText = "旧 PO#";
            this.dgvPackageStatus.Columns[8].HeaderText = "PO_MainLine";
            this.dgvPackageStatus.Columns[9].HeaderText = "出货日";
            this.dgvPackageStatus.Columns[10].HeaderText = "船务提前出货";
            this.dgvPackageStatus.Columns[11].HeaderText = "报延新交期";
            this.dgvPackageStatus.Columns[12].HeaderText = "BookingStatus";
            this.dgvPackageStatus.Columns[13].HeaderText = "Booking日期";

            this.dgvPackageStatus.Columns[14].HeaderText = "款号";
            this.dgvPackageStatus.Columns[15].HeaderText = " 颜色";
            this.dgvPackageStatus.Columns[16].HeaderText = "出货单件数";
            this.dgvPackageStatus.Columns[17].HeaderText = "溢出件数";
            this.dgvPackageStatus.Columns[18].HeaderText = "装箱单号码";
            this.dgvPackageStatus.Columns[19].HeaderText = "装箱单件数";
            this.dgvPackageStatus.Columns[20].HeaderText = "装箱单箱数";
            this.dgvPackageStatus.Columns[21].HeaderText = "已入库件数";
            this.dgvPackageStatus.Columns[22].HeaderText = "已入库箱数";

            this.dgvPackageStatus.Columns[23].HeaderText = "已出货件数";
            this.dgvPackageStatus.Columns[24].HeaderText = "已出货箱数";

            this.dgvPackageStatus.Columns[25].HeaderText = "未入库箱号";

            this.dgvPackageStatus.Columns[26].HeaderText = "入库比例";
            this.dgvPackageStatus.Columns[27].HeaderText = "出货比例";

            this.dgvPackageStatus.Columns[28].HeaderText = "BV验货PO";
            this.dgvPackageStatus.Columns[29].HeaderText = "款式描述";
            this.dgvPackageStatus.Columns[30].HeaderText = "電腦標縮寫";
            this.dgvPackageStatus.Columns[31].HeaderText = "出貨地";
            this.dgvPackageStatus.Columns[32].HeaderText = "出货方式";
            this.dgvPackageStatus.Columns[33].HeaderText = "防盜扣";

            this.dgvPackageStatus.Columns[34].HeaderText = "WWMT 吊卡";
            this.dgvPackageStatus.Columns[35].HeaderText = "CIT吊卡";
            this.dgvPackageStatus.Columns[36].HeaderText = "子彈";
            this.dgvPackageStatus.Columns[37].HeaderText = "钢板号";
            this.dgvPackageStatus.Columns[38].HeaderText = "加罩杯";
            this.dgvPackageStatus.Columns[39].HeaderText = "洗標";
            this.dgvPackageStatus.Columns[40].HeaderText = "敏感色";
            this.dgvPackageStatus.Columns[41].HeaderText = "备注";
            this.dgvPackageStatus.Columns[42].HeaderText = "厂区";

            /*******************************************************************/
            this.dgvPackageStatus.Columns[0].Name = "Aid";
            this.dgvPackageStatus.Columns[1].Name = "Is Cancel";
            this.dgvPackageStatus.Columns[2].Name = "Month BUY";
            this.dgvPackageStatus.Columns[3].Name = "Fty NO";
            this.dgvPackageStatus.Columns[4].Name = "Season";

            this.dgvPackageStatus.Columns[5].Name = "Master PO";
            this.dgvPackageStatus.Columns[6].Name = "Gtn PO";
            this.dgvPackageStatus.Columns[7].Name = "Old PO#";
            this.dgvPackageStatus.Columns[8].Name = "PO_Main Line";
            this.dgvPackageStatus.Columns[9].Name = "HOD";

            this.dgvPackageStatus.Columns[10].Name = "Befoe HOD";
            this.dgvPackageStatus.Columns[11].Name = "New HOD";
            this.dgvPackageStatus.Columns[12].Name = "Booking Status";
            this.dgvPackageStatus.Columns[13].Name = "Booking Data";
            this.dgvPackageStatus.Columns[14].Name = "Style Number ";
            this.dgvPackageStatus.Columns[15].Name = "Color";
            this.dgvPackageStatus.Columns[16].Name = "ShipPack Qty";
            this.dgvPackageStatus.Columns[17].Name = "Overflow Qty";
            this.dgvPackageStatus.Columns[18].Name = "PPRF No";
            this.dgvPackageStatus.Columns[19].Name = "PO Qty";
            this.dgvPackageStatus.Columns[20].Name = "PO Boxs";
            this.dgvPackageStatus.Columns[21].Name = "In Qty";
            this.dgvPackageStatus.Columns[22].Name = "In Boxs";

            this.dgvPackageStatus.Columns[23].Name = "CH Qty";
            this.dgvPackageStatus.Columns[24].Name = "CH Boxs";

            this.dgvPackageStatus.Columns[25].Name = "NoIn Con_no";

            this.dgvPackageStatus.Columns[26].Name = "CompletionRate";
            this.dgvPackageStatus.Columns[27].Name = "ShipmentRatio";

            this.dgvPackageStatus.Columns[28].Name = "BV PO";
            this.dgvPackageStatus.Columns[29].Name = "StyleName";
            this.dgvPackageStatus.Columns[30].Name = "ColDescription";
            this.dgvPackageStatus.Columns[31].Name = "Channel";
            this.dgvPackageStatus.Columns[32].Name = "Ship Mode";
            this.dgvPackageStatus.Columns[33].Name = "Source Tag";

            this.dgvPackageStatus.Columns[34].Name = "WWMT";
            this.dgvPackageStatus.Columns[35].Name = "CitHang Tag";
            this.dgvPackageStatus.Columns[36].Name = "Fastener";
            this.dgvPackageStatus.Columns[37].Name = "Steel Number";
            this.dgvPackageStatus.Columns[38].Name = "CUP";
            this.dgvPackageStatus.Columns[39].Name = "CC Lable";
            this.dgvPackageStatus.Columns[40].Name = "Sensitive";
            this.dgvPackageStatus.Columns[41].Name = "Remark";
            this.dgvPackageStatus.Columns[42].Name = "ORG";

            this.dgvPackageStatus.Columns[9].DefaultCellStyle.Format = "yyyy-MM-dd";
            this.dgvPackageStatus.Columns[10].DefaultCellStyle.Format = "yyyy-MM-dd";
            this.dgvPackageStatus.Columns[11].DefaultCellStyle.Format = "yyyy-MM-dd";
            this.dgvPackageStatus.Columns[13].DefaultCellStyle.Format = "yyyy-MM-dd";

            for (int i = 0; i < this.dgvPackageStatus.Rows.Count; i++)
            {
                if (this.dgvPackageStatus.Rows[i].Cells[12].Value.ToString() == "Yes")
                {
                  //  this.dgvPackageStatus.Rows[i].Cells[12].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#93FF93");
                }
                else
                {
                    this.dgvPackageStatus.Rows[i].Cells[12].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFED97");
                }

                string shipQty = this.dgvPackageStatus.Rows[i].Cells[16].Value.ToString(); // 16 +17
                string overQty = this.dgvPackageStatus.Rows[i].Cells[17].Value.ToString();
                string GtnQty = this.dgvPackageStatus.Rows[i].Cells[19].Value.ToString();
                //x < 0 ? y = 10 : z = 20;
                int sQty = shipQty.Length <= 0 ? 0 : Convert.ToInt32(shipQty);
                int oQty = overQty.Length <= 0 ? 0 : Convert.ToInt32(overQty);
                int gQty = GtnQty.Length <= 0 ? 0 : Convert.ToInt32(GtnQty);

                if ((sQty + oQty) == gQty)
                {

                }
                else
                {
                    this.dgvPackageStatus.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFED97");
                }


                string CompletionRate = this.dgvPackageStatus.Rows[i].Cells[26].Value.ToString();
                string ShipmentRatio = this.dgvPackageStatus.Rows[i].Cells[27].Value.ToString();
                double CRate = Convert.ToDouble(CompletionRate);
                double SRate = Convert.ToDouble(ShipmentRatio);
                DateTime nowdate = DateTime.Now;
                string HODateStr = this.dgvPackageStatus.Rows[i].Cells[9].Value.ToString();
                DateTime d1 = DateTime.Now;
                DateTime d2 = Convert.ToDateTime(HODateStr);

                TimeSpan d3 = d2.Subtract(d1);
                string DifferenceDate = d3.Days.ToString();
                if (Convert.ToInt32(DifferenceDate) <= 7 && CRate <= 80)
                {

                    this.dgvPackageStatus.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FE9600");
                }
                if (Convert.ToInt32(DifferenceDate) <= 2 && CRate <= 90)
                {

                    this.dgvPackageStatus.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FE0000");
                }
                this.dgvPackageStatus.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            }
        }

        private void dgvPackageStatus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string GtnPO = this.dgvPackageStatus[6, e.RowIndex].Value.ToString();
            string po_mainLine = this.dgvPackageStatus[8, e.RowIndex].Value.ToString();
            string styleNumber = this.dgvPackageStatus[14, e.RowIndex].Value.ToString();
            string color = this.dgvPackageStatus[15, e.RowIndex].Value.ToString();

            DataTable insResult = spm.getShippingPackagesSize(GtnPO, po_mainLine, styleNumber, color);
            this.dgvPackageSizes.DataSource = null;
            this.dgvPackageSizes.DataSource = insResult;
            this.splitContainer2.Panel2Collapsed = false;
            this.splitContainer2.SplitterDistance = Convert.ToInt32(this.splitContainer2.Height * 0.65);
            this.dgvPackageSizes.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
            //  chang_HeaderText();
        }

        private void dgvPackageSizes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        private void dgvPackageSizes_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvPackageStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.changBookingStatusHeaderText();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.selecteddgv = (DataGridView)sender;
            if (selecteddgv == null)
            {
                return;
            }

            this.tomenuRight(selecteddgv, e);
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvBookingStatusChanges_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            List<int> rowIndex = new List<int>();
            List<int> ids = new List<int>();

            for (int i = 0; i < this.dgvBookingStatusChanges.Rows.Count; i++)
            {
                if (this.dgvBookingStatusChanges.Rows[i].Selected)
                {
                    ids.Add(Convert.ToInt32(this.dgvBookingStatusChanges.Rows[i].Cells[1].Value.ToString()));
                    rowIndex.Add(i);
                }
            }

            for (int i = 0; i < rowIndex.Count; i++)
            {
                this.dgvBookingStatusChanges.Rows.RemoveAt(rowIndex[i] - i);
            }
            this.dgvBookingStatusChanges.ClearSelection();

            for (int i = 0; i < ids.Count; i++)
            {
                for (int j = 0; j < this.dgvBookingStatusOld.Rows.Count; j++)
                {
                    if (this.dgvBookingStatusOld.Rows[j].Cells[1].Value.ToString() == ids[i].ToString())
                    {
                        this.dgvBookingStatusOld.Rows[j].Cells[0].Value = false;
                        this.dgvBookingStatusOld.Rows[j].Cells[9].Value = "No";
                        this.dgvBookingStatusOld.Rows[j].DefaultCellStyle.BackColor = this.dgvBookingStatusOld.RowsDefaultCellStyle.BackColor;
                        this.dgvBookingStatusOld.Rows[j].Cells[9].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFED97");
                        this.dgvBookingStatusOld.Rows[j].Cells[10].Value = "";
                        this.dgvBookingStatusOld.Rows[j].Selected = false;
                    }
                }
            }
            this.dgvBookingStatusOld.Refresh();
            //  this.dgvBookingStatusChanges.Rows[0].Selected = false;
        }
    }
}