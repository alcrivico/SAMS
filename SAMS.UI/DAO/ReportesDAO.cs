using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;

namespace SAMS.UI.DAO;

public class ReportesDAO
{
    private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

    public static List<ReporteVentaDTO> ReporteVentas() => _sams.Set<ReporteVentaDTO>()
            .FromSqlRaw("EXEC [dbo].[SP_ReporteVenta]").ToList();

    public List<ReportePedidoDTO> ReportePedidos(DateTime? fechaInicio = null, DateTime? fechaFin = null)
    {
        //Se ejecuta el procedimiento almacenado sin parámetros
        if (fechaInicio == null && fechaFin == null)
        {
            return _sams.Set<ReportePedidoDTO>()
                .FromSqlRaw("EXEC [dbo].[SP_ReportePedido]")
                .ToList();
        }

        //Se ejecuta el procedimiento almacenado con parámetros
        var parameters = new[]
        {
        new SqlParameter("@fechaInicio", fechaInicio ?? DateTime.Now.AddMonths(-3)),
        new SqlParameter("@fechaFin", fechaFin ?? DateTime.Now)
    };

        return _sams.Set<ReportePedidoDTO>()
            .FromSqlRaw("EXEC [dbo].[SP_ReportePedido] @fechaInicio, @fechaFin", parameters)
            .ToList();
    }

    public static List<V_ProductoInventario> ReporteInventario() => 
            _sams.V_ProductoInventario.ToList();
}
