using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class ProductosRegistradosDTO
    {
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string cantidad { get; set; }
        public Decimal precioActual { get; set; }
        public string nombreCategoria { get; set; }
    }
}
