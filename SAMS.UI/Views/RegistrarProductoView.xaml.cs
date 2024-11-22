using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para RegistrarProductoView.xaml
    /// </summary>
    public partial class RegistrarProductoView : Window
    {
        EmpleadoLoginDTO empleado;
        SideBarControl SideBarControl_MenuLateral;

        public RegistrarProductoView(EmpleadoLoginDTO empleado)
        {
            this.empleado = empleado;

            InitializeComponent();

            DefinirColumnasPedidos();
            DefinirColumnasProductos();

            SideBarControl_MenuLateral = new SideBarControl(empleado);
            SideBarControl_MenuLateral.SideElementSelected = 1;
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;
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

        private void Button_Registrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {

        }

        private void DefinirColumnasPedidos()
        {

            Dictionary<string, string>[] columnas =
            {
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "No pedido" },
                    { "Width", "*" },
                    { "BindingName", "noPedido" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Fecha de entrega" },
                    { "Width", "*" },
                    { "BindingName", "fechaEntrega" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Proveedor" },
                    { "Width", "*" },
                    { "BindingName", "nombreProveedor" }

                }
            };

            TableControl_TablaPedidos.DefineColumns(columnas);

        }

        private void DefinirColumnasProductos()
        {

            Dictionary<string, string>[] columnas =
            {
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Código" },
                    { "Width", "*" },
                    { "BindingName", "codigoProducto" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Nombre" },
                    { "Width", "*" },
                    { "BindingName", "nombreProducto" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Cantidad" },
                    { "Width", "*" },
                    { "BindingName", "cantidad" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Precio de compra" },
                    { "Width", "*" },
                    { "BindingName", "precioCompra" }

                }
            };

            TableControl_TablaProductos.DefineColumns(columnas);

        }
    }
}
