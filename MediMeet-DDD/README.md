# MediMeet App (Modular Monolith)

This project is a backend system for a doctor appointment booking application. It is designed to handle the logic behind managing and booking appointments for a single doctor. The system is built using a **Modular Monolith** architecture, with each module adhering to a specific architectural style.

---

## Business Requirements

The application must meet the following business requirements:

### 1. **Doctor Availability**
   - **List Slots**: The doctor can list their available time slots.
   - **Add Slots**: The doctor can add new time slots with the following details:
     - `Id`: Unique identifier (Guid)
     - `Time`: Date and time of the slot (e.g., `22/02/2023 04:30 pm`)
     - `DoctorId`: Unique identifier for the doctor (Guid)
     - `DoctorName`: Name of the doctor (string)
     - `IsReserved`: Boolean indicating if the slot is reserved

### 2. **Appointment Booking**
   - **View Available Slots**: Patients can view all available slots.
   - **Book Appointment**: Patients can book an appointment on a free slot. An appointment includes:
     - `Id`: Unique identifier (Guid)
     - `SlotId`: Unique identifier for the time slot (Guid)
     - `PatientId`: Unique identifier for the patient (Guid)
     - `PatientName`: Name of the patient (string)
     - `ReservedAt`: Date and time when the appointment was booked

### 3. **Appointment Confirmation**
   - **Confirmation Notification**: Once an appointment is booked, the system sends a confirmation notification to both the patient and the doctor.
   - **Notification Details**: The notification includes the patient's name, appointment time, and doctor's name.
   - **Implementation**: For this assessment, the notification can be a simple log message.

### 4. **Doctor Appointment Management**
   - **View Appointments**: The doctor can view their upcoming appointments.
   - **Manage Appointments**: The doctor can mark appointments as completed or cancel them if necessary.

### 5. **Data Persistence**
   - In Memeory Database.

---

## Specifications

1. **Public APIs**: No authentication or authorization is required.
2. **Single Doctor**: The system is designed to serve a single doctor only.
3. **Modular Monolith Architecture**: The system is divided into four modules, each with a specific architecture:
   - **Doctor Availability Module**: Traditional Layered Architecture
   - **Appointment Booking Module**: Clean Architecture
   - **Appointment Confirmation Module**: Simplest Architecture Possible
   - **Doctor Appointment Management Module**: Hexagonal Architecture

---

## Evaluation Criteria

The project will be evaluated based on the following criteria:
1. **Correct Implementation**: All business requirements must be implemented correctly.
2. **Architecture Compliance**: Each module must adhere to the specified architectural style.
3. **Modularity and Integration**: Proper modularity and integration between modules.
4. **Code Quality**: Code should be readable, maintainable, and adhere to best practices (e.g., separation of concerns).

---

## How to Run the Project

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/Mohamed-Warda/MediMeet
   cd MediMeet
