using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;

namespace SAMS.UI.DAO;

public class ReportesDAO
{
    private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

    public static List<V_ReporteVenta> ReporteVentas() => _sams.Set<V_ReporteVenta>()
            .FromSqlRaw("EXEC [dbo].[SP_ReporteVenta]").ToList();

    public List<V_Pedido> ReportePedidos() => _sams.V_Pedido.ToList();

    public static List<V_ProductoInventario> ReporteInventario() => 
            _sams.V_ProductoInventario.ToList();
}
