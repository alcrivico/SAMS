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

        try
        {
            bool resultado;

            if (editarPromocion != null)
            {
                editarPromocion.nombre = campoNombre.Text;
                editarPromocion.porcentajeDescuento = porcentajeDescuento;
                editarPromocion.cantMaxima = cantMaxima;
                editarPromocion.cantMinima = cantMinima;
                editarPromocion.fechaInicio = campofechaInicio.SelectedDate.Value;
                editarPromocion.fechaFin = campofechaFin.SelectedDate.Value;

                resultado = await PromocionDAO.EditarPromocion(editarPromocion);
            }
            else
            {
                CrearPromocionVigenciaDTO promocion = new CrearPromocionVigenciaDTO
                {
                    nombre = campoNombre.Text,
                    porcentajeDescuento = porcentajeDescuento,
                    cantMaxima = cantMaxima,
                    cantMinima = cantMinima,
                    fechaInicio = campofechaInicio.SelectedDate.Value,
                    fechaFin = campofechaFin.SelectedDate.Value,
                    productoInventarioId = productoInventarioId
                };

                resultado = await PromocionDAO.CrearPromocionConVigencia(promocion);
            }

            if (resultado)
            {
                InformationControl.Show( "Éxito",
                    editarPromocion != null ? "Promoción modificada correctamente." : "Promoción registrada correctamente.",
                    "Aceptar");

                PrincipalView ventanaPrincipal = new(empleado);
                ventanaPrincipal.Show();

                this.ventanaPrincipal.Close();
                Close();
            }
            else
            {
                InformationControl.Show("Error", "No se pudo conectar a la red de la empresa, por favor revise su conexión.", "Aceptar");
            }
        }
        catch (Exception)
        {
            InformationControl.Show("Error", "No se pudo conectar a la red de la empresa, por favor revise su conexión", "Aceptar");
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
        => Button_Registrar.IsButtonEnabled = ValidarCampos();

    private void validarCampo_DatePickerControlSelectedDateChanged(object sender, RoutedEventArgs e)
        => Button_Registrar.IsButtonEnabled = ValidarCampos();

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