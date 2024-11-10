#nullable disable
namespace SAMS.UI.Models.Entities;

public class Venta
{
    public int id { get; set; }

    public int noVenta { get; set; }

    public DateTime fechaRegistro { get; set; }

    public decimal iva { get; private set; }

    public int cajaId { get; set; }

    public int monederoId { get; set; }

    public int empleadoId { get; set; }

    public Caja caja { get; set; }

    public Monedero monedero { get; set; }

    public Empleado empleado { get; set; }

    public void SetIva(decimal nuevoIva)
    {
        if (nuevoIva < 0)
            throw new ArgumentException("El IVA no puede ser negativo");
        iva = nuevoIva;
    }
}