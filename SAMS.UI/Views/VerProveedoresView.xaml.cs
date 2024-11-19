using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
    /// Lógica de interacción para VerProveedoresView.xaml
    /// </summary>
    public partial class VerProveedoresView : Window
    {
        Proveedor _proveedor;
        List<V_Proveedores> listaProveedores;
        ObservableCollection<Object> _proveedores;

        public VerProveedoresView()
        {
            _proveedor = new Proveedor();
            _proveedores = new ObservableCollection<Object>();

            InitializeComponent();
            DefinirColumnas();
            ObtenerProveedores();

            TablaProveedores.OnDetallesClickedHandler += botonDetallesClick;
            TablaProveedores.OnEditarClickedHandler += botonEditarClick;
            TablaProveedores.OnEliminarClickedHandler += botonEliminarClick;
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
                    { "Name", "Nombre" },
                    { "Width", "*" },
                    { "BindingName", "nombre" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "RFC" },
                    { "Width", "*" },
                    { "BindingName", "rfc" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Estado" },
                    { "Width", "*" },
                    { "BindingName", "estado" }
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

            TablaProveedores.DefineColumns(columnas);

        }

        private void ObtenerProveedores()
        {
            try
            {
                listaProveedores = ProveedorDAO.ObtenerProveedores();
                _proveedores.Clear();
                _proveedores = new ObservableCollection<Object>(listaProveedores);
                TablaProveedores.SetItemsSource(_proveedores);
            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", "Ocurrió un error al obtener los proveedores", "Aceptar");
                this.Close();
            }
        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (listaProveedores != null)
            {

                if (campoBuscar.Text.Length > 0)
                {
                    var proveedoresFiltrados = listaProveedores.Where(
                        p =>
                        p.nombre.ToUpper().Contains(campoBuscar.Text.ToUpper()) ||
                        p.rfc.ToUpper().Contains(campoBuscar.Text.ToUpper())).ToList();//ToUpper() para que no sea case sensitive

                    _proveedores.Clear();

                    _proveedores = new ObservableCollection<Object>(proveedoresFiltrados);

                    TablaProveedores.SetItemsSource(_proveedores);

                }
                else
                {

                    _proveedores.Clear();

                    _proveedores = new ObservableCollection<Object>(listaProveedores);

                    TablaProveedores.SetItemsSource(_proveedores);

                }

            }
        }

        private void botonDetallesClick(object sender, RoutedEventArgs e)
        {
            //ActionsControl actionBar = (ActionsControl)sender;
            //MonederosDTO monedero = (MonederosDTO)actionBar.DataContext;

            DetalleProveedorView detalleProveedorView = new DetalleProveedorView();
            detalleProveedorView.Show();
        }

        private void botonEditarClick(object sender, RoutedEventArgs e)
        {
            Debug.Print("Editar");
        }

        private void botonEliminarClick(object sender, RoutedEventArgs e)
        {
            Debug.Print("Eliminar");
        }

    }
}
