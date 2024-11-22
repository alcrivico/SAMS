#nullable disable
namespace SAMS.UI.DTO;

public class PromocionesDTO
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string cantidad { get; set; }
    public int porcentajeDescuento { get; set; }
    public int cantMaxima { get; set; }
    public int cantMinima { get; set; }
    public DateTime fechaInicio { get; set; }
    public DateTime fechaFin { get; set; }
}