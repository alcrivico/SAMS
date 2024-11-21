using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.VisualComponents;

namespace SAMS.UI.DAO;

public class ProductoInventarioDAO
{
    private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();
    
    private readonly SAMSContext context;
    public ProductoInventarioDAO(SAMSContext context) => this.context = context;
    public static List<ProductoInventarioPromocionDTO> OptenerProductosSinPromocion() =>
            _sams.V_ProductoInventarioPromocion.ToList();


    public static List<ProductosRegistradosDTO> ObtenerProductosRegistrados()
    {
        List<ProductosRegistradosDTO> productos = new List<ProductosRegistradosDTO>();

        var productosData = from p in _sams.V_ProductosRegistrados
                            select new
                            {
                                p.codigoProducto,
                                p.nombreProducto,
                                p.cantidad,
                                p.precioActual,
                                p.nombreCategoria
                            };

        if (productosData == null)
        {
            return null;
        }

        foreach (var productoData in productosData)
        {
            ProductosRegistradosDTO producto = new ProductosRegistradosDTO
            {
                codigoProducto = productoData.codigoProducto,
                nombreProducto = productoData.nombreProducto,
                cantidad = productoData.cantidad,
                precioActual = productoData.precioActual,
                nombreCategoria = productoData.nombreCategoria
            };

            productos.Add(producto);
        }
        return productos;
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

    public async Task<bool> EditarProductoInventario(EditarProductoInventarioDTO editarProductoInventarioDTO)
    {
        if (editarProductoInventarioDTO == null)
        {
            return false;
        }

        var parameters = new[]
        {
        new SqlParameter("@codigoProducto", editarProductoInventarioDTO.codigoProducto ?? (object)DBNull.Value),
        new SqlParameter("@descripcion", editarProductoInventarioDTO.descripcion ?? (object)DBNull.Value),
        new SqlParameter("@cantidadBodega", editarProductoInventarioDTO.cantidadBodega),
        new SqlParameter("@cantidadExhibicion", editarProductoInventarioDTO.cantidadExhibicion),
        new SqlParameter("@precioActual", editarProductoInventarioDTO.precioActual),
        new SqlParameter("@fechaCaducidad", editarProductoInventarioDTO.fechaCaducidad.ToString("yyyy-MM-dd")),
        new SqlParameter("@nombreCategoria", editarProductoInventarioDTO.nombreCategoria ?? (object)DBNull.Value),
        new SqlParameter("@nombreUnidadMedida", editarProductoInventarioDTO.nombreUnidadMedida ?? (object)DBNull.Value),
        new SqlParameter("@esPerecedero", editarProductoInventarioDTO.esPerecedero),
        new SqlParameter("@esDevolvible", editarProductoInventarioDTO.esDevolvible)
    };

        // Ejecutar el procedimiento almacenado
        int result = await context.Database.ExecuteSqlRawAsync(
            @"EXEC [dbo].[T_EditarProductoInventario] 
        @codigoProducto, @descripcion, @cantidadBodega, @cantidadExhibicion, 
        @precioActual, @fechaCaducidad, @nombreCategoria, @nombreUnidadMedida, 
        @esPerecedero, @esDevolvible",
            parameters);

        return result > 0;
    }
}
