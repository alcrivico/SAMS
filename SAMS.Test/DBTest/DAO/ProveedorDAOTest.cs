using SAMS.UI.DAO;
using SAMS.UI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Test.DBTest.DAO
{
    public class ProveedorDAOTest
    {
        [Fact]
        public void RegistrarProveedorYProductos_DeberiaLanzarExcepcion_CuandoFormatoDeProductoEsIncorrecto()
        {
            // Arrange
            var proveedor = new Proveedor
            {
                rfc = "PROV840214HNL",
                nombre = "Proveedor Test",
                correo = "test@proveedor.com",
                telefono = "5551234567"
            };

            string[] productos = new[] { "Codigo,Descripcion" }; // Formato incorrecto

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                ProveedorDAO.RegistrarProveedorYProductos(proveedor, productos);
            });
        }

        [Fact]
        public void RegistrarProveedorYProductos_DeberiaLanzarExcepcion_CuandoListaProductosVacia()
        {
            // Arrange
            var proveedor = new Proveedor
            {
                rfc = "PROV840214HNL",
                nombre = "Proveedor Test",
                correo = "test@proveedor.com",
                telefono = "5551234567"
            };

            string[] productos = new[] { "" }; // Lista vacía

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                ProveedorDAO.RegistrarProveedorYProductos(proveedor, productos);
            });
        }
    }
}
