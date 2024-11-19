using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;

namespace SAMS.UI.DAO
{
    public class MonederoDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();

        public static List<MonederosDTO> ObtenerMonederos()
        {

            List<MonederosDTO> monederos = new List<MonederosDTO>();

            var monederosData = from m in _sams.V_Monederos
                                select new
                                {
                                    m.codigoDeBarras,
                                    m.telefono,
                                    m.saldo,
                                    m.nombrePropietario
                                };

            foreach (var monederoData in monederosData)
            {

                MonederosDTO monedero = new MonederosDTO
                {
                    codigoDeBarras = monederoData.codigoDeBarras,
                    telefono = monederoData.telefono,
                    saldo = monederoData.saldo,
                    nombrePropietario = monederoData.nombrePropietario
                };

                monederos.Add(monedero);

            }

            return monederos;

        }

        public static MonederoDTO ObtenerMonedero(string codigoDeBarras)
        {

            var monederoData = (from m in _sams.V_Monedero
                                where m.codigoDeBarras == codigoDeBarras
                                select new
                                {
                                    m.nombre,
                                    m.apellidoPaterno,
                                    m.apellidoMaterno,
                                    m.telefono,
                                    m.saldo,
                                    m.codigoDeBarras
                                }).FirstOrDefault();

            MonederoDTO monedero = new MonederoDTO
            {
                nombre = monederoData.nombre,
                apellidoPaterno = monederoData.apellidoPaterno,
                apellidoMaterno = monederoData.apellidoMaterno,
                telefono = monederoData.telefono,
                saldo = monederoData.saldo,
                codigoDeBarras = monederoData.codigoDeBarras,
            };

            return monedero;

        }

    }

}
