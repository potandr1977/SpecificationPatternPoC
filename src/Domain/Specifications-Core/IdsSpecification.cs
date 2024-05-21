using Domain.Passengers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Domain.Specifications.Core;

/// <summary>
/// Спецификация для отбора по вхождению идентификатора в заданный массив.
/// </summary>
/// <param name="keySelector">Поле по которому будем фильровать, в следующем виде x=>x.Id .</param>
/// <param name="ids">Идентификаторы пассажиров.</param>
public class IdsSpecification<T>(Expression<Func<T, int>> keySelector, int[] ids) : Specification<T>
    where T : FiltrationFieldsSet
{
    private readonly List<int> Ids = [.. ids];
    private readonly Expression<Func<T, int>> keySelector = keySelector;

    private static MethodInfo ContainsMethodInfo { get => typeof(List<int>).GetMethod(nameof(List<int>.Contains)); }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var call = Expression.Call(
                    Expression.Constant(Ids),
                    ContainsMethodInfo,
                    (MemberExpression)keySelector.Body);

        return Expression.Lambda<Func<T, bool>>(call, keySelector.Parameters);
    }
}
