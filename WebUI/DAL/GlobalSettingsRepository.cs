using System;
using System.Collections.Generic;
using System.Linq;

using JobControl.Bll;

namespace JobControl.Dal
{
    public class GlobalSettingsRepository : IGlobalSettingsRepository
    {
        private JobControlContext _Context;

        public GlobalSettingsRepository(JobControlContext context)
        {
            _Context = context;
        }

        public GlobalSettingsEditor Fetch()
        {
            var data = _Context.GlobalSettings.Select(MapToEditor).FirstOrDefault();
            return data ?? new GlobalSettingsEditor();
        }

        public ConcurrencyResult<GlobalSettingsEditor> Save(GlobalSettingsEditor data)
        {
            
            throw new NotImplementedException();
        }

        private GlobalSettingsEditor MapToEditor(GlobalSettings data, int index)
        {
            return new GlobalSettingsEditor
            {
                Key = data.GlobalSettingsKey,
                SmtpHost = data.SmtpHost,
                SmtpPort = data.SmtpPort ?? 0,
                SenderEMailAddress = data.SenderEMailAddress,
                FailureEMailAddresses = new List<string>((data.FailureEMailAddresses ?? "").Split(';', StringSplitOptions.RemoveEmptyEntries)),
                WorkingFolderPathRoot = data.WorkingFolderPathRoot,
                ArchiveFolderPathRoot = data.ArchiveFolderPathRoot
            };
        }
    }
}
