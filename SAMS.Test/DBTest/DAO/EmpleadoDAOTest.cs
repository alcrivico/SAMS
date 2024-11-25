using Microsoft.EntityFrameworkCore;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SAMS.Test.DBTest.DAO
{
    public class EmpleadoDAOTest : SAMSContextTest
    {
        [Fact]
        public async Task LogInEmpleado_DeberiaRetornarEmpleado_CuandoCredencialesSonCorrectas()
        {
            using var context = GetContext();
            // Arrange
            string correo = "miguel@gmail.com";
            string password = "0x46BB6B6C29EB6E5E2D42596F56C0786F60B0FDADA37D5F80C7C2EC1F9BC23C2F";

            // Act
            var resultado = await Task.Run(() =>
            {
                // Consultar la vista V_EmpleadoLogin desde el contexto
                var empleadoData = (from e in context.V_EmpleadoLogin
                                    where e.correo == correo
                                    select new
                                    {
                                        e.correo,
                                        e.passwordHash,
                                        e.numeroEmpleado,
                                        e.nombreEmpleado,
                                        e.apellidoPaterno,
                                        e.apellidoMaterno,
                                        e.tipoEmpleado
                                    }).FirstOrDefault();

                // Si no encuentra un empleado con el correo, retorna null
                if (empleadoData == null)
                {
                    return null;
                }

                // Comparar la contraseña hasheada con la almacenada en la base de datos
                if (empleadoData.passwordHash != password)
                {
                    return null;
                }

                // Si las contraseñas coinciden, retornar los datos del empleado
                return new EmpleadoLoginDTO
                {
                    correo = empleadoData.correo,
                    passwordHash = empleadoData.passwordHash,
                    numeroEmpleado = empleadoData.numeroEmpleado,
                    nombreEmpleado = empleadoData.nombreEmpleado,
                    apellidoPaterno = empleadoData.apellidoPaterno,
                    apellidoMaterno = empleadoData.apellidoMaterno,
                    tipoEmpleado = empleadoData.tipoEmpleado
                };
            });

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(correo, resultado.correo);
            Assert.Equal(password, resultado.passwordHash);
        }

        [Fact]
        public async Task LogInEmpleado_DeberiaRetornarNull_CuandoCredencialesSonIncorrectas()
        {
            using var context = GetContext();
            // Arrange
            string correo = "miguel@gmail.com";
            string password = "passwordIncorrecto";

            // Act
            var resultado = await Task.Run(() =>
            {
                // Obtener la contraseña hasheada usando la entidad Empleado
                Empleado tempEmpleado = new Empleado { Password = password };
                string contraseñaHasheada = tempEmpleado.Password;

                // Consultar la vista V_EmpleadoLogin desde el contexto
                var empleadoData = (from e in context.V_EmpleadoLogin
                                    where e.correo == correo
                                    select new
                                    {
                                        e.correo,
                                        e.passwordHash,
                                        e.numeroEmpleado,
                                        e.nombreEmpleado,
                                        e.apellidoPaterno,
                                        e.apellidoMaterno,
                                        e.tipoEmpleado
                                    }).FirstOrDefault();

                // Si no encuentra un empleado con el correo, retorna null
                if (empleadoData == null)
                {
                    return null;
                }

                // Comparar la contraseña hasheada con la almacenada en la base de datos
                if (empleadoData.passwordHash != contraseñaHasheada)
                {
                    return null;
                }

                // Si las contraseñas coinciden, retornar los datos del empleado
                return new EmpleadoLoginDTO
                {
                    correo = empleadoData.correo,
                    passwordHash = empleadoData.passwordHash,
                    numeroEmpleado = empleadoData.numeroEmpleado,
                    nombreEmpleado = empleadoData.nombreEmpleado,
                    apellidoPaterno = empleadoData.apellidoPaterno,
                    apellidoMaterno = empleadoData.apellidoMaterno,
                    tipoEmpleado = empleadoData.tipoEmpleado
                };
            });

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task LogInEmpleado_DeberiaRetornarNull_CuandoEmpleadoNoExiste()
        {
            using var context = GetContext();
            // Arrange
            string correo = "empleado@inexistente.com";
            string password = "password";

            // Act
            var resultado = await Task.Run(() =>
            {
                // Obtener la contraseña hasheada usando la entidad Empleado
                Empleado tempEmpleado = new Empleado { Password = password };
                string contraseñaHasheada = tempEmpleado.Password;

                // Consultar la vista V_EmpleadoLogin desde el contexto
                var empleadoData = (from e in context.V_EmpleadoLogin
                                    where e.correo == correo
                                    select new
                                    {
                                        e.correo,
                                        e.passwordHash,
                                        e.numeroEmpleado,
                                        e.nombreEmpleado,
                                        e.apellidoPaterno,
                                        e.apellidoMaterno,
                                        e.tipoEmpleado
                                    }).FirstOrDefault();

                // Si no encuentra un empleado con el correo, retorna null
                if (empleadoData == null)
                {
                    return null;
                }

                // Comparar la contraseña hasheada con la almacenada en la base de datos
                if (empleadoData.passwordHash != contraseñaHasheada)
                {
                    return null;
                }

                // Si las contraseñas coinciden, retornar los datos del empleado
                return new EmpleadoLoginDTO
                {
                    correo = empleadoData.correo,
                    passwordHash = empleadoData.passwordHash,
                    numeroEmpleado = empleadoData.numeroEmpleado,
                    nombreEmpleado = empleadoData.nombreEmpleado,
                    apellidoPaterno = empleadoData.apellidoPaterno,
                    apellidoMaterno = empleadoData.apellidoMaterno,
                    tipoEmpleado = empleadoData.tipoEmpleado
                };
            });

            // Assert
            Assert.Null(resultado);
        }
    }
}