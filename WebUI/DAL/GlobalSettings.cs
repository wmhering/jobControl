using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobControl.Dal
{
    public class GlobalSettings
    {
        [Key]
        public int GlobalSettingsKey { get; set; }

        [ConcurrencyCheck]
        public byte[] Concurrency { get; set; }

        public string SmtpHost { get; set; }

        public int? SmtpPort { get; set; }

        public string SenderEMailAddress { get; set; }

        public string FailureEMailAddresses { get; set; }

        public string WorkingFolderPathRoot { get; set; }

        public string ArchiveFolderPathRoot { get; set; }
    }
}
