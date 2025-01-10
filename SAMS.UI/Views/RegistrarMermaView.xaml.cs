using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para RegistrarMermaView.xaml
    /// </summary>
    public partial class RegistrarMermaView : Window
    {
        List<ProductoInventarioMermaDTO> productosInventario;
        private ObservableCollection<object> _productosInventarios;
        private ObservableCollection<object> _ubicaciones;

        public RegistrarMermaView()
        {
            productosInventario = new List<ProductoInventarioMermaDTO>();
            _productosInventarios = new ObservableCollection<object>();
            _ubicaciones = new ObservableCollection<object>();

            InitializeComponent();
            LlenarProductos();
            LlenarUbicaciones();

            comboProducto.SetSelectedItem(productosInventario);
            comboProducto.SelectedItem = null;
            comboArea.SelectedItem = null;
        }

        private void LlenarProductos()
        {
            productosInventario = ProductoInventarioDAO.CargarProductosInventario();
            ConvertirProductosInventario(productosInventario);
            comboProducto.SetItemsSource(_productosInventarios, "nombre");
        }

        private void ConvertirProductosInventario(List<ProductoInventarioMermaDTO> productosInventario)
        {
            foreach (ProductoInventarioMermaDTO productoInventario in productosInventario)
            {
                _productosInventarios.Add(productoInventario);
            }
        }

        private void LlenarUbicaciones()
        {
            var lugares = new List<string> { "Bodega", "Exhibición" }; 
            foreach (var lugar in lugares)
            {
                _ubicaciones.Add(lugar);
            }

            comboArea.SetItemsSource(_ubicaciones, null); 
            comboArea.SelectedItem = null; 
        }

        private void comboProducto_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            var productoSeleccionado = (ProductoInventarioMermaDTO)comboProducto.SelectedItem;

            if (productoSeleccionado != null)
            {
                campoCantidadBodega.Text = productoSeleccionado.cantidadBodega.ToString();
                campoCantidadExhibicion.Text = productoSeleccionado.cantidadExhibicion.ToString();
            }
            else
            {
                campoCantidadBodega.Text = string.Empty;
                campoCantidadExhibicion.Text = string.Empty;
            }

            ValidarCampos();
        }

        private void botonRegistrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            var productoSeleccionado = (ProductoInventarioMermaDTO)comboProducto.SelectedItem;
            var lugarDescuento = comboArea.SelectedItem as string;
            var cantidad = campoCantidad.Text;
            var descripcion = campoDescripcion.Text;

            if (productoSeleccionado != null &&
                !string.IsNullOrEmpty(lugarDescuento) &&
                int.TryParse(cantidad, out int cantidadInt) &&
                !string.IsNullOrEmpty(descripcion))
            {
                if (lugarDescuento == "Bodega" && cantidadInt > productoSeleccionado.cantidadBodega)
                {
                    InformationControl.Show("Error", "La cantidad excede la disponible en bodega.", "Aceptar");
                    return;
                }
                else if (lugarDescuento == "Exhibición" && cantidadInt > productoSeleccionado.cantidadExhibicion)
                {
                    InformationControl.Show("Error", "La cantidad excede la disponible en exhibición.", "Aceptar");
                    return;
                }

                bool resultado = MermaDAO.RegistrarMerma(
                    productoSeleccionado.id,
                    lugarDescuento,
                    cantidadInt,
                    descripcion
                );

                if (resultado)
                {
                    InformationControl.Show("Éxito", "La merma se registró correctamente.", "Aceptar");
                    ResetFormulario();
                }
                else
                {
                    InformationControl.Show("Error", "Ocurrió un problema al registrar la merma.", "Aceptar");
                }
            }
            else
            {
                InformationControl.Show("Error", "Debe llenar todos los campos antes de registrar.", "Aceptar");
            }
        }

        private void ResetFormulario()
        {
            comboProducto.SelectedItem = null;
            comboArea.SelectedItem = null;
            campoCantidad.Text = string.Empty;
            campoDescripcion.Text = string.Empty;
            campoCantidadBodega.Text = string.Empty;
            campoCantidadExhibicion.Text = string.Empty;
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void campoCantidad_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ValidarCampos()
        {
            bool camposValidos =
                comboProducto.SelectedItem != null &&
                comboArea.SelectedItem != null &&
                !string.IsNullOrEmpty(campoCantidad.Text) &&
                int.TryParse(campoCantidad.Text, out _) &&
                !string.IsNullOrEmpty(campoDescripcion.Text);

            botonRegistrar.IsEnabled = camposValidos;
        }

        private void comboArea_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            ValidarCampos();
        }

        private void campoCantidad_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            ValidarCampos();
        }

        private void campoDescripcion_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            ValidarCampos();
        }
    }
}

