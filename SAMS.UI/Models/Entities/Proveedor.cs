#nullable disable
namespace SAMS.UI.Models.Entities;

public class Proveedor
{
    public int id { get; set; }

    public string rfc { get; set; }

    public string nombre { get; set; }

    public string correo { get; set; }

    public string telefono { get; set; }

    public bool estadoProveedor { get; set; }
}