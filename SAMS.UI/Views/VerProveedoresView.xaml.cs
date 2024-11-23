using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para VerProveedoresView.xaml
    /// </summary>
    public partial class VerProveedoresView : Window
    {
        List<V_Proveedores> listaProveedores;
        ObservableCollection<Object> _proveedores;
        EmpleadoLoginDTO _empleado;
        SideBarControl SideBarControl_MenuLateral;
        public VerProveedoresView(EmpleadoLoginDTO empleado)
        {
            _empleado = empleado;
            listaProveedores = new List<V_Proveedores>();
            _proveedores = new ObservableCollection<Object>();

            InitializeComponent();
            DefinirColumnas();
            ObtenerProveedores();

            SideBarControl_MenuLateral = new SideBarControl(_empleado);
            SideBarControl_MenuLateral.SideElementSelected = 2;
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = _empleado.tipoEmpleado;

            TablaProveedores.OnDetallesClickedHandler += botonDetallesClick;
            TablaProveedores.OnEditarClickedHandler += botonEditarClick;
            TablaProveedores.OnEliminarClickedHandler += botonEliminarClick;
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
                    { "Name", "Nombre" },
                    { "Width", "*" },
                    { "BindingName", "nombre" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "RFC" },
                    { "Width", "*" },
                    { "BindingName", "rfc" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Estado" },
                    { "Width", "*" },
                    { "BindingName", "estado" }
                },
                new Dictionary<string, string> {

                    { "Type", "Actions" },
                    { "Name", "Acciones" },
                    { "Width", "*" },
                    { "Detalles", "True" },
                    { "Editar", "True" },
                    { "Eliminar", "False" }

                }

            };

            TablaProveedores.DefineColumns(columnas);

        }

        private void ObtenerProveedores()
        {
            try
            {
                listaProveedores = ProveedorDAO.ObtenerProveedores().ToList();
                _proveedores.Clear();
                _proveedores = new ObservableCollection<Object>(listaProveedores);
                TablaProveedores.SetItemsSource(_proveedores);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                InformationControl.Show("Error", "Ocurrió un error al obtener los proveedores", "Aceptar");
                this.Close();
            }
        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (listaProveedores != null)
            {

                if (campoBuscar.Text.Length > 0)
                {
                    var proveedoresFiltrados = listaProveedores.Where(
                        p =>
                        p.nombre.ToUpper().Contains(campoBuscar.Text.ToUpper()) ||
                        p.rfc.ToUpper().Contains(campoBuscar.Text.ToUpper())).ToList();//ToUpper() para que no sea case sensitive

                    _proveedores.Clear();

                    _proveedores = new ObservableCollection<Object>(proveedoresFiltrados);

                    TablaProveedores.SetItemsSource(_proveedores);

                }
                else
                {

                    _proveedores.Clear();

                    _proveedores = new ObservableCollection<Object>(listaProveedores);

                    TablaProveedores.SetItemsSource(_proveedores);

                }

            }
        }

        private void botonDetallesClick(object sender, RoutedEventArgs e)
        {
            ActionsControl actionBar = (ActionsControl)sender;
            V_Proveedores proveedor = (V_Proveedores)actionBar.DataContext;

            DetalleProveedorView detalleProveedorView = new DetalleProveedorView(proveedor);
            detalleProveedorView.ShowDialog();
        }

        private void botonEditarClick(object sender, RoutedEventArgs e)
        {
            ActionsControl actionBar = (ActionsControl)sender;
            V_Proveedores proveedor = (V_Proveedores)actionBar.DataContext;

            EditarProveedorView editarProveedorView = new EditarProveedorView(proveedor);
            editarProveedorView.ShowDialog();
            ObtenerProveedores();
        }

        private void botonEliminarClick(object sender, RoutedEventArgs e)
        {
            ObtenerProveedores();
        }

        private void botonAgregar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            RegistrarProveedorView registrarProveedorView = new RegistrarProveedorView();
            registrarProveedorView.ShowDialog();
            ObtenerProveedores();
        }
    }
}
