#nullable disable
namespace SAMS.UI.Models.Entities;

public class Monedero
{
    public int id { get; set; }

    public string codigoDeBarras { get; set; }

    public decimal saldo { get; private set; } 

    public string telefono { get; set; }

    public string nombre { get; set; }

    public string apellidoPaterno { get; set; }

    public string apellidoMaterno { get; set; }

    public void SetSaldo(decimal nuevoSaldo)
    {
        if (nuevoSaldo < 0)
            throw new ArgumentException("El saldo no puede ser negativo");
        saldo = nuevoSaldo;
    }
}