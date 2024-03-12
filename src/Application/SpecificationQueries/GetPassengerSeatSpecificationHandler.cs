using Domain.Passengers.Models;
using Domain.Pessengers;
using MediatR;

namespace Application.NoSpecification
{
    /// <summary>
    /// Обработчик запросов на получение данных о номерах мест пассажиров. С использованием спецификаций.
    /// </summary>
    public class GetPassengerSeatSpecificationHandler(IPassengerSpecificationService passengerService) : 
        IRequestHandler<GetPassengerSeatSpecificationQuery, IReadOnlyCollection<PassengerSeatModel>>
    {
        private readonly IPassengerSpecificationService _passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));

        public async Task<IReadOnlyCollection<PassengerSeatModel>> Handle(GetPassengerSeatSpecificationQuery request, CancellationToken cancellationToken) =>
            await _passengerService.GetPessengersSeatAsync(request);
    }
}