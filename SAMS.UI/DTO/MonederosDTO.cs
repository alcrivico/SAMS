using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{

    public class MonederosDTO
    {

        public required string codigoDeBarras { get; set; }
        public decimal saldo { get; set; }
        public required string telefono { get; set; }
        public required string nombrePropietario { get; set; }

    }

}
