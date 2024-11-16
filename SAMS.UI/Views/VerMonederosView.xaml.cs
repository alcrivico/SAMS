using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
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
        List<MonederosDTO> listaMonederos;
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
                    { "Detalles", "True" },
                    { "Editar", "True" },
                    { "Eliminar", "False" }

                }

            };

            TablaMonederos.DefineColumns(columnas);

        }

        private void ObtenerMonederos()
        {

            try
            {

                listaMonederos = MonederoDAO.ObtenerMonederos();

                _monederos.Clear();

                _monederos = new ObservableCollection<Object>(listaMonederos);

                TablaMonederos.SetItemsSource(_monederos);

            }
            catch (Exception ex)
            {

                InformationControl.Show("Error", "Ocurrió un error al obtener los monederos", "Aceptar");

                this.Close();

            }

        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (listaMonederos != null)
            {

                if (campoBuscar.Text.Length > 0)
                {
                    var monederosFiltrados = listaMonederos.Where(
                        m =>
                        m.codigoDeBarras.Contains(campoBuscar.Text) ||
                        m.telefono.Contains(campoBuscar.Text) ||
                        m.nombrePropietario.Contains(campoBuscar.Text)).ToList();

                    _monederos.Clear();

                    _monederos = new ObservableCollection<Object>(monederosFiltrados);

                    TablaMonederos.SetItemsSource(_monederos);

                }
                else
                {

                    _monederos.Clear();

                    _monederos = new ObservableCollection<Object>(listaMonederos);

                    TablaMonederos.SetItemsSource(_monederos);

                }

            }

        }

    }
    
}
