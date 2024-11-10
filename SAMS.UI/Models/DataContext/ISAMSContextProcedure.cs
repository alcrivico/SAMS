using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using System.Data;

namespace SAMS.Models.DataContext
{
    public interface ISAMSContextProcedure
    {
        Task<List<SP_ReporteVentaResult>> SP_ReporteVentaAsync(ProcedureParameter procedureParameter);
        Task<int> T_CrearPromocionConVigenciaAsync(DTO_CrearPromocionVigencia crearPromocionVigencia, ProcedureParameter procedureParameter);
        Task<int> T_EditarPromocionAsync(DTO_EditarPromocion editarPromocion, ProcedureParameter procedureParameter);
        Task<int> T_FinalizarPromocionAsync(int? idPromocion, ProcedureParameter procedureParameter);
    }
}
