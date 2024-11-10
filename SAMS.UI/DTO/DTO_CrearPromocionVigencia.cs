namespace SAMS.UI.DTO
{
    public class DTO_CrearPromocionVigencia
    {
        public required string nombre { get; set; }
        public int? porcentajeDescuento { get; set; }
        public int? cantMaxima { get; set; }
        public int? cantMinima { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int? idProductoInventario { get; set; }
    }
}
