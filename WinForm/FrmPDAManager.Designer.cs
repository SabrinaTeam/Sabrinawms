namespace WinForm
{
    partial class FrmPDAManager
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUUID = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtMode = new System.Windows.Forms.TextBox();
            this.txtDept = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtMark = new System.Windows.Forms.TextBox();
            this.dtpBuyDate = new System.Windows.Forms.DateTimePicker();
            this.dtpUserDate = new System.Windows.Forms.DateTimePicker();
            this.butAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDevices = new System.Windows.Forms.DataGridView();
            this.butSave = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.cbSelectedALL = new System.Windows.Forms.CheckBox();
            this.cbUUID = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "財產編號:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "財產名稱:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(509, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "財產型號:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(308, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "購入日期:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(716, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "領用日期:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(509, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "領用部門:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(731, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "責任人:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "備注:";
            // 
            // txtUUID
            // 
            this.txtUUID.Location = new System.Drawing.Point(74, 15);
            this.txtUUID.Name = "txtUUID";
            this.txtUUID.Size = new System.Drawing.Size(226, 21);
            this.txtUUID.TabIndex = 0;
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(74, 40);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(226, 21);
            this.txtNumber.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(374, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(119, 21);
            this.txtName.TabIndex = 3;
            // 
            // txtMode
            // 
            this.txtMode.Location = new System.Drawing.Point(574, 16);
            this.txtMode.Name = "txtMode";
            this.txtMode.Size = new System.Drawing.Size(120, 21);
            this.txtMode.TabIndex = 4;
            // 
            // txtDept
            // 
            this.txtDept.Location = new System.Drawing.Point(574, 41);
            this.txtDept.Name = "txtDept";
            this.txtDept.Size = new System.Drawing.Size(120, 21);
            this.txtDept.TabIndex = 5;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(784, 42);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 21);
            this.txtUser.TabIndex = 7;
            // 
            // txtMark
            // 
            this.txtMark.Location = new System.Drawing.Point(74, 67);
            this.txtMark.Name = "txtMark";
            this.txtMark.Size = new System.Drawing.Size(810, 21);
            this.txtMark.TabIndex = 8;
            // 
            // dtpBuyDate
            // 
            this.dtpBuyDate.Location = new System.Drawing.Point(373, 19);
            this.dtpBuyDate.Name = "dtpBuyDate";
            this.dtpBuyDate.Size = new System.Drawing.Size(120, 21);
            this.dtpBuyDate.TabIndex = 2;
            // 
            // dtpUserDate
            // 
            this.dtpUserDate.Location = new System.Drawing.Point(781, 15);
            this.dtpUserDate.Name = "dtpUserDate";
            this.dtpUserDate.Size = new System.Drawing.Size(103, 21);
            this.dtpUserDate.TabIndex = 6;
            // 
            // butAdd
            // 
            this.butAdd.Location = new System.Drawing.Point(890, 56);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(100, 32);
            this.butAdd.TabIndex = 9;
            this.butAdd.Text = "新增";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbUUID);
            this.groupBox1.Controls.Add(this.cbSelectedALL);
            this.groupBox1.Controls.Add(this.butSearch);
            this.groupBox1.Controls.Add(this.butAdd);
            this.groupBox1.Controls.Add(this.dtpUserDate);
            this.groupBox1.Controls.Add(this.dtpBuyDate);
            this.groupBox1.Controls.Add(this.txtMark);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.txtDept);
            this.groupBox1.Controls.Add(this.txtMode);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtNumber);
            this.groupBox1.Controls.Add(this.txtUUID);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(996, 94);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "設備基本資料";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDevices);
            this.groupBox2.Location = new System.Drawing.Point(0, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(996, 578);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "設備清單";
            // 
            // dgvDevices
            // 
            this.dgvDevices.AllowUserToAddRows = false;
            this.dgvDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDevices.Location = new System.Drawing.Point(3, 17);
            this.dgvDevices.Name = "dgvDevices";
            this.dgvDevices.RowTemplate.Height = 23;
            this.dgvDevices.Size = new System.Drawing.Size(990, 558);
            this.dgvDevices.TabIndex = 0;
            this.dgvDevices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDevices_CellDoubleClick);
            this.dgvDevices.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDevices_RowPostPaint);
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(914, 688);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(75, 23);
            this.butSave.TabIndex = 21;
            this.butSave.Text = "保存";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(13, 691);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 22;
            this.butCancel.Text = "取消";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(890, 22);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(100, 32);
            this.butSearch.TabIndex = 10;
            this.butSearch.Text = "查詢";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // cbSelectedALL
            // 
            this.cbSelectedALL.AutoSize = true;
            this.cbSelectedALL.Location = new System.Drawing.Point(890, 4);
            this.cbSelectedALL.Name = "cbSelectedALL";
            this.cbSelectedALL.Size = new System.Drawing.Size(48, 16);
            this.cbSelectedALL.TabIndex = 11;
            this.cbSelectedALL.Text = "所有";
            this.cbSelectedALL.UseVisualStyleBackColor = true;
            // 
            // cbUUID
            // 
            this.cbUUID.AutoSize = true;
            this.cbUUID.ForeColor = System.Drawing.Color.Red;
            this.cbUUID.Location = new System.Drawing.Point(17, 18);
            this.cbUUID.Name = "cbUUID";
            this.cbUUID.Size = new System.Drawing.Size(54, 16);
            this.cbUUID.TabIndex = 12;
            this.cbUUID.Text = "UUID:";
            this.cbUUID.UseVisualStyleBackColor = true;
            // 
            // FrmPDAManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 723);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPDAManager";
            this.Text = "PDA掃描槍管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPDAManager_Load);
            this.Resize += new System.EventHandler(this.FrmPDAManager_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUUID;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtMode;
        private System.Windows.Forms.TextBox txtDept;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtMark;
        private System.Windows.Forms.DateTimePicker dtpBuyDate;
        private System.Windows.Forms.DateTimePicker dtpUserDate;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDevices;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.CheckBox cbSelectedALL;
        private System.Windows.Forms.CheckBox cbUUID;
    }
}