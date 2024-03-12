using Domain.Passengers.Models;
using Domain.Pessengers;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Обработчик запросов на получение данных о номерах рейсов пассажиров. С использованием спецификаций.
/// </summary>
public class GetPassengerFlightNumSpecificationHandler(IPassengerSpecificationService passengerService) : 
    IRequestHandler<GetPassengerFlightNumSpecificationQuery, IReadOnlyCollection<PassengerFlightNumModel>>
{
    private readonly IPassengerSpecificationService passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));

    public async Task<IReadOnlyCollection<PassengerFlightNumModel>> Handle(GetPassengerFlightNumSpecificationQuery request, CancellationToken cancellationToken) =>
        await passengerService.GetPessengersFlightNoAsync(request);
}