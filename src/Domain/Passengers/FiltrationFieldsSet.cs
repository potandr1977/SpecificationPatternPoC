using System;

namespace Domain.Passengers;

/// <summary>
/// Список полей по которым мы фильруем данные.
/// Интерфес нужен чтобы не плодить спецификации отдельно для сущности Passenger и PassengerRawModel
/// </summary>
public record FiltrationFieldsSet
{
    /// <summary>
    /// Ид.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата изменения.
    /// </summary>
    public DateTime? UpdateTs { get; set; }
}
