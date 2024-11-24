namespace SAMS.UI.DTO;

public class RangoFechasDTO
{
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }

    public string Rango
    {
        get
        {
            return $"{FechaInicio.ToString("dd/MM/yyyy")} - {FechaFin.ToString("dd/MM/yyyy")}";
        }
        set { }
    }
}
