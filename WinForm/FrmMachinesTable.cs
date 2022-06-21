using BLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmMachineTypeTable : Form
    {
        public MachinesTableManager mtm = new MachinesTableManager();
        private static FrmMachineTypeTable frm;
        List<Machines> machineLists = new List<Machines>();
        public DataTable machineDatatables = new DataTable();
        public int delRowIndex =-1;
        public string delMachinesName = "";

        byte[] picPathdata = null;
        /*
        public string CurveTableName = "";
        public DataGridView dgv = null;
        public int CureNamesID = -1;
        public string Creator = "";
        public string CreateDate = "";

        */

        public int hiedcolumnindex = -1; //是否选中外面
        public FrmMachineTypeTable()
        {
            InitializeComponent();
            dgvMachinesTable.DoubleBufferedDataGirdView(true);

        }
        public static FrmMachineTypeTable GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmMachineTypeTable();
            }
            return frm;
        }


        private void txtMachineTypeNameEN_TextChanged(object sender, EventArgs e)
        {
            //this.txtMachineTypeNameEN.Text = txtMachineTypeNameEN.Text.ToUpper().Trim();
        }

        private void FrmMachineTypeTable_Load(object sender, EventArgs e)
        {
           this.machineDatatables = mtm.getAllMachineTypes();
            if (this.machineDatatables != null  && this.machineDatatables.Rows.Count > 0  )
            {

                this.dgvMachinesTable.DataSource = null;
                this.dgvMachinesTable.DataSource = this.machineDatatables;
            }

        }
        private void butAddMachineType_Click(object sender, EventArgs e)
        {
            string machinesTypeName =  this.txtMachineTypeName.Text.Trim();
            string machineTypeShortName =  this.txtMachineTypeShortName.Text.Trim().ToUpper();
            string machinesMarckKhmer = this.txtMachinesMarckKhmer.Text.Trim();
            string imagestr = "";

            //  FileStream fs = new FileStream(label4.Text, FileMode.Open);
            if (this.picPathdata != null)
            {
                //  byte[] imageBytes = new byte[this.picPathfs.Length];
               // byte[] imageBytes = this.picPathdata
              //  BinaryReader br = new BinaryReader(this.picPathfs);
               // imageBytes = br.ReadBytes(Convert.ToInt32(this.picPathfs.Length));//图片转换成二进制流
                imagestr = Convert.ToBase64String(this.picPathdata);//二进制转换成字符串
            }

            if (machinesTypeName.Length <= 0)
            {
                return;
            }
            DataGridViewImageCell cell = new DataGridViewImageCell();
            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;


            DataRow dr = this.machineDatatables.NewRow();
            dr["machineClass"] = "";
            dr["MachineName"] = machinesTypeName;
            dr["machineTypeShortName"] = machineTypeShortName;
            dr["machinesMarckKhmer"] = machinesMarckKhmer;
            dr["imagestr"] = this.picPathdata;
            dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dr["Creator"] = Dns.GetHostName().ToUpper();
            dr["modify"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dr["modifor"] = Dns.GetHostName().ToUpper();
            dr["ismachinesStatus"] = 0;
            this.machineDatatables.Rows.Add(dr);
            bool isRepeat = false;
            isRepeat = this.machineDatatables.DefaultView.ToTable(true, "MachineName").Rows.Count < this.machineDatatables.Rows.Count;

            if (isRepeat)
            {
                MessageBox.Show("已有此机器");
                this.machineDatatables.Rows.Remove(dr);
                this.dgvMachinesTable.DataSource = null;
                this.dgvMachinesTable.DataSource = this.machineDatatables;
                return;
            }
            this.butConfirm.Enabled = true;
            this.dgvMachinesTable.DataSource = null;
            this.dgvMachinesTable.DataSource = this.machineDatatables;
            dgvMachinesTable.Columns["imagestr"].CellTemplate = cell;
            dgvMachinesTable.RowTemplate.Height = 30;
            this.dgvMachinesTable.AllowUserToOrderColumns = true;
            changDgvMachinesTable_HeaderText();
            MessageBox.Show("添加成功");
        }

        public void changDgvMachinesTable_HeaderText()
        {
            this.dgvMachinesTable.Columns["machineClass"].HeaderText = "机器大类";
            // this.dgvMachinesTable.Columns["machineClass"].Visible = false;
            this.dgvMachinesTable.Columns["MachineName"].HeaderText = "机器类别名称";
            this.dgvMachinesTable.Columns["machineTypeShortName"].HeaderText = "机器类别简称";
            this.dgvMachinesTable.Columns["machinesMarckKhmer"].HeaderText = "机器类别名称柬文";
            this.dgvMachinesTable.Columns["imagestr"].HeaderText = "机器图片";
           // this.dgvMachinesTable.Columns["imagestr"].CellTemplate = cell;

            this.dgvMachinesTable.Columns["CreateDate"].HeaderText = "创建日期";
            this.dgvMachinesTable.Columns["Creator"].HeaderText = "创建人员";
            this.dgvMachinesTable.Columns["modify"].HeaderText = "最后修改日期";
            this.dgvMachinesTable.Columns["modifor"].HeaderText = "最后修改人员";
            this.dgvMachinesTable.Columns["ismachinesStatus"].HeaderText = "机器状态";


        }



        private void FrmMachineTypeTable_SizeChanged(object sender, EventArgs e)
        {
            this.gbMachinesTable.Width = this.Width -20;
            this.gbMemu.Width = this.dgvMachinesTable.Width;
            this.butConfirm.Left = this.gbMemu.Width - this.butConfirm.Width - 10;
            this.gbMachinesTable.Height = this.Height - this.gbMemu.Height - 50;
            this.dgvMachinesTable.ReadOnly = true;
            this.dgvMachinesTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMachinesTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

        }

        private void butDeleteMachineType_Click(object sender, EventArgs e)
        {

            if(this.delRowIndex == -1)
            {
                return;
            }
            if(this.delMachinesName != "")
            {
              int DelRows =   mtm.delMachineTypeByNames(this.delMachinesName);
            }

            this.machineDatatables.Rows.RemoveAt(delRowIndex);
            this.dgvMachinesTable.DataSource = null;
            this.dgvMachinesTable.DataSource = this.machineDatatables;
            MessageBox.Show("删除成功");
            this.butConfirm.Enabled = true;

        }

        private void dgvMachinesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;


            if (rowIndex >-1 )
            {
                string machinesName = this.machineDatatables.Rows[rowIndex]["MachineName"].ToString();
                this.delRowIndex = rowIndex;
                this.delMachinesName = machinesName;
            }
        }

        private void dgvMachinesTable_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }

        }

        private void butConfirm_Click(object sender, EventArgs e)
        {

            if (this.machineDatatables.Rows.Count > 0)
            {
                List<string> result = mtm.saveMachineTypes(this.machineDatatables);
                if (result[0] == "0")
                {
                    MessageBox.Show("保存成功, 共保存： " + result[1] + " 行数据.");
                    this.butConfirm.Enabled = false;

                }
                else
                {
                    MessageBox.Show("保存失败,失败原因：" + result[0]);

                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            string picPath ="" ;
            if (open.ShowDialog() == DialogResult.OK)
            {
                picPath = open.FileName;
            }


            if (picPath.Length > 0)
            {
                try
                {
                    label4.Text = picPath;
                    FileStream picPathfs = new FileStream(open.FileName, FileMode.Open);
                    //把文件读取到字节数组
                    this.picPathdata = new byte[picPathfs.Length];
                    picPathfs.Read(this.picPathdata, 0, this.picPathdata.Length);
                    picPathfs.Close();
                    //实例化一个内存流--->把从文件流中读取的内容[字节数组]放到内存流中去
                    MemoryStream ms = new MemoryStream(this.picPathdata);
                    //设置图片框 pictureBox1中的图片
                    this.pictureBox1.Image = Image.FromStream(ms);
                    this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvMachinesTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewImageCell cell = new DataGridViewImageCell();
            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;

            if (dgvMachinesTable.Columns[e.ColumnIndex].Name.Equals("imagestr"))
            {
              //  e.Value = GetImage(this.picPathdata);
                dgvMachinesTable.Columns["imagestr"].CellTemplate = cell;
                dgvMachinesTable.RowTemplate.Height = 30;

            }
        }
        public Image GetImage(byte[] str)
        {
            MemoryStream ms = new MemoryStream(str);
            Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }
    }
}
