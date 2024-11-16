using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        Monedero _monedero;
        ObservableCollection<Object> _monederos;

        public VerMonederosView()
        {

            _monedero = new Monedero();
            _monederos = new ObservableCollection<Object>();

            InitializeComponent();

            DefinirColumnas();

            ObtenerMonederos();

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

        private void ObtenerMonederos()
        {

            List<MonederosDTO> listaMonederos = MonederoDAO.ObtenerMonederos();

            _monederos.Clear();

            _monederos = new ObservableCollection<Object>(listaMonederos);

            TablaMonederos.SetItemsSource(_monederos);

        }

    }
    
}
