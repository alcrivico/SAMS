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
    public class PedidoDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

        public static IEnumerable<PedidosDTO> ObtenerPedidos() => _sams.V_Pedidos.ToList();

        public static PedidosDTO ObtenerPedidoPorProveedor(string nombreProveedor) => _sams.V_Pedidos.FirstOrDefault(p => p.nombreProveedor == nombreProveedor);
    }
}
