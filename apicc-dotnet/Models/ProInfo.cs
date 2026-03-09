namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Complete professional information.
/// PT: Informações completas do profissional.
/// ES: Información completa del profesional.
/// </summary>
public class ProInfo
{
    public string? ProId { get; set; }
    public string? Id { get; set; }
    public ProfessionalDetails? Professional { get; set; }
    public string? WebLink { get; set; }
    public IList<OpeningHoursItem>? OpeningHours { get; set; }
    public string? Language { get; set; }
    public string? Country { get; set; }
    public string? TimeZone { get; set; }
    public Directions? Address { get; set; }
    public Contacts? Contact { get; set; }
    public IList<string>? Services { get; set; }
    public IList<string>? AppointmentTypes { get; set; }
    public string? PaymentInstructions { get; set; }
    public string? HealthInsuranceInfo { get; set; }
    public string? About { get; set; }
    public string? CompletedMessage { get; set; }
}
