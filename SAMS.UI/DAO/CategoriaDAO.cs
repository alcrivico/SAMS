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
    internal class CategoriaDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();
        public static IEnumerable<CategoriaDTO> ObtenerCategorias() => _sams.V_Categorias.ToList();
    }
}
