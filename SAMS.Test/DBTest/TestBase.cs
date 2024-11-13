using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.Models.DataContext;

namespace SAMS.Test.DBTest
{
    public class TestBase
    {
        protected readonly IServiceProvider ServiceProvider;

        public TestBase()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            services.AddDbContext<SAMSContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SAMSDatabase"), sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Número máximo de reintentos
                        maxRetryDelay: TimeSpan.FromSeconds(10), // Tiempo de espera entre reintentos
                        errorNumbersToAdd: null) // Si se desean agregar errores específicos para reintentos
                ));

            ServiceProvider = services.BuildServiceProvider();
        }

        protected SAMSContext GetContext()
        {
            return ServiceProvider.GetRequiredService<SAMSContext>();
        }

    }
}
