using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.Models.DataContext;
using Xunit;

namespace SAMS.Tests
{
    public class DatabaseTests
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public DatabaseTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddDbContext<SAMSContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("SAMSDatabase")));

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void TestDatabaseConnection()
        {
            using var context = _serviceProvider.GetService<SAMSContext>();
            Assert.NotNull(context);
            Assert.True(context.Database.CanConnect(), "No se pudo conectar a la base de datos.");

            var empleados = context.V_Empleados.ToList();
            Assert.NotNull(empleados);
        }
    }
}
