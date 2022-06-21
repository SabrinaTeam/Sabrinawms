using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL
{
    public class AlarmManager
    {
        //报警器
        private SerialPort sp1 = new SerialPort();
        public byte[] com = new byte[8];
        private static readonly string AlarmCOM = ConfigurationManager.ConnectionStrings["AlarmCOM"].ConnectionString; //Com口配置


        // 1 红灯 错误
        // 2 黄灯  等待下一动作
        // 3 绿灯 正确
        // 4 声音
        public void closeportall()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x13;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x04;
            com[7] = 0x4B;

            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception ex)
            {


                throw;
            }

        }
        public void openportall()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x14;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x04;
            com[7] = 0x4C;

            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception ex)
            {


                throw;
            }

        }
        public bool OpenAlarm()
        {
            bool AMresult = false;
                //sp1.Open();
            if (sp1.IsOpen)
            {
                AMresult = true;
                return AMresult;

            }
            else
            {
                sp1.PortName = AlarmCOM;
                sp1.BaudRate = 9600;
                sp1.DataBits = 8;
                sp1.StopBits = StopBits.One;
                sp1.Parity = Parity.None;
                try
                {
                    sp1.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }

            if (sp1.IsOpen)
            {
                AMresult = true;

            }
            else
            {
                AMresult = false;
            }

                return AMresult;

        }

        public void CloseAlarm()
        {
            // closeport1();
            sp1.Close();
        }

        public void openport1()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x12;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x01;
            com[7] = 0x47;

            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception ex)
            {


                throw;
            }

        }

        public void closeport1()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x11;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x01;
            com[7] = 0x46;
            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception)
            {

              //  throw;
            }

        }

        public void openport2()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x12;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x02;
            com[7] = 0x48;

            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void closeport2()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x11;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x02;
            com[7] = 0x47;

            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception)
            {

              //  throw;
            }
        }

        public void openport3()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x12;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x03;
            com[7] = 0x49;
            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception)
            {

              //  throw;
            }
        }

        public void closeport3()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x11;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x03;
            com[7] = 0x48;
            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception)
            {

              //  throw;
            }
        }

        public void openport4()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x12;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x04;
            com[7] = 0x4A;
            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception)
            {

             //   throw;
            }
        }

        public void closeport4()
        {
            com[0] = 0x33;
            com[1] = 0x01;
            com[2] = 0x11;
            com[3] = 0x00;
            com[4] = 0x00;
            com[5] = 0x00;
            com[6] = 0x04;
            com[7] = 0x49;
            try
            {
                sp1.Write(com, 0, 8);
            }
            catch (Exception)
            {

             //   throw;
            }
        }

        public void palyMedia(Boolean info)
        {
            string path = "";
            if (info)
            {
                //播放正确的声音
                path = "./wav/ok.wav";
            }
            else
            {
                //播放错误的声音
                path = "./wav/error.wav";
            }
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
            player.PlaySync();
        }
    }
}
