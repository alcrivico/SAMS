using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ActualizarMonederoView.xaml
    /// </summary>
    public partial class ActualizarMonederoView : Window
    {

        MonederoDTO _monedero;

        public event EventHandler<MonederosDTO> MonederoActualizado;

        public ActualizarMonederoView()
        {
            InitializeComponent();
        }

        public ActualizarMonederoView(MonederosDTO monedero)
        {

            InitializeComponent();

            this.DataContext = monedero;

            CargarDatos(monedero);

        }

        private void botonAceptar_ButtonControlClick(object sender, RoutedEventArgs e)
        {

            try
            {

                if (campoNombre.Text == "" || campoApellidoP.Text == "" || campoApellidoM.Text == "" || campoTelefono.Text == "")
                {
                    InformationControl.Show("Advertencia", "Por favor, llene todos los campos", "Aceptar");
                }
                else if (campoTelefono.Text.Length != 10 || !campoTelefono.Text.All(char.IsDigit))
                {
                    InformationControl.Show("Advertencia", "El formato del teléfono es incorrecto, debe tener 10 dígitos numéricos", "Aceptar");
                }
                else
                {

                    MonederoDTO monedero = new MonederoDTO
                    {
                        nombre = campoNombre.Text,
                        apellidoPaterno = campoApellidoP.Text,
                        apellidoMaterno = campoApellidoM.Text,
                        telefono = campoTelefono.Text,
                        codigoDeBarras = campoCodigoBarras.Text
                    };

                    MonederoDAO.ActualizarMonedero(monedero);
                    InformationControl.Show("Información", "Monedero actualizado correctamente", "Aceptar");

                    MonederoActualizado?.Invoke(this, new MonederosDTO
                    {
                        codigoDeBarras = monedero.codigoDeBarras,
                        telefono = monedero.telefono,
                        nombrePropietario = $"{monedero.nombre} {monedero.apellidoPaterno} {monedero.apellidoMaterno}",
                        saldo = monedero.saldo
                    });

                    this.Close();
                }

            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", "No se pudo conectar a la red del supermercado, inténtelo de nuevo más tarde", "Aceptar");
            }

        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            
            if (
                campoNombre.Text != _monedero.nombre || 
                campoApellidoP.Text != _monedero.apellidoPaterno || 
                campoApellidoM.Text != _monedero.apellidoMaterno || 
                campoTelefono.Text != _monedero.telefono)
            {
                if (ConfirmationControl.Show("Advertencia", "¿Estás seguro de cancelar los cambios?, se perderán los campos actualizados", "Aceptar", "Cancelar"))
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }

        }

        private void CargarDatos(MonederosDTO monedero)
        {
            try
            {

                _monedero = MonederoDAO.ObtenerMonedero(monedero.codigoDeBarras);

                campoNombre.Text = _monedero.nombre;

                campoApellidoP.Text = _monedero.apellidoPaterno;

                campoApellidoM.Text = _monedero.apellidoMaterno;

                campoTelefono.Text = _monedero.telefono;

                campoSaldo.Text = _monedero.saldo.ToString();

                campoCodigoBarras.Text = _monedero.codigoDeBarras;

            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", "No se pudo acceder a la red de la empresa", "Aceptar");
            }

        }

        private void campoNombre_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (campoNombre.Text.Length > 0)
            {
                campoNombre.Text = campoNombre.Text.Substring(0, 1).ToUpper() + campoNombre.Text.Substring(1);
                campoNombre.Text = campoNombre.Text.Trim();
            }

            if (campoNombre.Text.Length != 0 &&
                campoApellidoP.Text.Length != 0 &&
                campoApellidoM.Text.Length != 0 &&
                campoTelefono.Text.Length != 0 &&
                (campoNombre.Text != _monedero.nombre ||
                campoApellidoP.Text != _monedero.apellidoPaterno ||
                campoApellidoM.Text != _monedero.apellidoMaterno ||
                campoTelefono.Text != _monedero.telefono))
            {
                botonAceptar.IsButtonEnabled = true;
            }
            else
            {
                botonAceptar.IsButtonEnabled = false;
            }
        }

        private void campoApellidoP_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (campoApellidoP.Text.Length > 0)
            {
                campoApellidoP.Text = campoApellidoP.Text.Substring(0, 1).ToUpper() + campoApellidoP.Text.Substring(1);
                campoApellidoP.Text = campoApellidoP.Text.Trim();
            }

            if (campoNombre.Text.Length != 0 &&
                campoApellidoP.Text.Length != 0 &&
                campoApellidoM.Text.Length != 0 &&
                campoTelefono.Text.Length != 0 &&
                (campoNombre.Text != _monedero.nombre ||
                campoApellidoP.Text != _monedero.apellidoPaterno ||
                campoApellidoM.Text != _monedero.apellidoMaterno ||
                campoTelefono.Text != _monedero.telefono))
            {
                botonAceptar.IsButtonEnabled = true;
            }
            else
            {
                botonAceptar.IsButtonEnabled = false;
            }

        }

        private void campoApellidoM_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (campoApellidoM.Text.Length > 0)
            {
                campoApellidoM.Text = campoApellidoM.Text.Substring(0, 1).ToUpper() + campoApellidoM.Text.Substring(1);
                campoApellidoM.Text = campoApellidoM.Text.Trim();
            }

            if (campoNombre.Text.Length != 0 &&
                campoApellidoP.Text.Length != 0 &&
                campoApellidoM.Text.Length != 0 &&
                campoTelefono.Text.Length != 0 &&
                (campoNombre.Text != _monedero.nombre ||
                campoApellidoP.Text != _monedero.apellidoPaterno ||
                campoApellidoM.Text != _monedero.apellidoMaterno ||
                campoTelefono.Text != _monedero.telefono))
            {
                botonAceptar.IsButtonEnabled = true;
            }
            else
            {
                botonAceptar.IsButtonEnabled = false;
            }

        }

        private void campoTelefono_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (campoTelefono.Text.Length != 0)
            {
                campoTelefono.Text = campoTelefono.Text.Trim();
            }

            if (campoNombre.Text.Length != 0 &&
                campoApellidoP.Text.Length != 0 &&
                campoApellidoM.Text.Length != 0 &&
                campoTelefono.Text.Length != 0 &&
                (campoNombre.Text != _monedero.nombre ||
                campoApellidoP.Text != _monedero.apellidoPaterno ||
                campoApellidoM.Text != _monedero.apellidoMaterno ||
                campoTelefono.Text != _monedero.telefono))
            {
                botonAceptar.IsButtonEnabled = true;
            }
            else
            {
                botonAceptar.IsButtonEnabled = false;
            }

        }
    }

}
