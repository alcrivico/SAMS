using Microsoft.EntityFrameworkCore;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;

namespace SAMS.Models.DataContext;

public class SAMSContext : DbContext
{
    public SAMSContext(DbContextOptions<SAMSContext> options)
            : base(options) { }

    public DbSet<Caja> Caja { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<DetallePedido> DetallePedido { get; set; }
    public DbSet<DetalleVenta> DetalleVenta { get; set; }
    public DbSet<Empleado> Empleado { get; set; }
    public DbSet<EstadoPedido> EmpleadoPedido { get; set; }
    public DbSet<EstadoProducto> EmpleadoProducto { get; set; }
    public DbSet<Merma> Merma { get; set; }
    public DbSet<Pedido> Pedido { get; set; }
    public DbSet<Producto> Producto { get; set; }
    public DbSet<ProductoInventario> ProductoInventario { get; set; }
    public DbSet<Promocion> Promocion { get; set; }
    public DbSet<PromocionVigencia> PromocionVigencia { get; set; }
    public DbSet<Proveedor> Proveedor { get; set; }
    public DbSet<Puesto> Puesto { get; set; }
    public DbSet<UnidadDeMedida> UnidadDeMedida { get; set; }
    public DbSet<Venta> Venta { get; set; }

    //vistas
    public DbSet<SP_ReporteVentaResult> SP_ReporteVentaResult { get; set; }
    public DbSet<V_EmpleadoDetalle> V_EmpleadoDetalle { get; set; }
    public DbSet<V_Empleados> V_Empleados { get; set; }
    public DbSet<V_Producto> V_Producto { get; set; }
    public DbSet<V_ProductoInventario> V_ProductoInventario { get; set; }
    public DbSet<V_Promocion> V_Promocion { get; set; }
    public DbSet<V_Proveedores> V_Proveedores { get; set; }

}