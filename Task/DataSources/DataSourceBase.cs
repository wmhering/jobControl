using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JobControl.Task.DataSources
{
    public abstract class DataSourceBase : IDataSource
    {
        private const string HEX_DIGITS = "0123456789ABCDEF";
        private Dictionary<string, string> _ConnectionInfo;
 
        protected DataSourceBase(string name, bool checkModifiedDate, bool moveToWorkingDirectory, string connectionInfo)
        {
            Name = name;
            CheckModifiedDate = checkModifiedDate;
            MoveToWorkingDirectory = moveToWorkingDirectory;
            _ConnectionInfo = LoadConnectionInfo(connectionInfo);
        }

        public static IDataSource Create(DataSourceData data)
        {
            return Create(data.DataSourceType, data.Name, data.CheckModifiedDate, data.MoveToWorkingFolder, data.ConnectionInfo);
        }

        public static IDataSource Create(string dataSourceType, string name, bool checkModifiedDate, bool moveToWorkingFolder, string connectionInfo)
        {
            var type = Type.GetType(dataSourceType);
            var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null,
                new Type[] { typeof(string), typeof(bool), typeof(bool), typeof(string) }, null);
            var instance = constructor.Invoke(new object[] { name, checkModifiedDate, moveToWorkingFolder, connectionInfo });
            return (IDataSource)instance;
        }

        public virtual bool CheckModifiedDate { get; }

        public string Name { get; }

        public virtual bool MoveToWorkingDirectory { get; }

        private string Decode(string value)
        {
            var result = new StringBuilder(value.Length);
            for (var i = 0; i < value.Length - 2;)
            {
                if (value[i] == '#' && IsHexDigit(value[i + 1]) && IsHexDigit(value[i + 2]))
                {
                    result.Append(HexToChar(value[i + 1], value[i + 2]));
                    i += 3;
                }
                else
                    result.Append(value[i++]);
            }
            return result.ToString();
        }

        protected string GetConnectionInfo(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            name = name.ToUpper();
            if (_ConnectionInfo.ContainsKey(name))
                return _ConnectionInfo[name];
            return "";
        }

        public virtual Stream GetDataStream()
        {
            if (MoveToWorkingDirectory)
                throw new NotImplementedException($"{GetType().FullName}.{nameof(GetDataStream)} has not been implemented.");
            throw new InvalidOperationException($"Cannot call {GetType().FullName}.{nameof(GetDataStream)} when {nameof(MoveToWorkingDirectory)} is false.");
        }

        public virtual DateTime GetModifiedDate()
        {
            if (CheckModifiedDate)
                throw new NotImplementedException($"{GetType().FullName}.{nameof(GetModifiedDate)} has not been implemented.");
            throw new InvalidOperationException($"Cannot call {GetType().FullName}.{nameof(GetModifiedDate)} when {nameof(CheckModifiedDate)} is false.");
        }

        private char HexToChar(char highDigit, char lowDigit)
        {
            return Convert.ToChar(
                HEX_DIGITS.IndexOf(char.ToUpper(highDigit)) * 16
                + HEX_DIGITS.IndexOf(char.ToUpper(lowDigit)));
        }

        private bool IsHexDigit(char value)
        {
            return HEX_DIGITS.IndexOf(value) >= 0;
        }

        private Dictionary<string,string> LoadConnectionInfo(string data)
        {
            var dictionary = new Dictionary<string, string>();
            var nameValuePairs = data.Split(';');
            foreach (var pair in nameValuePairs)
            {
                var nameValue = pair.Split('=');
                if (nameValue.Length != 2)
                    continue;
                var name = Decode(nameValue[0]);
                name = name.ToUpper();
                if (dictionary.ContainsKey(name))
                    continue;
                dictionary.Add(name, Decode(nameValue[1]));
            }
            return dictionary;
        }
    }
}
