using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Trials.Kevin.Common.Profile;
using Trials.Kevin.Contract.IOC;
using Trials.Kevin.Model;

namespace Trials.Kevin.SaleOrder.New.Host
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
            services.AddControllers().AddControllersAsServices();
            //mysqlDbContext
            services.AddDbContext<SaleOrderContext>(options => options.UseMySQL(Configuration["DbConnectionStrings:MySqlMesOrder"]));

            services.AddUserService();

            //automapper
            services.AddAutoMapper(typeof(SaleOrderProfile));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;
            var service = Path.Combine(basePath, "Trials.Kevin.Service.dll");
            var repository = Path.Combine(basePath, "Trials.Kevin.Repository.dll");

            builder.RegisterAssemblyTypes(Assembly.LoadFrom(service))
                      .AsImplementedInterfaces()
                      .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.LoadFrom(repository))
                   .AsImplementedInterfaces()
                   .InstancePerDependency();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
