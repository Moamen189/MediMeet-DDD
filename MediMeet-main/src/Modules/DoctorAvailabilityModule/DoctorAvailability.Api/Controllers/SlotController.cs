using DoctorAvailability.Business.DomainModels;
using DoctorAvailability.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAvailability.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SlotController : ControllerBase
{
    private readonly SlotServices _slotServices;

    public SlotController(SlotServices slotServices)
    {
        _slotServices = slotServices;
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] SlotDto slotDto)
    {
        _slotServices.CreateSlot(slotDto);
        return Ok("Slot created successfully");
    }

    [HttpGet("get-all-available")]
    public IActionResult GetAllAvailable()
    {
        return Ok(_slotServices.GetAllAvailable());
    }
    [HttpGet("get-all")]
    public IActionResult GetAll()
    {
        return Ok(_slotServices.GetAll());
    }
}