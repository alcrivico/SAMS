using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class DetallePedidoTests
{
    // Prueba para verificar que una instancia de DetallePedido se inicializa con valores predeterminados
    [Fact]
    public void DetallePedido_ShouldInitializeWithDefaultValues()
    {
        var detallePedido = new DetallePedido();

        Assert.Equal(0, detallePedido.id);
        Assert.Equal(0, detallePedido.cantidad);
        Assert.Equal(0m, detallePedido.precioCompra);
        Assert.Equal(0, detallePedido.pedidoId);
        Assert.Equal(0, detallePedido.productoId);
        Assert.Equal(default, detallePedido.fechaCaducidad);
        Assert.Null(detallePedido.producto);
        Assert.Null(detallePedido.pedido);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void DetallePedido_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 5;
        var expectedCantidad = 10;
        var expectedPrecioCompra = 99.99m;
        var expectedPedidoId = 1;
        var expectedProductoId = 2;
        var expectedFechaCaducidad = new DateTime(2025, 1, 1);

        var producto = new Producto { id = expectedProductoId, nombre = "Producto1" };
        var pedido = new Pedido { id = expectedPedidoId, noPedido = "PED-001" };

        var detallePedido = new DetallePedido
        {
            id = expectedId,
            cantidad = expectedCantidad,
            precioCompra = expectedPrecioCompra,
            pedidoId = expectedPedidoId,
            productoId = expectedProductoId,
            fechaCaducidad = expectedFechaCaducidad,
            producto = producto,
            pedido = pedido
        };

        Assert.Equal(expectedId, detallePedido.id);
        Assert.Equal(expectedCantidad, detallePedido.cantidad);
        Assert.Equal(expectedPrecioCompra, detallePedido.precioCompra);
        Assert.Equal(expectedPedidoId, detallePedido.pedidoId);
        Assert.Equal(expectedProductoId, detallePedido.productoId);
        Assert.Equal(expectedFechaCaducidad, detallePedido.fechaCaducidad);
        Assert.Equal(producto, detallePedido.producto);
        Assert.Equal(pedido, detallePedido.pedido);
    }

    // Prueba para verificar que 'fechaCaducidad' puede estar en el futuro
    [Fact]
    public void DetallePedido_FechaCaducidad_ShouldBeInTheFuture()
    {
        var detallePedido = new DetallePedido
        {
            fechaCaducidad = DateTime.Now.AddMonths(1)
        };

        Assert.True(detallePedido.fechaCaducidad > DateTime.Now);
    }

    // Prueba para verificar que 'cantidad' puede ser 0
    [Fact]
    public void DetallePedido_Cantidad_ShouldAllowZero()
    {
        var detallePedido = new DetallePedido
        {
            cantidad = 0
        };

        Assert.Equal(0, detallePedido.cantidad);
    }

    // Prueba para verificar que 'producto' y 'pedido' pueden ser nulos
    [Fact]
    public void DetallePedido_ProductoAndPedido_ShouldAllowNull()
    {
        var detallePedido = new DetallePedido
        {
            producto = null,
            pedido = null
        };

        Assert.Null(detallePedido.producto);
        Assert.Null(detallePedido.pedido);
    }
}