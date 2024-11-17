namespace SAMS.UI.DTO;

public class ReportePedidoDTO
{
    public string noPedido { get; set; }
    public DateTime fechaPedido { get; set; }
    public DateTime fechaEntrega { get; set; }
    public string proveedor { get; set; }
    public decimal costoTotalPedido { get; set; }
}
