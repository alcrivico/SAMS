using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para EditarProveedorView.xaml
    /// </summary>
    public partial class EditarProveedorView : Window
    {
        private V_Proveedores proveedor;
        private bool nombreValido = true;
        private bool correoValido = true;
        private bool telefonoValido = true;

        public EditarProveedorView(V_Proveedores proveedor)
        {
            InitializeComponent();
            this.proveedor = proveedor;
            llenarCampos();
        }

        private void botonGuardar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            proveedor.nombre = campoNombre.Text;
            proveedor.correo = campoCorreo.Text;
            proveedor.telefono = campoTelefono.Text;

            try
            {
                ProveedorDAO.EditarProveedor(proveedor);
                InformationControl.Show("Éxito", "Los datos se han guardado con éxito", "Aceptar");
                this.Close();
            }
            catch (Exception ex)
            {
                InformationControl.Show("No hay conexión a la red", "No se pudo conectar la red del supermercado, inténtelo de nuevo más tarde", "Aceptar");
                this.Close();
            }
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void llenarCampos()
        {
            campoRfc.Text = proveedor.rfc;
            campoNombre.Text = proveedor.nombre;
            campoTelefono.Text = proveedor.telefono;
            campoCorreo.Text = proveedor.correo;
            labelEstado.Content = proveedor.estado;
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
            bool camposValidos = nombreValido && correoValido && telefonoValido;
            bool datosNoModificados = campoNombre.Text == proveedor.nombre
                                       && campoCorreo.Text == proveedor.correo
                                       && campoTelefono.Text == proveedor.telefono;

            if (camposValidos && !datosNoModificados)
            {
                botonGuardar.IsButtonEnabled = true;
            }
            else
            {
                botonGuardar.IsButtonEnabled = false;
            }
        }

    }
}
