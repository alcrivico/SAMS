using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;

namespace SAMS.UI.DAO;

public class ProductoInventarioDAO
{
    private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

    public static List<ProductoInventarioPromocionDTO> OptenerProductosSinPromocion() =>
            _sams.V_ProductoInventarioPromocion.ToList();

}
