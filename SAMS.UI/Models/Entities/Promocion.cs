#nullable disable
namespace SAMS.UI.Models.Entities;

public class Promocion
{
    public int id { get; set; }

    public string nombre { get; set; }

    public int porcentajeDescuento { get; set; }

    public int cantMaxima { get; set; }

    public int cantMinima { get; set; }
}