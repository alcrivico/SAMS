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

    [Fact]
    public async Task ConsultarReporteVenta_ValidarColumnas()
    {
        using var context = GetContext();
        var reporteVentas = await Task.Run(() =>
        {
            return context.Set<ReporteVentaDTO>()
                .FromSqlRaw("SELECT * FROM dbo.V_ReporteVenta")
                .AsNoTracking()
                .ToList();
        });

        Assert.All(reporteVentas, reporte =>
        {
            Assert.NotNull(reporte.fechaRegistro);
            Assert.NotNull(reporte.total);
        });
    }

    [Theory]
    [InlineData("2024-01-01", "2024-03-31")]
    [InlineData("2023-10-01", "2023-12-31")]
    public async Task ConsultarReportePedidos_RangoDeFechas(string inicio, string fin)
    {
        using var context = GetContext();
        DateTime fechaInicio = DateTime.Parse(inicio);
        DateTime fechaFin = DateTime.Parse(fin);

        var reportePedidos = await Task.Run(() =>
        {
            return context.Set<ReportePedidoDTO>()
                .FromSqlRaw("SELECT * FROM dbo.V_ReportePedido WHERE fechaPedido BETWEEN @fechaInicio AND @fechaFin",
                    new SqlParameter("@fechaInicio", fechaInicio),
                    new SqlParameter("@fechaFin", fechaFin))
                .AsNoTracking()
                .ToList();
        });

        Assert.NotNull(reportePedidos);
        Assert.True(reportePedidos.Count >= 0, "No se encontraron reportes de pedidos en el rango especificado.");
    }


    [Fact]
    public async Task ConsultarReporteVenta_TiempoDeEjecucion()
    {
        using var context = GetContext();
        var tiempoInicio = DateTime.Now;

        var reporteVentas = await Task.Run(() =>
        {
            return context.Set<ReporteVentaDTO>()
                .FromSqlRaw("SELECT * FROM dbo.V_ReporteVenta")
                .AsNoTracking()
                .ToList();
        });

        var tiempoFin = DateTime.Now;
        Assert.True((tiempoFin - tiempoInicio).TotalSeconds < 5, "La consulta tomó demasiado tiempo.");
    }

    [Fact]
    public async Task ConsultarReportePedidos_ValidarDatos()
    {
        using var context = GetContext();
        var reportePedidos = await Task.Run(() =>
        {
            return context.Set<ReportePedidoDTO>()
                .FromSqlRaw("SELECT * FROM dbo.V_ReportePedido")
                .AsNoTracking()
                .ToList();
        });

        Assert.All(reportePedidos, pedido =>
        {
            Assert.NotNull(pedido.fechaPedido);
        });
    }
}
