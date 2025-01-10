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
using SAMS.UI.DAO;
using SAMS.UI.DTO;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para DetallesMermaView.xaml
    /// </summary>
    public partial class DetallesMermaView : Window
    {
        private int _idMerma;

        public DetallesMermaView(MermaDTO merma)
        {
            InitializeComponent();
            CargarDetalles(merma);
        }

        private void CargarDetalles(MermaDTO merma)
        {
            // Asegúrate de que los controles de la UI existen y están disponibles
            campoProducto.Text = merma.productoInventario;
            campoCantidad.Text = merma.cantidad.ToString();
            campoFechaRegistro.Text = merma.fechaRegistro.ToString("dd/MM/yyyy");
        }

        private void botonCancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
