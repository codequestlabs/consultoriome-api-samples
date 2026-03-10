# Consultorio.me API - Node.js Examples

<!-- EN / PT / ES -->

## EN — English

Console project demonstrating usage of all [Consultorio.me API](https://api.consultoriome.com/swagger/v1/swagger.json) endpoints.

### Prerequisites

- Node.js 18.0 or higher

### Configuration

Configure API credentials via environment variables:

```powershell
$env:CONSULTORIO_CLIENT_ID = "your_client_id"
$env:CONSULTORIO_CLIENT_SECRET = "your_client_secret"
```

Or in the client constructor:

```javascript
const client = new ConsultorioMeApiClient("your_client_id", "your_client_secret");
```

### Run

```bash
node index.js
```

### Implemented endpoints

#### Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/v1/api/authorization/token` | Get JWT token (Basic Auth) |

#### Appointment
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/v1/api/appointment/professionals` | List professionals |
| GET | `/v1/api/appointment/professional-info/{id}` | Professional info |
| GET | `/v1/api/appointment/available-times/{id}` | Available time slots |
| POST | `/v1/api/appointment/patient-list` | Patient appointment list |
| POST | `/v1/api/appointment/create-appointment` | Create appointment |
| POST | `/v1/api/appointment/confirm/{id}/{going}` | Confirm/decline appointment |
| POST | `/v1/api/appointment/cancel-appointment/{id}` | Cancel appointment |

#### Messenger
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/v1/api/messenger/templates/{type}` | Templates (e.g. whatsapp) |
| GET | `/v1/api/messenger/birthdays/{date}` | Birthdays for the day |
| GET | `/v1/api/messenger/appointments/{date}/{messageId}` | Appointments for messaging |

### Project structure

```
├── models/              # DTOs (JSDoc types)
├── ConsultorioMeApiClient.js   # API HTTP client
├── index.js             # Usage examples
└── README.md
```

### Dependencies

Uses only Node.js built-in modules:
- `fetch` (Node 18+)
- `JSON` (serialization)

---

## PT — Português

Projeto de console que demonstra o uso de todos os endpoints da [API Consultorio.me](https://api.consultoriome.com/swagger/v1/swagger.json).

### Pré-requisitos

- Node.js 18.0 ou superior

### Configuração

Configure as credenciais da API via variáveis de ambiente:

```powershell
$env:CONSULTORIO_CLIENT_ID = "seu_client_id"
$env:CONSULTORIO_CLIENT_SECRET = "seu_client_secret"
```

Ou no construtor do cliente:

```javascript
const client = new ConsultorioMeApiClient("seu_client_id", "seu_client_secret");
```

### Executar

```bash
node index.js
```

### Endpoints implementados

#### Auth
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/v1/api/authorization/token` | Obtém token JWT (Basic Auth) |

#### Appointment
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/v1/api/appointment/professionals` | Lista profissionais |
| GET | `/v1/api/appointment/professional-info/{id}` | Info do profissional |
| GET | `/v1/api/appointment/available-times/{id}` | Horários disponíveis |
| POST | `/v1/api/appointment/patient-list` | Lista agendamentos do paciente |
| POST | `/v1/api/appointment/create-appointment` | Cria agendamento |
| POST | `/v1/api/appointment/confirm/{id}/{going}` | Confirma/recusa agendamento |
| POST | `/v1/api/appointment/cancel-appointment/{id}` | Cancela agendamento |

#### Messenger
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/v1/api/messenger/templates/{type}` | Templates (ex: whatsapp) |
| GET | `/v1/api/messenger/birthdays/{date}` | Aniversariantes do dia |
| GET | `/v1/api/messenger/appointments/{date}/{messageId}` | Agendamentos para mensagens |

### Estrutura do projeto

```
├── models/              # DTOs (JSDoc types)
├── ConsultorioMeApiClient.js   # Cliente HTTP da API
├── index.js             # Exemplos de uso
└── README.md
```

### Dependências

Utiliza apenas módulos nativos do Node.js:
- `fetch` (Node 18+)
- `JSON` (serialização)

---

## ES — Español

Proyecto de consola que demuestra el uso de todos los endpoints de la [API Consultorio.me](https://api.consultoriome.com/swagger/v1/swagger.json).

### Prerrequisitos

- Node.js 18.0 o superior

### Configuración

Configure las credenciales de la API mediante variables de entorno:

```powershell
$env:CONSULTORIO_CLIENT_ID = "su_client_id"
$env:CONSULTORIO_CLIENT_SECRET = "su_client_secret"
```

O en el constructor del cliente:

```javascript
const client = new ConsultorioMeApiClient("su_client_id", "su_client_secret");
```

### Ejecutar

```bash
node index.js
```

### Endpoints implementados

#### Auth
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/v1/api/authorization/token` | Obtiene token JWT (Basic Auth) |

#### Appointment
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/v1/api/appointment/professionals` | Lista profesionales |
| GET | `/v1/api/appointment/professional-info/{id}` | Info del profesional |
| GET | `/v1/api/appointment/available-times/{id}` | Horarios disponibles |
| POST | `/v1/api/appointment/patient-list` | Lista de citas del paciente |
| POST | `/v1/api/appointment/create-appointment` | Crear cita |
| POST | `/v1/api/appointment/confirm/{id}/{going}` | Confirmar/rechazar cita |
| POST | `/v1/api/appointment/cancel-appointment/{id}` | Cancelar cita |

#### Messenger
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/v1/api/messenger/templates/{type}` | Plantillas (ej: whatsapp) |
| GET | `/v1/api/messenger/birthdays/{date}` | Cumpleaños del día |
| GET | `/v1/api/messenger/appointments/{date}/{messageId}` | Citas para mensajería |

### Estructura del proyecto

```
├── models/              # DTOs (JSDoc types)
├── ConsultorioMeApiClient.js   # Cliente HTTP de la API
├── index.js             # Ejemplos de uso
└── README.md
```

### Dependencias

Utiliza únicamente módulos nativos de Node.js:
- `fetch` (Node 18+)
- `JSON` (serialización)
