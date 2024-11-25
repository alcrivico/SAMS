using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.VisualComponents;
using System.Diagnostics;

namespace SAMS.UI.DAO;

public class ProductoInventarioDAO
{
    private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

    public static List<ProductoInventarioPromocionDTO> OptenerProductosSinPromocion() =>
            _sams.V_ProductoInventarioPromocion.ToList();

    public static IEnumerable<ProductosRegistradosDTO> ObtenerProductosRegistrados()
    {
        using (var context = new SAMSContext(App.ServiceProvider.GetRequiredService<DbContextOptions<SAMSContext>>()))
        {
            return context.V_ProductosRegistrados.ToList();
        }
    }

    public static DetalleProductoDTO ObtenerDetalleProductoInventario(string codigoProducto)
    {
        var productoData = (from p in _sams.V_DetalleProducto
                            where p.codigoProducto == codigoProducto
                            select new
                            {
                                p.codigoProducto,
                                p.nombreProducto,
                                p.descripcion,
                                p.cantidadBodega,
                                p.cantidadExhibicion,
                                p.precioActual,
                                p.fechaCaducidad,
                                p.nombreCategoria,
                                p.nombreUnidadMedida,
                                p.esPerecedero,
                                p.esDevolvible
                            }).FirstOrDefault();

        if (productoData == null)
        {
            return null;
        }

        return new DetalleProductoDTO
        {
            codigoProducto = productoData.codigoProducto,
            nombreProducto = productoData.nombreProducto,
            descripcion = productoData.descripcion,
            cantidadBodega = productoData.cantidadBodega,
            cantidadExhibicion = productoData.cantidadExhibicion,
            precioActual = productoData.precioActual,
            fechaCaducidad = productoData.fechaCaducidad,
            nombreCategoria = productoData.nombreCategoria,
            nombreUnidadMedida = productoData.nombreUnidadMedida,
            esPerecedero = productoData.esPerecedero,
            esDevolvible = productoData.esDevolvible
        };
    }

    public static List<CategoriaDTO> ObtenerCategorias()
    {
        var categorias = _sams.Categoria
                              .Where(c => c.estado)
                              .Select(c => new CategoriaDTO
                              {
                                  nombre = c.nombre
                              })
                              .ToList();

        return categorias;
    }

    public static List<UnidadDeMedidaDTO> ObtenerUnidadesDeMedida()
    {
        var unidadesDeMedida = _sams.UnidadDeMedida
                                    .Select(u => new UnidadDeMedidaDTO
                                    {
                                        nombre = u.nombre
                                    })
                                    .ToList();

        return unidadesDeMedida;
    }

    public static async Task<bool> EditarProductoInventario(EditarProductoInventarioDTO editarProductoInventarioDTO)
    {
        var parameters = new[]
        {
            new SqlParameter("@codigoProducto", editarProductoInventarioDTO.codigoProducto),
            new SqlParameter("@descripcion", editarProductoInventarioDTO.descripcion),
            new SqlParameter("@cantidadBodega", editarProductoInventarioDTO.cantidadBodega),
            new SqlParameter("@cantidadExhibicion", editarProductoInventarioDTO.cantidadExhibicion),
            new SqlParameter("@precioActual", editarProductoInventarioDTO.precioActual),
            new SqlParameter("@fechaCaducidad", editarProductoInventarioDTO.fechaCaducidad.ToString("yyyy-MM-dd")),
            new SqlParameter("@nombreCategoria", editarProductoInventarioDTO.nombreCategoria),
            new SqlParameter("@nombreUnidadMedida", editarProductoInventarioDTO.nombreUnidadMedida),
            new SqlParameter("@esPerecedero", editarProductoInventarioDTO.esPerecedero),
            new SqlParameter("@esDevolvible", editarProductoInventarioDTO.esDevolvible)
        };

        // Ejecutar el procedimiento almacenado
        try
        {
            // Ejecutar el procedimiento almacenado
            await _sams.Database.ExecuteSqlRawAsync(
                @"EXEC [dbo].[T_EditarProductoInventario] 
                @codigoProducto, @descripcion, @cantidadBodega, @cantidadExhibicion, 
                @precioActual, @fechaCaducidad, @nombreCategoria, @nombreUnidadMedida, 
                @esPerecedero, @esDevolvible",
                parameters);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static List<PedidosPendientesDTO> ObtenerPedidosPendientes()
    {
        List<PedidosPendientesDTO> pedidosPendientes = new List<PedidosPendientesDTO>();

        var pedidosData = from p in _sams.V_PedidosPendientes
                          select new
                          {
                              p.noPedido,
                              p.fechaPedido,
                              p.nombreProveedor
                          };

        if (pedidosData == null)
        {
            return null;
        }

        foreach (var pedidoData in pedidosData)
        {
            PedidosPendientesDTO pedido = new PedidosPendientesDTO
            {
                noPedido = pedidoData.noPedido,
                fechaPedido = pedidoData.fechaPedido,
                nombreProveedor = pedidoData.nombreProveedor
            };

            pedidosPendientes.Add(pedido);
        }

        return pedidosPendientes;
    }

    public static List<ProductosPorPedidoDTO> ObtenerProductosPorPedido(string noPedido)
    {
        List<ProductosPorPedidoDTO> productosPorPedido = new List<ProductosPorPedidoDTO>();

        var productosData = from p in _sams.V_ProductosPorPedido
                            where p.numeroPedido == noPedido
                            select new
                            {
                                p.numeroPedido,
                                p.codigoProducto,
                                p.nombreProducto,
                                p.cantidad,
                                p.precioCompra
                            };

        if (productosData == null)
        {
            return null;
        }

        foreach (var productoData in productosData)
        {
            ProductosPorPedidoDTO producto = new ProductosPorPedidoDTO
            {
                numeroPedido = productoData.numeroPedido,
                codigoProducto = productoData.codigoProducto,
                nombreProducto = productoData.nombreProducto,
                cantidad = productoData.cantidad,
                precioCompra = productoData.precioCompra
            };

            productosPorPedido.Add(producto);
        }

        return productosPorPedido;
    }

    public static async Task<bool> RegistrarProductosInventario(List<RegistrarProductoInventarioDTO> productosInventario)
    {
        bool resultado = true;

        foreach (var producto in productosInventario)
        {
            var parameters = new[]
            {
            new SqlParameter("@noPedido", producto.noPedido),
            new SqlParameter("@codigoProducto", producto.codigoProducto),
            new SqlParameter("@nombreCategoria", producto.nombreCategoria),
            new SqlParameter("@precioActual", producto.precioActual),
            new SqlParameter("@fechaCaducidad", producto.fechaCaducidad)
        };

            try
            {
                await _sams.Database.ExecuteSqlRawAsync(
                    @"EXEC [dbo].[T_RegistrarProductoInventario] 
                @noPedido, @codigoProducto, @nombreCategoria, @precioActual, @fechaCaducidad",
                    parameters);
            }
            catch
            {
                resultado = false;
                break;
            }
        }

        return resultado;
    }

    public static async Task<bool> CambiarEstadoProductoAgotado(string codigoProducto)
    {
        var parameters = new[]
        {
            new SqlParameter("@codigoProducto", codigoProducto)
        };

        try
        {
            await _sams.Database.ExecuteSqlRawAsync(
                @"EXEC [dbo].[SP_CambiarEstadoProductoAgotado] 
                @codigoProducto",
                parameters);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return false;
        }
    }
}
