using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DAO
{
    public class VentaDAO
    {

        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

        public static List<VentasDTO> ObtenerVentas()
        {

            List<VentasDTO> ventas = _sams.V_Ventas.ToList();

            return ventas;

        }

    }

}
