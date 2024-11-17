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

    public static List<ReportePedidoDTO> ReportePedidos(DateTime? fechaInicio = null, DateTime? fechaFin = null)
    {
        List<ReportePedidoDTO> resultado;

        if (fechaInicio == null && fechaFin == null)
        {
            resultado = _sams.Set<ReportePedidoDTO>()
                .FromSqlRaw("EXEC [dbo].[SP_ReportePedido]")
                .ToList();
        }
        else
        {
            var parameters = new[]
            {
            new SqlParameter("@fechaInicio", fechaInicio ?? DateTime.Now.AddMonths(-3)),
            new SqlParameter("@fechaFin", fechaFin ?? DateTime.Now)
        };

            resultado = _sams.Set<ReportePedidoDTO>()
                .FromSqlRaw("EXEC [dbo].[SP_ReportePedido] @fechaInicio, @fechaFin", parameters)
                .ToList();
        }

        return resultado;
    }


    public static List<V_ProductoInventario> ReporteInventario() => 
            _sams.V_ProductoInventario.ToList();
}
