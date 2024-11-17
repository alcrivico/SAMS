using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DTO
{
    public class BusquedaMonederoDTO
    {

        public decimal saldo { get; set; }

        public required string codigoDeBarras { get; set; }

    }

}
