using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class ProductoInventarioMermaDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int cantidadBodega { get; set; }
        public int cantidadExhibicion { get; set; }
        public string estadoProducto { get; set; }
    }
}
