using Domain.Core;
using Domain.Entities;
using Domain.Passengers.DataAccess;
using Domain.Passengers.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure;

public class PassengerSpecificationProvider(AirContext airContext) : IPassengerSpecificationProvider
{
    private readonly AirContext airContext = airContext ?? throw new ArgumentNullException(nameof(airContext));

    /// <summary>
    /// Метод, который обслужит все запросы, в основе которых сущность Пассажир.
    /// </summary>
    /// <typeparam name="T">Тип выходной модели.</typeparam>
    /// <param name="filter">Выражение для фильрации данных.</param>
    /// <param name="selector">Выражение формирующее выходную модель. PassengerFlightNumModel, PassengerPhoneModel или PassengerSeatModel.</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<T>> GetPassengersWithSpecificationsAsync<T>(
        Expression<Func<Passenger, bool>> filter,
        Expression<Func<Passenger, T>> seletor)
    {
        var query = airContext
            .Passengers
            .Where(filter)
            .Select(seletor);

        return await query.ToListAsync();
    }

    /// <summary>
    /// Метод, который обслужит запросы на основе PassengerRawModel
    /// </summary>
    /// <typeparam name="T">Тип выходной модели.</typeparam>
    /// <param name="filter">Выражение для фильрации данных.</param>
    /// <param name="selector">Выражение формирующее выходную модель. PassengerFlightNumModel, PassengerPhoneModel или PassengerSeatModel.</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<T>> GetPassengersWithSpecificationsRawAsync<T>(
        Expression<Func<PassengerRawModel, bool>> filter,
        Expression<Func<PassengerRawModel, T>> selector)
    {
        var query = from ps in airContext.Passengers

                    // FlightNum
                    join bkngs in airContext.Bookings
                    on ps.BookingId equals bkngs.Id into bookings
                    from bk in bookings.DefaultIfEmpty()

                    let bl = airContext.BookingLegals
                        .Where(x => x.BookingId.Equals(bk.Id))
                        .FirstOrDefault()

                    join flts in airContext.Flights
                      on bl.FlightId equals flts.Id into flights
                    from fl in flights.DefaultIfEmpty()

                    // PhoneNumber
                    join accs in airContext.Accounts
                    on ps.AccountId equals accs.Id into accounts
                    from acc in accounts.DefaultIfEmpty()

                    let phn = airContext.Phones
                        .Where(x => x.AccountId.Equals(acc.Id))
                        .FirstOrDefault()

                    // Seat
                    let bp = airContext.BoardingPasses
                            .Where(x => x.PassengerId.Equals(ps.Id))
                            .FirstOrDefault()

                    select new PassengerRawModel
                    {
                        Id =  ps.Id,
                        FirstName = ps.FirstName,
                        LastName = ps.LastName,
                        UpdateTs = ps.UpdateTs,

                        FlightNum = fl.FlightNo,
                        PhoneNumber = phn != null
                            ? phn.PhoneValue
                            : null,

                        BoardingPassSeat = bp != null
                            ? bp.Seat
                            : null,
                    };

        query = query.Where(filter);

        var resultQuery = query.Select(selector);

        return await resultQuery.ToListAsync();
    }
}
