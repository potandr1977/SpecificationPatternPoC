using Domain.Passengers;
using Domain.Specifications.Core;
using System;
using System.Linq.Expressions;

namespace Domain.Specifications;

/// <summary>
/// Спецификация для отбора по вхождению даты обновления данных в заданный интервал.
/// <param name="fieldName">Наименование поля по которому нужно фильтровать.</param>
/// <param name="updateTsStart">Дата начала периода фильтрации.</param>
/// <param name="updateTsEnd">Дата окончания периода фильтрации.</param>
/// </summary
public class DateIntervalSpecification<T>(string fieldName, DateTime? updateTsStrart, DateTime? updateTsEnd) : Specification<T>
    where T : new()
{
    private readonly DateTime? updateTsStart = updateTsStrart;
    private readonly DateTime? updateTsEnd = updateTsEnd;

    public override Expression<Func<T, bool>> ToExpression()
    {
        var param = Expression.Parameter(typeof(T), fieldName);
       
        var expressionNull = Expression.Constant(null);

        if (updateTsStart.HasValue && !updateTsEnd.HasValue)
        {
            var dateFrom = GetStartDate(updateTsStart.Value);
            var expressionFrom = Expression.Constant(dateFrom);

            var orElse = Expression.OrElse(
                            GreaterThanNulable(Expression.Property(param, fieldName), expressionFrom),
                            Expression.Equal(Expression.Property(param, fieldName), expressionNull));

            //equivalent return x => x.UpdateTs >= dateFrom || x.UpdateTs == null;
            return Expression.Lambda<Func<T, bool>>(orElse,param);
        }

        if (updateTsEnd.HasValue && !updateTsStart.HasValue)
        {
            var dateTo = GetEndDate(updateTsEnd.Value);
            
            var expressionTo = Expression.Constant(dateTo);

            var orElse = Expression.OrElse(
                    GreaterThanNulable(expressionTo, Expression.Property(param, fieldName)),
                    Expression.Equal(Expression.Property(param, fieldName), expressionNull));

            //equivalent return x => x.UpdateTs < dateTo || x.UpdateTs == null;
            return Expression.Lambda<Func<T, bool>>(orElse, param);
        }

        if (updateTsEnd.HasValue && updateTsStart.HasValue)
        {
            var dateFrom = GetStartDate(updateTsStart.Value);
            var expressionFrom = Expression.Constant(dateFrom);

            var dateTo = GetEndDate(updateTsEnd.Value);
            var expressionTo = Expression.Constant(dateTo);

            var twoDateFilterExpression = Expression.AndAlso(
                    GreaterThanNulable(Expression.Property(param, fieldName), expressionFrom),
                    GreaterThanNulable(expressionTo,Expression.Property(param, fieldName)));

            var orElse = Expression.OrElse(
                    twoDateFilterExpression,
                    Expression.Equal(Expression.Property(param, fieldName), expressionNull));

            //equivalent return x => (x.UpdateTs >= dateFrom && x.UpdateTs < dateTo) || x.UpdateTs == null;
            return Expression.Lambda<Func<T, bool>>(orElse, param);
        }

        return x => true;
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