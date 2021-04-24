namespace WinForm
{
    partial class FrmMesEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.gbUserInfo = new System.Windows.Forms.GroupBox();
            this.gbChangAdd = new System.Windows.Forms.GroupBox();
            this.labmes = new System.Windows.Forms.Label();
            this.txtFrmPwd = new System.Windows.Forms.TextBox();
            this.butFrmCenl = new System.Windows.Forms.Button();
            this.butFromYes = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvUserInfo = new System.Windows.Forms.DataGridView();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtQAccount = new System.Windows.Forms.TextBox();
            this.txtQUserName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbSProcess = new System.Windows.Forms.ComboBox();
            this.gbAdduser = new System.Windows.Forms.GroupBox();
            this.txtOrgID = new System.Windows.Forms.TextBox();
            this.cbOrg = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProcessID = new System.Windows.Forms.TextBox();
            this.txtAccountID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbShowPWD = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtUmarsk = new System.Windows.Forms.TextBox();
            this.txtPmarsk = new System.Windows.Forms.TextBox();
            this.cbProcess = new System.Windows.Forms.ComboBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.gbUserInfo.SuspendLayout();
            this.gbChangAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserInfo)).BeginInit();
            this.gbSearch.SuspendLayout();
            this.gbAdduser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "制程名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户姓名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(7, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "制程备注";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(6, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户账号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(6, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "用户密码";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(6, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "用户姓名";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(7, 259);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "用户备注";
            // 
            // gbUserInfo
            // 
            this.gbUserInfo.Controls.Add(this.gbChangAdd);
            this.gbUserInfo.Controls.Add(this.dgvUserInfo);
            this.gbUserInfo.Location = new System.Drawing.Point(6, 66);
            this.gbUserInfo.Name = "gbUserInfo";
            this.gbUserInfo.Size = new System.Drawing.Size(698, 409);
            this.gbUserInfo.TabIndex = 8;
            this.gbUserInfo.TabStop = false;
            this.gbUserInfo.Text = "用户信息";
            // 
            // gbChangAdd
            // 
            this.gbChangAdd.Controls.Add(this.labmes);
            this.gbChangAdd.Controls.Add(this.txtFrmPwd);
            this.gbChangAdd.Controls.Add(this.butFrmCenl);
            this.gbChangAdd.Controls.Add(this.butFromYes);
            this.gbChangAdd.Controls.Add(this.label11);
            this.gbChangAdd.Location = new System.Drawing.Point(158, 76);
            this.gbChangAdd.Name = "gbChangAdd";
            this.gbChangAdd.Size = new System.Drawing.Size(364, 154);
            this.gbChangAdd.TabIndex = 1;
            this.gbChangAdd.TabStop = false;
            this.gbChangAdd.Text = "修改用户";
            // 
            // labmes
            // 
            this.labmes.AutoSize = true;
            this.labmes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labmes.ForeColor = System.Drawing.Color.Red;
            this.labmes.Location = new System.Drawing.Point(105, 25);
            this.labmes.Name = "labmes";
            this.labmes.Size = new System.Drawing.Size(0, 16);
            this.labmes.TabIndex = 5;
            // 
            // txtFrmPwd
            // 
            this.txtFrmPwd.Location = new System.Drawing.Point(103, 42);
            this.txtFrmPwd.Name = "txtFrmPwd";
            this.txtFrmPwd.PasswordChar = '*';
            this.txtFrmPwd.Size = new System.Drawing.Size(213, 21);
            this.txtFrmPwd.TabIndex = 4;
            this.txtFrmPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFrmPwd_KeyDown);
            // 
            // butFrmCenl
            // 
            this.butFrmCenl.Location = new System.Drawing.Point(114, 86);
            this.butFrmCenl.Name = "butFrmCenl";
            this.butFrmCenl.Size = new System.Drawing.Size(75, 23);
            this.butFrmCenl.TabIndex = 3;
            this.butFrmCenl.Text = "取消";
            this.butFrmCenl.UseVisualStyleBackColor = true;
            this.butFrmCenl.Click += new System.EventHandler(this.butFrmCenl_Click);
            // 
            // butFromYes
            // 
            this.butFromYes.Location = new System.Drawing.Point(221, 86);
            this.butFromYes.Name = "butFromYes";
            this.butFromYes.Size = new System.Drawing.Size(75, 23);
            this.butFromYes.TabIndex = 2;
            this.butFromYes.Text = "确认";
            this.butFromYes.UseVisualStyleBackColor = true;
            this.butFromYes.Click += new System.EventHandler(this.butFromYes_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(68, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "密码";
            // 
            // dgvUserInfo
            // 
            this.dgvUserInfo.AllowUserToAddRows = false;
            this.dgvUserInfo.AllowUserToDeleteRows = false;
            this.dgvUserInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUserInfo.Location = new System.Drawing.Point(3, 17);
            this.dgvUserInfo.MultiSelect = false;
            this.dgvUserInfo.Name = "dgvUserInfo";
            this.dgvUserInfo.ReadOnly = true;
            this.dgvUserInfo.RowTemplate.Height = 23;
            this.dgvUserInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserInfo.Size = new System.Drawing.Size(692, 389);
            this.dgvUserInfo.TabIndex = 0;
            this.dgvUserInfo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserInfo_CellDoubleClickAsync);
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.comboBox1);
            this.gbSearch.Controls.Add(this.label13);
            this.gbSearch.Controls.Add(this.button1);
            this.gbSearch.Controls.Add(this.btnSearch);
            this.gbSearch.Controls.Add(this.txtQAccount);
            this.gbSearch.Controls.Add(this.txtQUserName);
            this.gbSearch.Controls.Add(this.label9);
            this.gbSearch.Controls.Add(this.cbSProcess);
            this.gbSearch.Controls.Add(this.label1);
            this.gbSearch.Controls.Add(this.label2);
            this.gbSearch.Location = new System.Drawing.Point(6, 5);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(698, 55);
            this.gbSearch.TabIndex = 9;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "查询条件";
            this.gbSearch.Enter += new System.EventHandler(this.gbSearch_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(622, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 37);
            this.button1.TabIndex = 15;
            this.button1.Text = "新增";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(556, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(61, 37);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtQAccount
            // 
            this.txtQAccount.Location = new System.Drawing.Point(450, 20);
            this.txtQAccount.Name = "txtQAccount";
            this.txtQAccount.Size = new System.Drawing.Size(100, 21);
            this.txtQAccount.TabIndex = 13;
            // 
            // txtQUserName
            // 
            this.txtQUserName.Location = new System.Drawing.Point(275, 20);
            this.txtQUserName.Name = "txtQUserName";
            this.txtQUserName.Size = new System.Drawing.Size(100, 21);
            this.txtQUserName.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(391, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "用户账号";
            // 
            // cbSProcess
            // 
            this.cbSProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSProcess.FormattingEnabled = true;
            this.cbSProcess.ItemHeight = 12;
            this.cbSProcess.Location = new System.Drawing.Point(71, 32);
            this.cbSProcess.Name = "cbSProcess";
            this.cbSProcess.Size = new System.Drawing.Size(121, 20);
            this.cbSProcess.TabIndex = 2;
            this.cbSProcess.Click += new System.EventHandler(this.cbSProcess_Click);
            // 
            // gbAdduser
            // 
            this.gbAdduser.Controls.Add(this.txtOrgID);
            this.gbAdduser.Controls.Add(this.cbOrg);
            this.gbAdduser.Controls.Add(this.label12);
            this.gbAdduser.Controls.Add(this.txtProcessID);
            this.gbAdduser.Controls.Add(this.txtAccountID);
            this.gbAdduser.Controls.Add(this.label10);
            this.gbAdduser.Controls.Add(this.cbShowPWD);
            this.gbAdduser.Controls.Add(this.btnConfirm);
            this.gbAdduser.Controls.Add(this.btnClear);
            this.gbAdduser.Controls.Add(this.txtUmarsk);
            this.gbAdduser.Controls.Add(this.txtPmarsk);
            this.gbAdduser.Controls.Add(this.cbProcess);
            this.gbAdduser.Controls.Add(this.txtUserName);
            this.gbAdduser.Controls.Add(this.txtPassword);
            this.gbAdduser.Controls.Add(this.txtAccount);
            this.gbAdduser.Controls.Add(this.label4);
            this.gbAdduser.Controls.Add(this.label3);
            this.gbAdduser.Controls.Add(this.label8);
            this.gbAdduser.Controls.Add(this.label5);
            this.gbAdduser.Controls.Add(this.label6);
            this.gbAdduser.Controls.Add(this.label7);
            this.gbAdduser.Location = new System.Drawing.Point(709, 4);
            this.gbAdduser.Name = "gbAdduser";
            this.gbAdduser.Size = new System.Drawing.Size(212, 471);
            this.gbAdduser.TabIndex = 10;
            this.gbAdduser.TabStop = false;
            this.gbAdduser.Text = "新增/修改用户";
            this.gbAdduser.Enter += new System.EventHandler(this.gbAdduser_Enter);
            // 
            // txtOrgID
            // 
            this.txtOrgID.Location = new System.Drawing.Point(151, 183);
            this.txtOrgID.Name = "txtOrgID";
            this.txtOrgID.ReadOnly = true;
            this.txtOrgID.Size = new System.Drawing.Size(41, 21);
            this.txtOrgID.TabIndex = 24;
            // 
            // cbOrg
            // 
            this.cbOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrg.FormattingEnabled = true;
            this.cbOrg.Location = new System.Drawing.Point(66, 183);
            this.cbOrg.Name = "cbOrg";
            this.cbOrg.Size = new System.Drawing.Size(79, 20);
            this.cbOrg.TabIndex = 22;
            this.cbOrg.SelectedIndexChanged += new System.EventHandler(this.cbOrg_SelectedIndexChanged);
            this.cbOrg.Click += new System.EventHandler(this.cbOrg_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(6, 187);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "所属厂区";
            // 
            // txtProcessID
            // 
            this.txtProcessID.Location = new System.Drawing.Point(151, 219);
            this.txtProcessID.Name = "txtProcessID";
            this.txtProcessID.ReadOnly = true;
            this.txtProcessID.Size = new System.Drawing.Size(41, 21);
            this.txtProcessID.TabIndex = 21;
            // 
            // txtAccountID
            // 
            this.txtAccountID.Location = new System.Drawing.Point(66, 25);
            this.txtAccountID.Name = "txtAccountID";
            this.txtAccountID.ReadOnly = true;
            this.txtAccountID.Size = new System.Drawing.Size(126, 21);
            this.txtAccountID.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "用户ID";
            // 
            // cbShowPWD
            // 
            this.cbShowPWD.AutoSize = true;
            this.cbShowPWD.ForeColor = System.Drawing.Color.Blue;
            this.cbShowPWD.Location = new System.Drawing.Point(67, 94);
            this.cbShowPWD.Name = "cbShowPWD";
            this.cbShowPWD.Size = new System.Drawing.Size(72, 16);
            this.cbShowPWD.TabIndex = 2;
            this.cbShowPWD.Text = "显示密码";
            this.cbShowPWD.UseVisualStyleBackColor = true;
            this.cbShowPWD.CheckedChanged += new System.EventHandler(this.cbShowPWD_CheckedChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(117, 434);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(9, 434);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtUmarsk
            // 
            this.txtUmarsk.AllowDrop = true;
            this.txtUmarsk.Location = new System.Drawing.Point(9, 274);
            this.txtUmarsk.Multiline = true;
            this.txtUmarsk.Name = "txtUmarsk";
            this.txtUmarsk.Size = new System.Drawing.Size(191, 62);
            this.txtUmarsk.TabIndex = 7;
            // 
            // txtPmarsk
            // 
            this.txtPmarsk.AllowDrop = true;
            this.txtPmarsk.Location = new System.Drawing.Point(6, 354);
            this.txtPmarsk.Multiline = true;
            this.txtPmarsk.Name = "txtPmarsk";
            this.txtPmarsk.Size = new System.Drawing.Size(191, 74);
            this.txtPmarsk.TabIndex = 6;
            // 
            // cbProcess
            // 
            this.cbProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcess.FormattingEnabled = true;
            this.cbProcess.Location = new System.Drawing.Point(66, 219);
            this.cbProcess.Name = "cbProcess";
            this.cbProcess.Size = new System.Drawing.Size(79, 20);
            this.cbProcess.TabIndex = 5;
            this.cbProcess.SelectedIndexChanged += new System.EventHandler(this.cbProcess_SelectedIndexChanged);
            this.cbProcess.Click += new System.EventHandler(this.cbProcess_ClickAsync);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(66, 146);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(126, 21);
            this.txtUserName.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(66, 110);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(126, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(66, 65);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(126, 21);
            this.txtAccount.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(6, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "所属制程";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(71, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(79, 20);
            this.comboBox1.TabIndex = 24;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(11, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 25;
            this.label13.Text = "所属厂区";
            // 
            // FrmMesEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 477);
            this.Controls.Add(this.gbAdduser);
            this.Controls.Add(this.gbSearch);
            this.Controls.Add(this.gbUserInfo);
            this.Name = "FrmMesEmployee";
            this.Text = "Mes用户管理";
            this.Load += new System.EventHandler(this.FrmMesEmployee_Load);
            this.gbUserInfo.ResumeLayout(false);
            this.gbChangAdd.ResumeLayout(false);
            this.gbChangAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserInfo)).EndInit();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.gbAdduser.ResumeLayout(false);
            this.gbAdduser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbUserInfo;
        private System.Windows.Forms.DataGridView dgvUserInfo;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtQAccount;
        private System.Windows.Forms.TextBox txtQUserName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbSProcess;
        private System.Windows.Forms.GroupBox gbAdduser;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtUmarsk;
        private System.Windows.Forms.TextBox txtPmarsk;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.CheckBox cbShowPWD;
        private System.Windows.Forms.TextBox txtAccountID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gbChangAdd;
        private System.Windows.Forms.TextBox txtFrmPwd;
        private System.Windows.Forms.Button butFrmCenl;
        private System.Windows.Forms.Button butFromYes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labmes;
        private System.Windows.Forms.TextBox txtOrgID;
        private System.Windows.Forms.ComboBox cbOrg;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProcessID;
        private System.Windows.Forms.ComboBox cbProcess;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label13;
    }
}