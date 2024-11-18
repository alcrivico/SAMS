namespace SAMS.UI.DTO;

public class ReporteVentaDTO
{
    public int noVenta { get; set; }
    public DateTime fechaRegistro { get; set; }
    public decimal total { get; set; }
    public string noCaja { get; set; }
    public string nombre { get; set; }

    // Propiedad calculada para la fecha formateada
    public string FechaRegistroFormateada => fechaRegistro.ToString("dd/MM/yyyy HH:mm:ss");
}

