using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ConsultorioMeApiExamples.Models;

namespace ConsultorioMeApiExamples;

/// <summary>
/// EN: Client for Consultorio.me API - implements all Swagger endpoints. Base URL: https://api.consultoriome.com
/// PT: Cliente para a API do Consultorio.me - implementa todos os endpoints do Swagger. URL base: https://api.consultoriome.com
/// ES: Cliente para la API de Consultorio.me - implementa todos los endpoints del Swagger. URL base: https://api.consultoriome.com
/// </summary>
public class ConsultorioMeApiClient
{
    private const string BaseUrl = "https://api.consultoriome.com";    
    
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly JsonSerializerOptions _requestJsonOptions;

    public ConsultorioMeApiClient(string? clientId = null, string? clientSecret = null)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
        {
            var credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);
        }

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null,
            WriteIndented = false
        };
        _requestJsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // EN: ASP.NET Core APIs typically expect camelCase | PT: APIs ASP.NET Core geralmente esperam camelCase | ES: APIs ASP.NET Core típicamente esperan camelCase
            WriteIndented = false
        };
    }

    /// <summary>
    /// EN: Sets the Bearer token for request authentication. Get token via GetTokenAsync().
    /// PT: Define o token Bearer para autenticação nas requisições. Obtenha o token via GetTokenAsync().
    /// ES: Establece el token Bearer para autenticación de solicitudes. Obtenga el token vía GetTokenAsync().
    /// </summary>
    public void SetBearerToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    // ========== AUTH ==========

    /// <summary>
    /// EN: Gets JWT token using Basic Auth (ClientId:ClientSecret). POST /v1/api/authorization/token
    /// PT: Obtém token JWT usando Basic Auth (ClientId:ClientSecret). POST /v1/api/authorization/token
    /// ES: Obtiene token JWT usando Basic Auth (ClientId:ClientSecret). POST /v1/api/authorization/token
    /// </summary>
    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync(
            "v1/api/authorization/token",
            null,
            cancellationToken);

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken) ?? string.Empty;
    }

    // ========== APPOINTMENT ==========

    /// <summary>
    /// EN: Lists available professionals. GET /v1/api/appointment/professionals
    /// PT: Lista profissionais disponíveis. GET /v1/api/appointment/professionals
    /// ES: Lista profesionales disponibles. GET /v1/api/appointment/professionals
    /// </summary>
    public async Task<IList<ProfessionalModel>> GetProfessionalsAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            "v1/api/appointment/professionals",
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<ProfessionalModel>>(content, _jsonOptions)
            ?? [];
    }

    /// <summary>
    /// EN: Gets detailed professional information. GET /v1/api/appointment/professional-info/{id}
    /// PT: Obtém informações detalhadas do profissional. GET /v1/api/appointment/professional-info/{id}
    /// ES: Obtiene información detallada del profesional. GET /v1/api/appointment/professional-info/{id}
    /// </summary>
    public async Task<ProInfo?> GetProfessionalInfoAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            $"v1/api/appointment/professional-info/{id}",
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ProInfo>(content, _jsonOptions);
    }

    /// <summary>
    /// EN: Gets available times for booking. GET /v1/api/appointment/available-times/{id}
    /// PT: Obtém horários disponíveis para agendamento. GET /v1/api/appointment/available-times/{id}
    /// ES: Obtiene horarios disponibles para reserva. GET /v1/api/appointment/available-times/{id}
    /// </summary>
    public async Task<TimeSlots?> GetAvailableTimesAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            $"v1/api/appointment/available-times/{id}",
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<TimeSlots>(content, _jsonOptions);
    }

    /// <summary>
    /// EN: Lists patient appointments by phone or document. POST /v1/api/appointment/patient-list
    /// PT: Lista agendamentos do paciente por telefone ou documento. POST /v1/api/appointment/patient-list
    /// ES: Lista citas del paciente por teléfono o documento. POST /v1/api/appointment/patient-list
    /// </summary>
    public async Task<IList<AppointmentItem>> GetPatientListAsync(
        AppointmentQuery query,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(query, _requestJsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(
            "v1/api/appointment/patient-list",
            content,
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<AppointmentItem>>(responseContent, _jsonOptions)
            ?? [];
    }

    /// <summary>
    /// EN: Creates a new appointment. POST /v1/api/appointment/create-appointment
    /// PT: Cria um novo agendamento. POST /v1/api/appointment/create-appointment
    /// ES: Crea una nueva cita. POST /v1/api/appointment/create-appointment
    /// </summary>
    public async Task<PreAppointmentReturn?> CreateAppointmentAsync(
        PreAppointment preAppointment,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(preAppointment, _requestJsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(
            "v1/api/appointment/create-appointment",
            content,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException(
                $"CreateAppointment failed ({(int)response.StatusCode}): {response.ReasonPhrase}. Body: {errorBody}");
        }

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<PreAppointmentReturn>(responseContent, _jsonOptions);
    }

    /// <summary>
    /// EN: Confirms or rejects an appointment. POST /v1/api/appointment/confirm/{id}/{going}
    /// PT: Confirma ou rejeita um agendamento. POST /v1/api/appointment/confirm/{id}/{going}
    /// ES: Confirma o rechaza una cita. POST /v1/api/appointment/confirm/{id}/{going}
    /// </summary>
    public async Task<AppointmentReturn?> ConfirmAppointmentAsync(
        string id,
        string going,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync(
            $"v1/api/appointment/confirm/{id}/{going}",
            null,
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<AppointmentReturn>(content, _jsonOptions);
    }

    /// <summary>
    /// EN: Cancels an appointment. POST /v1/api/appointment/cancel-appointment/{id}
    /// PT: Cancela um agendamento. POST /v1/api/appointment/cancel-appointment/{id}
    /// ES: Cancela una cita. POST /v1/api/appointment/cancel-appointment/{id}
    /// </summary>
    public async Task<AppointmentReturn?> CancelAppointmentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync(
            $"v1/api/appointment/cancel-appointment/{id}",
            null,
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<AppointmentReturn>(content, _jsonOptions);
    }

    // ========== MESSENGER ==========

    /// <summary>
    /// EN: Lists message templates (e.g. whatsapp). GET /v1/api/messenger/templates/{type}
    /// PT: Lista templates de mensagem (ex: whatsapp). GET /v1/api/messenger/templates/{type}
    /// ES: Lista plantillas de mensaje (ej: whatsapp). GET /v1/api/messenger/templates/{type}
    /// </summary>
    public async Task<IList<Item>> GetTemplatesAsync(
        string type = "whatsapp",
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            $"v1/api/messenger/templates/{type}",
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<Item>>(content, _jsonOptions)
            ?? [];
    }

    /// <summary>
    /// EN: Gets birthdays for a date. GET /v1/api/messenger/birthdays/{date}
    /// PT: Obtém aniversariantes de uma data. GET /v1/api/messenger/birthdays/{date}
    /// ES: Obtiene cumpleaños de una fecha. GET /v1/api/messenger/birthdays/{date}
    /// </summary>
    public async Task<IList<BirthdayReturn>> GetBirthdaysAsync(
        string date,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            $"v1/api/messenger/birthdays/{date}",
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<BirthdayReturn>>(content, _jsonOptions)
            ?? [];
    }

    /// <summary>
    /// EN: Gets appointments for messaging on a date. GET /v1/api/messenger/appointments/{date}/{messageId}
    /// PT: Obtém agendamentos para mensagens em uma data. GET /v1/api/messenger/appointments/{date}/{messageId}
    /// ES: Obtiene citas para mensajería en una fecha. GET /v1/api/messenger/appointments/{date}/{messageId}
    /// </summary>
    public async Task<IList<Appointment>> GetAppointmentsForMessengerAsync(
        string date,
        int messageId = 0,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            $"v1/api/messenger/appointments/{date}/{messageId}",
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<Appointment>>(content, _jsonOptions)
            ?? [];
    }
}
