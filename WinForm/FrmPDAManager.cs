using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmPDAManager : Form
    {
        private static FrmPDAManager frm;
        DataTable devices = new DataTable();
        public int rows = -1;
        public PDAManager pm = new PDAManager();
        public FrmPDAManager()
        {

           
            InitializeComponent();        
            devices.Columns.Add("id", typeof(int));
            devices.Columns.Add("devUUID", typeof(string));
            devices.Columns.Add("devNumber", typeof(string));
            devices.Columns.Add("buyDate", typeof(string));
            devices.Columns.Add("devName", typeof(string));
            devices.Columns.Add("devMode", typeof(string));
            devices.Columns.Add("userDept", typeof(string));
            devices.Columns.Add("userDate", typeof(string));
            devices.Columns.Add("userName", typeof(string));
            devices.Columns.Add("mark", typeof(string));
             
        }
        public static FrmPDAManager GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmPDAManager();
            }
            return frm;
        }


        private void butAdd_Click(object sender, EventArgs e)
        {
            string devUUID = "";
            if (this.cbUUID.Checked)
            {
                if (this.txtUUID.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("請輸入 UUID");
                    return;
                }
                devUUID = this.txtUUID.Text.Trim().ToUpper();
            }
            else
            {
                devUUID = Guid.NewGuid().ToString();
            }
            

            if (this.txtNumber.Text.Trim().Length <= 0)
            {
                MessageBox.Show("請輸入財編");
                return;
            }

            
         
            string devNumber = this.txtNumber.Text.Trim().ToUpper();
            string buyDate = this.dtpBuyDate.Value.ToString("yyyy-MM-dd");
            string devName = this.txtName.Text.Trim();
            string devMode = this.txtMode.Text.Trim().ToUpper();
            string userDept = this.txtDept.Text.Trim();
            string userDate = this.dtpUserDate.Value.ToString("yyyy-MM-dd");
            string userName = this.txtUser.Text.Trim();
            string mark = this.txtMark.Text.Trim();
           

            DataRow row = this.devices.NewRow();
            row["ID"] = 0;
            row["devUUID"] = devUUID;
            row["devNumber"] = devNumber;
            row["buyDate"] = buyDate;
            row["devName"] = devName;
            row["devMode"] = devMode;
            row["userDept"] = userDept;
            row["userDate"] = userDate;
            row["userName"] = userName;
            row["mark"] = mark;          
            devices.Rows.Add(row);

            this.dgvDevices.DataSource = null;
            this.dgvDevices.DataSource = this.devices;
            this.dgvDevices.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
           
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            string DoubleUUID = "";
            string msg = "";


            if (this.dgvDevices.Rows.Count > 0)
            {
                DataTable saveDevices = new DataTable();
                saveDevices.Columns.Add("id", typeof(int));
                saveDevices.Columns.Add("devUUID", typeof(string));
                saveDevices.Columns.Add("devNumber", typeof(string));
                saveDevices.Columns.Add("buyDate", typeof(string));
                saveDevices.Columns.Add("devName", typeof(string));
                saveDevices.Columns.Add("devMode", typeof(string));
                saveDevices.Columns.Add("userDept", typeof(string));
                saveDevices.Columns.Add("userDate", typeof(string));
                saveDevices.Columns.Add("userName", typeof(string));
                saveDevices.Columns.Add("mark", typeof(string));



                for (int i =0; i<this.dgvDevices.Rows.Count;i++)
                {
                    string devUUIDstr = this.dgvDevices.Rows[i].Cells["devUUID"].Value.ToString().ToUpper();
                    int d = i;
                    if (!isExDoubleUUID(devUUIDstr, saveDevices))
                    {
                        DataRow row = saveDevices.NewRow();
                        row["ID"] = this.dgvDevices.Rows[i].Cells["ID"].Value.ToString();
                        row["devUUID"] = this.dgvDevices.Rows[i].Cells["devUUID"].Value.ToString();
                        row["devNumber"] = this.dgvDevices.Rows[i].Cells["devNumber"].Value.ToString();
                        row["buyDate"] = this.dgvDevices.Rows[i].Cells["buyDate"].Value.ToString();
                        row["devName"] = this.dgvDevices.Rows[i].Cells["devName"].Value.ToString();
                        row["devMode"] = this.dgvDevices.Rows[i].Cells["devMode"].Value.ToString();
                        row["userDept"] = this.dgvDevices.Rows[i].Cells["userDept"].Value.ToString();
                        row["userDate"] = this.dgvDevices.Rows[i].Cells["userDate"].Value.ToString();
                        row["userName"] = this.dgvDevices.Rows[i].Cells["userName"].Value.ToString();
                        row["mark"] = this.dgvDevices.Rows[i].Cells["mark"].Value.ToString();
                        saveDevices.Rows.Add(row);
                    }else
                    {
                        DoubleUUID =  (rows+1).ToString()+ "行的 UUID" + devUUIDstr + " 與" + (i+1).ToString() + "行的 UUID重復，請檢查 ";
                    }

                }

                if( DoubleUUID != "")
                {
                    msg = DoubleUUID;
                }
                else
                {
                    msg = this.pm.writePDAManagerToData(saveDevices);
                }
                  
                MessageBox.Show(msg);
            }
           
        }
      
        public bool isExDoubleUUID(string uuid,DataTable dt)
        {
           bool isEx = false;
     
            if (dt.Rows.Count <= 0)
            {
                isEx = false;
            }
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if(uuid == dt.Rows[i]["devUUID"].ToString().ToUpper())
                {
                    isEx = true;
                    this.rows = i;
                    break;
                }
                else
                {
                    isEx = false;
                }
            }

            return isEx;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            bool selectall = false;
            if (this.cbSelectedALL.Checked)
            {
                selectall = true;
            }
            string devUUID = this.txtUUID.Text.Trim().ToUpper();
            string devNumber = this.txtNumber.Text.Trim().ToUpper();
            string buyDate = this.dtpBuyDate.Value.ToString("yyyy-MM-dd");
            string devName = this.txtName.Text.Trim();
            string devMode = this.txtMode.Text.Trim().ToUpper();
            string userDept = this.txtDept.Text.Trim();
            string userDate = this.dtpUserDate.Value.ToString("yyyy-MM-dd");
            string userName = this.txtUser.Text.Trim();
            string mark = this.txtMark.Text.Trim();
            List<string> devs = new List<string> {
            devUUID,devNumber,devName,devMode,userDept,userName,mark
            };

          
                DataTable dt = this.pm.searchPDABuyUUID(devs,selectall);
           
           
            this.devices = dt;
            this.dgvDevices.DataSource = dt;
        }

        private void FrmPDAManager_Load(object sender, EventArgs e)
        {
            this.dgvDevices.ReadOnly = true;
        }

        private void dgvDevices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvDevices.ReadOnly = false;
        }

        private void FrmPDAManager_Resize(object sender, EventArgs e)
        {
            this.groupBox2.Width = this.Width - 20;
            this.groupBox2.Height = this.Height - 180;
            this.butCancel.Top = this.groupBox2.Height + 110;
            this.butSave.Top = this.butCancel.Top;
            this.butSave.Left = this.groupBox2.Width - 100;
        }

        private void dgvDevices_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.RowHeadersVisible)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, rect, e.InheritedRowStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
