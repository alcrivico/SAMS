using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para DetalleEmpleadoView.xaml
    /// </summary>
    public partial class DetalleEmpleadoView : Window
    {
        V_EmpleadoDetalle _empleado;
        public DetalleEmpleadoView(V_Empleados empleado)
        {
            InitializeComponent();
            CargarDatosEmpleado(empleado.rfc);
        }

        private void botonVolver_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CargarDatosEmpleado(string rfc)
        {
            try
            {
                _empleado = EmpleadoDAO.ObtenerEmpleadoPorRfc(rfc);
                campoNombre.Text = _empleado.nombre;
                campoRfc.Text = _empleado.rfc;
                campoCorreo.Text = _empleado.correo;
                campoTelefono.Text = _empleado.telefono;
                campoNumeroEmpleado.Text = _empleado.noempleado;
                campoPuesto.Text = _empleado.puesto;
                campoApellidoMaterno.Text = _empleado.apellidoMaterno;
                campoApellidoPaterno.Text = _empleado.apellidoPaterno;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                InformationControl.Show("Error", "No se pudo acceder a la red de la empresa", "Aceptar");
                this.Close();
            }
        }
    }
}
