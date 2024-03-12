namespace Domain.Passengers.Models;

/// <summary>
/// Модель данных о номере места пассажира.
/// </summary>
public record PassengerSeatModel
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
    /// Посадочное место пассажира.
    /// </summary>
    public string BoardingPassSeat { get; init; }
}
