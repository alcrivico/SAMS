using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Utils;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views;

/// <summary>
/// Lógica de interacción para ReportePedidosView.xaml
/// </summary>
public partial class ReportePedidosView : Window
{
    List<ReportePedidoDTO> listaReportePedido;
    ObservableCollection<Object> _reportes;
    private EmpleadoLoginDTO empleado;
    private SideBarControl SideBarControl_MenuLateral;
    private ObservableCollection<Object> _rangoFechas;
    private const int MAXIMO_DIAS = -7;
    private RangoFechasDTO rango;

    public ReportePedidosView(EmpleadoLoginDTO empleado)
    {
        _reportes = new ObservableCollection<Object>();
        this.empleado = empleado;
        _rangoFechas = new ObservableCollection<Object>();
        InitializeComponent();

        SideBarControl_MenuLateral = new SideBarControl(empleado);
        MenuLateral.Children.Add(SideBarControl_MenuLateral);
        SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;
        DefinirColumnas();
        DefinirFechas();
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
                    { "Name", "noPedido" },
                    { "Width", "*" },
                    { "BindingName", "noPedido" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Fecha del pedido" },
                    { "Width", "*" },
                    { "BindingName", "FechaPedidoFormateada" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Fecha de entrega" },
                    { "Width", "*" },
                    { "BindingName", "FechaEntregaFormateada" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Proveedor" },
                    { "Width", "*" },
                    { "BindingName", "proveedor" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Costo" },
                    { "Width", "*" },
                    { "BindingName", "costoTotalPedido" }

                }
            };

        TablaReporte.DefineColumns(columnas);

    }

    private void ObtenerPedidos()
    {
        try
        {
            rango = (RangoFechasDTO)ComboboxFechas.SelectedItem;
            listaReportePedido = ReportesDAO.ReportePedidos(rango.FechaInicio,rango.FechaFin);
            _reportes.Clear();
            _reportes = new ObservableCollection<Object>(listaReportePedido);
            TablaReporte.SetItemsSource(_reportes);
        }
        catch
        {
            InformationControl.Show("Error", "Ocurrió un error al obtener Pedidos", "Aceptar");

            Close();
        }
    }

    private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
    {
        if (listaReportePedido != null)
        {
            var reporteFiltado = listaReportePedido.Where(
            x => x.proveedor.ToLower().Contains(campoBuscar.Text.ToLower()) ||
                 x.noPedido.ToString().Contains(campoBuscar.Text))
            .ToList();

            _reportes = new ObservableCollection<Object>(reporteFiltado);

            TablaReporte.SetItemsSource(_reportes);
        }
        else
        {
            _reportes.Clear();

            _reportes = new ObservableCollection<Object>(listaReportePedido);

            TablaReporte.SetItemsSource(_reportes);
        }
    }

    private void Imprimir_ButtonControlClick(object sender, RoutedEventArgs e)
    {
        if (TablaReporte != null)
        {
            ReportePDFGenerator generator = new(listaReportePedido, rango.Rango);
        }
    }

    private void comboboxFechas_SelectedItemChanged(object sender, RoutedEventArgs e)
    {
        ObtenerPedidos();
    }

    private void DefinirFechas()
    {
        DateTime fechaLimite = new DateTime(2024, 8, 1);
        DateTime fechaFin = DateTime.Today;
        List<RangoFechasDTO> rangos = new();

        while (true)
        {
            DateTime fechaInicio = fechaFin.AddDays(MAXIMO_DIAS);

            // Si la fecha de inicio es menor al 1 de agosto, ajustarla y salir del bucle
            if (fechaInicio < fechaLimite)
            {
                fechaInicio = fechaLimite;
                rangos.Add(new RangoFechasDTO { FechaInicio = fechaInicio, FechaFin = fechaFin });
                break;
            }

            rangos.Add(new RangoFechasDTO { FechaInicio = fechaInicio, FechaFin = fechaFin });
            fechaFin = fechaInicio.AddDays(-1);
        }

        _rangoFechas.Clear();
        foreach (var rango in rangos)
            _rangoFechas.Add(rango);

        ComboboxFechas.SetItemsSource(_rangoFechas, "Rango");

        if (_rangoFechas.Count > 0)
            ComboboxFechas.SelectDefaultItem(0);
    }
}
