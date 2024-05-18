using System;

namespace Domain.Passengers.Models;

public record PassengerRawModel : FiltrationFieldsSet
{
    /// <summary>
    /// Ид.
    /// </summary>
    //public int Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Номер рейса.
    /// </summary>
    public string FlightNum { get; init; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string PhoneNumber { get; init; }

    /// <summary>
    /// Посадочное место пассажира.
    /// </summary>
    public string BoardingPassSeat { get; init; }

    /// <summary>
    /// Дата изменения.
    /// </summary>
    //public DateTime? UpdateTs { get; set; }
}
