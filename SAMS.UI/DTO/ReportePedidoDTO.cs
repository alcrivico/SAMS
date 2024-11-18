namespace SAMS.UI.DTO;

public class ReportePedidoDTO
{
    public string noPedido { get; set; }
    public DateTime fechaPedido { get; set; }
    public DateTime fechaEntrega { get; set; }
    public string proveedor { get; set; }
    public decimal costoTotalPedido { get; set; }

    // Propiedades formateadas
    public string FechaPedidoFormateada => fechaPedido.ToString("dd/MM/yyyy HH:mm:ss");
    public string FechaEntregaFormateada => fechaEntrega.ToString("dd/MM/yyyy HH:mm:ss");
}