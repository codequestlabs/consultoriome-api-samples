#!/bin/bash
# =============================================================================
# EN: Consultorio.me API - curl examples. Set CONSULTORIO_CLIENT_ID and CONSULTORIO_CLIENT_SECRET.
# PT: API Consultorio.me - exemplos curl. Defina CONSULTORIO_CLIENT_ID e CONSULTORIO_CLIENT_SECRET.
# ES: API Consultorio.me - ejemplos curl. Defina CONSULTORIO_CLIENT_ID y CONSULTORIO_CLIENT_SECRET.
# =============================================================================

BASE_URL="https://api.consultoriome.com"

if [ -z "$CONSULTORIO_CLIENT_ID" ] || [ -z "$CONSULTORIO_CLIENT_SECRET" ]; then
  echo "Error: Set CONSULTORIO_CLIENT_ID and CONSULTORIO_CLIENT_SECRET environment variables."
  exit 1
fi

# Extract first id from JSON array (works with python3, jq, or grep/sed)
extract_first_id() {
  local json="$1"
  if command -v python3 &>/dev/null; then
    echo "$json" | python3 -c "
import sys,json
try:
  d=json.load(sys.stdin)
  p=d[0] if isinstance(d,list) and len(d)>0 else {}
  print(p.get('id') or p.get('Id') or p.get('proId') or p.get('ProId') or '')
except: print('')
" 2>/dev/null
  elif command -v jq &>/dev/null; then
    echo "$json" | jq -r '.[0].id // .[0].Id // .[0].proId // .[0].ProId // empty' 2>/dev/null
  else
    echo "$json" | sed -n 's/.*"\(id\|Id\|proId\|ProId\)"[[:space:]]*:[[:space:]]*"\([^"]*\)".*/\2/p' | head -1
  fi
}

# Extract first timeSlotId from time slots JSON
extract_slot_id() {
  local json="$1"
  if command -v python3 &>/dev/null; then
    echo "$json" | python3 -c "
import sys,json
try:
  d=json.load(sys.stdin)
  slots=d.get('slots') or d.get('Slots') or []
  s=slots[0] if slots else {}
  val=s.get('timeSlotId') or s.get('TimeSlotId')
  print(val if val is not None else '')
except: print('')
" 2>/dev/null
  elif command -v jq &>/dev/null; then
    echo "$json" | jq -r '.slots[0].timeSlotId // .Slots[0].TimeSlotId // empty' 2>/dev/null
  else
    echo "$json" | sed -n 's/.*"timeSlotId"[[:space:]]*:[[:space:]]*"\([^"]*\)".*/\1/p' | head -1
  fi
}

echo "=== Consultorio.me API - Examples ==="
echo ""

# --- 1. AUTH: Get token ---
echo "--- 1. AUTH: Get token ---"
TOKEN_RESPONSE=$(curl -s -X POST "${BASE_URL}/v1/api/authorization/token" \
  -u "${CONSULTORIO_CLIENT_ID}:${CONSULTORIO_CLIENT_SECRET}" \
  -H "Accept: application/json")

TOKEN=$(echo "$TOKEN_RESPONSE" | sed 's/^"//;s/"$//')
if [ -z "$TOKEN" ] || [ "$TOKEN" = "null" ]; then
  echo "Error getting token. Response: $TOKEN_RESPONSE"
  exit 1
fi
echo "Token obtained successfully."
echo ""

# --- 2. APPOINTMENT: List professionals ---
echo "--- 2. APPOINTMENT: List professionals ---"
PROFESSIONALS=$(curl -s -X GET "${BASE_URL}/v1/api/appointment/professionals" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Accept: application/json")
echo "$PROFESSIONALS" | head -c 500
echo "..."
echo ""

# --- 3. APPOINTMENT: Professional info ---
echo "--- 3. APPOINTMENT: Professional info ---"
FIRST_ID=$(extract_first_id "$PROFESSIONALS")
if [ -n "$FIRST_ID" ]; then
  curl -s -X GET "${BASE_URL}/v1/api/appointment/professional-info/${FIRST_ID}" \
    -H "Authorization: Bearer $TOKEN" \
    -H "Accept: application/json" | head -c 500
  echo "..."
else
  echo "No professional available."
fi
echo ""

# --- 4. APPOINTMENT: Available times ---
echo "--- 4. APPOINTMENT: Available times ---"
if [ -n "$FIRST_ID" ]; then
  TIME_SLOTS=$(curl -s -X GET "${BASE_URL}/v1/api/appointment/available-times/${FIRST_ID}" \
    -H "Authorization: Bearer $TOKEN" \
    -H "Accept: application/json")
  echo "$TIME_SLOTS" | head -c 500
  echo "..."
  SLOT_ID=$(extract_slot_id "$TIME_SLOTS")
else
  echo "No professional ID."
fi
echo ""

# --- 5. APPOINTMENT: Patient list ---
echo "--- 5. APPOINTMENT: Patient list ---"
curl -s -X POST "${BASE_URL}/v1/api/appointment/patient-list" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{"phone":"11971006053","document":"00000000191"}' | head -c 300
echo "..."
echo ""

# --- 6. APPOINTMENT: Create appointment ---
echo "--- 6. APPOINTMENT: Create appointment ---"
if [ -n "$FIRST_ID" ] && [ -n "$SLOT_ID" ]; then
  DATE_NOW=$(date -u +"%Y-%m-%dT%H:%M:%S.000Z" 2>/dev/null || date -u +"%Y-%m-%dT%H:%M:%SZ")
  curl -s -X POST "${BASE_URL}/v1/api/appointment/create-appointment" \
    -H "Authorization: Bearer $TOKEN" \
    -H "Content-Type: application/json" \
    -H "Accept: application/json" \
    -d "{
      \"proId\":\"${FIRST_ID}\",
      \"dateTime\":\"${DATE_NOW}\",
      \"timeSlotId\":${SLOT_ID},
      \"name\":\"Sample Patient\",
      \"phone1\":\"11971006053\",
      \"phone2\":\"\",
      \"email\":\"patient@example.com\",
      \"document\":\"00000000191\",
      \"responsibleName\":\"Sample Patient\",
      \"appointmentType\":\"Consulta\",
      \"observation\":\"\",
      \"validationMessage\":\"\"
    }"
  echo ""
else
  echo "Skipped (need professional ID and slot from previous calls)."
  echo "  Get proId from #2, timeSlotId from #4."
fi
echo ""

# --- 7. APPOINTMENT: Confirm appointment (example) ---
echo "--- 7. APPOINTMENT: Confirm appointment ---"
echo "Example: curl -X POST \"${BASE_URL}/v1/api/appointment/confirm/APPOINTMENT_ID/yes\" -H \"Authorization: Bearer \$TOKEN\""
echo "  going: \"yes\" to confirm, \"no\" to decline."
echo ""

# --- 8. APPOINTMENT: Cancel appointment (example) ---
echo "--- 8. APPOINTMENT: Cancel appointment ---"
echo "Example: curl -X POST \"${BASE_URL}/v1/api/appointment/cancel-appointment/APPOINTMENT_ID\" -H \"Authorization: Bearer \$TOKEN\""
echo ""

# --- 9. MESSENGER: Templates ---
echo "--- 9. MESSENGER: Templates ---"
curl -s -X GET "${BASE_URL}/v1/api/messenger/templates/whatsapp" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Accept: application/json" | head -c 500
echo "..."
echo ""

# --- 10. MESSENGER: Birthdays ---
echo "--- 10. MESSENGER: Birthdays ---"
DATE_TODAY=$(date +%Y-%m-%d)
curl -s -X GET "${BASE_URL}/v1/api/messenger/birthdays/${DATE_TODAY}" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Accept: application/json" | head -c 300
echo "..."
echo ""

# --- 11. MESSENGER: Appointments for messaging ---
echo "--- 11. MESSENGER: Appointments for messaging ---"
curl -s -X GET "${BASE_URL}/v1/api/messenger/appointments/${DATE_TODAY}/0" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Accept: application/json" | head -c 300
echo "..."
echo ""

echo "=== Execution completed ==="
