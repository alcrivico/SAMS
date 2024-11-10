using System.Data;

namespace SAMS.UI.DTO
{
    public class DTO_EditarPromocion
    {
        public int? idPromocion { get; set; }
        public required string nombre { get; set; }
        public int? porcentajeDescuento { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public required DataTable idProductoInventarioList { get; set; }
    }
}
