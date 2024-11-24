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

namespace SAMS.UI.DAO
{
    public class PedidoDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

        public static IEnumerable<PedidosDTO> ObtenerPedidos() => _sams.V_Pedidos.ToList();

        public static PedidosDTO ObtenerPedidoPorProveedor(string nombreProveedor) => _sams.V_Pedidos.FirstOrDefault(p => p.nombreProveedor == nombreProveedor);

        public static PedidosDTO ObtenerPedidoPorNoPedido(string noPedido) => _sams.V_Pedidos.FirstOrDefault(p => p.noPedido == noPedido);

        public static IEnumerable<DetallesPedidoDTO> ObtenerDetallesPedidoPorId(int idPedido) => _sams.V_DetallesPedido.Where(p => p.idPedido == idPedido).ToList();

        public static async Task<bool> ActualizarEstadoPedido(ActualizarEstadoPedidoDTO actualizarEstadoPedidoDTO)
        {
            var parameters = new[]
            {
                new SqlParameter("@noPedido", actualizarEstadoPedidoDTO.noPedido),
                new SqlParameter("@fechaEntrega", actualizarEstadoPedidoDTO.fechaEntrega)
            };

            try
            {
                await _sams.Database.ExecuteSqlRawAsync(
                    @"EXEC [dbo].[T_ActualizarEstadoPedido] 
                    @noPedido, @fechaEntrega",
                    parameters);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
