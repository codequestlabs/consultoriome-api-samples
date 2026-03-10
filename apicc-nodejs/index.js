/**
 * EN: Consultorio.me API usage examples. Configure CLIENT_ID and CLIENT_SECRET via environment variables or replace.
 * PT: Exemplos de uso da API Consultorio.me. Configure CLIENT_ID e CLIENT_SECRET via variáveis de ambiente ou substitua.
 * ES: Ejemplos de uso de la API Consultorio.me. Configure CLIENT_ID y CLIENT_SECRET mediante variables de entorno o reemplace.
 */

import { ConsultorioMeApiClient } from './ConsultorioMeApiClient.js';

const clientId = 'lKKQAYCgYZ8okwwQBXUOTMZP4vyEhgzjg0ZGbD1TfX/KuYvZq7oM8BQqU3PHJ6B3oPhIdRFwd3TTJruZDPPOrQ==';
const clientSecret = 'dBqTtw8+gnX+sg1Xz8aNslPWeOu10en7NjGk+0oFWlFFgjzxbBIys7zxJoNAW7urmlufHe5vIB+e425sB7fZ7w==';

const client = new ConsultorioMeApiClient(clientId, clientSecret);

// Helper: API may return PascalCase or camelCase
const prop = (obj, ...keys) => {
  if (!obj) return undefined;
  for (const k of keys) {
    const v = obj[k] ?? obj[k.charAt(0).toUpperCase() + k.slice(1)];
    if (v !== undefined) return v;
  }
  return undefined;
};

console.log('=== Consultorio.me API - Examples ===\n');

// -----------------------------------------------------------------------------
// EN: 1. AUTH - Get token | PT: 1. AUTH - Obter token | ES: 1. AUTH - Obtener token
// -----------------------------------------------------------------------------
await exampleGetToken(client);

// -----------------------------------------------------------------------------
// EN: 2. APPOINTMENT - Professionals | PT: 2. APPOINTMENT - Profissionais | ES: 2. APPOINTMENT - Profesionales
// -----------------------------------------------------------------------------
await exampleListProfessionals(client);

// -----------------------------------------------------------------------------
// EN: 3. APPOINTMENT - Professional info | PT: 3. APPOINTMENT - Info do profissional | ES: 3. APPOINTMENT - Info del profesional
// -----------------------------------------------------------------------------
await exampleProfessionalInfo(client);

// -----------------------------------------------------------------------------
// EN: 4. APPOINTMENT - Available times | PT: 4. APPOINTMENT - Horários disponíveis | ES: 4. APPOINTMENT - Horarios disponibles
// -----------------------------------------------------------------------------
await exampleAvailableTimes(client);

// -----------------------------------------------------------------------------
// EN: 5. APPOINTMENT - Patient list | PT: 5. APPOINTMENT - Lista do paciente | ES: 5. APPOINTMENT - Lista del paciente
// -----------------------------------------------------------------------------
await examplePatientList(client);

// -----------------------------------------------------------------------------
// EN: 6. APPOINTMENT - Create appointment (structure example) | PT: 6. APPOINTMENT - Criar agendamento (exemplo de estrutura) | ES: 6. APPOINTMENT - Crear cita (ejemplo de estructura)
// -----------------------------------------------------------------------------
await exampleCreateAppointment(client);

// -----------------------------------------------------------------------------
// EN: 7. APPOINTMENT - Confirm appointment (structure example) | PT: 7. APPOINTMENT - Confirmar agendamento (exemplo de estrutura) | ES: 7. APPOINTMENT - Confirmar cita (ejemplo de estructura)
// -----------------------------------------------------------------------------
exampleConfirmAppointment(client);

// -----------------------------------------------------------------------------
// EN: 8. APPOINTMENT - Cancel appointment (structure example) | PT: 8. APPOINTMENT - Cancelar agendamento (exemplo de estrutura) | ES: 8. APPOINTMENT - Cancelar cita (ejemplo de estructura)
// -----------------------------------------------------------------------------
exampleCancelAppointment(client);

// -----------------------------------------------------------------------------
// EN: 9. MESSENGER - Templates | PT: 9. MESSENGER - Templates | ES: 9. MESSENGER - Plantillas
// -----------------------------------------------------------------------------
await exampleTemplates(client);

// -----------------------------------------------------------------------------
// EN: 10. MESSENGER - Birthdays | PT: 10. MESSENGER - Aniversariantes | ES: 10. MESSENGER - Cumpleaños
// -----------------------------------------------------------------------------
await exampleBirthdays(client);

// -----------------------------------------------------------------------------
// EN: 11. MESSENGER - Appointments for messaging | PT: 11. MESSENGER - Agendamentos para mensagem | ES: 11. MESSENGER - Citas para mensajería
// -----------------------------------------------------------------------------
await exampleAppointmentsForMessenger(client);

console.log('\n=== Execution completed ===');

// =============================================================================
// EN: Example implementations | PT: Implementações dos exemplos | ES: Implementaciones de los ejemplos
// =============================================================================

async function exampleGetToken(client) {
  console.log('--- 1. AUTH: Get token ---');
  try {
    const token = await client.getTokenAsync();
    client.setBearerToken(String(token).replace(/^"|"$/g, '')); // EN: Remove quotes if JSON string | PT: Remove aspas se vier como JSON | ES: Quitar comillas si viene como JSON
    console.log('Token obtained successfully.');
  } catch (ex) {
    console.log(`Error getting token: ${ex.message}`);
    console.log('Configure CONSULTORIO_CLIENT_ID and CONSULTORIO_CLIENT_SECRET.');
    return;
  }
  console.log();
}

async function exampleListProfessionals(client) {
  console.log('--- 2. APPOINTMENT: List professionals ---');
  try {
    const professionals = await client.getProfessionalsAsync();
    for (const p of professionals) {
      console.log(`  - ${prop(p, 'name')} (ProId: ${prop(p, 'proId')}, Speciality: ${prop(p, 'speciality')})`);
    }
    console.log(`Total: ${professionals.length} professional(s)`);
  } catch (ex) {
    console.log(`Error: ${ex.message}`);
  }
  console.log();
}

async function exampleProfessionalInfo(client) {
  console.log('--- 3. APPOINTMENT: Professional info ---');
  try {
    const professionals = await client.getProfessionalsAsync();
    const firstId = prop(professionals[0], 'id', 'proId');
    if (!firstId) {
      console.log('No professional available for example.');
      return;
    }

    const info = await client.getProfessionalInfoAsync(firstId);
    if (info) {
      const prof = prop(info, 'professional') || info.Professional;
      console.log(`  Name: ${prop(prof, 'name') || prof?.Name}`);
      console.log(`  WebLink: ${prop(info, 'webLink') || info.WebLink}`);
      console.log(`  Language: ${prop(info, 'language') || info.Language}, Country: ${prop(info, 'country') || info.Country}`);
    }
  } catch (ex) {
    console.log(`Error: ${ex.message}`);
  }
  console.log();
}

async function exampleAvailableTimes(client) {
  console.log('--- 4. APPOINTMENT: Available times ---');
  try {
    const professionals = await client.getProfessionalsAsync();
    const firstId = prop(professionals[0], 'id', 'proId');
    if (!firstId) {
      console.log('No professional available.');
      return;
    }

    const timeSlots = await client.getAvailableTimesAsync(firstId);
    const slots = prop(timeSlots, 'slots') || timeSlots?.Slots;
    if (slots?.length) {
      for (const slot of slots.slice(0, 5)) {
        const sid = prop(slot, 'timeSlotId') || slot.TimeSlotId;
        const dt = prop(slot, 'dateTime') || slot.DateTime;
        console.log(`  Slot ${sid}: ${dt}`);
      }
      console.log(`Total slots: ${slots.length}`);
    }
  } catch (ex) {
    console.log(`Error: ${ex.message}`);
  }
  console.log();
}

async function examplePatientList(client) {
  console.log('--- 5. APPOINTMENT: Patient list ---');
  const query = { phone: '11971006053', document: '00000000191' };
  try {
    const items = await client.getPatientListAsync(query);
    for (const item of items) {
      const id = prop(item, 'id');
      const dt = prop(item, 'dateTime');
      const status = prop(item, 'status');
      console.log(`  Id: ${id}, Date: ${dt}, Status: ${status}`);
    }
    console.log(`Total: ${items.length} appointment(s)`);
  } catch (ex) {
    console.log(`Error (expected without real data): ${ex.message}`);
  }
  console.log();
}

async function exampleCreateAppointment(client) {
  console.log('--- 6. APPOINTMENT: Create appointment ---');
  try {
    const professionals = await client.getProfessionalsAsync();
    const firstId = prop(professionals[0], 'id', 'proId');
    if (!firstId) {
      console.log('No professional available.');
      console.log();
      return;
    }

    const timeSlots = await client.getAvailableTimesAsync(firstId);
    const slots = prop(timeSlots, 'slots') || timeSlots?.Slots;
    const firstSlot = slots?.[0];
    if (!firstSlot) {
      console.log('No time slot available.');
      console.log();
      return;
    }

    const proInfo = await client.getProfessionalInfoAsync(firstId);
    const appointmentTypes = prop(proInfo, 'appointmentTypes') || proInfo?.AppointmentTypes;
    const appointmentType = appointmentTypes?.[0] || 'Consulta';

    const slotDt = prop(firstSlot, 'dateTime') || firstSlot.DateTime;
    const slotId = prop(firstSlot, 'timeSlotId') || firstSlot.TimeSlotId;

    const preAppointment = {
      proId: firstId,
      dateTime: slotDt || new Date().toISOString(),
      timeSlotId: parseFloat(slotId) || 0,
      name: 'Peter Parker',
      phone1: '11971006053',
      phone2: '',
      email: 'email@consultoriome.com',
      document: '00000000191',
      responsibleName: 'Peter Parker',
      appointmentType,
      observation: '',
      validationMessage: '',
    };

    const preAppointmentReturn = await client.createAppointmentAsync(preAppointment);
    if (preAppointmentReturn) {
      const id = prop(preAppointmentReturn, 'id') || preAppointmentReturn.Id;
      console.log(`Appointment created successfully: ${id}`);
    } else {
      console.log('Failed to create appointment.');
    }

    console.log('Example call: createAppointmentAsync(preAppointment)');
    console.log(`  ProId: ${preAppointment.proId}, Name: ${preAppointment.name}`);
    console.log('  Get ProId and TimeSlotId from previous APIs.');
  } catch (ex) {
    console.log(`Error: ${ex.message}`);
    console.log('Example call: createAppointmentAsync(preAppointment)');
    console.log('  Get ProId and TimeSlotId from previous APIs.');
  }
  console.log();
}

function exampleConfirmAppointment(client) {
  console.log('--- 7. APPOINTMENT: Confirm appointment ---');
  console.log('Example: confirmAppointmentAsync("appointment_id", "yes")');
  console.log('  going: "yes" to confirm, "no" to decline.');
  console.log();
}

function exampleCancelAppointment(client) {
  console.log('--- 8. APPOINTMENT: Cancel appointment ---');
  console.log('Example: cancelAppointmentAsync("appointment_id")');
  console.log();
}

async function exampleTemplates(client) {
  console.log('--- 9. MESSENGER: Templates ---');
  try {
    const templates = await client.getTemplatesAsync('whatsapp');
    for (const t of templates.slice(0, 5)) {
      const id = prop(t, 'id');
      const name = prop(t, 'name');
      const text = (prop(t, 'text') || t.Text || '').slice(0, 50);
      console.log(`  [${id}] ${name}: ${text}...`);
    }
    console.log(`Total: ${templates.length} template(s)`);
  } catch (ex) {
    console.log(`Error: ${ex.message}`);
  }
  console.log();
}

async function exampleBirthdays(client) {
  console.log('--- 10. MESSENGER: Birthdays ---');
  try {
    const date = new Date().toISOString().slice(0, 10);
    const birthdays = await client.getBirthdaysAsync(date);
    for (const b of birthdays) {
      const name = prop(b, 'name');
      const bday = prop(b, 'birthday') || b.Birthday;
      console.log(`  ${name} - ${bday}`);
    }
    console.log(`Birthdays on ${date}: ${birthdays.length}`);
  } catch (ex) {
    console.log(`Error: ${ex.message}`);
  }
  console.log();
}

async function exampleAppointmentsForMessenger(client) {
  console.log('--- 11. MESSENGER: Appointments for messaging ---');
  try {
    const date = new Date().toISOString().slice(0, 10);
    const appointments = await client.getAppointmentsForMessengerAsync(date, 0);
    for (const a of appointments.slice(0, 3)) {
      const name = prop(a, 'patientName') || a.PatientName;
      const d = prop(a, 'date') || a.Date;
      const type = prop(a, 'type') || a.Type;
      console.log(`  ${name} - ${d} - ${type}`);
    }
    console.log(`Appointments on ${date}: ${appointments.length}`);
  } catch (ex) {
    console.log(`Error: ${ex.message}`);
  }
  console.log();
}
