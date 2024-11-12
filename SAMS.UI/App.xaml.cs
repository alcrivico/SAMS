using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Views;

namespace SAMS.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        private IConfiguration Configuration { get; set; }


        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            // Cargar la configuración desde appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            // Configurar Dependency Injection
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();



            VerMonederosView initialView = new();
            initialView.Show();

        }
        private void ConfigureServices(IServiceCollection services)
        {
            // Registrar SAMSContext con la cadena de conexión
            services.AddDbContext<SAMSContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SAMSDatabase")));
        }

    }

}
