using Domain.Entities;
using Domain.Passengers;
using Domain.Passengers.DataAccess;
using Domain.Passengers.Models;
using Domain.Services.Requests;
using Domain.Specifications;
using Domain.Specifications.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Pessengers;

public class PassengerSpecificationService(IPassengerSpecificationProvider passengerSpecificationProvider) : IPassengerSpecificationService
{
    private readonly IPassengerSpecificationProvider _passengerSpecificationProvider = 
        passengerSpecificationProvider ?? throw new ArgumentException(nameof(passengerSpecificationProvider));

    /// <summary>
    /// Получаем данные о номерах рейсов пассажиров.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<PassengerFlightNumModel>> GetPessengersFlightNoAsync(GetPassengerRequest request)
    {
        // Запрос, который формирует EF по выражению полученному из спецификации.

        //SELECT p.passenger_id AS "Id", p.first_name AS "FirstName", p.last_name AS "LastName", (
        //    SELECT f.flight_no
        //    FROM postgres_air.booking_leg AS b0
        //    LEFT JOIN postgres_air.flight AS f ON b0.flight_id = f.flight_id
        //     WHERE b.booking_id IS NOT NULL AND b.booking_id = b0.booking_id
        //     LIMIT 1) AS "FlightNum"
        // FROM postgres_air.passenger AS p
        // LEFT JOIN postgres_air.booking AS b ON p.booking_id = b.booking_id
        // WHERE p.passenger_id = ANY(@__passengerIds_0) AND NOT(p.passenger_id = ANY(@__passengerIds_1) AND p.passenger_id = ANY(@__passengerIds_1) IS NOT NULL)

        //"QUERY PLAN"
        //"Nested Loop  (cost=0.87..136740.13 rows=5 width=48)"
        //"  ->  Index Scan using passenger_pkey on passenger p  (cost=0.43..42.29 rows=5 width=20)"
        //"        Index Cond: (passenger_id = ANY ('{1,2,3,4,5}'::integer[]))"
        //"        Filter: ((passenger_id <> ALL ('{2,3}'::integer[])) OR ((passenger_id = ANY ('{2,3}'::integer[])) IS NULL))"
        //"  ->  Index Only Scan using booking_pkey on booking b  (cost=0.43..4.45 rows=1 width=8)"
        //"        Index Cond: (booking_id = p.booking_id)"
        //"  SubPlan 1"
        //"    ->  Limit  (cost=0.42..27335.12 rows=1 width=4)"
        //"          ->  Nested Loop Left Join  (cost=0.42..355351.45 rows=13 width=4)"
        //"                ->  Seq Scan on booking_leg b0  (cost=0.00..355241.70 rows=13 width=4)"
        //"                      Filter: (b.booking_id = booking_id)"
        //"                ->  Index Scan using flight_pkey on flight f  (cost=0.42..8.44 rows=1 width=8)"
        //"                      Index Cond: (flight_id = b0.flight_id)"

        // формируем спецификацию для фильтрации данных.
        var filter = ConstructSpecification<Passenger>(request);

        // получаем данные. EF сгенерирует запрос опираясь на список полей в выходном наборе и переданную спецификацию.
        // При изменении списка полей спецификации EF может радикально менять запрос.
        return _passengerSpecificationProvider.GetPassengersWithSpecificationsAsync(
            filter.ToExpression(),
            p => new PassengerFlightNumModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                FlightNum = p.Booking.BookingLegals.FirstOrDefault().Flight.FlightNo
            });
    }

    /// <summary>
    /// Получаем данные о номерах телефонов пассажиров.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<PassengerPhoneModel>> GetPessengersPhoneAsync(GetPassengerRequest request)
    {
        // Запрос, который формирует EF по выражению полученному из спецификации.

        //SELECT p.passenger_id AS "Id", p.first_name AS "FirstName", p.last_name AS "LastName", (
        //                SELECT p0.phone
        //                FROM postgres_air.phone AS p0
        //                WHERE a.account_id IS NOT NULL AND a.account_id = p0.account_id AND p0.primary_phone
        //                LIMIT 1) AS "PhoneNumber"
        //FROM postgres_air.passenger AS p
        //LEFT JOIN postgres_air.account AS a ON p.account_id = a.account_id
        //WHERE p.passenger_id = ANY(@__passengerIds_0) AND NOT(p.passenger_id = ANY(@__passengerIds_1) AND p.passenger_id = ANY(@__passengerIds_1) IS NOT NULL) AND(p.update_ts < @__dateTo_2 OR p.update_ts IS NULL)

        //"QUERY PLAN"
        //"Nested Loop Left Join  (cost=0.86..41859.75 rows=5 width=48)"
        //"  ->  Index Scan using passenger_pkey on passenger p  (cost=0.43..42.29 rows=5 width=20)"
        //"        Index Cond: (passenger_id = ANY ('{1,2,3,4,5}'::integer[]))"
        //"        Filter: ((passenger_id <> ALL ('{2,3}'::integer[])) OR ((passenger_id = ANY ('{2,3}'::integer[])) IS NULL))"
        //"  ->  Index Only Scan using account_pkey on account a  (cost=0.42..4.44 rows=1 width=4)"
        //"        Index Cond: (account_id = p.account_id)"
        //"  SubPlan 1"
        //"    ->  Limit  (cost=0.00..8359.05 rows=1 width=10)"
        //"          ->  Result  (cost=0.00..8359.05 rows=1 width=10)"
        //"                One-Time Filter: (a.account_id IS NOT NULL)"
        //"                ->  Seq Scan on phone p0  (cost=0.00..8359.05 rows=1 width=10)"
        //"                      Filter: (primary_phone AND (a.account_id = account_id))"

        // формируем спецификацию для фильтрации данных.
        var filter = ConstructSpecification<Passenger>(request);

        return _passengerSpecificationProvider.GetPassengersWithSpecificationsAsync(
            filter.ToExpression(),
            p => new PassengerPhoneModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.Account.Phones.FirstOrDefault(x => x.PrimaryPhone).PhoneValue,
            });
    }

    /// <summary>
    /// Получаем данные о номерах мест пассажиров.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<PassengerSeatModel>> GetPessengersSeatAsync(GetPassengerRequest request)
    {
        // Запрос, который формирует EF по выражению полученному из спецификации.   

        //SELECT p.passenger_id AS "Id", p.first_name AS "FirstName", p.last_name AS "LastName", (
        //    SELECT b.seat
        //    FROM postgres_air.boarding_pass AS b
        //    WHERE p.passenger_id = b.passenger_id
        //     LIMIT 1) AS "BoardingPassSeat"
        // FROM postgres_air.passenger AS p
        // WHERE p.passenger_id = ANY(@__passengerIds_0) AND NOT(p.passenger_id = ANY(@__passengerIds_1) AND p.passenger_id = ANY(@__passengerIds_1) IS NOT NULL) AND(p.update_ts < @__dateTo_2 OR p.update_ts IS NULL)

        //"QUERY PLAN"
        //"Index Scan using passenger_pkey on passenger p  (cost=0.43..961587.41 rows=5 width=48)"
        //"  Index Cond: (passenger_id = ANY ('{1,2,3,4,5}'::integer[]))"
        //"  Filter: ((passenger_id <> ALL ('{2,3}'::integer[])) OR ((passenger_id = ANY ('{2,3}'::integer[])) IS NULL))"
        //"  SubPlan 1"
        //"    ->  Limit  (cost=0.00..192309.02 rows=1 width=3)"
        //"          ->  Seq Scan on boarding_pass b  (cost=0.00..576927.07 rows=3 width=3)"
        //"                Filter: (p.passenger_id = passenger_id)"
        //"JIT:"
        //"  Functions: 13"
        //"  Options: Inlining true, Optimization true, Expressions true, Deforming true"

        var filter = ConstructSpecification<Passenger>(request);

        return _passengerSpecificationProvider.GetPassengersWithSpecificationsAsync(
            filter.ToExpression(),
            p => new PassengerSeatModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BoardingPassSeat = p.BoardingPasses.FirstOrDefault().Seat,
            });
    }

    /// <summary>
    /// Получаем данные о номерах рейсов пассажиров с помощью спецификаций на основе "сырой" модели..
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<PassengerFlightNumModel>> GetPessengersFlightNoRawAsync(GetPassengerRequest request)
        {
        // Запрос, который формирует EF по выражению полученному из спецификации.

        // SELECT p.passenger_id AS "Id", p.first_name AS "FirstName", p.last_name AS "LastName", f.flight_no AS "FlightNum"
        // FROM postgres_air.passenger AS p
        // LEFT JOIN postgres_air.booking AS b ON p.booking_id = b.booking_id
        // LEFT JOIN postgres_air.flight AS f ON(
        //     SELECT b0.flight_id
        //     FROM postgres_air.booking_leg AS b0
        //     WHERE b0.booking_id = b.booking_id
        //     LIMIT 1) = f.flight_id
        // LEFT JOIN postgres_air.account AS a ON p.account_id = a.account_id
        // WHERE p.passenger_id = ANY(@__passengerIds_0)
        // AND NOT(p.passenger_id = ANY(@__passengerIds_1)
        // AND p.passenger_id = ANY(@__passengerIds_1) IS NOT NULL) AND(p.update_ts < @__dateTo_2 OR p.update_ts IS NULL)

        //"QUERY PLAN"
        //"Hash Left Join  (cost=26608.37..36174.63 rows=5 width=20)"
        //"  Hash Cond: ((SubPlan 1) = f.flight_id)"
        //"  ->  Nested Loop Left Join  (cost=0.87..64.54 rows=5 width=24)"
        //"        ->  Index Scan using passenger_pkey on passenger p  (cost=0.43..42.29 rows=5 width=24)"
        //"              Index Cond: (passenger_id = ANY ('{1,2,3,4,5}'::integer[]))"
        //"              Filter: ((passenger_id <> ALL ('{2,3}'::integer[])) OR ((passenger_id = ANY ('{2,3}'::integer[])) IS NULL))"
        //"        ->  Index Only Scan using booking_pkey on booking b  (cost=0.43..4.45 rows=1 width=8)"
        //"              Index Cond: (booking_id = p.booking_id)"
        //"  ->  Hash  (cost=15398.78..15398.78 rows=683178 width=8)"
        //"        ->  Seq Scan on flight f  (cost=0.00..15398.78 rows=683178 width=8)"
        //"  SubPlan 1"
        //"    ->  Limit  (cost=0.00..27326.28 rows=1 width=4)"
        //"          ->  Seq Scan on booking_leg b0  (cost=0.00..355241.70 rows=13 width=4)"
        //"                Filter: (booking_id = b.booking_id)"

        // формируем спецификацию для фильтрации данных.
        var filter = ConstructSpecification<PassengerRawModel>(request);

        // получаем данные. EF сгенерирует запрос опираясь на список полей в выходном наборе и переданную спецификацию.
        // При изменении списка полей и спецификации EF может радикально мзменять запрос.
        return _passengerSpecificationProvider.GetPassengersWithSpecificationsRawAsync(
            filter.ToExpression(),
            p => new PassengerFlightNumModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                FlightNum = p.FlightNum,
            });
    }

    /// <summary>
    /// Получаем данные о номерах мест пассажиров с помощью спецификаций на основе "сырой" модели..
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<PassengerPhoneModel>> GetPessengersPhoneRawAsync(GetPassengerRequest request)
    {
        // Запрос, который формирует EF по выражению полученному из спецификации.

        // SELECT p.passenger_id AS "Id", p.first_name AS "FirstName", p.last_name AS "LastName", (
        //     SELECT p0.phone
        //     FROM postgres_air.phone AS p0
        //     WHERE p0.account_id = a.account_id
        //     LIMIT 1) AS "PhoneNumber"
        // FROM postgres_air.passenger AS p
        // LEFT JOIN postgres_air.booking AS b ON p.booking_id = b.booking_id
        // LEFT JOIN postgres_air.flight AS f ON(
        //     SELECT b0.flight_id
        //     FROM postgres_air.booking_leg AS b0
        //     WHERE b0.booking_id = b.booking_id
        //     LIMIT 1) = f.flight_id
        // LEFT JOIN postgres_air.account AS a ON p.account_id = a.account_id
        // WHERE p.passenger_id = ANY(@__passengerIds_0)
        // AND NOT(p.passenger_id = ANY(@__passengerIds_1)
        // AND p.passenger_id = ANY(@__passengerIds_1) IS NOT NULL) AND(p.update_ts < @__dateTo_2 OR p.update_ts IS NULL)

        //"QUERY PLAN"
        //"Nested Loop Left Join  (cost=0.86..20962.12 rows=5 width=48)"
        //"  ->  Index Scan using passenger_pkey on passenger p  (cost=0.43..42.29 rows=5 width=24)"
        //"        Index Cond: (passenger_id = ANY ('{1,2,3,4,5}'::integer[]))"
        //"        Filter: ((passenger_id <> ALL ('{2,3}'::integer[])) OR ((passenger_id = ANY ('{2,3}'::integer[])) IS NULL))"
        //"  ->  Index Only Scan using account_pkey on account a  (cost=0.42..4.44 rows=1 width=4)"
        //"        Index Cond: (account_id = p.account_id)"
        //"  SubPlan 1"
        //"    ->  Limit  (cost=0.00..4179.52 rows=1 width=10)"
        //"          ->  Seq Scan on phone p0  (cost=0.00..8359.05 rows=2 width=10)"
        //"                Filter: (account_id = a.account_id)"

        // формируем спецификацию для фильтрации данных.
        var filter = ConstructSpecification<PassengerRawModel>(request);

        return _passengerSpecificationProvider.GetPassengersWithSpecificationsRawAsync(
            filter.ToExpression(),
            p => new PassengerPhoneModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
            }) ;
    }

    /// <summary>
    /// Получаем данные о номерах мест пассажиров с помощью спецификаций на основе "сырой" модели.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<PassengerSeatModel>> GetPessengersSeatRawAsync(GetPassengerRequest request)
    {
        // Запрос, который формирует EF по выражению полученному из спецификации.   

        // SELECT p.passenger_id AS "Id", p.first_name AS "FirstName", p.last_name AS "LastName", (
        //     SELECT b1.seat
        //     FROM postgres_air.boarding_pass AS b1
        //     WHERE b1.passenger_id = p.passenger_id
        //     LIMIT 1) AS "BoardingPassSeat"
        // FROM postgres_air.passenger AS p
        // LEFT JOIN postgres_air.booking AS b ON p.booking_id = b.booking_id
        // LEFT JOIN postgres_air.flight AS f ON(
        //     SELECT b0.flight_id
        //     FROM postgres_air.booking_leg AS b0
        //     WHERE b0.booking_id = b.booking_id
        //     LIMIT 1) = f.flight_id
        // LEFT JOIN postgres_air.account AS a ON p.account_id = a.account_id
        // WHERE p.passenger_id = ANY(@__passengerIds_0)
        // AND NOT(p.passenger_id = ANY(@__passengerIds_1)
        // AND p.passenger_id = ANY(@__passengerIds_1) IS NOT NULL) AND(p.update_ts < @__dateTo_2 OR p.update_ts IS NULL)

        //"QUERY PLAN"
        //"Index Scan using passenger_pkey on passenger p  (cost=0.43..961587.41 rows=5 width=48)"
        //"  Index Cond: (passenger_id = ANY ('{1,2,3,4,5}'::integer[]))"
        //"  Filter: ((passenger_id <> ALL ('{2,3}'::integer[])) OR ((passenger_id = ANY ('{2,3}'::integer[])) IS NULL))"
        //"  SubPlan 1"
        //"    ->  Limit  (cost=0.00..192309.02 rows=1 width=3)"
        //"          ->  Seq Scan on boarding_pass b1  (cost=0.00..576927.07 rows=3 width=3)"
        //"                Filter: (passenger_id = p.passenger_id)"
        //"JIT:"
        //"  Functions: 18"
        //"  Options: Inlining true, Optimization true, Expressions true, Deforming true"

        var filter = ConstructSpecification<PassengerRawModel>(request);

        return _passengerSpecificationProvider.GetPassengersWithSpecificationsRawAsync(
            filter.ToExpression(),
            p => new PassengerSeatModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BoardingPassSeat = p.BoardingPassSeat, 
            });
    }

    /// <summary>
    /// Конструируем спецификацию для фильрации данных.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private static Specification<T> ConstructSpecification<T>(GetPassengerRequest request)
        where T : FiltrationFieldsSet
    {
        // Выборка по идентификаторам пассажиров, используем соответствующую спецификацию.
        var filter = (Specification<T>) new IdsSpecification<T>(x => x.Id, request.PassengerIds);

        if (request.PassengerIdsToExclude != null)
        {
            // если заданы идентификаторы подлежащие исключению из выходного набора данных,
            // то переиспользуем спецификацию выборки по идентификаторам инвертировав её, с помощью оператора "!".
            //filter &= !(new PassengerIdsSpecification<T>(request.PassengerIdsToExclude));
            filter &= !(new IdsSpecification<T>(x => x.Id, request.PassengerIdsToExclude));
        }

        if (request.UpdateTsStart.HasValue || request.UpdateTsEnd.HasValue)
        {
            // если заданы границы временного интервала, то фильтруем по датам.
            filter &= new DateIntervalSpecification<T>(x => x.UpdateTs, request.UpdateTsStart, request.UpdateTsEnd);
        }

        return filter;
    }
}

/* отлавливаем крайние запросы к бд
set search_path = postgres_air;

select  datid, query_start, query from pg_stat_activity
 */