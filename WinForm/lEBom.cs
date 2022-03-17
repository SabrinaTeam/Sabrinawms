
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm;

namespace WinForm
{
    public partial class lEBom : Form
    {

        private static lEBom frm;
       // public sizeRunManager sizem = new sizeRunManager();
        public DataGridView dgv = null;
        public IEBomManager iem = new IEBomManager();
        public lEBom()
        {
            InitializeComponent();
            dgvIETables.DoubleBufferedDataGirdView(true);

        }
        public static lEBom GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new lEBom();
            }
            return frm;
        }

        private void lEBom_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.splitContainer1.Enabled = false;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dgvIETables.RowHeadersWidth - 4,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvIETables.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgvIETables.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);


        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rows= this.dgvIETables.Rows.Count;
            for (int i=1; i<=rows; i++)
            {
                this.dgvIETables.Rows[i-1].Cells[1].Value = (i).ToString();
            }

        }

        private void butExpandReplicate_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel1Collapsed)
            {
                butExpandReplicate.Text = "收起";
                this.splitContainer1.Panel1Collapsed = false;
            }
            else
            {
                butExpandReplicate.Text = "展开";
                this.splitContainer1.Panel1Collapsed = true;
                this.splitContainer1.SplitterDistance=415;
            }
        }

        private void butLearningCurveTable_Click(object sender, EventArgs e)
        {
            StyleLearningCurve frm = StyleLearningCurve.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }

        private void butMachinesTable_Click(object sender, EventArgs e)
        {
            FrmMachineTypeTable frm = FrmMachineTypeTable.GetSingleton();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            frm.Activate();
        }

        private void lEBom_SizeChanged(object sender, EventArgs e)
        {
            dgvIETables.RowHeadersWidth = 30;    //设定左侧空列的宽度
                                                 // dataGridView1.RowHeadersVisible = false;   //隐藏空列，使不可见
            this.groupBox2.Left = Convert.ToInt32( (this.Width - this.groupBox2.Width - 10) / 2);

            //  this.gbSearch.Width = this.Width - 20;
            //  this.gbSize.Width = this.gbSearch.Width;
            //   this.gbPO.Width = this.gbSearch.Width;
            //   this.gbPO.Height = this.Height - 335;
            //   this.txtLogs.Visible = false;
        }

        private void butAddIETable_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Enabled = true;
          //  getIEVersion()
        }

        private void txtStyleNumber_TextChanged(object sender, EventArgs e)
        {
           // MessageBox.Show("1");

        }

        private void txtStyleNumber_Validated(object sender, EventArgs e)
        {
            MessageBox.Show("1");
            string styleNumber = this.txtStyleNumber.Text.Trim();
            if(styleNumber != "" )
            {
             List<string> styles = iem.getIEVersions(styleNumber);

            }
        }

    }
}