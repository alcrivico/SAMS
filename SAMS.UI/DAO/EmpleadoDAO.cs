using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System.Diagnostics;

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

        public static IEnumerable<V_Empleados> ObtenerEmpleados() => _sams.V_Empleados.ToList();

        public static V_EmpleadoDetalle ObtenerEmpleadoPorRfc(string rfc) => _sams.V_EmpleadoDetalle.FirstOrDefault(e => e.rfc == rfc);

        public static List<String> ObtenerPuestos()
        {
            List<String> puestos = new List<String>();
            foreach (Puesto puesto in _sams.Puesto)
            {
                puestos.Add(puesto.nombre);
            }
            return puestos;
        }

        public static void RegistrarEmpleado(V_EmpleadoDetalle empleado)
        {
            var parametros = new[]
            {
                new SqlParameter("RFC", empleado.rfc),
                new SqlParameter("Nombre", empleado.nombre),
                new SqlParameter("ApellidoP", empleado.apellidoPaterno),
                new SqlParameter("ApellidoM", empleado.apellidoMaterno),
                new SqlParameter("Correo", empleado.correo),
                new SqlParameter("Telefono", empleado.telefono),
                new SqlParameter("Puesto", empleado.puesto)
            };

            _sams.Database.ExecuteSqlRaw("EXEC T_RegistrarEmpleado @RFC, @Nombre, @ApellidoP, @ApellidoM, @Correo, @Telefono, @Puesto", parametros);
        }

        public static void EliminarEmpleado(String rfc)
        {
            Empleado empleadoConId = _sams.Empleado.FirstOrDefault(e => e.rfc == rfc);
            if (empleadoConId != null)
            {
                empleadoConId.estado = false;
                _sams.SaveChanges();
            }
            else
            {
                throw new ArgumentException("El empleado no existe.");
            }
        }

        public static void EditarEmpleado(V_EmpleadoDetalle empleado)
        {
            int puestoId = ObtenerPuestoPorNombre(empleado.puesto).id;

            Empleado empleadoConId = _sams.Empleado.FirstOrDefault(e => e.rfc == empleado.rfc);
            if(empleadoConId != null)
            {
                empleadoConId.nombre = empleado.nombre;
                empleadoConId.apellidoPaterno = empleado.apellidoPaterno;
                empleadoConId.apellidoMaterno = empleado.apellidoMaterno;
                empleadoConId.correo = empleado.correo;
                empleadoConId.telefono = empleado.telefono;
                empleadoConId.puestoId = puestoId;
                _sams.SaveChanges();
            }
            else
            {
                throw new ArgumentException("El empleado no existe.");
            }
        }

        public static Puesto ObtenerPuestoPorNombre(string nombre) => _sams.Puesto.FirstOrDefault(p => p.nombre == nombre);
    }
}
