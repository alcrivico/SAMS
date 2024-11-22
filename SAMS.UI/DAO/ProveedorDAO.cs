using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DAO
{
    public class ProveedorDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

        public static IEnumerable<V_Proveedores> ObtenerProveedores() => _sams.V_Proveedores.ToList();
            
        public static V_Proveedores ObtenerProveedorPorRfc(string rfc) => _sams.V_Proveedores.FirstOrDefault(p => p.rfc == rfc);

        public static void RegistrarProveedor(Proveedor proveedor)
        {
            _sams.Proveedor.Add(proveedor);
            _sams.SaveChanges();
        }
    }
}
