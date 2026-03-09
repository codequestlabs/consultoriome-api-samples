using ConsultorioMeApiExamples;
using ConsultorioMeApiExamples.Models;

// =============================================================================
// EN: Consultorio.me API usage examples. Configure CLIENT_ID and CLIENT_SECRET via environment variables or replace.
// PT: Exemplos de uso da API Consultorio.me. Configure CLIENT_ID e CLIENT_SECRET via variáveis de ambiente ou substitua.
// ES: Ejemplos de uso de la API Consultorio.me. Configure CLIENT_ID y CLIENT_SECRET mediante variables de entorno o reemplace.
// =============================================================================

var clientId = ""; // Your client ID
var clientSecret = ""; // Your client secret

var client = new ConsultorioMeApiClient(clientId, clientSecret);

Console.WriteLine("=== Consultorio.me API - Examples ===\n");

// -----------------------------------------------------------------------------
// EN: 1. AUTH - Get token | PT: 1. AUTH - Obter token | ES: 1. AUTH - Obtener token
// -----------------------------------------------------------------------------
await ExampleGetToken(client);

// -----------------------------------------------------------------------------
// EN: 2. APPOINTMENT - Professionals | PT: 2. APPOINTMENT - Profissionais | ES: 2. APPOINTMENT - Profesionales
// -----------------------------------------------------------------------------
await ExampleListProfessionals(client);

// -----------------------------------------------------------------------------
// EN: 3. APPOINTMENT - Professional info | PT: 3. APPOINTMENT - Info do profissional | ES: 3. APPOINTMENT - Info del profesional
// -----------------------------------------------------------------------------
await ExampleProfessionalInfo(client);

// -----------------------------------------------------------------------------
// EN: 4. APPOINTMENT - Available times | PT: 4. APPOINTMENT - Horários disponíveis | ES: 4. APPOINTMENT - Horarios disponibles
// -----------------------------------------------------------------------------
await ExampleAvailableTimes(client);

// -----------------------------------------------------------------------------
// EN: 5. APPOINTMENT - Patient list | PT: 5. APPOINTMENT - Lista do paciente | ES: 5. APPOINTMENT - Lista del paciente
// -----------------------------------------------------------------------------
await ExamplePatientList(client);

// -----------------------------------------------------------------------------
// EN: 6. APPOINTMENT - Create appointment (structure example) | PT: 6. APPOINTMENT - Criar agendamento (exemplo de estrutura) | ES: 6. APPOINTMENT - Crear cita (ejemplo de estructura)
// -----------------------------------------------------------------------------
await ExampleCreateAppointment(client);

// -----------------------------------------------------------------------------
// EN: 7. APPOINTMENT - Confirm appointment (structure example) | PT: 7. APPOINTMENT - Confirmar agendamento (exemplo de estrutura) | ES: 7. APPOINTMENT - Confirmar cita (ejemplo de estructura)
// -----------------------------------------------------------------------------
ExampleConfirmAppointment(client);

// -----------------------------------------------------------------------------
// EN: 8. APPOINTMENT - Cancel appointment (structure example) | PT: 8. APPOINTMENT - Cancelar agendamento (exemplo de estrutura) | ES: 8. APPOINTMENT - Cancelar cita (ejemplo de estructura)
// -----------------------------------------------------------------------------
ExampleCancelAppointment(client);

// -----------------------------------------------------------------------------
// EN: 9. MESSENGER - Templates | PT: 9. MESSENGER - Templates | ES: 9. MESSENGER - Plantillas
// -----------------------------------------------------------------------------
await ExampleTemplates(client);

// -----------------------------------------------------------------------------
// EN: 10. MESSENGER - Birthdays | PT: 10. MESSENGER - Aniversariantes | ES: 10. MESSENGER - Cumpleaños
// -----------------------------------------------------------------------------
await ExampleBirthdays(client);

// -----------------------------------------------------------------------------
// EN: 11. MESSENGER - Appointments for messaging | PT: 11. MESSENGER - Agendamentos para mensagem | ES: 11. MESSENGER - Citas para mensajería
// -----------------------------------------------------------------------------
await ExampleAppointmentsForMessenger(client);

Console.WriteLine("\n=== Execution completed ===");

// =============================================================================
// EN: Example implementations | PT: Implementações dos exemplos | ES: Implementaciones de los ejemplos
// =============================================================================

static async Task ExampleGetToken(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 1. AUTH: Get token ---");
    try
    {
        var token = await client.GetTokenAsync();
        client.SetBearerToken(token.Trim('"')); // EN: Remove quotes if JSON string | PT: Remove aspas se vier como JSON | ES: Quitar comillas si viene como JSON
        Console.WriteLine("Token obtained successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting token: {ex.Message}");
        Console.WriteLine("Configure CONSULTORIO_CLIENT_ID and CONSULTORIO_CLIENT_SECRET.");
        return;
    }
    Console.WriteLine();
}

static async Task ExampleListProfessionals(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 2. APPOINTMENT: List professionals ---");
    try
    {
        var professionals = await client.GetProfessionalsAsync();
        foreach (var p in professionals)
        {
            Console.WriteLine($"  - {p.Name} (ProId: {p.ProId}, Speciality: {p.Speciality})");
        }
        Console.WriteLine($"Total: {professionals.Count} professional(s)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    Console.WriteLine();
}

static async Task ExampleProfessionalInfo(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 3. APPOINTMENT: Professional info ---");
    try
    {
        var professionals = await client.GetProfessionalsAsync();
        var firstId = professionals.FirstOrDefault()?.Id ?? professionals.FirstOrDefault()?.ProId;
        if (string.IsNullOrEmpty(firstId))
        {
            Console.WriteLine("No professional available for example.");
            return;
        }

        var info = await client.GetProfessionalInfoAsync(firstId);
        if (info != null)
        {
            Console.WriteLine($"  Name: {info.Professional?.Name}");
            Console.WriteLine($"  WebLink: {info.WebLink}");
            Console.WriteLine($"  Language: {info.Language}, Country: {info.Country}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    Console.WriteLine();
}

static async Task ExampleAvailableTimes(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 4. APPOINTMENT: Available times ---");
    try
    {
        var professionals = await client.GetProfessionalsAsync();
        var firstId = professionals.FirstOrDefault()?.Id ?? professionals.FirstOrDefault()?.ProId;
        if (string.IsNullOrEmpty(firstId))
        {
            Console.WriteLine("No professional available.");
            return;
        }

        var timeSlots = await client.GetAvailableTimesAsync(firstId);
        if (timeSlots?.Slots != null)
        {
            foreach (var slot in timeSlots.Slots.Take(5))
            {
                Console.WriteLine($"  Slot {slot.TimeSlotId}: {slot.DateTime}");
            }
            Console.WriteLine($"Total slots: {timeSlots.Slots.Count}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    Console.WriteLine();
}

static async Task ExamplePatientList(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 5. APPOINTMENT: Patient list ---");
    var query = new AppointmentQuery { Phone = "11971006053", Document = "00000000191" };
    try
    {
        var items = await client.GetPatientListAsync(query);
        foreach (var item in items)
        {
            Console.WriteLine($"  Id: {item.Id}, Date: {item.DateTime}, Status: {item.Status}");
        }
        Console.WriteLine($"Total: {items.Count} appointment(s)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error (expected without real data): {ex.Message}");
    }
    Console.WriteLine();
}

static async Task ExampleCreateAppointment(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 6. APPOINTMENT: Create appointment ---");

    var professionals = await client.GetProfessionalsAsync();
    var firstId = professionals.FirstOrDefault()?.Id ?? professionals.FirstOrDefault()?.ProId;
    if (string.IsNullOrEmpty(firstId))
    {
        Console.WriteLine("No professional available.");
        return;
    }

    var timeSlots = await client.GetAvailableTimesAsync(firstId);
    var firstTimeSlot = timeSlots?.Slots?.FirstOrDefault();
    if (firstTimeSlot == null)
    {
        Console.WriteLine("No time slot available.");
        return;
    }

    var proInfo = await client.GetProfessionalInfoAsync(firstId);    

    var preAppointment = new PreAppointment
    {
        ProId = firstId,
        DateTime = firstTimeSlot.DateTime ?? DateTime.UtcNow,
        TimeSlotId = double.Parse(firstTimeSlot.TimeSlotId ?? "0"),
        Name = "Peter Parker",
        Phone1 = "11971006053",    
        Email = "email@consultoriome.com",
        Document = "00000000191",
        //  Phone2 = "",
        //  ResponsibleName = "Sample Patient",
        //  AppointmentType = "",
        //  Observation = "",    
    };

    var preAppointmentReturn = await client.CreateAppointmentAsync(preAppointment);
    if (preAppointmentReturn != null)
    {
        Console.WriteLine($"Appointment created successfully: {preAppointmentReturn.Id}");
    }
    else
    {
        Console.WriteLine("Failed to create appointment.");
    }
 
    Console.WriteLine("Example call: CreateAppointmentAsync(preAppointment)");
    Console.WriteLine($"  ProId: {preAppointment.ProId}, Name: {preAppointment.Name}");
    Console.WriteLine("  Get ProId and TimeSlotId from previous APIs.");
    Console.WriteLine();
}

static void ExampleConfirmAppointment(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 7. APPOINTMENT: Confirm appointment ---");
    Console.WriteLine("Example: ConfirmAppointmentAsync(\"appointment_id\", \"yes\")");
    Console.WriteLine("  going: \"yes\" to confirm, \"no\" to decline.");
    Console.WriteLine();
}

static void ExampleCancelAppointment(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 8. APPOINTMENT: Cancel appointment ---");
    Console.WriteLine("Example: CancelAppointmentAsync(\"appointment_id\")");
    Console.WriteLine();
}

static async Task ExampleTemplates(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 9. MESSENGER: Templates ---");
    try
    {
        var templates = await client.GetTemplatesAsync("whatsapp");
        foreach (var t in templates.Take(5))
        {
            Console.WriteLine($"  [{t.Id}] {t.Name}: {t.Text?[..Math.Min(50, t.Text?.Length ?? 0)]}...");
        }
        Console.WriteLine($"Total: {templates.Count} template(s)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    Console.WriteLine();
}

static async Task ExampleBirthdays(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 10. MESSENGER: Birthdays ---");
    try
    {
        var date = DateTime.Today.ToString("yyyy-MM-dd");
        var birthdays = await client.GetBirthdaysAsync(date);
        foreach (var b in birthdays)
        {
            Console.WriteLine($"  {b.Name} - {b.Birthday}");
        }
        Console.WriteLine($"Birthdays on {date}: {birthdays.Count}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    Console.WriteLine();
}

static async Task ExampleAppointmentsForMessenger(ConsultorioMeApiClient client)
{
    Console.WriteLine("--- 11. MESSENGER: Appointments for messaging ---");
    try
    {
        var date = DateTime.Today.ToString("yyyy-MM-dd");
        var appointments = await client.GetAppointmentsForMessengerAsync(date, messageId: 0);
        foreach (var a in appointments.Take(3))
        {
            Console.WriteLine($"  {a.PatientName} - {a.Date} - {a.Type}");
        }
        Console.WriteLine($"Appointments on {date}: {appointments.Count}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    Console.WriteLine();
}
