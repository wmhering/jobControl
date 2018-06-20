using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobControl.Task
{
    public interface IDataAccessService
    {
        ConfigurationData GetConfigurationData();

        IEnumerable<DataSourceData> GetDataSourceData(string jobName);

        void SaveExecutionResult(string jobName, string status, string message);
    }
}
