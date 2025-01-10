using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class MermaDTO
    {
        public int MermaId { get; set; }
        public int cantidad {  get; set; }
        public string descripcion {  get; set; }
        public DateTime fechaRegistro { get; set; }
        public string productoInventario { get; set; }
    }
}
