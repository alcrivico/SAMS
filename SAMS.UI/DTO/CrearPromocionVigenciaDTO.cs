namespace SAMS.UI.DAO
{
    public class CrearPromocionVigenciaDTO
    {
        public required string nombre { get; set; }
        public int? porcentajeDescuento { get; set; }
        public int? cantMaxima { get; set; }
        public int? cantMinima { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int? productoInventarioId { get; set; }
    }
}
