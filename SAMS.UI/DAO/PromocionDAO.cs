using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using System.Data;

namespace SAMS.UI.DAO;

public class PromocionDAO
{
    private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

    public static async Task<bool> CrearPromocionConVigencia(CrearPromocionVigenciaDTO crearPromocionVigenciaDTO)
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

        int result = await _sams.Database.ExecuteSqlRawAsync(
            @"EXEC [dbo].[T_CrearPromocionConVigencia] 
            @nombre, @porcentajeDescuento, @cantMaxima, 
            @cantMinima, @fechaInicio, @fechaFin, 
            @idProductoInventario",
            parameters);

        return result > 0;
    }

    public static async Task<bool> EditarPromocion(EditarPromocionDTO editarPromocionDTO)
    {

        var parameters = new[]
        {
            new SqlParameter("@promocionId", editarPromocionDTO.promocionId ?? (object)DBNull.Value),
            new SqlParameter("@nombre", editarPromocionDTO.nombre),
            new SqlParameter("@porcentajeDescuento", editarPromocionDTO.porcentajeDescuento ?? (object)DBNull.Value),
            new SqlParameter("@fechaInicio", editarPromocionDTO.fechaInicio ?? (object)DBNull.Value),
            new SqlParameter("@fechaFin", editarPromocionDTO.fechaFin ?? (object)DBNull.Value),
          
        };

        int result = await _sams.Database.ExecuteSqlRawAsync(
            @"EXEC [dbo].[T_EditarPromocion] 
            @promocionId, @nombre, @porcentajeDescuento, 
            @fechaInicio, @fechaFin",
            parameters);

        return result > 0;
    }

    public static async Task<bool> FinalizarPromocion(int promocionId)
    {
        var parameters = new[]
        {
            new SqlParameter("@promocionId", promocionId)
        };

        int result = await _sams.Database.ExecuteSqlRawAsync(
            @"EXEC [dbo].[T_FinalizarPromocion] @promocionId",
            parameters);

        return result > 0;
    }

    public static List<PromocionesDTO> VerPromociones() =>
        _sams.V_Promocion.ToList();
}
