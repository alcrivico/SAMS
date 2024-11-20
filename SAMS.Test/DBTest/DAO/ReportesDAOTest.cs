using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMS.UI.DTO;
namespace SAMS.Test.DBTest.DAO;

public class ReportesDAOTest : SAMSContextTest
{
    [Fact]
    public async Task ConsultarReporteVenta()
    {
        using var context = GetContext();
        // Ejecutar la consulta directamente sobre el contexto
        var reporteVentas = await Task.Run(() =>
        {
            DateTime inicioDelDia = DateTime.Now.Date;
            DateTime finDelDia = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);

            return context.Set<ReporteVentaDTO>()
                .FromSqlRaw("SELECT * FROM dbo.V_ReporteVenta")
                .AsNoTracking()
                .ToList();
        });

        Assert.NotNull(reporteVentas);
        Assert.True(reporteVentas.Any(), "El reporte de ventas está vacío.");
    }

    [Fact]
    public async Task ConsultarReportePedidos()
    {
        using var context = GetContext();
        // Ejecutar la consulta directamente sobre el contexto
        var reportePedidos = await Task.Run(() =>
        {
            DateTime fechaInicio = DateTime.Now.AddMonths(-3);
            DateTime fechaFin = DateTime.Now;

            return context.Set<ReportePedidoDTO>()
                .FromSqlRaw("SELECT * FROM dbo.V_ReportePedido WHERE fechaPedido BETWEEN @fechaInicio AND @fechaFin",
                    new SqlParameter("@fechaInicio", fechaInicio),
                    new SqlParameter("@fechaFin", fechaFin))
                .AsNoTracking()
                .ToList();
        });

        Assert.NotNull(reportePedidos);
        Assert.True(reportePedidos.Any(), "No se encontraron reportes de pedidos.");
    }

    [Fact]
    public void ConsultarReporteInventario()
    {
        using var context = GetContext();
        var inventario = context.V_ProductoInventario.ToList();

        Assert.NotNull(inventario);
        Assert.True(inventario.Any(), "No se encontró inventario.");
    }
}
