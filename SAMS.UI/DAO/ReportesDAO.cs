using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;

namespace SAMS.UI.DAO;

public class ReportesDAO
{
    private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

    public static List<ReporteVentaDTO> ReporteVentas(DateTime? fechaRegistro = null)
    {
        // Si no se proporciona la fecha de ingreso, por defecto se usa la fecha de hoy
        fechaRegistro = fechaRegistro ?? DateTime.Now.Date;

        // Obtener el inicio y fin del día para la fecha proporcionada
        DateTime inicioDelDia = fechaRegistro.Value.Date;
        DateTime finDelDia = fechaRegistro.Value.Date.AddDays(1).AddMilliseconds(-1);

        // Ejecutar la consulta sobre la vista con la fecha de ingreso proporcionada
        return _sams.Set<ReporteVentaDTO>()
            .FromSqlRaw("SELECT * FROM dbo.V_ReporteVenta WHERE fechaRegistro BETWEEN @inicioDelDia AND @finDelDia",
                new SqlParameter("@inicioDelDia", inicioDelDia),
                new SqlParameter("@finDelDia", finDelDia))
            .AsNoTracking()
            .ToList();
    }

    public static List<ReportePedidoDTO> ReportePedidos(DateTime? fechaInicio = null, DateTime? fechaFin = null)
    {
        // Si no se proporcionan fechas se aplica un rango predeterminado
        if (fechaInicio == null && fechaFin == null)
        {
            fechaInicio = DateTime.Now.AddMonths(-3);
            fechaFin = DateTime.Now;
        }

        // Ejecuta la consulta sobre la vista con las fechas proporcionadas
        return _sams.Set<ReportePedidoDTO>()
            .FromSqlRaw("SELECT * FROM dbo.V_ReportePedido WHERE fechaPedido BETWEEN @fechaInicio AND @fechaFin",
                new SqlParameter("@fechaInicio", fechaInicio ?? DateTime.Now.AddMonths(-3)),
                new SqlParameter("@fechaFin", fechaFin ?? DateTime.Now))
            .AsNoTracking() 
            .ToList();
    }

    public static List<ReporteProductoInventarioDTO> ReporteInventario() => 
            _sams.V_ProductoInventario.ToList();
}
