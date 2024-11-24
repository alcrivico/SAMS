using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class RegistrarProductoInventarioDTO
    {
        public string noPedido { get; set; }
        public string codigoProducto { get; set; }
        public string nombreCategoria { get; set; }
        public Decimal precioActual { get; set; }
        public DateTime fechaCaducidad { get; set; }

    }
}
