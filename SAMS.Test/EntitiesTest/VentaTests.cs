using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class VentaTests
{
    // Prueba para verificar que una instancia de Venta se inicializa con valores predeterminados
    [Fact]
    public void Venta_ShouldInitializeWithDefaultValues()
    {
        var venta = new Venta();

        Assert.Equal(0, venta.id);
        Assert.Equal(0, venta.noVenta);
        Assert.Equal(default, venta.fechaRegistro);
        Assert.Equal(0m, venta.iva);
        Assert.Equal(0, venta.cajaId);
        Assert.Equal(0, venta.monederoId);
        Assert.Equal(0, venta.empleadoId);
        Assert.Null(venta.caja);
        Assert.Null(venta.monedero);
        Assert.Null(venta.empleado);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Venta_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedNoVenta = 123;
        var expectedFechaRegistro = new DateTime(2024, 11, 10);
        var expectedIva = 16m;
        var expectedCajaId = 2;
        var expectedMonederoId = 3;
        var expectedEmpleadoId = 4;

        var venta = new Venta
        {
            id = expectedId,
            noVenta = expectedNoVenta,
            fechaRegistro = expectedFechaRegistro,
            cajaId = expectedCajaId,
            monederoId = expectedMonederoId,
            empleadoId = expectedEmpleadoId
        };
        venta.SetIva(expectedIva);

        Assert.Equal(expectedId, venta.id);
        Assert.Equal(expectedNoVenta, venta.noVenta);
        Assert.Equal(expectedFechaRegistro, venta.fechaRegistro);
        Assert.Equal(expectedIva, venta.iva);
        Assert.Equal(expectedCajaId, venta.cajaId);
        Assert.Equal(expectedMonederoId, venta.monederoId);
        Assert.Equal(expectedEmpleadoId, venta.empleadoId);
    }

    // Verificar que se lanza la excepción cuando el IVA es negativo
    [Fact]
    public void Venta_Iva_ShouldBeNonNegative()
    {
        var venta = new Venta();

        Assert.Throws<ArgumentException>(() => venta.SetIva(-1m));
    }


    // Prueba para verificar que la fecha de registro esté en el pasado o presente
    [Fact]
    public void Venta_FechaRegistro_ShouldBePastOrPresent()
    {
        var venta = new Venta
        {
            fechaRegistro = DateTime.Now.AddMinutes(-10)
        };

        Assert.True(venta.fechaRegistro <= DateTime.Now);
    }
}