using ADSioLib;
using ADUtilsLib.Initializer;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON
{
    public class AsadDuooRFIDReaderHelper
    {

        private const int READ_STOP = 0;
        private const int READ_START = 1;
        private const int READ_SETANT = 2;
        private const int READ_SETANT_WAIT = 3;
        private const int READ_SETANTOK = 4;
        private const int READ_IDENTIFY = 5;
        private const int READ_IDENTIFY_WAIT = 6;
        private const int READ_IDENTIFYOK = 7;
        private const int READ_EXIT = 8;
        bool mIsStop = true;
        bool mIsStart = false;
        int mIsStatus = 0;
        int mLoopDelay = 0;
        int mLoopInterval = 0;
        DateTime nReadShowTime = DateTime.Now;
        int nReadShowRate = 50;
        int nAllTags = 0;
        int nInventoryTags = 0;
        int nInventoryCount = 0;
        int isStopAutoRead = 1; //是否主动工作方式
        int mCommandDelay = 2; //是否主动工作方式
        int mCurrentAnt = 0xff; //当前天线
        int nMaxAnt = 0;

        DateTime dtStart = DateTime.Now;

        public string[] getConnComs()
        {
            IniSettings.LoadCommunication();
            string[] portNames = SerialPort.GetPortNames();
            return portNames;

        }
        public int connRfidCOM(string PortName, int BaudRate)
        {
            int connCom = -1;
            if (AsadDuooSystemPub.IsConnectedSio)  // 如果是连接的
            {
                 AsadDuooSystemPub.SioBase.onReceived -= SioBase_onReceived;
                AsadDuooSystemPub.DisConnectSio();
                AsadDuooSystemPub.RcpBase.Address = 65535;
                // 断开连接
                connCom = - 1;
                return connCom;
            }
             InitSio(IniSettings.Communication, PortName, BaudRate);
             AsadDuooSystemPub.SioBase.Connect(IniSettings.HostName, IniSettings.HostPort);
            if (AsadDuooSystemPub.IsConnectedSio)
            {
                AsadDuooSystemPub.SioBase.onReceived += SioBase_onReceived;
                connCom = 0;
            }

            else
            {
                connCom = -1;
            }

            return connCom;
          //  button1.Text = AsadDuooSystemPub.IsConnectedSio ? "OPEN" : "CLOSE";
        }
        private void SioBase_onReceived(object sender, ADSioLib.ReceivedEventArgs e)
        {
            AsadDuooSystemPub.RcpBase.ReciveBytePkt(e.Data);
        }
        private void InitSio(int commType,string PortName, int BaudRate)
        {
            IniSettings.PortName = PortName;
            IniSettings.BaudRate = BaudRate;
            if (AsadDuooSystemPub.IsConnectedSio)
            {
                AsadDuooSystemPub.DisConnectSio();
            }
            AsadDuooSystemPub.SioBase = new SioCom();
        }



    }
}
