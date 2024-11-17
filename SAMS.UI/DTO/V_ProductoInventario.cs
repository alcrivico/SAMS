#nullable disable
namespace SAMS.UI.DTO;

public class V_ProductoInventario
{
    public int id { get; set; }

    public string nombre { get; set; }

    public string cantidadBodega { get; set; }

    public string cantidadExhibicion { get; set; }

    public decimal precioActual { get; set; }
    //eliminar ubicacion
    public bool ubicacion { get; set; }
}