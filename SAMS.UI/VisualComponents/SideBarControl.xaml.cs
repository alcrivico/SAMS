using SAMS.UI.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for SideBarControl.xaml
    /// </summary>
    public partial class SideBarControl : UserControl
    {

        public string Employee
        {
            get { return (string)GetValue(EmployeeProperty); }
            set { SetValue(EmployeeProperty, value); }
        }

        public static readonly DependencyProperty EmployeeProperty =
            DependencyProperty.Register(
                nameof(Employee),
                typeof(string),
                typeof(SideBarControl),
                new PropertyMetadata(string.Empty, LoadEmployeeMenu));

        public int SideElementSelected
        {
            get { return (int)GetValue(SideElementSelectedProperty); }
            set { SetValue(SideElementSelectedProperty, value); }
        }

        public static readonly DependencyProperty SideElementSelectedProperty =
            DependencyProperty.Register(
                nameof(SideElementSelected),
                typeof(int),
                typeof(SideBarControl),
                new PropertyMetadata(0, LoadEmployeeMenu));

        public SideBarControl()
        {

            InitializeComponent();

        }

        private void HomeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            PrincipalView principalView = new();

            principalView.Show();
            Window.GetWindow(this).Close();

        }

        private void SetMenuByEmployee()
        {

            switch (Employee)
            {
                case "Administrador":
                    
                    if (SideElementSelected == 1)
                    {
                        SetElement(
                            SideElement1,
                            "Productos",
                            (ImageSource)FindResource("Icon_ProductosActivo"),
                            (Brush)FindResource("SolidColorBrush_DodgerBlue"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement1.Cursor = Cursors.Arrow;

                        SideElement1.MouseLeftButtonUp -= SideElement1_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                            SideElement1,
                            "Productos",
                            (ImageSource)FindResource("Icon_ProductosInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_DodgerBlue"));

                        SideElement1.Cursor = Cursors.Hand;

                        SideElement1.MouseLeftButtonUp += SideElement1_MouseLeftButtonUp;

                    }

                    if (SideElementSelected == 2)
                    {
                        SetElement(
                            SideElement2,
                            "Proveedores",
                            (ImageSource)FindResource("Icon_ProveedoresActivo"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement2.Cursor = Cursors.Arrow;

                        SideElement2.MouseLeftButtonUp -= SideElement2_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                            SideElement2,
                            "Proveedores",
                            (ImageSource)FindResource("Icon_ProveedoresInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"));

                        SideElement2.Cursor = Cursors.Hand;

                        SideElement2.MouseLeftButtonUp += SideElement2_MouseLeftButtonUp;

                    }

                    if (SideElementSelected == 3)
                    {
                        SetElement(
                            SideElement3,
                            "Empleados",
                            (ImageSource)FindResource("Icon_EmpleadosActivo"),
                            (Brush)FindResource("SolidColorBrush_MediumSlateBlue"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement3.Cursor = Cursors.Arrow;

                        SideElement3.MouseLeftButtonUp -= SideElement3_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                        SideElement3,
                            "Empleados",
                            (ImageSource)FindResource("Icon_EmpleadosInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_MediumSlateBlue"));

                        SideElement3.Cursor = Cursors.Hand;

                        SideElement3.MouseLeftButtonUp += SideElement3_MouseLeftButtonUp;

                    }

                    if (SideElementSelected == 4)
                    {
                        SetElement(
                            SideElement4,
                            "Ventas",
                            (ImageSource)FindResource("Icon_VentasActivo"),
                            (Brush)FindResource("SolidColorBrush_PurplePizza"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement4.Cursor = Cursors.Arrow;

                        SideElement4.MouseLeftButtonUp -= SideElement4_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                            SideElement4,
                            "Ventas",
                            (ImageSource)FindResource("Icon_VentasInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_PurplePizza"));

                        SideElement4.Cursor = Cursors.Hand;

                        SideElement4.MouseLeftButtonUp += SideElement4_MouseLeftButtonUp;
                    }

                    if (SideElementSelected == 5)
                    {
                        SetElement(
                            SideElement5,
                            "Mermas",
                            (ImageSource)FindResource("Icon_MermasActivo"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement5.Cursor = Cursors.Arrow;

                        SideElement5.MouseLeftButtonUp -= SideElement5_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                            SideElement5,
                            "Mermas",
                            (ImageSource)FindResource("Icon_MermasInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"));

                        SideElement5.Cursor = Cursors.Hand;

                        SideElement5.MouseLeftButtonUp += SideElement5_MouseLeftButtonUp;

                    }

                    if (SideElementSelected == 6)
                    {
                        SetElement(
                            SideElement6,
                                "Reportes",
                                (ImageSource)FindResource("Icon_ReportesActivo"),
                                (Brush)FindResource("SolidColorBrush_DodgerBlue"),
                                (Brush)FindResource("SolidColorBrush_White"));

                        SideElement6.Cursor = Cursors.Arrow;

                        SideElement6.MouseLeftButtonUp -= SideElement6_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                            SideElement6,
                            "Reportes",
                            (ImageSource)FindResource("Icon_ReportesInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_DodgerBlue"));

                        SideElement6.Cursor = Cursors.Hand;

                        SideElement6.MouseLeftButtonUp += SideElement6_MouseLeftButtonUp;

                    }
                    break;
                case "Cajero":
                    if (SideElementSelected == 1)
                    {
                        SetElement(
                            SideElement1,
                            "Registrar Venta",
                            (ImageSource)FindResource("Icon_RegistrarVentaActivo"),
                            (Brush)FindResource("SolidColorBrush_DodgerBlue"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement1.Cursor = Cursors.Arrow;

                        SideElement1.MouseLeftButtonUp -= SideElement1_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                            SideElement1,
                            "Registrar Venta",
                            (ImageSource)FindResource("Icon_RegistrarVentaInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_DodgerBlue"));

                        SideElement1.Cursor = Cursors.Hand;

                        SideElement1.MouseLeftButtonUp += SideElement1_MouseLeftButtonUp;

                    }

                    if (SideElementSelected == 2)
                    {
                        SetElement(
                            SideElement2,
                            "Ver Venta",
                            (ImageSource)FindResource("Icon_VerVentaActivo"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement2.Cursor = Cursors.Arrow;

                        SideElement2.MouseLeftButtonUp -= SideElement2_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                            SideElement2,
                            "Ver Venta",
                            (ImageSource)FindResource("Icon_VerVentaInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"));

                        SideElement2.Cursor = Cursors.Hand;

                        SideElement2.MouseLeftButtonUp += SideElement2_MouseLeftButtonUp;

                    }

                    if (SideElementSelected == 3)
                    {
                        SetElement(
                            SideElement3,
                            "Monederos",
                            (ImageSource)FindResource("Icon_MonederoActivo"),
                            (Brush)FindResource("SolidColorBrush_MediumSlateBlue"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement3.Cursor = Cursors.Arrow;

                        SideElement3.MouseLeftButtonUp -= SideElement3_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                            SideElement3,
                            "Monederos",
                            (ImageSource)FindResource("Icon_MonederoInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_MediumSlateBlue"));

                        SideElement3.Cursor = Cursors.Hand;

                        SideElement3.MouseLeftButtonUp += SideElement3_MouseLeftButtonUp;

                    }

                    if (SideElementSelected == 4)
                    {
                        SetElement(
                            SideElement4,
                            "Cierre de Caja",
                            (ImageSource)FindResource("Icon_CerrarCajaActivo"),
                            (Brush)FindResource("SolidColorBrush_PurplePizza"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement4.Cursor = Cursors.Arrow;

                        SideElement4.MouseLeftButtonUp -= SideElement4_MouseLeftButtonUp;

                    }
                    else
                    {
                        SetElement(
                            SideElement4,
                            "Cierre de Caja",
                            (ImageSource)FindResource("Icon_CerrarCajaInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_PurplePizza"));

                        SideElement4.Cursor = Cursors.Hand;

                        SideElement4.MouseLeftButtonUp += SideElement4_MouseLeftButtonUp;

                    }
                    break;
                case "Paqueteria":

                    break;
                case "Contador":

                    break;
                default:

                    break;
            }

        }

        private void SetElement(SideElementControl element, string menuName, ImageSource iconSource, Brush backgroundColor, Brush foregroundColor)
        {

            element.MenuName = menuName;
            element.IconSource = iconSource;
            element.BackgroundColor = backgroundColor;
            element.ForegroundColor = foregroundColor;

        }

        private static void LoadEmployeeMenu(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (d is SideBarControl sideBarControl)
            {
                sideBarControl.SetMenuByEmployee();
            }

        }

        private void SideElement1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch(Employee)
            {
                case "Administrador": 
                    break;
                case "Cajero":   
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement1_MouseEnter(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 1)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_DodgerBlue");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Cajero":
                    if (SideElementSelected != 1)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_DodgerBlue");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement1_MouseLeave(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 1)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Cajero":
                    if (SideElementSelected != 1)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement2_MouseEnter(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 2)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PantoneOrange");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Cajero":
                    if (SideElementSelected != 2)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PantoneOrange");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement2_MouseLeave(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 2)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Cajero":
                    if (SideElementSelected != 2)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement3_MouseEnter(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 3)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_MediumSlateBlue");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Cajero":
                    if (SideElementSelected != 3)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_MediumSlateBlue");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement3_MouseLeave(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 3)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Cajero":
                    if (SideElementSelected != 3)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement4_MouseEnter(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 4)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PurplePizza");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Cajero":
                    if (SideElementSelected != 4)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PurplePizza");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement4_MouseLeave(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 4)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Cajero":
                    if (SideElementSelected != 4)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement5_MouseEnter(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 5)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PantoneOrange");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement5_MouseLeave(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 5)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement6_MouseEnter(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 6)
                    {
                        element.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_DodgerBlue");
                        element.ElementBorder.BorderThickness = new Thickness(3);
                    }
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

        private void SideElement6_MouseLeave(object sender, MouseEventArgs e)
        {

            SideElementControl element = sender as SideElementControl;

            switch (Employee)
            {
                case "Administrador":
                    if (SideElementSelected != 6)
                    {
                        element.ElementBorder.BorderThickness = new Thickness(0);
                    }
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                case "Contador":
                    break;
                default:
                    break;
            }

        }

    }

}
