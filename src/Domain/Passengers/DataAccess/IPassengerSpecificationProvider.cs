using Domain.Entities;
using Domain.Passengers.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Passengers.DataAccess;

/// <summary>
/// Провайдер данных о пассажирах с использованием спецификаций.
/// </summary>
public interface IPassengerSpecificationProvider
{
    /// <summary>
    /// Метод, который обслужит все запросы, в основе которых сущность Пассажир.
    /// </summary>
    /// <typeparam name="T">Тип выходной модели.</typeparam>
    /// <param name="filter">Выражение для фильрации данных.</param>
    /// <param name="selector">Выражение формирующее выходную модель. PassengerFlightNumModel, PassengerPhoneModel или PassengerSeatModel.</param>
    /// <returns></returns>

    Task<IReadOnlyCollection<T>> GetPassengersWithSpecificationsAsync<T>(
        Expression<Func<Passenger, bool>> filter,
        Expression<Func<Passenger, T>> selector);

    /// <summary>
    /// Метод, который обслужит запросы на основе PassengerRawModel.
    /// </summary>
    /// <typeparam name="T">Тип выходной модели.</typeparam>
    /// <param name="filter">Выражение для фильрации данных.</param>
    /// <param name="selector">Выражение формирующее выходную модель. PassengerFlightNumModel, PassengerPhoneModel или PassengerSeatModel.</param>
    /// <returns></returns>

    Task<IReadOnlyCollection<T>> GetPassengersWithSpecificationsRawAsync<T>(
        Expression<Func<PassengerRawModel, bool>> filter,
        Expression<Func<PassengerRawModel, T>> selector);
}
