using SAMS.UI.DAO;
using SAMS.UI.DTO;
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
    public ReportePedidosView()
    {
        _reportes = new ObservableCollection<Object>();
        InitializeComponent();
        DefinirColumnas();
        ObtenerPedidos();
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
                    { "BindingName", "fechaPedido" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Fecha de entrega" },
                    { "Width", "*" },
                    { "BindingName", "fechaEntrega" }

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
            listaReportePedido =ReportesDAO.ReportePedidos();
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

    }
}
