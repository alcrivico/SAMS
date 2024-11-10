#nullable disable
namespace SAMS.UI.Models.Entities;

public class Pedido
{
    public int id { get; set; }

    public string noPedido { get; set; }

    public DateTime fechaPedido { get; set; }

    public DateTime fechaEntrega { get; set; }

    public int estadoPedidoId { get; set; }

    public EstadoPedido estadoPedido { get; set; }
}