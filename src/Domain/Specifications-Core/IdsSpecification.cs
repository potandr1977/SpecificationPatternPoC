using Domain.Passengers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Specifications.Core;

/// <summary>
/// Спецификация для отбора по вхождению идентификатора в заданный массив.
/// </summary>
/// <param name="fieldName">Наименование поля по которому нужно фильтровать.</param>
/// <param name="passnegerIds">Идентификаторы пассажиров.</param>
public class IdsSpecification<T>(string fieldName, int[] passnegerIds) : Specification<T>
    where T : IFiltrationFieldsSet
{
    private readonly List<int> Ids = [.. passnegerIds];

    public override Expression<Func<T, bool>> ToExpression()
    {
        var param = Expression.Parameter(typeof(T), fieldName);
        var method = Ids.GetType().GetMethod("Contains");
        var call = Expression.Call(Expression.Constant(Ids), method, Expression.Property(param, fieldName));

        return Expression.Lambda<Func<T, bool>>(call, param);
    }
}
