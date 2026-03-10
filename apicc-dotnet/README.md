# Consultorio.me API - Examples

**EN:** Console project demonstrating the use of all [Consultorio.me API](https://api.consultoriome.com/swagger/v1/swagger.json) endpoints.  
**PT:** Projeto de console que demonstra o uso de todos os endpoints da [API Consultorio.me](https://api.consultoriome.com/swagger/v1/swagger.json).  
**ES:** Proyecto de consola que demuestra el uso de todos los endpoints de la [API Consultorio.me](https://api.consultoriome.com/swagger/v1/swagger.json).

---

## Configuration | Configuração | Configuración


**EN:** Or in the client constructor:  
**PT:** Ou no construtor do cliente:  
**ES:** O en el constructor del cliente:

```csharp
var client = new ConsultorioMeApiClient("your_client_id", "your_client_secret");
```

---

## Run | Executar | Ejecutar

```bash
dotnet run
```

---

## Implemented endpoints | Endpoints implementados | Endpoints implementados

### Auth
| Method | Endpoint | EN | PT | ES |
|--------|----------|----|----|-----|
| POST | `/v1/api/authorization/token` | Gets JWT token (Basic Auth) | Obtém token JWT (Basic Auth) | Obtiene token JWT (Basic Auth) |

### Appointment
| Method | Endpoint | EN | PT | ES |
|--------|----------|----|----|-----|
| GET | `/v1/api/appointment/professionals` | List professionals | Lista profissionais | Lista profesionales |
| GET | `/v1/api/appointment/professional-info/{id}` | Professional info | Info do profissional | Info del profesional |
| GET | `/v1/api/appointment/available-times/{id}` | Available times | Horários disponíveis | Horarios disponibles |
| POST | `/v1/api/appointment/patient-list` | List patient appointments | Lista agendamentos do paciente | Lista citas del paciente |
| POST | `/v1/api/appointment/create-appointment` | Create appointment | Cria agendamento | Crea cita |
| POST | `/v1/api/appointment/confirm/{id}/{going}` | Confirm/decline appointment | Confirma/recusa agendamento | Confirma/rechaza cita |
| POST | `/v1/api/appointment/cancel-appointment/{id}` | Cancel appointment | Cancela agendamento | Cancela cita |

### Messenger
| Method | Endpoint | EN | PT | ES |
|--------|----------|----|----|-----|
| GET | `/v1/api/messenger/templates/{type}` | Templates (e.g. whatsapp) | Templates (ex: whatsapp) | Plantillas (ej: whatsapp) |
| GET | `/v1/api/messenger/birthdays/{date}` | Birthdays of the day | Aniversariantes do dia | Cumpleaños del día |
| GET | `/v1/api/messenger/appointments/{date}/{messageId}` | Appointments for messaging | Agendamentos para mensagens | Citas para mensajería |

---

## Project structure | Estrutura do projeto | Estructura del proyecto

```
├── Models/                    # DTOs based on Swagger | DTOs baseados no Swagger | DTOs basados en Swagger
├── ConsultorioMeApiClient.cs  # API HTTP client | Cliente HTTP da API | Cliente HTTP de la API
├── Program.cs                # Usage examples | Exemplos de uso | Ejemplos de uso
└── README.md
```

---

## Dependencies | Dependências | Dependencias

**EN:** Uses only .NET standard libraries:  
**PT:** Utiliza apenas bibliotecas padrão do .NET:  
**ES:** Utiliza solo bibliotecas estándar de .NET:

- `System.Net.Http` (HttpClient)
- `System.Text.Json` (JSON serialization | serialização JSON | serialización JSON)
