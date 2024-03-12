using Domain.Passengers.Models;
using Domain.Services.Requests;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Запрос на получение данных о номерах мест пассажиров. Без использования спецификаций.
/// </summary>
public record GetPassengerSeatNoSpecificationQuery : GetPassengerRequest, IRequest<IReadOnlyCollection<PassengerSeatModel>>
{
}
