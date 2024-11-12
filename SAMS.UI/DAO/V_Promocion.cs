#nullable disable
namespace SAMS.UI.DTO;

public class V_Promocion
{
    public string nombre { get; set; }

    public int porcentajeDescuento { get; set; }

    public DateTime fechaInicio { get; set; }

    public DateTime fechaFin { get; set; }
}