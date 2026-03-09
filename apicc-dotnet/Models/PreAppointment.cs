namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Data for creating a new appointment.
/// PT: Dados para criação de um novo agendamento.
/// ES: Datos para crear una nueva cita.
/// </summary>
public class PreAppointment
{
    public string? ProId { get; set; }
    public DateTime DateTime { get; set; }
    public double TimeSlotId { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Name { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Email { get; set; }
    public string? Document { get; set; }
    public string? ResponsibleName { get; set; }
    public string? AppointmentType { get; set; }
    public string? Observation { get; set; }
    public string? ValidationMessage { get; set; }
}
