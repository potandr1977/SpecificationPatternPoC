namespace Domain.Passengers.Models;

/// <summary>
/// Модель данных о номере телефона пассажира.
/// </summary>
public record PassengerPhoneModel
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
    /// Номер телефона.
    /// </summary>
    public string PhoneNumber { get; init; }
}
