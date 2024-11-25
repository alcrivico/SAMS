using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para VerProductosView.xaml
    /// </summary>
    public partial class VerProductosView : Window
    {
        List<ProductosRegistradosDTO> listaProductos;
        ObservableCollection<Object> _productos;
        EmpleadoLoginDTO empleado;
        SideBarControl SideBarControl_MenuLateral;

        public VerProductosView(EmpleadoLoginDTO empleado)
        {
            this.empleado = empleado;
            _productos = new ObservableCollection<Object>();
            InitializeComponent();
            
            DefinirColumnas();
            ObtenerProductos();

            SideBarControl_MenuLateral = new SideBarControl(empleado);
            SideBarControl_MenuLateral.SideElementSelected = 1;
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;

            TableControl_TablaProductos.OnDetallesClickedHandler += botonDetallesClick;
            TableControl_TablaProductos.OnEditarClickedHandler += botonEditarClick;
            TableControl_TablaProductos.OnEliminarClickedHandler += botonEliminarClick;
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
                    { "BindingName", "nombreProducto" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Cantidad" },
                    { "Width", "*" },
                    { "BindingName", "cantidad" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Precio" },
                    { "Width", "*" },
                    { "BindingName", "precioActual" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "categoría" },
                    { "Width", "*" },
                    { "BindingName", "nombreCategoria" }

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

            TableControl_TablaProductos.DefineColumns(columnas);

        }

        private void ObtenerProductos()
        {

            try
            {
                listaProductos = ProductoInventarioDAO.ObtenerProductosRegistrados().ToList();
                _productos.Clear();

                _productos = new ObservableCollection<Object>(listaProductos);
                TableControl_TablaProductos.SetItemsSource(_productos);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                InformationControl.Show("Error", "No se pudo conectar a la red del supermercado," +
                    " inténtelo de nuevo más tarde", "Aceptar");
                this.Close();

            }

        }

        private void botonDetallesClick(object sender, RoutedEventArgs e)
        {
            ActionsControl actionBar = (ActionsControl)sender;
            ProductosRegistradosDTO productoSeleccionado = (ProductosRegistradosDTO)actionBar.DataContext;

            EditarProductoView editarProductoView = new EditarProductoView(empleado, productoSeleccionado, false);
            editarProductoView.Show();
            this.Close();

        }

        private void botonEditarClick(object sender, RoutedEventArgs e)
        {
            ActionsControl actionBar = (ActionsControl)sender;
            ProductosRegistradosDTO productoSeleccionado = (ProductosRegistradosDTO)actionBar.DataContext;

            EditarProductoView editarProductoView = new EditarProductoView(empleado, productoSeleccionado, true);
            editarProductoView.Show();
            this.Close();
        }

        private void botonEliminarClick(object sender, RoutedEventArgs e)
        {
            if (ConfirmationControl.Show("Eliminar", "¿Está seguro que desea eliminar este empleado?", "Aceptar", "Cancelar"))
            {
                ActionsControl actionBar = (ActionsControl)sender;
                ProductosRegistradosDTO productoSeleccionado = (ProductosRegistradosDTO)actionBar.DataContext;
                ProductoInventarioDAO.CambiarEstadoProductoAgotado(productoSeleccionado.codigoProducto);
                ObtenerProductos();
            }

            
        }

        private void Button_AgregarProductos_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            RegistrarProductoView registrarProductoView = new RegistrarProductoView(empleado);
            registrarProductoView.Show();
            this.Close();
        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (listaProductos != null)
            {

                if (campoBuscar.Text.Length > 0)
                {
                    var productosFiltrados = listaProductos.Where(
                        p =>
                        p.nombreProducto.Contains(campoBuscar.Text, StringComparison.OrdinalIgnoreCase) ||
                        p.nombreCategoria.Contains(campoBuscar.Text, StringComparison.OrdinalIgnoreCase) ||
                        p.precioActual.ToString().Contains(campoBuscar.Text, StringComparison.OrdinalIgnoreCase));

                    _productos.Clear();

                    _productos = new ObservableCollection<Object>(productosFiltrados);

                    TableControl_TablaProductos.SetItemsSource(_productos);

                }
                else
                {

                    _productos.Clear();

                    _productos = new ObservableCollection<Object>(listaProductos);

                    TableControl_TablaProductos.SetItemsSource(_productos);

                }

            }

        }
    }
}
