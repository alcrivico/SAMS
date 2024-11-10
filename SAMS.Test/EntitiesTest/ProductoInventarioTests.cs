using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class ProductoInventarioTests
{
    // Prueba para verificar que una instancia de ProductoInventario se inicializa con valores predeterminados
    [Fact]
    public void ProductoInventario_ShouldInitializeWithDefaultValues()
    {
        var productoInventario = new ProductoInventario();

        Assert.Equal(0, productoInventario.id);
        Assert.Null(productoInventario.codigo);
        Assert.Null(productoInventario.nombre);
        Assert.Null(productoInventario.descripcion);
        Assert.Equal(0, productoInventario.cantidadBodega);
        Assert.Equal(0, productoInventario.cantidadExhibicion);
        Assert.Equal(0m, productoInventario.precioActual);
        Assert.False(productoInventario.esPerecedero);
        Assert.False(productoInventario.esDevolvible);
        Assert.False(productoInventario.ubicacion);
        Assert.Equal(0, productoInventario.unidadMermaId);
        Assert.Equal(0, productoInventario.categoriaId);
        Assert.Equal(0, productoInventario.estadoProductoId);
        Assert.Null(productoInventario.promocionId);
        Assert.Null(productoInventario.unidadMerma);
        Assert.Null(productoInventario.categoria);
        Assert.Null(productoInventario.estadoProducto);
        Assert.Null(productoInventario.promocion);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void ProductoInventario_ShouldAllowPropertySetAndGet()
    {
        var productoInventario = new ProductoInventario
        {
            id = 1,
            codigo = "PI-001",
            nombre = "Producto A",
            descripcion = "Descripción del producto",
            precioActual = 59.99m,
            esPerecedero = true,
            esDevolvible = false,
            ubicacion = true,
            unidadMermaId = 2,
            categoriaId = 3,
            estadoProductoId = 4,
            promocionId = 5
        };
        productoInventario.SetCantidadBodega(100);
        productoInventario.SetCantidadExhibicion(20);

        Assert.Equal(1, productoInventario.id);
        Assert.Equal("PI-001", productoInventario.codigo);
        Assert.Equal("Producto A", productoInventario.nombre);
        Assert.Equal("Descripción del producto", productoInventario.descripcion);
        Assert.Equal(100, productoInventario.cantidadBodega);
        Assert.Equal(20, productoInventario.cantidadExhibicion);
        Assert.Equal(59.99m, productoInventario.precioActual);
        Assert.True(productoInventario.esPerecedero);
        Assert.False(productoInventario.esDevolvible);
        Assert.True(productoInventario.ubicacion);
    }

    // Prueba para verificar que 'cantidadBodega' no sea negativa
    [Fact]
    public void ProductoInventario_CantidadBodega_ShouldNotBeNegative()
    {
        var productoInventario = new ProductoInventario();

        // Intentar establecer una cantidad negativa debería lanzar una excepción
        Assert.Throws<ArgumentException>(() => productoInventario.SetCantidadBodega(-5));
    }
}
