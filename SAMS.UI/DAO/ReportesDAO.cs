using Microsoft.EntityFrameworkCore;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;

namespace SAMS.UI.DAO;

public class ReportesDAO
{
    private readonly SAMSContext context;

    public ReportesDAO(SAMSContext context) => this.context = context;

    public async Task<IEnumerable<V_ReporteVenta>> ReporteVentas ()
    {
        var result = await context.Set<V_ReporteVenta>()
            .FromSqlRaw("EXEC [dbo].[SP_ReporteVenta]").ToListAsync();

        return result;
    }

    public IEnumerable<V_Pedido> ReportePedidos () => context.V_Pedido.ToList();

    public IEnumerable<V_ProductoInventario> ReporteInventario () 
        => context.V_ProductoInventario.ToList();
}
