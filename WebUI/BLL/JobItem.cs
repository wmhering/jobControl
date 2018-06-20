using System;

namespace JobControl.Bll
{
    public class JobItem
    {
        public int Key { get; set; }

        public byte[] Concurrency { get; set; }

        public string Name { get; set; }

        public bool Disabled { get; set; }

        public DateTime? LatesttSuccessfulExecutionTime { get; set; }

        public DateTime? LatestExecutionTime { get; set; }

        public string LatestStatus { get; set; }

        public string LatestMessage  { get; set; }
    }
}