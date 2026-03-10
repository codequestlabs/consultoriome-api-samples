/**
 * EN: Client for Consultorio.me API - implements all Swagger endpoints. Base URL: https://api.consultoriome.com
 * PT: Cliente para a API do Consultorio.me - implementa todos os endpoints do Swagger. URL base: https://api.consultoriome.com
 * ES: Cliente para la API de Consultorio.me - implementa todos los endpoints del Swagger. URL base: https://api.consultoriome.com
 */

const BASE_URL = 'https://api.consultoriome.com';

export class ConsultorioMeApiClient {
  /**
   * EN: Creates the API client. Pass clientId and clientSecret for Basic Auth (token endpoint).
   * PT: Cria o cliente da API. Passe clientId e clientSecret para Basic Auth (endpoint de token).
   * ES: Crea el cliente de la API. Pase clientId y clientSecret para Basic Auth (endpoint de token).
   */
  constructor(clientId = '', clientSecret = '') {
    this.baseUrl = BASE_URL;
    this.bearerToken = null;
    this.authHeader =
      clientId && clientSecret
        ? 'Basic ' + Buffer.from(`${clientId}:${clientSecret}`).toString('base64')
        : null;
  }

  /**
   * EN: Sets the Bearer token for request authentication. Get token via getTokenAsync().
   * PT: Define o token Bearer para autenticação nas requisições. Obtenha o token via getTokenAsync().
   * ES: Establece el token Bearer para autenticación de solicitudes. Obtenga el token vía getTokenAsync().
   */
  setBearerToken(token) {
    this.bearerToken = token;
  }

  async #request(method, path, body = null) {
    const url = `${this.baseUrl}/${path.replace(/^\//, '')}`;
    const headers = {
      Accept: 'application/json',
      ...(this.bearerToken && { Authorization: `Bearer ${this.bearerToken}` }),
      ...(!this.bearerToken && this.authHeader && { Authorization: this.authHeader }),
      ...(body && { 'Content-Type': 'application/json' }),
    };
    const options = { method, headers };
    if (body) options.body = JSON.stringify(body);

    const response = await fetch(url, options);
    const text = await response.text();

    if (!response.ok) {
      throw new Error(`${method} ${path} failed (${response.status}): ${response.statusText}. Body: ${text}`);
    }

    if (!text) return null;
    try {
      return JSON.parse(text);
    } catch {
      // Token endpoint may return plain JWT string instead of JSON
      return text;
    }
  }

  // ========== AUTH ==========

  /**
   * EN: Gets JWT token using Basic Auth (ClientId:ClientSecret). POST /v1/api/authorization/token
   * PT: Obtém token JWT usando Basic Auth (ClientId:ClientSecret). POST /v1/api/authorization/token
   * ES: Obtiene token JWT usando Basic Auth (ClientId:ClientSecret). POST /v1/api/authorization/token
   */
  async getTokenAsync() {
    const result = await this.#request('POST', 'v1/api/authorization/token');
    return typeof result === 'string' ? result : JSON.stringify(result);
  }

  // ========== APPOINTMENT ==========

  /**
   * EN: Lists available professionals. GET /v1/api/appointment/professionals
   * PT: Lista profissionais disponíveis. GET /v1/api/appointment/professionals
   * ES: Lista profesionales disponibles. GET /v1/api/appointment/professionals
   */
  async getProfessionalsAsync() {
    const result = await this.#request('GET', 'v1/api/appointment/professionals');
    return Array.isArray(result) ? result : [];
  }

  /**
   * EN: Gets detailed professional information. GET /v1/api/appointment/professional-info/{id}
   * PT: Obtém informações detalhadas do profissional. GET /v1/api/appointment/professional-info/{id}
   * ES: Obtiene información detallada del profesional. GET /v1/api/appointment/professional-info/{id}
   */
  async getProfessionalInfoAsync(id) {
    return this.#request('GET', `v1/api/appointment/professional-info/${id}`);
  }

  /**
   * EN: Gets available times for booking. GET /v1/api/appointment/available-times/{id}
   * PT: Obtém horários disponíveis para agendamento. GET /v1/api/appointment/available-times/{id}
   * ES: Obtiene horarios disponibles para reserva. GET /v1/api/appointment/available-times/{id}
   */
  async getAvailableTimesAsync(id) {
    return this.#request('GET', `v1/api/appointment/available-times/${id}`);
  }

  /**
   * EN: Lists patient appointments by phone or document. POST /v1/api/appointment/patient-list
   * PT: Lista agendamentos do paciente por telefone ou documento. POST /v1/api/appointment/patient-list
   * ES: Lista citas del paciente por teléfono o documento. POST /v1/api/appointment/patient-list
   */
  async getPatientListAsync(query) {
    const result = await this.#request('POST', 'v1/api/appointment/patient-list', query);
    return Array.isArray(result) ? result : [];
  }

  /**
   * EN: Creates a new appointment. POST /v1/api/appointment/create-appointment
   * PT: Cria um novo agendamento. POST /v1/api/appointment/create-appointment
   * ES: Crea una nueva cita. POST /v1/api/appointment/create-appointment
   */
  async createAppointmentAsync(preAppointment) {
    return this.#request('POST', 'v1/api/appointment/create-appointment', preAppointment);
  }

  /**
   * EN: Confirms or rejects an appointment. POST /v1/api/appointment/confirm/{id}/{going}
   * PT: Confirma ou rejeita um agendamento. POST /v1/api/appointment/confirm/{id}/{going}
   * ES: Confirma o rechaza una cita. POST /v1/api/appointment/confirm/{id}/{going}
   */
  async confirmAppointmentAsync(id, going) {
    return this.#request('POST', `v1/api/appointment/confirm/${id}/${going}`);
  }

  /**
   * EN: Cancels an appointment. POST /v1/api/appointment/cancel-appointment/{id}
   * PT: Cancela um agendamento. POST /v1/api/appointment/cancel-appointment/{id}
   * ES: Cancela una cita. POST /v1/api/appointment/cancel-appointment/{id}
   */
  async cancelAppointmentAsync(id) {
    return this.#request('POST', `v1/api/appointment/cancel-appointment/${id}`);
  }

  // ========== MESSENGER ==========

  /**
   * EN: Lists message templates (e.g. whatsapp). GET /v1/api/messenger/templates/{type}
   * PT: Lista templates de mensagem (ex: whatsapp). GET /v1/api/messenger/templates/{type}
   * ES: Lista plantillas de mensaje (ej: whatsapp). GET /v1/api/messenger/templates/{type}
   */
  async getTemplatesAsync(type = 'whatsapp') {
    const result = await this.#request('GET', `v1/api/messenger/templates/${type}`);
    return Array.isArray(result) ? result : [];
  }

  /**
   * EN: Gets birthdays for a date. GET /v1/api/messenger/birthdays/{date}
   * PT: Obtém aniversariantes de uma data. GET /v1/api/messenger/birthdays/{date}
   * ES: Obtiene cumpleaños de una fecha. GET /v1/api/messenger/birthdays/{date}
   */
  async getBirthdaysAsync(date) {
    const result = await this.#request('GET', `v1/api/messenger/birthdays/${date}`);
    return Array.isArray(result) ? result : [];
  }

  /**
   * EN: Gets appointments for messaging on a date. GET /v1/api/messenger/appointments/{date}/{messageId}
   * PT: Obtém agendamentos para mensagens em uma data. GET /v1/api/messenger/appointments/{date}/{messageId}
   * ES: Obtiene citas para mensajería en una fecha. GET /v1/api/messenger/appointments/{date}/{messageId}
   */
  async getAppointmentsForMessengerAsync(date, messageId = 0) {
    const result = await this.#request('GET', `v1/api/messenger/appointments/${date}/${messageId}`);
    return Array.isArray(result) ? result : [];
  }
}
