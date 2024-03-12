using Domain.Passengers.Models;
using Domain.Services.Requests;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Запрос на получение данных о номерах телефонов пассажиров. С использованием спецификаций.
/// </summary>
public record GetPassengerPhoneSpecificationQuery : GetPassengerRequest, IRequest<IReadOnlyCollection<PassengerPhoneModel>>
{
}
