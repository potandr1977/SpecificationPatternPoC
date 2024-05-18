using Domain.Passengers;
using Domain.Specifications.Core;
using System;
using System.Linq.Expressions;

namespace Domain.Specifications;

/// <summary>
/// Спецификация для отбора по вхождению даты обновления данных о пассажире (UpdateTs) в заданный интервал.
/// </summary
public class UpdateTsIntervalSpecification<T>(DateTime? updateTsStrart, DateTime? updateTsEnd) : Specification<T>
    where T : FiltrationFieldsSet
{
    private readonly DateTime? updateTsStart = updateTsStrart;
    private readonly DateTime? updateTsEnd = updateTsEnd;

    public override Expression<Func<T, bool>> ToExpression()
    {
        if (updateTsStart.HasValue && !updateTsEnd.HasValue)
        {
            var dateFrom = GetStartDate(updateTsStart.Value);

            return x => x.UpdateTs >= dateFrom || x.UpdateTs == null;
        }

        if (updateTsEnd.HasValue && !updateTsStart.HasValue)
        {
            var dateTo = GetEndDate(updateTsEnd.Value);

            return x => x.UpdateTs < dateTo || x.UpdateTs == null;
        }

        if (updateTsEnd.HasValue && updateTsStart.HasValue)
        {
            var dateFrom = GetStartDate(updateTsStart.Value);
            var dateTo = GetEndDate(updateTsEnd.Value);

            return x =>
                (x.UpdateTs >= dateFrom && x.UpdateTs < dateTo) || x.UpdateTs == null;
        }

        return x => true;
    }

    private static DateTime GetStartDate(DateTime requestTo) => requestTo.Date;

    private static DateTime GetEndDate(DateTime requestFrom) => requestFrom.Date.AddDays(1);
}
