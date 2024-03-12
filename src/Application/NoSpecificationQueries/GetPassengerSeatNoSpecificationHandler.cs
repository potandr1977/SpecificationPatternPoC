using Domain.Passengers.Models;
using Domain.Passengers.Services;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Обработчик запросов на получение данных о номерах мест пассажиров. Без использования спецификаций.
/// </summary>
public class GetPassengerSeatNoSpecificationHandler(IPassengerNoSpecificationService passengerService) : 
    IRequestHandler<GetPassengerSeatNoSpecificationQuery, IReadOnlyCollection<PassengerSeatModel>>
{
    private readonly IPassengerNoSpecificationService _passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));

    public async Task<IReadOnlyCollection<PassengerSeatModel>> Handle(GetPassengerSeatNoSpecificationQuery request, CancellationToken cancellationToken) =>
        await _passengerService.GetPessengersSeatsAsync(request);
}