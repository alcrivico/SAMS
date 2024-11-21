using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
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
    /// Interaction logic for ActualizarMonederoView.xaml
    /// </summary>
    public partial class ActualizarMonederoView : Window
    {

        MonederoDTO _monedero;

        public ActualizarMonederoView()
        {
            InitializeComponent();
        }

        public ActualizarMonederoView(MonederosDTO monedero)
        {

            InitializeComponent();

            this.DataContext = monedero;

            CargarDatos(monedero);

        }

        private void botonAceptar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
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

    }

}
