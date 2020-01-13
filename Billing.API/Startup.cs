using Billing.API.SDK;
using Billing.Services;
using Billing.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RequestDataCollector.SDK;

namespace Billing.API
{
    public class Startup
    {
        BillingApiSettings Settings { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // ApiKey won't be populated for RequestDataCollector other than on dev's machine as it's stored in secret manager
            Settings = configuration.Get<BillingApiSettings>();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(Settings.RequestDataCollector);
            services.AddSingleton<IRequestDataCollectorApi, RequestDataCollectorApi>();

            services.AddSingleton<ICostCalculatorService, CostCalculatorService>();
        }

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
