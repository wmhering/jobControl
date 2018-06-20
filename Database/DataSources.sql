CREATE TABLE dbo.DataSources
(
  DataSourceKey INT IDENTITY(1,1) NOT NULL,
  JobKey INT NOT NULL, 
  DataSourceType VARCHAR(50) NOT NULL,
  Name VARCHAR(50) NOT NULL,
  ConnectionInfo VARCHAR(MAX) NOT NULL,
  CheckModifiedDate BIT NOT NULL,
  MoveToWorkingFolder BIT NOT NULL, 
  CONSTRAINT DataSources_PK
    PRIMARY KEY (DataSourceKey), 
  CONSTRAINT DataSources_Jobs_FK
    FOREIGN KEY (JobKey) REFERENCES dbo.Jobs (JobKey)
)
GO
GRANT DELETE,INSERT,SELECT,UPDATE ON dbo.DataSources TO ConfigureJobs;
GO
GRANT SELECT ON dbo.DataSources TO ExecuteJobs;
GO