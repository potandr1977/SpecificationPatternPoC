using Domain.Passengers.Models;
using Domain.Services.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Pessengers;

public interface IPassengerSpecificationService
{
    /// <summary>
    /// Получаем данные о номерах мест пассажиров.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<PassengerSeatModel>> GetPessengersSeatAsync(GetPassengerRequest request);

    /// <summary>
    /// Получаем данные о номерах рейсов пассажиров.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<PassengerFlightNumModel>> GetPessengersFlightNoAsync(GetPassengerRequest request);

    /// <summary>
    /// Получаем данные о номерах телефонов пассажиров.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<PassengerPhoneModel>> GetPessengersPhoneAsync(GetPassengerRequest request);

    /// <summary>
    /// Получаем данные о номерах мест пассажиров с помощью спецификаций на основе "сырой" модели.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<PassengerSeatModel>> GetPessengersSeatRawAsync(GetPassengerRequest request);

    /// <summary>
    /// Получаем данные о номерах рейсов пассажиров с помощью спецификаций на основе "сырой" модели.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<PassengerFlightNumModel>> GetPessengersFlightNoRawAsync(GetPassengerRequest request);

    /// <summary>
    /// Получаем данные о номерах телефонов пассажиров с помощью спецификаций на основе "сырой" модели.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<PassengerPhoneModel>> GetPessengersPhoneRawAsync(GetPassengerRequest request);
}
