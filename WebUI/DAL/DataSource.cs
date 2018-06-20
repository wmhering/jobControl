using System.ComponentModel.DataAnnotations;

namespace JobControl.Dal
{
    public class DataSource
    {
        [Key]
        public int  DataSourceKey { get; set; }

        public int JobKey { get; set; }

        public Job Job { get; set; }

        public string DataSourceType { get; set; }

        public string Name { get; set; }

        public string ConnectionInfo { get; set; }

        public bool CheckModifiedDate { get; set; }

        public bool MoveToWorkingFolder { get; set; }
    }
} 