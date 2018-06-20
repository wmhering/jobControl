using System.Collections.Generic;

namespace JobControl.Bll
{
    public interface IJobRepository
    {
        JobEditor Create();

        IEnumerable<JobItem> Fetch();

        JobEditor Fetch(int key);

        ConcurrencyResult<JobEditor> Save(JobEditor data);

        ConcurrencyResult<JobEditor> Delete(int key, byte[] concurrency);
    }
}