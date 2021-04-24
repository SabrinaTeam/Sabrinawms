using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class CompletedToMesLogin : Form
    {
        private static CompletedToMesLogin frm;
        private static FrmMain mainfrom;
        public string account = "";
        public string password = "";

        CompletedToMesLoginManager ctmm = new CompletedToMesLoginManager(); 
        public CompletedToMesLogin()
        {
            InitializeComponent();
            
        }
        public static CompletedToMesLogin GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new CompletedToMesLogin();
            }
            return frm;
        }

        private void butLogin_Click(object sender, EventArgs e)
        {

            this.login();
        }
        public void login()
        {

            this.account = this.txtAccount.Text.Trim().ToUpper();
            this.password = this.txtPwd.Text.Trim();
            
            DataTable dt = ctmm.getLoginByAccount(this.account, this.password);
            if (dt.Rows.Count != 1)
            {
                this.labMsg.Text = "用户或密码错误";
                AccessAppSettings(this.account, this.password);
                return;
            }
            // 保存登录信息
            AccessAppSettings(this.account, this.password);
            FrmCompletedToMes frm = FrmCompletedToMes.GetSingleton(dt);
            frm.MdiParent = mainfrom;
            frm.Show();
            frm.Activate();
            this.Visible = false;
        }
        private void AccessAppSettings(string account,string password)
        {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //根据Key读取<add>元素的Value
          //  string name = config.AppSettings.Settings["Account"].Value;
        //    string pwd = config.AppSettings.Settings["Pwd"].Value;
            //写入<add>元素的Value
            config.AppSettings.Settings["Account"].Value = account;
            config.AppSettings.Settings["pwd"].Value = password;

            //增加<add>元素
         //   config.AppSettings.Settings.Add("url", "http://www.fx163.net");
            //删除<add>元素
         //   config.AppSettings.Settings.Remove("name");
            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }


        private void txtAccount_TextChanged(object sender, EventArgs e)
        {
            this.txtAccount.BackColor = Color.White;
            this.labMsg.Text = "";

        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            this.txtPwd.BackColor = Color.White;
            this.labMsg.Text = "";
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            this.labMsg.Text = "";
            this.txtPwd.BackColor = Color.White;
            this.txtAccount.BackColor = Color.White;
            this.txtAccount.Text = "";
            this.txtPwd.Text = "";
        }

        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                this.txtPwd.Focus();
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                this.login();
            }
        }

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            this.txtPwd.Text = "";
        }

        private void CompletedToMesLogin_Load(object sender, EventArgs e)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.account = config.AppSettings.Settings["Account"].Value;
            this.password = config.AppSettings.Settings["Pwd"].Value;

            if (this.account.Length > 0)
            {
                this.txtAccount.Text = this.account;

            }
            if (this.password.Length > 0)
            {
                this.txtPwd.Text = this.password;

            }
            
        }
    }
}
