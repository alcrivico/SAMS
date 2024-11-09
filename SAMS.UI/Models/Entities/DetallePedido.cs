#nullable disable
namespace SAMS.UI.Models.Entities;

public class DetallePedido
{
    public int id { get; set; }

    public int cantidad { get; set; }

    public decimal precioCompra { get; set; }

    public int pedidoId { get; set; }

    public int productoId { get; set; }

    public DateTime fechaCaducidad { get; set; }

    public Producto producto { get; set; }

    public Pedido pedido { get; set; }
}