using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobControl.Bll
{
    public class GlobalSettingsEditor
    {
        public GlobalSettingsEditor()
        {
            Key = 0;
            SmtpHost = "";
            SmtpPort = 0;
            SenderEMailAddress = "";
            FailureEMailAddresses = new List<string>();
            WorkingFolderPathRoot = "";
            ArchiveFolderPathRoot = "";
        }

        public int Key { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public string SenderEMailAddress { get; set; }

        public List<string> FailureEMailAddresses { get; set; }

        public string WorkingFolderPathRoot { get; set; }

        public string ArchiveFolderPathRoot { get; set; }
    }
}
