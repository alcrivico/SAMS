namespace SAMS.UI.DTO
{
    public class EmpleadoLoginDTO
    {
        public string Correo { get; set; }
        public string PasswordHash { get; set; }
        public string NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string TipoEmpleado { get; set; }
    }
}
