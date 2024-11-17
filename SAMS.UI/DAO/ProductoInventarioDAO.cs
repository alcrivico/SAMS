using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using System.Data;

namespace SAMS.UI.DAO;

public class ProductoInventarioDAO
{
    private readonly SAMSContext context;

    public ProductoInventarioDAO(SAMSContext context) => this.context = context;

    public IEnumerable<ReporteProductoInventarioDTO> VerProductoInventario() => context.V_ProductoInventario.ToList();
}
