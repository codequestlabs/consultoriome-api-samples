# =============================================================================
# EN: Consultorio.me API - curl examples. Set CONSULTORIO_CLIENT_ID and CONSULTORIO_CLIENT_SECRET.
# PT: API Consultorio.me - exemplos curl. Defina CONSULTORIO_CLIENT_ID e CONSULTORIO_CLIENT_SECRET.
# ES: API Consultorio.me - ejemplos curl. Defina CONSULTORIO_CLIENT_ID y CONSULTORIO_CLIENT_SECRET.
# =============================================================================

$BaseUrl = "https://api.consultoriome.com"

$ClientId = $env:CONSULTORIO_CLIENT_ID
$ClientSecret = $env:CONSULTORIO_CLIENT_SECRET

if (-not $ClientId -or -not $ClientSecret) {
    Write-Host "Error: Set CONSULTORIO_CLIENT_ID and CONSULTORIO_CLIENT_SECRET environment variables."
    exit 1
}

$Auth = [Convert]::ToBase64String([Text.Encoding]::UTF8.GetBytes("${ClientId}:${ClientSecret}"))

Write-Host "=== Consultorio.me API - Examples ===" -ForegroundColor Cyan
Write-Host ""

# --- 1. AUTH: Get token ---
Write-Host "--- 1. AUTH: Get token ---"
$tokenResponse = Invoke-RestMethod -Uri "$BaseUrl/v1/api/authorization/token" -Method Post `
    -Headers @{ "Authorization" = "Basic $Auth"; "Accept" = "application/json" }

$Token = $tokenResponse -replace '^"|"$', ''
Write-Host "Token obtained successfully."
Write-Host ""

$headers = @{
    "Authorization" = "Bearer $Token"
    "Accept"        = "application/json"
}

# --- 2. APPOINTMENT: List professionals ---
Write-Host "--- 2. APPOINTMENT: List professionals ---"
$professionals = Invoke-RestMethod -Uri "$BaseUrl/v1/api/appointment/professionals" -Method Get -Headers $headers
$professionals | ConvertTo-Json -Depth 3 | Write-Host
Write-Host ""

# --- 3. APPOINTMENT: Professional info ---
Write-Host "--- 3. APPOINTMENT: Professional info ---"
$p0 = $professionals[0]
$firstId = if ($p0.id) { $p0.id } elseif ($p0.Id) { $p0.Id } elseif ($p0.proId) { $p0.proId } else { $p0.ProId }
if ($firstId) {
    $proInfo = Invoke-RestMethod -Uri "$BaseUrl/v1/api/appointment/professional-info/$firstId" -Method Get -Headers $headers
    $proInfo | ConvertTo-Json -Depth 3 | Write-Host
} else {
    Write-Host "No professional available."
}
Write-Host ""

# --- 4. APPOINTMENT: Available times ---
Write-Host "--- 4. APPOINTMENT: Available times ---"
$slotId = $null
if ($firstId) {
    $timeSlots = Invoke-RestMethod -Uri "$BaseUrl/v1/api/appointment/available-times/$firstId" -Method Get -Headers $headers
    $timeSlots | ConvertTo-Json -Depth 3 | Write-Host
    $slots = if ($timeSlots.slots) { $timeSlots.slots } else { $timeSlots.Slots }
    if ($slots -and $slots.Count -gt 0) {
        $s0 = $slots[0]
        $slotId = if ($s0.timeSlotId) { $s0.timeSlotId } else { $s0.TimeSlotId }
    }
}
Write-Host ""

# --- 5. APPOINTMENT: Patient list ---
Write-Host "--- 5. APPOINTMENT: Patient list ---"
$body = @{ phone = "11971006053"; document = "00000000191" } | ConvertTo-Json
$patientList = Invoke-RestMethod -Uri "$BaseUrl/v1/api/appointment/patient-list" -Method Post `
    -Headers $headers -ContentType "application/json" -Body $body
$patientList | ConvertTo-Json -Depth 3 | Write-Host
Write-Host ""

# --- 6. APPOINTMENT: Create appointment ---
Write-Host "--- 6. APPOINTMENT: Create appointment ---"
if ($firstId -and $slotId) {
    $dateNow = [DateTime]::UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
    $createBody = @{
        proId             = $firstId
        dateTime          = $dateNow
        timeSlotId        = [double]$slotId
        name              = "Sample Patient"
        phone1            = "11971006053"
        phone2            = ""
        email             = "patient@example.com"
        document          = "00000000191"
        responsibleName   = "Sample Patient"
        appointmentType   = "Consulta"
        observation       = ""
        validationMessage = ""
    } | ConvertTo-Json
    try {
        $createResult = Invoke-RestMethod -Uri "$BaseUrl/v1/api/appointment/create-appointment" -Method Post `
            -Headers $headers -ContentType "application/json" -Body $createBody
        $createResult | ConvertTo-Json | Write-Host
    } catch {
        Write-Host "Error: $_"
    }
} else {
    Write-Host "Skipped (need professional ID and slot from previous calls)."
}
Write-Host ""

# --- 7. APPOINTMENT: Confirm appointment (example) ---
Write-Host "--- 7. APPOINTMENT: Confirm appointment ---"
Write-Host "Example: Invoke-RestMethod -Uri `"$BaseUrl/v1/api/appointment/confirm/APPOINTMENT_ID/yes`" -Method Post -Headers `$headers"
Write-Host "  going: `"yes`" to confirm, `"no`" to decline."
Write-Host ""

# --- 8. APPOINTMENT: Cancel appointment (example) ---
Write-Host "--- 8. APPOINTMENT: Cancel appointment ---"
Write-Host "Example: Invoke-RestMethod -Uri `"$BaseUrl/v1/api/appointment/cancel-appointment/APPOINTMENT_ID`" -Method Post -Headers `$headers"
Write-Host ""

# --- 9. MESSENGER: Templates ---
Write-Host "--- 9. MESSENGER: Templates ---"
$templates = Invoke-RestMethod -Uri "$BaseUrl/v1/api/messenger/templates/whatsapp" -Method Get -Headers $headers
$templates | ConvertTo-Json -Depth 2 | Write-Host
Write-Host ""

# --- 10. MESSENGER: Birthdays ---
Write-Host "--- 10. MESSENGER: Birthdays ---"
$dateToday = Get-Date -Format "yyyy-MM-dd"
$birthdays = Invoke-RestMethod -Uri "$BaseUrl/v1/api/messenger/birthdays/$dateToday" -Method Get -Headers $headers
$birthdays | ConvertTo-Json -Depth 2 | Write-Host
Write-Host ""

# --- 11. MESSENGER: Appointments for messaging ---
Write-Host "--- 11. MESSENGER: Appointments for messaging ---"
$messengerApps = Invoke-RestMethod -Uri "$BaseUrl/v1/api/messenger/appointments/$dateToday/0" -Method Get -Headers $headers
$messengerApps | ConvertTo-Json -Depth 2 | Write-Host
Write-Host ""

Write-Host "=== Execution completed ===" -ForegroundColor Cyan
