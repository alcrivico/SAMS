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
        public void RegistrarProveedorYProductos_DeberiaRegistrarProveedorYProductos()
        {
            // Arrange
            var proveedor = new Proveedor
            {
                rfc = "PROV840214HNL",
                nombre = "Proveedor Test",
                correo = "test@proveedor.com",
                telefono = "5551234567"
            };

            string[] productos = new[] { "001,Vino tinto de la Rioja,No,Sí,Vino Tinto Rioja,Litro",
                "002,Vino blanco de la Rioja,No,Sí,Vino Blanco Rioja,Litro",
                "003,Vino rosado de la Rioja,No,Sí,Vino Rosado Rioja,Litro",
                "004,Vino tinto de Ribera del Duero,No,Sí,Vino Tinto Ribera,Litro"};

            // Act & Assert
            ProveedorDAO.RegistrarProveedorYProductos(proveedor, productos);
        }

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

        [Fact]
        public void RegistrarProveedorYProductos_DeberiaLanzarExcepcion_CuandoRFCDuplicado()
        {
            //Arrange
            var proveedor = new Proveedor
            {
                rfc = "PROV840214HNL",
                nombre = "Proveedor Test2",
                correo = "test2@proveedor.com",
                telefono = "5551234567"
            };

            string[] productos = new[] { "005,Vino blanco de Ribera del Duero,No,Sí,Vino Blanco Ribera,Litro",
                "006,Vino rosado de Ribera del Duero,No,Sí,Vino Rosado Ribera,Litro",
                "007,Whisky escocés,No,Sí,Whisky Escocés,Litro",
                "008,Whisky irlandés,No,Sí,Whisky Irlandés,Litro"};

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                ProveedorDAO.RegistrarProveedorYProductos(proveedor, productos);
            });
        }

        [Fact]
        public void RegistrarProveedorYProductos_DeberiaLanzarExcepcion_CuandoProveedorConCamposFaltantes()
        {
            // Arrange
            var proveedor = new Proveedor
            {
                rfc = "PRRV840214HN2",
                nombre = "Proveedor Test",
                correo = "test3@proveedor.com",
                telefono = "5551234567"
            };

            string[] productos = new[] { "005,Vino blanco de Ribera del Duero,No,Sí,Vino Blanco Ribera,Litro",
                "006,Vino rosado de Ribera del Duero,No,Sí,Vino Rosado Ribera,Litro",
                "007,Whisky escocés,No,Sí,Whisky Escocés,Litro",
                "008,Whisky irlandés,No,Sí,Whisky Irlandés,Litro"};

            Assert.Throws<ArgumentException>(() =>
            {
                {
                    ProveedorDAO.RegistrarProveedorYProductos(proveedor, productos);
                }
            });
        }
    }
}
