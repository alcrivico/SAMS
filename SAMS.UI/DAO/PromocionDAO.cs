using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using System.Data;

namespace SAMS.UI.DAO;

public class PromocionDAO
{
    private readonly SAMSContext context;

    public PromocionDAO(SAMSContext context) => this.context = context;

    public async Task<bool> CrearPromocionConVigencia(CrearPromocionVigenciaDTO crearPromocionVigenciaDTO)
    {
        var parameters = new[]
        {
            new SqlParameter("@nombre", crearPromocionVigenciaDTO.nombre),
            new SqlParameter("@porcentajeDescuento", crearPromocionVigenciaDTO.porcentajeDescuento),
            new SqlParameter("@cantMaxima", crearPromocionVigenciaDTO.cantMaxima),
            new SqlParameter("@cantMinima", crearPromocionVigenciaDTO.cantMinima),
            new SqlParameter("@fechaInicio", crearPromocionVigenciaDTO.fechaInicio),
            new SqlParameter("@fechaFin", crearPromocionVigenciaDTO.fechaFin),
            new SqlParameter("@idProductoInventario", crearPromocionVigenciaDTO.productoInventarioId)
        };

        int result = await context.Database.ExecuteSqlRawAsync(
            @"EXEC [dbo].[T_CrearPromocionConVigencia] 
            @nombre, @porcentajeDescuento, @cantMaxima, 
            @cantMinima, @fechaInicio, @fechaFin, 
            @idProductoInventario",
            parameters);

        return result > 0;
    }

    public async Task<bool> EditarPromocion(EditarPromocionDTO editarPromocionDTO)
    {
        if (editarPromocionDTO.ProductoInventarioIdList == null)
        {
            editarPromocionDTO.ProductoInventarioIdList = new DataTable();
            editarPromocionDTO.ProductoInventarioIdList.Columns.Add("productoInventarioId", typeof(int));
        }

        var parameters = new[]
        {
        new SqlParameter("@promocionId", editarPromocionDTO.promocionId ?? (object)DBNull.Value),
        new SqlParameter("@nombre", editarPromocionDTO.nombre),
        new SqlParameter("@porcentajeDescuento", editarPromocionDTO.porcentajeDescuento ?? (object)DBNull.Value),
        new SqlParameter("@fechaInicio", editarPromocionDTO.fechaInicio ?? (object)DBNull.Value),
        new SqlParameter("@fechaFin", editarPromocionDTO.fechaFin ?? (object)DBNull.Value),
        new SqlParameter("@productoInventarioIdList", SqlDbType.Structured)
        {
            TypeName = "dbo.productoInventarioIdList",
            Value = editarPromocionDTO.ProductoInventarioIdList
        }
    };

        int result = await context.Database.ExecuteSqlRawAsync(
            @"EXEC [dbo].[T_EditarPromocion] 
            @promocionId, @nombre, @porcentajeDescuento, 
            @fechaInicio, @fechaFin, @productoInventarioIdList",
            parameters);

        return result > 0;
    }

    public async Task<bool> FinalizarPromocion(int promocionId)
    {
        var parameters = new[]
        {
        new SqlParameter("@promocionId", promocionId)
    };

        int result = await context.Database.ExecuteSqlRawAsync(
            @"EXEC [dbo].[T_FinalizarPromocion] @promocionId",
            parameters);

        return result > 0;
    }

    public IEnumerable<V_Promocion> VerPromociones() => context.V_Promocion.ToList();
}
