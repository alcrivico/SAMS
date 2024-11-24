using Microsoft.VisualBasic;
using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para EditarEmpleadoView.xaml
    /// </summary>
    public partial class EditarEmpleadoView : Window
    {
        private V_EmpleadoDetalle empleado;
        private bool nombreValido = true;
        private bool correoValido = true;
        private bool telefonoValido = true;
        private bool apellidoPaternoValido = true;
        private bool apellidoMaternoValido = true;

        public EditarEmpleadoView(V_Empleados empleado)
        {
            InitializeComponent();
            this.empleado = new V_EmpleadoDetalle();
            this.empleado.rfc = empleado.rfc;
            cargarPuestos();
            cargarDatos();
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void botonGuardar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            empleado.nombre = campoNombre.Text;
            empleado.apellidoPaterno = campoApellidoPaterno.Text;
            empleado.apellidoMaterno = campoApellidoMaterno.Text;
            empleado.correo = campoCorreo.Text;
            empleado.telefono = campoTelefono.Text;
            empleado.puesto = campoPuesto.SelectedItem.ToString();

            try
            {
                EmpleadoDAO.EditarEmpleado(empleado);
                InformationControl.Show("Éxito", "Los datos se han guardado con éxito", "Aceptar");
                this.Close();
            }
            catch (Exception ex)
            {
                InformationControl.Show("No hay conexión a la red", "No se pudo conectar la red del supermercado, inténtelo de nuevo más tarde", "Aceptar");
                this.Close();
            }
        }

        private void cargarDatos()
        {
            empleado = EmpleadoDAO.ObtenerEmpleadoPorRfc(empleado.rfc);

            campoRfc.Text = empleado.rfc;
            campoNombre.Text = empleado.nombre;
            campoApellidoPaterno.Text = empleado.apellidoPaterno;
            campoApellidoMaterno.Text = empleado.apellidoMaterno;
            campoCorreo.Text = empleado.correo;
            campoTelefono.Text = empleado.telefono;
            campoPuesto.SelectedItem = empleado.puesto;
        }

        private void cargarPuestos()
        {
            List<String> puestos = EmpleadoDAO.ObtenerPuestos();
            campoPuesto.SetItemsSource(new ObservableCollection<object>(puestos), "");
        }

        private void ActualizarEstadoBoton()
        {
            bool camposValidos = nombreValido && apellidoPaternoValido && apellidoMaternoValido && correoValido && telefonoValido;
            bool datosNoModificados = campoNombre.Text == empleado.nombre
                && campoApellidoPaterno.Text == empleado.apellidoPaterno
                && campoApellidoMaterno.Text == empleado.apellidoMaterno
                && campoCorreo.Text == empleado.correo
                && campoTelefono.Text == empleado.telefono
                && campoPuesto.SelectedItem.ToString() == empleado.puesto;
            if (camposValidos && !datosNoModificados)
            {
                botonGuardar.IsButtonEnabled = true;
            }
            else
            {
                botonGuardar.IsButtonEnabled = false;
            }
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

        private void campoPuesto_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            ActualizarEstadoBoton();
        }
    }
}
