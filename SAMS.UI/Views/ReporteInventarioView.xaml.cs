using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views;

/// <summary>
/// Lógica de interacción para ReporteInventarioView.xaml
/// </summary>
public partial class ReporteInventarioView : Window
{
    List<ReporteProductoInventarioDTO> listaReporteInventario;
    ObservableCollection<Object> _reportes;
    private EmpleadoLoginDTO empleado;
    private SideBarControl SideBarControl_MenuLateral;
    public ReporteInventarioView(EmpleadoLoginDTO empleado)
    {

        _reportes = new ObservableCollection<Object>();
        this.empleado = empleado; 
        InitializeComponent();

        SideBarControl_MenuLateral = new SideBarControl(empleado);
        MenuLateral.Children.Add(SideBarControl_MenuLateral);
        SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;
        
        DefinirColumnas();
        ObtenerInventario();
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
                    { "BindingName", "nombre" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Bodega" },
                    { "Width", "*" },
                    { "BindingName", "cantidadBodega" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Exhibicion" },
                    { "Width", "*" },
                    { "BindingName", "cantidadExhibicion" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Precio" },
                    { "Width", "*" },
                    { "BindingName", "precioActual" }

                }
            };

        TablaReporte.DefineColumns(columnas);

    }

    private void ObtenerInventario()
    {
        try
        {
            listaReporteInventario = ReportesDAO.ReporteInventario();
            _reportes.Clear();
            _reportes = new ObservableCollection<Object>(listaReporteInventario);
            TablaReporte.SetItemsSource(_reportes);
        }
        catch (Exception ex)
        {
            InformationControl.Show("Error", "Ocurrió un error al obtener Inventario de Producto", "Aceptar");

            Close();
        }
    }

    private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
    {
        if (listaReporteInventario != null)
        {
            var reporteFiltado = listaReporteInventario.Where(
                x => x.nombre.ToLower().Contains(campoBuscar.Text.ToLower())).ToList();

            _reportes.Clear();

            _reportes = new ObservableCollection<Object>(reporteFiltado);

            TablaReporte.SetItemsSource(_reportes);
        }
        else
        {
            _reportes.Clear();

            _reportes = new ObservableCollection<Object>(listaReporteInventario);

            TablaReporte.SetItemsSource(_reportes);
        }
    }

    private void Imprimir_ButtonControlClick(object sender, RoutedEventArgs e)
    {

    }
}