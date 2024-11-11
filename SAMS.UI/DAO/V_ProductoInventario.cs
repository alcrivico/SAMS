#nullable disable
namespace SAMS.UI.DTO;

public class V_ProductoInventario
{
    public int id { get; set; }

    public string nombre { get; set; }

    public int cantidadBodega { get; set; }

    public int cantidadExhibicion { get; set; }

    public decimal precioActual { get; set; }

    public bool ubicacion { get; set; }
}