using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DAO
{
    public class VentaDAO
    {

        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

        public static List<VentasDTO> ObtenerVentas()
        {

            List<VentasDTO> ventas = _sams.V_Ventas.ToList();

            return ventas;

        }

        public static VentaDTO ObtenerVenta(int noVenta)
        {
            VentaDTO venta = _sams.V_Venta.Where(v => v.noVenta == noVenta).FirstOrDefault();
            return venta;
        }

        public static ProductoInventarioVentaDTO ObtenerProductoInventarioVenta(string codigoProducto)
        {
            ProductoInventarioVentaDTO producto = _sams.V_ProductoInventarioVenta.Where(p => p.codigo == codigoProducto).FirstOrDefault();
            return producto;
        }

        public static List<DetalleVentaDTO> ObtenerDetallesVenta(int noVenta)
        {
            List<DetalleVentaDTO> detalles = _sams.V_DetalleVenta.Where(d => d.noVenta == noVenta).ToList();
            return detalles;
        }

        public static async Task RegistrarVenta
            (
                decimal pagoEfectivo,
                decimal pagoTarjeta,
                decimal pagoMonedero,
                decimal iva,
                string codigoMonedero,
                string noEmpleado,
                string noCaja,
                bool tieneRedondeo,
                List<DetalleVentaDTO> detalles
            )
        {

            var detalleTable = new DataTable();

            detalleTable.Columns.Add("codigo", typeof(string));
            detalleTable.Columns.Add("cantidad", typeof(int));
            detalleTable.Columns.Add("precio", typeof(decimal));
            detalleTable.Columns.Add("total", typeof(decimal));

            foreach (var detalle in detalles)
            {
                Debug.WriteLine($"Adding to DataTable: codigo={detalle.codigo}, cantidad={detalle.cantidad}, precio={detalle.precio}, total={detalle.total}");
                detalleTable.Rows.Add(detalle.codigo, detalle.cantidad, detalle.precio, detalle.total);
            }

            var parameters = new[]
            {

                new SqlParameter("@pagoEfectivo", SqlDbType.Decimal) { Value = (object?) pagoEfectivo ?? DBNull.Value },
                new SqlParameter("@pagoTarjeta", SqlDbType.Decimal) { Value = (object?) pagoTarjeta ?? DBNull.Value },
                new SqlParameter("@pagoMonedero", SqlDbType.Decimal) { Value = (object?) pagoMonedero ?? DBNull.Value },
                new SqlParameter("@iva", SqlDbType.Decimal) { Value = iva },
                new SqlParameter("@codigoMonedero", SqlDbType.NVarChar) { Value = (object?)codigoMonedero ?? DBNull.Value },
                new SqlParameter("@noEmpleado", SqlDbType.NVarChar) { Value = noEmpleado },
                new SqlParameter("@noCaja", SqlDbType.NVarChar) { Value = noCaja },
                new SqlParameter("@tieneRedondeo", SqlDbType.Bit) { Value = tieneRedondeo },
                new SqlParameter("@detalles", SqlDbType.Structured)
                {
                    TypeName = "DetalleVentaType",
                    Value = detalleTable
                }

            };

            await _sams.Database.ExecuteSqlRawAsync("EXEC T_RegistrarVenta @pagoEfectivo, @pagoTarjeta, @pagoMonedero, @iva, @codigoMonedero, @noEmpleado, @noCaja, @tieneRedondeo, @detalles", parameters);

        }

    }

}
