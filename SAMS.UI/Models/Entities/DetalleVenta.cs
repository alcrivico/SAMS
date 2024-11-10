#nullable disable

namespace SAMS.UI.Models.Entities;

public class DetalleVenta
{
    public int id { get; set; }

    public int cantidad { get; set; }

    public decimal precioVenta { get; set; }

    public int ventaId { get; set; }

    public int productoInventarioId { get; set; }

    public decimal ganancia { get; set; }

    public Venta venta { get; set; }

    public ProductoInventario productoInventario { get; set; }

}