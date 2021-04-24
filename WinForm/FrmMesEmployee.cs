using BLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmMesEmployee : Form
    {
        mesEmployeeManager empm = new mesEmployeeManager();
        mesOrgManager orgm = new mesOrgManager();
        private static FrmMesEmployee frm;
        public List<mesEmployee> emps;
        public List<mesOrg> Orgs;
        public string SAAUlr = "http://192.168.4.251:5000/api/process";
        public string SAATest = "http://192.168.4.251:5001/api/reportPlace";
        public string TOPUlr = "http://192.168.7.240:5000/api/process";
        public string TOPTest = "http://192.168.7.240:5001/api/reportPlace";
        public FrmMesEmployee()
        {
            InitializeComponent();
            this.dgvUserInfo.DoubleBufferedDataGirdView(true);
        }
        public static FrmMesEmployee GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmMesEmployee();
            }
            return frm;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ClearAddUserInfo();

        }

        public void  ClearAddUserInfo()
        {

            this.txtAccountID.Text = ""; 
            this.txtAccount.Text = "";            
            this.txtPassword.Text = "";
            this.txtUserName.Text = "";
            this.cbProcess.SelectedIndex = -1;
            this.txtProcessID.Text = "";
            this.txtPmarsk.Text = "";
            this.txtUmarsk.Text = "";
            this.txtOrgID.Text = "";
            this.cbOrg.SelectedIndex = -1;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string accountID = this.txtAccountID.Text.Trim();
            string account = this.txtAccount.Text.Trim().ToUpper();
            string password = this.txtPassword.Text.Trim();
            string userName = this.txtUserName.Text.Trim();
            if (this.cbProcess.Items.Count <= 0 || this.cbProcess.SelectedIndex <0)
            {
                MessageBox.Show("未选择制程");
                return;
            }
            if( this.cbOrg.Items.Count <=0 || this.cbOrg.SelectedIndex < 0)
            {
                MessageBox.Show("未选择厂区");
                return;
            }
            string processName = this.cbProcess.SelectedItem.ToString().Trim();
            string processID = this.txtProcessID.Text.Trim();
            string orgName = this.cbOrg.SelectedItem.ToString().Trim();
            string orgId = this.txtOrgID.Text.Trim();
            string umarsk = this.txtUmarsk.Text.Trim();
            string pmarsk = this.txtPmarsk.Text.Trim();


            if (account.Length<=0)
            {
                MessageBox.Show("用户账号不能为空");
                return;
            }
           // if (password.Length <= 0)
           // {
          //      MessageBox.Show("用户密码不能为空");
          //      return;
          //  }
            if (processID.Length <= 0)
            {
                MessageBox.Show("用户所属制程不能为空");
                return;
            }
            string[] userInfo = { accountID,account, password, userName, processName,processID, orgId, pmarsk };
            int i = this.updataOrAddUser(userInfo);
            if (i == -1)
            {
                MessageBox.Show("账号:" + account + "已存在,请修改其它账号使用，谢谢!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }else if(i == -2)
            {
                MessageBox.Show("新增账号时密码不能为空，谢谢!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (i == -3)
            {
                MessageBox.Show("新增账号时制程不能为空，谢谢!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if(accountID != "")
                {
                    this.getUsers(Convert.ToInt32(accountID));
                }
                else
                {
                    this.getUsers(-1);
                    int s = this.dgvUserInfo.Rows.Count - 1;
                    this.dgvUserInfo.Rows[s].Selected = true; 
                }
               
                MessageBox.Show("账号"+ account + "新增/修改 成功,共影响 " +i.ToString()+"行数据.", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
          

            //  

        }
        public int  updataOrAddUser(string[] userInfo)
        {
          
           int i = empm.updataOrAddUser(userInfo);
            return i;

        }

        private void cbProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            int processNameIndex = this.cbProcess.SelectedIndex;
            if(processNameIndex >= 0)
            {
                this.txtProcessID.Text = this.emps[processNameIndex].ID;
            }
           

        }

        private async void  cbProcess_ClickAsync(object sender, EventArgs e)
        {
            if (this.cbOrg.SelectedIndex == -1)
            {
                MessageBox.Show("请选择所属厂区");
                return;
            }
            string Org = this.cbOrg.SelectedItem.ToString();
           // str1.IndexOf("字")
            if (Org.IndexOf("SAA") != -1)
            {
                this.emps = await empm.GetAllProducts(this.SAAUlr);
            }
            else
            {
                this.emps = await empm.GetAllProducts(this.TOPUlr);
            }
          
            if (emps.Count <= 0)
            {
                MessageBox.Show("获取制程失败,请查询是否正常连线服务器");
            }
            this.cbProcess.Items.Clear();
            for (int i = 0; i < emps.Count; i++)
            {
                this.cbProcess.Items.Add(emps[i].Name);
            }

        }

        private void cbShowPWD_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbShowPWD.Checked)
            {
                this.txtPassword.PasswordChar = default(char); 
            }
            else
            {
                this.txtPassword.PasswordChar = '*';
            }
        }

        private async void cbSProcess_Click(object sender, EventArgs e)
        {
            string Org = "";
            if (this.comboBox1.Items.Count <= 0)
            {
                return;
            }
              Org = this.comboBox1.SelectedItem.ToString();
            // str1.IndexOf("字")
            if (Org.IndexOf("SAA") != -1)
            {
                this.emps = await empm.GetAllProducts(this.SAAUlr);
            }
            else
            {
                this.emps = await empm.GetAllProducts(this.TOPUlr);
            }

             
            if (emps.Count <= 0)
            {
                MessageBox.Show("获取制程失败,请查询是否正常连线服务器");
                return;
            }
            this.cbSProcess.Items.Clear();
            for (int i = 0; i < emps.Count; i++)
            {
                this.cbSProcess.Items.Add(emps[i].Name);
            }
            this.cbSProcess.Items.Add("");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.getUsers(-1);
        }
        public void getUsers(int userID)
        {

            int processNameIndex = this.cbSProcess.SelectedIndex;
            string processID = "-1";
            if (this.cbSProcess.SelectedItem != null && this.cbSProcess.SelectedItem.ToString() != "")
            {
                processID = this.emps[processNameIndex].ID;
            }

            string userName = this.txtQUserName.Text.Trim();
            string account = this.txtQAccount.Text.Trim().ToUpper();

            // if(processID == "-1"  && userName.Length<=0 && account.Length <=0)
            //  {
            //    MessageBox.Show("请指定查询条件");
            //     return;
            // }
            string[] searchParameter = { account, processID, userName };
            DataTable users = empm.getUserInfo(searchParameter);
            this.dgvUserInfo.DataSource = users;

            for (int i=0; i < this.dgvUserInfo.Rows.Count; i++)
            {
                string a = this.dgvUserInfo.Rows[i].Cells["ID"].Value.ToString();
                string b = userID.ToString();
                if (this.dgvUserInfo.Rows[i].Cells["ID"].Value.ToString() == userID.ToString())
                {
                    this.dgvUserInfo.Rows[i].Selected = true;
                }
            } 
        }

        private async void dgvUserInfo_CellDoubleClickAsync(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (this.emps == null || this.emps.Count <= 0)
            {

                string Org = this.cbOrg.SelectedItem.ToString();
                // str1.IndexOf("字")
                if (Org.IndexOf("SAA") != -1)
                {
                    this.emps = await empm.GetAllProducts(this.SAAUlr);
                }
                else
                {
                    this.emps = await empm.GetAllProducts(this.TOPUlr);
                }
            }
            else
            {
                if (emps.Count <= 0)
                {
                    MessageBox.Show("获取制程失败,请查询是否正常连线服务器");
                    return;
                }
            }
                this.cbProcess.Items.Clear();
                for (int i = 0; i < emps.Count; i++)
                {
                    this.cbProcess.Items.Add(emps[i].Name);
                }
            
           
           

            string accountID = this.dgvUserInfo["ID", e.RowIndex].Value.ToString();
            string account = this.dgvUserInfo["account", e.RowIndex].Value.ToString();
            string userName = this.dgvUserInfo["userName", e.RowIndex].Value.ToString();
            string processID = this.dgvUserInfo["deptID", e.RowIndex].Value.ToString();
           // string processID = this.dgvUserInfo["--", e.RowIndex].Value.ToString();
           // string pmarsk = this.dgvUserInfo["--", e.RowIndex].Value.ToString();
            string umarsk = this.dgvUserInfo["marsk", e.RowIndex].Value.ToString();
            for(int i = 0; i < this.emps.Count; i++)
            {
                if(processID == this.emps[i].ID.ToString())
                {
                    this.cbProcess.SelectedItem = this.emps[i].Name;
                }
            } 
            this.txtAccountID.Text = accountID;
            this.txtAccount.Text = account;
            this.txtUserName.Text = userName;
            this.txtProcessID.Text = processID;
            this.txtUmarsk.Text = umarsk;
            this.txtPassword.Text = "";

        }

        private void FrmMesEmployee_Load(object sender, EventArgs e)
        {
            this.gbAdduser.Enabled = false;
            this.gbChangAdd.Visible = false;
        }

        private void gbAdduser_Enter(object sender, EventArgs e)
        {
          
        }

        private void gbSearch_Enter(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.txtFrmPwd.Text = "";
            this.labmes.Text = "";
            this.gbChangAdd.Visible = true;
           
        }

        private void butFromYes_Click(object sender, EventArgs e)
        {
            this.checkFromPWD();
        }

        private void butFrmCenl_Click(object sender, EventArgs e)
        {
            this.gbChangAdd.Visible = false;
        }

        private void txtFrmPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                this.checkFromPWD();
            }
        }
        private void checkFromPWD()
        {
            string pwd = this.txtFrmPwd.Text.Trim();
            if (pwd == "Sabrina2021")
            {
                this.gbAdduser.Enabled = true;
                this.gbChangAdd.Visible = false;
            }
            else
            {
                this.labmes.Text = "密码错误！";
            }
        }

        private async void cbOrg_Click(object sender, EventArgs e)
        {
            this.Orgs = await orgm.getOrgs(TOPTest);
            if (this.Orgs.Count <= 0)
            {
                this.Orgs = await orgm.getOrgs(SAATest);
                
            }

            if (this.Orgs.Count <= 0)
            {
                MessageBox.Show("获取厂区失败,请查询是否正常连线服务器");
                return;
            }
            this.cbOrg.Items.Clear();
            for (int i = 0; i < this.Orgs.Count; i++)
            {
                this.cbOrg.Items.Add(Orgs[i].ReportPlaceName);
            }
        }

        private void cbOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PlaceNameIndex = this.cbOrg.SelectedIndex;
            if (PlaceNameIndex >= 0)
            {
                this.txtOrgID.Text = this.Orgs[PlaceNameIndex].ReportPlaceId.ToString();
            }
        }

        private async void comboBox1_Click(object sender, EventArgs e)
        {
            this.Orgs = await orgm.getOrgs(TOPTest);
            if (this.Orgs.Count <= 0)
            {
                this.Orgs = await orgm.getOrgs(SAATest);

            }

            if (this.Orgs.Count <= 0)
            {
                MessageBox.Show("获取厂区失败,请查询是否正常连线服务器");
                return;
            }
            this.comboBox1.Items.Clear();
            for (int i = 0; i < this.Orgs.Count; i++)
            {
                this.comboBox1.Items.Add(Orgs[i].ReportPlaceName);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PlaceNameIndex = this.comboBox1.SelectedIndex;
            if (PlaceNameIndex >= 0)
            {
                this.txtOrgID.Text = this.Orgs[PlaceNameIndex].ReportPlaceId.ToString();
            }
        }
    }
}
