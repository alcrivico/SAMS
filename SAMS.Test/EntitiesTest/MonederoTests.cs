using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class MonederoTests
{
    // Prueba para verificar que una instancia de Monedero se inicializa con valores predeterminados
    [Fact]
    public void Monedero_ShouldInitializeWithDefaultValues()
    {
        var monedero = new Monedero();

        Assert.Equal(0, monedero.id);
        Assert.Null(monedero.codigoDeBarras);
        Assert.Equal(0m, monedero.saldo);
        Assert.Null(monedero.telefono);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Monedero_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedCodigoDeBarras = "123456789012";
        var expectedSaldo = 500.75m;
        var expectedTelefono = "5551234567";

        var monedero = new Monedero
        {
            id = expectedId,
            codigoDeBarras = expectedCodigoDeBarras,
            telefono = expectedTelefono
        };

        monedero.SetSaldo(expectedSaldo);

        Assert.Equal(expectedId, monedero.id);
        Assert.Equal(expectedCodigoDeBarras, monedero.codigoDeBarras);
        Assert.Equal(expectedSaldo, monedero.saldo);
        Assert.Equal(expectedTelefono, monedero.telefono);
    }

    // Prueba para verificar que 'codigoDeBarras' puede ser nulo
    [Fact]
    public void Monedero_CodigoDeBarras_ShouldAllowNull()
    {
        var monedero = new Monedero
        {
            codigoDeBarras = null
        };

        Assert.Null(monedero.codigoDeBarras);
    }

    // Prueba para verificar que 'telefono' puede ser un string vacío
    [Fact]
    public void Monedero_Telefono_ShouldAllowEmptyString()
    {
        var monedero = new Monedero
        {
            telefono = ""
        };

        Assert.Equal("", monedero.telefono);
    }

    // Prueba para verificar que el saldo no puede ser negativo
    [Fact]
    public void Monedero_Saldo_ShouldNotBeNegative()
    {
        var monedero = new Monedero();

        // Intentar establecer un saldo negativo debería lanzar una excepción
        Assert.Throws<ArgumentException>(() => monedero.SetSaldo(-50m));
    }

    // Prueba para verificar que el saldo se puede establecer correctamente
    [Fact]
    public void Monedero_SetSaldo_ShouldSetCorrectValue()
    {
        var monedero = new Monedero();
        monedero.SetSaldo(100m);

        Assert.Equal(100m, monedero.saldo);
    }
}