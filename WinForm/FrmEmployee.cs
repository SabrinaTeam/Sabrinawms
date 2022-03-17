using BLL;
using COMMON;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmEmployee : Form
    {
        public DataGridView dgv = null;
        public EmployeeManager em = new EmployeeManager();
        public int hiedcolumnindex = -1;
        //是否选中外面

        public string pNumber = "";
        public List<string> pNumbers = new List<string>();
        private static FrmEmployee frm;
        
        public List<Employees> emps = new List<Employees>();
        private string org = "";

        // 根据厂区查询部门
        private int rowIndex = -1; // 选中的行号

        public FrmEmployee()
        {
            InitializeComponent();
            this.dgvEmployee.DoubleBufferedDataGirdView(true);
            this.dgvEmployee.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
        }

        public static FrmEmployee GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmEmployee();
            }
            return frm;
        }

        public void addData(string value, int rowInded, int columnsInded)
        {
            if (columnsInded == 0)
            {
                string[] strTmp = { };
                strTmp = new string[] { "SAA", "TOP" };
                DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();
                combo.DataSource = strTmp;
                combo.Value = value;
                this.org = value;
                this.dgvEmployee.Rows[rowInded].Cells[columnsInded] = combo;
            }
            else if (columnsInded == 1)
            {
                List<string> strTmp = new List<string>();
                List<T_dept> depts = em.getDetp(org);
                DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();
                foreach (T_dept item in depts)
                {
                    strTmp.Add(item.deptName);
                }

                combo.DataSource = strTmp;
                combo.Value = value;
                this.dgvEmployee.Rows[rowInded].Cells[columnsInded] = combo;
            }
            else
            {
                this.dgvEmployee.Rows[rowInded].Cells[columnsInded].Value = value;
            }
        }

        /// <summary>
        /// 绑定第二列
        /// </summary>
        /// <param name="column"></param>
        /// <param name="strTmp"></param>
        public void DgvCombobox(ref DataGridViewComboBoxColumn column, string[] strTmp)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("tmp", typeof(string));
            dt.Columns.Add(dc);
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            DataRow dr;
            for (int i = 0; i < strTmp.Length; i++)
            {
                dr = dt.NewRow();
                dr["tmp"] = strTmp[i];
                dr["id"] = i;
                dt.Rows.Add(dr);
            }
            //为combobox绑定生成的表
            column.DataSource = dt; //combobox列的数据源，绑定为生成的表
            column.DisplayMember = "tmp";//要显示的名称，表的文字例
            column.ValueMember = dt.Columns[1].ToString();//文字对应的值，此列将和columns2.DataPropertyName 属性的值对应来显示选中的值
        }

        public void getDetps(string org)
        {
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

        public void ImproExcel(DataGridView selectDgv)
        {
            SaveFileDialog sdfExport = new SaveFileDialog();
            sdfExport.Filter = "Excel 97-2003文件|*.xls|Excel 2007文件|*.xlsx";
            //   sdfExport.ShowDialog();
            if (sdfExport.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            String filename = sdfExport.FileName;
            String tableName = "";
            NPOIExcelCompletedToMes NPOIexcel = new NPOIExcelCompletedToMes();
            DataTable tabl = new DataTable();
            tabl = GetDgvToTable(this.dgv);
            if (this.dgv == this.dgvEmployee)
            {
                tableName = "dgvEmployee";
            }

            NPOIexcel.ExcelWrite(filename, tabl, tableName);//excelhelper写出
            if (MessageBox.Show("导出成功，文件保存在" + filename.ToString() + ",是否打开此文件？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (File.Exists(filename))//文件是否存在
                {
                    Process.Start(filename);//执行打开导出的文件
                }
                else
                {
                    MessageBox.Show("文件不存在！", "提示");
                    return;
                }
            }
        }

        public void init()
        {
            this.cborg2.SelectedItem = "";
            this.cbDept2.SelectedValue = -1;
            this.cbPosition.SelectedValue = -1;
            this.txtSubID.Text = "";
            this.txtWorkID.Text = "";
            this.txtPassportNumber2.Text = "";
            this.txtPassportVisaArea.Text = "";
            this.cbJobChange.SelectedValue = -1;
            this.txtUserName2.Text = "";
            this.txtUserID.Text = "";
            this.cbSex.SelectedValue = -1;
            this.txtHomeTown.Text = "";
            this.txtUsernameEN.Text = "";
            this.txtPhoneNumber.Text = "";
            this.cbEducation.SelectedValue = -1;
            this.ckHealthCard.Checked = false;
            this.ckWorkerCard.Checked = false;
            this.txtWorkCarID.Text = "";
            this.txtPassportVisaNumber.Text = "";
            this.cbPassportVisaTimeLimit.SelectedItem = "";
            this.txtPassportSignArea.Text = "";
            this.dtpPassportIssueDate.Text = "";
            this.dtpPassportFinishDate.Text = "";
            this.dtpPassportVisaFinshDate.Text = "";
            this.dtpEntryVisaDate.Text = "";
            this.dtpBirthday.Text = "";
            this.dtpEntryDate.Text = "";
            this.dtpTryFinishDate.Text = "";
            this.dtpContractFinishDate.Text = "";
            //this.dtpPlanResignDate.Text = "";
            // this.dtpResignDate.Text = "";
            this.dtpPlanResignDate.Checked = false;
            this.dtpResignDate.Checked = false;

            this.txtAssessDate.Text = "";
            this.txtAge.Text = "";
            this.txtSeniority.Text = "";
            this.ckResigned.Checked = false;
            this.txtResignNote.Text = "";
            this.ckMsgCheck.Checked = false;
            this.txtMsgTxt.Text = "";
            this.txtDid.Text = "";
            this.txtCid.Text = "";
            this.txtPid.Text = "";
        }

        public void saveEmployee()
        {
            DataTable empTb = GetDgvToTable(this.dgvEmployee);
            if (empTb.Rows.Count <= 0)
            {
                return;
            }
            /******************T_certified******************************/
            DataTable T_certified = new DataTable();
            DataColumn dc;
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "id";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "passportNumber";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "passportIssueDate";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "passportFinishDate";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "passportSignArea";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "passportVisaNumber";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "passportVisaArea";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "passportVisaTimeLimit";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "passportVisaFinshDate";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "entryVisaDate";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "workerCard";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "workerCardID";
            T_certified.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "healthCard";
            T_certified.Columns.Add(dc);

            /******************T_dept******************************/
            DataTable T_dept = new DataTable();
            DataColumn dd;
            dd = new DataColumn();
            dd.DataType = System.Type.GetType("System.Int32");
            dd.ColumnName = "id";
            T_dept.Columns.Add(dd);

            dd = new DataColumn();
            dd.DataType = System.Type.GetType("System.String");
            dd.ColumnName = "org";
            T_dept.Columns.Add(dd);

            dd = new DataColumn();
            dd.DataType = System.Type.GetType("System.String");
            dd.ColumnName = "deptName";
            T_dept.Columns.Add(dd);

            /******************T_employee******************************/
            DataTable T_employee = new DataTable();
            DataColumn de;
            de = new DataColumn();
            de.DataType = System.Type.GetType("System.Int32");
            de.ColumnName = "id";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "passportNumber";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.Int32");
            de.ColumnName = "deptID";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "subID";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "workID";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "userName";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "userNameEN";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.Int32");
            de.ColumnName = "userSexID";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "birthday";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "educationID";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "hometown";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "phoneNumber";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "positionID";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "entryDate";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "jobChange";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "assessDate";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "contractFinishDate";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "tryFinishDate";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "planResignDate";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "resignDate";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.String");
            de.ColumnName = "resignNote";
            T_employee.Columns.Add(de);

            de = new DataColumn();
            de.DataType = System.Type.GetType("System.Int32");
            de.ColumnName = "resigned";
            T_employee.Columns.Add(de);

            /********************T_msg****************************/
            DataTable T_msg = new DataTable();
            DataColumn dm;
            dm = new DataColumn();
            dm.DataType = System.Type.GetType("System.Int32");
            dm.ColumnName = "id";
            T_msg.Columns.Add(dm);

            dm = new DataColumn();
            dm.DataType = System.Type.GetType("System.String");
            dm.ColumnName = "workID";
            T_msg.Columns.Add(dm);

            dm = new DataColumn();
            dm.DataType = System.Type.GetType("System.String");
            dm.ColumnName = "msgTxt";
            T_msg.Columns.Add(dm);

            dm = new DataColumn();
            dm.DataType = System.Type.GetType("System.Int32");
            dm.ColumnName = "msgCheck";
            T_msg.Columns.Add(dm);

            /*****************T_education*******************************/
            DataTable T_education = new DataTable();
            DataColumn ed;
            ed = new DataColumn();
            ed.DataType = System.Type.GetType("System.Int32");
            ed.ColumnName = "id";
            T_education.Columns.Add(ed);

            ed = new DataColumn();
            ed.DataType = System.Type.GetType("System.String");
            ed.ColumnName = "educationName";
            T_education.Columns.Add(ed);

            ed = new DataColumn();
            ed.DataType = System.Type.GetType("System.String");
            ed.ColumnName = "educationNameEN";
            T_education.Columns.Add(ed);

            ed = new DataColumn();
            ed.DataType = System.Type.GetType("System.String");
            ed.ColumnName = "educationNote";
            T_education.Columns.Add(ed);

            /********************T_Position****************************/
            DataTable T_Position = new DataTable();
            DataColumn pt;
            pt = new DataColumn();
            pt.DataType = System.Type.GetType("System.Int32");
            pt.ColumnName = "id";
            T_Position.Columns.Add(pt);

            pt = new DataColumn();
            pt.DataType = System.Type.GetType("System.String");
            pt.ColumnName = "positionName";
            T_Position.Columns.Add(pt);

            pt = new DataColumn();
            pt.DataType = System.Type.GetType("System.String");
            pt.ColumnName = "positionNameEN";
            T_Position.Columns.Add(pt);

            pt = new DataColumn();
            pt.DataType = System.Type.GetType("System.String");
            pt.ColumnName = "Org";
            T_Position.Columns.Add(pt);

            /********************T_Sex****************************/
            DataTable T_Sex = new DataTable();
            DataColumn sex;
            sex = new DataColumn();
            sex.DataType = System.Type.GetType("System.Int32");
            sex.ColumnName = "id";
            T_Sex.Columns.Add(sex);

            sex = new DataColumn();
            sex.DataType = System.Type.GetType("System.Int32");
            sex.ColumnName = "sexID";
            T_Sex.Columns.Add(sex);

            sex = new DataColumn();
            sex.DataType = System.Type.GetType("System.String");
            sex.ColumnName = "sexName";
            T_Sex.Columns.Add(sex);

            sex = new DataColumn();
            sex.DataType = System.Type.GetType("System.String");
            sex.ColumnName = "sexNote";
            T_Sex.Columns.Add(sex);

            /************************************************/

            foreach (DataRow dr in empTb.Rows)
            { 
                
                DataRow dcr = T_certified.NewRow();
                dcr["id"] = dr["Eid"];
                dcr["passportNumber"] = dr["passportNumber"];
                dcr["passportIssueDate"] = dr["passportIssueDate"];
                dcr["passportFinishDate"] = dr["passportFinishDate"];
                dcr["passportSignArea"] = dr["passportSignArea"];
                dcr["passportVisaNumber"] = dr["passportVisaNumber"];
                dcr["passportVisaArea"] = dr["passportVisaArea"];
                dcr["passportVisaTimeLimit"] = dr["passportVisaTimeLimit"];
                dcr["passportVisaFinshDate"] = dr["passportVisaFinshDate"];
                dcr["entryVisaDate"] = dr["entryVisaDate"];
                dcr["workerCard"] = dr["workerCard"];
                dcr["workerCardID"] = dr["workerCardID"];
                dcr["healthCard"] = dr["healthCard"];
                T_certified.Rows.Add(dcr); 
                 
                /*
                DataRow dept = T_dept.NewRow();
                dept["id"] = dr["Did"];
                dept["org"] = dr["Org"];
                dept["deptName"] = dr["deptName"];
                T_dept.Rows.Add(dept);
                */
                /*
                DataRow edu = T_education.NewRow();
                edu["id"] = dr["educationID"];
                edu["educationName"] = dr["educationName"];
                edu["educationNameEN"] = dr["educationNameEN"];
                edu["educationNote"] = dr["educationNote"];
                T_education.Rows.Add(edu);   
                */

                DataRow der = T_employee.NewRow();
                der["id"] = dr["Eid"];
                der["passportNumber"] = dr["passportNumber"];
                der["deptID"] = dr["Did"];
                der["subID"] = dr["subID"];
                der["workID"] = dr["workID"];
                der["userName"] = dr["userName"];
                der["userNameEN"] = dr["userNameEN"];                
                der["userSexID"] = dr["userSexID"];
                der["birthday"] = dr["birthday"];
                der["educationID"] = dr["educationID"];
                der["hometown"] = dr["hometown"];
                der["phoneNumber"] = dr["phoneNumber"];
                der["positionID"] = dr["positionID"];
                der["entryDate"] = dr["entryDate"];
                der["jobChange"] = dr["jobChange"];
                der["assessDate"] = dr["assessDate"];
                der["contractFinishDate"] = dr["contractFinishDate"];
                der["tryFinishDate"] = dr["tryFinishDate"];
                der["planResignDate"] = dr["planResignDate"];
                der["resignDate"] = dr["resignDate"];
                der["resignNote"] = dr["resignNote"];
                der["resigned"] = dr["resigned"]; 
                T_employee.Rows.Add(der);

                DataRow msg = T_msg.NewRow();
                msg["id"] = dr["Mid"];
                msg["workID"] = dr["workID"];
                msg["msgTxt"] = dr["msgTxt"];
                msg["msgCheck"] = dr["msgCheck"];
                T_msg.Rows.Add(msg);

                /*
                DataRow sexr = T_Sex.NewRow();
                sexr["id"] = dr["Sid"];
                sexr["sexID"] = dr["sexID"];
                sexr["sexName"] = dr["sexName"];
                sexr["sexNote"] = dr["sexNote"];
                T_Sex.Rows.Add(sexr); 
                */

                /*
                DataRow pos = T_Position.NewRow();
                pos["id"] = dr["Pid"];
                pos["positionName"] = dr["positionName"];
                pos["positionNameEN"] = dr["positionNameEN"];
                pos["Org"] = dr["Org"];
                T_Position.Rows.Add(pos);
                */

            }

            int[] result =  em.SaveEmployees(T_certified,   T_employee, T_msg   );
            MessageBox.Show("保存成功,共新增 "+ result[0].ToString() +" 位同事/b/n 共修改 "+result[1].ToString()+" 位同事资料","保存成功");
            
        }

        public string toDateString(string value)
        {
            if (value == "")
            {
                return "1900-01-01";
            }
            DateTime dt = Convert.ToDateTime(value);
            return dt.ToString("yyyy-MM-dd");
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

        private void butAdd_Click(object sender, EventArgs e)
        {
            if (ckNewEmp.Checked)
            {
                Employees ep = new Employees();
                if (this.cborg2.SelectedIndex <= -1) return;
                ep.Org = this.cborg2.SelectedItem.ToString();

                if (this.cbDept2.SelectedIndex <= -1) this.cbDept2.SelectedIndex = 0;
                ep.Did = Convert.ToInt32(this.cbDept2.SelectedValue.ToString());

                if (this.cbPosition.SelectedIndex <= -1) this.cbPosition.SelectedIndex = 0;
                ep.positionID = Convert.ToInt32(this.cbPosition.SelectedValue.ToString());

                if (this.txtSubID.Text.Trim().Length > 0)
                {
                    ep.subID = this.txtSubID.Text.Trim().ToUpper();
                }
                else
                {
                    ep.subID = "";
                }

                string workID = this.txtWorkID.Text.Trim();
                List<string> workIDs = em.getWorkIDs();
                List<string> tworkIDs = getempsWorkid();


                if (this.txtWorkID.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("工号不能为空!");
                    this.txtWorkID.BackColor = Color.Red;
                    return;
                }
                else if (checkIndex(workIDs, workID))
                {
                    this.txtWorkID.BackColor = Color.Red;
                    MessageBox.Show("已有相同工号");
                    return;
                }
                else if (checkIndex(tworkIDs, workID))
                {
                    this.txtWorkID.BackColor = Color.Red;
                    MessageBox.Show("已有相同工号");
                    return;
                }

                else
                {
                    ep.workID = this.txtWorkID.Text.Trim();
                }

                string passportNumber = this.txtPassportNumber2.Text.Trim();
                List<string> passportNumbers = em.getPassportNumbers();
                List<string> tps = getempsPassports();
                if (passportNumber.Length <= 0)
                {
                    this.txtPassportNumber2.BackColor = Color.Red;
                    MessageBox.Show("护照号码不能为空!");
                    return;
                }
                else if (checkIndex(passportNumbers, passportNumber))
                {
                    this.txtPassportNumber2.BackColor = Color.Red;
                    MessageBox.Show("已有相同护照号码");
                    return;
                }

                else if (checkIndex(tps, passportNumber))
                {
                    this.txtPassportNumber2.BackColor = Color.Red;
                    MessageBox.Show("已有相同护照号码");
                    return;
                }
                else
                {
                    ep.passportNumber = this.txtPassportNumber2.Text.Trim().ToUpper();
                }

                ep.jobChange = this.cbJobChange.SelectedValue.ToString();

                if (this.txtUserName2.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("用户名不能为空!");
                    return;
                }
                else
                {
                    ep.userName = this.txtUserName2.Text.Trim();
                }

                ep.userSexID = Convert.ToInt32(this.cbSex.SelectedValue.ToString());
                ep.hometown = this.txtHomeTown.Text.Trim();
                ep.userNameEN = this.txtUsernameEN.Text.Trim().ToUpper();
                ep.phoneNumber = this.txtPhoneNumber.Text.Trim();
                ep.educationID = Convert.ToInt32(this.cbEducation.SelectedValue.ToString());
                if (this.ckHealthCard.Checked)
                {
                    ep.healthCard = 1;
                }
                else
                {
                    ep.healthCard = 0;
                }

                if (this.ckWorkerCard.Checked)
                {
                    ep.workerCard = 1;
                }
                else
                {
                    ep.workerCard = 0;
                }

                ep.workerCardID = this.txtWorkCarID.Text.Trim();

                ep.passportVisaNumber = this.txtPassportVisaNumber.Text.Trim().ToUpper();
                ep.passportVisaTimeLimit = this.cbPassportVisaTimeLimit.SelectedItem.ToString();
                ep.passportSignArea = this.txtPassportSignArea.Text.Trim().ToUpper();
                ep.passportVisaArea = this.txtPassportVisaArea.Text.Trim().ToUpper();
                ep.passportIssueDate = this.dtpPassportIssueDate.Value.ToString("yyyy-MM-dd");
                ep.passportFinishDate = this.dtpPassportFinishDate.Value.ToString("yyyy-MM-dd");
                ep.passportVisaFinshDate = this.dtpPassportVisaFinshDate.Value.ToString("yyyy-MM-dd");
                ep.entryVisaDate = this.dtpEntryVisaDate.Value.ToString("yyyy-MM-dd");
                ep.birthday = this.dtpBirthday.Value.ToString("yyyy-MM-dd");
                ep.entryDate = this.dtpEntryDate.Value.ToString("yyyy-MM-dd");
                ep.tryFinishDate = this.dtpTryFinishDate.Value.ToString("yyyy-MM-dd");
                ep.contractFinishDate = this.dtpContractFinishDate.Value.ToString("yyyy-MM-dd");
                if (this.dtpPlanResignDate.Checked)
                {
                    ep.planResignDate = this.dtpPlanResignDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    ep.planResignDate = "";
                }
                if (this.dtpResignDate.Checked)
                {
                    ep.resignDate = this.dtpResignDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    ep.resignDate = "";
                }

                ep.assessDate = this.txtAssessDate.Text;
                ep.age = Convert.ToInt32(this.txtAge.Text);
                ep.Seniority = Convert.ToDouble(this.txtSeniority.Text);
                if (this.ckResigned.Checked)
                {
                    ep.resigned = 1;
                }
                else
                {
                    ep.resigned = 0;
                }
                ep.resignNote = this.txtResignNote.Text.Trim();
                if (this.ckMsgCheck.Checked)
                {
                    ep.msgCheck = 1;
                }
                else
                {
                    ep.msgCheck = 0;
                }
                ep.msgTxt = this.txtMsgTxt.Text.Trim();
                ep.Eid = -1;
                ep.Mid = -1;
                ep.Pid = Convert.ToInt32(this.cbPosition.SelectedValue.ToString());
                ep.passportVisaTimeLimit = this.cbPassportVisaTimeLimit.SelectedItem.ToString();
                ep.deptName = this.cbDept2.Text;
                ep.positionName = this.cbPosition.Text;
                ep.sexID = Convert.ToInt32(this.cbSex.SelectedValue.ToString());
                ep.sexName = this.cbSex.Text;

              //  List<Employees> cc = new List<Employees>();
              //  cc.Add(ep);
                 this.emps.Add(ep);
                this.dgvEmployee.DataSource = null;
                this.dgvEmployee.DataSource = this.emps;
            }
            else if( rowIndex >-1)
            {
                
                this.dgvEmployee["passportNumber", rowIndex].Value = this.txtPassportNumber2.Text.Trim().ToUpper();
                this.dgvEmployee["subID", rowIndex].Value = this.txtSubID.Text.ToUpper();
                this.dgvEmployee["workID", rowIndex].Value = this.txtWorkID.Text.ToUpper();
                this.dgvEmployee["userName", rowIndex].Value = this.txtUserName2.Text.Trim();
                this.dgvEmployee["userNameEN", rowIndex].Value = this.txtUsernameEN.Text.Trim().ToUpper();
                this.dgvEmployee["userSexID", rowIndex].Value = this.cbSex.SelectedValue.ToString();
                this.dgvEmployee["birthday", rowIndex].Value = this.dtpBirthday.Value.ToString("yyyy-MM-dd");
                this.dgvEmployee["educationID", rowIndex].Value = this.cbEducation.SelectedValue.ToString();
                this.dgvEmployee["hometown", rowIndex].Value = this.txtHomeTown.Text.Trim().ToUpper();
                this.dgvEmployee["phoneNumber", rowIndex].Value = this.txtPhoneNumber.Text.Trim().ToUpper();
                this.dgvEmployee["positionID", rowIndex].Value = this.cbPosition.SelectedValue.ToString();
                this.dgvEmployee["entryDate", rowIndex].Value = this.dtpEntryDate.Value.ToString("yyyy-MM-dd");
                this.dgvEmployee["jobChange", rowIndex].Value = this.cbJobChange.SelectedValue.ToString();
                this.dgvEmployee["assessDate", rowIndex].Value = this.txtAssessDate.Text.Trim().ToUpper();
                this.dgvEmployee["contractFinishDate", rowIndex].Value = this.dtpContractFinishDate.Value.ToString("yyyy-MM-dd");
                this.dgvEmployee["tryFinishDate", rowIndex].Value = this.dtpTryFinishDate.Value.ToString("yyyy-MM-dd");
                if (this.dtpPlanResignDate.Checked)
                {
                    this.dgvEmployee["planResignDate", rowIndex].Value = this.dtpPlanResignDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    this.dgvEmployee["planResignDate", rowIndex].Value = "";
                }
                if (this.dtpResignDate.Checked)
                {
                    this.dgvEmployee["resignDate", rowIndex].Value = this.dtpResignDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    this.dgvEmployee["resignDate", rowIndex].Value = "";
                }
                this.dgvEmployee["resignNote", rowIndex].Value = this.txtResignNote.Text.Trim();
                this.dgvEmployee["resigned", rowIndex].Value = this.ckResigned.Checked == true ? 1 : 0;
                this.dgvEmployee["Did", rowIndex].Value = this.cbDept2.SelectedValue.ToString();
                this.dgvEmployee["deptName", rowIndex].Value = this.cbDept2.Text.Trim().ToUpper();
                this.dgvEmployee["Cid", rowIndex].Value = this.txtCid.Text.Trim();
                this.dgvEmployee["passportIssueDate", rowIndex].Value = this.dtpPassportIssueDate.Value.ToString("yyyy-MM-dd");
                this.dgvEmployee["passportFinishDate", rowIndex].Value = this.dtpPassportFinishDate.Value.ToString("yyyy-MM-dd");
                this.dgvEmployee["passportSignArea", rowIndex].Value = this.txtPassportSignArea.Text.Trim().ToUpper();
                this.dgvEmployee["passportVisaNumber", rowIndex].Value = this.txtPassportVisaNumber.Text.Trim().ToUpper();
                this.dgvEmployee["passportVisaArea", rowIndex].Value = this.txtPassportVisaArea.Text.Trim().ToUpper();
                this.dgvEmployee["passportVisaTimeLimit", rowIndex].Value = this.cbPassportVisaTimeLimit.SelectedItem.ToString();
                this.dgvEmployee["passportVisaFinshDate", rowIndex].Value = this.dtpPassportVisaFinshDate.Value.ToString("yyyy-MM-dd");
                this.dgvEmployee["entryVisaDate", rowIndex].Value = this.dtpEntryVisaDate.Value.ToString("yyyy-MM-dd");
                this.dgvEmployee["workerCard", rowIndex].Value = this.ckWorkerCard.Checked == true ? 1 : 0;
                this.dgvEmployee["workerCardID", rowIndex].Value = this.txtWorkCarID.Text.Trim().ToUpper();
                this.dgvEmployee["healthCard", rowIndex].Value = this.ckHealthCard.Checked == true ? 1 : 0;
                this.dgvEmployee["age", rowIndex].Value = this.txtAge.Text.Trim().ToUpper();
                this.dgvEmployee["Seniority", rowIndex].Value = this.txtSeniority.Text.Trim().ToUpper();
                this.dgvEmployee["msgTxt", rowIndex].Value = this.txtMsgTxt.Text.Trim();
                this.dgvEmployee["msgCheck", rowIndex].Value = this.ckMsgCheck.Checked == true ? 1 : 0;
                this.dgvEmployee["Pid", rowIndex].Value = this.txtPid.Text.Trim().ToUpper();
                this.dgvEmployee["positionName", rowIndex].Value = this.cbPosition.Text.Trim().ToUpper();
                this.dgvEmployee["Org", rowIndex].Value = this.cborg2.SelectedItem.ToString();
                this.dgvEmployee["sexID", rowIndex].Value = this.cbSex.SelectedValue.ToString();
                this.dgvEmployee["sexName", rowIndex].Value = this.cbSex.Text.ToUpper();

                this.dgvEmployee.Refresh();
            }
        }

        
             public List<string> getempsPassports()
        {
            List<string> vs = new List<string>();
            for (int i = 0; i < this.emps.Count; i++)
            {
                vs.Add(this.emps[i].passportNumber.ToUpper());
            }
            return vs;
        }

        public List<string> getempsWorkid()
        {
            List<string> vs = new List<string>();
            for (int i = 0; i < this.emps.Count; i++)
            {
                vs.Add(this.emps[i].workID.ToUpper());
            }
            return vs;
        }
        private void butChange_Click(object sender, EventArgs e)
        {
            if (this.pNumber != "")
            {
                this.pNumbers.Add(this.pNumber);
                for (int i = 0; i < this.dgvEmployee.Rows.Count; i++)
                {
                    if (this.dgvEmployee.Rows[i].Cells["passportNumber"].Value.ToString() == this.pNumber)
                    {
                        this.dgvEmployee.Rows.RemoveAt(i);
                        this.pNumber = "";
                    }
                }
            }
        }

        private void butResign_Click(object sender, EventArgs e)
        {
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            this.saveEmployee();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            string org = "";
            string dept = "";
            if (this.cbOrg.Items.Count > 0)
            {
                org = this.cbOrg.SelectedItem.ToString();
            }

            if (this.cbDept.Items.Count > 0)
            {
                dept = this.cbDept.SelectedItem.ToString();
                if (dept == "All")
                {
                    dept = "";
                }
            }

            string passPortNumber = this.txtPassPortNumber.Text.Trim();
            string userName = this.txtUserName.Text.Trim();
            string workNumber = this.txtWorkNumber.Text.Trim();
            bool checkDate = this.cbDate.Checked;
            int? dateRequire = this.cbDateRequireMent.SelectedIndex;
            string starDate = this.dtpStarDate.Value.ToString("yyyy-MM-dd");
            string stopDate = this.dtpStopDate.Value.ToString("yyyy-MM-dd");
            string assessDate = this.textBox1.Text.Trim();

            EmployeesParameters ep = new EmployeesParameters();
            ep.org = org;
            ep.dept = dept;
            ep.passPortNumber = passPortNumber;
            ep.userName = userName;
            ep.workNumber = workNumber;
            ep.checkDate = checkDate;
            ep.dateRequire = dateRequire;
            ep.starDate = starDate;
            ep.starDate = stopDate;
            ep.assessDate = assessDate;

            //  List<Employees> employees = em.getEmployeesByParameters(ep);
          //  employees.Clear();
            this.emps = em.getEmployeesByParameters(ep);
            if (this.emps == null)
            {
                this.emps = new List<Employees>();
            }

                this.dgvEmployee.DataSource = null;
            if (  this.emps.Count > 0)
            {
                this.dgvEmployee.DataSource = this.emps;
                /*
                for (int i = 0; i < employees.Rows.Count; i++)
                {
                    this.dgvEmployee.Rows.Add();
                    for (int j = 0; j < employees.Columns.Count; j++)
                    {
                        string value = employees.Rows[i][j].ToString();
                        addData(value, i,j);
                    }
                }
                */
            }
            //DataGridViewRow.Cells[ColumnName].Value
            // this.dgvEmployee.DataSource = employees;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1Collapsed = !this.splitContainer1.Panel1Collapsed;
            if (this.button1.Text == "∨")
            {
                this.button1.Text = "∧";
                this.splitContainer1.SplitterDistance = 275;
            }
            else
            {
                this.button1.Text = "∨";
                // this.splitContainer1.SplitterDistance = 0;
                //  this.splitContainer1.Panel1.v
                //   this.splitContainer1.Panel1Collapsed = !this.splitContainer1.Panel1Collapsed;
            }
        }

        private void cbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDate.Checked)
            {
                this.cbDateRequireMent.Enabled = true;
                this.dtpStarDate.Enabled = true;
                this.dtpStopDate.Enabled = true;
            }
            else
            {
                this.cbDateRequireMent.Enabled = false;
                this.dtpStarDate.Enabled = false;
                this.dtpStopDate.Enabled = false;
            }
        }

        private void cbDateRequireMent_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectIndex = this.cbDateRequireMent.SelectedIndex;
            if (selectIndex == 0)
            {
                this.label16.Visible = false;
                this.dtpStarDate.Visible = false;
                this.dtpStopDate.Visible = false;
                this.textBox1.Visible = true;
            }
            else
            {
                this.label16.Visible = true;
                this.dtpStarDate.Visible = true;
                this.dtpStopDate.Visible = true;
                this.textBox1.Visible = false;
            }
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            this.cbDept.Items.Clear();
            List<dept> depts = em.getDepts();
            if (depts.Count > 0)
            {
                foreach (dept d in depts)
                {
                    this.cbDept.Items.Add(d.deptName);
                }
            }

            */
        }

        private void cbOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string org = cbOrg.SelectedItem.ToString();
            List<T_dept> depts = em.getDetp(org);
            this.cbDept.Items.Clear();
            foreach (T_dept item in depts)
            {
                this.cbDept.Items.Add(item.deptName);
            }
            this.cbDept.Items.Add("All");
            if (this.cbDept.Items.Count > 0)
            {
                this.cbDept.SelectedIndex = this.cbDept.Items.Count - 1;
            }
        }

        private void cborg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string org = cborg2.SelectedItem.ToString();
            List<T_dept> depts = em.getDetp(org);

            this.cbDept2.DataSource = depts;
            this.cbDept2.ValueMember = "id";
            this.cbDept2.DisplayMember = "deptName";

            if (this.cbDept2.Items.Count > 0)
            {
                this.cbDept2.SelectedIndex = this.cbDept2.Items.Count - 1;
            }

            List<T_Position> positions = em.getPositions(org);
            List<T_Position> Jobs = new List<T_Position>();
            if (positions.Count < 0)
            {
                return;
            }
            foreach (T_Position item in positions)
            {
                T_Position Job = new T_Position();
                Job.id = item.id;
                Job.Org = item.Org;
                Job.positionName = item.positionName;
                Job.positionNameEN = item.positionNameEN;
                Jobs.Add(Job);
            }

            this.cbPosition.DataSource = positions;
            this.cbPosition.ValueMember = "id";
            this.cbPosition.DisplayMember = "positionName";

            this.cbJobChange.DataSource = Jobs;
            this.cbJobChange.ValueMember = "id";
            this.cbJobChange.DisplayMember = "positionName";

            if (this.cbPosition.Items.Count > 0)
            {
                this.cbPosition.SelectedIndex = 0;
                this.cbJobChange.SelectedIndex = this.cbJobChange.Items.Count - 1;
            }
        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  MessageBox.Show("22");
        }

        private bool checkIndex(List<string> pas, string value)
        {
            bool result = false;
            if (pas == null || pas.Count <= 0)
            {
                result = false;
                return result;
            }
            for (int i = 0; i < pas.Count; i++)
            {
                if (pas[i].ToUpper() == value.ToUpper())
                {
                    result = true;
                    break;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dgvEmployee.CurrentCell.OwningColumn.Name == "org")
            {
                string str = ((ComboBox)sender).Text.ToString();
                List<T_dept> depts = em.getDetp(str);
                ((DataGridViewComboBoxCell)this.dgvEmployee.CurrentRow.Cells["deptName"]).DataSource = depts;
                ((DataGridViewComboBoxCell)this.dgvEmployee.CurrentRow.Cells["deptName"]).DisplayMember = "deptName";
                ((DataGridViewComboBoxCell)this.dgvEmployee.CurrentRow.Cells["deptName"]).ValueMember = "id";
                ((ComboBox)sender).SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //  this.dgvEmployee.EditMode = DataGridViewEditMode.EditOnEnter;
            // this.dgvEmployee.EditMode = DataGridViewEditMode.EditOnEnter;

            int CIndex = e.ColumnIndex;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewComboBoxColumn combo = dgvEmployee.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                if (combo != null)  //如果该列是ComboBox列
                {
                    dgvEmployee.BeginEdit(false); //结束该列的编辑状态
                    DataGridViewComboBoxEditingControl comboEdite = dgvEmployee.EditingControl as DataGridViewComboBoxEditingControl;
                    if (comboEdite != null)
                    {
                        comboEdite.DroppedDown = true; //展现下拉列表
                    }
                }
                DataGridViewTextBoxColumn textbox = dgvEmployee.Columns[e.ColumnIndex] as DataGridViewTextBoxColumn;
                if (textbox != null) //如果该列是TextBox列
                {
                    dgvEmployee.BeginEdit(true); //开始编辑状态
                }

                CalendarColumn Calendar = dgvEmployee.Columns[e.ColumnIndex] as CalendarColumn;

                dgvEmployee.BeginEdit(true); //开始编辑状态
            }
            else if (e.ColumnIndex == -1 && dgvEmployee.Rows[e.RowIndex].Cells["passportNumber"] != null)
            {
                this.pNumber = dgvEmployee.Rows[e.RowIndex].Cells["passportNumber"].Value.ToString();
            }
        }

        private void dgvEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            this.rowIndex = e.RowIndex;
            this.init();
            this.splitContainer1.Panel1Collapsed = false;
            this.button1.Text = "∧";
            this.splitContainer1.SplitterDistance = 275;
            // string Eid = this.dgvEmployee["Eid", e.RowIndex].Value.ToString();
            this.cborg2.SelectedItem = this.dgvEmployee["Org", e.RowIndex].Value.ToString();
            this.cbDept2.SelectedValue = Convert.ToInt32(this.dgvEmployee["Did", e.RowIndex].Value.ToString());

            this.cbPosition.SelectedValue = Convert.ToInt32(this.dgvEmployee["positionID", e.RowIndex].Value.ToString());
            this.txtSubID.Text = this.dgvEmployee["subID", e.RowIndex].Value.ToString();
            this.txtWorkID.Text = this.dgvEmployee["workID", e.RowIndex].Value.ToString();
            this.txtPassportNumber2.Text = this.dgvEmployee["passportNumber", e.RowIndex].Value.ToString();
            this.txtPassportVisaArea.Text = this.dgvEmployee["passportVisaArea", e.RowIndex].Value.ToString();
            this.cbJobChange.SelectedValue = Convert.ToInt32(this.dgvEmployee["jobChange", e.RowIndex].Value.ToString());
            this.txtUserName2.Text = this.dgvEmployee["userName", e.RowIndex].Value.ToString();
            this.txtUserID.Text = this.dgvEmployee["Eid", e.RowIndex].Value.ToString();
            this.cbSex.SelectedValue = Convert.ToInt32(this.dgvEmployee["userSexID", e.RowIndex].Value.ToString());
            this.txtHomeTown.Text = this.dgvEmployee["hometown", e.RowIndex].Value.ToString();
            this.txtUsernameEN.Text = this.dgvEmployee["userNameEN", e.RowIndex].Value.ToString();
            this.txtPhoneNumber.Text = this.dgvEmployee["phoneNumber", e.RowIndex].Value.ToString();
            this.cbEducation.SelectedValue = Convert.ToInt32(this.dgvEmployee["educationID", e.RowIndex].Value.ToString());
            string ckhealthCard = this.dgvEmployee["healthCard", e.RowIndex].Value.ToString();
            if (ckhealthCard == "1")
            {
                this.ckHealthCard.Checked = true;
            }
            else if (ckhealthCard == "0")
            {
                this.ckHealthCard.Checked = false;
            }
            string ckworkerCard = this.dgvEmployee["workerCard", e.RowIndex].Value.ToString();
            if (ckworkerCard == "1")
            {
                this.ckWorkerCard.Checked = true;
            }
            else if (ckworkerCard == "0")
            {
                this.ckWorkerCard.Checked = false;
            }
            this.txtWorkCarID.Text = this.dgvEmployee["workerCardID", e.RowIndex].Value.ToString();
            this.txtPassportVisaNumber.Text = this.dgvEmployee["passportVisaNumber", e.RowIndex].Value.ToString();
            this.cbPassportVisaTimeLimit.SelectedItem = this.dgvEmployee["passportVisaTimeLimit", e.RowIndex].Value.ToString();
            this.txtPassportSignArea.Text = this.dgvEmployee["passportSignArea", e.RowIndex].Value.ToString();
            this.dtpPassportIssueDate.Text = this.dgvEmployee["passportIssueDate", e.RowIndex].Value.ToString();
            this.dtpPassportFinishDate.Text = this.dgvEmployee["passportFinishDate", e.RowIndex].Value.ToString();
            this.dtpPassportVisaFinshDate.Text = this.dgvEmployee["passportVisaFinshDate", e.RowIndex].Value.ToString();
            this.dtpEntryVisaDate.Text = this.dgvEmployee["entryVisaDate", e.RowIndex].Value.ToString();
            this.dtpBirthday.Text = this.dgvEmployee["birthday", e.RowIndex].Value.ToString();
            this.dtpEntryDate.Text = this.dgvEmployee["entryDate", e.RowIndex].Value.ToString();
            this.dtpTryFinishDate.Text = this.dgvEmployee["tryFinishDate", e.RowIndex].Value.ToString();
            this.dtpContractFinishDate.Text = this.dgvEmployee["contractFinishDate", e.RowIndex].Value.ToString();
            string planResignDate = this.dgvEmployee["planResignDate", e.RowIndex].Value.ToString();
            string resignDate = this.dgvEmployee["resignDate", e.RowIndex].Value.ToString();
            if (planResignDate.Length > 0)
            {
                this.dtpPlanResignDate.Checked = true;
                this.dtpPlanResignDate.Text = planResignDate;
            }
            else
            {
                this.dtpPlanResignDate.Checked = false;
            }

            if (resignDate.Length > 0)
            {
                this.dtpResignDate.Checked = true;
                this.dtpResignDate.Text = resignDate;
            }
            else
            {
                this.dtpResignDate.Checked = false;
            }

            //   this.dtpPlanResignDate.Text = this.dgvEmployee["planResignDate", e.RowIndex].Value.ToString();
            //  this.dtpResignDate.Text = this.dgvEmployee["resignDate", e.RowIndex].Value.ToString();

            this.txtAssessDate.Text = this.dgvEmployee["assessDate", e.RowIndex].Value.ToString();
            this.txtAge.Text = this.dgvEmployee["age", e.RowIndex].Value.ToString();
            this.txtSeniority.Text = this.dgvEmployee["Seniority", e.RowIndex].Value.ToString();
            string ckresigned = this.dgvEmployee["resigned", e.RowIndex].Value.ToString();
            if (ckresigned == "1")
            {
                this.ckResigned.Checked = true;
            }
            else if (ckresigned == "0")
            {
                this.ckResigned.Checked = false;
            }
            this.txtResignNote.Text = this.dgvEmployee["resignNote", e.RowIndex].Value.ToString();
            string ckmsgCheck = this.dgvEmployee["msgCheck", e.RowIndex].Value.ToString();
            if (ckmsgCheck == "1")
            {
                this.ckMsgCheck.Checked = true;
            }
            else if (ckmsgCheck == "0")
            {
                this.ckMsgCheck.Checked = false;
            }
            this.txtMsgTxt.Text = this.dgvEmployee["msgTxt", e.RowIndex].Value.ToString();
            this.txtDid.Text = this.dgvEmployee["Did", e.RowIndex].Value.ToString();
            this.txtCid.Text = this.dgvEmployee["Cid", e.RowIndex].Value.ToString();
            this.txtPid.Text = this.dgvEmployee["Pid", e.RowIndex].Value.ToString();
        }

        // 完成时记算 年龄 年资
        private void dgvEmployee_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            if (this.dgvEmployee.Columns[col].Name == "birthday" && this.dgvEmployee.Rows[row].Cells[col].Value != null)    // 完成时记算 年龄
            {
                DateTime dateTime = DateTime.Now;
                string year = dateTime.Year.ToString();
                string birth = this.dgvEmployee.Rows[row].Cells[col].Value.ToString();
                birth = birth.Substring(0, 4);
                string age = Convert.ToString(Convert.ToInt32(year) - Convert.ToInt32(birth));
                this.dgvEmployee.Rows[row].Cells["age"].Value = age;
            }
            else if (this.dgvEmployee.Columns[col].Name == "entryDate" && this.dgvEmployee.Rows[row].Cells[col].Value != null)  // 入职记算
            {
                // 年资   （今天-入职日期）/365
                DateTime dateTime = DateTime.Now;
                string entry = this.dgvEmployee.Rows[row].Cells[col].Value.ToString();
                DateTime entryDate = Convert.ToDateTime(entry);
                TimeSpan sen = dateTime - entryDate;
                decimal seniority = sen.Days / (decimal)365;
                seniority = Math.Round(seniority, 2);
                this.dgvEmployee.Rows[row].Cells["seniority"].Value = seniority;

                // 考核到期月份
                string assess = entryDate.Month.ToString();
                this.dgvEmployee.Rows[row].Cells["assessDate"].Value = assess;

                // 试用到期月份 tryFinishDate
                string tryFinish = entryDate.AddMonths(2).ToString().Split(' ')[0];  // 试用到期月份
                this.dgvEmployee.Rows[row].Cells["tryFinishDate"].Value = tryFinish;

                // 如果没有值，用入职日期 自动增加一年
                // （ 如果有值  且 今天 >= 合约到期日期  且 未离职 ）  用合约到期日 增加一年 （小于/离职  不动 ）
                // 合约到期 contractFinishDate
                if (this.dgvEmployee.Rows[row].Cells["contractFinishDate"].Value == null)
                {
                    string cDatestr = this.dgvEmployee.Rows[row].Cells["entryDate"].Value.ToString();
                    DateTime cDate = Convert.ToDateTime(cDatestr);
                    cDate = cDate.AddYears(+1);
                    this.dgvEmployee.Rows[row].Cells["contractFinishDate"].Value = cDate.ToString().Split(' ')[0];
                }

                string contractFinishDate = this.dgvEmployee.Rows[row].Cells["contractFinishDate"].Value.ToString(); // 合约到期日

                string resigned = this.dgvEmployee.Rows[row].Cells["resigned"].Selected.ToString();  // 是否离职
                DateTime contractDate = Convert.ToDateTime(contractFinishDate);
                int compNum = DateTime.Compare(dateTime, contractDate);  //  大于   > 0
                if (compNum > 0 && resigned != "True")
                {
                    string c = this.dgvEmployee.Rows[row].Cells["contractFinishDate"].Value.ToString().Split(' ')[0];
                    DateTime cDate = Convert.ToDateTime(c).AddYears(1);
                    this.dgvEmployee.Rows[row].Cells["contractFinishDate"].Value = cDate.ToString().Split(' ')[0];
                }
            }
            else if (this.dgvEmployee.Columns[col].Name == "passportVisaTimeLimit" && this.dgvEmployee.Rows[row].Cells[col].Value != null)  // 签证到期日记算
            {
                // 本次不为空  为空 =  入职签证日期
                if (this.dgvEmployee.Rows[row].Cells["passportVisaFinshDate"].Value != null)
                {
                    // passportVisaTimeLimit

                    string visaFinshDate = this.dgvEmployee.Rows[row].Cells["passportVisaFinshDate"].Value.ToString().Split(' ')[0];
                    //   string lastVisaDate = this.dgvEmployee.Rows[row].Cells["entryVisaDate"].Value.ToString().Split(' ')[0];
                    DateTime FinshDate = Convert.ToDateTime(visaFinshDate);// 签证签证到期日期
                                                                           //   DateTime VisaDate = Convert.ToDateTime(lastVisaDate);  // 上次签证到期日期
                    int limitDate = Convert.ToInt32(this.dgvEmployee.Rows[row].Cells["passportVisaTimeLimit"].Value.ToString()); // 签证时长

                    //  int compNum = DateTime.Compare(VisaDate, FinshDate);  //  大于   > 0
                    //上次到期日 大于 本次到期日
                    // 上次日期 改成本次日期
                    // 本次日期 = 本次日期 + 签证时长
                    // if (compNum > 0)
                    //  {
                    //   this.dgvEmployee.Rows[row].Cells["entryVisaDate"].Value = visaFinshDate;
                    string passportVisaFinshDate = FinshDate.AddMonths(+limitDate).ToString().Split(' ')[0];
                    this.dgvEmployee.Rows[row].Cells["passportVisaFinshDate"].Value = passportVisaFinshDate;
                    //  }
                }
                else
                {
                    this.dgvEmployee.Rows[row].Cells["passportVisaFinshDate"].Value = this.dgvEmployee.Rows[row].Cells["entryVisaDate"].Value;
                }
            }
        }

        private void dgvEmployee_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //实现单击一次显示下拉列表框
            if (dgvEmployee.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && e.RowIndex != -1)
            {
                SendKeys.Send("{F4}");
                //SendKeys.Send("{INSERT}");
            }
        }

        private void dgvEmployee_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //右键弹出右键菜单
            {
                this.hiedcolumnindex = -1;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvEmployee.Rows[e.RowIndex].Selected == false)
                    {
                        dgvEmployee.ClearSelection();
                        dgvEmployee.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvEmployee.SelectedRows.Count == 1)
                    {
                        dgvEmployee.CurrentCell = dgvEmployee.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    this.dgv = dgvEmployee;
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

        private void dgvEmployee_CellMouseDown_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgv = (DataGridView)sender;
            if (dgv == null)
            {
                return;
            }

            this.tomenuRight(dgv, e);
        }

        private void dgvEmployee_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
        }

        private void dgvEmployee_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        //添加委托事件
        private void dgvEmployee_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvEmployee.CurrentCell.OwningColumn.Name == "org")
            {
                ((ComboBox)e.Control).SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        private void dgvEmployee_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dgvEmployee_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void dtpBirthday_ValueChanged(object sender, EventArgs e)
        {
            // this.txtAge.Text =   this.dtpBirthday.Value.ToString();

            DateTime dateTime = DateTime.Now;
            string year = dateTime.Year.ToString();
            string birth = this.dtpBirthday.Value.ToString();
            birth = birth.Substring(0, 4);
            string age = Convert.ToString(Convert.ToInt32(year) - Convert.ToInt32(birth));
            this.txtAge.Text = age;
        }

        private void dtpEntryDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            DateTime edt = this.dtpEntryDate.Value;
            if (dateTime < edt)
            {
                MessageBox.Show("入职日期不能大于现在时间");
                this.dtpEntryDate.Value = dateTime;
                return;
            }

            // 考核到期月份
            string assess = this.dtpEntryDate.Value.Month.ToString();
            this.txtAssessDate.Text = assess;

            // 年资   （今天-入职日期）/365
            string entry = this.dtpEntryDate.Value.ToString();
            DateTime entryDate = Convert.ToDateTime(entry);
            TimeSpan sen = dateTime - entryDate;
            decimal seniority = sen.Days / (decimal)365;
            seniority = Math.Round(seniority, 2);
            this.txtSeniority.Text = seniority.ToString();

            // 试用到期月份 tryFinishDate  dtpTryFinishDate
            this.dtpTryFinishDate.Value = this.dtpEntryDate.Value.AddMonths(+2);

            //  合约到期  dtpContractFinishDate
            this.dtpContractFinishDate.Value = this.dtpEntryDate.Value.AddYears(+1);
            DateTime cdt = this.dtpContractFinishDate.Value;
            if (cdt <= dateTime)
            {
                string cdtstr = cdt.ToString().Substring(5, cdt.ToString().Length - 6);
                string dateTimestr = dateTime.ToString().Substring(0, 5);

                this.dtpContractFinishDate.Value = Convert.ToDateTime(dateTimestr + cdtstr);
            }
            //  this.dtpContractFinishDate.Value = this.dtpEntryDate.Value.AddYears(+1);
            //  cdt = this.dtpContractFinishDate.Value;

            // 预计离职日 dtpPlanResignDate
            this.dtpPlanResignDate.ShowCheckBox = true;
            this.dtpPlanResignDate.Checked = false;

            // 离职日 dtpResignDate
            this.dtpResignDate.ShowCheckBox = true;
            this.dtpResignDate.Checked = false;
        }

        private void dtpPlanResignDate_ValueChanged(object sender, EventArgs e)
        {
            /*
             * // 预计离职日 dtpPlanResignDate
            this.dtpPlanResignDate.Format = DateTimePickerFormat.Custom;
            this.dtpPlanResignDate.CustomFormat = null;

            // 离职日 dtpResignDate
            this.dtpResignDate.Format = DateTimePickerFormat.Custom;
            this.dtpResignDate.CustomFormat = null;
*/
        }

        private void dtpStarDate_ValueChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = true;
        }

        private void dtpStopDate_ValueChanged(object sender, EventArgs e)
        {
            this.cbDate.Checked = true;
        }

        private void FrmDept_Load(object sender, EventArgs e)
        {
            this.cbOrg.SelectedIndex = 0;
            this.cbDate.Checked = false;
            this.cbDateRequireMent.Enabled = false;
            this.dtpStarDate.Enabled = false;
            this.dtpStopDate.Enabled = false;
            if (this.cbDept.Items.Count > 0)
            {
                this.cbDept.SelectedIndex = this.cbDept.Items.Count - 1;
            }
            this.textBox1.Visible = false;
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.Panel1Collapsed = true;
            this.getSex();
            this.getEducation();
            this.cbPassportVisaTimeLimit.SelectedIndex = 3;
            this.cborg2.SelectedIndex = 0;
            DateTime dt = DateTime.Now.AddYears(-18);
            dtpBirthday.Value = dt;

            DateTime edt = DateTime.Now.AddDays(-0);
            dtpEntryDate.Value = edt;
        }

        private void FrmEmployee_Resize(object sender, EventArgs e)
        {
            this.groupBox1.Width = this.Width - 15;
            this.groupBox1.Height = this.Height - 120;
        }

        private void getEducation()
        {
            List<T_Education> educations = em.getEducation();
            this.cbEducation.Items.Clear();
            this.cbEducation.DataSource = educations;
            this.cbEducation.ValueMember = "id";
            this.cbEducation.DisplayMember = "educationName";
            if (this.cbEducation.Items.Count > 0)
            {
                this.cbEducation.SelectedIndex = 1;
            }
            if (this.cbEducation.Items.Count > 5)
            {
                this.cbEducation.SelectedIndex = 4;
            }
        }

        private void getSex()
        {
            List<T_Sex> sexs = em.getSex();
            this.cbSex.Items.Clear();
            this.cbSex.DataSource = sexs;
            this.cbSex.ValueMember = "sexID";
            this.cbSex.DisplayMember = "sexName";
            if (this.cbSex.Items.Count > 0)
            {
                this.cbSex.SelectedIndex = 1;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void label29_Click(object sender, EventArgs e)
        {
        }

        private void RmeCopyCells_Click(object sender, EventArgs e)
        {
            if (this.dgv != null)
            {
                Clipboard.SetDataObject(this.dgv.CurrentCell.Value.ToString());
            }
        }

        private void RmeCopyRows_Click(object sender, EventArgs e)
        {
            if (this.dgv != null)
            {
                Clipboard.SetDataObject(this.dgv.GetClipboardContent());
            }
        }

        private void RmeExportExcel_Click(object sender, EventArgs e)
        {
            if (this.dgv != null)
            {
                ImproExcel(this.dgv);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPassportNumber2_TextChanged(object sender, EventArgs e)
        {
            this.txtPassportNumber2.BackColor = SystemColors.Control;
        }

        private void txtUserName2_TextChanged(object sender, EventArgs e)
        {
            this.txtUsernameEN.Text = NPinyin.Pinyin.GetPinyin(this.txtUserName2.Text).ToUpper();
        }

        private void txtWorkID_TextChanged(object sender, EventArgs e)
        {
            this.txtWorkID.BackColor = SystemColors.Control;
        }

        private void butAddJob_Click(object sender, EventArgs e)
        {
            this.gbAddJobs.Visible = true;
        }

        private void butCloseAddJobs_Click(object sender, EventArgs e)
        {
            this.gbAddJobs.Visible = false;
        }

        private void butAddSave_Click(object sender, EventArgs e)
        {
            string org = "";
                if (this.rbSelectedSAA.Checked)
            {
                org = "SAA";
            }
            else
            {
                org = "TOP";
            }
            string jobsName = this.txtJobsName.Text.Trim().ToUpper();
            string jobsNameEN = this.txtJobsNameEN.Text.Trim().ToUpper();
            List<string> jobs = em.getJobs(org);
            if (checkIndex(jobs, jobsName))
            {
                this.txtJobsName.BackColor = Color.Red;
                MessageBox.Show("已有相同职位");
                return;
            }
            
                int i =  em.addJobs(jobsName, jobsNameEN,org);
            if (i > 0)
            {
                MessageBox.Show("添加职位成功");
                this.txtJobsName.Text = "";
                this.txtJobsNameEN.Text = "";
            }
            else
            {
                MessageBox.Show("添加职位失败");
            }
                
             

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtJobsName_TextChanged(object sender, EventArgs e)
        {
            this.txtJobsNameEN.Text = NPinyin.Pinyin.GetPinyin(this.txtJobsName.Text.Trim().ToUpper());
            this.txtJobsName.BackColor = SystemColors.Control;
        }

        private void cbPosition_Click(object sender, EventArgs e)
        {



            string org = this.cborg2.Text;

            List<T_Position> positions = em.getPositions(org);
            List<T_Position> Jobs = new List<T_Position>();
            if (positions.Count < 0)
            {
                return;
            }
            foreach (T_Position item in positions)
            {
                T_Position Job = new T_Position();
                Job.id = item.id;
                Job.Org = item.Org;
                Job.positionName = item.positionName;
                Job.positionNameEN = item.positionNameEN;
                Jobs.Add(Job);
            }

            this.cbPosition.DataSource = positions;
            this.cbPosition.ValueMember = "id";
            this.cbPosition.DisplayMember = "positionName";

            this.cbJobChange.DataSource = Jobs;
            this.cbJobChange.ValueMember = "id";
            this.cbJobChange.DisplayMember = "positionName";

            if (this.cbPosition.Items.Count > 0)
            {
                this.cbPosition.SelectedIndex = 0;
                this.cbJobChange.SelectedIndex = this.cbJobChange.Items.Count - 1;
            }


        }
    }
}