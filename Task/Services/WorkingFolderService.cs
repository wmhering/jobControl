using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobControl.Task.Services
{
    public class WorkingFolderService : IWorkingFolderService
    {
        private string _ArchiveFolder;
        private string _WorkingFolder;

        public WorkingFolderService(string jobName, ConfigurationData configurationData, JobData jobData)
        {
            _WorkingFolder = string.IsNullOrWhiteSpace(jobData.WorkingFolder)
                ? Path.Combine(configurationData.WorkingFolderPathRoot, RemoveBadCharacters(jobName)) : jobData.WorkingFolder;
            _ArchiveFolder = string.IsNullOrWhiteSpace(jobData.ArchiveFolder)
                ? Path.Combine(configurationData.ArchiveFolderPathRoot, RemoveBadCharacters(jobName)) : jobData.ArchiveFolder;
        }

        private string RemoveBadCharacters(string jobName)
        {
            throw new NotImplementedException();
        }

        public void CreateArchive(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void DeleteContents()
        {
            throw new NotImplementedException();
        }

        public void RemoveOldArchives(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void SaveData(string name, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
