using System;
using System.Collections.Generic;

namespace Domain.Entities;

public record Account
{
    public int Id { get; set;}

    public string Login { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int? FrequentFlyerId { get; set; }

    public DateTime? UpdateTs { get; set; }

    public ICollection<Passenger> Passengers { get; set; }

    public ICollection<Phone> Phones { get; set; }
}
