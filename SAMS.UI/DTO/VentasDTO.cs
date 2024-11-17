using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class VentasDTO
    {

        public decimal precioVenta { get; set; }

        public int cantidad { get; set; }

        public required string noVenta { get; set; }

        public DateTime fechaRegistro { get; set; }

        public required string noCaja { get; set; }

        public required string nombreEmpleado { get; set; }

    }

}
