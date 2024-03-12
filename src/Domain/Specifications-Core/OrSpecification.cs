using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Specifications.Core;

/// <summary>
/// Класс переопределяет метод ToExpression таким образом,
/// чтобы на выходе генерировался Expression являющийся результатом дизъюнкции
/// двух переданных в конструкторе спецификаций.
/// </summary>
/// <typeparam name="T">Тип модели данных, поля которой будут использованы при фильтрации.</typeparam>
/// <param name="left">Спецификация стоящая в выражении слева.</param>
/// <param name="right">Спецификация стоящая в выражении справа.</param>
public class OrSpecification<T>(Specification<T> left, Specification<T> right) : Specification<T>
{
    private readonly Specification<T> left = left;
    private readonly Specification<T> right = right;

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = left.ToExpression();
        Expression<Func<T, bool>> rightExpression = right.ToExpression();


        ParameterExpression leftParam = leftExpression.Parameters.FirstOrDefault()
            ?? throw new Exception("Левый параметр пуст!");

        if (rightExpression.Parameters.FirstOrDefault() == null)
            throw new Exception("Правый параметр пуст!");

        if (ReferenceEquals(leftParam, rightExpression.Parameters.FirstOrDefault()))
        {
            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(leftExpression.Body, rightExpression.Body), leftParam);
        }

        return Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(
                leftExpression.Body,
                Expression.Invoke(rightExpression, leftParam)), leftParam);
    }
}
