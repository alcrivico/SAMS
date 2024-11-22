using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Windows;

namespace SAMS.UI.Views;

/// <summary>
/// Lógica de interacción para FormularioPromocionView.xaml
/// </summary>
public partial class FormularioPromocionView : Window
{
    private int productoInventarioId;
    private Window ventanaPrincipal; 
    private EmpleadoLoginDTO empleado;
    private EditarPromocionDTO editarPromocion;

    public FormularioPromocionView(
            int productoInventarioId, 
            Window ventanaPrincipal, 
            EmpleadoLoginDTO empleado, 
            EditarPromocionDTO? editarPromocionDTO = null)
    {
        this.empleado = empleado;
        this.productoInventarioId = productoInventarioId;
        this.ventanaPrincipal = ventanaPrincipal;
        

        InitializeComponent();

        editarPromocion = editarPromocionDTO;

        if (editarPromocion != null)
        {
            DefinirDatos();
        }
    }

    private async void Button_Registrar_ButtonControlClick(object sender, RoutedEventArgs e)
    {
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
        Close();
    }

    private void DefinirDatos()
    {
        campoNombre.Text = editarPromocion.nombre;
        campoPorcentajeDescuento.Text = editarPromocion.porcentajeDescuento.ToString();
        campoCantMaxima.Text = editarPromocion.cantMaxima.ToString();
        campoCantMinima.Text = editarPromocion.cantMinima.ToString();
        campofechaInicio.SelectedDate = editarPromocion.fechaInicio;
        campofechaFin.SelectedDate = editarPromocion.fechaFin;

        Button_Registrar.Text = "Actualizar";
    }

    private void validarCampo_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
    {
        Button_Registrar.IsEnabled = ValidarCampos();
    }

    private void campoPorcentajeDescuento_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
    {
        Button_Registrar.IsEnabled = ValidarCampos();
    }

    private void campoCantMaxima_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
    {
        Button_Registrar.IsEnabled = ValidarCampos();
    }

    private void campoCantMinima_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
    {
        Button_Registrar.IsButtonEnabled = ValidarCampos();
    }

    private bool ValidarCampos()
    {
        return campoNombre.Text.Length > 0 &&
            campoPorcentajeDescuento.Text.Length > 0 &&
            campoCantMaxima.Text.Length > 0 &&
            campoCantMinima.Text.Length > 0 &&
            campofechaInicio.SelectedDate != null &&
            campofechaFin.SelectedDate != null &&
            campofechaInicio.SelectedDate < campofechaFin.SelectedDate;
    }
}