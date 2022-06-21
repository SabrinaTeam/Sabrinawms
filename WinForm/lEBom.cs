
using BLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm;

namespace WinForm
{
    public partial class lEBom : Form
    {

        private static lEBom frm;
        public IEBom iebomHead ;

        // public sizeRunManager sizem = new sizeRunManager();
        public DataGridView dgv = null;
        public IEBomManager iem = new IEBomManager();
        public MachinesTableManager mtm = new MachinesTableManager();
        DataTable machines = new DataTable();
        DataTable cureNamesDT = new DataTable();
        DataTable StandardModulus = new DataTable();
        //标准系数表ID
        public int CureNamesID = -1;
        public string ver = "0";
        public int verIndex = -1;
        public int hiedcolumnindex = -1; //是否选中外面
        public List<string> fileLists = null; // 图片服务器图片列表
        public  bool iSnewVer = false;
        public int GroupID = 0;
        public int selectVer = -1; // 是不是按查询
        public int changeGroupName = 0; // 是不是按查询
        public int changeStyleName = 0; // 是不是按查询
        public bool IsNewIEVer = false;

        private delegate void getLectraNumber();

        public lEBom()
        {
            InitializeComponent();
            dgvIETables.DoubleBufferedDataGirdView(true);

        }
        public static lEBom GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new lEBom();
            }
            return frm;
        }

        private void lEBom_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.splitContainer1.Enabled = false;
            //获取所有机器类型表
            this.machines = mtm.getAllMachineTypes();
            //获取标准系数表
            this.cureNamesDT = iem.getCureNames();
            if (this.cureNamesDT.Rows.Count > 0)
            {
                this.cbCureNames.Items.Clear();
                foreach (DataRow dr in this.cureNamesDT.Rows)
                {
                    this.cbCureNames.Items.Add(dr["modulusName"].ToString());

                }
                this.cbCureNames.SelectedIndex = 0;
                this.CureNamesID = Convert.ToInt32( this.cureNamesDT.Rows[0]["id"].ToString());
            }

            this.comboBox1.SelectedIndex = 0;



            //  获取已建好的款式表
            this.cbStyleSearch.Items.Clear();
            List<string> styles = iem.getAllStyles();
            if (styles == null || styles.Count <= 0)
            {
                return;
            }
            else
            {
                foreach (string style in styles)
                {
                    this.cbStyleSearch.Items.Add(style);
                }
                this.cbStyleSearch.SelectedIndex = 0;

            }

              Thread getPictureThread = new Thread(new ThreadStart(getPicture));
            getPictureThread.IsBackground = true;
            this.ckChangeGroup.Checked = false;
            this.txtNewGroupName.Enabled = false;
            this.cbGroupStyleName.Enabled = false;
            this.cbSameGroupStyles.Enabled = false;
            getPictureThread.Start();
            this.txtLectra.ReadOnly = true;
            this.cbStyleSearch.Focus();


        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dgvIETables.RowHeadersWidth - 4,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvIETables.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgvIETables.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);


        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rows= this.dgvIETables.Rows.Count;
            for (int i=1; i<=rows; i++)
            {
                this.dgvIETables.Rows[i-1].Cells[1].Value = (i).ToString();
            }
            this.dgvIETables.AllowUserToAddRows = true;
            int Rindex = e.RowIndex;
            string columnName = "partMachine";
            if (Rindex < 0) return;
            List<Machines> machinesList = new List<Machines>();
            DataGridViewComboBoxCell DgvCell = this.dgvIETables.Rows[e.RowIndex].Cells["partMachineTypeName"] as DataGridViewComboBoxCell;
            DgvCell.Items.Clear();
            if (this.machines.Rows.Count > 0)
            {
                for (int i = 0; i < this.machines.Rows.Count; i++)
                {
                    Machines ms = new Machines();
                    ms.id = Convert.ToInt32( this.machines.Rows[i]["ID"].ToString());
                    ms.machineClass = this.machines.Rows[i]["machineClass"].ToString();
                    ms.machineName = this.machines.Rows[i]["machineName"].ToString();
                    ms.machineTypeShortName = this.machines.Rows[i]["machineTypeShortName"].ToString();
                    ms.machinesMarckKhmer = this.machines.Rows[i]["machinesMarckKhmer"].ToString();
                    ms.imagestr = this.machines.Rows[i]["imagestr"].ToString();
                    ms.isMachinesStatus = Convert.ToInt32(this.machines.Rows[i]["isMachinesStatus"].ToString());
                    ms.CreateDate = Convert.ToDateTime(this.machines.Rows[i]["CreateDate"].ToString());
                    ms.Creator = this.machines.Rows[i]["Creator"].ToString();
                    ms.modify = Convert.ToDateTime(this.machines.Rows[i]["modify"].ToString());
                    ms.modifor = this.machines.Rows[i]["modifor"].ToString();
                    machinesList.Add(ms);
                    DgvCell.Items.Add(ms.machineName);
                }
            }
        }

        private void butExpandReplicate_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel1Collapsed)
            {
                butExpandReplicate.Text = "收起";
                this.splitContainer1.Panel1Collapsed = false;
            }
            else
            {
                butExpandReplicate.Text = "展开";
                this.splitContainer1.Panel1Collapsed = true;
                this.splitContainer1.SplitterDistance=415;
            }
        }

        private void butLearningCurveTable_Click(object sender, EventArgs e)
        {
            StyleLearningCurve frm = StyleLearningCurve.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }

        private void butMachinesTable_Click(object sender, EventArgs e)
        {
            FrmMachineTypeTable frm = FrmMachineTypeTable.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }

        private void lEBom_SizeChanged(object sender, EventArgs e)
        {
            dgvIETables.RowHeadersWidth = 30;    //设定左侧空列的宽度
                                                 // dataGridView1.RowHeadersVisible = false;   //隐藏空列，使不可见
            this.groupBox2.Left = Convert.ToInt32( (this.Width - this.groupBox2.Width - 10) / 2);
           // this.comboBox1.Left = this.groupBox1.Left;

            this.butSave.Left = this.groupBox2.Width  +this.groupBox2.Left - this.butSave.Width -20;
            this.butOk.Left = this.groupBox2.Width + this.groupBox2.Left - this.butSave.Width - this.butOk.Width - 40;
            this.butSave.Enabled = false;
            this.butCopyAddIETable.Enabled = false;
            this.butModifyIETable.Enabled = false;
            this.butDelIETable.Enabled = false;
          //  this.butPrint.Enabled = false;
            this.groupBox2.Height = this.Height - 100;
            this.splitContainer1.Height = this.groupBox2.Height - 80;
            this.butSave.Top = this.groupBox2.Height + 5;
            this.butOk.Top = this.butSave.Top;
            this.label5.Top = this.butSave.Top;
            this.label6.Top = this.butSave.Top;
            this.label7.Top = this.butSave.Top;
            this.label8.Top = this.butSave.Top;
            this.label9.Top = this.butSave.Top;

            this.labAverageSecond.Top = this.label5.Top + 20;
            this.labStandardSecond.Top = this.label5.Top + 20;
            this.labStandardHourproductionCapacity.Top = this.label5.Top + 20;
            this.labassignmentAllocate.Top = this.label5.Top + 20;
            this.labactualAllocate.Top = this.label5.Top + 20;
            this.label1.Top = this.label5.Top   + 10;


            //  this.gbSearch.Width = this.Width - 20;
            //  this.gbSize.Width = this.gbSearch.Width;
            //   this.gbPO.Width = this.gbSearch.Width;
            //   this.gbPO.Height = this.Height - 335;
            //   this.txtLogs.Visible = false;
        }

        private void butAddIETable_Click(object sender, EventArgs e)
        {
            this.initialization();
            this.IsNewIEVer = false;
            this.splitContainer1.Enabled = true;
            this.labCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.labLastModifyDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.labCreator.Text = Dns.GetHostName().ToUpper();
            this.dgvIETables.AllowUserToAddRows = true;
            this.butSave.Enabled = false;
            this.butCopyAddIETable.Enabled = false;
            this.butModifyIETable.Enabled = false;
            this.butDelIETable.Enabled = false;
            // this.butPrint.Enabled = false;

            this.ckChangeGroup.Checked = true;
            this.txtNewGroupName.Enabled = true;
            this.cbGroupStyleName.Enabled = true;
            this.cbSameGroupStyles.Enabled = true;
            this.txtStyleNumber.Focus();



        }


        private void txtStyleNumber_Validated(object sender, EventArgs e)
        {
            this.iSnewVer = true;

            string styleNumber = this.txtStyleNumber.Text.Trim();
            this.cbIEVersion.Items.Clear();
            if (styleNumber != "" )
            {
                List<IEVersions> styles = iem.getIEVersions(styleNumber);
                if (styles.Count != null)
                {
                    foreach (IEVersions style in styles)
                    {
                        this.cbIEVersion.Items.Add(style.VersionDisplay);

                    }
                    IEVersions s = new IEVersions();
                    s.VersionNumber = styles.Count;
                    s.VersionDisplay = "V" + styles.Count;
                    this.cbIEVersion.Items.Add(s.VersionDisplay);


                }
                else
                {
                    IEVersions ver = new IEVersions();
                        ver.VersionNumber = 0;
                        ver.VersionDisplay = "V0";

                    this.cbIEVersion.Items.Add(ver.VersionDisplay);

                }

            }
            string picturePath1 = "";
            string picturePath2 = "";
            if (this.fileLists != null)
            {
                string pictureName = this.txtStyleNumber.Text.Trim();
                foreach (string f in this.fileLists)
                {
                    if(f.IndexOf("A"+pictureName) > 0)
                    {
                        picturePath1 = f;
                        break;
                    }

                }
                foreach (string f in this.fileLists)
                {
                    if (f.IndexOf("B" + pictureName) > 0)
                    {
                        picturePath2 = f;
                        break;
                    }

                }
                //   var filePath = this.fileLists.Select(t=>t.IndexOf(pictureName)).ToList();

            }
            if (picturePath1 != "")
            {
                this.loadPicturel(picturePath1,1);
            }
            else
            {
                this.pbPicture1.Image = null;
            }
            if (picturePath2 != "")
            {
                this.loadPicturel(picturePath2,2);
            }
            else
            {
                this.pbPicture2.Image = null;

            }


            Thread getLectraThread = new Thread(new ThreadStart(setLectraNumber));
            getLectraThread.IsBackground = true;
            getLectraThread.Start();

            // this.cbIEVersion.DroppedDown = true;
            this.cbIEVersion.SelectedIndex = this.cbIEVersion.Items.Count - 1;
            this.cbIEVersion.Enabled = false; // 新增自动版本号
            this.txtStyleRemark.Focus();



        }
        int ccc = 0;

        public void setLectraNumber()
        {
            string LectraNumber = "";
            if (this.txtStyleNumber.InvokeRequired)
            {
                ccc = 0;
                  getLectraNumber cb = new getLectraNumber(setLectraNumber);
                  this.BeginInvoke( cb);

            }
            else
            {

                //this.cbStyleSearch.Text = this.txtStyleNumber.Text.Trim();

                string styleName = this.txtStyleNumber.Text.Trim();
                if (styleName == "") return;
                // //  this.txtLectra.Text = iem.getLectraNumber(styleName);
                // LectraNumber = this.txtLectra.Text.Trim();
                LectraNumber = iem.getLectraNumber(styleName);
                if (LectraNumber.Length <= 0 && ccc <= 0)
                {
                    ccc++;
                    this.txtLectra.Text = "";
                    this.txtStyleNumber.Focus();
                    MessageBox.Show("没有找到版号，请输入正确的款式", "款式号错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // return;
                }
                else
                {
                    this.txtLectra.Text = LectraNumber;

                }
            }

        }

        public void loadPicturel(string picPath,int picindex)
        {
            if (picPath.Length > 0)
            {
                if(picindex == 1)
                {
                    try
                    {

                        this.pbPicture1.Load(picPath);
                        this.pbPicture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        this.labPicture1.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }else if(picindex == 2)
                {
                    try
                    {

                        this.pbPicture2.Load(picPath);
                        this.pbPicture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        this.labPicture2.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }
        }

        private void pbPicture1_Click(object sender, EventArgs e)
        {
            string picPath = iem.uploadPicturel();
            this.loadPicturel(picPath,1);


        }


        private void pbPicture2_Click(object sender, EventArgs e)
        {

            string picPath = iem.uploadPicturel();
            this.loadPicturel(picPath,2);


        }
        public void calculate()
        {

            DataTable msDt = iem.GetDgvToTable(this.dgvIETables);
            if (msDt == null || msDt.Rows.Count <= 0)
            {
                return;
            }


            double countAverageSecond = 0.0;
            double countStandardSecond = 0.0;
            double countStandardHourproductionCapacity = 0.0;
            double countAssignmentAllocate = 0.0;
            string tempaver = "";
            double aver = 0.0;


           // int rowindex = e.RowIndex;
           // int colindex = e.ColumnIndex;
            double coefficient = Convert.ToDouble(this.txtStandardCoefficient.Text.Trim());
            double HourSecon = Convert.ToDouble(this.txtStandardHourProductionCapacity.Text.Trim());
            string standardHourproductionCapacity = "";


            for (int i = 0; i < msDt.Rows.Count; i++)
            {
                double average = 0.00;
                if (msDt.Rows[i]["averageSecond"].ToString() != null && msDt.Rows[i]["averageSecond"].ToString() != "")
                {
                    average = Convert.ToDouble(msDt.Rows[i]["averageSecond"].ToString());
                }


                msDt.Rows[i]["standardSecond"] = average * coefficient;

                //standardHourproductionCapacity  standardSecond
                standardHourproductionCapacity = Convert.ToString(HourSecon / Convert.ToDouble(msDt.Rows[i]["standardSecond"].ToString()));
                if (standardHourproductionCapacity == "∞")
                {
                    standardHourproductionCapacity = "0";
                }
                msDt.Rows[i]["standardHourproductionCapacity"] = standardHourproductionCapacity;

            }




            for (int i = 0; i < msDt.Rows.Count; i++)
            {
                tempaver = msDt.Rows[i]["averageSecond"].ToString();
                aver = Convert.ToDouble(tempaver == "" ? "0" : tempaver);
                countAverageSecond = countAverageSecond + aver;

                tempaver = msDt.Rows[i]["standardSecond"].ToString();
                aver = Convert.ToDouble(tempaver == "" ? "0" : tempaver);
                countStandardSecond = countStandardSecond + aver;

                tempaver = msDt.Rows[i]["standardHourproductionCapacity"].ToString();
                aver = Convert.ToDouble(tempaver == "" ? "0" : tempaver);
                countStandardHourproductionCapacity = countStandardHourproductionCapacity + aver;
            }

            double countAverage = Math.Round( countAverageSecond,0);
            double countStandard = Math.Round(countStandardSecond, 0);
            double countStandardHourProduction = Math.Round(countStandardHourproductionCapacity, 0);

            this.labAverageSecond.Text = countAverage.ToString();
            this.labStandardSecond.Text = countStandard.ToString();
            this.labStandardHourproductionCapacity.Text = countStandardHourProduction.ToString();

            //每人每小时产能(件)
            this.txtHourSingleMakes.Text = Convert.ToString(Convert.ToDouble(this.txtStandardHourProductionCapacity.Text) / Convert.ToDouble(this.labStandardSecond.Text));
            //每件所需时间(分钟)
            this.txtSinglePieceMakeTime.Text = Convert.ToString(Convert.ToDouble(this.labStandardSecond.Text) / 60);
            //全组每小时产能(件)
            string sewWorkman = this.txtSewWorkmanCount.Text;
            double mans = Convert.ToDouble(sewWorkman == "" ? "0" : sewWorkman);
            this.txtHourGroupMakes.Text = Convert.ToString(Convert.ToDouble(this.txtHourSingleMakes.Text) * mans);
            //8小时100%产能(件)
            this.txtEightMakePieces.Text = Convert.ToString(Convert.ToDouble(this.txtHourGroupMakes.Text) * 8);
            //10小时100%产能(件)
            this.txtTenMakePieces.Text = Convert.ToString(Convert.ToDouble(this.txtHourGroupMakes.Text) * 10);
            //节拍时间
            this.txtTaktTime.Text = Convert.ToString(3600 * 10 / Convert.ToDouble(this.txtTenMakePieces.Text));
            //难易级别y
            this.txtDifficultyy.Text =  Convert.ToString(Convert.ToDouble(this.txtSinglePieceMakeTime.Text) / 10);


            for (int i = 0; i < msDt.Rows.Count; i++)
            {
                tempaver = msDt.Rows[i]["standardSecond"].ToString();
                aver = Convert.ToDouble(tempaver == "" ? "0.0" : tempaver);
                if (aver == 0)
                {
                    msDt.Rows[i]["assignmentAllocate"] = 0.0;
                    this.dgvIETables.Rows[i].Cells["assignmentAllocate"].Value = 0.0;
                    continue;
                }
                double assignmen = Math.Round(aver / Convert.ToDouble(this.txtTaktTime.Text), 1);

                msDt.Rows[i]["assignmentAllocate"] = assignmen.ToString();
                this.dgvIETables.Rows[i].Cells["assignmentAllocate"].Value = msDt.Rows[i]["assignmentAllocate"].ToString();
              //  if (msDt.Rows[i]["assignmentAllocate"].ToString() == "∞")
               // {
                //    MessageBox.Show(msDt.Rows[i]["assignmentAllocate"].ToString());
               // }
            }

            for (int i = 0; i < msDt.Rows.Count; i++)
            {
                tempaver = msDt.Rows[i]["assignmentAllocate"].ToString();
                aver = Convert.ToDouble(Math.Round(Convert.ToDouble(tempaver == "" ? "0.0" : tempaver), 1, MidpointRounding.AwayFromZero));
                countAssignmentAllocate = countAssignmentAllocate + aver;

            }

            double Assignment = Math.Round( countAssignmentAllocate,1);
            this.labassignmentAllocate.Text = Assignment.ToString();

            //难易级别y
            double difficulty = Convert.ToDouble(this.txtSinglePieceMakeTime.Text);
            //难易级别x
            //取出标准系数表，对比难易级别y区间  取出 Y 值  取出产能
            int NewOrOld = this.comboBox1.SelectedIndex;
            this.StandardModulus = iem.getStandardModulus(this.CureNamesID, NewOrOld);
            double ratio = 0;
            if (this.StandardModulus != null && this.StandardModulus.Rows.Count > 0)
            {
                foreach (DataRow dr in this.StandardModulus.Rows)
                {
                    int min = Convert.ToInt32(dr["CArea"].ToString().Split('-')[0]);
                    int max = Convert.ToInt32(dr["CArea"].ToString().Split('-')[1]);
                    if (min-1 <= difficulty && difficulty <= max)
                    {
                        this.txtDifficultyx.Text = dr["Clevel"].ToString();
                        ratio = Convert.ToDouble(dr["COneday"].ToString() == "" ? "1" : dr["COneday"].ToString());
                        this.txt1day.Text = Convert.ToString(ratio * 100);
                        this.txt1Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt1Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CTwoDay"].ToString() == "" ? "1" : dr["CTwoDay"].ToString());
                        this.txt2day.Text = Convert.ToString(ratio * 100);
                        this.txt2Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt2Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CThreeDay"].ToString() == "" ? "1" : dr["CThreeDay"].ToString());
                        this.txt3day.Text = Convert.ToString(ratio * 100);
                        this.txt3Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt3Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));

                        ratio = Convert.ToDouble(dr["CFourDay"].ToString() == "" ? "1" : dr["CFourDay"].ToString());
                        this.txt4day.Text = Convert.ToString(ratio * 100);
                        this.txt4Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt4Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));

                        ratio = Convert.ToDouble(dr["CFiveDay"].ToString() == "" ? "1" : dr["CFiveDay"].ToString());
                        this.txt5day.Text = Convert.ToString(ratio * 100);
                        this.txt5Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt5Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));

                        ratio = Convert.ToDouble(dr["CSixDay"].ToString() == "" ? "1" : dr["CSixDay"].ToString());
                        this.txt6day.Text = Convert.ToString(ratio * 100);
                        this.txt6Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt6Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CSevenDay"].ToString() == "" ? "1" : dr["CSevenDay"].ToString());
                        this.txt7day.Text = Convert.ToString(ratio * 100);
                        this.txt7Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt7Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CEightDay"].ToString() == "" ? "1" : dr["CEightDay"].ToString());
                        this.txt8day.Text = Convert.ToString(ratio * 100);
                        this.txt8Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt8Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CNineDay"].ToString() == "" ? "1" : dr["CNineDay"].ToString());
                        this.txt9day.Text = Convert.ToString(ratio * 100);
                        this.txt9Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt9Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CTenDay"].ToString() == "" ? "1" : dr["CTenDay"].ToString());
                        this.txt10day.Text = Convert.ToString(ratio * 100);
                        this.txt10Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt10Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CElevenDay"].ToString() == "" ? "1" : dr["CElevenDay"].ToString());
                        this.txt11day.Text = Convert.ToString(ratio * 100);
                        this.txt11Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt11Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CTwelveDay"].ToString() == "" ? "1" : dr["CTwelveDay"].ToString());
                        this.txt12day.Text = Convert.ToString(ratio * 100);
                        this.txt12Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt12Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CThirteenDay"].ToString() == "" ? "1" : dr["CThirteenDay"].ToString());
                        this.txt13day.Text = Convert.ToString(ratio * 100);
                        this.txt13Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt13Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(dr["CFourteenDay"].ToString() == "" ? "1" : dr["CFourteenDay"].ToString());
                        this.txt14day.Text = Convert.ToString(ratio * 100);
                        this.txt14Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt14Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));

                    }
                    else if(difficulty > Convert.ToInt32(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CArea"].ToString().Split('-')[1]))
                    {
                        this.txtDifficultyx.Text = this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["Clevel"].ToString();
                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["COneday"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["COneday"].ToString());
                        this.txt1day.Text = Convert.ToString(ratio * 100);
                        this.txt1Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt1Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CTwoDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CTwoDay"].ToString());
                        this.txt2day.Text = Convert.ToString(ratio * 100);
                        this.txt2Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt2Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CThreeDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CThreeDay"].ToString());
                        this.txt3day.Text = Convert.ToString(ratio * 100);
                        this.txt3Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt3Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));

                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CFourDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CFourDay"].ToString());
                        this.txt4day.Text = Convert.ToString(ratio * 100);
                        this.txt4Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt4Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));

                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CFiveDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CFiveDay"].ToString());
                        this.txt5day.Text = Convert.ToString(ratio * 100);
                        this.txt5Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt5Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));

                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CSixDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CSixDay"].ToString());
                        this.txt6day.Text = Convert.ToString(ratio * 100);
                        this.txt6Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt6Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CSevenDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CSevenDay"].ToString());
                        this.txt7day.Text = Convert.ToString(ratio * 100);
                        this.txt7Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt7Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CEightDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CEightDay"].ToString());
                        this.txt8day.Text = Convert.ToString(ratio * 100);
                        this.txt8Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt8Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CNineDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CNineDay"].ToString());
                        this.txt9day.Text = Convert.ToString(ratio * 100);
                        this.txt9Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt9Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CTenDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CTenDay"].ToString());
                        this.txt10day.Text = Convert.ToString(ratio * 100);
                        this.txt10Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt10Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CElevenDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CElevenDay"].ToString());
                        this.txt11day.Text = Convert.ToString(ratio * 100);
                        this.txt11Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt11Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CTwelveDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CTwelveDay"].ToString());
                        this.txt12day.Text = Convert.ToString(ratio * 100);
                        this.txt12Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt12Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CThirteenDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CThirteenDay"].ToString());
                        this.txt13day.Text = Convert.ToString(ratio * 100);
                        this.txt13Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt13Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));


                        ratio = Convert.ToDouble(this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CFourteenDay"].ToString() == "" ? "1" : this.StandardModulus.Rows[this.StandardModulus.Rows.Count - 1]["CFourteenDay"].ToString());
                        this.txt14day.Text = Convert.ToString(ratio * 100);
                        this.txt14Day8HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));
                        this.txt14Day10HourMakes.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text) * ratio, 0, MidpointRounding.AwayFromZero));



                    }

                }
            }

            bool completeData = true;

            for (int i = 0; i < this.dgvIETables.RowCount; i++)
            {
                if (this.dgvIETables.Rows[i].Cells["Scope"].Value == null || this.dgvIETables.Rows[i].Cells["Scope"].Value.ToString() == "")
                {
                    completeData = false;
                }
                if (this.dgvIETables.Rows[i].Cells["partNumber"].Value == null ||  this.dgvIETables.Rows[i].Cells["partNumber"].Value.ToString() == "")
                {
                    completeData = false;
                }
                if (this.dgvIETables.Rows[i].Cells["partName"].Value == null ||  this.dgvIETables.Rows[i].Cells["partName"].Value.ToString() == "")
                {
                    completeData = false;
                }
                if (this.dgvIETables.Rows[i].Cells["partMachineTypeName"].Value == null ||  this.dgvIETables.Rows[i].Cells["partMachineTypeName"].Value.ToString() == "")
                {
                    completeData = false;
                }

                // 重要工段
                if (this.dgvIETables.Rows[i].Cells["importantPart"].Value == null || this.dgvIETables.Rows[i].Cells["importantPart"].Value.ToString() == "")
                {
                    this.dgvIETables.Rows[i].Cells["importantPart"].Value = 0;
                }

                //工段备注 partRemark

                //平均时长(秒)
                if (this.dgvIETables.Rows[i].Cells["averageSecond"].Value == null || this.dgvIETables.Rows[i].Cells["averageSecond"].Value.ToString() == "" || this.dgvIETables.Rows[i].Cells["averageSecond"].Value.ToString() == "∞")
                {
                    this.dgvIETables.Rows[i].Cells["averageSecond"].Value = 0;
                }

                //标准时长(秒)
                if (this.dgvIETables.Rows[i].Cells["standardSecond"].Value == null || this.dgvIETables.Rows[i].Cells["standardSecond"].Value.ToString() == "" || this.dgvIETables.Rows[i].Cells["standardSecond"].Value.ToString() == "∞")
                {
                    this.dgvIETables.Rows[i].Cells["standardSecond"].Value = 0;
                }

                //标准时产能(件)
                if (this.dgvIETables.Rows[i].Cells["standardHourproductionCapacity"].Value == null || this.dgvIETables.Rows[i].Cells["standardHourproductionCapacity"].Value.ToString() == "" || this.dgvIETables.Rows[i].Cells["standardHourproductionCapacity"].Value.ToString() == "∞")
                {
                    this.dgvIETables.Rows[i].Cells["standardHourproductionCapacity"].Value = 0;
                }

                //作业分配(人)
                if (this.dgvIETables.Rows[i].Cells["assignmentAllocate"].Value == null || this.dgvIETables.Rows[i].Cells["assignmentAllocate"].Value.ToString() == "" || this.dgvIETables.Rows[i].Cells["assignmentAllocate"].Value.ToString() == "∞")
                {
                    this.dgvIETables.Rows[i].Cells["assignmentAllocate"].Value = 0.0;
                }

                //实际配置(人)
                if (this.dgvIETables.Rows[i].Cells["actualAllocate"].Value == null || this.dgvIETables.Rows[i].Cells["actualAllocate"].Value.ToString() == "" || this.dgvIETables.Rows[i].Cells["actualAllocate"].Value.ToString() == "∞")
                {
                    this.dgvIETables.Rows[i].Cells["actualAllocate"].Value = 0.0;
                }
                //备注 remark
            }
            GroupID = 0;
            //获取群组名称的 groupID
            //如果获取失败 那就获取最大的 groupID ，返加 MAX groupID + 1
            if(this.cbGroupStyleName.Items.Count >0 && this.cbGroupStyleName.SelectedIndex > -1)
            {
                string GroupStyleName = this.cbGroupStyleName.SelectedItem.ToString();
                if (GroupStyleName != "")
                {
                    GroupID = iem.getGroupIDByGroupStyleName(GroupStyleName);
                }

            }
            if (GroupID == 0)
            {
                GroupID = iem.getGroupMaxID();
                GroupID = GroupID + 1;

            }




            if (!completeData)
           {

            //    MessageBox.Show("部件，工段名称，机器 为必填项", "资料不完整");
                return;
            }

            this.butSave.Enabled = true;

            this.changHeaderText();
            this.dgvIETables.Refresh();



        }
        public void changHeaderText()
        {
            this.dgvIETables.Columns["standardHourproductionCapacity"].DefaultCellStyle.Format = "0";
            this.dgvIETables.Columns["assignmentAllocate"].DefaultCellStyle.Format = "0.0";
            this.dgvIETables.Columns["actualAllocate"].DefaultCellStyle.Format = "0.0";

            double Difficulty = Math.Round(Convert.ToDouble(this.txtDifficultyy.Text), 2);
            this.txtDifficultyy.Text = Difficulty.ToString();

            double TaktTime = Math.Round(Convert.ToDouble(this.txtTaktTime.Text), 2);
            this.txtTaktTime.Text = TaktTime.ToString();

            double SinglePiece = Math.Round(Convert.ToDouble(this.txtSinglePieceMakeTime.Text), 2);
            this.txtSinglePieceMakeTime.Text = SinglePiece.ToString();

            double HourSingle = Math.Round(Convert.ToDouble(this.txtHourSingleMakes.Text), 2);
            this.txtHourSingleMakes.Text = HourSingle.ToString();

            double HourGroup = Math.Round(Convert.ToDouble(this.txtHourGroupMakes.Text), 2);
            this.txtHourGroupMakes.Text = HourGroup.ToString();


            double EightMake = Math.Round(Convert.ToDouble(this.txtEightMakePieces.Text), 0);
            this.txtEightMakePieces.Text = EightMake.ToString();

            double TenMake = Math.Round(Convert.ToDouble(this.txtTenMakePieces.Text), 0);
            this.txtTenMakePieces.Text = TenMake.ToString();





        }
        private void butOk_Click(object sender, EventArgs e)
        {
            this.calculate();
        }

        private void dgvIETables_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvIETables.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && e.RowIndex != -1)
            {
                SendKeys.Send("{F4}");
            }
        }


        ComboBox cbo = new ComboBox();
        public DataGridViewTextBoxEditingControl CellEdit = null;
        private void dgvIETables_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvIETables.CurrentCell.RowIndex != -1)
            {
                if (dgvIETables.CurrentCell.OwningColumn.Name == "Scope" || dgvIETables.CurrentCell.OwningColumn.Name == "partMachine")
                {
                    cbo = e.Control as ComboBox; //保存当前的事件源。为了触发事件后。在取消
                    if(cbo != null)
                    {
                        cbo.SelectedIndexChanged += new EventHandler(cbo_SelectedIndexChanged);
                    }

                }

                if (dgvIETables.CurrentCell.OwningColumn.Name == "averageSecond")
                {
                    CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
                    CellEdit.SelectAll();
                    CellEdit.KeyPress += Cells_KeyPress; //绑定事件
                }

            }
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e) //自定义事件
        {
            if (dgvIETables.CurrentCell.OwningColumn.Name == "averageSecond")
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
                if (e.KeyChar == '\b') e.Handled = false;

               // if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != '\b')
               //     e.Handled = true;
            }
        }


        void cbo_SelectedIndexChanged(object sender, EventArgs e)
            {
                ComboBox combox = sender as ComboBox;
                //这里就可以写。触发后是逻辑代码
              //  MessageBox.Show(combox.Text);

                //combox.Text和cbo.Text获取的值是相同的

                //做完处理，须撤销动态事件。如果不撤销会遇到什么问题。你可以自己试试
                cbo.SelectedIndexChanged -= new EventHandler(cbo_SelectedIndexChanged);
                //或者
                combox.SelectedIndexChanged -= new EventHandler(cbo_SelectedIndexChanged);
            }

        private void dgvIETables_Validated(object sender, EventArgs e)
        {
            this.dgvIETables.AllowUserToAddRows = false;
            this.ChangeParameters();
        }

        private void dgvIETables_Validating(object sender, CancelEventArgs e)
        {
            this.dgvIETables.AllowUserToAddRows = true;
        }

        private void dgvIETables_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            int colindex = e.ColumnIndex;
            double coefficient = Convert.ToDouble( this.txtStandardCoefficient.Text.Trim());
            double HourSecon = Convert.ToDouble( this.txtStandardHourProductionCapacity.Text.Trim() );
            string standardHourproductionCapacity = "";
            if (rowindex == -1) return;




            for (int i = 0; i < dgvIETables.Columns.Count; i++)
            {
                if (dgvIETables.Columns[colindex].Name.Equals("averageSecond"))
                {
                    double average = 0.00;
                    if(dgvIETables.Rows[rowindex].Cells[colindex].Value != null && dgvIETables.Rows[rowindex].Cells[colindex].Value.ToString() != "")
                    {
                        average = Convert.ToDouble(dgvIETables.Rows[rowindex].Cells[colindex].Value);
                    }


                    dgvIETables.Rows[rowindex].Cells[colindex + 1].Value = average  * coefficient;

                    standardHourproductionCapacity = Convert.ToString( HourSecon / Convert.ToDouble(dgvIETables.Rows[rowindex].Cells[colindex + 1].Value));

                    if(standardHourproductionCapacity == "∞")
                    {
                        standardHourproductionCapacity = "0";
                    }
                    dgvIETables.Rows[rowindex].Cells[colindex + 2].Value = standardHourproductionCapacity;
                }
            }
          //  MessageBox.Show("1");
        }

        private void dgvIETables_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {
            MessageBox.Show("11");
        }

        private void cbCureNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string  CureNames = this.cbCureNames.SelectedItem.ToString();
            var row = this.cureNamesDT.Select("modulusName = '" + CureNames + "'");
            this.CureNamesID = Convert.ToInt32(row[0]["id"].ToString());


        }

        private void txtStandardCoefficient_TextChanged(object sender, EventArgs e)
        {
            this.ChangeParameters();
        }

        private void txtStandardHourProductionCapacity_TextChanged(object sender, EventArgs e)
        {
            this.ChangeParameters();
        }
        public void ChangeParameters()
        {
            string coefficientstr = this.txtStandardCoefficient.Text.Trim();
            string HourSeconstr = this.txtStandardHourProductionCapacity.Text.Trim();
            if (coefficientstr.Length == 0 || HourSeconstr.Length == 0) return;
            double coefficient = Convert.ToDouble(coefficientstr);
            double HourSecon = Convert.ToDouble(HourSeconstr);
            for (int i = 0; i < this.dgvIETables.RowCount; i++)
            {
                for (int j = 0; j < dgvIETables.Columns.Count; j++)
                {
                    if (dgvIETables.Columns[j].Name.Equals("averageSecond"))
                    {
                        double average = 0.00;
                        if (dgvIETables.Rows[i].Cells[j].Value != null && dgvIETables.Rows[i].Cells[j].Value.ToString() != "")
                        {
                            average =   Convert.ToDouble(dgvIETables.Rows[i].Cells[j].Value);
                        }
                        dgvIETables.Rows[i].Cells[j + 1].Value = average * coefficient;
                        dgvIETables.Rows[i].Cells[j + 2].Value = HourSecon / (average * coefficient);
                    }
                }
            }
            this.calculate();
        }

        private void txtSewWorkmanCount_TextChanged(object sender, EventArgs e)
        {
            this.ChangeParameters();
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            string StyleNumber = this.txtStyleNumber.Text.Trim();
            string lectra = this.txtLectra.Text.Trim();
            string IEVersion = this.cbIEVersion.Text.Trim();
            string curenames = this.cbCureNames.SelectedItem.ToString();
            string Workman = this.txtSewWorkmanCount.Text.Trim();

            if (StyleNumber.Length <=0 || lectra.Length <= 0 || IEVersion.Length <= 0 || curenames.Length <= 0  || Workman.Length <= 0)
            {
                string message = "       款式名 \r\n  或 力克版号  \r\n  或 IE版本号   \r\n  或 标准系数表   \r\n  或  车缝人数   \r\n  全部不能为空";
                string caption = "错误";
                MessageBox.Show(message, caption,
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                return;
            }


            iebomHead = new IEBom(); // 基本信息表


            iebomHead.IEBomName =this.txtStyleNumber.Text.Trim(); //Bom名称
            iebomHead.IEBomStyleName = this.txtStyleNumber.Text.Trim(); //款式名
            iebomHead.LectraNumber = this.txtLectra.Text.Trim(); //力克版号

           // string IEVersion = this.cbIEVersion.SelectedItem.ToString();
            IEVersion = IEVersion.Substring(1, IEVersion.Length - 1);
            iebomHead.IEBomVersion = IEVersion; //Bom款式版本号


             int id = iem.getIEBomByStyleVer(iebomHead.IEBomStyleName, iebomHead.IEBomVersion);
            if(id > -1)
            {
                string message = "本版本已保存，是否保存更新版本?";
                string title = "版本已存在";
                DialogResult result = MessageBox.Show(message, title,MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // 跳到更新的代码处
                    this.updataIEBom(id);
                    return;
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }

            }

            //本版本已保存，是否保存修订版本

            iebomHead.IEBomCreateDate = this.labCreateDate.Text.Trim(); // BOM 创建日期
            iebomHead.IEBomLastModifyDate = this.labLastModifyDate.Text.Trim(); // BOM 最后修改日期
            iebomHead.IEBomCreator = this.labCreator.Text.Trim(); // BOM 创建人

            string IEBomBodyID = this.txtStyleNumber.Text.Trim() + this.cbIEVersion.SelectedItem.ToString();
            //txtStyleNumber +  cbIEVersion
            iebomHead.IEBomModifyHistoryNumber = IEBomBodyID; // ! 修改历史记录ID编号
            iebomHead.IEBomProcessNumber = IEBomBodyID; // ! 工序表ID
            iebomHead.IEBomRatioID = IEBomBodyID; // ! IE系数表ID
            System.Drawing.Image MainImg = this.pbPicture1.Image;
            System.Drawing.Image ReverseImg = this.pbPicture2.Image;
            iebomHead.MainPicture = this.ImgToByt(MainImg); // ! 正面图
            iebomHead.ReversePicture = this.ImgToByt(ReverseImg); // ! 背面图

            iebomHead.StyleRemark = this.txtStyleRemark.Text.Trim(); // 款号描述
            iebomHead.MakeGroup = this.txgMakeGroup.Text.Trim(); // 生产组别
            iebomHead.TaktTime = Convert.ToDouble(this.txtTaktTime.Text.Trim()); // 节拍时间
            iebomHead.SinglePieceMakeTime = Convert.ToDouble(this.txtSinglePieceMakeTime.Text.Trim()); // 单件时间
            iebomHead.HourSingleMakes = Convert.ToDouble(this.txtHourSingleMakes.Text.Trim()); // 时产能-单人
            iebomHead.HourGroupMakes = Convert.ToDouble(this.txtHourGroupMakes.Text.Trim()); // 时产能-小组
            iebomHead.SewWorkmanCount = Convert.ToInt32(this.txtSewWorkmanCount.Text.Trim()); // 车缝人数
            iebomHead.EightMakePieces = Convert.ToDouble(this.txtEightMakePieces.Text.Trim()); // 8小时产能-组
            iebomHead.TenMakePieces = Convert.ToDouble( this.txtTenMakePieces.Text.Trim()); // 10小时产能-组

            iebomHead.Season = this.txtSeason.Text.Trim(); // 季节
            iebomHead.Difficultyx = this.txtDifficultyx.Text.Trim(); // 难易级别 字母
            iebomHead.Difficultyy = Convert.ToDouble(this.txtDifficultyy.Text.Trim()); // 难易级别 数字
            iebomHead.StandardCoefficient = Convert.ToDouble(this.txtStandardCoefficient.Text.Trim()); // 宽放系数
            iebomHead.StandardHourProductionCapacity = Convert.ToInt32(this.txtStandardHourProductionCapacity.Text.Trim()); // 标准时秒数
            iebomHead.CureNames = this.cbCureNames.Text.Trim(); // 标准系数表
            iebomHead.GroupID = GroupID; // 群组ID



            List <IEBomProces>    iebomProcesTables = new List<IEBomProces>();//工段表
            for (int i = 0; i < this.dgvIETables.Rows.Count; i++)
            {
                IEBomProces iebomProcesTable = new IEBomProces();
                iebomProcesTable.ProcessNumber = IEBomBodyID; // IE表对应头ID
                iebomProcesTable.Scope = Convert.ToString(this.dgvIETables.Rows[i].Cells["Scope"].Value); // !部件
                iebomProcesTable.partNumber = Convert.ToInt32( this.dgvIETables.Rows[i].Cells["partNumber"].Value.ToString()); // !工段号
                iebomProcesTable.partName = Convert.ToString(this.dgvIETables.Rows[i].Cells["partName"].Value); // !工段名称
                string importantPart = this.dgvIETables.Rows[i].Cells["importantPart"].Value.ToString();
                if(importantPart == "true")
                {
                    importantPart = "1";
                }
                else
                {
                    importantPart = "0";
                }
                iebomProcesTable.importantPart = Convert.ToInt32(importantPart); // 重要工段
                iebomProcesTable.partRemark = Convert.ToString( this.dgvIETables.Rows[i].Cells["partRemark"].Value); // 工段备注
                iebomProcesTable.partMachineTypeID = Convert.ToInt32(this.dgvIETables.Rows[i].Cells["partMachineTypeID"].Value.ToString()); // !机器ID号
                iebomProcesTable.partMachineTypeName = Convert.ToString(this.dgvIETables.Rows[i].Cells["partMachineTypeName"].Value); // !机器名称
                iebomProcesTable.averageSecond = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["averageSecond"].Value.ToString()); //平均时长(秒)
                iebomProcesTable.standardSecond = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["standardSecond"].Value.ToString()); //标准时长(秒)
                iebomProcesTable.standardHourproductionCapacity = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["standardHourproductionCapacity"].Value.ToString()); //标准时产能(件)
                iebomProcesTable.assignmentAllocate = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["assignmentAllocate"].Value.ToString()); //作业分配(人)
                iebomProcesTable.actualAllocate = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["actualAllocate"].Value.ToString()); //实际配置(人)
                iebomProcesTable.remark = Convert.ToString(this.dgvIETables.Rows[i].Cells["remark"].Value); //备注
                iebomProcesTable.isDel = 0; //是否删除
                iebomProcesTables.Add(iebomProcesTable);

            }

            NewStyleLearningCurve learningCurve = new NewStyleLearningCurve();
            learningCurve.IEBomLearningCurveID = IEBomBodyID;
            learningCurve.CureNamesID = this.cbCureNames.SelectedItem.ToString();
            learningCurve.day1 = Convert.ToInt32( this.txt1day.Text);
            learningCurve.day2 = Convert.ToInt32( this.txt2day.Text);
            learningCurve.day3 = Convert.ToInt32(this.txt3day.Text);
            learningCurve.day4 = Convert.ToInt32(this.txt4day.Text);
            learningCurve.day5 = Convert.ToInt32(this.txt5day.Text);
            learningCurve.day6 = Convert.ToInt32(this.txt6day.Text);
            learningCurve.day7 = Convert.ToInt32(this.txt7day.Text);
            learningCurve.day8 = Convert.ToInt32(this.txt8day.Text);
            learningCurve.day9 = Convert.ToInt32(this.txt9day.Text);
            learningCurve.day10 = Convert.ToInt32(this.txt10day.Text);
            learningCurve.day11 = Convert.ToInt32(this.txt11day.Text);
            learningCurve.day12 = Convert.ToInt32(this.txt12day.Text);
            learningCurve.day13 = Convert.ToInt32(this.txt13day.Text);
            learningCurve.day14 = Convert.ToInt32(this.txt14day.Text);

            learningCurve.hour8Day1Makes = Convert.ToInt32(this.txt1Day8HourMakes.Text);
            learningCurve.hour8Day2Makes = Convert.ToInt32(this.txt2Day8HourMakes.Text);
            learningCurve.hour8Day3Makes = Convert.ToInt32(this.txt3Day8HourMakes.Text);
            learningCurve.hour8Day4Makes = Convert.ToInt32(this.txt4Day8HourMakes.Text);
            learningCurve.hour8Day5Makes = Convert.ToInt32(this.txt5Day8HourMakes.Text);
            learningCurve.hour8Day6Makes = Convert.ToInt32(this.txt6Day8HourMakes.Text);
            learningCurve.hour8Day7Makes = Convert.ToInt32(this.txt7Day8HourMakes.Text);
            learningCurve.hour8Day8Makes = Convert.ToInt32(this.txt8Day8HourMakes.Text);
            learningCurve.hour8Day9Makes = Convert.ToInt32(this.txt9Day8HourMakes.Text);
            learningCurve.hour8Day10Makes = Convert.ToInt32(this.txt10Day8HourMakes.Text);
            learningCurve.hour8Day11Makes = Convert.ToInt32(this.txt11Day8HourMakes.Text);
            learningCurve.hour8Day12Makes = Convert.ToInt32(this.txt12Day8HourMakes.Text);
            learningCurve.hour8Day13Makes = Convert.ToInt32(this.txt13Day8HourMakes.Text);
            learningCurve.hour8Day14Makes = Convert.ToInt32(this.txt14Day8HourMakes.Text);

            learningCurve.hour10Day1Makes = Convert.ToInt32(this.txt1Day10HourMakes.Text);
            learningCurve.hour10Day2Makes = Convert.ToInt32(this.txt2Day10HourMakes.Text);
            learningCurve.hour10Day3Makes = Convert.ToInt32(this.txt3Day10HourMakes.Text);
            learningCurve.hour10Day4Makes = Convert.ToInt32(this.txt4Day10HourMakes.Text);
            learningCurve.hour10Day5Makes = Convert.ToInt32(this.txt5Day10HourMakes.Text);
            learningCurve.hour10Day6Makes = Convert.ToInt32(this.txt6Day10HourMakes.Text);
            learningCurve.hour10Day7Makes = Convert.ToInt32(this.txt7Day10HourMakes.Text);
            learningCurve.hour10Day8Makes = Convert.ToInt32(this.txt8Day10HourMakes.Text);
            learningCurve.hour10Day9Makes = Convert.ToInt32(this.txt9Day10HourMakes.Text);
            learningCurve.hour10Day10Makes = Convert.ToInt32(this.txt10Day10HourMakes.Text);
            learningCurve.hour10Day11Makes = Convert.ToInt32(this.txt11Day10HourMakes.Text);
            learningCurve.hour10Day12Makes = Convert.ToInt32(this.txt12Day10HourMakes.Text);
            learningCurve.hour10Day13Makes = Convert.ToInt32(this.txt13Day10HourMakes.Text);
            learningCurve.hour10Day14Makes = Convert.ToInt32(this.txt14Day10HourMakes.Text);


            iebomGroup Groups = new iebomGroup();
            string groupStyleName = "";
            if (this.cbGroupStyleName.Items.Count > 0 && this.cbGroupStyleName.SelectedIndex > -1)
            {
                groupStyleName = this.cbGroupStyleName.SelectedItem.ToString();
            }else if(groupStyleName == "" && this.txtNewGroupName.Text.Trim() != "")
            {
                groupStyleName = this.txtNewGroupName.Text.Trim().ToUpper();
            }
            if(groupStyleName != "")
            {
                Groups.groupid = this.GroupID;
                Groups.groupname = groupStyleName;
                Groups.groupstyle = this.txtStyleNumber.Text.Trim().ToUpper();
                Groups.note = "";
            }
            if (Groups.groupid > 0)
            {

                iem.insertIEBomGroup(Groups);
            }


            int Hresult =  iem.saveIEBomHead(iebomHead);
            string Presult = iem.saveIEBomProcesTables(iebomProcesTables);
            int lresult = iem.savelearningCurves(learningCurve);

            if (Hresult > 0  && Presult =="0" && lresult > 0)
            {
                this.butSave.Enabled = false;
                MessageBox.Show("保存成功");
                this.butCopyAddIETable.Enabled = false;
                this.butModifyIETable.Enabled = false;
                this.butDelIETable.Enabled = false;
                //this.butPrint.Enabled = false;

            }
            else
            {
                MessageBox.Show("保存失败");
            }


        }

        /// <summary>
        /// 将一张图片转换为字节
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="imgFormat">保存图片的类型</param>
        /// <returns>byte[]</returns>
        /// <summary>
        /// 图片转换成字节流
        /// </summary>
        /// <param name="img">要转换的Image对象</param>
        /// <returns>转换后返回的字节流</returns>
        public  byte[] ImgToByt(Image img)
        {
            if (img == null)
            {
                return new byte[0];
            }


            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imagedata = ms.GetBuffer();
            return imagedata;
        }
        /// <summary>
        /// 字节流转换成图片
        /// </summary>
        /// <param name="byt">要转换的字节流</param>
        /// <returns>转换得到的Image对象</returns>
        public  Image BytToImg(byte[] byt)
        {
            MemoryStream ms = new MemoryStream(byt);
            Image img = Image.FromStream(ms);
            return img;
        }

        //
        /// <summary>
        /// 根据图片路径返回图片的字节流byte[]
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <returns>返回的字节流</returns>
        private  byte[] getImageByte(string imagePath)
        {
            FileStream files = new FileStream(imagePath, FileMode.Open);
            byte[] imgByte = new byte[files.Length];
            files.Read(imgByte, 0, imgByte.Length);
            files.Close();
            return imgByte;
        }

        private void butCopyAddIETable_Click(object sender, EventArgs e)
        {

            this.butSave.Enabled = false;
            this.butCopyAddIETable.Enabled = false;
            this.butModifyIETable.Enabled = false;
            this.butDelIETable.Enabled = false;
            // this.butPrint.Enabled = false;

            this.cbIEVersion.Enabled = true;
           // this.cbIEVersion.Items.Clear();

           /*

            foreach (DataRow Row in VersionDT.Rows)
            {
                string vers = Row["IEBomVersion"].ToString();
                this.cbIEVersion.Items.Add("V" + vers);
            }
           */
            List<string> vers = new List<string>();
            if(this.cbIEVersion.Items.Count > 0)
            {
                foreach (string item in this.cbIEVersion.Items)
                {
                    string v = item.ToString();
                    v= v.Substring(1, v.Length - 1);
                    vers.Add(v);
                }
            }
            int maxv = -1;
            if (vers.Count > 0)
            {
                for (int i = 0; i < vers.Count; i++)
                {
                    if(Convert.ToInt32( vers[i]) > maxv)
                    {
                        maxv = Convert.ToInt32(vers[i]);
                    }

                }
            }
            maxv += 1;

            this.cbIEVersion.Items.Add("V"+maxv.ToString());
            this.IsNewIEVer = true;
           this.cbIEVersion.SelectedIndex = this.cbIEVersion.Items.Count - 1;
            this.butModifyIETable.Enabled = true;

            /*

            this.IsNewIEVer = false;
            this.splitContainer1.Enabled = true;
            this.labCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.labLastModifyDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.labCreator.Text = Dns.GetHostName().ToUpper();
            this.dgvIETables.AllowUserToAddRows = true;
            this.butSave.Enabled = false;
            this.butCopyAddIETable.Enabled = false;
            this.butModifyIETable.Enabled = false;
            this.butDelIETable.Enabled = false;
            this.ckChangeGroup.Checked = true;
            this.txtNewGroupName.Enabled = true;
            this.cbGroupStyleName.Enabled = true;
            this.cbSameGroupStyles.Enabled = true;
            this.dgvIETables.Enabled = true;
            this.dgvIETables.ReadOnly = false;
            this.labPicture1.Visible = true;
            this.labPicture2.Visible = true;

            this.txtSewWorkmanCount.Text = "36";

            this.txtStandardCoefficient.Text = "1.2";
            this.txtStandardHourProductionCapacity.Text = "3600";

            this.splitContainer1.Enabled = true;
            this.txtStyleNumber.Enabled = true;
            this.txtLectra.Enabled = true;
            this.gbIEBast.Enabled = true;
            this.gbNewStyleLearningCurve.Enabled = true;
            this.dgvIETables.Enabled = true;
            this.butAddIETable.Enabled = true;
            this.butModifyIETable.Enabled = true;
            this.butDelIETable.Enabled = true;
            this.butCopyAddIETable.Enabled = true;
            this.splitContainer1.Enabled = false;
            */

        }

        private void dgvIETables_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {




        }

        private void dgvIETables_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            string machineName = "";
            if (rowIndex < 0) return;
            machineName = Convert.ToString(this.dgvIETables.Rows[rowIndex].Cells["partMachineTypeName"].Value);

            if (machineName != "")
            {
                for (int j = 0; j < this.machines.Rows.Count; j++)
                {
                    if (this.dgvIETables.Rows[rowIndex].Cells["partMachineTypeName"].Value.ToString() == this.machines.Rows[j]["MachineName"].ToString())
                    {
                        this.dgvIETables.Rows[rowIndex].Cells["partMachineTypeID"].Value = this.machines.Rows[j]["ID"].ToString();
                    }

                }

            }
        }

        public void searchIEBom( string ver)
        {
            this.selectVer = 1;
            string SearchStyle = "";
            if (this.IsNewIEVer) return;

            if (this.cbStyleSearch.SelectedItem != null)
            {
                 SearchStyle = this.cbStyleSearch.SelectedItem.ToString();
            }
            else
            {
                SearchStyle = this.cbStyleSearch.Text.Trim();

            }
         //   this.cbStyleSearch.SelectedIndex = this.cbStyleSearch.Items.Count - 1;


            if (SearchStyle == "")
            {
                return;

            }




            DataTable VersionDT = iem.searchByStyle(SearchStyle);
            if (VersionDT == null || VersionDT.Rows.Count <= 0)
            {
                MessageBox.Show("没有建立此款式的IE基本资料，请输入正确的款式，谢谢！");
                return;
            }

            this.txtNewGroupName.Text = "";
            this.cbGroupStyleName.Items.Clear();
            this.cbSameGroupStyles.Items.Clear();
            this.txtStyleGroup.Text = "";
            string groupID = VersionDT.Rows[0]["GroupID"].ToString();
            DataTable StyleGroupDT;
            if (groupID != "")
            {
                 StyleGroupDT = iem.searchGroupByGroupID( Convert.ToInt32( groupID));
                if (StyleGroupDT != null && StyleGroupDT.Rows.Count > 0)
                {
                    // 群组的内容
                    this.changeGroupStyles(StyleGroupDT);
                }
            }




            this.initialization();
            this.cbIEVersion.Enabled=true;
            this.cbIEVersion.Items.Clear();
            this.txtStyleNumber.Text = VersionDT.Rows[VersionDT.Rows.Count - 1]["IEBomStyleName"].ToString();
            this.txtLectra.Text = VersionDT.Rows[VersionDT.Rows.Count - 1]["LectraNumber"].ToString();

            if (this.cbIEVersion.Items.Count > 0)
            {
                this.cbIEVersion.SelectedIndex = 0;
                this.ver = this.cbIEVersion.SelectedItem.ToString();
            }

                foreach (DataRow Row in VersionDT.Rows)
                {
                    string vers = Row["IEBomVersion"].ToString();
                    this.cbIEVersion.Items.Add("V" + vers);
                }



            DataTable BomBaseDT = iem.searchByStyle(SearchStyle, this.ver);
            if (BomBaseDT == null || BomBaseDT.Rows.Count <= 0)
            {
                MessageBox.Show("没有建立此款式的IE基本资料，请输入正确的款式，谢谢！");
                return;
            }
            foreach (DataRow Row in BomBaseDT.Rows)
            {
                byte[] MainBytes = null;
                MainBytes = (byte[])Row["MainPicture"];
                if (MainBytes != null && MainBytes.Length > 0)
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream(MainBytes);
                        Bitmap Mmpt = new Bitmap(ms);
                        this.pbPicture1.Image = Mmpt;
                        this.pbPicture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        this.labPicture1.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


                byte[] ReverseBytes = null;
                ReverseBytes = (byte[])Row["ReversePicture"];
                if (ReverseBytes != null && ReverseBytes.Length > 0)
                {

                    try
                    {
                        MemoryStream rs = new MemoryStream(ReverseBytes);
                        Bitmap Rmpt = new Bitmap(rs);
                        this.pbPicture2.Image = Rmpt;
                        this.pbPicture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        this.labPicture2.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

                this.labCreateDate.Text = Row["IEBomCreateDate"].ToString();
                this.labLastModifyDate.Text = Row["IEBomLastModifyDate"].ToString();
                this.labCreator.Text = Row["IEBomCreator"].ToString();
                this.txtStyleRemark.Text = Row["StyleRemark"].ToString();
                this.txtSeason.Text = Row["Season"].ToString();
                this.txgMakeGroup.Text = Row["MakeGroup"].ToString();
                this.txtDifficultyx.Text = Row["Difficultyx"].ToString();

                this.txtDifficultyy.Text = Row["Difficultyy"].ToString();
                this.txtTaktTime.Text = Row["TaktTime"].ToString();
                this.txtSinglePieceMakeTime.Text = Row["SinglePieceMakeTime"].ToString();
                this.txtHourSingleMakes.Text = Row["HourSingleMakes"].ToString();
                this.txtHourGroupMakes.Text = Row["HourGroupMakes"].ToString();
                this.txtSewWorkmanCount.Text = Row["SewWorkmanCount"].ToString();
                this.txtEightMakePieces.Text = Row["EightMakePieces"].ToString();
                this.txtTenMakePieces.Text = Row["TenMakePieces"].ToString();

                this.txtStandardCoefficient.Text = Row["StandardCoefficient"].ToString();
                this.txtStandardHourProductionCapacity.Text = Row["StandardHourProductionCapacity"].ToString();
                this.cbCureNames.Items.Clear();
                this.cbCureNames.Items.Add(Row["CureNames"].ToString());
                this.cbCureNames.SelectedIndex = 0;
            }
            this.splitContainer1.Enabled = true;
            this.pbPicture1.Enabled = false;
            this.pbPicture2.Enabled = false;
            this.txtStyleNumber.Enabled = false;
            this.txtLectra.Enabled = false;
            this.gbIEBast.Enabled = false;
           // this.gbNewStyleLearningCurve.Enabled = false;
            this.dgvIETables.Enabled = false;
            this.butAddIETable.Enabled = true;
            this.butModifyIETable.Enabled = true;
            this.butDelIETable.Enabled = true;
            this.butCopyAddIETable.Enabled = true;
            this.cbIEVersion.SelectedIndex = this.verIndex;

            if(this.cbIEVersion.Items.Count > 0 && this.cbIEVersion.SelectedIndex <= -1)
            {
                this.cbIEVersion.SelectedIndex = 0;
            }


            // 工段表的资料
            string processNumber = "";
            if (this.cbIEVersion.Items.Count > 0 && this.cbIEVersion.SelectedIndex > -1)
            {
                processNumber = this.txtStyleNumber.Text +  this.cbIEVersion.SelectedItem.ToString();
            }

            DataTable ProcessDT = iem.searchProcesByProcessNumber(processNumber);

            if (ProcessDT == null || ProcessDT.Rows.Count <= 0)
            {
                 MessageBox.Show("没有建立此款式的工程工段表，请输入，谢谢！");

            }
            else
            {
                this.dgvIETables.Rows.Clear();
                bool important = false;
                for (   int i =0;i< ProcessDT.Rows.Count;i++)
                {
                    this.dgvIETables.Rows.Add();
                    this.dgvIETables.Rows[i].Cells["Scope"].Value = ProcessDT.Rows[i]["Scope"].ToString();
                    this.dgvIETables.Rows[i].Cells["partNumber"].Value = ProcessDT.Rows[i]["partNumber"].ToString();
                    this.dgvIETables.Rows[i].Cells["partName"].Value = ProcessDT.Rows[i]["partName"].ToString();
                    int importantstr = Convert.ToInt32(ProcessDT.Rows[i]["importantPart"].ToString());

                    if(importantstr == 1)
                    {
                        important = true;
                    }
                    else
                    {
                        important = false;
                    }
                    this.dgvIETables.Rows[i].Cells["importantPart"].Value = important;
                    this.dgvIETables.Rows[i].Cells["partRemark"].Value = ProcessDT.Rows[i]["partRemark"].ToString();
                    this.dgvIETables.Rows[i].Cells["partMachineTypeID"].Value = ProcessDT.Rows[i]["partMachineTypeID"].ToString();
                    this.dgvIETables.Rows[i].Cells["partMachineTypeName"].Value = ProcessDT.Rows[i]["partMachineTypeName"].ToString();
                    this.dgvIETables.Rows[i].Cells["averageSecond"].Value = ProcessDT.Rows[i]["averageSecond"].ToString();
                    this.dgvIETables.Rows[i].Cells["standardSecond"].Value = ProcessDT.Rows[i]["standardSecond"].ToString();
                    this.dgvIETables.Rows[i].Cells["standardHourproductionCapacity"].Value = ProcessDT.Rows[i]["standardHourproductionCapacity"].ToString();
                    this.dgvIETables.Rows[i].Cells["assignmentAllocate"].Value = ProcessDT.Rows[i]["assignmentAllocate"].ToString();
                    this.dgvIETables.Rows[i].Cells["actualAllocate"].Value = ProcessDT.Rows[i]["actualAllocate"].ToString();

                    this.dgvIETables.Rows[i].Cells["remark"].Value = ProcessDT.Rows[i]["remark"].ToString();
                }
                this.dgvIETables.AllowUserToAddRows = false;

            }




            this.calculate();
            this.butSave.Enabled = false;
            // 查询IE曲线学习表
            this.dgvIETables.Enabled = true;
            this.dgvIETables.ReadOnly = true;



        }

        public void  changeGroupStyles( DataTable StyleGroups)
        {

            string groupstyles = "";
            string groupname = "";

            for (int i = 0; i < StyleGroups.Rows.Count; i++)
            {
                string groupstyle = StyleGroups.Rows[i]["groupstyle"].ToString();
                this.cbSameGroupStyles.Items.Add(groupstyle);
                groupstyles = groupstyles + "," + groupstyle;
                groupname = StyleGroups.Rows[i]["groupname"].ToString();
            }
            if(groupstyles.Length > 1)
            {
                groupstyles = groupstyles.Substring(1, groupstyles.Length - 1);
            }

            this.txtStyleGroup.Text = groupstyles;
            this.cbGroupStyleName.Items.Add(groupname);
            if (this.cbSameGroupStyles.Items.Count > 0)
            {
                this.cbSameGroupStyles.SelectedIndex = 0;
            }
            if (this.cbGroupStyleName.Items.Count > 0)
            {
                this.cbGroupStyleName.SelectedIndex = 0;
            }

        }
        public void initialization()
        {
            this.dgvIETables.Enabled = true;
            this.dgvIETables.ReadOnly = false;

            this.cbIEVersion.Items.Clear();
            this.txtStyleNumber.Text = "";
            this.txtLectra.Text ="";

            this.pbPicture1.Image = null;
                this.labPicture1.Visible = true;
                this.pbPicture2.Image = null;
                this.labPicture2.Visible = true;

                this.labCreateDate.Text ="";
                this.labLastModifyDate.Text = "";
            this.labCreator.Text = "";
            this.txtStyleRemark.Text = "";
            this.txtSeason.Text = "";
            this.txgMakeGroup.Text = "";
            this.txtDifficultyx.Text = "";
            this.txtDifficultyy.Text = "";
            this.txtTaktTime.Text = "";
            this.txtSinglePieceMakeTime.Text = "";
            this.txtHourSingleMakes.Text = "";
            this.txtHourGroupMakes.Text = "";
            this.txtSewWorkmanCount.Text = "36";
            this.txtEightMakePieces.Text = "";
            this.txtTenMakePieces.Text = "";

            this.txtStandardCoefficient.Text = "1.2";
            this.txtStandardHourProductionCapacity.Text = "3600";
            this.cbCureNames.Items.Clear();

            this.splitContainer1.Enabled = true;
            this.txtStyleNumber.Enabled = true;
            this.txtLectra.Enabled = true;
            this.gbIEBast.Enabled = true;
            this.gbNewStyleLearningCurve.Enabled = true;
            this.dgvIETables.Enabled = true;
            this.butAddIETable.Enabled = true;
            this.butModifyIETable.Enabled = true;
            this.butDelIETable.Enabled = true;
            this.butCopyAddIETable.Enabled = true;


            this.WindowState = FormWindowState.Maximized;
            this.splitContainer1.Enabled = false;
            //获取所有机器类型表
            this.machines = mtm.getAllMachineTypes();
            //获取标准系数表
            this.cureNamesDT = iem.getCureNames();
            if (this.cureNamesDT.Rows.Count > 0)
            {
                this.cbCureNames.Items.Clear();
                foreach (DataRow dr in this.cureNamesDT.Rows)
                {
                    this.cbCureNames.Items.Add(dr["modulusName"].ToString());

                }
                this.cbCureNames.SelectedIndex = 0;
                this.CureNamesID = Convert.ToInt32(this.cureNamesDT.Rows[0]["id"].ToString());
            }
            this.dgvIETables.Rows.Clear();

        }
        private void butSearch_Click(object sender, EventArgs e)
        {
            this.ver = "0";
            this.verIndex = 0;
            this.selectVer = 0;
            this.IsNewIEVer = false;
            //  this.ver
            this.searchIEBom("0");
        }

        private void cbIEVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newver = this.cbIEVersion.SelectedItem.ToString();
            newver = newver.Substring(1, newver.Length - 1);
            if (newver == this.ver  ||  this.iSnewVer )
            {
                this.iSnewVer = false;
                return;
            }
            this.ver = newver;
            this.verIndex = this.cbIEVersion.SelectedIndex;
            this.searchIEBom(newver);
        }



        private void updataIEBom(int id)
        {
            string StyleNumber = this.txtStyleNumber.Text.Trim();
            string lectra = this.txtLectra.Text.Trim();
            string IEVersion = this.cbIEVersion.Text.Trim();
            string curenames = this.cbCureNames.SelectedItem.ToString();
            string Workman = this.txtSewWorkmanCount.Text.Trim();

            if (StyleNumber.Length <= 0 || lectra.Length <= 0 || IEVersion.Length <= 0 || curenames.Length <= 0 || Workman.Length <= 0)
            {
                string message = "       款式名 \r\n  或 力克版号  \r\n  或 IE版本号   \r\n  或 标准系数表   \r\n  或  车缝人数   \r\n  全部不能为空";
                string caption = "错误";
                MessageBox.Show(message, caption,
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                return;
            }


            iebomHead = new IEBom(); // 基本信息表

            iebomHead.Id = id;
            iebomHead.IEBomName = this.txtStyleNumber.Text.Trim(); //Bom名称
            iebomHead.IEBomStyleName = this.txtStyleNumber.Text.Trim(); //款式名
            iebomHead.LectraNumber = this.txtLectra.Text.Trim(); //力克版号

            // string IEVersion = this.cbIEVersion.SelectedItem.ToString();
            IEVersion = IEVersion.Substring(1, IEVersion.Length - 1);
            iebomHead.IEBomVersion = IEVersion; //Bom款式版本号


            //本版本已保存，是否保存修订版本

            iebomHead.IEBomCreateDate = this.labCreateDate.Text.Trim(); // BOM 创建日期
            iebomHead.IEBomLastModifyDate = this.labLastModifyDate.Text.Trim(); // BOM 最后修改日期
            iebomHead.IEBomCreator = this.labCreator.Text.Trim(); // BOM 创建人

            string IEBomBodyID = this.txtStyleNumber.Text.Trim() + this.cbIEVersion.SelectedItem.ToString();
            //txtStyleNumber +  cbIEVersion
            iebomHead.IEBomModifyHistoryNumber = IEBomBodyID; // ! 修改历史记录ID编号
            iebomHead.IEBomProcessNumber = IEBomBodyID; // ! 工序表ID
            iebomHead.IEBomRatioID = IEBomBodyID; // ! IE系数表ID
            System.Drawing.Image MainImg = this.pbPicture1.Image;
            System.Drawing.Image ReverseImg = this.pbPicture2.Image;
            iebomHead.MainPicture = this.ImgToByt(MainImg); // ! 正面图
            iebomHead.ReversePicture = this.ImgToByt(ReverseImg); // ! 背面图

            iebomHead.StyleRemark = this.txtStyleRemark.Text.Trim(); // 款号描述
            iebomHead.MakeGroup = this.txgMakeGroup.Text.Trim(); // 生产组别
            iebomHead.TaktTime = Convert.ToDouble(this.txtTaktTime.Text.Trim()); // 节拍时间
            iebomHead.SinglePieceMakeTime = Convert.ToDouble(this.txtSinglePieceMakeTime.Text.Trim()); // 单件时间
            iebomHead.HourSingleMakes = Convert.ToDouble(this.txtHourSingleMakes.Text.Trim()); // 时产能-单人
            iebomHead.HourGroupMakes = Convert.ToDouble(this.txtHourGroupMakes.Text.Trim()); // 时产能-小组
            iebomHead.SewWorkmanCount = Convert.ToInt32(this.txtSewWorkmanCount.Text.Trim()); // 车缝人数
            iebomHead.EightMakePieces = Convert.ToDouble(this.txtEightMakePieces.Text.Trim()); // 8小时产能-组
            iebomHead.TenMakePieces = Convert.ToDouble(this.txtTenMakePieces.Text.Trim()); // 10小时产能-组

            iebomHead.Season = this.txtSeason.Text.Trim(); // 季节
            iebomHead.Difficultyx = this.txtDifficultyx.Text.Trim(); // 难易级别 字母
            iebomHead.Difficultyy = Convert.ToDouble(this.txtDifficultyy.Text.Trim()); // 难易级别 数字
            iebomHead.StandardCoefficient = Convert.ToDouble(this.txtStandardCoefficient.Text.Trim()); // 宽放系数
            iebomHead.StandardHourProductionCapacity = Convert.ToInt32(this.txtStandardHourProductionCapacity.Text.Trim()); // 标准时秒数
            iebomHead.CureNames = this.cbCureNames.Text.Trim(); // 标准系数表
            iebomHead.GroupID = this.GroupID;



            List<IEBomProces> iebomProcesTables = new List<IEBomProces>();//工段表
            for (int i = 0; i < this.dgvIETables.Rows.Count; i++)
            {
                IEBomProces iebomProcesTable = new IEBomProces();
                iebomProcesTable.ProcessNumber = IEBomBodyID; // IE表对应头ID
                iebomProcesTable.Scope = Convert.ToString(this.dgvIETables.Rows[i].Cells["Scope"].Value); // !部件
                iebomProcesTable.partNumber = Convert.ToInt32(this.dgvIETables.Rows[i].Cells["partNumber"].Value.ToString()); // !工段号
                iebomProcesTable.partName = Convert.ToString(this.dgvIETables.Rows[i].Cells["partName"].Value); // !工段名称
                int  isImportanPart = 0;
                if(  this.dgvIETables.Rows[i].Cells["importantPart"].Value.ToString() =="True")
                {
                    isImportanPart = 1;
                }
                else
                {
                    isImportanPart = 0;
                }
                iebomProcesTable.importantPart = isImportanPart; // 重要工段
                iebomProcesTable.partRemark = Convert.ToString(this.dgvIETables.Rows[i].Cells["partRemark"].Value); // 工段备注
                iebomProcesTable.partMachineTypeID = Convert.ToInt32(this.dgvIETables.Rows[i].Cells["partMachineTypeID"].Value.ToString()); // !机器ID号
                iebomProcesTable.partMachineTypeName = Convert.ToString(this.dgvIETables.Rows[i].Cells["partMachineTypeName"].Value); // !机器名称
                iebomProcesTable.averageSecond = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["averageSecond"].Value.ToString()); //平均时长(秒)
                iebomProcesTable.standardSecond = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["standardSecond"].Value.ToString()); //标准时长(秒)
                iebomProcesTable.standardHourproductionCapacity = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["standardHourproductionCapacity"].Value.ToString()); //标准时产能(件)
                iebomProcesTable.assignmentAllocate = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["assignmentAllocate"].Value.ToString()); //作业分配(人)
                iebomProcesTable.actualAllocate = Convert.ToDouble(this.dgvIETables.Rows[i].Cells["actualAllocate"].Value.ToString()); //实际配置(人)
                iebomProcesTable.remark = Convert.ToString(this.dgvIETables.Rows[i].Cells["remark"].Value); //备注
                iebomProcesTable.isDel =0; //是否删除
                iebomProcesTables.Add(iebomProcesTable);

            }

            NewStyleLearningCurve learningCurve = new NewStyleLearningCurve();
            learningCurve.IEBomLearningCurveID = IEBomBodyID;
            learningCurve.CureNamesID = this.cbCureNames.SelectedItem.ToString();
            learningCurve.day1 = Convert.ToInt32(this.txt1day.Text);
            learningCurve.day2 = Convert.ToInt32(this.txt2day.Text);
            learningCurve.day3 = Convert.ToInt32(this.txt3day.Text);
            learningCurve.day4 = Convert.ToInt32(this.txt4day.Text);
            learningCurve.day5 = Convert.ToInt32(this.txt5day.Text);
            learningCurve.day6 = Convert.ToInt32(this.txt6day.Text);
            learningCurve.day7 = Convert.ToInt32(this.txt7day.Text);
            learningCurve.day8 = Convert.ToInt32(this.txt8day.Text);
            learningCurve.day9 = Convert.ToInt32(this.txt9day.Text);
            learningCurve.day10 = Convert.ToInt32(this.txt10day.Text);
            learningCurve.day11 = Convert.ToInt32(this.txt11day.Text);
            learningCurve.day12 = Convert.ToInt32(this.txt12day.Text);
            learningCurve.day13 = Convert.ToInt32(this.txt13day.Text);
            learningCurve.day14 = Convert.ToInt32(this.txt14day.Text);

            learningCurve.hour8Day1Makes = Convert.ToInt32(this.txt1Day8HourMakes.Text);
            learningCurve.hour8Day2Makes = Convert.ToInt32(this.txt2Day8HourMakes.Text);
            learningCurve.hour8Day3Makes = Convert.ToInt32(this.txt3Day8HourMakes.Text);
            learningCurve.hour8Day4Makes = Convert.ToInt32(this.txt4Day8HourMakes.Text);
            learningCurve.hour8Day5Makes = Convert.ToInt32(this.txt5Day8HourMakes.Text);
            learningCurve.hour8Day6Makes = Convert.ToInt32(this.txt6Day8HourMakes.Text);
            learningCurve.hour8Day7Makes = Convert.ToInt32(this.txt7Day8HourMakes.Text);
            learningCurve.hour8Day8Makes = Convert.ToInt32(this.txt8Day8HourMakes.Text);
            learningCurve.hour8Day9Makes = Convert.ToInt32(this.txt9Day8HourMakes.Text);
            learningCurve.hour8Day10Makes = Convert.ToInt32(this.txt10Day8HourMakes.Text);
            learningCurve.hour8Day11Makes = Convert.ToInt32(this.txt11Day8HourMakes.Text);
            learningCurve.hour8Day12Makes = Convert.ToInt32(this.txt12Day8HourMakes.Text);
            learningCurve.hour8Day13Makes = Convert.ToInt32(this.txt13Day8HourMakes.Text);
            learningCurve.hour8Day14Makes = Convert.ToInt32(this.txt14Day8HourMakes.Text);

            learningCurve.hour10Day1Makes = Convert.ToInt32(this.txt1Day10HourMakes.Text);
            learningCurve.hour10Day2Makes = Convert.ToInt32(this.txt2Day10HourMakes.Text);
            learningCurve.hour10Day3Makes = Convert.ToInt32(this.txt3Day10HourMakes.Text);
            learningCurve.hour10Day4Makes = Convert.ToInt32(this.txt4Day10HourMakes.Text);
            learningCurve.hour10Day5Makes = Convert.ToInt32(this.txt5Day10HourMakes.Text);
            learningCurve.hour10Day6Makes = Convert.ToInt32(this.txt6Day10HourMakes.Text);
            learningCurve.hour10Day7Makes = Convert.ToInt32(this.txt7Day10HourMakes.Text);
            learningCurve.hour10Day8Makes = Convert.ToInt32(this.txt8Day10HourMakes.Text);
            learningCurve.hour10Day9Makes = Convert.ToInt32(this.txt9Day10HourMakes.Text);
            learningCurve.hour10Day10Makes = Convert.ToInt32(this.txt10Day10HourMakes.Text);
            learningCurve.hour10Day11Makes = Convert.ToInt32(this.txt11Day10HourMakes.Text);
            learningCurve.hour10Day12Makes = Convert.ToInt32(this.txt12Day10HourMakes.Text);
            learningCurve.hour10Day13Makes = Convert.ToInt32(this.txt13Day10HourMakes.Text);
            learningCurve.hour10Day14Makes = Convert.ToInt32(this.txt14Day10HourMakes.Text);


            iebomGroup Groups = new iebomGroup();
            string groupStyleName = "";
            if (this.cbGroupStyleName.Items.Count > 0 && this.cbGroupStyleName.SelectedIndex > -1)
            {
                groupStyleName = this.cbGroupStyleName.SelectedItem.ToString();
            }
            else if (groupStyleName == "" && this.txtNewGroupName.Text.Trim() != "")
            {
                groupStyleName = this.txtNewGroupName.Text.Trim().ToUpper();
            }
            if (groupStyleName != "")
            {
                Groups.groupid = this.GroupID;
                Groups.groupname = groupStyleName;
                Groups.groupstyle = this.txtStyleNumber.Text.Trim().ToUpper();
                Groups.note = "";
            }

            if (Groups.groupid > 0)
            {
                iem.insertIEBomGroup(Groups);
            }




            int Hresult = iem.updataIEBomHead(iebomHead,id);
            // 删除原有的，保存新的
            int PupResult = iem.updataIEBomProcesTables( IEBomBodyID); //软删除原来的表单内容
            string Presult = iem.saveIEBomProcesTables(iebomProcesTables);
            int lresult = iem.updatalearningCurves(learningCurve, IEBomBodyID);

            if (Hresult > 0 && Presult == "0" && lresult > 0)
            {
                this.butSave.Enabled = false;
                MessageBox.Show("修改保存完成");
                this.butCopyAddIETable.Enabled = false;
                this.butModifyIETable.Enabled = false;
                this.butDelIETable.Enabled = false;
               // this.butPrint.Enabled = false;

            }
            else
            {
                MessageBox.Show("修改保存失败");
            }


        }

        private void butModifyIETable_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Enabled = true;
            this.pbPicture1.Enabled = true;
            this.pbPicture2.Enabled = true;
            this.txtStyleNumber.Enabled = true;
            this.txtLectra.Enabled = true;
            this.gbIEBast.Enabled = true;
            this.gbNewStyleLearningCurve.Enabled = true;
            this.dgvIETables.Enabled = true;
            this.butAddIETable.Enabled = true;
            this.butModifyIETable.Enabled = true;
            this.butDelIETable.Enabled = true;
            this.butCopyAddIETable.Enabled = true;

            this.dgvIETables.Enabled = true;
            this.dgvIETables.ReadOnly = false;
        }

        private void dgvIETables_Click(object sender, EventArgs e)
        {
            this.dgvIETables.AllowUserToAddRows = true;



        }

        private void butDelIETable_Click(object sender, EventArgs e)
        {



            MessageBox.Show("待开发...");
            return;
        }

        private void butPrint_Click(object sender, EventArgs e)
        {
            if (this.txtStyleNumber.Text.Trim().Length <= 0)
            {
                return;
            }

            string styleNumber = this.txtStyleNumber.Text.Trim().ToUpper();
            string ieVersion = this.cbIEVersion.SelectedItem.ToString().Trim().ToUpper();
            int isNewStyle = this.comboBox1.SelectedIndex;
            string ep = this.txtEightMakePieces.Text;
            string tp = this.txtTenMakePieces.Text;
            FrmIEBomPrintGrid frm = FrmIEBomPrintGrid.GetSingleton(styleNumber, ieVersion, isNewStyle,ep,tp);
            frm.Show();
            frm.Activate();


        }

        private void butPartsTable_Click(object sender, EventArgs e)
        {
            MessageBox.Show("待开发...");
            return;
        }

        private void dgvIETables_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }



        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            /*
           // m.WParam.ToInt32() 要和 注册热键时的第2个参数一样
           if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == 101) //判断热键
           {
               MessageBox.Show("1");
           }
           base.WndProc(ref m);

            */

            //按快捷键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:

                            // Clipboard.SetText(this.dgvSearch.Rows[0].Cells[0].Value.ToString());
                            break;
                        case 101:
                            this.pasteData();
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);

        }

        public void pasteData()
        {
           if(this.dgvIETables.Focused)
            {
                int CellsIndex = -1;
                if (rbPasteScope.Checked)
                {
                    CellsIndex = 0;

                }else if (rbPastePartName.Checked)
                {
                    CellsIndex = 2;
                }
                else if (rbPastePartRemark.Checked)
                {
                    CellsIndex = 4;
                }
                else if (rbPasteAverageSecond.Checked)
                {
                    CellsIndex = 7;
                }

                try
                {
                    string clipboardText = Clipboard.GetText(); //获取剪贴板中的内容
                    string NewClipboardText = Clipboard.GetText(); //获取剪贴板中的内容
                    if (string.IsNullOrEmpty(clipboardText))
                    {
                        MessageBox.Show("剪贴板中无内容");
                        return;
                    }
                    int colnum = 0;
                    int rownum = 0;
                    for (int i = 0; i < clipboardText.Length; i++)
                    {
                        if (clipboardText.Substring(i, 1) == "\t")
                        {
                            colnum++;
                        }
                        if (clipboardText.Substring(i, 1) == "\r")
                        {
                            rownum++;
                        }
                    }
                    //粘贴板上的数据来源于EXCEL时，每行末尾都有\n，来源于DataGridView是，最后一行末尾没有\n
                    //   if (clipboardText.Substring(clipboardText.Length - 1, 1) == "\n")
                    //  {
                    //       rownum--;
                    //   }
                    //   colnum = colnum / (rownum + 1);
                    //     object[,] data; //定义object类型的二维数组
                    //   data = new object[rownum + 1, colnum + 1];  //根据剪贴板的行列数实例化数组
                    string rowStr = "";

                    //对数组各元素赋值
                    for (int i = 0; i < rownum; i++)
                    {
                        //DataRow dr =
                        if(this.dgvIETables.Rows.Count <= rownum)
                        {
                            this.dgvIETables.Rows.Add();
                        }

                        //  for (int j = 0; j <= colnum; j++)
                        //   {
                        //一行中的其它列
                        //  if (j != colnum)
                        //   {
                      //  rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\r"));
                       // clipboardText = clipboardText.Substring(clipboardText.IndexOf("\r") + 1);
                       // this.dgvIETables.Rows[i].Cells[CellsIndex].Value = rowStr.Trim().ToUpper();
                        string cellstr = "";
                        if(CellsIndex == 0)
                        {

                            rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\r"));
                            this.dgvIETables.Rows[i].Cells[CellsIndex].Value = rowStr.Trim().ToUpper();
                            cellstr = rowStr.Trim().ToUpper();
                            if (clipboardText.IndexOf("\r") != -1)
                            {
                                rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\r"));
                                this.dgvIETables.Rows[i ].Cells[CellsIndex].Value = cellstr;
                            }
                            //最后一行的最后一列
                            if (i == colnum && clipboardText.IndexOf("\r") == -1)
                            {
                                rowStr = clipboardText.Substring(0);
                                this.dgvIETables.Rows[i].Cells[CellsIndex].Value = rowStr;
                            }
                            NewClipboardText = NewClipboardText.Substring(NewClipboardText.IndexOf("\r") + 1);
                            string newRowStr = "";
                            if (NewClipboardText.IndexOf("\r") != -1)
                            {
                                newRowStr = NewClipboardText.Substring(0, NewClipboardText.IndexOf("\r"));
                            }
                            else
                            {
                                newRowStr = NewClipboardText.Substring(0, NewClipboardText.Length);
                                this.dgvIETables.Rows[i].Cells[CellsIndex].Value = rowStr;

                            }


                            if(newRowStr != ""  && newRowStr != "\n")
                            {
                                // rowStr = newRowStr;
                                clipboardText = NewClipboardText;


                            }

                        }
                        else if(CellsIndex == 7){

                                rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\r"));
                               // clipboardText = clipboardText.Substring(clipboardText.IndexOf("\r") + 1);
                            if (Regex.IsMatch(rowStr, @"^[0-9]+$") || Regex.IsMatch(rowStr, @"^\n[0-9]+$") || rowStr == "" || rowStr == "\n")  // 只能输入数字
                            {
                                this.dgvIETables.Rows[i].Cells[CellsIndex].Value = rowStr.Trim().ToUpper();
                                if (i == colnum && clipboardText.IndexOf("\r") != -1)
                                {
                                    rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\r"));
                                    this.dgvIETables.Rows[i + 1].Cells[CellsIndex].Value = rowStr;
                                }
                                //最后一行的最后一列
                                if (i == colnum && clipboardText.IndexOf("\r") == -1)
                                {
                                    rowStr = clipboardText.Substring(0);
                                }
                            }
                            clipboardText = clipboardText.Substring(clipboardText.IndexOf("\r") + 1);

                        }
                        else
                        {
                            rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\r"));
                            clipboardText = clipboardText.Substring(clipboardText.IndexOf("\r") + 1);
                            this.dgvIETables.Rows[i].Cells[CellsIndex].Value = rowStr.Trim().ToUpper();

                            if (i == colnum && clipboardText.IndexOf("\r") != -1)
                            {
                                rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\r"));
                                //  dr["PO"] = rowStr;
                                this.dgvIETables.Rows[i + 1].Cells[CellsIndex].Value = rowStr;

                            }
                            //最后一行的最后一列
                            if (i == colnum && clipboardText.IndexOf("\r") == -1)
                            {
                                rowStr = clipboardText.Substring(0);
                            }

                        }
                        //  dr["style"]=
                        //  data[i, j] = rowStr;
                    }
                    //截取下一行及以后的数据
                    clipboardText = clipboardText.Substring(clipboardText.IndexOf("\r") + 1);
                    // SearchDT.Rows.Add(dr);
                    // }

                    // this.dgvSearch.DataSource = SearchDT;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }

            }

        }

        private void dgvIETables_MouseEnter(object sender, EventArgs e)
        {
            //注册热键Ctrl+C，Id号为100。。
            HotKeysManager.RegisterHotKey(Handle, 100, HotKeysManager.KeyModifiers.Ctrl, Keys.C);
            //注册热键Ctrl+V，Id号为101。
            HotKeysManager.RegisterHotKey(Handle, 101, HotKeysManager.KeyModifiers.Ctrl, Keys.V);
        }

        private void dgvIETables_MouseLeave(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 100);
            //注销Id号为101的热键设定
            HotKeysManager.UnregisterHotKey(Handle, 101);
        }

        private void butSyncScope_Click(object sender, EventArgs e)
        {
            string Scopestr = this.txtScope.Text.Trim();
            if (Scopestr == "") return;
            for (int i = 0; i < this.dgvIETables.Rows.Count; i++)
            {
                if (this.dgvIETables.Rows[i].Cells[0].Selected)
                {
                    this.dgvIETables.Rows[i].Cells[0].Value = Scopestr;
                }

            }


        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {

            if (this.dgv == null)
            {
                return;
            }


            //  Clipboard.SetDataObject(dgv.CurrentCell.Value.ToString());
            Clipboard.SetDataObject(Convert.ToString( this.dgv.CurrentCell.Value));
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
          //  this.dgv = (DataGridView)sender;
         //   if (this.dgv == null)
         //   {
         //       return;
          //  }

         //   Clipboard.SetDataObject(this.dgv.GetClipboardContent());
        }

        private void dgvIETables_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }

            this.tomenuRight(dgv, e);
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
                    MenuRight.Show(MousePosition.X, MousePosition.Y);

                }
            }
        }

        private void delteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dgvIETables.Rows.Insert(this.dgvIETables.CurrentRow.Index);
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvIETables.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.dgvIETables.Rows.Remove(row);
                }
            }
        }

        private void dgvIETables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            if (columnIndex <= -1 || rowIndex <= -1) return;
            if( columnIndex ==0)
            {
                this.rbPasteScope.Checked = true;
            }
            if ( columnIndex == 2)
            {
                this.rbPastePartName.Checked = true;
            }
            if (columnIndex == 4)
            {
                this.rbPastePartRemark.Checked = true;
            }
            if (columnIndex == 7)
            {
                this.rbPasteAverageSecond.Checked = true;
            }
        }

        private void txtStyleNumber_TextChanged_1(object sender, EventArgs e)
        {
            //\\172.16.2.162\picture$\
           // this.getFilesName();

        }

        public void getPicture()
        {
            //获取当前程序所在的文件路径
            // String rootPath = Directory.GetCurrentDirectory();
            //string parentPath = Directory.GetParent(rootPath).FullName;//上级目录
            //  string topPath = Directory.GetParent(parentPath).FullName;//上上级目录
            // string topPath = @"\\172.16.2.162\picture$\";//上上级目录
            string path = @"\\172.16.2.162\picture$";
            bool status = false;

            //连接共享目录
            status = connectState(path, "administrator", "");
            if (status)
            {
                //共享文件夹的目录
                DirectoryInfo theFolder = new DirectoryInfo(path);
                string filename = theFolder.ToString();
                //执行方法
                //   TransportLocalToRemote(@"D:\readme1.txt", filename, "readme1.txt");  //实现将本地文件写入到远程服务器
                //  TransportRemoteToLocal(@"D:\readme.txt", filename, "readme.txt");    //实现将远程服务器文件写入到本地
                List<string> fileList = this.GetFileList(path);
                this.fileLists =  fileList;


            }
            else
            {
                MessageBox.Show("未能连接图片服务器");
               // return null;
                //  Console.WriteLine("未能连接！");
            }
          //  Console.WriteLine("成功");
           // Console.ReadKey();

        }

        /*

        public void setLectraNumber()  // CallBackStyle
        {
            string LectraNumber = "";
            if (this.txtStyleNumber.InvokeRequired )
            {
                // string styleName = this.txtStyleNumber.Text.Trim();
                getLectraNumber cb = new getLectraNumber(setLectraNumber);
                // this.Invoke(cb );
                IAsyncResult result =  this.BeginInvoke(cb);


            }
            else
            {
                string styleName = this.txtStyleNumber.Text.Trim();
                this.txtLectra.Text = iem.getLectraNumber(styleName);
                LectraNumber = this.txtLectra.Text.Trim();
                if (LectraNumber.Length <= 0)
                {
                    this.txtLectra.Text = "";
                    this.txtStyleNumber.Focus();
                  //  MessageBox.Show("没有找到版号，请输入正确的款式", "款式号错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  //  return;

                }

            }
          //  return LectraNumber;

        }
        */

        /*

        public void CallWhenDone(IAsyncResult iar)
        {
            AsyncResult ar = (AsyncResult)iar;
         //   getLectraNumber cb = new getLectraNumber(setLectraNumber);

            getLectraNumber del = (getLectraNumber)ar.AsyncDelegate;  //从参数中获取我们的委托函数
              del.EndInvoke(iar);
            string LectraNumber = this.txtLectra.Text.Trim();
            if (LectraNumber.Length <= 0)
            {
                  MessageBox.Show("没有找到版号，请输入正确的款式", "款式号错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  return;
            }


        }
        */

        public static bool connectState(string path)
        {
            return connectState(path, "", "");
        }
        /// <summary>
        /// 连接远程共享文件夹
        /// </summary>
        /// <param name="path">远程共享文件夹的路径</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = "net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }



        public   List<string>  GetFileList(string path)
        {
            List<string> fileList = new List<string>();

            if (Directory.Exists(path) == true)
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    fileList.Add(file);
                }

                foreach (string directory in Directory.GetDirectories(path))
                {
                    fileList.AddRange(GetFileList(directory));
                }
            }

            return fileList;
        }

        private void ckAutoPicture_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (this.ckAutoPicture.Checked)
            {
                List<string> fileList = this.getFilesName();
                if (fileList != null && fileList.Count > 0)
                {
                    this.fileLists = fileList;

                }
            }
            */
        }

        private void txtStyleNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                txtStyleNumber_Validated(sender, e);//触发button事件
            }
        }



        private void txtStyleSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.searchIEBom("0");
            }
        }

        private void labModifyHistory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("待开发...");
            return;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.calculate();
        }

        // 或取所有的群组
        private void ckChangeGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckChangeGroup.Checked == false)
            {
                this.txtNewGroupName.Enabled = false;
                this.cbGroupStyleName.Enabled = false;
                this.cbSameGroupStyles.Enabled = false;
                return;
            }
            this.txtNewGroupName.Enabled = true;
            this.cbGroupStyleName.Enabled = true;
            this.cbSameGroupStyles.Enabled = true;

            this.selectVer = 0;
            this.txtNewGroupName.Text = "";
            this.cbGroupStyleName.Items.Clear();
            this.cbSameGroupStyles.Items.Clear();
            this.txtStyleGroup.Text = "";

            DataTable SourceDT = iem.searchGroupAll( );
            if (SourceDT != null && SourceDT.Rows.Count > 0)
            {
                string[] name =  { "groupname" };
                DataTable groupnameDT = GetDistinctTable(SourceDT, name);

                for (int i = 0; i < groupnameDT.Rows.Count; i++)
                {
                    string groupname = groupnameDT.Rows[i]["groupname"].ToString();
                    this.cbGroupStyleName.Items.Add(groupname);
                }

                string[] style = { "groupstyle" };
                DataTable groupStyleDT = GetDistinctTable(SourceDT, style);

                for (int i = 0; i < groupStyleDT.Rows.Count; i++)
                {
                    string groupstyle = groupStyleDT.Rows[i]["groupstyle"].ToString();
                    this.cbSameGroupStyles.Items.Add(groupstyle);
                }

            }



        }

        private void cbGroupStyleName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.selectVer != 0 || this.changeGroupName !=0) return;
            if (this.ckChangeGroup.Checked == false) return;
            if(this.cbGroupStyleName.Items == null || this.cbGroupStyleName.Items.Count < 0)
            {
                return;
            }


            // this.cbGroupStyleName.Items.Clear();
            // this.cbSameGroupStyles.Items.Clear();
            this.txtStyleGroup.Text = "";
            string groupStyleName = this.cbGroupStyleName.SelectedItem.ToString();  // 群组名
            this.txtNewGroupName.Text = groupStyleName;
            string groupstyles = "";
            int ItemIndex = -1;


            DataTable SourceDT = iem.searchGroupAll();
            if (SourceDT != null && SourceDT.Rows.Count > 0)
            {
                for (int i = 0; i < SourceDT.Rows.Count; i++)
                {
                    if (SourceDT.Rows[i]["groupname"].ToString() == groupStyleName)
                    {
                        ItemIndex = i;
                        string groupstyle = SourceDT.Rows[i]["groupstyle"].ToString();
                        groupstyles = groupstyles + "," + groupstyle;
                    }


                }
                if (groupstyles.Length > 1)
                {
                    groupstyles = groupstyles.Substring(1, groupstyles.Length - 1);
                    this.txtStyleGroup.Text = groupstyles;
                }



                this.cbSameGroupStyles.SelectedIndex = ItemIndex;
                this.changeGroupName = 1;

            }
}

        #region datatable去重
        /// <summary>
        /// datatable去重
        /// </summary>
        /// <param name="dtSource">需要去重的datatable</param>
        /// <param name="columnNames">依据哪些列去重</param>
        /// <returns></returns>
        public static DataTable GetDistinctTable(DataTable dtSource, params string[] columnNames)
        {
            DataTable distinctTable = dtSource.Clone();
            try
            {
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtSource);
                    distinctTable = dv.ToTable(true, columnNames);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            return distinctTable;
        }

        /// <summary>
        /// datatable去重
        /// </summary>
        /// <param name="dtSource">需要去重的datatable</param>
        /// <returns></returns>
        public static DataTable GetDistinctTable(DataTable dtSource)
        {
            DataTable distinctTable = null;
            try
            {
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    string[] columnNames = GetTableColumnName(dtSource);
                    DataView dv = new DataView(dtSource);
                    distinctTable = dv.ToTable(true, columnNames);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            return distinctTable;
        }

        #endregion

        #region 获取表中所有列名
        public static string[] GetTableColumnName(DataTable dt)
        {
            string cols = string.Empty;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                cols += (dt.Columns[i].ColumnName + ",");
            }
            cols = cols.TrimEnd(',');
            return cols.Split(',');
        }
        #endregion

        private void cbSameGroupStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.selectVer != 0 || this.changeStyleName != 0) return;
            if (this.ckChangeGroup.Checked == false) return;
            if (this.cbGroupStyleName.Items == null || this.cbGroupStyleName.Items.Count < 0)
            {
                return;
            }


            this.txtStyleGroup.Text = "";
            string groupstyles = "";
            string sameGroupStyles = this.cbSameGroupStyles.SelectedItem.ToString();  // 同款式名
            string groupStyleName = ""; // 群组名
            int ItemIndex = -1;
            DataTable SourceDT = iem.searchGroupAll();
            if (SourceDT != null && SourceDT.Rows.Count > 0)
            {
                for (int i = 0; i < SourceDT.Rows.Count; i++)
                {
                    if (SourceDT.Rows[i]["groupstyle"].ToString() == sameGroupStyles)
                    {

                        groupStyleName = SourceDT.Rows[i]["groupname"].ToString();
                     //   this.cbSameGroupStyles.SelectedIndex = i;
                       // string groupstyle = SourceDT.Rows[i]["groupstyle"].ToString();
                       // groupstyles = groupstyles + "," + groupstyle;
                    }
                }


                for (int i = 0; i < SourceDT.Rows.Count; i++)
                {
                    if (SourceDT.Rows[i]["groupname"].ToString() == groupStyleName)
                    {

                        string groupstyle = SourceDT.Rows[i]["groupstyle"].ToString();
                        groupstyles = groupstyles + "," + groupstyle;
                    }
                }

                for (int i = 0; i < this.cbGroupStyleName.Items.Count; i++)
                {
                    if (this.cbGroupStyleName.Items[i].ToString() == groupStyleName)
                    {
                        ItemIndex = i;

                    }
                }


                if (groupstyles.Length > 1)
                {
                    groupstyles = groupstyles.Substring(1, groupstyles.Length - 1);
                    this.txtStyleGroup.Text = groupstyles;
                }
            }


            this.changeStyleName = 1;
            this.cbGroupStyleName.SelectedIndex = ItemIndex;
            this.txtNewGroupName.Text = groupStyleName;

        }

        private void cbGroupStyleName_Click(object sender, EventArgs e)
        {
            this.changeGroupName = 0;
        }

        private void cbSameGroupStyles_Click(object sender, EventArgs e)
        {

            this.changeStyleName = 0;
        }
    }

}