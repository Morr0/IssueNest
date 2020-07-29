using System;
using AutoMapper;
using dotenv.net.DependencyInjection.Microsoft;
using IssueNest.Data;
using IssueNest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddHttpClient();
            // Cors
            services.AddCors((options) =>
            {
                options.AddPolicy("policy", (policy) =>
                {
                    // TODO READ Origin from config 
                    policy.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });

            // .env reader from dotenv.net && dotenv.net.DependencyInjection.Microsoft
            services.AddEnv();
            services.AddEnvReader();


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
