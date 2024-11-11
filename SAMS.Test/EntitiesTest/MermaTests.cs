using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class MermaTests
{
    // Prueba para verificar que una instancia de Merma se inicializa con valores predeterminados
    [Fact]
    public void Merma_ShouldInitializeWithDefaultValues()
    {
        var merma = new Merma();

        Assert.Equal(0, merma.id);
        Assert.Equal(0, merma.cantidad);
        Assert.Null(merma.descripcion);
        Assert.Equal(default, merma.fechaRegistro);
        Assert.Equal(0, merma.productoInventarioId);
        Assert.Null(merma.productoInventario);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Merma_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedCantidad = 10;
        var expectedDescripcion = "Producto dañado";
        var expectedFechaRegistro = new DateTime(2024, 11, 10);
        var expectedProductoInventarioId = 5;
        var productoInventario = new ProductoInventario { id = expectedProductoInventarioId, nombre = "Producto X" };

        var merma = new Merma
        {
            id = expectedId,
            cantidad = expectedCantidad,
            descripcion = expectedDescripcion,
            fechaRegistro = expectedFechaRegistro,
            productoInventarioId = expectedProductoInventarioId,
            productoInventario = productoInventario
        };

        Assert.Equal(expectedId, merma.id);
        Assert.Equal(expectedCantidad, merma.cantidad);
        Assert.Equal(expectedDescripcion, merma.descripcion);
        Assert.Equal(expectedFechaRegistro, merma.fechaRegistro);
        Assert.Equal(expectedProductoInventarioId, merma.productoInventarioId);
        Assert.Equal(productoInventario, merma.productoInventario);
    }

    // Prueba para verificar que 'descripcion' puede ser nula
    [Fact]
    public void Merma_Descripcion_ShouldAllowNull()
    {
        var merma = new Merma
        {
            descripcion = null
        };

        Assert.Null(merma.descripcion);
    }

    // Prueba para verificar que 'fechaRegistro' se puede establecer en una fecha pasada
    [Fact]
    public void Merma_FechaRegistro_ShouldBeInThePast()
    {
        var merma = new Merma
        {
            fechaRegistro = DateTime.Now.AddDays(-1)
        };

        Assert.True(merma.fechaRegistro < DateTime.Now);
    }
}