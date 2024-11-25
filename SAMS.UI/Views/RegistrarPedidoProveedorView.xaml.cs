using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Lógica de interacción para RegistrarPedidoProveedorView.xaml
    /// </summary>
    public partial class RegistrarPedidoProveedorView : Window
    {

        List<V_Proveedores> proveedores;
        private ObservableCollection<object> _proveedores;
        private List<DetallesPedidoDTO> productosPedidos;

        public RegistrarPedidoProveedorView()
        {
            proveedores = new List<V_Proveedores>();
            _proveedores = new ObservableCollection<object>();
            productosPedidos = new List<DetallesPedidoDTO>();

            InitializeComponent();
            LlenarProveedores();
            DefinirColumnas();

            comboProveedor.SetSelectedItem(proveedores);
            comboProveedor.SelectedItem = null;  // No seleccionar proveedor por defecto
            comboProducto.SelectedItem = null; //No
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DefinirColumnas()
        {
            Dictionary<string, string>[] columnas =
            {
                new Dictionary<string, string>
                {
                    {"Type", "Text"},
                    {"Name", "Producto"},
                    {"Width", "*"},
                    {"BindingName", "nombreProducto" }
                },
                new Dictionary<string, string>
                {
                    {"Type", "Text"},
                    {"Name", "Unidad de medida"},
                    {"Width", "*"},
                    {"BindingName", "nombreUnidadMedida" }
                },
                new Dictionary<string, string>
                {
                    {"Type", "Text"},
                    {"Name", "Cantidad"},
                    {"Width", "*"},
                    {"BindingName", "cantidad" }
                }
            };

            TablaProductosPedidos.DefineColumns(columnas);
        }

        private void LlenarProveedores()
        {
            proveedores = ProveedorDAO.CargarProveedores();
            ConvertirProveedores(proveedores);
            comboProveedor.SetItemsSource(_proveedores, "nombre");
        }

        private void ConvertirProveedores(List<V_Proveedores> proveedores)
        {
            foreach (V_Proveedores proveedor in proveedores)
            {
                _proveedores.Add(proveedor);
            }

        }

        private void comboProveedor_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            var proveedorSeleccionado = (V_Proveedores)comboProveedor.SelectedItem;
            if (proveedorSeleccionado != null)
            {
                var productos = new ObservableCollection<object>(ProductoProveedorDAO.ObtenerListaProductosPorRfc(proveedorSeleccionado.rfc));
                comboProducto.SetItemsSource(productos, "nombre");
                comboProducto.IsEnabled = true;
            }
            else
            {
                comboProducto.IsEnabled = false;
            }
        }

        private void comboProducto_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            var productoSeleccionado = (V_Producto)comboProducto.SelectedItem;
            if (productoSeleccionado != null)
            {
                campoCantidad.IsEnabled = true; 
                campoPrecioCompra.IsEnabled = true;
            }
            else
            {
                campoCantidad.IsEnabled = false;
                campoPrecioCompra.IsEnabled = false;
            }
        }

        private void campoCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);

            if (!e.Handled)
            {
                campoPrecioCompra.IsEnabled = true;
            }
        }

        private void botonRegistrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            if (productosPedidos != null && productosPedidos.Count > 0)
            {
                PedidoDAO.RegistrarProductosAlPedido(productosPedidos);

                InformationControl.Show("Información", "El pedido se ha registrado de forma exitosa.", "Aceptar");
                this.Close();

            }
            else
            {
                InformationControl.Show("Error", "No hay productos para registrar en el pedido.", "Aceptar");
            }
        }

        private void botonAgregarProducto_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            var proveedorSeleccionado = (V_Proveedores)comboProveedor.SelectedItem;
            var productoSeleccionado = (V_Producto)comboProducto.SelectedItem;
            var cantidad = campoCantidad.Text;

            if (proveedorSeleccionado != null && productoSeleccionado != null && !string.IsNullOrEmpty(cantidad) && int.TryParse(cantidad, out int cantidadInt))
            {

                bool productoExiste = productosPedidos.Any(p => p.nombreProducto == productoSeleccionado.nombre);

                if (productoExiste)
                {
                    InformationControl.Show("Información", "El producto ya ha sido agregado a la tabla.", "Aceptar");
                    return;
                }

                var unidadMedida = "Litro";

                DetallesPedidoDTO productoPedido = new DetallesPedidoDTO
                {
                    nombreProducto = productoSeleccionado.nombre,
                    nombreUnidadMedida = unidadMedida,
                    cantidad = cantidadInt
                };

                productosPedidos.Add(productoPedido);
                ActualizarTablaProductosPedidos();

                campoPrecioCompra.Text = string.Empty;
                campoPrecioCompra.IsEnabled = false;

                comboProducto.SelectedItem = null;
                campoCantidad.Text = string.Empty;
                campoCantidad.IsEnabled = false;
                comboProveedor.IsEnabled = false;
            }
            else
            {
                InformationControl.Show("Informacion", "Debe llenar todos los campos para agregar un producto", "Aceptar");
            }
        }

        private void ActualizarTablaProductosPedidos()
        {           
                TablaProductosPedidos.SetItemsSource(new ObservableCollection<object>(productosPedidos));
        }


        private void botonEliminarProducto_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            var productoSeleccionado = TablaProductosPedidos.SelectedItem as DetallesPedidoDTO;

            if (productoSeleccionado != null)
            {
                productosPedidos.Remove(productoSeleccionado);
                ActualizarTablaProductosPedidos();
            }
            else
            {
                InformationControl.Show("Informacion", "Debe seleccionar un producto para eliminar", "Aceptar");
            }
        }

        private void campoPrecioCompra_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new System.Text.RegularExpressions.Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
