using SAMS.UI.Models.DataContext;

namespace SAMS.UI.DTO
{
    public class ProcedureParameter
    {
        public OutputParameter<int> returnValue = null;
        public CancellationToken cancellationToken = default;
    }
}
