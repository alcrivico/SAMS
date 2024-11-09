#nullable disable
namespace SAMS.UI.Models.Entities;

public class Monedero
{
    public int id { get; set; }

    public string codigoDeBarras { get; set; }

    public decimal saldo { get; set; }

    public string telefono { get; set; }
}