using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para RegistrarEmpleadoView.xaml
    /// </summary>
    public partial class RegistrarEmpleadoView : Window
    {

        private bool nombreValido = false;
        private bool apellidoPaternoValido = false;
        private bool apellidoMaternoValido = false;
        private bool correoValido = false;
        private bool rfcValido = false;
        private bool telefonoValido = false;

        public RegistrarEmpleadoView()
        {
            InitializeComponent();
            cargarPuestos();
            campoRfc.Focus();            
        }

        private void cargarPuestos()
        {
            List<String> puestos = EmpleadoDAO.ObtenerPuestos();
            campoPuesto.SetItemsSource(new ObservableCollection<Object>(puestos), "");
        }

        private void botonRegistrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            string rfc = campoRfc.Text.ToUpper().Trim();
            string nombre = campoNombre.Text.Trim();
            string apellidoPaterno = campoApellidoPaterno.Text.Trim();
            string apellidoMaterno = campoApellidoMaterno.Text.Trim();
            string correo = campoCorreo.Text.ToLower().Trim();
            string telefono = campoTelefono.Text.Trim();
            string puesto = campoPuesto.SelectedItem.ToString();

            V_EmpleadoDetalle empleado = new V_EmpleadoDetalle
            {
                rfc = rfc,
                nombre = nombre,
                apellidoPaterno = apellidoPaterno,
                apellidoMaterno = apellidoMaterno,
                correo = correo,
                telefono = telefono,
                puesto = puesto
            };

            try
            {
                EmpleadoDAO.RegistrarEmpleado(empleado);
                InformationControl.Show("Éxito", "El empleado se ha realizado exitosamente", "Aceptar");
                this.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                InformationControl.Show("Error al registrar empleado", "No se pudo conectar a la red del supermercado, inténtelo de nuevo más tarde", "Aceptar");
                this.Close();
            }
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void campoRfc_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            var regexCaracteresEspeciales = new Regex(@"^[a-zA-Z0-9]+$");

            if (regexCaracteresEspeciales.IsMatch(campoRfc.Text) || string.IsNullOrWhiteSpace(campoNombre.Text))
            {
                mensajeErrorRfc.Visibility = Visibility.Hidden;
            }
            else
            {
                mensajeErrorRfc.Content = "* El RFC solo puede contener letras y números";
                mensajeErrorRfc.Visibility = Visibility.Visible;
            }
            // Regex para validar el RFC de persona moral
            var regexRfc = new Regex(@"^[A-Z&]{4}\d{6}[A-Z0-9]{3}$");

            if (campoRfc.Text.Length == 13 && regexRfc.IsMatch(campoRfc.Text.ToUpper()))
            {
                if (EmpleadoDAO.ObtenerEmpleadoPorRfc(campoRfc.Text) != null)
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
                    campoPuesto.IsEnabled = true;
                    campoApellidoPaterno.EnableTextBox = true;
                    campoApellidoMaterno.EnableTextBox = true;
                    campoNombre.Focus();
                    rfcValido = true;
                }
            }
            else
            {
                campoNombre.EnableTextBox = false;
                campoCorreo.EnableTextBox = false;
                campoTelefono.EnableTextBox = false;
                campoApellidoPaterno.EnableTextBox = false;
                campoApellidoMaterno.EnableTextBox = false;
                campoPuesto.IsEnabled = false;
                botonRegistrar.IsButtonEnabled = false;
                rfcValido = false;
            }
            ActualizarEstadoBoton();
        }

        private void campoNombre_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            nombreValido = false;
            var regex = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");

            if (string.IsNullOrWhiteSpace(campoNombre.Text))
            {
                mensajeErrorNombre.Content = "* Campo obligatorio";
                mensajeErrorNombre.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            if (!regex.IsMatch(campoNombre.Text))
            {
                mensajeErrorNombre.Content = "* No se permiten numeros ni caracteres especiales";
                mensajeErrorNombre.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            mensajeErrorNombre.Visibility = Visibility.Hidden;
            nombreValido = true;
            ActualizarEstadoBoton();
        }

        private void campoApellidoPaterno_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            apellidoPaternoValido = false;
            var regex = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");

            if (string.IsNullOrWhiteSpace(campoApellidoPaterno.Text))
            {
                mensajeErrorApellidoPaterno.Content = "* Campo obligatorio";
                mensajeErrorApellidoPaterno.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            if (!regex.IsMatch(campoApellidoPaterno.Text))
            {
                mensajeErrorApellidoPaterno.Content = "* No se permiten numeros ni caracteres especiales";
                mensajeErrorApellidoPaterno.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            mensajeErrorApellidoPaterno.Visibility = Visibility.Hidden;
            apellidoPaternoValido = true;
            ActualizarEstadoBoton();
        }

        private void campoApellidoMaterno_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            apellidoMaternoValido = false;
            var regex = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");

            if (string.IsNullOrWhiteSpace(campoApellidoMaterno.Text))
            {
                mensajeErrorApellidoMaterno.Content = "* Campo obligatorio";
                mensajeErrorApellidoMaterno.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            if (!regex.IsMatch(campoApellidoMaterno.Text))
            {
                mensajeErrorApellidoMaterno.Content = "* No se permiten numeros ni caracteres especiales";
                mensajeErrorApellidoMaterno.Visibility = Visibility.Visible;
                ActualizarEstadoBoton();
                return;
            }

            mensajeErrorApellidoMaterno.Visibility = Visibility.Hidden;
            apellidoMaternoValido = true;
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
            if (rfcValido 
                && nombreValido 
                && apellidoMaternoValido 
                && apellidoPaternoValido 
                && correoValido 
                && telefonoValido)
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
