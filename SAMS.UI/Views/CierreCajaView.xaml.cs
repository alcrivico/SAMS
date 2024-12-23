using DotNetEnv;
using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for CierreCajaView.xaml
    /// </summary>
    public partial class CierreCajaView : Window
    {
        EmpleadoLoginDTO _empleado;
        decimal _conteoEfectivo;
        List<VentasCierreCajaDTO> _ventasCierreCaja;
        ObservableCollection<Object> _ventas;
        decimal _totalEfectivo;
        decimal _totalTarjeta;
        decimal _totalMonedero;

        public CierreCajaView(EmpleadoLoginDTO empleado)
        {

            _empleado = empleado;
            _ventas = new ObservableCollection<Object>();
            _conteoEfectivo = 0;

            InitializeComponent();

            ConfigurarSideBar();

            DefinirColumnas();

            ObtenerVentasCierreCaja();

            ObtenerTotales();

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

        private void ConfigurarSideBar()
        {

            SideBarControl sideBarControl = new SideBarControl(_empleado);
            sideBarControl.Employee = _empleado.tipoEmpleado;
            sideBarControl.SideElementSelected = 4;

            MenuLateral.Children.Add(sideBarControl);

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
                    { "BindingName", "totalVenta" },

                }

            };

            tablaVentas.DefineColumns(columnas);

        }

        private void ObtenerVentasCierreCaja()
        {

            try
            {
                var envPath = System.IO.Path.Combine(AppContext.BaseDirectory, "../../../.env");

                if (!File.Exists(envPath))
                {
                    throw new Exception("No se encontró el archivo .env");
                }

                Env.Load(envPath);

                String noCaja = Env.GetString("NO_CAJA");

                _ventasCierreCaja = VentaDAO.ObtenerVentasCierreCaja(noCaja);

                _ventas.Clear();

                _ventas = new ObservableCollection<Object>(_ventasCierreCaja);

                tablaVentas.SetItemsSource(_ventas);

            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", ex.Message, "Aceptar");
            }

        }

        private void ObtenerTotales()
        {

            _totalEfectivo = 0;
            _totalTarjeta = 0;
            _totalMonedero = 0;

            foreach (VentasCierreCajaDTO venta in _ventasCierreCaja)
            {

                _totalEfectivo += venta.totalEfectivo;
                _totalTarjeta += venta.totalTarjeta;
                _totalMonedero += venta.totalMonedero;

            }

            campoTotalEfectivo.Text = _totalEfectivo.ToString();
            campoTotalTarjeta.Text = _totalTarjeta.ToString();
            campoTotalMonedero.Text = _totalMonedero.ToString();

        }

        private void botonAccion_ButtonControlClick(object sender, RoutedEventArgs e)
        {

            bool decision = ConfirmationControl.Show(
                "Confirmar",
                "¿Estás seguro de cerrar el turno de la caja?, no podrá reabrirse",
                "Aceptar",
                "Cancelar"
            );

            if (decision)
            {
                try
                {
                    var envPath = System.IO.Path.Combine(AppContext.BaseDirectory, "../../../.env");

                    if (!File.Exists(envPath))
                    {
                        throw new Exception("No se encontró el archivo .env");
                    }

                    Env.Load(envPath);

                    String noCaja = Env.GetString("NO_CAJA");

                    int noVentas = _ventasCierreCaja.Count;

                    decimal totalVentas = _totalEfectivo + _totalTarjeta + _totalMonedero;

                    decimal diferencia = _conteoEfectivo - _totalEfectivo;

                    String responsableCaja = _ventasCierreCaja[0].nombreEmpleado;

                    InformationControl.Show("Cierre Realizado", "El cierre de caja ha sido exitoso", "Aceptar");


                    InformationControl.Show("Información de Cierre", $"Numero de Ventas: {noVentas}\nDiferencia: {diferencia}", "Aceptar");

                    PrincipalView principalView = new PrincipalView(_empleado);

                    principalView.Show();
                    this.Close();

                }
                catch (Exception ex)
                {
                    InformationControl.Show("Error", ex.Message, "Aceptar");
                }           

            }

        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {

            PrincipalView principalView = new PrincipalView(_empleado);

            principalView.Show();
            this.Close();

        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if(_ventasCierreCaja != null)
            {
                if (campoBuscar.Text.Length > 0)
                {

                    var ventasFiltradas = _ventasCierreCaja.Where(
                        v =>
                        v.noVenta.ToString().Contains(campoBuscar.Text) ||
                        v.totalVenta.ToString().Contains(campoBuscar.Text)).ToList();

                    _ventas.Clear();

                    _ventas = new ObservableCollection<Object>(ventasFiltradas);

                    tablaVentas.SetItemsSource(_ventas);

                }
                else
                {

                    _ventas.Clear();

                    _ventas = new ObservableCollection<Object>(_ventasCierreCaja);

                    tablaVentas.SetItemsSource(_ventas);

                }

            }

        }

        private void campoConteoEfectivo_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (campoConteoEfectivo.Text != "")
            {

                if (decimal.TryParse(campoConteoEfectivo.Text, out decimal conteoEfectivo))
                {

                    botonAccion.IsButtonEnabled = true;
                    _conteoEfectivo = conteoEfectivo;

                }
                else
                {
                    campoConteoEfectivo.Text = campoConteoEfectivo.Text.Substring(0, campoConteoEfectivo.Text.Length - 1);
                }

            }
            else
            {
                botonAccion.IsButtonEnabled = false;
            }

        }

    }

}
