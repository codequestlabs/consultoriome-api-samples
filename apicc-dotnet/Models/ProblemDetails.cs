namespace ConsultorioMeApiExamples.Models;

/// <summary>
/// EN: Error details returned by the API (RFC 7807 standard).
/// PT: Detalhes de erro retornados pela API (padrão RFC 7807).
/// ES: Detalles de error retornados por la API (estándar RFC 7807).
/// </summary>
public class ProblemDetails
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public string? Detail { get; set; }
    public string? Instance { get; set; }
}
