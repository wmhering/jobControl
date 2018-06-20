using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobControl.Task
{
    public class ConfigurationData
    {
        public ConfigurationData(string smtpHost, int smtpPort, string senderEMaillAddress, string failureEMailAddresses,
            string workingFolderPathRoot, string archiveFolderPathRoot)
        {
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
            SenderEMailAddress = senderEMaillAddress;
            FailureEMailAddresses = failureEMailAddresses;
            WorkingFolderPathRoot = workingFolderPathRoot;
            ArchiveFolderPathRoot = archiveFolderPathRoot;
        }

        public string ArchiveFolderPathRoot { get; }

        public string FailureEMailAddresses { get; }

        public string SmtpHost { get; }

        public int SmtpPort { get; }

        public string SenderEMailAddress { get; }

        public string WorkingFolderPathRoot { get; }
    }
}
