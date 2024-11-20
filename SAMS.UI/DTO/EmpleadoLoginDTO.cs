namespace SAMS.UI.DTO
{
    public class EmpleadoLoginDTO
    {
        public string correo { get; set; }
        public string passwordHash { get; set; }
        public string numeroEmpleado { get; set; }
        public string nombreEmpleado { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string tipoEmpleado { get; set; }
    }
}
