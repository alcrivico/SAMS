using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class DetalleVentaTests
{
    // Prueba para verificar que una instancia de DetalleVenta se inicializa con valores predeterminados
    [Fact]
    public void DetalleVenta_ShouldInitializeWithDefaultValues()
    {
        var detalleVenta = new DetalleVenta();

        Assert.Equal(0, detalleVenta.id);
        Assert.Equal(0, detalleVenta.cantidad);
        Assert.Equal(0m, detalleVenta.precioVenta);
        Assert.Equal(0, detalleVenta.ventaId);
        Assert.Equal(0, detalleVenta.productoInventarioId);
        Assert.Equal(0m, detalleVenta.ganancia);
        Assert.Null(detalleVenta.venta);
        Assert.Null(detalleVenta.productoInventario);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void DetalleVenta_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 10;
        var expectedCantidad = 5;
        var expectedPrecioVenta = 150.75m;
        var expectedVentaId = 3;
        var expectedProductoInventarioId = 7;
        var expectedGanancia = 50.25m;

        var venta = new Venta { id = expectedVentaId, noVenta = 101 };
        var productoInventario = new ProductoInventario { id = expectedProductoInventarioId, nombre = "Producto A" };

        var detalleVenta = new DetalleVenta
        {
            id = expectedId,
            cantidad = expectedCantidad,
            precioVenta = expectedPrecioVenta,
            ventaId = expectedVentaId,
            productoInventarioId = expectedProductoInventarioId,
            ganancia = expectedGanancia,
            venta = venta,
            productoInventario = productoInventario
        };

        Assert.Equal(expectedId, detalleVenta.id);
        Assert.Equal(expectedCantidad, detalleVenta.cantidad);
        Assert.Equal(expectedPrecioVenta, detalleVenta.precioVenta);
        Assert.Equal(expectedVentaId, detalleVenta.ventaId);
        Assert.Equal(expectedProductoInventarioId, detalleVenta.productoInventarioId);
        Assert.Equal(expectedGanancia, detalleVenta.ganancia);
        Assert.Equal(venta, detalleVenta.venta);
        Assert.Equal(productoInventario, detalleVenta.productoInventario);
    }

    // Prueba para verificar que 'precioVenta' puede ser mayor que cero
    [Fact]
    public void DetalleVenta_PrecioVenta_ShouldBeGreaterThanZero()
    {
        var detalleVenta = new DetalleVenta
        {
            precioVenta = 250.50m
        };

        Assert.True(detalleVenta.precioVenta > 0);
    }

    // Prueba para verificar que 'cantidad' puede ser igual a cero
    [Fact]
    public void DetalleVenta_Cantidad_ShouldAllowZero()
    {
        var detalleVenta = new DetalleVenta
        {
            cantidad = 0
        };

        Assert.Equal(0, detalleVenta.cantidad);
    }

    // Prueba para verificar que 'productoInventario' y 'venta' pueden ser nulos
    [Fact]
    public void DetalleVenta_ProductoInventarioAndVenta_ShouldAllowNull()
    {
        var detalleVenta = new DetalleVenta
        {
            productoInventario = null,
            venta = null
        };

        Assert.Null(detalleVenta.productoInventario);
        Assert.Null(detalleVenta.venta);
    }

    // Prueba para verificar que 'ganancia' puede ser negativa
    [Fact]
    public void DetalleVenta_Ganancia_ShouldAllowNegativeValues()
    {
        var detalleVenta = new DetalleVenta
        {
            ganancia = -20.5m
        };

        Assert.Equal(-20.5m, detalleVenta.ganancia);
    }
}