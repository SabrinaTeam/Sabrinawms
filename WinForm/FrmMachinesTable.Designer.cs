namespace WinForm
{
    partial class FrmMachineTypeTable
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
            this.gbMachinesTable = new System.Windows.Forms.GroupBox();
            this.dgvMachinesTable = new System.Windows.Forms.DataGridView();
            this.butStatusNormal = new System.Windows.Forms.Button();
            this.butStatusScrap = new System.Windows.Forms.Button();
            this.butStatusRepair = new System.Windows.Forms.Button();
            this.txtMachineTypeName = new System.Windows.Forms.TextBox();
            this.txtMachineTypeShortName = new System.Windows.Forms.TextBox();
            this.txtMachinesMarckKhmer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.butAddMachines = new System.Windows.Forms.Button();
            this.butConfirm = new System.Windows.Forms.Button();
            this.gbMemu = new System.Windows.Forms.GroupBox();
            this.butDeleteMachineType = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbMachinesTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachinesTable)).BeginInit();
            this.gbMemu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbMachinesTable
            // 
            this.gbMachinesTable.Controls.Add(this.dgvMachinesTable);
            this.gbMachinesTable.Location = new System.Drawing.Point(5, 95);
            this.gbMachinesTable.Name = "gbMachinesTable";
            this.gbMachinesTable.Size = new System.Drawing.Size(1202, 602);
            this.gbMachinesTable.TabIndex = 1;
            this.gbMachinesTable.TabStop = false;
            this.gbMachinesTable.Text = "机器表";
            // 
            // dgvMachinesTable
            // 
            this.dgvMachinesTable.AllowUserToAddRows = false;
            this.dgvMachinesTable.AllowUserToDeleteRows = false;
            this.dgvMachinesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMachinesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMachinesTable.Location = new System.Drawing.Point(3, 18);
            this.dgvMachinesTable.Name = "dgvMachinesTable";
            this.dgvMachinesTable.RowTemplate.Height = 24;
            this.dgvMachinesTable.Size = new System.Drawing.Size(1196, 581);
            this.dgvMachinesTable.TabIndex = 14;
            this.dgvMachinesTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMachinesTable_CellClick);
            this.dgvMachinesTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMachinesTable_CellFormatting);
            this.dgvMachinesTable.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvMachinesTable_RowPostPaint);
            // 
            // butStatusNormal
            // 
            this.butStatusNormal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.butStatusNormal.Location = new System.Drawing.Point(1020, 25);
            this.butStatusNormal.Name = "butStatusNormal";
            this.butStatusNormal.Size = new System.Drawing.Size(75, 52);
            this.butStatusNormal.TabIndex = 13;
            this.butStatusNormal.Text = "正常";
            this.butStatusNormal.UseVisualStyleBackColor = false;
            this.butStatusNormal.Visible = false;
            // 
            // butStatusScrap
            // 
            this.butStatusScrap.BackColor = System.Drawing.Color.Red;
            this.butStatusScrap.Location = new System.Drawing.Point(885, 25);
            this.butStatusScrap.Name = "butStatusScrap";
            this.butStatusScrap.Size = new System.Drawing.Size(75, 52);
            this.butStatusScrap.TabIndex = 9;
            this.butStatusScrap.Text = "报废";
            this.butStatusScrap.UseVisualStyleBackColor = false;
            this.butStatusScrap.Visible = false;
            // 
            // butStatusRepair
            // 
            this.butStatusRepair.BackColor = System.Drawing.Color.Yellow;
            this.butStatusRepair.Location = new System.Drawing.Point(953, 25);
            this.butStatusRepair.Name = "butStatusRepair";
            this.butStatusRepair.Size = new System.Drawing.Size(75, 52);
            this.butStatusRepair.TabIndex = 12;
            this.butStatusRepair.Text = "维修";
            this.butStatusRepair.UseVisualStyleBackColor = false;
            this.butStatusRepair.Visible = false;
            // 
            // txtMachineTypeName
            // 
            this.txtMachineTypeName.Location = new System.Drawing.Point(88, 23);
            this.txtMachineTypeName.Name = "txtMachineTypeName";
            this.txtMachineTypeName.Size = new System.Drawing.Size(183, 22);
            this.txtMachineTypeName.TabIndex = 2;
            // 
            // txtMachineTypeShortName
            // 
            this.txtMachineTypeShortName.Location = new System.Drawing.Point(360, 21);
            this.txtMachineTypeShortName.Name = "txtMachineTypeShortName";
            this.txtMachineTypeShortName.Size = new System.Drawing.Size(144, 22);
            this.txtMachineTypeShortName.TabIndex = 3;
            this.txtMachineTypeShortName.TextChanged += new System.EventHandler(this.txtMachineTypeNameEN_TextChanged);
            // 
            // txtMachinesMarckKhmer
            // 
            this.txtMachinesMarckKhmer.Location = new System.Drawing.Point(125, 53);
            this.txtMachinesMarckKhmer.Name = "txtMachinesMarckKhmer";
            this.txtMachinesMarckKhmer.Size = new System.Drawing.Size(379, 22);
            this.txtMachinesMarckKhmer.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "机器类别名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "机器类别代号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "机器类别名称(柬文)";
            // 
            // butAddMachines
            // 
            this.butAddMachines.Location = new System.Drawing.Point(693, 21);
            this.butAddMachines.Name = "butAddMachines";
            this.butAddMachines.Size = new System.Drawing.Size(83, 56);
            this.butAddMachines.TabIndex = 8;
            this.butAddMachines.Text = "增加";
            this.butAddMachines.UseVisualStyleBackColor = true;
            this.butAddMachines.Click += new System.EventHandler(this.butAddMachineType_Click);
            // 
            // butConfirm
            // 
            this.butConfirm.Location = new System.Drawing.Point(1101, 21);
            this.butConfirm.Name = "butConfirm";
            this.butConfirm.Size = new System.Drawing.Size(92, 56);
            this.butConfirm.TabIndex = 10;
            this.butConfirm.Text = "保存提交";
            this.butConfirm.UseVisualStyleBackColor = true;
            this.butConfirm.Click += new System.EventHandler(this.butConfirm_Click);
            // 
            // gbMemu
            // 
            this.gbMemu.Controls.Add(this.label4);
            this.gbMemu.Controls.Add(this.pictureBox1);
            this.gbMemu.Controls.Add(this.butConfirm);
            this.gbMemu.Controls.Add(this.butStatusNormal);
            this.gbMemu.Controls.Add(this.butDeleteMachineType);
            this.gbMemu.Controls.Add(this.butStatusScrap);
            this.gbMemu.Controls.Add(this.butStatusRepair);
            this.gbMemu.Controls.Add(this.txtMachineTypeShortName);
            this.gbMemu.Controls.Add(this.butAddMachines);
            this.gbMemu.Controls.Add(this.label3);
            this.gbMemu.Controls.Add(this.label2);
            this.gbMemu.Controls.Add(this.label1);
            this.gbMemu.Controls.Add(this.txtMachinesMarckKhmer);
            this.gbMemu.Controls.Add(this.txtMachineTypeName);
            this.gbMemu.Location = new System.Drawing.Point(5, 4);
            this.gbMemu.Name = "gbMemu";
            this.gbMemu.Size = new System.Drawing.Size(1199, 87);
            this.gbMemu.TabIndex = 11;
            this.gbMemu.TabStop = false;
            this.gbMemu.Text = "功能菜单";
            // 
            // butDeleteMachineType
            // 
            this.butDeleteMachineType.Location = new System.Drawing.Point(796, 23);
            this.butDeleteMachineType.Name = "butDeleteMachineType";
            this.butDeleteMachineType.Size = new System.Drawing.Size(83, 56);
            this.butDeleteMachineType.TabIndex = 14;
            this.butDeleteMachineType.Text = "删除";
            this.butDeleteMachineType.UseVisualStyleBackColor = true;
            this.butDeleteMachineType.Click += new System.EventHandler(this.butDeleteMachineType_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "机器名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 180;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "机器名称(英文)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "机器备注";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(522, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(111, 61);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(523, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "图片";
            // 
            // FrmMachineTypeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 698);
            this.Controls.Add(this.gbMachinesTable);
            this.Controls.Add(this.gbMemu);
            this.Name = "FrmMachineTypeTable";
            this.Text = "机器类别列表维护";
            this.Load += new System.EventHandler(this.FrmMachineTypeTable_Load);
            this.SizeChanged += new System.EventHandler(this.FrmMachineTypeTable_SizeChanged);
            this.gbMachinesTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachinesTable)).EndInit();
            this.gbMemu.ResumeLayout(false);
            this.gbMemu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbMachinesTable;
        private System.Windows.Forms.TextBox txtMachineTypeName;
        private System.Windows.Forms.TextBox txtMachineTypeShortName;
        private System.Windows.Forms.TextBox txtMachinesMarckKhmer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butAddMachines;
        private System.Windows.Forms.Button butConfirm;
        private System.Windows.Forms.GroupBox gbMemu;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button butDeleteMachineType;
        private System.Windows.Forms.Button butStatusNormal;
        private System.Windows.Forms.Button butStatusRepair;
        private System.Windows.Forms.Button butStatusScrap;
        private System.Windows.Forms.DataGridView dgvMachinesTable;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
    }
}