# Consultorio.me API - Curl Examples

**EN:** Scripts with curl/HTTP calls demonstrating all [Consultorio.me API](https://api.consultoriome.com/swagger/v1/swagger.json) endpoints.  
**PT:** Scripts com chamadas curl/HTTP que demonstram todos os endpoints da [API Consultorio.me](https://api.consultoriome.com/swagger/v1/swagger.json).  
**ES:** Scripts con llamadas curl/HTTP que demuestran todos los endpoints de la [API Consultorio.me](https://api.consultoriome.com/swagger/v1/swagger.json).

---

## Prerequisites | Pré-requisitos | Requisitos previos

**EN:** `curl` (or PowerShell on Windows)  
**PT:** `curl` (ou PowerShell no Windows)  
**ES:** `curl` (o PowerShell en Windows)

**EN:** For `run-examples.sh`: bash, `curl`. Optional: `python3` or `jq` for JSON parsing (create appointment step).  
**PT:** Para `run-examples.sh`: bash, `curl`. Opcional: `python3` ou `jq` para parse de JSON (etapa criar agendamento).  
**ES:** Para `run-examples.sh`: bash, `curl`. Opcional: `python3` o `jq` para parsear JSON (paso crear cita).

---

## Configuration | Configuração | Configuración

**EN:** Set the API credentials as environment variables:  
**PT:** Defina as credenciais da API como variáveis de ambiente:  
**ES:** Configure las credenciales de la API como variables de entorno:

**Bash (Linux/macOS/Git Bash):**
```bash
export CONSULTORIO_CLIENT_ID="your_client_id"
export CONSULTORIO_CLIENT_SECRET="your_client_secret"
```

**PowerShell (Windows):**
```powershell
$env:CONSULTORIO_CLIENT_ID = "your_client_id"
$env:CONSULTORIO_CLIENT_SECRET = "your_client_secret"
```

---

## Run | Executar | Ejecutar

**Bash (Linux/macOS/Git Bash):**
```bash
chmod +x run-examples.sh
./run-examples.sh
```

**PowerShell (Windows):**
```powershell
.\run-examples.ps1
```

---

## Implemented endpoints | Endpoints implementados | Endpoints implementados

| # | Method | Endpoint | EN | PT | ES |
|---|--------|----------|----|----|-----|
| 1 | POST | `/v1/api/authorization/token` | Get JWT token | Obtém token JWT | Obtiene token JWT |
| 2 | GET | `/v1/api/appointment/professionals` | List professionals | Lista profissionais | Lista profesionales |
| 3 | GET | `/v1/api/appointment/professional-info/{id}` | Professional info | Info do profissional | Info del profesional |
| 4 | GET | `/v1/api/appointment/available-times/{id}` | Available times | Horários disponíveis | Horarios disponibles |
| 5 | POST | `/v1/api/appointment/patient-list` | Patient list | Lista do paciente | Lista del paciente |
| 6 | POST | `/v1/api/appointment/create-appointment` | Create appointment | Cria agendamento | Crea cita |
| 7 | POST | `/v1/api/appointment/confirm/{id}/{going}` | Confirm appointment | Confirma agendamento | Confirma cita |
| 8 | POST | `/v1/api/appointment/cancel-appointment/{id}` | Cancel appointment | Cancela agendamento | Cancela cita |
| 9 | GET | `/v1/api/messenger/templates/{type}` | Templates | Templates | Plantillas |
| 10 | GET | `/v1/api/messenger/birthdays/{date}` | Birthdays | Aniversariantes | Cumpleaños |
| 11 | GET | `/v1/api/messenger/appointments/{date}/{messageId}` | Appointments for messaging | Agendamentos para mensagem | Citas para mensajería |

---

## Project structure | Estrutura do projeto | Estructura del proyecto

```
├── run-examples.sh     # Bash script (Linux/macOS/Git Bash)
├── run-examples.ps1    # PowerShell script (Windows)
├── curl-commands.txt   # Individual curl commands for reference
└── README.md
```

---

## Reference: Individual curl commands | Referência: Comandos curl individuais | Referencia: Comandos curl individuales

See `curl-commands.txt` for copy-paste curl commands. Replace placeholders:

- `YOUR_CLIENT_ID`, `YOUR_CLIENT_SECRET` – API credentials
- `YOUR_TOKEN` – JWT from step 1
- `PROFESSIONAL_ID` – From step 2 or 3
- `APPOINTMENT_ID` – From create/patient-list response
- Date format: `yyyy-MM-dd` (e.g. `2025-03-09`)
