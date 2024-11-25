using SAMS.UI.DAO;
using System.Data;
namespace SAMS.Test.DBTest.DAO;

public class PromocionDAOTest : SAMSContextTest
{
    //[Fact]
    //public async Task CrearPromocionConVigencia_DeberiaCrearPromocion()
    //{
    //    using var context = GetContext();
    //    var promocionDao = new PromocionDAO(context);

    //    var nuevaPromocion = new CrearPromocionVigenciaDTO
    //    {
    //        nombre = "Promocion Test",
    //        porcentajeDescuento = 15,
    //        cantMaxima = 100,
    //        cantMinima = 10,
    //        fechaInicio = DateTime.Now,
    //        fechaFin = DateTime.Now.AddDays(30),
    //        productoInventarioId = 1 // Asumiendo que este producto existe.
    //    };

    //    var resultado = await promocionDao.CrearPromocionConVigencia(nuevaPromocion);
    //    Assert.True(resultado, "La promoción no se pudo crear.");
    //}

    //[Fact]
    //public async Task EditarPromocion_DeberiaEditarPromocion()
    //{
    //    using var context = GetContext();
    //    var promocionDao = new PromocionDAO(context);

    //    var promociones = promocionDao.VerPromociones();
    //    Assert.True(promociones.Any(), "No hay promociones disponibles para editar.");

    //    var ultimoId = promociones.Last().id;

    //    var dataTable = new DataTable();
    //    dataTable.Columns.Add("productoInventarioId", typeof(int));
    //    dataTable.Rows.Add(1);
    //    dataTable.Rows.Add(2);
    //    dataTable.Rows.Add(3);

    //    var editarPromocion = new EditarPromocionDTO
    //    {
    //        promocionId = ultimoId,
    //        nombre = "Promoción Actualizada",
    //        porcentajeDescuento = 25,
    //        fechaInicio = DateTime.Now,
    //        fechaFin = DateTime.Now.AddDays(10),
    //        ProductoInventarioIdList = dataTable
    //    };

    //    var resultado = await promocionDao.EditarPromocion(editarPromocion);

    //    Assert.True(resultado, "La promoción no se pudo editar.");

    //    var promocionesActualizadas = promocionDao.VerPromociones();
    //    var promocionEditada = promocionesActualizadas.FirstOrDefault(p => p.id == ultimoId);

    //    Assert.NotNull(promocionEditada);
    //    Assert.Equal("Promoción Actualizada", promocionEditada.nombre);
    //    Assert.Equal(25, promocionEditada.porcentajeDescuento);
    //}

    //[Fact]
    //public async Task FinalizarPromocion_DeberiaFinalizarPromocion()
    //{
    //    using var context = GetContext();
    //    var promocionDao = new PromocionDAO(context);

    //    var promociones = promocionDao.VerPromociones();
    //    Assert.True(promociones.Any(), "No hay promociones disponibles para finalizar.");

    //    var ultimoId = promociones.Last().id;

    //    var resultado = await promocionDao.FinalizarPromocion(ultimoId);
    //    Assert.True(resultado, "La promoción no se pudo finalizar.");

    //    var promocionFinalizada = context.PromocionVigencia
    //        .FirstOrDefault(p => p.promocionId == ultimoId);

    //    Assert.NotNull(promocionFinalizada);
    //    Assert.True(promocionFinalizada.fechaFin < DateTime.Now, "La fecha de finalización no fue actualizada correctamente.");
    //}


    //[Fact]
    //public void VerPromociones_DeberiaRetornarLista()
    //{
    //    using var context = GetContext();
    //    var promocionDao = new PromocionDAO(context);

    //    var promociones = promocionDao.VerPromociones();
    //    Assert.NotNull(promociones);
    //    Assert.True(promociones.Any(), "No se encontraron promociones.");
    //}
}
