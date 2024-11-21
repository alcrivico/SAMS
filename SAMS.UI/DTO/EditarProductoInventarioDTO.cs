using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class EditarProductoInventarioDTO
    {
        public string codigoProducto { get; set; }
        public string descripcion { get; set; }
        public int cantidadBodega { get; set; }
        public int cantidadExhibicion { get; set; }
        public decimal precioActual { get; set; }
        public DateTime fechaCaducidad { get; set; }
        public string nombreCategoria { get; set; }
        public string nombreUnidadMedida { get; set; }
        public bool esPerecedero { get; set; }
        public bool esDevolvible { get; set; }


    }
}
