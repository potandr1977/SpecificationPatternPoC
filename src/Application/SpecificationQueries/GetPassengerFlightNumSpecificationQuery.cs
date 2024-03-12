using Domain.Passengers.Models;
using Domain.Services.Requests;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Запрос на получение данных о номерах рейсов пассажиров. С использованием спецификаций.
/// </summary>
public record GetPassengerFlightNumSpecificationQuery : GetPassengerRequest, IRequest<IReadOnlyCollection<PassengerFlightNumModel>>
{
}
