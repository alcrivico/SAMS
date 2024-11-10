using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class PromocionTests
{
    // Prueba para verificar que una instancia de Promocion se inicializa con valores predeterminados
    [Fact]
    public void Promocion_ShouldInitializeWithDefaultValues()
    {
        var promocion = new Promocion();

        Assert.Equal(0, promocion.id);
        Assert.Null(promocion.nombre);
        Assert.Equal(0, promocion.porcentajeDescuento);
        Assert.Equal(0, promocion.cantMaxima);
        Assert.Equal(0, promocion.cantMinima);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Promocion_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedNombre = "Descuento Black Friday";
        var expectedPorcentajeDescuento = 20;
        var expectedCantMaxima = 100;
        var expectedCantMinima = 10;

        var promocion = new Promocion
        {
            id = expectedId,
            nombre = expectedNombre,
            porcentajeDescuento = expectedPorcentajeDescuento,
            cantMaxima = expectedCantMaxima,
            cantMinima = expectedCantMinima
        };

        Assert.Equal(expectedId, promocion.id);
        Assert.Equal(expectedNombre, promocion.nombre);
        Assert.Equal(expectedPorcentajeDescuento, promocion.porcentajeDescuento);
        Assert.Equal(expectedCantMaxima, promocion.cantMaxima);
        Assert.Equal(expectedCantMinima, promocion.cantMinima);
    }
}