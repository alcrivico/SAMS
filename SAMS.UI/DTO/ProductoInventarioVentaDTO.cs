using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class ProductoInventarioVentaDTO
    {

        public string codigo { get; set; }

        public string nombre { get; set; }

        public decimal precio { get; set; }

        public int cantidadExhibicion { get; set; }
        
        public string unidadDeMedida { get; set; }

        public string promocion { get; set; }

        public int porcentajeDescuento { get; set; }

    }

}
