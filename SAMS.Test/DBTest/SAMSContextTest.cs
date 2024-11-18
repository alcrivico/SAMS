using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.Models.DataContext;

namespace SAMS.Test.DBTest
{
    public class SAMSContextTest
    {
        protected readonly IServiceProvider ServiceProvider;

        public SAMSContextTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            services.AddDbContext<SAMSContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SAMSDatabase"), sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null)
                ));

            ServiceProvider = services.BuildServiceProvider();
        }

        // Método para obtener un contexto
        protected SAMSContext GetContext()
        {
            return ServiceProvider.GetRequiredService<SAMSContext>();
        }
    }
}