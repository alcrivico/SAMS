using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DotNetEnv;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Interaction logic for VentaView.xaml
    /// </summary>
    public partial class VentaView : Window
    {
        EmpleadoLoginDTO _empleado;
        string _NoCaja;
        int _opcionMenu;
        DetalleVentaDTO _detalleVenta;
        List<DetalleVentaDTO> _listaDetalleVentas;
        ObservableCollection<Object> _detalleVentas;
        decimal _pagoEfectivo;
        decimal _pagoTarjeta;
        decimal _pagoMonedero;

        public VentaView()
        {
            InitializeComponent();
        }

        public VentaView(EmpleadoLoginDTO empleado, int opcion)
        {
            
            _empleado = empleado;
            _opcionMenu = opcion;
            _listaDetalleVentas = new List<DetalleVentaDTO>();
            _detalleVentas = new ObservableCollection<Object>();
            _pagoEfectivo = 0;
            _pagoTarjeta = 0;
            _pagoMonedero = 0;

            InitializeComponent();

            ConfigurarSideBar(opcion);

            ConfigurarMenu(opcion);

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
                    { "Name", "Producto" },
                    { "Width", "*" },
                    { "BindingName", "nombreDetalleVenta" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Precio" },
                    { "Width", "*" },
                    { "BindingName", "precio" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Cantidad" },
                    { "Width", "*" },
                    { "BindingName", "cantidad" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Promoción" },
                    { "Width", "*" },
                    { "BindingName", "promocion" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Total" },
                    { "Width", "*" },
                    { "BindingName", "total" }

                }

            };

            TablaProductos.DefineColumns(columnas);

        }

        private void ConfigurarMenu(int opcion)
        {
            switch (opcion)
            {
                case 1:

                    var envPath = System.IO.Path.Combine(AppContext.BaseDirectory, "../../../.env");

                    if (!File.Exists(envPath))
                    {
                        throw new Exception("No se encontró el archivo .env");
                    }

                    Env.Load(envPath);

                    _NoCaja = Env.GetString("NO_CAJA");

                    botonAgregar.IsButtonEnabled = false;
                    campoCantidad.EnableTextBox = false;
                    botonBorrar.Visibility = Visibility.Hidden;
                    
                    campoSubtotal.IsReadOnly = true;
                    campoSubtotal.Cursor = Cursors.Arrow;
                    campoIVA.IsReadOnly = true;
                    campoIVA.Cursor = Cursors.Arrow;
                    campoTotal.IsReadOnly = true;
                    campoTotal.Cursor = Cursors.Arrow;

                    campoSubtotal.Text = "0.00";
                    campoIVA.Text = "0.00";
                    campoTotal.Text = "0.00";

                    checkRedondear.IsEnabled = false;

                    campoPagoEfectivo.Visibility = Visibility.Hidden;
                    campoCambio.Visibility = Visibility.Hidden;

                    campoPagoTarjeta.Visibility = Visibility.Hidden;

                    campoMonedero.Visibility = Visibility.Hidden;
                    buscarMonedero.Visibility = Visibility.Hidden;
                    verificarMonedero.Visibility = Visibility.Hidden;
                    campoPagoMonedero.EnableTextBox = false;
                    campoPagoMonedero.Cursor = Cursors.Arrow;
                    campoPagoMonedero.Visibility = Visibility.Hidden;

                    campoMontoPagado.Text = "0.00";
                    campoSaldoRestante.Text = "0.00";
                    campoMontoPagado.IsReadOnly = true;
                    campoMontoPagado.Cursor = Cursors.Arrow;
                    campoSaldoRestante.IsReadOnly = true;
                    campoSaldoRestante.Cursor = Cursors.Arrow;

                    botonAccion.IsButtonEnabled = false;

                    break;

                case 2:

                    UseCaseTitle.Text = "Editar Venta";

                    botonAgregar.Text = "Cambiar";
                    botonAgregar.IsButtonEnabled = false;
                    botonBorrar.IsButtonEnabled = false;
                    campoCantidad.EnableTextBox = false;

                    campoSubtotal.IsReadOnly = true;
                    campoSubtotal.Cursor = Cursors.Arrow;
                    campoIVA.IsReadOnly = true;
                    campoIVA.Cursor = Cursors.Arrow;
                    campoTotal.IsReadOnly = true;
                    campoTotal.Cursor = Cursors.Arrow;

                    checkRedondear.IsEnabled = false;

                    campoPagoEfectivo.Visibility = Visibility.Hidden;
                    campoCambio.Visibility = Visibility.Hidden;
                    campoPagoTarjeta.Visibility = Visibility.Hidden;
                    campoMonedero.Visibility = Visibility.Hidden;
                    buscarMonedero.Visibility = Visibility.Hidden;
                    verificarMonedero.Visibility = Visibility.Hidden;
                    campoPagoMonedero.Visibility = Visibility.Hidden;

                    campoMontoPagado.IsReadOnly = true;
                    campoMontoPagado.Cursor = Cursors.Arrow;
                    campoSaldoRestante.IsReadOnly = true;
                    campoSaldoRestante.Cursor = Cursors.Arrow;

                    botonAccion.Text = "Editar Cambios";
                    botonAccion.IsButtonEnabled = false;

                    botonCancelar.Text = "Cancelar";

                    break;

                case 3:
                    UseCaseTitle.Text = "Detalle de Venta";

                    campoProducto.FieldName = "Responsable";
                    campoProducto.IsReadOnly = true;
                    campoProducto.Cursor = Cursors.Arrow;

                    campoCantidad.FieldName = "No. Caja";
                    campoCantidad.IsReadOnly = true;
                    campoCantidad.Cursor = Cursors.Arrow;

                    botonAgregar.IsButtonEnabled = false;
                    botonAgregar.Visibility = Visibility.Hidden;
                    Viewbox? vb = botonAgregar.Parent as Viewbox;
                    vb.Visibility = Visibility.Hidden;
                    elementoDesechable.Visibility = Visibility.Hidden;

                    TextBoxControl campoFechaRegistro = new TextBoxControl();
                    campoFechaRegistro.FieldName = "Fecha de Registro";
                    campoFechaRegistro.Margin = new Thickness(4);
                    campoFechaRegistro.IsReadOnly = true;
                    campoFechaRegistro.Cursor = Cursors.Arrow;

                    Viewbox vbFechaRegistro = new Viewbox();
                    vbFechaRegistro.Child = campoFechaRegistro;

                    elementosDinamicos.Children.Add(vbFechaRegistro);
                    elementosDinamicos.SetCurrentValue(Grid.ColumnSpanProperty, 2);

                    campoSubtotal.IsReadOnly = true;
                    campoSubtotal.Cursor = Cursors.Arrow;
                    campoIVA.IsReadOnly = true;
                    campoIVA.Cursor = Cursors.Arrow;
                    campoTotal.IsReadOnly = true;
                    campoTotal.Cursor = Cursors.Arrow;

                    checkEfectivo.IsEnabled = false;
                    checkTarjeta.IsEnabled = false;
                    checkMonedero.IsEnabled = false;
                    checkRedondear.IsEnabled = false;

                    campoPagoEfectivo.IsReadOnly = true;
                    campoPagoEfectivo.Cursor = Cursors.Arrow;
                    campoPagoEfectivo.Visibility = Visibility.Hidden;

                    campoCambio.IsReadOnly = true;
                    campoCambio.Cursor = Cursors.Arrow;
                    campoCambio.Visibility = Visibility.Hidden;

                    campoPagoTarjeta.IsReadOnly = true;
                    campoPagoTarjeta.Cursor = Cursors.Arrow;
                    campoPagoTarjeta.Visibility = Visibility.Hidden;

                    campoMonedero.IsReadOnly = true;
                    campoMonedero.Cursor = Cursors.Arrow;
                    campoMonedero.Visibility = Visibility.Hidden;

                    campoPagoMonedero.IsReadOnly = true;
                    campoPagoMonedero.Cursor = Cursors.Arrow;
                    campoPagoMonedero.Visibility = Visibility.Hidden;

                    campoMontoPagado.IsReadOnly = true;
                    campoMontoPagado.Cursor = Cursors.Arrow;

                    verificarMonedero.Visibility = Visibility.Hidden;
                    buscarMonedero.Visibility = Visibility.Hidden;

                    campoSaldoRestante.Visibility = Visibility.Hidden;

                    botonAccion.Visibility = Visibility.Hidden;

                    contenedorBotonCancelar.SetCurrentValue(Grid.ColumnProperty, 0);
                    contenedorBotonCancelar.SetCurrentValue(Grid.ColumnSpanProperty, 2);

                    break;

            }

        }

        private void ConfigurarSideBar(int opcion)
        {

            SideBarControl sideBarControl = new SideBarControl(_empleado);
            sideBarControl.Employee = _empleado.tipoEmpleado;

            if (_empleado.tipoEmpleado == "Cajero")
            {

                if (opcion == 1)
                {
                    sideBarControl.SideElementSelected = 1;
                }
                else
                {
                    sideBarControl.SideElementSelected = 2;
                }

            }
            else
            {
                sideBarControl.SideElementSelected = 4;
            }

            MenuLateral.Children.Add(sideBarControl);

        }

        private void buscarMonedero_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (VerificarFormatoMonedero(campoMonedero.Text))
            {

                try
                {

                    if(MonederoDAO.ConfirmarExistencia(campoMonedero.Text))
                    {

                        verificarMonederoImage.ImageSource = (ImageSource) FindResource("Icon_Check");
                        verificarMonedero.BorderBrush = FindResource("SolidColorBrush_DodgerBlue") as SolidColorBrush;
                        campoPagoMonedero.EnableTextBox = true;
                        campoPagoMonedero.Text = "0.00";    
                        campoPagoMonedero.Cursor = Cursors.Arrow;
                        checkRedondear.IsEnabled = true;

                    }
                    else
                    {

                        verificarMonederoImage.ImageSource = (ImageSource) FindResource("Icon_Equis");
                        verificarMonedero.BorderBrush = FindResource("SolidColorBrush_PantoneOrange") as SolidColorBrush;
                        campoPagoMonedero.EnableTextBox = false;
                        campoPagoMonedero.Text = "";
                        checkRedondear.IsChecked = false;
                        checkRedondear.IsEnabled = false;

                    }

                }
                catch (Exception ex)
                {

                    campoPagoMonedero.EnableTextBox= false;
                    campoPagoMonedero.Text = "";
                    checkRedondear.IsChecked = false;
                    checkRedondear.IsEnabled = false;
                    InformationControl.Show("Error", "No se pudo realizar la operación", "Aceptar");

                }

            }
            else
            {

                verificarMonederoImage.ImageSource = (ImageSource)FindResource("Icon_Circulo");
                verificarMonedero.BorderBrush = FindResource("SolidColorBrush_DodgerBlue") as SolidColorBrush;

                campoPagoMonedero.EnableTextBox = false;
                campoPagoMonedero.Text = "";

                checkRedondear.IsChecked = false;
                checkRedondear.IsEnabled = false;

                InformationControl.Show("Advertencia", "El monedero debe tener 12 dígitos", "Aceptar");

            }

        }

        private bool VerificarFormatoMonedero(string monedero)
        {

            bool resultado = false;

            resultado = Regex.IsMatch(monedero, @"^\d{12}$");

            return resultado;

        }

        private void checkTarjeta_CheckedChanged(object sender, RoutedEventArgs e)
        {

            bool isChecked = checkTarjeta.IsChecked;

            if (isChecked)
            {
                campoPagoTarjeta.Visibility = Visibility.Visible;
                campoPagoTarjeta.Text = "0.00";
            }
            else
            {

                campoPagoTarjeta.Visibility = Visibility.Hidden;
                campoPagoTarjeta.Text = "";
                _pagoTarjeta = 0;

            }

        }

        private void checkEfectivo_CheckedChanged(object sender, RoutedEventArgs e)
        {

            bool isChecked = checkEfectivo.IsChecked;

            if (isChecked)
            {

                campoPagoEfectivo.Visibility = Visibility.Visible;
                campoCambio.Visibility = Visibility.Visible;
                campoPagoEfectivo.Text = "0.00";

            }
            else
            {

                campoPagoEfectivo.Visibility = Visibility.Hidden;
                campoPagoEfectivo.Text = "";
                campoCambio.Visibility = Visibility.Hidden;
                campoCambio.Text = "";
                _pagoEfectivo = 0;

            }

        }

        private void checkMonedero_CheckedChanged(object sender, RoutedEventArgs e)
        {

            bool isChecked = checkMonedero.IsChecked;

            if (isChecked)
            {

                campoMonedero.Visibility = Visibility.Visible;
                buscarMonedero.Visibility = Visibility.Visible;
                verificarMonedero.Visibility = Visibility.Visible;
                campoPagoMonedero.Visibility = Visibility.Visible;

            }
            else
            {

                campoMonedero.Visibility = Visibility.Hidden;
                campoMonedero.Text = "";
                buscarMonedero.Visibility = Visibility.Hidden;
                verificarMonedero.Visibility = Visibility.Hidden;
                verificarMonedero.BorderBrush = (SolidColorBrush) FindResource("SolidColorBrush_DodgerBlue");
                verificarMonederoImage.ImageSource = (ImageSource) FindResource("Icon_Circulo");
                campoPagoMonedero.Visibility = Visibility.Hidden;
                campoPagoMonedero.Text = "";
                _pagoMonedero = 0;

                checkRedondear.IsChecked = false;
                checkRedondear.IsEnabled = false;

            }

        }

        private void campoProducto_KeyUp(object sender, KeyEventArgs e)
        {

            switch(_opcionMenu)
            {
                case 1:

                    if (e.Key == Key.Enter)
                    {

                        if (campoProducto.Text != "" && 
                            (campoProducto.Text.Length < 14 || campoProducto.Text.Length > 11) &&
                            campoProducto.Text.All(char.IsDigit))
                        {

                            ProductoInventarioVentaDTO producto = VentaDAO.ObtenerProductoInventarioVenta(campoProducto.Text); 

                            if (producto != null)
                            {
                                
                                _detalleVenta = new DetalleVentaDTO();

                                _detalleVenta.codigo = producto.codigo;
                                _detalleVenta.nombreDetalleVenta = producto.nombre;
                                _detalleVenta.precio = producto.precioActual;
                                _detalleVenta.promocion = producto.promocion;
                                _detalleVenta.porcentajeDescuento = (decimal) producto.porcentajeDescuento / 100.0m;
                                _detalleVenta.cantidadMinima = producto.cantMinima;
                                _detalleVenta.cantidadMaxima = producto.cantMaxima;

                                campoCantidad.EnableTextBox = true;
                                campoCantidad.Cursor = Cursors.IBeam;
                                campoCantidad.Text = "1";
                                _detalleVenta.cantidad = 1;

                            }
                            else
                            {
                                InformationControl.Show("Error", "El producto no fue encontrado en la tienda", "Aceptar");
                            }

                        }

                    }

                    break;

                case 2:

                    break;
                case 3:

                    break;
            }

        }

        private void campoCantidad_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            switch(_opcionMenu)
            {
                case 1:

                    if (campoCantidad.Text != "")
                    {
                        if (int.TryParse(campoCantidad.Text, out int cantidad))
                        {
                            if (cantidad > 0)
                            {

                                _detalleVenta.cantidad = cantidad;

                                int conPromocion = _detalleVenta.cantidad / _detalleVenta.cantidadMinima;
                                int sinPromocion = _detalleVenta.cantidad % _detalleVenta.cantidadMinima;

                                decimal subtotalConPromocion = conPromocion * _detalleVenta.cantidadMinima * _detalleVenta.precio * (1 - _detalleVenta.porcentajeDescuento);
                                decimal subtotalSinPromocion = sinPromocion * _detalleVenta.precio;

                                _detalleVenta.total = Math.Round(subtotalConPromocion + subtotalSinPromocion, 2);

                                botonAgregar.IsButtonEnabled = true;

                            }
                            else
                            {
                                botonAgregar.IsButtonEnabled = false;
                            }

                        }
                        else
                        {
                            campoCantidad.Text = campoCantidad.Text.Substring(0, campoCantidad.Text.Length - 1);
                        }

                    }
                    else
                    {
                        botonAgregar.IsButtonEnabled = false;
                    }

                    break;

                case 2:

                    break;

                case 3:

                    break;

            }

        }

        private void botonAgregar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            
            switch(_opcionMenu)
            {
                case 1:

                    DetalleVentaDTO nuevoDetalleVenta = new DetalleVentaDTO
                    {
                        codigo = _detalleVenta.codigo,
                        nombreDetalleVenta = _detalleVenta.nombreDetalleVenta,
                        precio = _detalleVenta.precio,
                        cantidad = _detalleVenta.cantidad,
                        promocion = _detalleVenta.promocion,
                        porcentajeDescuento = _detalleVenta.porcentajeDescuento,
                        total = _detalleVenta.total,
                        cantidadMinima = _detalleVenta.cantidadMinima,
                        cantidadMaxima = _detalleVenta.cantidadMaxima
                    };

                    bool flag = false;

                    foreach (DetalleVentaDTO detalle in _listaDetalleVentas)
                    {

                        if (detalle.codigo == nuevoDetalleVenta.codigo)
                        {
                            detalle.cantidad += nuevoDetalleVenta.cantidad;

                            int conPromocion = detalle.cantidad / detalle.cantidadMinima;
                            int sinPromocion = detalle.cantidad % detalle.cantidadMinima;

                            decimal subtotalConPromocion = conPromocion * detalle.cantidadMinima * detalle.precio * (1 - detalle.porcentajeDescuento);
                            decimal subtotalSinPromocion = sinPromocion * detalle.precio;

                            detalle.total = Math.Round(subtotalConPromocion + subtotalSinPromocion, 2);

                            flag = true;

                            break;

                        }

                    }

                    if (!flag)
                        _listaDetalleVentas.Add(nuevoDetalleVenta);

                    _detalleVentas.Clear();

                    _detalleVentas = new ObservableCollection<Object>(_listaDetalleVentas);

                    TablaProductos.SetItemsSource(null);
                    TablaProductos.SetItemsSource(_detalleVentas);

                    campoSubtotal.Text = _listaDetalleVentas.Sum(d => d.total).ToString("0.00");

                    campoIVA.Text = (_listaDetalleVentas.Sum(d => d.total) * (decimal) 0.16).ToString("0.00");

                    campoTotal.Text = decimal.Parse(campoSubtotal.Text) + decimal.Parse(campoIVA.Text) + "";

                    campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";

                    break;

                case 2:

                    break;

                case 3:

                    break;

            }

        }

        private void campoSaldoRestante_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (decimal.Parse(campoMontoPagado.Text) == decimal.Parse(campoTotal.Text))
            {
                botonAccion.IsButtonEnabled = true;
                campoCambio.Text = "0.00";
            }
            else
            {
                
                botonAccion.IsButtonEnabled = false;

                decimal cambioTruncado = Math.Floor(decimal.Parse(campoMontoPagado.Text) - decimal.Parse(campoTotal.Text));
                decimal cambioDecimal = decimal.Parse(campoMontoPagado.Text) - decimal.Parse(campoTotal.Text);

                if ((cambioDecimal - cambioTruncado) < 0.4m)
                {
                    campoCambio.Text = Math.Floor(decimal.Parse(campoMontoPagado.Text) - decimal.Parse(campoTotal.Text)).ToString("0.00");
                }
                else if ((cambioDecimal - cambioTruncado) > 0.6m)
                {
                    campoCambio.Text = Math.Ceiling(decimal.Parse(campoMontoPagado.Text) - decimal.Parse(campoTotal.Text)).ToString("0.00");
                }
                else
                {
                    campoCambio.Text = cambioTruncado + 0.5m + "";
                }

                if (decimal.Parse(campoSaldoRestante.Text) < 0 && checkEfectivo.IsChecked)
                {
                    campoSaldoRestante.Text = "0.00";
                }

            }

        }

        private void campoPagoEfectivo_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (campoPagoEfectivo.Text != "")
            {

                if (decimal.TryParse(campoPagoEfectivo.Text, out decimal pago))
                {

                    _pagoEfectivo = pago;

                    campoMontoPagado.Text = _pagoEfectivo + _pagoTarjeta + _pagoMonedero + "";
                    
                }
                else
                {
                    campoPagoEfectivo.Text = campoPagoEfectivo.Text.Substring(0, campoPagoEfectivo.Text.Length - 1);
                }

                campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";

            }
            else
            {

                _pagoEfectivo = 0;
                campoMontoPagado.Text = _pagoEfectivo + _pagoTarjeta + _pagoMonedero + "";
                campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";

            }

        }

        private void campoPagoTarjeta_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (campoPagoTarjeta.Text != "")
            {

                if (decimal.TryParse(campoPagoTarjeta.Text, out decimal pago))
                {
                    
                    _pagoTarjeta = pago;

                    campoMontoPagado.Text = _pagoEfectivo + _pagoTarjeta + _pagoMonedero + "";

                }
                else
                {
                    campoPagoTarjeta.Text = campoPagoTarjeta.Text.Substring(0, campoPagoTarjeta.Text.Length - 1);
                }

                campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";

            }
            else
            {

                _pagoTarjeta = 0;
                campoMontoPagado.Text = _pagoEfectivo + _pagoTarjeta + _pagoMonedero + "";
                campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";

            }

        }

        private void campoPagoMonedero_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (campoPagoMonedero.Text != "")
            {

                if (decimal.TryParse(campoPagoMonedero.Text, out decimal pago))
                {

                    _pagoMonedero = pago;

                    campoMontoPagado.Text = _pagoEfectivo + _pagoTarjeta + _pagoMonedero + "";
                    

                }
                else
                {
                    campoPagoMonedero.Text = campoPagoMonedero.Text.Substring(0, campoPagoMonedero.Text.Length - 1);
                }

                campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";

            }
            else
            {

                _pagoMonedero = 0;
                campoMontoPagado.Text = _pagoEfectivo + _pagoTarjeta + _pagoMonedero + "";
                campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";

            }

        }

        private void campoMontoPagado_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";
        }

        private void campoCambio_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (decimal.Parse(campoCambio.Text) < 0)
            {
                campoCambio.Text = "0.00";
            }

        }

    }

}
