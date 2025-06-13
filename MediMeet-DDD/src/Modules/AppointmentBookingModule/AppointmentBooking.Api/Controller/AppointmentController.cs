using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppointmentBooking.Application.CreateAppointment;

namespace AppointmentBooking.Api.Controller;

[ApiController]
[Route("api/v1/appointments")]
public class AppointmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> CreateAppointment(
        [FromBody] CreateAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }
} 