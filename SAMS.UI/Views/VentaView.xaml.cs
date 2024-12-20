﻿using SAMS.UI.DAO;
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
using SAMS.UI.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using static ICSharpCode.SharpZipLib.Zip.ExtendedUnixData;

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
        TextBoxControl campoFechaRegistro;
        VentasDTO _venta;

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

        public VentaView(EmpleadoLoginDTO empleado, int opcion, VentasDTO venta)
        {

            _empleado = empleado;
            _opcionMenu = opcion;
            _listaDetalleVentas = new List<DetalleVentaDTO>();
            _detalleVentas = new ObservableCollection<Object>();
            _pagoEfectivo = 0;
            _pagoTarjeta = 0;
            _pagoMonedero = 0;
            _venta = venta;

            InitializeComponent();

            ConfigurarSideBar(opcion);

            ConfigurarMenu(opcion);

            DefinirColumnas();

            try
            {

                AgregarDetallesVenta(ObtenerDetallesVenta(venta));

                AgregarInformacionVenta(venta);

            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", ex.Message, "Aceptar");
            }

            

        }

        private List<DetalleVentaDTO> ObtenerDetallesVenta(VentasDTO venta)
        {
            List<DetalleVentaDTO> detalles = VentaDAO.ObtenerDetallesVenta(venta.noVenta);
            return detalles;
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

                    campoFechaRegistro = new TextBoxControl();
                    campoFechaRegistro.FieldName = "Fecha de Registro";
                    campoFechaRegistro.Margin = new Thickness(4);
                    campoFechaRegistro.TextFontSize = 16;
                    campoFechaRegistro.IsReadOnly = true;
                    campoFechaRegistro.Cursor = Cursors.Arrow;

                    campoProducto.TextFontSize = 14;

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
                    InformationControl.Show("Error", ex.Message, "Aceptar");

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

            campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";

        }

        private void checkMonedero_CheckedChanged(object sender, RoutedEventArgs e)
        {

            bool isChecked = checkMonedero.IsChecked;

            if (isChecked)
            {

                campoMonedero.Visibility = Visibility.Visible;

                if (_opcionMenu != 3)
                {

                    buscarMonedero.Visibility = Visibility.Visible;
                    verificarMonedero.Visibility = Visibility.Visible;

                }

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

                case 2:

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

                                if (_detalleVenta.promocion != null)
                                {

                                    _detalleVenta.porcentajeDescuento = (decimal)producto.porcentajeDescuento / 100.0m;
                                    _detalleVenta.cantidadMinima = producto.cantidadMinima;
                                    _detalleVenta.cantidadMaxima = producto.cantidadMaxima;

                                }

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

            }

        }

        private void campoCantidad_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            switch(_opcionMenu)
            {
                case 1:

                case 2:

                    if (campoCantidad.Text != "")
                    {
                        if (int.TryParse(campoCantidad.Text, out int cantidad))
                        {
                            if (cantidad > 0)
                            {
                                decimal subtotalSinPromocion = 0;
                                decimal subtotalConPromocion = 0;
                                int sinPromocion = cantidad;

                                _detalleVenta.cantidad = cantidad;

                                if (_detalleVenta.promocion != null)
                                {

                                    int conPromocion = cantidad / (int)_detalleVenta.cantidadMinima;
                                    sinPromocion = cantidad % (int)_detalleVenta.cantidadMinima;

                                    subtotalConPromocion = conPromocion * (int)_detalleVenta.cantidadMinima * _detalleVenta.precio * (1 - _detalleVenta.porcentajeDescuento);

                                }

                                subtotalSinPromocion = sinPromocion * _detalleVenta.precio;

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

                case 3:

                    break;

            }

        }

        private void botonAgregar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            
            switch(_opcionMenu)
            {
                case 1:

                    AgregarUnDetalleVenta();

                    break;

                case 2:

                    CambiarUnDetalleVenta();

                    break;

            }

        }

        private void botonBorrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            if (_opcionMenu == 2)
            {
                BorrarUnDetalleVenta();
            }

        }

        private void TablaProductos_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            
            TableControl tabla = sender as TableControl;

            DetalleVentaDTO venta = tabla.SelectedItem as DetalleVentaDTO;

            if (venta != null)
            {

                botonBorrar.IsButtonEnabled = true;

                campoProducto.Text = venta.nombreDetalleVenta;
                _detalleVenta.codigo = venta.codigo;
                _detalleVenta.nombreDetalleVenta = venta.nombreDetalleVenta;
                _detalleVenta.precio = venta.precio;
                _detalleVenta.promocion = venta.promocion;
                _detalleVenta.porcentajeDescuento = venta.porcentajeDescuento;
                _detalleVenta.cantidad = venta.cantidad;

                if (_detalleVenta.promocion != null)
                {
                    _detalleVenta.cantidadMinima = venta.cantidadMinima;
                    _detalleVenta.cantidadMaxima = venta.cantidadMaxima;
                }

                campoCantidad.EnableTextBox = true;
                campoCantidad.Text = "";

            }

        }

        private void BorrarUnDetalleVenta()
        {
            
            if (_detalleVentas.Count() > 1)
            {

                var detalleVentaExistente = _detalleVentas.OfType<DetalleVentaDTO>().FirstOrDefault(d => d.codigo == _detalleVenta.codigo);

                if (detalleVentaExistente != null)
                {

                    _listaDetalleVentas.Remove(detalleVentaExistente);

                    _detalleVentas.Clear();

                    _detalleVentas = new ObservableCollection<Object>(_listaDetalleVentas);

                    TablaProductos.SetItemsSource(null);
                    TablaProductos.SetItemsSource(_detalleVentas);

                    campoSubtotal.Text = _listaDetalleVentas.Sum(d => d.total).ToString("0.00");
                    campoIVA.Text = (_listaDetalleVentas.Sum(d => d.total) * (decimal)0.16).ToString("0.00");
                    campoTotal.Text = decimal.Parse(campoSubtotal.Text) + decimal.Parse(campoIVA.Text) + "";

                    if (_opcionMenu != 3)
                    {
                        campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";
                    }

                    decimal auxiliarDeEvento = campoPagoEfectivo.Text != "" ? decimal.Parse(campoPagoEfectivo.Text) : 0;
                    campoPagoEfectivo.Text = 0.00 + "";
                    campoPagoEfectivo.Text = auxiliarDeEvento.ToString("0.00");

                }

            }
            else
            {
                InformationControl.Show("Advertencia", "No se pueden borrar todos los productos de la venta", "Aceptar");
            }

        }

        private void AgregarInformacionVenta(VentasDTO venta)
        {
            VentaDTO ventaSolida = null;


            try
            {
                ventaSolida = VentaDAO.ObtenerVenta(venta.noVenta);
            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", "No se pudo obtener la información de la venta", "Aceptar");
                Debug.WriteLine(ex.Message);
            }

            if (ventaSolida != null)
            {
                if (_opcionMenu == 3)
                {

                    campoProducto.Text = ventaSolida.nombreEmpleado;
                    campoCantidad.Text = ventaSolida.noCaja;
                    campoFechaRegistro.Text = ventaSolida.fechaRegistro.ToString("dd/MM/yyyy HH:mm:ss");

                }

                if (ventaSolida.totalEfectivo > 0)
                {

                    checkEfectivo.IsChecked = true;
                    campoPagoEfectivo.Text = ventaSolida.totalEfectivo.ToString("0.00");

                }

                if (ventaSolida.totalTarjeta > 0)
                {

                    checkTarjeta.IsChecked = true;
                    campoPagoTarjeta.Text = ventaSolida.totalTarjeta.ToString("0.00");

                }

                if (ventaSolida.codigoMonedero != null)
                {

                    checkMonedero.IsChecked = true;
                    campoMonedero.Text = ventaSolida.codigoMonedero;
                    campoPagoMonedero.Text = ventaSolida.totalMonedero.ToString("0.00");

                    if (ventaSolida.tieneRedondeo)
                    {
                        checkRedondear.IsChecked = true;
                    }

                }

            }

        }

        private void AgregarDetallesVenta(List<DetalleVentaDTO> detalles)
        {

            foreach (DetalleVentaDTO detalle in detalles)
            {

                DefinirDetalleVenta(detalle);
                AgregarUnDetalleVenta();

            }

        }

        private void DefinirDetalleVenta(DetalleVentaDTO detalle)
        {
            
            _detalleVenta = new DetalleVentaDTO();

            _detalleVenta.codigo = detalle.codigo;
            _detalleVenta.nombreDetalleVenta = detalle.nombreDetalleVenta;
            _detalleVenta.precio = detalle.precio;
            _detalleVenta.promocion = detalle.promocion;
            _detalleVenta.porcentajeDescuento = detalle.porcentajeDescuento;
            _detalleVenta.cantidadMinima = detalle.cantidadMinima;
            _detalleVenta.cantidadMaxima = detalle.cantidadMaxima;
            _detalleVenta.cantidad = detalle.cantidad;
            _detalleVenta.total = detalle.total;

        }

        private void CambiarUnDetalleVenta()
        {

            DetalleVentaDTO nuevoDetalleVenta = new DetalleVentaDTO();

            if (_detalleVenta != null)
            {

                nuevoDetalleVenta.codigo = _detalleVenta.codigo;
                nuevoDetalleVenta.nombreDetalleVenta = _detalleVenta.nombreDetalleVenta;
                nuevoDetalleVenta.precio = _detalleVenta.precio;
                nuevoDetalleVenta.promocion = _detalleVenta.promocion;

                if (_detalleVenta.promocion != null)
                {
                    nuevoDetalleVenta.porcentajeDescuento = _detalleVenta.porcentajeDescuento;
                    nuevoDetalleVenta.cantidadMinima = _detalleVenta.cantidadMinima;
                    nuevoDetalleVenta.cantidadMaxima = _detalleVenta.cantidadMaxima;
                }

                nuevoDetalleVenta.cantidad = _detalleVenta.cantidad;
                nuevoDetalleVenta.total = _detalleVenta.total;

                foreach (DetalleVentaDTO detalle in _listaDetalleVentas)
                {

                    if (detalle.codigo == nuevoDetalleVenta.codigo)
                    {
                        detalle.cantidad = nuevoDetalleVenta.cantidad;

                        decimal subtotalSinPromocion = 0;
                        decimal subtotalConPromocion = 0;
                        int sinPromocion = detalle.cantidad;

                        if (detalle.promocion != null)
                        {

                            int conPromocion = detalle.cantidad / (int)detalle.cantidadMinima;
                            sinPromocion = detalle.cantidad % (int)detalle.cantidadMinima;

                            subtotalConPromocion = conPromocion * (int)detalle.cantidadMinima * detalle.precio * (1 - detalle.porcentajeDescuento);

                        }

                        subtotalSinPromocion = sinPromocion * detalle.precio;

                        detalle.total = Math.Round(subtotalConPromocion + subtotalSinPromocion, 2);

                        break;

                    }

                }

                _detalleVentas.Clear();

                _detalleVentas = new ObservableCollection<Object>(_listaDetalleVentas);

                TablaProductos.SetItemsSource(null);
                TablaProductos.SetItemsSource(_detalleVentas);

                campoSubtotal.Text = _listaDetalleVentas.Sum(d => d.total).ToString("0.00");

                campoIVA.Text = (_listaDetalleVentas.Sum(d => d.total) * (decimal)0.16).ToString("0.00");

                campoTotal.Text = decimal.Parse(campoSubtotal.Text) + decimal.Parse(campoIVA.Text) + "";

            }

        }

        private void AgregarUnDetalleVenta()
        {
            DetalleVentaDTO nuevoDetalleVenta = new DetalleVentaDTO();
            
            if (_detalleVenta != null)
            {

                nuevoDetalleVenta.codigo = _detalleVenta.codigo;
                nuevoDetalleVenta.nombreDetalleVenta = _detalleVenta.nombreDetalleVenta;
                nuevoDetalleVenta.precio = _detalleVenta.precio;
                nuevoDetalleVenta.promocion = _detalleVenta.promocion;

                if (_detalleVenta.promocion != null)
                {
                    nuevoDetalleVenta.porcentajeDescuento = _detalleVenta.porcentajeDescuento;
                    nuevoDetalleVenta.cantidadMinima = _detalleVenta.cantidadMinima;
                    nuevoDetalleVenta.cantidadMaxima = _detalleVenta.cantidadMaxima;
                }

                nuevoDetalleVenta.cantidad = _detalleVenta.cantidad;
                nuevoDetalleVenta.total = _detalleVenta.total;

            }

            bool flag = false;

            foreach (DetalleVentaDTO detalle in _listaDetalleVentas)
            {

                if (detalle.codigo == nuevoDetalleVenta.codigo)
                {
                    detalle.cantidad += nuevoDetalleVenta.cantidad;

                    decimal subtotalSinPromocion = 0;
                    decimal subtotalConPromocion = 0;
                    int sinPromocion = detalle.cantidad;

                    if (detalle.promocion != null)
                    {

                        int conPromocion = detalle.cantidad / (int) detalle.cantidadMinima;
                        sinPromocion = detalle.cantidad % (int) detalle.cantidadMinima;

                        subtotalConPromocion = conPromocion * (int) detalle.cantidadMinima * detalle.precio * (1 - detalle.porcentajeDescuento);

                    }

                    subtotalSinPromocion = sinPromocion * detalle.precio;

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

            campoIVA.Text = (_listaDetalleVentas.Sum(d => d.total) * (decimal)0.16).ToString("0.00");

            campoTotal.Text = decimal.Parse(campoSubtotal.Text) + decimal.Parse(campoIVA.Text) + "";

            if (_opcionMenu == 1)
            {
                campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";
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

        private void campoSaldoRestante_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (decimal.Parse(campoMontoPagado.Text) == decimal.Parse(campoTotal.Text) && decimal.Parse(campoTotal.Text) != 0)
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

            }

            if (decimal.Parse(campoSaldoRestante.Text) < 0 && checkEfectivo.IsChecked)
            {
                campoSaldoRestante.Text = "0.00";
            }

            if (decimal.Parse(campoSaldoRestante.Text) == 0 && _detalleVentas.Count() > 0)
            {
                botonAccion.IsButtonEnabled = true;
            }

        }

        private void campoMontoPagado_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            campoSaldoRestante.Text = decimal.Parse(campoTotal.Text) - decimal.Parse(campoMontoPagado.Text) + "";
        }

        private void campoCambio_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (decimal.TryParse(campoCambio.Text, out decimal result))
            {

                if (result < 0)
                {
                    campoCambio.Text = "0.00";
                }

            }
            else
            {
                campoCambio.Text = "0.00";
            }

            if(_detalleVentas.Count() < 1)
            {
                if (checkEfectivo.IsChecked)
                {
                    campoPagoEfectivo.Text = "0.00";
                }

                if (checkTarjeta.IsChecked)
                {
                    campoPagoTarjeta.Text = "0.00";
                }

                if (checkMonedero.IsChecked && !campoPagoMonedero.Text.IsNullOrEmpty())
                {
                    campoPagoMonedero.Text = "0.00";
                }

                botonAccion.IsButtonEnabled = false;

            }

        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            bool confirmacion = true;

            if (_listaDetalleVentas.Count > 0 && _opcionMenu == 1)
            {
                confirmacion = ConfirmationControl.Show("Confirmación", "¿Estás seguro de abandonar el registro?, No se guardarán los productos agregados", "Aceptar", "Cancelar");
            }

            if (confirmacion)
            {

                VerVentasView ventasView = new VerVentasView(_empleado);

                ventasView.Show();
                this.Close();

            }

        }

        private async void botonAccion_ButtonControlClick(object sender, RoutedEventArgs e)
        {

            switch (_opcionMenu)
            {
                case 1:

                    try
                    {

                        if (decimal.Parse(campoMontoPagado.Text) >= decimal.Parse(campoTotal.Text))
                        {

                            string? codigoMonedero = (campoMonedero.Text != "" && (decimal.Parse(campoPagoMonedero.Text) > 0 || checkRedondear.IsChecked)) ? campoMonedero.Text : null;

                            await VentaDAO.RegistrarVenta(
                                _pagoEfectivo,
                                _pagoTarjeta,
                                _pagoMonedero,
                                decimal.Parse(campoIVA.Text),
                                codigoMonedero,
                                _empleado.numeroEmpleado,
                                _NoCaja,
                                checkRedondear.IsChecked,
                                _listaDetalleVentas
                            );

                            InformationControl.Show("Éxito", "La venta se ha registrado exitosamente", "Aceptar");

                            PrincipalView principalView = new PrincipalView(_empleado);

                            principalView.Show();

                            this.Close();

                        }
                        else
                        {

                            InformationControl.Show("Advertencia", "El monto pagado no coincide con el total de la venta", "Aceptar");

                        }

                    }
                    catch (Exception ex)
                    {
                        InformationControl.Show("Error", "No se pudo realizar la operación:\n" + ex.Message, "Aceptar");
                        Debug.WriteLine(ex.Message);
                    }

                    break;

                case 2:

                    try
                    {

                        if (decimal.Parse(campoMontoPagado.Text) >= decimal.Parse(campoTotal.Text))
                        {

                            string? codigoMonedero = (campoMonedero.Text != "" && (decimal.Parse(campoPagoMonedero.Text) > 0 || checkRedondear.IsChecked)) ? campoMonedero.Text : null;

                            await VentaDAO.ActualizarVenta(
                                _venta.noVenta,
                                _pagoEfectivo,
                                _pagoTarjeta,
                                _pagoMonedero,
                                decimal.Parse(campoIVA.Text),
                                codigoMonedero,
                                checkRedondear.IsChecked,
                                _listaDetalleVentas
                            );

                            InformationControl.Show("Éxito", "La venta se ha actualizado exitosamente", "Aceptar");

                            PrincipalView principalView = new PrincipalView(_empleado);

                            principalView.Show();

                            this.Close();

                        }

                    }
                    catch (Exception ex)
                    {

                        InformationControl.Show("Error", "No se pudo realizar la operación:\n" + ex.Message, "Aceptar");
                        Debug.WriteLine(ex.Message);

                    }

                    break;

            }

        }

    }

}
