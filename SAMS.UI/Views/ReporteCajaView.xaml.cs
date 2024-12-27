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
    /// Interaction logic for ReporteCajaView.xaml
    /// </summary>
    public partial class ReporteCajaView : Window
    {

        public event EventHandler? SalirClicked;

        public ReporteCajaView(String noCaja, int noVentas, decimal totalEfectivo, decimal totalTarjeta, decimal totalMonedero, decimal total, decimal diferencia, string responsable)
        {

            InitializeComponent();

            CargarDatos(noCaja, noVentas, totalEfectivo, totalTarjeta, totalMonedero, total, diferencia, responsable);

        }

        private void CargarDatos(String noCaja, int noVentas, decimal totalEfectivo, decimal totalTarjeta, decimal totalMonedero, decimal total, decimal diferencia, string responsable)
        {

            campoNoCaja.Text = noCaja;
            campoFechaCierre.Text = DateTime.Now.ToString("dd/MM/yyyy");
            campoNoVentas.Text = noVentas.ToString();
            campoTotalEfectivo.Text = totalEfectivo.ToString();
            campoTotalTarjeta.Text = totalTarjeta.ToString();
            campoTotalMonedero.Text = totalMonedero.ToString();
            campoTotal.Text = total.ToString();
            campoDiferencias.Text = diferencia.ToString();
            campoResponsable.Text = responsable;

        }

        private void botonSalir_ButtonControlClick(object sender, RoutedEventArgs e)
        {

            SalirClicked?.Invoke(this, EventArgs.Empty);

            this.Close();

        }

    }
}
