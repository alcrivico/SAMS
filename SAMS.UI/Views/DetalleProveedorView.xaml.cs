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
    /// Lógica de interacción para DetalleProveedorView.xaml
    /// </summary>
    public partial class DetalleProveedorView : Window
    {
        public DetalleProveedorView()
        {
            InitializeComponent();
            DefinirColumnas();
        }

        private void botonVolver_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DefinirColumnas()
        {
            Dictionary<string, string>[] columnas =
            {
                new Dictionary<string, string>
                {
                    {"Type", "Text"},
                    {"Name", "Producto"},
                    {"Width", "*"},
                    {"BindingName", "nombre" }
                },
                new Dictionary<string, string>
                {
                    {"Type", "Text"},
                    {"Name", "Código"},
                    {"Width", "*"},
                    {"BindingName", "codigo" }
                },
                new Dictionary<string, string>
                {
                    {"Type", "Text"},
                    {"Name", "Descripción"},
                    {"Width", "*"},
                    {"BindingName", "descripcion" }
                }     
            };

            TablaProductosProveedor.DefineColumns(columnas);
        }
    }
}
