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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Interaction logic for IniciarSesionView.xaml
    /// </summary>
    public partial class IniciarSesionView : Window
    {
        public IniciarSesionView()
        {
            InitializeComponent();
        }

        private void TitleBarControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (e.OriginalSource is FrameworkElement element &&
                (element.Name == "MinusLogo" ||
                element.Name == "MaximizeLogo" ||
                element.Name == "ExitLogo"))
            {
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void TitleBarControl_WindowStateChangeRequested(object sender, WindowState e)
        {
            this.WindowState = e;
        }

        private void Button_Entrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            if (!ValidarCredenciales())
            {
                MostrarMensajeErrorDatosInvalidos();
            }
            else
            {
                AlertaBorder.Visibility = Visibility.Hidden;
                string correo = TextBoxControl_Email.Text.Trim();
                string password = PasswordBoxControl.PasswordText;
                AutenticarEmpleado(correo, password);
            }
        }

        private void MostrarMensajeErrorDatosInvalidos()
        {
            ActualizarAlerta(
                mensaje: "Correo y/o contraseña incorrectos. Por favor, verifíquelos.",
                colorFondo: (Brush)Application.Current.Resources["SolidColorBrush_Gold"],
                colorTexto: (Brush)Application.Current.Resources["SolidColorBrush_EerieBlack"]
            );
        }

        private void MostrarMensajeErrorConexion()
        {
            ActualizarAlerta(
                mensaje: "No se pudo conectar a la red del supermercado\n Inténtelo de nuevo más tarde.",
                colorFondo: (Brush)Application.Current.Resources["SolidColorBrush_RustyRed"],
                colorTexto: (Brush)Application.Current.Resources["SolidColorBrush_White"]
            );
        }

        private void ActualizarAlerta(string mensaje, Brush colorFondo, Brush colorTexto)
        {
            AlertaBorder.Background = colorFondo;
            AlertTextBlock_Alerta.Foreground = colorTexto;
            AlertTextBlock_Alerta.Text = mensaje;

            AlertaBorder.Visibility = Visibility.Visible;
        }

        private bool ValidarCredenciales()
        {
            string email = TextBoxControl_Email.Text.Trim();
            string password = PasswordBoxControl.PasswordText;

            // Validar correo
            if (string.IsNullOrEmpty(email) 
                    || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") 
                    || email.Length < 5)
                return false;

            // Validar password
            if (string.IsNullOrEmpty(password) || password.Length < 6)
                return false;

            return true;
        }

        private void AutenticarEmpleado(string correo, string contraseña)
        {
            try
            {
                EmpleadoLoginDTO empleadoAutenticado = EmpleadoDAO.LogInEmpleado(correo, contraseña);

                // Verifica si el empleado fue encontrado
                if (empleadoAutenticado == null)
                {
                    MostrarMensajeErrorDatosInvalidos();
                }else
                {
                    PrincipalView principalView = new PrincipalView(empleadoAutenticado);
                    principalView.Show();
                    this.Close();

                }

            }
            catch (Exception ex)
            {
                MostrarMensajeErrorConexion();
            }
        }
    }
}
