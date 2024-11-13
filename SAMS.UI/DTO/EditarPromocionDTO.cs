using System.Data;

namespace SAMS.UI.DAO
{
    public class EditarPromocionDTO
    {
        public int? promocionId { get; set; }
        public required string nombre { get; set; }
        public int? porcentajeDescuento { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public required DataTable idProductoInventarioList { get; set; }
    }
}
