using System;

namespace Domain.Entities
{
    public record BoardingPass
    {
        public int Id { get; set; }

        public int PassengerId { get; set; }

        public int BookingLegId { get; set; }

        public string Seat { get; set; }

        public DateTime BoardingTime { get; set; }

        public bool PreCheck { get; set; }

        public DateTime UpdateTs { get; set; }

        public Passenger Passenger { get; set;}
    }
}
