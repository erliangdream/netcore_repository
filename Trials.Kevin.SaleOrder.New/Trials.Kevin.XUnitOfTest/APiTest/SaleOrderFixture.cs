using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Trials.Kevin.Model;
using Trials.Kevin.SaleOrder.New;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.TestHost;
using Autofac.Extensions.DependencyInjection;
using System.Threading;
using Trials.Kevin.SaleOrder.New.Host;

namespace Trials.Kevin.XUnitOfTest
{
    public class SaleOrderFixture : IDisposable
    {
        public TestServer testServer { get; }
        public SaleOrderFixture()
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder()
                  .ConfigureServices(services =>
                  {
                      services.AddDbContext<SaleOrderContext>(options =>
                      {
                          options.UseInMemoryDatabase("SaleOrderUnitOfTestDB");
                      });

                      services.AddAutofac();

                      services.AddScoped<CancellationTokenSource>();
                  })
            .UseStartup<Startup>();
            testServer = new TestServer(webHostBuilder);
        }

        public void Dispose()
        {
            testServer.Dispose();
        }
    }
}
