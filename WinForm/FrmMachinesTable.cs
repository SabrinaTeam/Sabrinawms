using BLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            string machinesTypeNameEN =  this.txtMachineTypeNameEN.Text.Trim().ToUpper();
            string machinesTypeMarck = this.txtMachinesMarck.Text.Trim();

            if(machinesTypeName.Length <= 0)
            {
                return;
            }

            DataRow dr = this.machineDatatables.NewRow();

            dr["machineClass"] = "";
            dr["MachineName"] = machinesTypeName;

            dr["machineNameEN"] = machinesTypeNameEN;
            dr["machinesMark"] = machinesTypeMarck;
            dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            dr["Creator"] = Dns.GetHostName();
            dr["modify"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dr["modifor"] = Dns.GetHostName();
            dr["ismachinesStatus"] = 0;
            this.machineDatatables.Rows.Add(dr);

            bool isRepeat = false;

            isRepeat = this.machineDatatables.DefaultView.ToTable(true, "MachineName").Rows.Count < this.machineDatatables.Rows.Count;

          //  isRepeat = machineLists.GroupBy(i => i.machineName).Where(g => g.Count() > 1).Count() > 0;


            if (isRepeat)
            {
                MessageBox.Show("已有此机器");
                this.machineDatatables.Rows.Remove(dr);

               // this.machineLists.Remove(machine);
                  this.dgvMachinesTable.DataSource = null;
                  this.dgvMachinesTable.DataSource = this.machineDatatables;

                return;
            }

            this.butConfirm.Enabled = true;

            //  this.dgvMachinesTable.Rows.Add(new object[] { machinesTypeName, machinesTypeNameEN, machinesTypeMarck });
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
            // this.machineLists.Remove(machine);
            this.dgvMachinesTable.DataSource = null;
            this.dgvMachinesTable.DataSource = this.machineDatatables;
            MessageBox.Show("删除成功");
            this.butConfirm.Enabled = true;

        }

        private void dgvMachinesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            string machinesName = this.machineDatatables.Rows[rowIndex]["MachineName"].ToString();

            if (rowIndex >-1 )
            {
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
    }
}
