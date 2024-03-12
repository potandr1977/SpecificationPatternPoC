using System;

namespace Domain.Entities
{
    public record BookingLegal
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public int FlightId { get; set; }

        public int LegNum { get; set; }

        public bool IsReturning { get; set; }

        public DateTime UpdateTs { get; set; }

        public Flight Flight { get; set; }

        public Booking Booking { get; set; }
    }
}
