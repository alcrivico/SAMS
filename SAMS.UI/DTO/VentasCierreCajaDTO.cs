using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    
    public class VentasCierreCajaDTO
    {

        public int noVenta { get; set; }

        public DateTime fechaRegistro { get; set; }

        public int cantidad { get; set; }

        public decimal precioVenta { get; set; }

        public decimal ganancia { get; set; }

        public required String noCaja { get; set; }

        public required String nombreEmpleado { get; set; }

    }

}
