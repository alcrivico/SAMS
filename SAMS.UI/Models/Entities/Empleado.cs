#nullable disable
using System.Security.Cryptography;
using System.Text;

namespace SAMS.UI.Models.Entities;

public class Empleado
{
    public int id { get; set; }

    public string rfc { get; set; }

    public string noEmpleado { get; set; }

    public string nombre { get; set; }

    public string apellidoPaterno { get; set; }

    public string apellidoMaterno { get; set; }

    public string correo { get; set; }

    public string password;

    public string telefono { get; set; }

    public int puestoId { get; set; }

    public Puesto puesto { get; set; }

    public bool estado { get; set; }

    private string hashedPassword;

    // Propiedad para establecer y obtener la contraseña encriptada
    public string Password
    {
        get => hashedPassword;
        set => hashedPassword = HashPassword(value);
    }

    // Método para encriptar el password usando SHA256
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Usa Encoding.Unicode para asegurar compatibilidad con SQL Server
            byte[] bytes = sha256.ComputeHash(Encoding.Unicode.GetBytes(password));

            // Convierte a formato hexadecimal con prefijo '0x' y en mayúsculas
            StringBuilder builder = new StringBuilder("0x");
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("X2")); // Hexadecimal en mayúsculas
            }
            return builder.ToString();
        }
    }

    // Método para verificar si una contraseña coincide con el hash
    public bool VerifyPassword(string inputPassword)
    {
        string hashedInput = HashPassword(inputPassword);
        return hashedInput == hashedPassword;
    }

}