using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class PedidosDTO
    {
        public string noPedido {  get; set; }
        public string nombreProveedor { get; set; }
        public DateTime fechaEntrega { get; set; }
        public string nombreEstado { get; set; }

    }
}
