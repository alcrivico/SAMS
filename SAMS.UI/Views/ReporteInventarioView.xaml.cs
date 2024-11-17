using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views;

/// <summary>
/// Lógica de interacción para ReporteInventarioView.xaml
/// </summary>
public partial class ReporteInventarioView : Window
{
    public ReporteInventarioView()
    {
        InitializeComponent();
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

    private void TablaReporte_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void Imprimir_ButtonControlClick(object sender, RoutedEventArgs e)
    {

    }
}