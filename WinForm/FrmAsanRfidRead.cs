using ADRcpLib;
using ADSioLib;
using ADUtilsLib.Initializer;
using ADUtilsLib.Utils;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmAsanRfidRead : Form
    {
        private static FrmAsanRfidRead frm;
        public FrmAsanRfidRead()
        {
            InitializeComponent();
        }

        private void FrmAsanRfidRead_Load(object sender, EventArgs e)
        {



        }
        public static FrmAsanRfidRead GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmAsanRfidRead();
            }
            return frm;
        }

        private void FrmAsanRfidRead_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemPub.DisConnectSio();
        }



    }
}
