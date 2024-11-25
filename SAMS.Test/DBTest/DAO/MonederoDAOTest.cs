using SAMS.UI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Test.DBTest.DAO
{
    public class MonederoDAOTest : SAMSContextTest
    {

        [Fact]
        public void ObtenerMonederos_DeberiaRetornarMonederos()
        {

            // Arrange
            using var context = GetContext();

            // Act
            var monederos = context.V_Monederos.ToList();

            // Assert
            Assert.NotNull(monederos);
            Assert.True(monederos.Any(), "No se encontraron monederos.");

        }

        [Fact]
        public void ObtenerMonederoPorCodigoDeBarras_DeberiaRetornarMonedero()
        {

            // Arrange
            using var context = GetContext();
            string codigoDeBarrasCorrecto = "000000000003";

            // Act
            var monedero = context.V_Monedero.FirstOrDefault(m => m.codigoDeBarras == codigoDeBarrasCorrecto);

            // Assert
            Assert.NotNull(monedero);
            Assert.True(monedero.codigoDeBarras != null, "No se encontró el monedero.");

        }

        [Fact]
        public void ObtenerMonederoPorCodigoDeBarras_NoDeberiaRetornarMonedero()
        {

            // Arrange
            using var context = GetContext();
            string codigoDeBarrasIncorrecto = "123456789012";

            // Act
            var monedero = context.V_Monedero.FirstOrDefault(m => m.codigoDeBarras == codigoDeBarrasIncorrecto);

            // Assert
            Assert.Null(monedero);

        }

        [Fact]
        public void ObtenerMonederoPorCodigoDeBarras_DeberiaRetornarMonederoConSaldo()
        {

            // Arrange
            using var context = GetContext();
            string codigoDeBarrasCorrecto = "000000000003";

            // Act
            var monedero = context.V_Monedero.FirstOrDefault(m => m.codigoDeBarras == codigoDeBarrasCorrecto);

            // Assert
            Assert.NotNull(monedero);
            Assert.True(monedero.saldo > 0, "El monedero no tiene saldo.");

        }

        [Fact]
        public void CRUDMonedero_DeberiaHacerFlujoCompleto()
        {

            // Arrange
            using var context = GetContext();
            Monedero monedero = new Monedero
            {
                nombre = "Juan",
                apellidoPaterno = "Perez",
                apellidoMaterno = "Gonzalez",
                telefono = "1234567890",
                codigoDeBarras = "123456789012"
            };

            monedero.SetSaldo(0);

            // Act
            context.Monedero.Add(monedero);
            context.SaveChanges();

            // Assert
            Assert.NotNull(context.Monedero.FirstOrDefault(m => m.codigoDeBarras == monedero.codigoDeBarras));

            // Arrange
            Monedero cambioMonedero = context.Monedero.FirstOrDefault(m => m.codigoDeBarras == monedero.codigoDeBarras);
            cambioMonedero.nombre = "Pedro";
            cambioMonedero.apellidoPaterno = "Ramirez";
            cambioMonedero.apellidoMaterno = "Gomez";
            cambioMonedero.telefono = "0987654321";

            // Act
            context.SaveChanges();

            // Assert
            Assert.Equal("Pedro", context.Monedero.FirstOrDefault(m => m.codigoDeBarras == monedero.codigoDeBarras).nombre);

            // Arrange
            Monedero monederoAEliminar = context.Monedero.FirstOrDefault(m => m.codigoDeBarras == monedero.codigoDeBarras);

            // Act
            context.Monedero.Remove(monederoAEliminar);
            context.SaveChanges();

            // Assert
            Assert.Null(context.Monedero.FirstOrDefault(m => m.codigoDeBarras == monedero.codigoDeBarras));

        }

    }

}
