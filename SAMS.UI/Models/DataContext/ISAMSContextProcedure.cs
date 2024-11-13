using SAMS.UI.DAO;
using SAMS.UI.DTO;
namespace SAMS.UI.Models.DataContext;

public interface ISAMSContextProcedure
{
    Task<List<V_ReporteVentaResult>> SP_ReporteVentaAsync(ProcedureParameter procedureParameter);
    Task<int> T_CrearPromocionConVigenciaAsync(CrearPromocionVigenciaDTO crearPromocionVigencia, ProcedureParameter procedureParameter);
    Task<int> T_EditarPromocionAsync(EditarPromocionDTO editarPromocion, ProcedureParameter procedureParameter);
    Task<int> T_FinalizarPromocionAsync(int? idPromocion, ProcedureParameter procedureParameter);
}
