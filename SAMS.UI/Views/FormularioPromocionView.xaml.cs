using SAMS.UI.DAO;
using SAMS.UI.DTO;
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
    /// Lógica de interacción para FormularioPromocionView.xaml
    /// </summary>
    public partial class FormularioPromocionView : Window
    {
        private int productoInventarioId;
        private Window ventanaPrincipal; 
        private EmpleadoLoginDTO empleado;

        public FormularioPromocionView(int productoInventarioId, Window ventanaPrincipal, EmpleadoLoginDTO empleado)
        {
            this.empleado = empleado;
            this.productoInventarioId = productoInventarioId;
            this.ventanaPrincipal = ventanaPrincipal;
            InitializeComponent();
        }

        private async void Button_Registrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(campoNombre.Text) ||
                string.IsNullOrWhiteSpace(campoPorcentajeDescuento.Text) ||
                string.IsNullOrWhiteSpace(campoCantMaxima.Text) ||
                string.IsNullOrWhiteSpace(campoCantMinima.Text) ||
                campofechaInicio.SelectedDate == null ||
                campofechaFin.SelectedDate == null)
            {
                InformationControl.Show("Error", "Todos los campos son obligatorios.", "Aceptar");
                return;
            }

            // Validar formato de números
            if (!int.TryParse(campoPorcentajeDescuento.Text, out int porcentajeDescuento) ||
                !int.TryParse(campoCantMaxima.Text, out int cantMaxima) ||
                !int.TryParse(campoCantMinima.Text, out int cantMinima))
            {
                InformationControl.Show("Error", "Por favor, introduce valores numéricos válidos para Descuento, Cantidad Máxima y Cantidad Mínima.", "Aceptar");
                return;
            }

            // Validar que la fecha de inicio no sea posterior a la fecha de fin
            DateTime fechaInicio = campofechaInicio.SelectedDate.Value;
            DateTime fechaFin = campofechaFin.SelectedDate.Value;

            if (fechaInicio > fechaFin)
            {
                InformationControl.Show("Error", "La fecha de inicio no puede ser posterior a la fecha de finalización.", "Aceptar");
                return;
            }

            CrearPromocionVigenciaDTO promocion = new CrearPromocionVigenciaDTO
            {
                nombre = campoNombre.Text,
                porcentajeDescuento = porcentajeDescuento,
                cantMaxima = cantMaxima,
                cantMinima = cantMinima,
                fechaInicio = fechaInicio,
                fechaFin = fechaFin,
                productoInventarioId = productoInventarioId 
            };

            try
            {
                bool resultado = await PromocionDAO.CrearPromocionConVigencia(promocion);

                if (resultado)
                {
                    InformationControl.Show("Éxito", "Promoción registrada correctamente.", "Aceptar");

                    this.Close();
                    this.ventanaPrincipal.Close();

                    PrincipalView ventanaPrincipal = new(empleado);
                    ventanaPrincipal.Show();
                }
                else
                {
                    InformationControl.Show("Error", "Ocurrió un error al registrar la promoción. Intenta de nuevo.", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                InformationControl.Show("Error", $"Error al registrar la promoción: {ex.Message}", "Aceptar");
            }
        }

        private void Button_Cancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}