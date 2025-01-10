using SAMS.UI.DAO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
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

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para RegistrarCategoriaView.xaml
    /// </summary>
    public partial class RegistrarCategoriaView : Window
    {
        public RegistrarCategoriaView()
        {
            InitializeComponent();
        }

        private void botonRegistrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombreCategoria = campoCategoria.Text.Trim();

                bool registroExitoso = CategoriaDAO.RegistrarCategoria(nombreCategoria);

                if (registroExitoso)
                {
                    InformationControl.Show("Información", $"Categoría '{nombreCategoria}' registrada exitosamente.", "Aceptar");
                }
                else
                {
                    InformationControl.Show("Advertencia", $"La categoría '{nombreCategoria}' ya existe.", "Aceptar");
                }

                campoCategoria.Text = string.Empty;
                this.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                InformationControl.Show("Error", "Ocurrió un error al registrar la categoría.", "Aceptar");
                this.Close();
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
            botonRegistrar.IsButtonEnabled = !string.IsNullOrWhiteSpace(campoCategoria.Text);
        }
    }
}
