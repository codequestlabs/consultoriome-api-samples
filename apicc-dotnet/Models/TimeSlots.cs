namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: List of available time slots.
/// PT: Lista de horários disponíveis.
/// ES: Lista de horarios disponibles.
/// </summary>
public class TimeSlots
{
    public IList<TimeSlot>? Slots { get; set; }
    public string? Language { get; set; }
    public string? Country { get; set; }
    public string? Id { get; set; }
    public string? ProId { get; set; }
    public string? TimeZone { get; set; }
}
