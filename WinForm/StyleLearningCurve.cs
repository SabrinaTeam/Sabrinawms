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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class StyleLearningCurve : Form
    {
        private static StyleLearningCurve frm;
        public StyleLearningCurveManager slcm = new StyleLearningCurveManager();
        public string CurveTableName = "";
        public DataGridView dgv = null;

        public int CureNamesID = -1;
        public string Creator = "";
        public string CreateDate = "";

        public int hiedcolumnindex = -1; //是否选中外面
        public StyleLearningCurve()
        {
            InitializeComponent();
            dgvStandardNewModulus.DoubleBufferedDataGirdView(true);
            dgvStandardOldModulus.DoubleBufferedDataGirdView(true);
            dgvCapacityReport.DoubleBufferedDataGirdView(true);
        }
        public static StyleLearningCurve GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new StyleLearningCurve();
            }
            return frm;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StyleLearningCurve_Load(object sender, EventArgs e)
        {
            gbCapacityReport.Visible = false;
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 添加IE表清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCurveNames_Click(object sender, EventArgs e)
        {
            cbCurveNames.Items.Clear();
            List<StyleLearingCurve> names = slcm.getCurveNames();
            if(names == null || names.Count<=0)
            {
                MessageBox.Show("没有IE表，请新增");
                return;
            }
            foreach (StyleLearingCurve item in names)
            {
                cbCurveNames.Items.Add(item.modulusName);
            }

        }



        private void butLoadLearningCurve_Click(object sender, EventArgs e)
        {
            if(cbCurveNames.SelectedIndex < 0)
            {
                return;
            }
            this.CurveTableName = cbCurveNames.SelectedItem.ToString();
            int CurveNameID = -1;
            List<DataTable> ldt = new List<DataTable>();
            //DataTable newStyle = new DataTable();
           // DataTable oldStyle = new DataTable();
            if (this.CurveTableName.Length > 0)
            {
                CurveNameID = slcm.getLearningByCurveName(this.CurveTableName);
            }
            if (CurveNameID > 0)
            {
                ldt = slcm.getStandardModulusByCurveNameID(CurveNameID);
            }
            if (ldt.Count >= 2)
            {
                //newStyle = ldt[0];
                //  oldStyle = ldt[1];
                this.dgvStandardNewModulus.DataSource =null;
                this.dgvStandardOldModulus.DataSource = null;
                this.dgvStandardNewModulus.DataSource = ldt[0];
                this.dgvStandardOldModulus.DataSource = ldt[1];
                this.changStandardNewModulusHeaderText();
                this.changStandardOldModulusHeaderText();

                this.gbStandardNewModulus.Text = this.CurveTableName + " - 新款";
                this.gbStandardOldModulus.Text = this.CurveTableName + " - 老款";

            }
        }


        /// <summary>
        /// 打开新增IE表权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butAddLearningCurve_Click(object sender, EventArgs e)
        {
            this.dgvStandardNewModulus.ReadOnly = false;
            this.dgvStandardOldModulus.ReadOnly = false;
            this.dgvStandardNewModulus.AllowUserToAddRows = true;
            this.dgvStandardOldModulus.AllowUserToAddRows = true;
            this.gbCurveTableName.Visible = true;
            this.gbMemu.Enabled = false;
            this.gbProductionCapacity.Enabled = false;
            this.gbStandardNewModulus.Enabled = false;
            this.gbStandardOldModulus.Enabled = false;
            this.gbStandardNewModulus.Enabled = false;


        }



        /// <summary>
        /// 新款难易系数转大写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStandardNewModulus_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex =e.RowIndex;
            int cellindex = e.ColumnIndex;
            if (cellindex == 1)
            {
                this.dgvStandardNewModulus.Rows[rowindex].Cells[cellindex].Value =
                    this.dgvStandardNewModulus.Rows[rowindex].Cells[cellindex].Value.ToString().ToUpper();

            }

        }

        /// <summary>
        /// 新款行离开时保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStandardNewModulus_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
          //  this.dgvStandardNewModulus.CurrentCell = null;
            //MessageBox.Show("保存数据");
        }


        /// <summary>
        /// 新款删除最后一行空白行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStandardNewModulus_Validated(object sender, EventArgs e)
        {
            this.dgvStandardNewModulus.AllowUserToAddRows = false;
            this.dgvStandardNewModulus.EndEdit();
            this.dgvStandardNewModulus.ReadOnly = true;

        }

        /// <summary>
        /// 新款点击回来时继续添加内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void dgvStandardNewModulus_Enter(object sender, EventArgs e)
        {
            if (!this.dgvStandardNewModulus.ReadOnly)
            {
                this.dgvStandardNewModulus.AllowUserToAddRows = true;
            }

        }



        /// <summary>
        /// 老款难易系数转大写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStandardOldModulus_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            int cellindex = e.ColumnIndex;
            if (cellindex == 1)
            {
                this.dgvStandardOldModulus.Rows[rowindex].Cells[cellindex].Value =
                    this.dgvStandardOldModulus.Rows[rowindex].Cells[cellindex].Value.ToString().ToUpper();

            }

        }


        /// <summary>
        /// 老款删除最后一行空白行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStandardOldModulus_Validated(object sender, EventArgs e)
        {
            this.dgvStandardOldModulus.AllowUserToAddRows = false;
        }


        /// <summary>
        /// 老款离开行时保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStandardOldModulus_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //  this.dgvStandardNewModulus.CurrentCell = null;
            //MessageBox.Show("保存数据");

        }


        /// <summary>
        /// 老款点击回来时继续添加内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStandardOldModulus_Enter(object sender, EventArgs e)
        {
            if (!this.dgvStandardOldModulus.ReadOnly)
            {
                this.dgvStandardOldModulus.AllowUserToAddRows = true;
            }
        }

        private void butCurveCancel_Click(object sender, EventArgs e)
        {
            this.txtCurveTableName.Text = "";
            this.gbCurveTableName.Visible = false;
            this.dgvStandardNewModulus.ReadOnly = true;
            this.dgvStandardOldModulus.ReadOnly = true;
            this.dgvStandardNewModulus.AllowUserToAddRows = false;
            this.dgvStandardOldModulus.AllowUserToAddRows = false;

            this.gbMemu.Enabled = true;
            this.gbProductionCapacity.Enabled = true;
            this.gbStandardNewModulus.Enabled = true;
            this.gbStandardOldModulus.Enabled = true;
        }

        private void butCurveConfirm_Click(object sender, EventArgs e)
        {
            this.CurveTableName = this.txtCurveTableName.Text;
            this.gbStandardNewModulus.Text = this.CurveTableName + " - 新款";
            this.gbStandardOldModulus.Text = this.CurveTableName + " - 老款";
            this.txtCurveTableName.Text = "";
            this.gbCurveTableName.Visible = false;
            this.gbMemu.Enabled = true;
            this.gbProductionCapacity.Enabled = true;
            this.gbStandardNewModulus.Enabled = true;
            this.gbStandardOldModulus.Enabled = true;


            DataTable dt = slcm.addStandardModulus();
            if (dt.Rows.Count > 0)
            {
                this.dgvStandardNewModulus.DataSource = null;
                 this.dgvStandardNewModulus.DataSource = dt;
                changStandardNewModulusHeaderText();
            }
             this.dgvStandardNewModulus.AllowUserToAddRows = false;
             this.dgvStandardNewModulus.ReadOnly = true;


            DataTable oldDt = slcm.addOldStandardModulus();
            if (dt.Rows.Count > 0)
            {
                this.dgvStandardOldModulus.DataSource = null;
                this.dgvStandardOldModulus.DataSource = oldDt;
                changStandardOldModulusHeaderText();
            }
            this.dgvStandardOldModulus.AllowUserToAddRows = false;
            this.dgvStandardOldModulus.ReadOnly = true;
        }
        public void changStandardNewModulusHeaderText()
        {

            this.dgvStandardNewModulus.Columns["COldArea"].HeaderText = "区间 (分钟)";
            this.dgvStandardNewModulus.Columns["COldlevel"].HeaderText = "难易等级";
            this.dgvStandardNewModulus.Columns["COldSingleMinute"].HeaderText = "单件时长 (分钟)";

            this.dgvStandardNewModulus.Columns["COldratio"].HeaderText = "学系率";
            this.dgvStandardNewModulus.Columns["COldratio"].DefaultCellStyle.Format = "0.00000";

            this.dgvStandardNewModulus.Columns["COldOneday"].HeaderText = "上线第 01 天";
            this.dgvStandardNewModulus.Columns["COldTwoDay"].HeaderText = "上线第 02 天";
            this.dgvStandardNewModulus.Columns["COldThreeDay"].HeaderText = "上线第 03 天";
            this.dgvStandardNewModulus.Columns["COldFourDay"].HeaderText = "上线第 04 天";
            this.dgvStandardNewModulus.Columns["COldFiveDay"].HeaderText = "上线第 05 天";
            this.dgvStandardNewModulus.Columns["COldSixDay"].HeaderText = "上线第 06 天";
            this.dgvStandardNewModulus.Columns["COldSevenDay"].HeaderText = "上线第 07 天";

            this.dgvStandardNewModulus.Columns["COldEightDay"].HeaderText = "上线第 08 天";
            this.dgvStandardNewModulus.Columns["COldNineDay"].HeaderText = "上线第 09 天";
            this.dgvStandardNewModulus.Columns["COldTenDay"].HeaderText = "上线第 10 天";
            this.dgvStandardNewModulus.Columns["COldElevenDay"].HeaderText = "上线第 11 天";
            this.dgvStandardNewModulus.Columns["COldTwelveDay"].HeaderText = "上线第 12 天";
            this.dgvStandardNewModulus.Columns["COldThirteenDay"].HeaderText = "上线第 13 天";
            this.dgvStandardNewModulus.Columns["COldFourteenDay"].HeaderText = "上线第 14 天";

            this.dgvStandardNewModulus.Columns["COldTwelveDay"].Visible = false;
            this.dgvStandardNewModulus.Columns["COldThirteenDay"].Visible = false;
            this.dgvStandardNewModulus.Columns["COldFourteenDay"].Visible = false;



            this.dgvStandardNewModulus.Columns["COldOneday"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldTwoDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldThreeDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldFourDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldFiveDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldSixDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldSevenDay"].DefaultCellStyle.Format = "0.00%";

            this.dgvStandardNewModulus.Columns["COldEightDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldNineDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldTenDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldElevenDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldTwelveDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldThirteenDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardNewModulus.Columns["COldFourteenDay"].DefaultCellStyle.Format = "0.00%";

            this.dgvStandardNewModulus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvStandardNewModulus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvStandardNewModulus.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
        }

        public void changStandardOldModulusHeaderText()
        {

            this.dgvStandardOldModulus.Columns["COldArea"].HeaderText = "区间 (分钟)";
            this.dgvStandardOldModulus.Columns["COldlevel"].HeaderText = "难易等级";
            this.dgvStandardOldModulus.Columns["COldSingleMinute"].HeaderText = "单件时长 (分钟)";

            this.dgvStandardOldModulus.Columns["COldratio"].HeaderText = "学系率";
            this.dgvStandardOldModulus.Columns["COldratio"].DefaultCellStyle.Format = "0.00000";

            this.dgvStandardOldModulus.Columns["COldOneday"].HeaderText = "上线第 01 天";
            this.dgvStandardOldModulus.Columns["COldTwoDay"].HeaderText = "上线第 02 天";
            this.dgvStandardOldModulus.Columns["COldThreeDay"].HeaderText = "上线第 03 天";
            this.dgvStandardOldModulus.Columns["COldFourDay"].HeaderText = "上线第 04 天";
            this.dgvStandardOldModulus.Columns["COldFiveDay"].HeaderText = "上线第 05 天";
            this.dgvStandardOldModulus.Columns["COldSixDay"].HeaderText = "上线第 06 天";
            this.dgvStandardOldModulus.Columns["COldSevenDay"].HeaderText = "上线第 07 天";

            this.dgvStandardOldModulus.Columns["COldEightDay"].HeaderText = "上线第 08 天";
            this.dgvStandardOldModulus.Columns["COldNineDay"].HeaderText = "上线第 09 天";
            this.dgvStandardOldModulus.Columns["COldTenDay"].HeaderText = "上线第 10 天";
            this.dgvStandardOldModulus.Columns["COldElevenDay"].HeaderText = "上线第 11 天";
            this.dgvStandardOldModulus.Columns["COldTwelveDay"].HeaderText = "上线第 12 天";
            this.dgvStandardOldModulus.Columns["COldThirteenDay"].HeaderText = "上线第 13 天";
            this.dgvStandardOldModulus.Columns["COldFourteenDay"].HeaderText = "上线第 14 天";

            this.dgvStandardOldModulus.Columns["COldNineDay"].Visible = false;
            this.dgvStandardOldModulus.Columns["COldTenDay"].Visible = false;
            this.dgvStandardOldModulus.Columns["COldElevenDay"].Visible = false;
            this.dgvStandardOldModulus.Columns["COldTwelveDay"].Visible = false;
            this.dgvStandardOldModulus.Columns["COldThirteenDay"].Visible = false;
            this.dgvStandardOldModulus.Columns["COldFourteenDay"].Visible = false;



            this.dgvStandardOldModulus.Columns["COldOneday"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldTwoDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldThreeDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldFourDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldFiveDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldSixDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldSevenDay"].DefaultCellStyle.Format = "0.00%";

            this.dgvStandardOldModulus.Columns["COldEightDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldNineDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldTenDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldElevenDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldTwelveDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldThirteenDay"].DefaultCellStyle.Format = "0.00%";
            this.dgvStandardOldModulus.Columns["COldFourteenDay"].DefaultCellStyle.Format = "0.00%";

            this.dgvStandardOldModulus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvStandardOldModulus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvStandardOldModulus.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
        }

        private void dgvStandardNewModulus_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
            if (this.dgv == null)
            {
                return;
            }


            //  Clipboard.SetDataObject(dgv.CurrentCell.Value.ToString());
            Clipboard.SetDataObject(this.dgv.CurrentCell.Value.ToString());
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            if (this.dgv == null)
            {
                return;
            }

            Clipboard.SetDataObject(this.dgv.GetClipboardContent());
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            if (this.dgv == null)
            {
                return;
            }

            ImproExcel(this.dgv);
        }
        public void ImproExcel(DataGridView dgv)
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
            tabl = GetDgvToTable(dgv);

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

        private void dgvStandardNewModulus_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }

            this.tomenuRight(dgv, e);
        }

        private void dgvStandardOldModulus_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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

        private void dgvStandardNewModulus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            int ColumnIndex = e.ColumnIndex;
            this.dgvStandardNewModulus.ReadOnly = false;
            this.dgvStandardNewModulus.Rows[RowIndex].Cells[ColumnIndex].ReadOnly = false;
            this.dgvStandardNewModulus.EditMode = DataGridViewEditMode.EditOnEnter;

        }

        private void butSaveLearningCurve_Click(object sender, EventArgs e)
        {
            if(this.dgvStandardNewModulus.Rows.Count <= 0  || this.dgvStandardOldModulus.Rows.Count <=0)
            {
                return;
            }

            if(CurveTableName.Length <= 0)
            {
                return;
            }
             this.CureNamesID = slcm.getLearningByCurveName(CurveTableName);
            DataTable newDt = (DataTable)this.dgvStandardNewModulus.DataSource;
            DataTable oldDt = (DataTable)this.dgvStandardOldModulus.DataSource;

            // -1 没有保存过  新增
            if(this.CureNamesID == -1)
            {
                this.CureNamesID = slcm.saveLearningCurveName(CurveTableName);
            }
            if (this.CureNamesID > 0)
            {
                //先删除旧数据  再保存新数据
                int delRows = slcm.delStandardModulusByCurveNameID(this.CureNamesID);
                string insetNewStylec = slcm.saveLearningCurve(this.CureNamesID, newDt,  this.Creator, this.CreateDate, 0);
                string insetOldStylec = slcm.saveLearningCurve(this.CureNamesID, oldDt, this.Creator, this.CreateDate, 1);

                if(insetNewStylec != "0")
                {
                    MessageBox.Show("保存新款曲线学习表失败。");
                }else if (insetOldStylec != "0")
                {
                    MessageBox.Show("保存老款曲线学习表失败。");
                }
                else
                {
                    MessageBox.Show("保存成功。");
                }

            }
            else
            {
                MessageBox.Show("保存失败,请重试");
            }

        }

        private void dgvStandardOldModulus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            int ColumnIndex = e.ColumnIndex;
            this.dgvStandardOldModulus.ReadOnly = false;
            this.dgvStandardOldModulus.Rows[RowIndex].Cells[ColumnIndex].ReadOnly = false;
            this.dgvStandardOldModulus.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void dgvStandardOldModulus_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void butProductionCapacity_Click(object sender, EventArgs e)
        {



            bool styleNew = false;
            bool styleOld = false;
            bool Hour8 = false;
            bool Hour10 = false;
            int counts = 0;
            string txtGroupCounts = this.txtGroupCounts.Text.ToString();
            if (!string.IsNullOrEmpty(txtGroupCounts))
            {
                counts = Convert.ToInt32(txtGroupCounts);
            }

            if (cbNewStyle.Checked)
            { styleNew = true; }
            else { styleNew = false; }

            if (cbOldStyle.Checked)
            { styleOld = true; }
            else { styleOld = false; }

            if (cb8Hour.Checked)
            { Hour8 = true; }
            else { Hour8 = false; }

            if (cb10Hour.Checked)
            { Hour10 = true; }
            else { Hour10 = false; }

            CalculatProductivityParameters cpps = new CalculatProductivityParameters();
            cpps.counts = counts;
            cpps.styleNew = styleNew;
            cpps.styleOld = styleOld;
            cpps.Hour10 = Hour10;
            cpps.Hour8 = Hour8;


            DataTable StandardNew = (DataTable)this.dgvStandardNewModulus.DataSource;
            DataTable StandardOld = (DataTable)this.dgvStandardOldModulus.DataSource;
            DataTable ProducTivitys =   slcm.CalculatProductivity(cpps, StandardNew, StandardOld);
            if (ProducTivitys.Rows.Count > 0)
            {
                if (this.butProductionCapacity.Text == "》计算产能")
                {
                    this.butProductionCapacity.Text = "《学习曲线表";
                    this.gbCapacityReport.Visible = true;
                }
                else
                {
                    this.butProductionCapacity.Text = "》计算产能";
                    this.gbCapacityReport.Visible = false;
                    return;
                }

                this.dgvCapacityReport.DataSource = null;
                this.dgvCapacityReport.DataSource = ProducTivitys;
                this.gbCapacityReport.Visible = true;
                this.gbCapacityReport.Left = 3;
                this.gbCapacityReport.Top = 70;
                this.gbCapacityReport.Width = this.Width - 20;
                this.gbCapacityReport.Height = this.Height - 110;
                this.dgvCapacityReport.ReadOnly = true;
                this.dgvCapacityReport.AllowUserToAddRows = false;
                this.dgvCapacityReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                this.changCapacityReportHeaderText();
            }



        }

        private void dgvCapacityReport_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvCapacityReport_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }

            this.tomenuRight(dgv, e);
        }


        public void changCapacityReportHeaderText()
        {

            this.dgvCapacityReport.Columns["StyleHours"].HeaderText = "款式/工作时间";
            this.dgvCapacityReport.Columns["CArea"].HeaderText = "区间";
            this.dgvCapacityReport.Columns["Clevel"].HeaderText = "难易等级";
            this.dgvCapacityReport.Columns["CSingleMinute"].HeaderText = "单件时长 (分钟)";
            this.dgvCapacityReport.Columns["Cratio"].HeaderText = "学系率";
            this.dgvCapacityReport.Columns["Cratio"].DefaultCellStyle.Format = "0.00000";
            this.dgvCapacityReport.Columns["CalculatProductivitys"].HeaderText = "人均日产能";


            this.dgvCapacityReport.Columns["COnedayGroup"].HeaderText = "第 01 天组产能";
            this.dgvCapacityReport.Columns["CTwoDayGroup"].HeaderText = "第 02 天组产能";
            this.dgvCapacityReport.Columns["CThreeDayGroup"].HeaderText = "第 03 天组产能";
            this.dgvCapacityReport.Columns["CFourDayGroup"].HeaderText = "第 04 天组产能";
            this.dgvCapacityReport.Columns["CFiveDayGroup"].HeaderText = "第 05 天组产能";
            this.dgvCapacityReport.Columns["CSixDayGroup"].HeaderText = "第 06 天组产能";
            this.dgvCapacityReport.Columns["CSevenDayGroup"].HeaderText = "第 07 天组产能";

            this.dgvCapacityReport.Columns["CEightDayGroup"].HeaderText = "第 08 天组产能";
            this.dgvCapacityReport.Columns["CNineDayGroup"].HeaderText = "第 09 天组产能";
            this.dgvCapacityReport.Columns["CTenDayGroup"].HeaderText = "第 10 天组产能";
            this.dgvCapacityReport.Columns["CElevenDayGroup"].HeaderText = "第 11 天组产能";
            this.dgvCapacityReport.Columns["CTwelveDayGroup"].HeaderText = "第 12 天组产能";
            this.dgvCapacityReport.Columns["CThirteenDayGroup"].HeaderText = "第 13 天组产能";
            this.dgvCapacityReport.Columns["CFourteenDayGroup"].HeaderText = "第 14 天组产能";



            this.dgvCapacityReport.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvCapacityReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCapacityReport.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

            for (int i = 0; i < this.dgvCapacityReport.Rows.Count; i++)
            {
                for (int j = 5; j < this.dgvCapacityReport.Columns.Count; j++)
                {
                    if (this.dgvCapacityReport.Rows[i].Cells[j].Value.ToString() == "0")
                    {
                        this.dgvCapacityReport.Rows[i].Cells[j].Value = this.dgvCapacityReport.Rows[i].Cells[j - 1].Value;

                    }
                }


            }


        }

        private void StyleLearningCurve_SizeChanged(object sender, EventArgs e)
        {
            gbStandardNewModulus.Width = this.Width - 20;
            gbStandardOldModulus.Width = this.Width - 20;
            gbStandardOldModulus.Height = this.Height - this.gbStandardNewModulus.Height - this.gbMemu.Height - 30;
            dgvStandardNewModulus.RowHeadersWidth = 30;    //设定左侧空列的宽度
            dgvStandardOldModulus.RowHeadersWidth = 30;
            dgvStandardNewModulus.ReadOnly = true;
            dgvStandardOldModulus.ReadOnly = true;
            this.gbCurveTableName.Visible = false;
           //
        }
    }
}
