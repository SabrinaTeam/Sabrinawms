
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace zengzhouming.Update
{
    public class AutoUpdater
    {
        private const string FILENAME = "update.config";
        private UpdateConfig updateconfig = null;
        private bool bNeedRestart = false;

        public AutoUpdater()
        {
            updateconfig = UpdateConfig.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME));
        }

       
        public void Update()
        {
            if (!updateconfig.Enabled)
                return;
            
            WebClient client = new WebClient();
            string strXml = client.DownloadString(updateconfig.ServerUrl);

            Dictionary<string, RemoteFile> listRemotFile = ParseRemoteXml(strXml);

            List<DownloadFileInfo> downloadList = new List<DownloadFileInfo>();
            
            List<LocalFile> preDeleteFile = new List<LocalFile>();

            foreach (LocalFile file in updateconfig.UpdateFileList)
            {
                if (listRemotFile.ContainsKey(file.Path))
                {
                    RemoteFile rf = listRemotFile[file.Path];
                    

                    if (rf.LastVer != file.LastVer)
                    {
                        downloadList.Add(new DownloadFileInfo(rf.Url, file.Path, rf.LastVer, rf.Size, rf.ReMark));
                        file.LastVer = rf.LastVer;
                        file.Size = rf.Size;

                        if (rf.NeedRestart)
                            bNeedRestart = true;
                    }

                    listRemotFile.Remove(file.Path);
                }
                else
                {
                    preDeleteFile.Add(file);
                }
            }

            foreach (RemoteFile file in listRemotFile.Values)
            {
                downloadList.Add(new DownloadFileInfo(file.Url, file.Path, file.LastVer, file.Size, file.ReMark));
                updateconfig.UpdateFileList.Add(new LocalFile(file.Path, file.LastVer, file.Size, file.ReMark));

                if (file.NeedRestart)
                    bNeedRestart = true;
            }

            if (downloadList.Count > 0)
            {
                DownloadConfirm dc = new DownloadConfirm(downloadList);

                if (this.OnShow != null)
                    this.OnShow();

                if (DialogResult.OK == dc.ShowDialog())
                {
                    foreach (LocalFile file in preDeleteFile)
                    {
                        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.Path);
                        if (File.Exists(filePath))
                            File.Delete(filePath);

                        updateconfig.UpdateFileList.Remove(file);
                    }

                    StartDownload(downloadList);
                }
            }
        }

        private void StartDownload(List<DownloadFileInfo> downloadList)
        {
            DownloadProgress dp = new DownloadProgress(downloadList);
            if (dp.ShowDialog() == DialogResult.OK)
            {
               
                updateconfig.SaveConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME));
                if (updateconfig.UpdateFileList.Count <= 0) return;

              
                string unRarPath = AppDomain.CurrentDomain.BaseDirectory + "TEMP";
               
                string rarPath = AppDomain.CurrentDomain.BaseDirectory;
               
                string rarName = updateconfig.UpdateFileList[0].Path;

                unRarPath = UnCompressRar(unRarPath, rarPath, rarName);
                if (unRarPath.Trim().Length <= 0)
                {
                    MessageBox.Show("rterte");
                    return;
                }

                if (bNeedRestart)
                {
                    File.Delete(unRarPath + rarName);
                    MessageBox.Show("Download Success", "Download Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string batstr = @"xcopy TEMP\* * /s /f /h /y"
                                            + "\r\n" + "rd /s /q TEMP"
                                            + "\r\n" + "del " + rarName
                                            + "\r\n" + "start " + Application.ExecutablePath
                                            + "\r\n" + "del %0 ";
                    StreamWriter sw = new StreamWriter(rarPath + "\\update.bat", false);
                    sw.WriteLine(batstr);
                    sw.Close();//迡

                    Process.Start(rarPath + "\\update.bat");
                                                           
                    Environment.Exit(0);
                }
            }
        }

      
        private static bool ExistsRar(out String winRarPath)
        {
            winRarPath = String.Empty;

           
            var registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");

            if (registryKey == null) return false;

            
            winRarPath = registryKey.GetValue("").ToString();

            registryKey.Close();

            return !String.IsNullOrEmpty(winRarPath);
        }

       
        public static void CompressRar(String path, String rarPath, String rarName)
        {
            try
            {
                String winRarPath = null;
                if (!ExistsRar(out winRarPath)) return;

                var pathInfo = String.Format("a -afzip -m0 -ep1 \"{0}\" \"{1}\"", rarName, path);

              

            
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = winRarPath,
                        Arguments = pathInfo,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = rarPath,
                        CreateNoWindow = false,
                    },
                };
                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public static String UnCompressRar(String unRarPath, String rarPath, String rarName)
        {
            try
            {
                String winRarPath = null;
                if (!ExistsRar(out winRarPath))
                {
                    return "";
                }

                if (Directory.Exists(unRarPath) == false)
                {
                    Directory.CreateDirectory(unRarPath);
                }

                var pathInfo = "x " + rarName + " \"" + unRarPath + "\" -y";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = winRarPath,
                        Arguments = pathInfo,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = rarPath,
                        CreateNoWindow = false,
                    },
                };
                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRarPath;
        }

        private Dictionary<string, RemoteFile> ParseRemoteXml(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            Dictionary<string, RemoteFile> list = new Dictionary<string, RemoteFile>();
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                string dd = node.Attributes["path"].Value;
                list.Add(node.Attributes["path"].Value, new RemoteFile(node));
            }

            return list;
        }

        public event ShowHandler OnShow;
    }

    public class RemoteFile
    {
        private string path = "";
        private string url = "";
        private string lastver = "";
        private int size = 0;
        private string remark = "";
        private bool needRestart = false;

        public string Path { get { return path; } }
        public string Url { get { return url; } }
        public string LastVer { get { return lastver; } }
        public int Size { get { return size; } }

        public string ReMark { get { return remark; } }
        public bool NeedRestart { get { return needRestart; } }

        public RemoteFile(XmlNode node)
        {
            this.path = node.Attributes["path"].Value;
            this.url = node.Attributes["url"].Value;
            this.lastver = node.Attributes["lastver"].Value;
            this.size = Convert.ToInt32(node.Attributes["size"].Value);
            this.remark = node.Attributes["remark"].Value;
            this.needRestart = Convert.ToBoolean(node.Attributes["needRestart"].Value);
        }
    }

    public class LocalFile
    {
        private string path = "";
        private string lastver = "";
        private int size = 0;
        private string remark = "";

        [XmlAttribute("path")]
        public string Path { get { return path; } set { path = value; } }

        [XmlAttribute("lastver")]
        public string LastVer { get { return lastver; } set { lastver = value; } }

        [XmlAttribute("size")]
        public int Size { get { return size; } set { size = value; } }

        [XmlAttribute("remark")]
        public string ReMark { get { return remark; } set { remark = value; } }

        public LocalFile(string path, string ver, int size, string remark)
        {
            this.path = path;
            this.lastver = ver;
            this.size = size;
            this.remark = remark;
        }

        public LocalFile()
        {
        }
    }

    public delegate void ShowHandler();

    public class DownloadFileInfo
    {
        private string downloadUrl = "";
        private string fileName = "";
        private string lastver = "";
        private string remark = "";
        private int size = 0;

     
        public string DownloadUrl { get { return downloadUrl; } }

      
        public string FileFullName { get { return fileName; } }

        public string FileName { get { return Path.GetFileName(FileFullName); } }
        public string LastVer { get { return lastver; } set { lastver = value; } }
        public string ReMark { get { return remark; } set { remark = value; } }
        public int Size { get { return size; } }

        public DownloadFileInfo(string url, string name, string ver, int size, string remark)
        {
            this.downloadUrl = url;
            this.fileName = name;
            this.lastver = ver;
            this.size = size;
            this.remark = remark;
        }
    }
}