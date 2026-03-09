using System.Text.Json.Serialization;

namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Return of appointment operations (cancel, confirm).
/// PT: Retorno de operações de agendamento (cancelar, confirmar).
/// ES: Retorno de operaciones de cita (cancelar, confirmar).
/// </summary>
public class AppointmentReturn
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("going")]
    public string? Going { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
