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
using SAMS.UI.Models.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Interaction logic for PrincipalView.xaml
    /// </summary>
    public partial class PrincipalView : Window
    {

        private readonly SAMSContext _sams;

        public PrincipalView()
        {

            InitializeComponent();

            Debug.WriteLine("No hay inicializado la conexión");

            _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

            Debug.WriteLine("Ya inicializo la conexión");

            SAMSContextProcedure DAO = new SAMSContextProcedure(_sams);

            DateTime fechaInicio = DateTime.Now;

            DateTime fechaFin = DateTime.Now.AddDays(11);

            Task<bool> response = DAO.CrearPromocionConVigencia("Mal Fin", 90, 5, 1, fechaInicio, fechaFin, 2);

            //InformationControl.Show("Base de Datos", response.Result.ToString(), "Aceptar");

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
