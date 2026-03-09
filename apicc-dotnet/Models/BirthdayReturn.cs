namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Birthday data for the day.
/// PT: Dados de aniversariante do dia.
/// ES: Datos de cumpleaños del día.
/// </summary>
public class BirthdayReturn
{
    public string? Name { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Email { get; set; }
}
