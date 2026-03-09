namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Available time slot for booking.
/// PT: Horário disponível para agendamento.
/// ES: Horario disponible para reserva.
/// </summary>
public class TimeSlot
{
    public string? TimeSlotId { get; set; }
    public DateTime? DateTime { get; set; }
}
