using Domain.Passengers.Models;
using Domain.Passengers.Services;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Обработчик запроса на получение данных о номерах рейсов пассажиров. Без использования спецификаций.
/// </summary>
public class GetPassengerFlightNumNoSpecificationHandler(IPassengerNoSpecificationService passengerService) : 
    IRequestHandler<GetPassengerFlightNumNoSpecificationQuery, IReadOnlyCollection<PassengerFlightNumModel>>
{
    private readonly IPassengerNoSpecificationService passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));

    public async Task<IReadOnlyCollection<PassengerFlightNumModel>> Handle(GetPassengerFlightNumNoSpecificationQuery request, CancellationToken cancellationToken) =>
        await passengerService.GetPessengersFlightNoAsync (request);
}