using System;
using System.IO;

namespace JobControl.Task
{
    public interface IDataSource
    {
        Stream GetDataStream();
        DateTime GetModifiedDate();

        bool CheckModifiedDate { get; }
        string Name { get; }
        bool MoveToWorkingDirectory { get; }
    }
}
