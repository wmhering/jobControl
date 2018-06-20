using System;
using System.ComponentModel.DataAnnotations;

namespace JobControl.Dal
{
    public class ExecutionResult
    {
        [Key]
        public int ExecutionResultsKey { get; set; }

        public int JobKey { get; set; }

        public Job Job { get; set; }

        public DateTime ExecutionTime { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }
    }
} 