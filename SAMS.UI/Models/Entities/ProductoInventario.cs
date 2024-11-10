using SAMS.UI.Models.Entities;

public class ProductoInventario
{
    public int id { get; set; }
    public string codigo { get; set; }
    public string nombre { get; set; }
    public string descripcion { get; set; }
    public int cantidadBodega { get; private set; }
    public int cantidadExhibicion { get; private set; }
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

    public void SetCantidadBodega(int cantidad)
    {
        if (cantidad < 0)
            throw new ArgumentException("La cantidad en bodega no puede ser negativa.");
        cantidadBodega = cantidad;
    }
    public void SetCantidadExhibicion(int cantidad)
    {
        if (cantidad < 0)
            throw new ArgumentException("La cantidad en exhibición no puede ser negativa.");
        cantidadExhibicion = cantidad;
    }
}