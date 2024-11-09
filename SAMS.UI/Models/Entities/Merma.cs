#nullable disable
namespace SAMS.UI.Models.Entities;

public class Merma
{
    public int id { get; set; }

    public int cantidad { get; set; }

    public string descripcion { get; set; }

    public DateTime fechaRegistro { get; set; }

    public int productoInventarioID { get; set; }

    public  ProductoInventario productoInventario { get; set; }
}