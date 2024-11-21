using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Interaction logic for VerMonederosView.xaml
    /// </summary>
    public partial class VerMonederosView : Window
    {

        Monedero _monedero;
        List<MonederosDTO> listaMonederos;
        ObservableCollection<Object> _monederos;

        public VerMonederosView(EmpleadoLoginDTO empleado)
        {

            _monedero = new Monedero();
            _monederos = new ObservableCollection<Object>();

            InitializeComponent();

            SideBarControl sideBarControl = new SideBarControl(empleado);
            sideBarControl.Employee = empleado.tipoEmpleado;
            sideBarControl.SideElementSelected = 3;
            MenuLateral.Children.Add(sideBarControl);

            DefinirColumnas();

            ObtenerMonederos();

            TablaMonederos.OnDetallesClickedHandler += botonDetallesClick;
            TablaMonederos.OnEditarClickedHandler += botonEditarClick;
            TablaMonederos.OnEliminarClickedHandler += botonEliminarClick;

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

        private void DefinirColumnas()
        {

            Dictionary<string, string>[] columnas =
            {
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Código de Barras" },
                    { "Width", "*" },
                    { "BindingName", "codigoDeBarras" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Teléfono" },
                    { "Width", "*" },
                    { "BindingName", "telefono" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Nombre del Propietario" },
                    { "Width", "*" },
                    { "BindingName", "nombrePropietario" }

                },
                new Dictionary<string, string> {

                    { "Type", "Actions" },
                    { "Name", "Acciones" },
                    { "Width", "*" },
                    { "Detalles", "True" },
                    { "Editar", "True" },
                    { "Eliminar", "False" }

                }

            };

            TablaMonederos.DefineColumns(columnas);

        }

        private void ObtenerMonederos()
        {

            try
            {

                listaMonederos = MonederoDAO.ObtenerMonederos();

                _monederos.Clear();

                _monederos = new ObservableCollection<Object>(listaMonederos);

                TablaMonederos.SetItemsSource(_monederos);

            }
            catch (Exception ex)
            {

                InformationControl.Show("Error", "Ocurrió un error al obtener los monederos", "Aceptar");

                this.Close();

            }

        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (listaMonederos != null)
            {

                if (campoBuscar.Text.Length > 0)
                {
                    var monederosFiltrados = listaMonederos.Where(
                        m =>
                        m.codigoDeBarras.Contains(campoBuscar.Text) ||
                        m.telefono.Contains(campoBuscar.Text) ||
                        m.nombrePropietario.ToUpper().Contains(campoBuscar.Text.ToUpper())).ToList();//ToUpper() para que no sea case sensitive

                    _monederos.Clear();

                    _monederos = new ObservableCollection<Object>(monederosFiltrados);

                    TablaMonederos.SetItemsSource(_monederos);

                }
                else
                {

                    _monederos.Clear();

                    _monederos = new ObservableCollection<Object>(listaMonederos);

                    TablaMonederos.SetItemsSource(_monederos);

                }

            }

        }

        private void botonDetallesClick(object sender, RoutedEventArgs e)
        {

            ActionsControl actionBar = (ActionsControl)sender;
            MonederosDTO monedero = (MonederosDTO)actionBar.DataContext;

            ConsultarMonederoView consultarMonederoView = new ConsultarMonederoView(monedero);
            consultarMonederoView.Show();

        }

        private void botonEditarClick(object sender, RoutedEventArgs e)
        {

            ActionsControl actionBar = (ActionsControl)sender;
            MonederosDTO mondero = (MonederosDTO)actionBar.DataContext;

            ActualizarMonederoView actualizarMonederoView = new ActualizarMonederoView(mondero);
            actualizarMonederoView.Show();

        }

        private void botonEliminarClick(object sender, RoutedEventArgs e)
        {

            ActionsControl actionBar = (ActionsControl)sender;
            MonederosDTO monedero = (MonederosDTO)actionBar.DataContext;

        }

    }

}