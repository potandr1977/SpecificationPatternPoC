using Domain.Passengers.Models;
using Domain.Services.Requests;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Запрос на получение данных о номерах телефонов пассажиров. Без использования спецификаций.
/// </summary>
public record GetPassengerPhoneNoSpecificationQuery : GetPassengerRequest, IRequest<IReadOnlyCollection<PassengerPhoneModel>>
{
}
