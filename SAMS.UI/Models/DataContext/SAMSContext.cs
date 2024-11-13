using Microsoft.EntityFrameworkCore;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
namespace SAMS.UI.Models.DataContext;

public class SAMSContext : DbContext
{
    public SAMSContext(DbContextOptions<SAMSContext> options)
            : base(options) { }

    public DbSet<Caja> Caja { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<DetallePedido> DetallePedido { get; set; }
    public DbSet<DetalleVenta> DetalleVenta { get; set; }
    public DbSet<Empleado> Empleado { get; set; }
    public DbSet<EstadoPedido> EstadoPedido { get; set; }
    public DbSet<EstadoProducto> EstadoProducto { get; set; }
    public DbSet<Merma> Merma { get; set; }
    public DbSet<Monedero> Monedero { get; set; }
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

    // Configurar las vistas como una entidad sin clave
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SP_ReporteVentaResult>()
            .HasNoKey()
            .ToView("SP_ReporteVentaResult");
        modelBuilder.Entity<V_EmpleadoDetalle>()
            .HasNoKey()
            .ToView("V_EmpleadoDetalle");
        modelBuilder.Entity<V_Empleados>()
            .HasNoKey()
            .ToView("V_Empleados");
        modelBuilder.Entity<V_Producto>()
            .HasNoKey()
            .ToView("V_Producto");
        modelBuilder.Entity<V_ProductoInventario>()
            .HasNoKey()
            .ToView("V_ProductoInventario");
        modelBuilder.Entity<V_Promocion>()
            .HasNoKey()
            .ToView("V_Promocion");
        modelBuilder.Entity<V_Proveedores>()
            .HasNoKey()
            .ToView("V_Proveedores");

        base.OnModelCreating(modelBuilder);
    }

}