# Consultorio.me API - Exemplos em .NET

Projeto de console que demonstra o uso de todos os endpoints da [API Consultorio.me](https://api.consultoriome.com/swagger/v1/swagger.json).

## Pré-requisitos

- .NET 8.0 ou superior

## Configuração

Configure as credenciais da API via variáveis de ambiente:

```powershell
$env:CONSULTORIO_CLIENT_ID = "seu_client_id"
$env:CONSULTORIO_CLIENT_SECRET = "seu_client_secret"
```

Ou no construtor do cliente:

```csharp
var client = new ConsultorioMeApiClient("seu_client_id", "seu_client_secret");
```

## Executar

```bash
dotnet run
```

## Endpoints implementados

### Auth
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/v1/api/authorization/token` | Obtém token JWT (Basic Auth) |

### Appointment
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/v1/api/appointment/professionals` | Lista profissionais |
| GET | `/v1/api/appointment/professional-info/{id}` | Info do profissional |
| GET | `/v1/api/appointment/available-times/{id}` | Horários disponíveis |
| POST | `/v1/api/appointment/patient-list` | Lista agendamentos do paciente |
| POST | `/v1/api/appointment/create-appointment` | Cria agendamento |
| POST | `/v1/api/appointment/confirm/{id}/{going}` | Confirma/recusa agendamento |
| POST | `/v1/api/appointment/cancel-appointment/{id}` | Cancela agendamento |

### Messenger
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/v1/api/messenger/templates/{type}` | Templates (ex: whatsapp) |
| GET | `/v1/api/messenger/birthdays/{date}` | Aniversariantes do dia |
| GET | `/v1/api/messenger/appointments/{date}/{messageId}` | Agendamentos para mensagens |

## Estrutura do projeto

```
├── Models/                 # DTOs baseados no Swagger
├── ConsultorioMeApiClient.cs  # Cliente HTTP da API
├── Program.cs             # Exemplos de uso
└── README.md
```

## Dependências

Utiliza apenas bibliotecas padrão do .NET:
- `System.Net.Http` (HttpClient)
- `System.Text.Json` (serialização JSON)
