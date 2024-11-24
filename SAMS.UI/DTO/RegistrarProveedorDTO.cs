using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    class RegistrarProveedorDTO
    {
        public string rfc {  get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public DataTable productos { get; set; }

    }
}
