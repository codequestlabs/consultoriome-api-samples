namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Full appointment with messaging details.
/// PT: Agendamento completo com detalhes para mensagens.
/// ES: Cita completa con detalles para mensajería.
/// </summary>
public class Appointment
{
    public string? Id { get; set; }
    public DateTime? Date { get; set; }
    public string? Type { get; set; }
    public string? PatientName { get; set; }
    public string? Phone1 { get; set; }
    public string? PhoneNumber1 { get; set; }
    public string? Phone2 { get; set; }
    public string? PhoneNumber2 { get; set; }
    public string? Email { get; set; }
    public string? PatientExtraField1 { get; set; }
    public string? PatientExtraField2 { get; set; }
    public string? ClinicName { get; set; }
    public string? ClinicAddress { get; set; }
    public string? ClinicLocation { get; set; }
    public string? ProfessionalName { get; set; }
    public string? ProfessionalExtraInfo { get; set; }
    public string? ProfessionalCredentials { get; set; }
    public string? LinkCreateAppointment { get; set; }
    public string? LinkConfirmation { get; set; }
    public string? LinkMeeting { get; set; }
    public string? Message { get; set; }
    public int? CustomMessageId { get; set; }
    public IList<Form>? Forms { get; set; }
}
