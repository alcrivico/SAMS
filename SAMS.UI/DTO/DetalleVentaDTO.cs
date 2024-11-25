using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class DetalleVentaDTO
    {

        public string codigo { get; set; }
        public string nombreDetalleVenta { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public string promocion { get; set; }
        public decimal porcentajeDescuento { get; set; }
        public decimal total { get; set; }
        public int cantidadMinima { get; set; }
        public int cantidadMaxima { get; set; }
        public int noVenta { get; set; }

    }

}
