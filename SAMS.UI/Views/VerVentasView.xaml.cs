using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for VerVentasView.xaml
    /// </summary>
    public partial class VerVentasView : Window
    {

        Venta _venta;
        EmpleadoLoginDTO _empleado;
        List<VentasDTO> listaVentas;
        ObservableCollection<Object> _ventas;

        public VerVentasView()
        {
            InitializeComponent();
        }

        public VerVentasView(EmpleadoLoginDTO empleado)
        {

            _venta = new Venta();
            _ventas = new ObservableCollection<Object>();
            _empleado = empleado;

            InitializeComponent();

            ConfigurarSideBar();

            DefinirColumnas();

            ObtenerVentas();

            TablaVentas.OnDetallesClickedHandler += botonDetallesClick;
            TablaVentas.OnEditarClickedHandler += botonEditarClick;
            TablaVentas.OnEliminarClickedHandler += botonEliminarClick;

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
                    { "Name", "No. Venta" },
                    { "Width", "*" },
                    { "BindingName", "noVenta" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Total de Venta" },
                    { "Width", "*" },
                    { "BindingName", "precioVenta" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Fecha de Registro" },
                    { "Width", "*" },
                    { "BindingName", "fechaRegistro" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "No. Caja" },
                    { "Width", "*" },
                    { "BindingName", "noCaja" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Responsable de Caja" },
                    { "Width", "*" },
                    { "BindingName", "nombreEmpleado" }

                },
                new Dictionary<string, string> {

                    { "Type", "Actions" },
                    { "Name", "Acciones" },
                    { "Width", "*" },
                    { "Detalles", "True" },
                    { "Editar", "True" },
                    { "Eliminar", "True" }

                }

            };

            if (_empleado.tipoEmpleado == "Cajero")
            {
                columnas[5]["Editar"] = "False";
                columnas[5]["Eliminar"] = "False";
            }

            TablaVentas.DefineColumns(columnas);

        }

        private void ConfigurarSideBar()
        {

            SideBarControl sideBarControl = new SideBarControl(_empleado);
            sideBarControl.Employee = _empleado.tipoEmpleado;

            if (_empleado.tipoEmpleado == "Cajero")
            {
                sideBarControl.SideElementSelected = 2;
            }
            else
            {
                sideBarControl.SideElementSelected = 4;
            }

            MenuLateral.Children.Add(sideBarControl);

        }

        private void ObtenerVentas()
        {
            try
            {

                listaVentas = VentaDAO.ObtenerVentas();

                _ventas.Clear();

                _ventas = new ObservableCollection<Object>(listaVentas);

                TablaVentas.SetItemsSource(_ventas);

            }
            catch (Exception ex)
            {

                InformationControl.Show("Error", ex.Message, "Aceptar");

                this.Close();

            }
        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void botonDetallesClick(object sender, RoutedEventArgs e)
        {

            ActionsControl actionBar = (ActionsControl)sender;

            VentasDTO venta = (VentasDTO) actionBar.DataContext;

            VentaView ventaView = new VentaView(_empleado, 3);

            ventaView.Show();

            this.Close();

        }

        private void botonEditarClick(object sender, RoutedEventArgs e)
        {

            ActionsControl actionBar = (ActionsControl)sender;

            VentasDTO venta = (VentasDTO)actionBar.DataContext;

            VentaView ventaView = new VentaView(_empleado, 2);

            ventaView.Show();

            this.Close();

        }

        private void botonEliminarClick(object sender, RoutedEventArgs e)
        {
            ActionsControl actionBar = (ActionsControl)sender;
        }

    }
}
