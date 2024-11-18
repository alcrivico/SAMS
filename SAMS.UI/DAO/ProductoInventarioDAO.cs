using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;

namespace SAMS.UI.DAO;

public class ProductoInventarioDAO
{
    private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

    public static List<ProductoInventarioPromocionDTO> OptenerProductosSinPromocion()
    {
        try
        {
            // Intenta obtener la lista de productos
            return _sams.V_ProductoInventarioPromocion.ToList();
        }
        catch (Exception ex)
        {
            // Captura la excepción y escribe el error en la consola o en un log
            Console.WriteLine("Error al obtener productos sin promoción: " + ex.Message);
            Console.WriteLine("StackTrace: " + ex.StackTrace);

            // Retorna una lista vacía en caso de error
            return new List<ProductoInventarioPromocionDTO>();
        }
    }

}
