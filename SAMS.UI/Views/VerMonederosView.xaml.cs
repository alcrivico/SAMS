using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Interaction logic for VerMonederosView.xaml
    /// </summary>
    public partial class VerMonederosView : Window
    {
        public VerMonederosView()
        {
            InitializeComponent();

            DefinirColumnas();

        }

        private void TitleBarControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (e.OriginalSource is FrameworkElement element &&
                (element.Name == "MinusLogo" ||
                element.Name == "MaximizeLogo" ||
                element.Name == "ExitLogo"))
            {
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void TitleBarControl_WindowStateChangeRequested(object sender, WindowState e)
        {
            this.WindowState = e;
        }

        private void DefinirColumnas()
        {

            Dictionary<string, string>[] columnas =
            {
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Código de Barras" },
                    { "Width", "*" },
                    { "BindingName", "codigoDeBarras" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Teléfono" },
                    { "Width", "*" },
                    { "BindingName", "telefono" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Nombre del Propietario" },
                    { "Width", "*" },
                    { "BindingName", "nombrePropietario" }

                },
                new Dictionary<string, string> {

                    { "Type", "Actions" },
                    { "Name", "Acciones" },
                    { "Width", "*" },

                }

            };

            TablaMonederos.DefineColumns(columnas);

        }

        private void TablaMonederos_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
    
}
