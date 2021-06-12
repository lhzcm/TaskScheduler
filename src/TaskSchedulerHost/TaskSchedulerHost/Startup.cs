using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskSchedulerRespository.DbContexts;
using System.Reflection;
using System.IO;
using TaskSchedulerRespository.Respositorys;
using TaskSchedulerHost.Task;

namespace TaskSchedulerHost
{
    public class Startup
    {
        private Config _config;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _config = configuration.GetSection("AppConfig").Get<Config>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<Config>(_config);
            services.AddDbContext<TaskSchedulerDbContext>((op) => op.UseSqlServer(Configuration.GetConnectionString("TaskScheduler")));
            services.AddScoped<TaskRepository>();
            services.AddScoped<LogRepository>();
            services.AddControllers();
            services.AddScoped<TaskManager>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => {

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
            services.AddCors(Options =>
            {
                //Options.AddPolicy("cors", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());\
                if (_config.CORS != null)
                {
                    Options.AddPolicy("custom", p => p.WithOrigins(_config.CORS).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                }
            });
            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (_config.CORS != null)
            {
                app.UseCors("custom");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskScheduler");
            }
            );

            app.UseRouting();

            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
