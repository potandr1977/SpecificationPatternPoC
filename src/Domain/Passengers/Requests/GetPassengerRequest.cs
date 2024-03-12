using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Services.Requests;

/// <summary>
/// Запрос на выборку данных.
/// </summary>
public record GetPassengerRequest
{
    /// <summary>
    /// Идентификаторы пассажиров, которые должны присутсвовать в выходном наборе.
    /// </summary>
    [Required]
    public int[] PassengerIds { get; init; }

    /// <summary>
    /// Идентификаторы пассажиров, которые НЕ должны присутсвовать в выходном наборе.
    /// </summary>
    public int[] PassengerIdsToExclude { get; init; }

    /// <summary>
    /// Начало интервала для дат изменения данных о пассажире. Дата изменения не присутствует ни в одном выходном наборе.
    /// </summary>
    public DateTime? UpdateTsStart { get; init; }

    /// <summary>
    /// Конец интервала для дат изменения данных о пассажире. Дата изменения не присутствует ни в одном выходном наборе.
    /// </summary>
    public DateTime? UpdateTsEnd { get; init; }
}
