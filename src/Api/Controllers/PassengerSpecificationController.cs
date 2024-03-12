// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Application.NoSpecification;
using Domain.Passengers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для обработки запросов с использованием спецификаций.
/// </summary>
/// <param name="mediator"></param>
[Route("api/[controller]")]
[ApiController]
public class PassengerSpecificationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetPassengerFlights")]
    public Task<IReadOnlyCollection<PassengerFlightNumModel>> GetPassengerFlightNumAsync([FromQuery]
        GetPassengerFlightNumSpecificationQuery   request) =>
       _mediator.Send(request);

    [HttpGet("GetPassengerPhones")]
    public Task<IReadOnlyCollection<PassengerPhoneModel>> GetPassengerPhonesAsync([FromQuery]
        GetPassengerPhoneSpecificationQuery request) =>
       _mediator.Send(request);

    [HttpGet("GetPassengerSeats")]
    public Task<IReadOnlyCollection<PassengerSeatModel>> GetPassengerSeatsAsync([FromQuery]
        GetPassengerSeatSpecificationQuery request) =>
       _mediator.Send(request);
}
