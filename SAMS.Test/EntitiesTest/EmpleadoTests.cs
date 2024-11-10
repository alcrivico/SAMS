using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class EmpleadoTests
{
    // Prueba para verificar que una instancia de Empleado se inicializa con valores predeterminados
    [Fact]
    public void Empleado_ShouldInitializeWithDefaultValues()
    {
        var empleado = new Empleado();

        Assert.Equal(0, empleado.id);
        Assert.Null(empleado.rfc);
        Assert.Null(empleado.noempleado);
        Assert.Null(empleado.nombre);
        Assert.Null(empleado.apellidoPaterno);
        Assert.Null(empleado.apellidoMaterno);
        Assert.Null(empleado.correo);
        Assert.Null(empleado.password);
        Assert.Null(empleado.telefono);
        Assert.Equal(0, empleado.puestoId);
        Assert.Null(empleado.puesto);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Empleado_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 100;
        var expectedRfc = "XAXX010101000";
        var expectedNoEmpleado = "EMP-001";
        var expectedNombre = "Juan";
        var expectedApellidoPaterno = "Pérez";
        var expectedApellidoMaterno = "Gómez";
        var expectedCorreo = "juan.perez@example.com";
        var expectedPassword = "1234Password";
        var expectedTelefono = "5551234567";
        var expectedPuestoId = 5;

        var puesto = new Puesto { id = expectedPuestoId, nombre = "Gerente" };

        var empleado = new Empleado
        {
            id = expectedId,
            rfc = expectedRfc,
            noempleado = expectedNoEmpleado,
            nombre = expectedNombre,
            apellidoPaterno = expectedApellidoPaterno,
            apellidoMaterno = expectedApellidoMaterno,
            correo = expectedCorreo,
            password = expectedPassword,
            telefono = expectedTelefono,
            puestoId = expectedPuestoId,
            puesto = puesto
        };

        Assert.Equal(expectedId, empleado.id);
        Assert.Equal(expectedRfc, empleado.rfc);
        Assert.Equal(expectedNoEmpleado, empleado.noempleado);
        Assert.Equal(expectedNombre, empleado.nombre);
        Assert.Equal(expectedApellidoPaterno, empleado.apellidoPaterno);
        Assert.Equal(expectedApellidoMaterno, empleado.apellidoMaterno);
        Assert.Equal(expectedCorreo, empleado.correo);
        Assert.Equal(expectedPassword, empleado.password);
        Assert.Equal(expectedTelefono, empleado.telefono);
        Assert.Equal(expectedPuestoId, empleado.puestoId);
        Assert.Equal(puesto, empleado.puesto);
    }

    // Prueba para verificar que 'correo' puede ser nulo
    [Fact]
    public void Empleado_Correo_ShouldAllowNull()
    {
        var empleado = new Empleado
        {
            correo = null
        };

        Assert.Null(empleado.correo);
    }

    // Prueba para verificar que 'telefono' puede ser un string vacío
    [Fact]
    public void Empleado_Telefono_ShouldAllowEmptyString()
    {
        var empleado = new Empleado
        {
            telefono = ""
        };

        Assert.Equal("", empleado.telefono);
    }

    // Prueba para verificar que 'password' puede ser nulo
    [Fact]
    public void Empleado_Password_ShouldAllowNull()
    {
        var empleado = new Empleado
        {
            password = null
        };

        Assert.Null(empleado.password);
    }

    // Prueba para verificar que 'puesto' puede ser nulo
    [Fact]
    public void Empleado_Puesto_ShouldAllowNull()
    {
        var empleado = new Empleado
        {
            puesto = null
        };

        Assert.Null(empleado.puesto);
    }

    // Prueba para verificar que 'rfc' tiene un formato válido
    [Fact]
    public void Empleado_Rfc_ShouldHaveValidFormat()
    {
        var empleado = new Empleado
        {
            rfc = "XAXX010101000"
        };

        Assert.Matches(@"^[A-Z]{4}\d{6}[A-Z0-9]{3}$", empleado.rfc);
    }

    // Prueba para verificar que 'correo' tiene un formato válido
    [Fact]
    public void Empleado_Correo_ShouldHaveValidFormat()
    {
        var empleado = new Empleado
        {
            correo = "juan.perez@example.com"
        };

        Assert.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", empleado.correo);
    }
}