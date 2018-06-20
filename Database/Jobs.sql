CREATE TABLE dbo.Jobs
(
  JobKey INT IDENTITY(1,1) NOT NULL, 
  Concurrency ROWVERSION NOT NULL,
  Name VARCHAR(100) NOT NULL, 
  Disabled BIT NOT NULL,
  SuccessEMailAddresses VARCHAR(2000) NULL,
  FailureEMailAddresses VARCHAR(2000) NULL, 
  DaysBeforeTimeout INT NOT NULL,
  WorkingFolderPath VARCHAR(250) NULL, 
  CleanUpWorkingFolder BIT NOT NULL, 
  ArchiveFolderPath VARCHAR(250) NULL, 
  DaysToKeepArchive INT NOT NULL,
  CONSTRAINT Jobs_PK
    PRIMARY KEY (JobKey),
  CONSTRAINT Jobs_Name_UX 
    UNIQUE (Name)
)
GO
GRANT DELETE,INSERT,SELECT,UPDATE ON dbo.Jobs TO ConfigureJobs;
GO
GRANT SELECT ON dbo.Jobs TO ExecuteJobs;
GO
GRANT SELECT ON dbo.Jobs TO ReportOnJobs;
GO