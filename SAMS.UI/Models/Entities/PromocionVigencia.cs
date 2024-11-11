#nullable disable
namespace SAMS.UI.Models.Entities;

public class PromocionVigencia
{
    public int id { get; set; }

    public DateTime fechaFin { get; set; }

    public DateTime fechaInicio { get; set; }

    public int promocionId { get; set; }

    public Promocion promocion { get; set; }
}