using System;
using System.Collections.Generic;
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
using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SAMS.UI.Models.Entities;
using System.Diagnostics;


namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para VerPedidosView.xaml
    /// </summary>
    public partial class VerPedidosView : Window
    {
        List<PedidosDTO> listaPedidos;
        ObservableCollection<Object> _pedidos;
        EmpleadoLoginDTO _empleado;
        SideBarControl SideBarControl_MenuLateral;

        public VerPedidosView(EmpleadoLoginDTO empleado)
        {
            _empleado = empleado;
            listaPedidos = new List<PedidosDTO>();
            _pedidos = new ObservableCollection<Object>();

            InitializeComponent();
            DefinirColumnas();
            ObtenerPedidos();

            SideBarControl_MenuLateral = new SideBarControl(_empleado);
            SideBarControl_MenuLateral.SideElementSelected = 5;
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = _empleado.tipoEmpleado;

            TablaPedidos.OnDetallesClickedHandler += botonDetallesClick;
            TablaPedidos.OnEditarClickedHandler += botonEditarClick;
            TablaPedidos.OnEliminarClickedHandler += botonEliminarClick;
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
                    { "Name", "Numero de pedido" },
                    { "Width", "*" },
                    { "BindingName", "noPedido" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Proveedor" },
                    { "Width", "*" },
                    { "BindingName", "nombreProveedor" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Fecha de registro" },
                    { "Width", "*" },
                    { "BindingName", "FechaPedido" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Estado" },
                    { "Width", "*" },
                    { "BindingName", "nombreEstado" }

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

            TablaPedidos.DefineColumns(columnas);

        }

        private void ObtenerPedidos()
        {
            try
            {
                listaPedidos = PedidoDAO.ObtenerPedidos().ToList();
                _pedidos.Clear();
                _pedidos = new ObservableCollection<Object>(listaPedidos);
                TablaPedidos.SetItemsSource(_pedidos);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                InformationControl.Show("Error", "Ocurrió un error al obtener los pedidos", "Aceptar");
                this.Close();
            }
        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (listaPedidos != null)
            {

                if (campoBuscar.Text.Length > 0)
                {
                    var pedidosFiltrados = listaPedidos.Where(
                        p =>
                        p.nombreProveedor.ToUpper().Contains(campoBuscar.Text.ToUpper())).ToList();

                    _pedidos.Clear();

                    _pedidos = new ObservableCollection<Object>(pedidosFiltrados);

                    TablaPedidos.SetItemsSource(_pedidos);

                }
                else
                {

                    _pedidos.Clear();

                    _pedidos = new ObservableCollection<Object>(listaPedidos);

                    TablaPedidos.SetItemsSource(_pedidos);

                }

            }
        }

        private void botonDetallesClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void botonEditarClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void botonEliminarClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void botonAgregar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
