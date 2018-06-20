using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Internal;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Query.Internal;
//using Microsoft.EntityFrameworkCore.Utilities;

using JobControl.Bll;

namespace JobControl.Dal
{
    public class JobControlContext : DbContext
    {

        public JobControlContext(DbContextOptions<JobControlContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder model)
        {        
            model.Entity<JobItem>().ToTable("JobItems_VW").HasKey(o => o.Key);
        }

        public DbSet<DataSource> DataSources { get; set; }

        public DbSet<ExecutionResult> ExecutionResults { get; set;}

        public DbSet<GlobalSettings> GlobalSettings { get; set; }

        public DbSet<JobItem> JobItems { get; set; }

        public DbSet<Job> Jobs { get; set; }
    }
}
