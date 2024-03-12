using Domain.Passengers.Models;
using Domain.Passengers.Services;
using MediatR;

namespace Application.NoSpecification;

/// <summary>
/// Обработчик запросов на получение данных о номерах телефонов пассажиров. Без использования спецификаций.
/// </summary>
public class GetPassengerPhoneNoSpecificationHandler(IPassengerNoSpecificationService passengerService) : 
    IRequestHandler<GetPassengerPhoneNoSpecificationQuery, IReadOnlyCollection<PassengerPhoneModel>>
{
    private readonly IPassengerNoSpecificationService passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));

    public async Task<IReadOnlyCollection<PassengerPhoneModel>> Handle(GetPassengerPhoneNoSpecificationQuery request, CancellationToken cancellationToken) =>
        await passengerService.GetPessengersPhoneAsync (request);
}