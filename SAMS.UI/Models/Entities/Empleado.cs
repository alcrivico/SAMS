#nullable disable
namespace SAMS.UI.Models.Entities;

public class Empleado
{
    public int id { get; set; }

    public string rfc { get; set; }

    public string noempleado { get; set; }

    public string nombre { get; set; }

    public string apellidoPaterno { get; set; }

    public string apellidoMaterno { get; set; }

    public string correo { get; set; }

    public string password { get; set; }

    public string telefono { get; set; }

    public int puestoId { get; set; }

    public Puesto puesto { get; set; }
}