using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace JobControl.Task.Services
{
    internal class DataAccessService : IDataAccessService
    {
        private string _ConnectionString;

        public DataAccessService(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public ConfigurationData GetConfigurationData()
        {
            #region cmdText =
            var cmdText = @"
SELECT SmtpHost, SmptPort, SenderEMailAddress, FailureEMailAddresses, WorkingFolderPathRoot, ArchiveFolderPathRoot
FROM Configuration;
";
            #endregion
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(cmdText, connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                    return new ConfigurationData(reader.GetString(0), reader.GetInt32(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5));
                return new ConfigurationData("", 0, "", "", "", "");
            }
        }

        public IEnumerable<DataSourceData> GetDataSourceData(string jobName)
        {
            #region cmdText =
            var cmdText = @"
SELECT D.Name, D.DataSourceType, D.ConnectionInfo, D.CheckModifiedDate, D.MoveToWorkingFolder
FROM DataSources AS D
  INNER JOIN Jobs AS J ON D.JobKey = J.JobKey
WHERE J.JobName = @JobName;
";
            #endregion
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(cmdText, connection))
            {
                command.Parameters.AddWithValue("JobName", jobName);
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        yield return new DataSourceData(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                            reader.GetBoolean(3), reader.GetBoolean(4));
            }
            yield break;
        }

        public JobData GetJobData(string jobName)
        {
            #region cmdText =
            var cmdText = @"
SELECT Name
FROM Jobs
WHERE JobName = @JobName;
";
            #endregion
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(cmdText, connection))
            {
                command.Parameters.AddWithValue("JobName", jobName);
                using (var reader = command.ExecuteReader())
                {
                    return new JobData();
                }
                   
            }
        }

        private SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(_ConnectionString);
            connection.Open();
            return connection;
        }

        public void SaveExecutionResult(string jobName, string status, string message)
        {
            #region cmdText =
            var cmdText = @"
INSERT INTO ExecutionResult (JobKey, ExecutionTime, Status, Message)
SELECT JobKey, @ExecutionTime, @Status, @Message
FROM Jobs
WHERE JobName = @JobName;
";
            #endregion
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(cmdText, connection))
            {
                command.Parameters.AddWithValue("ExecutionTime", DateTime.UtcNow);
                command.Parameters.AddWithValue("Status", status);
                command.Parameters.AddWithValue("Message", message);
                command.ExecuteNonQuery();
            }
        }

    }
}
