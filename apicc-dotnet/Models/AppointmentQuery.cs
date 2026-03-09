namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Query to list patient appointments.
/// PT: Query para listar agendamentos do paciente.
/// ES: Consulta para listar citas del paciente.
/// </summary>
public class AppointmentQuery
{
    public string? Phone { get; set; }
    public string? Document { get; set; }
}
