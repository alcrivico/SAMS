#nullable disable
namespace SAMS.UI.DTO;

public class ProductoInventarioPromocionDTO
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string cantidad { get; set; }
    public bool esPerecedero { get; set; }

    public string PerecederoTexto => esPerecedero ? "Sí" : "No";
}
