using Microsoft.Win32;
using SAMS.UI.DAO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para RegistrarProveedorView.xaml
    /// </summary>
    public partial class RegistrarProveedorView : Window
    {
        private string archivoSeleccionado;
        private string[] productos;
        private bool nombreValido = false;
        private bool correoValido = false;
        private bool telefonoValido = false;
        private bool archivoValido = false;
        private bool rfcValido = false;
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
                productos = System.IO.File.ReadAllLines(dialogo.FileName);
                botonSubirArchivoBrush.ImageSource = (DrawingImage)FindResource("Icon_ArchivoCargado");
                archivoValido = true;
                ActualizarEstadoBoton();
            }
        }

        private void botonRegistrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            string rfc = campoRfc.Text.ToUpper();
            string nombre = campoNombre.Text;
            string correo = campoCorreo.Text.ToLower();
            string telefono = campoTelefono.Text;

            Proveedor proveedor = new Proveedor
            {
                rfc = rfc,
                nombre = nombre,
                correo = correo,
                telefono = telefono
            };

            try
            {
                ProveedorDAO.RegistrarProveedorYProductos(proveedor, productos);
                InformationControl.Show("Proveedor registrado", "El registro del proveedor " + campoNombre.Text + " se ha realizado correctamente", "Aceptar");
                this.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                InformationControl.Show("Error al registrar proveedor", "No se pudo conectar a la red del supermercado, inténtelo de nuevo más tarde", "Aceptar");
                this.Close();
            }
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void campoRfc_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            var regexCaracteresEspeciales = new Regex(@"^[a-zA-Z0-9&]+$");

            if (regexCaracteresEspeciales.IsMatch(campoRfc.Text) || string.IsNullOrWhiteSpace(campoNombre.Text))
            {
                mensajeErrorRfc.Visibility = Visibility.Hidden;
            }
            else
            {
                mensajeErrorRfc.Content = "* El RFC solo puede contener letras, números y el carácter '&'";
                mensajeErrorRfc.Visibility = Visibility.Visible;
            }
            // Regex para validar el RFC de persona moral
            var regexRfc = new Regex(@"^[A-Z&]{3}\d{6}[A-Z0-9]{3}$");

            if (campoRfc.Text.Length == 12 && regexRfc.IsMatch(campoRfc.Text))
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
                    campoNombre.Focus();
                    rfcValido = true;
                }
            }
            else
            {
                campoNombre.EnableTextBox = false;
                campoCorreo.EnableTextBox = false;
                campoTelefono.EnableTextBox = false;
                botonSubirArchivo.IsEnabled = false;
                botonRegistrar.IsButtonEnabled = false;
                rfcValido = false;
            }
            ActualizarEstadoBoton();
        }


        private void campoNombre_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            nombreValido = false;
            var regex = new Regex(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\.\-]+$");

            if (string.IsNullOrWhiteSpace(campoNombre.Text))
            {
                mensajeErrorNombre.Content = "* Campo obligatorio";
                mensajeErrorNombre.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            if (!regex.IsMatch(campoNombre.Text))
            {
                mensajeErrorNombre.Content = "* Solo se permiten letras, números, espacios, puntos y guiones.";
                mensajeErrorNombre.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            mensajeErrorNombre.Visibility = Visibility.Hidden;
            nombreValido = true;
            ActualizarEstadoBoton();
        }

        private void campoCorreo_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            correoValido = false;
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            if (string.IsNullOrWhiteSpace(campoCorreo.Text))
            {
                mensajeErrorCorreo.Content = "* Campo obligatorio";
                mensajeErrorCorreo.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            if (!regex.IsMatch(campoCorreo.Text))
            {
                mensajeErrorCorreo.Content = "* Formato de correo inválido";
                mensajeErrorCorreo.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            mensajeErrorCorreo.Visibility = Visibility.Hidden;
            correoValido = true;
            ActualizarEstadoBoton();
        }

        private void campoTelefono_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            telefonoValido = false;
            var regex = new Regex(@"^\d{10}$");

            if (string.IsNullOrWhiteSpace(campoTelefono.Text))
            {
                mensajeErrorTelefono.Content = "* Campo obligatorio";
                mensajeErrorTelefono.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            if (!regex.IsMatch(campoTelefono.Text))
            {
                mensajeErrorTelefono.Content = "* Formato de teléfono inválido";
                mensajeErrorTelefono.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            mensajeErrorTelefono.Visibility = Visibility.Hidden;
            telefonoValido = true;
            ActualizarEstadoBoton();
        }

        private void ActualizarEstadoBoton()
        {
            if(rfcValido && nombreValido && correoValido && telefonoValido && archivoValido)
            {
                botonRegistrar.IsButtonEnabled = true;
            }
            else
            {
                botonRegistrar.IsButtonEnabled = false;
            }
        }

    }
}
