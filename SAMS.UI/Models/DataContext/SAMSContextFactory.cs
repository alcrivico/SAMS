using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SAMS.UI.Models.DataContext
{
    public class SAMSContextFactory : IDesignTimeDbContextFactory<SAMSContext>
    {
        public SAMSContext CreateDbContext(string[] args)
        {
            // Cargar configuración desde appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Obtener la cadena de conexión del archivo appsettings.json
            var connectionString = configuration.GetConnectionString("SAMSDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<SAMSContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SAMSContext(optionsBuilder.Options);
        }
    }
}
