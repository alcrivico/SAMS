namespace SAMS.UI.DTO
{
    public class VentasDTO
    {

        public required int noVenta { get; set; }

        public decimal totalVenta { get; set; }

        public DateTime fechaRegistro { get; set; }

        public required string noCaja { get; set; }

        public required string nombreEmpleado { get; set; }

    }

}
