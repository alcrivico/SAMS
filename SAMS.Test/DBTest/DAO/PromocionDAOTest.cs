using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMS.UI.DAO;

namespace SAMS.Test.DBTest.DAO
{
    public class PromocionDAOTest : SAMSContextTest
    {
        [Fact]
        public async Task CrearPromocionConVigencia_DeberiaCrearPromocion()
        {
            using var context = GetContext();

            // Crear una instancia de datos para la prueba
            var crearPromocion = new CrearPromocionVigenciaDTO
            {
                nombre = "Promoción Test",
                porcentajeDescuento = 10,
                cantMaxima = 100,
                cantMinima = 1,
                fechaInicio = DateTime.Now,
                fechaFin = DateTime.Now.AddDays(10),
                productoInventarioId = 1 
            };

            // Ejecutar la lógica directamente en el contexto
            var parameters = new[]
            {
                new SqlParameter("@nombre", crearPromocion.nombre),
                new SqlParameter("@porcentajeDescuento", crearPromocion.porcentajeDescuento),
                new SqlParameter("@cantMaxima", crearPromocion.cantMaxima),
                new SqlParameter("@cantMinima", crearPromocion.cantMinima),
                new SqlParameter("@fechaInicio", crearPromocion.fechaInicio),
                new SqlParameter("@fechaFin", crearPromocion.fechaFin),
                new SqlParameter("@idProductoInventario", crearPromocion.productoInventarioId)
            };

            int result = await context.Database.ExecuteSqlRawAsync(
                @"EXEC [dbo].[T_CrearPromocionConVigencia] 
                @nombre, @porcentajeDescuento, @cantMaxima, 
                @cantMinima, @fechaInicio, @fechaFin, 
                @idProductoInventario",
                parameters);

            Assert.True(result > 0, "La promoción no fue creada.");
        }

        [Fact]
        public async Task VerPromociones_DeberiaRetornarPromociones()
        {
            using var context = GetContext();

            // Consultar directamente desde el contexto
            var promociones = context.V_Promocion.ToList();

            Assert.NotNull(promociones);
            Assert.True(promociones.Any(), "No se encontraron promociones.");
        }

        [Fact]
        public async Task FinalizarPromocion_DeberiaMarcarComoFinalizada()
        {
            using var context = GetContext();

            int promocionId = 1; 
            var parameters = new[]
            {
                new SqlParameter("@promocionId", promocionId)
            };

            int result = await context.Database.ExecuteSqlRawAsync(
                @"EXEC [dbo].[T_FinalizarPromocion] @promocionId",
                parameters);

            Assert.True(result > 0, "La promoción no pudo ser finalizada.");
        }
    }
}
