using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class ProveedorTests
{
    // Prueba para verificar que una instancia de Proveedor se inicializa con valores predeterminados
    [Fact]
    public void Proveedor_ShouldInitializeWithDefaultValues()
    {
        var proveedor = new Proveedor();

        Assert.Equal(0, proveedor.id);
        Assert.Null(proveedor.rfc);
        Assert.Null(proveedor.nombre);
        Assert.Null(proveedor.correo);
        Assert.Null(proveedor.telefono);
        Assert.False(proveedor.estadoProveedor);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Proveedor_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedRfc = "ABC123456789";
        var expectedNombre = "Proveedor XYZ";
        var expectedCorreo = "proveedor@xyz.com";
        var expectedTelefono = "1234567890";
        var expectedEstadoProveedor = true;

        var proveedor = new Proveedor
        {
            id = expectedId,
            rfc = expectedRfc,
            nombre = expectedNombre,
            correo = expectedCorreo,
            telefono = expectedTelefono,
            estadoProveedor = expectedEstadoProveedor
        };

        Assert.Equal(expectedId, proveedor.id);
        Assert.Equal(expectedRfc, proveedor.rfc);
        Assert.Equal(expectedNombre, proveedor.nombre);
        Assert.Equal(expectedCorreo, proveedor.correo);
        Assert.Equal(expectedTelefono, proveedor.telefono);
        Assert.True(proveedor.estadoProveedor);
    }

    // Prueba para verificar que el estado del proveedor por defecto sea inactivo
    [Fact]
    public void Proveedor_DefaultEstado_ShouldBeInactive()
    {
        var proveedor = new Proveedor();
        Assert.False(proveedor.estadoProveedor);
    }

    // Prueba para verificar que el RFC puede contener hasta 13 caracteres
    [Fact]
    public void Proveedor_RFC_ShouldAllowMaxLength()
    {
        var proveedor = new Proveedor
        {
            rfc = "ABCDEFGHIJKLM"
        };

        Assert.Equal(13, proveedor.rfc.Length);
    }
}