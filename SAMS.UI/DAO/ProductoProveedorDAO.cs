using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SAMS.UI.DAO
{
    class ProductoProveedorDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

        public static IEnumerable<V_Producto> ObtenerListaProductosPorRfc(string rfc) => _sams.V_Producto.Where(p => p.rfc == rfc).ToList();

        public static void RegistrarProducto(string codigo, string descripcion, string esDevolvible, string esPerecedero, string nombre, string rfcProveedor, string unidadDeMedida)
        {
            _sams.Database.ExecuteSqlRaw("EXEC SP_RegistrarProducto @Codigo, @Descripcion, @EsDevolvible, @EsPerecedero, @Nombre, @ProveedorRFC, @UnidadDeMedida",
            new SqlParameter("@Codigo", codigo),
            new SqlParameter("@Descripcion", descripcion),
            new SqlParameter("@EsDevolvible", esDevolvible),
            new SqlParameter("@EsPerecedero", esPerecedero),
            new SqlParameter("@Nombre", nombre),
            new SqlParameter("@ProveedorRFC", rfcProveedor),
            new SqlParameter("@UnidadDeMedida", unidadDeMedida));
        }

    }
}
