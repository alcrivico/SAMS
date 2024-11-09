#nullable disable
namespace SAMS.UI.Models.Entities;

public class Producto
{
    public int id { get; set; }

    public string codigo { get; set; }

    public string descripcion { get; set; }

    public bool esDevolvible { get; set; }

    public bool esPerecedero { get; set; }

    public string nombre { get; set; }

    public int proveedorId { get; set; }

    public int unidadDeMedidaId { get; set; }

    public Proveedor proveedor { get; set; }

    public UnidadDeMedida unidadDeMedida { get; set; }
}