namespace SAMS.UI.Models.DataContext
{
    public class ProcedureParameter
    {
        public OutputParameter<int> returnValue = null;
        public CancellationToken cancellationToken = default;
    }
}
