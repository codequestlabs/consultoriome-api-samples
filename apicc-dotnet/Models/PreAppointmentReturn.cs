namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Return of appointment creation.
/// PT: Retorno da criação de agendamento.
/// ES: Retorno de la creación de cita.
/// </summary>
public class PreAppointmentReturn
{
    public bool Result { get; set; }
    public string? Id { get; set; }
    public string? Message { get; set; }
}
