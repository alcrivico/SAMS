using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;

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

        public static MonederoDTO ActualizarMonedero(MonederoDTO monedero)
        {

            var monederoData = _sams.Monedero.Where(m => m.codigoDeBarras == monedero.codigoDeBarras).FirstOrDefault();

            monederoData.nombre = monedero.nombre;
            monederoData.apellidoPaterno = monedero.apellidoPaterno;
            monederoData.apellidoMaterno = monedero.apellidoMaterno;
            monederoData.telefono = monedero.telefono;

            _sams.SaveChanges();

            return monedero;

        }

        public static MonederoDTO RegistrarMonedero(MonederoDTO monedero)
        {

            Monedero monederoData = new Monedero
            {
                nombre = monedero.nombre,
                apellidoPaterno = monedero.apellidoPaterno,
                apellidoMaterno = monedero.apellidoMaterno,
                telefono = monedero.telefono,
                codigoDeBarras = monedero.codigoDeBarras
            };

            monederoData.SetSaldo(0);

            _sams.Monedero.Add(monederoData);
            _sams.SaveChanges();

            return monedero;

        }

        public static void GenerarCodigoDeBarras(MonederoDTO monedero)
        {

            Random random = new Random();

            string codigoDeBarras = "";

            do
            {
                codigoDeBarras = "";
                for (int i = 0; i < 12; i++)
                {
                    codigoDeBarras += random.Next(0, 9).ToString();
                }
            } while (_sams.Monedero.Any(m => m.codigoDeBarras == codigoDeBarras));

            monedero.codigoDeBarras = codigoDeBarras;

        }

        public static bool ExisteMonedero(MonederoDTO monedero)
        {
            return (_sams.Monedero.Any(m => m.telefono == monedero.telefono)) ? true : false;
        }

    }

}
