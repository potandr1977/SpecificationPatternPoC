using Domain.Passengers.Models;
using Domain.Pessengers;
using MediatR;

namespace Application.NoSpecification
{
    /// <summary>
    /// Обработчик запросов на получение данных о номерах телефонов пассажиров. С использованием спецификаций.
    /// </summary>
    public class GetPassengerPhoneSpecificationHandler(IPassengerSpecificationService passengerService) : 
        IRequestHandler<GetPassengerPhoneSpecificationQuery, IReadOnlyCollection<PassengerPhoneModel>>
    {
        private readonly IPassengerSpecificationService passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));

        public async Task<IReadOnlyCollection<PassengerPhoneModel>> Handle(GetPassengerPhoneSpecificationQuery request, CancellationToken cancellationToken) =>
            await passengerService.GetPessengersPhoneAsync (request);
    }
}