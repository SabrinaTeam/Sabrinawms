namespace WinForm
{
    partial class FrmRFIDNikeImport
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
            this.components = new System.ComponentModel.Container();
            this.bgDirectoryFiles = new System.Windows.Forms.GroupBox();
            this.dgvDirectoryFiles = new System.Windows.Forms.DataGridView();
            this.gbLoad = new System.Windows.Forms.GroupBox();
            this.labDirectoryCounts = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labDirectoryNowFile = new System.Windows.Forms.Label();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoadDirectory = new System.Windows.Forms.Button();
            this.txtSelectedDirectoryPath = new System.Windows.Forms.TextBox();
            this.btnSelectDirectory = new System.Windows.Forms.Button();
            this.MenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RmeCopyCells = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.RmeExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.bgDirectoryFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDirectoryFiles)).BeginInit();
            this.gbLoad.SuspendLayout();
            this.MenuRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgDirectoryFiles
            // 
            this.bgDirectoryFiles.Controls.Add(this.dgvDirectoryFiles);
            this.bgDirectoryFiles.Location = new System.Drawing.Point(4, 70);
            this.bgDirectoryFiles.Name = "bgDirectoryFiles";
            this.bgDirectoryFiles.Size = new System.Drawing.Size(638, 428);
            this.bgDirectoryFiles.TabIndex = 13;
            this.bgDirectoryFiles.TabStop = false;
            this.bgDirectoryFiles.Text = "解析内容";
            // 
            // dgvDirectoryFiles
            // 
            this.dgvDirectoryFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDirectoryFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDirectoryFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDirectoryFiles.Location = new System.Drawing.Point(3, 18);
            this.dgvDirectoryFiles.Name = "dgvDirectoryFiles";
            this.dgvDirectoryFiles.ReadOnly = true;
            this.dgvDirectoryFiles.RowTemplate.Height = 23;
            this.dgvDirectoryFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDirectoryFiles.Size = new System.Drawing.Size(632, 407);
            this.dgvDirectoryFiles.TabIndex = 0;
            this.dgvDirectoryFiles.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDirectoryFiles_CellMouseDown);
            this.dgvDirectoryFiles.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDirectoryFiles_RowPostPaint);
            // 
            // gbLoad
            // 
            this.gbLoad.Controls.Add(this.labDirectoryCounts);
            this.gbLoad.Controls.Add(this.label2);
            this.gbLoad.Controls.Add(this.labDirectoryNowFile);
            this.gbLoad.Controls.Add(this.pgBar);
            this.gbLoad.Controls.Add(this.btnSave);
            this.gbLoad.Controls.Add(this.btnLoadDirectory);
            this.gbLoad.Controls.Add(this.txtSelectedDirectoryPath);
            this.gbLoad.Controls.Add(this.btnSelectDirectory);
            this.gbLoad.Location = new System.Drawing.Point(2, 2);
            this.gbLoad.Name = "gbLoad";
            this.gbLoad.Size = new System.Drawing.Size(640, 63);
            this.gbLoad.TabIndex = 12;
            this.gbLoad.TabStop = false;
            this.gbLoad.Text = "导入条件";
            // 
            // labDirectoryCounts
            // 
            this.labDirectoryCounts.AutoSize = true;
            this.labDirectoryCounts.Location = new System.Drawing.Point(421, 20);
            this.labDirectoryCounts.Name = "labDirectoryCounts";
            this.labDirectoryCounts.Size = new System.Drawing.Size(11, 12);
            this.labDirectoryCounts.TabIndex = 11;
            this.labDirectoryCounts.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(406, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(8, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "/";
            // 
            // labDirectoryNowFile
            // 
            this.labDirectoryNowFile.AutoSize = true;
            this.labDirectoryNowFile.Location = new System.Drawing.Point(389, 20);
            this.labDirectoryNowFile.Name = "labDirectoryNowFile";
            this.labDirectoryNowFile.Size = new System.Drawing.Size(11, 12);
            this.labDirectoryNowFile.TabIndex = 9;
            this.labDirectoryNowFile.Text = "0";
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(6, 45);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(627, 10);
            this.pgBar.TabIndex = 8;
            this.pgBar.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(548, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 26);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "上传";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoadDirectory
            // 
            this.btnLoadDirectory.Location = new System.Drawing.Point(461, 14);
            this.btnLoadDirectory.Name = "btnLoadDirectory";
            this.btnLoadDirectory.Size = new System.Drawing.Size(81, 26);
            this.btnLoadDirectory.TabIndex = 6;
            this.btnLoadDirectory.Text = "加载数据";
            this.btnLoadDirectory.UseVisualStyleBackColor = true;
            this.btnLoadDirectory.Click += new System.EventHandler(this.btnLoadDirectory_Click);
            // 
            // txtSelectedDirectoryPath
            // 
            this.txtSelectedDirectoryPath.Location = new System.Drawing.Point(87, 17);
            this.txtSelectedDirectoryPath.Name = "txtSelectedDirectoryPath";
            this.txtSelectedDirectoryPath.ReadOnly = true;
            this.txtSelectedDirectoryPath.Size = new System.Drawing.Size(296, 22);
            this.txtSelectedDirectoryPath.TabIndex = 1;
            // 
            // btnSelectDirectory
            // 
            this.btnSelectDirectory.Location = new System.Drawing.Point(6, 16);
            this.btnSelectDirectory.Name = "btnSelectDirectory";
            this.btnSelectDirectory.Size = new System.Drawing.Size(75, 23);
            this.btnSelectDirectory.TabIndex = 0;
            this.btnSelectDirectory.Text = "选择目录";
            this.btnSelectDirectory.UseVisualStyleBackColor = true;
            this.btnSelectDirectory.Click += new System.EventHandler(this.btnSelectDirectory_Click);
            // 
            // MenuRight
            // 
            this.MenuRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RmeCopyCells,
            this.RmeCopyRows,
            this.RmeExportExcel});
            this.MenuRight.Name = "contextMenuStrip1";
            this.MenuRight.Size = new System.Drawing.Size(141, 70);
            // 
            // RmeCopyCells
            // 
            this.RmeCopyCells.Image = global::WinForm.Properties.Resources.icons8_复制_64;
            this.RmeCopyCells.Name = "RmeCopyCells";
            this.RmeCopyCells.Size = new System.Drawing.Size(140, 22);
            this.RmeCopyCells.Text = "CopyCells";
            this.RmeCopyCells.Click += new System.EventHandler(this.RmeCopyCells_Click);
            // 
            // RmeCopyRows
            // 
            this.RmeCopyRows.Image = global::WinForm.Properties.Resources.icons8_复制_48;
            this.RmeCopyRows.Name = "RmeCopyRows";
            this.RmeCopyRows.Size = new System.Drawing.Size(140, 22);
            this.RmeCopyRows.Text = "CopyRows";
            this.RmeCopyRows.Click += new System.EventHandler(this.RmeCopyRows_Click);
            // 
            // RmeExportExcel
            // 
            this.RmeExportExcel.Image = global::WinForm.Properties.Resources.Excel_32px_1185986_easyicon_net;
            this.RmeExportExcel.Name = "RmeExportExcel";
            this.RmeExportExcel.Size = new System.Drawing.Size(140, 22);
            this.RmeExportExcel.Text = "ExportExcel";
            this.RmeExportExcel.Click += new System.EventHandler(this.RmeExportExcel_Click);
            // 
            // FrmRFIDNikeImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 498);
            this.Controls.Add(this.bgDirectoryFiles);
            this.Controls.Add(this.gbLoad);
            this.Name = "FrmRFIDNikeImport";
            this.Text = "FrmRFIDNiknImport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.FrmRFIDLuluImport_Resize);
            this.bgDirectoryFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDirectoryFiles)).EndInit();
            this.gbLoad.ResumeLayout(false);
            this.gbLoad.PerformLayout();
            this.MenuRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox bgDirectoryFiles;
        private System.Windows.Forms.DataGridView dgvDirectoryFiles;
        private System.Windows.Forms.GroupBox gbLoad;
        private System.Windows.Forms.Label labDirectoryCounts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labDirectoryNowFile;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoadDirectory;
        private System.Windows.Forms.TextBox txtSelectedDirectoryPath;
        private System.Windows.Forms.Button btnSelectDirectory;
        private System.Windows.Forms.ContextMenuStrip MenuRight;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyCells;
        private System.Windows.Forms.ToolStripMenuItem RmeCopyRows;
        private System.Windows.Forms.ToolStripMenuItem RmeExportExcel;
    }
}