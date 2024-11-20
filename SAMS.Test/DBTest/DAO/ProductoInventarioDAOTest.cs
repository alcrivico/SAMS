namespace SAMS.Test.DBTest.DAO
{
    public class ProductoInventarioDAOTest : SAMSContextTest
    {
        [Fact]
        public void OptenerProductosSinPromocion_DeberiaRetornarProductos()
        {
            using var context = GetContext();

            var productos = context.V_ProductoInventarioPromocion.ToList();

            // Verificamos que no sea nulo y que haya productos
            Assert.NotNull(productos);
            Assert.True(productos.Any(), "No se encontraron productos sin promoción.");
        }
    }
}