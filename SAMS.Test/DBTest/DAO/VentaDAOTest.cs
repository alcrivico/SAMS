using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMS.UI.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Test.DBTest.DAO
{
    
    public class VentaDAOTest : SAMSContextTest
    {

        [Fact]
        public void ObtenerVentas_DeberiaRetornarVentas()
        {

            // Arrange
            using var context = GetContext();

            // Act
            var ventas = context.V_Ventas.ToList();

            // Assert
            Assert.NotNull(ventas);
            Assert.True(ventas.Any(), "No se encontraron ventas.");

        }

        [Fact]
        public void ObtenerVentaPorNoVenta_DeberiaRetornarVenta()
        {

            // Arrange
            using var context = GetContext();
            int noVentaCorrecto = 1001;

            // Act
            var venta = context.V_Venta.FirstOrDefault(v => v.noVenta == noVentaCorrecto);

            // Assert
            Assert.NotNull(venta);
            Assert.True(venta.noVenta == noVentaCorrecto, "No se encontró la venta.");

        }

        [Fact]
        public void ObtenerVentaPorNoVenta_NoDeberiaRetornarVenta()
        {

            // Arrange
            using var context = GetContext();
            int noVentaIncorrecto = 0;

            // Act
            var venta = context.V_Venta.FirstOrDefault(v => v.noVenta == noVentaIncorrecto);

            // Assert
            Assert.Null(venta);

        }

        [Fact]
        public void ObtenerProductoInventarioVentaPorCodigoProducto_DeberiaRetornarProducto()
        {

            // Arrange
            using var context = GetContext();
            string codigoProductoCorrecto = "7501000122223";

            // Act
            var producto = context.V_ProductoInventarioVenta.FirstOrDefault(p => p.codigo == codigoProductoCorrecto);

            // Assert
            Assert.NotNull(producto);
            Assert.True(producto.codigo == codigoProductoCorrecto, "No se encontró el producto.");

        }

        [Fact]
        public void ObtenerProductoInventarioVentaPorCodigoProducto_NoDeberiaRetornarProducto()
        {

            // Arrange
            using var context = GetContext();
            string codigoProductoIncorrecto = "0000000000000";

            // Act
            var producto = context.V_ProductoInventarioVenta.FirstOrDefault(p => p.codigo == codigoProductoIncorrecto);

            // Assert
            Assert.Null(producto);
        }

        [Fact]
        public void ObtenerDetallesVentaPorNoVenta_DeberiaRetornarDetalles()
        {

            // Arrange
            using var context = GetContext();
            int noVentaCorrecto = 1001;

            // Act
            var detalles = context.V_DetalleVenta.Where(d => d.noVenta == noVentaCorrecto).ToList();

            // Assert
            Assert.NotNull(detalles);
            Assert.True(detalles.Any(), "No se encontraron detalles de la venta.");

        }

        [Fact]
        public void ObtenerDetallesVentaPorNoVenta_NoDeberiaRetornarDetalles()
        {

            // Arrange
            using var context = GetContext();
            int noVentaIncorrecto = 0;

            // Act
            var detalles = context.V_DetalleVenta.Where(d => d.noVenta == noVentaIncorrecto).ToList();

            // Assert
            Assert.Empty(detalles);

        }

    }

}
