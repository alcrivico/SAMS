using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
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
    /// Interaction logic for PrincipalView.xaml
    /// </summary>
    public partial class PrincipalView : Window
    {
        private EmpleadoLoginDTO empleado;

        //Este constructor no deberia existir
        public PrincipalView()
        {
            InitializeComponent();
        }

        public PrincipalView(EmpleadoLoginDTO empleado)
        {
            InitializeComponent();
            this.empleado = empleado;
            //SideBarControl_MenuLateral.Employee = empleado.TipoEmpleado;
            SideBarControl_MenuLateral.Employee = "Administrador";
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

            IniciarSesionView iniciarSesionView = new IniciarSesionView();

            iniciarSesionView.Show();
            this.Close();

        }

    }

}
