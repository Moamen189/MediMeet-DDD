using DoctorAvailability.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAvailability.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorAvailabilityController : ControllerBase
{
    private readonly DoctorAvailabilityService _availabilityService;

    public DoctorAvailabilityController(DoctorAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    [HttpGet("doctors/{doctorId}")]
    public async Task<IActionResult> GetDoctor(int doctorId)
    {
        try
        {
            var doctor = await _availabilityService.GetDoctorAsync(doctorId);
            return Ok(doctor);
        }
        catch (DomainException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("doctors/specialty/{specialty}")]
    public async Task<IActionResult> GetDoctorsBySpecialty(string specialty)
    {
        try
        {
            var doctors = await _availabilityService.GetDoctorsBySpecialtyAsync(specialty);
            return Ok(doctors);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("doctors/{doctorId}/slots")]
    public async Task<IActionResult> GetAvailableSlots(
        int doctorId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        try
        {
            var slots = await _availabilityService.GetAvailableSlotsAsync(doctorId, startDate, endDate);
            return Ok(slots);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("doctors/{doctorId}/slots")]
    public async Task<IActionResult> AddAvailabilitySlot(
        int doctorId,
        [FromBody] AddSlotRequest request)
    {
        try
        {
            await _availabilityService.AddAvailabilitySlotAsync(
                doctorId,
                request.StartTime,
                request.EndTime,
                request.ConsultationFee
            );
            return Ok();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("doctors/{doctorId}/slots/{slotId}")]
    public async Task<IActionResult> RemoveAvailabilitySlot(int doctorId, Guid slotId)
    {
        try
        {
            await _availabilityService.RemoveAvailabilitySlotAsync(doctorId, slotId);
            return Ok();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("doctors/{doctorId}/slots/{slotId}/fee")]
    public async Task<IActionResult> UpdateSlotFee(
        int doctorId,
        Guid slotId,
        [FromBody] UpdateFeeRequest request)
    {
        try
        {
            await _availabilityService.UpdateSlotFeeAsync(doctorId, slotId, request.NewFee);
            return Ok();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("doctors/{doctorId}/slots/{slotId}/reserve")]
    public async Task<IActionResult> ReserveSlot(int doctorId, Guid slotId)
    {
        try
        {
            var success = await _availabilityService.ReserveSlotAsync(doctorId, slotId);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Failed to reserve slot");
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("doctors/{doctorId}/slots/{slotId}/cancel")]
    public async Task<IActionResult> CancelReservation(int doctorId, Guid slotId)
    {
        try
        {
            var success = await _availabilityService.CancelReservationAsync(doctorId, slotId);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Failed to cancel reservation");
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class AddSlotRequest
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal ConsultationFee { get; set; }
}

public class UpdateFeeRequest
{
    public decimal NewFee { get; set; }
} 