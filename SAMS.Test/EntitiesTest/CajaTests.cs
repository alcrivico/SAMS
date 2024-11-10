using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class CajaTests
{
    // Prueba para verificar que una instancia de Caja se inicializa con valores predeterminados
    [Fact]
    public void Caja_ShouldInitializeWithDefaultValues()
    {
        var caja = new Caja();

        Assert.Equal(0, caja.id);
        Assert.Null(caja.noCaja);
        Assert.Equal(default, caja.horaDeCorte);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Caja_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedNoCaja = "Caja-01";
        var expectedHoraDeCorte = new DateTime(2024, 11, 10, 18, 30, 0);

        var caja = new Caja
        {
            id = expectedId,
            noCaja = expectedNoCaja,
            horaDeCorte = expectedHoraDeCorte
        };

        Assert.Equal(expectedId, caja.id);
        Assert.Equal(expectedNoCaja, caja.noCaja);
        Assert.Equal(expectedHoraDeCorte, caja.horaDeCorte);
    }

    // Prueba para verificar la propiedad de horaDeCorte establecida en el pasado
    [Fact]
    public void Caja_HoraDeCorte_ShouldBeInThePast()
    {
        var caja = new Caja
        {
            horaDeCorte = DateTime.Now.AddHours(-2)
        };
        bool isPast = caja.horaDeCorte < DateTime.Now;

        Assert.True(isPast);
    }

    // Prueba para verificar que noCaja puede ser un string vacío
    [Fact]
    public void Caja_NoCaja_ShouldAllowEmptyString()
    {
        var caja = new Caja
        {
            noCaja = ""
        };

        Assert.Equal("", caja.noCaja);
    }
}