# MediMeet - DDD Medical Appointment System

MediMeet is a medical appointment scheduling system built using Domain-Driven Design (DDD) principles. The system is divided into several bounded contexts, each handling specific business capabilities.

## Architecture Overview

The system follows a clean DDD architecture with the following layers:

- **Domain Layer**: Contains the business logic, domain models, and business rules
- **Application Layer**: Orchestrates use cases and coordinates domain objects
- **Infrastructure Layer**: Handles technical concerns like persistence and external services
- **API Layer**: Exposes the functionality through RESTful endpoints

## Bounded Contexts

### 1. Appointment Booking Module
Handles the core appointment booking process.

- **Domain Models**:
  - Patient (Aggregate Root)
  - Appointment (Aggregate Root)
  - Value Objects: EmailAddress, PhoneNumber, AppointmentTime

- **Key Features**:
  - Book appointments
  - Manage patient information
  - Handle scheduling constraints

### 2. Doctor Availability Module
Manages doctor schedules and availability.

- **Domain Models**:
  - Doctor (Aggregate Root)
  - TimeSlot (Entity)
  - Value Objects: DoctorId, DoctorName, Specialty, TimeRange

- **Key Features**:
  - Manage doctor schedules
  - Define available time slots
  - Handle scheduling conflicts

### 3. Doctor Appointment Management Module
Handles appointment confirmations and management.

- **Domain Models**:
  - AppointmentConfirmation (Aggregate Root)
  - Value Objects: 
    - AppointmentConfirmationId
    - AppointmentId
    - SlotId
    - PatientInfo
    - AppointmentDateTime

- **Key Features**:
  - Confirm appointments
  - Cancel appointments
  - Track appointment status
  - Manage upcoming appointments

### 4. Appointment Confirmation Module
Handles notifications and confirmations.

- **Domain Models**:
  - Notification (Aggregate Root)
  - Value Objects:
    - NotificationId
    - NotificationContent
    - NotificationRecipient
    - NotificationType
    - NotificationStatus

- **Key Features**:
  - Send appointment confirmations
  - Send appointment reminders
  - Handle cancellation notifications
  - Track notification status

## Domain Events

The system uses domain events for cross-boundary communication:

- AppointmentCreatedEvent
- AppointmentConfirmedEvent
- AppointmentCanceledEvent
- NotificationCreatedEvent
- NotificationSentEvent
- NotificationFailedEvent

## Value Objects

Value objects are used to encapsulate domain concepts:

- IDs (AppointmentId, DoctorId, NotificationId, etc.)
- DateTime handling (AppointmentDateTime, TimeRange)
- Contact information (EmailAddress, PhoneNumber)
- Domain-specific types (NotificationContent, PatientInfo)

## Repository Pattern

Each aggregate root has its own repository with domain-focused methods:

- IAppointmentRepository
- IDoctorRepository
- IAppointmentConfirmationRepository
- INotificationRepository

## Getting Started

1. Clone the repository
2. Restore NuGet packages
3. Run the application

## API Endpoints

### Appointment Booking Module
- POST /api/appointment
  - Book a new appointment
- PUT /api/appointment/{id}
  - Update an existing appointment
- DELETE /api/appointment/{id}
  - Cancel an appointment
- GET /api/appointment/{id}
  - Get appointment details
- GET /api/appointment/patient/{patientId}
  - Get patient's appointments

### Doctor Availability Module
- POST /api/availability
  - Add doctor availability slots
- PUT /api/availability/{slotId}
  - Update availability slot
- DELETE /api/availability/{slotId}
  - Remove availability slot
- GET /api/availability/doctor/{doctorId}
  - Get doctor's availability
- GET /api/availability/date/{date}
  - Get available slots by date

### Doctor Appointment Management Module
- POST /api/appointmentconfirmation
  - Create appointment confirmation
- POST /api/appointmentconfirmation/{appointmentId}/confirm
  - Confirm an appointment
- POST /api/appointmentconfirmation/{appointmentId}/cancel
  - Cancel an appointment
- GET /api/appointmentconfirmation/upcoming
  - Get upcoming appointments
- GET /api/appointmentconfirmation/patient/{patientId}
  - Get patient's appointment confirmations

### Appointment Confirmation Module (Notifications)
- POST /api/notification/appointment/confirmation
  - Send appointment confirmation notification
- POST /api/notification/appointment/cancellation
  - Send appointment cancellation notification
- POST /api/notification/appointment/reminder
  - Send appointment reminder notification
- POST /api/notification/resend-failed
  - Resend failed notifications

### Request/Response Examples

#### Create Appointment Confirmation
```http
POST /api/appointmentconfirmation
{
  "appointmentId": "guid",
  "slotId": "guid",
  "patientId": 123,
  "patientName": "John Doe",
  "reservedAt": "2024-03-20T10:00:00Z",
  "comments": "Initial consultation"
}
```

#### Send Appointment Confirmation Notification
```http
POST /api/notification/appointment/confirmation
{
  "recipientEmail": "patient@example.com",
  "recipientName": "John Doe",
  "appointmentDateTime": "2024-03-20T10:00:00Z",
  "doctorName": "Dr. Smith"
}
```

## Best Practices

- Rich domain models with proper encapsulation
- Value objects for immutable concepts
- Domain events for cross-boundary communication
- Clear separation of concerns
- Business rules enforced in the domain layer
- Proper error handling and validation

## Technologies

- .NET 8.0
- MediatR for domain events
- In-memory storage (for demonstration)
- RESTful APIs

# MediMeet - Domain-Driven Design Implementation

## Architecture Overview

This project follows Domain-Driven Design (DDD) principles to create a robust and maintainable medical appointment scheduling system. The system is divided into several bounded contexts, each handling a specific domain of the business.

### Bounded Contexts

1. **Appointment Booking Module**
   - Core domain for patient appointment scheduling
   - Handles appointment creation, confirmation, and cancellation
   - Manages patient information

2. **Doctor Availability Module**
   - Manages doctor schedules and availability
   - Handles time slot management
   - Coordinates with appointment booking

3. **Doctor Appointment Management Module**
   - Doctor-focused appointment management
   - Schedule viewing and management
   - Patient history and appointment details

4. **Appointment Confirmation Module**
   - Handles appointment confirmation workflow
   - Manages notifications and reminders
   - Tracks appointment status changes

### DDD Implementation Guidelines

#### 1. Domain Layer
- Contains business logic and rules
- Implements aggregates, entities, and value objects
- Defines domain events
- No dependencies on external layers

#### 2. Application Layer
- Orchestrates use cases
- Implements application services
- Handles domain events
- Manages transactions

#### 3. Infrastructure Layer
- Implements repositories
- Handles persistence
- Manages external services
- Provides technical capabilities

#### 4. API Layer
- Exposes REST endpoints
- Handles HTTP requests/responses
- Maps DTOs to domain models

### Best Practices

1. **Aggregate Design**
   - Keep aggregates small and focused
   - Ensure consistency boundaries
   - Use domain events for cross-aggregate communication

2. **Value Objects**
   - Make them immutable
   - Validate in constructors
   - Use factory methods for creation

3. **Domain Events**
   - Use for cross-boundary communication
   - Keep them immutable
   - Include only necessary information

4. **Repository Pattern**
   - One repository per aggregate root
   - Return fully-constructed aggregates
   - Handle persistence details

### Project Structure

```
src/
├── Modules/                          # Bounded Contexts
│   ├── AppointmentBookingModule/     # Appointment Booking Context
│   │   ├── Domain/                   # Domain Layer
│   │   ├── Application/              # Application Layer
│   │   ├── Infrastructure/           # Infrastructure Layer
│   │   └── Api/                      # API Layer
│   ├── DoctorAvailabilityModule/     # Doctor Availability Context
│   └── [Other Modules]/              # Other Bounded Contexts
└── Shared/                           # Shared Kernel
    └── Common infrastructure and contracts
```


### Development Guidelines

1. **Domain Model**
   - Use ubiquitous language
   - Implement business rules in domain objects
   - Keep domain logic pure

2. **Bounded Contexts**
   - Clear boundaries between modules
   - Explicit context mapping
   - Independent deployability

3. **Testing**
   - Unit tests for domain logic
   - Integration tests for use cases
   - End-to-end tests for critical paths

4. **Event-Driven**
   - Use domain events for loose coupling
   - Implement event sourcing where appropriate
   - Maintain event consistency

### Getting Started

1. Clone the repository
2. Install dependencies
3. Set up the database
4. Run the application

### Contributing

1. Follow DDD principles
2. Use ubiquitous language
3. Document domain decisions
4. Write tests
