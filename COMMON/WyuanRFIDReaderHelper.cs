using ADUtilsLib.Initializer;
using ReaderB;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON
{
    public class WyuanRFIDReaderHelper
    {
        private byte fComAdr = 0xff; //当前操作的 ComAdr
        private byte fBaud;  //COM 波特率(每秒位远数)
        private int frmcomportindex; //选择 com口的索引号
        private int fOpenComIndex; //打开的串口COM索引号
        private bool ComOpen = false; // 串口COM端口是否打开
        private int fCmdRet = 30; //所有执行指令的返回值

        public int connRfidCOM(string PortName, int BaudRate, string ReaderAddr)
        {
            fBaud = Convert.ToByte(BaudRate);
            int port = 0;
            int openresult, i;
            openresult = 30;

            string temp;
            int connCom = -1;

            if (ReaderAddr == "")
            {
                ReaderAddr = "FF";
            }
            fComAdr = Convert.ToByte(ReaderAddr, 16); // $FF;
            port = Convert.ToInt32(PortName.Substring(3, PortName.Length - 3)); //选的是第几个PRO
            openresult = StaticClassReaderB.OpenComPort(port, ref fComAdr, fBaud, ref frmcomportindex);
            fOpenComIndex = frmcomportindex;
            if (openresult == 0x35)
            {
                //  MessageBox.Show("COM Opened", "Information");
                //  COM 口已被占用
                connCom = 1;
            }
            if (openresult == 0)
            {
                ComOpen = true;
                if ((fCmdRet == 0x35) || (fCmdRet == 0x30))
                {
                    ComOpen = false;
                    //  MessageBox.Show("Serial Communication Error or Occupied", "Information");
                    StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                    //  COM 口连接失败
                    connCom = -1;
                }
            }
            if ((fOpenComIndex != -1) & (openresult != 0X35) & (openresult != 0X30))
            {
                //  COM 口连接成功
                ComOpen = true;
                connCom = 0;
            }
            if ((fOpenComIndex == -1) && (openresult == 0x30))
            {
                //  COM 口连接失败
                connCom = -1;
            }
            return connCom;
        }

        public WyuanInfos GetReaderInfo()
        {
            byte[] TrType = new byte[2];
            byte[] VersionInfo = new byte[2];
            byte ReaderType = 0;
            byte ScanTime = 0;
            byte dmaxfre = 0;
            byte dminfre = 0;
            byte powerdBm = 0;
            byte FreBand = 0;
            WyuanInfos wyuanInfo = new WyuanInfos();
            fCmdRet = StaticClassReaderB.GetReaderInformation(ref fComAdr, VersionInfo, ref ReaderType, TrType, ref dmaxfre, ref dminfre, ref powerdBm, ref ScanTime, frmcomportindex);
            wyuanInfo._CmdRet = fCmdRet;
            if (fCmdRet == 0)
            {
                wyuanInfo._Version = Convert.ToString(VersionInfo[0], 10).PadLeft(2, '0') + "." + Convert.ToString(VersionInfo[1], 10).PadLeft(2, '0');
                wyuanInfo._Ver = Convert.ToInt32(VersionInfo[0]);

                wyuanInfo._PowerDbm = Convert.ToString(powerdBm, 10).PadLeft(2, '0');
                wyuanInfo._ComAdr = Convert.ToString(fComAdr, 16).PadLeft(2, '0');
                wyuanInfo._NewComAdr = Convert.ToString(fComAdr, 16).PadLeft(2, '0');
                wyuanInfo._scantime = Convert.ToString(ScanTime, 10).PadLeft(2, '0') + "*100ms";
                wyuanInfo._scantimeIndex = ScanTime - 3;
                FreBand = Convert.ToByte(((dmaxfre & 0xc0) >> 4) | (dminfre >> 6));
                wyuanInfo._FreBand = FreBand;
                wyuanInfo._ReaderType = ReaderType;
                wyuanInfo._TrType = TrType;
                switch (FreBand)
                {
                    case 0:
                        {

                            wyuanInfo._fdminfre = Convert.ToString(902.6 + (dminfre & 0x3F) * 0.4) + "MHz";
                            wyuanInfo._fdmaxfre = Convert.ToString(902.6 + (dmaxfre & 0x3F) * 0.4) + "MHz";

                        }
                        break;
                    case 1:
                        {

                            wyuanInfo._fdminfre = Convert.ToString(920.125 + (dminfre & 0x3F) * 0.25) + "MHz";
                            wyuanInfo._fdmaxfre = Convert.ToString(920.125 + (dmaxfre & 0x3F) * 0.25) + "MHz";

                        }
                        break;
                    case 2:
                        {
                            wyuanInfo._fdminfre = Convert.ToString(902.75 + (dminfre & 0x3F) * 0.5) + "MHz";
                            wyuanInfo._fdmaxfre = Convert.ToString(902.75 + (dmaxfre & 0x3F) * 0.5) + "MHz";


                        }
                        break;
                    case 3:
                        {
                            wyuanInfo._fdminfre = Convert.ToString(917.1 + (dminfre & 0x3F) * 0.2) + "MHz";
                            wyuanInfo._fdmaxfre = Convert.ToString(917.1 + (dmaxfre & 0x3F) * 0.2) + "MHz";


                        }
                        break;
                    case 4:
                        {
                            wyuanInfo._fdminfre = Convert.ToString(865.1 + (dminfre & 0x3F) * 0.2) + "MHz";
                            wyuanInfo._fdmaxfre = Convert.ToString(865.1 + (dmaxfre & 0x3F) * 0.2) + "MHz";

                        }
                        break;
                }
            }
            return wyuanInfo;
        }
        public WyuanWorkMode getRFIDWorkMode()
        {
            byte[] Parameter = new byte[12];
            WyuanWorkMode mode = new WyuanWorkMode();

            fCmdRet = StaticClassReaderB.GetWorkModeParameter(ref fComAdr, Parameter, frmcomportindex);
            mode._CmdRet = fCmdRet;

            if (fCmdRet == 0)
            {
                mode._Wiegand = Parameter[0];
                mode._DataOutputInvterval = Convert.ToInt32(Parameter[1]);
                mode._PulseWidth = Convert.ToInt32(Parameter[2] - 1);
                mode._PulseInterval = Convert.ToInt32(Parameter[3] - 1);
                mode._WorkMode = Convert.ToInt32(Parameter[4]);
                mode._MultiTagEAS = Convert.ToInt32(Parameter[5]);
                mode._Inquiry = Convert.ToInt32(Parameter[6]);

                mode._FirstWordAddr = Convert.ToString(Parameter[7], 16).PadLeft(2, '0');
                mode._ReadWordNumber = Convert.ToInt32(Parameter[8]);
                mode._SingleTagFilteringTime = Convert.ToInt32(Parameter[9]);
                mode._EASAccuracy = Convert.ToInt32(Parameter[10]);
                mode._OffsetTime = Convert.ToInt32(Parameter[11]);
            }
            return mode;
        }

        public string[] getConnComs()
        {
            IniSettings.LoadCommunication();
            string[] portNames = SerialPort.GetPortNames();
            return portNames;

        }

        public int CloseRfidReader(string portName)
        {
            int port;
            string temp = portName;
            if (portName.Length <= 0) return 0;
            try
            {
                port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                fCmdRet = StaticClassReaderB.CloseSpecComPort(port);
                if (fCmdRet == 0)
                {
                    /*

                    port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                    StaticClassReaderB.CloseSpecComPort(port);
                    fComAdr = 0xFF;
                    StaticClassReaderB.OpenComPort(port, ref fComAdr, fBaud, ref frmcomportindex);
                    fOpenComIndex = frmcomportindex;
                    GetReaderInfo();
                    */
                }
                else
                {
                    //MessageBox.Show("Serial Communication Error", "Information");
                    return -1;


                }
            }
            finally
            {

            }
            fOpenComIndex = -1;
            ComOpen = false;
            return 0;
        }

        public string GetData()
        {
            byte[] ScanModeData = new byte[40960];
            int ValidDatalength, i;
            string temp = "";
            string temps = "";
            ValidDatalength = 0;
            fCmdRet = StaticClassReaderB.ReadActiveModeData(ScanModeData, ref ValidDatalength, frmcomportindex);
            if (fCmdRet == 0)
            {
                temp = ByteArrayToHexString(ScanModeData, 4, ValidDatalength - 6);
                if (ValidDatalength > 0)
                {
                    return temp.ToUpper();
                }
            }

            return temp.ToUpper();
        }
        public static string ByteArrayToHexString(byte[] bytArray, int start, int length)
        {
            StringBuilder stringBuilder = new StringBuilder(bytArray.Length * 2);
            for (int i = start; i < length + start; i++)
            {
                try
                {
                    stringBuilder.Append(Convert.ToString(bytArray[i], 16).PadLeft(2, '0'));
                }
                catch
                {
                }
            }

            return stringBuilder.ToString().ToUpper();
        }

    }
}

public class WyuanInfos
{
    public int _CmdRet { get; set; }
    public string _Version { get; set; }
    public int _Ver { get; set; }
    public string _PowerDbm { get; set; }
    public string _ComAdr { get; set; }
    public string _NewComAdr { get; set; }
    public string _scantime { get; set; }
    public int _scantimeIndex { get; set; }
    public byte _FreBand { get; set; }
    public string _fdminfre { get; set; }
    public string _fdmaxfre { get; set; }
    public int _ReaderType { get; set; }
    public byte[] _TrType { get; set; }

}

public class WyuanWorkMode
{

    public int _CmdRet { get; set; }
    public byte _Wiegand { get; set; }
    public int _DataOutputInvterval { get; set; }
    public int _PulseInterval { get; set; }
    public int _PulseWidth { get; set; }
    public int _WorkMode { get; set; }
    public int _MultiTagEAS { get; set; }
    public int _Inquiry { get; set; }
    public string _FirstWordAddr { get; set; }
    public int _ReadWordNumber { get; set; }
    public int _SingleTagFilteringTime { get; set; }
    public int _EASAccuracy { get; set; }
    public int _OffsetTime { get; set; }

}
