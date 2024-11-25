using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    public class PedidoDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

        public static IEnumerable<PedidosDTO> ObtenerPedidos()
        {
            using (var context = new SAMSContext(App.ServiceProvider.GetRequiredService<DbContextOptions<SAMSContext>>()))
            {
                return context.V_Pedidos
                      .OrderByDescending(p => p.fechaPedido)
                      .ToList();
            }
        }

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

        public static async Task<bool> CambiarEstadoPedidoACancelado(string noPedido)
        {
            var parameters = new[]
            {
                new SqlParameter("@noPedido", noPedido)
            };

            try
            {
                await _sams.Database.ExecuteSqlRawAsync(
                    @"EXEC [dbo].[SP_CambiarEstadoPedidoCancelado] 
                    @noPedido",
                    parameters);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void RegistrarProductosAlPedido(List<DetallesPedidoDTO> productosPedidos)
        {
            DataTable productosDetalles = new DataTable();
            productosDetalles.Columns.Add("ProductoNombre", typeof(string)); 
            productosDetalles.Columns.Add("Cantidad", typeof(int));
 
            foreach (var producto in productosPedidos)
            {
                productosDetalles.Rows.Add(producto.nombreProducto, producto.cantidad);
            }

            var parametros = new List<Microsoft.Data.SqlClient.SqlParameter>
    {
        new Microsoft.Data.SqlClient.SqlParameter("@ProductosDetalle", SqlDbType.Structured)
        {
            TypeName = "TipoTablaPedidoDetalle", 
            Value = productosDetalles
        }
    };

            _sams.Database.ExecuteSqlRaw("EXEC T_RegistrarPedido @ProductosDetalle", parametros);
        }
    }
}
