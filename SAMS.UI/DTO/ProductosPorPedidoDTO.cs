using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class ProductosPorPedidoDTO
    {
        public string numeroPedido { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precioCompra { get; set; }
        
    }
}
