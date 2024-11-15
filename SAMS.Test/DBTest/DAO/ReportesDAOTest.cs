using SAMS.UI.DAO;
namespace SAMS.Test.DBTest.DAO;

public class ReportesDAOTest : SAMSContextTest
{
    [Fact]
    public async Task ConsultarReporteVenta()
    {
        using var context = GetContext();
        var reportesDao = new ReportesDAO(context);

        var reporteVentas = await reportesDao.ReporteVentas();

        Assert.NotNull(reporteVentas);
        Assert.True(reporteVentas.Any(), "El reporte de ventas está vacío.");
    }

    [Fact]
    public void ConsultarReportePedidos()
    {
        using var context = GetContext();
        var reportesDao = new ReportesDAO(context);

        var reportePedidos = reportesDao.ReportePedidos();

        Assert.NotNull(reportePedidos);
        Assert.True(reportePedidos.Any(), "No se encontraron Reporte de Pedidos");
    }

    [Fact]
    public void ConsultarReporteInventario()
    {
        using var context = GetContext();
        var repotesDao = new ReportesDAO(context);

        var inventario = repotesDao.ReporteInventario();
        Assert.NotNull(inventario);
        Assert.True(inventario.Any(), "No se encontro inventario");
    }
}
