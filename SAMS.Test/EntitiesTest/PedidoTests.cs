using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class PedidoTests
{
    // Prueba para verificar que una instancia de Pedido se inicializa con valores predeterminados
    [Fact]
    public void Pedido_ShouldInitializeWithDefaultValues()
    {
        var pedido = new Pedido();

        Assert.Equal(0, pedido.id);
        Assert.Null(pedido.noPedido);
        Assert.Equal(default, pedido.fechaPedido);
        Assert.Equal(default, pedido.fechaEntrega);
        Assert.Equal(0, pedido.estadoPedidoId);
        Assert.Null(pedido.estadoPedido);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Pedido_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedNoPedido = "PED-001";
        var expectedFechaPedido = new DateTime(2024, 11, 1);
        var expectedFechaEntrega = new DateTime(2024, 11, 15);
        var expectedEstadoPedidoId = 2;
        var estadoPedido = new EstadoPedido { id = expectedEstadoPedidoId, nombre = "En Proceso" };

        var pedido = new Pedido
        {
            id = expectedId,
            noPedido = expectedNoPedido,
            fechaPedido = expectedFechaPedido,
            fechaEntrega = expectedFechaEntrega,
            estadoPedidoId = expectedEstadoPedidoId,
            estadoPedido = estadoPedido
        };

        Assert.Equal(expectedId, pedido.id);
        Assert.Equal(expectedNoPedido, pedido.noPedido);
        Assert.Equal(expectedFechaPedido, pedido.fechaPedido);
        Assert.Equal(expectedFechaEntrega, pedido.fechaEntrega);
        Assert.Equal(expectedEstadoPedidoId, pedido.estadoPedidoId);
        Assert.Equal(estadoPedido, pedido.estadoPedido);
    }

    // Prueba para verificar que 'noPedido' puede ser nulo
    [Fact]
    public void Pedido_NoPedido_ShouldAllowNull()
    {
        var pedido = new Pedido
        {
            noPedido = null
        };

        Assert.Null(pedido.noPedido);
    }

    // Prueba para verificar que 'fechaEntrega' es posterior a 'fechaPedido'
    [Fact]
    public void Pedido_FechaEntrega_ShouldBeAfterFechaPedido()
    {
        var pedido = new Pedido
        {
            fechaPedido = new DateTime(2024, 11, 1),
            fechaEntrega = new DateTime(2024, 11, 5)
        };

        Assert.True(pedido.fechaEntrega > pedido.fechaPedido);
    }
}