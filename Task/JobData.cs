using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobControl.Task
{
    public class JobData
    {

        public string ArchiveFolder { get; }

        public bool ClearWorkingFolderAfter { get; }

        public bool ClearWorkingFolderBefore { get; }

        public int DaysBeforeTimeout { get; }

        public int DaysToKeepArchive { get; }

        public bool Disabled { get; }

        public string FailureEMailAddresses { get; }

        public DateTime LastSuccessRunDate { get; }

        public string SuccessEMailAddresses { get; }

        public string WorkingFolder { get; }
    }
}
