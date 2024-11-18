using SAMS.UI.DAO;
using SAMS.UI.DTO;
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
    public ReporteVentasView()
    {
        _reportes = new ObservableCollection<Object>();
        InitializeComponent();
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
            listaReporteVenta = ReportesDAO.ReporteVentas();
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

    }
}
