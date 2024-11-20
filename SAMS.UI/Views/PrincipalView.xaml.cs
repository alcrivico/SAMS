using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Globalization;
using System.Resources;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Interaction logic for PrincipalView.xaml
    /// </summary>
    public partial class PrincipalView : Window
    {
        private EmpleadoLoginDTO empleado;
        private SideBarControl SideBarControl_MenuLateral;

        //Este constructor no deberia existir
        public PrincipalView()
        {
            InitializeComponent();
        }

        public PrincipalView(EmpleadoLoginDTO empleado)
        {
            InitializeComponent();
            this.empleado = empleado;
            
            
            SideBarControl_MenuLateral = new SideBarControl(empleado);
            MenuLateral.Children.Add(SideBarControl_MenuLateral);  
            SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;
            TextBlock_MensajeBienvenida.Text = "Bienvenid@, "
                    + empleado.nombreEmpleado + " "
                    + empleado.apellidoPaterno + " "
                    + empleado.apellidoMaterno;
            CargarImagenPrincipal();
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

        private void CerrarSesionLogo_MouseEnter(object sender, MouseEventArgs e)
        {
            CerrarSesionLogo.Width = 60;
            CerrarSesionLogo.Height = 60;
            ImageBrush_CerrarSesionLogo.ImageSource = FindResource("Icon_DoorOpen") as ImageSource;
        }

        private void CerrarSesionLogo_MouseLeave(object sender, MouseEventArgs e)
        {
            CerrarSesionLogo.Width = 30;
            CerrarSesionLogo.Height = 30;
            ImageBrush_CerrarSesionLogo.ImageSource = FindResource("Icon_DoorClosed") as ImageSource;
        }

        private void CerrarSesionLogo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool result = ConfirmationControl.Show(
                "Confirmación",
                "¿Estás seguro de cerrar sesión?",
                "Aceptar",
                "Cancelar"
            );

            if (result)
            {
                IniciarSesionView iniciarSesionView = new IniciarSesionView();
                iniciarSesionView.Show();
                this.Close();
            }

        }

        private void CargarImagenPrincipal()
        {
            if (SideBarControl_MenuLateral.Employee == "Administrador")
            {
                ViewBox_ImagenPrincipal.Source = new BitmapImage(
                        new Uri("../Resources/Images/PrincipalAdministrador.png",
                        UriKind.RelativeOrAbsolute));
            }
            else if (SideBarControl_MenuLateral.Employee == "Cajero")
            {
                ViewBox_ImagenPrincipal.Source = new BitmapImage(
                        new Uri("../Resources/Images/PrincipalCajero.png",
                        UriKind.RelativeOrAbsolute));
            }
            else if (SideBarControl_MenuLateral.Employee == "Paqueteria")
            {
                ViewBox_ImagenPrincipal.Source = new BitmapImage(
                        new Uri("../Resources/Images/PrincipalPaqueteria.png",
                        UriKind.RelativeOrAbsolute));
            }
            else if (SideBarControl_MenuLateral.Employee == "Contador")
            {
                ViewBox_ImagenPrincipal.Source = new BitmapImage(
                        new Uri("../Resources/Images/PrincipalContador.png",
                        UriKind.RelativeOrAbsolute));
            }
        }
    }

}
