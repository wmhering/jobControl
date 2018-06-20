using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobControl.Dal
{
    public class Job
    {
        [Key]
        public int JobKey{get;set;}

        public ICollection<DataSource> DataSources { get; set; }

        public ICollection<ExecutionResult> ExecutionResults { get; set; }

        public string Name { get; set; }

        public bool Disabled { get; set; }

        public string SuccessEMailAddresses { get; set; }

        public string FailureEMailAddresses { get; set; }

        public int DaysBeforeTimeout { get; set; }

        public string WorkingFolderPath { get; set; }

        public string CleanUpWorkingFolder { get; set; }

        public string ArchiveFolderPath { get; set; }

        public int DaysToKeepArchive { get; set; }
    }
} 
