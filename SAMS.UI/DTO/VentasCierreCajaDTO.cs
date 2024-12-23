namespace SAMS.UI.DTO
{

    public class VentasCierreCajaDTO
    {

        public int noVenta { get; set; }

        public DateTime fechaRegistro { get; set; }

        public decimal totalEfectivo { get; set; }

        public decimal totalTarjeta { get; set; }

        public decimal totalMonedero { get; set; }

        public decimal totalVenta { get; set; }

        public required String noCaja { get; set; }

        public required String nombreEmpleado { get; set; }

    }

}
