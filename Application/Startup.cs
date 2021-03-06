using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Services;
using Repository.RepositoryPattern;
using Repository.DatabaseSeedLoader;
using Application.Mappings;
using Serilog;
using Application.Logger;

namespace Application
{
    public class Startup
    {
        readonly IWebHostEnvironment _currentEnvironment;


        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _currentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Application", Version = "v1" });
            });

            services.AddAutoMapper(typeof(ApiToDtoModelMappingProfile), typeof(DtoToEntityModelMappingProfile),
                typeof(DtoToApiModelMappingProfile));

            services.AddDbContext<Repository.ApplicationDbContext>(options =>
                options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;"));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<ICarService, CarService>();

            services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

            services.AddTransient<IReportService, ReportService>();

            if (_currentEnvironment.IsDevelopment())
            {
                services.AddTransient<IDatabaseSeedLoader, DatabaseSeedLoader>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Application v1"));
            }

            app.UseSerilogRequestLogging();

            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
