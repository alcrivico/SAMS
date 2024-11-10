using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class EstadoPedidoTests
{
    // Prueba para verificar que una instancia de EstadoPedido se inicializa con valores predeterminados
    [Fact]
    public void EstadoPedido_ShouldInitializeWithDefaultValues()
    {
        var estadoPedido = new EstadoPedido();

        Assert.Equal(0, estadoPedido.id);
        Assert.Null(estadoPedido.nombre);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void EstadoPedido_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedNombre = "En Proceso";

        var estadoPedido = new EstadoPedido
        {
            id = expectedId,
            nombre = expectedNombre
        };

        Assert.Equal(expectedId, estadoPedido.id);
        Assert.Equal(expectedNombre, estadoPedido.nombre);
    }

    // Prueba para verificar que 'nombre' puede ser un string vacío
    [Fact]
    public void EstadoPedido_Nombre_ShouldAllowEmptyString()
    {
        var estadoPedido = new EstadoPedido
        {
            nombre = ""
        };

        Assert.Equal("", estadoPedido.nombre);
    }

    // Prueba para verificar que 'nombre' puede ser nulo
    [Fact]
    public void EstadoPedido_Nombre_ShouldAllowNull()
    {
        var estadoPedido = new EstadoPedido
        {
            nombre = null
        };

        Assert.Null(estadoPedido.nombre);
    }
}