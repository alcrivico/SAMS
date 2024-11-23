using System.Data;

namespace SAMS.UI.DTO;

public class EditarPromocionDTO
{
    public int? promocionId { get; set; }
    public required string nombre { get; set; }
    public int? porcentajeDescuento { get; set; }
    public int? cantMaxima { get; set; }
    public int? cantMinima { get; set; }
    public DateTime? fechaInicio { get; set; }
    public DateTime? fechaFin { get; set; }
}
