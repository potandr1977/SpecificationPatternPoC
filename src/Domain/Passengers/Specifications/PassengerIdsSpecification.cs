using Domain.Passengers;
using Domain.Specifications.Core;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Specifications;

/// <summary>
/// Спецификация для отбора по вхождению идентификатора пассажира в заданный массив.
/// </summary>
/// <param name="passnegerIds">Идентификаторы пассажиров.</param>
public class PassengerIdsSpecification<T>(int[] passnegerIds) : Specification<T>
    where T : FiltrationFieldsSet
{
    private readonly int[] passengerIds = passnegerIds;

    public override Expression<Func<T, bool>> ToExpression() =>
        p => passengerIds.Contains(p.Id);
}
