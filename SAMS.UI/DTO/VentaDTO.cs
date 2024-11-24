using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class VentaDTO
    {

        public string noVenta { get; set; }

        public DateTime fechaRegistro { get; set; }

        public decimal iva { get; set; }

        public decimal totalEfectivo { get; set; }

        public decimal totalTarjeta { get; set; }

        public decimal totalMonedero { get; set; }

        public string noCaja { get; set; }

        public string nombreEmpleado { get; set; }

        public string codigoMonedero { get; set; }

    }

}
