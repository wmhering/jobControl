using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;

namespace JobControl.Task.DataSources
{
    public class FileSystemDataSource : DataSourceBase
    {
        public FileSystemDataSource(string description, bool checkModifiedDate, bool moveToWorkingFolder, string connectionInfo)
            : base(description, checkModifiedDate, moveToWorkingFolder, connectionInfo)
        {
            Domain = GetConnectionInfo("Domain");
            FilePath = GetConnectionInfo("File Path");
            Password = GetConnectionInfo("Password");
            UserID = GetConnectionInfo("User ID");
        }

        public override Stream GetDataStream()
        {
            var info = new FileInfo(FilePath);
            return info.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public override DateTime GetModifiedDate()
        {
            var info = new FileInfo(FilePath);
            return info.LastWriteTime;
        }

        public string Domain { get; }

        public string FilePath { get; }

        public string Password { get; }

        public string UserID { get; }
    }
}
