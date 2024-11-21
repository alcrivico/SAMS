using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para DetalleProveedorView.xaml
    /// </summary>
    public partial class DetalleProveedorView : Window
    {
        V_Proveedores _proveedor;
        ObservableCollection<Object> _productos;

        public DetalleProveedorView(V_Proveedores proveedor)
        {
            _proveedor = new V_Proveedores();
            _productos = new ObservableCollection<Object>();

            InitializeComponent();
            DefinirColumnas();
            CargarDatosProveedor(proveedor.rfc);
            CargarProductosProveedor(_proveedor.rfc);
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
                    {"BindingName", "nombre" }
                },
                new Dictionary<string, string>
                {
                    {"Type", "Text"},
                    {"Name", "Código"},
                    {"Width", "*"},
                    {"BindingName", "codigo" }
                },
                new Dictionary<string, string>
                {
                    {"Type", "Text"},
                    {"Name", "Descripción"},
                    {"Width", "*"},
                    {"BindingName", "descripcion" }
                }     
            };

            TablaProductosProveedor.DefineColumns(columnas);
        }

        private void CargarDatosProveedor(string rfc)
        {
            try
            {
                _proveedor = ProveedorDAO.ObtenerProveedorPorRfc(rfc);
                campoNombre.Text = _proveedor.nombre;
                campoRFC.Text = _proveedor.rfc;
                campoCorreo.Text = _proveedor.correo;
                campoTelefono.Text = _proveedor.telefono;
                campoEstado.Text = _proveedor.estado;
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
            try
            {
                _productos = new ObservableCollection<Object>(ProductoProveedorDAO.ObtenerListaProductosPorRfc(rfc));
                TablaProductosProveedor.SetItemsSource(_productos);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                InformationControl.Show("Error", "Ocurrió un error al obtener los productos del proveedor", "Aceptar");
                this.Close();
            }
        }
    }
}
