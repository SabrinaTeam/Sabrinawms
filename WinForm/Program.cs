using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using zengzhouming.Update;

namespace WinForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process instance = RunningInstance();
            
            if (instance == null)
            {
                //1.1 没有实例在运行
                //

                AutoUpdater au = new AutoUpdater();
                try
                {
                    au.Update();
                }
                catch (WebException exp)
                {
                    MessageBox.Show(String.Format("无法找到指定资源\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (XmlException exp)
                {
                    MessageBox.Show(String.Format("下载的升级文件有错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (NotSupportedException exp)
                {
                    MessageBox.Show(String.Format("升级地址配置错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ArgumentException exp)
                {
                    MessageBox.Show(String.Format("下载的升级文件有错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format("升级过程中发生错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Application.Run(new FrmMain());
            }
            else
            {

                //1.2 已经有一个实例在运行
                HandleRunningInstance(instance);
            }
            
           // Application.Run(new FrmMain());
        }

        //2.在进程中查找是否已经有实例在运行

        private static System.Diagnostics.Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程
                if (process.Id != current.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //返回已经存在的进程
                        return process;
                    }
                }
            }
            return null;
        }

        public delegate bool EnumWindowsProc(int hWnd, int lParam);

        //3.已经有了就把它激活，并将其窗口放置最前端
        public delegate bool CallBack(IntPtr hwnd, int lParam);

        private static void HandleRunningInstance(Process instance)
        {
            // MessageBox.Show("已经在运行！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            IntPtr hwnd = gethund();
            if (hwnd != IntPtr.Zero)
            {
                ShowWindow(hwnd, 1);
                SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端
            }
            else
            {
                // Application.Run(new FrmLogin());
                MessageBox.Show("已经在运行！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //string hwnd = instance.MainWindowTitle;
            //IntPtr hwnd = FindWindow("WindowsForms10.Window.8.app.0.74db74_r12_ad1", "穎強鞋業%");

            //  ShowWindowAsync(instance.MainWindowHandle, 3);  //调用api函数，正常显示窗口
            // ShowWindowAsync(instance.MainWindowHandle, 3);  //调用api函数，正常显示窗口

            //this.Visible = true;//显示窗体
            //this.WindowState = FormWindowState.Maximized;
            //this.notifyIcon1.Visible = false;//隐藏系统托盘图标
        }

        /// <summary>
        /// 查找窗体上控件句柄
        /// </summary>
        /// <param name="hwnd">父窗体句柄</param>
        /// <param name="lpszWindow">控件标题(Text)</param>
        /// <param name="bChild">设定是否在子窗体中查找</param>
        /// <returns>控件句柄，没找到返回IntPtr.Zero</returns>
        private static IntPtr FindWindowEx(IntPtr hwnd, string lpszWindow, bool bChild)
        {
            IntPtr iResult = IntPtr.Zero;
            // 首先在父窗体上查找控件
            iResult = FindWindowEx(hwnd, 0, null, lpszWindow);
            // 如果找到直接返回控件句柄
            if (iResult != IntPtr.Zero) return iResult;

            // 如果设定了不在子窗体中查找
            if (!bChild) return iResult;

            // 枚举子窗体，查找控件句柄
            int i = EnumChildWindows(
            hwnd,
            (h, l) =>
            {
                IntPtr f1 = FindWindowEx(h, 0, null, lpszWindow);
                if (f1 == IntPtr.Zero)
                    return true;
                else
                {
                    iResult = f1;
                    return false;
                }
            },
            0);
            // 返回查找结果
            return iResult;
        }

        private static IntPtr gethund()
        {
            // 查找主界面句柄
            IntPtr mainHandle = FindWindow("WindowsForms10.Window.8.app.0.74db74_r12_ad1", null);
            if (mainHandle == IntPtr.Zero)
            {
                mainHandle = FindWindow("WindowsForms10.Window.8.app.0.74db74_r12_ad1", "欢迎登录~~");
            }

            //if (mainHandle != IntPtr.Zero)
            //{
            //    // 查找按钮句柄
            //    //IntPtr iBt = FindWindowEx(mainHandle, "menuStrip1", true);
            //    //if (iBt != IntPtr.Zero)
            //    // 发送单击消息
            //    //SendMessage(iBt, 0xF5, 0, 0);
            //   // return mainHandle;
            //}
            return mainHandle;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
        public static extern void SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
    }
}
