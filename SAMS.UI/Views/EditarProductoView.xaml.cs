using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para EditarProductoView.xaml
    /// </summary>
    public partial class EditarProductoView : Window
    {
        bool esModificacion;
        DetalleProductoDTO detalleProducto;
        EmpleadoLoginDTO empleado;
        ProductosRegistradosDTO producto;
        SideBarControl SideBarControl_MenuLateral;
        List<CategoriaDTO> categorias;
        private ObservableCollection<object> _categorias;
        List<UnidadDeMedidaDTO> unidadesDeMedida;
        private ObservableCollection<object> _unidadesDeMedida;

        public EditarProductoView(EmpleadoLoginDTO empleado, ProductosRegistradosDTO producto, bool esModificación)
        {
            this.esModificacion = esModificación;
            this.empleado = empleado;
            this.producto = producto;

            InitializeComponent();

            categorias = new List<CategoriaDTO>();
            _categorias = new ObservableCollection<object>();

            unidadesDeMedida = new List<UnidadDeMedidaDTO>();
            _unidadesDeMedida = new ObservableCollection<object>();

            SideBarControl_MenuLateral = new SideBarControl(empleado);
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;

            ObtenerCategorias();
            ObtenerUnidadesDeMedida();
            ObtenerDetalleproducto();
            

            if (esModificacion)
            {
                ActivarCamposProducto();
            }  

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

        private void Button_Modificar_ButtonControlClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            VerProductosView verProductosView = new VerProductosView(empleado);
            verProductosView.Show();
            this.Close();
        }

        private void ObtenerCategorias()
        {
            categorias = ProductoInventarioDAO.ObtenerCategorias();
            ConvertirCategorias(categorias);
            ComboBox_Categorias.SetItemsSource(_categorias, "nombre");
        }

        private void ConvertirCategorias(List<CategoriaDTO> categorias)
        {
            _categorias.Clear();

            foreach (CategoriaDTO categoria in categorias)
            {
                _categorias.Add(categoria);
            }

        }

        private void ObtenerUnidadesDeMedida()
        {
            unidadesDeMedida = ProductoInventarioDAO.ObtenerUnidadesDeMedida();
            ConvertirUnidadesDeMedida(unidadesDeMedida);
            ComboBox_UnidadMedida.SetItemsSource(_unidadesDeMedida, "nombre");
        }

        private void ConvertirUnidadesDeMedida(List<UnidadDeMedidaDTO> unidadesDeMedida)
        {
            _unidadesDeMedida.Clear();

            foreach (UnidadDeMedidaDTO unidadDeMedida in unidadesDeMedida)
            {
                _unidadesDeMedida.Add(unidadDeMedida);
            }

        }

        private void ObtenerDetalleproducto()
        {
            try
            {
                detalleProducto = ProductoInventarioDAO.ObtenerDetalleProductoInventario(producto.codigoProducto);
                
                TextBox_Nombre.Text = detalleProducto.nombreProducto;
                TextArea_Descripcion.Text = detalleProducto.descripcion;
                TextBox_CantidadBodega.Text = detalleProducto.cantidadBodega.ToString();
                TextBox_CantidadExhibicion.Text = detalleProducto.cantidadExhibicion.ToString();
                TextBox_PrecioActual.Text = detalleProducto.precioActual.ToString();
                DatePicker_FechaCaducidad.SelectedDate = detalleProducto.fechaCaducidad;

                var categoriaSeleccionada = _categorias
                        .FirstOrDefault(c => ((CategoriaDTO)c).nombre == detalleProducto.nombreCategoria);
                ComboBox_Categorias.ComboBoxControlType.SelectedItem = categoriaSeleccionada;

                var unidadDeMedidaSeleccionada = _unidadesDeMedida
                        .FirstOrDefault(u => ((UnidadDeMedidaDTO)u).nombre == detalleProducto.nombreUnidadMedida);
                ComboBox_UnidadMedida.ComboBoxControlType.SelectedItem = unidadDeMedidaSeleccionada;

                CheckBox_EsPerecedero.IsChecked = detalleProducto.esPerecedero;
                CheckBox_EsDevolvible.IsChecked = detalleProducto.esDevolvible;

            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", "No se pudo conectar a la red del supermercado," +
                    " inténtelo de nuevo más tarde", "Aceptar");
                this.Close();
            }
        }

        private void ActivarCamposProducto()
        {
            TextArea_Descripcion.IsEnabled = true;
            TextBox_PrecioActual.IsEnabled = true;
            DatePicker_FechaCaducidad.IsEnabled = true;
            ComboBox_Categorias.IsEnabled = true;
            ComboBox_UnidadMedida.IsEnabled = true;
            CheckBox_EsPerecedero.IsEnabled = true;
            CheckBox_EsDevolvible.IsEnabled = true;
            EditarCantidadBodegaExhibición.Visibility = Visibility.Visible;
            Button_Modificar.Visibility = Visibility.Visible;
            Button_Volver.Text = "Cancelar";

        }

        private void ValidarCamposProducto()
        {

        }

    }
}
