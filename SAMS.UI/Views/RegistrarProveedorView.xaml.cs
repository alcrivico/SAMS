using Microsoft.Win32;
using SAMS.UI.DAO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para RegistrarProveedorView.xaml
    /// </summary>
    public partial class RegistrarProveedorView : Window
    {
        private string archivoSeleccionado;
        public RegistrarProveedorView()
        {
            InitializeComponent();
            campoRfc.Focus();
        }

        private void botonSubirArchivo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog
            {
                Filter = "Archivos CSV (*.csv)|*.csv",
                Title = "Seleccionar archivo CSV"
            };

            if (dialogo.ShowDialog() == true)
            {
                archivoSeleccionado = dialogo.FileName;
            }
        }

        private void botonRegistrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            string rfc = campoRfc.Text.ToUpper();
            string nombre = campoNombre.Text;
            string correo = campoCorreo.Text.ToLower();
            string telefono = campoTelefono.Text;

            ProveedorDAO.RegistrarProveedor(new Proveedor
            {
                rfc = rfc,
                nombre = nombre,
                correo = correo,
                telefono = telefono,
                estadoProveedor = true
            });

            registrarProductos(archivoSeleccionado);
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void campoRfc_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (campoRfc.Text.Length == 12)
            {
                if (ProveedorDAO.ObtenerProveedorPorRfc(campoRfc.Text) != null)
                {
                    InformationControl.Show("RFC registrado", "El RFC ingresado ya se encuentra registrado", "Aceptar");
                    campoRfc.Text = "";
                    campoRfc.Focus();
                }
                else
                {
                    campoNombre.EnableTextBox = true;
                    campoCorreo.EnableTextBox = true;
                    campoTelefono.EnableTextBox = true;
                    botonSubirArchivo.IsEnabled = true;
                    botonRegistrar.IsButtonEnabled = true;
                    campoNombre.Focus();
                }
            }
        }

        private void registrarProductos(string pathArchivo)
        {
            List<Producto> productos = new List<Producto>();
            string[] lineas = System.IO.File.ReadAllLines(pathArchivo);
            try
            {
                foreach (string linea in lineas)
                {
                    string[] campos = linea.Split(',');
                    Debug.WriteLine(campos);
                    ProductoProveedorDAO.RegistrarProducto(campos[0], campos[1], campos[2].ToLower(), campos[3].ToLower(), campos[4], campoRfc.Text, campos[5]);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                InformationControl.Show("Error al registrar productos", "Ocurrió un error al registrar los productos", "Aceptar");
            }
        }
    }
}
