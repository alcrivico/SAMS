#nullable disable
namespace SAMS.UI.Models.Entities;

public class ProductoInventario
{
    public int id { get; set; }

    public string codigo { get; set; }

    public string nombre { get; set; }

    public string descripcion { get; set; }

    public int cantidadBodega { get; set; }

    public int cantidadExhibicion { get; set; }

    public decimal precioActual { get; set; }

    public bool esPerecedero { get; set; }

    public bool esDevolvible { get; set; }

    public bool ubicacion { get; set; }

    public int unidadMermaId { get; set; }

    public int categoriaId { get; set; }

    public int estadoProductoId { get; set; }

    public int? promocionId { get; set; }

    public UnidadDeMedida unidadMerma { get; set; }

    public Categoria categoria { get; set; }

    public EstadoProducto estadoProducto { get; set; }

    public Promocion promocion { get; set; }
}