using System;
using System.Collections.Generic;
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
using SAMS.UI.DAO;
using SAMS.UI.VisualComponents;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para EditarCategoriasView.xaml
    /// </summary>
    public partial class EditarCategoriasView : Window
    {
        private string _nombre;

        public EditarCategoriasView(string nombre)
        {
            InitializeComponent();
            _nombre = nombre; // Inicializar el nombre original
            LlenarDatosCategoria(nombre);
        }

        private void LlenarDatosCategoria(string nombre)
        {
            try
            {
                campoCategoria.Text = nombre;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                InformationControl.Show("Error", "Ocurrió un error al cargar la información de la categoría.", "Aceptar");
                this.Close();
            }
        }

        private void botonGuardarCambios_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            string nuevoNombre = campoCategoria.Text.Trim();

            try
            {
                // Pasar el nombre original y el nuevo nombre al DAO
                bool edicionExitosa = CategoriaDAO.EditarCategoria(nombre: _nombre, nuevoNombre: nuevoNombre);

                if (edicionExitosa)
                {
                    InformationControl.Show("Información", $"La categoría se actualizó a '{nuevoNombre}'", "Aceptar");
                    this.Close();
                }
                else
                {
                    InformationControl.Show("Advertencia", $"No se pudo editar la categoría. Verifica que '{nuevoNombre}' no exista ya.", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                InformationControl.Show("Error", "Ocurrió un error al guardar los cambios.", "Aceptar");
            }
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void campoCategoria_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void campoCategoria_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            botonGuardarCambios.IsButtonEnabled = !string.IsNullOrWhiteSpace(campoCategoria.Text);
        }
    }
}
