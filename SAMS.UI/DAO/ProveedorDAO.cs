using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
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

        public static List<V_Proveedores> ObtenerProveedores()
        {
            List<V_Proveedores> proveedores = new List<V_Proveedores>();

            var proveedoresData = from p in _sams.V_Proveedores
                                  select new
                                  {
                                      p.nombre,
                                      p.rfc,
                                      p.estadoProveedor
                                  };

            foreach (var proveedorData in proveedoresData)
            {

                V_Proveedores proveedor = new V_Proveedores
                {
                    nombre = proveedorData.nombre,
                    rfc = proveedorData.rfc,
                    estadoProveedor = proveedorData.estadoProveedor
                };

                proveedores.Add(proveedor);
            }

            return proveedores;
        }
    }
}
