using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Windows;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Interaction logic for ConsultarMonederoView.xaml
    /// </summary>
    public partial class ConsultarMonederoView : Window
    {
        MonederoDTO _monedero;

        public ConsultarMonederoView()
        {
            InitializeComponent();
        }

        public ConsultarMonederoView(MonederosDTO monedero)
        {

            InitializeComponent();

            this.DataContext = monedero;

            CargarDatos(monedero);

        }

        private void CargarDatos(MonederosDTO monedero)
        {

            try
            {

                _monedero = MonederoDAO.ObtenerMonedero(monedero.codigoDeBarras);

                campoNombre.Text = _monedero.nombre;

                campoApellidoP.Text = _monedero.apellidoPaterno;

                campoApellidoM.Text = _monedero.apellidoMaterno;

                campoTelefono.Text = _monedero.telefono;

                campoSaldo.Text = _monedero.saldo.ToString();

                campoCodigoBarras.Text = _monedero.codigoDeBarras;

            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", "No se pudo acceder a la red de la empresa", "Aceptar");
            }

        }

        private void botonSalir_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

}
