using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class DetalleVentasDTO
    {

        public required string nombre { get; set; }

        public int cantidad { get; set; }

        public decimal precioVenta { get; set; }

        public decimal ganancia { get; set; }

        public required string noVenta { get; set; }

        public DateTime fechaRegistro { get; set; }

        public required string noCaja { get; set; }

        public required string nombrePromocion { get; set; }

    }

}
