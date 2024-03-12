using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Specifications.Core;

/// <summary>
/// Класс переопределяет метод ToExpression таким образом,
/// чтобы на выходе генерировался Expression являющийся результатом логического отрицания
/// переданной в конструкторе исходной спецификации.
/// </summary>
/// <typeparam name="T">Тип модели данных, поля которой будут использованы при фильтрации.</typeparam>
/// <param name="spec">Спецификация, которой нужно сделать инверсию.</param>
public class NotSpecification<T>(Specification<T> spec) : Specification<T>
{
    private readonly Specification<T> spec = spec;

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> expr = spec.ToExpression();

        var notExpression = Expression.Not(expr.Body);

        return Expression.Lambda<Func<T, bool>>(
            notExpression, expr.Parameters.Single());
    }
}
