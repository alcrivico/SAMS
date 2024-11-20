using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for SideBarControl.xaml
    /// </summary>
    public partial class SideBarControl : UserControl
    {
        private EmpleadoLoginDTO empleado;
        private Window _parentWindow;
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

        public SideBarControl(EmpleadoLoginDTO empleado)
        {
            this.empleado = empleado;
            InitializeComponent();

        }

        private void HomeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            PrincipalView principalView = new(empleado);

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
                        SideElement1.MouseEnter -= SideElement1_MouseEnter;
                        SideElement1.MouseLeave -= SideElement1_MouseLeave;

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
                        SideElement1.MouseEnter += SideElement1_MouseEnter;
                        SideElement1.MouseLeave += SideElement1_MouseLeave;

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
                        SideElement2.MouseEnter -= SideElement2_MouseEnter;
                        SideElement2.MouseLeave -= SideElement2_MouseLeave;

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
                        SideElement2.MouseEnter += SideElement2_MouseEnter;
                        SideElement2.MouseLeave += SideElement2_MouseLeave;

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
                        SideElement3.MouseEnter -= SideElement3_MouseEnter;
                        SideElement3.MouseLeave -= SideElement3_MouseLeave;

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
                        SideElement3.MouseEnter += SideElement3_MouseEnter;
                        SideElement3.MouseLeave += SideElement3_MouseLeave;

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
                        SideElement4.MouseEnter -= SideElement4_MouseEnter;
                        SideElement4.MouseLeave -= SideElement4_MouseLeave;

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
                        SideElement4.MouseEnter += SideElement4_MouseEnter;
                        SideElement4.MouseLeave += SideElement4_MouseLeave;

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
                        SideElement5.MouseEnter -= SideElement5_MouseEnter;
                        SideElement5.MouseLeave -= SideElement5_MouseLeave;

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
                        SideElement5.MouseEnter += SideElement5_MouseEnter;
                        SideElement5.MouseLeave += SideElement5_MouseLeave;

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
                        SideElement6.MouseEnter -= SideElement6_MouseEnter;
                        SideElement6.MouseLeave -= SideElement6_MouseLeave;

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
                        SideElement6.MouseEnter += SideElement6_MouseEnter;
                        SideElement6.MouseLeave += SideElement6_MouseLeave;

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
                        SideElement1.MouseEnter -= SideElement1_MouseEnter;
                        SideElement1.MouseLeave -= SideElement1_MouseLeave;

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
                        SideElement1.MouseEnter += SideElement1_MouseEnter;
                        SideElement1.MouseLeave += SideElement1_MouseLeave;

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
                        SideElement2.MouseEnter -= SideElement2_MouseEnter;
                        SideElement2.MouseLeave -= SideElement2_MouseLeave;

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
                        SideElement2.MouseEnter += SideElement2_MouseEnter;
                        SideElement2.MouseLeave += SideElement2_MouseLeave;

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
                        SideElement3.MouseEnter -= SideElement3_MouseEnter;
                        SideElement3.MouseLeave -= SideElement3_MouseLeave;

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
                        SideElement3.MouseEnter += SideElement3_MouseEnter;
                        SideElement3.MouseLeave += SideElement3_MouseLeave;

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
                        SideElement4.MouseEnter -= SideElement4_MouseEnter;
                        SideElement4.MouseLeave -= SideElement4_MouseLeave;

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
                        SideElement4.MouseEnter += SideElement4_MouseEnter;
                        SideElement4.MouseLeave += SideElement4_MouseLeave;

                    }
                    break;
                case "Paqueteria":

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
                        SideElement1.MouseEnter -= SideElement1_MouseEnter;
                        SideElement1.MouseLeave -= SideElement1_MouseLeave;

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
                        SideElement1.MouseEnter += SideElement1_MouseEnter;
                        SideElement1.MouseLeave += SideElement1_MouseLeave;

                    }

                    if (SideElementSelected == 2)
                    {
                        SetElement(
                            SideElement2,
                            "Proveedores",
                            (ImageSource)FindResource("Icon_ProveedoresActivo"),
                            (Brush)FindResource("SolidColorBrush_PurplePizza"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement2.Cursor = Cursors.Arrow;

                        SideElement2.MouseLeftButtonUp -= SideElement2_MouseLeftButtonUp;
                        SideElement2.MouseEnter -= SideElement2_MouseEnter;
                        SideElement2.MouseLeave -= SideElement2_MouseLeave;

                    }
                    else
                    {
                        SetElement(
                            SideElement2,
                            "Proveedores",
                            (ImageSource)FindResource("Icon_ProveedoresPaqueteriaInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_PurplePizza"));

                        SideElement2.Cursor = Cursors.Hand;

                        SideElement2.MouseLeftButtonUp += SideElement2_MouseLeftButtonUp;
                        SideElement2.MouseEnter += SideElement2_MouseEnter;
                        SideElement2.MouseLeave += SideElement2_MouseLeave;

                    }

                    if (SideElementSelected == 3)
                    {
                        SetElement(
                            SideElement3,
                            "Mermas",
                            (ImageSource)FindResource("Icon_MermasActivo"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement3.Cursor = Cursors.Arrow;

                        SideElement3.MouseLeftButtonUp -= SideElement3_MouseLeftButtonUp;
                        SideElement3.MouseEnter -= SideElement3_MouseEnter;
                        SideElement3.MouseLeave -= SideElement3_MouseLeave;

                    }
                    else
                    {
                        SetElement(
                            SideElement3,
                            "Mermas",
                            (ImageSource)FindResource("Icon_MermasInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"));

                        SideElement3.Cursor = Cursors.Hand;

                        SideElement3.MouseLeftButtonUp += SideElement3_MouseLeftButtonUp;
                        SideElement3.MouseEnter += SideElement3_MouseEnter;
                        SideElement3.MouseLeave += SideElement3_MouseLeave;

                    }

                    if (SideElementSelected == 4)
                    {
                        SetElement(
                            SideElement4,
                            "Categorías",
                            (ImageSource)FindResource("Icon_CategoriasActivo"),
                            (Brush)FindResource("SolidColorBrush_MediumSlateBlue"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement4.Cursor = Cursors.Arrow;

                        SideElement4.MouseLeftButtonUp -= SideElement4_MouseLeftButtonUp;
                        SideElement4.MouseEnter -= SideElement4_MouseEnter;
                        SideElement4.MouseLeave -= SideElement4_MouseLeave;

                    }
                    else
                    {
                        SetElement(
                            SideElement4,
                            "Categorías",
                            (ImageSource)FindResource("Icon_CategoriasInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_MediumSlateBlue"));

                        SideElement4.Cursor = Cursors.Hand;

                        SideElement4.MouseLeftButtonUp += SideElement4_MouseLeftButtonUp;
                        SideElement4.MouseEnter += SideElement4_MouseEnter;
                        SideElement4.MouseLeave += SideElement4_MouseLeave;

                    }

                    if (SideElementSelected == 5)
                    {
                        SetElement(
                            SideElement5,
                            "Pedidos",
                            (ImageSource)FindResource("Icon_PedidosActivo"),
                            (Brush)FindResource("SolidColorBrush_DodgerBlue"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement5.Cursor = Cursors.Arrow;

                        SideElement5.MouseLeftButtonUp -= SideElement5_MouseLeftButtonUp;
                        SideElement5.MouseEnter -= SideElement5_MouseEnter;
                        SideElement5.MouseLeave -= SideElement5_MouseLeave;

                    }
                    else
                    {
                        SetElement(
                            SideElement5,
                            "Pedidos",
                            (ImageSource)FindResource("Icon_PedidosInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_DodgerBlue"));

                        SideElement5.Cursor = Cursors.Hand;

                        SideElement5.MouseLeftButtonUp += SideElement5_MouseLeftButtonUp;
                        SideElement5.MouseEnter += SideElement5_MouseEnter;
                        SideElement5.MouseLeave += SideElement5_MouseLeave;

                    }

                    if (SideElementSelected == 6)
                    {
                        SetElement(
                            SideElement6,
                            "Promociones",
                            (ImageSource)FindResource("Icon_PromoActivo"),
                            (Brush)FindResource("SolidColorBrush_PurplePizza"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement6.Cursor = Cursors.Arrow;

                        SideElement6.MouseLeftButtonUp -= SideElement6_MouseLeftButtonUp;
                        SideElement6.MouseEnter -= SideElement6_MouseEnter;
                        SideElement6.MouseLeave -= SideElement6_MouseLeave;

                    }
                    else
                    {
                        SetElement(
                            SideElement6,
                            "Promociones",
                            (ImageSource)FindResource("Icon_PromoInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_PurplePizza"));

                        SideElement6.Cursor = Cursors.Hand;

                        SideElement6.MouseLeftButtonUp += SideElement6_MouseLeftButtonUp;
                        SideElement6.MouseEnter += SideElement6_MouseEnter;
                        SideElement6.MouseLeave += SideElement6_MouseLeave;

                    }

                    break;
                case "Contador":

                    if (SideElementSelected == 1)
                    {
                        SetElement(
                            SideElement1,
                            "Reporte de Ventas",
                            (ImageSource)FindResource("Icon_ReportesActivo"),
                            (Brush)FindResource("SolidColorBrush_DodgerBlue"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement1.Cursor = Cursors.Arrow;

                        SideElement1.MouseLeftButtonUp -= SideElement1_MouseLeftButtonUp;
                        SideElement1.MouseEnter -= SideElement1_MouseEnter;
                        SideElement1.MouseLeave -= SideElement1_MouseLeave;

                    }
                    else
                    {
                        SetElement(
                            SideElement1,
                            "Reporte de Ventas",
                            (ImageSource)FindResource("Icon_ReportesInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_DodgerBlue"));

                        SideElement1.Cursor = Cursors.Hand;

                        SideElement1.MouseLeftButtonUp += SideElement1_MouseLeftButtonUp;
                        SideElement1.MouseEnter += SideElement1_MouseEnter;
                        SideElement1.MouseLeave += SideElement1_MouseLeave;

                    }

                    if (SideElementSelected == 2)
                    {
                        SetElement(
                            SideElement2,
                            "Reporte de Pedidos",
                            (ImageSource)FindResource("Icon_PedidosActivo"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"),
                            (Brush)FindResource("SolidColorBrush_White"));

                        SideElement2.Cursor = Cursors.Arrow;

                        SideElement2.MouseLeftButtonUp -= SideElement2_MouseLeftButtonUp;
                        SideElement2.MouseEnter -= SideElement2_MouseEnter;
                        SideElement2.MouseLeave -= SideElement2_MouseLeave;

                    }
                    else
                    {
                        SetElement(
                            SideElement2,
                            "Reporte de Pedidos",
                            (ImageSource)FindResource("Icon_ReportePedidosInactivo"),
                            (Brush)FindResource("SolidColorBrush_White"),
                            (Brush)FindResource("SolidColorBrush_PantoneOrange"));

                        SideElement2.Cursor = Cursors.Hand;

                        SideElement2.MouseLeftButtonUp += SideElement2_MouseLeftButtonUp;
                        SideElement2.MouseEnter += SideElement2_MouseEnter;
                        SideElement2.MouseLeave += SideElement2_MouseLeave;

                    }

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

        private void SideElement1_MouseEnter(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 1)
            {
                SideElement1.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_DodgerBlue");
                SideElement1.ElementBorder.BorderThickness = new Thickness(3);
            }


        }

        private void SideElement1_MouseLeave(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 1)
            {
                SideElement1.ElementBorder.BorderThickness = new Thickness(0);
            }

        }

        private void SideElement2_MouseEnter(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 2)
            {

                if (Employee != "Paqueteria")
                {

                    SideElement2.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PantoneOrange");
                    SideElement2.ElementBorder.BorderThickness = new Thickness(3);

                }
                else
                {
                    SideElement2.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PurplePizza");
                    SideElement2.ElementBorder.BorderThickness = new Thickness(3);
                }

            }

        }

        private void SideElement2_MouseLeave(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 2)
            {
                SideElement2.ElementBorder.BorderThickness = new Thickness(0);
            }

        }

        private void SideElement3_MouseEnter(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 3 && Employee != "Contador")
            {

                if (Employee != "Paqueteria")
                {

                    SideElement3.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_MediumSlateBlue");
                    SideElement3.ElementBorder.BorderThickness = new Thickness(3);

                }
                else
                {

                    SideElement3.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PantoneOrange");
                    SideElement3.ElementBorder.BorderThickness = new Thickness(3);

                }

            }

        }

        private void SideElement3_MouseLeave(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 3 && Employee != "Contador")
            {
                SideElement3.ElementBorder.BorderThickness = new Thickness(0);
            }

        }

        private void SideElement4_MouseEnter(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 4 && Employee != "Contador")
            {

                if (Employee != "Paqueteria")
                {

                    SideElement4.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PurplePizza");
                    SideElement4.ElementBorder.BorderThickness = new Thickness(3);

                }
                else
                {

                    SideElement4.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_MediumSlateBlue");
                    SideElement4.ElementBorder.BorderThickness = new Thickness(3);

                }

            }

        }

        private void SideElement4_MouseLeave(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 4 && Employee != "Contador")
            {
                SideElement4.ElementBorder.BorderThickness = new Thickness(0);
            }

        }

        private void SideElement5_MouseEnter(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 5 && (Employee != "Contador" && Employee != "Cajero"))
            {

                if (Employee != "Paqueteria")
                {

                    SideElement5.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PantoneOrange");
                    SideElement5.ElementBorder.BorderThickness = new Thickness(3);

                }
                else
                {

                    SideElement5.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_DodgerBlue");
                    SideElement5.ElementBorder.BorderThickness = new Thickness(3);

                }

            }

        }

        private void SideElement5_MouseLeave(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 5 && (Employee != "Contador" && Employee != "Cajero"))
            {
                SideElement5.ElementBorder.BorderThickness = new Thickness(0);
            }

        }

        private void SideElement6_MouseEnter(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 6 && (Employee != "Contador" && Employee != "Cajero"))
            {

                if (Employee != "Paqueteria")
                {

                    SideElement6.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_DodgerBlue");
                    SideElement6.ElementBorder.BorderThickness = new Thickness(3);

                }
                else
                {

                    SideElement6.ElementBorder.BorderBrush = (Brush)FindResource("SolidColorBrush_PurplePizza");
                    SideElement6.ElementBorder.BorderThickness = new Thickness(3);

                }

            }

        }

        private void SideElement6_MouseLeave(object sender, MouseEventArgs e)
        {

            if (SideElementSelected != 6 && (Employee != "Contador" && Employee != "Cajero"))
            {
                SideElement6.ElementBorder.BorderThickness = new Thickness(0);

            }

        }

        private void SideElement1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = (SideElementControl)sender;

            switch (Employee)
            {
                case "Administrador":
                    VerProductosView principalView = new(empleado);
                    principalView.Show();
                    
                    //Este metodo para cerrar la ventana se debe remplazar
                    Window.GetWindow(this).Close();
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

            SideElementControl element = (SideElementControl)sender;

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

            SideElementControl element = (SideElementControl)sender;

            switch (Employee)
            {
                case "Administrador":
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                default:
                    break;
            }

        }

        private void SideElement4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = (SideElementControl)sender;

            switch (Employee)
            {
                case "Administrador":
                    break;
                case "Cajero":
                    break;
                case "Paqueteria":
                    break;
                default:
                    break;
            }

        }

        private void SideElement5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = (SideElementControl)sender;

            switch (Employee)
            {
                case "Administrador":
                    break;
                case "Paqueteria":
                    break;
                default:
                    break;
            }

        }

        private void SideElement6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SideElementControl element = (SideElementControl)sender;

            switch (Employee)
            {
                case "Administrador":
                    break;
                case "Paqueteria":
                    break;
                default:
                    break;
            }

        }

    }

}
