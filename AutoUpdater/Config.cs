using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace zengzhouming.Update
{
    public class UpdateConfig
    {
        private string configuration { get; set; }

        private bool enabled = true;
        public bool Enabled { get { return enabled; } set { enabled = value; } }

        private string serverUrl = "";
        public string ServerUrl { get { return serverUrl; } set { serverUrl = value; } }

        private UpdateFileList updateFileList = new UpdateFileList();

        public UpdateFileList UpdateFileList
        {
            get { return updateFileList; }
            set { updateFileList = value; }
        }

        public static UpdateConfig LoadConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(UpdateConfig));
            StreamReader sr = new StreamReader(file);
            UpdateConfig config = xs.Deserialize(sr) as UpdateConfig;
            sr.Close();

            return config;
        }

        public void SaveConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(UpdateConfig));
            StreamWriter sw = new StreamWriter(file);
            xs.Serialize(sw, this);
            sw.Close();
        }
    }

    public class UpdateFileList : List<LocalFile>
    {
    }
}