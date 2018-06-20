using System;
using System.Collections.Generic;
using System.Linq;

using JobControl.Bll;

namespace JobControl.Dal
{
    public class JobRepository : IJobRepository
    {
        JobControlContext _context;

        public JobRepository(JobControlContext context)
        {
            _context = context;
        }

        public JobEditor Create()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JobItem> Fetch()
        {
            return _context.JobItems.OrderByDescending(r => r.LatestExecutionTime);
        }

        public JobEditor Fetch(int key)
        {
            throw new NotImplementedException();
        }
        
        public ConcurrencyResult<JobEditor> Save(JobEditor data)
        {
            throw new NotImplementedException();
        }
        
        public ConcurrencyResult<JobEditor> Delete(int key, byte[] concurrency)
        {
            throw new NotImplementedException();
        }
    }
}
