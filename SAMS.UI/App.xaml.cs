using System.Configuration;
using System.Data;
using System.Windows;
using SAMS.UI.Views;

namespace SAMS.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public void ApplicationStart(object sender, StartupEventArgs e)
        {

            PrincipalView initialView = new();

            initialView.Show();

        }

    }

}
