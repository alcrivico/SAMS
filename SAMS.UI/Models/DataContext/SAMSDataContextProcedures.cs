#nullable disable
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMS.UI.DAO;
using SAMS.UI.DTO;
namespace SAMS.UI.Models.DataContext;

public partial class SAMSContextProcedure : ISAMSContextProcedure
{
    private readonly SAMSContext _context;

    public SAMSContextProcedure(SAMSContext context)
    {
        _context = context;
    }

    public async Task<List<V_ReporteVentaResult>> SP_ReporteVentaAsync(ProcedureParameter procedureParameter)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new[]
        {
                parameterreturnValue,
            };
        var _ = await _context.SqlQueryAsync<V_ReporteVentaResult>("EXEC @returnValue = [dbo].[SP_ReporteVenta]", sqlParameters, procedureParameter.cancellationToken);

        procedureParameter.returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }

    public async Task<int> T_CrearPromocionConVigenciaAsync(CrearPromocionVigenciaDTO crearPromocionVigencia, ProcedureParameter procedureParameter)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new[]
        {
            new SqlParameter
            {
                ParameterName = "nombre",
                Size = 100,
                Value = crearPromocionVigencia.nombre ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.NVarChar,
            },
            new SqlParameter
            {
                ParameterName = "porcentajeDescuento",
                Value = crearPromocionVigencia.porcentajeDescuento ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            },
            new SqlParameter
            {
                ParameterName = "cantMaxima",
                Value = crearPromocionVigencia.cantMaxima ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            },
            new SqlParameter
            {
                ParameterName = "cantMinima",
                Value = crearPromocionVigencia.cantMinima ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            },
            new SqlParameter
            {
                ParameterName = "fechaInicio",
                Value = crearPromocionVigencia.fechaInicio ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Date,
            },
            new SqlParameter
            {
                ParameterName = "fechaFin",
                Value = crearPromocionVigencia.fechaFin ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Date,
            },
            new SqlParameter
            {
                ParameterName = "productoInventarioId",
                Value = crearPromocionVigencia.productoInventarioId ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            },
            parameterreturnValue,
        };
        var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[T_CrearPromocionConVigencia] @nombre = @nombre, @porcentajeDescuento = @porcentajeDescuento, @cantMaxima = @cantMaxima, @cantMinima = @cantMinima, @fechaInicio = @fechaInicio, @fechaFin = @fechaFin, @productoInventarioId = @productoInventarioId", sqlParameters, procedureParameter.cancellationToken);

        procedureParameter.returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }

    public virtual async Task<int> T_EditarPromocionAsync(EditarPromocionDTO editarPromocion, ProcedureParameter procedureParameter)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new[]
        {
            new SqlParameter
            {
                ParameterName = "promocionId",
                Value = editarPromocion.promocionId ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            },
            new SqlParameter
            {
                ParameterName = "nombre",
                Size = 200,
                Value = editarPromocion.nombre ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.NVarChar,
            },
            new SqlParameter
            {
                ParameterName = "porcentajeDescuento",
                Value = editarPromocion.porcentajeDescuento ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            },
            new SqlParameter
            {
                ParameterName = "fechaInicio",
                Value = editarPromocion.fechaInicio ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Date,
            },
            new SqlParameter
            {
                ParameterName = "fechaFin",
                Value = editarPromocion.fechaFin ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Date,
            },
            new SqlParameter
            {
                ParameterName = "idProductoInventarioList",
                Value = editarPromocion.idProductoInventarioList ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Structured,
                TypeName = "[dbo].[ProductoInventarioIDList]",
            },
            parameterreturnValue,
        };
        var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[T_EditarPromocion] @idPromocion = @idPromocion, @nombre = @nombre, @porcentajeDescuento = @porcentajeDescuento, @fechaInicio = @fechaInicio, @fechaFin = @fechaFin, @idProductoInventarioList = @idProductoInventarioList", sqlParameters, procedureParameter.cancellationToken);

        procedureParameter.returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }
    public virtual async Task<int> T_FinalizarPromocionAsync(int? idPromocion, ProcedureParameter procedureParameter)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new[]
        {
            new SqlParameter
            {
                ParameterName = "idPromocion",
                Value = idPromocion ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            },
            parameterreturnValue,
        };
        var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[T_FinalizarPromocion] @idPromocion = @idPromocion", sqlParameters, procedureParameter.cancellationToken);

        procedureParameter.returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }
}