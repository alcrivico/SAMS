using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views;

/// <summary>
/// Lógica de interacción para EditarPromocionView.xaml
/// </summary>
public partial class EditarPromocionView : Window
{
    List<PromocionesDTO> listaPromociones;
    ObservableCollection<Object> _promociones;
    private EmpleadoLoginDTO empleado;
    private SideBarControl SideBarControl_MenuLateral;

    public EditarPromocionView(EmpleadoLoginDTO? empleado = null)
    {
        _promociones = new ObservableCollection<Object>();
        this.empleado = empleado ?? new EmpleadoLoginDTO() { tipoEmpleado = "Paqueteria" };
        InitializeComponent();

        SideBarControl_MenuLateral = new SideBarControl(empleado);
        MenuLateral.Children.Add(SideBarControl_MenuLateral);
        SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;

        DefinirColumnas();
        ObtenerPromociones();
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
                    { "Name", "nombre" },
                    { "Width", "*" },
                    { "BindingName", "nombre" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Cantidad" },
                    { "Width", "*" },
                    { "BindingName", "cantidad" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Finzalizacion" },
                    { "Width", "*" },
                    { "BindingName", "fechaFin" }

                },
                 new Dictionary<string, string> {

                    { "Type", "Actions" },
                    { "Name", "Acciones" },
                    { "Width", "*" },
                    { "Detalles", "False" },
                    { "Editar", "True" },
                    { "Eliminar", "True" }

                }
                //Aqui agregar la columna de editar modificar y eliminar hasta que lo termine cristoff
            };

        TablaPromociones.DefineColumns(columnas);

    }

    private async void ObtenerPromociones()
    {
        try
        {
            listaPromociones = PromocionDAO.VerPromociones();
            _promociones.Clear();
            _promociones = new ObservableCollection<Object>(listaPromociones);
            TablaPromociones.SetItemsSource(_promociones);
        }
        catch
        {
            InformationControl.Show("Error", "Ocurrió un error al obtener Inventario de Producto", "Aceptar");

            Close();
        }
    }

    private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
    {
        if (listaPromociones != null)
        {
            var reporteFiltado = listaPromociones.Where(
                x => x.nombre.ToLower().Contains(campoBuscar.Text.ToLower())).ToList();

            _promociones.Clear();

            _promociones = new ObservableCollection<Object>(reporteFiltado);

            TablaPromociones.SetItemsSource(_promociones);
        }
        else
        {
            _promociones.Clear();

            _promociones = new ObservableCollection<Object>(listaPromociones);

            TablaPromociones.SetItemsSource(_promociones);
        }
    }

    private void Registrar_ButtonControlClick(object sender, RoutedEventArgs e)
    {
        RegistrarPromocionView registrarPromocionView = new(empleado);
        registrarPromocionView.ShowDialog();
        this.Close();
    }
}
