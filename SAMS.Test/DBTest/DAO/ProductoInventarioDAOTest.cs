using SAMS.UI.DAO;

namespace SAMS.Test.DBTest.DAO;

public class ProductoInventarioDAOTest : SAMSContextTest
{
    [Fact]
    public void ConsultarProductoInventario()
    {
        using var context = GetContext();
        var productoInventarioDao = new ProductoInventarioDAO(context);

        var productoInventario = productoInventarioDao.VerProductoInventario();

        Assert.NotNull(productoInventario);
        Assert.True(productoInventario.Any(), "No se encontro inventario");
    }
}
