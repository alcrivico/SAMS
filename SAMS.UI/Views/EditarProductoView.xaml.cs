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
        private ObservableCollection<object> _ubicaciones;

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
            _ubicaciones = new ObservableCollection<object>();

            SideBarControl_MenuLateral = new SideBarControl(empleado);
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;

            ObtenerCategorias();
            ObtenerUnidadesDeMedida();
            ObtenerDetalleproducto();

            if (esModificacion)
            {
                ActivarCamposProducto();
                CargarUbicaciones();
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
            if (ValidarCamposProducto() && ModificarCantidadDeInventario())
            {
                if (!CamposCambiados())
                {
                    EditarProductoInventarioDTO();
                }
                else
                {
                    CerrarVentana();
                }
            }

        }

        private void Button_Cancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            if (esModificacion)
            {
                bool result = ConfirmationControl.Show(
                    "Confirmación",
                    "¿Estás seguro de cancelar la operación? se perderán los cambios",
                    "Aceptar",
                    "Cancelar"
                );

                if (result)
                {
                    CerrarVentana();
                }
            }
            else
            {
                CerrarVentana();
            }
        }

        private void CerrarVentana()
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

        private bool ValidarCamposProducto()
        {
            LimpiarMensajesError();
            bool camposValidos = true;
            if (string.IsNullOrEmpty(TextArea_Descripcion.Text.Trim()))
            {
                TextBlock_MensajeDescripcionInvalida.Visibility = Visibility.Visible;
                camposValidos = false;
            }

            if (string.IsNullOrEmpty(TextBox_PrecioActual.Text.Trim()) ||
                    !decimal.TryParse(TextBox_PrecioActual.Text.Trim(), out decimal precioActual) ||
                    precioActual <= 0)
            {
                TextBlock_MensajePrecioInvalido.Visibility = Visibility.Visible;
                camposValidos = false;
            }

            if (DatePicker_FechaCaducidad.SelectedDate == null)
            {
                TextBlock_MensajeFechaInvalida.Visibility = Visibility.Visible;
                camposValidos = false;
            }

            if (ComboBox_Categorias.SelectedItem == null)
            {
                camposValidos = false;
            }

            if (ComboBox_UnidadMedida.SelectedItem == null)
            {
                camposValidos = false;
            }

            if (CheckBox_EsPerecedero.IsChecked == null)
            {
                camposValidos = false;
            }

            if (CheckBox_EsDevolvible.IsChecked == null)
            {
                camposValidos = false;
            }

            return camposValidos;
        }

        private void LimpiarMensajesError()
        {
            TextBlock_MensajeDescripcionInvalida.Visibility = Visibility.Hidden;
            TextBlock_MensajePrecioInvalido.Visibility = Visibility.Hidden;
            TextBlock_MensajeFechaInvalida.Visibility = Visibility.Hidden;
        }

        private bool CamposCambiados()
        {
            if (TextBox_Nombre.Text.Trim() != detalleProducto.nombreProducto)
            {
                return false;
            }

            if (TextArea_Descripcion.Text.Trim() != detalleProducto.descripcion)
            {
                return false;
            }

            if (TextBox_CantidadBodega.Text.Trim() != detalleProducto.cantidadBodega.ToString())
            {
                return false;
            }

            if (TextBox_CantidadExhibicion.Text.Trim() != detalleProducto.cantidadExhibicion.ToString())
            {
                return false;
            }

            if (TextBox_PrecioActual.Text.Trim() != detalleProducto.precioActual.ToString())
            {
                return false;
            }

            if (DatePicker_FechaCaducidad.SelectedDate != detalleProducto.fechaCaducidad)
            {
                return false;
            }

            var categoriaSeleccionada = ComboBox_Categorias.SelectedItem as CategoriaDTO;
            if (categoriaSeleccionada == null || categoriaSeleccionada.nombre != detalleProducto.nombreCategoria)
            {
                return false;
            }

            var unidadDeMedidaSeleccionada = ComboBox_UnidadMedida.SelectedItem as UnidadDeMedidaDTO;
            if (unidadDeMedidaSeleccionada == null || unidadDeMedidaSeleccionada.nombre != detalleProducto.nombreUnidadMedida)
            {
                return false;
            }

            if (CheckBox_EsPerecedero.IsChecked != detalleProducto.esPerecedero)
            {
                return false;
            }

            if (CheckBox_EsDevolvible.IsChecked != detalleProducto.esDevolvible)
            {
                return false;
            }

            // Si todos los datos son iguales, devolver true
            return true;
        }

        private bool ModificarCantidadDeInventario()
        {
            ReiniciarMensajeCantidadInvalida();

            // Si esta vacio entonces el metodo ignora la modificacion
            if (string.IsNullOrWhiteSpace(TextBoxControl_CantidadAMover.Text) ||
                TextBoxControl_CantidadAMover.Text.Trim() == "0")
            {
                return true;
            }

            if (ComboBoxControl_Ubicaciones.SelectedItem == null)
            {
                return false;
            }

            UbicacionDTO ubicacionSeleccionada = ComboBoxControl_Ubicaciones.SelectedItem as UbicacionDTO;

            // Validar que se haya ingresado una cantidad válida
            if (!int.TryParse(TextBoxControl_CantidadAMover.Text.Trim(), out int cantidadAMover) ||
                        cantidadAMover < 0)
            {
                TextBlock_MensajeCantidadInvalida.Visibility = Visibility.Visible;
                return false;
            }



            if (ubicacionSeleccionada.nombre == "Bodega")
            {

                if (detalleProducto.cantidadExhibicion < cantidadAMover)
                {
                    TextBlock_MensajeCantidadInvalida.Text = "Cantidad en exhibición insuficiente.";
                    TextBlock_MensajeCantidadInvalida.Visibility = Visibility.Visible;
                    return false;
                }

                // Mover cantidad de exhibición a bodega
                detalleProducto.cantidadExhibicion -= cantidadAMover;
                detalleProducto.cantidadBodega += cantidadAMover;
            }
            else if (ubicacionSeleccionada.nombre == "Exhibición")
            {
                if (detalleProducto.cantidadBodega < cantidadAMover)
                {
                    TextBlock_MensajeCantidadInvalida.Text = "Cantidad en bodega insuficiente";
                    TextBlock_MensajeCantidadInvalida.Visibility = Visibility.Visible;
                    return false;
                }

                // Mover cantidad de bodega a exhibición
                detalleProducto.cantidadBodega -= cantidadAMover;
                detalleProducto.cantidadExhibicion += cantidadAMover;
            }

            // Ocultar mensaje de error
            TextBlock_MensajeCantidadInvalida.Visibility = Visibility.Hidden;

            return true;
        }

        private void ReiniciarMensajeCantidadInvalida()
        {
            TextBlock_MensajeCantidadInvalida.Text = "Ingrese una cantidad valida.";
        }

        private void CargarUbicaciones()
        {
            var ubicaciones = ObtenerUbicaciones();
            ConvertirUbicaciones(ubicaciones);
            ComboBoxControl_Ubicaciones.SetItemsSource(_ubicaciones, "nombre");
        }

        private List<UbicacionDTO> ObtenerUbicaciones()
        {
            return new List<UbicacionDTO>
                {
                    new UbicacionDTO { nombre = "Bodega" },
                    new UbicacionDTO { nombre = "Exhibición" }
                };
        }

        private void ConvertirUbicaciones(List<UbicacionDTO> ubicaciones)
        {
            _ubicaciones.Clear();

            foreach (UbicacionDTO ubicacion in ubicaciones)
            {
                _ubicaciones.Add(ubicacion);
            }
        }

        private async void EditarProductoInventarioDTO()
        {
            try
            {
                // Crear una nueva instancia del DTO y asignar los valores de los campos
                var editarProductoDTO = new EditarProductoInventarioDTO
                {
                    codigoProducto = producto.codigoProducto,
                    descripcion = TextArea_Descripcion.Text.Trim(),
                    cantidadBodega = detalleProducto.cantidadBodega,
                    cantidadExhibicion = detalleProducto.cantidadExhibicion,
                    precioActual = decimal.Parse(TextBox_PrecioActual.Text.Trim()),
                    fechaCaducidad = DatePicker_FechaCaducidad.SelectedDate.Value,
                    nombreCategoria = ((CategoriaDTO)ComboBox_Categorias.SelectedItem)?.nombre,
                    nombreUnidadMedida = ((UnidadDeMedidaDTO)ComboBox_UnidadMedida.SelectedItem)?.nombre,
                    esPerecedero = CheckBox_EsPerecedero.IsChecked,
                    esDevolvible = CheckBox_EsDevolvible.IsChecked
                };

                bool resultado = await ProductoInventarioDAO.EditarProductoInventario(editarProductoDTO);

                if (resultado)
                {
                    InformationControl.Show("Éxito", "El producto se editó correctamente", "Aceptar");
                    CerrarVentana();
                }
                else
                {
                    InformationControl.Show("Error", "No se pudo editar el producto. Inténtalo de nuevo más tarde.", "Aceptar");
                }
            }
            catch (FormatException)
            {
                InformationControl.Show("Error", "Algunos campos tienen un formato incorrecto. Por favor verifica los datos.", "Aceptar");
            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", $"Ocurrió un error inesperado: {ex.Message}", "Aceptar");
            }

        }

    }
}
