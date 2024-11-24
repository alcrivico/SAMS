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
    /// Lógica de interacción para VerDetallesPedidoView.xaml
    /// </summary>
    public partial class VerDetallesPedidoView : Window
    {
        PedidosDTO _pedido;
        ObservableCollection<Object> _productos;
        public VerDetallesPedidoView(PedidosDTO pedido)
        {
            _pedido = new PedidosDTO();
            _productos = new ObservableCollection<Object>();

            InitializeComponent();
            DefinirColumnas();
            LlenarDatosDetallesPedido(pedido.noPedido);
        }

        private void botonVolver_ButtonControlClick(object sender, RoutedEventArgs e)
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

        private void LlenarDatosDetallesPedido(string noPedido)
        {
            try
            {
                _pedido = PedidoDAO.ObtenerPedidoPorNoPedido(noPedido);
                campoProveedor.Text = _pedido.nombreProveedor;
                campoEstado.Text = _pedido.nombreEstado;
                datePickerFechaPedido.SelectedDate = _pedido.fechaPedido;
                datePickerFechaEntrega.SelectedDate = _pedido.fechaEntrega;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                InformationControl.Show("Error", "No se pudo acceder a la red de la empresa", "Aceptar");
                this.Close();
        }
    }

        private void CargarProductosProveedor(string rfc)
        {
            /*try
            {
                _productos = new ObservableCollection<Object>(ProductoProveedorDAO.ObtenerListaProductosPorRfc(rfc));
                TablaProductosPedidos.SetItemsSource(_productos);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                InformationControl.Show("Error", "Ocurrió un error al obtener los productos del proveedor", "Aceptar");
                this.Close();
            }*/
        }

    }
}
