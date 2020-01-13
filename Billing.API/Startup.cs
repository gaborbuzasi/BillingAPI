using Billing.API.SDK;
using RequestDataCollector.SDK;
using Billing.Services;
using Billing.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Billing.API
{
    public class Startup
    {
        BillingApiSettings Settings { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Settings = configuration.Get<BillingApiSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(Settings.RequestDataCollector);
            services.AddSingleton<IRequestDataCollectorApi, RequestDataCollectorApi>();

            services.AddSingleton<ICostCalculatorService, CostCalculatorService>();
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
