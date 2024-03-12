using Domain.Passengers.Models;
using Domain.Services.Requests;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Запрос на получение данных о номерах рейсов пассажиров. Без использования спецификаций.
/// </summary>
public record GetPassengerFlightNumNoSpecificationQuery : GetPassengerRequest, IRequest<IReadOnlyCollection<PassengerFlightNumModel>>
{
}
