#nullable disable
namespace SAMS.UI.Models.Entities;

public class Venta
{
    public int id { get; set; }

    public int noVenta { get; set; }

    public DateTime fechaRegistro { get; set; }

    public decimal iva { get; set; }

    public int cajaId { get; set; }

    public int monederoId { get; set; }

    public int empleadoId { get; set; }

    public Caja caja { get; set; }

    public Monedero monedero { get; set; }

    public Empleado empleado { get; set; }
}