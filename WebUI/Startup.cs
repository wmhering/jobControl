using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Routing;

using JobControl.Bll;
using JobControl.Dal;

namespace JobControl.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddMvc();
            services.AddTransient(typeof(IGlobalSettingsRepository), typeof(GlobalSettingsRepository));
            services.AddTransient(typeof(IJobRepository), typeof(JobRepository));
            services.AddScoped(typeof(JobControlContext));
            services.AddDbContext<JobControlContext>( options => {
                options.UseSqlServer("****");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseWhen(
                context =>
                    context.Request.Path.StartsWithSegments("/api"),
                builder =>
                    builder.UseApiExceptionHandler());

            app.UseMvc(routes => { });
        }
    }
}
