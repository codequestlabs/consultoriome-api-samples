/**
 * EN: DTOs based on Consultorio.me API Swagger.
 * PT: DTOs baseados no Swagger da API Consultorio.me.
 * ES: DTOs basados en el Swagger de la API Consultorio.me.
 *
 * JavaScript uses plain objects - these are JSDoc type definitions for reference.
 * API uses camelCase for request/response (ASP.NET Core default).
 */

/** @typedef {{ phone?: string, document?: string }} AppointmentQuery */

/** @typedef {{ id?: string, dateTime?: string, status?: string, statusId?: string }} AppointmentItem */

/** @typedef {{ success?: boolean, id?: string, going?: string, message?: string }} AppointmentReturn */

/** @typedef {{ proId?: string, dateTime: string, timeSlotId: number, birthDate?: string, name?: string, phone1?: string, phone2?: string, email?: string, document?: string, responsibleName?: string, appointmentType?: string, observation?: string, validationMessage?: string }} PreAppointment */

/** @typedef {{ result?: boolean, id?: string, message?: string }} PreAppointmentReturn */

/** @typedef {{ proId?: string, id?: string, name?: string, extraInfo?: string, info?: string, speciality?: string, groupSpeciality?: string }} ProfessionalModel */

/** @typedef {{ name?: string, document?: string, extraInfo?: string, additionalInfo?: string, speciality?: string, groupSpeciality?: string }} ProfessionalDetails */

/** @typedef {{ dayOfWeek?: string, hours?: string }} OpeningHoursItem */

/** @typedef {{ address?: string, location?: string, googleMapsUrl?: string }} Directions */

/** @typedef {{ facebook?: string, instagram?: string, whatsApp?: string, phone?: string }} Contacts */

/** @typedef {{ proId?: string, id?: string, professional?: ProfessionalDetails, webLink?: string, openingHours?: OpeningHoursItem[], language?: string, country?: string, timeZone?: string, address?: Directions, contact?: Contacts, services?: string[], appointmentTypes?: string[], paymentInstructions?: string, healthInsuranceInfo?: string, about?: string, completedMessage?: string }} ProInfo */

/** @typedef {{ timeSlotId?: string, dateTime?: string }} TimeSlot */

/** @typedef {{ slots?: TimeSlot[], language?: string, country?: string, id?: string, proId?: string, timeZone?: string }} TimeSlots */

/** @typedef {{ id?: number, name?: string, text?: string }} Item */

/** @typedef {{ name?: string, birthday?: string, phone1?: string, phone2?: string, email?: string }} BirthdayReturn */

/** @typedef {{ id?: string, name?: string, url?: string }} Form */

/** @typedef {{ id?: string, date?: string, type?: string, patientName?: string, phone1?: string, phoneNumber1?: string, phone2?: string, phoneNumber2?: string, email?: string, patientExtraField1?: string, patientExtraField2?: string, clinicName?: string, clinicAddress?: string, clinicLocation?: string, professionalName?: string, professionalExtraInfo?: string, professionalCredentials?: string, linkCreateAppointment?: string, linkConfirmation?: string, linkMeeting?: string, message?: string, customMessageId?: number, forms?: Form[] }} Appointment */
