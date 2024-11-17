using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class PedidosPendientesDTO
    {
        public string NoPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string NombreProveedor { get; set; }
    }
}
