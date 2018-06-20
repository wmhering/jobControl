CREATE VIEW dbo.JobItems_VW AS
  WITH
    LastSuccess AS (
      SELECT JobKey, MAX(ExecutionTime) AS ExecutionTime
      FROM dbo.ExecutionResults
      WHERE Status = 'SUCCESS'
      GROUP BY JobKey),
    LastExecution AS (
      SELECT JobKey, ExecutionTime, Status, Message
      FROM dbo.ExecutionResults
      WHERE ExecutionResultsKey IN (SELECT MAX(ExecutionResultsKey) FROM dbo.ExecutionResults GROUP BY JobKey))
  SELECT JobKey, Concurrency, Name, Disabled, S.ExecutionTime AS LatesttSuccessfulExecutionTime,
    E.ExecutionTime AS LatestExecutionTime, E.Status AS LatestStatus, E.Message AS LatestMessage
  FROM dbo.Jobs AS J
    LEFT OUTER JOIN LastSuccess AS S ON J.JobKey = S.JobKey
    LEFT OUTER JOIN LastExecution AS E ON J.JobKey = E.JobKey
