namespace Domain.Passengers.Models;

/// <summary>
/// Модель данных о номере рейса пассажира.
/// </summary>
public record PassengerFlightNumModel
{
    /// <summary>
    /// Ид. пассажира.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Имя пассажира.
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Фамилия пассажира.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Номер рейса.
    /// </summary>
    public string FlightNum { get; init; }
}
