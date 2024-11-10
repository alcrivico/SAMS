using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class PromocionVigenciaTests
{
    // Prueba para verificar que una instancia de PromocionVigencia se inicializa con valores predeterminados
    [Fact]
    public void PromocionVigencia_ShouldInitializeWithDefaultValues()
    {
        var promocionVigencia = new PromocionVigencia();

        Assert.Equal(0, promocionVigencia.id);
        Assert.Equal(default, promocionVigencia.fechaInicio);
        Assert.Equal(default, promocionVigencia.fechaFin);
        Assert.Equal(0, promocionVigencia.promocionId);
        Assert.Null(promocionVigencia.promocion);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void PromocionVigencia_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedFechaInicio = new DateTime(2024, 11, 1);
        var expectedFechaFin = new DateTime(2024, 11, 30);
        var expectedPromocionId = 2;
        var promocion = new Promocion { id = expectedPromocionId, nombre = "Promo de Verano" };

        var promocionVigencia = new PromocionVigencia
        {
            id = expectedId,
            fechaInicio = expectedFechaInicio,
            fechaFin = expectedFechaFin,
            promocionId = expectedPromocionId,
            promocion = promocion
        };

        Assert.Equal(expectedId, promocionVigencia.id);
        Assert.Equal(expectedFechaInicio, promocionVigencia.fechaInicio);
        Assert.Equal(expectedFechaFin, promocionVigencia.fechaFin);
        Assert.Equal(expectedPromocionId, promocionVigencia.promocionId);
        Assert.Equal(promocion, promocionVigencia.promocion);
    }

    // Prueba para verificar que la fecha de fin es posterior a la fecha de inicio
    [Fact]
    public void PromocionVigencia_FechaFin_ShouldBeAfterFechaInicio()
    {
        var promocionVigencia = new PromocionVigencia
        {
            fechaInicio = new DateTime(2024, 11, 1),
            fechaFin = new DateTime(2024, 11, 15)
        };

        Assert.True(promocionVigencia.fechaFin > promocionVigencia.fechaInicio);
    }
}