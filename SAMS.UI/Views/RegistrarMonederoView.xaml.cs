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
    /// Interaction logic for RegistrarMonederoView.xaml
    /// </summary>
    public partial class RegistrarMonederoView : Window
    {

        MonederoDTO _monedero;

        public event EventHandler<MonederosDTO> MonederoRegistrado;

        public RegistrarMonederoView()
        {

            _monedero = new MonederoDTO();

            InitializeComponent();

            GenerarCodigoDeBarras();

            campoCodigoBarras.Text = _monedero.codigoDeBarras;

        }

        private void botonRegistrar_ButtonControlClick(object sender, RoutedEventArgs e)
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
                    _monedero = new MonederoDTO
                    {
                        nombre = campoNombre.Text,
                        apellidoPaterno = campoApellidoP.Text,
                        apellidoMaterno = campoApellidoM.Text,
                        telefono = campoTelefono.Text,
                        saldo = 0,
                        codigoDeBarras = campoCodigoBarras.Text
                    };

                    MonederoDAO.RegistrarMonedero(_monedero);

                    InformationControl.Show("Éxito", "Monedero registrado correctamente", "Aceptar");

                    MonederoRegistrado?.Invoke(this, new MonederosDTO
                    {
                        nombrePropietario = $"{_monedero.nombre} {_monedero.apellidoPaterno} {_monedero.apellidoMaterno}",
                        telefono = _monedero.telefono,
                        saldo = _monedero.saldo,
                        codigoDeBarras = _monedero.codigoDeBarras
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
                campoNombre.Text.Length == 0 ||
                campoApellidoP.Text.Length == 0 ||
                campoApellidoM.Text.Length == 0 ||
                campoTelefono.Text.Length == 0)
            {
                if (ConfirmationControl.Show("Advertencia", "¿Estás seguro de cancelar el registro?, se perderán los campos llenados", "Aceptar", "Cancelar"))
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }

        }


        private void GenerarCodigoDeBarras()
        {

            try
            {
                MonederoDAO.GenerarCodigoDeBarras(_monedero);
            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", "No se pudo conectar a la red del supermercado, inténtelo de nuevo más tarde", "Aceptar");
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
                campoTelefono.Text.Length != 0)
            {
                botonRegistrar.IsButtonEnabled = true;
            }
            else
            {
                botonRegistrar.IsButtonEnabled = false;
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
                campoTelefono.Text.Length != 0)
            {
                botonRegistrar.IsButtonEnabled = true;
            }
            else
            {
                botonRegistrar.IsButtonEnabled = false;
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
                campoTelefono.Text.Length != 0)
            {
                botonRegistrar.IsButtonEnabled = true;
            }
            else
            {
                botonRegistrar.IsButtonEnabled = false;
            }

        }

        private void campoTelefono_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

            if (campoNombre.Text.Length != 0 &&
                campoApellidoP.Text.Length != 0 &&
                campoApellidoM.Text.Length != 0 &&
                campoTelefono.Text.Length != 0)
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
