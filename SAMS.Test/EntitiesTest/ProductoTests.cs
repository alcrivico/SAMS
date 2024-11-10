using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class ProductoTests
{
    // Prueba para verificar que una instancia de Producto se inicializa con valores predeterminados
    [Fact]
    public void Producto_ShouldInitializeWithDefaultValues()
    {
        var producto = new Producto();

        Assert.Equal(0, producto.id);
        Assert.Null(producto.codigo);
        Assert.Null(producto.descripcion);
        Assert.False(producto.esDevolvible);
        Assert.False(producto.esPerecedero);
        Assert.Null(producto.nombre);
        Assert.Equal(0, producto.proveedorId);
        Assert.Equal(0, producto.unidadDeMedidaId);
        Assert.Null(producto.proveedor);
        Assert.Null(producto.unidadDeMedida);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Producto_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedCodigo = "PROD-001";
        var expectedDescripcion = "Producto de prueba";
        var expectedEsDevolvible = true;
        var expectedEsPerecedero = false;
        var expectedNombre = "Producto A";
        var expectedProveedorId = 3;
        var expectedUnidadDeMedidaId = 2;
        var proveedor = new Proveedor { id = expectedProveedorId, nombre = "Proveedor X" };
        var unidadDeMedida = new UnidadDeMedida { id = expectedUnidadDeMedidaId, nombre = "Litros" };

        var producto = new Producto
        {
            id = expectedId,
            codigo = expectedCodigo,
            descripcion = expectedDescripcion,
            esDevolvible = expectedEsDevolvible,
            esPerecedero = expectedEsPerecedero,
            nombre = expectedNombre,
            proveedorId = expectedProveedorId,
            unidadDeMedidaId = expectedUnidadDeMedidaId,
            proveedor = proveedor,
            unidadDeMedida = unidadDeMedida
        };

        Assert.Equal(expectedId, producto.id);
        Assert.Equal(expectedCodigo, producto.codigo);
        Assert.Equal(expectedDescripcion, producto.descripcion);
        Assert.Equal(expectedEsDevolvible, producto.esDevolvible);
        Assert.Equal(expectedEsPerecedero, producto.esPerecedero);
        Assert.Equal(expectedNombre, producto.nombre);
        Assert.Equal(expectedProveedorId, producto.proveedorId);
        Assert.Equal(expectedUnidadDeMedidaId, producto.unidadDeMedidaId);
        Assert.Equal(proveedor, producto.proveedor);
        Assert.Equal(unidadDeMedida, producto.unidadDeMedida);
    }

    // Prueba para verificar que 'codigo' puede ser nulo
    [Fact]
    public void Producto_Codigo_ShouldAllowNull()
    {
        var producto = new Producto
        {
            codigo = null
        };

        Assert.Null(producto.codigo);
    }
}