using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Utils;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views;

/// <summary>
/// Lógica de interacción para ReporteVentasView.xaml
/// </summary>
public partial class ReporteVentasView : Window
{
    List<ReporteVentaDTO> listaReporteVenta;
    ObservableCollection<Object> _reportes;
    private EmpleadoLoginDTO empleado;
    private SideBarControl SideBarControl_MenuLateral;

    public ReporteVentasView(EmpleadoLoginDTO empleado)
    {
        _reportes = new ObservableCollection<Object>();
        this.empleado = empleado;
        InitializeComponent();

        SideBarControl_MenuLateral = new SideBarControl(empleado);
        MenuLateral.Children.Add(SideBarControl_MenuLateral);
        SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;

        campoFecha.SelectedDate = DateTime.Now;

        DefinirColumnas();
        ObtenerReporte();
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
                    { "Name", "NoVenta" },
                    { "Width", "*" },
                    { "BindingName", "noVenta" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Fecha" },
                    { "Width", "*" },
                    { "BindingName", "FechaRegistroFormateada" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Total" },
                    { "Width", "*" },
                    { "BindingName", "total" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Caja" },
                    { "Width", "*" },
                    { "BindingName", "noCaja" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Cajero" },
                    { "Width", "*" },
                    { "BindingName", "nombre" }

                }
            };

        TablaReporte.DefineColumns(columnas);

    }

    private void ObtenerReporte()
    {
        try
        {

            DateTime fecha = campoFecha.SelectedDate.Value;

            listaReporteVenta = ReportesDAO.ReporteVentas(fecha);
            _reportes.Clear();
            _reportes = new ObservableCollection<Object>(listaReporteVenta);
            TablaReporte.SetItemsSource(_reportes);
        }
        catch
        {
            InformationControl.Show("Error", "Ocurrió un error al obtener las ventas", "Aceptar");

            Close();
        }
    }

    private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
    {
        if (listaReporteVenta != null)
        {
            var reporteFiltado = listaReporteVenta.Where(
                x =>
                x.noCaja.ToLower().Contains(campoBuscar.Text.ToLower()) ||
                x.nombre.ToLower().Contains(campoBuscar.Text.ToLower()) ||
                x.noVenta.ToString().Contains(campoBuscar.Text))
                .ToList();

            _reportes = new ObservableCollection<Object>(reporteFiltado);

            TablaReporte.SetItemsSource(_reportes);
        }
        else
        {
            _reportes.Clear();

            _reportes = new ObservableCollection<Object>(listaReporteVenta);

            TablaReporte.SetItemsSource(_reportes);
        }
    }

    private void Imprimir_ButtonControlClick(object sender, RoutedEventArgs e)
    {
        if(TablaReporte != null)
        {
            ReportePDFGenerator generator = 
                new(listaReporteVenta, campoFecha.SelectedDate.Value.ToString("dd/MM/yyyy"));
        }
        else
            InformationControl.Show("Error", "No hay datos para imprimir", "Aceptar");

    }

    private void campoFecha_SelectedDateChanged(object sender, RoutedEventArgs e) 
        => ObtenerReporte();
}
