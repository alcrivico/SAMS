using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para RegistrarProveedorView.xaml
    /// </summary>
    public partial class RegistrarProveedorView : Window
    {
        public RegistrarProveedorView()
        {
            InitializeComponent();
            prepararComponentes();
        }

        private void botonSubirArchivo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Subiendo archivo...");
        }

        private void botonRegistrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Registrando archivo...");
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void prepararComponentes()
        {
            botonRegistrar.IsEnabled = false;
            campoNombre.IsEnabled = false;
            campoCorreo.IsEnabled = false;
            campoTelefono.IsEnabled = false;
        }

        private void campoRfc_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
