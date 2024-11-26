using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class DetallesPedidoDTO
    {
        public int idPedido {  get; set; }
        public string nombreProducto { get; set; }
        public string? nombreUnidadMedida { get; set; }
        public int cantidad {  get; set; }
        public decimal? precioCompra {  get; set; }

    }
}
