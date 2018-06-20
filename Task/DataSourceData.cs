using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobControl.Task
{
    public class DataSourceData
    {
        public DataSourceData(string name, string dataSourceType, string connectionInfo, bool checkModifiedDate, bool moveToWorkingFolder)
        {
            Name = name;
            DataSourceType = dataSourceType;
            ConnectionInfo = connectionInfo;
            CheckModifiedDate = checkModifiedDate;
            MoveToWorkingFolder = moveToWorkingFolder;
        }

        public bool CheckModifiedDate { get; }

        public string ConnectionInfo { get; }

        public string DataSourceType { get; }

        public bool MoveToWorkingFolder { get; }

        public string Name { get; }
    }
}
