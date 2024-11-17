namespace SAMS.UI.DTO;

public class ReportePedidoDTO
{
    public int noPedido { get; set; }
    public DateTime fechaPedido { get; set; }
    public DateTime fechaEntrega { get; set; }
    public string proveedor { get; set; }
    public string costo { get; set; }
}
