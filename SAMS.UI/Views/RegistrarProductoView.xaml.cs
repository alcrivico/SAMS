using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

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
        ProductosPorPedidoDTO productoSeleccionadoAnterior;
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
            RegistrarOActualizarListaDeProductosInventario();
            
            if (!MostrarErrorEnProductoInvalido(ObtenerProductoConValoresPorDefecto()))
            {
                RegistrarProductosEnInventarioYEnPedido();
            }
        }

        private void Button_Cancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            if (productosInventario.Count != 0)
            {
                bool result = ConfirmationControl.Show(
                    "Confirmación",
                    "¿Estás seguro de cancelar la operación? se perderán los cambios",
                    "Aceptar",
                    "Cancelar"
                );

                if (result)
                {
                    CerrarVentana();
                }
            }
            else
            {
                CerrarVentana();
            }
        }

        private void CerrarVentana()
        {
            VerProductosView verProductosView = new VerProductosView(empleado);
            verProductosView.Show();
            this.Close();
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
                    { "Name", "Fecha de pedido" },
                    { "Width", "*" },
                    { "BindingName", "fechaPedido" },

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
                productosInventario.Clear();
                LimpiarApartadoRegistro();
                VerificarYActivarBotonRegistrarYCamposRellenables();
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
            RegistrarOActualizarListaDeProductosInventario();
            VerificarYActivarBotonRegistrarYCamposRellenables();
        }

        private void RegistrarOActualizarListaDeProductosInventario()
        {
            ProductosPorPedidoDTO productoSelecionadoActual = (ProductosPorPedidoDTO)TableControl_TablaProductos.GetSelectedItem();
            List<ProductosPorPedidoDTO> productosSeleccionados = _productos.OfType<ProductosPorPedidoDTO>().ToList();

            if (productoSelecionadoActual != null)
            {
                ModificarEtiquetaDeProducto(productoSelecionadoActual);
                
                if (YaFueRegistrado(productoSeleccionadoAnterior) && productoSeleccionadoAnterior != null)
                {
                    if (!productoSeleccionadoAnteriorSinCambios(productoSeleccionadoAnterior) || !CamposVacios())
                    {
                        ModificarProductoARegistrar(productoSeleccionadoAnterior);
                    }
                }
                else
                {
                    GuardarProductosARegistrar(productosSeleccionados);
                }

                if (YaFueRegistrado(productoSelecionadoActual))
                {
                    LlenarCamposConProducto(productoSelecionadoActual);
                }
                else
                {
                    LimpiarCampos();
                }

                productoSeleccionadoAnterior = productoSelecionadoActual;

            }
            
        }

        private bool YaFueRegistrado(ProductosPorPedidoDTO productoSeleccionado)
        {
            foreach (var productoInventario in productosInventario)
            {
                if (productoSeleccionado != null && productoInventario.codigoProducto == productoSeleccionado.codigoProducto)
                {
                    return true;
                }
            }
            return false;
        }

        private void GuardarProductosARegistrar(List<ProductosPorPedidoDTO> productosSeleccionados)
        {
            if (productosSeleccionados != null && productosSeleccionados.Count > 0)
            {
                foreach (var productoSeleccionado in productosSeleccionados)
                {
                    decimal precioPorDefecto = 0.0m;
                    DateTime fechaCaducidadPorDefecto = DateTime.MinValue;

                    RegistrarProductoInventarioDTO productoInventario = new RegistrarProductoInventarioDTO
                    {
                        noPedido = productoSeleccionado.numeroPedido,
                        codigoProducto = productoSeleccionado.codigoProducto,
                        nombreCategoria = ((CategoriaDTO)ComboBoxControl_Categorias.SelectedItem)?.nombre,
                        precioActual = decimal.TryParse(TextBoxControl_PrecioVenta.Text.Trim(), out decimal precio) ? precio : precioPorDefecto,
                        fechaCaducidad = DatePicker_FechaCaducidad.SelectedDate ?? fechaCaducidadPorDefecto,
                    };

                    productosInventario.Add(productoInventario);
                }
            }
        }

        private void ModificarProductoARegistrar(ProductosPorPedidoDTO productoSeleccionado)
        {
            DateTime fechaCaducidadPorDefecto = DateTime.MinValue;

            foreach (RegistrarProductoInventarioDTO productoInventario in productosInventario)
            {
                if (productoInventario.codigoProducto == productoSeleccionadoAnterior.codigoProducto)
                {
                    productoInventario.precioActual = decimal
                        .TryParse(TextBoxControl_PrecioVenta.Text, out decimal precio) ? precio : 0;
                    productoInventario.nombreCategoria = ((CategoriaDTO)ComboBoxControl_Categorias.SelectedItem)?.nombre;
                    productoInventario.fechaCaducidad = DatePicker_FechaCaducidad.SelectedDate ?? fechaCaducidadPorDefecto;
                }
            }
        }

        private void LimpiarCampos()
        {
            TextBoxControl_PrecioVenta.Text = string.Empty;
            DatePicker_FechaCaducidad.SelectedDate = null;
            ComboBoxControl_Categorias.SelectedItem = 1;
            ReiniciarMensajesError();
        }

        private void LimpiarApartadoRegistro()
        {
            TextBoxControl_PrecioVenta.Text = string.Empty;
            DatePicker_FechaCaducidad.SelectedDate = null;
            ComboBoxControl_Categorias.SelectedItem = 1;
            TextBlock_NombreProducto.Text = "";
            ReiniciarMensajesError();
        }

        private bool CamposVacios()
        {
            return string.IsNullOrWhiteSpace(TextBoxControl_PrecioVenta.Text) ||
                !DatePicker_FechaCaducidad.SelectedDate.HasValue;
        }

        private void LlenarCamposConProducto(ProductosPorPedidoDTO productoSeleccionado)
        {
            foreach (RegistrarProductoInventarioDTO productoInventario in productosInventario)
            {
                if (productoInventario.codigoProducto == productoSeleccionado.codigoProducto)
                {
                    TextBoxControl_PrecioVenta.Text = productoInventario.precioActual.ToString();
                    var categoriaSeleccionada = _categorias
                        .FirstOrDefault(c => ((CategoriaDTO)c).nombre == productoInventario.nombreCategoria);
                    ComboBoxControl_Categorias.ComboBoxControlType.SelectedItem = categoriaSeleccionada;
                    DatePicker_FechaCaducidad.SelectedDate = productoInventario.fechaCaducidad;
                    LimpiarCamposSiNoSeRegistraron();
                    break;
                }
            }
        }

        private void ModificarEtiquetaDeProducto(ProductosPorPedidoDTO productoSelecionadoActual)
        {
            if (productoSelecionadoActual != null)
            {
                TextBlock_NombreProducto.Inlines.Clear();
                TextBlock_NombreProducto.Inlines
                        .Add(new Run("Datos a registrar del producto: ")
                        { Foreground = Brushes.Black });
                TextBlock_NombreProducto.Inlines
                        .Add(new Run(productoSelecionadoActual.nombreProducto)
                        { Foreground = (Brush)Application.Current.Resources["SolidColorBrush_DodgerBlue"] });

            }
        }

        private bool productoSeleccionadoAnteriorSinCambios(ProductosPorPedidoDTO productoSeleccionado)
        {
            bool sonIguales = false;

            foreach (RegistrarProductoInventarioDTO productoInventario in productosInventario)
            {
                if (productoInventario.codigoProducto == productoSeleccionado.codigoProducto)
                {
                    if (TextBoxControl_PrecioVenta.Text == productoInventario.precioActual.ToString() &&
                            DatePicker_FechaCaducidad.SelectedDate == productoInventario.fechaCaducidad &&
                            productoInventario.nombreCategoria ==
                            ((CategoriaDTO)ComboBoxControl_Categorias.SelectedItem)?.nombre)
                    {
                        return sonIguales = true;
                    }
                }
            }
            return sonIguales;
        }

        private void LimpiarCamposSiNoSeRegistraron()
        {
            if (decimal.TryParse(TextBoxControl_PrecioVenta.Text, out decimal precio) && precio == 0.0m
                        || TextBoxControl_PrecioVenta.Text == null)
            {
                TextBoxControl_PrecioVenta.Text = null;
            }
            if (DatePicker_FechaCaducidad.SelectedDate == DateTime.MinValue
                        || DatePicker_FechaCaducidad.SelectedDate == null)
            {
                DatePicker_FechaCaducidad.SelectedDate = null;
            }

        }

        private RegistrarProductoInventarioDTO ObtenerProductoConValoresPorDefecto()
        {
            decimal precioPorDefecto = 0.0m;
            DateTime fechaCaducidadPorDefecto = DateTime.MinValue;
            string codigoFallido;

            foreach (RegistrarProductoInventarioDTO productoInventario in productosInventario)
            {
                if (productoInventario.precioActual == precioPorDefecto &&
                    productoInventario.fechaCaducidad == fechaCaducidadPorDefecto || productoInventario.precioActual <= 0)
                {
                    return productoInventario;
                }

                if (productoInventario.fechaCaducidad == fechaCaducidadPorDefecto)
                {
                    return productoInventario;
                }
            }

            return null;
        }


        private bool MostrarErrorEnProductoInvalido(RegistrarProductoInventarioDTO productoInvalido)
        {
            ReiniciarMensajesError();
            bool productoInvalidoEncontrado = false;
            List<ProductosPorPedidoDTO> productosCargadosEnTablaproductos = _productos.OfType<ProductosPorPedidoDTO>().ToList();
            decimal precioPorDefecto = 0.0m; //Esto deberian ser constantes
            DateTime fechaCaducidadPorDefecto = DateTime.MinValue;
            if (productoInvalido != null)
            {
                foreach (ProductosPorPedidoDTO producto in productosCargadosEnTablaproductos)
                {
                    if (producto.codigoProducto == productoInvalido.codigoProducto)
                    {
                        TableControl_TablaProductos.SetSelectedItem(producto);

                        if (productoInvalido.fechaCaducidad == fechaCaducidadPorDefecto)
                        {
                            TextBlock_MensajeFechaInvalida.Visibility = Visibility.Visible;
                            productoInvalidoEncontrado = true;
                        }

                        if (productoInvalido.precioActual == precioPorDefecto)
                        {
                            TextBlock_MensajePrecioInvalido.Visibility = Visibility.Visible;
                            productoInvalidoEncontrado = true;
                        }

                    }
                }
            }
            return productoInvalidoEncontrado;
        }

        private void ReiniciarMensajesError()
        {
            TextBlock_MensajeFechaInvalida.Visibility = Visibility.Hidden;
            TextBlock_MensajePrecioInvalido.Visibility = Visibility.Hidden;
        }
        private void VerificarYActivarBotonRegistrarYCamposRellenables()
        {
            if (TableControl_TablaProductos.SelectedItem != null)
            {
                Button_Registrar.IsButtonEnabled = true;
                TextBoxControl_PrecioVenta.IsEnabled = true;
                DatePicker_FechaCaducidad.IsEnabled = true;
                ComboBoxControl_Categorias.IsEnabled = true;
            }
            else
            {
                Button_Registrar.IsButtonEnabled = false;
                TextBoxControl_PrecioVenta.IsEnabled = false;
                DatePicker_FechaCaducidad.IsEnabled = false;
                ComboBoxControl_Categorias.IsEnabled = false;
            }
        }

        private async void RegistrarProductosEnInventarioYEnPedido()
        {
            try
            {
                bool resultado = await ProductoInventarioDAO.RegistrarProductosInventario(productosInventario);
                bool resultadoPedido = await PedidoDAO.ActualizarEstadoPedido(new ActualizarEstadoPedidoDTO
                {
                    noPedido = productosInventario[0].noPedido,
                    fechaEntrega = DateTime.Now
                });

                if (resultado && resultadoPedido)
                {
                    InformationControl.Show("Éxito", "Los productos del pedido se registraron correctamente.", "Aceptar");
                    CerrarVentana();
                }
                else
                {
                    InformationControl.Show("Error", "Ocurrió un error al registrar los productos. Intenta de nuevo.", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", $"Error al registrar los productos: {ex.Message}", "Aceptar");
            }
        }
    }
}
