using Domain.Passengers;
using Domain.Specifications.Core;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Specifications;

/// <summary>
/// Спецификация для отбора по вхождению даты в заданный интервал.
/// <param name="keySelector">Поле по которому нужно фильтровать.</param>
/// <param name="dateStart">Дата начала периода фильтрации.</param>
/// <param name="dateEnd">Дата окончания периода фильтрации.</param>
/// <param name="defaultValue">Значение по умолчанию.</param>
/// </summary
public class DateIntervalSpecification<T>(
    Expression<Func<T, DateTime?>> keySelector, 
    DateTime? dateStart, DateTime? dateEnd, bool defaultValue = false) : Specification<T>
    where T : FiltrationFieldsSet
{
    private readonly Expression<Func<T, DateTime?>> keySelector = keySelector;
    private readonly DateTime? dateStart = dateStart;
    private readonly DateTime? dateEnd = dateEnd;

    public override Expression<Func<T, bool>> ToExpression()
    {
        var fieldName = ((MemberExpression)keySelector.Body).Member.Name;
        var param = keySelector.Parameters.FirstOrDefault();
        var dateExpr = Expression.Property(param, fieldName);


        var expressionNull = Expression.Constant(null);

        if (dateStart.HasValue && !dateEnd.HasValue)
        {
            var dateFrom = GetStartDate(dateStart.Value);
            var expressionFrom = Expression.Constant(dateFrom);

            var orElse = Expression.OrElse(
                            GreaterThanNulable(dateExpr, expressionFrom),
                            Expression.Equal(dateExpr, expressionNull));

            return Expression.Lambda<Func<T, bool>>(orElse,param);
        }

        if (dateEnd.HasValue && !dateStart.HasValue)
        {
            var dateTo = GetEndDate(dateEnd.Value);
            
            var expressionTo = Expression.Constant(dateTo);

            var orElse = Expression.OrElse(
                    GreaterThanNulable(expressionTo, dateExpr),
                    Expression.Equal(dateExpr, expressionNull));

            return Expression.Lambda<Func<T, bool>>(orElse, param);
        }

        if (dateEnd.HasValue && dateStart.HasValue)
        {
            var dateFrom = GetStartDate(dateStart.Value);
            var expressionFrom = Expression.Constant(dateFrom);

            var dateTo = GetEndDate(dateEnd.Value);
            var expressionTo = Expression.Constant(dateTo);

            var twoDateFilterExpression = Expression.AndAlso(
                    GreaterThanNulable(dateExpr, expressionFrom),
                    GreaterThanNulable(expressionTo, dateExpr));

            var orElse = Expression.OrElse(
                    twoDateFilterExpression,
                    Expression.Equal(dateExpr, expressionNull));

            return Expression.Lambda<Func<T, bool>>(orElse, param);
        }

        return x => defaultValue;
    }

    private static DateTime GetStartDate(DateTime requestTo) =>
        DateTime.SpecifyKind(requestTo.Date, DateTimeKind.Utc);

    private static DateTime GetEndDate(DateTime requestFrom) =>
        DateTime.SpecifyKind(requestFrom.Date.AddDays(1), DateTimeKind.Utc);

    private static BinaryExpression GreaterThanNulable(Expression left, Expression right)
    {
        if (IsNullableType(left.Type) && !IsNullableType(right.Type))
        {
            right = Expression.Convert(right, left.Type);

            return Expression.GreaterThan(left, right);
        }
        
        if (!IsNullableType(left.Type) && IsNullableType(right.Type))
        {
            left = Expression.Convert(left, right.Type);
        }

        return Expression.GreaterThan(left, right);
    }

    private static bool IsNullableType(Type type) => 
        type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
}