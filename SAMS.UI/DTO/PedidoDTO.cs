#nullable disable
namespace SAMS.UI.DTO;

public class PedidoDTO
{
    public string nombre { get; set; }

    public string NoPedido { get; set; }

    public DateTime FechaPedido { get; set; }

    public DateTime FechaEntrega { get; set; }
}