using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;

namespace SAMS.UI.DAO
{
    class EmpleadoDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

        public static EmpleadoLoginDTO LogInEmpleado(string correo, string password)
        {
            // Obtener la contraseña hasheada usando la entidad Empleado
            Empleado tempEmpleado = new Empleado { Password = password };
            string contraseñaHasheada = tempEmpleado.Password;

            // Consultar la vista V_EmpleadoLogin desde _sams
            var empleadoData = (from e in _sams.V_EmpleadoLogin
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
        }

    }
}
