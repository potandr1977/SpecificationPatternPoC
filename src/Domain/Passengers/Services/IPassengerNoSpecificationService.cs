using Domain.Passengers.Models;
using Domain.Services.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Passengers.Services;

public interface IPassengerNoSpecificationService
{
    /// <summary>
    /// Получаем данные о номерах мест пассажиров.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<PassengerSeatModel>> GetPessengersSeatsAsync(GetPassengerRequest request);

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
}
