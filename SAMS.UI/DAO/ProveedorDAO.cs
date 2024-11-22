using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

        public static void RegistrarProveedorYProductos(Proveedor proveedor, string[] productos)
        {
            RegistrarProveedorDTO registrarProveedorDTO = new RegistrarProveedorDTO();

            if (registrarProveedorDTO.productos == null)
            {
                registrarProveedorDTO.productos = new DataTable();
                registrarProveedorDTO.productos.Columns.Add("Codigo");
                registrarProveedorDTO.productos.Columns.Add("Descripcion");
                registrarProveedorDTO.productos.Columns.Add("EsDevolvible");
                registrarProveedorDTO.productos.Columns.Add("EsPerecedero");
                registrarProveedorDTO.productos.Columns.Add("Nombre");
                registrarProveedorDTO.productos.Columns.Add("UnidadDeMedida");
            }

            foreach (var producto in productos)
            {
                string[] valores = producto.Split(',');

                if (valores.Length == 6)
                {
                    registrarProveedorDTO.productos.Rows.Add(
                        valores[0].Trim(),
                        valores[1].Trim(),
                        valores[2].Trim().ToLower(),
                        valores[3].Trim().ToLower(),
                        valores[4].Trim(),
                        valores[5].Trim()
                        
                    );
                }
                else
                {
                    throw new ArgumentException("El formato de los productos es incorrecto.");
                }
            }

            var parametros = new[]
            {
                new SqlParameter("@rfc", proveedor.rfc),
                new SqlParameter("@nombre", proveedor.nombre),
                new SqlParameter("@correo", proveedor.correo),
                new SqlParameter("@telefono", proveedor.telefono),
                new SqlParameter
                {
                    ParameterName = "@productos",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.TipoProducto",
                    Value = registrarProveedorDTO.productos
                }
            };

            _sams.Database.ExecuteSqlRaw("EXEC SP_RegistrarProveedorYProductos @rfc, @nombre, @correo, @telefono, @productos", parametros);
        }

    }
}
