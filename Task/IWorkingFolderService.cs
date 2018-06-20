using System;
using System.IO;

namespace JobControl.Task
{
    public interface IWorkingFolderService
    {
        void CreateArchive(DateTime date);
        void DeleteContents();
        void RemoveOldArchives(DateTime date);
        void SaveData(string name, Stream stream);
    }
}
