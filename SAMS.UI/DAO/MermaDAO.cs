using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;

namespace SAMS.UI.DAO
{
    internal class MermaDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();
        public static IEnumerable<MermaDTO> ObtenerMermas() => _sams.V_Mermas.ToList();

        public static MermaDTO ObtenerMermaPorId(int idMerma)
        {
            return _sams.V_Mermas
                .Where(m => m.MermaId == idMerma) 
                .Select(m => new MermaDTO
                {
                    MermaId = m.MermaId,
                    cantidad = m.cantidad,
                    descripcion = m.descripcion,
                    fechaRegistro = m.fechaRegistro,
                    productoInventario = m.productoInventario
                })
                .FirstOrDefault();
        }

        public static bool RegistrarMerma(int productoId, string lugarDescuento, int cantidad, string descripcion)
        {
            try
            {
                using (var context = new SAMSContext(App.ServiceProvider.GetRequiredService<DbContextOptions<SAMSContext>>()))
                {
                    // Crear los parámetros para el procedimiento almacenado
                    var parametros = new List<Microsoft.Data.SqlClient.SqlParameter>
            {
                new Microsoft.Data.SqlClient.SqlParameter("@ProductoId", productoId),
                new Microsoft.Data.SqlClient.SqlParameter("@LugarDescuento", lugarDescuento),
                new Microsoft.Data.SqlClient.SqlParameter("@Cantidad", cantidad),
                new Microsoft.Data.SqlClient.SqlParameter("@Descripcion", descripcion)
            };

                    // Ejecutar el procedimiento almacenado
                    context.Database.ExecuteSqlRaw("EXEC T_RegistrarMerma @ProductoId, @LugarDescuento, @Cantidad, @Descripcion", parametros);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al registrar la merma: {ex.Message}");
                return false;
            }
        }

    }
}
