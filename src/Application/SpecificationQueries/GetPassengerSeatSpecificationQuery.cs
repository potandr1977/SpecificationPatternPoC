using Domain.Passengers.Models;
using Domain.Services.Requests;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Запрос на получение данных о номерах мест пассажиров. С использованием спецификаций.
/// </summary>
public record GetPassengerSeatSpecificationQuery : GetPassengerRequest, IRequest<IReadOnlyCollection<PassengerSeatModel>>
{
}
