namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Appointment item in the patient list.
/// PT: Item de agendamento na lista do paciente.
/// ES: Ítem de cita en la lista del paciente.
/// </summary>
public class AppointmentItem
{
    public string? Id { get; set; }
    public DateTime? DateTime { get; set; }
    public string? Status { get; set; }
    public string? StatusId { get; set; }
}
