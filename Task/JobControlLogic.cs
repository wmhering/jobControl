using System;
using System.Collections.Generic;

using JobControl.Task.DataSources;
using JobControl.Task.Services;

namespace JobControl.Task
{
    public class JobControlLogic
    {
        private bool _ClearWorkingFolderAfter;
        private bool _ClearWorkingFolderBefore;
        private IDataAccessService _DataAccess;
        private IList<IDataSource> _DataSources;
        private int _DaysBeforeTimeout;
        private int _DaysToKeepArchive;
        private bool _Disabled;
        private IEMailService _EMailService;
        private string _FailureEMailAddresses;
        private string _JobName;
        private DateTime _LastSuccessRunDate;
        private string _SuccessEMailAddresses;
        private IWorkingFolderService _WorkingFolder;

        public static JobControlLogic Create(string jobName, string connectionString)
        {
            var dataAccessService = new DataAccessService(connectionString);
            var configurationData = dataAccessService.GetConfigurationData();
            var emailService = new EMailService(configurationData);
            var jobData = dataAccessService.GetJobData(jobName);
            var workingFolderService = new WorkingFolderService(jobName, configurationData, jobData);
            var dataSources = new List<IDataSource>();
            foreach (var dataSourceData in dataAccessService.GetDataSourceData(jobName))
                dataSources.Add(DataSourceBase.Create(dataSourceData));
            return new JobControlLogic(jobName, dataSources, dataAccessService, workingFolderService, emailService, jobData);
        }

        #region Constructors
        public JobControlLogic(string jobName, IList<IDataSource> dataSources,
            IDataAccessService dataAccess, IWorkingFolderService workingFolder, IEMailService eMail, JobData jobData) :
            this(jobName, dataSources, dataAccess, workingFolder, eMail, jobData.Disabled, jobData.LastSuccessRunDate,
                jobData.ClearWorkingFolderBefore, jobData.ClearWorkingFolderAfter, jobData.DaysToKeepArchive,
                jobData.SuccessEMailAddresses, jobData.FailureEMailAddresses, jobData.DaysBeforeTimeout)
        { }

        public JobControlLogic(string jobName, IList<IDataSource> dataSources,
            IDataAccessService dataAccess, IWorkingFolderService workingFolder, IEMailService eMail, 
            bool disabled, DateTime lastSuccessRunDate,
            bool clearWorkingFolderBefore, bool clearWorkingFolderAfter, int daysToKeepArchive,
            string successEMailAddresses, string failureEMailAddresses, int daysBeforeTimeout)
        {
            _JobName = jobName;
            _DataSources = dataSources;
            _DataAccess = dataAccess;
            _WorkingFolder = workingFolder;
            _EMailService = eMail;
            _Disabled = disabled;
            _LastSuccessRunDate = lastSuccessRunDate;
            _ClearWorkingFolderBefore = clearWorkingFolderBefore;
            _ClearWorkingFolderAfter = clearWorkingFolderAfter;
            _DaysToKeepArchive = daysToKeepArchive;
            _SuccessEMailAddresses = successEMailAddresses;
            _FailureEMailAddresses = failureEMailAddresses;
            _DaysBeforeTimeout = daysBeforeTimeout;
        }
        #endregion

        #region Methods
        private void CopyDataToWorkingFolder()
        {
            if (_ClearWorkingFolderBefore)
                _WorkingFolder.DeleteContents();
            foreach (var dataSource in _DataSources)
                if (dataSource.MoveToWorkingDirectory)
                    _WorkingFolder.SaveData(dataSource.Name, dataSource.GetDataStream());
        }

        private bool DataSourcesAreNotReady()
        {
            var staleOrMissing = new List<string>();
            foreach (var dataSource in _DataSources)
            {
                if (dataSource.CheckModifiedDate && dataSource.GetModifiedDate() <= _LastSuccessRunDate)
                    staleOrMissing.Add(dataSource.Name);
            }
            if (staleOrMissing.Count == 0)
                return false;
            ReportStaleOrMissingDataSources(staleOrMissing);
            return true;
        }

        public bool JobShouldRun()
        {
            if (JobIsDisabled())
                return false;
            if (DataSourcesAreNotReady())
                return false;
            CopyDataToWorkingFolder();
            return true;
        }

        private bool JobIsDisabled()
        {
            if (!_Disabled)
                return false;
            Status = "DISABLED";
            Message = $"Job '{_JobName}' is disabled.";
            _DataAccess.SaveExecutionResult(_JobName, Status, Message);
            return true;
        }

        public void ReportFailure(string message)
        {
            Status = "FAILURE";
            Message = message;
            _DataAccess.SaveExecutionResult(_JobName, Status, Message);
            _EMailService.SendMessage(_FailureEMailAddresses, $"Job '{_JobName}' failed", Message);
        }

        private void ReportStaleOrMissingDataSources(List<string> dataSourceNames)
        {
            var nl = Environment.NewLine + "    ";
            Message = $"The following data sources are missing or have not been updated since {_LastSuccessRunDate:yyyy-MM-dd}:"
                + $"{nl}{string.Join(nl, dataSourceNames.ToArray())}";
            if (_LastSuccessRunDate.AddDays(_DaysBeforeTimeout) >= DateTime.UtcNow)
                Status = "NOT-READY";
            else
            {
                Status = "TIMEOUT";
                _EMailService.SendMessage(_FailureEMailAddresses, $"Job '{_JobName}' failed", Message);
            }
            _DataAccess.SaveExecutionResult(_JobName, Status, Message);
        }

        public void ReportSuccess(string message)
        {
            _WorkingFolder.RemoveOldArchives(DateTime.Now.AddDays(0 - _DaysToKeepArchive));
            _WorkingFolder.CreateArchive(DateTime.Now);
            if (_ClearWorkingFolderAfter)
                _WorkingFolder.DeleteContents();
            Status = "SUCCESS";
            Message = message;
            _DataAccess.SaveExecutionResult(_JobName, Status, Message);
            _EMailService.SendMessage(_SuccessEMailAddresses, $"Job '{_JobName}' succeeded", Message);
        }
        #endregion

        #region Properties
        public string Message { get; private set; }
        public string Status { get; private set; }
        public string WorkingFolder { get; private set; }
        #endregion
    }
}
