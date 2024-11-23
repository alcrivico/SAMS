using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para RegistrarProductoView.xaml
    /// </summary>
    public partial class RegistrarProductoView : Window
    {
        EmpleadoLoginDTO empleado;
        SideBarControl SideBarControl_MenuLateral;
        List<PedidosPendientesDTO> listaPedidos;
        ObservableCollection<Object> _pedidos;
        ObservableCollection<Object> _productos;
        List<CategoriaDTO> categorias;
        private ObservableCollection<object> _categorias;
        List<RegistrarProductoInventarioDTO> productosInventario;

        public RegistrarProductoView(EmpleadoLoginDTO empleado)
        {
            this.empleado = empleado;

            InitializeComponent();

            _pedidos = new ObservableCollection<Object>();
            _productos = new ObservableCollection<Object>();
            categorias = new List<CategoriaDTO>();
            _categorias = new ObservableCollection<object>();
            productosInventario = new List<RegistrarProductoInventarioDTO>();

            DefinirColumnasPedidos();
            ObtenerPedidosPendientes();
            DefinirColumnasProductos();
            ObtenerCategorias();

            SideBarControl_MenuLateral = new SideBarControl(empleado);
            SideBarControl_MenuLateral.SideElementSelected = 1;
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;

            TableControl_TablaPedidos.SelectedItemChanged += TableControl_TablaPedidos_SelectedItemChanged;
            TableControl_TablaProductos.SelectedItemChanged += TableControl_TablaProductos_SelectedItemChanged;

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

        private void Button_Registrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {

        }

        private void DefinirColumnasPedidos()
        {

            Dictionary<string, string>[] columnas =
            {
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "No pedido" },
                    { "Width", "*" },
                    { "BindingName", "noPedido" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Fecha de entrega" },
                    { "Width", "*" },
                    { "BindingName", "fechaEntrega" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Proveedor" },
                    { "Width", "*" },
                    { "BindingName", "nombreProveedor" }

                }
            };
            
            TableControl_TablaPedidos.DefineColumns(columnas);

        }

        private void ObtenerPedidosPendientes()
        {
            try
            {
                listaPedidos = ProductoInventarioDAO.ObtenerPedidosPendientes();
                _pedidos.Clear();

                _pedidos = new ObservableCollection<Object>(listaPedidos);

                TableControl_TablaPedidos.SetItemsSource(_pedidos);

            }
            catch (Exception ex)
            {
                PrincipalView principalView = new PrincipalView(empleado);
                principalView.Show();
                this.Close();
            }
        }

        private void DefinirColumnasProductos()
        {

            Dictionary<string, string>[] columnas =
            {
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Código" },
                    { "Width", "*" },
                    { "BindingName", "codigoProducto" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Nombre" },
                    { "Width", "*" },
                    { "BindingName", "nombreProducto" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Cantidad" },
                    { "Width", "*" },
                    { "BindingName", "cantidad" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Precio de compra" },
                    { "Width", "*" },
                    { "BindingName", "precioCompra" }

                }
            };

            TableControl_TablaProductos.DefineColumns(columnas);

        }

        private void ObtenerProductosPorPedido(string noPedido)
        {
            try
            {
                var productosPorPedido = ProductoInventarioDAO.ObtenerProductosPorPedido(noPedido);
                _productos.Clear();

                _productos = new ObservableCollection<Object>(productosPorPedido);

                TableControl_TablaProductos.SetItemsSource(_productos);
            }
            catch (Exception ex)
            {
                
                InformationControl.Show("Error", "No se pudo conectar a la red del supermercado," +
                    " inténtelo de nuevo más tarde", "Aceptar");
                
                PrincipalView principalView = new PrincipalView(empleado);
                principalView.Show();
                this.Close();
            }
        }

        private void TableControl_TablaPedidos_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            if (TableControl_TablaPedidos.GetSelectedItem() is PedidosPendientesDTO pedidoSeleccionado)
            {
                string noPedido = pedidoSeleccionado.noPedido;
                ObtenerProductosPorPedido(noPedido);
            }
        }

        private void ObtenerCategorias()
        {
            categorias = ProductoInventarioDAO.ObtenerCategorias();
            ConvertirCategorias(categorias);
            ComboBoxControl_Categorias.SetItemsSource(_categorias, "nombre");
        }

        private void ConvertirCategorias(List<CategoriaDTO> categorias)
        {
            _categorias.Clear();

            foreach (CategoriaDTO categoria in categorias)
            {
                _categorias.Add(categoria);
            }

        }

        private void TableControl_TablaProductos_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            ProductosPorPedidoDTO productoSeleccionado = (ProductosPorPedidoDTO)TableControl_TablaProductos.GetSelectedItem();

            // Validar si el producto seleccionado ya tiene un precio y una categoría registrados
            if (ProductoTieneDatosRegistrados(productoSeleccionado))
            {
                // Cargar los datos del producto seleccionado
                CargarDatosProducto(productoSeleccionado);
            }
            else
            {
                // Guardar los datos ingresados anteriormente
                GuardarDatosProducto();

                // No actualizar los controles si no tiene datos registrados
            }

        }

        private bool ProductoTieneDatosRegistrados(ProductosPorPedidoDTO productoSeleccionado)
        {
            var productoInventario = productosInventario
                        .FirstOrDefault(p => p.codigoProducto == productoSeleccionado.codigoProducto);

            return productoInventario != null;
        }

        private void GuardarDatosProducto()
        {
            if (TableControl_TablaProductos.GetSelectedItem() is ProductosPorPedidoDTO productoSeleccionado)
            {
                // Validar si hay datos ingresados en los controles
                if (!string.IsNullOrEmpty(TextBoxControl_PrecioVenta.Text) && ComboBoxControl_Categorias.SelectedItem != null)
                {
                    var productoInventario = productosInventario
                                .FirstOrDefault(p => p.codigoProducto == productoSeleccionado.codigoProducto);

                    if (productoInventario == null)
                    {
                        productoInventario = new RegistrarProductoInventarioDTO
                        {
                            noPedido = productoSeleccionado.numeroPedido,
                            codigoProducto = productoSeleccionado.codigoProducto,
                            nombreCategoria = ComboBoxControl_Categorias.SelectedItem?.ToString(),
                            precioActual = decimal
                                .TryParse(TextBoxControl_PrecioVenta.Text, out decimal precio) ? precio : 0
                        };
                        productosInventario.Add(productoInventario);
                    }
                    else
                    {
                        productoInventario.nombreCategoria = ComboBoxControl_Categorias.SelectedItem?.ToString();
                        productoInventario.precioActual = decimal
                            .TryParse(TextBoxControl_PrecioVenta.Text, out decimal precio) ? precio : 0;
                    }
                }
            }
        }

        private void CargarDatosProducto(ProductosPorPedidoDTO productoSeleccionado)
        {
            var productoInventario = productosInventario.FirstOrDefault(p => p.codigoProducto == productoSeleccionado.codigoProducto);

            if (productoInventario != null)
            {
                ComboBoxControl_Categorias.SelectedItem = productoInventario.nombreCategoria;
                TextBoxControl_PrecioVenta.Text = productoInventario.precioActual.ToString();
            }
            else
            {
                LimpiarControles();
            }
        }

        private void LimpiarControles()
        {
            TextBoxControl_PrecioVenta.Text = string.Empty;
        }
    }
}
