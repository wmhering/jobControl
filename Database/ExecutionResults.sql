CREATE TABLE dbo.ExecutionResults
(
  ExecutionResultsKey INT IDENTITY(1,1) NOT NULL,
  JobKey INT NOT NULL, 
  ExecutionTime DATETIME NOT NULL, 
  Status VARCHAR(10) NOT NULL, 
  Message VARCHAR(MAX) NULL, 
  CONSTRAINT ExecutionResults_PK
    PRIMARY KEY (ExecutionResultsKey),
  CONSTRAINT ExecutionResults_Jobs_FK
    FOREIGN KEY (JobKey) REFERENCES dbo.Jobs (JobKey)
)
GO
GRANT DELETE ON dbo.ExecutionResults TO ConfigureJobs;
GO
GRANT INSERT,SELECT ON dbo.ExecutionResults TO ExecuteJobs;
GO
GRANT SELECT ON dbo.ExecutionResults TO ReportOnJobs;
GO