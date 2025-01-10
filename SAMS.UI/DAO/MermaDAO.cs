using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;

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

    }
}
