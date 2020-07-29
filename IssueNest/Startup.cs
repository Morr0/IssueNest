using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IssueNest.Data;
using IssueNest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IssueNest
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
            services.AddControllers();
            services.AddDbContext<IssuesDBContext>();
            // Cors
            services.AddCors((options) =>
            {
                options.AddPolicy("policy", (policy) =>
                {
                    // TODO READ Origin from config 
                    policy.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });

            // AutoMapper: looks for a class inheriting Profile
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddSingleton<IssuesManager>();
            services.AddTransient<IHookService, HookService>();
            services.AddSingleton<IUserAuthService, UserAuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("policy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
