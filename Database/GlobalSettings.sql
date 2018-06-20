CREATE TABLE dbo.GlobalSettings
(
  GlobalSettingsKey INT NOT NULL
    CONSTRAINT GlobalSettings_GlobalSettingsKey_DF DEFAULT (1),
  Concurrency TIMESTAMP NOT NULL,
  SmtpHost VARCHAR(100) NOT NULL,
  SmtpPort INT NOT NULL,
  SenderEMailAddress VARCHAR(2000) NOT NULL,
  FailureEMailAddresses VARCHAR(2000) NOT NULL,
  WorkingFolderPathRoot VARCHAR(250) NOT NULL,
  ArchiveFolderPathRoot VARCHAR(250) NOT NULL,
  CONSTRAINT GlobalSettings_PK
    PRIMARY KEY (GlobalSettingsKey),
  CONSTRAINT GobalSettings_GlobalSettingsKey_CK
    CHECK (GlobalSettingsKey = 1)
)
GO
GRANT INSERT,SELECT,UPDATE ON dbo.GlobalSettings TO ConfigureJobs;
GO
GRANT SELECT ON dbo.GlobalSettings TO ExecuteJobs;
GO