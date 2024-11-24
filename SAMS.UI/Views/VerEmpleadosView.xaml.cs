using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para VerEmpleadosView.xaml
    /// </summary>
    public partial class VerEmpleadosView : Window
    {
        List<V_Empleados> listaEmpleados;
        ObservableCollection<Object> _empleados;
        EmpleadoLoginDTO _empleado;
        SideBarControl SideBarControl_MenuLateral;

        public VerEmpleadosView(EmpleadoLoginDTO empleado)
        {
            _empleado = empleado;
            listaEmpleados = new List<V_Empleados>();
            _empleados = new ObservableCollection<Object>();

            InitializeComponent();
            DefinirColumnas();
            ObtenerEmpleados();

            SideBarControl_MenuLateral = new SideBarControl(_empleado);
            SideBarControl_MenuLateral.SideElementSelected = 3;
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = _empleado.tipoEmpleado;

            TablaEmpleados.OnDetallesClickedHandler += botonDetallesClick;
            TablaEmpleados.OnEditarClickedHandler += botonEditarClick;
            TablaEmpleados.OnEliminarClickedHandler += botonEliminarClick;
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
                    { "Name", "Puesto" },
                    { "Width", "*" },
                    { "BindingName", "puesto" }
                },
                new Dictionary<string, string> {

                    { "Type", "Actions" },
                    { "Name", "Acciones" },
                    { "Width", "*" },
                    { "Detalles", "True" },
                    { "Editar", "True" },
                    { "Eliminar", "True" }

                }

            };

            TablaEmpleados.DefineColumns(columnas);

        }

        private void ObtenerEmpleados()
        {
            try
            {
                listaEmpleados = EmpleadoDAO.ObtenerEmpleados().ToList();
                listaEmpleados.Remove(listaEmpleados.Where(e => e.correo == _empleado.correo).FirstOrDefault());
                _empleados.Clear();
                _empleados = new ObservableCollection<Object>(listaEmpleados.OrderBy(e => e.nombre));
                TablaEmpleados.SetItemsSource(_empleados);
            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", "Ocurrió un error al obtener los empleados", "Aceptar");
                this.Close();
            }
        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (listaEmpleados != null)
            {

                if (campoBuscar.Text.Length > 0)
                {
                    var proveedoresFiltrados = listaEmpleados.Where(
                        p =>
                        p.nombre.ToUpper().Contains(campoBuscar.Text.ToUpper()) ||
                        p.rfc.ToUpper().Contains(campoBuscar.Text.ToUpper())).ToList();//ToUpper() para que no sea case sensitive

                    _empleados.Clear();

                    _empleados = new ObservableCollection<Object>(proveedoresFiltrados);

                    TablaEmpleados.SetItemsSource(_empleados);

                }
                else
                {

                    _empleados.Clear();

                    _empleados = new ObservableCollection<Object>(listaEmpleados);

                    TablaEmpleados.SetItemsSource(_empleados);

                }

            }
        }

        private void botonDetallesClick(object sender, RoutedEventArgs e)
        {
            //ActionsControl actionBar = (ActionsControl)sender;
            //V_Proveedores proveedor = (V_Proveedores)actionBar.DataContext;

            //DetalleProveedorView detalleProveedorView = new DetalleProveedorView(proveedor);
            //detalleProveedorView.ShowDialog();
        }

        private void botonEditarClick(object sender, RoutedEventArgs e)
        {
            //ActionsControl actionBar = (ActionsControl)sender;
            //V_Proveedores proveedor = (V_Proveedores)actionBar.DataContext;

            //EditarProveedorView editarProveedorView = new EditarProveedorView(proveedor);
            //editarProveedorView.ShowDialog();
            ObtenerEmpleados();
        }

        private void botonEliminarClick(object sender, RoutedEventArgs e)
        {
            //if (ConfirmationControl.Show("Confirmar", "¿Está seguro de que desea eliminar a este proveedor?\n Esta acción no se puede deshacer", "Aceptar", "Cancelar"))
            //{
            //    ProveedorDAO.EliminarProveedor((V_Proveedores)((ActionsControl)sender).DataContext);
            //}

            ObtenerEmpleados();
        }

        private void botonAgregar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            RegistrarEmpleadoView registrarEmpleadoView = new RegistrarEmpleadoView();
            registrarEmpleadoView.ShowDialog();
            ObtenerEmpleados();
        }
    }
}