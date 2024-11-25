using SAMS.UI.DTO;
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
    /// Interaction logic for MenuReportes.xaml
    /// </summary>
    public partial class MenuReportes : Window
    {
        
        EmpleadoLoginDTO _empleado;

        public MenuReportes()
        {
            InitializeComponent();
        }

        public MenuReportes(EmpleadoLoginDTO empleado)
        {
            
            _empleado = empleado;

            InitializeComponent();

            SideBarControl sideBarControl = new SideBarControl(_empleado);
            sideBarControl.Employee = _empleado.tipoEmpleado;
            sideBarControl.SideElementSelected = 6;

            MenuLateral.Children.Add(sideBarControl);

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

        private void ReporteInventario_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReporteInventarioView reporteInventarioView = new ReporteInventarioView(_empleado);
            reporteInventarioView.Show();

            this.Close();
        }

        private void ReportePedidos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReportePedidosView reportePedidosView = new ReportePedidosView(_empleado);
            reportePedidosView.Show();

            this.Close();
        }

        private void ReporteVentas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReporteVentasView reporteVentasView = new ReporteVentasView(_empleado);
            reporteVentasView.Show();

            this.Close();
        }

    }

}
