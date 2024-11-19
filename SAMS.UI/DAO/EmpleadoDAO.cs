using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                where e.Correo == correo
                                select new
                                {
                                    e.Correo,
                                    e.PasswordHash,
                                    e.NumeroEmpleado,
                                    e.NombreEmpleado,
                                    e.ApellidoPaterno,
                                    e.ApellidoMaterno,
                                    e.TipoEmpleado
                                }).FirstOrDefault();

            // Si no encuentra un empleado con el correo, retorna null
            if (empleadoData == null)
            {
                return null;
            }

            // Comparar la contraseña hasheada con la almacenada en la base de datos
            if (empleadoData.PasswordHash != contraseñaHasheada)
            {
                InformationControl.Show("Autenticación Fallida", "La contraseña" + contraseñaHasheada, "Aceptar");
                return null;
            }

            // Si las contraseñas coinciden, retornar los datos del empleado
            return new EmpleadoLoginDTO
            {
                Correo = empleadoData.Correo,
                PasswordHash = empleadoData.PasswordHash,
                NumeroEmpleado = empleadoData.NumeroEmpleado,
                NombreEmpleado = empleadoData.NombreEmpleado,
                ApellidoPaterno = empleadoData.ApellidoPaterno,
                ApellidoMaterno = empleadoData.ApellidoMaterno,
                TipoEmpleado = empleadoData.TipoEmpleado
            };
        }

    }
}
