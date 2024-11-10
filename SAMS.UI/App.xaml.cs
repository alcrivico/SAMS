using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Views;

namespace SAMS.UI
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        private IConfiguration Configuration { get; set; }

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var initialView = new IniciarSesionView();
            initialView.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SAMSContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SAMSDatabase")));
        }
    }
}
