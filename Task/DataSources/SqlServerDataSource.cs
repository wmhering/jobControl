using System;
using System.Data;
using System.Data.SqlClient;

namespace JobControl.Task.DataSources
{
    public class SqlServerDataSource : DataSourceBase
    {
        public SqlServerDataSource(string description, bool checkModifiedDate, bool moveToWorkingFolder, string connectionInfo)
            : base(description, checkModifiedDate, moveToWorkingFolder, connectionInfo)
        {
            // Get parameters from connection info
            CommandText = GetConnectionInfo("Command Text");
            CommandType = GetConnectionInfo("Command Type");
            DataSource = GetConnectionInfo("Data Source");
            InitialCatelog = GetConnectionInfo("Initial Catelog");
            IntegratedSecurity = GetConnectionInfo("Integrated Security");
            Password = GetConnectionInfo("Password");
            UserID = GetConnectionInfo("UserID");
            // Format the connection string
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = DataSource;
            builder.InitialCatalog = InitialCatelog;
            builder.IntegratedSecurity = (IntegratedSecurity == "SSPI");
            if (string.IsNullOrWhiteSpace(Password))
                builder.Password = Password;
            if (string.IsNullOrWhiteSpace(UserID))
                builder.UserID = UserID;
            ConnectionString = builder.ToString();
        }

        public override DateTime GetModifiedDate()
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = CommandText;
                command.CommandType = (CommandType)Enum.Parse(typeof(CommandType), CommandType, ignoreCase: true);
                return (DateTime)command.ExecuteScalar();
            }
        }

        public override bool CheckModifiedDate
        {
            get { return true; }
        }

        public string CommandText { get; }

        public string CommandType { get; }

        public string ConnectionString { get; }

        public string DataSource { get; }

        public string InitialCatelog { get; }

        public string IntegratedSecurity { get; }

        public override bool MoveToWorkingDirectory
        {
            get { return false; }
        }

        public string Password { get; }

        public string UserID { get; }
    }
}
